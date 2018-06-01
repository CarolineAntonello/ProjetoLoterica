using Loterica.Aplication.Abstract;
using Loterica.Domain.Abstract;
using Loterica.Domain.Features.Lotericas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Aplication.Features.Lotericas
{
    public class FaturamentoLotericaService : Service<FaturamentoLoterica>
    {
        IFaturamentoLotericaRepository _repository;
        public FaturamentoLotericaService(IFaturamentoLotericaRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public FaturamentoLoterica GetByConcursoId(int Id)
        {
            return _repository.GetByConcursoId(Id);
        }

    }
}
