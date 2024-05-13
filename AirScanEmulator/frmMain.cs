using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using static AirScanEmulator.frmMain;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using Newtonsoft.Json.Linq;

namespace AirScanEmulator
{
    public partial class frmMain : Form
    {
        private List<Point> mlPoints { get; set; }
        private readonly List<AirScan> _airScans;
        private readonly List<Hand> _hands;
        private IEnumerable<TouchPoint> _touchPoints;
        //private readonly Timer timer;
        private readonly Random rnd;
        private bool calibration = false;
        private readonly DBSCAN _dbSCAN;
        private readonly TouchPointManager _touchPointManager;
        public frmMain()
        {
            InitializeComponent();
            
            // DBSCAN parameters
            // Create DBSCAN instance and perform clustering
            _dbSCAN = new DBSCAN(epsilon.Value, minPts.Value);

            rnd = new Random();

            mlPoints = new List<Point>();
            _airScans = new List<AirScan>() { new AirScan(mainPanel, rnd, _dbSCAN, interval) { Resolution = resolution.Value}};
            _hands = new List<Hand> { new Hand(mainPanel, handSize.Value, rnd) { Visible = chkHand.Checked } };

            _touchPointManager = new TouchPointManager();
        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {

            if (chkPaint.Checked == false)
                return;
            
            //if (calibration == false)
            //    return;

            if (_airScans == null)
                return;

            // Draw all the lines stored in the list
            Pen pen = new Pen(Color.Black, 1);
            try
            {
                foreach (var airScan in _airScans)
                {
                    if (optPoint.Checked)
                        pen.Width = 3;
                    else
                        pen.Width = 1;
                    pen.Color = airScan.BackColor;

                    if (optNoPoints.Checked == false)
                    {
                        //airScan.Locked = true;
                        var airScanCentroid = airScan.GetCentroid();
                        //var lines = airScan.Points.Select(x => new Line(, x));
                        var points = airScan.Points; //FromZero.Select(x => new Point(x.X, x.Y).Move(airScan.Location));

                        //airScan.Locked = false;
                        if (points != null && points.Any())
                        {
                            foreach (var point in points)
                            {
                                if (optPoint.Checked)
                                    e.Graphics.DrawEllipse(pen, new Rectangle(point.X - 4, point.Y - 4, 4, 4));
                                else
                                    e.Graphics.DrawLine(pen, airScanCentroid, point);
                            }
                        }
                    }

                    if (chkCentroids.Checked)
                    {
                        var centroids = airScan.Manager.CalibratedCentroids.Select(x => x.Move(_airScans[0].Location));
                        if (centroids != null && centroids.Any())
                        {
                            pen.Width = 6;
                            foreach (var centroid in centroids)
                            {
                                e.Graphics.DrawRectangle(pen, new Rectangle(centroid.X - 8, centroid.Y - 8, 8, 8));
                            }
                        }
                    }

                    if (chkTouchPoints.Checked)
                    {
                        if (_touchPoints != null && _touchPoints.Any())
                        {
                            //var touchPoints = _touchPoints.Select(x => x.Move(_airScans[0].Location));
                            pen.Width = 9;
                            foreach (var touchPoint in _touchPoints)
                            {
                                var point = touchPoint.Location.Move(_airScans[0].Location);
                                pen.Color = Color.Black;
                                e.Graphics.DrawRectangle(pen, new Rectangle(point.X - 12, point.Y - 12, 12, 12));
                                e.Graphics.DrawString(touchPoint.Id.ToString(), new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold), new SolidBrush(Color.White), point.X - 12, point.Y - 12);
                            }
                        }
                    }
                }

                if (chkML.Checked && mlPoints != null && mlPoints.Any())
                {
                    foreach (var point in mlPoints)
                    {
                        pen.Width = 10;
                        e.Graphics.DrawRectangle(pen, new Rectangle(point.X - 4, point.Y - 4, 4, 4));
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            pen.Dispose();
        }

        private async Task Tick(Rectangle panel, bool chkEdgeChecked, int handSizeValue, int speedValue)
        {
            try
            {
                mainPanel.Invalidate();

                foreach (var hand in _hands)
                    await hand.Animate(panel, handSize.Value, speed.Value, rnd);

                foreach (var airScan in _airScans)
                {
                    var obstacles = _hands.Select(x => new Rectangle(x.Location, x.Size)).ToList();
                    if (chkEdge.Checked)
                        obstacles.Add(new Rectangle(0, 0, panel.Width, panel.Height));
                    await airScan.ExecuteRaycast(obstacles, true);
                }

                var centroids = _airScans.SelectMany(x => x.Manager.CalibratedCentroids);
                _touchPointManager.AddCentroids(centroids.ToArray());

                _touchPoints = _touchPointManager.GetTouchPoints();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            await Task.Delay(interval.Value);

            await Tick(panel, chkEdgeChecked, handSizeValue, speedValue);
        }

        private async Task Calibration()
        {
            while (calibration)
            {
                int calibrated = 0;
                if (_airScans.Any() && _airScans.Count > 1)
                {
                    var centroids = _airScans[0].Manager.Centroids;
                    if (centroids != null && centroids.Any())
                    {
                        for (var i = 1; i < _airScans.Count; i++)
                        {
                            try
                            {
                                _airScans[i].Manager.Calibrate(centroids[0]);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                            if (_airScans[i].Manager.IsCalibrated)
                                calibrated++;
                        }
                    }
                }

                if (calibrated == _airScans.Count - 1)
                    btnStartCalibration.PerformClick();

                // Use MachineLearning
                if (chkML.Checked)
                {
                    try
                    {
                        var points = String.Join("\n", _airScans.SelectMany(airScan => airScan.Points).Select(point => $"{point.X},{point.Y}"));
                        File.WriteAllText(
                            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scripts\\py\\points.csv"),
                            points
                        );
                        ThreadPool.QueueUserWorkItem(x =>
                        {
                            try
                            {
                                mlPoints = GetMLPoints();
                            }
                            catch (Exception e)
                            {
                            }
                        });
                    }
                    catch (Exception e)
                    {
                    }
                }

                await Task.Delay(interval.Value);
            }
        }

        private List<Point> GetMLPoints()
        {
            // Path to the Python interpreter
            string pythonInterpreter = FindPythonInterpreter(); // @"C:\Users\ikazemi\AppData\Local\Programs\Python\Python312\python.exe";

            // Path to your Python script
            string pythonScript = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scripts/py/script.py");

            // Create process info
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = pythonInterpreter;
            //start.Arguments = $"{pythonScript} \"{pointsData}\""; // Pass points data as an argument
            start.Arguments = $"{pythonScript}"; // Pass points data as an argument
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.CreateNoWindow = true; // Hide the command prompt window

            // Start the process
            using (Process process = Process.Start(start))
            {
                using (System.IO.StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<List<List<double>>>(result).Select(x => new Point((int)x[0], (int)x[1])).ToList();
                }
            }
        }

        private string FindPythonInterpreter()
        {
            // Look for Python interpreter in PATH environment variable
            string pythonExe = "python.exe";
            string[] paths = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User).Split(Path.PathSeparator);

            foreach (string path in paths)
            {
                string fullPath = Path.Combine(path, pythonExe);
                if (File.Exists(fullPath))
                {
                    return fullPath;
                }
            }

            paths = Environment.GetEnvironmentVariable("PATH").Split(Path.PathSeparator);

            foreach (string path in paths)
            {
                string fullPath = Path.Combine(path, pythonExe);
                if (File.Exists(fullPath))
                {
                    return fullPath;
                }
            }

            return null; // Python interpreter not found
        }

        private void btnAddAirScan_Click(object sender, EventArgs e)
        {
            if (calibration)
            {
                MessageBox.Show("New Airscan cannot be added in calibrating");
                return;
            }
            _airScans.Add(new AirScan(mainPanel, rnd, _dbSCAN, interval) { Resolution = resolution.Value});
        }

        private async void btnStartCalibration_Click(object sender, EventArgs e)
        {
            if (calibration == false && _hands.Count != 1)
            {
                MessageBox.Show("There should only exists one hand while calibrating");
                return;
            }

            if (calibration == false && _airScans.Count <= 1)
            {
                MessageBox.Show("For calibrating at least two Airscans are needed");
                return;
            }

            calibration = !calibration;
            
            btnStartCalibration.Text = calibration ? "Stop Calibration" : "Start Calibration";
            
            if(calibration)
            {
                for (var i = 1; i < _airScans.Count; i++)
                {
                    _airScans[i].Manager.IsCalibrated = false;
                }
                await Calibration();
            }

        }

        private void btnClearAllAirScans_Click(object sender, EventArgs e)
        {
            if (calibration)
            {
                MessageBox.Show("This action is not allowed while calibrating");
                return;
            }
            foreach (var airscan in _airScans)
            {
                mainPanel.Controls.Remove(airscan);
            }

            _airScans.Clear();
        }

        private void btnAddHand_Click(object sender, EventArgs e)
        {
            if (calibration)
            {
                MessageBox.Show("New Hand cannot be added in calibrating");
                return;
            }
            var hand = new Hand(mainPanel, handSize.Value, rnd) { Visible = chkHand.Checked };
            _hands.Add(hand);
        }

        private void btnClearAllHands_Click(object sender, EventArgs e)
        {
            if (calibration)
            {
                MessageBox.Show("This action is not allowed while calibrating");
                return;
            }
            foreach (var hand in _hands)
            {
                mainPanel.Controls.Remove(hand);
            }
            _hands.Clear();
        }

        private void chkEdge_CheckedChanged(object sender, EventArgs e)
        {
            mainPanel.BorderStyle = chkEdge.Checked ? BorderStyle.FixedSingle : BorderStyle.None;
            optPoint.Checked = true;
        }

        //private void interval_Scroll(object sender, EventArgs e)
        //{
        //    timer.Interval = interval.Value;
        //}

        private void resolution_Scroll(object sender, EventArgs e)
        {
            _airScans.ForEach(x=> x.Resolution = resolution.Value);
        }

        private void chkML_CheckedChanged(object sender, EventArgs e)
        {
            chkHand.Checked = !chkML.Checked;
            optPoint.Checked = true;
        }

        private async void frmMain_Load(object sender, EventArgs e)
        {
            Rectangle panel = new Rectangle(mainPanel.Location, mainPanel.Size);
            bool chkEdgeChecked = chkEdge.Checked;
            int handSizeValue = handSize.Value;
            int speedValue = speed.Value;
            await Tick(panel, chkEdgeChecked, handSizeValue, speedValue);
        }

        private void chkHand_CheckedChanged(object sender, EventArgs e)
        {
            _hands.ForEach(x => x.Visible = chkHand.Checked);
        }

        private void epsilon_Scroll(object sender, EventArgs e)
        {
            _dbSCAN.Epsilon = epsilon.Value;
        }

        private void minPts_Scroll(object sender, EventArgs e)
        {
            _dbSCAN.MinPts = minPts.Value;
        }
    }
}
