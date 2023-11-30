using AOC2023Backend.Days;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace AOC2023Backend.Controllers
{
    [ApiController]
    [Route("aoc")]
    public class AOCController : ControllerBase
    {
        [Route("/Day/{dayNum}")]
        [HttpPost]
        public async Task<IActionResult> ResultForDay([FromRoute] string dayNum, [FromForm] string data)
        {
            
            var day = Day.DayFactory(int.Parse(dayNum));
            return new OkObjectResult(day.PartOneAndTwo(data));
        }
    }
}
