using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirScanEmulator
{
    public class CalibrationStructure
    {
        public CalibrationStructure()
        {
            Reset();
        }
        public void Reset()
        {
            this.XWidth = 1.0;
            this.XPower = 1.0;
            this.YWidth = 1.0;
            this.YPower = 1.0;
            this.Additional = 1.0;
        }
        public bool IsValid()
        {
            return
                !double.IsNaN(this.XWidth) && !double.IsInfinity(this.XWidth)&&
                !double.IsNaN(this.XPower) && !double.IsInfinity(this.XPower)&&
                !double.IsNaN(this.YWidth) && !double.IsInfinity(this.YWidth)&&
                !double.IsNaN(this.YPower) && !double.IsInfinity(this.YPower)&&
                !double.IsNaN(this.Additional) && !double.IsInfinity(this.Additional);
        }
        public double XWidth { get; set; }
        public double XPower { get; set; }
        public double YWidth { get; set; }
        public double YPower { get; set; }
        public double Additional { get; set; }
        public int IsCalibratedSequence { get; set; }
        public bool IsCalibrated { get; set; }

        public double GetXPower(Point centroid)
        {
            this.XPower = 1;
            return Math.Pow(centroid.X, this.XPower);
        }

        public double GetYPower(Point centroid)
        {
            this.YPower = 1;
            return Math.Pow(centroid.Y, this.YPower);
        }
    }
}
