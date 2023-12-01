using AOC2023Backend.Days;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace AOC2023Backend.Controllers
{
    [ApiController]
    [Route("aoc")]
    public class AOCController : ControllerBase
    {
        private IInputService _InputService;
        public AOCController(IInputService inputService)
        {
            _InputService = inputService;
        }

        [Route("/Day/{dayNum}/PartOne")]
        [HttpPost]
        public async Task<IActionResult> ResultForPartOne([FromRoute] string dayNum, [FromHeader] string Cookie)
        {
            var data = await _InputService.GetInputString(dayNum, Cookie);

            var day = Day.DayFactory(int.Parse(dayNum));
            return new OkObjectResult(day.PartOne(data));
        }

        [Route("/Day/{dayNum}/PartOne/Test")]
        [HttpPost]
        public IActionResult ResultForPartOneTest([FromRoute] string dayNum, [FromForm] string data)
        {
            var day = Day.DayFactory(int.Parse(dayNum));
            return new OkObjectResult(day.PartOne(data));
        }

        [Route("/Day/{dayNum}/PartTwo")]
        [HttpPost]
        public async Task<IActionResult> ResultForPartTwo([FromRoute] string dayNum, [FromHeader] string Cookie)
        {
            var data = await _InputService.GetInputString(dayNum, Cookie);

            var day = Day.DayFactory(int.Parse(dayNum));
            return new OkObjectResult(day.PartTwo(data));
        }

        [Route("/Day/{dayNum}/PartTwo/Test")]
        [HttpPost]
        public IActionResult ResultForPartTwoTest([FromRoute] string dayNum, [FromForm] string data)
        {
            var day = Day.DayFactory(int.Parse(dayNum));
            return new OkObjectResult(day.PartTwo(data));
        }


    }
}
