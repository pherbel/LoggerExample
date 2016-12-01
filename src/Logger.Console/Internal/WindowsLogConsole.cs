using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logger.Console.Internal
{
    public class WindowsLogConsole : IConsole
    {
        private void SetColor(ConsoleColor? background, ConsoleColor? foreground)
        {
            if (background.HasValue)
            {
                System.Console.BackgroundColor = background.Value;
            }


            if (foreground.HasValue)
            {
                System.Console.ForegroundColor = foreground.Value;
            }
        }


        private void ResetColor()
        {
            System.Console.ResetColor();
        }


        public void Write(string message, ConsoleColor? background, ConsoleColor? foreground)
        {
            SetColor(background, foreground);
            System.Console.Out.Write(message);
            ResetColor();
        }


        public void WriteLine(string message, ConsoleColor? background, ConsoleColor? foreground)
        {
            SetColor(background, foreground);
            System.Console.Out.WriteLine(message);
            ResetColor();
        }


        public void Flush()
        {
            // No action required as for every write, data is sent directly to the console 
            // output stream 
        }
    }

}
