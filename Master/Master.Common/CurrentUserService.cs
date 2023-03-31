using System.Security.Claims;
using Master.Shared;
using Microsoft.AspNetCore.Http;

namespace Master.Common
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? GetTenantId()
        {
            //var tenantClaim = _httpContextAccessor.HttpContext?.User.FindFirstValue(Constants.TenantIdClaimType);

            //if (int.TryParse(tenantClaim, out var tenantId))
            //    return tenantId;

            return null;
        } 

        public int? GetUserId()
        {
            return _httpContextAccessor.HttpContext?.User.GetUserId();
        }

        public string GetUserName()
        {
            return _httpContextAccessor.HttpContext?.User.GetUserName();
        }

        public string? GetUserFullName()
        {
            return _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.GivenName).Value;
        }

        public bool IsInRole(string role)
        {
            return _httpContextAccessor.HttpContext?.User.IsInRole(role) ?? false;
        }

        public bool HasPermission(string permission)
        {
            return _httpContextAccessor.HttpContext?.User.HasClaim(x => x.Type == Constants.PermissionsClaimType && x.Value == permission) ?? false;
        }

        public string GetUserRole() 
        {
            return _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role).Value;
        }
    }
}
