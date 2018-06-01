using Loterica.Aplication.Abstract;
using Loterica.Aplication.Features.Concursos;
using Loterica.Aplication.Features.ResultadoConcursos;
using Loterica.Domain.Abstract;
using Loterica.Domain.Features.Apostas;
using Loterica.Domain.Features.Lotericas;
using Loterica.Domain.Features.ResultadoConcursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Aplication.Features.Apostas
{
    public class ApostaService : Service<Aposta>
    {
        public IApostaRepository _apostaRepository;
        public IResultadoConcursoRepository _resultadoConcursoRepository;
        public IFaturamentoLotericaRepository _faturamentoLotericaRepository;
        public ResultadoConcursoService _resultadoConcursoService;
        public ApostaService(IApostaRepository apostaRepository, IResultadoConcursoRepository resultadoConcursoRepository, IFaturamentoLotericaRepository faturamentoLotericaRepository) : base(apostaRepository)
        {
            _apostaRepository = apostaRepository;
            _resultadoConcursoRepository = resultadoConcursoRepository;
            _faturamentoLotericaRepository = faturamentoLotericaRepository;
            _resultadoConcursoService = new ResultadoConcursoService(_resultadoConcursoRepository, _apostaRepository, _faturamentoLotericaRepository);
        }

        public void GerarApostaAutomaticamente(Aposta aposta)
        {
            aposta.GerarNumerosAposta();
        }

        public int CalcularResultadoFinal(Aposta aposta)
        {
            ResultadoConcurso resultado = _resultadoConcursoService.BuscarPorConcursoId(aposta.concurso.Id);
            return aposta.CalcularResultadoBilhete(resultado.numerosResultado);
        }
    }
}
