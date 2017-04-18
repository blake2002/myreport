namespace MESReportRunnerShell
{
    partial class MESReportRunnerShellForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MESReportRunnerShellForm));
            this.mesReportViewer1 = new PMS.Libraries.ToolControls.MESReportViewer();
            this.SuspendLayout();
            // 
            // mesReportViewer1
            // 
            resources.ApplyResources(this.mesReportViewer1, "mesReportViewer1");
            this.mesReportViewer1.Name = "mesReportViewer1";
            this.mesReportViewer1.RptFileName = null;
            this.mesReportViewer1.ToolBarVisible = false;
            // 
            // MESReportRunnerShellForm
            // 
            resources.ApplyResources(this, "$this");
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mesReportViewer1);
            this.KeyPreview = true;
            this.Name = "MESReportRunnerShellForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MESReportRunnerShellForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MESReportRunnerShellForm_DragEnter);
            this.ResumeLayout(false);

        }

        #endregion

        private PMS.Libraries.ToolControls.MESReportViewer mesReportViewer1;
    }
}

