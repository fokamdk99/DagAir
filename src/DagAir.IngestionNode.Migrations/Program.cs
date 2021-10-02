using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DagAir.IngestionNode.Data.Buckets;
using DagAir.IngestionNode.Data.Influx;
using DagAir.IngestionNode.Data.Sharding;
using DagAir.IngestionNode.Data.UserContext;
using DagAir.IngestionNode.Migrations.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Context;
using Serilog.Core;

namespace DagAir.IngestionNode.Migrations
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var migrationTasks = CreateUserDbMigrationTasks(configuration).ToList();

            try
            {
                Logger.Information("Starting parallel execution of pending migrations...");
                await Task.WhenAll(migrationTasks);
            }
            catch
            {
                Logger.Warning("Parallel execution of pending migrations is complete with error(s).");
            }

            Logger.Information("Parallel execution of pending migrations is complete");

            if (args.Contains("create_bucket"))
            {
                await CreateInfluxBucketTask(configuration);
            }
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

        private static async Task CreateInfluxBucketTask(IConfiguration configuration)
        {
            var influxConfigurationProvider = new InfluxConfigurationProvider(configuration, null);
            var influxCfg = influxConfigurationProvider.Provide();
            await CreateBucketService.CreateBucket(
                influxCfg.Token,
                influxCfg.BucketName,
                influxCfg.OrgId,
                influxCfg.Url,
                influxCfg.Retention
            );
        }

        private static IEnumerable<Task> CreateUserDbMigrationTasks(IConfiguration configuration)
        {
            var connectionStringProvider = new UserConnectionStringProvider(configuration, null);
            var shardsInfo = connectionStringProvider.ProvideAll();
            return shardsInfo.Select(x => MigrateDbAsync(x, new ContextFactory<DagAirUserContext>()));
        }
    }
}