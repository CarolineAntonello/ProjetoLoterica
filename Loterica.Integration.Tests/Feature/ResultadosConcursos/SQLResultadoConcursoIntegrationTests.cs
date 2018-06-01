using FluentAssertions;
using Loterica.Aplication.Features.ResultadoConcursos;
using Loterica.Common.Tests.Base;
using Loterica.Common.Tests.Concursos;
using Loterica.Domain;
using Loterica.Domain.Exceptions;
using Loterica.Domain.Features.Apostas;
using Loterica.Domain.Features.Lotericas;
using Loterica.Domain.Features.ResultadoConcursos;
using Loterica.Infra.Data.Features.Apostas;
using Loterica.Infra.Data.Features.Lotericas;
using Loterica.Infra.Data.Features.ResultadoConcursos;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Loterica.Integration.Tests.Feature.ResultadosConcursos
{
    [TestFixture]
    public class SQLResultadoConcursoIntegrationTests
    {
        IResultadoConcursoRepository _resultadoConcursoRepository;
        IApostaRepository _apostaRepository;
        IFaturamentoLotericaRepository _faturamentoLotericaRepository;
        ResultadoConcursoService _service;
        Concurso _concurso;
        Random random;

        [SetUp]
        public void Initialize()
        {
            BaseSqlTest.SeedDatabase();
            _resultadoConcursoRepository = new ResultadoConcursoSQLRepository();
            _apostaRepository = new ApostaSQLRepository();
            _faturamentoLotericaRepository = new FaturamentoLotericaSQLRepository();
            random = new Random();
            _service = new ResultadoConcursoService(_resultadoConcursoRepository, _apostaRepository, _faturamentoLotericaRepository);
        }

        [Test]
        public void Integration_ResultadoConcurso_GerarResultadoConcurso_Deveria_Gerar_Dados()
        {
            _concurso = ObjectMother.GetConcursoComId();
            ResultadoConcurso resultado = new ResultadoConcurso(_concurso);
            resultado = _service.GerarResultadoConcurso(resultado, random);
            foreach (var item in resultado.numerosResultado)
            {
                if (item > 60 || item < 1)
                {
                    Assert.Fail();
                }
            }
        }

        [Test]
        public void Integration_ResultadoConcurso_Deveria_Adicionar_Corretamente()
        {
            _concurso = ObjectMother.GetConcursoComId();
            ResultadoConcurso resultado = new ResultadoConcurso(_concurso);
            resultado = _service.GerarResultadoConcurso(resultado, random);
            _service.Adicionar(resultado);
            ResultadoConcurso r = _service.Get(2);
            r.numerosResultado.Should().BeEquivalentTo(resultado.numerosResultado);
        }

        [Test]
        public void Integration_ResultadoConcurso_Deveria_Editar_Corretamente()
        {
            ResultadoConcurso resultado = _service.BuscarPorConcursoId(2);
            resultado =_service.GerarResultadoConcurso(resultado, random);
            _service.Editar(resultado);
            ResultadoConcurso r = _service.Get(resultado.Id);
            r.numerosResultado.Should().BeEquivalentTo(resultado.numerosResultado);
        }

        [Test]
        public void Integration_ResultadoConcurso_Excluir_Deveria_Retornar_Excecao()
        {
            ResultadoConcurso resultado = Loterica.Common.Tests.ResultadoConcursos.ObjectMother.GetResultadoConcursoComId();
            Action action = () =>_service.Excluir(resultado);
            action.Should().Throw<UnsupportedOperationException>();
        }

        [Test]
        public void Integration_ResultadoConcurso_GetById_Deveria_Buscar_Corretamente()
        {
            ResultadoConcurso resultado = _service.Get(1);
            resultado.Should().NotBeNull();
        }

        [Test]
        public void Integration_ResultadoConcurso_GetByConcursoId_Deveria_Buscar_Corretamente()
        {
            ResultadoConcurso resultado = _service.BuscarPorConcursoId(2);
            resultado.Should().NotBeNull();
        }

        [Test]
        public void Integration_ResultadoConcurso_GetAll_Deveria_Buscar_Corretamente()
        {
            List<ResultadoConcurso> resultados = _service.PegarTodos();
            resultados.Count.Should().BeGreaterThan(0);
        }
    }
}
