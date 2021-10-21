using DagAir.PolicyNode.Data.AppContext;

namespace DagAir.PolicyNode.Data.Migrations.Context
{
    public class AppContextFactory : ContextFactory<DagAirAppContext>
    {
        protected override string ConnectionString => "DagAir.PolicyNode";
    }
}