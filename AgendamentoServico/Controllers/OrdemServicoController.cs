using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendamentoServico.Models;
using AgendamentoServico.Regra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgendamentoServico.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdemServicoController : ControllerBase
    {
        private readonly RgrVendasPorPeriodo rgrVendasPorPeriodo;
        private readonly RgrPedidoCliente rgrPedidoCliente;

        public OrdemServicoController(RgrVendasPorPeriodo rgrVendasPorPeriodo, RgrPedidoCliente rgrPedidoCliente)
        {
            this.rgrVendasPorPeriodo = rgrVendasPorPeriodo;
            this.rgrPedidoCliente = rgrPedidoCliente;
        }

        [Route("VendasPeriodo/{datainicio}/{datafim}")]
        public ReturnVendasPorPeriodo VendasPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            return this.rgrVendasPorPeriodo.ObterVendasPorPeriodo(dataInicio, dataFim);
        }

        [Route("PedidosCliente/{idCliente}")]
        public ReturnPedidosCliente PedidosCliente(int idCliente)
        {
            return this.rgrPedidoCliente.ObterPedidos(idCliente);
        }
    }
}