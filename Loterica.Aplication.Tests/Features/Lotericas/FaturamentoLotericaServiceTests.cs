using FluentAssertions;
using Loterica.Aplication.Features.Lotericas;
using Loterica.Domain.Features.Lotericas;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Loterica.Aplication.Tests.Features.Lotericas
{
    [TestFixture]
    public class FaturamentoLotericaServiceTests
    {
        Mock<IFaturamentoLotericaRepository> _faturamentoLotericaRepository;
        FaturamentoLotericaService _service;
        FaturamentoLoterica _faturamentoLoterica;

        [SetUp]
        public void Initialize()
        {
            _faturamentoLotericaRepository = new Mock<IFaturamentoLotericaRepository>();
            _service = new FaturamentoLotericaService(_faturamentoLotericaRepository.Object);

        }

        [Test]
        public void Service_FaturamentoLoterica_Deveria_Adicionar_Faturamento()
        {
            _faturamentoLoterica = Loterica.Common.Tests.Lotericas.ObjectMother.GetFaturamento();
            _faturamentoLoterica.CalcularFaturamentoELucro(100, 10);

            _faturamentoLotericaRepository
                .Setup(x => x.Adicionar(It.IsAny<FaturamentoLoterica>()))
                .Returns(
                new FaturamentoLoterica(_faturamentoLoterica.concurso)
                {
                    Id = 1,
                    faturamento = _faturamentoLoterica.faturamento,
                    lucro = _faturamentoLoterica.lucro
                });

            _service.Adicionar(_faturamentoLoterica);

            _faturamentoLotericaRepository.Verify(x => x.Adicionar(_faturamentoLoterica));
        }

        [Test]
        public void Service_FaturamentoLoterica_Nao_Deve_Adicionar_Caso_Nao_Passe_Na_Validacao()
        {
            _faturamentoLoterica = Loterica.Common.Tests.Lotericas.ObjectMother.GetFaturamento();
            _faturamentoLoterica.CalcularFaturamentoELucro(100, 10);
            _faturamentoLoterica.lucro = 1000;
           
            Action action = () =>_service.Adicionar(_faturamentoLoterica);

            action.Should().Throw<InvalidFaturamentoException>();
            _faturamentoLotericaRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Service_FaturamentoLoterica_Deveria_Editar_Faturamento()
        {
            _faturamentoLoterica = Loterica.Common.Tests.Lotericas.ObjectMother.GetFaturamentoComId();
            _faturamentoLoterica.CalcularFaturamentoELucro(100, 10);

            _faturamentoLotericaRepository
                .Setup(x => x.Editar(It.IsAny<FaturamentoLoterica>()));

            _service.Editar(_faturamentoLoterica);

            _faturamentoLotericaRepository.Verify(x => x.Editar(_faturamentoLoterica));
        }

        [Test]
        public void Service_FaturamentoLoterica_Nao_Deve_Editar_Caso_Nao_Passe_Na_Validacao()
        {
            _faturamentoLoterica = Loterica.Common.Tests.Lotericas.ObjectMother.GetFaturamentoComId();
            _faturamentoLoterica.CalcularFaturamentoELucro(100, 10);
            _faturamentoLoterica.lucro = 1000;

            Action action = () => _service.Editar(_faturamentoLoterica);

            action.Should().Throw<InvalidFaturamentoException>();
            _faturamentoLotericaRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Service_FaturamentoLoterica_Deveria_Chamar_Excluir_Faturamento_do_Repository()
        {
            _faturamentoLoterica = Loterica.Common.Tests.Lotericas.ObjectMother.GetFaturamentoComId();
            _faturamentoLoterica.CalcularFaturamentoELucro(100, 10);

            _faturamentoLotericaRepository
                .Setup(x => x.Excluir(It.IsAny<int>()));

            _service.Excluir(_faturamentoLoterica);

            _faturamentoLotericaRepository.Verify(x => x.Excluir(_faturamentoLoterica.Id));
        }

        [Test]
        public void Service_FaturamentoLoterica_Deveria_Chamar_GetById_do_Repository()
        {
            _faturamentoLoterica = Loterica.Common.Tests.Lotericas.ObjectMother.GetFaturamentoComId();
            _faturamentoLoterica.CalcularFaturamentoELucro(1000, 10);

            _faturamentoLotericaRepository
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(_faturamentoLoterica);

            FaturamentoLoterica faturamentoRecebido = _service.Get(_faturamentoLoterica.Id);

            _faturamentoLotericaRepository
                .Verify(x => x.GetById(_faturamentoLoterica.Id));

            faturamentoRecebido.Should().BeEquivalentTo(_faturamentoLoterica);
        }

        [Test]
        public void Service_FaturamentoLoterica_Deveria_Chamar_GetAll_do_Repository()
        {
            List<FaturamentoLoterica> faturamentosLoterica = Loterica.Common.Tests.Lotericas.ObjectMother.GetFaturamentos();

            _faturamentoLotericaRepository
                .Setup(x => x.GetAll())
                .Returns(faturamentosLoterica);

            List<FaturamentoLoterica> faturamentosRecebidos = _service.PegarTodos();

            _faturamentoLotericaRepository
                .Verify(x => x.GetAll());

            faturamentosRecebidos.Should().BeEquivalentTo(faturamentosLoterica);
        }

        [Test]
        public void Service_FaturamentoLoterica_Deveria_Chamar_GetByConcursoId_do_Repository()
        {
            _faturamentoLoterica = Loterica.Common.Tests.Lotericas.ObjectMother.GetFaturamentoComId();
            _faturamentoLoterica.CalcularFaturamentoELucro(1000, 10);

            _faturamentoLotericaRepository
                .Setup(x => x.GetByConcursoId(It.IsAny<int>()))
                .Returns(_faturamentoLoterica);

            FaturamentoLoterica faturamentoRecebido = _service.GetByConcursoId(_faturamentoLoterica.concurso.Id);

            _faturamentoLotericaRepository
                .Verify(x => x.GetByConcursoId(_faturamentoLoterica.concurso.Id));

            faturamentoRecebido.Should().BeEquivalentTo(_faturamentoLoterica);
        }


    }
}
