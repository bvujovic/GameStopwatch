namespace GameStopwatch
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            Label label1;
            tim = new System.Windows.Forms.Timer(components);
            lblMinutes = new Label();
            cmbVoices = new ComboBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(167, 14);
            label1.Name = "label1";
            label1.Size = new Size(35, 15);
            label1.TabIndex = 2;
            label1.Text = "Voice";
            // 
            // tim
            // 
            tim.Enabled = true;
            tim.Interval = 50;
            tim.Tick += Tim_Tick;
            // 
            // lblMinutes
            // 
            lblMinutes.AutoSize = true;
            lblMinutes.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMinutes.Location = new Point(12, 19);
            lblMinutes.Name = "lblMinutes";
            lblMinutes.Size = new Size(104, 30);
            lblMinutes.TabIndex = 0;
            lblMinutes.Text = "0 minutes";
            // 
            // cmbVoices
            // 
            cmbVoices.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbVoices.FormattingEnabled = true;
            cmbVoices.Items.AddRange(new object[] { "Microsoft David Desktop", "Microsoft Zira Desktop" });
            cmbVoices.Location = new Point(167, 32);
            cmbVoices.Name = "cmbVoices";
            cmbVoices.Size = new Size(161, 23);
            cmbVoices.TabIndex = 1;
            cmbVoices.SelectedIndexChanged += CmbVoices_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(340, 67);
            Controls.Add(label1);
            Controls.Add(cmbVoices);
            Controls.Add(lblMinutes);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Game Stopwatch";
            WindowState = FormWindowState.Minimized;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer tim;
        private Label lblMinutes;
        private ComboBox cmbVoices;
        private Label label1;
    }
}
