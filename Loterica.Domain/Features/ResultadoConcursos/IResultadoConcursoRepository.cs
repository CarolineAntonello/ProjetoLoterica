using Loterica.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Domain.Features.ResultadoConcursos
{
    public interface IResultadoConcursoRepository : IRepository<ResultadoConcurso>
    {
        ResultadoConcurso GetByConcursoId(int Id);

    }
}
