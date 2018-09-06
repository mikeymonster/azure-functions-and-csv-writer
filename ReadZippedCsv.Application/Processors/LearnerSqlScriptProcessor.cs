using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using ReadZippedCsv.Application.Interfaces;
using ReadZippedCsv.Data.Entities;

namespace ReadZippedCsv.Application.Processors
{
    public class LearnerSqlScriptProcessor : ProcessorBase<Learner>, IProcessor
    {
        public LearnerSqlScriptProcessor(IConfigurationRoot configuration)
            : base(configuration)
        {
            Schema = "Data_Lock";
            TableName = "DAS_ValidLearners";
            SqlFileNamePrefix = "DAS_ValidLearners";
        }

        public void Process(Stream stream)
        {
            var filePath = GetSqlFilePath(SqlFileNamePrefix);
            using (var file = new StreamWriter(filePath))
            {
                WriteSqlPrologue(file);

                foreach (var item in GetData<Learner>(stream))
                {
                    //Console.WriteLine($"Learner: UkPrn = {learner.UkPrn}, NumberOfLearners = {learner.NumberOfLearners}");

                    var sb = new StringBuilder();

                    sb.Append($"INSERT INTO [{Schema}].[{TableName}] ([Ukprn], [NumberOfLearners]) ");
                    sb.Append("VALUES (");
                    sb.Append($"{item.UkPrn}, ");
                    sb.Append($"{item.NumberOfLearners}");
                    sb.Append(")");

                    file.WriteLine(sb.ToString());
                }

                WriteSqlEpilogue(file);

                Console.WriteLine($"Created file {filePath}");
            }
        }
    }
}
