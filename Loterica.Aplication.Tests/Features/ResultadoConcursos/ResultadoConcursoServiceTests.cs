using FluentAssertions;
using Loterica.Aplication.Features.ResultadoConcursos;
using Loterica.Common.Tests.Concursos;
using Loterica.Domain;
using Loterica.Domain.Features.Apostas;
using Loterica.Domain.Features.Lotericas;
using Loterica.Domain.Features.ResultadoConcursos;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Loterica.Aplication.Tests.Features.ResultadoConcursos
{
    [TestFixture]
    public class ResultadoConcursoServiceTests
    {
        Mock<IResultadoConcursoRepository> _resultadoConcursoRepository;
        Mock<IApostaRepository> _apostaRepository;
        Mock<IFaturamentoLotericaRepository> _faturamentoLotericaRepository;
        Mock<Random> randomMoq;
        int numeroAleatorio = 2;
        ResultadoConcursoService _service;
        Concurso _concurso;

        [SetUp]
        public void Initialize()
        {
            _resultadoConcursoRepository = new Mock<IResultadoConcursoRepository>();
            _apostaRepository = new Mock<IApostaRepository>();
            _faturamentoLotericaRepository = new Mock<IFaturamentoLotericaRepository>();
            randomMoq = new Mock<Random>();
            _service = new ResultadoConcursoService(_resultadoConcursoRepository.Object, _apostaRepository.Object, _faturamentoLotericaRepository.Object);
        }

        [Test]
        public void Service_ResultadoConcurso_GerarResultadoConcurso_Deveria_Gerar_Dados()
        {
            _concurso = ObjectMother.GetConcursoComId();
            ResultadoConcurso resultado = new ResultadoConcurso(_concurso);
            List<Aposta> apostas = Loterica.Common.Tests.Apostas.ObjectMother.GetApostas();

            _apostaRepository
                .Setup(x => x.GetByConcursoId(It.IsAny<int>()))
                .Returns(apostas);

            _apostaRepository
               .Setup(x => x.GetByConcursoIdComBolao(It.IsAny<int>()))
               .Returns(apostas);

            randomMoq
                .Setup(r => r.Next(1, 61))
                .Returns(() => numeroAleatorio++);

            resultado = _service.GerarResultadoConcurso(resultado, randomMoq.Object);

            _apostaRepository
                .Verify(x => x.GetByConcursoId(_concurso.Id));

            resultado.premioTotal.Should().Be(7200.9);
            resultado.numerosResultado.Count().Should().Be(6);
            foreach (var item in resultado.numerosResultado)
            {
                if (item > 60 || item < 1)
                {
                    Assert.Fail();
                }
            }
        }

        [Test]
        public void Service_ResultadoConcurso_Deveria_Adicionar_ResultadoConcurso()
        {
            _concurso = ObjectMother.GetConcursoComId();
            ResultadoConcurso resultado = new ResultadoConcurso(_concurso);
            FaturamentoLoterica faturamento = Loterica.Common.Tests.Lotericas.ObjectMother.GetFaturamentoComId();
            List<Aposta> apostas = Loterica.Common.Tests.Apostas.ObjectMother.GetApostas();

            _apostaRepository
                .Setup(x => x.GetByConcursoId(It.IsAny<int>()))
                .Returns(apostas);

            _apostaRepository
                .Setup(x => x.GetByConcursoIdComBolao(It.IsAny<int>()))
                .Returns(apostas);

            _resultadoConcursoRepository
                .Setup(y => y.Adicionar(It.IsAny<ResultadoConcurso>()));

            randomMoq
                .Setup(r => r.Next(1, 61))
                .Returns(() => numeroAleatorio++);

            resultado =_service.GerarResultadoConcurso(resultado, randomMoq.Object);
            _service.Adicionar(resultado);

            _apostaRepository
                .Verify(x => x.GetByConcursoId(_concurso.Id));

            _resultadoConcursoRepository
                .Verify(y => y.Adicionar(resultado));

            resultado.premioTotal.Should().Be(7200.9);
            resultado.numerosResultado.Count().Should().Be(6);
            foreach (var item in resultado.numerosResultado)
            {
                if (item > 60 || item < 1)
                {
                    Assert.Fail();
                }
            }
            
        }

        [Test]
        public void Service_ResultadoConcurso_BuscarPorId_Deveria_Buscar_ResultadoConcurso()
        {
            ResultadoConcurso resultado = Loterica.Common.Tests.ResultadoConcursos.ObjectMother.GetResultadoConcursoComId();

            _resultadoConcursoRepository
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(resultado);

            ResultadoConcurso resultadoRecebido = _service.Get(resultado.Id);

            _resultadoConcursoRepository
                .Verify(x => x.GetById(resultado.Id));

            resultadoRecebido.Id.Should().Be(1);
        }

        [Test]
        public void Service_ResultadoConcurso_BuscarTodos_Deveria_Buscar_ResultadosConcurso()
        {
            List<ResultadoConcurso> resultados = new List<ResultadoConcurso>();
            ResultadoConcurso resultado = Loterica.Common.Tests.ResultadoConcursos.ObjectMother.GetResultadoConcursoComId();
            resultados.Add(resultado);
            resultados.Add(resultado);
            resultados.Add(resultado);

            _resultadoConcursoRepository
                .Setup(x => x.GetAll())
                .Returns(resultados);

            List<ResultadoConcurso> resultadosRecebidos = _service.PegarTodos();

            _resultadoConcursoRepository.Verify(x => x.GetAll());
            resultadosRecebidos.Should().BeEquivalentTo(resultados);
        }

        [Test]
        public void Service_ResultadoConcurso_Deveria_Adicionar_ResultadoConcurso_Sem_Aposta()
        {
            _concurso = ObjectMother.GetConcursoSemApostas();
            ResultadoConcurso resultado = new ResultadoConcurso(_concurso);
            List<Aposta> apostas = new List<Aposta>();

            _apostaRepository
                .Setup(x => x.GetByConcursoId(It.IsAny<int>()))
                .Returns(apostas);

            _apostaRepository
               .Setup(x => x.GetByConcursoIdComBolao(It.IsAny<int>()))
               .Returns(apostas);

            _resultadoConcursoRepository
                .Setup(y => y.Adicionar(It.IsAny<ResultadoConcurso>()));

            randomMoq
                .Setup(r => r.Next(1, 61))
                .Returns(() => numeroAleatorio++);

            resultado =_service.GerarResultadoConcurso(resultado, randomMoq.Object);
            _service.Adicionar(resultado);

            _apostaRepository
                .Verify(x => x.GetByConcursoId(_concurso.Id));

            _resultadoConcursoRepository
                .Verify(y => y.Adicionar(resultado));

            resultado.premioTotal.Should().Be(0);
            resultado.numerosResultado.Count().Should().Be(6);
            resultado.premioQuadraPorJogador.Should().Be(0);
            resultado.premioQuinaPorJogador.Should().Be(0);
            resultado.premioSenaPorJogador.Should().Be(0);
            resultado.qtdAcertadoresQuadra.Should().Be(0);
            resultado.qtdAcertadoresQuina.Should().Be(0);
            resultado.qtdAcertadoresSena.Should().Be(0);
            resultado.ganhadores.Count.Should().Be(0);
            foreach (var item in resultado.numerosResultado)
            {
                if (item > 60 || item < 1)
                {
                    Assert.Fail();
                }
            }
        }
    }
}
