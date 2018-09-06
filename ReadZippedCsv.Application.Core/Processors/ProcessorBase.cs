using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using FileHelpers;

namespace ReadZippedCsv.Application.Processors
{
    public class ProcessorBase<T>
    {
        public string Schema { get; protected set; }
        public string TableName { get; protected set; }
        public string SqlFileNamePrefix { get; protected set; }

        public ProcessorBase()
        {
        }

        public IList<T> GetData<T>(ZipArchiveEntry entry)
            where T : class
        {
            using (var stream = entry.Open())
            {
                return GetData<T>(stream);
            }
        }

        public IList<T> GetData<T>(Stream stream)
            where T : class
        {
            var engine = new FileHelperEngine<T>
            {
                Options = { IgnoreFirstLines = 1 }
            };
            //https://stackoverflow.com/questions/11000797/filehelpers-how-to-skip-first-and-last-line-reading-fixed-width-text
            //engine.BeforeReadRecord += (e, args) =>
            //{
            //    if (args.LineNumber == 1)
            //        args.SkipThisRecord = true;
            //};

            using (var reader = new StreamReader(stream))
            {
                return engine.ReadStream(reader).ToList();
            }
        }

        protected string GetSqlFilePath(string prefix)
        {
            //TODO: Get directory from config
            var directory = $@"C:\temp\";

            return Path.Combine(directory,
                $@"{prefix}_{DateTime.Now:dd_MM_yyyy_HH_mm_ss}.sql");
        }

        protected void WriteSqlPrologue(StreamWriter file, string TableName)
        {
            //file.WriteLine("USE [SFA.DAS.Data.Database]");
            //file.WriteLine("GO");
            //file.WriteLine("");
            file.WriteLine($"TRUNCATE TABLE [{Schema}].[{TableName}]");
            file.WriteLine("GO");
            file.WriteLine("");
        }
    }
}
