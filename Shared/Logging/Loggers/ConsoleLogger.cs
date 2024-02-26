using System;

namespace Shared.Logging.Loggers
{
    public class ConsoleLogger : AbstractLogger
    {
        protected override void LogImpl(string message)
        {
            Console.WriteLine(message);
        }
    }
}
