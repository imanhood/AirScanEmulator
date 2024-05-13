using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirScanEmulator
{
    public class TouchPoint
    {
        private static int _lastId = 0;
        private readonly List<Point> _lastLocations;
        public TouchPoint(Point location, int lifeTime, int movingAverage)
        {
            _lastLocations = new List<Point>();
            this.LifeTime = lifeTime;
            this.MovingAverage = movingAverage;
            SetLocation(location);
        }

        public int Id { get; set; }
        public int MovingAverage { get; set; }
        public Point Location => _lastLocations.GetCentroid();
        public int LifeTime { get; set; }
        private bool _visible = false;
        public bool Visible
        {
            get => _visible;
            set
            {
                _visible = value;
                if (value != true || this.Id != 0) 
                    return;
                _lastId++;
                this.Id = _lastId;
            }
        }

        public void SetLocation(Point location)
        {
            _lastLocations.Add(location);
            if (_lastLocations.Count > this.MovingAverage)
            {
                _lastLocations.RemoveRange(0, _lastLocations.Count - this.MovingAverage);
            }
        }
    }
}
