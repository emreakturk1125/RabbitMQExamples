using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _5RabbitMQ.Publisher.Header.Exchange.Console
{
    // Not : Header üzerinde okuma işlemleridir.
    // Not : Kuyruk consumer'da tanımlanır
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

                channel.ExchangeDeclare(exchange: "logs-header-exchange", type: ExchangeType.Headers, durable: true);     // Exchange tanımladık

                channel.ExchangeDelete("header-exchange");

                Dictionary<string, object> headers = new Dictionary<string, object>();
                headers.Add("format", "pdf");
                headers.Add("shape", "a4");

                var properties = channel.CreateBasicProperties();
                properties.Headers = headers;
                properties.Persistent = true;  // mesajarı kalıcı hale getirmek için

                channel.BasicPublish("logs-header-exchange", string.Empty, properties, Encoding.UTF8.GetBytes("header mesajım"));

                System.Console.WriteLine("Mesaj Gönderilmiştir.");

                System.Console.ReadLine();
            }
        }
    }
}
