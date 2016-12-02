using Logger.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Logger.Tests
{
    public class LogFormatterHelperTest
    {

        [Fact]
        public void Formatter_should_return_with_the_proper_formatted_text()
        {
            string name = "Name";
            LogLevel level = LogLevel.Debug;
            string message = "Log Message";
            DateTime time = DateTime.UtcNow;

            string resultText = LogFormatterHelper.Format(name, level,message,time);


            Assert.NotNull(resultText);
            Assert.NotEmpty(resultText);
            string format = $"[{name}]: {time.ToUniversalTime()} [debug] {message}";
            Assert.Equal(resultText,format );
        }
    }
}
