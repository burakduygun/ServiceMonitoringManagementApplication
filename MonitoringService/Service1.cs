using Shared.Logging;
using System;
using System.ServiceProcess;
using System.Timers;

namespace MonitoringService
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer;

        private readonly AbstractLogger _logger;
        public Service1(AbstractLogger logger)
        {
            _logger = logger;
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

        private void CheckServices(object sender, ElapsedEventArgs e)
        {
            if (!IsServiceRunning("MockWindows"))
            {
                _logger.Info("MockWindows Service başlatılacak.");
                RestartService("MockWindows");
            }

            _logger.Info("MockWindows zaten çalışıyor.");
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
    }
}
