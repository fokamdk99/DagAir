using DagAir.Sensors.Data.AppContext;

namespace DagAir.Sensors.Data.Migrations.Context
{
    public class AppContextFactory : ContextFactory<DagAirSensorAppContext>
    {
        protected override string ConnectionString => "DagAir.Sensors";
    }
}