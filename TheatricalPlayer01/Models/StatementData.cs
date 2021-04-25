using System;
using System.Collections.Generic;
using System.Text;

namespace TheatricalPlayer01.Models
{
    public class StatementData
    {
        public Invoice Invoice { get; set; }
        public Dictionary<string, Play> Plays { get; set; }
    }
}
