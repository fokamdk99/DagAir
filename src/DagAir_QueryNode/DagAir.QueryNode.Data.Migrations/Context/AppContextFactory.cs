using DagAir.QueryNode.Data.AppContext;

namespace DagAir.QueryNode.Data.Migrations.Context
{
    public class AppContextFactory : ContextFactory<DagAirAppContext>
    {
        protected override string ConnectionString => "DagAir";
    }
}