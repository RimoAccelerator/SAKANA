using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakanaController
{
    public class Curve
    {
        private List<(double x, double y)> smoothedDataPoints = new List<(double x, double y)>();
        private int smoothingWindowSize = 5; // 平滑窗口大小，可以根据需要调整

        public double StartE { get; set; }
        public double EndE { get; set; }
        public double ScanRate { get; set; }

        public void Clear()
        {
            this.smoothedDataPoints.Clear();
        }

        public (double x, double y) SmoothDataPoint(double newX, double newY)
        {
            // 将新数据点添加到历史数据点列表中
            smoothedDataPoints.Add((newX, newY));
            // 如果数据点数量小于平滑窗口大小，直接返回新数据点
            if (smoothedDataPoints.Count < smoothingWindowSize)
            {
                return (newX, newY);
            }

            // 计算平滑后的y值
            double smoothedY = 0;
            int startIndex = Math.Max(0, smoothedDataPoints.Count - smoothingWindowSize);
            int count = 0;

            for (int i = startIndex; i < smoothedDataPoints.Count; i++)
            {
                smoothedY += smoothedDataPoints[i].y;
                count++;
            }

            smoothedY /= count;

            // 更新最后一个数据点为平滑后的数据点
            smoothedDataPoints[smoothedDataPoints.Count - 1] = (newX, smoothedY);

            // 返回平滑后的数据点
            return (newX, smoothedY);
        }

        public void Save(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                writer.WriteLine("MEASURED BY SAKANA");
                writer.WriteLine($"StartE = {StartE} V, EndE = {EndE} V, ScanRate = {ScanRate} mV/s");
                writer.WriteLine(); 

                foreach (var point in smoothedDataPoints)
                {
                    writer.WriteLine($"{point.x} {point.y}");
                }
            }
        }

    }


}
