using Logger.Abstractions;
using Logger.File.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logger.File
{
    public class FileLogger :ILogger

    {
        public FileLogger(string name, Func<LogLevel, bool> filter)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
            Filter = filter ?? ((logLevel) => true);
            FileWriter = new FileWriter();
        }
        private static readonly object _lock = new object();

        private Func<LogLevel, bool> _filter;

        private IFileWriter _fileWriter;

        public IFileWriter FileWriter
        {
            get { return _fileWriter; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                _fileWriter = value;
            }
        }

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

        public string Name { get; }

        public void Log(LogLevel logLevel, string message)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var formattedMessage = LogFormatterHelper.Format(Name, logLevel, message, DateTime.UtcNow);
            if (!string.IsNullOrEmpty(formattedMessage))
            {
                WriteMessage(formattedMessage, logLevel);
            }
        }

        public virtual void WriteMessage(string message, LogLevel logLevel)
        {


            if (message.Length > 0)
            {
                lock (_lock)
                {
                    FileWriter.WriteLine(message);
                    FileWriter.Flush();

                }
            }

        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return Filter(logLevel);
        }
    }
}
