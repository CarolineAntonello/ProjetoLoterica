using Loterica.Domain.Abstract;
using System;
using System.Collections.Generic;
using Loterica.Domain.Features.Apostas;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Domain.Features.Boloes
{
    public class Bolao : Entidade
    {
        public Concurso concurso;
        public double valorTotalApostasBolao;
        public List<Aposta> apostas;

        public Bolao()
        {
            apostas = new List<Aposta>();
            concurso = new Concurso();
        }

        public override void Validar()
        {
            if (concurso.Id == 0)
                throw new InvalidBolaoConcursoException();

            if (apostas.Count == 0)
                throw new InvalidListApostaException();
        }

        public void CalculaValorDoBolao()
        {
            valorTotalApostasBolao = Math.Round(((apostas.Count * Aposta.valorAposta) * 1.05),2);
        }

        public void GerarApostas(int numeroApostas)
        {
            for (int i = 0; i < numeroApostas; i++)
            {
                var aposta = new Aposta(this.concurso);
                aposta.GerarNumerosAposta();
                apostas.Add(aposta);
            }
        }
    }
}
