using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace DagAir.ClientNode.Hubs
{
    public class ChatHub : Hub<IClientNodeHub>
    {
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(ILogger<ChatHub> logger)
        {
            _logger = logger;
        }

        public async Task SubscribeToPoliciesEvaluationResultEvent(string uniqueRoomId)
        {
            _logger.LogInformation($"Client app sent a request to subscribe to {uniqueRoomId}");
            await Groups.AddToGroupAsync(Context.ConnectionId, uniqueRoomId);
        }
    }
}