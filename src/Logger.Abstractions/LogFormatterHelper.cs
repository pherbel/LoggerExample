using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.Abstractions
{
    public static class LogFormatterHelper
    {

        [ThreadStatic]
        private static StringBuilder _logBuilder;
        public static string Format(string name, LogLevel logLevel, string message, DateTime time)
        {
            var logBuilder = _logBuilder;
            _logBuilder = null;

            if (logBuilder == null)
            {
                logBuilder = new StringBuilder();
            }
            if (!string.IsNullOrEmpty(message))
            {
                logBuilder.Append("[");
                logBuilder.Append(name);
                logBuilder.Append("]: ");
                logBuilder.Append(time.ToUniversalTime());
                logBuilder.Append(" ");
                logBuilder.Append("[");
                logBuilder.Append(GetLogLevelString(logLevel));
                logBuilder.Append("] ");
                logBuilder.Append(message);
            }
            string logMessage = null;
            if (logBuilder.Length > 0)
            {
                logMessage = logBuilder.ToString();
            }
            
            logBuilder.Clear();
            _logBuilder = logBuilder;

            return logMessage;
        }
        private static string GetLogLevelString(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    return "debug";
                case LogLevel.Info:
                    return "info";
                case LogLevel.Error:
                    return "error";
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel));
            }
        }
    }
}
