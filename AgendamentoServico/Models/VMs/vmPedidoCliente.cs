using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendamentoServico.Models
{
    public class vmPedidoCliente
    {
        public DateTime DataAgendamento { get; set; }
        public string Item { get; set; }
        public string Tipo { get; set; }
        public decimal Valor { get; set; }
        public int NumeroOS { get; set; }
        public string NomeTecnico { get; set; }
        public string NomeCliente { get; set; }
    }
}
