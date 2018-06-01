using Loterica.Domain;
using Loterica.Domain.Features.Apostas;
using Loterica.Domain.Features.ResultadoConcursos;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Loterica.Common.Tests.ResultadoConcursos
{
    [ExcludeFromCodeCoverage]
    public partial class ObjectMother
    {
        public static Random  random = new Random();
        public static ResultadoConcurso GetResultadoConcurso()
        {
            Concurso concurso = Loterica.Common.Tests.Concursos.ObjectMother.GetConcursoComId();
            ResultadoConcurso resultado = new ResultadoConcurso(concurso);
            resultado.CalculaNumeroResultado(random);
            resultado.CalcularPremioTotal();
            return resultado;
        }

        public static ResultadoConcurso GetResultadoSemConcurso()
        {
            Concurso concurso = Loterica.Common.Tests.Concursos.ObjectMother.GetConcurso();
            ResultadoConcurso resultado = new ResultadoConcurso(concurso);
            resultado.CalculaNumeroResultado(random);
            resultado.CalcularPremioTotal();
            return resultado;
        }

        public static ResultadoConcurso GetResultadoConcursoComId()
        {
            
            Concurso concurso = Loterica.Common.Tests.Concursos.ObjectMother.GetConcursoComId();
            ResultadoConcurso resultado = new ResultadoConcurso(concurso);
            resultado.Id = 1;
            resultado.CalculaNumeroResultado(random);
            resultado.CalcularPremioTotal();
            return resultado;
        }

        public static ResultadoConcurso GetResultadoConcursoComGanhadores()
        {
            List<Aposta> apostas = new List<Aposta>();
            apostas = Loterica.Common.Tests.Apostas.ObjectMother.Get3Apostas();
            Concurso concurso = Loterica.Common.Tests.Concursos.ObjectMother.GetConcursoComId();
            ResultadoConcurso resultado = new ResultadoConcurso(concurso);
            resultado.CalculaNumeroResultado(random);
            resultado.CalcularPremioTotal();

            foreach (var item in apostas)
            {
                resultado.AdicionarGanhador(item,4);
            }
            resultado.CalcularPremioPorAposta();
            return resultado;
        }

        public static ResultadoConcurso GetResultadoConcursoComGanhadoresComId()
        {
            List<Aposta> apostas = new List<Aposta>();
            apostas = Loterica.Common.Tests.Apostas.ObjectMother.Get3Apostas();
            Concurso concurso = Loterica.Common.Tests.Concursos.ObjectMother.GetConcursoComId();
            ResultadoConcurso resultado = new ResultadoConcurso(concurso);
            resultado.Id = 1;
            resultado.CalculaNumeroResultado(random);
            resultado.CalcularPremioTotal();

            foreach (var item in apostas)
            {
                resultado.AdicionarGanhador(item, 4);
            }
            resultado.CalcularPremioPorAposta();
            return resultado;
        }
    }
}
