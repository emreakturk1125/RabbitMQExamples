using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace _4RabbitMQ.Subscriber.Topic.Exchange.Console
{
    // Detaylı routing işlemleri için
    // String ifadeler nokta ile ayrılır.
    // Ör : *.Error.*  içinde Error geçen kuyruklar
    // Ör : #.Error  sonu Error ile biten kuyruklar
    // Varyasyon çok olduğu için consumer nasıl bir data istiyorsa ona göre kuyruk oluşturur.

    class Program
    { 
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new System.Uri("amqps://gjyjerez:7VoqVrr1tPpEj8VP-RseNeYmUrtGngtI@tiger.rmq.cloudamqp.com/gjyjerez");

            using (var connection = factory.CreateConnection())
            {
                var channel = connection.CreateModel();

                channel.BasicQos(0, 1, false);

                var consumer = new EventingBasicConsumer(channel);

                var queueName = channel.QueueDeclare().QueueName;
                //var routeKey = "*.Error.*";      // * tek bir karaktere karşılık gelir
                var routeKey = "#.Warning";        // # birde fazla karaktere karşılık gelir.
                channel.QueueBind(queueName, "logs-topic", routeKey);

                channel.BasicConsume(queueName, false, consumer);

                System.Console.WriteLine("Loglar dinleniyor..");

                consumer.Received += (object sender, BasicDeliverEventArgs e) =>
                {
                    var message = Encoding.UTF8.GetString(e.Body.ToArray());

                    Thread.Sleep(1000);
                    System.Console.WriteLine("Gelen Mesaj : " + message);

                    // File.AppendAllText("log-critical.txt", message + "\n");

                    channel.BasicAck(e.DeliveryTag, false);                                    // Mesajı işlediğimiz için RabbitMQ'ya bilgi verdik, ilgili mesaj silebilirsin demektir.    

                };

                System.Console.ReadLine();
            }
        }
    }
}
