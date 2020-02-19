using System;
using System.Collections.Generic;

namespace AgendamentoServico.Models
{
    public partial class Itens
    {
        public Itens()
        {
            OrdemServico = new HashSet<OrdemServico>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }

        public virtual ICollection<OrdemServico> OrdemServico { get; set; }
    }
}
