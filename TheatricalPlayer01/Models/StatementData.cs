using System;
using System.Collections.Generic;
using System.Text;

namespace TheatricalPlayer01.Models
{
    public class StatementData
    {
        public Invoice Invoice { get; set; }
        public Dictionary<string, Play> Plays { get; set; }

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
                result += AmountFor(perf);
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

       

        public int AmountFor(Performance aPerformance)
        {
            var result = 0;
            switch (aPerformance.PlayFor(Plays).Type)
            {
                case "tragedy":
                    result = 40000;
                    if (aPerformance.Audience > 30)
                    {
                        result += 1000 * (aPerformance.Audience - 30);
                    }
                    break;
                case "comedy":
                    result = 30000;
                    if (aPerformance.Audience > 20)
                    {
                        result += 10000 + 500 * (aPerformance.Audience - 20);
                    }
                    result += 300 * aPerformance.Audience;
                    break;
                default:
                    throw new Exception($"Unknown {aPerformance.PlayFor(Plays).Type} ");
            }

            return result;
        }
    }
}
