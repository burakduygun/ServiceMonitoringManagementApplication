using SettingsApplication;
using SettingsApplication.Settings;

namespace ServiceMonitoringManagementApplication.Test
{
    public class SettingsApplicationTest
    {

        [Fact]
        public void SaveButton_Click_ShouldUpdateJsonFile()
        {
            // Arrange
            var settingsManager = new SettingsManager("C:\\Users\\Burak.Duygun\\OneDrive - Logo\\Desktop\\settings\\servicesettings.json");
            var initialServiceSettings = Shared.Services.FileAccess.LoadServiceSettings("C:\\Users\\Burak.Duygun\\OneDrive - Logo\\Desktop\\settings\\servicesettings.json");
            var selectedServiceSetting = settingsManager.GetServiceSettingByName("MockWindows");

            // Act
            selectedServiceSetting.Frequency = 10;
            settingsManager.UpdateServiceSetting(selectedServiceSetting);

            // Assert
            var updatedServiceSettings = Shared.Services.FileAccess.LoadServiceSettings("C:\\Users\\Burak.Duygun\\OneDrive - Logo\\Desktop\\settings\\servicesettings.json");
            Assert.NotEqual(initialServiceSettings, updatedServiceSettings);
        }
    }
}