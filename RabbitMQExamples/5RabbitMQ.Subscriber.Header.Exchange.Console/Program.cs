using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace _5RabbitMQ.Subscriber.Header.Exchange.Console
{
    // Not : Header üzerinde okuma işlemleridir.
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new System.Uri("amqps://gjyjerez:7VoqVrr1tPpEj8VP-RseNeYmUrtGngtI@tiger.rmq.cloudamqp.com/gjyjerez");

            using (var connection = factory.CreateConnection())
            {
                var channel = connection.CreateModel();

                channel.ExchangeDeclare(exchange: "logs-header-exchange", type: ExchangeType.Headers, durable: true);     // Exchange tanımladık yoksa oluşturacak varsa oluşturmayacak, hata almaması için koyduk

                channel.BasicQos(0, 1, false);

                var consumer = new EventingBasicConsumer(channel);

                var queueName = channel.QueueDeclare().QueueName;

                Dictionary<string, object> headers = new Dictionary<string, object>();
                headers.Add("format", "pdf");
                headers.Add("shape", "a4");
                headers.Add("x-match", "all");   // Consumer'ın okuma yapabilimesi için "x-match" : "all"  dersen bütün key'ler eşit olmalıdır. "x-match" : "any"  key'lerden herhangibir aeşit olması yeterlidir

                channel.QueueBind(queueName, "logs-header-exchange", string.Empty,headers);

                channel.BasicConsume(queueName, false, consumer);

                System.Console.WriteLine("Loglar dinleniyor..");

                consumer.Received += (object sender, BasicDeliverEventArgs e) =>
                {
                    var message = Encoding.UTF8.GetString(e.Body.ToArray());

                    Thread.Sleep(1000);
                    System.Console.WriteLine("Gelen Mesaj : " + message);
                     

                    channel.BasicAck(e.DeliveryTag, false);                                    // Mesajı işlediğimiz için RabbitMQ'ya bilgi verdik, ilgili mesaj silebilirsin demektir.    

                };

                System.Console.ReadLine();
            }
        }
    }
}
