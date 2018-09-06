using System.IO;

namespace ReadZippedCsv.Application.Interfaces
{
    public interface IZipFileProcessor
    {
        void Process(string inputFilePath);

        void Process(Stream stream);
    }
}
