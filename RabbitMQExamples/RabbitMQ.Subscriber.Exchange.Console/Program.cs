using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace RabbitMQ.Subscriber.FanoutExchange.Console
{
    class Program
    {
        // Not : Ör; Bu uygulamayı cmd üzerinde ayrı ayrı çalıştırıldığın farklı subscriber oluyor.
        // Not : Farklı Subscriber'lardan, Farklı kuyruklara, farklı kuyruklardan da Exchange'e bağlanma işlemi
        // Not : random isimli kuyrukları burada oluşturduk
        // Not : Subscriber'lar down olduğu zaman, RabbitMQ daki kuyruklar otomatik silinecek
        // Not : Bu subscriber uygulaması çalıştıktan sonra, publisher uygulaması mesaj yollarsa eğer subscriber uygulaması mesajları alır.
        // Not : Bu uygulamada Kuyruklar random isimlerle oluşacağı için ilk bu uygulama çalışmalı ve kuyruk oluşmalı daha sonra publisher çalışmalıdır ki mesajlar okunabilsin
        // Aksi durumda ilk publisher çalışırsa mesaj RabbitMQ daki o an var olan kuyruklara yollanmış olacak,
        // daha sonra subscriber çalışırsa mesaj yollandıktan sonra kuyruk oluşacağı için subscriber mesajları okuayamacak
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new System.Uri("amqps://gjyjerez:7VoqVrr1tPpEj8VP-RseNeYmUrtGngtI@tiger.rmq.cloudamqp.com/gjyjerez");

            using (var connection = factory.CreateConnection())
            {
                var channel = connection.CreateModel();
                 
                // Subscriber'lar aynı kuyruğua bağlanmasın diye random kuyruk adı oluşturuyoruz
                var randomQueueName = channel.QueueDeclare().QueueName;

                // Kuyruk subscriber tarafında oluşturulduğu için alınan mesaj kuyruğa bind edilmelidir.
                channel.QueueBind(randomQueueName, "logs-fanout", "", null);

                channel.BasicQos(0, 1, false);

                var consumer = new EventingBasicConsumer(channel);

                channel.BasicConsume(randomQueueName, false, consumer);

                System.Console.WriteLine("Loglar dinleniyor..");

                consumer.Received += (object sender, BasicDeliverEventArgs e) =>
                {
                    var message = Encoding.UTF8.GetString(e.Body.ToArray());
                    System.Console.WriteLine("Gelen Mesaj : " + message);
                    channel.BasicAck(e.DeliveryTag, false);                                    // Mesajı işlediğimiz için RabbitMQ'ya bilgi verdik, ilgili mesaj silebilirsin demektir.    

                };
                 
                System.Console.ReadLine();
            }
        }

    }
}
