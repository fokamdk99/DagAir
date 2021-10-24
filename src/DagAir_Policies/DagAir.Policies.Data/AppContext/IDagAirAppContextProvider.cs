namespace DagAir.Policies.Data.AppContext
{
    public interface IDagAirAppContextProvider
    {
        DagAirAppContext Provide();
    }
}