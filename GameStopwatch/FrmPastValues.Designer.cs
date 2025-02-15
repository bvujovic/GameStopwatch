namespace GameStopwatch
{
    partial class FrmPastValues
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
            components = new System.ComponentModel.Container();
            ToolStripStatusLabel toolStripStatusLabel1;
            ToolStripStatusLabel toolStripStatusLabel2;
            statusStrip1 = new StatusStrip();
            lblCount = new ToolStripStatusLabel();
            lblAvg = new ToolStripStatusLabel();
            bs = new BindingSource(components);
            dgv = new DataGridView();
            webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            cmbFilter = new ComboBox();
            btnFilterReset = new Button();
            chkIncludeCurrent = new CheckBox();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bs).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            ((System.ComponentModel.ISupportInitialize)webView).BeginInit();
            SuspendLayout();
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(43, 17);
            toolStripStatusLabel1.Text = "Count:";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(53, 17);
            toolStripStatusLabel2.Text = "Average:";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, lblCount, toolStripStatusLabel2, lblAvg });
            statusStrip1.Location = new Point(0, 428);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1049, 22);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblCount
            // 
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(13, 17);
            lblCount.Text = "0";
            // 
            // lblAvg
            // 
            lblAvg.Name = "lblAvg";
            lblAvg.Size = new Size(12, 17);
            lblAvg.Text = "/";
            // 
            // dgv
            // 
            dgv.AllowUserToAddRows = false;
            dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Location = new Point(12, 74);
            dgv.Name = "dgv";
            dgv.Size = new Size(363, 351);
            dgv.TabIndex = 2;
            dgv.SelectionChanged += Dgv_SelectionChanged;
            // 
            // webView
            // 
            webView.AllowExternalDrop = true;
            webView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            webView.CreationProperties = null;
            webView.DefaultBackgroundColor = Color.White;
            webView.Location = new Point(381, 12);
            webView.Name = "webView";
            webView.Size = new Size(656, 413);
            webView.TabIndex = 3;
            webView.ZoomFactor = 1D;
            webView.Resize += WebView_Resize;
            // 
            // cmbFilter
            // 
            cmbFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilter.FormattingEnabled = true;
            cmbFilter.Location = new Point(12, 12);
            cmbFilter.Name = "cmbFilter";
            cmbFilter.Size = new Size(191, 23);
            cmbFilter.TabIndex = 4;
            cmbFilter.SelectedIndexChanged += CmbFilter_SelectedIndexChanged;
            // 
            // btnFilterReset
            // 
            btnFilterReset.FlatStyle = FlatStyle.System;
            btnFilterReset.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnFilterReset.Location = new Point(203, 11);
            btnFilterReset.Name = "btnFilterReset";
            btnFilterReset.Size = new Size(30, 25);
            btnFilterReset.TabIndex = 5;
            btnFilterReset.Text = "X";
            btnFilterReset.UseVisualStyleBackColor = true;
            btnFilterReset.Click += BtnFilterReset_Click;
            // 
            // chkIncludeCurrent
            // 
            chkIncludeCurrent.AutoSize = true;
            chkIncludeCurrent.Location = new Point(12, 42);
            chkIncludeCurrent.Name = "chkIncludeCurrent";
            chkIncludeCurrent.Size = new Size(226, 19);
            chkIncludeCurrent.TabIndex = 6;
            chkIncludeCurrent.Text = "Include current date and gaming time";
            chkIncludeCurrent.UseVisualStyleBackColor = true;
            chkIncludeCurrent.CheckedChanged += ChkIncludeCurrent_CheckedChanged;
            // 
            // FrmPastValues
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1049, 450);
            Controls.Add(chkIncludeCurrent);
            Controls.Add(btnFilterReset);
            Controls.Add(cmbFilter);
            Controls.Add(webView);
            Controls.Add(dgv);
            Controls.Add(statusStrip1);
            Name = "FrmPastValues";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Past Values";
            FormClosing += FrmPastValues_FormClosing;
            Load += FrmPastValues_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bs).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            ((System.ComponentModel.ISupportInitialize)webView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblCount;
        private ToolStripStatusLabel lblAvg;
        private BindingSource bs;
        private DataGridView dgv;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView;
        private ComboBox cmbFilter;
        private Button btnFilterReset;
        private CheckBox chkIncludeCurrent;
    }
}