using Loterica.Domain.Features.Boloes;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Loterica.Common.Tests.Boloes
{
    [ExcludeFromCodeCoverage]
    public partial class ObjectMother
    {
        public static Bolao GetBolao()
        {
            return new Bolao
            {
                //valorTotalApostasBolao = 6.30,
                apostas = Loterica.Common.Tests.Apostas.ObjectMother.GetApostas(),
                concurso = Loterica.Common.Tests.Concursos.ObjectMother.GetConcursoComId(),
            };
        }

        public static Bolao GetBolaoComId()
        {
            return new Bolao
            {
                Id = 1,
                valorTotalApostasBolao = 6.30,
                apostas = Loterica.Common.Tests.Apostas.ObjectMother.GetApostas(),
                concurso = Loterica.Common.Tests.Concursos.ObjectMother.GetConcursoComId(),
            };
        }
        public static Bolao GetBolaoSemConcurso()
        {
            return new Bolao
            {
                valorTotalApostasBolao = 6.30,
                apostas = Loterica.Common.Tests.Apostas.ObjectMother.GetApostas(),
            };
        }

        public static Bolao GetBolaoSemApostas()
        {
            return new Bolao
            {
                valorTotalApostasBolao = 6.30,
                concurso = Loterica.Common.Tests.Concursos.ObjectMother.GetConcursoComId(),
            };
        }

        public static List<Bolao> GetBoloes()
        {
            return new List<Bolao>
            {
                 new Bolao
                 {
                     Id = 1,
                     valorTotalApostasBolao = 6.30,
                     apostas = Loterica.Common.Tests.Apostas.ObjectMother.GetApostas(),
                     concurso = Loterica.Common.Tests.Concursos.ObjectMother.GetConcursoComId(),
                 },
                  new Bolao
                  {
                      Id = 2,
                      valorTotalApostasBolao = 6.30,
                      apostas = Loterica.Common.Tests.Apostas.ObjectMother.GetApostas(),
                      concurso = Loterica.Common.Tests.Concursos.ObjectMother.GetConcursoComId(),
                  },
            };
        }

        public static Bolao GetBolaoCom10Apostas()
        {
            Bolao bolao = new Bolao
            {
                concurso = Loterica.Common.Tests.Concursos.ObjectMother.GetConcursoComId(),
            };
            bolao.GerarApostas(10);
            bolao.CalculaValorDoBolao();
            return bolao;
        }
    }
}
