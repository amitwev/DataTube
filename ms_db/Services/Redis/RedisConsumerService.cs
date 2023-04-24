using System.Text;
using ms_db.Interfaces;
using StackExchange.Redis;

namespace ms_db.Services;

public class RedisConsumerService : IConsumer
{
    private readonly ConnectionMultiplexer _redis;
    private readonly ISubscriber _sub;
    private readonly string _channel;

    public RedisConsumerService(string hostName, string channel)
    {
        _redis = ConnectionMultiplexer.Connect(hostName);
        _sub = _redis.GetSubscriber();
        _channel = channel;
    }
    
    public void Start()
    {
        Console.WriteLine("Listening to redis");

        _sub.Subscribe(_channel, (channel, message) =>
        {
            OnConsume((byte[]) message);
        });
    }

    public void Stop()
    {
        throw new NotImplementedException();
    }

    public event OnConsume? OnConsume;
}