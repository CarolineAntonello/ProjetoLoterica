using Loterica.Aplication.Features.CSV;
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
using System.IO;

namespace Loterica.Aplication.Tests.Features.CSV
{
    [TestFixture]
    public class CSVServiceTests
    {
        CSVService _csvService;
        ResultadoConcursoService _resultadoConcurso;
        //ResultadoConcurso _resultado;
        Concurso _concurso;
        Mock<IResultadoConcursoRepository> _resultadoConcursoRepository;
        Mock<IApostaRepository> _apostaRepository;
        Mock<IFaturamentoLotericaRepository> _faturamentoLotericaRepository;
        Mock<Random> randomMoq;
        int numeroAleatorio = 2;
        string file = @"C:\Users\ndduser\Desktop\Academia 2018\Dupla 12\Projeto-Loterica\Loterica.Aplication.Tests\Arquivo.csv";

        [SetUp]
        public void Initialize()
        {
            _csvService = new CSVService();
            _resultadoConcursoRepository = new Mock<IResultadoConcursoRepository>();
            _apostaRepository = new Mock<IApostaRepository>();
            _faturamentoLotericaRepository = new Mock<IFaturamentoLotericaRepository>();
            randomMoq = new Mock<Random>();
            _resultadoConcurso = new ResultadoConcursoService(_resultadoConcursoRepository.Object, _apostaRepository.Object, _faturamentoLotericaRepository.Object);
            File.Delete(file);
        }

        [Test]
        public void Service_CSV_Exportar_Resultado()
        {
            _concurso = ObjectMother.GetConcursoComId();
            ResultadoConcurso resultadoConcurso = new ResultadoConcurso(_concurso);
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

            resultadoConcurso = _resultadoConcurso.GerarResultadoConcurso(resultadoConcurso, randomMoq.Object);
            List<ResultadoConcurso> resultado = new List<ResultadoConcurso>();
            resultado.Add(resultadoConcurso);
            _csvService.ExportarCSV(resultado, file);
            FileAssert.Exists(file);
        }
    }
}
