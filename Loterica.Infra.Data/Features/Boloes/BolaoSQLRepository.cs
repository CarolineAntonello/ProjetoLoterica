using Loterica.Domain;
using Loterica.Domain.Exceptions;
using Loterica.Domain.Features.Apostas;
using Loterica.Domain.Features.Boloes;
using Loterica.Infra.Data.Features.Apostas;
using Loterica.Infra.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Infra.Data.Features.Boloes
{
    public class BolaoSQLRepository : IBolaoRepository
    {
        private string _sqlAdd = @"INSERT INTO TBBolao(ValorTotalApostasBolao, ConcursoId) values (@ValorTotalApostasBolao, @ConcursoId)";

        private string _sqlAddApostaBolao = @"INSERT INTO TBApostaBolao(ApostaId, BolaoId) values (@ApostaId, @BolaoId)";
        
        private string _sqlGetById = @"SELECT b.Id,
                                              b.ValorTotalApostasBolao,
                                              b.ConcursoId,
                                              c.DataFechamento,
                                              c.ValorTotalApostas
                                              FROM TBBolao as b
                                              INNER JOIN TBConcurso as c ON c.Id = b.ConcursoId                                                
                                               where b.Id = @Id";

        private string _sqlGetAll = @"SELECT b.Id,
                                              b.ValorTotalApostasBolao,
                                              b.ConcursoId,
                                              c.DataFechamento,
                                              c.ValorTotalApostas
                                              FROM TBBolao as b
                                              INNER JOIN TBConcurso as c ON c.Id = b.ConcursoId";


        ApostaSQLRepository _apostaRepository;

        public BolaoSQLRepository()
        {
            _apostaRepository = new ApostaSQLRepository();
        }

        public Bolao Adicionar(Bolao entidade)
        {
            entidade.Id = Db.Insert(_sqlAdd, Take(entidade));


            foreach (var aposta in entidade.apostas)
            {
                Aposta apostaAdicionada =_apostaRepository.Adicionar(aposta);

                Dictionary<string, object> parms = new Dictionary<string, object>
                { {"ApostaId", apostaAdicionada.Id}, { "BolaoId", entidade.Id }};
                Db.Update(_sqlAddApostaBolao, parms);
            }
            
            return entidade;
        }

        public void Editar(Bolao entidade)
        {
            throw new UnsupportedOperationException();
        }

        public void Excluir(int Id)
        {
            throw new UnsupportedOperationException();
        }

        public List<Bolao> GetAll()
        {
            List<Bolao> boloes = Db.GetAll(_sqlGetAll, Make);
            foreach (var item in boloes)
            {
                item.apostas = _apostaRepository.GetByBolaoId(item.Id);
            }
            return boloes;
        }

        public Bolao GetById(int Id)
        {
            Dictionary<string, object> parms = new Dictionary<string, object> { { "Id", Id } };

            Bolao bolao = Db.Get(_sqlGetById, Make, parms);

            bolao.apostas = _apostaRepository.GetByBolaoId(bolao.Id);


            return bolao;
        }

        private Dictionary<string, object> Take(Bolao bolao)
        {

            return new Dictionary<string, object>
            {
                { "Id", bolao.Id },
                { "ValorTotalApostasBolao", bolao.valorTotalApostasBolao },
                { "ConcursoId", bolao.concurso.Id }

            };
        }

        private Bolao Make(IDataReader reader)
        {
            Bolao bolao = new Bolao();
            bolao.concurso = new Concurso();

            bolao.Id = Convert.ToInt32(reader["Id"]);
            bolao.valorTotalApostasBolao = Convert.ToDouble(reader["ValorTotalApostasBolao"]);
            bolao.concurso.Id = Convert.ToInt32(reader["ConcursoId"]);
            bolao.concurso.dataFechamento = Convert.ToDateTime(reader["DataFechamento"]);
            bolao.concurso.valorTotalApostas = Convert.ToDouble(reader["ValorTotalApostas"]);
            return bolao;
        }

    }
}
