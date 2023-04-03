using System;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using Lila.Platform.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Master.Domain.Accounts;
using Master.Data.Providers.Accounts;
using Master.Domain.Logging;
using Master.Data.Models.Account;
using Master.Data.Models.Role;
using Microsoft.AspNetCore.Builder;
using Master.Manager.Logging;
using Master.Domain.Trips;
using Master.Data.Providers.TripProvider;
using Master.Data.Providers.BookTripProvider;

namespace Master.Data
{
    public static class DependencyInjection
    {
        const string ConnectionStringName = "DefaultConnection";
        const bool SeedData = true;

        public static IServiceCollection ConfigureDataModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MasterDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(ConnectionStringName)));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddProviders();

            return services;
        }

        public static async Task MigrateAndSeedDatabaseAsync(this IApplicationBuilder builder)
        {
            var scope = builder.ApplicationServices.CreateAsyncScope();

            try
            {
                var context = scope.ServiceProvider.GetRequiredService<MasterDbContext>();

                if (context.Database.IsSqlServer())
                {
                    await context.Database.MigrateAsync();
                }

                if (SeedData)
                {
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserAccount>>();
                    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<UserRole>>();

                    await MasterDBContextSeed.SeedSuperAdminAsync(context, roleManager, userManager);

                        await MasterDBContextSeed.SeedStaticRolesAsync(roleManager);
                        await MasterDBContextSeed.SeedDefaultUserAsync(userManager, roleManager);
                        await MasterDBContextSeed.SeedDefaultSettingsAsync(context);

                }
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<DbContext>>();

                logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                throw;
            }
        }

        private static void AddProviders(this IServiceCollection services)
        {
            services.AddTransient<IAccountProvider, AccountProvider>();
            services.AddTransient<ITripProvider, TripProvider>();
            services.AddTransient<IBookTripProvider, BookingProvider>();
            services.AddScoped<ILoggerManager, LoggerManager>();

        }
    }
}
