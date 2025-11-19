using System;
using System.Collections.Generic;
using System.Linq;

namespace SakanaController
{
    public class Calibration
    {
        private List<(double x, double y)> dataPoints = new List<(double x, double y)>();
        
        public double Slope = 1;
        public double Intercept = 0;

        public void AddDataPoint(double x, double y)
        {
            dataPoints.Add((x, y));
        }

        public void Fitting()
        {
            if (dataPoints.Count <= 5)
                throw new InvalidOperationException("Not enough data points available for fitting.");

            var fittingPoints = dataPoints.Skip(5).ToList();

            double sumX = fittingPoints.Sum(p => p.x);
            double sumY = fittingPoints.Sum(p => p.y);
            double sumXY = fittingPoints.Sum(p => p.x * p.y);
            double sumX2 = fittingPoints.Sum(p => p.x * p.x);

            int n = fittingPoints.Count;
            this.Slope = Math.Round((n * sumXY - sumX * sumY) / (n * sumX2 - sumX * sumX), 4);
            this.Intercept = Math.Round((sumY - this.Slope * sumX) / n, 4);
        }

        public void Clear()
        {
            this.dataPoints.Clear();
        }
    }
}
