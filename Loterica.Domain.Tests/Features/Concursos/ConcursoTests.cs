using FluentAssertions;
using Loterica.Common.Tests.Concursos;
using Loterica.Domain.Concursos;
using NUnit.Framework;
using System;

namespace Loterica.Domain.Tests
{
    [TestFixture]
    public class ConcursoTests
    {
        private Concurso _concurso; 
		
        [Test]
        public void Domain_Concurso_Validar_Deveria_Estar_OK()
        {
            _concurso = ObjectMother.GetConcurso();
            _concurso.Validar();
            _concurso.dataFechamento.Should().NotBeBefore(DateTime.Now);
        }

        [Test]
        public void Domain_Concurso_Validar_Nao_Deveria_Permitir_Data_Vazia()
        {
            _concurso = new Concurso();
            Action action = () =>_concurso.Validar();
            action.Should().Throw<InvalidDateTimeException>();
        }

        [Test]
        public void Domain_Concurso_Validar_Nao_Deveria_Permitir_Horario_Menor_Que_O_Atual()
        {
            _concurso = ObjectMother.GetConcursoHoraMenorAtual();
            Action action = () => _concurso.Validar();
            action.Should().Throw<InvalidDateTimeException>();
        }
    }
}





