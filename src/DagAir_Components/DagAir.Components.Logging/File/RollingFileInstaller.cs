using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Configuration;

namespace DagAir.Components.Logging.File
{
    public static class RollingFileInstaller
    {
        public static LoggerConfiguration RollingFile(
            this LoggerSinkConfiguration loggerSinkConfiguration,
            IConfiguration configuration,
            RollingInterval rollingInterval = RollingInterval.Day,
            bool rollOnFileSizeLimit = true)
        {
            return loggerSinkConfiguration.File(
                configuration["logging:file:path"],
                rollingInterval: rollingInterval,
                rollOnFileSizeLimit: rollOnFileSizeLimit
                );
        }
    }
}