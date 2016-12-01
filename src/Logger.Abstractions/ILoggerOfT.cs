using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logger.Abstractions
{
    public interface ILogger<out TCategoryName> : ILogger
    {

    }
}
