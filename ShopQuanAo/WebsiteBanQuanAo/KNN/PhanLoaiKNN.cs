using WebsiteBanQuanAo.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace WebsiteBanQuanAo.KNN
{
    public class KNNClassifier
    {
        public KNNClassifier() { }

        private readonly List<double[]> trainingDataFeatures = new List<double[]>();
        private readonly List<string> trainingDataLabels = new List<string>();

        public void LoadTrainingDataFromFile()
        {
            string filePath = @"C:\Users\admin\Desktop\DoAn_PhatTrienUngDungThongMinh_Finall\ShopQuanAo\ShopQuanAo\WebsiteBanQuanAo\KNN\train_data.txt";

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"The file does not exist: {filePath}");

            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length != 5)
                    continue;

                double age = double.Parse(parts[0]);
                double spending = double.Parse(parts[1]);
                double normalizedAge = double.Parse(parts[2], CultureInfo.InvariantCulture);
                double normalizedSpending = double.Parse(parts[3], CultureInfo.InvariantCulture);
                string label = parts[4].Trim();

                trainingDataFeatures.Add(new[] { age, spending, normalizedAge, normalizedSpending });
                trainingDataLabels.Add(label);
            }
        }

        public string Predict(double[] userData, int neighborsCount = 11)
        {
            var distances = new List<(double Distance, string Label)>();

            for (int i = 0; i < trainingDataFeatures.Count; i++)
            {
                double distance = CalculateEuclideanDistance(userData, trainingDataFeatures[i]);
                distances.Add((distance, trainingDataLabels[i]));
            }

            var sortedDistances = distances.OrderBy(d => d.Distance).ToList();
            var nearestNeighbors = sortedDistances.Take(neighborsCount).ToList();

            var weightedLabels = nearestNeighbors
                .Select(nn => (Label: nn.Label, Weight: 1.0 / (nn.Distance + 1e-5)))
                .GroupBy(nn => nn.Label)
                .Select(group => new { Label = group.Key, TotalWeight = group.Sum(nn => nn.Weight) })
                .OrderByDescending(x => x.TotalWeight)
                .ToList();

            var topGroups = weightedLabels.Where(w => w.TotalWeight == weightedLabels.First().TotalWeight).ToList();

            if (topGroups.Count == 1)
            {
                return topGroups.First().Label;
            }

            var densityAnalysis = nearestNeighbors
                .Where(nn => topGroups.Any(group => group.Label == nn.Label))
                .GroupBy(nn => nn.Label)
                .Select(group => new
                {
                    Label = group.Key,
                    AvgDensity = group.Average(nn => 1.0 / (nn.Distance + 1e-5)),
                    AvgDistance = group.Average(nn => nn.Distance)
                })
                .OrderByDescending(x => x.AvgDensity)
                .ThenBy(x => x.AvgDistance)
                .ToList();

            if (!densityAnalysis.Any())
            {
                return "Unable to predict";
            }

            return densityAnalysis.First().Label;
        }

        private static double CalculateEuclideanDistance(double[] point1, double[] point2)
        {
            double sum = 0.0;

            for (int i = 0; i < point1.Length; i++)
            {
                double difference = point1[i] - point2[i];
                sum += difference * difference;
            }

            return Math.Sqrt(sum);
        }

        public void LoadTrainingDataFromDatabase()
        {
            using (var db = new ShopQuanAoEntities())
            {
                var customers = db.NguoiDungs.Where(c => c.Train == true).ToList();

                foreach (var customer in customers)
                {
                    double age = customer.DoTuoi ?? 0;
                    double spending = customer.MucChiTieu ?? 0;
                    double normalizedAge = age / 100;
                    double normalizedSpending = spending / 100000000;

                    string predictedLabel = Predict(new[] { age, spending, normalizedAge, normalizedSpending });
                    customer.PhanKhucKH = predictedLabel;
                }

                db.SaveChanges();
            }
        }

        public void LoadLabelsFromDatabase()
        {
            using (var db = new ShopQuanAoEntities())
            {
                var sampleCustomers = db.NguoiDungs.Where(c => c.Train == true).ToList();

                foreach (var customer in sampleCustomers)
                {
                    double age = customer.DoTuoi ?? 0;
                    double spending = customer.MucChiTieu ?? 0;
                    double normalizedAge = age / 100;
                    double normalizedSpending = spending / 100000000;

                    trainingDataFeatures.Add(new[] { age, spending, normalizedAge, normalizedSpending });
                    trainingDataLabels.Add(customer.PhanKhucKH);
                }
            }
        }
    }
}
