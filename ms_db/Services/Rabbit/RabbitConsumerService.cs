using ms_db.Interfaces;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ms_db.Services;

public class RabbitConsumerService : IConsumer
{
    private ConnectionFactory? _factory;
    private IConnection? _connection;
    private IModel? _channel;
    private string _queueName;
    
    public RabbitConsumerService(string hostName, string queueName)
    {
        Console.WriteLine("Rabbit ms_db 0");
        _factory = new ConnectionFactory { HostName = hostName };
        Console.WriteLine("Rabbit ms_db 0.1");
        _connection = _factory.CreateConnection();
        Console.WriteLine("Rabbit ms_db 0.2");
        _channel = _connection.CreateModel();
        Console.WriteLine("Rabbit ms_db 0.3");
        _queueName = queueName;
        Console.WriteLine("Rabbit ms_db 0.4");
    }
    
    public void Start()
    {
        Console.WriteLine("Rabbit ms_db 1");

        // Delete later, only for now it is not ms_db's responsibility to declare the queue
        _channel.QueueDeclare(queue: _queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        Console.WriteLine("Rabbit ms_db 2");

        // Do stuff when a message was received
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray(); 
            OnConsume(body);
        };
        // Listen to Queue
        _channel.BasicConsume(queue: _queueName,
            autoAck: true,
            consumer: consumer);

        Console.WriteLine("Rabbit ms_db 3");
    }

    public void Stop()
    {
        throw new NotImplementedException();
    }

    public event OnConsume? OnConsume;
}