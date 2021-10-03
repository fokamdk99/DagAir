using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DagAir.IngestionNode.Consumers;
using MassTransit;
using MassTransit.NewIdProviders;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DagAir.IngestionNode
{
    public class Worker : BackgroundService
    {
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;

        public Worker()
        {
            _factory = new ConnectionFactory() { HostName = "192.168.0.12" };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            
            _channel.ExchangeDeclare("amq.topic", ExchangeType.Topic, durable: true);

            var queueName = _channel.QueueDeclare().QueueName;

            _channel.QueueBind(queue: queueName,
                exchange: "amq.topic",
                routingKey: "room_measurements");
            
            Console.WriteLine("RABBITMQ: waiting for messages!123");
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