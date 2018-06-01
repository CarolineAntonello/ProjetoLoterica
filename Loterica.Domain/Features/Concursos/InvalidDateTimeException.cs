using Loterica.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Domain.Concursos
{
    public class InvalidDateTimeException : BusinessException
    {
        public InvalidDateTimeException() : base("valor da data inválido!")
        {
        }
    }
}
