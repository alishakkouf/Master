using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Master.Domain.Accounts;

namespace Master.Data.Models.Role
{
    public class RoleDomain
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public List<UserAccountDomain> UserAccounts { get; set; } = new List<UserAccountDomain>();

        public List<string> PermissionList { get; set; } = new List<string>();

        public List<RolePermissionsDomain> Permissions { get; set; } = new List<RolePermissionsDomain>();
    }
}
