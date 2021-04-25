using System;
using System.Collections.Generic;
using System.Text;

namespace TheatricalPlayer01.Models
{
    public class StatementData
    {
        public Invoice Invoice { get; set; }
        public Dictionary<string, Play> Plays { get; set; }
        public PerformanceCalculator PerformanceCalculator { get; set; }

        public string GetCustomer()
        {
            return Invoice.Customer;
        }

        public List<Performance> GetPerformances()
        {
            return Invoice.Performances;
        }

        public double TotalAmountFor()
        {
            var result = 0.0;
            foreach (var perf in Invoice.Performances)
            {
                result += new PerformanceCalculator(perf).GetAmount(perf.PlayFor(Plays));
            }
            return result;
        }

        public double TotalVolumeCreditsFor()
        {
            var result = 0.0;
            foreach (var perf in Invoice.Performances)
            {
                result += VolumeCreditsFor(perf);
            }
            return result;
        }

        public double VolumeCreditsFor(Performance aPerformance)
        {
            double result = Math.Max(aPerformance.Audience - 30, 0);
            if (aPerformance.PlayFor(Plays).Type.Equals("comedy", StringComparison.InvariantCultureIgnoreCase))
            {
                result += Math.Floor(aPerformance.Audience / 5.0);
            }
            return result;
        }
    }
}
