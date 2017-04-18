namespace PMS.Libraries.ToolControls.Report.Controls.EditorDialog
{
    partial class FieldBindDialog
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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.DataSourceTreeView = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.FieldTextTb = new System.Windows.Forms.TextBox();
            this.CancleBtn = new System.Windows.Forms.Button();
            this.OkBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.CancleBtn);
            this.splitContainer2.Panel2.Controls.Add(this.OkBtn);
            this.splitContainer2.Size = new System.Drawing.Size(650, 392);
            this.splitContainer2.SplitterDistance = 358;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.DataSourceTreeView);
            this.splitContainer3.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.FieldTextTb);
            this.splitContainer3.Size = new System.Drawing.Size(650, 358);
            this.splitContainer3.SplitterDistance = 208;
            this.splitContainer3.TabIndex = 0;
            // 
            // DataSourceTreeView
            // 
            this.DataSourceTreeView.AllowDrop = true;
            this.DataSourceTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DataSourceTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DataSourceTreeView.HideSelection = false;
            this.DataSourceTreeView.Location = new System.Drawing.Point(4, 19);
            this.DataSourceTreeView.Name = "DataSourceTreeView";
            this.DataSourceTreeView.Size = new System.Drawing.Size(200, 336);
            this.DataSourceTreeView.TabIndex = 0;
            this.DataSourceTreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.DataSourceTreeView_ItemDrag);
            this.DataSourceTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.DataSourceTreeView_NodeMouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "可用数据源";
            // 
            // FieldTextTb
            // 
            this.FieldTextTb.AllowDrop = true;
            this.FieldTextTb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FieldTextTb.HideSelection = false;
            this.FieldTextTb.Location = new System.Drawing.Point(0, 0);
            this.FieldTextTb.Multiline = true;
            this.FieldTextTb.Name = "FieldTextTb";
            this.FieldTextTb.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.FieldTextTb.Size = new System.Drawing.Size(436, 356);
            this.FieldTextTb.TabIndex = 0;
            this.FieldTextTb.DragDrop += new System.Windows.Forms.DragEventHandler(this.TextTb_DragDrop);
            this.FieldTextTb.DragEnter += new System.Windows.Forms.DragEventHandler(this.TextTb_DragEnter);
            // 
            // CancleBtn
            // 
            this.CancleBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CancleBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancleBtn.Location = new System.Drawing.Point(330, 2);
            this.CancleBtn.Name = "CancleBtn";
            this.CancleBtn.Size = new System.Drawing.Size(64, 23);
            this.CancleBtn.TabIndex = 1;
            this.CancleBtn.Text = "取消";
            this.CancleBtn.UseVisualStyleBackColor = true;
            this.CancleBtn.Click += new System.EventHandler(this.CancleBtn_Click);
            // 
            // OkBtn
            // 
            this.OkBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OkBtn.Location = new System.Drawing.Point(264, 2);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(64, 23);
            this.OkBtn.TabIndex = 0;
            this.OkBtn.Text = "确定";
            this.OkBtn.UseVisualStyleBackColor = true;
            this.OkBtn.Click += new System.EventHandler(this.OKClick);
            // 
            // FieldBindDialog
            // 
            this.AcceptButton = this.OkBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.CancelButton = this.CancleBtn;
            this.ClientSize = new System.Drawing.Size(650, 392);
            this.Controls.Add(this.splitContainer2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(666, 430);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(666, 430);
            this.Name = "FieldBindDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "字段绑定";
            this.Load += new System.EventHandler(this.FieldBindDialogLoad);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TreeView DataSourceTreeView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox FieldTextTb;
        private System.Windows.Forms.Button CancleBtn;
        private System.Windows.Forms.Button OkBtn;

    }
}