namespace PMS.Libraries.ToolControls.Report.Controls.EditorDialog
{
    partial class SourceBindDialog
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SourceBindDialog));
            this.DataSourceTreeView = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.OkBtn = new System.Windows.Forms.Button();
            this.CancleBtn = new System.Windows.Forms.Button();
            this.cb_CustomMode = new System.Windows.Forms.CheckBox();
            this.tb_TablePath = new System.Windows.Forms.TextBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // DataSourceTreeView
            // 
            this.DataSourceTreeView.AllowDrop = true;
            this.DataSourceTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataSourceTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DataSourceTreeView.HideSelection = false;
            this.DataSourceTreeView.ImageIndex = 0;
            this.DataSourceTreeView.ImageList = this.imageList1;
            this.DataSourceTreeView.Location = new System.Drawing.Point(4, 28);
            this.DataSourceTreeView.Name = "DataSourceTreeView";
            this.DataSourceTreeView.SelectedImageIndex = 0;
            this.DataSourceTreeView.Size = new System.Drawing.Size(339, 270);
            this.DataSourceTreeView.TabIndex = 3;
            this.DataSourceTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.DataSourceTreeView_NodeMouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "可用数据源";
            // 
            // OkBtn
            // 
            this.OkBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.OkBtn.Location = new System.Drawing.Point(93, 337);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(71, 25);
            this.OkBtn.TabIndex = 4;
            this.OkBtn.Text = "确定";
            this.OkBtn.UseVisualStyleBackColor = true;
            this.OkBtn.Click += new System.EventHandler(this.OkBtnClick);
            // 
            // CancleBtn
            // 
            this.CancleBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.CancleBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancleBtn.Location = new System.Drawing.Point(177, 337);
            this.CancleBtn.Name = "CancleBtn";
            this.CancleBtn.Size = new System.Drawing.Size(71, 25);
            this.CancleBtn.TabIndex = 4;
            this.CancleBtn.Text = "取消";
            this.CancleBtn.UseVisualStyleBackColor = true;
            this.CancleBtn.Click += new System.EventHandler(this.CancleBtn_Click);
            // 
            // cb_CustomMode
            // 
            this.cb_CustomMode.AutoSize = true;
            this.cb_CustomMode.Location = new System.Drawing.Point(7, 308);
            this.cb_CustomMode.Name = "cb_CustomMode";
            this.cb_CustomMode.Size = new System.Drawing.Size(96, 16);
            this.cb_CustomMode.TabIndex = 6;
            this.cb_CustomMode.Text = "自定义绑定：";
            this.cb_CustomMode.UseVisualStyleBackColor = true;
            this.cb_CustomMode.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // tb_TablePath
            // 
            this.tb_TablePath.Enabled = false;
            this.tb_TablePath.Location = new System.Drawing.Point(98, 306);
            this.tb_TablePath.Name = "tb_TablePath";
            this.tb_TablePath.Size = new System.Drawing.Size(245, 21);
            this.tb_TablePath.TabIndex = 7;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "table.png");
            this.imageList1.Images.SetKeyName(1, "(37,47).png");
            this.imageList1.Images.SetKeyName(2, "(00,32).png");
            this.imageList1.Images.SetKeyName(3, "field.png");
            // 
            // SourceBindDialog
            // 
            this.AcceptButton = this.OkBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancleBtn;
            this.ClientSize = new System.Drawing.Size(348, 362);
            this.Controls.Add(this.tb_TablePath);
            this.Controls.Add(this.cb_CustomMode);
            this.Controls.Add(this.CancleBtn);
            this.Controls.Add(this.OkBtn);
            this.Controls.Add(this.DataSourceTreeView);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(364, 400);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(364, 400);
            this.Name = "SourceBindDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据源绑定";
            this.Load += new System.EventHandler(this.SourceBindDialogLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView DataSourceTreeView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.Button CancleBtn;
        private System.Windows.Forms.CheckBox cb_CustomMode;
        private System.Windows.Forms.TextBox tb_TablePath;
        private System.Windows.Forms.ImageList imageList1;
    }
}