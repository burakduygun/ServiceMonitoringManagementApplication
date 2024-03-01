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

namespace MonitoringService
{
    public partial class Service1 : ServiceBase
    {
        private List<Timer> timers = new List<Timer>();
        private readonly AbstractLogger _logger;
        private string filePath = "C:\\Users\\Burak.Duygun\\OneDrive - Logo\\Desktop\\settings\\servicesettings.json";

        public Service1(AbstractLogger logger)
        {
            _logger = logger;
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                var serviceSettings = ReadServiceSettings(filePath);

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

        private async Task CheckIISService(string serviceName, string pingUrl)
        {
            if (!await IsHttpServiceControlling(pingUrl))
            {
                _logger.Info(serviceName + " başlatılacak.");
                try
                {
                    var server = new ServerManager();
                    var site = server.Sites.FirstOrDefault(s => s.Name == serviceName);

                    if (site != null)
                    {
                        site.Start();
                        _logger.Info(serviceName + " servisi yeniden başlatılıyor.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error($"Hata: {ex.Message}");
                    return;
                }
                _logger.Info(serviceName + " servisi yeniden başlatıldı.");
            }
            else
            {
                _logger.Info(serviceName + " zaten çalışıyor.");
            }
        }

        private void CheckWindowsService(string serviceName)
        {
            if (!IsServiceRunning(serviceName))
            {
                _logger.Info(serviceName + " servisi yeniden başlatılıyor.");

                try
                {
                    ServiceController sc = new ServiceController(serviceName);

                    sc.Start();
                    sc.WaitForStatus(ServiceControllerStatus.Running);

                    _logger.Info(serviceName + " servisi yeniden başlatıldı.");
                }
                catch (Exception ex)
                {
                    _logger.Error($"Hata: {ex.Message}");
                }
            }
            else
            {
                _logger.Info(serviceName + " zaten çalışıyor.");
            }
        }
        private bool IsServiceRunning(string serviceName)
        {
            try
            {
                ServiceController sc = new ServiceController(serviceName);
                return sc.Status == ServiceControllerStatus.Running;

            }
            catch (Exception ex)
            {
                _logger.Error($"Hata: {ex.Message}");
            }
            return false;
        }

        private async Task<bool> IsHttpServiceControlling(string url)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    // Sertifika doğrulamasını devre dışı bırak
                    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                    var response = await httpClient.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        _logger.Info($"{url} servisi erişilebilir durumda. {response.StatusCode}");
                        return true;
                    }
                    else
                    {
                        _logger.Info($"{url} servisine erişilemedi. Durum kodu: {response.StatusCode}");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Hata: {ex.InnerException.Message}");
                return false;
            }
        }

        private List<ServiceSettings> ReadServiceSettings(string path)
        {
            string jsonContent = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<ServiceSettings>>(jsonContent);
        }

        private void OnSettingsFileChanged(object source, FileSystemEventArgs e)
        {
            var serviceSettings = ReadServiceSettings(filePath);

            foreach (var timer in timers)
            {
                timer.Stop();
                timer.Dispose();
            }
            timers.Clear();

            ApplySettings(serviceSettings);
        }

        private void ApplySettings(List<ServiceSettings> serviceSettings)
        {
            foreach (var serviceSetting in serviceSettings)
            {
                var timer = new Timer();
                timer.Interval = serviceSetting.Frequency * 60000;

                if (serviceSetting.ServiceType == Shared.Services.ServiceType.IIS)
                {
                    timer.Elapsed += async (object sender, ElapsedEventArgs e) =>
                    {
                        await CheckIISService(serviceSetting.ServiceName, serviceSetting.PingUrl);
                    };
                }
                else
                {
                    timer.Elapsed += (object sender, ElapsedEventArgs e) =>
                    {
                        CheckWindowsService(serviceSetting.ServiceName);
                    };
                }

                timer.Start();
                timers.Add(timer);
            }
        }
    }
}
