using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Master.Domain.Accounts;
using Master.Domain.Logging;
using Master.Domain.Passenger;
using Master.Domain.Trips;
using Master.Manager.Accounts;
using Master.Manager.Logging;
using Master.Manager.Predection;
using Master.Manager.TripsManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Master.Manager
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureManagerModule(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<IAccountManager, AccountManager>();
            services.AddTransient<IAccountManager, AccountManager>();
            services.AddScoped<ILoggerManager, LoggerManager>();
            services.AddTransient<ITripManager, TripManager>();
            services.AddTransient<IPassengerManager, PassengerManager>();


            return services;
        }
    }
}
