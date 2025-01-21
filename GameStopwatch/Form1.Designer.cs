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
            Label label2;
            Label label3;
            ctxTimeTotal = new ContextMenuStrip(components);
            tsmiCountInCurrent = new ToolStripMenuItem();
            tsmiResetTotalTime = new ToolStripMenuItem();
            tsmiChangeBeforeTime = new ToolStripMenuItem();
            tim = new System.Windows.Forms.Timer(components);
            lblMinutes = new Label();
            cmbVoices = new ComboBox();
            lblMinutesTotal = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
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
            label2.Size = new Size(118, 15);
            label2.TabIndex = 2;
            label2.Text = "Time in app (current)";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ContextMenuStrip = ctxTimeTotal;
            label3.Location = new Point(15, 78);
            label3.Name = "label3";
            label3.Size = new Size(104, 15);
            label3.TabIndex = 5;
            label3.Text = "Time in app (total)";
            // 
            // ctxTimeTotal
            // 
            ctxTimeTotal.Items.AddRange(new ToolStripItem[] { tsmiCountInCurrent, tsmiResetTotalTime, tsmiChangeBeforeTime });
            ctxTimeTotal.Name = "ctxTimeTotal";
            ctxTimeTotal.Size = new Size(193, 70);
            // 
            // tsmiCountInCurrent
            // 
            tsmiCountInCurrent.Checked = true;
            tsmiCountInCurrent.CheckOnClick = true;
            tsmiCountInCurrent.CheckState = CheckState.Checked;
            tsmiCountInCurrent.Name = "tsmiCountInCurrent";
            tsmiCountInCurrent.Size = new Size(192, 22);
            tsmiCountInCurrent.Text = "Count in Current Time";
            tsmiCountInCurrent.CheckedChanged += TsmiCountInCurrent_CheckedChanged;
            // 
            // tsmiResetTotalTime
            // 
            tsmiResetTotalTime.Name = "tsmiResetTotalTime";
            tsmiResetTotalTime.Size = new Size(192, 22);
            tsmiResetTotalTime.Text = "Reset Total Time";
            tsmiResetTotalTime.Click += TsmiResetTotalTime_Click;
            // 
            // tsmiChangeBeforeTime
            // 
            tsmiChangeBeforeTime.Name = "tsmiChangeBeforeTime";
            tsmiChangeBeforeTime.Size = new Size(192, 22);
            tsmiChangeBeforeTime.Text = "Change Before Time";
            tsmiChangeBeforeTime.Click += TsmiChangeBeforeTime_Click;
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
            lblMinutesTotal.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMinutesTotal.Location = new Point(12, 92);
            lblMinutesTotal.Name = "lblMinutesTotal";
            lblMinutesTotal.Size = new Size(104, 30);
            lblMinutesTotal.TabIndex = 4;
            lblMinutesTotal.Text = "0 minutes";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(340, 132);
            Controls.Add(label3);
            Controls.Add(lblMinutesTotal);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cmbVoices);
            Controls.Add(lblMinutes);
            MaximizeBox = false;
            Name = "Form1";
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
    }
}
