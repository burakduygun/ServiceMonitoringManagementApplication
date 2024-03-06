using System;
using Shared.Logging;
using System.ServiceProcess;
using System.Timers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Web.Administration;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using Shared.Services;
using System.Text.Json;
using System.Configuration;
using MonitoringService.ManageService;

namespace MonitoringService
{
    public partial class Service1 : ServiceBase
    {
        private List<Timer> timers = new List<Timer>();
        private readonly AbstractLogger _logger;
        private string filePath = ConfigurationManager.AppSettings["ServiceSettingsPath"];
        public Service1(AbstractLogger logger)
        {
            _logger = logger;
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                var serviceSettings = Shared.Services.FileAccess.LoadServiceSettings(filePath);

                ApplySettings(serviceSettings);

                _logger.Info("Monitoring servis başlatılıyor.");

                FileSystemWatcher watcher = new FileSystemWatcher();
                watcher.Path = Path.GetDirectoryName(filePath);
                watcher.Filter = Path.GetFileName(filePath);
                watcher.NotifyFilter = NotifyFilters.LastWrite;
                watcher.Changed += OnSettingsFileChanged;
                watcher.EnableRaisingEvents = true;
            }
            catch (Exception ex)
            {
                _logger.Info(ex.Message);
            }
        }

        protected override void OnStop()
        {
            foreach (var timer in timers)
            {
                timer.Stop();
                timer.Dispose();
            }
            _logger.Info("Monitoring servis durduruldu.");
        }

        //private List<ServiceSettings> ReadServiceSettings(string path)
        //{
        //    //string jsonContent = File.ReadAllText(path);
        //    //return JsonSerializer.Deserialize<List<ServiceSettings>>(jsonContent);

        //    using (StreamReader reader = new StreamReader(path))
        //    {
        //        string jsonContent = reader.ReadToEnd();
        //        return JsonSerializer.Deserialize<List<ServiceSettings>>(jsonContent);
        //    }
        //}

        private void OnSettingsFileChanged(object source, FileSystemEventArgs e)
        {
            try
            {
                var serviceSettings = Shared.Services.FileAccess.LoadServiceSettings(filePath);

                foreach (var timer in timers)
                {
                    timer.Stop();
                    timer.Dispose();
                }
                timers.Clear();

                ApplySettings(serviceSettings);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }

        private void ApplySettings(List<ServiceSettings> serviceSettings)
        {
            try
            {
                foreach (var serviceSetting in serviceSettings)
                {
                    IService service;
                    var timer = new Timer();
                    timer.Interval = serviceSetting.Frequency * 60000;

                    if (serviceSetting.ServiceType == Shared.Services.ServiceType.IIS)
                    {
                        service = new IisService(serviceSetting.ServiceName, serviceSetting.PingUrl, _logger);
                    }
                    else
                    {
                        service = new MockWindowsService(serviceSetting.ServiceName, _logger);
                    }

                    timer.Elapsed += async (object sender, ElapsedEventArgs e) =>
                    {
                        await service.CheckStatus();
                    };

                    timer.Start();
                    timers.Add(timer);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }
    }
}
