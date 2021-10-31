using Microsoft.AspNetCore.Mvc;

namespace DagAir.Swagger.Controllers
{
    [Route("tmp")]
    public class TmpController : Controller
    {
        [HttpGet("dostuff")]
        public IActionResult DoStuff()
        {
            return Ok("eloelo");
        }
    }
}