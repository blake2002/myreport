using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMS.Libraries.ToolControls.PMSPublicInfo;
using System.Windows.Forms;
using System.ComponentModel;
using PMS.Libraries.ToolControls.PMSReport;
using System.IO;
using System.Data.SqlClient;

namespace MESReportServer
{
    public class MESReportService : IMESReportService
    {

        private PMS.Libraries.ToolControls.MESReportRun _reportRun = new PMS.Libraries.ToolControls.MESReportRun();

        public MESReportService()
        {
            Start();
        }

        public bool Start()
        {
            try
            {
                // 初始化pmspublicinfo相关信息
                //ConnectSqlServer("192.168.0.171\\SQL2005", "MESCenter", "sa", "123");
            }
            catch (System.Exception ex)
            {
                return false;
            }
            
            return true;
        }

        public bool QueryReport(string rptFilePath, PMS.Libraries.ToolControls.PMSReport.PMSReportViewer viewer)
        {
            return _reportRun.QueryRptReport(rptFilePath, viewer);
        }

        public bool QueryReport(string rptFilePath, NetSCADA.ReportEngine.ReportViewer viewer)
        {
            return _reportRun.QueryRptReport(rptFilePath, viewer);
        }

        public bool QueryReport(string rptFilePath, string reportVarFilepath, PMS.Libraries.ToolControls.PMSReport.PMSReportViewer viewer)
        {
            return _reportRun.QueryFileReport(rptFilePath, reportVarFilepath, viewer);
        }

        public bool RunReport(string strFileFullPath, PMS.Libraries.ToolControls.PMSReport.PMSReportViewer viewer)
        {
            return _reportRun.RunReport(strFileFullPath, viewer);
        }

        public bool RunReport(string strFileFullPath, NetSCADA.ReportEngine.ReportViewer viewer)
        {
            return _reportRun.RunReport(strFileFullPath, viewer);
        }

        public void ReleaseReportResource()
        {
            _reportRun.ReleaseReportResource();
        }

        public bool GeneratePdfReport(byte[] Content, object FilterCondition, string strFileName)
        {
            return _reportRun.GeneratePdfReport(Content, FilterCondition, strFileName);
        }

        public bool SetRptReport(string rptFilePath, PMS.Libraries.ToolControls.PMSReport.PMSReportViewer viewer)
        {
            return _reportRun.SetRptReport(rptFilePath, viewer);
        }

        public bool SetRptReport(string rptFilePath, NetSCADA.ReportEngine.ReportViewer viewer)
        {
            return _reportRun.SetRptReport(rptFilePath, viewer);
        }

        public bool SetDrptReport(string drptFilePath, PMS.Libraries.ToolControls.PMSReport.PMSReportViewer viewer)
        {
            return _reportRun.SetDrptReport(drptFilePath, viewer);
        }

        public bool SetDrptReport(string drptFilePath, PMS.Libraries.ToolControls.PMSReport.MESReportViewer viewer)
        {
            return _reportRun.SetDrptReportEx(drptFilePath, viewer);
        }

        public bool SetOrptReport(string orptFilePath, PMS.Libraries.ToolControls.PMSReport.MESObjectReportViewer viewer)
        {
            return _reportRun.SetOrptReport(orptFilePath, viewer);
        }

        public bool QueryReport(PMS.Libraries.ToolControls.PMSReport.PMSReportViewer viewer)
        {
            return _reportRun.QueryReport(viewer);
        }

        public bool QueryReport(NetSCADA.ReportEngine.ReportViewer viewer)
        {
            return _reportRun.QueryReport(viewer);
        }

        public bool SaveAsDrpt(string drptFilePath, string rptFilePath, PMS.Libraries.ToolControls.PMSReport.PMSReportViewer viewer)
        {
            return _reportRun.SaveAsDrpt(drptFilePath, rptFilePath, viewer);
        }

        public bool SaveAsDrpt(string drptFilePath, PMS.Libraries.ToolControls.PMSReport.PMSReportViewer viewer)
        {
            return _reportRun.SaveAsDrptEx(drptFilePath, viewer);
        }

        public bool SaveAsOrpt(string orptFilePath, PMS.Libraries.ToolControls.PMSReport.PMSReportViewer viewer)
        {
            return _reportRun.SaveAsOrpt(orptFilePath, viewer);
        }

        public bool SaveAsRptx(string rptxFilePath, PMS.Libraries.ToolControls.PMSReport.PMSReportViewer viewer)
        {
            return _reportRun.SaveAsRptx(rptxFilePath, viewer);
        }

        public bool SaveAsRptx(string rptxFilePath, NetSCADA.ReportEngine.ReportViewer viewer)
        {
            return _reportRun.SaveAsRptx(rptxFilePath, viewer);
        }

        private int ConnectSqlServer(string myServerName
                                    , string myDBName
                                    , string userId
                                    , string passWord)
        {
            if (string.IsNullOrEmpty(myServerName))
            {
                string strText = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMSDBServerNULL");
                string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Warnning");
                MessageBox.Show(strText, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }

            if (string.IsNullOrEmpty(myDBName))
            {
                string strText = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMSDBDBNameNULL");
                string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Warnning");
                MessageBox.Show(strText, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }

            if (string.IsNullOrEmpty(userId))
            {
                string strText = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMSDBUserIDNULL");
                string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Warnning");
                MessageBox.Show(strText, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }

            ConnectType iProtocolType = (ConnectType)(1);

            string portID = "1433";
            //string strConnect = "Data Source=" + myServerName +
            //        ";Initial Catalog=" + myDBName +
            //        ";User Id=" + userId +
            //        ";Password=" + passWord +
            //        ";";

            try
            {

                //SqlStructure sqlStructure = new SqlStructure(myServerName, myDBName, userId, passWord, iProtocolType, portID);
                SqlStructure.InitSqlStructure(myServerName, myDBName, userId, passWord, iProtocolType, portID);

                SqlConnection _conn = SqlStructure.GetSqlConncetion();
                if (_conn == null)
                {
                    return 0;
                }
                if (!SqlStructure.ConnectSMOServer())
                {
                    return 0;
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(ex.ToString());
                return 0;
            }

            return 1;
        }
    }
}
