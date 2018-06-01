using Loterica.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Domain.Features.Apostas
{
    public interface IApostaRepository : IRepository<Aposta>
    {
        List<Aposta> GetByConcursoId(int Id);

        List<Aposta> GetByConcursoIdComBolao(int Id);
    }
}
