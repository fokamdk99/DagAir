#nullable enable
using System;
using Serilog.Core;
using Serilog.Events;

namespace DagAir.Components.Logging.Enrichers
{
    public class HostNameEnricher : ILogEventEnricher
    {
        private LogEventProperty? _cachedProperty;
        private const string HostNamePropertyName = "hostname";
        
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            _cachedProperty ??= CreateProperty(propertyFactory);
            logEvent.AddPropertyIfAbsent(_cachedProperty);
        }

        private static LogEventProperty CreateProperty(ILogEventPropertyFactory propertyFactory) =>
            propertyFactory.CreateProperty(HostNamePropertyName, Environment.MachineName);
    }
}