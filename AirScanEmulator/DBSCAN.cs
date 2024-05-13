using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirScanEmulator
{
    public class DBSCAN
    {
        // DBSCAN parameters
        public double Epsilon { get; set; }
        public int MinPts { get; set; }

        public DBSCAN(double epsilon, int minPts)
        {
            this.Epsilon = epsilon;
            this.MinPts = minPts;
        }

        public List<List<Point>> Cluster(Point[] points)
        {
            var pointsEnumerator = points.GetEnumerator();
            int numPoints = points.Length;
            bool[] visited = new bool[numPoints];
            int[] clusterIDs = new int[numPoints];
            int clusterID = 0;

            for (int i = 0; i < numPoints; i++)
            {
                if (!visited[i])
                {
                    visited[i] = true;
                    Point currentPoint = points[i];
                    List<int> neighbors = GetNeighbors(points, i);

                    if (neighbors.Count >= MinPts)
                    {
                        ExpandCluster(points, visited, clusterIDs, i, neighbors, clusterID);
                        clusterID++;
                    }
                    else
                    {
                        clusterIDs[i] = -1; // Mark as noise
                    }
                }
            }

            // Group points by cluster
            var clusters = new List<List<Point>>();
            int numClusters = clusterIDs.Max() + 1; // Number of clusters (excluding noise)
            for (int i = 0; i < numClusters; i++)
            {
                clusters.Add(new List<Point>());
            }

            for (int i = 0; i < numPoints; i++)
            {
                if (clusterIDs[i] != -1)
                {
                    clusters[clusterIDs[i]].Add(points[i]);
                }
            }

            return clusters;
        }

        private List<int> GetNeighbors(Point[] points, int pointIndex)
        {
            List<int> neighbors = new List<int>();
            Point currentPoint = points[pointIndex];

            for (int i = 0; i < points.Length; i++)
            {
                if (i != pointIndex)
                {
                    Point otherPoint = points[i];
                    double distance = CalculateDistance(currentPoint, otherPoint);
                    if (distance <= Epsilon)
                    {
                        neighbors.Add(i);
                    }
                }
            }

            return neighbors;
        }

        private void ExpandCluster(Point[] points, bool[] visited, int[] clusterIDs, int pointIndex, List<int> neighbors, int clusterID)
        {
            clusterIDs[pointIndex] = clusterID;

            int i = 0;
            while (i < neighbors.Count)
            {
                int neighborIndex = neighbors[i];
                if (!visited[neighborIndex])
                {
                    visited[neighborIndex] = true;
                    //Point neighborPoint = points[neighborIndex];
                    List<int> neighborNeighbors = GetNeighbors(points, neighborIndex);
                    if (neighborNeighbors.Count >= MinPts)
                    {
                        neighbors.AddRange(neighborNeighbors);
                    }
                }

                if (clusterIDs[neighborIndex] == 0)
                {
                    clusterIDs[neighborIndex] = clusterID;
                }

                i++;
            }
        }

        private double CalculateDistance(Point point1, Point point2)
        {
            double sumSquaredDifferences = Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2);
            return Math.Sqrt(sumSquaredDifferences);
        }
    }
}
