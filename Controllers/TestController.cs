using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Controllers
{

    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult TestGet()
        {
            return Ok("Test endpoint is working");
        }

        [HttpPost]
        public IActionResult TestPost([FromBody] object payload)
        {
            return Ok(payload);
        }
    }



}
