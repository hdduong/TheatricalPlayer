using System.Collections.Generic;

namespace TheatricalPlayer01.Models
{
    public class Performance
    {
        public string PlayId { get; set; }
        public int Audience { get; set; }

        public Play PlayFor(Dictionary<string, Play> plays)
        {
            return plays[PlayId];
        }
    }
}
