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
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            using (var channel2 = connection.CreateModel())
            {
                channel.ExchangeDeclare("amq.topic", ExchangeType.Topic, durable: true);

                var queueName = channel.QueueDeclare().QueueName;
                Console.WriteLine($"queue name: {queueName}");

                channel.QueueBind(queue: queueName,
                    exchange: "amq.topic",
                    routingKey: "request_measurement"); //room_measurements
                
                channel2.ExchangeDeclare("amq.topic", ExchangeType.Topic, durable: true);

                Console.WriteLine(" [*] Waiting for logs1");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine("[x][{0}] {1}", DateTime.Now, message);
                    if (ea.RoutingKey == "request_measurement")
                    {
                        string responseMessage = "53.950000;438;45.480000;wemos_stas1";
                        var responseBody = Encoding.UTF8.GetBytes(responseMessage);
                        channel2.BasicPublish(exchange: "amq.topic",
                            routingKey: "room_measurements",
                            basicProperties: null,
                            body: responseBody);

                        Console.WriteLine(" [x] Sent {0}", message);
                    }
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
