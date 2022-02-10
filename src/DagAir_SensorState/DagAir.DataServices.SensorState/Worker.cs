using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DagAir.DataServices.SensorState.Infrastructure.Configuration;
using DagAir.DataServices.SensorState.Measurements.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DagAir.DataServices.SensorState
{
    public class Worker : BackgroundService
    {
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

        struct SensorMeasurement
        {
            public SensorMeasurement(float temperature, int illuminance, float humidity)
            {
                Temperature = temperature;
                Illuminance = illuminance;
                Humidity = humidity;
            }
            
            public float Temperature;
            public int Illuminance;
            public float Humidity;
        }

        private void ReceiveMessages()
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                INewMeasurementReceivedHandler newMeasurementReceivedHandler =
                    scope.ServiceProvider.GetRequiredService<INewMeasurementReceivedHandler>();

                IMeasurementDeserializer measurementDeserializer =
                    scope.ServiceProvider.GetRequiredService<IMeasurementDeserializer>();
                
                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    
                    _logger.LogInformation("[x][{0}] {1}", DateTime.Now, message);

                    var newMeasurementReceived = measurementDeserializer.DeserializeMeasurement(message);
                    newMeasurementReceivedHandler.Handle(newMeasurementReceived);
                };
                _channel.BasicConsume(queue: "",
                    autoAck: true,
                    consumer: consumer);
            }
        }

        
    }
}