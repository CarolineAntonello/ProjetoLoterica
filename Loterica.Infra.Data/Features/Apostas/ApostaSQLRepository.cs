using Loterica.Domain;
using Loterica.Domain.Exceptions;
using Loterica.Domain.Features.Apostas;
using Loterica.Domain.Features.Concursos;
using Loterica.Infra.Data.Features.Concursos;
using Loterica.Infra.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Infra.Data.Features.Apostas
{
    public class ApostaSQLRepository : IApostaRepository
    {
        private string _sqlAdd = @"insert into TBAposta(NumerosApostados, ConcursoId) values (@NumerosApostados, @ConcursoId)";
        //private string _sqlUpdate = @"update TBAposta set NumerosApostados = @NumerosApostados, ConcursoId = @ConcursoId where Id = @Id";
        private string _sqlGetById = @"SELECT a.Id,
                                              a.NumerosApostados,
                                              a.ConcursoId,
                                              c.DataFechamento,
                                              c.ValorTotalApostas
                                              FROM TBAposta as a
                                              INNER JOIN TBConcurso as c ON c.Id = a.ConcursoId                                                
                                               where a.Id = @Id";
        //private string _sqlDelete = @"delete from TBAposta where Id = @Id";
        private string _sqlGetAll = @"SELECT a.Id,
                                              a.NumerosApostados,
                                              a.ConcursoId,
                                              c.DataFechamento,
                                              c.ValorTotalApostas
                                              FROM TBAposta as a
                                              INNER JOIN TBConcurso as c ON c.Id = a.ConcursoId";
        private string _sqlGetByConcursoId = @"SELECT a.Id,
                                              a.NumerosApostados,
                                              a.ConcursoId,
                                              c.DataFechamento,
                                              c.ValorTotalApostas
                                              FROM TBAposta as a
                                              INNER JOIN TBConcurso as c ON c.Id = a.ConcursoId                                                
                                               where a.ConcursoId = @Id";

        private string _sqlGetByConcursoIdInBoloes = @"SELECT a.Id,
                                              a.NumerosApostados,
                                              a.ConcursoId,
                                              c.DataFechamento,
                                              c.ValorTotalApostas
                                              FROM TBAposta as a
                                              INNER JOIN TBConcurso as c ON c.Id = a.ConcursoId    
                                              INNER JOIN TBApostaBolao as l ON l.ApostaId = a.Id
                                               where a.ConcursoId = @Id";

        private string _sqlGetByBolaoId = @"SELECT a.Id,
                                              a.NumerosApostados,
                                              a.ConcursoId,
                                              c.DataFechamento,
                                              c.ValorTotalApostas
                                              FROM TBAposta as a
                                              INNER JOIN TBConcurso as c ON c.Id = a.ConcursoId   
                                              INNER JOIN TBApostaBolao as l ON l.ApostaId = a.Id
                                               where l.BolaoId = @Id";

        IConcursoRepository _concursoRepository;
        public ApostaSQLRepository()
        {
            _concursoRepository = new ConcursoSQLRepository();
        }
        public Aposta Adicionar(Aposta entidade)
        {
            entidade.Validar();
            entidade.Id = Db.Insert(_sqlAdd, Take(entidade));
            _concursoRepository.Editar(entidade.concurso);
            return entidade;
        }

        public void Editar(Aposta entidade)
        {
            //Db.Update(_sqlUpdate, Take(entidade));
            throw new UnsupportedOperationException();
        }

        public void Excluir(int Id)
        {
            throw new UnsupportedOperationException();
        }

        public List<Aposta> GetAll()
        {
            return Db.GetAll(_sqlGetAll, Make);
        }

        public Aposta GetById(int Id)
        {
            Dictionary<string, object> parms = new Dictionary<string, object> { { "Id", Id } };
            return Db.Get(_sqlGetById, Make, parms);
        }

        public List<Aposta> GetByConcursoId(int Id)
        {
            Dictionary<string, object> parms = new Dictionary<string, object> { { "Id", Id } };
            return Db.GetAll(_sqlGetByConcursoId, Make, parms);
        }

        public List<Aposta> GetByConcursoIdComBolao(int Id)
        {
            Dictionary<string, object> parms = new Dictionary<string, object> { { "Id", Id } };
            return Db.GetAll(_sqlGetByConcursoIdInBoloes, Make, parms);
        }

        public List<Aposta> GetByBolaoId(int Id)
        {
            Dictionary<string, object> parms = new Dictionary<string, object> { { "Id", Id } };
            return Db.GetAll(_sqlGetByBolaoId, Make, parms);
        }


        private Dictionary<string, object> Take(Aposta aposta)
        {
            return new Dictionary<string, object>
            {
                { "Id", aposta.Id },
                { "NumerosApostados", ConverterNumerosApostadosParaString(aposta.numerosApostados) },
                { "ConcursoId", aposta.concurso.Id }
            };
        }

        private Aposta Make(IDataReader reader)
        {
            Aposta aposta = new Aposta(new Concurso());
            aposta.Id = Convert.ToInt32(reader["Id"]);
            aposta.numerosApostados = ConverterStringParaNumerosApostados(Convert.ToString(reader["NumerosApostados"]));
            aposta.concurso.Id = Convert.ToInt32(reader["ConcursoId"]);
            aposta.concurso.dataFechamento = Convert.ToDateTime(reader["DataFechamento"]);
            aposta.concurso.valorTotalApostas = Convert.ToDouble(reader["ValorTotalApostas"]);
            return aposta;
        }

        public string ConverterNumerosApostadosParaString(List<int> numerosAposta)
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

        public List<int> ConverterStringParaNumerosApostados(string dados)
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
