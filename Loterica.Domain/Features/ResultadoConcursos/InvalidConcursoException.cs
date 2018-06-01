using Loterica.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Domain.Features.ResultadoConcursos
{
    public class InvalidConcursoException : BusinessException
    {
        public InvalidConcursoException() : base("É preciso de um concurso!")
        {
        }
    }
}
