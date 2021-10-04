using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DagAir.IngestionNode.Consumers;
using DagAir.IngestionNode.Infrastructure.Configuration;
using MassTransit;
using MassTransit.NewIdProviders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DagAir.IngestionNode
{
    public class Worker : BackgroundService
    {
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;

        public Worker(ISensorRabbitMqConfiguration cfg, ILogger<Worker> logger)
        {
            _factory = new ConnectionFactory()
            {
                HostName = cfg.HostName
            };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            
            _channel.ExchangeDeclare(cfg.SensorExchange, ExchangeType.Topic, durable: true);

            var queueName = _channel.QueueDeclare().QueueName;

            _channel.QueueBind(queue: queueName,
                exchange: cfg.SensorExchange,
                routingKey: cfg.RoutingKey);
            
            logger.LogInformation("Sensor rabbit mq configuration: waiting for messages");
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                _channel.Dispose();
                _connection.Dispose();
                return Task.CompletedTask;
            }
            
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("[x][{0}] {1}", DateTime.Now, message);
            };
            _channel.BasicConsume(queue: "",
                autoAck: true,
                consumer: consumer);

            return Task.CompletedTask;
        }
    }
}