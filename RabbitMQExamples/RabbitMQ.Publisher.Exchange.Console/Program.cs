using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace RabbitMQ.Publisher.Exchange.Console
{
    // *************  FANOUT EXCHANGE  *********************
    // Kuyrukları consumer'ların oluşturması daha mantıklıdır. Bu sayede çok sayıda consumer gerektiği zaman exchange bağlanıp veriyi alabilir. Kapandığı zaman da kuyruk silinebilir
    // Hava durumu tahminide kullanılabilir saatlik kuyruğu mesaj atılabilir, önceki bilgilerin önemi yoktur. Consumer ayağa kalktığı vakit olan biligiyi okur
    // Not : Kuyruk publisher tarafında oluşturulmayacak, bunun yerine exchange oluşturulacak
    // Fanout exchange : Exchange'e bağlı tüm kuyruklara filtreleme yapmadan aynı mesajları yollar. Kuyruk exchange bağlı değilse mesajı göndermez
    // Önemli : "Burası çok önemli aynı instance den oluşan birden fazla subscriber'a yollar mesajları" 
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new System.Uri("amqps://gjyjerez:7VoqVrr1tPpEj8VP-RseNeYmUrtGngtI@tiger.rmq.cloudamqp.com/gjyjerez");

            using (var connection = factory.CreateConnection())
            {
                var channel = connection.CreateModel();
                 
                // exchange   =>  exchange adı
                // type       =>  exchange tipi
                // durable    =>  uygulama restart olduğuna exchange kaybolmasın
                // autoDelete => 
                // arguments  => 

                channel.ExchangeDeclare(exchange:"logs-fanout", type: ExchangeType.Fanout, durable: true);     // Exchange tanımladık
                 
                Enumerable.Range(1, 50).ToList().ForEach(x =>
                {
                    string message = $"log - {x}";
                    var messageBody = Encoding.UTF8.GetBytes(message);

                    // exchage           => Arada Exchange kullanacaksa o belirtilir. Kullanılmayacaksa "string.Empty" yazılır, böylece publisherdan direk kuyruğa gönderilmiş olur buna da "default exchange" denir.
                    // routingKey        => Filtreleme olmayacaksa Kuyruk adı boş bırakılır
                    // basicProperties   => ?
                    // body              => message verilir

                    channel.BasicPublish(exchange:"logs-fanout", routingKey:"", basicProperties: null, body: messageBody);
                    System.Console.WriteLine($"{message} gönderilmiştir.");

                });



                System.Console.ReadLine();
            }
        }
    }
}
