using System.Threading.Tasks;

namespace DagAir.AdminNode.Hubs
{
    public interface IAdminNodeHub
    {
        Task PoliciesEvaluationResultEvent(string message);
        Task GetCurrentMeasurement(string roomId);
    }
}