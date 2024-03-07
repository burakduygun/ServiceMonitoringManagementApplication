using System.ServiceProcess;
using System.Configuration;
using Shared.Logging.Loggers;
using System;
using Serilog;

namespace MonitoringService
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;

            //string fileLoggingPath = ConfigurationManager.AppSettings["FileLoggingPath"];
            //string fileLoggingServiceName = ConfigurationManager.AppSettings["FileLoggingServiceName"];
            //string fileLoggingLogLevel = ConfigurationManager.AppSettings["FileLoggingLogLevel"];

            //var logger = new FileLogger(fileLoggingPath, fileLoggingServiceName);

            //logger.SetLogLevel((Shared.Logging.LogLevel)Enum.Parse(typeof(Shared.Logging.LogLevel), fileLoggingLogLevel));

            Log.Logger = new LoggerConfiguration()
                 .ReadFrom.AppSettings()
                 .CreateLogger();

            var logger = Log.Logger;

            ServicesToRun = new ServiceBase[]
            {
                new Service1(logger)
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
