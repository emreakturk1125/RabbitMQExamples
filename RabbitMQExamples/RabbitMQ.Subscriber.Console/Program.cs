using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace RabbitMQ.Subscriber.Console
{
    // Not : Ör; Bu uygulamayı cmd üzerinde ayrı ayrı çalıştırıldığın farklı subscriber oluyor.
    // Not : Farklı Subscriber'lardan, arada Exchange olmadan aynı kuyruğa bağlanma işlemi
    class Program
    { 
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new System.Uri("amqps://gjyjerez:7VoqVrr1tPpEj8VP-RseNeYmUrtGngtI@tiger.rmq.cloudamqp.com/gjyjerez");

            using (var connection = factory.CreateConnection())
            {
                var channel = connection.CreateModel();

                // Subscriber(Consumer) kısmında alttaki kodu;
                // Publisher'ın "hello-queue" kuyruğu oluşturduğuna emin isen kullanmayabilirsin
                // Kullanmazsan  => Subscriber ı ayağa kaldırdığınızda böyle bir kuyruk yoksa hata alırsın
                // Kullanırsan   => Böyle bir kuyruk yoksa Subscriber önce kuyruk oluşturur ve kuyrukta herhangibir hata almazsın,  varsa var olan kuyruğu kullanır
                // Bu kodun olması daha iyi olur ve parametreler publisher da olduğu gibi aynı olmalıdır. Yoksa hata verir.
                channel.QueueDeclare("hello-queue", true, false, false);

                //channel.BasicQos(1, 2, 3)
                // 1 -> prefetchSize      => 0 : herhamgibir boyuttaki mesajı gönderebilirsin demektir.
                // 2 -> prefetchCount     => her bir subcriber'a kaç mesaj gelsin
                // 3 -> global            => true : bütün subscriber'lara toplam "prefetchCount" değeri kadar mesaj gönderilecek, false : her bir subscriber'a "prefetchCount" değeri kadar mesaj gönderilecek, örneği 5 adet mesaj geldiyse kalanları memory den okuyacak
                channel.BasicQos(0, 1, false);

                var consumer = new EventingBasicConsumer(channel);

                //channel.BasicConsume("1", "2", "3");
                // 1 -> queue      => Okunacak kuyruk adı
                // 2 -> autoAck    => true : RabbitMQ subscriber'a bir mesaj gönderdiğinde bu mesaj doğruda işlense yanlışta işlense sil demektir. false : Sen kuyruktaki mesajı silme ben sana haber edeceğim demektir. Normalde en uygunu false olmasıdır
                // 3 -> consumer   => yukardaki consumer'ı ver
                channel.BasicConsume("hello-queue", false, consumer);

                // += yazıp   tab + tab tıkladığında otomatik metod oluşur, ama biz alttaki gibi tercih ettik 
                consumer.Received += (object sender, BasicDeliverEventArgs e) =>
                {
                    var message = Encoding.UTF8.GetString(e.Body.ToArray());
                    Thread.Sleep(1000);
                    System.Console.WriteLine("Gelen Mesaj : " + message);
                    channel.BasicAck(e.DeliveryTag, false);                            // Mesajı işlediğimiz için RabbitMQ'ya bilgi verdik, ilgili mesaj silebilirsin demektir.

                };

                System.Console.WriteLine("Mesaj alımıştır.");
                System.Console.ReadLine();
            }
        }

    }
}
