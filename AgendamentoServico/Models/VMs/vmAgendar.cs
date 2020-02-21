using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendamentoServico.Models
{
    public class vmAgendar
    {
        public DateTime Data { get; set; }
        public vmCliente Cliente { get; set; }
        public vmTecnico Tecnico { get; set; }
        public vmTipoServico TipoServico { get; set; }
    }

    public class vmCliente
    {
        public string Nome { get; set; }
        public decimal CPF { get; set; }
        public string Email { get; set; }
        public decimal Telefone { get; set; }
    }

    public class vmTecnico
    {
        public string Nome { get; set; }
        public decimal Telefone { get; set; }
        public string Email { get; set; }
        public decimal CPF { get; set; }
    }

    public class vmTipoServico
    {
        public int IdItem { get; set; }
        public string Tipo { get; set; }
        public decimal Valor { get; set; }
    }
}
