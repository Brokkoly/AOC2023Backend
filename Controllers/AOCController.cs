using AOC2023Backend.Days;
using Microsoft.AspNetCore.Mvc;

namespace AOC2023Backend.Controllers
{
    [ApiController]
    [Route("aoc")]
    public class AOCController : ControllerBase
    {
        [Route("/DayOne")]
        [HttpPost]
        public IActionResult DayOne ([FromBody] string inputString)
        {
            return new OkObjectResult((new DayOne()).PartOneAndTwo(inputString));
        }
    }
}
