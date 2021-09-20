using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMqSender
{
    class Program
    {
        static void Main(string[] args)
        {
            var message = "";
            var continueMessaging = false;

            do
            {
                Console.WriteLine("Send a message to the consumer:");
                message = Console.ReadLine();
                SendMessage(message);

                Console.WriteLine("Continue messaging?: y/n");
                continueMessaging = Console.ReadLine().Substring(0, 1) == "y" ? true : false;
                
            } while (continueMessaging);
        }

        static void SendMessage(string messageText)
        {
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


                int countTimes = 1;

                while (countTimes <= 20)
                {
                    string message = $"Message #{countTimes} - {messageText}";
                    var body = Encoding.UTF8.GetBytes(message);
                    
                    channel.BasicPublish(
                        exchange: "",
                        routingKey: CONFIG_QUEUE,
                        basicProperties: null,
                        body: body);

                    countTimes++;
                }

            }
        }
    }
}
