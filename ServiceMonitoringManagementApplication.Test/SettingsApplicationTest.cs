using SettingsApplication;
using SettingsApplication.Settings;
using Shared.Logging;
using Shared.Services;
using System.Text.Json;

namespace ServiceMonitoringManagementApplication.Test
{
    public class SettingsApplicationTest
    {

        [Fact]
        public void SaveButton_Click_ShouldUpdateJsonFile()
        {
            // Arrange
            var form = new frm_settings();
            var initialServiceSettings = Shared.Services.FileAccess.LoadServiceSettings("C:\\Users\\Burak.Duygun\\OneDrive - Logo\\Desktop\\settings\\servicesettings.json");
            var selectedServiceSetting = form._settingsManager.GetServiceSettingByName("MockWindows");

            // Act
            selectedServiceSetting.Frequency = 10;
            form._settingsManager.UpdateServiceSetting(selectedServiceSetting);

            // Assert
            var updatedServiceSettings = Shared.Services.FileAccess.LoadServiceSettings("C:\\Users\\Burak.Duygun\\OneDrive - Logo\\Desktop\\settings\\servicesettings.json");
            Assert.NotEqual(initialServiceSettings, updatedServiceSettings);
        }
    }
}