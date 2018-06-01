using Loterica.Aplication.Abstract;
using Loterica.Aplication.Features.Lotericas;
using Loterica.Domain;
using Loterica.Domain.Abstract;
using Loterica.Domain.Features.Apostas;
using Loterica.Domain.Features.Lotericas;
using Loterica.Domain.Features.ResultadoConcursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Aplication.Features.ResultadoConcursos
{
    public class ResultadoConcursoService : Service<ResultadoConcurso>
    {
        IResultadoConcursoRepository _resultadoCouncursoRepository;
        IFaturamentoLotericaRepository _faturamentoLotericaRepository;
        IApostaRepository _apostaRepository;
        //public ResultadoConcurso _resultadoConcurso;
        public FaturamentoLotericaService _faturamentoLoterica;

        
        public ResultadoConcursoService(IResultadoConcursoRepository resultadoCouncursoRepository, IApostaRepository apostaRepository, IFaturamentoLotericaRepository faturamentoLotericaRepository) : base(resultadoCouncursoRepository)
        {
            _resultadoCouncursoRepository = resultadoCouncursoRepository;
            _apostaRepository = apostaRepository;
            _faturamentoLotericaRepository = faturamentoLotericaRepository;
            
        }

        public ResultadoConcurso GerarResultadoConcurso(ResultadoConcurso resultadoConcurso, Random random)
        {
            resultadoConcurso.LimparCamposGanhadores();
            resultadoConcurso.CalculaNumeroResultado(random);
            List<Aposta> apostasDoConcurso = new List<Aposta>();
            apostasDoConcurso = _apostaRepository.GetByConcursoId(resultadoConcurso.concurso.Id);
            foreach (var aposta in apostasDoConcurso)
            {
                var resultadoDaAposta = aposta.CalcularResultadoBilhete(resultadoConcurso.numerosResultado);
                if (resultadoDaAposta >=4)
                {
                    resultadoConcurso.AdicionarGanhador(aposta, resultadoDaAposta);
                }
            }
            resultadoConcurso.AdicionarPremioPorGanhador();
            List<Aposta> apostasDoConcursoDeBoloes = new List<Aposta>();
            apostasDoConcursoDeBoloes = _apostaRepository.GetByConcursoIdComBolao(resultadoConcurso.concurso.Id);
            resultadoConcurso.faturamento.CalcularFaturamentoELucro(apostasDoConcurso.Count, apostasDoConcursoDeBoloes.Count);
            return resultadoConcurso;
        }

        public ResultadoConcurso BuscarPorConcursoId(int Id)
        {
            return _resultadoCouncursoRepository.GetByConcursoId(Id);
        }
    }
}
