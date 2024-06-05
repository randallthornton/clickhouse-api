using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace clickhouse_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ClickhouseContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ClickhouseContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IAsyncEnumerable<MyFirstTable>> GetClicks()
        {
            return _context.MyFirstTableSet.AsAsyncEnumerable();
        }

        [HttpPost]
        [Route("random")]
        public async Task<IActionResult> GenerateRandomData()
        {
            foreach (var i in Enumerable.Range(0, 100))
            {
                var entry = new MyFirstTable
                {
                    UserId = (uint)i,
                    Message = $"Random Message {i}",
                    Metric = i,
                    Timestamp = DateTime.UtcNow,
                };

                await _context.Database.ExecuteSqlInterpolatedAsync($"""
                    INSERT INTO my_first_table (user_id, message, timestamp, metric)
                    VALUES ({entry.UserId}, {entry.Message}, {entry.Timestamp}, {entry.Metric})
                    """);
            }

            return Ok();
        }
    }
}
