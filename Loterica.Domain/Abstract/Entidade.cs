using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Domain.Abstract
{
    public abstract class Entidade
    {
        public int Id;

        public abstract void Validar();
        
    }
}
