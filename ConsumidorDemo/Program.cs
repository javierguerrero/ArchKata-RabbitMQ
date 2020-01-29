using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace ConsumidorDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string cola;
            Console.WriteLine("Ingrese la cola...");
            cola = Console.ReadLine();

            var connectionFactory = new ConnectionFactory()
            {
                UserName = "kajpjxqe",
                Password = "LQE0ncQ-row6e0NVD2zb7Jexo8Gyap5Q",
                VirtualHost = "kajpjxqe",
                HostName = "kajpjxqe",
                Uri = new Uri("amqp://kajpjxqe:LQE0ncQ-row6e0NVD2zb7Jexo8Gyap5Q@rhino.rmq.cloudamqp.com/kajpjxqe")
            };

            using (var conn = connectionFactory.CreateConnection())
            {
                using (var channel = conn.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: cola,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(cola, true, consumer);
                    Console.WriteLine("Esperando por los mensajes, Ctrl + C para salir...");
                    while (true)
                    {
                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("Recibido: {0}", message);
                    }
                }
            }
        }
    }
}
