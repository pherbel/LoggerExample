using Logger;
using Logger.Abstractions;
using Logger.Console;
using Logger.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggerConsoleApp
{
    public class Program
    {
        private static readonly ILogger _logger;
        private static readonly ILoggerFactory _factory;
        static  Program()
        {
          _factory = new LoggerFactory();

            _logger = _factory.CreateLogger<Program>();
            _factory.AddConsole(LogLevel.Error);
            _factory.AddFile(LogLevel.Debug);
        }
        public static void Main(string[] args)
        {
            var testClass = new TestClass(_factory);
            testClass.DoSomthing();
            for (int i = 0; i < 100; i++)
            {
                _logger.LogInfo("Info message");
                _logger.LogDebug("Debug message");
                _logger.LogError("Error message");
            }


            Console.ReadLine();

        }
    }

    public class TestClass
    {
        private readonly ILogger _logger;
        public TestClass(ILoggerFactory factory)
        {
            _logger = factory.CreateLogger<TestClass>();
        }

        public void DoSomthing()
        {
            _logger.LogInfo("Info message");
            _logger.LogDebug("Debug message");
            _logger.LogError("Error message");
        }
    }
}
