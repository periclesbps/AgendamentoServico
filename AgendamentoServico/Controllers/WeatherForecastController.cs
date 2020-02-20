using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendamentoServico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AgendamentoServico.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Cliente> Get()
        {
            var cliente = new Cliente { Cpf = 12345678901, Email = "naldin.naldo.com.br", Nome = "Naldim", Telefone = 31999999999 };

            using (var db = new AgendamentoDBContext())
            {
                db.Cliente.Add(cliente);
                db.SaveChanges();
            }

            var rng = new Random();
            return Enumerable.Range(1,1).Select(index => cliente).ToArray();
        }
    }
}
