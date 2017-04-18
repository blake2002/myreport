using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetSCADA.ReportEngine
{
    /// <summary>
    /// 变量配置视图
    /// </summary>
    public partial class VarConfigForm : Form
    {
		//public PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.CustomPageRun customPageRun1;
        public VarConfigForm()
        {
            InitializeComponent();
        }

		protected override void OnLoad (EventArgs e)
		{
			base.OnLoad (e);

		// this.customPageRun1 = new PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.CustomPageRun();
		// this.customPageRun1.Connections = null;
		// this.customPageRun1.DisplayList = ((System.Collections.Generic.List<PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.PmsDisplay>)(resources.GetObject("customPageRun1.DisplayList")));
		// this.customPageRun1.Dock = System.Windows.Forms.DockStyle.Fill;
		// this.customPageRun1.FilterDataSet = null;
		// this.customPageRun1.Location = new System.Drawing.Point(0, 0);
		// this.customPageRun1.Name = "customPageRun1";
		// this.customPageRun1.QueryResultList = ((System.Collections.Generic.List<PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.QueryResultObj>)(resources.GetObject("customPageRun1.QueryResultList")));
		// this.customPageRun1.RunPageData = null;
		// this.customPageRun1.Size = new System.Drawing.Size(413, 275);
		// this.customPageRun1.TabIndex = 0;
		// this.customPageRun1.TableName = null;
		// this.splitContainer1.Panel1.Controls.Add(this.customPageRun1);
		}

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK; 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; 
        } 
    }
}
