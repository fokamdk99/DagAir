using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "192.168.0.12" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("amq.topic", ExchangeType.Topic, durable: true);

                var queueName = channel.QueueDeclare().QueueName;
                Console.WriteLine($"queue name: {queueName}");

                channel.QueueBind(queue: queueName,
                    exchange: "amq.topic",
                    routingKey: "room_measurements");

                Console.WriteLine(" [*] Waiting for logs");

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

                Console.WriteLine("Press [enter] to exit");
                Console.ReadLine();
            }
        }
    }
}
