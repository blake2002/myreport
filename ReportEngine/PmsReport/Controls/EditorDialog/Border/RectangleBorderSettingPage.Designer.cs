using PMS.Libraries.ToolControls.ReportControls;
namespace PMS.Libraries.ToolControls.Report.Controls.EditorDialog
{
    partial class RectangleBorderSettingPage
    {
    
        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.BorderWidthTb = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.BorderColorLabel = new System.Windows.Forms.Label();
            this.ColorBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.displayBorder = new PMS.Libraries.ToolControls.Report.Controls.EditorDialog.BorderCtrl();
            this.label5 = new System.Windows.Forms.Label();
            this.LineTypeCmb = new PMS.Libraries.ToolControls.ReportControls.LineCombobox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.BorderWidthTb)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "边框宽度:";
            // 
            // BorderWidthTb
            // 
            this.BorderWidthTb.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BorderWidthTb.Location = new System.Drawing.Point(151, 4);
            this.BorderWidthTb.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.BorderWidthTb.Name = "BorderWidthTb";
            this.BorderWidthTb.Size = new System.Drawing.Size(81, 21);
            this.BorderWidthTb.TabIndex = 1;
            this.BorderWidthTb.ValueChanged += new System.EventHandler(this.BorderWidthValueChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(238, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "边框颜色:";
            // 
            // BorderColorLabel
            // 
            this.BorderColorLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BorderColorLabel.BackColor = System.Drawing.Color.Black;
            this.BorderColorLabel.Location = new System.Drawing.Point(297, 9);
            this.BorderColorLabel.Name = "BorderColorLabel";
            this.BorderColorLabel.Size = new System.Drawing.Size(33, 12);
            this.BorderColorLabel.TabIndex = 3;
            // 
            // ColorBtn
            // 
            this.ColorBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ColorBtn.Location = new System.Drawing.Point(336, 2);
            this.ColorBtn.Name = "ColorBtn";
            this.ColorBtn.Size = new System.Drawing.Size(46, 23);
            this.ColorBtn.TabIndex = 4;
            this.ColorBtn.Text = "...";
            this.ColorBtn.UseVisualStyleBackColor = true;
            this.ColorBtn.Click += new System.EventHandler(this.ColrBtnClick);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.Silver;
            this.label3.Location = new System.Drawing.Point(3, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(658, 1);
            this.label3.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.displayBorder);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(142, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(328, 222);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "预览效果";
            // 
            // displayBorder
            // 
            this.displayBorder.BackColor = System.Drawing.SystemColors.Control;
            this.displayBorder.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.displayBorder.Location = new System.Drawing.Point(59, 29);
            this.displayBorder.Name = "displayBorder";
            this.displayBorder.Size = new System.Drawing.Size(200, 162);
            this.displayBorder.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(388, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "线条类型:";
            // 
            // LineTypeCmb
            // 
            this.LineTypeCmb.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.LineTypeCmb.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.LineTypeCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LineTypeCmb.FormattingEnabled = true;
            this.LineTypeCmb.Location = new System.Drawing.Point(452, 3);
            this.LineTypeCmb.Name = "LineTypeCmb";
            this.LineTypeCmb.Size = new System.Drawing.Size(130, 22);
            this.LineTypeCmb.TabIndex = 14;
            this.LineTypeCmb.SelectedIndexChanged += new System.EventHandler(this.LineTypeCmb_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(212, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "单击边框激活或者去掉边框";
            // 
            // RectangleBorderSettingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LineTypeCmb);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ColorBtn);
            this.Controls.Add(this.BorderColorLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BorderWidthTb);
            this.Controls.Add(this.label1);
            this.Name = "RectangleBorderSettingPage";
            this.Size = new System.Drawing.Size(664, 346);
            this.StyleName = "矩形边框";
            ((System.ComponentModel.ISupportInitialize)(this.BorderWidthTb)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown BorderWidthTb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label BorderColorLabel;
        private System.Windows.Forms.Button ColorBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private BorderCtrl displayBorder;
        private System.Windows.Forms.Label label5;
        private LineCombobox LineTypeCmb;
        private System.Windows.Forms.Label label4;
    }
}
