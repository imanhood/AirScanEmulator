using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirScanEmulator
{
    public class AirScan : TouchPictureBox
    {
        private readonly DBSCAN _dbSCAN;
        public AirScan(Panel panel, Random rnd, DBSCAN dbSCAN, TrackBar interval, double angleOffset = 0d)
        {
            _dbSCAN = dbSCAN;
            this.Points = new List<Point>();

            this.BackColor = Color.FromArgb(rnd.Next(200), rnd.Next(200), rnd.Next(200)); // Set your image here
            this.Size = new System.Drawing.Size(30, 30);
            this.Location = new System.Drawing.Point(rnd.Next(panel.Width - 30), rnd.Next(panel.Height - 30));
            this.AngleOffset = angleOffset;

            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, this.Width, this.Height);
            this.Region = new Region(path);

            // Add PictureBox to the framePanel
            panel.Controls.Add(this);
            Manager = new AirScanManager(this, _dbSCAN, interval);
        }
        public int Resolution { get; set; } = 360;
        public double RayCastLength { get; set; } = 100000;
        public double AngleOffset { get; set; } = 0;
        public double FieldOfViewBeginAngle { get; set; } = 0;
        public double FieldOfViewEndAngle { get; set; } = 360;
        public List<Point> Points { get; set; }
        public List<Point> PointsFromZero => this.Points?.Select(x => new Point(x.X - this.Left, x.Y - this.Top)).ToList();
        public AirScanManager Manager { get; set; }
        public IEnumerable<Line> Lines =>
            Points.Select(x => new Line(this.GetCentroid(), x));

        public bool Locked { get; set; } = false;

        private delegate void ExecuteRayCastCallback(RayCast rayCast);
        public async Task<bool> ExecuteRayCast(List<Rectangle> obstacles, int? rayCastValue = null)
        {
            rayCastValue = rayCastValue ?? this.Resolution;
            for (int i = 0; i < rayCastValue.Value; i++)
            {
                var angle = i * ((this.FieldOfViewEndAngle - this.FieldOfViewBeginAngle) / rayCastValue.Value) + this.FieldOfViewBeginAngle;
                var rayCast = new RayCast(this.GetCentroid(), angle, this.RayCastLength);
                ExecuteRayCastCallback callback = Callback;
                ThreadPool.QueueUserWorkItem((state) =>
                {
                    try
                    {
                        if (rayCast.Execute(obstacles).Result && rayCast.HitLength > 0)
                        {
                            callback(rayCast);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                });
            }
            return true;
        }

        private void Callback(RayCast raycast)
        {
            if(this.Locked)
                return;
            try
            {
                this.Locked = true;
                this.Points.Add(raycast.StartPoint.GetEndPoint(this.Manager.AngleOffset + raycast.Angle, raycast.HitLength));
            }
            catch (Exception e)
            {
                this.Points = new List<Point>();
            }
            finally
            {
                this.Locked = false;
            }
        }

        public async Task<bool> ExecuteRaycast(List<Rectangle> obstacles, bool cleanPoints, int? raycast = null)
        {
            if (this.Locked == false)
            {
                try
                {
                        this.Locked = true;
                        this.Points.Clear();
                }
                catch (Exception e)
                {
                    this.Points = new List<Point>();
                }
                finally
                {
                    this.Locked = false;
                }
            }
            return await this.ExecuteRayCast(obstacles, raycast);
        }
    }
}
