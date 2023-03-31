using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Master.Domain.Accounts;
using Master.Domain.Logging;
using Master.Manager.Accounts;
using Master.Manager.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Master.Manager
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureManagerModule(this IServiceCollection services,
            IConfiguration configuration)
        {
            //services.AddMediatR(typeof(DependencyInjection));
            //services.AddTransient<ISettingManager, SettingManager>();
            //
            //services.AddTransient<ITenantManager, TenantManager>();
            services.AddTransient<IAccountManager, AccountManager>();
            services.AddTransient<IAccountManager, AccountManager>();
            services.AddScoped<ILoggerManager, LoggerManager>();


            return services;
        }
    }
}
