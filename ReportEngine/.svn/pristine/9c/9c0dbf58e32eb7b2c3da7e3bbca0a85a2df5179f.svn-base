#define EventVertion
#define ReportNewEngine

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Drawing.Printing;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using PMS.Libraries.ToolControls.PMSPublicInfo;

namespace PMS.Libraries.ToolControls
{

    /// <summary>
    /// COM Interface - enables to run c# code from c++
    /// 接口添加直接往后加，中间加会影响NetSCADA插件。
    /// </summary> 
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    [Guid("b9288468-6d36-4a82-be4b-8b6b54f0f997")]
    public interface IMESReportViewerInterface
    {
        void OpenReport(String strFileName);
        void SetReport(String strFileName);
        void SetProjectPath(String strProjectPath);
        void QueryReport();
        void RefreshReport();
        void Print();
        void PrintEx(String filename);
        void PrintPreview();
        void Export();
        void ExportEx(String strFileName);
        int SetParameter(String strParaName, String strParaValue);
        int SetDSParameter(String strDSName, String strParaName, String strParaValue);
        void ReleaseReport();
        object GetParameter(String strParaName);
#if EventVertion
        void SetParentWndHandle(IntPtr hdWnd);
#endif
        int CallFunction(String strParaName, String strParaValue);
        int SetDataSetParameter(String strDatasetPath, String strDSName);
        int SetMultiDataSetParameter(String strDatasetPath, String strDSName, String strSQL);
    }

    /// <summary>
    /// MESReportViewer displays rpt files or syntax. 
    /// </summary>
    [ProgId("MESReportUnit.ReportViewer")]
    [Guid("e8e1e845-b989-4fd6-af58-ee928421335e")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public partial class MESReportViewer : UserControl, IMESReportViewerInterface
    {
        public delegate void RptQueryEndEventHandler(object sender, EventArgs e);
        public event RptQueryEndEventHandler RptQueryEnd = null;

        public delegate void RptExportEndEventHandler(object sender, EventArgs e);
        public event RptExportEndEventHandler RptExportEnd = null;

        public delegate void RptPrintEndEventHandler(object sender, EventArgs e);
        public event RptPrintEndEventHandler RptPrintEnd = null;

        MESReportServer.MESReportService service = new MESReportServer.MESReportService();

        string _prjPath = null;

        private CNSDogVarExpert _dogExpert = new CNSDogVarExpert();

        private const string _AuthorizedID = "MESReport-4fffC6F66-3C0D-4c0d-abab-B8F16199e95f";

        private bool _bAuthorized = false;

        private PMS.Libraries.ToolControls.MESReportRun reportRun = null;

        public MESReportViewer()
        {
            InitializeComponent();

            // 设置全局运行模式-报表查看器模式
            PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.IsReportViewerMode = true;
            //SetProjectPath(AppDomain.CurrentDomain.BaseDirectory);
            //CheckForIllegalCrossThreadCalls  = true;
            this.MouseWheel += new MouseEventHandler(MESReportViewer_MouseWheel);
            Init();
        }

        private bool Init()
        {
            try
            {
                string ModulePath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                string iniPath = Path.Combine(ModulePath, PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.SetupIniName);
                // 读取安装包Setup版本号
                PMS.Libraries.ToolControls.PMSPublicInfo.INIClass ini = new PMS.Libraries.ToolControls.PMSPublicInfo.INIClass(iniPath);
                if (ini.ExistINIFile())
                {
                    PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentSetupVersion = ini.IniReadValue("SoftInfo", "Version");
                    string strIsPlugin = ini.IniReadValue("SoftInfo", "IsPlugin");
                    PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.IsPlugin = strIsPlugin == "1";
                    string strInstallMode = ini.IniReadValue("SoftInfo", "InstallMode");
                    int iInstallMode = int.Parse(strInstallMode);
                    PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentInstallMode = (PMS.Libraries.ToolControls.PMSPublicInfo.InstallMode)iInstallMode;
                    return true;
                }
#if ReportNewEngine
#else
            this.pmsReportViewer1.SetToolBar(0, false);
#endif
                ToolBarVisible = false;
                return true;
            }
            catch (Exception ex)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.IsPlugin = true;
                PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentInstallMode = PMSPublicInfo.InstallMode.MESReportViewer_NSPlugin;
#if ReportNewEngine
#else
            this.pmsReportViewer1.SetToolBar(0, false);
#endif
                ToolBarVisible = false;
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(ex.Message);
            }
            return false;
        }

        void MESReportViewer_MouseWheel(object sender, MouseEventArgs e)
        {
#if ReportNewEngine
#else
            this.pmsReportViewer1.ReportViewerMouseWheel(sender, e);
#endif
        }

        private List<PMS.Libraries.ToolControls.PMSPublicInfo.PMSRefDBConnectionObj> RefDBConnectionObjList = null;

        #region ActiveX interface
        /// <summary>
        /// 打开报表 后缀名为 .rpt(暂停使用2011.10.25)
        /// </summary>
        /// <param name="strFileName">报表文件名</param>
        public void OpenReport(String strFileName)
        {
            QueryReport(strFileName);
        }

        private string _strFileName = null;
        public string RptFileName
        {
            get { return _strFileName; }
            set { _strFileName = value; }
        }

        public bool ToolBarVisible
        {
            get { return this.pmsReportViewer1.ToolBarVisible; }
            set { this.pmsReportViewer1.ToolBarVisible = value; }
        }

        /// <summary>
        /// 设置报表所需属性，不查询
        /// </summary>
        /// <param name="strFileName"></param>
        public void SetReport(String strFileName)
        {
            _strFileName = strFileName;
            string strExt = Path.GetExtension(strFileName);
            if (string.Compare(strExt, ".rpt", true) == 0)
            {
#if ReportNewEngine
                this.pmsReportViewer1.Dock = DockStyle.None;
                this.pmsReportViewer1.Size = new Size(0, 0);
                this.pmsReportViewer1.Visible = false;
                this.mesObjectReportViewer1.Dock = DockStyle.None;
                this.mesObjectReportViewer1.Size = new Size(0, 0);
                this.mesObjectReportViewer1.Visible = false;
                this.reportViewer1.Dock = DockStyle.Fill;
                this.reportViewer1.Visible = true;
                this.reportViewer1.InitialReport();
                InitConnections();
                service.SetRptReport(strFileName, this.reportViewer1);
#else
                this.pmsReportViewer1.Dock = DockStyle.Fill;
                this.mesObjectReportViewer1.Dock = DockStyle.None;
                this.mesObjectReportViewer1.Size = new Size(0, 0);
                this.pmsReportViewer1.Visible = true;
                this.mesObjectReportViewer1.Visible = false;
                service.SetRptReport(strFileName, this.pmsReportViewer1);
#endif
            }
            else if (string.Compare(strExt, ".drpt", true) == 0)
            {
                this.reportViewer1.Dock = DockStyle.None;
                this.reportViewer1.Size = new Size(0, 0);
                this.reportViewer1.Visible = false;

                this.pmsReportViewer1.Dock = DockStyle.Fill;
                this.mesObjectReportViewer1.Dock = DockStyle.None;
                this.mesObjectReportViewer1.Size = new Size(0, 0);
                this.pmsReportViewer1.Visible = true;
                this.mesObjectReportViewer1.Visible = false;
                service.SetDrptReport(strFileName, this.pmsReportViewer1);
                this.pmsReportViewer1.Refresh();
            }
            else if (string.Compare(strExt, ".orpt", true) == 0)
            {
                this.reportViewer1.Dock = DockStyle.None;
                this.reportViewer1.Size = new Size(0, 0);
                this.reportViewer1.Visible = false;

                this.mesObjectReportViewer1.Dock = DockStyle.Fill;
                this.pmsReportViewer1.Dock = DockStyle.None;
                this.pmsReportViewer1.Size = new Size(0, 0);
                this.pmsReportViewer1.Visible = false;
                this.mesObjectReportViewer1.Visible = true;
                service.SetOrptReport(strFileName, this.mesObjectReportViewer1);
                this.mesObjectReportViewer1.Refresh();
            }
        }

        /// <summary>
        /// 查询报表
        /// </summary>
        public void QueryReport()
        {
           // if(!IsAuthorized())
             //   return;
#if ReportNewEngine
            service.QueryReport(this.reportViewer1);
#else
            service.QueryReport(this.pmsReportViewer1);
#endif
        }

        /// <summary>
        /// 刷新报表
        /// </summary> 
        public void RefreshReport()
        {
            if (!IsAuthorized())
                return;
#if ReportNewEngine
            this.reportViewer1.QueryReport();
#else
            this.pmsReportViewer1.QueryReport();
#endif
        }

        public void SetProjectPath(String strPrjPath)
        {
            if (strPrjPath != _prjPath)
            {
                _prjPath = strPrjPath;
                PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.ProjectPath = strPrjPath;
                PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.CreateReportPrjDirectory();
                InitConnections();
            }
        }

        private void InitConnections()
        {
            RefDBConnectionObjList = PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetRefDBConnectionObjListFromLocalFile();
#if ReportNewEngine
            this.reportViewer1.Connections = RefDBConnectionObjList;
#else
            this.pmsReportViewer1.Connections = RefDBConnectionObjList;
#endif
        }

        bool bprint = false;
        /// <summary>
        /// 选择打印机，并打印报表
        /// </summary> 
        public void Print()
        {
            if (bprint == true)			// already printing
            {
                MessageBox.Show("Can only print one file at a time.");
                return;
            }

            bprint = true;
#if ReportNewEngine
            this.reportViewer1.Print();
#else
            this.pmsReportViewer1.Print();
#endif

            bprint = false;
        }
        /// <summary>
        /// 根据默认打印机配置直接打印报表
        /// </summary> 
        public void PrintEx(string filename)
        {
            if (bprint == true)			// already printing
            {
                MessageBox.Show("Can only print one file at a time.");
                return;
            }

            bprint = true;
#if ReportNewEngine
            this.reportViewer1.PrintEx(filename);
#else
            this.pmsReportViewer1.PrintEx(filename);
#endif

            bprint = false;
        }
        /// <summary>
        /// 打印预览报表
        /// </summary> 
        public void PrintPreview()
        {
#if ReportNewEngine
            this.reportViewer1.PrintPreview();
#else
            this.pmsReportViewer1.PrintPreview();
#endif
        }
        /// <summary>
        /// 导出报表，并选择保存文件
        /// </summary>
        /// <param name="iType">导出类型</param>
        public void Export()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "报表导出";
            dlg.Filter = "NetSCADA MES报表文件(*.RPTX)|*.RPTX|NetSCADA MES报表文件(*.ORPT)|*.ORPT|NetSCADA MES报表文件(*.DRPT)|*.DRPT|位图(*.BMP)|*.BMP|增强图元(*.EMF)|*.EMF|可交换图像(*.EXIF)|*.EXIF|图形交换(*.GIF)|*.GIF|联合图像(*.JPEG)|*.JPEG|可移植网络图形(*.PNG)|*.PNG";
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".JPEG"; // Default file extension

            // Show save file dialog box
            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                ExportEx(dlg.FileName);
            }
        }

        public void ExportEx(String strFileName)
        {
            string strExt = Path.GetExtension(strFileName);
            System.Drawing.Imaging.ImageFormat imageformat = System.Drawing.Imaging.ImageFormat.Jpeg;
            switch (strExt.ToLower())
            {
                case "bmp":
                    imageformat = System.Drawing.Imaging.ImageFormat.Bmp;
                    this.pmsReportViewer1.SavePictrue(strFileName, imageformat);
                    break;
                case "emf":
                    imageformat = System.Drawing.Imaging.ImageFormat.Emf;
                    this.pmsReportViewer1.SavePictrue(strFileName, imageformat);
                    break;
                case "exif":
                    imageformat = System.Drawing.Imaging.ImageFormat.Exif;
                    this.pmsReportViewer1.SavePictrue(strFileName, imageformat);
                    break;
                case "gif":
                    imageformat = System.Drawing.Imaging.ImageFormat.Gif;
                    this.pmsReportViewer1.SavePictrue(strFileName, imageformat);
                    break;
                case "jpeg":
                    imageformat = System.Drawing.Imaging.ImageFormat.Jpeg;
                    this.pmsReportViewer1.SavePictrue(strFileName, imageformat);
                    break;
                case "png":
                    imageformat = System.Drawing.Imaging.ImageFormat.Png;
                    this.pmsReportViewer1.SavePictrue(strFileName, imageformat);
                    break;
                //保存为自定义报表文件格式（包含数据集）
                case ".drpt":
                    ExportWait form = new ExportWait(strFileName, RptFileName, this.pmsReportViewer1);
                    if(form.ShowDialog() == DialogResult.OK)
                    {
                        pmsReportViewer1_ExportFinish(this.pmsReportViewer1, new System.EventArgs());
                    }
                    //if(service.SaveAsDrpt(strFileName, RptFileName, this.pmsReportViewer1))
                    //    pmsReportViewer1_ExportFinish(this.pmsReportViewer1, new System.EventArgs());
                    //service.SaveAsDrpt(strFileName, this.pmsReportViewer1);
                    break;
                //保存为自定义报表文件格式（不包含数据集)
                case ".orpt":
                    service.SaveAsOrpt(strFileName, this.pmsReportViewer1);
                    break;
                //保存为自定义静态报表文件格式（不包含数据集)
                case ".rptx":
                    service.SaveAsRptx(strFileName, this.reportViewer1);
                    break;
                default:
                    imageformat = System.Drawing.Imaging.ImageFormat.Jpeg;
                    this.pmsReportViewer1.SavePictrue(strFileName, imageformat);
                    break;
            }
        }

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="strParaName">参数名</param>
        /// <param name="strParaValue">参数值</param>
        public int SetParameter(String strParaName, String strParaValue)
        {
            if (strParaName == _AuthorizedID && strParaValue == _AuthorizedID)
            {
                _bAuthorized = true;
                return 1;
            }
#if ReportNewEngine
            return this.reportViewer1.SetParameter(strParaName, strParaValue);
#else
            return this.pmsReportViewer1.SetParameter(strParaName, strParaValue);
#endif
        }

        /// <summary>
        /// 调用功能参数
        /// </summary>
        /// <param name="strParaName">参数名</param>
        /// <param name="strParaValue">参数值</param>
        public int CallFunction(String strParaName, String strParaValue)
        {
            if (string.IsNullOrEmpty(_strFileName))
                return -1;
#if ReportNewEngine
            this.reportViewer1.CallFunction(strParaName, strParaValue);
#else
            try
            {
                switch (strParaName.ToLower())
                {
                    case "zoom":
                        this.pmsReportViewer1.Zoom(strParaValue);
                        break;
                    case "zoomin":
                        this.pmsReportViewer1.ZoomIn();
                        break;
                    case "zoomout":
                        this.pmsReportViewer1.ZoomOut();
                        break;
                    case "fullpagedisplay":
                        this.pmsReportViewer1.FullPageDisplay();
                        break;
                    case "refresh":
                        this.pmsReportViewer1.Refresh();
                        break;
                    case "print":
                        this.pmsReportViewer1.Print();
                        break;
                    case "printpreview":
                        this.pmsReportViewer1.PrintPreview();
                        break;
                    case "export":
                        if (string.IsNullOrEmpty(strParaValue))
                            Export();
                        else
                            ExportEx(strParaValue);
                        break;
                    default:
                        break;
                }
            }
            catch (System.Exception ex)
            {
                return -1;
            }
#endif
            return 0;
        }

        /// <summary>
        /// 获取报表变量值
        /// </summary>
        /// <param name="strParaName">变量名</param>
        /// 0-string
        /// 1-int
        /// 2-datetime
        /// 3-double
        /// 4-bool
        /// <returns>
        /// 返回的变量值
        /// </returns>
        public object GetParameter(String strParaName)
        {
#if ReportNewEngine
            return this.reportViewer1.GetParameter(strParaName);
#else
            return this.pmsReportViewer1.GetParameter(strParaName);
#endif
            //strValue = null;
            //iType = -1;
            //object ob = this.pmsReportViewer1.GetPMSVarValue(strParaName);
            //if (null == ob)
            //    return -1;
            //Type tp = ob.GetType();
            //if (tp.Equals(typeof(string)))
            //{
            //    iType = 0;
            //    strValue = Convert.ToString(ob);
            //}
            //else if(tp.Equals(typeof(int)))
            //{
            //    iType = 1;
            //    strValue = Convert.ToString(ob);
            //}
            //else if (tp.Equals(typeof(DateTime)))
            //{
            //    iType = 2;
            //    strValue = Convert.ToString(ob);
            //}
            //else if (tp.Equals(typeof(double)))
            //{
            //    iType = 3;
            //    strValue = Convert.ToString(ob);
            //}
            //else if (tp.Equals(typeof(bool)))
            //{
            //    iType = 4;
            //    strValue = Convert.ToString(ob);
            //}
            //return 1;
        }


        /// <summary>
        /// 获取报表变量列表
        /// </summary>
        /// <returns></returns>
        public List<PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.PMSVar> GetVariables()
        {
#if ReportNewEngine
            return null;
#else
            return this.pmsReportViewer1.GetParameters();
#endif
        }

        /// <summary>
        /// 设置数据源参数
        /// </summary>
        /// <param name="strDSName">数据源名</param>
        /// <param name="strParaName">
        /// 参数名：
        /// RefDBType 引用数据库类型 0-MSAccess,1-MSSqlServer,2-Oracle,3-OleDB
        /// StrServerName 服务器名
        /// StrDBName 数据库名
        /// StrUserID 用户名
        /// StrPassWord 密码
        /// StrPort 端口号
        /// StrDBPath 数据库路径
        /// EConnectType 连接协议 0-namepipe，1-tcpip
        /// ConnectString 连接字符串
        /// </param>
        /// <param name="strParaValue">参数值</param>
        /// <returns></returns>
        public int SetDSParameter(String strDSName, String strParaName, String strParaValue)
        {
            if (RefDBConnectionObjList == null)
                return -1;
            if(!RefDBConnectionObjList.Exists(o => o.StrName == strDSName))
            {
                // 数据源不存在则新增
                PMSRefDBConnectionObj refConnect = new PMSRefDBConnectionObj();
                refConnect.StrName = strDSName;
                refConnect.RefDBConnection = new PMSRefDBConnection();
                RefDBConnectionObjList.Add(refConnect);
            }

            foreach (PMSPublicInfo.PMSRefDBConnectionObj ob in RefDBConnectionObjList)
            {
                if (string.Compare(ob.StrName, strDSName, true) == 0)
                {
                    switch (strParaName)
                    {
                        case "RefDBType":
                        case "DBType":
                            ob.RefDBConnection.RefDBType = GetRefDBTypeFromString(strParaValue);
                            break;
                        case "StrServerName":
                        case "ServerName":
                            ob.RefDBConnection.StrServerName = strParaValue;
                            break;
                        case "StrDBName":
                        case "DBName":
                            ob.RefDBConnection.StrDBName = strParaValue;
                            break;
                        case "StrUserID":
                        case "UserID":
                            ob.RefDBConnection.StrUserID = strParaValue;
                            break;
                        case "StrPassWord":
                        case "PassWord":
                            ob.RefDBConnection.StrPassWord = strParaValue;
                            break;
                        case "StrPort":
                        case "Port":
                            ob.RefDBConnection.StrPortID = strParaValue;
                            break;
                        case "StrDBPath":
                        case "DBPath":
                            ob.RefDBConnection.StrDBPath = strParaValue;
                            break;
                        case "EConnectType":
                        case "ConnectType":
                            ob.RefDBConnection.EConnectType = GetConnectTypeFromString(strParaValue);
                            break;
                        case "ConnectString":
                        case "OleDBConnectString":
                            ob.RefDBConnection.ConnectString = strParaValue;
                            break;
                    }
                }
            }
#if ReportNewEngine
            this.reportViewer1.Connections = RefDBConnectionObjList;
#else
            this.pmsReportViewer1.Connections = RefDBConnectionObjList;
#endif
            return 0;
        }

        /// <summary>
        /// 设置多数据源查询
        /// </summary>
        /// <param name="strDatasetPath">配置的数据集路径，以.隔开</param>
        /// <param name="strDSName">一个或多个数据源名，以逗号,隔开</param>
        /// <returns></returns>
        public int SetDataSetParameter(String strDatasetPath, String strDSName)
        {
#if ReportNewEngine
            return this.reportViewer1.SetDataSetParameter(strDatasetPath, strDSName);
#else
#endif
            return 0;
        }
        /// <summary>
        /// 设置多数据源查询
        /// </summary>
        /// <param name="strDatasetPath">配置的数据集路径，以.隔开</param>
        /// <param name="strDSName">数据源名</param>
        /// <param name="strSQL">sql语句，为空表示与单数据集语句配置一致</param>
        /// <returns></returns>
        public int SetMultiDataSetParameter(String strDatasetPath, String strDSName, String strSQL)
        {
#if ReportNewEngine
            return this.reportViewer1.SetMultiDataSetParameter(strDatasetPath, strDSName, strSQL);
#else
#endif
            return 0;
        }

        private IntPtr _hdWnd = IntPtr.Zero;
        /// <summary>
        /// 设置父窗口句柄
        /// </summary>
        /// <param name="hdWnd">句柄</param>
        public void SetParentWndHandle(IntPtr hdWnd)
        {
            _hdWnd = hdWnd;
        }

        private PMSPublicInfo.RefDBType GetRefDBTypeFromString(string strtype)
        {
            switch (strtype)
            {
                case "MSAccess":
                case "0":
                    return PMSPublicInfo.RefDBType.MSAccess;

                case "MSSqlServer":
                case "1":
                    return PMSPublicInfo.RefDBType.MSSqlServer;

                case "Oracle":
                case "2":
                    return PMSPublicInfo.RefDBType.Oracle;

                case "OleDB":
                case "3":
                    return PMSPublicInfo.RefDBType.OleDB;

                default:
                    return PMSPublicInfo.RefDBType.NULL;
            }
        }

        private PMSPublicInfo.ConnectType GetConnectTypeFromString(string strtype)
        {
            switch (strtype)
            {
                case "namepipe":
                case "0":
                    return PMSPublicInfo.ConnectType.namepipe;

                case "tcpip":
                case "1":
                    return PMSPublicInfo.ConnectType.tcpip;

                default:
                    return PMSPublicInfo.ConnectType.namepipe;
            }
        }

        #region ActiveX控件注册

        /// <summary>
        /// ActiveX控件注册
        /// </summary>
        /// <param name="i_Key">registration key</param>
        [ComRegisterFunction()]
        public static void RegisterClass(string i_Key)
        {
            // strip off HKEY_CLASSES_ROOT\ from the passed key as I don't need it
            StringBuilder sb = new StringBuilder(i_Key);
            sb.Replace(@"HKEY_CLASSES_ROOT\", "");

            // open the CLSID\{guid} key for write access
            RegistryKey registerKey = Registry.ClassesRoot.OpenSubKey(sb.ToString(), true);

            // and create the 'Control' key - this allows it to show up in 
            // the ActiveX control container 
            RegistryKey ctrl = registerKey.CreateSubKey("Control");
            ctrl.Close();

            // next create the CodeBase entry - needed if not string named and GACced.
            RegistryKey inprocServer32 = registerKey.OpenSubKey("InprocServer32", true);
            inprocServer32.SetValue("CodeBase", Assembly.GetExecutingAssembly().CodeBase);
            inprocServer32.Close();

            // finally close the main key
            registerKey.Close();
        }

        /// <summary>
        /// Unregister ActiveX dll function
        /// </summary>
        /// <param name="i_Key"></param>
        [ComUnregisterFunction()]
        public static void UnregisterClass(string i_Key)
        {
            // strip off HKEY_CLASSES_ROOT\ from the passed key as I don't need it
            StringBuilder sb = new StringBuilder(i_Key);
            sb.Replace(@"HKEY_CLASSES_ROOT\", "");

            // open HKCR\CLSID\{guid} for write access
            RegistryKey registerKey = Registry.ClassesRoot.OpenSubKey(sb.ToString(), true);

            // delete the 'Control' key, but don't throw an exception if it does not exist
            registerKey.DeleteSubKey("Control", false);

            // next open up InprocServer32
            RegistryKey inprocServer32 = registerKey.OpenSubKey("InprocServer32", true);

            // and delete the CodeBase key, again not throwing if missing
            inprocServer32.DeleteSubKey("CodeBase", false);

            // finally close the main key
            registerKey.Close();
        }
        #endregion
        #endregion

        private bool QueryReport(string rptFilePath)
        {
#if ReportNewEngine
            return service.QueryReport(rptFilePath, this.reportViewer1);
#else
            return service.QueryReport(rptFilePath, this.pmsReportViewer1);
#endif
        }

        private void MESReportViewer_SizeChanged(object sender, EventArgs e)
        {
#if ReportNewEngine
            reportViewer1.Invalidate(true);
#else
            pmsReportViewer1.Invalidate(true);
#endif
        }

        public bool OpenReport(string strFileName, string reportVarFilepath)
        {
            // 设置报表名，用于注册报表UI层变量
            this.pmsReportViewer1.CurrentFile = Path.GetFileNameWithoutExtension(_strFileName);
            return service.QueryReport(strFileName, reportVarFilepath, this.pmsReportViewer1);
        }

        public bool RunReport(string strFileFullPath)
        {
            // 设置报表名，用于注册报表UI层变量
            this.pmsReportViewer1.CurrentFile = Path.GetFileNameWithoutExtension(strFileFullPath);
            _strFileName = strFileFullPath;
#if ReportNewEngine
            this.reportViewer1.InitialReport();
            InitConnections();
            return service.RunReport(strFileFullPath, this.reportViewer1);
#else
            InitConnections();
            return service.RunReport(strFileFullPath, this.pmsReportViewer1);
#endif
        }

        public void ReleaseReportResource()
        {
            service.ReleaseReportResource();
        }

        public bool CancelReport()
        {
#if ReportNewEngine
            this.pmsReportViewer1.CancelThread();
#else
#endif
            return true;
        }

        public void ReleaseReport()
        {
            _dogExpert.Release();
            Dispose(true);
        }

        public object GetReportCtrlExpressions(string xmlFilePath)
        {
            if (null == reportRun)
                reportRun = new PMS.Libraries.ToolControls.MESReportRun();

            return reportRun.GetReportCtrlExpressions(xmlFilePath, this.pmsReportViewer1);
        }

        public object GetReportCtrlExpressions(object rptFileObj)
        {
            if (null == reportRun)
                reportRun = new PMS.Libraries.ToolControls.MESReportRun();

            return reportRun.GetReportCtrlExpressions(rptFileObj, this.pmsReportViewer1);
        }

        public int SetToolBar(int Index, bool Visible)
        {
#if ReportNewEngine
            this.reportViewer1.RunMode = false;//出现打开文件工具条
            return 1;
#else
            return this.pmsReportViewer1.SetToolBar(Index, Visible);
#endif
        }

        public bool RunMESReport(string strFileFullPath)
        {
            return RunMESReport(strFileFullPath, null);
        }

        public bool RunMESReport(string strFileFullPath, Dictionary<string, object> vars)
        {
            try
            {
                SetProjectPath(AppDomain.CurrentDomain.BaseDirectory);
                SetReport(strFileFullPath);
                if (null != vars)
                {
                    IEnumerator<KeyValuePair<string, object>> itor = vars.GetEnumerator();
                    if (null != itor)
                    {
                        while (itor.MoveNext())
                        {
                            if (!string.IsNullOrEmpty(itor.Current.Key))
                            {
                                SetParameter(itor.Current.Key, itor.Current.Value.ToString());
                            }
                        }
                    }
                }
                if (string.Compare(Path.GetExtension(strFileFullPath), ".drpt", true) == 0
                    || string.Compare(Path.GetExtension(strFileFullPath), ".orpt", true) == 0
                    || string.Compare(Path.GetExtension(strFileFullPath), ".rpt", true) == 0)
                    QueryReport();
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        private void pmsReportViewer1_SaveAsProjectFile(object sender, EventArgs e)
        {
            Export();
        }

        private void mesObjectReportViewer1_OpenLocalFile(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "orpt files (*.orpt)|*.orpt|drpt files (*.drpt)|*.drpt";
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    SetProjectPath(AppDomain.CurrentDomain.BaseDirectory);
                    SetReport(openFileDialog1.FileName);
                    //SetParameter("df", "222");
                    if (string.Compare(Path.GetExtension(openFileDialog1.FileName) , ".drpt",true)==0)
                        QueryReport();
                }
            }
            catch (System.Exception ex)
            {
                
            }
        }

        private void pmsReportViewer1_StopThread(object sender, System.EventArgs e)
        {
            if (null != RptQueryEnd)
            {
                RptQueryEnd(sender, e);
            }
            if(_hdWnd != IntPtr.Zero)
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.PostMsgToMainForm(_hdWnd, PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_PARENTFORMHANDLE, PMS.Libraries.ToolControls.PMSPublicInfo.Message.NSPLUG_EVENT_ENDOPEN, IntPtr.Zero);
        }

        private void pmsReportViewer1_StartThread(object sender, System.EventArgs e)
        {
            //if(_hdWnd != IntPtr.Zero)
            //    PMS.Libraries.ToolControls.PMSPublicInfo.Message.PostMsgToMainForm(_hdWnd, PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_PARENTFORMHANDLE, (IntPtr)1, IntPtr.Zero);
        }

        private void pmsReportViewer1_ExportFinish(object sender, System.EventArgs e)
        {
            if (null != RptExportEnd)
            {
                RptExportEnd(sender, e);
            }
            if (_hdWnd != IntPtr.Zero)
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.PostMsgToMainForm(_hdWnd, PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_PARENTFORMHANDLE, PMS.Libraries.ToolControls.PMSPublicInfo.Message.NSPLUG_EVENT_ENDEXPORT, IntPtr.Zero);
        }

        private void pmsReportViewer1_PrintFinish(object sender, System.EventArgs e)
        {
            if (null != RptPrintEnd)
            {
                RptPrintEnd(sender, e);
            }
            if (_hdWnd != IntPtr.Zero)
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.PostMsgToMainForm(_hdWnd, PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_PARENTFORMHANDLE, PMS.Libraries.ToolControls.PMSPublicInfo.Message.NSPLUG_EVENT_ENDPRINT, IntPtr.Zero);
        }

        private void reportViewer1_OnConfig(object sender)
        {
            using (ReportConfigForm form = new ReportConfigForm())
            {
                form.ShowDialog();
            }
        }

        private bool IsAuthorized()
        {
            if (CurrentPrjInfo.CurrentEnvironment == MESEnvironment.MESReportDesigner
                || CurrentPrjInfo.CurrentEnvironment == MESEnvironment.MESDesigner
                || CurrentPrjInfo.CurrentEnvironment == MESEnvironment.MESDeveloper
                || CurrentPrjInfo.CurrentEnvironment == MESEnvironment.MESClient
                )
                return true;
            else if (CurrentPrjInfo.CurrentEnvironment == MESEnvironment.None)
            {
                if (_dogExpert.IsFieldRunning())
                {
                    return true;
                }
            }
            
            if (_dogExpert.IsMESReportEnabled())
                return true;
            else
            {
                if (_bAuthorized)
                    return true;
                ToolBarVisible = false;
                Form f = this.FindForm();
                f.Text += GetStringFromPublicResourceClass.GetStringFromPublicResource("MESReportIsNotAuthorized");
                return false;
            }
        }

        private void reportViewer1_RptExportEnd(object sender, EventArgs e)
        {
            if (null != RptExportEnd)
            {
                RptExportEnd(sender, e);
            }
            if (_hdWnd != IntPtr.Zero)
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.PostMsgToMainForm(_hdWnd, PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_PARENTFORMHANDLE, PMS.Libraries.ToolControls.PMSPublicInfo.Message.NSPLUG_EVENT_ENDEXPORT, IntPtr.Zero);
        }

        private void reportViewer1_RptPrintEnd(object sender, EventArgs e)
        {
            if (null != RptPrintEnd)
            {
                RptPrintEnd(sender, e);
            }
            if (_hdWnd != IntPtr.Zero)
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.PostMsgToMainForm(_hdWnd, PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_PARENTFORMHANDLE, PMS.Libraries.ToolControls.PMSPublicInfo.Message.NSPLUG_EVENT_ENDPRINT, IntPtr.Zero);
        }

        private void reportViewer1_RptQueryEnd(object sender, EventArgs e)
        {
            if (null != RptQueryEnd)
            {
                RptQueryEnd(sender, e);
            }
            if (_hdWnd != IntPtr.Zero)
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.PostMsgToMainForm(_hdWnd, PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_PARENTFORMHANDLE, PMS.Libraries.ToolControls.PMSPublicInfo.Message.NSPLUG_EVENT_ENDOPEN, IntPtr.Zero);
        }

        private void reportViewer1_OnRefreshReport(object sender)
        {
            if (RunReport(_strFileName))
            {
                QueryReport();
            }
        }

        private void reportViewer1_OnOpenReport(object sender, string fileName)
        {
            if (RunReport(fileName))
            {
                QueryReport();
            }
        }
    }
}
