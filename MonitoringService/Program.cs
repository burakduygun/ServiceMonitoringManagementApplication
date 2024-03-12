using System.ServiceProcess;
using System.Configuration;
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
