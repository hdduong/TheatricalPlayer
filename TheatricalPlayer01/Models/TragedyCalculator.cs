namespace TheatricalPlayer01.Models
{
    public class TragedyCalculator : PerformanceCalculator
    {
        public TragedyCalculator(Performance performance, Play play) : base(performance, play)
        {
        }

        public new int GetAmount()
        {
            var result = 40000;
            if (Performance.Audience > 30)
            {
                result += 1000 * (Performance.Audience - 30);
            }

            return result;
        }
    }
}
