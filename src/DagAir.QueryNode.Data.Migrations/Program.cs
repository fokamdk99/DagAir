using System;
using System.Threading.Tasks;
using DagAir.IngestionNode.Data.Sharding;
using DagAir.QueryNode.Data.Migrations.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Context;

namespace DagAir.QueryNode.Data.Migrations
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

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
            const string appDatabaseName = "DagAir";
            var connectionString =
                new ConnectionString(appDatabaseName, configuration.GetConnectionString(appDatabaseName));
            return MigrateDbAsync(connectionString, new AppContextFactory());
        }
    }
}