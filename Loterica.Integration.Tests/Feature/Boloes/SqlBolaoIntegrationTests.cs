using FluentAssertions;
using Loterica.Aplication.Features.Boloes;
using Loterica.Common.Tests.Base;
using Loterica.Common.Tests.Boloes;
using Loterica.Domain.Exceptions;
using Loterica.Domain.Features.Apostas;
using Loterica.Domain.Features.Boloes;
using Loterica.Infra.Data.Features.Boloes;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Loterica.Integration.Tests.Feature.Boloes
{
    [TestFixture]
    public class SqlBolaoIntegrationTests
    {
        Bolao _bolao;
        BolaoService _service;
        IBolaoRepository _repository;

        [SetUp]
        public void Initialize()
        {
            BaseSqlTest.SeedDatabase();
            _repository = new BolaoSQLRepository();
            _service = new BolaoService(_repository);
        }

        [Test]
        public void Integration_AdicionarBoloes_Corretamente()
        {
            _bolao = ObjectMother.GetBolao();
            _service.Adicionar(_bolao);
            var recebeBolao = _service.Get(2);
            recebeBolao.Should().NotBeNull();
            recebeBolao.valorTotalApostasBolao.Should().Be(_bolao.valorTotalApostasBolao);
        }

        [Test]
        public void Integration_AdicionarBoloes_Incorretamente_Sem_Apostas()
        {
            _bolao = ObjectMother.GetBolaoSemApostas();
            Action action = () => _service.Adicionar(_bolao);
            action.Should().Throw<InvalidListApostaException>();
        }

        [Test]
        public void Integration_EditarBoloes_Incorretamente_Caindo_Na_Excecao()
        {
            _bolao = ObjectMother.GetBolaoComId();
            Action action = () => _service.Editar(_bolao);
            action.Should().Throw<UnsupportedOperationException>();
        }

        [Test]
        public void Integration_DeletarBoloes_Incorretamente_Caindo_Na_Excecao()
        {
            _bolao = ObjectMother.GetBolaoComId();
            Action action = () => _service.Excluir(_bolao);
            action.Should().Throw<UnsupportedOperationException>();
        }

        [Test]
        public void Integration_PegarTodosOsBoloes_Corretamente()
        {
            List<Bolao> boloes = _service.PegarTodos();
            boloes.Count.Should().BeGreaterThan(0);
        }

        [Test]
        public void Integration_PegarBoloesPorId_Corretamente()
        {
            Bolao bolao = _service.Get(1);
            bolao.Should().NotBeNull();
            bolao.Id.Should().BeGreaterThan(0);
        }
    }
}
