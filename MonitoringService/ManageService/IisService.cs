using Microsoft.Web.Administration;
using System;
using System.Linq;
using System.Net.Http;
using System.Net;
using Serilog;
using Shared.Services;
using System.Text.Json;
using System.IO;

namespace MonitoringService.ManageService
{
    public class IisService : IService
    {
        private readonly string _serviceName;
        private readonly string _pingUrl;
        private readonly ILogger _logger;
        private readonly ServiceSettings _settings;
        public string ServiceName { get; }

        public IisService(ServiceSettings serviceSetting, ILogger logger)
        {
            _serviceName = serviceSetting.ServiceName;
            ServiceName = serviceSetting.ServiceName;
            _pingUrl = serviceSetting.PingUrl;
            _logger = logger;
            _settings = serviceSetting;
        }

        public bool CheckStatus()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {

                    var task = httpClient.GetAsync(_pingUrl);
                    var response = task.Result;

                    return response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        public void RestartService()
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                var server = new ServerManager();
                var site = server.Sites.FirstOrDefault(s => s.Name == _serviceName);

                if (site != null)
                {
                    site.Start();
                    _logger.Information($"{_serviceName} servisi yeniden başlatılıyor.");
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Hata: {ex.Message}");
            }
        }
    }
}
