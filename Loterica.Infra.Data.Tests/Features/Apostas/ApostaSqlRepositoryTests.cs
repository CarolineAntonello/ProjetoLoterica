using FluentAssertions;
using Loterica.Common.Tests.Apostas;
using Loterica.Common.Tests.Base;
using Loterica.Domain;
using Loterica.Domain.Exceptions;
using Loterica.Domain.Features.Apostas;
using Loterica.Domain.Features.Boloes;
using Loterica.Domain.Features.Concursos;
using Loterica.Infra.Data.Features.Apostas;
using Loterica.Infra.Data.Features.Boloes;
using Loterica.Infra.Data.Features.Concursos;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Loterica.Infra.Data.Tests.Features.Apostas
{
    [TestFixture]
    public class ApostaSqlRepositoryTests
    {
        IApostaRepository _repository;
        IBolaoRepository _repositoryBolao;
        Aposta _aposta;

        [SetUp]
        public void Initilaze()
        {
            BaseSqlTest.SeedDatabase();
            _repository = new ApostaSQLRepository();
        }

        [Test]
        public void Infra_Aposta_SQLRepository_Deveria_Adicionar_Aposta()
        {
            _aposta = ObjectMother.GetAposta();
            _aposta = _repository.Adicionar(_aposta);
            _aposta.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void Infra_Aposta_SQLRepository_Deveria_Editar_Aposta()
        {
            _aposta = ObjectMother.GetApostaComId();
             Action action = () => _repository.Editar(_aposta);
            action.Should().Throw<UnsupportedOperationException>();
        }

        [Test]
        public void Infra_Aposta_SQLRepository_Deveria_Excluir_Aposta()
        {
            _aposta = ObjectMother.GetApostaComId();
            Action action = () => _repository.Excluir(_aposta.Id);
            action.Should().Throw<UnsupportedOperationException>();
        }

        [Test]
        public void Infra_Aposta_SQLRepository_Deveria_BuscarPorId()
        {
            _aposta = _repository.GetById(1);
            _aposta.Should().NotBeNull();
        }

        [Test]
        public void Infra_Aposta_SQLRepository_Deveria_BuscarTodos()
        {
           List<Aposta> apostas = ObjectMother.GetApostas();
            foreach (var item in apostas)
            {
                _repository.Adicionar(item);
            }
            var apostasBanco = _repository.GetAll();
            apostasBanco.Count().Should().Be(3);
        }

        [Test]
        public void Infra_Aposta_SQLRepository_Deveria_BuscarPorConcursoId()
        {
            List<Aposta> apostas = ObjectMother.GetApostas();
            foreach (var item in apostas)
            {
                _repository.Adicionar(item);
            }
            var apostasBanco = _repository.GetByConcursoId(1);
            apostasBanco.Count().Should().Be(2);
        }

        [Test]
        public void Infra_Aposta_SQLRepository_Ao_Adicionar_Aposta_Deve_Atualizar_Valor_Total_Concurso()
        {
            IConcursoRepository _concursoRepository = new ConcursoSQLRepository();
            Concurso c = new Concurso();
            _aposta = ObjectMother.GetAposta();
            _aposta = _repository.Adicionar(_aposta);
            c = _concursoRepository.GetById(_aposta.concurso.Id);
            c.valorTotalApostas.Should().Be(8004);            
        }

        [Test]
        public void Infra_Aposta_SQLRepository_Deveria_BuscarConcursoIdComBolao()
        {
            _repositoryBolao = new BolaoSQLRepository();
            Bolao bolao = Loterica.Common.Tests.Boloes.ObjectMother.GetBolaoCom10Apostas();
            _repositoryBolao.Adicionar(bolao);
            var apostasBanco = _repository.GetByConcursoIdComBolao(1);
            apostasBanco.Count().Should().Be(10);
        }
    }
}
