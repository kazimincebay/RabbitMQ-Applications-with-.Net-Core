using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "guest",
    Password = "guest"
}; 

var connection = factory.CreateConnection();

var channel = connection.CreateModel();

channel.QueueDeclare("work-queue", true, false, false);


Enumerable.Range(1, 50).ToList().ForEach(x =>
{
    string message = $"mesaj {x}";

    var messageBody = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(string.Empty, "work-queue", null, messageBody);

    Console.WriteLine($"Mesaj Gönderilmiştir : {message}");
});



Console.ReadKey();