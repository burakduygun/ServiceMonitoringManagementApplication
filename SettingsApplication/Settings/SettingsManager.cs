using Shared.Services;
using System.Text.Json;
using FileAccess = Shared.Services.FileAccess;

namespace SettingsApplication.Settings
{
    public class SettingsManager
    {
        private readonly string _filePath;

        public SettingsManager(string filePath)
        {
            _filePath = filePath;
        }

        public ServiceSettings GetServiceSettingByName(string serviceName)
        {
            var serviceSettings = FileAccess.LoadServiceSettings(_filePath);
            return serviceSettings.Find(s => s.ServiceName == serviceName)!;
        }

        public void UpdateServiceSetting(ServiceSettings updatedSetting)
        {
            var serviceSettings = FileAccess.LoadServiceSettings(_filePath);
            int index = serviceSettings.FindIndex(s => s.ServiceName == updatedSetting.ServiceName);
            if (index != -1)
            {
                serviceSettings[index] = updatedSetting;
                FileAccess.SaveServiceSettings(serviceSettings, _filePath);
            }
        }
    }
}
