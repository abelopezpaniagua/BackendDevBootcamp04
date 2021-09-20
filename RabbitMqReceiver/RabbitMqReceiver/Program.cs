using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace RabbitMqReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Listening Sender Messages...:");

            var connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = ConnectionFactory.DefaultUser,
                Password = ConnectionFactory.DefaultPass
            };

            const string CONFIG_QUEUE = "configQueue";

            using (var connection = connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: CONFIG_QUEUE,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, args) =>
                {
                    var body = args.Body;
                    var message = Encoding.UTF8.GetString(body.ToArray());

                    Console.WriteLine($"Message from Sender: {message}");

                    channel.BasicAck(deliveryTag: args.DeliveryTag, multiple: false);

                    Thread.Sleep(1000);
                };

                channel.BasicConsume(queue: CONFIG_QUEUE, autoAck: false, consumer: consumer);
                Console.ReadLine();
            }
        }
    }
}
