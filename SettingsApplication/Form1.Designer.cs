﻿namespace SettingsApplication
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
            cmb_logLevel = new ComboBox();
            btn_start = new Button();
            btn_stop = new Button();
            cmb_serviceInfo = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)nupViewingFrequency).BeginInit();
            SuspendLayout();
            // 
            // lbl_serviceInfo
            // 
            lbl_serviceInfo.AutoSize = true;
            lbl_serviceInfo.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lbl_serviceInfo.Location = new Point(99, 88);
            lbl_serviceInfo.Name = "lbl_serviceInfo";
            lbl_serviceInfo.Size = new Size(96, 20);
            lbl_serviceInfo.TabIndex = 1;
            lbl_serviceInfo.Text = "Service Info:";
            // 
            // nupViewingFrequency
            // 
            nupViewingFrequency.Location = new Point(273, 134);
            nupViewingFrequency.Name = "nupViewingFrequency";
            nupViewingFrequency.Size = new Size(172, 27);
            nupViewingFrequency.TabIndex = 4;
            // 
            // lbl_viewingFrequency
            // 
            lbl_viewingFrequency.AutoSize = true;
            lbl_viewingFrequency.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lbl_viewingFrequency.Location = new Point(99, 141);
            lbl_viewingFrequency.Name = "lbl_viewingFrequency";
            lbl_viewingFrequency.Size = new Size(145, 20);
            lbl_viewingFrequency.TabIndex = 3;
            lbl_viewingFrequency.Text = "Viewing Frequency:";
            // 
            // lbl_logLevel
            // 
            lbl_logLevel.AutoSize = true;
            lbl_logLevel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lbl_logLevel.Location = new Point(99, 193);
            lbl_logLevel.Name = "lbl_logLevel";
            lbl_logLevel.Size = new Size(79, 20);
            lbl_logLevel.TabIndex = 5;
            lbl_logLevel.Text = "Log Level:";
            // 
            // cmb_logLevel
            // 
            cmb_logLevel.FormattingEnabled = true;
            cmb_logLevel.Items.AddRange(new object[] { " Trace", " Debug", " Info", " Warn", " Error", " Fatal", " Off" });
            cmb_logLevel.Location = new Point(271, 190);
            cmb_logLevel.Name = "cmb_logLevel";
            cmb_logLevel.Size = new Size(174, 28);
            cmb_logLevel.TabIndex = 6;
            // 
            // btn_start
            // 
            btn_start.BackColor = Color.DarkRed;
            btn_start.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btn_start.ForeColor = SystemColors.Control;
            btn_start.Location = new Point(95, 277);
            btn_start.Name = "btn_start";
            btn_start.Size = new Size(172, 40);
            btn_start.TabIndex = 7;
            btn_start.Text = "Start";
            btn_start.UseVisualStyleBackColor = false;
            btn_start.Click += btn_start_Click;
            // 
            // btn_stop
            // 
            btn_stop.Enabled = false;
            btn_stop.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btn_stop.Location = new Point(273, 277);
            btn_stop.Name = "btn_stop";
            btn_stop.Size = new Size(172, 40);
            btn_stop.TabIndex = 8;
            btn_stop.Text = "Stop";
            btn_stop.UseVisualStyleBackColor = true;
            // 
            // cmb_serviceInfo
            // 
            cmb_serviceInfo.FormattingEnabled = true;
            cmb_serviceInfo.Items.AddRange(new object[] { "MockWindows", "WebApi" });
            cmb_serviceInfo.Location = new Point(271, 85);
            cmb_serviceInfo.Name = "cmb_serviceInfo";
            cmb_serviceInfo.Size = new Size(174, 28);
            cmb_serviceInfo.TabIndex = 2;
            // 
            // frm_settings
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(554, 413);
            Controls.Add(cmb_serviceInfo);
            Controls.Add(btn_stop);
            Controls.Add(btn_start);
            Controls.Add(cmb_logLevel);
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
        private ComboBox cmb_logLevel;
        private Button btn_start;
        private Button btn_stop;
        private ComboBox cmb_serviceInfo;
    }
}
