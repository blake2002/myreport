using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.IO;

namespace PMS.Libraries.ToolControls.PMSPublicInfo
{
    public class DBFileManager
    {
        public static System.Data.Common.DbTransaction Transaction
        {
            get;
            set;
        }

        public static bool UpdateFile(s_CfgFileInfo ConfigFileInfo, bool acceptChangesDuringSave)
        {
            try
            {
                var q = (from PMSInfo in PMSDBStructure.PMSCenterDataContext.s_CfgFileInfo
                         where PMSInfo.FID == ConfigFileInfo.FID
                         select PMSInfo).First();
                q = ConfigFileInfo;
                if (acceptChangesDuringSave)
                    PMSDBStructure.PMSCenterDataContext.SaveChanges();
                //else
                //    PMSDBStructure.PMSCenterDataContext.SaveChanges(acceptChangesDuringSave);
            }
            catch (System.Exception e)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, e.Message, true);
                return false;
            }
            return true;
        }

        public static bool UpdateFile(s_CfgFileInfo ConfigFileInfo)
        {
            return UpdateFile(ConfigFileInfo, true);
        }

        public static bool UpdateFile(s_ServerCfgFiles ConfigFileInfo, bool acceptChangesDuringSave)
        {
            try
            {
                var q = (from PMSInfo in PMSDBStructure.PMSCenterDataContext.s_ServerCfgFiles
                         where PMSInfo.FID == ConfigFileInfo.FID
                         select PMSInfo).First();
                q = ConfigFileInfo;
                if (acceptChangesDuringSave)
                    PMSDBStructure.PMSCenterDataContext.SaveChanges();
            }
            catch (System.Exception e)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, e.Message, true);
                return false;
            }
            return true;
        }

        public static bool UpdateFile(s_ServerCfgFiles ConfigFileInfo)
        {
            return UpdateFile(ConfigFileInfo, true);
        }

        public static bool UploadFile(s_CfgFileInfo ConfigFileInfo)
        {
            return UploadFile(ConfigFileInfo, true);
        }

        public static bool UploadFile(s_CfgFileInfo ConfigFileInfo, bool acceptChangesDuringSave)
        {
            // 
            try
            {
                PMSDBStructure.PMSCenterDataContext.AddTos_CfgFileInfo(ConfigFileInfo);
                if (acceptChangesDuringSave)
                    PMSDBStructure.PMSCenterDataContext.SaveChanges();
                //else
                //    PMSDBStructure.PMSCenterDataContext.SaveChanges(acceptChangesDuringSave);
    //            System.Data.SqlClient.SqlConnection connection = SqlStructure.GetSqlConncetion();
    //            System.Data.SqlClient.SqlCommand command = connection.CreateCommand();
    //            System.Data.SqlClient.SqlParameter parameter;
    //            int rowsInserted;
    //            System.DateTime startTime;
    //            System.DateTime endTime;

    //            command.CommandText = @"INSERT INTO [s_CfgFileInfo] ([FID]
    //                                ,[Name]
    //                                ,[RelativePath]
    //                                ,[Type]
    //                                ,[Content]
    //                                ,[TimeStamp]
    //                                ,[Version]
    //                                ,[Check]
    //                                ,[Current] 
    //                                ,[CheckUserID]) 
    //                            VALUES 
    //                                (@FID
    //                                 ,@Name
    //                                 ,@RelativePath
    //                                 ,@Type
    //                                 ,@Content
    //                                 ,@TimeStamp
    //                                 ,@Version
    //                                 ,@Check
    //                                 ,@Current
    //                                 ,@CheckUserID)";
    //            command.CommandType = System.Data.CommandType.Text;

    //            parameter = new System.Data.SqlClient.SqlParameter("@FID", System.Data.SqlDbType.UniqueIdentifier);
    //            parameter.Value = System.Guid.NewGuid(); file.Substring(file.LastIndexOf('\\') + 1);
    //            command.Parameters.Add(parameter);

    //            parameter = new System.Data.SqlClient.SqlParameter("@Name", System.Data.SqlDbType.NVarChar, 50);
    //            parameter.Value = file.Substring(file.LastIndexOf('\\') + 1);
    //            command.Parameters.Add(parameter);

    //            parameter = new System.Data.SqlClient.SqlParameter("@Data", System.Data.SqlDbType.VarBinary);
    //            parameter.Value = System.IO.File.ReadAllBytes(file);
    //            command.Parameters.Add(parameter);

    //            command.Transaction = connection.BeginTransaction();
    //            startTime = System.DateTime.Now;
    //            for (int counter = 0; counter < repeatCount; counter++)
    //            {
    //                rowsInserted = command.ExecuteNonQuery();
    //            }
    //            endTime = System.DateTime.Now;
    //            command.Transaction.Commit();

    //            connection.Close();
            }
            catch (System.Exception e)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, e.Message, true);
                return false;
            }
            return true;
        }

        public static bool UploadRelation(s_CfgFInfo_s_CfgFInfoMap_r RelationInfo)
        {
            return UploadRelation(RelationInfo, true);
        }

        public static bool UploadRelation(s_CfgFInfo_s_CfgFInfoMap_r RelationInfo, bool acceptChangesDuringSave)
        {
            try
            {
                PMSDBStructure.PMSCenterDataContext.AddTos_CfgFInfo_s_CfgFInfoMap_r(RelationInfo);
                //var cmd = PMSDBStructure.PMSCenterDataContext.Connection.CreateCommand();
                //System.Data.Common.DbConnection conn = PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBConnection.GetConnection() as System.Data.Common.DbConnection;
                //var cmd = PMSDBStructure.EConn.CreateCommand();
                //var cmd = conn.CreateCommand();
                //System.Data.Common.DbTransaction tran = conn.BeginTransaction();
                //if (null != Transaction)
                //    cmd.Transaction = Transaction as System.Data.EntityClient.EntityTransaction;
                //string strcmd = "INSERT INTO [s_CfgFInfo_s_CfgFInfoMap_r] ([FID],[MAPID])VALUES('" + RelationInfo.FID.ToString() + "','" + RelationInfo.MAPID.ToString() + "')";
                //strcmd = "INSERT INTO [s_unit] ([name],[description]) VALUES ('1111','dddd')";
                //cmd.CommandText = strcmd;
                //PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBConnection.ExecuteCommandNonQuery(strcmd);
                //cmd.ExecuteNonQuery();


                //PMSDBStructure.PMSCenterDataContext.ExecuteStoreCommand(strcmd, null);
                if (acceptChangesDuringSave)
                    PMSDBStructure.PMSCenterDataContext.SaveChanges();
                //else
                //    PMSDBStructure.PMSCenterDataContext.SaveChanges(acceptChangesDuringSave);
            }
            catch (System.Exception e)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, e.Message, true);
                return false;
            }
            return true;
        }

        public static bool UploadServerCfgFile(s_ServerCfgFiles ConfigFileInfo)
        {
            return UploadServerCfgFile(ConfigFileInfo, true);
        }

        public static bool UploadServerCfgFile(s_ServerCfgFiles ConfigFileInfo, bool acceptChangesDuringSave)
        {
            try
            {
                var q = from cfg in PMSDBStructure.PMSCenterDataContext.s_ServerCfgFiles
                         where cfg.FID == ConfigFileInfo.FID
                         select cfg;
                if (q.Count() > 0)
                {
                    s_ServerCfgFiles s_cfg = q.First();
                    s_cfg.FID = ConfigFileInfo.FID;
                    s_cfg.Name = ConfigFileInfo.Name;
                    s_cfg.Description = ConfigFileInfo.Description;
                    s_cfg.RelativePath = ConfigFileInfo.RelativePath;
                    s_cfg.Check_ = ConfigFileInfo.Check_;
                    s_cfg.CheckUserID = ConfigFileInfo.CheckUserID;
                    s_cfg.TimeStamp = ConfigFileInfo.TimeStamp;
                    //s_cfg = ConfigFileInfo;
                }
                else
                    PMSDBStructure.PMSCenterDataContext.AddTos_ServerCfgFiles(ConfigFileInfo);
                if (acceptChangesDuringSave)
                    PMSDBStructure.PMSCenterDataContext.SaveChanges();
            }
            catch (System.Exception e)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, e.Message, true);
                return false;
            }
            return true;
        }

        public static int SaveChanges()
        {
            return PMSDBStructure.PMSCenterDataContext.SaveChanges();
        }
        
        public static bool UploadFiles(s_CfgFileInfo ConfigFileInfo)
        {
            return false;

        }

        public static bool DownloadFileByUserInfo(s_UserInfo UserInfo)
        {
            // 调用权限管理下载接口

            //调用 参数：接口返回值dictionary
            //DownloadFiles(dictionary);

            return false;
        }

        public static bool DownloadSysViewFile(Binary bi, string strFileFullPath, Guid sysGuid, Guid newGuid)
        {
            if (bi != null)
            {
                byte[] data = bi.ToArray();
                try
                {
                    if (SaveZipFileAndRenameUnzipThenDelZipfile(strFileFullPath, data, sysGuid, newGuid))
                    {
                        return true;
                    }
                }
                catch (System.IO.IOException e)
                {
                    PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.PublicFunctionClass.EnhancedStackTrace(e));
                    return false;
                }
            }
            return false;
        }

        public static bool DownloadFile(byte[] bi, string strFileFullPath)
        {
            if (bi != null)
            {
                byte[] data = bi;//.ToArray();
                try
                {
                    if (SaveZipFileAndUnzipThenDelZipfile(strFileFullPath, data))
                    {
                        return true;
                    }
                }
                catch (System.IO.IOException e)
                {
                    PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.PublicFunctionClass.EnhancedStackTrace(e));
                    return false;
                }
            }
            return false;
        }

        public static bool UnzipCsfFile(s_CfgFileInfo ConfigFileInfo)
        {
            try
            {
                System.Guid fileGuid = ConfigFileInfo.FID;
                string filename = fileGuid.ToString() + ".csf";
                string filePath = System.IO.Path.Combine(PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.ProjectPath, ConfigFileInfo.RelativePath);
                filePath = System.IO.Path.Combine(filePath, filename);

                if (System.IO.File.Exists(filePath))
                {
                    string Ext = System.IO.Path.GetExtension(filePath);
                    if (Ext.Equals(".csf", StringComparison.OrdinalIgnoreCase))
                    {
                        string err = string.Empty;
                        if (PMS.Libraries.ToolControls.PMSPublicInfo.ZipClass.UnZipFile(filePath, string.Empty, out err))
                        {
                            return true;
                        }
                    }
                }
            }
            catch (System.IO.IOException e)
            {
                return false;
            }
            return false;
        }

        public static bool UnzipCsfFile(string csfFilePath)
        {
            try
            {
                if (System.IO.File.Exists(csfFilePath))
                {
                    string Ext = System.IO.Path.GetExtension(csfFilePath);
                    if (Ext.Equals(".csf", StringComparison.OrdinalIgnoreCase))
                    {
                        string err = string.Empty;
                        if (PMS.Libraries.ToolControls.PMSPublicInfo.ZipClass.UnZipFile(csfFilePath, string.Empty, out err))
                        {
                            return true;
                        }
                    }
                }
            }
            catch (System.IO.IOException e)
            {
                return false;
            }
            return false;
        }

        public static bool UnzipCsfFile(string csfFilePath, string unzipPath)
        {
            try
            {
                if (System.IO.File.Exists(csfFilePath))
                {
                    string Ext = System.IO.Path.GetExtension(csfFilePath);
                    if (Ext.Equals(".csf", StringComparison.OrdinalIgnoreCase))
                    {
                        string err = string.Empty;
                        if (PMS.Libraries.ToolControls.PMSPublicInfo.ZipClass.UnZipFile(csfFilePath, unzipPath, out err))
                        {
                            return true;
                        }
                    }
                }
            }
            catch (System.IO.IOException e)
            {
                return false;
            }
            return false;
        }

        public static bool DownloadFile(s_CfgFileInfo ConfigFileInfo)
        {
            System.Guid fileGuid = ConfigFileInfo.FID;
            string filename = fileGuid.ToString() + ".zip";
            string filePath = System.IO.Path.Combine(PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.ProjectPath, ConfigFileInfo.RelativePath);
            filePath = System.IO.Path.Combine(filePath, filename);

            //方案1  直接下载覆盖 不判断条件
            if(ConfigFileInfo.Content != null)
            {
                byte[] data = ConfigFileInfo.Content.ToArray();
                SaveZipFileAndUnzipThenDelZipfile(filePath, data);
            }
            

            //方案2
            //1. 查看本地是否有此文件FID存在，
            //1.1 不存在直接下载，
            //1.2 存在 比较版本号，版本号不一样直接下载 ，版本号一样，比较修改时间，修改时间新于本机下载，修改时间相等，不下载

            /*
            Dictionary<System.Guid, s_CfgFileInfo> DownloadedFilesDictionary =
                PMSDBStructure.PMSCenterDataContext.GetTable<s_CfgFileInfo>().ToDictionary(s_CfgFileInfo => s_CfgFileInfo.FID);
            
            if(DownloadedFilesDictionary.ContainsKey(fileGuid))
            {
                // 本地存在
                s_CfgFileInfo ExistedConfigFileInfo = null;
                if(DownloadedFilesDictionary.TryGetValue(fileGuid,out ExistedConfigFileInfo))
                {
                    if(ExistedConfigFileInfo.Version != ConfigFileInfo.Version)
                    {
                        // 版本号不一样直接下载
                        byte[] data = ConfigFileInfo.Content.ToArray();
                        //PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.SaveFile(filePath, data);
                        SaveZipFileAndUnzipThenDelZipfile(filePath, data);
                    }
                    else
                    {
                        // 版本号一样
                        if(ExistedConfigFileInfo.TimeStamp < ConfigFileInfo.TimeStamp)
                        {
                            // 修改时间新于本机文件，下载替换，否则不予处理
                            byte[] data = ConfigFileInfo.Content.ToArray();
                            //PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.SaveFile(filePath, data);
                            SaveZipFileAndUnzipThenDelZipfile(filePath, data);
                        }
                    }
                }
            }
            else
            {
                // 本地不存在
                byte[] data = ConfigFileInfo.Content.ToArray();
                //PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.SaveFile(filePath, data);
                SaveZipFileAndUnzipThenDelZipfile(filePath, data);
            }
             * */

            //System.Data.Linq.Binary binary = ConfigFileInfo.Content;
            //var DownloadFiles = 
            //    from p in _PMSCenterDataContext.PMSSys_ConfigFileInfos

            //System.Data.Linq.Table<s_CfgFileInfo> PMSSys_ConfigFileInfos = _PMSCenterDataContext.GetTable<s_CfgFileInfo>();

            //PMSSys_ConfigFileInfos.InsertOnSubmit(ConfigFileInfo);
            //PMSSys_ConfigFileInfos.InsertOnSubmit(ConfigFileInfo);
            //_PMSCenterDataContext.SubmitChanges();
            return true;
        }

        public static bool DownloadFile(s_CfgFTplInfo ConfigFileTemplateInfo)
        {
            System.Guid fileGuid = ConfigFileTemplateInfo.TID;
            string filename = fileGuid.ToString() + ".zip";
            string filePath = System.IO.Path.Combine(PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.ProjectPath, ConfigFileTemplateInfo.RelativePath);
            filePath = System.IO.Path.Combine(filePath, filename);

            //方案1  直接下载覆盖 不判断条件
            if (ConfigFileTemplateInfo.Content != null)
            {
                byte[] data = ConfigFileTemplateInfo.Content.ToArray();
                SaveZipFileAndUnzipThenDelZipfile(filePath, data);
            }
            return true;
        }

        public static bool DownloadFiles(Dictionary<System.Guid, s_CfgFileInfo> FilesToDownloadDictionary)
        {
            // 通过UserInfo调用权限管理模块获得查询s_CfgFileInfo结果var
            foreach (System.Guid key in FilesToDownloadDictionary.Keys)
            {
                s_CfgFileInfo FileToDownloadInfo = null;
                if (FilesToDownloadDictionary.TryGetValue(key, out FileToDownloadInfo))
                {
                    DownloadFile(FileToDownloadInfo);
                }
            }
            return true;
        }

        public static bool SaveZipFileAndUnzipThenDelZipfile(string filePath, byte[] data)
        {
            try
            {
                if(PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.SaveFile(filePath, data))
                {
                    if(System.IO.File.Exists(filePath))
                    {
                        string Ext = System.IO.Path.GetExtension(filePath);
                        if(Ext.Equals(".zip",StringComparison.OrdinalIgnoreCase))
                        {
                            string err = string.Empty;
                            if(PMS.Libraries.ToolControls.PMSPublicInfo.ZipClass.UnZipFile(filePath,string.Empty,out err))
                            {
                                System.IO.File.Delete(filePath);
                                return true;
                            }
                        }
                    }
                }
            }
            catch (System.IO.IOException e)
            {
                return false;
            }
            return false;
        }

        public static bool DownloadCsfFile(s_CfgFileInfo ConfigFileInfo)
        {
            try
            {
                System.Guid fileGuid = ConfigFileInfo.FID;
                string filename = fileGuid.ToString() + ".csf";
                string filePath = System.IO.Path.Combine(PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.UserCustomPath, filename); 

                if (ConfigFileInfo.Content != null)
                {
                    byte[] data = ConfigFileInfo.Content.ToArray();
                    if (PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.SaveFile(filePath, data))
                    {
                        return true;
                    }
                }
            }
            catch (System.IO.IOException)
            {
                return false;
            }
            return false;
        }

        public static bool SaveCsfFile(s_CfgFileInfo ConfigFileInfo)
        {
            try
            {
                System.Guid fileGuid = ConfigFileInfo.FID;
                string filename = fileGuid.ToString() + ".csf";
                string filePath = System.IO.Path.Combine(PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.UserCustomPath, filename);

                string[] fileNameArray = Directory.GetFiles(PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.UserCustomPath, fileGuid.ToString() + ".*", SearchOption.TopDirectoryOnly);
                if (fileNameArray.Length == 0)
                    return false;
                List<string> fileNameList = new List<string>();
                foreach (string strfileName in fileNameArray)
                {
                    if (Path.GetExtension(strfileName).ToLower() == ".xml"
                        || Path.GetExtension(strfileName).ToLower() == ".sco"
                        || Path.GetExtension(strfileName).ToLower() == ".dll")
                        fileNameList.Add(strfileName);
                }
               
                string err = string.Empty;
                string strCsfPath = Path.Combine(PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.UserCustomPath, fileGuid.ToString() + ".csf");
                if (!PMS.Libraries.ToolControls.PMSPublicInfo.ZipClass.ZipFile(fileNameList, strCsfPath, out err))
                {
                    PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, err, true);
                    return false;
                }
            }
            catch (System.IO.IOException ex)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(ex.Message);
                return false;
            }
            return true;
        }

        public static bool SaveCsfFileFromTempPath(string tempPath, bool delTempPath = false, string destCsfFileName = "")
        {
            try
            {
                string guidName = Path.GetFileNameWithoutExtension(tempPath);

                System.Guid fileGuid = Guid.Empty;
                if(Guid.TryParse(guidName, out fileGuid))
                {
                    string filename = fileGuid.ToString() + ".csf";
                    if(string.IsNullOrEmpty(destCsfFileName))
                        destCsfFileName = System.IO.Path.Combine(PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.UserCustomPath, filename);

                    string[] fileNameArray = Directory.GetFiles(tempPath, fileGuid.ToString() + ".*", SearchOption.TopDirectoryOnly);
                    if (fileNameArray.Length == 0)
                        return false;
                    List<string> fileNameList = new List<string>();
                    foreach (string strfileName in fileNameArray)
                    {
                        if (Path.GetExtension(strfileName).ToLower() == ".xml"
                            || Path.GetExtension(strfileName).ToLower() == ".sco"
                            || Path.GetExtension(strfileName).ToLower() == ".dll")
                            fileNameList.Add(strfileName);
                    }

                    string err = string.Empty;
                    //string strCsfPath = Path.Combine(PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.UserCustomPath, fileGuid.ToString() + ".csf");
                    if (!PMS.Libraries.ToolControls.PMSPublicInfo.ZipClass.ZipFile(fileNameList, destCsfFileName, out err))
                    {
                        PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, err, true);
                        return false;
                    }

                    if (delTempPath)
                    {
                        if (Directory.Exists(tempPath))
                        {
                            Directory.Delete(tempPath, true);
                        }
                    }
                }
                
            }
            catch (System.IO.IOException ex)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(ex.Message);
                return false;
            }
            return true;
        }

        public static bool SaveRptFile(s_CfgFileInfo ConfigFileInfo)
        {
            try
            {
                string filePath = ConfigFileInfo.Description;

                if (ConfigFileInfo.Content != null)
                {
                    byte[] data = ConfigFileInfo.Content.ToArray();
                    if (PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.SaveFile(filePath, data))
                    {
                        return true;
                    }
                }
            }
            catch (System.IO.IOException)
            {
                return false;
            }
            return false;
        }

        public static bool SaveDrptFile(string drptFilePath, string strFilePath, string strFileName)
        {
            try
            {
                return PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.ZipFiles(strFilePath, drptFilePath);
            }
            catch (System.IO.IOException)
            {
                return false;
            }
        }

        public static bool SaveDrptFile(string drptFilePath,  MESDrptFileObj drptobj)
        {
            try
            {
                return PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.SaveFile(drptFilePath, drptobj);
            }
            catch (System.IO.IOException)
            {
                return false;
            }
        }

        public static bool LoadDrptFile(string drptFilePath, ref MESDrptFileObj fobj)
        {
            try
            {
                object obj = fobj;
                if (PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.LoadFile(drptFilePath, ref obj))
                {
                    fobj = obj as MESDrptFileObj;
                    return true;
                }
                return false;
            }
            catch (System.IO.IOException)
            {
                return false;
            }
        }

        public static bool SaveFormFile(string frmFilePath, MESFormFileObj fobj)
        {
            try
            {
                return PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.SaveFile(frmFilePath, fobj);
            }
            catch (System.IO.IOException)
            {
                return false;
            }
        }

        public static bool LoadFormFile(string frmFilePath, ref MESFormFileObj fobj)
        {
            try
            {
                object obj = fobj;
                if(PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.LoadFile(frmFilePath,ref obj))
                {
                    fobj = obj as MESFormFileObj;
                    return true;
                }
                return false;
            }
            catch (System.IO.IOException)
            {
                return false;
            }
        }

        public static bool LoadReportFile(string rptFilePath, ref MESReportFileObj robj)
        {
            try
            {
                object obj = robj;
                if (PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.LoadFile(rptFilePath, ref obj, new UBinder()))
                {
                    robj = obj as MESReportFileObj;
                    return true;
                }
                return false;
            }
            catch (System.IO.IOException)
            {
                return false;
            }
        }

        public static bool SaveReportFile(string rptFilePath, MESReportFileObj robj)
        {
            try
            {
                return PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.SaveFile(rptFilePath, robj);
            }
            catch (System.IO.IOException)
            {
                return false;
            }
        }

        public static bool SaveNavigatorFile(string navFilePath, MESNavigatorFileObj nobj)
        {
            try
            {
                return PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.SaveFile(navFilePath, nobj);
            }
            catch (System.IO.IOException)
            {
                return false;
            }
        }

        public static bool LoadNavigatorFile(string navFilePath, ref MESNavigatorFileObj nobj)
        {
            try
            {
                object obj = nobj;
                if (PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.LoadFile(navFilePath, ref obj))
                {
                    nobj = obj as MESNavigatorFileObj;
                    return true;
                }
                return false;
            }
            catch (System.IO.IOException)
            {
                return false;
            }
        }

        public static bool UnzipFile(string zipfilePath,string unZipDir)
        {
            string Ext = System.IO.Path.GetExtension(zipfilePath);
            if (Ext.Equals(".zip", StringComparison.OrdinalIgnoreCase))
            {
                string err = string.Empty;
                if (PMS.Libraries.ToolControls.PMSPublicInfo.ZipClass.UnZipFile(zipfilePath, unZipDir, out err))
                {
                    //System.IO.File.Delete(zipfilePath);
                    return true;
                }
            }
            return false;
        }

        public static bool UnzipRptFile(string rptfilePath, string unZipDir, out List<string> UnzipFiles)
        {
            string Ext = System.IO.Path.GetExtension(rptfilePath);
            List<string> filenames = null; ;
            if (Ext.Equals(".rpt", StringComparison.OrdinalIgnoreCase))
            {
                string err = string.Empty;
                if (PMS.Libraries.ToolControls.PMSPublicInfo.ZipClass.UnZipFile(rptfilePath, unZipDir, out filenames, out err))
                {
                    UnzipFiles = filenames;
                    //System.IO.File.Delete(zipfilePath);
                    return true;
                }
            }
            UnzipFiles = filenames;
            return false;
        }

        public static bool UnzipDrptFile(string drptfilePath, string unZipDir, out List<string> UnzipFiles)
        {
            string Ext = System.IO.Path.GetExtension(drptfilePath);
            List<string> filenames = null; ;
            if (Ext.Equals(".drpt", StringComparison.OrdinalIgnoreCase))
            {
                string err = string.Empty;
                if (PMS.Libraries.ToolControls.PMSPublicInfo.ZipClass.UnZipFile(drptfilePath, unZipDir, out filenames, out err))
                {
                    UnzipFiles = filenames;
                    //System.IO.File.Delete(zipfilePath);
                    return true;
                }
            }
            UnzipFiles = filenames;
            return false;
        }

        public static bool SaveZipFileAndRenameUnzipThenDelZipfile(string filePath, byte[] data, Guid oldGuid, Guid newGuid)
        {
            try
            {
                if (PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.SaveFile(filePath, data))
                {
                    if (System.IO.File.Exists(filePath))
                    {
                        string Ext = System.IO.Path.GetExtension(filePath);
                        if (Ext.Equals(".zip", StringComparison.OrdinalIgnoreCase))
                        {
                            string strDirectoryName = newGuid.ToString();
                            string tempPath = System.IO.Path.Combine(PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.UserCustomPath, strDirectoryName);
                            // 临时文件夹不存在新建
                            if (!System.IO.Directory.Exists(tempPath))
                                System.IO.Directory.CreateDirectory(tempPath);
                            string err = string.Empty;
                            if (PMS.Libraries.ToolControls.PMSPublicInfo.ZipClass.UnZipFile(filePath, tempPath, out err))
                            {
                                if(PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.CopyFiles(oldGuid, newGuid, tempPath, PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.UserCustomPath))
                                {
                                    if (System.IO.Directory.Exists(tempPath))
                                        System.IO.Directory.Delete(tempPath, true);
                                }
                                System.IO.File.Delete(filePath);
                                return true;
                            }
                        }
                    }
                }
            }
            catch (System.IO.IOException e)
            {
                return false;
            }
            return false;
        }

        public static bool UploadSysTable(s_TableInfo TableInfo)
        {
            try
            {
                PMSDBStructure.PMSCenterDataContext.AddTos_TableInfo(TableInfo);
                PMSDBStructure.PMSCenterDataContext.SaveChanges();
            }
            catch (System.Exception e)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, e.Message, true);
                return false;
            }
            return true;
        }

        public static bool UploadTableProp(s_TableProperty TableProperty)
        {
            try
            {
                PMSDBStructure.PMSCenterDataContext.AddTos_TableProperty(TableProperty);
                PMSDBStructure.PMSCenterDataContext.SaveChanges();
            }
            catch (System.Exception e)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, e.Message, true);
                return false;
            }
            return true;
        }

        public static bool UploadTableFieldInfo(s_TableFieldInfo TableFieldInfo)
        {
            try
            {
                PMSDBStructure.PMSCenterDataContext.AddTos_TableFieldInfo(TableFieldInfo);
                PMSDBStructure.PMSCenterDataContext.SaveChanges();
            }
            catch (System.Exception e)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, e.Message, true);
                return false;
            }
            return true;
        }

        public static bool UploadTableFieldInfo(PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBFieldPropCollection fpc,Guid tableGuid)
        {
            try
            {
                foreach (PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBFieldProp fp in fpc)
                {
                    s_TableFieldInfo FieldInfo = new s_TableFieldInfo();
                    FieldInfo.FieldGuid = System.Guid.NewGuid();
                    FieldInfo.TableGuid = tableGuid;
                    FieldInfo.FieldID = fp.IFildID;
                    FieldInfo.FieldName = fp.StrFieldName;
                    FieldInfo.FieldType= fp.StrFieldType;
                    FieldInfo.FieldLength = fp.IFieldLength.ToString();
                    FieldInfo.FieldPrimaryKey = fp.BFieldPrimaryKey;
                    FieldInfo.FieldIdentity = fp.BFieldIdentity;
                    FieldInfo.FieldNullAble = fp.BFieldNullable;
                    FieldInfo.FieldIsSystem = fp.BFieldIsSystem;
                    FieldInfo.FieldDefault = fp.StrFieldDefault;
                    FieldInfo.FieldDescription = fp.StrFieldDescription;
                    if (fp.ExProps != null)
                    {
                        foreach (KeyValuePair<string, PMSDBExtendedProp> entry in fp.ExProps)
                        {
                            s_TableFieldProperty FieldProperty = new s_TableFieldProperty();
                            FieldProperty.PropertyGuid = System.Guid.NewGuid();
                            FieldProperty.FieldGuid = FieldInfo.FieldGuid;
                            FieldProperty.PropertyName = entry.Value.StrPropName;
                            FieldProperty.PropertyValue = entry.Value.StrPropValue;
                            PMSDBStructure.PMSCenterDataContext.AddTos_TableFieldProperty(FieldProperty);
                        }
                    }

                    PMSDBStructure.PMSCenterDataContext.AddTos_TableFieldInfo(FieldInfo);
                }

                PMSDBStructure.PMSCenterDataContext.SaveChanges();
            }
            catch (System.Exception e)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, e.Message, true);
                return false;
            }
            return true;
        }

        public static bool DeleteTableInfo(s_TableInfo TableInfo)
        {
            try
            {
                PMSDBStructure.PMSCenterDataContext.DeleteObject(TableInfo);
                PMSDBStructure.PMSCenterDataContext.SaveChanges();
            }
            catch (System.Exception e)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, e.Message, true);
                return false;
            }
            return true;
        }

        public static bool DeleteTableInfo(string tablename)
        {
            try
            {
                var q = (from PMSTableInfo in PMSDBStructure.PMSCenterDataContext.s_TableInfo
                         where PMSTableInfo.TableName == tablename
                         select PMSTableInfo);
                if (q.Count() == 0)
                    return true;
                s_TableInfo info = q.First();
                PMSDBStructure.PMSCenterDataContext.Refresh(System.Data.Entity.Core.Objects.RefreshMode.StoreWins, info);
                if (info != null)
                {
                    PMSDBStructure.PMSCenterDataContext.DeleteObject(info);
                    PMSDBStructure.PMSCenterDataContext.SaveChanges();
                }
            }
            catch (System.Exception e)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, e.Message, true);
                return false;
            }
            return true;
        }

        public static bool DeleteTableFieldInfo(s_TableFieldInfo TableFieldInfo)
        {
            try
            {
                PMSDBStructure.PMSCenterDataContext.DeleteObject(TableFieldInfo);
                PMSDBStructure.PMSCenterDataContext.SaveChanges();
            }
            catch (System.Exception e)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, e.Message, true);
                return false;
            }
            return true;
        }

        public static bool DeleteTableInfo(string tablename,string columnname)
        {
            try
            {
                var q = (from PMSTableFieldInfo in PMSDBStructure.PMSCenterDataContext.s_TableFieldInfo
                         where PMSTableFieldInfo.TableGuid == MESCenterFunctions.fn_GetGuidFromTableName(tablename) &&
                         PMSTableFieldInfo.FieldName == columnname
                         select PMSTableFieldInfo);
                if (q.Count() == 0)
                    return true;
                s_TableFieldInfo info = q.First();
                PMSDBStructure.PMSCenterDataContext.Refresh(System.Data.Entity.Core.Objects.RefreshMode.StoreWins, info);
                if (info != null)
                {
                    PMSDBStructure.PMSCenterDataContext.DeleteObject(info);
                    PMSDBStructure.PMSCenterDataContext.SaveChanges();
                }
            }
            catch (System.Exception e)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, e.Message, true);
                return false;
            }
            return true;
        }

        public static bool UploadMITableRelation(s_MITableRelationInfo rInfo)
        {
            try
            {
                PMSDBStructure.PMSCenterDataContext.AddTos_MITableRelationInfo(rInfo);
                PMSDBStructure.PMSCenterDataContext.SaveChanges();
            }
            catch (System.Exception e)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, e.Message, true);
                return false;
            }
            return true;
        }

        public static bool UploadIITableRelation(s_IITableRelationInfo rInfo)
        {
            try
            {
                PMSDBStructure.PMSCenterDataContext.AddTos_IITableRelationInfo(rInfo);
                PMSDBStructure.PMSCenterDataContext.SaveChanges();
            }
            catch (System.Exception e)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, e.Message, true);
                return false;
            }
            return true;
        }

        public static List<s_AttachmentFile> GetAttachment(string strLinkIdentity)
        {
            try
            {
                var q = from attachment in PMSDBStructure.PMSCenterDataContext.s_AttachmentFile
                         where attachment.LinkIdentity == strLinkIdentity
                         select attachment;
                return q.ToList();
            }
            catch (System.Exception e)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, e.Message, true);
                return null;
            }
        }

        public static List<s_AttachmentFile> GetAttachment(Guid guid)
        {
            try
            {
                var q = from attachment in PMSDBStructure.PMSCenterDataContext.s_AttachmentFile
                        where attachment.AFID == guid
                        select attachment;
                return q.ToList();
            }
            catch (System.Exception e)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, e.Message, true);
                return null;
            }
        }

        public static int DeleteAttachment(string strLinkIdentity)
        {
            try
            {
                return PMSDBConnection.ExecuteCommandNonQuery("DELETE FROM {0}[s_AttachmentFile] WHERE [LinkIdentity] = '" + strLinkIdentity + "'");
            }
            catch (System.Exception e)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, e.Message, true);
                return 0;
            }
        }

        public static int DeleteAttachment(Guid guid)
        {
            try
            {
                return PMSDBConnection.ExecuteCommandNonQuery("DELETE FROM {0}[s_AttachmentFile] WHERE [AFID] = '" + guid.ToString() + "'");
            }
            catch (System.Exception e)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, e.Message, true);
                return 0;
            }
        }

        public static bool UploadAttachment(List<s_AttachmentFile> afs, string strLinkIdentity)
        {
            try
            {
                foreach (s_AttachmentFile af in afs)
                {
                    PMSDBStructure.PMSCenterDataContext.AddTos_AttachmentFile(af);
                }
                PMSDBStructure.PMSCenterDataContext.SaveChanges();
                return true;
            }
            catch (System.Exception e)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, e.Message, true);
                return false;
            }
        }


        public static bool AcceptAllChanges()
        {
            try
            {
                PMSDBStructure.PMSCenterDataContext.AcceptAllChanges();
            }
            catch (System.Exception e)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, e.Message, true);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 根据服务端配置文件的路径查找已发布的后台文件ID号
        /// </summary>
        /// <param name="serverRelativePath">
        /// 服务端文件相对路径,
        /// eg:Folder1\Folder2\报表1.rpt
        ///    Folder1\Folder2\表单1.frm
        /// </param>
        /// <returns>ID号</returns>
        public static string GetFileIDByPath(string serverRelativePath)
        {
            try
            {
                if(string.IsNullOrEmpty(serverRelativePath))
                    return null;
                string strPath = serverRelativePath.Trim();
                string strDirectory = Path.GetDirectoryName(strPath);
                string fileName = Path.GetFileName(serverRelativePath);
                string[] strArr = strDirectory.Split(new char[] { '\\' });
                Guid? ParentID = null;
                s_CfgFInfoMap currentFileInfoMap = null;
                int count = strArr.Count();
                for (int i = 0; i < count; i++)
                {
                    string sep = strArr[i];
                    if (null == ParentID)
                    {
                        var q = from cfgMap in PMSDBStructure.PMSCenterDataContext.s_CfgFInfoMap
                                where Guid.Equals(cfgMap.ParentID, ParentID) && cfgMap.Name == sep
                                select cfgMap;
                        if (q.Count() > 0)
                        {
                            currentFileInfoMap = q.First();
                            ParentID = currentFileInfoMap.MAPID;
                        }
                        else
                            return null;

                    }
                    else
                    {
                        var q = from cfgMap in PMSDBStructure.PMSCenterDataContext.s_CfgFInfoMap
                                where cfgMap.ParentID == ParentID && cfgMap.Name == sep
                                select cfgMap;
                        if (q.Count() > 0)
                        {
                            currentFileInfoMap = q.First();
                            ParentID = currentFileInfoMap.MAPID;
                        }
                        else
                            return null;
                    }
                    
                    //PMSDBStructure.PMSCenterDataContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, q);
                    
                }

                var query = from cfgInfoMap_r in PMSDBStructure.PMSCenterDataContext.s_CfgFInfo_s_CfgFInfoMap_r
                        join cfg in PMSDBStructure.PMSCenterDataContext.s_CfgFileInfo on cfgInfoMap_r.FID equals cfg.FID
                        where cfgInfoMap_r.MAPID == currentFileInfoMap.MAPID && cfg.Name == fileName && cfg.Current_ == true
                        select cfg.FID;
                if (query.Count() > 0)
                    return query.First().ToString();
            }
            catch (System.Exception e)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, e.Message, true);
            }
            return null;
        }
    }
}
