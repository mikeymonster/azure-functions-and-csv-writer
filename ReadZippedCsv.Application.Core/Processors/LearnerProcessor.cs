using System.IO;
using System.Text;
using ReadZippedCsv.Application.Entities;

namespace ReadZippedCsv.Application.Processors
{
    public class LearnerProcessor : ProcessorBase<Learner>, IProcessor
    {
        public LearnerProcessor()
        {
            Schema = "Data_Lock";
            TableName = "DAS_ValidLearners";
            SqlFileNamePrefix = "DAS_ValidLearners";
        }

        public void Process(Stream stream)
        {
            using (var file = new StreamWriter(GetSqlFilePath(SqlFileNamePrefix)))
            {
                WriteSqlPrologue(file, TableName);

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
            }
        }
    }
}
