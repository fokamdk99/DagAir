using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DagAir.IngestionNode.Infrastructure
{
    public static class RabbitMqConsumer
    {
        private const string ExchangeName = "amq.topic";
        private const string RoutingKey = "room_measurements";
        public static EventingBasicConsumer CreateNewConsumer()
        {
            var factory = new ConnectionFactory() {HostName = "192.168.0.12"};
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(ExchangeName, ExchangeType.Topic, durable: true);

                var queueName = channel.QueueDeclare().QueueName;

                channel.QueueBind(queue: queueName,
                    exchange: ExchangeName,
                    routingKey: RoutingKey);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine("[x][{0}] {1}", DateTime.Now, message);
                };
                channel.BasicConsume(queue: "",
                    autoAck: true,
                    consumer: consumer);
                
                return consumer;
            }
        }
    }
}