using Shared.Logging;
using System.IO;
using System.ServiceProcess;
using System.Timers;

namespace MockWindows
{
    public partial class Service1 : ServiceBase
    {
        //Timer timer = new Timer();

        private readonly AbstractLogger _logger;
        private readonly string _watchingPath;
        public Service1(AbstractLogger logger, string watchingPath)
        {
            _logger = logger;
            _watchingPath = watchingPath;
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //WriteToFile("Service is started at " + DateTime.Now);
            _logger.Info("service is started");

            FileSystemWatcher Watcher = new FileSystemWatcher();
            //Watcher.Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Watcher.Path = _watchingPath;
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
            _logger.Info("Service is stopped");
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            //WriteToFile("Service is recall at " + DateTime.Now);
            _logger.Info("Service is recall");

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
            _logger.Info($"Change Type = {e.ChangeType}, Path = {e.FullPath}");
        }
    }
}
