using Loterica.Infra.Database;
using System.Diagnostics.CodeAnalysis;

namespace Loterica.Common.Tests.Base
{
    [ExcludeFromCodeCoverage]
    public static class BaseSqlTest
    {
        private const string RECREATE_TBCONCURSO_TABLE = "DELETE FROM [dbo].[TBConcurso]" +
                                                    "DBCC CHECKIDENT ('TBConcurso', RESEED, 0)";

        private const string RECREATE_TBAPOSTA_TABLE = "DELETE FROM [dbo].[TBAposta]" +
                                                    "DBCC CHECKIDENT ('TBAposta', RESEED, 0)";

        private const string RECREATE_TBBOLAO_TABLE = "DELETE FROM [dbo].[TBBolao]" +
                                                            "DBCC CHECKIDENT ('TBBolao', RESEED, 0)";

        private const string RECREATE_TBAPOSTABOLAO_TABLE = "DELETE FROM [dbo].[TBApostaBolao]";

        private const string RECREATE_TBRESULTADOCONCURSO_TABLE = "DELETE FROM [dbo].[TBResultadoConcurso]" +
                                                            "DBCC CHECKIDENT ('TBResultadoConcurso', RESEED, 0)";

        private const string RECREATE_TBGANHADOR_TABLE = "DELETE FROM [dbo].[TBGanhador]";

        private const string RECREATE_TBFATURAMENTOLOTERICA_TABLE = "DELETE FROM [dbo].[TBFaturamentoLoterica]" +
                                                            "DBCC CHECKIDENT ('TBFaturamentoLoterica', RESEED, 0)";


        private const string INSERT_CONCURSO = "INSERT INTO TBConcurso(DataFechamento,ValorTotalApostas) VALUES (GETDATE(), 0), (GETDATE(), 0)";

        private const string INSERT_APOSTA = "INSERT INTO TBAposta(NumerosApostados, ConcursoId) VALUES ('1,2,3,4,5,6', 2)";

        private const string INSERT_BOLAO = "INSERT INTO TBBolao(ValorTotalApostasBolao, ConcursoId) values (30.00, 2)";

        private const string INSERT_APOSTABOLAO = "INSERT INTO TBApostaBolao(ApostaId, BolaoId) values (1, 1)";

        private const string INSERT_RESULTADOCONCURSO = @"INSERT INTO TBResultadoConcurso(PremioTotal,
                                                                                         PremioQuadraPorJogador,
                                                                                         PremioQuinaPorJogador,         
                                                                                         PremioSenaPorJogador,
                                                                                         QtdAcertadoresQuadra,
                                                                                         QtdAcertadoresQuina,
                                                                                         QtdAcertadoresSena,
                                                                                         NumerosResultado,
                                                                                         ConcursoId) 
                                                                                         values (
                                                                                         100000.00,
                                                                                         500.00,
                                                                                         1000.00,
                                                                                         7000.00,
                                                                                         2,
                                                                                         2,
                                                                                         1,
                                                                                         '1,2,3,4,5,6',
                                                                                         2)";

        private const string INSERT_GANHADOR = @"INSERT INTO TBGanhador
                                                            (ValorPremio,
                                                             TipoPremio,
                                                             ApostaId,         
                                                             ResultadoConcursoId)
                                                             values
                                                            (7000,
                                                             6,
                                                             1,
                                                             1)";

        private const string INSERT_FATURAMENTO = @"INSERT INTO TBFaturamentoLoterica
                                                            (Lucro,
                                                             Faturamento,
                                                             ConcursoId)
                                                             values
                                                            (3.30,
                                                             30.30,
                                                             2)";


        public static void SeedDatabase()
        {
            Db.Update(RECREATE_TBFATURAMENTOLOTERICA_TABLE);
            Db.Update(RECREATE_TBGANHADOR_TABLE);
            Db.Update(RECREATE_TBRESULTADOCONCURSO_TABLE);
            Db.Update(RECREATE_TBAPOSTABOLAO_TABLE);
            Db.Update(RECREATE_TBBOLAO_TABLE);
            Db.Update(RECREATE_TBAPOSTA_TABLE);
            Db.Update(RECREATE_TBCONCURSO_TABLE);
            
            Db.Update(INSERT_CONCURSO);
            Db.Update(INSERT_APOSTA);
            Db.Update(INSERT_BOLAO);
            Db.Update(INSERT_APOSTABOLAO);
            Db.Update(INSERT_RESULTADOCONCURSO);
            Db.Update(INSERT_GANHADOR);
            Db.Update(INSERT_FATURAMENTO);
        }

    }
}
