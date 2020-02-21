using AgendamentoServico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendamentoServico.Regra
{
    public class RgrVendasPorPeriodo
    {
        private readonly AgendamentoDBContext db;

        public RgrVendasPorPeriodo(AgendamentoDBContext db)
        {
            this.db = db;
        }

        public ReturnVendasPorPeriodo ObterVendasPorPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            var retorno = new ReturnVendasPorPeriodo();

            try
            {
                retorno = this.ValidarDados(dataInicio, ref dataFim);

                if (!retorno.IsSuccess)
                {
                    return retorno;
                }

                retorno.Reset();

                var dados = this.ObterListaBanco(dataInicio, dataFim);

                if (dados == null || !dados.Any())
                {
                    retorno.Message = "Nenhuma venda encontrada com o filtro informado.";
                    return retorno;
                }

                retorno.Vendas.AddRange(dados.ToList());
                retorno.IsSuccess = true;
            }
            catch (Exception ex)
            {
                retorno.Message = ex.Message;
            }

            return retorno;
        }

        private ReturnVendasPorPeriodo ValidarDados(DateTime dataInicio, ref DateTime dataFim)
        {
            var retorno = new ReturnVendasPorPeriodo();

            if (dataInicio == null || dataInicio == DateTime.MinValue)
            {
                retorno.Message = "Data inicial deve ser informada";
                return retorno;
            }

            dataFim = this.DefinirValoresDataFinal(dataFim);

            if (dataInicio > dataFim)
            {
                retorno.Message = "Data inicial não pode ser maior que a data final.";
                return retorno;
            }

            retorno.IsSuccess = true;
            return retorno;
        }

        private DateTime DefinirValoresDataFinal(DateTime dataFim)
        {
            if (dataFim == null || dataFim == DateTime.MinValue)
            {
                dataFim = DateTime.MaxValue;
            }
            else
            {
                dataFim = dataFim.AddDays(1).AddTicks(-1);
            }

            return dataFim;
        }

        private IQueryable<vmVendas> ObterListaBanco(DateTime dataInicio, DateTime dataFim)
        {
            return from a in this.db.Agendamento
                   join o in this.db.OrdemServico on a.IdordemServico equals o.Id
                   join c in this.db.Cliente on o.Idcliente equals c.Id
                   join t in this.db.Tecnico on o.Idtecnico equals t.Id
                   join i in this.db.Itens on o.Iditem equals i.Id
                   where a.Data >= dataInicio && a.Data <= dataFim
                   select new vmVendas
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