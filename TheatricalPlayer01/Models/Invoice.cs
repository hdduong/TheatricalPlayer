using System;
using System.Collections.Generic;
using System.Text;

namespace TheatricalPlayer01.Models
{
    public class Invoice
    {
        public string Customer { get; set; }
        public List<Performance> Performances { get; set; }
    }
}
