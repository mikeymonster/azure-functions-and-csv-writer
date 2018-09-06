using System.IO;

namespace ReadZippedCsv.Application.Processors
{
    public interface IProcessor
    {
        void Process(Stream stream);
    }
}
