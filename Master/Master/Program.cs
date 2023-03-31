using Master.Common.Middlewares;
using Master.Configuration;
using Master.Data;
using Master.Domain.Logging;
using Master.Manager;
using Microsoft.Extensions.Localization;
using NLog;
using static Master.Domain.Authorization.Permissions;

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

var builder = WebApplication.CreateBuilder(args);

//public IConfiguration? configuration { get; private set; }

// Add services to the container.
builder.Services.ConfigureDataModule(configuration);

builder.Services.ConfigureManagerModule(configuration);

builder.Services.ConfigureApiControllers(configuration, "CorsPolicy");

builder.Services.ConfigureApiIdentity(configuration);

// Add feature management to the container of services.
//builder.Services.AddFeatureManagement();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerDocumentation();

builder.Services.AddHealthChecks();
builder.Services.ConfigureDataModule(configuration);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

var scope = app.Services.CreateScope();
ILoggerManager logger = scope.ServiceProvider.GetService<ILoggerManager>();

IWebHostEnvironment env = app.Environment;

app.UseSwaggerDocumentation(configuration);

app.UseCustomExceptionHandler(logger);

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseStaticFiles();

app.UseRequestLocalization();

app.UseResultWrapper();

app.UseRouting();

app.UseAuthentication();
app.UseRolePermissions();
app.UseAuthorization();

//if (!env.IsEnvironment("Prod"))
//    app.UseRequestBodyLogging();

app.UseUnitOfWork();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/health");
    endpoints.MapDefaultControllerRoute();
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
