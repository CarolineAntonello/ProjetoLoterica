using Loterica.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Domain.Features.Concursos
{
    public class ExcludeConcursoException : BusinessException
    {
        public ExcludeConcursoException() : base("Não é possivel excluir um concurso com apostas!")
        {
        }
    }
}
