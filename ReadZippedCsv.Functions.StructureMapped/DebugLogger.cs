using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace ReadZippedCsv.Functions.StructureMapped
{
    public class DebugLogger<T> : ILogger<T>, IDisposable
    {
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Debug.Print($"Log::{logLevel}: {state}");
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }

        public void Dispose()
        {
        }
    }
}
