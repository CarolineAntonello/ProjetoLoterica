using Loterica.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Domain.Features.Lotericas
{
    public class InvalidFaturamentoException : BusinessException
    {
        public InvalidFaturamentoException() : base("Dados de faturamento inválidos!")
        {
        }
    }
}
