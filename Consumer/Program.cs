﻿using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory{ HostName = "localhost" };
//Create connection
using var connection = factory.CreateConnection();
//Create channel
using var channel = connection.CreateModel();
channel.QueueDeclare(queue: "order", durable:false, exclusive:false, autoDelete:false, arguments: null);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (ModuleHandle, ea) => {
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine("Message Received:{0}", message);
};

channel.BasicConsume(queue:"order", autoAck: true, consumer:consumer);

Console.ReadKey();
