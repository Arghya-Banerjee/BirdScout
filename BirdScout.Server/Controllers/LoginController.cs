using Microsoft.AspNetCore.Mvc;

namespace BirdScout.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        [HttpGet("Validate")]
        public IActionResult Validate(string username, string password)
        {

            return Ok(new {username, password});
        }
    }
}
