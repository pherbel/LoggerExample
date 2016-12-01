using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logger.Abstractions
{
    public static class LoggerExtensions
    {
        public static void LogDebug(this ILogger logger, string message)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }


            logger.Log(LogLevel.Debug, message);
        }


        public static void LogInfo(this ILogger logger, string message)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }


            logger.Log(LogLevel.Info, message);
        }

        public static void LogError(this ILogger logger, string message)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }


            logger.Log(LogLevel.Error, message);
        }

    }
}
