using System;
using System.IO;
using System.IO.Compression;
using Microsoft.Extensions.Configuration;
using ReadZippedCsv.Application.Factories;
using ReadZippedCsv.Application.Interfaces;
using ReadZippedCsv.Application.Helpers;

namespace ReadZippedCsv.Application
{
    public class ZipFileToSqlScriptsProcessor : IZipFileProcessor
    {
        public const string SqlFileNamePrefix = "Data_Locks_Insert_";

        public IConfigurationRoot Configuration { get; private set; }

        public IProcessorFactory ProcessorFactory { get; private set; }

        public ZipFileToSqlScriptsProcessor(IConfigurationRoot configuration, IProcessorFactory processorFactory = null)
        {
            Configuration = configuration;
            ProcessorFactory = processorFactory ?? new SqlScriptProcessorFactory(configuration);
        }

        public void Process(string inputFilePath)
        {
            using (var stream = File.OpenRead(inputFilePath))
            {
                Process(stream);
            }
        }

        public void Process(Stream stream)
        {
            using (var zipArchive = new ZipArchive(stream, ZipArchiveMode.Read))
            {
                var directory = Configuration?["scriptOutputDirectory"] ?? $@"C:\temp\";
                var filePath = Helpers.FileHelpers.GetSqlFilePath(directory, SqlFileNamePrefix);

                using (var fileStreamWriter = new StreamWriter(filePath))
                {
                    foreach (var entry in zipArchive.Entries)
                    {
                        Console.WriteLine($"Processing zip file entry: {entry.Name}");
                        using (var entryStream = entry.Open())
                        {
                            var processor = ProcessorFactory.GetProcessor(entry.Name);
                            ((IFileOutputProcessor) processor).FileStreamWriter = fileStreamWriter;
                                processor.Process(entryStream);
                        }
                    }

                    WriteSqlEpilogue(fileStreamWriter);
                }
            }
        }

        private void WriteSqlEpilogue(StreamWriter fileStreamWriter)
        {
            fileStreamWriter.WriteLine("");
            fileStreamWriter.WriteLine("/*"); 
            fileStreamWriter.WriteLine("SELECT * FROM [Data_Lock].[DAS_DataLocks]"); 
            fileStreamWriter.WriteLine("SELECT * FROM [Data_Lock].[DAS_ValidAims]"); 
            fileStreamWriter.WriteLine("SELECT * FROM [Data_Lock].[DAS_ValidLearners]"); 
            fileStreamWriter.WriteLine("*/"); 
        }
    }
}
