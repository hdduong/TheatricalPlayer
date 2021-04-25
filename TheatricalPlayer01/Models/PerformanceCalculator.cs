using System;

namespace TheatricalPlayer01.Models
{
    public class PerformanceCalculator
    {
        protected readonly Performance Performance;
        protected readonly Play Play;

        public PerformanceCalculator(Performance performance, Play play)
        {
            Performance = performance;
            Play = play;
        }

        public int GetAmount()
        {
            switch (Play.Type)
            {
                case "tragedy":
                    return new TragedyCalculator(Performance, Play).GetAmount();
                case "comedy":
                    return new ComedyCalculator(Performance, Play).GetAmount();
                default:
                    throw new Exception($"Unknown {Play.Type} ");
            }
        }
    }
}
