namespace GameStopwatch
{
    partial class FrmMain
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
            Label label2;
            Label label3;
            Label label4;
            Label label5;
            ctxTimeTotal = new ContextMenuStrip(components);
            tsmiResetTotalTime = new ToolStripMenuItem();
            tsmiChangeBeforeTime = new ToolStripMenuItem();
            tsmiCountInCurrent = new ToolStripMenuItem();
            tim = new System.Windows.Forms.Timer(components);
            lblMinutes = new Label();
            cmbVoices = new ComboBox();
            lblMinutesTotal = new Label();
            btnPastValues = new Button();
            lblCurrentDate = new Label();
            lblLastBackup = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            ctxTimeTotal.SuspendLayout();
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
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 14);
            label2.Name = "label2";
            label2.Size = new Size(127, 15);
            label2.TabIndex = 2;
            label2.Text = "Gaming Time (current)";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ContextMenuStrip = ctxTimeTotal;
            label3.Cursor = Cursors.Hand;
            label3.Location = new Point(15, 78);
            label3.Name = "label3";
            label3.Size = new Size(113, 15);
            label3.TabIndex = 5;
            label3.Text = "Gaming Time (total)";
            // 
            // ctxTimeTotal
            // 
            ctxTimeTotal.Items.AddRange(new ToolStripItem[] { tsmiResetTotalTime, tsmiChangeBeforeTime, tsmiCountInCurrent });
            ctxTimeTotal.Name = "ctxTimeTotal";
            ctxTimeTotal.Size = new Size(222, 70);
            // 
            // tsmiResetTotalTime
            // 
            tsmiResetTotalTime.Name = "tsmiResetTotalTime";
            tsmiResetTotalTime.Size = new Size(221, 22);
            tsmiResetTotalTime.Text = "Reset Total Time (New Date)";
            tsmiResetTotalTime.Click += TsmiResetTotalTime_Click;
            // 
            // tsmiChangeBeforeTime
            // 
            tsmiChangeBeforeTime.Name = "tsmiChangeBeforeTime";
            tsmiChangeBeforeTime.Size = new Size(221, 22);
            tsmiChangeBeforeTime.Text = "Change Before Time";
            tsmiChangeBeforeTime.Click += TsmiChangeBeforeTime_Click;
            // 
            // tsmiCountInCurrent
            // 
            tsmiCountInCurrent.Checked = true;
            tsmiCountInCurrent.CheckOnClick = true;
            tsmiCountInCurrent.CheckState = CheckState.Checked;
            tsmiCountInCurrent.Name = "tsmiCountInCurrent";
            tsmiCountInCurrent.Size = new Size(221, 22);
            tsmiCountInCurrent.Text = "Count in Current Time";
            tsmiCountInCurrent.CheckedChanged += TsmiCountInCurrent_CheckedChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ContextMenuStrip = ctxTimeTotal;
            label4.Location = new Point(170, 78);
            label4.Name = "label4";
            label4.Size = new Size(74, 15);
            label4.TabIndex = 8;
            label4.Text = "Current Date";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(15, 142);
            label5.Name = "label5";
            label5.Size = new Size(70, 15);
            label5.TabIndex = 10;
            label5.Text = "Last Backup";
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
            lblMinutes.Location = new Point(12, 28);
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
            // lblMinutesTotal
            // 
            lblMinutesTotal.AutoSize = true;
            lblMinutesTotal.ContextMenuStrip = ctxTimeTotal;
            lblMinutesTotal.Cursor = Cursors.Hand;
            lblMinutesTotal.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMinutesTotal.Location = new Point(12, 92);
            lblMinutesTotal.Name = "lblMinutesTotal";
            lblMinutesTotal.Size = new Size(104, 30);
            lblMinutesTotal.TabIndex = 4;
            lblMinutesTotal.Text = "0 minutes";
            // 
            // btnPastValues
            // 
            btnPastValues.Location = new Point(167, 160);
            btnPastValues.Name = "btnPastValues";
            btnPastValues.Size = new Size(102, 23);
            btnPastValues.TabIndex = 6;
            btnPastValues.Text = "Past values...";
            btnPastValues.UseVisualStyleBackColor = true;
            btnPastValues.Click += BtnPastValues_Click;
            // 
            // lblCurrentDate
            // 
            lblCurrentDate.AutoSize = true;
            lblCurrentDate.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCurrentDate.Location = new Point(167, 92);
            lblCurrentDate.Name = "lblCurrentDate";
            lblCurrentDate.Size = new Size(21, 30);
            lblCurrentDate.TabIndex = 7;
            lblCurrentDate.Text = "/";
            // 
            // lblLastBackup
            // 
            lblLastBackup.AutoSize = true;
            lblLastBackup.Cursor = Cursors.Hand;
            lblLastBackup.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLastBackup.Location = new Point(12, 156);
            lblLastBackup.Name = "lblLastBackup";
            lblLastBackup.Size = new Size(117, 30);
            lblLastBackup.TabIndex = 9;
            lblLastBackup.Text = "2025-02-05";
            lblLastBackup.Click += LblLastBackup_Click;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(340, 192);
            Controls.Add(label5);
            Controls.Add(lblLastBackup);
            Controls.Add(label4);
            Controls.Add(lblCurrentDate);
            Controls.Add(btnPastValues);
            Controls.Add(label3);
            Controls.Add(lblMinutesTotal);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cmbVoices);
            Controls.Add(lblMinutes);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Game Stopwatch";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ctxTimeTotal.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer tim;
        private Label lblMinutes;
        private ComboBox cmbVoices;
        private Label label1;
        private Label lblMinutesTotal;
        private ContextMenuStrip ctxTimeTotal;
        private ToolStripMenuItem tsmiCountInCurrent;
        private ToolStripMenuItem tsmiResetTotalTime;
        private ToolStripMenuItem tsmiChangeBeforeTime;
        private Button btnPastValues;
        private Label lblCurrentDate;
        private Label lblLastBackup;
    }
}
