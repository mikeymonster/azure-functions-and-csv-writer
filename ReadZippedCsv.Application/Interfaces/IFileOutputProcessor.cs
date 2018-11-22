using System;
using System.IO;

namespace ReadZippedCsv.Application.Interfaces
{
    public interface IFileOutputProcessor
    {
        StreamWriter FileStreamWriter { get; set; }
    }
}
