using CsvHelper.Configuration;
using Loterica.Domain;

namespace Loterica.Infra.CSV
{
    public sealed class ConcursoMap : ClassMap<Concurso>
    {
        public ConcursoMap()
        {
            Map(m => m.Id).Name("Número do Concurso");
            Map(m => m.dataFechamento).Name("Data de fechamento do concurso");
            Map(m => m.valorTotalApostas).Name("Valor Total das apostas");
        }
    }
}