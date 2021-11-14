using System.Threading.Tasks;

namespace DagAir.ClientNode.Hubs
{
    public interface IClientNodeHub
    {
        Task ReceiveMessage(string user, string message);
    }
}