using Loterica.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;


namespace Loterica.Common.Tests.Concursos
{
    [ExcludeFromCodeCoverage]
    public partial class ObjectMother
    {
        public static Concurso GetConcurso()
        {
            return new Concurso
            {
                dataFechamento = DateTime.Now.AddDays(2),
            };
        }

        public static Concurso GetConcursoHoraMenorAtual()
        {
            return new Concurso
            {
                dataFechamento = DateTime.Now.AddHours(-2),
            };
        }

        public static Concurso GetConcursoComId()
        {
            return new Concurso
            {
                Id = 1,
                dataFechamento = DateTime.Now.AddHours(2),
                valorTotalApostas = 8001,
            };
        }

        public static Concurso GetConcursoSemApostas()
        {
            return new Concurso
            {
                Id = 1,
                dataFechamento = DateTime.Now.AddHours(2),
                valorTotalApostas = 0,
            };
        }

        public static List<Concurso> GetConcursos()
        {
            List<Concurso> concursos = new List<Concurso>()
            {
                new Concurso
                {
                    dataFechamento = DateTime.Now.AddDays(2),
                },
                new Concurso
                {
                      dataFechamento = DateTime.Now.AddDays(5),
                },
            };
            return concursos;
        }
    }
}
