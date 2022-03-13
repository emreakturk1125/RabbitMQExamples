using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace RabbitMQ.Publisher.DirectExchange.Console
{
    // Not : Direct Exchange; direk bir kuyruğu hedef alır. Route Name'ine göre o kuyruğa mesajları gönderir. Daha sonra bu kuyrukları dinleyen consumer'lar olabilir.
    // Not : kuyrular publisher tarafında oluşturulacağı için, herhangibir consumer down olması durumunda kuyruklar silinmeyecek
    // Not : Consumer ilgili kuyruğa bağlanıp mesajı okudukça, ilgili mesajlar kuyruktan silinecek

    public enum LogNames
    {
        Critical = 1,
        Error = 2,
        Warning = 3,
        Info = 4,
    }
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new System.Uri("amqps://gjyjerez:7VoqVrr1tPpEj8VP-RseNeYmUrtGngtI@tiger.rmq.cloudamqp.com/gjyjerez");

            using (var connection = factory.CreateConnection())
            {
                var channel = connection.CreateModel();

                channel.ExchangeDeclare(exchange: "logs-direct", type: ExchangeType.Direct, durable: true);     // Exchange tanımladık

                Enum.GetNames(typeof(LogNames)).ToList().ForEach(x =>
                {
                    var routeKey = $"route-{x}";
                    var queueName = $"direct-queue-{x}";
                    channel.QueueDeclare(queueName, true, false, false);
                    channel.QueueBind(queueName, "logs-direct", routeKey, null);
                });

                Enumerable.Range(1, 50).ToList().ForEach(x =>
                {
                    LogNames log = (LogNames)new Random().Next(1, 5);

                    string message = $"log-type: {log}";

                    var messageBody = Encoding.UTF8.GetBytes(message);

                    var routeKey = $"route-{log}";

                    channel.BasicPublish(exchange: "logs-direct", routingKey: routeKey, basicProperties: null, body: messageBody);
                    System.Console.WriteLine($"Log gönderilmiştir : " + message);

                });



                System.Console.ReadLine();
            }
        }
    }
}
