using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace DagAir.ClientNode.Hubs
{
    public class ChatHub : Hub<IClientNodeHub>
    {
        public async Task SendMessage(string user, string message)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "test_group");
            await Clients.Group("test_group").ReceiveMessage(user, message);
        }
    }
}