using System;
using System.Threading.Tasks;
using DagAir.Facilities.Data.Migrations.Context;
using DagAir.Facilities.Data.Sharding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Context;

namespace DagAir.Facilities.Data.Migrations
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            bool isDevelopment = false;
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (String.IsNullOrEmpty(env))
            {
                throw new Exception("ASPNETCORE_ENVIRONMENT variable is missing in the configuration files.");
            }
            else
            {
                if (env.Equals("Development"))
                {
                    isDevelopment = true;
                }
            }

            IConfigurationRoot configuration;
            if (isDevelopment)
            {
                configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables()
                    .AddUserSecrets<Startup>()
                    .Build();
            }
            else
            {
                configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables()
                    .Build();
            }
            
            var migrationTask = CreateUserDbMigrationTask(configuration);

            try
            {
                Logger.Information("Starting parallel execution of pending migrations...");
                await migrationTask;
            }
            catch
            {
                Logger.Warning("Parallel execution of pending migrations is complete with error(s).");
            }

            Logger.Information("Parallel execution of pending migrations is complete");
        }
        
        private static readonly ILogger Logger = Log.Logger = new LoggerConfiguration()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

        private static async Task MigrateDbAsync<T>(ConnectionString shardConnectionString, ContextFactory<T> factory)
            where T : DbContext, new()
        {
            using var logContext = LogContext.PushProperty("TenantName", $"{shardConnectionString.Name}");
            try
            {
                await using var context = factory?.CreateDbContext(new[] {shardConnectionString.Value});

                if (context?.Database != null)
                    await context.Database.MigrateAsync();
            }
            catch (Exception e)
            {
                Logger.Error(e, "Error occurred during migration");
                throw;
            }
        }

        private static Task CreateUserDbMigrationTask(IConfiguration configuration)
        {
            const string appDatabaseName = "DagAir.Facilities";
            var connectionString = configuration.GetConnectionString(appDatabaseName);
            connectionString = connectionString + configuration[$"ConnectionKeys:{appDatabaseName}"] + ";";
            return MigrateDbAsync(new ConnectionString(appDatabaseName, connectionString), new AppContextFactory());
        }
    }
}