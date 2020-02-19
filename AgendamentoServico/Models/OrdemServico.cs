using System;
using System.Collections.Generic;

namespace AgendamentoServico.Models
{
    public partial class OrdemServico
    {
        public OrdemServico()
        {
            Agendamento = new HashSet<Agendamento>();
        }

        public int Id { get; set; }
        public int Idcliente { get; set; }
        public int Idtecnico { get; set; }
        public int Iditem { get; set; }
        public string Tipo { get; set; }
        public decimal Valor { get; set; }

        public virtual Cliente IdclienteNavigation { get; set; }
        public virtual Itens IditemNavigation { get; set; }
        public virtual Tecnico IdtecnicoNavigation { get; set; }
        public virtual ICollection<Agendamento> Agendamento { get; set; }
    }
}
