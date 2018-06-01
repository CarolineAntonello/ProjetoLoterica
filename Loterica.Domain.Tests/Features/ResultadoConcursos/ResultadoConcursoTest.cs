using FluentAssertions;
using Loterica.Common.Tests.Apostas;
using Loterica.Domain.Features.Apostas;
using Loterica.Domain.Features.ResultadoConcursos;
using NUnit.Framework;
using System;
using System.Linq;

namespace Loterica.Domain.Tests.Features.ResultadoConcursos
{
    [TestFixture]
    public class ResultadoConcursoTest
    {
        ResultadoConcurso _resultadoConcurso;

        [Test]
        public void Domain_ResultadoConcurso_Nao_Deveria_Ter_Concurso_Vazio()
        {
            _resultadoConcurso = Loterica.Common.Tests.ResultadoConcursos.ObjectMother.GetResultadoSemConcurso();
            Action action = () => _resultadoConcurso.Validar();
            action.Should().Throw<InvalidConcursoException>();
        }

        [Test]
        public void Domain_ResultadoConcurso_Deveria_Calcular_Premio_Total()
        {
            _resultadoConcurso = Loterica.Common.Tests.ResultadoConcursos.ObjectMother.GetResultadoConcurso();
            _resultadoConcurso.CalcularPremioTotal();
            _resultadoConcurso.premioTotal.Should().Be(7200.9);
        }

        [Test]
        public void Domain_ResultadoConcurso_Deveria_Adicionar_Ganhador_4_Acertos()
        {
            Aposta aposta = ObjectMother.GetApostaComId();
            _resultadoConcurso = Loterica.Common.Tests.ResultadoConcursos.ObjectMother.GetResultadoConcurso();
            _resultadoConcurso.AdicionarGanhador(aposta, 4);
            _resultadoConcurso.ganhadores.Count.Should().Be(1);
            _resultadoConcurso.qtdAcertadoresQuadra.Should().Be(1);
        }

        [Test]
        public void Domain_ResultadoConcurso_Deveria_Adicionar_Ganhador_5_Acertos()
        {
            Aposta aposta = ObjectMother.GetApostaComId();
            _resultadoConcurso = Loterica.Common.Tests.ResultadoConcursos.ObjectMother.GetResultadoConcurso();
            _resultadoConcurso.AdicionarGanhador(aposta, 5);
            _resultadoConcurso.ganhadores.Count.Should().Be(1);
            _resultadoConcurso.qtdAcertadoresQuina.Should().Be(1);
        }

        [Test]
        public void Domain_ResultadoConcurso_Deveria_Adicionar_Ganhador_6_Acertos()
        {
            Aposta aposta = ObjectMother.GetApostaComId();
            _resultadoConcurso = Loterica.Common.Tests.ResultadoConcursos.ObjectMother.GetResultadoConcurso();
            _resultadoConcurso.AdicionarGanhador(aposta, 6);
            _resultadoConcurso.ganhadores.Count.Should().Be(1);
            _resultadoConcurso.qtdAcertadoresSena.Should().Be(1);
        }

        [Test]
        public void Domain_ResultadoConcurso_Deveria_Calcular_Premio_Aposta_4_5_6()
        {
            Aposta aposta = ObjectMother.GetApostaComId();
            _resultadoConcurso = Loterica.Common.Tests.ResultadoConcursos.ObjectMother.GetResultadoConcurso();
            _resultadoConcurso.premioTotal = 10000;
            _resultadoConcurso.qtdAcertadoresQuadra = 3;
            _resultadoConcurso.qtdAcertadoresQuina = 2;
            _resultadoConcurso.qtdAcertadoresSena = 1;
            _resultadoConcurso.CalcularPremioPorAposta();
            _resultadoConcurso.premioQuadraPorJogador.Should().Be(333.33);
            _resultadoConcurso.premioQuinaPorJogador.Should().Be(1000);
            _resultadoConcurso.premioSenaPorJogador.Should().Be(7000);
        }

        [Test]
        public void Domain_ResultadoConcurso_Deveria_Calcular_Premio_Aposta_4_5()
        {
            Aposta aposta = ObjectMother.GetApostaComId();
            _resultadoConcurso = Loterica.Common.Tests.ResultadoConcursos.ObjectMother.GetResultadoConcurso();
            _resultadoConcurso.premioTotal = 10000;
            _resultadoConcurso.qtdAcertadoresQuadra = 3;
            _resultadoConcurso.qtdAcertadoresQuina = 2;
            _resultadoConcurso.CalcularPremioPorAposta();
            _resultadoConcurso.premioQuadraPorJogador.Should().Be(333.33);
            _resultadoConcurso.premioQuinaPorJogador.Should().Be(1000);
            _resultadoConcurso.premioSenaPorJogador.Should().Be(0);
        }

        [Test]
        public void Domain_ResultadoConcurso_Deveria_Calcular_Premio_Aposta_4_6()
        {
            Aposta aposta = ObjectMother.GetApostaComId();
            _resultadoConcurso = Loterica.Common.Tests.ResultadoConcursos.ObjectMother.GetResultadoConcurso();
            _resultadoConcurso.premioTotal = 10000;
            _resultadoConcurso.qtdAcertadoresQuadra = 3;
            _resultadoConcurso.qtdAcertadoresSena = 1;
            _resultadoConcurso.CalcularPremioPorAposta();
            _resultadoConcurso.premioQuadraPorJogador.Should().Be(333.33);
            _resultadoConcurso.premioQuinaPorJogador.Should().Be(0);
            _resultadoConcurso.premioSenaPorJogador.Should().Be(9000);
        }

        [Test]
        public void Domain_ResultadoConcurso_Deveria_Calcular_Premio_Aposta_5_6()
        {
            Aposta aposta = ObjectMother.GetApostaComId();
            _resultadoConcurso = Loterica.Common.Tests.ResultadoConcursos.ObjectMother.GetResultadoConcurso();
            _resultadoConcurso.premioTotal = 10000;
            _resultadoConcurso.qtdAcertadoresQuina = 2;
            _resultadoConcurso.qtdAcertadoresSena = 1;
            _resultadoConcurso.CalcularPremioPorAposta();
            _resultadoConcurso.premioQuadraPorJogador.Should().Be(0);
            _resultadoConcurso.premioQuinaPorJogador.Should().Be(1250);
            _resultadoConcurso.premioSenaPorJogador.Should().Be(7500);
        }

        [Test]
        public void Domain_ResultadoConcurso_Deveria_Calcular_Premio_Aposta_6()
        {
            Aposta aposta = ObjectMother.GetApostaComId();
            _resultadoConcurso = Loterica.Common.Tests.ResultadoConcursos.ObjectMother.GetResultadoConcurso();
            _resultadoConcurso.premioTotal = 10000;
            _resultadoConcurso.qtdAcertadoresSena = 1;
            _resultadoConcurso.CalcularPremioPorAposta();
            _resultadoConcurso.premioQuadraPorJogador.Should().Be(0);
            _resultadoConcurso.premioQuinaPorJogador.Should().Be(0);
            _resultadoConcurso.premioSenaPorJogador.Should().Be(10000);
        }

        [Test]
        public void Domain_ResultadoConcurso_Deveria_Adicionar_Premio_Por_Ganhador()
        {
            Aposta aposta = ObjectMother.GetApostaComId();
            _resultadoConcurso = Loterica.Common.Tests.ResultadoConcursos.ObjectMother.GetResultadoConcurso();
            _resultadoConcurso.AdicionarGanhador(aposta, 3);
            _resultadoConcurso.AdicionarGanhador(aposta, 4);
            _resultadoConcurso.AdicionarGanhador(aposta, 5);
            _resultadoConcurso.AdicionarGanhador(aposta, 6);
            _resultadoConcurso.premioTotal = 10000;
            _resultadoConcurso.CalcularPremioPorAposta();
            _resultadoConcurso.AdicionarPremioPorGanhador();
            _resultadoConcurso.ganhadores.Count.Should().Be(3);
            _resultadoConcurso.ganhadores[0].valorPremio.Should().Be(1000);
            _resultadoConcurso.ganhadores[1].valorPremio.Should().Be(2000);
            _resultadoConcurso.ganhadores[2].valorPremio.Should().Be(7000);
        }

        [Test]
        public void Domain_ResultadoConcursoT_Deveria_Calcular_Numeros_Resultado()
        {
            Concurso concurso = Loterica.Common.Tests.Concursos.ObjectMother.GetConcursoComId();
            _resultadoConcurso = new ResultadoConcurso(concurso);
            _resultadoConcurso.numerosResultado.Count().Should().Be(6);
            foreach (var item in _resultadoConcurso.numerosResultado)
            {
                if (item > 60 || item < 1)
                {
                    Assert.Fail();
                }
            }
        }

        [Test]
        public void Domain_ResultadoConcurso_Deveria_LimparCamposGanhadores()
        {
            _resultadoConcurso = new ResultadoConcurso(new Concurso());
            _resultadoConcurso.AdicionarGanhador(new Aposta(new Concurso()), 4);
            _resultadoConcurso.AdicionarGanhador(new Aposta(new Concurso()), 5);
            _resultadoConcurso.AdicionarGanhador(new Aposta(new Concurso()), 6);
            _resultadoConcurso.qtdAcertadoresQuadra = 3;
            _resultadoConcurso.qtdAcertadoresQuina = 2;
            _resultadoConcurso.qtdAcertadoresSena = 1;
            _resultadoConcurso.LimparCamposGanhadores();
            _resultadoConcurso.ganhadores.Count.Should().Be(0);
            _resultadoConcurso.qtdAcertadoresQuadra.Should().Be(0);
            _resultadoConcurso.qtdAcertadoresQuina.Should().Be(0);
            _resultadoConcurso.qtdAcertadoresSena.Should().Be(0);
        }
    }
}
