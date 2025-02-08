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
            statusStrip1.Size = new Size(865, 22);
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
            dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Location = new Point(12, 41);
            dgv.Name = "dgv";
            dgv.Size = new Size(363, 384);
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
            webView.Size = new Size(472, 413);
            webView.TabIndex = 3;
            webView.ZoomFactor = 1D;
            webView.Resize += WebView_Resize;
            // 
            // cmbFilter
            // 
            cmbFilter.FormattingEnabled = true;
            cmbFilter.Location = new Point(164, 12);
            cmbFilter.Name = "cmbFilter";
            cmbFilter.Size = new Size(211, 23);
            cmbFilter.TabIndex = 4;
            cmbFilter.SelectedIndexChanged += CmbFilter_SelectedIndexChanged;
            // 
            // FrmPastValues
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(865, 450);
            Controls.Add(cmbFilter);
            Controls.Add(webView);
            Controls.Add(dgv);
            Controls.Add(statusStrip1);
            Name = "FrmPastValues";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Past Values";
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
    }
}