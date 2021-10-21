using System;
using Serilog;
using Serilog.Configuration;

namespace DagAir.Components.Logging.Enrichers
{
    public static class EnricherInstaller
    {
        private const string BaseServiceName = "DagAir";

        public static LoggerConfiguration WithHostName(this LoggerEnrichmentConfiguration enrich)
        {
            if (enrich == null)
            {
                throw new ArgumentNullException(nameof(enrich));
            }

            return enrich.With<HostNameEnricher>();
        }
        
        public static LoggerConfiguration WithLoggerName(this LoggerEnrichmentConfiguration enrich)
        {
            if (enrich == null)
            {
                throw new ArgumentNullException(nameof(enrich));
            }

            return enrich.With<LoggerNameEnricher>();
        }
    }
}