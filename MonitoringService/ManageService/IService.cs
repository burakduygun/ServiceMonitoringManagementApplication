using System;

namespace MonitoringService.ManageService
{
    public interface IService
    {
        string ServiceName { get; }
        bool CheckStatus();
        void RestartService();
    }
}
