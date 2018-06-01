using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Infra.CSV
{
    public class InvalidElementException : Exception
    {
        public InvalidElementException() : base("Não existe nada para exportar!")
        {
        }
    }
}
