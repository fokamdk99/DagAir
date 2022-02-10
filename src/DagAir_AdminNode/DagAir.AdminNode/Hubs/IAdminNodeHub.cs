using System.Threading.Tasks;
using DagAir.PolicyNode.Contracts.Contracts;

namespace DagAir.AdminNode.Hubs
{
    public interface IAdminNodeHub
    {
        Task PoliciesEvaluationResultEvent(PoliciesEvaluationResultEvent policiesEvaluationResultEvent);
        //Task PoliciesEvaluationResultEvent(string message);
        Task GetCurrentMeasurement(string roomId);
    }
}