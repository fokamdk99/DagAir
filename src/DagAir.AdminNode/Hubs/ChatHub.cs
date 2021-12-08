using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace DagAir.AdminNode.Hubs
{
    public class ChatHub : Hub<IAdminNodeHub>
    {
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(ILogger<ChatHub> logger)
        {
            _logger = logger;
        }

        public async Task SubscribeToPoliciesEvaluationResultEvent(string uniqueRoomId)
        {
            _logger.LogInformation($"Admin app sent a request to subscribe to {uniqueRoomId}");
            await Groups.AddToGroupAsync(Context.ConnectionId, uniqueRoomId);
        }
    }
}