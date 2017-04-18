namespace PMS.Libraries.ToolControls.PMSChart
{
    partial class RadarAlertApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RadarAlertApp));
            this.CBdrawingStyle = new System.Windows.Forms.ComboBox();
            this.Apply = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.Sure = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.CBreportMode = new System.Windows.Forms.ComboBox();
            this.AxisLineCheck = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CBlableStyle = new System.Windows.Forms.ComboBox();
            this.Area3Dcheck = new System.Windows.Forms.CheckBox();
            this.btnTitleDel = new System.Windows.Forms.Button();
            this.btnTitleAdd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.TitlelistBox = new System.Windows.Forms.ListBox();
            this.ApplistBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.VarcheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.propertyTree1 = new WRM.Windows.Forms.PropertyTree();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.ppPane1 = new WRM.Windows.Forms.PropertyPane();
            this.button1 = new System.Windows.Forms.Button();
            this.Dechecked = new System.Windows.Forms.CheckBox();
            this.Allcheck = new System.Windows.Forms.CheckBox();
            this.ppPane2 = new WRM.Windows.Forms.PropertyPane();
            this.btnLegendDel = new System.Windows.Forms.Button();
            this.btnLegendAdd = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.LegendlistBox = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertyTree1)).BeginInit();
            this.propertyTree1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ppPane1)).BeginInit();
            this.ppPane1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ppPane2)).BeginInit();
            this.ppPane2.SuspendLayout();
            this.SuspendLayout();
            // 
            // CBdrawingStyle
            // 
            this.CBdrawingStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBdrawingStyle.FormattingEnabled = true;
            this.CBdrawingStyle.Items.AddRange(new object[] {
            "Polygon",
            "Circle"});
            this.CBdrawingStyle.Location = new System.Drawing.Point(70, 264);
            this.CBdrawingStyle.Name = "CBdrawingStyle";
            this.CBdrawingStyle.Size = new System.Drawing.Size(82, 20);
            this.CBdrawingStyle.TabIndex = 22;
            this.CBdrawingStyle.SelectedIndexChanged += new System.EventHandler(this.CBdrawingStyle_SelectedIndexChanged);
            this.CBdrawingStyle.Enter += new System.EventHandler(this.CBdrawingStyle_Enter);
            this.CBdrawingStyle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CBdrawingStyle_KeyDown);
            this.CBdrawingStyle.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CBdrawingStyle_KeyUp);
            // 
            // Apply
            // 
            this.Apply.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Apply.Location = new System.Drawing.Point(430, 396);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(75, 23);
            this.Apply.TabIndex = 19;
            this.Apply.Text = "应 用";
            this.Apply.UseVisualStyleBackColor = true;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // Cancel
            // 
            this.Cancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(349, 396);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 18;
            this.Cancel.Text = "取 消";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // Sure
            // 
            this.Sure.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Sure.Location = new System.Drawing.Point(268, 396);
            this.Sure.Name = "Sure";
            this.Sure.Size = new System.Drawing.Size(75, 23);
            this.Sure.TabIndex = 17;
            this.Sure.Text = "确 定";
            this.Sure.UseVisualStyleBackColor = true;
            this.Sure.Click += new System.EventHandler(this.Sure_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 245);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 28;
            this.label6.Text = "显示模式";
            // 
            // CBreportMode
            // 
            this.CBreportMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBreportMode.FormattingEnabled = true;
            this.CBreportMode.Items.AddRange(new object[] {
            "Percent",
            "Absolute",
            "Relative"});
            this.CBreportMode.Location = new System.Drawing.Point(70, 241);
            this.CBreportMode.Name = "CBreportMode";
            this.CBreportMode.Size = new System.Drawing.Size(82, 20);
            this.CBreportMode.TabIndex = 27;
            this.CBreportMode.SelectedIndexChanged += new System.EventHandler(this.CBreportMode_SelectedIndexChanged);
            this.CBreportMode.Enter += new System.EventHandler(this.CBreportMode_Enter);
            this.CBreportMode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CBreportMode_KeyDown);
            this.CBreportMode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CBreportMode_KeyUp);
            // 
            // AxisLineCheck
            // 
            this.AxisLineCheck.AutoSize = true;
            this.AxisLineCheck.Location = new System.Drawing.Point(15, 323);
            this.AxisLineCheck.Name = "AxisLineCheck";
            this.AxisLineCheck.Size = new System.Drawing.Size(132, 16);
            this.AxisLineCheck.TabIndex = 26;
            this.AxisLineCheck.Text = "是否显示刻度间连线";
            this.AxisLineCheck.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 291);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 25;
            this.label5.Text = "标签样式";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 268);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 24;
            this.label4.Text = "图表形状";
            // 
            // CBlableStyle
            // 
            this.CBlableStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBlableStyle.FormattingEnabled = true;
            this.CBlableStyle.Items.AddRange(new object[] {
            "Auto",
            "Radial",
            "Horizontal",
            "Circular"});
            this.CBlableStyle.Location = new System.Drawing.Point(70, 287);
            this.CBlableStyle.Name = "CBlableStyle";
            this.CBlableStyle.Size = new System.Drawing.Size(82, 20);
            this.CBlableStyle.TabIndex = 23;
            this.CBlableStyle.SelectedIndexChanged += new System.EventHandler(this.CBlableStyle_SelectedIndexChanged);
            this.CBlableStyle.Enter += new System.EventHandler(this.CBlableStyle_Enter);
            this.CBlableStyle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CBlableStyle_KeyDown);
            this.CBlableStyle.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CBlableStyle_KeyUp);
            // 
            // Area3Dcheck
            // 
            this.Area3Dcheck.AutoSize = true;
            this.Area3Dcheck.Location = new System.Drawing.Point(15, 308);
            this.Area3Dcheck.Name = "Area3Dcheck";
            this.Area3Dcheck.Size = new System.Drawing.Size(108, 16);
            this.Area3Dcheck.TabIndex = 21;
            this.Area3Dcheck.Text = "是否显示3D效果";
            this.Area3Dcheck.UseVisualStyleBackColor = true;
            // 
            // btnTitleDel
            // 
            this.btnTitleDel.Location = new System.Drawing.Point(82, 119);
            this.btnTitleDel.Name = "btnTitleDel";
            this.btnTitleDel.Size = new System.Drawing.Size(82, 23);
            this.btnTitleDel.TabIndex = 9;
            this.btnTitleDel.Text = "删除";
            this.btnTitleDel.UseVisualStyleBackColor = true;
            this.btnTitleDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnTitleAdd
            // 
            this.btnTitleAdd.Location = new System.Drawing.Point(1, 119);
            this.btnTitleAdd.Name = "btnTitleAdd";
            this.btnTitleAdd.Size = new System.Drawing.Size(82, 23);
            this.btnTitleAdd.TabIndex = 8;
            this.btnTitleAdd.Text = "添加";
            this.btnTitleAdd.UseVisualStyleBackColor = true;
            this.btnTitleAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "标题配置";
            // 
            // TitlelistBox
            // 
            this.TitlelistBox.FormattingEnabled = true;
            this.TitlelistBox.ItemHeight = 12;
            this.TitlelistBox.Location = new System.Drawing.Point(3, 79);
            this.TitlelistBox.Name = "TitlelistBox";
            this.TitlelistBox.Size = new System.Drawing.Size(161, 40);
            this.TitlelistBox.TabIndex = 6;
            this.TitlelistBox.SelectedIndexChanged += new System.EventHandler(this.TitlelistBox_SelectedIndexChanged);
            // 
            // ApplistBox
            // 
            this.ApplistBox.FormattingEnabled = true;
            this.ApplistBox.ItemHeight = 12;
            this.ApplistBox.Location = new System.Drawing.Point(3, 21);
            this.ApplistBox.Name = "ApplistBox";
            this.ApplistBox.Size = new System.Drawing.Size(161, 40);
            this.ApplistBox.TabIndex = 4;
            this.ApplistBox.SelectedIndexChanged += new System.EventHandler(this.ApplistBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "图表外观";
            // 
            // VarcheckedListBox
            // 
            this.VarcheckedListBox.FormattingEnabled = true;
            this.VarcheckedListBox.Location = new System.Drawing.Point(-1, -1);
            this.VarcheckedListBox.Name = "VarcheckedListBox";
            this.VarcheckedListBox.Size = new System.Drawing.Size(168, 340);
            this.VarcheckedListBox.TabIndex = 0;
            this.VarcheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.VarcheckedListBox_ItemCheck);
            this.VarcheckedListBox.SelectedIndexChanged += new System.EventHandler(this.VarcheckedListBox_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.propertyGrid1);
            this.groupBox1.Location = new System.Drawing.Point(303, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(204, 383);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "属性配置";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(3, 17);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(198, 363);
            this.propertyGrid1.TabIndex = 8;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // propertyTree1
            // 
            this.propertyTree1.ImageIndex = 0;
            this.propertyTree1.ImageList = this.imageList2;
            this.propertyTree1.Indent = 19;
            this.propertyTree1.Location = new System.Drawing.Point(3, 2);
            this.propertyTree1.Name = "propertyTree1";
            this.propertyTree1.ShowLines = true;
            this.propertyTree1.ShowPlusMinus = true;
            this.propertyTree1.ShowRootLines = true;
            this.propertyTree1.Size = new System.Drawing.Size(297, 388);
            this.propertyTree1.SplitterColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.propertyTree1.SplitterLeft = 125;
            this.propertyTree1.TabIndex = 21;
            this.propertyTree1.PaneNodes.Add(this.ppPane1, 0, 2, 2);
            this.propertyTree1.PaneNodes.Add(this.ppPane2, 1, 3, 3);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "(42,48).png");
            this.imageList2.Images.SetKeyName(1, "(04,29).png");
            this.imageList2.Images.SetKeyName(2, "(02,42).png");
            this.imageList2.Images.SetKeyName(3, "(01,23).png");
            // 
            // ppPane1
            // 
            this.ppPane1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ppPane1.Controls.Add(this.button1);
            this.ppPane1.Controls.Add(this.VarcheckedListBox);
            this.ppPane1.Controls.Add(this.Dechecked);
            this.ppPane1.Controls.Add(this.Allcheck);
            this.ppPane1.ImageIndex = 2;
            this.ppPane1.Name = "ppPane1";
            this.ppPane1.SelectedImageIndex = 2;
            this.ppPane1.Text = "设置报警变量";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(112, 344);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(52, 20);
            this.button1.TabIndex = 14;
            this.button1.Text = "反选";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Dechecked
            // 
            this.Dechecked.AutoSize = true;
            this.Dechecked.Location = new System.Drawing.Point(49, 346);
            this.Dechecked.Name = "Dechecked";
            this.Dechecked.Size = new System.Drawing.Size(60, 16);
            this.Dechecked.TabIndex = 13;
            this.Dechecked.Text = "全不选";
            this.Dechecked.UseVisualStyleBackColor = true;
            this.Dechecked.CheckedChanged += new System.EventHandler(this.Dechecked_CheckedChanged);
            // 
            // Allcheck
            // 
            this.Allcheck.AutoSize = true;
            this.Allcheck.Location = new System.Drawing.Point(5, 346);
            this.Allcheck.Name = "Allcheck";
            this.Allcheck.Size = new System.Drawing.Size(48, 16);
            this.Allcheck.TabIndex = 12;
            this.Allcheck.Text = "全选";
            this.Allcheck.UseVisualStyleBackColor = true;
            this.Allcheck.CheckedChanged += new System.EventHandler(this.Allcheck_CheckedChanged);
            // 
            // ppPane2
            // 
            this.ppPane2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ppPane2.Controls.Add(this.label6);
            this.ppPane2.Controls.Add(this.btnLegendDel);
            this.ppPane2.Controls.Add(this.CBreportMode);
            this.ppPane2.Controls.Add(this.AxisLineCheck);
            this.ppPane2.Controls.Add(this.btnLegendAdd);
            this.ppPane2.Controls.Add(this.label5);
            this.ppPane2.Controls.Add(this.label7);
            this.ppPane2.Controls.Add(this.label4);
            this.ppPane2.Controls.Add(this.LegendlistBox);
            this.ppPane2.Controls.Add(this.CBlableStyle);
            this.ppPane2.Controls.Add(this.ApplistBox);
            this.ppPane2.Controls.Add(this.CBdrawingStyle);
            this.ppPane2.Controls.Add(this.label2);
            this.ppPane2.Controls.Add(this.Area3Dcheck);
            this.ppPane2.Controls.Add(this.label3);
            this.ppPane2.Controls.Add(this.TitlelistBox);
            this.ppPane2.Controls.Add(this.btnTitleDel);
            this.ppPane2.Controls.Add(this.btnTitleAdd);
            this.ppPane2.ImageIndex = 3;
            this.ppPane2.Name = "ppPane2";
            this.ppPane2.SelectedImageIndex = 3;
            this.ppPane2.Text = "外观设置";
            // 
            // btnLegendDel
            // 
            this.btnLegendDel.Location = new System.Drawing.Point(82, 199);
            this.btnLegendDel.Name = "btnLegendDel";
            this.btnLegendDel.Size = new System.Drawing.Size(82, 23);
            this.btnLegendDel.TabIndex = 11;
            this.btnLegendDel.Text = "删除";
            this.btnLegendDel.UseVisualStyleBackColor = true;
            this.btnLegendDel.Click += new System.EventHandler(this.btnLegendDel_Click);
            // 
            // btnLegendAdd
            // 
            this.btnLegendAdd.Location = new System.Drawing.Point(1, 199);
            this.btnLegendAdd.Name = "btnLegendAdd";
            this.btnLegendAdd.Size = new System.Drawing.Size(82, 23);
            this.btnLegendAdd.TabIndex = 10;
            this.btnLegendAdd.Text = "添加";
            this.btnLegendAdd.UseVisualStyleBackColor = true;
            this.btnLegendAdd.Click += new System.EventHandler(this.btnLegendAdd_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 144);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "图例配置";
            // 
            // LegendlistBox
            // 
            this.LegendlistBox.FormattingEnabled = true;
            this.LegendlistBox.ItemHeight = 12;
            this.LegendlistBox.Location = new System.Drawing.Point(3, 159);
            this.LegendlistBox.Name = "LegendlistBox";
            this.LegendlistBox.Size = new System.Drawing.Size(161, 40);
            this.LegendlistBox.TabIndex = 8;
            this.LegendlistBox.SelectedIndexChanged += new System.EventHandler(this.LegendlistBox_SelectedIndexChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // RadarAlertApp
            // 
            this.AcceptButton = this.Sure;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(508, 422);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.propertyTree1);
            this.Controls.Add(this.Apply);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Sure);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RadarAlertApp";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "雷达报警图外观";
            this.Load += new System.EventHandler(this.RadarAlertApp_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertyTree1)).EndInit();
            this.propertyTree1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ppPane1)).EndInit();
            this.ppPane1.ResumeLayout(false);
            this.ppPane1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ppPane2)).EndInit();
            this.ppPane2.ResumeLayout(false);
            this.ppPane2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Apply;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Sure;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox VarcheckedListBox;
        private System.Windows.Forms.ListBox ApplistBox;
        private System.Windows.Forms.Button btnTitleDel;
        private System.Windows.Forms.Button btnTitleAdd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox TitlelistBox;
        private System.Windows.Forms.CheckBox Area3Dcheck;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CBlableStyle;
        private System.Windows.Forms.ComboBox CBdrawingStyle;
        private System.Windows.Forms.CheckBox AxisLineCheck;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox CBreportMode;
        private WRM.Windows.Forms.PropertyTree propertyTree1;
        private WRM.Windows.Forms.PropertyPane ppPane1;
        private System.Windows.Forms.ImageList imageList2;
        private WRM.Windows.Forms.PropertyPane ppPane2;
        private System.Windows.Forms.Button btnLegendDel;
        private System.Windows.Forms.Button btnLegendAdd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox LegendlistBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox Dechecked;
        private System.Windows.Forms.CheckBox Allcheck;
        private System.Windows.Forms.Timer timer1;
    }
}