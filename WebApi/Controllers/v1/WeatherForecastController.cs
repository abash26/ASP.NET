using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1;

[ApiExplorerSettings(GroupName = "v1")]
[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Tags("Web Api - Get")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpPut]
    [Tags("Web Api - Change")]
    public IActionResult Update()
    {
        return NoContent();
    }

    [HttpPost]
    [Tags("Web Api - Change")]
    public IActionResult Create(WeatherForecast weatherForecast)
    {
        return Created();
    }
}
