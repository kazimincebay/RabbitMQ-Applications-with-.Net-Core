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

//channel.QueueDeclare("work-queue", true, false, false);

channel.BasicQos(0, 1, false);
/*
 BasicQos değeri, içerisinde global değeri false olursa kaç subscriber varsa hepsine ikinci indis kadar mesaj gönderir.
true olursa gelen mesaj sayısını subscriberlara böler.
birinci indis ise mesajın boyutunu belirtir 0 yazarsak herhangi bir boyutta mesaj gelebileceğini belirtir.
 
 */
var consumer = new EventingBasicConsumer(channel);

channel.BasicConsume("work-queue", false, consumer);



consumer.Received += (object sender, BasicDeliverEventArgs e) =>
{
    var message = Encoding.UTF8.GetString(e.Body.ToArray());

    Thread.Sleep(1500);

    Console.WriteLine("Gelen Mesaj : " + message);

    channel.BasicAck(e.DeliveryTag,false);
    /*
     basicAck işlenen mesajların silinmesini sağlar
    ikinci indis ise multiple değeridir eğer true olursa işlenene ama silinmeyen mesaj varsa onları da iletir ama false olursa yalnızca bahse konu mesaj silinir.
     */
};



Console.ReadKey();