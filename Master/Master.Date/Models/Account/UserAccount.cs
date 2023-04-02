using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Master.Data.Models.Role;
using Master.Domain.MultyTenants;
using Master.Shared.Enums;
using Microsoft.AspNetCore.Identity;

namespace Master.Data.Models.Account
{
    public class UserAccount : IdentityUser<int>, IAuditedEntity
    {
        [Required]
        [StringLength(200)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(200)]
        public string LastName { get; set; }

        [Required]
        [StringLength(200)]
        public override string PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Gender? Gender { get; set; }

        public long? CreatorUserId { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public long? ModifierUserId { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public bool? IsDeleted { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime? LastLoginTime { get; set; }

        [StringLength(10)]
        public string? ConfirmationCode { get; set; }

        public bool IsCodeConfirmed { get; set; }

        public DateTime? ConfirmationCodeEndDate { get; set; }

        internal virtual ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    }
}
