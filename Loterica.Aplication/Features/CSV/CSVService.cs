using Loterica.Domain.Features.Lotericas;
using Loterica.Domain.Features.ResultadoConcursos;
using Loterica.Infra.CSV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Aplication.Features.CSV
{
    public class CSVService
    {
        public void ExportarCSV(List<ResultadoConcurso> resultadoConcurso, string path)
        {
            foreach (var item in resultadoConcurso)
            {
                CSVExtension.Valida(item);
            }
            CSVExtension.SerializeResultadoConcurso(resultadoConcurso, path);
        }
    }
}
