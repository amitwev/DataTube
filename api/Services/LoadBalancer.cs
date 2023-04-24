using api.Services;
using shared_library.Models;

namespace api.Services;

public class LoadBalancer
{
    private static int _balance = 0;
    private static RedisProducerService _redisProducerService;
    private static RabbitProducerService _rabbitProducerService;

    public LoadBalancer()
    {
        _redisProducerService = new RedisProducerService();
        _rabbitProducerService = new RabbitProducerService();
    }
    
    public async Task<bool> BalanceRequests(CompletedText completedText)
    {
        bool isOk = true;
        
        switch (_balance % 2)
        {
            case 0:
                await _redisProducerService.Send(completedText);
                break;
            case 1:
                //_rabbitProducerService.Produce();
                break;
        }

        _balance += 1;
        return isOk;
    }
    
    
}