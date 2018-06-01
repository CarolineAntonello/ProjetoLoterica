using FluentAssertions;
using Loterica.Aplication.Features.Apostas;
using Loterica.Common.Tests.Apostas;
using Loterica.Common.Tests.Base;
using Loterica.Domain;
using Loterica.Domain.Exceptions;
using Loterica.Domain.Features.Apostas;
using Loterica.Domain.Features.Lotericas;
using Loterica.Domain.Features.ResultadoConcursos;
using Loterica.Infra.Data.Features.Apostas;
using Loterica.Infra.Data.Features.Lotericas;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Loterica.Integration.Tests.Feature.Apostas
{
    [TestFixture]
    public class SqlApostaIntegrationTests
    {
        Aposta _aposta;
        ApostaService _service;
        IApostaRepository _repository;
        Mock<IResultadoConcursoRepository> _resultadoConcursoRepository;
        IFaturamentoLotericaRepository _faturamentoLotericaRepository;

        [SetUp]
        public void Initialize()
        {
            BaseSqlTest.SeedDatabase();
            _repository = new ApostaSQLRepository();
            _resultadoConcursoRepository = new Mock<IResultadoConcursoRepository>();
            _faturamentoLotericaRepository = new FaturamentoLotericaSQLRepository();
            _service = new ApostaService(_repository, _resultadoConcursoRepository.Object, _faturamentoLotericaRepository);
        }

        [Test]
        public void Integration_AdicionarAposta_Corretamente()
        {
            _aposta = ObjectMother.GetAposta();
            _service.Adicionar(_aposta);
            var recebeAposta = _service.Get(2);
            recebeAposta.Should().NotBeNull();
            recebeAposta.numerosApostados.Should().BeEquivalentTo(_aposta.numerosApostados);
        }

        [Test]
        public void Integration_AdicionarAposta_Incorretamente_Com_Sete_Numeros_Apostados()
        {
            _aposta = ObjectMother.GetApostaSeteNumerosApostados();
            Action action = () => _service.Adicionar(_aposta);
            action.Should().Throw<InvalidBetException>();
        }

        [Test]
        public void Integration_EditarAposta_Corretamente()
        {
            _aposta = ObjectMother.GetApostaComId();
            Action action = () =>_service.Editar(_aposta);
            action.Should().Throw<UnsupportedOperationException>();
        }

        [Test]
        public void Integration_DeletarAposta_Corretamente()
        {
            _aposta = ObjectMother.GetApostaComId();
            Action action = () => _service.Excluir(_aposta);
            action.Should().Throw<UnsupportedOperationException>();
        }

        [Test]
        public void Integration_PegarApostaPorId_Corretamente()
        {
            Aposta aposta =_service.Get(1);
            aposta.Should().NotBeNull();
            aposta.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void Integration_PegarTodasAsAposta_Corretamente()
        {
            List<Aposta> apostas = _service.PegarTodos();
            apostas.Count.Should().BeGreaterThan(0);
        }

        [Test]
        public void Integration_GerarApostaAutomaticamente_Corretamente()
        {
            Aposta aposta = new Aposta(new Concurso());
            _service.GerarApostaAutomaticamente(aposta);
            aposta.numerosApostados.Count.Should().Be(6);
        }

        [Test]
        public void Integration_CalcularResultadoFinal_Corretamente()
        {
            _aposta = ObjectMother.GetAposta();
            List<int> numerosResultado = new List<int> { 2, 4, 1, 6, 9, 11 };
            ResultadoConcurso resultadoConcurso = new ResultadoConcurso(_aposta.concurso);
            resultadoConcurso.numerosResultado.Clear();
            resultadoConcurso.numerosResultado = numerosResultado;
            _resultadoConcursoRepository.Setup(x => x.GetByConcursoId(It.IsAny<int>())).Returns(resultadoConcurso);
            int acertos = _service.CalcularResultadoFinal(_aposta);
            acertos.Should().Be(4);
        }
    }
}
