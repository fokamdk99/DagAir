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
                        Random rnd = new Random();
                        var temperature = rnd.NextDouble() * 15 + 15;
                        var illuminance = rnd.Next(100, 200);
                        var humidity = rnd.NextDouble();
                        string responseMessage = temperature + ";" + illuminance + ";" +
                                            humidity + ";" + "wemos_stas1";
                        //string responseMessage = "53.950000;438;45.480000;wemos_stas1";
                        var responseBody = Encoding.UTF8.GetBytes(responseMessage);
                        channel2.BasicPublish(exchange: "amq.topic",
                            routingKey: "room_measurements",
                            basicProperties: null,
                            body: responseBody);

                        Console.WriteLine(" [x] Sent {0}", responseMessage);
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

    public static class RandomExtensions
    {
        public static int NextInt32(this Random rng)
        {
            int firstBits = rng.Next(0, 1 << 4) << 28;
            int lastBits = rng.Next(0, 1 << 28);
            return firstBits | lastBits;
        }

        public static decimal NextDecimal(this Random rng)
        {
            byte scale = (byte) rng.Next(29);
            bool sign = rng.Next(2) == 1;
            return new decimal(rng.NextInt32(), 
                rng.NextInt32(),
                rng.NextInt32(),
                sign,
                scale);
        }
    }
}
