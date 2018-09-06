using System;
using System.IO;
using System.IO.Compression;
using Microsoft.Extensions.Configuration;
using ReadZippedCsv.Application.Factories;
using ReadZippedCsv.Application.Interfaces;
using ReadZippedCsv.Data;

namespace ReadZippedCsv.Application
{
    public class ZipFileDatabaseUpdateProcessor : IZipFileProcessor
    {
        public IConfigurationRoot Configuration { get; private set; }

        public IDataLockRepository Repository { get; private set; }

        public IProcessorFactory ProcessorFactory { get; private set; }

        public ZipFileDatabaseUpdateProcessor(IDataLockRepository repository, IConfigurationRoot configuration, IProcessorFactory processorFactory = null)
        {
            Configuration = configuration;
            Repository = repository;

            ProcessorFactory = processorFactory ?? new DatabaseUpdateProcessorFactory(repository, configuration);
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
                foreach (var entry in zipArchive.Entries)
                {
                    Console.WriteLine($"Processing zip file entry: {entry.Name}");
                    using (var entryStream = entry.Open())
                    {
                        ProcessorFactory
                            .GetProcessor(entry.Name)
                            .Process(entryStream);
                    }
                }
            }
        }
    }
}
