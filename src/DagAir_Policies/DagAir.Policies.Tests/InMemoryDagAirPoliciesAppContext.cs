using DagAir.Policies.Data.AppContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DagAir.Policies.Tests
{
    public class InMemoryDagAirPoliciesAppContextProvider : IDagAirPoliciesAppContextProvider
    {
        public DagAirPoliciesAppContext Provide()
        {
            return Provide("Policies");
        }

        public DagAirPoliciesAppContext Provide(string databaseName)
        {
            var contextOptions = new DbContextOptionsBuilder<DagAirPoliciesAppContext>()
                .UseInMemoryDatabase(databaseName)
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .EnableSensitiveDataLogging()
                .Options;

            return new DagAirPoliciesAppContext(contextOptions);
        }
    }
}