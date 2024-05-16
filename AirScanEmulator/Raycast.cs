using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace AirScanEmulator
{
    public class RayCast
    {
        public RayCast(int index, Point startPoint, double angle, double maxLength)
        {
            this.Index = index;
            this.StartPoint = startPoint;
            this.Angle = angle;
            this.MaxLength = maxLength;
        }

        public int Index { get; set; }
        public Point StartPoint { get; set; }
        public double Angle { get; set; }
        public double MaxLength { get; set; }
        public double HitLength { get; set; }
        //public Point? HitPoint => this.HitLength > 0 ? this.StartPoint.GetEndPoint(this.Angle, this.HitLength) : (Point?)null;
        public async Task<bool> Execute(List<Rectangle> objects)
        {
            var endPoint = StartPoint.GetEndPoint(this.Angle, this.MaxLength);
            var maxLength = this.MaxLength;
            bool find = false;
            foreach (var obj in objects)
            {
                // Find the point where the line touches the rectangle
                Point[] points = {
                    new Point(obj.Left, obj.Top),
                    new Point(obj.Right, obj.Top),
                    new Point(obj.Right, obj.Bottom),
                    new Point(obj.Left, obj.Bottom)
                };

                for (int i = 0; i < points.Length; i++)
                {
                    int j = (i + 1) % points.Length;

                    (bool result, Point intersectionPoint) = await LineIntersects(new Line(StartPoint, endPoint), new Line(points[i], points[j]));
                    if (result)
                    {
                        var length = intersectionPoint.Length(this.StartPoint);
                        if (length < maxLength)
                        {
                            maxLength = length;
                            endPoint = intersectionPoint;
                            find = true;
                        }
                    }
                }
            }

            if (find == false)
                return false;

            HitLength = maxLength;
            //HitPoint = endPoint;
            return true;
        }

        // Method to check if two lines intersect and find the intersection point
        private async Task<(bool, Point)> LineIntersects(Line line1, Line line2)
        {
            var intersection = Point.Empty;

            float a1 = line1.EndPoint.Y - line1.StartPoint.Y;
            float b1 = line1.StartPoint.X - line1.EndPoint.X;
            float c1 = a1 * line1.StartPoint.X + b1 * line1.StartPoint.Y;

            float a2 = line2.EndPoint.Y - line2.StartPoint.Y;
            float b2 = line2.StartPoint.X - line2.EndPoint.X;
            float c2 = a2 * line2.StartPoint.X + b2 * line2.StartPoint.Y;

            float delta = a1 * b2 - a2 * b1;
            if (delta == 0)
                return (false, intersection); // Parallel lines

            intersection = new Point(
                Convert.ToInt32((b2 * c1 - b1 * c2) / delta),
                Convert.ToInt32((a1 * c2 - a2 * c1) / delta)
            );

            // Check if intersection point lies within both line segments
            if (await IsPointOnLine(intersection, line1) && await IsPointOnLine(intersection, line2))
                return (true, intersection);

            return (false, intersection);
        }

        // Method to check if a point lies on a line segment
        private async Task<bool> IsPointOnLine(Point p, Line line)
        {
            return (p.X >= Math.Min(line.StartPoint.X, line.EndPoint.X) && p.X <= Math.Max(line.StartPoint.X, line.EndPoint.X) &&
                    p.Y >= Math.Min(line.StartPoint.Y, line.EndPoint.Y) && p.Y <= Math.Max(line.StartPoint.Y, line.EndPoint.Y));
        }
    }
}
