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
        Console.WriteLine("Hello World!");
         
         
        ISerialize<CompletedText> serialize = new JsonSerializer<CompletedText>();
        FlowManagerService flowManagerService = new FlowManagerService(serialize);

        // Redis
        var redisWorker = new RedisConsumerService(hostName: "redis", channel:"mykey");
        redisWorker.OnConsume += flowManagerService.RunFlow;
        redisWorker.Start();


        // Rabbit
        try {
            var rabbitConsumer = new RabbitConsumerService(hostName: "rabbitmq", queueName: "hello");
            rabbitConsumer.OnConsume += flowManagerService.RunFlow;
            rabbitConsumer.Start();
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }
          

        while (true)
        {
            // Your application logic here

            // Wait for a specific duration before the next iteration
            Thread.Sleep(TimeSpan.FromSeconds(5)); // Adjust the duration as needed
        }

    }
}