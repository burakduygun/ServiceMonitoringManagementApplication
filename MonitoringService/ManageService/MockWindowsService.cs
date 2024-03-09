using Serilog;
using Shared.Logging;
using System;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringService.ManageService
{
    public class MockWindowsService : IService
    {
        private readonly string _serviceName;
        //private readonly AbstractLogger _logger;
        private readonly ILogger _logger;
        public string ServiceName { get; }

        public MockWindowsService(string serviceName, ILogger logger)
        {
            _serviceName = serviceName;
            _logger = logger;
            ServiceName = serviceName;
        }

        //public async Task CheckStatussss()
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error($"Hata: {ex.Message}");
        //    }
        //}

        public void RestartService()
        {
            ServiceController sc = new ServiceController(_serviceName);
            sc.Start();
            sc.WaitForStatus(ServiceControllerStatus.Running);
        }

        public bool CheckStatus()
        {
            ServiceController sc = new ServiceController(_serviceName);
            return sc.Status == ServiceControllerStatus.Running;

            //if (sc.Status != ServiceControllerStatus.Running)
            //{
            //    _logger.Information(_serviceName + " servisi yeniden başlatılıyor.");
            //    sc.Start();
            //    sc.WaitForStatus(ServiceControllerStatus.Running);

            //    _logger.Information(_serviceName + " servisi yeniden başlatıldı.");
            //}
            //else
            //{
            //    _logger.Information(_serviceName + " zaten çalışıyor.");
            //}

        }
    }
}
