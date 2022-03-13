using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQ.Subscriber.FanoutExchange2.Console
{
    class Program
    {
        // Not : Ör; Bu uygulamayı cmd üzerinde ayrı ayrı çalıştırıldığın farklı subscriber oluyor.
        // Not : Farklı Subscriber'lardan, aynı kuyruğa, kuyruktanda Exchange'e bağlanma işlemi
        // Not : Kuyruk ismi sabit çünkü kuyruk RabbitMQ da sabit kalacak,  
        // Not : Subscriber'lar down olduğu zaman, RabbitMQ daki kuyruklar ***silinmeyecek**** kalıcı olacak, tekrar çalıştığı zaman kaldığı yerden devam edecek
   
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new System.Uri("amqps://gjyjerez:7VoqVrr1tPpEj8VP-RseNeYmUrtGngtI@tiger.rmq.cloudamqp.com/gjyjerez");

            using (var connection = factory.CreateConnection())
            {
                var channel = connection.CreateModel();

                var queueName = "log-database-save-queue";

                // Kuyruk kalıcı olması için QueueDeclare metodu tanımlanmalıdır. Aşağıdaki parametrelere göre;
                // Fiziksel olarak sabit diskte kaydedilsin, memory de tutulmasın
                // farklı kanllardan bu kuyruğa bağlanılsın
                // otomatik silinmesin, bu kuyruğa subcribe olan uygulama down olursa kuyruk silinemsin
                channel.QueueDeclare(queueName, true, false, false);

                channel.QueueBind(queueName, "logs-fanout", "", null);

                channel.BasicQos(0, 1, false);

                var consumer = new EventingBasicConsumer(channel);

                channel.BasicConsume(queueName, false, consumer);

                System.Console.WriteLine("Loglar dinleniyor..");

                consumer.Received += (object sender, BasicDeliverEventArgs e) =>
                {
                    var message = Encoding.UTF8.GetString(e.Body.ToArray());
                    System.Console.WriteLine("Gelen Mesaj : " + message);
                    channel.BasicAck(e.DeliveryTag, false);                                 // Mesajı işlediğimiz için RabbitMQ'ya bilgi verdik, ilgili mesaj silebilirsin demektir.

                };


                System.Console.ReadLine();
            }
        }

    }
}
