using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PMS.Libraries.ToolControls
{
    public partial class ReportViewerDoModalForm : Form
    {
        public ReportViewerDoModalForm()
        {
            InitializeComponent();
            this.mesReportViewer1.SetToolBar(0, false);
        }

        public bool Run(string strFileFullPath, Dictionary<string, object> vars)
        {
            return this.mesReportViewer1.RunMESReport(strFileFullPath,vars);
        }

        private void ReportViewerDoModalForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.mesReportViewer1.ReleaseReportResource();
        }
    }
}
