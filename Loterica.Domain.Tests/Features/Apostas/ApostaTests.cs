using Loterica.Domain.Features.Apostas;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace Loterica.Domain.Tests.Features.Apostas
{
    [TestFixture]
    public class ApostaTests
    {
        private Aposta _aposta;

        [Test]
        public void Domain_Aposta_Validar_Deveria_Estar_OK()
        {
            _aposta = Common.Tests.Apostas.ObjectMother.GetAposta();
            _aposta.Validar();
            _aposta.numerosApostados.Count().Should().Be(6);
        }

        [Test]
        public void Domain_Aposta_Validar_Deveria_Nao_Permitir_Mais_que_6_Numeros_Apostados()
        {
            _aposta = Common.Tests.Apostas.ObjectMother.GetApostaSeteNumerosApostados();
            Action action = () => _aposta.Validar();
            action.Should().Throw<InvalidBetException>();
        }

        [Test]
        public void Domain_Aposta_Validar_Nao_Permitir_Numeros_Apostados_Vazios()
        {
            _aposta = Common.Tests.Apostas.ObjectMother.GetApostaSemNumerosApostados();
            Action action = () => _aposta.Validar();
            action.Should().Throw<InvalidBetException>();
        }

        [Test]
        public void Domain_Aposta_Validar_Nao_Permitir_Numeros_Maiores_Que_60()
        {
            _aposta = Common.Tests.Apostas.ObjectMother.GetApostaNumerosMaioresQue60();
            Action action = () => _aposta.Validar();
            action.Should().Throw<InvalidNumberException>();
        }

        [Test]
        public void Domain_Aposta_Validar_Nao_Permitir_Numeros_Menores_Que_1()
        {
            _aposta = Common.Tests.Apostas.ObjectMother.GetApostaNumerosMaioresQue60();
            Action action = () => _aposta.Validar();
            action.Should().Throw<InvalidNumberException>();
        }

        [Test]
        public void Domain_Aposta_Validar_Gerar_Numeros_Aleatorios()
        {
            _aposta = new Aposta(new Concurso());
            _aposta.GerarNumerosAposta();
            _aposta.numerosApostados.Count().Should().Be(6);
            foreach (var item in _aposta.numerosApostados)
            {
                if (item > 60 || item < 1)
                {
                    Assert.Fail();
                }
            }
        }

        [Test]
        public void Domain_Aposta_CalcularResultado_Quatro_Acertos()
        {
            _aposta = Common.Tests.Apostas.ObjectMother.GetAposta();
            List<int> resultado = new List<int> { 2, 3, 6, 4, 15, 18 };
            int acertos =_aposta.CalcularResultadoBilhete(resultado);
            acertos.Should().Be(4);
        }

        [Test]
        public void Domain_Aposta_CalcularResultado_Cinco_Acertos()
        {
            _aposta = Common.Tests.Apostas.ObjectMother.GetAposta();
            List<int> resultado = new List<int> { 2, 3, 6, 4, 1, 18 };
            int acertos = _aposta.CalcularResultadoBilhete(resultado);
            acertos.Should().Be(5);
        }

        [Test]
        public void Domain_Aposta_CalcularResultado_Seis_Acertos()
        {
            _aposta = Common.Tests.Apostas.ObjectMother.GetAposta();
            List<int> resultado = new List<int> { 2, 3, 6, 4, 1, 5 };
            int acertos = _aposta.CalcularResultadoBilhete(resultado);
            acertos.Should().Be(6);
        }
    }
}
