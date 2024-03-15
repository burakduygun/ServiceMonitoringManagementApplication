using Serilog;
using Shared.Services;
using System.Runtime;
using System.ServiceProcess;
using System.Xml;

namespace MonitoringService.ManageService
{
    public class MockWindowsService : IService
    {
        private readonly string _serviceName;
        private readonly ILogger _logger;
        private readonly ServiceSettings _settings;
        public string ServiceName { get; }

        public MockWindowsService(ServiceSettings serviceSetting, ILogger logger)
        {
            _serviceName = serviceSetting.ServiceName;
            _logger = logger;
            ServiceName = serviceSetting.ServiceName;
            _settings = serviceSetting;
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
