using CsvHelper;
using Loterica.Domain;
using Loterica.Domain.Features.Lotericas;
using Loterica.Domain.Features.ResultadoConcursos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Infra.CSV
{
    public static class CSVExtension
    {
        /// <summary>
        /// Gera uma string serializada em csv de qualquer objeto não estático.
        /// </summary>
        /// <param name="objs"></param>
        /// <returns>String in CSV format</returns>
        public static void SerializeResultadoConcurso(List<ResultadoConcurso> resultado, string path)
        {
            using (var writer = new StreamWriter(path, false, Encoding.UTF8))
            using (var csvWriter = new CsvWriter(writer))
            {
                csvWriter.Configuration.CultureInfo = CultureInfo.GetCultureInfo("pt-BR");

                csvWriter.Configuration.Delimiter = ";";
                csvWriter.Configuration.HasHeaderRecord = true;
                csvWriter.Configuration.RegisterClassMap<ConcursoMap>();
                csvWriter.Configuration.RegisterClassMap<ResultadoConcursoMap>();
                csvWriter.Configuration.RegisterClassMap<FaturamentoLotericaMap>();
                csvWriter.WriteHeader<Concurso>();
                csvWriter.WriteHeader<ResultadoConcurso>();
                csvWriter.WriteHeader<FaturamentoLoterica>();
                csvWriter.NextRecord();
                foreach (var item in resultado)
                {
                    csvWriter.WriteRecord(item.concurso);
                    csvWriter.WriteRecord(item);
                    csvWriter.WriteRecord(item.faturamento);
                    csvWriter.NextRecord();
                }

            }
        }

        public static void Valida<T>(T obj)
        {
            if (obj == null)
                throw new InvalidElementException();
        }
    }
}
