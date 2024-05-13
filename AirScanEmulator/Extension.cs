using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirScanEmulator
{
    public static class Extension
    {
        public static double Length(this Point start, Point end)
        {
            return Math.Sqrt(
                Math.Pow(start.X - end.X, 2) + Math.Pow(start.Y - end.Y, 2));
        }

        public static Point GetCentroid(this Control control)
        {
            return new Point(control.Left + control.Width / 2, control.Top + control.Height / 2);
        }

        public static Point GetEndPoint(this Point start, double angle, double length)
        {
            return new Point(Convert.ToInt32(start.X + length * Math.Cos(angle * Math.PI / 180)), Convert.ToInt32(start.Y + length * Math.Sin(angle * Math.PI / 180)));
        }

        public static Point Move(this Point point, Point based)
        {
            return new Point(point.X + based.X, point.Y + based.Y);
        }

        public static Point GetCentroid(this List<Point> points)
        {
            return new Point((int)points.Average(x => x.X), (int)points.Average(x => x.Y));
        }
    }
}
