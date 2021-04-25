using System;
using System.Collections.Generic;
using System.Text;

namespace TheatricalPlayer01.Models
{
    public class ComedyCalculator : PerformanceCalculator
    {
        public ComedyCalculator(Performance performance, Play play) : base(performance, play)
        {
        }

        public new int GetAmount()
        {
            var result = 30000;
            if (Performance.Audience > 20)
            {
                result += 10000 + 500 * (Performance.Audience - 20);
            }
            result += 300 * Performance.Audience;

            return result;
        }
    }
}
