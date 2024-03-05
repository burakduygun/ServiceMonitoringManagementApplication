using Shared.Logging;
using Shared.Services;
using System.Configuration;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace SettingsApplication
{
    public partial class frm_settings : Form
    {
        public TextBox TxtUrl => txt_url;
        public Label LblUrl => lbl_url;

        public ComboBox CmbServiceInfo => cmb_serviceInfo;
        public NumericUpDown NupViewingFrequency => nupViewingFrequency;
        //public ComboBox CmbLogLevel => cmb_logLevel;
        public Button BtnSave => btn_save;

        private readonly List<ServiceSettings> _serviceSettings;
        string serviceSettingsPath = ConfigurationManager.AppSettings["ServiceSettingsPath"]!;

        public frm_settings()
        {
            InitializeComponent();

            string jsonContent = File.ReadAllText(serviceSettingsPath);
            _serviceSettings = JsonSerializer.Deserialize<List<ServiceSettings>>(jsonContent)!;

            foreach (var servicesetting in _serviceSettings)
            {
                cmb_serviceInfo.Items.Add(servicesetting.ServiceName);
            }

            btn_save.Enabled = false;
        }

        public void btn_save_Click(object sender, EventArgs e)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string settingsPath = Path.Combine(desktopPath, "settings");

            if (!Directory.Exists(settingsPath))
            {
                Directory.CreateDirectory(settingsPath);
            }

            var selectedServiceSetting = _serviceSettings.Find(s => s.ServiceName == cmb_serviceInfo.Text);

            if (selectedServiceSetting != null)
            {
                selectedServiceSetting.Frequency = (int)nupViewingFrequency.Value;
                if (selectedServiceSetting.ServiceType == ServiceType.IIS)
                    selectedServiceSetting.PingUrl = txt_url.Text;
            }

            string jsonString = JsonSerializer.Serialize(_serviceSettings);
            File.WriteAllText(serviceSettingsPath, jsonString);

            MessageBox.Show("Veriler dosyaya yazıldı.");

            cmb_serviceInfo.Text = "";
            txt_logLevel.Text = "";
            nupViewingFrequency.Value = 0;
            txt_url.Text = "";
            btn_save.Enabled = false;
        }

        public void CheckFormCompletion()
        {
            if (!string.IsNullOrWhiteSpace(txt_logLevel.Text) &&
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
        public void cmb_serviceInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedServiceSetting = _serviceSettings.Find(s => s.ServiceName == cmb_serviceInfo.Text);
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

                txt_logLevel.Text = selectedServiceSetting.LogLevel.ToString();
            }
            CheckFormCompletion();
        }

        public void nupViewingFrequency_ValueChanged(object sender, EventArgs e)
        {
            CheckFormCompletion();
        }
    }
}
