using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        static int count = 0;
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("GetConcurrent")]
        [EnableRateLimiting("concurrency")]
        public IActionResult GetConcurrent(string guid)
        {
            Console.WriteLine($"Request processed {guid} - {DateTime.Now.ToString("hh:mm:ss.fff")}");
            return Ok();
        }

        [HttpGet]
        [EnableRateLimiting("window")]
        [Route("GetWindow")]
        public IActionResult GetWindow(int requestNumber, string guid)
        {
            Console.WriteLine($"{requestNumber}. Request processed {guid} - {DateTime.Now.ToString("hh:mm:ss.fff")}");
            return Ok();
        }

        [HttpGet]
        [EnableRateLimiting("tokenbucket")]
        [Route("GetTokenBucket")]
        public IActionResult GetTokenBucket(int requestNumber, string guid)
        {
            Console.WriteLine($"{requestNumber}. Request processed {guid} - {DateTime.Now.ToString("hh:mm:ss.fff")}");
            return Ok();
        }

        [HttpGet]
        [EnableRateLimiting("slidingwindow")]
        [Route("GetSlidingWindow")]
        public IActionResult GetSlidingWindow(int requestNumber, string guid)
        {
            Console.WriteLine($"{requestNumber}. Request processed {guid} - {DateTime.Now.ToString("hh:mm:ss.fff")}");
            return Ok();
        }
    }
}