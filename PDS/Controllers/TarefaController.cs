using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PDS.Models;
using PDS.Dtos;

namespace PDS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<TarefaController> _logger;

        public TarefaController(ILogger<TarefaController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetTarefaController")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}

    

