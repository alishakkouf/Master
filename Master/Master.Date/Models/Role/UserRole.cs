using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Master.Data.Models.Account;
using Master.Domain.MultyTenants;
using Microsoft.AspNetCore.Identity;

namespace Master.Data.Models.Role
{
    public class UserRole : IdentityRole<int>, IAuditedEntity
    {
        UserRole() : base() { }

        [StringLength(1000)]
        public string Description { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDoctor { get; set; }

        public long? CreatorUserId { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public long? ModifierUserId { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public bool? IsDeleted { get; set; } = false;

        public int? TenantId { get; set; }

        public UserRole(string roleName) : base(roleName)
        {
        }

        internal virtual ICollection<UserAccount> UserAccounts { get; set; } = new HashSet<UserAccount>();

        public virtual ICollection<IdentityRoleClaim<int>> Claims { get; set; } = new HashSet<IdentityRoleClaim<int>>();
    }
}
