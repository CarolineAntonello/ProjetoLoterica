using FluentAssertions;
using Loterica.Common.Tests.Base;
using Loterica.Domain.Exceptions;
using Loterica.Domain.Features.Lotericas;
using Loterica.Infra.Data.Features.Lotericas;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Loterica.Infra.Data.Tests.Features.Lotericas
{
    [TestFixture]
    public class FaturamentoLotericaSQLRepositoryTests
    {
        IFaturamentoLotericaRepository _faturamentoLotericaRepository;
        FaturamentoLoterica _faturamentoLoterica;

        [SetUp]
        public void Initialize()
        {
            BaseSqlTest.SeedDatabase();
            _faturamentoLotericaRepository = new FaturamentoLotericaSQLRepository();
        }

        [Test]
        public void Infra_FaturamentoLoterica_SQLRepository_Deveria_Adicionar()
        {
            _faturamentoLoterica = Loterica.Common.Tests.Lotericas.ObjectMother.GetFaturamento();
            _faturamentoLoterica = _faturamentoLotericaRepository.Adicionar(_faturamentoLoterica);
            _faturamentoLoterica.Id.Should().Be(2);
        }

        [Test]
        public void Infra_FaturamentoLoterica_SQLRepository_Nao_Deveria_Adicionar_Lucro_Maior_Que_Faturamento()
        {
            _faturamentoLoterica = Loterica.Common.Tests.Lotericas.ObjectMother.GetFaturamento();
            _faturamentoLoterica.lucro = 20000;
            _faturamentoLoterica.faturamento = 10000;
            Action action = () => _faturamentoLotericaRepository.Adicionar(_faturamentoLoterica);
            action.Should().Throw<InvalidFaturamentoException>();
        }

        [Test]
        public void Infra_FaturamentoLoterica_SQLRepository_Deveria_Alterar()
        {
            _faturamentoLoterica = Loterica.Common.Tests.Lotericas.ObjectMother.GetFaturamentoComId();
            _faturamentoLotericaRepository.Editar(_faturamentoLoterica);
            FaturamentoLoterica f = _faturamentoLotericaRepository.GetById(_faturamentoLoterica.Id);
            f.Id.Should().Be(_faturamentoLoterica.Id);
            f.lucro.Should().Be(_faturamentoLoterica.lucro);
            f.faturamento.Should().Be(_faturamentoLoterica.faturamento);
        }

        [Test]
        public void Infra_FaturamentoLoterica_SQLRepository_Nao_Deveria_AlterarLucro_Maior_Que_Faturamento()
        {
            _faturamentoLoterica = Loterica.Common.Tests.Lotericas.ObjectMother.GetFaturamentoComId();
            _faturamentoLoterica.lucro = 20000;
            _faturamentoLoterica.faturamento = 10000;
            Action action = () => _faturamentoLotericaRepository.Editar(_faturamentoLoterica);
            action.Should().Throw<InvalidFaturamentoException>();
        }

        [Test]
        public void Infra_FaturamentoLoterica_SQLRepository_Deveria_Lançar_Exceçao_No_Excluir()
        {
            _faturamentoLoterica = Loterica.Common.Tests.Lotericas.ObjectMother.GetFaturamentoComId();
            Action action = () => _faturamentoLotericaRepository.Excluir(_faturamentoLoterica.Id);
            action.Should().Throw<UnsupportedOperationException>();
        }

        [Test]
        public void Infra_FaturamentoLoterica_SQLRepository_Deveria_PegarPorId()
        {
            FaturamentoLoterica faturamento = _faturamentoLotericaRepository.GetById(1);
            faturamento.Should().NotBeNull();
        }

        [Test]
        public void Infra_FaturamentoLoterica_SQLRepository_Deveria_PegarTodos()
        {
            List<FaturamentoLoterica> faturamentos = _faturamentoLotericaRepository.GetAll();
            faturamentos.Count.Should().Be(1);
        }

        [Test]
        public void Infra_FaturamentoLoterica_SQLRepository_Deveria_Pegar_Por_ConcursoId()
        {
            _faturamentoLoterica = _faturamentoLotericaRepository.GetByConcursoId(2);
            _faturamentoLoterica.concurso.Id.Should().Be(2);
        }
    }
}
