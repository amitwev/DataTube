using System.Net;
using System.Xml.Serialization;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
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
    
    [Route("add-data/json")]
    [Consumes("application/json")]
    [HttpPost]
    public async Task<IActionResult> AddJsonData(TextDTO textDto)
    {
        var completedText = _addDataService.TextDtoToTextComplete(textDto, Request.Headers);
        bool result = await _loadBalancer.BalanceRequests(completedText);
        return result ? Ok(completedText) : Ok(HttpStatusCode.InternalServerError);
    }
    
    [Route("add-data/xml")]
    [Consumes("application/xml")]
    [Produces("application/xml")]
    [HttpPost]
    public async Task<IActionResult> AddXmlData(TextDTO textDto)
    {
        var completedText = _addDataService.TextDtoToTextComplete(textDto, Request.Headers);
        bool result = await _loadBalancer.BalanceRequests(completedText);
        if (result)
        {
            var serializer = new XmlSerializer(typeof(CompletedText)); 
            var stringWriter = new StringWriter();
            serializer.Serialize(stringWriter, completedText);
            var xmlContent = stringWriter.ToString();
            var final = new OkObjectResult(xmlContent)
            {
                ContentTypes = new MediaTypeCollection()
                {
                    new Microsoft.Net.Http.Headers.MediaTypeHeaderValue("application/xml")
                }
            };
            return final;
        }
        else
        {
            return Ok(HttpStatusCode.InternalServerError);
        }
    }
    
    [Route("add-file")]
    [HttpPost]
    public IActionResult AddFile()
    {
        return Ok("Bla");
    }
    
}