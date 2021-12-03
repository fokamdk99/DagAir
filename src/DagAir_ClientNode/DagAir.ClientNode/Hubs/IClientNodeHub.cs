using System.Threading.Tasks;

namespace DagAir.ClientNode.Hubs
{
    public interface IClientNodeHub
    {
        Task PoliciesEvaluationResultEvent(string message);
    }
}