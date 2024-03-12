using Serilog;
using Shared.Logging;
using System.IO;
using System.ServiceProcess;
using System.Timers;

namespace MockWindows
{
    public partial class Service1 : ServiceBase
    {
        private readonly ILogger _logger;
        private readonly string _watchingPath;
        public Service1(ILogger logger, string watchingPath)
        {
            _logger = logger;
            _watchingPath = watchingPath;
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _logger.Information("Servis başlatıldı.");
            FileSystemWatcher Watcher = new FileSystemWatcher();
            Watcher.Path = _watchingPath;
            Watcher.IncludeSubdirectories = true;
            Watcher.Created += new FileSystemEventHandler(Wathcer_Changed);
            Watcher.Deleted += new FileSystemEventHandler(Wathcer_Changed);
            Watcher.EnableRaisingEvents = true;
        }

        protected override void OnStop()
        {
            _logger.Information("Servis durduruldu.");
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            _logger.Information("Servis geri çağırılıyor.");
        }

        void Wathcer_Changed(object sender, FileSystemEventArgs e)
        {
            _logger.Information($"Change Type = {e.ChangeType}, Path = {e.FullPath}");
        }
    }
}
