using Loterica.Domain.Abstract;
using Loterica.Domain.Concursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Domain
{
    public class Concurso : Entidade
    {
        
        public DateTime dataFechamento;
        public double valorTotalApostas;

        public override void Validar()
        {
            if (dataFechamento < DateTime.Now)
                throw new InvalidDateTimeException();
        }
    }
}
