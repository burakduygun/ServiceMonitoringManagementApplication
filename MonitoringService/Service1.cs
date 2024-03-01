using System;
using Shared.Logging;
using System.ServiceProcess;
using System.Timers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Web.Administration;
using System.Linq;
using System.Diagnostics;

namespace MonitoringService
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer;
        private readonly AbstractLogger _logger;
        //private readonly HttpClient _httpClient;

        public Service1(AbstractLogger logger)
        {
            _logger = logger;
            //_httpClient = new HttpClient();
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer = new Timer();
            timer.Interval = 10000;
            timer.Elapsed += CheckServices;
            timer.Start();
            _logger.Info("Monitoring service is running.");
        }

        protected override void OnStop()
        {
            timer.Stop();
            timer.Dispose();
            _logger.Info("Monitoring service is stopped.");
        }

        private async void CheckServices(object sender, ElapsedEventArgs e)
        {
            if (!IsServiceRunning("MockWindows"))
            {
                _logger.Info("MockWindows Service başlatılacak.");
                RestartService("MockWindows");
            }
            else
            {
                _logger.Info("MockWindows zaten çalışıyor.");
            }

            if (!await IsHttpServiceControlling("https://localhost:5011/api/Ping"))
            //if (!await IsHttpServiceControlling("https://localhost:7071/api/Ping"))
            {
                _logger.Info("WebApi başlatılacak.");
                RestartWebApi();
            }
            else
            {
                _logger.Info("WebApi zaten çalışıyor.");
            }
        }

        private void RestartService(string serviceName)
        {
            try
            {
                ServiceController sc = new ServiceController(serviceName);

                sc.Start();
                sc.WaitForStatus(ServiceControllerStatus.Running);

                _logger.Info($"{serviceName} servisi yeniden başlatıldı.");
            }
            catch (Exception ex)
            {
                _logger.Error($"Hata: {ex.Message}");
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

        private void RestartWebApi()
        {
            try
            {
                var server = new ServerManager();
                var site = server.Sites.FirstOrDefault(s => s.Name == "WebApi");

                if (site != null)
                {
                    site.Start();
                    _logger.Info($"WebApi servisi yeniden başlatılıyor.");

                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Hata: {ex.Message}");
                return;
            }
            _logger.Info($"WebApi servisi yeniden başlatıldı.");

          
        }
    }
}
