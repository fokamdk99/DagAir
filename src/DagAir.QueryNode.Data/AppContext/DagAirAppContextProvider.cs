using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;

namespace DagAir.QueryNode.Data.AppContext
{
    internal class DagAirAppContextProvider : IDagAirAppContextProvider
    {
        private const string ConnectionStringName = "DagAir";
        private readonly string _connectionString;
        private readonly ILoggerFactory _loggerFactory;

        public DagAirAppContextProvider(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _connectionString = configuration.GetConnectionString(ConnectionStringName);
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException(
                    $"Connection string for '{ConnectionStringName}' is not defined. Please provide a value.");
            }
        }
        public DagAirAppContext Provide()
        {
            var contextOptions = new DbContextOptionsBuilder<DagAirAppContext>()
                .UseLoggerFactory(_loggerFactory)
                .UseMySQL(_connectionString)
                .Options;

            return new DagAirAppContext(contextOptions);
        }
    }
}