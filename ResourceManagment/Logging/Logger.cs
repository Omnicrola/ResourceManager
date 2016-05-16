using System;
using System.Diagnostics;

namespace ResourceManagment.Logging
{
    public class Logger
    {
        public static Logger Instance = new Logger();

        public EventHandler<LoggingEventArgs> LogAdded;
        public EventHandler<ExceptionLogEventArgs> ExceptionRaised;
        private bool _logToConsole;

        private Logger()
        {
            LogToConsole = true;
        }


        public bool LogToConsole
        {
            get { return _logToConsole; }
            set
            {
                _logToConsole = value;
                if (_logToConsole)
                {
                    LogAdded += HandleLogToConsole;
                    ExceptionRaised += HandleExceptionToConsole;
                }
            }
        }

        private void HandleExceptionToConsole(object sender, ExceptionLogEventArgs e)
        {
            Debug.WriteLine(e.LogTime + " : " + e.Exception.Message);
        }

        private void HandleLogToConsole(object sender, LoggingEventArgs e)
        {
            Debug.WriteLine(e.LogTime + " : " + e.Message);
        }
    }


    public class ExceptionLogEventArgs
    {
        public DateTime LogTime { get; }
        public Exception Exception { get; }

        public ExceptionLogEventArgs(DateTime logTime, Exception exception)
        {
            LogTime = logTime;
            Exception = exception;
        }
    }

    public class LoggingEventArgs
    {
        public DateTime LogTime { get; }
        public string Message { get; }

        public LoggingEventArgs(DateTime logTime, string message)
        {
            LogTime = logTime;
            Message = message;
        }
    }
}