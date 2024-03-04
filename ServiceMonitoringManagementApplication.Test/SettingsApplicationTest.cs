using SettingsApplication;
using Shared.Logging;
using Shared.Services;
using System.Text.Json;

namespace ServiceMonitoringManagementApplication.Test
{
    public class SettingsApplicationTest
    {

        [Fact]
        public void CheckFormCompletion_WithCompleteForm_EnablesSaveButton()
        {
            // Arrange
            var form = new frm_settings();
            form.CmbLogLevel.Text = "Info";
            form.CmbServiceInfo.Text = "WebApi";
            form.NupViewingFrequency.Value = 2;

            // Act
            form.CheckFormCompletion();

            // Assert
            Assert.True(form.BtnSave.Enabled);
        }


        [Fact]
        public void NupViewingFrequency_ValueChanged_EnablesSaveButton()
        {
            // Arrange
            var form = new frm_settings();
            form.NupViewingFrequency.Value = 5;

            // Act
            form.nupViewingFrequency_ValueChanged(null, null);

            // Assert
            Assert.False(form.BtnSave.Enabled);
        }

        [Fact]
        public void Cmb_logLevel_SelectedIndexChanged_EnablesSaveButton()
        {
            // Arrange
            var form = new frm_settings();
            form.CmbLogLevel.Text = "Info";

            // Act
            form.cmb_logLevel_SelectedIndexChanged(null, null);

            // Assert
            Assert.False(form.BtnSave.Enabled);
        }

        [Fact]
        public void SaveButton_Click_ShouldUpdateJsonFile()
        {
            // Arrange
            var form = new frm_settings();
            var initialJsonContent = File.ReadAllText("C:\\Users\\Burak.Duygun\\OneDrive - Logo\\Desktop\\settings\\servicesettings.json");
            var initialServiceSettings = JsonSerializer.Deserialize<List<ServiceSettings>>(initialJsonContent);

            // Act
            var serviceName = "WebApi";
            var logLevel = LogLevel.Info;
            var frequency = 10;
            var pingUrl = "https://localhost:5011/api/Ping";
            form.CmbServiceInfo.SelectedItem = serviceName;
            form.CmbLogLevel.SelectedItem = logLevel;
            form.NupViewingFrequency.Value = frequency;
            form.TxtUrl.Text = pingUrl;
            form.btn_save_Click(null, null);

            // Assert
            var updatedJsonContent = File.ReadAllText("C:\\Users\\Burak.Duygun\\OneDrive - Logo\\Desktop\\settings\\servicesettings.json");
            var updatedServiceSettings = JsonSerializer.Deserialize<List<ServiceSettings>>(updatedJsonContent);

            Assert.NotEqual(initialServiceSettings, updatedServiceSettings);
        }
    }
}