using NUnit.Framework;
using Moq;
using AgendamentoServico.Regra;
using AgendamentoServico.Models;
using System;

namespace Test
{
    public class RgrVendasPorPeriodoTest
    {
        private Mock<RgrVendasPorPeriodo> MockRegra;
        private RgrVendasPorPeriodo Regra;
        private DateTime DataInicio;
        private DateTime DataFim;

        [SetUp]
        public void Setup()
        {
            this.MockRegra = new Mock<AgendamentoDBContext>();
            this.Regra = new RgrVendasPorPeriodo(this.MockRegra.Object);

            this.DataInicio = new DateTime(2020, 02, 15);
            this.DataFim = new DateTime(2020, 02, 18);
        }

        [Test]
        public void SeDataInicialNaoInformada_RetornarErro()
        {
            var retorno = this.Regra.ObterVendasPorPeriodo(this.DataInicio, this.DataFim);
            Assert.IsFalse(retorno.IsSuccess);
        }

        [Test]
        public void SeDataFinalNaoInformada_DefinirDataETrazerDados()
        {
            this.DataFim = new DateTime();
            var retorno = this.Regra.ObterVendasPorPeriodo(this.DataInicio, this.DataFim);

            this.MockRegra.Setup(x=>x.Agendamento.)
            Assert.IsFalse(retorno.IsSuccess);
        }
    }
}