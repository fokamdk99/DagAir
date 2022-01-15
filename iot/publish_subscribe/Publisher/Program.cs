using RabbitMQ.Client;
using System;
using System.Text;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("amq.topic", ExchangeType.Topic, durable: true);

                //string message = "968376";
                string message = "23.950000;438;45.480000;wemos_stas1";
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "amq.topic",
                    //routingKey: "request_measurement",
                routingKey: "room_measurements",
                    basicProperties: null,
                    body: body);

                Console.WriteLine(" [x] Sent {0}", message);
            }

            Console.WriteLine("Press [enter] to exit");
            Console.ReadLine();
        }

        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
        }
    }
}
