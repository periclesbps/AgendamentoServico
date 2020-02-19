using System;
using System.Collections.Generic;

namespace AgendamentoServico.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            OrdemServico = new HashSet<OrdemServico>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Telefone { get; set; }
        public decimal? Telefone2 { get; set; }
        public string Email { get; set; }
        public decimal Cpf { get; set; }

        public virtual ICollection<OrdemServico> OrdemServico { get; set; }
    }
}
