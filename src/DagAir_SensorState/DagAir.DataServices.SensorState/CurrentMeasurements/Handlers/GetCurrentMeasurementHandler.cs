using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace DagAir.DataServices.SensorState.CurrentMeasurements.Handlers
{
    public class GetCurrentMeasurementHandler : IGetCurrentMeasurementHandler
    {
        private readonly IConnection _connection;

        private const string ExchangeName = "amq.topic";
        private const string RoutingKey = "request_measurement";

        public GetCurrentMeasurementHandler(IConnection connection)
        {
            _connection = connection;
        }

        public async Task Handle(string sensorName)
        {
            using var channel = _connection.CreateModel();
            channel.ExchangeDeclare("amq.topic", ExchangeType.Topic, durable: true);
            
            string message = sensorName; // e.g. "968376"
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: ExchangeName,
                routingKey: RoutingKey,
                basicProperties: null,
                body: body);
        }
    }
}