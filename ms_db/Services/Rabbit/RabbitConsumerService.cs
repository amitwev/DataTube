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
        _factory = new ConnectionFactory { HostName = hostName };
        _connection = _factory.CreateConnection();
        _channel = _connection.CreateModel();
        _queueName = queueName;
    }
    
    public void Start()
    {
        // Delete later, only for now it is not ms_db's responsibility to declare the queue
        _channel.QueueDeclare(queue: _queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

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
    }

    public void Stop()
    {
        throw new NotImplementedException();
    }

    public event OnConsume? OnConsume;
}