using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;

namespace DagAir.Sensors.Data.AppContext
{
    internal class DagAirSensorAppContextProvider : IDagAirSensorAppContextProvider
    {
        private const string ConnectionStringName = "DagAir";
        private readonly string _connectionString;
        private readonly ILoggerFactory _loggerFactory;

        public DagAirSensorAppContextProvider(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _connectionString = configuration.GetConnectionString(ConnectionStringName);
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException(
                    $"Connection string for '{ConnectionStringName}' is not defined. Please provide a value.");
            }
        }
        public DagAirSensorAppContext Provide()
        {
            var contextOptions = new DbContextOptionsBuilder<DagAirSensorAppContext>()
                .UseLoggerFactory(_loggerFactory)
                .UseMySQL(_connectionString)
                .Options;

            return new DagAirSensorAppContext(contextOptions);
        }
    }
}