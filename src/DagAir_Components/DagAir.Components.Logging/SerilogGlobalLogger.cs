using Microsoft.Extensions.Configuration;
using Serilog;

namespace DagAir.Components.Logging
{
    public static class SerilogGlobalLogger
    {
        public static void ConfigureGlobalLogger(IConfiguration configuration, string environment)
        {
            var loggerConfiguration = new LoggerConfiguration();
            
            loggerConfiguration.ConfigureDagAirLogger(configuration, environment);

            Log.Logger = loggerConfiguration.CreateLogger();
        }
    }
}