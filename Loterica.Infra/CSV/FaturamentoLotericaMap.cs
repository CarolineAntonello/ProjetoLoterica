using CsvHelper.Configuration;
using Loterica.Domain.Features.Lotericas;

namespace Loterica.Infra.CSV
{
    public sealed class FaturamentoLotericaMap : ClassMap<FaturamentoLoterica>
    {
        public FaturamentoLotericaMap()
        {
            //Map(m => m.Id).Name("Número ");
            Map(m => m.faturamento).Name("Faturamento da lotérica");
            Map(m => m.lucro).Name("Lucro da lotérica");
            //Map(m => m.concurso.Id).Name("Número do concurso");
        }
    }
}