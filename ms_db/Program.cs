// See https://aka.ms/new-console-template for more information

using System.Runtime.Serialization;
using System.Text;
using Microsoft.Extensions.Logging;
using ms_db;
using ms_db.Interfaces;
using ms_db.Services;
using shared_library.Helpers;
using shared_library.Models;
using StackExchange.Redis;

public class Program
{
    public static void Main(string[] args)
    {
        ISerialize<CompletedText> serialize = new JsonSerializer<CompletedText>();
        FlowManagerService flowManagerService = new FlowManagerService(serialize);

        // Redis
        var redisWorker = new RedisConsumerService(hostName: "localhost", channel:"mykey");
        redisWorker.OnConsume += flowManagerService.RunFlow;
        redisWorker.Start();
        
        // Rabbit
        var rabbitConsumer = new RabbitConsumerService(hostName: "localhost",queueName:"hello");
        rabbitConsumer.OnConsume += flowManagerService.RunFlow;
        rabbitConsumer.Start();
        
        Console.ReadLine();
    }
}