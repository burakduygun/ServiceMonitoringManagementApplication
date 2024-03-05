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
        private readonly AbstractLogger _logger;

        public MockWindowsService(string serviceName, AbstractLogger logger)
        {
            _serviceName = serviceName;
            _logger = logger;
        }
        public async Task CheckStatus()
        {
            try
            {
                ServiceController sc = new ServiceController(_serviceName);
                if (sc.Status != ServiceControllerStatus.Running)
                {
                    _logger.Info(_serviceName + " servisi yeniden başlatılıyor.");

                    sc.Start();
                    sc.WaitForStatus(ServiceControllerStatus.Running);

                    _logger.Info(_serviceName + " servisi yeniden başlatıldı.");

                }
                else
                {
                    _logger.Info(_serviceName + " zaten çalışıyor.");
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Hata: {ex.Message}");
            }
        }
    }
}
