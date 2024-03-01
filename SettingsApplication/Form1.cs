using System.Windows.Forms;

namespace SettingsApplication
{
    public partial class frm_settings : Form
    {
        public frm_settings()
        {
            InitializeComponent();

            btn_start.Enabled = false;
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            //string logLevel = cmb_logLevel.Text;

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string settingsPath = Path.Combine(desktopPath, "settings");
            string filePath = Path.Combine(settingsPath, "data.txt");

            if (!Directory.Exists(settingsPath))
            {
                Directory.CreateDirectory(settingsPath);
            }

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                string serviceInfo = cmb_serviceInfo.Text;
                int frequency = (int)nupViewingFrequency.Value;

                writer.WriteLine($"{serviceInfo}, {frequency}");
            }

            MessageBox.Show("Veriler dosyaya yazıldı.");

            cmb_serviceInfo.Text = "";
            cmb_logLevel.Text = "";
            nupViewingFrequency.Value = 0;
            btn_start.Enabled = false;
        }

        private void CheckFormCompletion()
        {
            if (!string.IsNullOrWhiteSpace(cmb_logLevel.Text) &&
                !string.IsNullOrWhiteSpace(cmb_serviceInfo.Text) &&
                nupViewingFrequency.Value > 0)
            {
                btn_start.Enabled = true;
            }
            else
            {
                btn_start.Enabled = false;
            }
        }

        private void cmb_serviceInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
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
