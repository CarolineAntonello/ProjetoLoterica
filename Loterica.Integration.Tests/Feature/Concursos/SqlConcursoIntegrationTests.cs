using FluentAssertions;
using Loterica.Aplication.Features.Concursos;
using Loterica.Common.Tests.Base;
using Loterica.Common.Tests.Concursos;
using Loterica.Domain;
using Loterica.Domain.Concursos;
using Loterica.Domain.Features.Concursos;
using Loterica.Infra.Data.Features.Concursos;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Loterica.Integration.Tests.Feature.Concursos
{
    [TestFixture]
    public class SqlConcursoIntegrationTests
    {
        Concurso _concurso;
        ConcursoService _service;
        IConcursoRepository _repositorio;

        [SetUp]
        public void Initialize()
        {
            BaseSqlTest.SeedDatabase();
            _repositorio = new ConcursoSQLRepository();
            _service = new ConcursoService(_repositorio);
        }

        [Test]
        public void Integration_AdicionarConcurso_Corretamente()
        {
            _concurso = ObjectMother.GetConcurso();
            _service.Adicionar(_concurso);
            var recebeConcurso = _service.Get(3);
            recebeConcurso.Should().NotBeNull();
            recebeConcurso.dataFechamento.ToShortDateString().Should().Be(_concurso.dataFechamento.ToShortDateString());
        }

        [Test]
        public void Integration_AdicionarConcurso_Incorretamente_Com_Hora_Menor_Que_Atual()
        {
            _concurso = ObjectMother.GetConcursoHoraMenorAtual();
            Action action = () => _service.Adicionar(_concurso);
            action.Should().Throw<InvalidDateTimeException>();
        }

        [Test]
        public void Integration_EditarConcurso_Corretamente()
        {
            _concurso = ObjectMother.GetConcursoComId();
            _service.Editar(_concurso);
            var recebeConcurso = _service.Get(_concurso.Id);
            recebeConcurso.Should().NotBeNull();
        }

        [Test]
        public void Integration_EditarConcurso_Incorretamente_Com_Hora_Menor_Que_Atual()
        {
            _concurso = ObjectMother.GetConcursoHoraMenorAtual();
            Action action = () => _service.Editar(_concurso);
            action.Should().Throw<InvalidDateTimeException>();
        }

        [Test]
        public void Integration_ExcluirConcurso_Corretamente()
        {
            _concurso = ObjectMother.GetConcursoComId();
            _service.Editar(_concurso);
            Concurso c = _service.Get(_concurso.Id);
            c.dataFechamento.ToShortDateString().Should().Be(_concurso.dataFechamento.ToShortDateString());  
        }

        [Test]
        public void Integration_PegarPorConcursoId_Corretamente()
        {
           Concurso c = _service.Get(1);
            c.Should().NotBeNull();
        }

        [Test]
        public void Integration_PegarTodosOsConcurso_Corretamente()
        {
            List<Concurso> c = _service.PegarTodos();
            c.Count.Should().BeGreaterThan(0);
        }
    }
}
