using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using ReadZippedCsv.Application.Interfaces;
using ReadZippedCsv.Data.Entities;

namespace ReadZippedCsv.Application.Processors
{
    public class DataLockSqlScriptProcessor : ProcessorBase<DataLock>, IProcessor
    {

        public DataLockSqlScriptProcessor(IConfigurationRoot configuration)
            : base(configuration)
        {
            Schema = "Data_Lock";
            TableName = "DAS_DataLocks";
            SqlFileNamePrefix = "DAS_DataLocks";
        }

        public void Process(Stream stream)
        {
            var filePath = GetSqlFilePath(SqlFileNamePrefix);
            using (var file = new StreamWriter(filePath))
            {
                WriteSqlPrologue(file);

                foreach (var item in GetData<DataLock>(stream))
                {
                    //Console.WriteLine($"DataLock: Collection = {item.Collection}, UkPrn = {item.UkPrn}, LearnRefNumber = {item.LearnRefNumber}, ULN = {item.ULN}, AimSeqNumber = {item.AimSeqNumber}, RuleId = {item.RuleId}, CollectionPeriodName = {item.CollectionPeriodName}, CollectionPeriodMonth = {item.CollectionPeriodMonth}, CollectionPeriodYear = {item.CollectionPeriodYear }, LastSubmission = {item.LastSubmission:dd/MM/yyyy HH:mm:ss}, TNP = {item.TNP} ");
                    
                    var sb = new StringBuilder();

                    sb.Append($"INSERT INTO [{Schema}].[{TableName}] (");
                    sb.Append($"[Collection], [Ukprn], [LearnRefNumber], [ULN], ");
                    sb.Append($"[AimSeqNumber], [RuleId], ");
                    sb.Append($"[CollectionPeriodName], [CollectionPeriodMonth], [CollectionPeriodYear], ");
                    sb.Append($"[LastSubmission], [TNP]) ");
                    sb.Append("VALUES (");
                    sb.Append($"'{item.Collection}', ");
                    sb.Append($"{item.UkPrn}, ");
                    sb.Append($"'{item.LearnRefNumber}', ");
                    sb.Append($"{item.ULN}, ");
                    sb.Append($"{item.AimSeqNumber}, ");
                    sb.Append($"'{item.RuleId}', ");
                    sb.Append($"'{item.CollectionPeriodName}', ");
                    sb.Append($"{item.CollectionPeriodMonth}, ");
                    sb.Append($"{item.CollectionPeriodYear}, ");
                    sb.Append($"'{item.LastSubmission:yyyy-MM-dd HH:mm:ss}', ");
                    sb.Append($"{item.TNP}");
                    sb.Append(")");

                    file.WriteLine(sb.ToString());
                }

                WriteSqlEpilogue(file);

                Console.WriteLine($"Created file {filePath}");
            }
        }
    }
}
