using Microsoft.AspNetCore.Mvc;

namespace DataTube.Controllers;

[ApiController]
[Route("other")]
public class ReqResControllers : ControllerBase
{
    [HttpGet]
    public IActionResult GetData()
    {
        return Ok("You've made it here :)");
    }
    
    
}