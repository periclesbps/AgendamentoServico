using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendamentoServico.Models
{
    public class ReturnVendasPorPeriodo : ReturnBase
    {
        public List<vmVendas> Vendas { get; set; }

        public ReturnVendasPorPeriodo()
        {
            this.Vendas = new List<vmVendas>();
        }
    }
}