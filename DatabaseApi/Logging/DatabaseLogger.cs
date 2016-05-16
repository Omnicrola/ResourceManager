using System;
using System.Diagnostics;
using DatabaseApi.SqlLite.Api;

namespace DatabaseApi.Logging
{
    public class DatabaseLogger
    {
        public static DatabaseLogger Instance = new DatabaseLogger();

        public EventHandler<LoggingEventArgs> LogAdded;
        public EventHandler<ExceptionLogEventArgs> ExceptionRaised;
        public string SourceName { get; private set; }
        public bool LogToConsole { get; set; }

        private DatabaseLogger()
        {
            SourceName = "SQLite Database API";
            LogAdded += HandleDefaultLog;
            ExceptionRaised += HandleDefaultException;
            LogToConsole = true;
        }


        public void Log(Exception exception)
        {
            var now = DateTime.Now;
            ExceptionRaised?.Invoke(this, new ExceptionLogEventArgs(now, exception, LogLevel.ERROR));
        }

        public void Log(string message, LogLevel logLevel)
        {
            var dateTime = DateTime.Now;
            LogAdded?.Invoke(this, new LoggingEventArgs(dateTime, message, logLevel));
        }

        private void HandleDefaultException(object sender, ExceptionLogEventArgs e)
        {
            var message = e.LogTime + " : " + e.Exception.Message;
            if (LogToConsole)
            {
                Debug.WriteLine(message);
            }
        }


        private void HandleDefaultLog(object sender, LoggingEventArgs e)
        {
            if (LogToConsole)
            {
                Debug.WriteLine(e.LogTime + " : " + e.Message);
            }
        }

    }
}