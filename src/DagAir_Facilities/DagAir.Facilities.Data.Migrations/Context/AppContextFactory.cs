using DagAir.Facilities.Data.AppContext;

namespace DagAir.Facilities.Data.Migrations.Context
{
    public class AppContextFactory : ContextFactory<DagAirFacilitiesAppContext>
    {
        protected override string ConnectionString => "DagAir.Facilities";
    }
}