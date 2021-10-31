using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DagAir.Policies.Data.AppContext
{
    public class DagAirPoliciesAppContextProvider : IDagAirAppContextProvider
    {
        private const string ConnectionStringName = "DagAir.Policies";
        private readonly string _connectionString;
        private readonly ILoggerFactory _loggerFactory;

        public DagAirPoliciesAppContextProvider(IConfiguration configuration, ILoggerFactory loggerFactory)
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
        public DagAirPoliciesAppContext Provide()
        {
            var contextOptions = new DbContextOptionsBuilder<DagAirPoliciesAppContext>()
                .UseLoggerFactory(_loggerFactory)
                .UseMySQL(_connectionString)
                .Options;

            return new DagAirPoliciesAppContext(contextOptions);
        }
    }
}