using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearRegression;

namespace AirScanEmulator
{
    public class AirScanManager
    {
        public double AngleOffset { get; internal set; } = Double.NaN;
        public List<Point> Centroids => this.GetCentroids();
        public List<Point> CalibratedCentroids => this.GetCalibratedCentroids();
        private AirScan _airScan;
        private CalibrationStructure _calibrationStructureX;
        private CalibrationStructure _calibrationStructureY;
        private DBSCAN _dbSCAN;
        private readonly TrackBar _interval;

        public bool IsCalibrated
        {
            get => AngleOffset.IsFinite() && (_calibrationStructureX?.IsCalibrated ?? false) && (_calibrationStructureY?.IsCalibrated ?? false);
            set
            {
                if(_calibrationStructureX != null)
                    _calibrationStructureX.IsCalibrated = value;
                if(_calibrationStructureY != null)
                    _calibrationStructureY.IsCalibrated = value;
            }
        }
        public AirScanManager(AirScan airScan, DBSCAN dbScan, TrackBar interval)
        {
            _airScan = airScan;
            _dbSCAN = dbScan;
            _interval = interval;
        }
        private List<Point> GetCentroids()
        {
            var centroids = new List<Point>();
            try
            {
                var points = _airScan.PointsFromZero;
                if (points == null || points.Any() == false)
                    return centroids;

                // Find Centroids based on Points
                var clusters = _dbSCAN.Cluster(points.ToArray());
                clusters.ForEach(x => centroids.Add(x.GetCentroid()));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return centroids;
        }

        private DateTime _lastTimeGetCalibratedCentroids;
        private List<Point> _lastCalibratedCentroids;
        private List<Point> GetCalibratedCentroids()
        {
            if ((DateTime.Now - _lastTimeGetCalibratedCentroids).TotalMilliseconds < 40)
            {
                return _lastCalibratedCentroids;
            }
            _lastTimeGetCalibratedCentroids = DateTime.Now;
            var centroids = this.Centroids;
            if (_calibrationStructureX == null || _calibrationStructureY == null)
            {
                return _lastCalibratedCentroids = centroids;
            }
            
            // Do the process with the help of Calibrator
            return _lastCalibratedCentroids = centroids.Select(x => new Point((int)CalibrateValue(_calibrationStructureX, x),
                (int)CalibrateValue(_calibrationStructureY, x))).ToList();
        }

        private List<(Point, Point)> _calibrationPoints = new List<(Point, Point)>();

        public void Calibrate(Point centroid)
        {
            if (this.Centroids == null || this.Centroids.Any() == false)
                return;

            var localCentroid = this.Centroids[0];

            if (_calibrationPoints.Count < 20)
            {
                _calibrationPoints.Add((centroid, localCentroid));
                return;
            }
            else
            {
                
            }

            // Do the MachineLearning based on the first Centroid (this.Centroids[0]) and the centroid parameter

            // Number of iterations (epochs)
            int numIterations = (int)Math.Ceiling(_interval.Value / 10.0);

            // Perform SGD
            for (int i = 0; i < numIterations; i++)
            {
                //_calibrationStructureX = RFL((_calibrationStructureX = _calibrationStructureX ?? new CalibrationStructure() { XWidth = 1, YWidth = 0, Additional = (centroid.X - localCentroid.X) }), localCentroid, centroid.X);
                //_calibrationStructureY = RFL((_calibrationStructureY = _calibrationStructureY ?? new CalibrationStructure() { XWidth = 0, YWidth = 1, Additional = (centroid.Y - localCentroid.Y) }), localCentroid, centroid.Y);

                (_calibrationStructureX, _calibrationStructureY) = LinearRegression(_calibrationStructureX,
                    _calibrationStructureY, localCentroid, centroid);
            }
        }

        private CalibrationStructure RFL(CalibrationStructure calibrationStructure, Point centroid, double goal)
        {
            if (calibrationStructure.IsCalibrated)
                return calibrationStructure;

            // Learning rate
            double learningRate = 0.01;

            // Compute the prediction using the current parameters
            double prediction = CalibrateValue(calibrationStructure, centroid);

            if (double.IsNaN(prediction) == false && double.IsInfinity(prediction) == false)
            {
                if (Math.Abs(prediction - goal) <= 1)
                {
                    calibrationStructure.IsCalibratedSequence++;
                    if (calibrationStructure.IsCalibratedSequence >= 20)
                    {
                        calibrationStructure.IsCalibrated = true;
                        return calibrationStructure;
                    }
                }
                else
                {
                    calibrationStructure.IsCalibratedSequence = 0;
                }

                // Compute the gradients (partial derivatives) of the loss function w.r.t. parameters
                double grad_a = 2 * (prediction - goal) * calibrationStructure.GetXPower(centroid);
                double grad_b = 2 * (prediction - goal) * calibrationStructure.GetYPower(centroid);
                //double grad_p = 2 * (prediction - goal) * calibrationStructure.XWidth * calibrationStructure.GetXPower(centroid) * Math.Log(centroid.X);
                //double grad_o = 2 * (prediction - goal) * calibrationStructure.YWidth * calibrationStructure.GetYPower(centroid) * Math.Log(centroid.Y);
                double grad_c = 2 * (prediction - goal);

                // Update the parameters using gradient descent
                calibrationStructure.XWidth -= learningRate * (double.IsNaN(grad_a) ? 0 : grad_a);
                calibrationStructure.YWidth -= learningRate * (double.IsNaN(grad_b) ? 0 : grad_b);
                //calibrationStructure.XPower -= learningRate * (double.IsNaN(grad_p) ? 0 : grad_p);
                //calibrationStructure.YPower -= learningRate * (double.IsNaN(grad_o) ? 0 : grad_o);
                calibrationStructure.Additional -= learningRate * (double.IsNaN(grad_c) ? 0 : grad_c);
            }
            else
            {
                calibrationStructure.Reset();
            }

            if (calibrationStructure.IsValid() == false)
                calibrationStructure.Reset();
            return calibrationStructure;
        }

        private List<double> _x1Points;
        private List<double> _y1Points;
        private List<double> _x2Points;
        private List<double> _y2Points;
        private (CalibrationStructure, CalibrationStructure) LinearRegression(CalibrationStructure calibrationStructureX, CalibrationStructure calibrationStructureY, Point centroid, Point goal)
        {
            if (_x1Points == null && _y1Points == null)
            {
                if(calibrationStructureX != null && calibrationStructureX.IsCalibrated == false)
                    calibrationStructureX = null;
                if(calibrationStructureY != null && calibrationStructureY.IsCalibrated == false)
                    calibrationStructureY = null;
            }

            if (calibrationStructureX != null && calibrationStructureX.IsCalibrated)
                if (calibrationStructureY != null && calibrationStructureY.IsCalibrated)
                {
                    _x1Points = null;
                    _x2Points = null;
                    _y1Points = null;
                    _y2Points = null;
                    return (calibrationStructureX, calibrationStructureY);
                }

            if (calibrationStructureX != null)
            {
                // Compute the prediction using the current parameters
                var predictionX = CalibrateValue(calibrationStructureX, centroid);

                if (Math.Abs(predictionX - goal.X) <= 4)
                {
                    calibrationStructureX.IsCalibratedSequence++;
                    if (calibrationStructureX.IsCalibratedSequence >= 20)
                    {
                        _x1Points = null;
                        _x2Points = null;
                        calibrationStructureX.IsCalibrated = true;
                    }
                }
                else
                {
                    calibrationStructureX.IsCalibratedSequence = 0;
                }
            }

            if (calibrationStructureX == null || calibrationStructureX.IsCalibrated == false)
            {
                _x1Points = _x1Points ?? new List<double>();
                if (_x1Points.Contains(centroid.X) == false)
                {
                    _x1Points.Add(centroid.X);
                    _x2Points = _x2Points ?? new List<double>();
                    _x2Points.Add(goal.X);
                }

                if (_x1Points.Count > 100)
                {
                    // Perform linear regression for x
                    var xRegression = Fit.Line(_x1Points.ToArray(), _x2Points.ToArray());
                    if (calibrationStructureX == null)
                        calibrationStructureX = new CalibrationStructure() { XPower = 1, YWidth = 0 };

                    calibrationStructureX.XWidth = xRegression.B;
                    calibrationStructureX.Additional = xRegression.A;
                }
                else
                {
                    calibrationStructureX = null;
                }
            }


            if (calibrationStructureY != null)
            {
                // Compute the prediction using the current parameters
                var predictionY= CalibrateValue(calibrationStructureY, centroid);

                if (Math.Abs(predictionY - goal.Y) <= 4)
                {
                    calibrationStructureY.IsCalibratedSequence++;
                    if (calibrationStructureY.IsCalibratedSequence >= 20)
                    {
                        _y1Points = null;
                        _y2Points = null;
                        calibrationStructureY.IsCalibrated = true;
                    }
                }
                else
                {
                    calibrationStructureY.IsCalibratedSequence = 0;
                }
            }

            if (calibrationStructureY == null || calibrationStructureY.IsCalibrated == false)
            {
                _y1Points = _y1Points ?? new List<double>();
                if (_y1Points.Contains(centroid.Y) == false)
                {
                    _y1Points.Add(centroid.Y);
                    _y2Points = _y2Points ?? new List<double>(); 
                    _y2Points.Add(goal.Y);
                }

                if (_y1Points.Count > 100)
                {
                    // Perform linear regression for y
                    var yRegression = Fit.Line(_y1Points.ToArray(), _y2Points.ToArray());

                    if (calibrationStructureY == null)
                        calibrationStructureY = new CalibrationStructure() { YPower = 1, XWidth = 0 };

                    calibrationStructureY.YWidth = yRegression.B;
                    calibrationStructureY.Additional = yRegression.A;
                }
                else
                {
                    calibrationStructureY = null;
                }
            }

            return (calibrationStructureX, calibrationStructureY);
        }

        private double CalibrateValue(CalibrationStructure calibrationStructure, Point centroid)
        {
            return calibrationStructure.XWidth * calibrationStructure.GetXPower(centroid) + calibrationStructure.YWidth * calibrationStructure.GetYPower(centroid) + calibrationStructure.Additional;
        }
    }
}
