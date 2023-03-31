using System;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using Lila.Platform.Shared;
using Master.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Master.Domain.Accounts;
using Master.Data.Providers.Accounts;

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

        public static async Task MigrateAndSeedDatabaseAsync(this IServiceProvider services)
        {
            try
            {
                var context = services.GetRequiredService<MasterDbContext>();

                if (context.Database.IsSqlServer())
                {
                    await context.Database.MigrateAsync();
                }

                if (SeedData)
                {
                    var userManager = services.GetRequiredService<UserManager<UserAccount>>();
                    var roleManager = services.GetRequiredService<RoleManager<UserRole>>();

                    //await DbContextSeed.SeedOnlinePatientRoleAsync(context, roleManager);
                    await MasterDBContextSeed.SeedSuperAdminAsync(context, roleManager, userManager);
                    //await DbContextSeed.SeedDefaultTenantAsync(context);

                    foreach (var tenant in await context.Tenants.Where(x => x.IsActive && x.IsDeleted != true).ToListAsync())
                    {
                        await MasterDBContextSeed.SeedStaticRolesAsync(roleManager, tenant);
                        await MasterDBContextSeed.SeedDefaultUserAsync(userManager, roleManager, tenant);
                        await MasterDBContextSeed.SeedDefaultSettingsAsync(context, tenant);
                        //await DbContextSeed.SeedDefaultChannelsAsync(context, tenant);
                    }

                    //await DbContextSeed.SeedHostSampleDataAsync(context);
                }
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<DbContext>>();

                logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                throw;
            }
        }

        private static void AddProviders(this IServiceCollection services)
        {
            //services.AddTransient<ILegalDocumentProvider, LegalDocumentProvider>();
            services.AddTransient<IAccountProvider, AccountProvider>();

        }
    }
}
