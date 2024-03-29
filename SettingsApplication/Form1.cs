﻿using SettingsApplication.Settings;
using Shared.Logging;
using Shared.Services;

namespace SettingsApplication
{
    public partial class frm_settings : Form
    {
        public readonly SettingsManager _settingsManager;
        private string serviceSettingsPath = Shared.PathAccess.ServiceSettingsPath;

        public frm_settings()
        {
            InitializeComponent();

            _settingsManager = new SettingsManager(serviceSettingsPath);

            PopulateComboBox();
            btn_save.Enabled = false;
        }

        private void PopulateComboBox()
        {
            var serviceSettings = Shared.Services.FileAccess.LoadServiceSettings(serviceSettingsPath);

            foreach (var servicesetting in serviceSettings)
            {
                cmb_serviceInfo.Items.Add(servicesetting.ServiceName);
            }

            foreach (var loglevel in Enum.GetValues(typeof(LogLevel)))
            {
                cmb_logLevel.Items.Add(loglevel);
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
                selectedServiceSetting.LogLevel = Enum.Parse<LogLevel>(cmb_logLevel.Text);
            }

            _settingsManager.UpdateServiceSetting(selectedServiceSetting!);

            MessageBox.Show("Veriler dosyaya yazıldı.");

            ClearFormFields();
            btn_save.Enabled = false;
        }

        private void ClearFormFields()
        {
            cmb_serviceInfo.Text = "";
            cmb_logLevel.Text = "";
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

                cmb_logLevel.Text = selectedServiceSetting.LogLevel.ToString();

            }
            CheckFormCompletion();
        }

        public void nupViewingFrequency_ValueChanged(object sender, EventArgs e)
        {
            CheckFormCompletion();
        }

        public void CheckFormCompletion()
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
    }
}
