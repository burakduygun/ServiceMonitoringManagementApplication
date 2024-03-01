using Shared.Logging;
using Shared.Services;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace SettingsApplication
{
    public partial class frm_settings : Form
    {
        private readonly List<ServiceSettings> _serviceSettings;
        public frm_settings()
        {
            InitializeComponent();

            string jsonContent = File.ReadAllText("C:\\Users\\Burak.Duygun\\OneDrive - Logo\\Desktop\\settings\\servicesettings.json");
            _serviceSettings = JsonSerializer.Deserialize<List<ServiceSettings>>(jsonContent)!;

            foreach (var servicesetting in _serviceSettings)
            {
                cmb_serviceInfo.Items.Add(servicesetting.ServiceName);
            }
            foreach (var level in Enum.GetValues(typeof(LogLevel)))
            {
                cmb_logLevel.Items.Add(level);
            }

            btn_save.Enabled = false;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string settingsPath = Path.Combine(desktopPath, "settings");
            string filePath = "servicesettings.json";

            if (!Directory.Exists(settingsPath))
            {
                Directory.CreateDirectory(settingsPath);
            }

            var selectedServiceSetting = _serviceSettings.FirstOrDefault(s => s.ServiceName == cmb_serviceInfo.Text);

            if (selectedServiceSetting != null)
            {
                selectedServiceSetting.LogLevel = Enum.Parse<LogLevel>(cmb_logLevel.Text);
                selectedServiceSetting.Frequency = (int)nupViewingFrequency.Value;
                if (selectedServiceSetting.ServiceType == ServiceType.IIS)
                    selectedServiceSetting.PingUrl = txt_url.Text;
            }

            string jsonString = JsonSerializer.Serialize(_serviceSettings);
            File.WriteAllText("C:\\Users\\Burak.Duygun\\OneDrive - Logo\\Desktop\\settings\\servicesettings.json", jsonString);

            MessageBox.Show("Veriler dosyaya yazıldı.");

            cmb_serviceInfo.Text = "";
            cmb_logLevel.Text = "";
            nupViewingFrequency.Value = 0;
            txt_url.Text = "";
            btn_save.Enabled = false;
        }

        private void CheckFormCompletion()
        {
            if (!string.IsNullOrWhiteSpace(cmb_logLevel.Text) &&
                !string.IsNullOrWhiteSpace(cmb_serviceInfo.Text) &&
                nupViewingFrequency.Value > 0)
            {
                btn_save.Enabled = true;
            }
            else
            {
                btn_save.Enabled = false;
            }
        }

        private void cmb_serviceInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedServiceSetting = _serviceSettings.FirstOrDefault(s => s.ServiceName == cmb_serviceInfo.Text);
            if (selectedServiceSetting != null)
            {
                if (selectedServiceSetting.ServiceType == ServiceType.IIS)
                {
                    txt_url.Visible = true;
                    lbl_url.Visible = true;

                    txt_url.Text = selectedServiceSetting.PingUrl;
                }
                else
                {
                    txt_url.Visible = false;
                    lbl_url.Visible = false;
                    txt_url.Text = "";
                }

                nupViewingFrequency.Value = selectedServiceSetting.Frequency;

                cmb_logLevel.SelectedItem = selectedServiceSetting.LogLevel;
            }
            CheckFormCompletion();
        }

        private void cmb_logLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckFormCompletion();
        }
        private void nupViewingFrequency_ValueChanged(object sender, EventArgs e)
        {
            CheckFormCompletion();
        }
    }
}
