using System;
using System.Collections.Generic;
using FluentAssertions;
using Loterica.Common.Tests.Base;
using Loterica.Domain.Exceptions;
using Loterica.Domain.Features.Apostas;
using Loterica.Domain.Features.ResultadoConcursos;
using Loterica.Infra.Data.Features.Apostas;
using Loterica.Infra.Data.Features.ResultadoConcursos;
using NUnit.Framework;

namespace Loterica.Infra.Data.Tests.Features.ResultadoConcursos
{
    [TestFixture]
    public class ResultadoConcursoSQLRepositoryTests
    {
        IResultadoConcursoRepository _resultadoConcursoRepository;
        IApostaRepository _apostaRepository;
        ResultadoConcurso _resultadoConcurso;

        [SetUp]
        public void Initialize()
        {
            BaseSqlTest.SeedDatabase();
            _resultadoConcursoRepository = new ResultadoConcursoSQLRepository();
            _apostaRepository = new ApostaSQLRepository();
        }

        [Test]
        public void Infra_ResultadoConcurso_SQLRepository_Deveria_Adicionar()
        {
            _resultadoConcurso = Loterica.Common.Tests.ResultadoConcursos.ObjectMother.GetResultadoConcursoComGanhadores();
            List<Aposta> aposta = Loterica.Common.Tests.Apostas.ObjectMother.Get3Apostas();
            foreach (var item in aposta)
            {
                _apostaRepository.Adicionar(item);
            }
            _resultadoConcurso = _resultadoConcursoRepository.Adicionar(_resultadoConcurso);
            _resultadoConcurso.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void Infra_ResultadoConcurso_SQLRepository_Nao_Deveria_Adicionar_Concurso_Sem_Id()
        {
            _resultadoConcurso = Loterica.Common.Tests.ResultadoConcursos.ObjectMother.GetResultadoSemConcurso();
            List<Aposta> aposta = Loterica.Common.Tests.Apostas.ObjectMother.Get3Apostas();
            foreach (var item in aposta)
            {
                _apostaRepository.Adicionar(item);
            }
            Action action = () => _resultadoConcursoRepository.Adicionar(_resultadoConcurso);
            action.Should().Throw<InvalidConcursoException>();
        }

        [Test]
        public void Infra_ResultadoConcurso_SQLRepository_Deveria_Editar()
        {
            List<Aposta> aposta = Loterica.Common.Tests.Apostas.ObjectMother.Get3Apostas();
            foreach (var item in aposta)
            {
                _apostaRepository.Adicionar(item);
            }
            _resultadoConcurso = Loterica.Common.Tests.ResultadoConcursos.ObjectMother.GetResultadoConcursoComGanhadoresComId();
            _resultadoConcursoRepository.Editar(_resultadoConcurso);
            ResultadoConcurso resultadoConcursoGet =_resultadoConcursoRepository.GetById(1);
            resultadoConcursoGet.ganhadores.Count.Should().Be(3);
        }
        

        [Test]
        public void Infra_ResultadoConcurso_SQLRepository_Deveria_Excluir()
        {
            Action action = () => _resultadoConcursoRepository.Excluir(1);
            action.Should().Throw<UnsupportedOperationException>();
        }


        [Test]
        public void Infra_ResultadoConcurso_SQLRepository_Deveria_PegarTodos()
        {
            List< ResultadoConcurso> resultado = _resultadoConcursoRepository.GetAll();
            resultado.Count.Should().Be(1);
        }

        [Test]
        public void Infra_ResultadoConcurso_SQLRepository_Deveria_Pegar_Por_Id()
        {
            _resultadoConcurso = _resultadoConcursoRepository.GetById(1);
            _resultadoConcurso.Id.Should().Be(1);
            _resultadoConcurso.ganhadores.Count.Should().Be(1);
        }

        [Test]
        public void Infra_ResultadoConcurso_SQLRepository_Deveria_Pegar_Por_Id_Nulo()
        {
            _resultadoConcurso = _resultadoConcursoRepository.GetById(666);
            _resultadoConcurso.Should().BeNull();
        }

        [Test]
        public void Infra_ResultadoConcurso_SQLRepository_Deveria_Pegar_Por_ConcursoId()
        {
            _resultadoConcurso = _resultadoConcursoRepository.GetByConcursoId(2);
            _resultadoConcurso.concurso.Id.Should().Be(2);
            _resultadoConcurso.ganhadores.Count.Should().Be(1);
        }

        [Test]
        public void Infra_ResultadoConcurso_SQLRepository_Nao_Deveria_Pegar_Por_ConcursoId_Invalido()
        {
            _resultadoConcurso = _resultadoConcursoRepository.GetByConcursoId(3);
            _resultadoConcurso.Should().BeNull();
        }
    }
}
