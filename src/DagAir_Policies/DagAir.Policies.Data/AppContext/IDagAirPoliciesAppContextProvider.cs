namespace DagAir.Policies.Data.AppContext
{
    public interface IDagAirAppContextProvider
    {
        DagAirPoliciesAppContext Provide();
    }
}