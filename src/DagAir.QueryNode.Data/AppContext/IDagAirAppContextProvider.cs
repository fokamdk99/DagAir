namespace DagAir.QueryNode.Data.AppContext
{
    public interface IDagAirAppContextProvider
    {
        DagAirAppContext Provide();
    }
}