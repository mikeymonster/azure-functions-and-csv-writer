using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using ReadZippedCsv.Application.Interfaces;
using ReadZippedCsv.Data.Entities;

namespace ReadZippedCsv.Application.Processors
{
    public class AimSqlScriptProcessor : ProcessorBase<Aim>, IProcessor
    {
        public AimSqlScriptProcessor(IConfigurationRoot configuration)
            : base(configuration)
        {
            Schema = "Data_Lock";
            TableName = "DAS_ValidAims";
            SqlFileNamePrefix = "DAS_ValidAims";
        }
        
        public void Process(Stream stream)
        {
            var filePath = GetSqlFilePath(SqlFileNamePrefix);
            using (var file = new StreamWriter(filePath))
            {
                WriteSqlPrologue(file);

                foreach (var item in GetData<Aim>(stream))
                {
                    //Console.WriteLine($"Aim: UkPrn = {item.UkPrn}, NumberOfLearnersWithACT1 = NumberOfLearnersWith{item.NumberOfLearnersWithACT1}, NumberOfLearnersWithACT2 = {item.NumberOfLearnersWithACT2}");

                    var sb = new StringBuilder();

                    sb.Append($"INSERT INTO [{Schema}].[{TableName}] ([Ukprn], [NumberOfLearnersWithACT1], [NumberOfLearnersWithACT2]) ");
                    sb.Append("VALUES (");
                    sb.Append($"{item.UkPrn}, ");
                    sb.Append($"{item.NumberOfLearnersWithACT1}, ");
                    sb.Append($"{item.NumberOfLearnersWithACT2}");
                    sb.Append(")");

                    file.WriteLine(sb.ToString());
                }

                WriteSqlEpilogue(file);

                Console.WriteLine($"Created file {filePath}");
            }
        }
    }
}
