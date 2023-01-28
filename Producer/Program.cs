using System;
using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory{ HostName = "localhost" };
//Create connection
using var connection = factory.CreateConnection();
//Create channel
using var channel = connection.CreateModel();
channel.QueueDeclare(queue: "order", durable:false, exclusive:false, autoDelete:false, arguments: null);

var message = "This is message from producer.";
var encodedMessage = Encoding.UTF8.GetBytes(message);

//publish message
channel.BasicPublish("", "order", null, encodedMessage);
Console.WriteLine("Message is published");