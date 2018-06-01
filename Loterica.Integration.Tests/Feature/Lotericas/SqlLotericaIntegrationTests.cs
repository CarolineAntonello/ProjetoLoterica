using FluentAssertions;
using Loterica.Aplication.Features.Lotericas;
using Loterica.Common.Tests.Base;
using Loterica.Domain.Exceptions;
using Loterica.Domain.Features.Lotericas;
using Loterica.Infra.Data.Features.Lotericas;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Loterica.Integration.Tests.Feature.Lotericas
{
    [TestFixture]
    public class SqlLotericaIntegrationTests
    {
        FaturamentoLotericaService _service;
        IFaturamentoLotericaRepository _repository;

        [SetUp]
        public void Initialize()
        {
            BaseSqlTest.SeedDatabase();
            _repository = new FaturamentoLotericaSQLRepository();
            _service = new FaturamentoLotericaService(_repository);
        }

        [Test]
        public void Integration_AdicionarFaturamentoLoterica_Corretamente()
        {
            FaturamentoLoterica faturamentoLoterica = Loterica.Common.Tests.Lotericas.ObjectMother.GetFaturamento();
            faturamentoLoterica.CalcularFaturamentoELucro(10, 2);
            _service.Adicionar(faturamentoLoterica);
            FaturamentoLoterica f = _service.Get(2);
            f.lucro.Should().Be(faturamentoLoterica.lucro);
        }

        [Test]
        public void Integration_EditarFaturamentoLoterica_Corretamente()
        {
            FaturamentoLoterica faturamentoLoterica = Loterica.Common.Tests.Lotericas.ObjectMother.GetFaturamentoComId();
            faturamentoLoterica.CalcularFaturamentoELucro(10, 2);
            _service.Editar(faturamentoLoterica);
            FaturamentoLoterica f = _service.Get(faturamentoLoterica.Id);
            f.lucro.Should().Be(faturamentoLoterica.lucro);
        }

        [Test]
        public void Integration_ExcluirFaturamentoLoterica_Incorretamente_Retornando_Excecao()
        {
            FaturamentoLoterica faturamentoLoterica = Loterica.Common.Tests.Lotericas.ObjectMother.GetFaturamentoComId();
            Action action = () =>_service.Excluir(faturamentoLoterica);
            action.Should().Throw<UnsupportedOperationException>();
        }

        [Test]
        public void Integration_PegarFaturamentoLotericaPorId_Corretamente()
        {
            FaturamentoLoterica f = _service.Get(1);
            f.Should().NotBeNull();
        }

        [Test]
        public void Integration_PegarConcursoIdDeFaturamentoLoterica_Corretamente()
        {
            FaturamentoLoterica f = _service.GetByConcursoId(2);
            f.Should().NotBeNull();
        }

        [Test]
        public void Integration_PegarTodosOsFaturamentoLoterica_Devedia_Buscar_Corretamente()
        {
            List<FaturamentoLoterica> f = _service.PegarTodos();
            f.Count.Should().BeGreaterThan(0);
        }
    }
}
