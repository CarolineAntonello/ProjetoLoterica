using Loterica.Domain;
using Loterica.Domain.Exceptions;
using Loterica.Domain.Features.Lotericas;
using Loterica.Infra.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Infra.Data.Features.Lotericas
{
    public class FaturamentoLotericaSQLRepository : IFaturamentoLotericaRepository
    {
        #region SQLQuery
        private string _sqlAdd = @"INSERT INTO TBFaturamentoLoterica
                                    (Lucro,
                                     Faturamento,
                                     ConcursoId)
                                     values
                                    (@Lucro,
                                     @Faturamento,
                                     @ConcursoId)";

        private string _sqlUpdate = @"UPDATE TBFaturamentoLoterica SET Lucro = @Lucro,
                                                                       Faturamento = @Faturamento,
                                                                       ConcursoId = @ConcursoId         
                                                                       WHERE Id = @Id";

        private string _sqlGet = @"SELECT f.Id,
                                        f.Lucro,
                                        f.Faturamento,
                                        f.ConcursoId,
                                        c.DataFechamento,
                                        c.ValorTotalApostas
                                        FROM TBFaturamentoLoterica as f
                                        INNER JOIN TBConcurso as c ON c.Id = f.ConcursoId                                                
                                        WHERE f.Id = @Id";

        private string _sqlGetByConcursoId = @"SELECT f.Id,
                                                        f.Lucro,
                                                        f.Faturamento,
                                                        f.ConcursoId,
                                                        c.DataFechamento,
                                                        c.ValorTotalApostas
                                                        FROM TBFaturamentoLoterica as f
                                                        INNER JOIN TBConcurso as c ON c.Id = f.ConcursoId                                                
                                                        WHERE f.ConcursoId = @Id";

        private string _sqlGetAll = @"SELECT f.Id,
                                        f.Lucro,
                                        f.Faturamento,
                                        f.ConcursoId,
                                        c.DataFechamento,
                                        c.ValorTotalApostas
                                        FROM TBFaturamentoLoterica as f
                                        INNER JOIN TBConcurso as c ON c.Id = f.ConcursoId";

        #endregion

        public FaturamentoLoterica Adicionar(FaturamentoLoterica entidade)
        {
            entidade.Validar();
           entidade.Id = Db.Insert(_sqlAdd, Take(entidade));
            return entidade;
        }

        public void Editar(FaturamentoLoterica entidade)
        {
            entidade.Validar();
            Db.Update(_sqlUpdate, Take(entidade));
        }

        public void Excluir(int Id)
        {
            throw new UnsupportedOperationException();
        }

        public List<FaturamentoLoterica> GetAll()
        {
            List<FaturamentoLoterica> faturamentos = Db.GetAll(_sqlGetAll, Make);
            return faturamentos;
        }

        public FaturamentoLoterica GetByConcursoId(int Id)
        {
            Dictionary<string, object> parms = new Dictionary<string, object> { { "Id", Id } };

            FaturamentoLoterica faturamento = Db.Get(_sqlGetByConcursoId, Make, parms);
            return faturamento;
        }

        public FaturamentoLoterica GetById(int Id)
        {
            Dictionary<string, object> parms = new Dictionary<string, object> { { "Id", Id } };

            FaturamentoLoterica faturamento = Db.Get(_sqlGet, Make, parms);
            return faturamento;
        }

        private Dictionary<string, object> Take(FaturamentoLoterica faturamento)
        {
            return new Dictionary<string, object>
            {
                { "Id", faturamento.Id },
                { "Lucro", faturamento.lucro },
                { "Faturamento", faturamento.faturamento },
                { "ConcursoId", faturamento.concurso.Id }
            };
        }

        private FaturamentoLoterica Make(IDataReader reader)
        {
            Concurso concurso = new Concurso();
            FaturamentoLoterica faturamento = new FaturamentoLoterica(concurso);
            
            faturamento.Id = Convert.ToInt32(reader["Id"]);
            faturamento.lucro = Convert.ToDouble(reader["Lucro"]);
            faturamento.faturamento = Convert.ToDouble(reader["Faturamento"]);
            faturamento.concurso.Id = Convert.ToInt32(reader["ConcursoId"]);
            faturamento.concurso.dataFechamento = Convert.ToDateTime(reader["DataFechamento"]);
            faturamento.concurso.valorTotalApostas = Convert.ToDouble(reader["ValorTotalApostas"]);

            return faturamento;
        }
    }
}
