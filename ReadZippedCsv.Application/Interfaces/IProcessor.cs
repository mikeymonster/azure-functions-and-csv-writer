using System.IO;
using Microsoft.Extensions.Configuration;

namespace ReadZippedCsv.Application.Interfaces
{
    public interface IProcessor
    {
        void Process(Stream stream);
    }
}
