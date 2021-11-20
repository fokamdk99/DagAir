using DagAir.Addresses.Data.AppContext;

namespace DagAir.Addresses.Data.Migrations.Context
{
    public class AppContextFactory : ContextFactory<DagAirAddressesAppContext>
    {
        protected override string ConnectionString => "DagAir.Addresses";
    }
}