using System.Windows.Forms;

namespace SettingsApplication
{
    public partial class frm_settings : Form
    {
        public frm_settings()
        {
            InitializeComponent();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            string logLevel = cmb_logLevel.Text;

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string settingsPath = Path.Combine(desktopPath, "settings");
            string filePath = Path.Combine(settingsPath, "data.txt");

            if (!Directory.Exists(settingsPath))
            {
                Directory.CreateDirectory(settingsPath);
            }

            using (StreamWriter writer = File.Exists(filePath) ? File.AppendText(filePath) : new StreamWriter(filePath))
            {
                string serviceInfo = cmb_serviceInfo.Text;
                int frequency = (int)nupViewingFrequency.Value;

                writer.WriteLine($"{serviceInfo}, {frequency}");
            }

            MessageBox.Show("Veriler dosyaya yazıldı.");
        }
    }
}
