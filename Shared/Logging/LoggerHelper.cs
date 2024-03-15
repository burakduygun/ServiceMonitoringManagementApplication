using Serilog.Events;
using Shared.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Shared.Logging
{
    public class LoggerHelper
    {
        public static LogEventLevel GetLogLevel(string serviceName)
        {
            string serviceSettingsPath = Shared.PathAccess.ServiceSettingsPath;

            var serviceSettings = JsonSerializer.Deserialize<List<ServiceSettings>>(File.ReadAllText(serviceSettingsPath));

            var serviceSetting = serviceSettings.FirstOrDefault(s => s.ServiceName == serviceName);

            var minLogLevel = (Serilog.Events.LogEventLevel)serviceSetting.LogLevel;

            return minLogLevel;
        }
    }
}
