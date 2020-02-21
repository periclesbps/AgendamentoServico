using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendamentoServico.Models;
using AgendamentoServico.Regra;
using Microsoft.AspNetCore.Mvc;

namespace AgendamentoServico.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AgendamentosController : Controller
    {
        private readonly RgrAgendarServico rgr;

        public AgendamentosController(RgrAgendarServico rgr)
        {
            this.rgr = rgr;
        }

        [HttpPost]
        public ReturnBase Post(vmAgendar agendar)
        {
            return rgr.Salvar(agendar);
        }
    }
}