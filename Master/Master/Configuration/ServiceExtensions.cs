using Master.Common;
using Master.Common.Filters;
using Master.Common.Middlewares;
using Master.Configuration.Authorization;
using Master.Data;
using Master.Data.Models;
using Master.Domain.Settings;
using Master.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using System.Globalization;

namespace Master.Configuration
{
    internal static class ServiceExtensions
    {
        /// <summary>
        /// Add Controllers, AutoMapper, Cors  
        /// </summary> 
        internal static IServiceCollection ConfigureApiControllers(this IServiceCollection services, IConfiguration configuration, string corsPolicyName)
        {
            services.AddAutoMapper(typeof(Program));
            
            services.AddControllers(config =>
            { 
                config.Filters.Add(new ValidationFilterAttribute());
                config.Filters.Add(new NormalizeFilterAttribute());
            })
                .AddDataAnnotationsLocalization(o =>
                {
                    o.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(CommonResource));
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });

            services.AddLocalization(opt =>
            {
                opt.ResourcesPath = "Resources";
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = SupportedLanguages.ListAll.Select(x => new CultureInfo(x)).ToList();

                options.DefaultRequestCulture = new RequestCulture(SupportedLanguages.English);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.FallBackToParentUICultures = true;
            });

            services.AddCors(options =>
            {
                options.AddPolicy(corsPolicyName,
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            return services;
        } 

        /// <summary>
        /// Add Identity specific services, and Api Bearer Authentication
        /// </summary>
        internal static IServiceCollection ConfigureApiIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            });

            services.AddIdentityCore<UserAccount>()
                .AddRoles<UserRole>()
                .AddEntityFrameworkStores<MasterDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", opt =>
                {
                    //opt.RequireHttpsMetadata = false;
                    //opt.Authority = configuration["IdentityServer:Authority"];
                    //opt.Audience = Constants.WebScopeName;
                }); 

            services.AddSingleton<IAuthorizationMiddlewareResultHandler, FailedAuthorizationWrapperHandler>();

            // Default authorization policy = User must have the claim UserIsActive which is fetched from db
            // on PermissionsMiddleware
            services.AddAuthorization(options => options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireClaim(Constants.ActiveUserClaimType)
                .Build()); 

            services.AddScoped<IRolePermissionsService, RolePermissionsService>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="PermissionsMiddleware"/> to the specified <see cref="IApplicationBuilder"/>, which retrieves permissions
        /// from the role of user and add them as claims. Must be called after UseAuthentication and before UseAuthorization.
        /// </summary>
        internal static IApplicationBuilder UseRolePermissions(this IApplicationBuilder app)
        {
            app.UseMiddleware<PermissionsMiddleware>();
            return app;
        }

        /// <summary>
        /// Add Swagger Documentation
        /// </summary>
        internal static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Master", Version = "v1" });

                var basePath = AppContext.BaseDirectory;
                //var xmlPath = Path.Combine(basePath, "Application.xml");
                //var sharedXmlPath = Path.Combine(basePath, "Shared.xml");

                //x.IncludeXmlComments(xmlPath);
                //x.IncludeXmlComments(sharedXmlPath);

                //Adding API http header
                x.OperationFilter<SwaggerHttpHeaderFilter>();

                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                });
                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {new OpenApiSecurityScheme{
                        Reference = new OpenApiReference
                        {
                            Id="Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                        }, new List<string>()
                    }
                });
            });

            return services;
        }

        /// <summary>
        /// Register Swagger, Swagger UI middlewares
        /// </summary>
        internal static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app, IConfiguration config)
        {
            app.UseSwagger();

            var uri = new Uri(config["App:ServerRootAddress"]);
            var basePath = uri.LocalPath.TrimEnd('/');

            app.UseSwaggerUI(c =>
            {
                c.DisplayRequestDuration();
                c.SwaggerEndpoint($"{basePath}/swagger/v1/swagger.json", "Master v1");
            });

            return app;
        }
    }
}
