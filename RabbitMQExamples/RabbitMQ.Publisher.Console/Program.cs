

using RabbitMQ.Client;
using System.Linq;
using System.Text;

namespace RabbitMQ.Publisher.Console
{
    // Not : Kuyruğu burada oluşturduk
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new System.Uri("amqps://gjyjerez:7VoqVrr1tPpEj8VP-RseNeYmUrtGngtI@tiger.rmq.cloudamqp.com/gjyjerez");

            using (var connection = factory.CreateConnection())
            {
                var channel = connection.CreateModel();

                // queue       =>   Kuyruk adı
                // durable     =>   false : kuyruklar bellekte tutulur. restart atılırsa kuyrukla silinir,  true : restart atılsa dahi kuyruk kaybolmaz
                // exclusive   =>   true : sadece yukarda oluşturduğun (channel) kanal üzerinden bağlan demektir,  false : başka bir yerde oluşturduğun kanal üzerinden bağlan demektir
                // autoDelete  =>   kuyruğa bağlı olan son subscriber da bağlantısını koparırsa otomatik olarak kuyruğu sil demektir. true : otomatik silinsin, false : otomatik silinmesin

                channel.QueueDeclare(queue: "hello-queue",durable: true,exclusive: false,autoDelete: false);

                Enumerable.Range(1, 50).ToList().ForEach(x =>
                 {
                     string message = $"Message - {x}";
                     var messageBody = Encoding.UTF8.GetBytes(message);

                     // exchage           => Arada Exchange kullanacaksa o belirtilir. Kullanılmayacaksa "string.Empty" yazılır, böylece publisherdan direk kuyruğa gönderilmiş olur buna da "default exchange" denir.
                     // routingKey        => Mesaj hangi kuyruğa yollanacaksa o kuyruğun adı verilir. Boş bırakılırsa filtre yapmadan bütün kuyruklara gönder demektir.
                     // basicProperties   => ?
                     // body              => message verilir

                     channel.BasicPublish(exchange: string.Empty,routingKey: "hello-queue",basicProperties: null,body: messageBody);
                     System.Console.WriteLine($"{message} gönderilmiştir.");

                 });

              

                System.Console.ReadLine();
            }
        }
    }
}
