using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Master.Domain.Accounts;

namespace Master.Domain.Trips
{
    public class UserTripDomain
    {
        public int Id { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public DateTime Date { get; set; }

        public UserAccountDomain User { get; set; }
    }
}
