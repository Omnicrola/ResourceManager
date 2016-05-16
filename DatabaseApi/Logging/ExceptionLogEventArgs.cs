using System;
using System.Diagnostics;

namespace DatabaseApi.Logging
{
    public class ExceptionLogEventArgs
    {
        public DateTime LogTime { get; }
        public Exception Exception { get; }
        public LogLevel EntryType { get; private set; }

        public ExceptionLogEventArgs(DateTime logTime, Exception exception, LogLevel entryType)
        {
            LogTime = logTime;
            Exception = exception;
            EntryType = entryType;
        }
    }
}