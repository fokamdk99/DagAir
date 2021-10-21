using Serilog.Core;
using Serilog.Events;

namespace DagAir.Components.Logging.Enrichers
{
    public class LoggerNameEnricher : ILogEventEnricher
    {
        private const string LoggerNamePropertyName = "logger_name";
        private const string OriginalPropertyName = "sourceContext";
        
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent.Properties.TryGetValue(OriginalPropertyName, out var property))
            {
                logEvent.RemovePropertyIfPresent(OriginalPropertyName);
                
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(LoggerNamePropertyName, property.ToString()));
            }
        }
    }
}