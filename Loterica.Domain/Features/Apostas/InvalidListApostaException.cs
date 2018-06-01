using Loterica.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Domain.Features.Apostas
{
    public class InvalidListApostaException : BusinessException
    {
        public InvalidListApostaException() : base("Lista de apostas para o bolão inválidas!")
        {
        }
    }
}
