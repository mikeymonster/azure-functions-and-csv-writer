using System.IO;
using System.Text;
using ReadZippedCsv.Application.Entities;

namespace ReadZippedCsv.Application.Processors
{
    public class AimProcessor : ProcessorBase<Aim>, IProcessor
    {
        public AimProcessor()
        {
            Schema = "Data_Lock";
            TableName = "DAS_ValidAims";
            SqlFileNamePrefix = "DAS_ValidAims";
        }

        public void Process(Stream stream)
        {
            using (var file = new StreamWriter(GetSqlFilePath(SqlFileNamePrefix)))
            {
                WriteSqlPrologue(file, TableName);

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
            }
        }
    }
}
