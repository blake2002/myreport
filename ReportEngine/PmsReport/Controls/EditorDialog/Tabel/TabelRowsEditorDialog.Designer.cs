namespace PMS.Libraries.ToolControls.Report.Controls.EditorDialog
{
    partial class TabelRowsEditorDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TabelRowsEditorDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.RowPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.AddRowBtn = new System.Windows.Forms.Button();
            this.DeleteBtn = new System.Windows.Forms.Button();
            this.CancleBtn = new System.Windows.Forms.Button();
            this.OkBtn = new System.Windows.Forms.Button();
            this.UpBtn = new System.Windows.Forms.Button();
            this.DownBtn = new System.Windows.Forms.Button();
            this.RowTreeView = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选定的行";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(272, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "行属性";
            // 
            // RowPropertyGrid
            // 
            this.RowPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.RowPropertyGrid.Location = new System.Drawing.Point(273, 40);
            this.RowPropertyGrid.Name = "RowPropertyGrid";
            this.RowPropertyGrid.Size = new System.Drawing.Size(301, 459);
            this.RowPropertyGrid.TabIndex = 2;
            // 
            // AddRowBtn
            // 
            this.AddRowBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.AddRowBtn.Location = new System.Drawing.Point(37, 463);
            this.AddRowBtn.Name = "AddRowBtn";
            this.AddRowBtn.Size = new System.Drawing.Size(71, 27);
            this.AddRowBtn.TabIndex = 3;
            this.AddRowBtn.Text = "添加";
            this.AddRowBtn.UseVisualStyleBackColor = true;
            // 
            // DeleteBtn
            // 
            this.DeleteBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.DeleteBtn.Location = new System.Drawing.Point(124, 463);
            this.DeleteBtn.Name = "DeleteBtn";
            this.DeleteBtn.Size = new System.Drawing.Size(71, 27);
            this.DeleteBtn.TabIndex = 3;
            this.DeleteBtn.Text = "移除";
            this.DeleteBtn.UseVisualStyleBackColor = true;
            // 
            // CancleBtn
            // 
            this.CancleBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.CancleBtn.Location = new System.Drawing.Point(479, 505);
            this.CancleBtn.Name = "CancleBtn";
            this.CancleBtn.Size = new System.Drawing.Size(91, 29);
            this.CancleBtn.TabIndex = 5;
            this.CancleBtn.Text = "取消";
            this.CancleBtn.UseVisualStyleBackColor = true;
            // 
            // OkBtn
            // 
            this.OkBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.OkBtn.Location = new System.Drawing.Point(382, 505);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(91, 29);
            this.OkBtn.TabIndex = 4;
            this.OkBtn.Text = "确定";
            this.OkBtn.UseVisualStyleBackColor = true;
            // 
            // UpBtn
            // 
            this.UpBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("UpBtn.BackgroundImage")));
            this.UpBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.UpBtn.Location = new System.Drawing.Point(224, 41);
            this.UpBtn.Name = "UpBtn";
            this.UpBtn.Size = new System.Drawing.Size(27, 39);
            this.UpBtn.TabIndex = 6;
            this.UpBtn.UseVisualStyleBackColor = true;
            // 
            // DownBtn
            // 
            this.DownBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DownBtn.BackgroundImage")));
            this.DownBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DownBtn.Location = new System.Drawing.Point(224, 85);
            this.DownBtn.Name = "DownBtn";
            this.DownBtn.Size = new System.Drawing.Size(27, 39);
            this.DownBtn.TabIndex = 7;
            this.DownBtn.UseVisualStyleBackColor = true;
            // 
            // RowTreeView
            // 
            this.RowTreeView.Location = new System.Drawing.Point(11, 41);
            this.RowTreeView.Name = "RowTreeView";
            this.RowTreeView.Size = new System.Drawing.Size(209, 418);
            this.RowTreeView.TabIndex = 8;
            // 
            // TabelRowsEditorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 538);
            this.Controls.Add(this.RowTreeView);
            this.Controls.Add(this.DownBtn);
            this.Controls.Add(this.UpBtn);
            this.Controls.Add(this.CancleBtn);
            this.Controls.Add(this.OkBtn);
            this.Controls.Add(this.DeleteBtn);
            this.Controls.Add(this.AddRowBtn);
            this.Controls.Add(this.RowPropertyGrid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TabelRowsEditorDialog";
            this.Text = "行编辑";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PropertyGrid RowPropertyGrid;
        private System.Windows.Forms.Button AddRowBtn;
        private System.Windows.Forms.Button DeleteBtn;
        private System.Windows.Forms.Button CancleBtn;
        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.Button UpBtn;
        private System.Windows.Forms.Button DownBtn;
        private System.Windows.Forms.TreeView RowTreeView;
    }
}