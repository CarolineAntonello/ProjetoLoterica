using Loterica.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Domain.Features.Apostas
{
    public class Aposta : Entidade
    {
        public List<int> numerosApostados;
        public Concurso concurso;
        public const double valorAposta = 3.00;

        public Aposta(Concurso concursoRecebido)
        {
            numerosApostados = new List<int>();
            concurso = concursoRecebido;
            concurso.valorTotalApostas += valorAposta;
        }

        public override void Validar()
        {
            if (numerosApostados.Count != 6 || numerosApostados.Equals(null))
                throw new InvalidBetException();

          if(!ValidaNumerosApostados())
                throw new InvalidNumberException();
        }

        public int CalcularResultadoBilhete(List<int> numerosResultado)
        {
            int quantidadeAcertos = 0;
            foreach (var item in numerosApostados)
            {
                if (numerosResultado.Contains(item))
                {
                    quantidadeAcertos += 1;
                }  
            }

            return quantidadeAcertos;
        }

        public void GerarNumerosAposta()
        {
            Random random = new Random();

            for (int i = 1; i <= 6;)
            {
                var resultado = random.Next(1, 61);
                if (!numerosApostados.Contains(resultado))
                {
                    numerosApostados.Add(resultado);
                    i++;
                }
            }
        }

        private bool ValidaNumerosApostados()
        {
            foreach (var item in numerosApostados)
            {
                if (item > 60 || item < 1)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
