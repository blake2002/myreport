namespace PMS.Libraries.ToolControls.PMSChart
{
    partial class SectionApperence
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
            this.Apply = new System.Windows.Forms.Button();
            this.AreaManage = new System.Windows.Forms.GroupBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.Cancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.Sure = new System.Windows.Forms.Button();
            this.titleAdd = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addTitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.titleDelete = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteTitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.limitAdd = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.添加警戒线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.limitDelete = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除警戒线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AreaManage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.titleAdd.SuspendLayout();
            this.titleDelete.SuspendLayout();
            this.limitAdd.SuspendLayout();
            this.limitDelete.SuspendLayout();
            this.SuspendLayout();
            // 
            // Apply
            // 
            this.Apply.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Apply.Location = new System.Drawing.Point(307, 426);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(75, 23);
            this.Apply.TabIndex = 21;
            this.Apply.Text = "应 用";
            this.Apply.UseVisualStyleBackColor = true;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // AreaManage
            // 
            this.AreaManage.Controls.Add(this.treeView1);
            this.AreaManage.Location = new System.Drawing.Point(0, 0);
            this.AreaManage.Name = "AreaManage";
            this.AreaManage.Size = new System.Drawing.Size(179, 420);
            this.AreaManage.TabIndex = 17;
            this.AreaManage.TabStop = false;
            this.AreaManage.Text = "外观";
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(3, 17);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(173, 400);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // Cancel
            // 
            this.Cancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(226, 426);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 20;
            this.Cancel.Text = "取 消";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.propertyGrid1);
            this.groupBox1.Location = new System.Drawing.Point(181, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(198, 417);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "属性列表";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(3, 17);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(192, 397);
            this.propertyGrid1.TabIndex = 8;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // Sure
            // 
            this.Sure.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Sure.Location = new System.Drawing.Point(145, 426);
            this.Sure.Name = "Sure";
            this.Sure.Size = new System.Drawing.Size(75, 23);
            this.Sure.TabIndex = 19;
            this.Sure.Text = "确 定";
            this.Sure.UseVisualStyleBackColor = true;
            this.Sure.Click += new System.EventHandler(this.Sure_Click);
            // 
            // titleAdd
            // 
            this.titleAdd.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addTitleToolStripMenuItem});
            this.titleAdd.Name = "titleEdit";
            this.titleAdd.Size = new System.Drawing.Size(101, 26);
            // 
            // addTitleToolStripMenuItem
            // 
            this.addTitleToolStripMenuItem.Name = "addTitleToolStripMenuItem";
            this.addTitleToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.addTitleToolStripMenuItem.Text = "添加";
            this.addTitleToolStripMenuItem.Click += new System.EventHandler(this.addTitleToolStripMenuItem_Click);
            // 
            // titleDelete
            // 
            this.titleDelete.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteTitleToolStripMenuItem});
            this.titleDelete.Name = "titleDelete";
            this.titleDelete.Size = new System.Drawing.Size(101, 26);
            this.titleDelete.Click += new System.EventHandler(this.deleteTitleToolStripMenuItem_Click);
            // 
            // deleteTitleToolStripMenuItem
            // 
            this.deleteTitleToolStripMenuItem.Name = "deleteTitleToolStripMenuItem";
            this.deleteTitleToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.deleteTitleToolStripMenuItem.Text = "删除";
            this.deleteTitleToolStripMenuItem.Click += new System.EventHandler(this.deleteTitleToolStripMenuItem_Click);
            // 
            // limitAdd
            // 
            this.limitAdd.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加警戒线ToolStripMenuItem});
            this.limitAdd.Name = "limitAdd";
            this.limitAdd.Size = new System.Drawing.Size(101, 26);
            // 
            // 添加警戒线ToolStripMenuItem
            // 
            this.添加警戒线ToolStripMenuItem.Name = "添加警戒线ToolStripMenuItem";
            this.添加警戒线ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.添加警戒线ToolStripMenuItem.Text = "添加";
            this.添加警戒线ToolStripMenuItem.Click += new System.EventHandler(this.AddLimitToolStripMenuItem_Click);
            // 
            // limitDelete
            // 
            this.limitDelete.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除警戒线ToolStripMenuItem});
            this.limitDelete.Name = "contextMenuStrip1";
            this.limitDelete.Size = new System.Drawing.Size(101, 26);
            // 
            // 删除警戒线ToolStripMenuItem
            // 
            this.删除警戒线ToolStripMenuItem.Name = "删除警戒线ToolStripMenuItem";
            this.删除警戒线ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.删除警戒线ToolStripMenuItem.Text = "删除";
            this.删除警戒线ToolStripMenuItem.Click += new System.EventHandler(this.DeleteLimitToolStripMenuItem_Click);
            // 
            // SectionApperence
            // 
            this.AcceptButton = this.Sure;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(381, 455);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.AreaManage);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Sure);
            this.Controls.Add(this.Apply);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SectionApperence";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "分段曲线外观配置";
            this.Load += new System.EventHandler(this.SectionApperence_Load);
            this.AreaManage.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.titleAdd.ResumeLayout(false);
            this.titleDelete.ResumeLayout(false);
            this.limitAdd.ResumeLayout(false);
            this.limitDelete.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Apply;
        private System.Windows.Forms.GroupBox AreaManage;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Button Sure;
        private System.Windows.Forms.ContextMenuStrip titleAdd;
        private System.Windows.Forms.ToolStripMenuItem addTitleToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip titleDelete;
        private System.Windows.Forms.ToolStripMenuItem deleteTitleToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip limitAdd;
        private System.Windows.Forms.ToolStripMenuItem 添加警戒线ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip limitDelete;
        private System.Windows.Forms.ToolStripMenuItem 删除警戒线ToolStripMenuItem;
    }
}