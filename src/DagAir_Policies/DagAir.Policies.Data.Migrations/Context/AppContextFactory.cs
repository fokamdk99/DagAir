using DagAir.Policies.Data.AppContext;

namespace DagAir.Policies.Data.Migrations.Context
{
    public class AppContextFactory : ContextFactory<DagAirPoliciesAppContext>
    {
        protected override string ConnectionString => "DagAir.Policies";
    }
}