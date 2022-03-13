using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace _4RabbitMQ.Publisher.TopicExchange.Console
{
    //  Not : Burada kuyruğu consumer oluşturur.
    //  Not : Gelen mesajı detaylı route a göre ilgili kuyruk'a yollar. Consumer nasıl bir data istiyorsa ona göre route key oluştursun ve kuyruğu okusun
    //  Not : String ifadeler nokta ile ayrılır.Ör : *.Error.*
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

                channel.ExchangeDeclare(exchange: "logs-topic", type: ExchangeType.Topic, durable: true);     // Exchange tanımladık

                Random rnd = new Random();

                Enumerable.Range(1, 50).ToList().ForEach(x =>
                {
                     
                    LogNames log1 = (LogNames)rnd.Next(1, 5);
                    LogNames log2 = (LogNames)rnd.Next(1, 5);
                    LogNames log3 = (LogNames)rnd.Next(1, 5);
                    var routeKey = $"{log1}.{log2}.{log3}";

                    string message = $"log-type: {log1}-{log2}-{log3}";
                    var messageBody = Encoding.UTF8.GetBytes(message);
                      
                    channel.BasicPublish(exchange: "logs-topic", routingKey: routeKey, basicProperties: null, body: messageBody);
                    System.Console.WriteLine($"Log gönderilmiştir : " + message);

                });



                System.Console.ReadLine();
            }
        }
    }
}
