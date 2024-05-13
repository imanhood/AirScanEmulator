using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirScanEmulator
{
    public class TouchPointManager
    {
        private readonly List<TouchPoint> _touchPoints = new List<TouchPoint>();
        public int MovementTolerance { get; set; } = 40;
        public int LifeTime { get; set; } = 10;
        public int MovingAverage { get; set; } = 10;
        public void AddCentroids(Point[] centroids)
        {
            if(centroids == null || centroids.Any() == false)
                return;
            //var clusteredTouchPoints = _dbScan.Cluster(centroids);
            //foreach (var cluster in clusteredTouchPoints)
            //{
            //    var centroid = cluster.GetCentroid();
            foreach (var centroid in centroids) {
                var touchPoint = _touchPoints.FirstOrDefault(x => x.Location.Length(centroid) <= this.MovementTolerance);
                if (touchPoint == null)
                {
                    _touchPoints.Add((touchPoint = new TouchPoint(centroid, (int)(this.LifeTime * 1.5), this.MovingAverage)));
                }
                else
                {
                    touchPoint.SetLocation(centroid);
                    if (touchPoint.LifeTime < this.LifeTime * 3)
                    {
                        touchPoint.LifeTime = (int)(touchPoint.LifeTime * 1.5);
                        if(touchPoint.Visible == false && touchPoint.LifeTime >= this.LifeTime * 2)
                            touchPoint.Visible = true;
                    }
                }
            }
        }

        public IEnumerable<TouchPoint> GetTouchPoints()
        {
            _touchPoints.ForEach(x =>
            {
                x.LifeTime--;
                if (x.LifeTime < this.LifeTime)
                {
                    x.Visible = false;
                }
            });
            _touchPoints.RemoveAll(x => x.LifeTime <= 0);
            
            return _touchPoints.Where(x => x.Visible);
        }
    }
}