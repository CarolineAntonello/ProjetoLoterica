using FluentAssertions;
using Loterica.Domain.Features.Lotericas;
using NUnit.Framework;
using System;

namespace Loterica.Domain.Tests.Features.Lotericas
{
    [TestFixture]
    public class FaturamentoLotericaTests
    {
        FaturamentoLoterica _faturamento;

        [Test]
        public void Domain_FaturamentoLoterica_Calcular_Faturamento_Lucro()
        {
            _faturamento = Loterica.Common.Tests.Lotericas.ObjectMother.GetFaturamento();
            _faturamento.CalcularFaturamentoELucro(10,2);
            _faturamento.lucro.Should().Be(3.30);
            _faturamento.faturamento.Should().Be(30.30);
        }

        [Test]
        public void Domain_FaturamentoLoterica_Validar_Lucro_Negativo()
        {
            _faturamento = Loterica.Common.Tests.Lotericas.ObjectMother.GetFaturamento();
            _faturamento.lucro = -2;
            Action action = () => _faturamento.Validar();
            action.Should().Throw<InvalidFaturamentoException>();
        }

        [Test]
        public void Domain_FaturamentoLoterica_Validar_Faturamento_Negativo()
        {
            _faturamento = Loterica.Common.Tests.Lotericas.ObjectMother.GetFaturamento();
            _faturamento.faturamento = -2;
            Action action = () => _faturamento.Validar();
            action.Should().Throw<InvalidFaturamentoException>();
        }

        [Test]
        public void Domain_FaturamentoLoterica_Validar_Faturamento_Maior_Que_Lucro()
        {
            _faturamento = Loterica.Common.Tests.Lotericas.ObjectMother.GetFaturamento();
            _faturamento.faturamento = 10;
            _faturamento.lucro = 20;
            Action action = () => _faturamento.Validar();
            action.Should().Throw<InvalidFaturamentoException>();
        }

        [Test]
        public void Domain_FaturamentoLoterica_Validar_Should_Be_Ok()
        {
            _faturamento = Loterica.Common.Tests.Lotericas.ObjectMother.GetFaturamento();
            _faturamento.faturamento = 20;
            _faturamento.lucro = 10;
            Action action = () => _faturamento.Validar();
            action.Should().NotThrow<Exception>();
        }

    }
}
