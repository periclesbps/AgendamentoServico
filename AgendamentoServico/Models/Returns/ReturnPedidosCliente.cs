using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendamentoServico.Models
{
    public class ReturnPedidosCliente : ReturnBase
    {
        public List<vmPedidoCliente> Pedidos { get; set; }

        public ReturnPedidosCliente()
        {
            this.Pedidos = new List<vmPedidoCliente>();
        }
    }
}