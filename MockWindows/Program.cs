﻿using Shared.Logging.Loggers;
using System;
using System.Configuration;
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

            string fileLoggingPath = ConfigurationManager.AppSettings["FileLoggingPath"];
            string fileLoggingServiceName = ConfigurationManager.AppSettings["FileLoggingServiceName"];
            string fileLoggingLogLevel = ConfigurationManager.AppSettings["FileLoggingLogLevel"];
            string watchingPath = ConfigurationManager.AppSettings["WatchingPath"];

            // log nereye atılcak
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
