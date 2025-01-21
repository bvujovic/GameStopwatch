namespace GameStopwatch
{
    partial class FrmChangeBeforeTime
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Label label2;
            numMinutesBefore = new NumericUpDown();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)numMinutesBefore).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 14);
            label2.Name = "label2";
            label2.Size = new Size(114, 15);
            label2.TabIndex = 3;
            label2.Text = "Time in app (before)";
            // 
            // numMinutesBefore
            // 
            numMinutesBefore.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            numMinutesBefore.Location = new Point(15, 32);
            numMinutesBefore.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numMinutesBefore.Name = "numMinutesBefore";
            numMinutesBefore.Size = new Size(114, 23);
            numMinutesBefore.TabIndex = 4;
            numMinutesBefore.KeyDown += NumMinutesBefore_KeyDown;
            // 
            // FrmChangeBeforeTime
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(210, 74);
            Controls.Add(numMinutesBefore);
            Controls.Add(label2);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "FrmChangeBeforeTime";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Change Before Time";
            ((System.ComponentModel.ISupportInitialize)numMinutesBefore).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown numMinutesBefore;
    }
}