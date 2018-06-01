using FluentAssertions;
using Loterica.Common.Tests.Base;
using Loterica.Common.Tests.Boloes;
using Loterica.Domain.Exceptions;
using Loterica.Domain.Features.Boloes;
using Loterica.Infra.Data.Features.Boloes;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Loterica.Infra.Data.Tests.Features.Boloes
{
    [TestFixture]
    public class BolaoSQLRepositorytests
    {
        IBolaoRepository _repository;
        Bolao _bolao;
        [SetUp]
        public void Initialize()
        {
            BaseSqlTest.SeedDatabase();
            _repository = new BolaoSQLRepository();
        }

        [Test]
        public void Infra_Bolao_SQLRepository_Deveria_Adicionar_Bolao()
        {
            _bolao = ObjectMother.GetBolaoCom10Apostas();
            _bolao = _repository.Adicionar(_bolao);
            _bolao.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void Infra_Bolao_SQLRepository_Nao_Deveria_Editar_Bolao()
        {
            _bolao = ObjectMother.GetBolaoCom10Apostas();
            Action action = () => _repository.Editar(_bolao);
            action.Should().Throw<UnsupportedOperationException>();
        }

        [Test]
        public void Infra_Bolao_SQLRepository_Nao_Deveria_Excluir_Bolao()
        {
            _bolao = ObjectMother.GetBolaoComId();
            Action action = () => _repository.Excluir(_bolao.Id);
            action.Should().Throw<UnsupportedOperationException>();
        }

        [Test]
        public void Infra_Bolao_SQLRepository_Deveria_Buscar_Bolao_Por_ID()
        {
            _bolao = _repository.GetById(1);
            _bolao.Should().NotBeNull();
        }

        [Test]
        public void Infra_Bolao_SQLRepository_Deveria_Buscar_Todos_Boloes()
        {
            _bolao = ObjectMother.GetBolaoCom10Apostas();
            _bolao = _repository.Adicionar(_bolao);
            _bolao = _repository.Adicionar(_bolao);
            _bolao = _repository.Adicionar(_bolao);
            _bolao = _repository.Adicionar(_bolao);
            List<Bolao> boloes =_repository.GetAll();
            boloes.Count.Should().Be(5);
        }
    }
}
