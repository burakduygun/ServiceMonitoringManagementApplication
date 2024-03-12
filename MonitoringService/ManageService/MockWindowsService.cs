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
        private readonly ILogger _logger;
        public string ServiceName { get; }

        public MockWindowsService(string serviceName, ILogger logger)
        {
            _serviceName = serviceName;
            _logger = logger;
            ServiceName = serviceName;
        }

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
        }
    }
}
