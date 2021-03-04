using Microsoft.AspNetCore.Mvc;

namespace LetsLearnTDD.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloWorldController : ControllerBase
    {
        public HelloWorldController() {} 

        [HttpGet]
        public OkResult Get()
        {
            return Ok();
        }
    }
}