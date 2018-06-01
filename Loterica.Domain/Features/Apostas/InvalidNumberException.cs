using Loterica.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Domain.Features.Apostas
{
    public class InvalidNumberException : BusinessException
    {
        public InvalidNumberException() : base("Números apostados inválidos!")
        {
        }
    }
}
