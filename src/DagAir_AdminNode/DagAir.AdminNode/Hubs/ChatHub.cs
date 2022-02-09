using System.Threading.Tasks;
using DagAir.AdminNode.CurrentMeasurements;
using DagAir.AdminNode.Sensors;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace DagAir.AdminNode.Hubs
{
    public class ChatHub : Hub<IAdminNodeHub>
    {
        private readonly ILogger<ChatHub> _logger;
        private readonly ICurrentMeasurementsHandler _currentMeasurementsHandler;
        private readonly ISensorsHandler _sensorsHandler;

        public ChatHub(ILogger<ChatHub> logger, 
            ICurrentMeasurementsHandler currentMeasurementsHandler, 
            ISensorsHandler sensorsHandler)
        {
            _logger = logger;
            _currentMeasurementsHandler = currentMeasurementsHandler;
            _sensorsHandler = sensorsHandler;
        }

        public async Task SubscribeToPoliciesEvaluationResultEvent(string uniqueRoomId)
        {
            _logger.LogInformation($"Admin app sent a request to subscribe to {uniqueRoomId}");
            await Groups.AddToGroupAsync(Context.ConnectionId, uniqueRoomId);
        }
        
        public async Task GetCurrentMeasurement(string roomIdentifier)
        {
            long roomId = long.Parse(roomIdentifier); 
            _logger.LogInformation($"Admin app sent a request for current measurement for room with id {roomId}");
            var sensorDto = await _sensorsHandler.GetSensorByRoomId(roomId);
            await _currentMeasurementsHandler.Handle(sensorDto.SensorName);
        }
    }
}