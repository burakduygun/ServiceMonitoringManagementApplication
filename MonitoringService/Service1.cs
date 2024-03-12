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
using Serilog;

namespace MonitoringService
{
    public partial class Service1 : ServiceBase
    {
        private List<Timer> timers = new List<Timer>();
        private readonly ILogger _logger;
        private FileSystemWatcher watcher;
        private string filePath = Shared.PathAccess.ServiceSettingsPath;

        public Service1(ILogger logger)
        {
            _logger = logger;
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            StartMonitoringService();
        }

        protected override void OnStop()
        {
            StopMonitoringService();
        }

        public FileSystemWatcher GetWatcher()
        {
            return watcher;
        }

        public List<Timer> GetTimers()
        {
            return timers;
        }
        public void SetTimer(Timer timer)
        {
            timers.Add(timer);
        }

        public void StartMonitoringService()
        {
            try
            {
                var serviceSettings = Shared.Services.FileAccess.LoadServiceSettings(filePath);

                ApplySettings(serviceSettings);

                _logger.Information("Monitoring servis başlatılıyor.");

                //FileSystemWatcher watcher = new FileSystemWatcher();
                watcher = new FileSystemWatcher();
                watcher.Path = Path.GetDirectoryName(filePath);
                watcher.Filter = Path.GetFileName(filePath);
                watcher.NotifyFilter = NotifyFilters.LastWrite;
                watcher.Changed += OnSettingsFileChanged;
                watcher.EnableRaisingEvents = true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }

        public void StopMonitoringService()
        {
            foreach (var timer in timers)
            {
                timer.Stop();
                timer.Dispose();
            }
            timers.Clear();
            _logger.Information("Monitoring servis durduruldu.");
        }

        public void OnSettingsFileChanged(object source, FileSystemEventArgs e)
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

        public void ApplySettings(List<ServiceSettings> serviceSettings)
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

                    timer.Elapsed += (object sender, ElapsedEventArgs e) =>
                    {
                        CheckService(service, 3);
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

        public void CheckService(IService service, int tryCount)
        {
            try
            {
                if (!service.CheckStatus())
                {
                    _logger.Information(service.ServiceName + " servisi yeniden başlatılıyor.");
                    service.RestartService();
                    if (tryCount > 0)
                    {
                        tryCount--;
                        System.Threading.Thread.Sleep(100);
                        CheckService(service, tryCount);
                    }
                }
                else
                {
                    _logger.Information(service.ServiceName + " çalışıyor.");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }

        }
    }
}
