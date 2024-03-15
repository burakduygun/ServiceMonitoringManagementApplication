using MonitoringService;
using MonitoringService.ManageService;
using Moq;
using Serilog;
using Shared.Services;
using Shared.Logging;

namespace ServiceMonitoringManagementApplication.Test
{
    public class MonitoringServiceTest
    {
        [Fact]
        public void ApplySettings_CreatesTimersAccordingToServiceSettings()
        {
            // Arrange
            string serviceSettingsPath = Shared.PathAccess.ServiceSettingsPath;
            var loggerMock = new Mock<ILogger>();
            Service1 service = new Service1(loggerMock.Object);

            // Act
            var serviceSettings = Shared.Services.FileAccess.LoadServiceSettings(serviceSettingsPath);
            service.ApplySettings(serviceSettings);

            // Assert
            Assert.Equal(serviceSettings.Count, service.GetTimers().Count);
        }

        [Fact]
        public void CheckService_DoesNotRestartService_WhenServiceIsRunning()
        {
            // Arrange
            var loggerMock = new Mock<ILogger>();
            var mockService = new Mock<IService>();
            mockService.Setup(m => m.CheckStatus()).Returns(true);
            mockService.Setup(m => m.ServiceName).Returns("MockWindows");
            var service1 = new Service1(loggerMock.Object);

            // Act
            service1.CheckService(mockService.Object, 3);

            // Assert
            loggerMock.Verify(logger => logger.Information("MockWindows çalışıyor."), Times.Once);
            mockService.Verify(service => service.RestartService(), Times.Never);
        }


        [Fact]
        public void CheckService_RestartsService_WhenServiceIsNotRunning()
        {
            // Arrange
            var loggerMock = new Mock<ILogger>();
            var mockService = new Mock<IService>();
            mockService.Setup(m => m.CheckStatus()).Returns(false);
            mockService.Setup(m => m.ServiceName).Returns("MockWindows");
            var service = new Service1(loggerMock.Object);

            // Act
            service.CheckService(mockService.Object, 3);

            // Assert
            loggerMock.Verify(logger => logger.Information("MockWindows servisi yeniden başlatılıyor."), Times.Exactly(4));
        }

        [Fact]
        public void StartMonitoringService_Success()
        {
            // Arrange
            var loggerMock = new Mock<ILogger>();
            var service = new Service1(loggerMock.Object);

            // Act
            service.StartMonitoringService();

            // Assert
            Assert.NotNull(service.GetWatcher());
            loggerMock.Verify(x => x.Information("Monitoring servis başlatılıyor."), Times.Once);
        }

        [Fact]
        public void StopMonitoringService_StopsAndDisposesTimers()
        {
            // Arrange
            var loggerMock = new Mock<ILogger>();
            var service = new Service1(loggerMock.Object);
            var timer1 = new System.Timers.Timer();
            var timer2 = new System.Timers.Timer();

            service.SetTimer(timer1);
            service.SetTimer(timer2);

            // Act
            service.StopMonitoringService();

            // Assert
            Assert.Empty(service.GetTimers());
            loggerMock.Verify(x => x.Information("Monitoring servis durduruldu."), Times.Once);
        }

        [Fact]
        public void CheckService_LogsError_WhenExceptionOccurs()
        {
            // Arrange
            var loggerMock = new Mock<ILogger>();
            var mockService = new Mock<IService>();
            mockService.Setup(m => m.CheckStatus()).Throws(new Exception("Test exception"));
            mockService.Setup(m => m.ServiceName).Returns("MockWindows");
            var service1 = new Service1(loggerMock.Object);

            // Act
            service1.CheckService(mockService.Object, 3);

            // Assert
            loggerMock.Verify(logger => logger.Error("Test exception"), Times.Once);
        }

        [Fact]
        public void CheckStatus_ShouldReturnFalse_WhenServiceIsNotRunning()
        {
            // Arrange
            string serviceName = "MockWindows";
            var mockService = new MockWindowsService(serviceName, null);

            // Act
            var isRunning = mockService.CheckStatus();

            // Assert
            Assert.False(isRunning);
        }

        [Fact]
        public void OnSettingsFileChanged_LoadsNewSettingsAndAppliesChanges()
        {
            // Arrange
            var loggerMock = new Mock<ILogger>();
            var service = new Service1(loggerMock.Object);
            var oldSettings = Shared.Services.FileAccess.LoadServiceSettings(Shared.PathAccess.ServiceSettingsPath);

            var newSettings = new List<ServiceSettings>
            {
                new ServiceSettings { ServiceName = "WebApi", ServiceType = Shared.Services.ServiceType.IIS,Frequency = 3, PingUrl = "https://localhost:5011/api/Ping",LogLevel = LogLevel.Info  },
                new ServiceSettings { ServiceName = "MockWindows", ServiceType = Shared.Services.ServiceType.WindowsService, Frequency = 4,LogLevel = LogLevel.Info },
            };

            Shared.Services.FileAccess.SaveServiceSettings(newSettings, Shared.PathAccess.ServiceSettingsPath);

            service.OnSettingsFileChanged(null, null);

            Assert.NotEqual(oldSettings, newSettings);
        }


        [Fact]
        public void CheckStatus_ReturnsTrue_WhenPingUrlIsreachable()
        {
            // Arrange
            var serviceName = "WebApi";
            var pingUrl = "https://localhost:5011/api/Ping";
            var iisService = new IisService(serviceName, pingUrl, null);

            // Act
            var result = iisService.CheckStatus();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void RestartService_ThrowsOnNonexistentSite()
        {
            // Arrange
            var serviceName = "NonexistentSite";
            var pingUrl = "https://localhost:5000";
            var loggerMock = new Mock<ILogger>();

            var service = new IisService(serviceName, pingUrl, loggerMock.Object);

            // Act
            service.RestartService();

            // Act & Assert
            loggerMock.Verify(x => x.Warning(It.IsAny<string>()), Times.Never);
        }
    }
}