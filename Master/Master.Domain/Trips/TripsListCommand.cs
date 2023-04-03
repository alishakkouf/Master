using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Domain.Trips
{
    public class TripsListCommand
    {
        public string? From { get; set; }

        public string? To { get; set; }

        public DateTime? Date { get; set; }
    }
}
