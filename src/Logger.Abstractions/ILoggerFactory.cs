using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logger.Abstractions
{
    public interface ILoggerFactory
    {
        ILogger CreateLogger(string categoryName);

        void AddProvider(ILoggerProvider provider);

    }
}
