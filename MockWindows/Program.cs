using Shared.Logging.Loggers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

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
            // log nereye atılcak

            string fileLoggingPath = ConfigurationManager.AppSettings["FileLoggingPath"];
            string fileLoggingServiceName = ConfigurationManager.AppSettings["FileLoggingServiceName"];
            string fileLoggingLogLevel = ConfigurationManager.AppSettings["FileLoggingLogLevel"];
            string watchingPath = ConfigurationManager.AppSettings["WatchingPath"];

            //var logger = new FileLogger("C:\\Users\\Burak.Duygun\\OneDrive - Logo\\Desktop", "MockWindows");
            var logger = new FileLogger(fileLoggingPath, fileLoggingServiceName);

            logger.SetLogLevel((Shared.Logging.LogLevel)Enum.Parse(typeof(Shared.Logging.LogLevel), fileLoggingLogLevel));

            ServicesToRun = new ServiceBase[]
            {
                // neresi takip edilcek
                new Service1(logger,watchingPath)
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
