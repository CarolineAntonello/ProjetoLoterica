using FluentAssertions;
using Loterica.Common.Tests.Base;
using Loterica.Common.Tests.Concursos;
using Loterica.Domain;
using Loterica.Domain.Features.Concursos;
using Loterica.Infra.Data.Features.Concursos;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Loterica.Infra.Data.Tests.Features.Concursos
{
    [TestFixture]
    public class ConcursoSqlRepositoryTests
    {
        IConcursoRepository _repository;
        Concurso _concurso;

        [SetUp]
        public void Initilaze()
        {
            BaseSqlTest.SeedDatabase();
            _repository = new ConcursoSQLRepository();
        }

        [Test]
        public void Infra_Concurso_SQLRepository_Deveria_Adicionar_Concurso()
        {
            _concurso = ObjectMother.GetConcurso();
           _concurso = _repository.Adicionar(_concurso);
            _concurso.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void Infra_Concurso_SQLRepository_Deveria_Alterar_Concurso()
        {
            _concurso = ObjectMother.GetConcursoComId();
            _repository.Editar(_concurso);
            Concurso c =_repository.GetById(_concurso.Id);
            c.dataFechamento.ToShortDateString().Should().Be(_concurso.dataFechamento.ToShortDateString());
        }

        [Test]
        public void Infra_Concurso_SQLRepository_Deveria_Excluir_Concurso()
        {
            _concurso = ObjectMother.GetConcursoComId();
            _repository.Excluir(_concurso.Id);
            Concurso c = _repository.GetById(_concurso.Id);
            c.Should().BeNull();
        }

        [Test]
        public void Infra_Concurso_SQLRepository_Não_Deveria_Excluir_Concurso_Com_Aposta()
        {
            Action action = () => _repository.Excluir(2);
            action.Should().Throw<ExcludeConcursoException>();
        }

        [Test]
        public void Infra_Concurso_SQLRepository_Deveria_BuscarPorId()
        {
            _concurso = _repository.GetById(1);
            _concurso.Should().NotBeNull();
        }

        [Test]
        public void Infra_Concurso_SQLRepository_Deveria_BuscarTodos()
        {
            List<Concurso> concursos = new List<Concurso>()
            {
                new Concurso
                {
                    dataFechamento = DateTime.Now.AddDays(2),
                },
                new Concurso
                {
                      dataFechamento = DateTime.Now.AddDays(5),
                },
            };
            foreach (var item in concursos)
            {
                _repository.Adicionar(item);
            }
            var concursosBanco = _repository.GetAll();
            concursosBanco.Count().Should().Be(4);
        }
    }
}