namespace PMS.Libraries.ToolControls
{
    partial class ReportConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportConfigForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.propertyTree1 = new WRM.Windows.Forms.PropertyTree();
            this.ppPane_DBSource = new WRM.Windows.Forms.PropertyPane();
            this.dbSourceDefineControl1 = new PMS.Libraries.ToolControls.PMSPublicInfo.DBSourceDefine.DBSourceDefineControl();
            this.ppPane_MapTable = new WRM.Windows.Forms.PropertyPane();
            this.button_Apply = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertyTree1)).BeginInit();
            this.propertyTree1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ppPane_DBSource)).BeginInit();
            this.ppPane_DBSource.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ppPane_MapTable)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            this.splitContainer1.Panel1.Controls.Add(this.propertyTree1);
            // 
            // splitContainer1.Panel2
            // 
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            this.splitContainer1.Panel2.Controls.Add(this.button_Apply);
            this.splitContainer1.Panel2.Controls.Add(this.button_Cancel);
            this.splitContainer1.Panel2.Controls.Add(this.button_OK);
            // 
            // propertyTree1
            // 
            resources.ApplyResources(this.propertyTree1, "propertyTree1");
            this.propertyTree1.ImageList = null;
            this.propertyTree1.Indent = 19;
            this.propertyTree1.Name = "propertyTree1";
            this.propertyTree1.SelectedImageIndex = -1;
            this.propertyTree1.ShowLines = true;
            this.propertyTree1.ShowPlusMinus = true;
            this.propertyTree1.ShowRootLines = true;
            this.propertyTree1.SplitterColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.propertyTree1.SplitterLeft = 184;
            this.propertyTree1.PaneNodes.Add(this.ppPane_DBSource, 0, -1, -1);
            this.propertyTree1.PaneNodes.Add(this.ppPane_MapTable, 1, -1, -1);
            // 
            // ppPane_DBSource
            // 
            resources.ApplyResources(this.ppPane_DBSource, "ppPane_DBSource");
            this.ppPane_DBSource.Controls.Add(this.dbSourceDefineControl1);
            this.ppPane_DBSource.ImageIndex = -1;
            this.ppPane_DBSource.Name = "ppPane_DBSource";
            this.ppPane_DBSource.SelectedImageIndex = -1;
            // 
            // dbSourceDefineControl1
            // 
            resources.ApplyResources(this.dbSourceDefineControl1, "dbSourceDefineControl1");
            this.dbSourceDefineControl1.DBDefineManager = null;
            this.dbSourceDefineControl1.Name = "dbSourceDefineControl1";
            // 
            // ppPane_MapTable
            // 
            resources.ApplyResources(this.ppPane_MapTable, "ppPane_MapTable");
            this.ppPane_MapTable.ImageIndex = -1;
            this.ppPane_MapTable.Name = "ppPane_MapTable";
            this.ppPane_MapTable.SelectedImageIndex = -1;
            // 
            // button_Apply
            // 
            resources.ApplyResources(this.button_Apply, "button_Apply");
            this.button_Apply.Name = "button_Apply";
            this.button_Apply.UseVisualStyleBackColor = true;
            this.button_Apply.Click += new System.EventHandler(this.button_Apply_Click);
            // 
            // button_Cancel
            // 
            resources.ApplyResources(this.button_Cancel, "button_Cancel");
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_OK
            // 
            resources.ApplyResources(this.button_OK, "button_OK");
            this.button_OK.Name = "button_OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // ReportConfigForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ReportConfigForm";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.ReportConfigForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertyTree1)).EndInit();
            this.propertyTree1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ppPane_DBSource)).EndInit();
            this.ppPane_DBSource.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ppPane_MapTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private WRM.Windows.Forms.PropertyPane ppPane_DBSource;
        private PMSPublicInfo.DBSourceDefine.DBSourceDefineControl dbSourceDefineControl1;
        private WRM.Windows.Forms.PropertyPane ppPane_MapTable;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button_Apply;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_OK;
        private WRM.Windows.Forms.PropertyTree propertyTree1;
    }
}