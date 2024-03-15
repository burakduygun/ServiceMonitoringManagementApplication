using Serilog;
using Shared.Logging;
using System.Configuration;
using System.Reflection;
using System.ServiceProcess;

namespace MockWindows
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;

            string watchingPath = ConfigurationManager.AppSettings["WatchingPath"];

            var minLogLevel = LoggerHelper.GetLogLevel("MockWindows");
            var logger = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .MinimumLevel.Is(minLogLevel)
                .Enrich.FromLogContext()
                .CreateLogger();

            ServicesToRun = new ServiceBase[]
            {
                new Service1(logger,watchingPath)
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
