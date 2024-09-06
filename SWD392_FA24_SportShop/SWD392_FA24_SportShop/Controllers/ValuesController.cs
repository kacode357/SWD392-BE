using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SWD392_FA24_SportShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() 
        {
            return Ok("good");
        }
    }
}
