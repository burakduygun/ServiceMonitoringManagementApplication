using Microsoft.Web.Administration;
using Shared.Logging;
using System;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using Serilog;

namespace MonitoringService.ManageService
{
    public class IisService : IService
    {
        private readonly string _serviceName;
        private readonly string _pingUrl;
        //private readonly AbstractLogger _logger;
        private readonly ILogger _logger;

        public IisService(string serviceName, string pingUrl, ILogger logger)
        {
            _serviceName = serviceName;
            _pingUrl = pingUrl;
            _logger = logger;
        }

        public async Task CheckStatus()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                    //var server = new ServerManager();
                    //var state = server.ApplicationPools["WebApi"].State;
                    //state == ObjectState.Started;

                    var response = await httpClient.GetAsync(_pingUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        //_logger.Info($"{_pingUrl} servisi erişilebilir durumda. {response.StatusCode}.");
                        _logger.Information($"{_pingUrl} servisi erişilebilir durumda. {response.StatusCode}.");
                    }
                }
            }
            catch (Exception ex)
            {
                //_logger.Error($"Hata: {ex.InnerException.Message}");
                _logger.Error($"Hata: {ex.InnerException.Message}");

                RestartService();

                //_logger.Info($"{_serviceName} başlatıldı.");
                _logger.Information($"{_serviceName} başlatıldı.");
            }
        }

        private void RestartService()
        {
            try
            {
                var server = new ServerManager();
                var site = server.Sites.FirstOrDefault(s => s.Name == _serviceName);

                if (site != null)
                {
                    site.Start();
                    //_logger.Info($"{_serviceName} servisi yeniden başlatılıyor.");
                    _logger.Information($"{_serviceName} servisi yeniden başlatılıyor.");
                }
            }
            catch (Exception ex)
            {
                //_logger.Error($"Hata: {ex.Message}");
                _logger.Error($"Hata: {ex.Message}");
            }
        }
    }
}
