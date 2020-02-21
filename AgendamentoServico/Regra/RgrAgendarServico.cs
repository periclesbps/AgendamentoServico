using AgendamentoServico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace AgendamentoServico.Regra
{
    public class RgrAgendarServico
    {
        private readonly AgendamentoDBContext db;

        public RgrAgendarServico(AgendamentoDBContext db)
        {
            this.db = db;
        }

        public ReturnBase Salvar(vmAgendar agendar)
        {
            var retorno = new ReturnBase();
            var cliente = new Cliente();
            var tecnico = new Tecnico();

            try
            {
                retorno = this.ValidarDadosInputados(agendar, ref cliente, ref tecnico);

                if (!retorno.IsSuccess)
                {
                    return retorno;
                }

                var os = new OrdemServico
                {
                    Idcliente = cliente.Id,
                    Iditem = agendar.TipoServico.IdItem,
                    Tipo = agendar.TipoServico.Tipo,
                    Valor = agendar.TipoServico.Valor,
                    Idtecnico = tecnico.Id
                };

                using (var scope = new TransactionScope())
                {
                    db.OrdemServico.Add(os);
                    db.SaveChanges();
                    db.Agendamento.Add(new Agendamento { Data = agendar.Data, IdordemServico = os.Id });
                    db.SaveChanges();
                    
                    scope.Complete();
                }

                retorno.Message = string.Format("Pedido número {0} cadastrado com sucesso para o dia {1}", os.Id, agendar.Data.ToString("dd/MM/yy hh:mm"));
                retorno.IsSuccess = true;
            }
            catch (Exception ex)
            {
                retorno.Message = ex.Message;
            }

            return retorno;
        }

        private ReturnBase ValidarDadosInputados(vmAgendar agendar, ref Cliente cliente, ref Tecnico tecnico)
        {
            var retorno = new ReturnBase();

            if (agendar.Data == DateTime.MinValue)
            {
                retorno.Message = "Data do agendamento é obrigatória.";
                return retorno;
            }

            retorno = this.ValidarDadosCliente(agendar, ref cliente);

            if (!retorno.IsSuccess)
            {
                return retorno;
            }

            retorno.Reset();

            retorno = this.ValidarDadosTecnico(agendar, ref tecnico);

            if (!retorno.IsSuccess)
            {
                return retorno;
            }

            retorno.Reset();

            retorno = this.ValidarDadosTipoServico(agendar);

            if (!retorno.IsSuccess)
            {
                return retorno;
            }

            return retorno;
        }

        private ReturnBase ValidarDadosTipoServico(vmAgendar agendar)
        {
            var retorno = new ReturnBase();

            if (agendar.TipoServico == null || string.IsNullOrEmpty(agendar.TipoServico.Tipo) ||
                agendar.TipoServico.IdItem <= 0 || agendar.TipoServico.Valor <= 0)
            {
                retorno.Message = "É necessário o preenchimento dos itens do serviço";
                return retorno;
            }

            retorno.IsSuccess = true;
            return retorno;
        }

        private ReturnBase ValidarDadosCliente(vmAgendar agendar, ref Cliente cliente)
        {
            if (agendar.Cliente.CPF > 0M)
            {
                return this.BuscarClienteBancoECasoNaoExistaCadastrar(agendar, out cliente);
            }
            else
            {
                return new ReturnBase { Message = "CPF do cliente não informado." };
            }
        }

        private ReturnBase BuscarClienteBancoECasoNaoExistaCadastrar(vmAgendar agendar, out Cliente cliente)
        {
            var retorno = new ReturnBase();

            cliente = this.BuscarDadosCliente(agendar);

            if (cliente == null || cliente.Id < 1)
            {
                if (string.IsNullOrEmpty(agendar.Cliente.Nome) || string.IsNullOrEmpty(agendar.Cliente.Email) || agendar.Cliente.Telefone <= 0)
                {
                    retorno.Message = "CPF do cliente não informado.";
                    return retorno;
                }

                cliente = new Cliente
                {
                    Email = agendar.Cliente.Email,
                    Cpf = agendar.Cliente.CPF,
                    Nome = agendar.Cliente.Nome,
                    Telefone = agendar.Cliente.Telefone
                };

                this.CadastrarCliente(cliente);
            }

            retorno.IsSuccess = true;
            return retorno;
        }

        private void CadastrarCliente(Cliente cliente)
        {
            db.Cliente.Add(cliente);
            db.SaveChanges();
        }

        private Cliente BuscarDadosCliente(vmAgendar agendar)
        {
            return db.Cliente.Where(x => x.Cpf == agendar.Cliente.CPF).ToList().FirstOrDefault();
        }

        private ReturnBase ValidarDadosTecnico(vmAgendar agendar, ref Tecnico tecnico)
        {
            if (agendar.Tecnico.CPF > 0M)
            {
                return this.BuscarTecnicoBancoECasoNaoExistaCadastrar(agendar, out tecnico);
            }
            else
            {
                return new ReturnBase { Message = "CPF do tecnico não informado." };
            }
        }

        private ReturnBase BuscarTecnicoBancoECasoNaoExistaCadastrar(vmAgendar agendar, out Tecnico tecnico)
        {
            var retorno = new ReturnBase();

            tecnico = this.BuscarDadosTecnico(agendar);

            if (tecnico == null || tecnico.Id < 1)
            {
                if (string.IsNullOrEmpty(agendar.Tecnico.Nome) || string.IsNullOrEmpty(agendar.Tecnico.Email) || agendar.Tecnico.Telefone <= 0)
                {
                    retorno.Message = "CPF do cliente não informado.";
                    return retorno;
                }

                tecnico = new Tecnico
                {
                    Email = agendar.Tecnico.Email,
                    Cpf = agendar.Tecnico.CPF,
                    Nome = agendar.Tecnico.Nome,
                    Telefone = agendar.Tecnico.Telefone
                };

                this.CadastrarTecnico(tecnico);
            }

            retorno.IsSuccess = true;
            return retorno;
        }

        private void CadastrarTecnico(Tecnico tecnico)
        {
            db.Tecnico.Add(tecnico);
            db.SaveChanges();
        }

        private Tecnico BuscarDadosTecnico(vmAgendar agendar)
        {
            return db.Tecnico.Where(x => x.Cpf == agendar.Tecnico.CPF).ToList().FirstOrDefault();
        }
    }
}