using FluentAssertions;
using Loterica.Aplication.Features.Apostas;
using Loterica.Common.Tests.Apostas;
using Loterica.Domain;
using Loterica.Domain.Features.Apostas;
using Loterica.Domain.Features.Lotericas;
using Loterica.Domain.Features.ResultadoConcursos;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Loterica.Aplication.Tests.Features.Apostas
{
    [TestFixture]
    public class ApostaServiceTests
    {
        ApostaService _service;
        Mock<IApostaRepository> _apostaRepository;
        Mock<IResultadoConcursoRepository> _resultadoConcursoRepository;
        Mock<IFaturamentoLotericaRepository> _faturamentoLotericaRepository;
        Aposta _aposta;

        [SetUp]
        public void Initialize()
        {
            _apostaRepository = new Mock<IApostaRepository>();
            _resultadoConcursoRepository = new Mock<IResultadoConcursoRepository>();
            _faturamentoLotericaRepository = new Mock<IFaturamentoLotericaRepository>();
            _service = new ApostaService(_apostaRepository.Object, _resultadoConcursoRepository.Object, _faturamentoLotericaRepository.Object);
        }

        [Test]
        public void Service_Aposta_Deveria_Adicionar_Aposta()
        {
            _aposta = ObjectMother.GetAposta();
            _apostaRepository

                .Setup(x => x.Adicionar(It.IsAny<Aposta>()))
                .Returns( new Aposta(new Concurso
                {
                    Id = 1,
                    dataFechamento = DateTime.Now.AddHours(2),
                    valorTotalApostas = 8001,
                })
                {
                    Id = 1,
                   numerosApostados = new List<int> { 1, 2, 3, 4, 5, 6 },

                });

            _service.Adicionar(_aposta);
            _apostaRepository.Verify(x => x.Adicionar(_aposta));
        }

        [Test]
        public void Service_Aposta_Adicionar_Numeros_Apostados_Maiores_Que_6()
        {
            _aposta = ObjectMother.GetApostaSeteNumerosApostados();
            Action action = () => _service.Adicionar(_aposta);
            action.Should().Throw<InvalidBetException>();
            _apostaRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Service_Aposta_Deveria_Editar_Aposta()
        {
            _aposta = ObjectMother.GetApostaComId();

            _apostaRepository
                .Setup(x => x.Editar(It.IsAny<Aposta>()));

            _service.Editar(_aposta);
            _apostaRepository.Verify(x => x.Editar(_aposta));
        }

        [Test]
        public void Service_Aposta_Editar_Numeros_Apostados_Maiores_Que_6()
        {
            _aposta = ObjectMother.GetApostaSeteNumerosApostados();
            Action action = () => _service.Editar(_aposta);
            action.Should().Throw<InvalidBetException>();
            _apostaRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Service_Aposta_Deveria_Excluir_Aposta()
        {
            _aposta = ObjectMother.GetApostaComId();

            _apostaRepository
                .Setup(x => x.Excluir(It.IsAny<int>()));

            _service.Excluir(_aposta);
            _apostaRepository.Verify(x => x.Excluir(_aposta.Id));
        }

        [Test]
        public void Service_Aposta_Deveria_BuascarPorId_Aposta_Corretamente()
        {
            _aposta = ObjectMother.GetApostaComId();

            _apostaRepository
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(_aposta);

            Aposta a = _service.Get(_aposta.Id);
            _apostaRepository.Verify(x => x.GetById(_aposta.Id));
            a.numerosApostados.Should().BeEquivalentTo(_aposta.numerosApostados);
        }

        [Test]
        public void Service_Aposta_Deveria_BuscarTodos_Apostas_Corretamente()
        {
            List<Aposta> apostas = ObjectMother.GetApostas();

            _apostaRepository
                .Setup(x => x.GetAll())
                .Returns(apostas);

            List<Aposta> a = _service.PegarTodos();
            _apostaRepository.Verify(x => x.GetAll());
            a.Should().BeEquivalentTo(apostas);
        }

        [Test]
        public void Service_Aposta_Deveria_Gerar_Numeros_Da_Aposta_Automaticamente()
        {
            _aposta = ObjectMother.GetApostaSemNumerosApostados();
            _service.GerarApostaAutomaticamente(_aposta);
            _aposta.numerosApostados.Count.Should().Be(6);
        }

        [Test]
        public void Service_Aposta_Deveria_Calcular_Resultado_Com_Quatro_Acertos()
        {
            _aposta = ObjectMother.GetAposta();
            List<int> numerosResultado = new List<int> { 2, 4, 1, 6, 9, 11 };
            ResultadoConcurso resultadoConcurso = new ResultadoConcurso(_aposta.concurso);
            resultadoConcurso.numerosResultado.Clear();
            resultadoConcurso.numerosResultado = numerosResultado;

            _resultadoConcursoRepository
                .Setup(x => x.GetByConcursoId(It.IsAny<int>()))
                .Returns(resultadoConcurso);
            
            int acertos =_service.CalcularResultadoFinal(_aposta);
            acertos.Should().Be(4);
        }
    }
}
