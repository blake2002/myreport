using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMS.Libraries.ToolControls.PMSPublicInfo
{
    public class LogManager
    {
        private static bool InsertMsg(DateTime dtLogTime, string strSource,
            byte byteType, string strUID,
            string strActionName, string strActionObject, byte byteResult,
            byte byteSigType, string strSigAffirmUID, DateTime dtSigAffirmTime,byte byteSigAffirmResult,
            string strSigCheckUID, DateTime dtSigCheckTime,byte byteSigCheckResult,
            string strDescription)
        {
            try
            {
                s_LogInfo LogInfo = new s_LogInfo();
                LogInfo.LogTime = dtLogTime;
                LogInfo.Source = strSource;
                LogInfo.LogType = byteType;
                LogInfo.UID_ = strUID;
                LogInfo.ActionName = strActionName;
                LogInfo.ActionObject = strActionObject;
                LogInfo.Result = byteResult;
                LogInfo.SigType = byteSigType;
                LogInfo.SigAffirmUID = strSigAffirmUID;
                LogInfo.SigAffirmTime = dtSigAffirmTime;
                LogInfo.SigAffirmResult = byteSigAffirmResult;
                LogInfo.SigCheckUID = strSigCheckUID;
                LogInfo.SigCheckTime = dtSigCheckTime;
                LogInfo.SigCheckResult = byteSigCheckResult;
                LogInfo.Description = strDescription;
                PMSDBStructure.PMSCenterDataContext.AddTos_LogInfo(LogInfo);
                PMSDBStructure.PMSCenterDataContext.SaveChanges();
            }
            catch (System.Exception e)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.PublicFunctionClass.EnhancedStackTrace(e);
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(e.Message);
                return false;
            }
            return true;
        }

        private static bool InsertMsg(DateTime dtLogTime, string strSource,
            byte byteType, string strUID,
            string strDescription)
        {
            try
            {
                s_LogInfo LogInfo = new s_LogInfo();
                LogInfo.LogTime = dtLogTime;
                LogInfo.Source = strSource;
                LogInfo.LogType = byteType;
                LogInfo.UID_ = strUID;
                LogInfo.Description = strDescription;
                PMSDBStructure.PMSCenterDataContext.AddTos_LogInfo(LogInfo);
                PMSDBStructure.PMSCenterDataContext.SaveChanges();
            }
            catch (System.Exception e)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.PublicFunctionClass.EnhancedStackTrace(e);
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(e.Message);
                return false;
            }
            return true;
        }

        private static bool InsertMsg(DateTime dtLogTime, string strSource,
            byte byteType, string strUID,
            string strDescription, byte byteResult)
        {
            try
            {
                s_LogInfo LogInfo = new s_LogInfo();
                LogInfo.LogTime = dtLogTime;
                LogInfo.Source = strSource;
                LogInfo.LogType = byteType;
                LogInfo.UID_ = strUID;
                LogInfo.Description = strDescription;
                LogInfo.Result = byteResult;
                PMSDBStructure.PMSCenterDataContext.AddTos_LogInfo(LogInfo);
                PMSDBStructure.PMSCenterDataContext.SaveChanges();
            }
            catch (System.Exception e)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.PublicFunctionClass.EnhancedStackTrace(e);
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(e.Message);
                return false;
            }
            return true;
        }

        public static bool Info(string msg)
        {
            return InsertMsg(System.DateTime.Now, CurrentPrjInfo.LoacalIPAddress.ToString(),
                (byte)LogMsgObj.LogMsgType.Info, CurrentPrjInfo.CurrentLoginUserID,
                msg);
        }

        public static bool Info(string msg,bool bResult)
        {
            return InsertMsg(System.DateTime.Now, CurrentPrjInfo.LoacalIPAddress.ToString(),
                (byte)LogMsgObj.LogMsgType.Info, CurrentPrjInfo.CurrentLoginUserID,
                msg, System.Convert.ToByte(bResult));
        }

        public static bool Warn(string msg)
        {
            return InsertMsg(System.DateTime.Now, CurrentPrjInfo.LoacalIPAddress.ToString(),
                (byte)LogMsgObj.LogMsgType.Warnning, CurrentPrjInfo.CurrentLoginUserID,
                msg);
        }

        public static bool Error(string msg)
        {
            return InsertMsg(System.DateTime.Now, CurrentPrjInfo.LoacalIPAddress.ToString(),
                (byte)LogMsgObj.LogMsgType.Error, CurrentPrjInfo.CurrentLoginUserID,
                msg);
        }
    }
}
