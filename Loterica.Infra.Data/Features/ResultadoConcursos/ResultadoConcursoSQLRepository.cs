using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loterica.Domain;
using Loterica.Domain.Exceptions;
using Loterica.Domain.Features.Ganhadores;
using Loterica.Domain.Features.Lotericas;
using Loterica.Domain.Features.ResultadoConcursos;
using Loterica.Infra.Data.Features.Lotericas;
using Loterica.Infra.Database;

namespace Loterica.Infra.Data.Features.ResultadoConcursos
{
    public class ResultadoConcursoSQLRepository : IResultadoConcursoRepository
    {
        #region SQLQuery
        private string _sqlAddResultadoConcurso = @"INSERT INTO TBResultadoConcurso
                                    (PremioTotal,
                                     PremioQuadraPorJogador,
                                     PremioQuinaPorJogador,         
                                     PremioSenaPorJogador,
                                     QtdAcertadoresQuadra,
                                     QtdAcertadoresQuina,
                                     QtdAcertadoresSena,
                                     NumerosResultado,
                                     ConcursoId)
                                     values
                                    (@PremioTotal,
                                     @PremioQuadraPorJogador,
                                     @PremioQuinaPorJogador,
                                     @PremioSenaPorJogador,
                                     @QtdAcertadoresQuadra,
                                     @QtdAcertadoresQuina,
                                     @QtdAcertadoresSena,
                                     @NumerosResultado,
                                     @ConcursoId)";

        private string _sqlUpdateResultadoConcurso = @"UPDATE TBResultadoConcurso SET PremioTotal = @PremioTotal,
                                                                                         PremioQuadraPorJogador = @PremioQuadraPorJogador,
                                                                                         PremioQuinaPorJogador = @PremioQuinaPorJogador,         
                                                                                         PremioSenaPorJogador = @PremioSenaPorJogador,
                                                                                         QtdAcertadoresQuadra = @QtdAcertadoresQuadra,
                                                                                         QtdAcertadoresQuina = @QtdAcertadoresQuina,
                                                                                         QtdAcertadoresSena = @QtdAcertadoresSena,
                                                                                         NumerosResultado = @NumerosResultado,
                                                                                         ConcursoId = @ConcursoId
                                                                                         WHERE Id = @Id";



        private string _sqlGetResultadoConcursoById = @"SELECT r.Id,
                                                        r.PremioTotal,
                                                        r.PremioQuadraPorJogador,
                                                        r.PremioQuinaPorJogador,
                                                        r.PremioSenaPorJogador,
                                                        r.QtdAcertadoresQuadra,
                                                        r.QtdAcertadoresQuina,
                                                        r.QtdAcertadoresSena,
                                                        r.NumerosResultado,
                                                        r.ConcursoId,
                                                        c.DataFechamento,
                                                        c.ValorTotalApostas
                                                        FROM TBResultadoConcurso as r
                                                        INNER JOIN TBConcurso as c ON c.Id = r.ConcursoId                                                
                                                        WHERE r.Id = @Id";

        private string _sqlGetResultadoConcursoByConcursoId = @"SELECT r.Id,
                                                                r.PremioTotal,
                                                                r.PremioQuadraPorJogador,
                                                                r.PremioQuinaPorJogador,
                                                                r.PremioSenaPorJogador,
                                                                r.QtdAcertadoresQuadra,
                                                                r.QtdAcertadoresQuina,
                                                                r.QtdAcertadoresSena,
                                                                r.NumerosResultado,
                                                                r.ConcursoId,
                                                                c.DataFechamento,
                                                                c.ValorTotalApostas
                                                                FROM TBResultadoConcurso as r
                                                                INNER JOIN TBConcurso as c ON c.Id = r.ConcursoId                                                
                                                                WHERE r.ConcursoId = @Id";

        private string _sqlGetAllResultadoConcurso = @"SELECT r.Id,
                                                        r.PremioTotal,
                                                        r.PremioQuadraPorJogador,
                                                        r.PremioQuinaPorJogador,
                                                        r.PremioSenaPorJogador,
                                                        r.QtdAcertadoresQuadra,
                                                        r.QtdAcertadoresQuina,
                                                        r.QtdAcertadoresSena,
                                                        r.NumerosResultado,
                                                        r.ConcursoId,
                                                        c.DataFechamento,
                                                        c.ValorTotalApostas
                                                        FROM TBResultadoConcurso as r
                                                        INNER JOIN TBConcurso as c ON c.Id = r.ConcursoId";

        private string _sqlAddGanhador = @"INSERT INTO TBGanhador
                                    (ValorPremio,
                                     TipoPremio,
                                     ApostaId,         
                                     ResultadoConcursoId)
                                     values
                                    (@ValorPremio,
                                     @TipoPremio,
                                     @ApostaId,
                                     @ResultadoConcursoId)";

        private string _sqlDeleteGanhadorByResultadoConcursoId = @"DELETE FROM TBGanhador WHERE ResultadoConcursoId = @Id";

        private string _sqlGetGanhadorByResultadoConcursoId = @"SELECT g.ValorPremio,
                                                                g.TipoPremio,
                                                                g.ValorPremio,
                                                                g.ResultadoConcursoId,
                                                                g.ApostaId,
                                                                a.NumerosApostados,
                                                                a.ConcursoId,
                                                                c.DataFechamento,
                                                                c.ValorTotalApostas
                                                                FROM TBGanhador as g
                                                                INNER JOIN TBAposta as a ON a.Id = g.ApostaId
                                                                INNER JOIN TBConcurso as c ON c.Id = a.ConcursoId                                                
                                                                where g.ResultadoConcursoId = @Id";
        #endregion

        IFaturamentoLotericaRepository _faturamentoLotericaRepository;
        public ResultadoConcursoSQLRepository()
        {
            _faturamentoLotericaRepository = new FaturamentoLotericaSQLRepository();
        }

        public ResultadoConcurso Adicionar(ResultadoConcurso entidade)
        {
            entidade.Validar();
            entidade.Id = Db.Insert(_sqlAddResultadoConcurso, TakeResultadoConcurso(entidade));

            foreach (var item in entidade.ganhadores)
            {
                item.ResultadoConcursoId = entidade.Id;
                Db.Update(_sqlAddGanhador, TakeGanhador(item));
            }

            entidade.faturamento = _faturamentoLotericaRepository.Adicionar(entidade.faturamento);

            return entidade;
        }

        public void Editar(ResultadoConcurso entidade)
        {
            entidade.Validar();

            Dictionary<string, object> parms = new Dictionary<string, object> { { "Id", entidade.Id } };
            Db.Delete(_sqlDeleteGanhadorByResultadoConcursoId, parms);
            Db.Update(_sqlUpdateResultadoConcurso, TakeResultadoConcurso(entidade));

            foreach (var item in entidade.ganhadores)
            {
                item.ResultadoConcursoId = entidade.Id;
                Db.Update(_sqlAddGanhador, TakeGanhador(item));
            }

            _faturamentoLotericaRepository.Editar(entidade.faturamento);
        }

        public void Excluir(int Id)
        {
            throw new UnsupportedOperationException();
        }

        public List<ResultadoConcurso> GetAll()
        {
            List<ResultadoConcurso> bolresultadosConcurso = Db.GetAll(_sqlGetAllResultadoConcurso, MakeResultadoConcurso);
            foreach (var item in bolresultadosConcurso)
            {
                Dictionary<string, object> parms = new Dictionary<string, object> { { "Id", item.Id } };
                item.ganhadores = Db.GetAll(_sqlGetGanhadorByResultadoConcursoId, MakeGanhador, parms);
                item.faturamento = _faturamentoLotericaRepository.GetByConcursoId(item.concurso.Id);
            }


            return bolresultadosConcurso;
        }

        public ResultadoConcurso GetById(int Id)
        {
            Dictionary<string, object> parms = new Dictionary<string, object> { { "Id", Id } };

            ResultadoConcurso resultadoConcurso = Db.Get(_sqlGetResultadoConcursoById, MakeResultadoConcurso, parms);
            if (resultadoConcurso != null)
            {
                resultadoConcurso.ganhadores = Db.GetAll(_sqlGetGanhadorByResultadoConcursoId, MakeGanhador, parms);

                resultadoConcurso.faturamento = _faturamentoLotericaRepository.GetByConcursoId(resultadoConcurso.concurso.Id);
            }
            return resultadoConcurso;
        }

        public ResultadoConcurso GetByConcursoId(int Id)
        {
            Dictionary<string, object> parms = new Dictionary<string, object> { { "Id", Id } };

            ResultadoConcurso resultadoConcurso = Db.Get(_sqlGetResultadoConcursoByConcursoId, MakeResultadoConcurso, parms);
            if (resultadoConcurso != null)
            {
                Dictionary<string, object> parmsResultadoConcurso = new Dictionary<string, object> { { "Id", resultadoConcurso.Id } };
                resultadoConcurso.ganhadores = Db.GetAll(_sqlGetGanhadorByResultadoConcursoId, MakeGanhador, parmsResultadoConcurso);

                resultadoConcurso.faturamento = _faturamentoLotericaRepository.GetByConcursoId(Id);
            }



            return resultadoConcurso;

        }

        private Dictionary<string, object> TakeResultadoConcurso(ResultadoConcurso resultadoConcurso)
        {
            return new Dictionary<string, object>
            {
                { "Id", resultadoConcurso.Id },
                { "PremioTotal", resultadoConcurso.premioTotal },
                { "PremioQuadraPorJogador", resultadoConcurso.premioQuadraPorJogador },
                { "PremioQuinaPorJogador", resultadoConcurso.premioQuinaPorJogador },
                { "PremioSenaPorJogador", resultadoConcurso.premioSenaPorJogador },
                { "QtdAcertadoresQuadra", resultadoConcurso.qtdAcertadoresQuadra },
                { "QtdAcertadoresQuina", resultadoConcurso.qtdAcertadoresQuina },
                { "QtdAcertadoresSena", resultadoConcurso.qtdAcertadoresSena },
                { "NumerosResultado", ConverterNumerosResultadoParaString(resultadoConcurso.numerosResultado) },
                { "ConcursoId", resultadoConcurso.concurso.Id }
            };
        }

        private ResultadoConcurso MakeResultadoConcurso(IDataReader reader)
        {
            ResultadoConcurso resultadoConcurso = new ResultadoConcurso();
            resultadoConcurso.concurso = new Concurso();
            resultadoConcurso.ganhadores = new List<Ganhador>();

            resultadoConcurso.Id = Convert.ToInt32(reader["Id"]);
            resultadoConcurso.premioTotal = Convert.ToDouble(reader["PremioTotal"]);
            resultadoConcurso.premioQuadraPorJogador = Convert.ToDouble(reader["PremioQuadraPorJogador"]);
            resultadoConcurso.premioQuinaPorJogador = Convert.ToDouble(reader["PremioQuinaPorJogador"]);
            resultadoConcurso.premioSenaPorJogador = Convert.ToDouble(reader["PremioSenaPorJogador"]);
            resultadoConcurso.qtdAcertadoresQuadra = Convert.ToInt32(reader["QtdAcertadoresQuadra"]);
            resultadoConcurso.qtdAcertadoresQuina = Convert.ToInt32(reader["QtdAcertadoresQuina"]);
            resultadoConcurso.qtdAcertadoresSena = Convert.ToInt32(reader["QtdAcertadoresSena"]);
            resultadoConcurso.numerosResultado = ConverterStringParaNumerosResultado(Convert.ToString(reader["NumerosResultado"]));
            resultadoConcurso.concurso.Id = Convert.ToInt32(reader["ConcursoId"]);
            resultadoConcurso.concurso.dataFechamento = Convert.ToDateTime(reader["DataFechamento"]);
            resultadoConcurso.concurso.valorTotalApostas = Convert.ToDouble(reader["ValorTotalApostas"]);

            return resultadoConcurso;
        }

        private Dictionary<string, object> TakeGanhador(Ganhador ganhador)
        {
            return new Dictionary<string, object>
            {
                { "ValorPremio", ganhador.valorPremio },
                { "TipoPremio", ganhador.tipoPremio },
                { "ApostaId", ganhador.aposta.Id },
                { "ResultadoConcursoId", ganhador.ResultadoConcursoId },
            };
        }

        private Ganhador MakeGanhador(IDataReader reader)
        {
            Ganhador ganhador = new Ganhador();
            ganhador.aposta = new Domain.Features.Apostas.Aposta(new Concurso());

            ganhador.valorPremio = Convert.ToDouble(reader["ValorPremio"]);
            ganhador.tipoPremio = Convert.ToInt32(reader["TipoPremio"]);
            ganhador.ResultadoConcursoId = Convert.ToInt32(reader["ResultadoConcursoId"]);
            ganhador.aposta.Id = Convert.ToInt32(reader["ApostaId"]);
            ganhador.aposta.numerosApostados = ConverterStringParaNumerosResultado(Convert.ToString(reader["NumerosApostados"]));
            ganhador.aposta.concurso.Id = Convert.ToInt32(reader["ConcursoId"]);
            ganhador.aposta.concurso.dataFechamento = Convert.ToDateTime(reader["DataFechamento"]);
            ganhador.aposta.concurso.valorTotalApostas = Convert.ToDouble(reader["ValorTotalApostas"]);

            return ganhador;
        }

        public string ConverterNumerosResultadoParaString(List<int> numerosAposta)
        {
            string numeros = string.Empty;
            foreach (var item in numerosAposta)
            {
                if (numeros == string.Empty)
                    numeros = item.ToString();
                else
                    numeros = numeros + "," + item.ToString();
            }
            return numeros;
        }

        public List<int> ConverterStringParaNumerosResultado(string dados)
        {
            var numeros = dados.Split(',');
            List<int> numerosAposta = new List<int>();
            foreach (var item in numeros)
            {
                numerosAposta.Add(Convert.ToInt32(item));
            }
            return numerosAposta;
        }
    }
}
