using Microsoft.AspNetCore.Mvc;
using LetsLearnTDD.Models;

namespace LetsLearnTDD.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloWorldController : ControllerBase
    {
        public HelloWorldController() {} 

        [HttpGet]
        public OkObjectResult Get()
        {
            var helloWorld = new HelloWorld{Name = "Unicorn"};
            return new OkObjectResult(helloWorld);
        }
    }
}