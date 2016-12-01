using Logger.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logger
{
    internal class Logger : ILogger
    {
        private readonly LoggerFactory _loggerFactory;
        private readonly string _name;
        private ILogger[] _loggers;


        public Logger(LoggerFactory loggerFactory, string name)
        {
            _loggerFactory = loggerFactory;
            _name = name;


            var providers = loggerFactory.GetProviders();
            if (providers.Length > 0)
            {
                _loggers = new ILogger[providers.Length];
                for (var index = 0; index < providers.Length; index++)
                {
                    _loggers[index] = providers[index].CreateLogger(name);
                }
            }
        }


        public void Log(LogLevel logLevel,string message)
        {
            if (_loggers == null)
            {
                return;
            }


            List<Exception> exceptions = null;
            foreach (var logger in _loggers)
            {
                try
                {
                    logger.Log(logLevel, message);
                }
                catch (Exception ex)
                {
                    if (exceptions == null)
                    {
                        exceptions = new List<Exception>();
                    }


                    exceptions.Add(ex);
                }
            }


            if (exceptions != null && exceptions.Count > 0)
            {
                throw new AggregateException(
                    message: "An error occurred while writing to logger(s).", innerExceptions: exceptions);
            }
        }


        public bool IsEnabled(LogLevel logLevel)
        {
            if (_loggers == null)
            {
                return false;
            }


            List<Exception> exceptions = null;
            foreach (var logger in _loggers)
            {
                try
                {
                    if (logger.IsEnabled(logLevel))
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    if (exceptions == null)
                    {
                        exceptions = new List<Exception>();
                    }


                    exceptions.Add(ex);
                }
            }


            if (exceptions != null && exceptions.Count > 0)
            {
                throw new AggregateException(
                    message: "An error occurred while writing to logger(s).",
                    innerExceptions: exceptions);
            }


            return false;
        }




        internal void AddProvider(ILoggerProvider provider)
        {
            var logger = provider.CreateLogger(_name);
            int logIndex;
            if (_loggers == null)
            {
                logIndex = 0;
                _loggers = new ILogger[1];
            }
            else
            {
                logIndex = _loggers.Length;
                Array.Resize(ref _loggers, logIndex + 1);
            }
            _loggers[logIndex] = logger;
        }

    }

}
