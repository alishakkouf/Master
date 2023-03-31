using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Master.Shared;

namespace Master.Common
{
    public static class IdentityUserExtensions
    {
        public static int? GetUserId(this ClaimsPrincipal user)
        {
            var id = user?.FindFirst(ClaimTypes.NameIdentifier);
            return string.IsNullOrEmpty(id.Value) ? (int?)null : int.Parse(id.Value);
        }

        public static string GetUserName(this ClaimsPrincipal user)
            => user?.FindFirst(ClaimTypes.Name).Value;

        //public static int? GetTenantId(this ClaimsPrincipal user)
        //{ 
        //    var tenantId = user?.FindFirstValue(Constants.TenantIdClaimType);
        //    return string.IsNullOrEmpty(tenantId) ? (int?)null : int.Parse(tenantId);
        //}
         
        public static List<string> GetPermissions(this ClaimsPrincipal user)
        {
            return user?.Claims.Where(c => c.Type == Constants.PermissionsClaimType).Select(c => c.Value).ToList() ?? new List<string>();
        }

        public static bool IsWithoutRole(this ClaimsPrincipal user)
        {
            var role = user?.FindFirst(ClaimTypes.Role);
            return string.IsNullOrEmpty(role.Value);
        }
    }
}
