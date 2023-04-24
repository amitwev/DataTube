using System.Text;
using api.Interfaces;
using RabbitMQ.Client;

public class RabbitProducerService : IProducer
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitProducerService()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }
     
    public void Produce()
    {
        const string message = "Hello World!";
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: string.Empty, routingKey: "hello",
            basicProperties: null, body: body);
          
        Console.WriteLine($" [x] Sent {message}");

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }
     
    // Dispose method to clean up resources
    public void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
    }
}