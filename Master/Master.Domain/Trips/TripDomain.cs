﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Domain.Trips
{
    public class TripDomain
    {
        public int Id { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public DateTime Date { get; set; }

        public int NumOfSeats { get; set; }

        public int AvailableNumOfSeats { get; set; }
    }
}
