namespace PMS.Libraries.ToolControls.PmsSheet.PmsPublicData
{
    partial class OkCancelText
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
            this.label1 = new System.Windows.Forms.Label();
            this.Ok = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.No = new System.Windows.Forms.TextBox();
            this.Sure = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "确定按钮文本:";
            // 
            // Ok
            // 
            this.Ok.Location = new System.Drawing.Point(138, 48);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(142, 21);
            this.Ok.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "取消按钮文本:";
            // 
            // No
            // 
            this.No.Location = new System.Drawing.Point(138, 101);
            this.No.Name = "No";
            this.No.Size = new System.Drawing.Size(142, 21);
            this.No.TabIndex = 1;
            // 
            // Sure
            // 
            this.Sure.Location = new System.Drawing.Point(83, 156);
            this.Sure.Name = "Sure";
            this.Sure.Size = new System.Drawing.Size(75, 31);
            this.Sure.TabIndex = 2;
            this.Sure.Text = "确定";
            this.Sure.UseVisualStyleBackColor = true;
            this.Sure.Click += new System.EventHandler(this.Sure_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(175, 156);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 31);
            this.Cancel.TabIndex = 2;
            this.Cancel.Text = "取消";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // OkCancelText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 206);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Sure);
            this.Controls.Add(this.No);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Ok);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OkCancelText";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置文本";
            this.Load += new System.EventHandler(this.OkCancelText_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Ok;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox No;
        private System.Windows.Forms.Button Sure;
        private System.Windows.Forms.Button Cancel;
    }
}