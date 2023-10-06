using System.Net;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using shared_library.Helpers;
using shared_library.Models;
using StackExchange.Redis;

namespace api.Controllers;

[ApiController]
public class AddDataController : Controller
{
    private readonly AddDataService _addDataService;
    private readonly LoadBalancer _loadBalancer;
    private readonly ILogger<AddDataController> _logger;

    public AddDataController(AddDataService addDataService, ILogger<AddDataController> logger)
    {
        _addDataService = addDataService;
        _logger = logger;
        _loadBalancer = new LoadBalancer();
    }
    
    [Route("add-data")]
    [HttpPost]
    public async Task<IActionResult> AddData([FromBody] TextDTO textDto)
    {
        var completedText = _addDataService.TextDtoToTextComplete(textDto, Request.Headers);
        bool result = await _loadBalancer.BalanceRequests(completedText);
        return result ? Ok(completedText) : Ok(HttpStatusCode.InternalServerError);
    }
    
    [Route("add-file")]
    [HttpPost]
    public IActionResult AddFile()
    {
        return Ok("Bla");
    }
    
}