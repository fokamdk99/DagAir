using System;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using ILogger = Serilog.ILogger;
using System.Linq;

namespace DagAir.IngestionNode.Migrations.Context
{
    public class ContextFactory<T> : IDesignTimeDbContextFactory<T> where T :DbContext, new()
    {
        protected virtual string ConnectionString => string.Empty;
        private string AppSettingsJsonFile => "AppSettingsJsonFile.json";
        private readonly IConfigurationRoot _configuration;
        private static readonly ILogger Logger = Log.Logger = new LoggerConfiguration()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

        public ContextFactory() : base()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(AppSettingsJsonFile)
                .Build();
        }

        public ContextFactory(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        public T CreateDbContext(string[] args)
        {
            var connectionString = args.FirstOrDefault() ?? _configuration.GetConnectionString(ConnectionString);
            return CreateDbContext(connectionString);
        }

        public T CreateDbContext(string connectionString)
        {
            var options = CreateDefaultDbContextOptions(connectionString);
            var context = Activator.CreateInstance(typeof(T), options) as T;

            return context;
        }
        
        private static DbContextOptions<T> CreateDefaultDbContextOptions(string connectionString) => 
        new DbContextOptionsBuilder<T>()
            .LogTo(action: Logger.Information, filter: MigrationInfoLogFilter(), options: DbContextLoggerOptions.None)
            .UseSqlServer(connectionString, x => x.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName))
            .Options;

        private static Func<EventId, LogLevel, bool> MigrationInfoLogFilter() => (eventId, level) =>
            level > LogLevel.Information ||
            (level == LogLevel.Information &&
             new[]
             {
                 RelationalEventId.MigrationApplying,
                 RelationalEventId.MigrationAttributeMissingWarning,
                 RelationalEventId.MigrationGeneratingDownScript,
                 RelationalEventId.MigrationGeneratingUpScript,
                 RelationalEventId.MigrationReverting,
                 RelationalEventId.MigrationsNotApplied,
                 RelationalEventId.MigrationsNotFound,
                 RelationalEventId.MigrateUsingConnection
             }.Contains(eventId));

    }
}