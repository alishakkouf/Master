using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Domain.Accounts
{
    public class RegisterInputCommand
    {
        public int? Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        //public string PhoneCountryCode { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        //public DateTime? DateOfBirth { get; set; }

        //public string CountryName { get; set; }

        //public string City { get; set; }

        //public string Street { get; set; }

        //public string Building { get; set; }

        //public string PostalCode { get; set; }

        //public int TenantId { get; set; }
    }
}
