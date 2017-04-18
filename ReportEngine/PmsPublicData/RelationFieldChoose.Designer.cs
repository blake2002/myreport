namespace PMS.Libraries.ToolControls.PmsSheet.PmsPublicData
{
    partial class RelationFieldChoose
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.HideSelection = false;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.MultiSelect = false;
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(305, 222);
            this.listBox1.TabIndex = 0;
            this.listBox1.UseCompatibleStateImageBehavior = false;
            this.listBox1.View = System.Windows.Forms.View.Details;
            this.listBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseClick_1);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "表名";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "描述";
            this.columnHeader2.Width = 150;
            // 
            // RelationFieldChoose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listBox1);
            this.Name = "RelationFieldChoose";
            this.Size = new System.Drawing.Size(305, 222);
            this.Load += new System.EventHandler(this.RelationFieldChoose_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listBox1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;

    }
}
