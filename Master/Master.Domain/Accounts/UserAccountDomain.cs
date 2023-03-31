using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Master.Shared.Enums;

namespace Master.Domain.Accounts
{
    public class UserAccountDomain
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string PhoneCountryCode { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public string Email { get; set; }

        public string ImageRelativePath { get; set; }

        public string ImageUrl { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Gender? Gender { get; set; }

       // public AddressDomain Address { get; set; }

        /// <summary>
        /// Role of the user which should be only one currently
        /// </summary>
        public string Role { get; set; }

        //public RoleDomain RoleDomain { get; set; }

        /// <summary>
        /// Position (Role) of the user
        /// </summary>
        public string Position => Role;

        public string FullName => $"{FirstName} {LastName}";        

        public bool? IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public string ConfirmationCode { get; set; }

        public DateTime? ConfirmationCodeEndDate { get; set; }

        public DateTime? LastLogin { get; set; }

        //public EmployeeInfoDomain EmployeeInfo { get; set; }

        public int? TenantId { get; set; }

    }
}
