using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    // GET
    [HttpGet]
    public String Index()
    {
        return "Hello World";
    }
}