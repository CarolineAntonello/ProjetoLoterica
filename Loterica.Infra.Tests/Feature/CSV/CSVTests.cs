using FluentAssertions;
using Loterica.Domain.Features.ResultadoConcursos;
using Loterica.Infra.CSV;
using NUnit.Framework;
using System;

namespace Loterica.Infra.Tests.Feature.CSV
{
    [TestFixture]
    public class CSVTests
    {
        ResultadoConcurso _resultado;

        [Test]
        public void Infra_CSV_Deveria_Cair_Na_Validacao()
        {
            Action action = () => CSVExtension.Valida(_resultado);
            action.Should().Throw<InvalidElementException>();
        }
    }
}
