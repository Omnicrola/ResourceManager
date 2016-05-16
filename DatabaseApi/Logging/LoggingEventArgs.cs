using System;

namespace DatabaseApi.Logging
{
    public class LoggingEventArgs
    {
        public DateTime LogTime { get; }
        public string Message { get; }
        public LogLevel LogLevel { get; }

        public LoggingEventArgs(DateTime logTime, string message, LogLevel logLevel)
        {
            LogTime = logTime;
            Message = message;
            LogLevel = logLevel;
        }
    }
}