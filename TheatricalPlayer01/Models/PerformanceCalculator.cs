using System;

namespace TheatricalPlayer01.Models
{
    public class PerformanceCalculator
    {
        private readonly Performance _performance;
        private readonly Play _play;

        public PerformanceCalculator(Performance performance, Play play)
        {
            _performance = performance;
            _play = play;
        }

        public int GetAmount()
        {
            var result = 0;
            switch (_play.Type)
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
                    throw new Exception($"Unknown {_play.Type} ");
            }

            return result;
        }
    }
}
