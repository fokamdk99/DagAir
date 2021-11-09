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
        
        private ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public Worker(
            ISensorRabbitMqConfiguration cfg, 
            ILogger<Worker> logger,
            IServiceProvider serviceProvider
            )
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _factory = new ConnectionFactory()
            {
                HostName = cfg.HostName,
                VirtualHost = cfg.VirtualHost
            };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            
            _channel.ExchangeDeclare(cfg.SensorExchange, ExchangeType.Topic, durable: true);

            var queueName = _channel.QueueDeclare().QueueName;

            _channel.QueueBind(queue: queueName,
                exchange: cfg.SensorExchange,
                routingKey: cfg.RoutingKey);
            
            _logger.LogInformation("Sensor rabbit mq configuration: waiting for messages");
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                _channel.Dispose();
                _connection.Dispose();
                return Task.CompletedTask;
            }

            ReceiveMessages();

            return Task.CompletedTask;
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
                    //Console.WriteLine("[x][{0}] {1}", DateTime.Now, message);
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