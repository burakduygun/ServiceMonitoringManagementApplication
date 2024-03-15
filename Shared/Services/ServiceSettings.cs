using Shared.Logging;

namespace Shared.Services
{
    public class ServiceSettings
    {
        public string ServiceName { get; set; }
        public ServiceType ServiceType { get; set; }
        public LogLevel LogLevel { get; set; }
        public int Frequency { get; set; }
        public string PingUrl { get; set; }
        //public string SettingsPath { get; set; }
    }
}
