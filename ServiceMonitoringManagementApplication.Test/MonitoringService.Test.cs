using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonitoringService;
using MonitoringService.ManageService;
using Shared.Services;
using Moq;
using Serilog;

namespace ServiceMonitoringManagementApplication.Test
{
    public class MonitoringService
    {
        [Fact]
        public void ApplySettings_CreatesTimersAccordingToServiceSettings()
        {
            var loggerMock = new Mock<ILogger>();
            Service1 service = new Service1(loggerMock.Object);

            //var service = new Service1(logger);
            var serviceSettings = Shared.Services.FileAccess.LoadServiceSettings("C:\\Users\\Burak.Duygun\\OneDrive - Logo\\Desktop\\settings\\servicesettings.json");
            
            service.ApplySettings(serviceSettings);
            
            var asd = service.timers.Count;

            Assert.Equal(2, service.timers.Count);
            //Assert.Empty(service.timers);

        }
    }
}
