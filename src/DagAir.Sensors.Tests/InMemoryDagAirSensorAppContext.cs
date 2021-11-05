using DagAir.Sensors.Data.AppContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DagAir.Sensors.Tests
{
    public class InMemoryDagAirSensorAppContextProvider : IDagAirSensorAppContextProvider
    {
        public DagAirSensorAppContext Provide()
        {
            return Provide("Sensors");
        }

        public DagAirSensorAppContext Provide(string databaseName)
        {
            var contextOptions = new DbContextOptionsBuilder<DagAirSensorAppContext>()
                .UseInMemoryDatabase(databaseName)
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .EnableSensitiveDataLogging()
                .Options;

            return new DagAirSensorAppContext(contextOptions);
        }
    }
}