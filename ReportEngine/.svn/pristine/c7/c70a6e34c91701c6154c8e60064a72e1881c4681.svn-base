using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMS.Libraries.ToolControls.PMSPublicInfo;

namespace PMS.Libraries.ToolControls
{
    public class MESReportIndependentRun
    {
        public static bool RunReport(MESCustomViewIdentity identity,Dictionary<string, object> vars)
        {
            if(identity.IsSpecifiedVersion)
            {
                return RunReport(identity.ViewID,vars);

            }
            else
            {
                string strfilepath =  CurrentPrjInfo.GetViewFile(identity.ParentID, identity.ViewName);
                return RunReport(strfilepath,vars);
            }
            
        }

        public static bool RunReport(Guid id, Dictionary<string, object> vars)
        {
            try
            {
                string filePath = PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetReportFile(id);
                return RunReport(filePath, vars);
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        private static bool RunReport(string rptFilePath, Dictionary<string, object> vars)
        {
            try
            {
                ReportViewerDoModalForm form = new ReportViewerDoModalForm();
                form.Run(rptFilePath,vars);
                form.ShowDialog();
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool ExportReport(MESCustomViewIdentity identity, Dictionary<string, object> vars, string fileFullPath)
        {
            if (identity.IsSpecifiedVersion)
            {
                return ExportReport(identity.ViewID, vars, fileFullPath);
            }
            else
            {
                string strfilepath = CurrentPrjInfo.GetViewFile(identity.ParentID, identity.ViewName);
                return ExportReport(strfilepath, vars, fileFullPath);
            }

        }

        public static bool ExportReport(Guid id, Dictionary<string, object> vars, string fileFullPath)
        {
            try
            {
                string filePath = PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetReportFile(id);
                return ExportReport(filePath, vars, fileFullPath);
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        private static bool ExportReport(string rptFilePath, Dictionary<string, object> vars, string fileFullPath)
        {
            try
            {
                MESReportViewer mesReportViewer1 = new MESReportViewer();
                mesReportViewer1.RunMESReport(rptFilePath, vars);
                mesReportViewer1.ExportEx(fileFullPath);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

    }
}
