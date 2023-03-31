using Master.Common;

namespace Master.Configuration.Authorization
{
    internal class PermissionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<PermissionsMiddleware> _logger;

        public PermissionsMiddleware(
            RequestDelegate next,
            ILogger<PermissionsMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context,
            IRolePermissionsService permissionService/*, ITenantManager tenantManager*/)
        {
            if (context.User.Identity == null || !context.User.Identity.IsAuthenticated)
            {
                await _next(context);
                return;
            }

            var cancellationToken = context.RequestAborted;
            //var tenantId = context.User.GetTenantId();

            // if tenant not active then skip permissions and return Unauthorized
            //if (tenantId.HasValue && !await tenantManager.IsActiveTenantAsync(tenantId.Value))
            //{
            //    await _next(context);
            //    return;
            //}

            var userId = context.User.GetUserId();

            if (userId.HasValue)
            {
                var permissionsIdentity = await permissionService.GetUserPermissionsIdentity(userId.Value, cancellationToken);
                if (permissionsIdentity != null)
                {
                    context.User.AddIdentity(permissionsIdentity);
                }
            }

            await _next(context);
        }
    }
}
