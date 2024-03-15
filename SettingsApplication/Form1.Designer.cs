namespace SettingsApplication
{
    partial class frm_settings
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lbl_serviceInfo = new Label();
            nupViewingFrequency = new NumericUpDown();
            lbl_viewingFrequency = new Label();
            lbl_logLevel = new Label();
            btn_save = new Button();
            cmb_serviceInfo = new ComboBox();
            txt_url = new TextBox();
            lbl_url = new Label();
            cmb_logLevel = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)nupViewingFrequency).BeginInit();
            SuspendLayout();
            // 
            // lbl_serviceInfo
            // 
            lbl_serviceInfo.AutoSize = true;
            lbl_serviceInfo.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lbl_serviceInfo.Location = new Point(47, 45);
            lbl_serviceInfo.Name = "lbl_serviceInfo";
            lbl_serviceInfo.Size = new Size(96, 20);
            lbl_serviceInfo.TabIndex = 1;
            lbl_serviceInfo.Text = "Service Info:";
            // 
            // nupViewingFrequency
            // 
            nupViewingFrequency.Location = new Point(219, 91);
            nupViewingFrequency.Name = "nupViewingFrequency";
            nupViewingFrequency.Size = new Size(235, 27);
            nupViewingFrequency.TabIndex = 4;
            nupViewingFrequency.ValueChanged += nupViewingFrequency_ValueChanged;
            // 
            // lbl_viewingFrequency
            // 
            lbl_viewingFrequency.AutoSize = true;
            lbl_viewingFrequency.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lbl_viewingFrequency.Location = new Point(47, 98);
            lbl_viewingFrequency.Name = "lbl_viewingFrequency";
            lbl_viewingFrequency.Size = new Size(145, 20);
            lbl_viewingFrequency.TabIndex = 3;
            lbl_viewingFrequency.Text = "Viewing Frequency:";
            // 
            // lbl_logLevel
            // 
            lbl_logLevel.AutoSize = true;
            lbl_logLevel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lbl_logLevel.Location = new Point(47, 150);
            lbl_logLevel.Name = "lbl_logLevel";
            lbl_logLevel.Size = new Size(79, 20);
            lbl_logLevel.TabIndex = 5;
            lbl_logLevel.Text = "Log Level:";
            // 
            // btn_save
            // 
            btn_save.BackColor = Color.DarkRed;
            btn_save.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btn_save.ForeColor = SystemColors.Control;
            btn_save.Location = new Point(43, 244);
            btn_save.Name = "btn_save";
            btn_save.Size = new Size(411, 40);
            btn_save.TabIndex = 7;
            btn_save.Text = "Save";
            btn_save.UseVisualStyleBackColor = false;
            btn_save.Click += btn_save_Click;
            // 
            // cmb_serviceInfo
            // 
            cmb_serviceInfo.FormattingEnabled = true;
            cmb_serviceInfo.Location = new Point(219, 42);
            cmb_serviceInfo.Name = "cmb_serviceInfo";
            cmb_serviceInfo.Size = new Size(235, 28);
            cmb_serviceInfo.TabIndex = 2;
            cmb_serviceInfo.SelectedIndexChanged += cmb_serviceInfo_SelectedIndexChanged;
            // 
            // txt_url
            // 
            txt_url.Location = new Point(219, 193);
            txt_url.Name = "txt_url";
            txt_url.Size = new Size(235, 27);
            txt_url.TabIndex = 8;
            txt_url.Visible = false;
            // 
            // lbl_url
            // 
            lbl_url.AutoSize = true;
            lbl_url.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lbl_url.Location = new Point(47, 200);
            lbl_url.Name = "lbl_url";
            lbl_url.Size = new Size(69, 20);
            lbl_url.TabIndex = 5;
            lbl_url.Text = "Ping Url:";
            lbl_url.Visible = false;
            // 
            // cmb_logLevel
            // 
            cmb_logLevel.FormattingEnabled = true;
            cmb_logLevel.Location = new Point(219, 142);
            cmb_logLevel.Name = "cmb_logLevel";
            cmb_logLevel.Size = new Size(235, 28);
            cmb_logLevel.TabIndex = 10;
            // 
            // frm_settings
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(480, 324);
            Controls.Add(cmb_logLevel);
            Controls.Add(txt_url);
            Controls.Add(cmb_serviceInfo);
            Controls.Add(btn_save);
            Controls.Add(lbl_url);
            Controls.Add(lbl_logLevel);
            Controls.Add(lbl_viewingFrequency);
            Controls.Add(nupViewingFrequency);
            Controls.Add(lbl_serviceInfo);
            Name = "frm_settings";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Settings Application";
            ((System.ComponentModel.ISupportInitialize)nupViewingFrequency).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lbl_serviceInfo;
        private NumericUpDown nupViewingFrequency;
        private Label lbl_viewingFrequency;
        private Label lbl_logLevel;
        private Button btn_save;
        private ComboBox cmb_serviceInfo;
        private TextBox txt_url;
        private Label lbl_url;
        private ComboBox cmb_logLevel;
    }
}
