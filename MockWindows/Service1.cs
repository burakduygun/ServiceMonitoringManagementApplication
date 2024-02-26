using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MockWindows
{
    public partial class Service1 : ServiceBase
    {
        //Timer timer = new Timer();

        private readonly ILogger _logger;

        public Service1(ILogger logger)
        {
            _logger = logger;
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //WriteToFile("Service is started at " + DateTime.Now);
            _logger.LogInformation("service is started");

            FileSystemWatcher Watcher = new FileSystemWatcher();
            //Watcher.Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Watcher.Path = "C:\\Users\\Burak.Duygun\\OneDrive - Logo\\Desktop";
            Watcher.IncludeSubdirectories = true;
            Watcher.Created += new FileSystemEventHandler(Wathcer_Changed);
            Watcher.Deleted += new FileSystemEventHandler(Wathcer_Changed);
            Watcher.EnableRaisingEvents = true;
            //timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            //timer.Interval = 3000;
            //timer.Enabled = true;
        }

        protected override void OnStop()
        {
            //WriteToFile("Service is stopped at " + DateTime.Now);
            _logger.LogInformation("Service is stopped");
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            //WriteToFile("Service is recall at " + DateTime.Now);
            _logger.LogInformation("Service is recall");

        }

        //public void WriteToFile(string Message)
        //{
        //    string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
        //    if (!Directory.Exists(path))
        //    {
        //        Directory.CreateDirectory(path);
        //    }
        //    string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
        //    if (!File.Exists(filepath))
        //    {
        //        // Create a file to write to.
        //        using (StreamWriter sw = File.CreateText(filepath))
        //        {
        //            sw.WriteLine(Message);
        //        }
        //    }
        //    else
        //    {
        //        using (StreamWriter sw = File.AppendText(filepath))
        //        {
        //            sw.WriteLine(Message);
        //        }
        //    }
        //}

        void Wathcer_Changed(object sender, FileSystemEventArgs e)
        {
            _logger.LogInformation($"Change Type = {e.ChangeType}, Path = {e.FullPath}");
        }
    }
}
