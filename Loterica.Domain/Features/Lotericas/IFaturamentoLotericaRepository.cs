using Loterica.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Domain.Features.Lotericas
{
    public interface IFaturamentoLotericaRepository : IRepository<FaturamentoLoterica>
    {
        FaturamentoLoterica GetByConcursoId(int Id);
    }
}
