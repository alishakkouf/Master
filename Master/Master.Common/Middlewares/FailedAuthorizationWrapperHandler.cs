﻿using System.Net;
using System.Threading.Tasks;
using Lila.Platform.Shared;
using Lila.Platform.Shared.ResultDtos;
using Master.Shared;
using Master.Shared.ResultDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Master.Common.Middlewares
{
    /// <summary>
    /// Log and convert authorization failures to <see cref="ErrorResultDto"/> with generic message.
    /// </summary>
    public class FailedAuthorizationWrapperHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler _defaultHandler;
        private readonly IStringLocalizer _localizer;
        private readonly ILogger _logger; 
         
        public FailedAuthorizationWrapperHandler(IStringLocalizerFactory factory,
            ILogger<FailedAuthorizationWrapperHandler> logger)
        {
            _defaultHandler = new AuthorizationMiddlewareResultHandler();
            _localizer = factory.Create(typeof(CommonResource));
            _logger = logger;
        } 

        public async Task HandleAsync( 
            RequestDelegate requestDelegate,
            HttpContext httpContext, 
            AuthorizationPolicy authorizationPolicy,
            PolicyAuthorizationResult policyAuthorizationResult)
        {
            if (policyAuthorizationResult.Challenged)
            { 
                _logger.LogWarning($"Unauthenticated access to url {httpContext.Request.GetEncodedUrl()}");

                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                httpContext.Response.ContentType = "application/json";

                var result = JsonCamelCaseSerializer.Serialize(new ErrorResultDto(_localizer["Unauthorized"]));

                await httpContext.Response.WriteAsync(result);
                return;
            }

            if (policyAuthorizationResult.Forbidden)
            {
                _logger.LogWarning($"Unauthorized access by user {httpContext.User.Identity?.Name}, to url {httpContext.Request.GetEncodedUrl()}");

                httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                httpContext.Response.ContentType = "application/json";

                var result = JsonCamelCaseSerializer.Serialize(new ErrorResultDto(_localizer["Forbidden"]));

                await httpContext.Response.WriteAsync(result);
                return;
            }

            // Fallback to the default implementation.
            await _defaultHandler.HandleAsync(requestDelegate, httpContext, authorizationPolicy, policyAuthorizationResult);
        }
    }
}
