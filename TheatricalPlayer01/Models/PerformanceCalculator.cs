using System;
using System.Collections.Generic;
using System.Text;

namespace TheatricalPlayer01.Models
{
    public class PerformanceCalculator
    {
        private readonly Performance _performance;

        public PerformanceCalculator(Performance performance)
        {
            _performance = performance;
        }

        public int GetAmount(Play play)
        {
            var result = 0;
            switch (play.Type)
            {
                case "tragedy":
                    result = 40000;
                    if (_performance.Audience > 30)
                    {
                        result += 1000 * (_performance.Audience - 30);
                    }
                    break;
                case "comedy":
                    result = 30000;
                    if (_performance.Audience > 20)
                    {
                        result += 10000 + 500 * (_performance.Audience - 20);
                    }
                    result += 300 * _performance.Audience;
                    break;
                default:
                    throw new Exception($"Unknown {play.Type} ");
            }

            return result;
        }
    }
}
