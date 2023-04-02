using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Data.Models.Role
{
    public class RolePermissionsDomain
    {
        public string Key { get; set; }

        public string Display { get; set; }

        public bool Value { get; set; }

        public bool IsFixed { get; set; }
    }
}
