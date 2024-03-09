using SettingsApplication.Settings;
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
        public readonly SettingsManager _settingsManager;
        //string serviceSettingsPath = ConfigurationManager.AppSettings["ServiceSettingsPath"]!;
        string serviceSettingsPath = "C:\\Users\\Burak.Duygun\\OneDrive - Logo\\Desktop\\settings\\servicesettings.json";
        public frm_settings()
        {
            InitializeComponent();

            _settingsManager = new SettingsManager(serviceSettingsPath);

            PopulateServiceInfoComboBox();
            btn_save.Enabled = false;
        }

        private void PopulateServiceInfoComboBox()
        {
            var serviceSettings = Shared.Services.FileAccess.LoadServiceSettings(serviceSettingsPath);

            foreach (var servicesetting in serviceSettings)
            {
                cmb_serviceInfo.Items.Add(servicesetting.ServiceName);
            }
        }

        public void btn_save_Click(object sender, EventArgs e)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string settingsPath = Path.Combine(desktopPath, "settings");

            if (!Directory.Exists(settingsPath))
            {
                Directory.CreateDirectory(settingsPath);
            }

            var selectedServiceSetting = _settingsManager.GetServiceSettingByName(cmb_serviceInfo.Text);

            if (selectedServiceSetting != null)
            {
                selectedServiceSetting.Frequency = (int)nupViewingFrequency.Value;
                if (selectedServiceSetting.ServiceType == ServiceType.IIS)
                    selectedServiceSetting.PingUrl = txt_url.Text;
            }

            _settingsManager.UpdateServiceSetting(selectedServiceSetting!);

            MessageBox.Show("Veriler dosyaya yazıldı.");

            ClearFormFields();
            btn_save.Enabled = false;
        }

        private void ClearFormFields()
        {
            cmb_serviceInfo.Text = "";
            nupViewingFrequency.Value = 0;
            txt_url.Text = "";
        }

        public void cmb_serviceInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedServiceSetting = _settingsManager.GetServiceSettingByName(cmb_serviceInfo.Text);

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
    }
}
