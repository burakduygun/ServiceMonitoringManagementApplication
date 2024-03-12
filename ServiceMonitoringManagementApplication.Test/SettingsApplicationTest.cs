using SettingsApplication;
using SettingsApplication.Settings;
using System.Configuration;
using System.Reflection;


namespace ServiceMonitoringManagementApplication.Test
{

    public class SettingsApplicationTest
    {
        [Fact]
        public void SaveButton_Click_ShouldUpdateJsonFile()
        {
            //Assembly me = Assembly.GetExecutingAssembly();
            //Configuration config = ConfigurationManager.OpenExeConfiguration(me.ManifestModule.Assembly.Location);
            //string serviceSettingsPath = config.AppSettings.Settings["ServiceSettingsPath"].Value;

            // Arrange
            string serviceSettingsPath = Shared.PathAccess.ServiceSettingsPath;
            var settingsManager = new SettingsManager(serviceSettingsPath);
            var initialServiceSettings = Shared.Services.FileAccess.LoadServiceSettings(serviceSettingsPath);
            var selectedServiceSetting = settingsManager.GetServiceSettingByName("MockWindows");

            // Act
            selectedServiceSetting.Frequency = 5;
            settingsManager.UpdateServiceSetting(selectedServiceSetting);

            // Assert
            var updatedServiceSettings = Shared.Services.FileAccess.LoadServiceSettings(serviceSettingsPath);
            Assert.NotEqual(initialServiceSettings, updatedServiceSettings);
        }

    }
}