using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "guest",
    Password = "guest"
};

var connection = factory.CreateConnection();

var channel = connection.CreateModel();

var consumer = new EventingBasicConsumer(channel);

channel.BasicConsume("hello-queue", true, consumer);

consumer.Received += (object sender, BasicDeliverEventArgs e) =>
{
    var message = Encoding.UTF8.GetString(e.Body.ToArray());

    Console.WriteLine("Gelen Mesaj " + message);
};


//channel.QueueDeclare("hello-queue", true, false, false);

/* eğer ki QueueDeclare kısmını silersek publisher bu kuyruğu oluşturmadıysa hata verir. Eğer silmez isek publisher oluşturmadı ise subscriber tarafından kuyruk oluşturulmuş olur
 bu nedenle hata vermez. Publisherın oluşturduğundan eminsek silinebilir, parametreleride aynı olmalıdır. */

Console.ReadKey();