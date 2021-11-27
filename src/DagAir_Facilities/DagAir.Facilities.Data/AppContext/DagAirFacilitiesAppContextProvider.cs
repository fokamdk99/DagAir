using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;

namespace DagAir.Facilities.Data.AppContext
{
    internal class DagAirFacilitiesAppContextProvider : IDagAirFacilitiesAppContextProvider
    {
        private const string ConnectionStringName = "DagAir.Facilities";
        private readonly string _connectionString;
        private readonly ILoggerFactory _loggerFactory;

        public DagAirFacilitiesAppContextProvider(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            var connectionString = configuration.GetConnectionString(ConnectionStringName);
            _connectionString = connectionString + configuration[$"ConnectionKeys:{ConnectionStringName}"] + ";";
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException(
                    $"Connection string for '{ConnectionStringName}' is not defined. Please provide a value.");
            }
        }
        public DagAirFacilitiesAppContext Provide()
        {
            var contextOptions = new DbContextOptionsBuilder<DagAirFacilitiesAppContext>()
                .UseLoggerFactory(_loggerFactory)
                .UseMySQL(_connectionString)
                .Options;

            return new DagAirFacilitiesAppContext(contextOptions);
        }
    }
}