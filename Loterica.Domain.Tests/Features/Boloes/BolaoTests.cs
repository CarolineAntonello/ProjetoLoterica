using FluentAssertions;
using Loterica.Common.Tests.Boloes;
using Loterica.Domain.Features.Apostas;
using Loterica.Domain.Features.Boloes;
using NUnit.Framework;
using System;
using System.Linq;

namespace Loterica.Domain.Tests.Features.Boloes
{
    public class BolaoTests
    {
        private Bolao _bolao;

        [Test]
        public void Domain_Bolao_Validar_Deveria_Ser_Okay()
        {
            _bolao = ObjectMother.GetBolao();
            _bolao.Validar();
            _bolao.apostas.Count().Should().Be(2);
        }

        [Test]
        public void Domain_Bolao_Validar_Não_Deveria_Permitir_Sem_Concurso()
        {
            _bolao = ObjectMother.GetBolaoSemConcurso();
            Action action = () => _bolao.Validar();
            action.Should().Throw<InvalidBolaoConcursoException>();
        }

        [Test]
        public void Domain_Bolao_Validar_Não_Deveria_Permitir_Sem_Apostas()
        {
            _bolao = ObjectMother.GetBolaoSemApostas();
            Action action = () => _bolao.Validar();
            action.Should().Throw<InvalidListApostaException>();
        }

        [Test]
        public void Domain_Bolao_CalculaValorDoBolao()
        {
            _bolao = ObjectMother.GetBolao();
            _bolao.CalculaValorDoBolao();
            _bolao.valorTotalApostasBolao.Should().Be(6.30);
        }

        [Test]
        public void Domain_Bolao_GerarApostas_Deve_Gerar_Numero_Apostas_Selecionado()
        {
            _bolao = ObjectMother.GetBolaoSemApostas();
            _bolao.GerarApostas(15);
            _bolao.apostas.Count.Should().Be(15);
        }
    }
}
