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

channel.QueueDeclare("hello-queue",true,false,false);

string message = "hello world";

var messageBody=Encoding.UTF8.GetBytes(message);

channel.BasicPublish(string.Empty,"hello-queue",null,messageBody);

Console.WriteLine("Mesaj Gönderilmiştir");
Console.ReadKey();