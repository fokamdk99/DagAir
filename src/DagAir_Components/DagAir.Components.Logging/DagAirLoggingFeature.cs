using System;
using DagAir.Components.Logging.Enrichers;
using DagAir.Components.Logging.File;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace DagAir.Components.Logging
{
    public static class DagAirLoggingFeature
    {
        public static IHostBuilder UseDagAirLogger(this IHostBuilder hostBuilder)
        {
            return hostBuilder.UseSerilog((hostBuilderContext, provider, loggerConfiguration) =>
            {
                var configuration = hostBuilderContext.Configuration;
                var environmentName = hostBuilderContext.HostingEnvironment.EnvironmentName;
                loggerConfiguration.ConfigureDagAirLogger(configuration, environmentName);
            });
        }

        public static void ConfigureDagAirLogger(this LoggerConfiguration loggerConfiguration,
            IConfiguration configuration,
            string environmentName)
        {
            loggerConfiguration
                .MinimumLevel.Information()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithHostName()
                .Enrich.WithLoggerName()
                .Enrich.WithProperty("environment", environmentName);

            loggerConfiguration = loggerConfiguration.WriteTo.Console();
            loggerConfiguration = loggerConfiguration.WriteTo.RollingFile(configuration);
            
        }
    }
}