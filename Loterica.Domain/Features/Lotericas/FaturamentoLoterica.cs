using Loterica.Domain.Abstract;
using Loterica.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Domain.Features.Lotericas
{
    public class FaturamentoLoterica : Entidade
    {
        public Concurso concurso;
        public double lucro;
        public double faturamento;

        public FaturamentoLoterica(Concurso concursoRecebido)
        {
            concurso = concursoRecebido;
        }
       
        public override void Validar()
        {
            if (lucro < 0)
                throw new InvalidFaturamentoException();

            if(faturamento < 0)
                throw new InvalidFaturamentoException();

            if(faturamento < lucro)
                throw new InvalidFaturamentoException();
        }

        public void CalcularFaturamentoELucro(int numeroApostas, int numeroBoloes)
        {
            var totalApostas = numeroApostas * 3;
            var lucroaposta = totalApostas * 0.1;
            var totalBoloes = (numeroBoloes * 3) * 0.05;
            faturamento = Math.Round((totalApostas + totalBoloes),2);
            lucro = Math.Round((lucroaposta + totalBoloes),2);
        }
    }
}
