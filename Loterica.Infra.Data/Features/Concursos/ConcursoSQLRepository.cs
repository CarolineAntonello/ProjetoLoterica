using Loterica.Domain;
using Loterica.Domain.Features.Concursos;
using Loterica.Infra.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Infra.Data.Features.Concursos
{
    public class ConcursoSQLRepository : IConcursoRepository
    {
        private string _sqlAdd = "insert into TBConcurso(DataFechamento, ValorTotalApostas) values (@DataFechamento, @ValorTotalApostas)";
        private string _sqlUpdate = "update TBConcurso set DataFechamento = @DataFechamento, ValorTotalApostas = @ValorTotalApostas where Id = @Id";
        private string _sqlGetById = "select *from TBConcurso where Id = @Id";
        private string _sqlDelete = "delete from TBConcurso where Id = @Id";
        private string _sqlGetAll = "select * from TBConcurso";

        public Concurso Adicionar(Concurso entidade)
        {
            entidade.Validar();
            entidade.Id = Db.Insert(_sqlAdd, Take(entidade));
            return entidade;
        }

        public void Editar(Concurso entidade)
        {
            Db.Update(_sqlUpdate, Take(entidade));
        }

        public void Excluir(int Id)
        {
            try
            {
                Dictionary<string, object> parms = new Dictionary<string, object> { { "Id", Id } };
                Db.Delete(_sqlDelete, parms);
            }
            catch (Exception)
            {

                throw new ExcludeConcursoException();
            }
            
        }

        public Concurso GetById(int Id)
        {
            Dictionary<string, object> parms = new Dictionary<string, object> { { "Id", Id } };
            return Db.Get(_sqlGetById, Make, parms);
        }

        public List<Concurso> GetAll()
        {
            return Db.GetAll(_sqlGetAll, Make);
        }

        private Dictionary<string, object> Take(Concurso concurso)
        {
            return new Dictionary<string, object>
            {
                { "Id", concurso.Id },
                { "DataFechamento", concurso.dataFechamento },
                { "ValorTotalApostas", concurso.valorTotalApostas }
            };
        }

        private Concurso Make(IDataReader reader)
        {
            Concurso concurso = new Concurso();
            concurso.Id = Convert.ToInt32(reader["Id"]);
            concurso.dataFechamento = Convert.ToDateTime(reader["DataFechamento"]);
            concurso.valorTotalApostas = Convert.ToDouble(reader["ValorTotalApostas"]);
            return concurso;
        }

    }
}
