using Loterica.Domain;
using Loterica.Domain.Features.Apostas;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Loterica.Common.Tests.Apostas
{
    [ExcludeFromCodeCoverage]
    public partial class ObjectMother
    {
        public static Aposta GetAposta()
        {
            return new Aposta(new Concurso
            {
                Id = 1,
                dataFechamento = DateTime.Now.AddHours(2),
                valorTotalApostas = 8001,
            })
            {
                numerosApostados = new List<int> { 1, 2, 3, 4, 5, 6 },
            };
        }

        public static Aposta GetApostaComId()
        {
            return new Aposta(new Concurso
            {
                Id = 1,
                dataFechamento = DateTime.Now.AddHours(2),
                valorTotalApostas = 8001,
            })
            {
                Id = 1,
                numerosApostados = new List<int> { 1, 2, 3, 4, 5, 6 },
            };
        }

        public static Aposta GetApostaSeteNumerosApostados()
        {
            return new Aposta(new Concurso
            {
                Id = 1,
                dataFechamento = DateTime.Now.AddHours(2),
                valorTotalApostas = 8001,
            })
            {
                numerosApostados = new List<int> { 1, 2, 3, 4, 5, 6, 7 },
            };
        }

        public static Aposta GetApostaSemNumerosApostados()
        {
            return new Aposta(new Concurso
            {
                Id = 1,
                dataFechamento = DateTime.Now.AddHours(2),
                valorTotalApostas = 8001,
            })
            {
               numerosApostados = new List<int> { },
            };
        }

        public static Aposta GetApostaNumerosMaioresQue60()
        {
            return new Aposta(new Concurso
            {
                Id = 1,
                dataFechamento = DateTime.Now.AddHours(2),
                valorTotalApostas = 8001,
            })
            {
              numerosApostados = new List<int> { 1, 2, 3, 4, 5, 65 },
            };
        }

        public static Aposta GetApostaNumerosMenoresQue1()
        {
            return new Aposta(new Concurso
            {
                Id = 1,
                dataFechamento = DateTime.Now.AddHours(2),
                valorTotalApostas = 8001,
            })
            {
               numerosApostados = new List<int> { 1, 2, 3, 4, 5, 0 },
            };
        }

        public static List<Aposta> GetApostas()
        {
            return new List<Aposta>
            {
                  new Aposta(new Concurso
                      {
                          Id = 1,
                          dataFechamento = DateTime.Now.AddHours(2),
                          valorTotalApostas = 8001,
                      })
                  {
                      Id = 1,
                      numerosApostados = new List<int> { 1, 2, 3, 4, 5, 6 },

                  },

                    new Aposta(new Concurso
                      {
                          Id = 1,
                          dataFechamento = DateTime.Now.AddHours(2),
                          valorTotalApostas = 8001,
                      })
                  {
                      Id = 2,
                      numerosApostados = new List<int> { 1, 2, 3, 4, 5, 6 },
                  },
            };
        }

        public static List<Aposta> Get3Apostas()
        {
            return new List<Aposta>
            {
                 new Aposta(new Concurso
                      {
                          Id = 1,
                          dataFechamento = DateTime.Now.AddHours(2),
                          valorTotalApostas = 8001,
                      })
                  {
                      Id = 1,
                      numerosApostados = new List<int> { 1, 2, 3, 4, 5, 6 },
                  },

                    new Aposta(new Concurso
                      {
                          Id = 1,
                          dataFechamento = DateTime.Now.AddHours(2),
                          valorTotalApostas = 8001,
                      })
                  {
                      Id = 2,
                      numerosApostados = new List<int> { 1, 2, 3, 4, 5, 6 },
                  },

                     new Aposta(new Concurso
                      {
                          Id = 1,
                          dataFechamento = DateTime.Now.AddHours(2),
                          valorTotalApostas = 8001,
                      })
                  {
                      Id = 3,
                      numerosApostados = new List<int> { 1, 2, 3, 4, 5, 6 },
                  },
            };
        }
    }
}
