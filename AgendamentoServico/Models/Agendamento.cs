using System;
using System.Collections.Generic;

namespace AgendamentoServico.Models
{
    public partial class Agendamento
    {
        public DateTime Data { get; set; }
        public int IdordemServico { get; set; }
        public bool Cancelado { get; set; }

        public virtual OrdemServico IdordemServicoNavigation { get; set; }
    }
}
