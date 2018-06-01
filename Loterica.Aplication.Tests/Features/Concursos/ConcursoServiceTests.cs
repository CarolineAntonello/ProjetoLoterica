using FluentAssertions;
using Loterica.Aplication.Features.Concursos;
using Loterica.Common.Tests.Concursos;
using Loterica.Domain;
using Loterica.Domain.Concursos;
using Loterica.Domain.Features.Concursos;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Loterica.Aplication.Tests.Features.Concursos
{
    [TestFixture]
    public class ConcursoServiceTests
    {

        Mock<IConcursoRepository> _repository;
        ConcursoService _service;
        Concurso _concurso;


        [SetUp]
        public void Initialize()
        {

            _repository = new Mock<IConcursoRepository>();
            _service = new ConcursoService(_repository.Object);
        }

        [Test]
        public void Service_Deveria_Adicionar_Concurso_Corretamente()
        {
            _concurso = ObjectMother.GetConcurso();

            _repository.
                Setup(x => x.Adicionar(It.IsAny<Concurso>()))
                .Returns(new Concurso
                {
                    Id = 2,
                    dataFechamento = _concurso.dataFechamento,
                    valorTotalApostas = _concurso.valorTotalApostas
                });

            _service.Adicionar(_concurso);
            _repository.Verify(x => x.Adicionar(_concurso));
        }

        [Test]
        public void Service_Deveria_Adicionar_Concurso_Data_Menor_Que_Atual()
        {
            _concurso = ObjectMother.GetConcursoHoraMenorAtual();
            Action action = () =>_service.Adicionar(_concurso);
            action.Should().Throw<InvalidDateTimeException>();
            _repository.VerifyNoOtherCalls();
        }

        [Test]
        public void Service_Deveria_Editar_Concurso_Corretamente()
        {
            _concurso = ObjectMother.GetConcursoComId();

            _repository
                .Setup(x => x.Editar(It.IsAny<Concurso>()));

            _service.Editar(_concurso);
            _repository.Verify(x => x.Editar(_concurso));
        }

        [Test]
        public void Service_Deveria_Editar_Concurso_Data_Menor_Que_Atual()
        {
            _concurso = ObjectMother.GetConcursoComId();
            _concurso.dataFechamento = DateTime.Now.AddDays(-1);
            Action action = () => _service.Editar(_concurso);
            action.Should().Throw<InvalidDateTimeException>();
            _repository.VerifyNoOtherCalls();
        }

        [Test]
        public void Service_Deveria_Deletar_Concurso_Corretamente()
        {
            _concurso = ObjectMother.GetConcursoComId();

            _repository
                .Setup(x => x.Excluir(It.IsAny<int>()));

            _service.Excluir(_concurso);
            _repository.Verify(x => x.Excluir(_concurso.Id));
        }

        [Test]
        public void Service_Deveria_BuascarPorId_Concurso_Corretamente()
        {
            _concurso = ObjectMother.GetConcursoComId();
            _repository

                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(_concurso);

            Concurso c =_service.Get(_concurso.Id);
            _repository.Verify(x => x.GetById(_concurso.Id));
            c.dataFechamento.ToShortDateString().Should().Be(_concurso.dataFechamento.ToShortDateString());
        }


        [Test]
        public void Service_Deveria_BuascarTodos_Concursos_Corretamente()
        {
            List<Concurso> concursos = ObjectMother.GetConcursos();
            _repository
                .Setup(x => x.GetAll())
                .Returns(concursos);

            List<Concurso> c = _service.PegarTodos();
            _repository.Verify(x => x.GetAll());
            c.Count.Should().Be(concursos.Count);
        }

    }
}
