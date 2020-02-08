using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Stock.DB.Logging
{
    class FileLogger : ILogger
    {
        private static readonly Object _lock = new object();
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            lock(_lock)
               File.AppendAllText("StockDBLog.txt", formatter(state, exception));
        }
    }
}
