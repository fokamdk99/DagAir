namespace DagAir.PolicyNode.Data.AppContext
{
    public interface IDagAirAppContextProvider
    {
        DagAirAppContext Provide();
    }
}