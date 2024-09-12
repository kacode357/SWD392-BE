using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SWD392_FA24_SportShop.Controllers
{
    [Route("api/TestController/")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("test")]
        public IActionResult Get() 
        {
            return Ok("good");
        }
    }
}
