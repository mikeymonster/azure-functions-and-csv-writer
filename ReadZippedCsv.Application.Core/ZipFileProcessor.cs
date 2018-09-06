using System;
using System.IO.Compression;
using ReadZippedCsv.Application.Processors;

namespace ReadZippedCsv.Application
{
    public class ZipFileProcessor
    {
        public IProcessorFactory ProcessorFactory { get; private set; }

        public string InputFilePath { get; private set; }

        public ZipFileProcessor(string inputFilePath, IProcessorFactory processorFactory = null)
        {
            InputFilePath = inputFilePath;
            ProcessorFactory = processorFactory ?? new ProcessorFactory();
        }

        public void GetStreamFromZipFile()
        {
            //https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-compress-and-extract-files
            using (var zipFile = ZipFile.OpenRead(InputFilePath))
            {
                foreach (var entry in zipFile.Entries)
                {
                    Console.WriteLine($"Processing zip file entry: {entry.Name}");
                    using (var stream = entry.Open())
                    {
                        ProcessorFactory
                            .GetProcessor(entry.Name)
                            .Process(stream);
                    }
                }
            }
        }
    }
}
