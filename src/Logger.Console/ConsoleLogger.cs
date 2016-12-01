using Logger.Abstractions;
using Logger.Console.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Logger.Console
{
    public class ConsoleLogger : ILogger
    {

        private static readonly object _lock = new object();


        // ConsoleColor does not have a value to specify the 'Default' color
        private readonly ConsoleColor? DefaultConsoleColor = null;

        private IConsole _console;

        private Func<LogLevel, bool> _filter;

        public Func<LogLevel, bool> Filter
        {
            get { return _filter; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }


                _filter = value;
            }
        }


        public ConsoleLogger(string name, Func<LogLevel, bool> filter)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
            Filter = filter ?? ((logLevel) => true);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console = new WindowsLogConsole();
            }
            else
            {
                //TODO: Add AnsiConsole for support other platform
            }
        }

        public IConsole Console
        {
            get { return _console; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                _console = value;
            }
        }


        public string Name { get; }

        public void Log(LogLevel logLevel, string message)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var formattedMessage = LogFormatterHelper.Format(Name, logLevel, message, DateTime.UtcNow);

            //we just trim the message if it is longer the 1000 char. We don't want to throw exception
            if (formattedMessage.Length > 1000)
            {
                formattedMessage = formattedMessage.Substring(0, 1000);
            }
            if (!string.IsNullOrEmpty(formattedMessage))
            {
                WriteMessage(formattedMessage,logLevel);
            }
        }

        public virtual void WriteMessage(string message, LogLevel logLevel)
        {

            var logLevelColors = GetLogLevelConsoleColors(logLevel);

            if (message.Length > 0)
            {
                lock (_lock)
                {

                    // use default colors from here on
                    Console.Write(message, logLevelColors.Background, logLevelColors.Foreground);

                    Console.Flush();
                }
            }

        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return Filter(logLevel);
        }



        private ConsoleColors GetLogLevelConsoleColors(LogLevel logLevel)
        {
            switch (logLevel)
            {

                case LogLevel.Error:
                    return new ConsoleColors(ConsoleColor.Red, ConsoleColor.Black);
                case LogLevel.Info:
                    return new ConsoleColors(ConsoleColor.DarkGreen, ConsoleColor.Black);
                case LogLevel.Debug:
                    return new ConsoleColors(ConsoleColor.Gray, ConsoleColor.Black);
                default:
                    return new ConsoleColors(DefaultConsoleColor, DefaultConsoleColor);
            }
        }

        private struct ConsoleColors
        {
            public ConsoleColors(ConsoleColor? foreground, ConsoleColor? background)
            {
                Foreground = foreground;
                Background = background;
            }

            public ConsoleColor? Foreground { get; }

            public ConsoleColor? Background { get; }
        }

    }


}
