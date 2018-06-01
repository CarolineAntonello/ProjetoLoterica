using Loterica.Domain;
using Loterica.Domain.Features.Lotericas;
using System.Collections.Generic;

namespace Loterica.Common.Tests.Lotericas
{
    public partial class ObjectMother
    {
        public static FaturamentoLoterica GetFaturamento()
        {
            Concurso concurso = Loterica.Common.Tests.Concursos.ObjectMother.GetConcursoComId();
            FaturamentoLoterica faturamento = new FaturamentoLoterica(concurso);
            return faturamento;
        }

        public static FaturamentoLoterica GetFaturamentoComId()
        {
            Concurso concurso = Loterica.Common.Tests.Concursos.ObjectMother.GetConcursoComId();
            FaturamentoLoterica faturamento = new FaturamentoLoterica(concurso);
            faturamento.Id = 1;
            return faturamento;
        }

        public static List<FaturamentoLoterica> GetFaturamentos()
        {
            Concurso concurso = Loterica.Common.Tests.Concursos.ObjectMother.GetConcursoComId();

            return new List<FaturamentoLoterica>
            {
                new FaturamentoLoterica(concurso) { Id = 1 },
                new FaturamentoLoterica(concurso) { Id = 2 },
                new FaturamentoLoterica(concurso) { Id = 3 }
            };
        }
    }
}
