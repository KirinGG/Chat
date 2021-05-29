using Microsoft.AspNetCore.Mvc;

namespace ChatRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Login()
        {
            return new JsonResult(true);
        }
    }
}
