using Loterica.Aplication.Abstract;
using Loterica.Domain;
using Loterica.Domain.Features.Concursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Aplication.Features.Concursos
{
    public class ConcursoService : Service<Concurso>
    {
        public IConcursoRepository _repository;

        public ConcursoService(IConcursoRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
