using CsvHelper.Configuration;
using Loterica.Domain.Features.ResultadoConcursos;
using System.Linq;

namespace Loterica.Infra.CSV
{
    public sealed class ResultadoConcursoMap : ClassMap<ResultadoConcurso>
    {
        public ResultadoConcursoMap()
        {
            Map(m => m.Id).Name("Número do Resulatdo do Concurso");
            Map(m => m.numerosResultado)
                .ConvertUsing(row => string.Join(",", row.numerosResultado.ToArray()))
                .Name("Número dos resultados");
            Map(m => m.premioQuadraPorJogador).Name("Valor da quadra por jogador");
            Map(m => m.premioQuinaPorJogador).Name("Valor da quina por jogador");
            Map(m => m.premioSenaPorJogador).Name("Valor da sena por jogador");
            Map(m => m.premioTotal).Name("Premio total");
            Map(m => m.qtdAcertadoresQuadra).Name("Quantidade de acertadores da quadra");
            Map(m => m.qtdAcertadoresQuina).Name("Quantidade de acertadores da quina");
            Map(m => m.qtdAcertadoresSena).Name("Quantidade de acertadores da sena");
        }


    }
}