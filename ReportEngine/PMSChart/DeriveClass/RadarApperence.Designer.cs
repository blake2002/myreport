namespace PMS.Libraries.ToolControls.PMSChart
{
    partial class RadarApperence
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RadarApperence));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.AreaManage = new System.Windows.Forms.GroupBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.Apply = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.Sure = new System.Windows.Forms.Button();
            this.LegendDelete = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteLegend = new System.Windows.Forms.ToolStripMenuItem();
            this.TitleDelete = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteTitle = new System.Windows.Forms.ToolStripMenuItem();
            this.ManageSeries = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddField = new System.Windows.Forms.ToolStripMenuItem();
            this.FieldDelete = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteField = new System.Windows.Forms.ToolStripMenuItem();
            this.ManageArea = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddArea = new System.Windows.Forms.ToolStripMenuItem();
            this.AreaChildManage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteArea = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.AddLegend = new System.Windows.Forms.ToolStripMenuItem();
            this.AddTitile = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.AreaManage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.LegendDelete.SuspendLayout();
            this.TitleDelete.SuspendLayout();
            this.ManageSeries.SuspendLayout();
            this.FieldDelete.SuspendLayout();
            this.ManageArea.SuspendLayout();
            this.AreaChildManage.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.AreaManage);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(487, 491);
            this.splitContainer1.SplitterDistance = 203;
            this.splitContainer1.TabIndex = 11;
            // 
            // AreaManage
            // 
            this.AreaManage.Controls.Add(this.treeView1);
            this.AreaManage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AreaManage.Location = new System.Drawing.Point(0, 0);
            this.AreaManage.Name = "AreaManage";
            this.AreaManage.Size = new System.Drawing.Size(203, 491);
            this.AreaManage.TabIndex = 0;
            this.AreaManage.TabStop = false;
            this.AreaManage.Text = "区域管理";
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(3, 17);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(197, 471);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "(42,40).png");
            this.imageList1.Images.SetKeyName(1, "(34,34).png");
            this.imageList1.Images.SetKeyName(2, "(35,20).png");
            this.imageList1.Images.SetKeyName(3, "(26,04).png");
            this.imageList1.Images.SetKeyName(4, "(37,29).png");
            this.imageList1.Images.SetKeyName(5, "(16,32).png");
            this.imageList1.Images.SetKeyName(6, "justice.png");
            this.imageList1.Images.SetKeyName(7, "Field.ico");
            this.imageList1.Images.SetKeyName(8, "Axis.png");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.propertyGrid1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 491);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "属性配置";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(3, 17);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(274, 471);
            this.propertyGrid1.TabIndex = 8;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // Apply
            // 
            this.Apply.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Apply.Location = new System.Drawing.Point(404, 497);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(75, 23);
            this.Apply.TabIndex = 16;
            this.Apply.Text = "应 用";
            this.Apply.UseVisualStyleBackColor = true;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // Cancel
            // 
            this.Cancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Cancel.Location = new System.Drawing.Point(323, 497);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 15;
            this.Cancel.Text = "取 消";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // Sure
            // 
            this.Sure.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Sure.Location = new System.Drawing.Point(242, 497);
            this.Sure.Name = "Sure";
            this.Sure.Size = new System.Drawing.Size(75, 23);
            this.Sure.TabIndex = 14;
            this.Sure.Text = "确 定";
            this.Sure.UseVisualStyleBackColor = true;
            this.Sure.Click += new System.EventHandler(this.Sure_Click);
            // 
            // LegendDelete
            // 
            this.LegendDelete.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteLegend});
            this.LegendDelete.Name = "LegendDelete";
            this.LegendDelete.Size = new System.Drawing.Size(69, 26);
            // 
            // DeleteLegend
            // 
            this.DeleteLegend.Name = "DeleteLegend";
            this.DeleteLegend.Size = new System.Drawing.Size(68, 22);
            this.DeleteLegend.Click += new System.EventHandler(this.DeleteLegend_Click);
            // 
            // TitleDelete
            // 
            this.TitleDelete.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteTitle});
            this.TitleDelete.Name = "TitleDelete";
            this.TitleDelete.Size = new System.Drawing.Size(69, 26);
            // 
            // DeleteTitle
            // 
            this.DeleteTitle.Name = "DeleteTitle";
            this.DeleteTitle.Size = new System.Drawing.Size(68, 22);
            this.DeleteTitle.Click += new System.EventHandler(this.DeleteTitle_Click);
            // 
            // ManageSeries
            // 
            this.ManageSeries.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddField});
            this.ManageSeries.Name = "AddField";
            this.ManageSeries.Size = new System.Drawing.Size(69, 26);
            // 
            // AddField
            // 
            this.AddField.Name = "AddField";
            this.AddField.Size = new System.Drawing.Size(68, 22);
            this.AddField.Click += new System.EventHandler(this.AddField_Click);
            // 
            // FieldDelete
            // 
            this.FieldDelete.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteField});
            this.FieldDelete.Name = "FieldDelete";
            this.FieldDelete.Size = new System.Drawing.Size(69, 26);
            // 
            // DeleteField
            // 
            this.DeleteField.Name = "DeleteField";
            this.DeleteField.Size = new System.Drawing.Size(68, 22);
            this.DeleteField.Click += new System.EventHandler(this.DeleteField_Click);
            // 
            // ManageArea
            // 
            this.ManageArea.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddArea});
            this.ManageArea.Name = "contextMenuStrip1";
            this.ManageArea.Size = new System.Drawing.Size(153, 48);
            // 
            // AddArea
            // 
            this.AddArea.Name = "AddArea";
            this.AddArea.Size = new System.Drawing.Size(152, 22);
            this.AddArea.Click += new System.EventHandler(this.AddArea_Click);
            // 
            // AreaChildManage
            // 
            this.AreaChildManage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteArea,
            this.toolStripSeparator1,
            this.AddLegend,
            this.AddTitile});
            this.AreaChildManage.Name = "AreaChildManage";
            this.AreaChildManage.Size = new System.Drawing.Size(69, 76);
            // 
            // DeleteArea
            // 
            this.DeleteArea.Name = "DeleteArea";
            this.DeleteArea.Size = new System.Drawing.Size(68, 22);
            this.DeleteArea.Click += new System.EventHandler(this.DeleteArea_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(65, 6);
            // 
            // AddLegend
            // 
            this.AddLegend.Name = "AddLegend";
            this.AddLegend.Size = new System.Drawing.Size(68, 22);
            this.AddLegend.Click += new System.EventHandler(this.AddLegend_Click);
            // 
            // AddTitile
            // 
            this.AddTitile.Name = "AddTitile";
            this.AddTitile.Size = new System.Drawing.Size(68, 22);
            this.AddTitile.Click += new System.EventHandler(this.AddTitile_Click);
            // 
            // RadarApperence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 524);
            this.Controls.Add(this.Apply);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Sure);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RadarApperence";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "雷达图外观配置";
            this.Load += new System.EventHandler(this.RadarApperence_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.AreaManage.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.LegendDelete.ResumeLayout(false);
            this.TitleDelete.ResumeLayout(false);
            this.ManageSeries.ResumeLayout(false);
            this.FieldDelete.ResumeLayout(false);
            this.ManageArea.ResumeLayout(false);
            this.AreaChildManage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox AreaManage;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Button Apply;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Sure;
        private System.Windows.Forms.ContextMenuStrip LegendDelete;
        private System.Windows.Forms.ToolStripMenuItem DeleteLegend;
        private System.Windows.Forms.ContextMenuStrip TitleDelete;
        private System.Windows.Forms.ToolStripMenuItem DeleteTitle;
        private System.Windows.Forms.ContextMenuStrip ManageSeries;
        private System.Windows.Forms.ToolStripMenuItem AddField;
        private System.Windows.Forms.ContextMenuStrip FieldDelete;
        private System.Windows.Forms.ToolStripMenuItem DeleteField;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip ManageArea;
        private System.Windows.Forms.ToolStripMenuItem AddArea;
        private System.Windows.Forms.ContextMenuStrip AreaChildManage;
        private System.Windows.Forms.ToolStripMenuItem DeleteArea;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem AddLegend;
        private System.Windows.Forms.ToolStripMenuItem AddTitile;
    }
}