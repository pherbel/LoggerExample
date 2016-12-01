using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logger.Abstractions
{
    public interface ILogger
    {

        void Log(LogLevel logLevel, string msg); 


        /// <summary> 
        /// Checks if the given <paramref name="logLevel"/> is enabled. 
        /// </summary> 
        /// <param name="logLevel">level to be checked.</param> 
        /// <returns><c>true</c> if enabled.</returns> 
        bool IsEnabled(LogLevel logLevel);

    }
}
