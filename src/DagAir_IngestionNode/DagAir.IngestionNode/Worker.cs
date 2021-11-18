using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DagAir.IngestionNode.Contracts;
using DagAir.IngestionNode.Infrastructure.Configuration;
using DagAir.IngestionNode.Measurements.Commands;
using DagAir.IngestionNode.Measurements.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DagAir.IngestionNode
{
    public class Worker : BackgroundService
    {
        private const int NumberOfParameters = 4; 
        
        private IConnection _connection;
        private IModel _channel;
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly ISensorRabbitMqConfiguration _rabbitMqConfiguration;

        public Worker(
            ISensorRabbitMqConfiguration cfg, 
            ILogger<Worker> logger,
            IServiceProvider serviceProvider
            )
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _rabbitMqConfiguration = cfg;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                _connection = scope.ServiceProvider.GetRequiredService<IConnection>();
            }
            
            _channel = _connection!.CreateModel();
            _channel.ExchangeDeclare(_rabbitMqConfiguration.SensorExchange, ExchangeType.Topic, durable: true);

            var queueName = _channel.QueueDeclare().QueueName;
            _logger.LogInformation($"Queue name: {queueName}");

            _channel.QueueBind(queue: queueName,
                exchange: _rabbitMqConfiguration.SensorExchange,
                routingKey: _rabbitMqConfiguration.RoutingKey);
            
            _logger.LogInformation("Sensor rabbit mq configuration: waiting for messages");

            if (cancellationToken.IsCancellationRequested)
            {
                _channel.Dispose();
                _connection.Dispose();
                return;
            }

            ReceiveMessages();
        }

        private void ReceiveMessages()
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                INewMeasurementReceivedHandler newMeasurementReceivedHandler =
                    scope.ServiceProvider.GetRequiredService<INewMeasurementReceivedHandler>();
                
                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    _logger.LogInformation("[x][{0}] {1}", DateTime.Now, message);

                    var newMeasurementReceived = DeserializeMeasurement(message);
                    newMeasurementReceivedHandler.Handle(newMeasurementReceived);
                };
                _channel.BasicConsume(queue: "",
                    autoAck: true,
                    consumer: consumer);
            }
        }

        private NewMeasurementReceivedCommand DeserializeMeasurement(string message)
        {
            try
            {
                string[] parameters = message.Split(";");
                if (parameters.Length != NumberOfParameters)
                {
                    throw new Exception(
                        $"Measurement sent by a sensor is invalid. Excepted number of parameters: {NumberOfParameters}, given: {parameters.Length}");
                }

                var measurement = new RoomMeasurement(
                    float.Parse(parameters.ElementAt(0)), 
                    float.Parse(parameters.ElementAt(1)), 
                    float.Parse(parameters.ElementAt(2)));
                return new NewMeasurementReceivedCommand(measurement, parameters.ElementAt(3));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception();
            }
        }
    }
}