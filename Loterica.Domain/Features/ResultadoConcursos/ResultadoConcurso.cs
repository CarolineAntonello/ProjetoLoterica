using Loterica.Domain.Abstract;
using Loterica.Domain.Features.Apostas;
using Loterica.Domain.Features.Ganhadores;
using Loterica.Domain.Features.Lotericas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Domain.Features.ResultadoConcursos
{
    public class ResultadoConcurso : Entidade
    {
        public Concurso concurso;
        public double premioTotal;
        public double premioQuadraPorJogador;
        public double premioQuinaPorJogador;
        public double premioSenaPorJogador;
        public List<Ganhador> ganhadores;
        public int qtdAcertadoresQuadra;
        public int qtdAcertadoresQuina;
        public int qtdAcertadoresSena;
        public List<int> numerosResultado;
        public FaturamentoLoterica faturamento;

        public ResultadoConcurso()
        {

        }

        public ResultadoConcurso(Concurso concurso)
        {
            this.concurso = concurso;
            faturamento = new FaturamentoLoterica(concurso);
            ganhadores = new List<Ganhador>();
            numerosResultado = new List<int>();
            CalculaNumeroResultado(new Random());
            CalcularPremioTotal();
        }

        public override void Validar()
        {
            if (concurso.Id == 0)
                throw new InvalidConcursoException();
        }

        public void CalculaNumeroResultado(Random random)
        {
            numerosResultado.Clear();

            for (int i = 1; i <= 6;)
            {
                var resultado = random.Next(1, 61);
                if (!numerosResultado.Contains(resultado))
                {
                    numerosResultado.Add(resultado);
                    i++;
                }
            }
        }

        public void CalcularPremioTotal()
        {
            premioTotal = Math.Round(((concurso.valorTotalApostas) * 0.90), 2);
        }

        public void AdicionarGanhador(Aposta aposta, int tipoPremio)
        {
            if (tipoPremio >= 4 && tipoPremio <= 6)
            {
                Ganhador ganhador = new Ganhador();
                ganhador.aposta = aposta;
                ganhador.tipoPremio = tipoPremio;
                ganhadores.Add(ganhador);
            }

            switch (tipoPremio)
            {
                case 4:
                    qtdAcertadoresQuadra += 1;
                    break;
                case 5:
                    qtdAcertadoresQuina += 1;
                    break;
                case 6:
                    qtdAcertadoresSena += 1;
                    break;
                default:
                    break;
            }
        }

        public void CalcularPremioPorAposta()
        {
            var percentQuadra = 0.10;
            var percentQuina = 0.20;
            var percentSena = 0.70;

            if (qtdAcertadoresQuadra <= 0)
            {
                percentQuina += 0.05;
                percentSena += 0.05;
                percentQuadra = 0;
            }
            if (qtdAcertadoresQuina <= 0)
            {
                percentSena += percentQuina;
                percentQuina = 0;
            }
            if (qtdAcertadoresSena <= 0)
            {
                percentSena = 0;
            }

            premioQuinaPorJogador = Math.Round((qtdAcertadoresQuina > 0 ? (premioTotal * percentQuina) / qtdAcertadoresQuina : 0), 2);
            premioQuadraPorJogador = Math.Round((qtdAcertadoresQuadra > 0 ? (premioTotal * percentQuadra) / qtdAcertadoresQuadra : 0), 2);
            premioSenaPorJogador = Math.Round((qtdAcertadoresSena > 0 ? (premioTotal * percentSena) / qtdAcertadoresSena : 0), 2);
        }

        public void AdicionarPremioPorGanhador()
        {
            foreach (var item in ganhadores)
            {
                if (item.tipoPremio == 4)
                    item.valorPremio = premioQuadraPorJogador;

                if (item.tipoPremio == 5)
                    item.valorPremio = premioQuinaPorJogador;

                if (item.tipoPremio == 6)
                    item.valorPremio = premioSenaPorJogador;
            }
        }

        public void LimparCamposGanhadores()
        {
            ganhadores.Clear();
            qtdAcertadoresQuadra = 0;
            qtdAcertadoresQuina = 0;
            qtdAcertadoresSena = 0;
        }
    }
}
