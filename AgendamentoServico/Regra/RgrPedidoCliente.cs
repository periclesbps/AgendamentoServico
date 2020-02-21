using AgendamentoServico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendamentoServico.Regra
{
    public class RgrPedidoCliente
    {
        private readonly AgendamentoDBContext db;

        public RgrPedidoCliente(AgendamentoDBContext db)
        {
            this.db = db;
        }

        public ReturnPedidosCliente ObterPedidos(int idCliente)
        {
            var retorno = new ReturnPedidosCliente();

            try
            {
                if (idCliente <= 0)
                {
                    retorno.Message = "Favor informar o código do cliente";
                }

                var dados = this.ObterDadosBanco(idCliente);

                if (dados == null || !dados.Any())
                {
                    retorno.Message = "Nenhum agendamento encontrado para o cliente";
                    return retorno;
                }

                retorno.Pedidos.AddRange(dados.ToList());
                retorno.IsSuccess = true;
            }
            catch (Exception ex)
            {
                retorno.Message = ex.Message;
            }

            return retorno;
        }

        private IQueryable<vmPedidoCliente> ObterDadosBanco(int idCliente)
        {
            return from a in this.db.Agendamento
                   join o in this.db.OrdemServico on a.IdordemServico equals o.Id
                   join c in this.db.Cliente on o.Idcliente equals c.Id
                   join t in this.db.Tecnico on o.Idtecnico equals t.Id
                   join i in this.db.Itens on o.Iditem equals i.Id
                   where c.Id == idCliente
                   select new vmPedidoCliente
                   {
                       DataAgendamento = a.Data,
                       NumeroOS = o.Id,
                       Tipo = o.Tipo,
                       Valor = o.Valor,
                       Item = i.Descricao,
                       NomeTecnico = t.Nome,
                       NomeCliente = c.Nome
                   };
        }
    }
}
