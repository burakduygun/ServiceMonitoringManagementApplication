using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringService.ManageService
{
    public interface IService
    {
        string ServiceName { get; }

        bool CheckStatus();
        void RestartService();
    }
}
