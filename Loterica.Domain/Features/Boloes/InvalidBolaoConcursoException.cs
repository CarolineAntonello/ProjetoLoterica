using Loterica.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Domain.Features.Boloes
{
    public class InvalidBolaoConcursoException : BusinessException
    {
        public InvalidBolaoConcursoException() : base("Não é possível criar um bolão sem concurso!")
        {
        }
    }
}
