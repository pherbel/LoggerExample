using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logger.File.Internal
{
   public interface IFileWriter :IDisposable
    {
        void WriteLine(string message);
        void Flush();
    }
}
