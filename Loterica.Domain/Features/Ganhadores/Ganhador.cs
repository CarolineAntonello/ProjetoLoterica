using Loterica.Domain.Abstract;
using Loterica.Domain.Features.Apostas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Domain.Features.Ganhadores
{
    public class Ganhador 
    {
        public Aposta aposta;
        public int tipoPremio;
        public double valorPremio;
        public int ResultadoConcursoId;



    }
}
