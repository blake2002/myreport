using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Reflection;

namespace PMS.Libraries.ToolControls
{
    public partial class ExportWait : Form
    {
        private DateTime Started;
        private int count = 0;
        private object _Viewer;
        private string _strFileName = null;
        private string _RptFileName = null;

        public ExportWait(string strFileName, string RptFileName, object sender)
        {
            try
            {
                InitializeComponent();
                _Viewer = sender;
                _strFileName = strFileName;
                _RptFileName = RptFileName;
            }
            catch(System.Exception ex)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Info(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, ex.Message, false);
            }
        }
        public void StopTimer()
        {
            timer1.Stop();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (progressBar1.Value >= 100)
                {
                    progressBar1.Value = 0;
                }
                progressBar1.Value = progressBar1.Value + 20;
                ++count;
                TimeSpan time = DateTime.Now - Started;
                lblTimeTaken.Text = (System.Convert.ToDateTime(time.ToString())).ToString("HH:mm:ss");
            }
            catch(System.Exception ex)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Info(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, ex.Message, false);
            }
        }

        private void DialogWait_Load(object sender, EventArgs e)
        {
            Started = DateTime.Now;
            timer1.Interval = 1000;
            timer1_Tick(null, null);
            timer1.Start();
            if (null != _Viewer && _Viewer is PMSReport.PMSReportViewer)
            {
                PMSReport.PMSReportViewer viewer = _Viewer as PMSReport.PMSReportViewer;
                Thread bb = new System.Threading.Thread(new ThreadStart(delegate
                    {
                        MESReportServer.MESReportService service = new MESReportServer.MESReportService();
                        service.SaveAsDrpt(_strFileName, _RptFileName, viewer);
                        StopTimer();
                        DialogResult = DialogResult.OK;
                    }));
                bb.Start();
            }
        }
         
    }
}