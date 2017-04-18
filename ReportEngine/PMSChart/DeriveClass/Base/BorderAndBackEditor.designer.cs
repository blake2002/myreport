namespace PMS.Libraries.ToolControls.PMSChart
{
    partial class BorderAndBackEditor
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
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.ccboBackSecondColor = new ColorComBoBox.ColorComBoBox();
            this.ccboBackColor = new ColorComBoBox.ColorComBoBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.cboBorderDashStyle = new System.Windows.Forms.ComboBox();
            this.ccboBorderColor = new ColorComBoBox.ColorComBoBox();
            this.lblBorderStyle = new System.Windows.Forms.Label();
            this.lblBorderColor = new System.Windows.Forms.Label();
            this.nudBorderWidth = new System.Windows.Forms.NumericUpDown();
            this.lblBorderWidth = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboBackHatchStyle = new System.Windows.Forms.ComboBox();
            this.cboBackGradientStyle = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chartPreview = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.nudBorderWidth)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // ccboBackSecondColor
            // 
            this.ccboBackSecondColor.Location = new System.Drawing.Point(79, 48);
            this.ccboBackSecondColor.Name = "ccboBackSecondColor";
            this.ccboBackSecondColor.SelectedItem = System.Drawing.Color.White;
            this.ccboBackSecondColor.Size = new System.Drawing.Size(100, 22);
            this.ccboBackSecondColor.TabIndex = 21;
            this.ccboBackSecondColor.Text = "colorComBoBox1";
            this.ccboBackSecondColor.SelectColorChanged += new ColorComBoBox.ColorChangeEvent(this.ccboBackSecondColor_SelectColorChanged);
            // 
            // ccboBackColor
            // 
            this.ccboBackColor.Location = new System.Drawing.Point(79, 23);
            this.ccboBackColor.Name = "ccboBackColor";
            this.ccboBackColor.SelectedItem = System.Drawing.Color.White;
            this.ccboBackColor.Size = new System.Drawing.Size(100, 22);
            this.ccboBackColor.TabIndex = 22;
            this.ccboBackColor.Text = "colorComBoBox1";
            this.ccboBackColor.SelectColorChanged += new ColorComBoBox.ColorChangeEvent(this.ccboBackColor_SelectColorChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "渐变方向:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "结束颜色:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "起始颜色:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(228, 287);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(62, 23);
            this.btnCancel.TabIndex = 32;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(152, 287);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(62, 23);
            this.btnOK.TabIndex = 31;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cboBorderDashStyle
            // 
            this.cboBorderDashStyle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboBorderDashStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBorderDashStyle.FormattingEnabled = true;
            this.cboBorderDashStyle.Location = new System.Drawing.Point(79, 47);
            this.cboBorderDashStyle.Name = "cboBorderDashStyle";
            this.cboBorderDashStyle.Size = new System.Drawing.Size(100, 22);
            this.cboBorderDashStyle.TabIndex = 42;
            this.cboBorderDashStyle.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cboBorderDashStyle_DrawItem);
            this.cboBorderDashStyle.SelectedIndexChanged += new System.EventHandler(this.cboBorderDashStyle_SelectedIndexChanged);
            // 
            // ccboBorderColor
            // 
            this.ccboBorderColor.Location = new System.Drawing.Point(79, 20);
            this.ccboBorderColor.Name = "ccboBorderColor";
            this.ccboBorderColor.SelectedItem = System.Drawing.Color.Black;
            this.ccboBorderColor.Size = new System.Drawing.Size(100, 21);
            this.ccboBorderColor.TabIndex = 41;
            this.ccboBorderColor.Text = "ccboBorderColor";
            this.ccboBorderColor.SelectColorChanged += new ColorComBoBox.ColorChangeEvent(this.ccboBorderColor_SelectColorChanged);
            // 
            // lblBorderStyle
            // 
            this.lblBorderStyle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblBorderStyle.AutoSize = true;
            this.lblBorderStyle.Location = new System.Drawing.Point(19, 53);
            this.lblBorderStyle.Name = "lblBorderStyle";
            this.lblBorderStyle.Size = new System.Drawing.Size(59, 12);
            this.lblBorderStyle.TabIndex = 40;
            this.lblBorderStyle.Text = "线条类型:";
            // 
            // lblBorderColor
            // 
            this.lblBorderColor.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblBorderColor.AutoSize = true;
            this.lblBorderColor.Location = new System.Drawing.Point(19, 26);
            this.lblBorderColor.Name = "lblBorderColor";
            this.lblBorderColor.Size = new System.Drawing.Size(59, 12);
            this.lblBorderColor.TabIndex = 39;
            this.lblBorderColor.Text = "边框颜色:";
            // 
            // nudBorderWidth
            // 
            this.nudBorderWidth.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nudBorderWidth.Location = new System.Drawing.Point(79, 73);
            this.nudBorderWidth.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudBorderWidth.Name = "nudBorderWidth";
            this.nudBorderWidth.Size = new System.Drawing.Size(100, 21);
            this.nudBorderWidth.TabIndex = 38;
            this.nudBorderWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBorderWidth.ValueChanged += new System.EventHandler(this.nudBorderWidth_ValueChanged);
            // 
            // lblBorderWidth
            // 
            this.lblBorderWidth.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblBorderWidth.AutoSize = true;
            this.lblBorderWidth.Location = new System.Drawing.Point(19, 78);
            this.lblBorderWidth.Name = "lblBorderWidth";
            this.lblBorderWidth.Size = new System.Drawing.Size(59, 12);
            this.lblBorderWidth.TabIndex = 37;
            this.lblBorderWidth.Text = "边框宽度:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboBackHatchStyle);
            this.groupBox1.Controls.Add(this.cboBackGradientStyle);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ccboBackSecondColor);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.ccboBackColor);
            this.groupBox1.Location = new System.Drawing.Point(13, 140);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 135);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "背景";
            // 
            // cboBackHatchStyle
            // 
            this.cboBackHatchStyle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboBackHatchStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBackHatchStyle.FormattingEnabled = true;
            this.cboBackHatchStyle.Location = new System.Drawing.Point(79, 75);
            this.cboBackHatchStyle.Name = "cboBackHatchStyle";
            this.cboBackHatchStyle.Size = new System.Drawing.Size(100, 22);
            this.cboBackHatchStyle.TabIndex = 74;
            this.cboBackHatchStyle.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cboBackHatchStyle_DrawItem);
            this.cboBackHatchStyle.SelectedIndexChanged += new System.EventHandler(this.cboBackHatchStyle_SelectedIndexChanged);
            // 
            // cboBackGradientStyle
            // 
            this.cboBackGradientStyle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboBackGradientStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBackGradientStyle.FormattingEnabled = true;
            this.cboBackGradientStyle.Location = new System.Drawing.Point(79, 102);
            this.cboBackGradientStyle.Name = "cboBackGradientStyle";
            this.cboBackGradientStyle.Size = new System.Drawing.Size(100, 22);
            this.cboBackGradientStyle.TabIndex = 74;
            this.cboBackGradientStyle.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cboBackGradientStyle_DrawItem);
            this.cboBackGradientStyle.SelectedIndexChanged += new System.EventHandler(this.cboBackGradientStyle_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "背景图形:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cboBorderDashStyle);
            this.groupBox2.Controls.Add(this.ccboBorderColor);
            this.groupBox2.Controls.Add(this.lblBorderStyle);
            this.groupBox2.Controls.Add(this.lblBorderColor);
            this.groupBox2.Controls.Add(this.nudBorderWidth);
            this.groupBox2.Controls.Add(this.lblBorderWidth);
            this.groupBox2.Location = new System.Drawing.Point(13, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 110);
            this.groupBox2.TabIndex = 44;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "边框";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chartPreview);
            this.groupBox3.Location = new System.Drawing.Point(228, 14);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 261);
            this.groupBox3.TabIndex = 45;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "预览";
            // 
            // chartPreview
            // 
            this.chartPreview.BackColor = System.Drawing.Color.Empty;
            this.chartPreview.BorderlineColor = System.Drawing.Color.Empty;
            this.chartPreview.Location = new System.Drawing.Point(15, 20);
            this.chartPreview.Name = "chartPreview";
            this.chartPreview.Size = new System.Drawing.Size(170, 228);
            this.chartPreview.TabIndex = 0;
            this.chartPreview.Text = "chart1";
            title1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Left;
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "TextPreview";
            title1.Text = "示例文字/TextPreview";
            title1.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Horizontal;
            this.chartPreview.Titles.Add(title1);
            // 
            // BorderAndBackEditor
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(439, 321);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BorderAndBackEditor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "边框和背景";
            this.Load += new System.EventHandler(this.BorderAndBackEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudBorderWidth)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ColorComBoBox.ColorComBoBox ccboBackSecondColor;
        private ColorComBoBox.ColorComBoBox ccboBackColor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox cboBorderDashStyle;
        private ColorComBoBox.ColorComBoBox ccboBorderColor;
        private System.Windows.Forms.Label lblBorderStyle;
        private System.Windows.Forms.Label lblBorderColor;
        private System.Windows.Forms.NumericUpDown nudBorderWidth;
        private System.Windows.Forms.Label lblBorderWidth;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboBackHatchStyle;
        private System.Windows.Forms.ComboBox cboBackGradientStyle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPreview;
    }
}