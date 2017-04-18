namespace PMS.Libraries.ToolControls
{
    partial class ReportViewerDoModalForm
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
            if (disposing)
            {
                if(components != null)
                    components.Dispose();
                this.mesReportViewer1.ReleaseReportResource();
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
            this.mesReportViewer1 = new PMS.Libraries.ToolControls.MESReportViewer();
            this.SuspendLayout();
            // 
            // mesReportViewer1
            // 
            this.mesReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mesReportViewer1.Location = new System.Drawing.Point(0, 0);
            this.mesReportViewer1.Name = "mesReportViewer1";
            this.mesReportViewer1.RptFileName = null;
            this.mesReportViewer1.Size = new System.Drawing.Size(1053, 718);
            this.mesReportViewer1.TabIndex = 0;
            // 
            // ReportViewerDoModalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1053, 718);
            this.Controls.Add(this.mesReportViewer1);
            this.Name = "ReportViewerDoModalForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "报表查看器";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ReportViewerDoModalForm_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private MESReportViewer mesReportViewer1;
    }
}