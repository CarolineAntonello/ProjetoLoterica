using FluentAssertions;
using Loterica.Aplication.Features.Boloes;
using Loterica.Common.Tests.Boloes;
using Loterica.Domain.Features.Apostas;
using Loterica.Domain.Features.Boloes;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Loterica.Aplication.Tests.Features.Boloes
{
    [TestFixture]
    public class BolaoServiceTests
    {
        Mock<IBolaoRepository> _repository;
        BolaoService _service;
        Bolao _bolao;
        [SetUp]
        public void Initialize()
        {
            _repository = new Mock<IBolaoRepository>();
            _service = new BolaoService(_repository.Object);
        }

        [Test]
        public void Service_Bolao_Deveria_Adicionar_Bolao()
        {
            _bolao = ObjectMother.GetBolao();

            _repository
                .Setup(x => x.Adicionar(It.IsAny<Bolao>()))
                .Returns(new Bolao
                {
                    apostas = _bolao.apostas,
                    concurso = _bolao.concurso,
                    valorTotalApostasBolao = _bolao.valorTotalApostasBolao,
                    Id = 1
                });

            _service.Adicionar(_bolao);
            _repository.Verify(x => x.Adicionar(_bolao));
        }

        [Test]
        public void Service_Bolao_Nao_Deveria_Adicionar_Bolao_Com_Erros()
        {
            _bolao = ObjectMother.GetBolaoSemApostas();
            Action action = () =>_service.Adicionar(_bolao);
            action.Should().Throw<InvalidListApostaException>();
            _repository.VerifyNoOtherCalls();
        }

        [Test]
        public void Service_Bolao_Deveria_Editar_Bolao()
        {
            _bolao = ObjectMother.GetBolaoComId();

            _repository
                .Setup(x => x.Editar(It.IsAny<Bolao>()));

            _service.Editar(_bolao);
            _repository.Verify(x => x.Editar(_bolao));
        }

        [Test]
        public void Service_Bolao_Deveria_Excluir_Bolao()
        {
            _bolao = ObjectMother.GetBolaoComId();

            _repository
                .Setup(x => x.Excluir(It.IsAny<int>()));

            _service.Excluir(_bolao);
            _repository.Verify(x => x.Excluir(_bolao.Id));
        }
        [Test]
        public void Service_Bolao_Deveria_Buscar_Bolao_Por_Id()
        {
            _bolao = ObjectMother.GetBolaoComId();

            _repository
                .Setup(x => x.GetById(It.IsAny<int>()));

            _service.Get(_bolao.Id);
            _repository.Verify(x => x.GetById(_bolao.Id));
        }

        [Test]
        public void Service_Bolao_Deveria_Buscar_Todos_Os_Boloes()
        {
            List<Bolao> boloes = ObjectMother.GetBoloes();

            _repository
                .Setup(x => x.GetAll())
                .Returns(boloes);

            List<Bolao> recebidos =_service.PegarTodos();
            _repository.Verify(x => x.GetAll());
            recebidos.Should().BeEquivalentTo(boloes);
        }
    }
}
