/***************************************************************************
    GetStringFromPublicResource - PublicInterface.cs
                             -------------------
    begin                : 12:36 p.m. 23/07/2009
    copyright            : (C) 2009 by yanjielu
    email                : yanjielu@hotmail.com
	website:			 : luyanjie-00.blog.163.com	
 ***************************************************************************/

/***************************************************************************
 *                                                                         *
 *   This project is free software; you can redistribute it and/or modify  *
 *   it under the terms of the HITE RD Public License as published by      *
 *   the Free Software Foundation; either version 2 of the License, or     *
 *   (at your option) any later version.                                   *
 *                                                                         *
 ***************************************************************************/


/*====================================================
||--# # # #----###-----###---# # # #----###---###---||
||--#------#----#-------#----#------#----#-----#----||
||--#------#----#-------#----#------#----#-----#----||
||--#------#----#-------#----#------#-----#---#-----||
||--# # # #-----#-------#----# # # # -------#-------||
||--#---# - - - #-------#----#------#-------#--blue-||
||--#----#------#-------#----#------#-------#-------||
||--#-----#-----#-------#----#------#-------#--|C|--||
||--#------#----#-------#----#------#-------#-------||
||--#-------#----# # # #------# # #---------# 2009--||
====================================================*/



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Collections;
using System.Data.Linq;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Remoting.Messaging;
using System.Reflection.Emit;
using System.Xml;
using System.Web;
using System.Security.Permissions;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.IO.Compression;
using System.Net.NetworkInformation;
using System.Diagnostics;


namespace PMS.Libraries.ToolControls.PMSPublicInfo
{
	#region enum

	public enum ScriptLanguage
	{
		CSharp,
		VBNET
	}

	/// <summary>
	/// 运行模式 
	/// 开发器
	/// 客户端
	/// 报表查看器控件独立运行模式
	/// 报表设计器控件独立运行模式
	/// </summary>
	public enum RunMode
	{
		Develope,
		Client
	}

	public enum MESEnvironment
	{
		None = 0,
		MESReportDesigner,
		MESReportViewer,
		MESFormDesigner,
		MESFormViewer,
		MESDeveloper,
		MESClient,
		MESReportRunner,
		MESDesigner,
		MESWFServer,
		MESReportServer,
		MESGeneralSevice,
		MESDataModelSevice,
	}

	public enum InstallMode
	{
		None = 0,
		MESReportDesigner_NSPlugin = 1,
		MESReportViewer_NSPlugin = 2,
		MESBroadcastClient_NSPlugin = 4,
		MESFormDesigner_NSPlugin = 8,
		MESFormViewer_NSPlugin = 16,
		AllDesign = MESReportDesigner_NSPlugin | MESFormDesigner_NSPlugin,
		AllRun = MESReportViewer_NSPlugin | MESFormViewer_NSPlugin | MESBroadcastClient_NSPlugin,
		MESDeveloper = 32,
		MESClient = 64,
	}

	/// <summary>
	/// 定向发布包
	/// </summary>
	public enum PackageFor
	{
		None = 0,
		ShuangLiang,
		// 双良
	}

	/// <summary>
	/// 引用数据库类型 
	/// </summary>
	public enum RefDBType
	{
		NULL = -1,
		MSAccess,
		MSSqlServer,
		Oracle,
		OleDB
	}

	/// <summary>
	/// 连接主数据库类型 
	/// </summary>
	public enum MESDBType
	{
		MSSqlServer,
		Oracle
	}

	public enum TemplateTableType
	{
		NONE,
		InfoTable,
		MapTable
	}

	[DataContract]
	public enum DocType
	{
		[EnumMember]
		All = 0,
		Report,
		Static,
		Sheet,
		OrgnizationStruct,
		DBDesigner,
		Custom,
		Template,
		TestRunTime,
		RunTime,
		DVRunTime,
		[EnumMember]
		ReportDesigner,
		ReportTestRunTime,
		ReportRunTime,
		MesSheet,
		MESWorkFlow,
		MESWorkFlowMonitor,
		MESWorkFlowInstanceMonitor,
		MESWorkFlowCustom,
		[EnumMember]
		FormDesigner,
		DocumentWindow,
		NavigatorDesigner,
	}

	public enum CfgType
	{
		None = 0,
		RefDBSource,
		MapTable,
		CustomMsg,
		SystemStruct,
	}

	public enum TabType
	{
		Designer = 1,
		Script,
		DBVarDefine,
		TestRun
	}

	public enum ShowMode
	{
		DoModal = 1,
		Normal
	}

	#endregion

	[Serializable]
	public class ConvertLanguage
	{
		public ScriptLanguage OldLanguage;
		public ScriptLanguage NewLanguage;
	}

	/// <summary>
	/// 数据库表类型说明
	/// </summary>
	public class TableType
	{
		// 普通表
		public const string Table = "Table";
		// 结构表
		public const string MapTable = "MapTable";
		// 元素表
		public const string InfoTable = "InfoTable";
		// 元素与元素关系表
		public const string IIRelationTable = "I-I";
		// 元素与结构关系表
		public const string IMRelationTable = "I-M";
	}

	public class TablePropertyName
	{
		// 表类型
		public const string TableType = "TableType";

		// 表描述
		public const string Description = "Description";
	}

	public class ColumnPropertyName
	{
		// 列类型
		public const string ColumnType = "ColumnType";

		// 列描述
		public const string Description = "Description";
        
		// 列加密类型
		public const string EncryptType = "EncryptType";
	}

	/// <summary>
	/// 数据库表中列类型说明
	/// </summary>
	public class ColumnType
	{
		public const string MapTree = "MapTree";

		public const string Encrypted = "Encrypted";
	}

	/// <summary>
	/// 数据库中列加密类型
	/// </summary>
	public class EncryptType
	{
		public const string DES = "DES";

		public const string MD5 = "MD5";
	}

	public class DatabaseProvider
	{
		public const string SqlProvider = "System.Data.SqlClient";
        
		public const string OracleProvider = "Oracle.DataAccess.Client";

		public const string OracleDevartProvider = "Devart.Data.Oracle";

		public const string EFOracleProvider = "MES.EFOracleProvider";
	}

	public class ServerConfigFileGuid
	{
		// 引用数据源配置
		public static readonly Guid RefDBSourcesFileConfig = new Guid ("c42e2ab6-4271-474e-ac87-ec1b14bb1465");

		// 映射表配置
		public static readonly Guid MapTableFileConfig = new Guid ("f729202e-9ec5-4060-b509-1b6a7628669f");

		// 自定义消息
		public static readonly Guid CustomMsgConfig = new Guid ("a7d8934b-c473-411c-bd58-6773b2bc07d4");

		// 系统结构
		public static readonly Guid SystemStructConfig = new Guid ("824ca068-94d2-4239-8b82-9588a23e72bf");

		//// 报表设计时模板
		//public static Guid ReportDesignModel = new Guid("c5688f99-5a6e-4752-989b-6ec95c8c902e");

		//// 报表查看器
		//public static Guid ReportViewer = new Guid("319a40b9-0293-4b73-8545-335be9e98b6b");

		////
		//public static Guid DBSourceDefine = new Guid("21B2A4EE-4C0D-400E-B3C0-50ED5EC75D2D");
	}

	public class DBTableNameComparer : IEqualityComparer<string>
	{
		public string Text;
		public string Value;

		public bool Equals (string sv1, string sv2)
		{
			if (sv1.ToUpper () == sv2.ToUpper ())
				return true;
			else
				return false;
		}

		public int GetHashCode (string sv)
		{
			return sv.ToUpper ().GetHashCode ();
		}
	}

	public class PropertySorter : IComparer
	{
		#region IComparer Member

		public int Compare (object x, object y)
		{
			PropertyDescriptor p1 = x as PropertyDescriptor;
			PropertyDescriptor p2 = y as PropertyDescriptor;

			MESPropertyAttributeAttribute patt1 = null;
			MESPropertyAttributeAttribute patt2 = null;

			Attribute att1 = p1.Attributes [typeof(MESPropertyAttributeAttribute)];
			if (att1 != null) {
				patt1 = (MESPropertyAttributeAttribute)att1;
			}

			Attribute att2 = p2.Attributes [typeof(MESPropertyAttributeAttribute)];
			if (att2 != null) {
				patt2 = (MESPropertyAttributeAttribute)att2;
			}

			if (patt1 == null && patt2 != null) {
				return -patt2.AttOrder;
			} else if (patt1 != null && patt2 == null) {
				return patt1.AttOrder;
			} else if ((patt1 == null && patt2 == null) || patt1.AttOrder == patt2.AttOrder) {
				return 0;
			} else {
				return CompareInt (patt1.AttOrder, patt2.AttOrder);
			}
		}

		/// <summary>
		/// 比较两个数字的大小
		/// </summary>
		/// <param name="ipx">要比较的第一个对象</param>
		/// <param name="ipy">要比较的第二个对象</param>
		/// <returns>比较的结果.如果相等返回0，如果x大于y返回1，如果x小于y返回-1</returns>
		private int CompareInt (int x, int y)
		{
			if (x > y) {
				return 1;
			} else if (x < y) {
				return -1;
			} else {
				return 0;
			}
		}

		#endregion
	}

	public class TableLayoutControlsSorter : IComparer
	{
		#region IComparer Member

		public int Compare (object x, object y)
		{
			Control c1 = x as Control;
			Control c2 = y as Control;

			if (c1.Location.Y < c2.Location.Y) {
				return -1;
			} else if (c1.Location.Y > c2.Location.Y) {
				return 1;
			} else {// if (c1.Location.Y == c2.Location.Y)
				return CompareInt (c1.Location.X, c2.Location.X);
			}
		}

		/// <summary>
		/// 比较两个数字的大小
		/// </summary>
		/// <param name="ipx">要比较的第一个对象</param>
		/// <param name="ipy">要比较的第二个对象</param>
		/// <returns>比较的结果.如果相等返回0，如果x大于y返回1，如果x小于y返回-1</returns>
		private int CompareInt (int x, int y)
		{
			if (x > y) {
				return 1;
			} else if (x < y) {
				return -1;
			} else {
				return 0;
			}
		}

		#endregion

		public static Control.ControlCollection SortItByYXCoordinate (Control.ControlCollection ControlCollection)
		{
			IEnumerable<Control> sortedlist =
				from c in ControlCollection.Cast<Control> ()
				orderby c.Location.Y, c.Location.X
				select c;

			int counter = 0;
			foreach (Control muc in sortedlist) {
				ControlCollection.SetChildIndex (muc, counter);
				counter++;
			}
			return ControlCollection;
		}
	}

	//打印参数
	[Serializable]
	public class MESPrintSetup
	{
		public PageSettings DefaultPageSettings { get; set; }

		[DefaultValue ("document")]
		public string DocumentName { get; set; }

		[DefaultValue (false)]
		public bool OriginAtMargins { get; set; }

		[Browsable (false)]
		public PrinterSettings PrinterSettings { get; set; }
	}

	//视图标识
	[Serializable]
	public class MESCustomViewIdentity : ICloneable
	{
		public string FullPath { get; set; }

		[DefaultValue (false)]
		public bool IsSpecifiedVersion { get; set; }

		public string ViewName { get; set; }

		public Guid ViewID { get; set; }

		public Guid ParentID { get; set; }

		public object Clone ()
		{
			MESCustomViewIdentity obj = new MESCustomViewIdentity ();
			obj.FullPath = this.FullPath;
			obj.IsSpecifiedVersion = this.IsSpecifiedVersion;
			obj.ViewName = this.ViewName;
			obj.ViewID = this.ViewID;
			obj.ParentID = this.ParentID;
			return obj;
		}

		public override string ToString ()
		{
			return string.Format ("{0}{1}", FullPath, ViewName);
		}
	}
	//todo:qiuleilie
	public enum ReportViewType
	{
		None,
		Query,
		Export,
		ExportQuery
	}

	/// <summary>
	/// 当前项目公有信息
	/// </summary>
	public class CurrentPrjInfo
	{
		// 当前报表的控件表达式List，沈寅辉
		public static object CurrentRptCtrlExpressions = null;

		public static object CurrentRptDataDef = null;

		public static Control CurrentControl = null;

		public static TreeView CurrentReportUIExpressionTreeView = null;

		public static object CurrentFrmDataDef = null;

		public static object CurrentWorkFlowDataDef = null;

		/// <summary>
		/// 通用型当前数据，设计时用，不支持并行取值
		/// </summary>
		public static object CurrentObj = null;

		// 当前登录用户
		public static string CurrentLoginUserID = string.Empty;

		// 运行模式-开发器，客户端
		public static RunMode CurrentRunMode = RunMode.Develope;

		// 当前运行环境
		public static MESEnvironment CurrentEnvironment = MESEnvironment.None;

		// 当前发布包
		public static PackageFor CurrentPackageFor = PackageFor.None;

		// 当前安装模式
		public static InstallMode CurrentInstallMode = InstallMode.None;

		// 插件标识
		public static bool IsPlugin = false;

		// 视图结构配置中
		public static bool ViewOrgStructConfiging = false;

		// 当前配置视图结构用户
		public static string ViewOrgStructConfigingCheckUserID = string.Empty;

		// 本机IP地址
		public static System.Net.IPAddress LoacalIPAddress = System.Net.IPAddress.Parse ("127.0.0.1");

		// 现版本软件所对应的数据库版本，用来检测数据库版本的可用性
		public static string CurrentMatchVersion = "V2.0";

		// 全屏视窗
		public static object ToggleForm = null;

		public static MESDBType CurrentDBType = MESDBType.MSSqlServer;

		public static MESDBType CurrentTestDBType = MESDBType.MSSqlServer;

		public static bool IsReportViewerMode = false;

		public static bool IsIndependentDesignerMode = false;

		// 当前安装包版本
		public static string CurrentSetupVersion;

		// 当前授权服务地址(eg:10.1.20.77:8087)
		public static string CurrentAuthorizationServerAddress = string.Empty;

		// 新建表单cfgfileinfo
		public static object CurrentNewFormCfgFileInfo = null;

		// 设计时是否正在读取xml配置并反射出控件的过程中
		public static bool ReadingXmlInDesignerTime = false;

		/// <summary>
		/// 系统选项配置
		/// </summary>
		public static PMS.Libraries.ToolControls.PMSPublicInfo.ConfigFile.Options OptionsDS = new PMS.Libraries.ToolControls.PMSPublicInfo.ConfigFile.Options ();


		/// <summary>
		/// 获取定向包类型
		/// </summary>
		/// <param name="packagefor"></param>
		/// <returns></returns>
		public static PackageFor GetCurrentPackageFor (string packagefor)
		{
			if (string.IsNullOrEmpty (packagefor))
				return PackageFor.None;
			foreach (PackageFor v in Enum.GetValues(typeof(PackageFor))) {
				if (string.Compare (packagefor, Enum.GetName (typeof(PackageFor), v), true) == 0) {
					return v;
				}
			}
			return PackageFor.None;
		}

		/// <summary>
		/// 此接口在设计时打开窗口的过程中（即在窗口Show()之前调用返回为空）
		/// </summary>
		/// <returns></returns>
		public static object GetCurrentReportDataDefine ()
		{
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_GETCURRENTREPORTDATADEF;
			IntPtr handle = PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle;
			int iReturn = PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (handle, msgID, IntPtr.Zero, IntPtr.Zero);
			if (iReturn != 0)
				return CurrentRptDataDef;
			else
				return null;
		}

		public static object GetCurrentReportDataDefine (Control ct)
		{
			CurrentControl = ct;

			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_GETCURRENTREPORTDATADEFEx;
			IntPtr handle = PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle;
			int iReturn = PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (handle, msgID, IntPtr.Zero, IntPtr.Zero);
			if (iReturn != 0)
				return CurrentRptDataDef;
			else
				return null;
		}

		public static TreeView GetCurrentReportUIExpressionTreeView ()
		{
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_GETCURRENTREPORTUIEXPRESSIONTREEVIEW;
			IntPtr handle = PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle;
			int iReturn = PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (handle, msgID, IntPtr.Zero, IntPtr.Zero);
			if (iReturn != 0)
				return CurrentReportUIExpressionTreeView;
			else
				return null;
		}

		/// <summary>
		/// 此接口在设计时打开窗口的过程中（即在窗口Show()之前调用返回为空）
		/// </summary>
		/// <returns></returns>
		public static object GetCurrentFormDataDefine ()
		{
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_GETCURRENTFORMDATADEF;
			IntPtr handle = PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle;
			int iReturn = PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (handle, msgID, IntPtr.Zero, IntPtr.Zero);
			if (iReturn != 0)
				return CurrentFrmDataDef;
			else
				return null;
		}

		/// <summary>
		/// 此接口在设计时打开窗口的过程中（即在窗口Show()之前调用返回为空）
		/// </summary>
		/// <returns></returns>
		public static object GetCurrentWorkFlowDataDefine ()
		{
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_GETCURRENTWORKFLOWDATADEF;
			IntPtr handle = PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle;
			int iReturn = PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (handle, msgID, IntPtr.Zero, IntPtr.Zero);
			if (iReturn != 0)
				return CurrentWorkFlowDataDef;
			else
				return null;
		}

		/// <summary>
		/// 此接口在设计时打开窗口的过程中（即在窗口Show()之前调用返回为空）
		/// </summary>
		/// <returns></returns>
		public static object GetCurrentHost ()
		{
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_GETCURRENTHOST;
			IntPtr handle = PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle;
			int iReturn = PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (handle, msgID, IntPtr.Zero, IntPtr.Zero);
			if (iReturn != 0)
				return CurrentObj;
			else
				return null;
		}

		/// <summary>
		/// 此接口在设计时打开窗口的过程中（即在窗口Show()之前调用返回为空）
		/// </summary>
		/// <returns>返回当前活动的IGetCurrent接口对象</returns>
		public static object GetCurrent ()
		{
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_GETCURRENT;
			IntPtr handle = PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle;
			int iReturn = PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (handle, msgID, IntPtr.Zero, IntPtr.Zero);
			if (iReturn != 0)
				return CurrentObj;
			else
				return null;
		}

		/// <summary>
		/// 此接口在设计时打开窗口的过程中（即在窗口Show()之前调用返回为空）
		/// </summary>
		/// <returns></returns>
		public static object GetCurrentDocument ()
		{
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_GETCURRENTDOCUMENT;
			IntPtr handle = PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle;
			int iReturn = PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (handle, msgID, IntPtr.Zero, IntPtr.Zero);
			if (iReturn != 0)
				return CurrentObj;
			else
				return null;
		}

		/// <summary>
		/// 此接口在设计时打开窗口的过程中（即在窗口Show()之前调用返回为空）
		/// </summary>
		/// <returns></returns>
		public static DocType GetCurrentDocumentType ()
		{
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_GETCURRENTDOCUMENTTYPE;
			IntPtr handle = PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle;
			int iReturn = PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (handle, msgID, IntPtr.Zero, IntPtr.Zero);
			if (iReturn != 0)
				return (DocType)Convert.ToInt32 (CurrentObj);
			else
				return DocType.All;
		}

		/// <summary>
		/// 此接口在设计时打开窗口的过程中（即在窗口Show()之前调用返回为空）
		/// </summary>
		/// <returns></returns>
		public static object GetCurrentDocumentIdentity ()
		{
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_GETCURRENTDOCUMENTIDENTITY;
			IntPtr handle = PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle;
			if (IntPtr.Zero == handle)
				return null;
			int iReturn = PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (handle, msgID, IntPtr.Zero, IntPtr.Zero);
			if (iReturn != 0)
				return CurrentObj;
			else
				return null;
		}

        

		public static bool IsCustomGetHostMode ()
		{
			object o = GetCurrentDocument ();
			if (null != o && o is IDocument) {
				switch ((o as IDocument).DocumentType) {
				case DocType.MESWorkFlow:
				case DocType.MESWorkFlowCustom:
					return true;
				default:
					return false;
				}
			}
			return false;
		}

		/// <summary>
		/// 此接口在设计时打开窗口的过程中（即在窗口Show()之前调用返回为空）
		/// </summary>
		/// <returns></returns>
		public static object GetCurrentObj ()
		{
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_GETCURRENTOBJ;
			IntPtr handle = PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle;
			int iReturn = PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (handle, msgID, IntPtr.Zero, IntPtr.Zero);
			if (iReturn != 0)
				return CurrentObj;
			else
				return null;
		}

		public static List<PMSRefDBConnection> GetRefDBConnectionList ()
		{
			var DBSourceDefines = from DBSourceDefine in PMSDBStructure.PMSCenterDataContext.s_DBSourceDefine
			                      select DBSourceDefine;
			PMSDBStructure.PMSCenterDataContext.Refresh (System.Data.Entity.Core.Objects.RefreshMode.StoreWins, DBSourceDefines);

			if (DBSourceDefines.Count () == 0)
				return null;
			else {
				List<PMSRefDBConnection> list = new List<PMSRefDBConnection> ();
				foreach (var source in DBSourceDefines) {
					PMSRefDBConnection rc = new PMSRefDBConnection ();
					rc.RefDBType = PMS.Libraries.ToolControls.PMSPublicInfo.PublicFunctionClass.GetRefDBType (source.DBType);
					rc.StrServerName = source.DBServer;
					rc.StrDBName = source.DBName;
					rc.StrUserID = source.UserID;
					rc.StrPassWord = source.Password;
					rc.EConnectType = PMS.Libraries.ToolControls.PMSPublicInfo.PublicFunctionClass.GetConnectType (source.Protocol);
					rc.StrPortID = source.PortID;
					rc.StrDBPath = source.Path;
					rc.ConnectString = source.ConnectString;
					list.Add (rc);
				}
				return list;
			}
		}

		public static PMSRefDBConnection GetRefDBConnectionList (string SourceName)
		{
			var DBSourceDefines = from DBSourceDefine in PMSDBStructure.PMSCenterDataContext.s_DBSourceDefine
			                      where DBSourceDefine.Name == SourceName
			                      select DBSourceDefine;
			PMSDBStructure.PMSCenterDataContext.Refresh (System.Data.Entity.Core.Objects.RefreshMode.StoreWins, DBSourceDefines);

			if (DBSourceDefines.Count () == 0)
				return null;
			else {
				s_DBSourceDefine source = DBSourceDefines.First ();

				PMSRefDBConnection rc = new PMSRefDBConnection ();
				rc.RefDBType = PMS.Libraries.ToolControls.PMSPublicInfo.PublicFunctionClass.GetRefDBType (source.DBType);
				rc.StrServerName = source.DBServer;
				rc.StrDBName = source.DBName;
				rc.StrUserID = source.UserID;
				rc.StrPassWord = source.Password;
				rc.EConnectType = PMS.Libraries.ToolControls.PMSPublicInfo.PublicFunctionClass.GetConnectType (source.Protocol);
				rc.StrPortID = source.PortID;
				rc.StrDBPath = source.Path;
				rc.ConnectString = source.ConnectString;

				return rc;
			}
		}

		public static PMSRefDBConnectionObj GetDefaultRefDBConnectionObjList ()
		{
			List<PMSRefDBConnectionObj> list = GetRefDBConnectionObjListFromLocalFile ();
			if (null != list) {
				IEnumerable<PMSRefDBConnectionObj> ret = list.Where (o => o.BDefault == true);
				if (ret.Count () > 0) {
					return ret.First ();
				}
			}
			return null;
		}

		public static List<PMSRefDBConnectionObj> GetRefDBConnectionObjList ()
		{
			return GetRefDBConnectionObjListFromLocalFile ();

			if (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.IsReportViewerMode == true) {
				return GetRefDBConnectionObjListFromLocalFile ();
			}

			var DBSourceDefines = from DBSourceDefine in PMSDBStructure.PMSCenterDataContext.s_DBSourceDefine
			                      select DBSourceDefine;
			PMSDBStructure.PMSCenterDataContext.Refresh (System.Data.Entity.Core.Objects.RefreshMode.StoreWins, DBSourceDefines);

			if (DBSourceDefines.Count () == 0)
				return null;
			else {
				List<PMSRefDBConnectionObj> list = new List<PMSRefDBConnectionObj> ();
				foreach (var source in DBSourceDefines) {
					PMSRefDBConnectionObj rco = new PMSRefDBConnectionObj ();
					rco.StrName = source.Name;
					rco.StrDescription = source.Description;
					PMSRefDBConnection rc = new PMSRefDBConnection ();
					rc.RefDBType = PMS.Libraries.ToolControls.PMSPublicInfo.PublicFunctionClass.GetRefDBType (source.DBType);
					rc.StrServerName = source.DBServer;
					rc.StrDBName = source.DBName;
					rc.StrUserID = source.UserID;
					rc.StrPassWord = source.Password;
					rc.EConnectType = PMS.Libraries.ToolControls.PMSPublicInfo.PublicFunctionClass.GetConnectType (source.Protocol);
					rc.StrPortID = source.PortID;
					rc.StrDBPath = source.Path;
					rc.ConnectString = source.ConnectString;
					rco.RefDBConnection = rc;
					list.Add (rco);
				}
				return list;
			}
		}

		public static bool SaveRefDBConnectionObjList (List<PMSRefDBConnectionObj> obl)
		{
			return PMSFileClass.SaveFile (ProjectPathClass.RefDBSourcesFilePath, obl);
		}

		public static List<PMSRefDBConnectionObj> GetRefDBConnectionObjListFromLocalFile ()
		{
			object ob = null;
			if (!PMSFileClass.LoadFile (ProjectPathClass.RefDBSourcesFilePath, ref ob))
				return null;
			List<PMSRefDBConnectionObj> list = ob as List<PMSRefDBConnectionObj>;
			return list;
		}

		public static List<PMSRefDBConnectionObj> GetRefDBConnectionObjListFromLocalFile (string dataSourceCfgFilePath)
		{
			object ob = null;
			if (!PMSFileClass.LoadFile (dataSourceCfgFilePath, ref ob))
				return null;
			List<PMSRefDBConnectionObj> list = ob as List<PMSRefDBConnectionObj>;
			return list;
		}

		public static bool SaveServerRefDBConnectionObjList (List<PMSRefDBConnectionObj> obl)
		{
			return PMSFileClass.SaveFile (ProjectPathClass.ServerRefDBSourcesFilePath, obl);
		}

		public static List<PMSRefDBConnectionObj> GetServerRefDBConnectionObjListFromLocalFile ()
		{
			object ob = null;
			if (!PMSFileClass.LoadFile (ProjectPathClass.ServerRefDBSourcesFilePath, ref ob))
				return null;
			List<PMSRefDBConnectionObj> list = ob as List<PMSRefDBConnectionObj>;
			return list;
		}

		public static PMSRefDBConnectionObj GetRefDBConnectionObj (string SourceName)
		{
			var DBSourceDefines = from DBSourceDefine in PMSDBStructure.PMSCenterDataContext.s_DBSourceDefine
			                      where DBSourceDefine.Name == SourceName
			                      select DBSourceDefine;
			PMSDBStructure.PMSCenterDataContext.Refresh (System.Data.Entity.Core.Objects.RefreshMode.StoreWins, DBSourceDefines);

			if (DBSourceDefines.Count () == 0)
				return null;
			else {
				s_DBSourceDefine source = DBSourceDefines.First ();

				PMSRefDBConnectionObj rco = new PMSRefDBConnectionObj ();
				rco.StrName = source.Name;
				rco.StrDescription = source.Description;
				PMSRefDBConnection rc = new PMSRefDBConnection ();
				rc.RefDBType = PMS.Libraries.ToolControls.PMSPublicInfo.PublicFunctionClass.GetRefDBType (source.DBType);
				rc.StrServerName = source.DBServer;
				rc.StrDBName = source.DBName;
				rc.StrUserID = source.UserID;
				rc.StrPassWord = source.Password;
				rc.EConnectType = PMS.Libraries.ToolControls.PMSPublicInfo.PublicFunctionClass.GetConnectType (source.Protocol);
				rc.StrPortID = source.PortID;
				rc.StrDBPath = source.Path;
				rc.ConnectString = source.ConnectString;
				rco.RefDBConnection = rc;

				return rco;
			}
		}

		public static List<string> EnumDataMapTable ()
		{
			MES.DataMapTable.MapTable maptable = new MES.DataMapTable.MapTable (ProjectPathClass.PrjMapTableFilePath);
			return maptable.EnumDataMapTable ();
		}

		public static string GetReplaceValue (string TableName, string OldValue)
		{
			MES.DataMapTable.MapTable maptable = new MES.DataMapTable.MapTable (ProjectPathClass.PrjMapTableFilePath);
			return maptable.GetReplaceValue (TableName, OldValue);
		}

		public static string GetOldValue (string TableName, string ReplaceValue)
		{
			MES.DataMapTable.MapTable maptable = new MES.DataMapTable.MapTable (ProjectPathClass.PrjMapTableFilePath);
			return maptable.GetOldValue (TableName, ReplaceValue);
		}

		public static object GetReportCtrlExpressions ()
		{
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_GETCURRENTRPTCTRLEXPRESSIONS;
			IntPtr handle = PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle;
			int iReturn = PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (handle, msgID, IntPtr.Zero, IntPtr.Zero);
			if (iReturn != 0)
				return CurrentRptCtrlExpressions;
			else
				return null;
		}

		public static string GetFormFile (Guid id)
		{
			var fileInfos = (from fileInfo in PMSDBStructure.PMSCenterDataContext.s_CfgFileInfo
			                 where fileInfo.FID == id && fileInfo.Type == 2
			                 select fileInfo).First ();
			PMSDBStructure.PMSCenterDataContext.Refresh (System.Data.Entity.Core.Objects.RefreshMode.StoreWins, fileInfos);

			if (fileInfos == null)
				return null;
			else {
				string strFilePath = Path.Combine (ProjectPathClass.UserCustomPath, id.ToString () + PublicFunctionClass.FormFilePostfix);
				return strFilePath;
			}
		}

		public static string GetReportFile (Guid id)
		{
			var fileInfos = (from fileInfo in PMSDBStructure.PMSCenterDataContext.s_CfgFileInfo
			                 where fileInfo.FID == id && fileInfo.Type == 1
			                 select fileInfo).First ();
			PMSDBStructure.PMSCenterDataContext.Refresh (System.Data.Entity.Core.Objects.RefreshMode.StoreWins, fileInfos);

			if (fileInfos == null)
				return null;
			else {
				string strFilePath = Path.Combine (ProjectPathClass.UserCustomPath, id.ToString () + PublicFunctionClass.ReportFilePostfix);
				return strFilePath;
			}
		}

		public static string GetViewFile (Guid id)
		{
			var fileInfos = (from fileInfo in PMSDBStructure.PMSCenterDataContext.s_CfgFileInfo
			                 where fileInfo.FID == id && fileInfo.Type == 0
			                 select fileInfo).First ();
			PMSDBStructure.PMSCenterDataContext.Refresh (System.Data.Entity.Core.Objects.RefreshMode.StoreWins, fileInfos);

			if (fileInfos == null)
				return null;
			else {
				string strFilePath = Path.Combine (ProjectPathClass.UserCustomPath, id.ToString () + PublicFunctionClass.CustomFilePostfix);
				return strFilePath;
			}
		}

		public static string GetViewFile (Guid parentid, string viewname)
		{
			var query = from ConfigFileInfo in PMSDBStructure.PMSCenterDataContext.s_CfgFileInfo
                        orderby ConfigFileInfo.Version descending
			            where ConfigFileInfo.Name == viewname && ((from imRelation in PMSDBStructure.PMSCenterDataContext.s_CfgFInfo_s_CfgFInfoMap_r
			                                                       where imRelation.MAPID == parentid
			                                                       select imRelation.FID).Distinct ()).Contains (ConfigFileInfo.FID)
			            select ConfigFileInfo;

			PMSDBStructure.PMSCenterDataContext.Refresh (System.Data.Entity.Core.Objects.RefreshMode.StoreWins, query);

			if (query.Count () == 0)
				return null;
			else {
				s_CfgFileInfo info = null;
				var q = query.Where (p => p.Current_ == true);
				if (q.Count () == 0) {
					info = query.First ();
				} else {
					info = q.First ();
				}
				string strFilePath = Path.Combine (ProjectPathClass.UserCustomPath, info.FID.ToString () + Path.GetExtension (info.Name));
				return strFilePath;
			}
		}

		public static Guid GetViewFileID (Guid parentid, string viewname)
		{
			var query = from ConfigFileInfo in PMSDBStructure.PMSCenterDataContext.s_CfgFileInfo
                        orderby ConfigFileInfo.Version descending
			            where ConfigFileInfo.Name == viewname && ((from imRelation in PMSDBStructure.PMSCenterDataContext.s_CfgFInfo_s_CfgFInfoMap_r
			                                                       where imRelation.MAPID == parentid
			                                                       select imRelation.FID).Distinct ()).Contains (ConfigFileInfo.FID)
			            select ConfigFileInfo;

			PMSDBStructure.PMSCenterDataContext.Refresh (System.Data.Entity.Core.Objects.RefreshMode.StoreWins, query);

			if (query.Count () == 0)
				return Guid.Empty;
			else {
				s_CfgFileInfo info = null;
				var q = query.Where (p => p.Current_ == true);
				if (q.Count () == 0) {
					info = query.First ();
				} else {
					info = q.First ();
				}
				return info.FID;
			}
		}

		public static s_CfgFileInfo GetCfgFileInfo (MESCustomViewIdentity identity)
		{
			if (identity.IsSpecifiedVersion) {
				var fileInfos = (from fileInfo in PMSDBStructure.PMSCenterDataContext.s_CfgFileInfo
				                 where fileInfo.FID == identity.ViewID
				                 select fileInfo).First ();
				PMSDBStructure.PMSCenterDataContext.Refresh (System.Data.Entity.Core.Objects.RefreshMode.StoreWins, fileInfos);

				if (null != fileInfos)
					return fileInfos;
			} else {
				var query = from ConfigFileInfo in PMSDBStructure.PMSCenterDataContext.s_CfgFileInfo
                            orderby ConfigFileInfo.Version descending
				            where ConfigFileInfo.Name == identity.ViewName && ((from imRelation in PMSDBStructure.PMSCenterDataContext.s_CfgFInfo_s_CfgFInfoMap_r
				                                                                where imRelation.MAPID == identity.ParentID
				                                                                select imRelation.FID).Distinct ()).Contains (ConfigFileInfo.FID)
				            select ConfigFileInfo;

				PMSDBStructure.PMSCenterDataContext.Refresh (System.Data.Entity.Core.Objects.RefreshMode.StoreWins, query);

				if (query.Count () > 0) {
					s_CfgFileInfo info = null;
					var q = query.Where (p => p.Current_ == true);
					if (q.Count () == 0) {
						info = query.First ();
					} else {
						info = q.First ();
					}
					return info;
				}
			}
			return null;
		}

		public static bool GetFormTreeView (int doctype, ref TreeView tv)
		{
			tv.Nodes.Clear ();
			tv.ShowNodeToolTips = true;
			System.Windows.Forms.ImageList imageList1 = new System.Windows.Forms.ImageList ();
			// 
			// imageList1
			// 
			imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(PMSPublicResource.ResourceManager.GetObject ("imageList1.ImageStream")));
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			imageList1.Images.SetKeyName (0, "UserCsf.png");
			imageList1.Images.SetKeyName (1, "UserCustom.png");
			imageList1.Images.SetKeyName (2, "folderOpend.png");
			imageList1.Images.SetKeyName (3, "folderClosed.png");
			imageList1.Images.SetKeyName (4, "Current.png");
			imageList1.Images.SetKeyName (5, "Version.png");
			imageList1.Images.SetKeyName (6, "Report.png");
			imageList1.Images.SetKeyName (7, "Reports.png");
			tv.ImageList = imageList1;

			Queue<TreeNode> queue = new Queue<TreeNode> ();

			var ConfigFileInfoMaps = from FileInfoMap in PMSDBStructure.PMSCenterDataContext.s_CfgFInfoMap
			                         select FileInfoMap;

			var rootGroups = from rootGroup in ConfigFileInfoMaps
			                 orderby rootGroup.Name
			                 where rootGroup.ParentID == null
			                 select rootGroup;

			PMSDBStructure.PMSCenterDataContext.Refresh (System.Data.Entity.Core.Objects.RefreshMode.StoreWins, rootGroups);

			foreach (var rootGroup in rootGroups) {
				TreeNode itemRoot = new TreeNode (rootGroup.Name, 3, 2);
				itemRoot.Tag = rootGroup;
				tv.Nodes.Add (itemRoot);
				itemRoot.Expand ();
				queue.Enqueue (itemRoot);
			}

			while (queue.Count > 0) {//添加一级分类下面的子分类
				TreeNode parentNode = (TreeNode)queue.Dequeue ();
				var nodeGroups = from nodeGroup in ConfigFileInfoMaps
				                 orderby nodeGroup.Name
				                 where nodeGroup.ParentID == (Guid)((s_CfgFInfoMap)parentNode.Tag).MAPID
				                 select nodeGroup;

				PMSDBStructure.PMSCenterDataContext.Refresh (System.Data.Entity.Core.Objects.RefreshMode.StoreWins, nodeGroups);

				foreach (var nodeGroup in nodeGroups) {
					TreeNode itemB = new TreeNode (nodeGroup.Name, 3, 2);
					itemB.Tag = nodeGroup;
					parentNode.Nodes.Add (itemB);
					queue.Enqueue (itemB);
				}
			}

			foreach (TreeNode treenode in tv.Nodes) {
				if (doctype == byte.MaxValue)
					InsertItemInfoNodes (treenode, tv);
				else
					InsertItemInfoNodes (treenode, tv, doctype);
			}
			if (doctype == byte.MaxValue)
				InsertItemInfoNodes (null, tv);
			else
				InsertItemInfoNodes (null, tv, doctype);
			return true;
		}

		private static void InsertItemInfoNodes (TreeNode parentNode, TreeView tv, int doctype)
		{
			if (parentNode == null) {
				var query = from ConfigFileInfo in PMSDBStructure.PMSCenterDataContext.s_CfgFileInfo
				            orderby ConfigFileInfo.Name, ConfigFileInfo.Version
				            where !((from imRelation in PMSDBStructure.PMSCenterDataContext.s_CfgFInfo_s_CfgFInfoMap_r
				                     select imRelation.FID).Distinct ()).Contains (ConfigFileInfo.FID)
				                    && ConfigFileInfo.Type == doctype
				            group ConfigFileInfo by ConfigFileInfo.Name into g
				            select g;

				foreach (var gp in query) {
					TreeNode viewNode = new TreeNode (gp.Key, 0, 0);
					tv.Nodes.Add (viewNode);
					foreach (var item in gp) {
						//PMSDBStructure.PMSCenterDataContext.Refresh(System.Data.Entity.Core.Objects.RefreshMode.StoreWins, item);
						//do something
						if (item.Current_ == true) {
							TreeNode versionNode = new TreeNode (PublicFunctionClass.GetTabText (item), 4, 4);
							versionNode.Tag = item;
							versionNode.ToolTipText = PublicFunctionClass.GetTabToolTipText (item);
							viewNode.Nodes.Add (versionNode);
						} else {
							TreeNode versionNode = new TreeNode (PublicFunctionClass.GetTabText (item), 5, 5);
							versionNode.Tag = item;
							versionNode.ToolTipText = PublicFunctionClass.GetTabToolTipText (item);
							viewNode.Nodes.Add (versionNode);
						}
					}
				}
				return;
			}
			foreach (TreeNode treenode in parentNode.Nodes) {
				InsertItemInfoNodes (treenode, tv, doctype);
			}

			if ((Guid)((s_CfgFInfoMap)parentNode.Tag).MAPID == null)
				return;

			var q = from ConfigFileInfo in PMSDBStructure.PMSCenterDataContext.s_CfgFileInfo
			        orderby ConfigFileInfo.Name, ConfigFileInfo.Version
			        where
			                (
			                        from imRelation in PMSDBStructure.PMSCenterDataContext.s_CfgFInfo_s_CfgFInfoMap_r
			                        where imRelation.MAPID == (Guid)((s_CfgFInfoMap)parentNode.Tag).MAPID
			                        select imRelation.FID
			                ).Contains (ConfigFileInfo.FID)
			                && ConfigFileInfo.Type == doctype
			        group ConfigFileInfo by ConfigFileInfo.Name into g
			        select g;

			foreach (var gp in q) {
				TreeNode viewNode = new TreeNode (gp.Key, 0, 0);
				parentNode.Nodes.Add (viewNode);
				foreach (var item in gp) {
					//PMSDBStructure.PMSCenterDataContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, item);
					//do something
					if (item.Current_ == true) {
						TreeNode versionNode = new TreeNode (PublicFunctionClass.GetTabText (item), 4, 4);
						versionNode.Tag = item;
						versionNode.ToolTipText = PublicFunctionClass.GetTabToolTipText (item);
						viewNode.Nodes.Add (versionNode);
					} else {
						TreeNode versionNode = new TreeNode (PublicFunctionClass.GetTabText (item), 5, 5);
						versionNode.Tag = item;
						versionNode.ToolTipText = PublicFunctionClass.GetTabToolTipText (item);
						viewNode.Nodes.Add (versionNode);
					}
				}
			}
		}

		private static void InsertItemInfoNodes (TreeNode parentNode, TreeView tv)
		{
			if (parentNode == null) {
				var query = from ConfigFileInfo in PMSDBStructure.PMSCenterDataContext.s_CfgFileInfo
				            orderby ConfigFileInfo.Name, ConfigFileInfo.Version
				            where !((from imRelation in PMSDBStructure.PMSCenterDataContext.s_CfgFInfo_s_CfgFInfoMap_r
				                     select imRelation.FID).Distinct ()).Contains (ConfigFileInfo.FID)
				            group ConfigFileInfo by ConfigFileInfo.Name into g
				            select g;

				foreach (var gp in query) {
					TreeNode viewNode = new TreeNode (gp.Key, 0, 0);
					tv.Nodes.Add (viewNode);
					foreach (var item in gp) {
						//PMSDBStructure.PMSCenterDataContext.Refresh(System.Data.Entity.Core.Objects.RefreshMode.StoreWins, item);
						//do something
						if (item.Current_ == true) {
							TreeNode versionNode = new TreeNode (PublicFunctionClass.GetTabText (item), 4, 4);
							versionNode.Tag = item;
							versionNode.ToolTipText = PublicFunctionClass.GetTabToolTipText (item);
							viewNode.Nodes.Add (versionNode);
						} else {
							TreeNode versionNode = new TreeNode (PublicFunctionClass.GetTabText (item), 5, 5);
							versionNode.Tag = item;
							versionNode.ToolTipText = PublicFunctionClass.GetTabToolTipText (item);
							viewNode.Nodes.Add (versionNode);
						}
					}
				}
				return;
			}
			foreach (TreeNode treenode in parentNode.Nodes) {
				InsertItemInfoNodes (treenode, tv);
			}

			if ((Guid)((s_CfgFInfoMap)parentNode.Tag).MAPID == null)
				return;

			var q = from ConfigFileInfo in PMSDBStructure.PMSCenterDataContext.s_CfgFileInfo
			        orderby ConfigFileInfo.Name, ConfigFileInfo.Version
			        where
			                (
			                        from imRelation in PMSDBStructure.PMSCenterDataContext.s_CfgFInfo_s_CfgFInfoMap_r
			                        where imRelation.MAPID == (Guid)((s_CfgFInfoMap)parentNode.Tag).MAPID
			                        select imRelation.FID
			                ).Contains (ConfigFileInfo.FID)
			        group ConfigFileInfo by ConfigFileInfo.Name into g
			        select g;

			foreach (var gp in q) {
				TreeNode viewNode = new TreeNode (gp.Key, 0, 0);
				parentNode.Nodes.Add (viewNode);
				foreach (var item in gp) {
					//PMSDBStructure.PMSCenterDataContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, item);
					//do something
					if (item.Current_ == true) {
						TreeNode versionNode = new TreeNode (PublicFunctionClass.GetTabText (item), 4, 4);
						versionNode.Tag = item;
						versionNode.ToolTipText = PublicFunctionClass.GetTabToolTipText (item);
						viewNode.Nodes.Add (versionNode);
					} else {
						TreeNode versionNode = new TreeNode (PublicFunctionClass.GetTabText (item), 5, 5);
						versionNode.Tag = item;
						versionNode.ToolTipText = PublicFunctionClass.GetTabToolTipText (item);
						viewNode.Nodes.Add (versionNode);
					}
				}
			}
		}
	}

	public class RefDBInfo
	{
		private static List<PMSRefDBConnectionObj> refDbList {
			get { return CurrentPrjInfo.GetRefDBConnectionObjListFromLocalFile (); }
		}

		public static PMSRefDBConnectionObj GetRefDBConnectionObj (string refSourceName)
		{
			PMSRefDBConnectionObj defaultconnect = null;
			foreach (PMSRefDBConnectionObj obj in refDbList) {
				if (obj.StrName.CompareTo (refSourceName) == 0) {
					return obj;
				}
				if (obj.BDefault == true) {
					defaultconnect = obj;
				}
			}
			if (null != defaultconnect) {
				return defaultconnect;
			}
			string strError = string.Format (GetStringFromPublicResourceClass.GetStringFromPublicResource ("MES_RefDBNotFound"), refSourceName);
			PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error (strError);
			return null;
		}

		public static List<PMSRefDBConnectionObj> GetRefDBSources ()
		{
			return refDbList;
		}

		public static Type GetCSharpType (string VarType)
		{
			Type typeName = null;
			switch (VarType) {
			case "nvarchar":
				typeName = typeof(string);
				break;
			case "bit":
				typeName = typeof(bool);
				break;
			case "datetime":
				typeName = typeof(DateTime);
				break;
			case "float":
				typeName = typeof(float);
				break;
			case "decimal":
				typeName = typeof(decimal);
				break;
			case "uniqueidentifier":
				typeName = typeof(Guid);
				break;
			case "int":
				typeName = typeof(int);
				break;
			case "tinyint":
				typeName = typeof(byte);
				break;
			case "image":
				typeName = typeof(Image);
				break;
			default:
				typeName = typeof(string);
				break;
			}
			return typeName;
		}

		public static Type GetCSharpType (DbType VarType)
		{
			Type typeName = null;
			switch (VarType) {
			case DbType.AnsiString:
			case DbType.AnsiStringFixedLength:
				typeName = typeof(string);
				break;
			case DbType.Boolean:
				typeName = typeof(bool);
				break;
			case DbType.Date:
			case DbType.DateTime:
				typeName = typeof(DateTime);
				break;
			case DbType.Double:
				typeName = typeof(float);
				break;
			case DbType.Decimal:
				typeName = typeof(decimal);
				break;
			case DbType.Guid:
				typeName = typeof(Guid);
				break;
			case DbType.Int16:
			case DbType.Int32:
			case DbType.Int64:
				typeName = typeof(int);
				break;
			case DbType.SByte:
				typeName = typeof(byte);
				break;
			case DbType.Binary:
				typeName = typeof(Image);
				break;
			default:
				typeName = typeof(string);
				break;
			}
			return typeName;
		}

		public static DbType GetDbType (string VarType)
		{
			string strtype = VarType.ToLower ();
			DbType typeName = DbType.String;
			switch (strtype) {
			case "char":
			case "varchar2":
			case "nvarchar2":
			case "nvarchar":
			case "clob":
			case "nclob":
			case "system.string":
				typeName = DbType.String;
				break;
			case "bit":
				typeName = DbType.Boolean;
				break;
			case "date":
				typeName = DbType.Date;
				break;
			case "datetime":
			case "timestamp":
			case "system.datetime":
				typeName = DbType.DateTime;
				break;
			case "float":
				typeName = DbType.Double;
				break;
			case "raw":
			case "uniqueidentifier":
				typeName = DbType.Guid;
				break;
			case "int":
			case "system.int32":
				typeName = DbType.Int32;
				break;
			case "system.single":
				typeName = DbType.Single;
				break;
			case "system.int16":
				typeName = DbType.Int16;
				break;
			case "long":
			case "system.int64":
				typeName = DbType.Int64;
				break;
			case "tinyint":
				typeName = DbType.Byte;
				break;
			case "varbinary":
			case "system.byte[]":
			case "image":
			case "blob":
			case "system.drawing.image":
				typeName = DbType.Binary;
				break;
			case "number":
			case "system.decimal":
				typeName = DbType.Decimal;
				break;
			case "system.double":
				typeName = DbType.Double;
				break;
			case "system.guid":
				typeName = DbType.Guid;
				break;
                
			default:
				typeName = DbType.String;
				break;
			}
			return typeName;
		}

		public static bool IsTableExisted (string refSourceName, string tableName)
		{
			Dictionary<string, PMSRefDBTableProp> dic = GetDatabaseTables (refSourceName);
			return dic.ContainsKey (tableName);
		}

		public static Dictionary<string,PMSRefDBTableProp> GetDatabaseTables (string refSourceName)
		{
			PMSRefDBConnectionObj robj = GetRefDBConnectionObj (refSourceName);
			if (null != robj) {
				switch (robj.RefDBConnection.RefDBType) {
				case RefDBType.MSSqlServer:
					robj.RefDBConnection.GetSqlConnection ();
					return robj.RefDBConnection.Sa.GetDatabaseTables (robj.RefDBConnection);
				case RefDBType.MSAccess:
					AccessStructure.OleDbConn = robj.RefDBConnection.GetOleConnection ();
					return AccessStructure.GetDatabaseTables (robj.RefDBConnection);
				case RefDBType.Oracle:
					return OracleStructure.GetDatabaseTables (robj.RefDBConnection);
				case RefDBType.OleDB:
					new OleDBStructure (robj.RefDBConnection.ConnectString);
					OleDBStructure.OleDbConn = robj.RefDBConnection.GetOleConnection ();
					return OleDBStructure.GetDatabaseTables (robj.RefDBConnection);
				default:
					return null;
				}
			}
			return null;
		}

		public static Dictionary<string, PMSRefDBViewProp> GetDatabaseViews (string refSourceName)
		{
			PMSRefDBConnectionObj robj = GetRefDBConnectionObj (refSourceName);
			if (null != robj) {
				switch (robj.RefDBConnection.RefDBType) {
				case RefDBType.MSSqlServer:
					robj.RefDBConnection.GetSqlConnection ();
					return robj.RefDBConnection.Sa.GetDatabaseViews (robj.RefDBConnection);
				case RefDBType.MSAccess:
					return null;
				//AccessStructure.OleDbConn = robj.RefDBConnection.GetOleConnection();
				//return AccessStructure.GetDatabaseViews(robj.RefDBConnection);
				case RefDBType.Oracle:
					return OracleStructure.GetDatabaseViews (robj.RefDBConnection);
				case RefDBType.OleDB:
					return null;
				//new OleDBStructure(robj.RefDBConnection.ConnectString);
				//OleDBStructure.OleDbConn = robj.RefDBConnection.GetOleConnection();
				//return OleDBStructure.GetDatabaseViews(robj.RefDBConnection);
				default:
					return null;
				}
			}
			return null;
		}

		public static Dictionary<string, PMSRefDBFieldProp> GetTableColumns (string refSourceName, string tableName)
		{
			PMSRefDBConnectionObj robj = GetRefDBConnectionObj (refSourceName);
			if (null != robj) {
				switch (robj.RefDBConnection.RefDBType) {
				case RefDBType.MSSqlServer:
					robj.RefDBConnection.GetSqlConnection ();
					return robj.RefDBConnection.Sa.GetRefTableColumns (tableName, robj.RefDBConnection);
				case RefDBType.MSAccess:
					AccessStructure.OleDbConn = robj.RefDBConnection.GetOleConnection ();
					return AccessStructure.GetTableColumns (tableName, robj.RefDBConnection);
				case RefDBType.Oracle:
					return OracleStructure.GetTableColumns (tableName, robj.RefDBConnection);
				case RefDBType.OleDB:
					new OleDBStructure (robj.RefDBConnection.ConnectString);
					OleDBStructure.OleDbConn = robj.RefDBConnection.GetOleConnection ();
					return OleDBStructure.GetTableColumns (tableName, robj.RefDBConnection);
				default:
					return null;
				}
			}
			return null;
		}

		public static Dictionary<string, PMSRefDBFieldProp> GetViewColumns (string refSourceName, string viewName)
		{
			PMSRefDBConnectionObj robj = GetRefDBConnectionObj (refSourceName);
			if (null != robj) {
				switch (robj.RefDBConnection.RefDBType) {
				case RefDBType.MSSqlServer:
					robj.RefDBConnection.GetSqlConnection ();
					return robj.RefDBConnection.Sa.GetRefViewColumns (viewName, robj.RefDBConnection);
				case RefDBType.MSAccess:
					return null;
					AccessStructure.OleDbConn = robj.RefDBConnection.GetOleConnection ();
					return AccessStructure.GetTableColumns (viewName, robj.RefDBConnection);
				case RefDBType.Oracle:
					return null;
				case RefDBType.OleDB:
					return null;
					new OleDBStructure (robj.RefDBConnection.ConnectString);
					OleDBStructure.OleDbConn = robj.RefDBConnection.GetOleConnection ();
					return OleDBStructure.GetTableColumns (viewName, robj.RefDBConnection);
				default:
					return null;
				}
			}
			return null;
		}
	}

	public class SystemDefinedString
	{
		public static string strElementHostName = "d83152f4-d3da-4a56-a31e-4eaef9db6e9a";
	}

	/// <summary>
	/// PMS 系统信息
	/// </summary>
	public class SystemInfo
	{
		// 生产管理系统保留系统管理员组ID
		public static string PMSAdministrators = "PMS Administrators";

		// 生产管理系统保留系统管理员ID
		public static string PMSAdmin = "admin";
	}

	/// <summary>
	/// 根据文化Culture读取全局字符串，多语言版本适用
	/// </summary>
	public class GetStringFromPublicResourceClass
	{
		private static CultureInfo ci = null;

		public static string GetStringFromPublicResource (string strName)
		{ 
			// Method implementation.
			PMSCultureInfo cultureInfo = new PMSCultureInfo ();
			if (null == ci)
				ci = cultureInfo.GetPMSCultureinfo ();
			ResourceManager rm = new ResourceManager ("PMS.Libraries.ToolControls.PMSPublicInfo.PMSPublicResource", typeof(PMSPublicResource).Assembly);
			if (ci.ToString ().CompareTo ("zh-CN") == 0)
				ci = CultureInfo.CreateSpecificCulture ("");
			return rm.GetString (strName, ci);
		}
	}

	public class PublicObject
	{
		public static PMSTaskbarNotifier taskbarNotifier = new PMSTaskbarNotifier ();

		/// <summary>
		/// PMS.WinFormsUI中DragControl.FindForm()为空时使用此值
		/// </summary>
		public static IntPtr MainHandle = IntPtr.Zero;

		//public static object RvFileObj = null;
	}

	/// <summary>
	/// 全局公有函数
	/// </summary>
	public class PublicFunctionClass
	{
		public const string ReportFilePostfix = ".rpt";
		public const string SheetFilePostfix = ".sht";
		public const string StaticFilePostfix = ".stc";
		public const string ScriptFilePostfix = ".sco";
		public const string CustomFilePostfix = ".csf";
		public const string OrgFilePostfix = ".org";
		public const string ScriptCompiledFilePostfix = ".dll";
		public const string TemplateFilePostfix = ".tmp";
		public const string ReportVarFilePostfix = ".rv";
		public const string FormFilePostfix = ".frm";
		public const string NavigatorFilePostfix = ".nav";

		public static string[] KeywordArray = {
			"abstract",
			"as",
			"base",
			"bool",
			"break",
			"byte",
			"case",
			"catch",
			"char",
			"checked",
			"class",
			"const",
			"continue",
			"decimal",
			"default",
			"delegate",
			"do",
			"double",
			"else",
			"enum",
			"event",
			"explicit",
			"extern",
			"false",
			"finally",
			"fixed",
			"float",
			"for",
			"foreach",
			"goto",
			"if",
			"implicit",
			"in",
			"int",
			"interface",
			"internal",
			"is",
			"lock",
			"long",
			"namespace",
			"new",
			"null",
			"object",
			"operator",
			"out",
			"override",
			"params",
			"private",
			"protected",
			"public",
			"readonly",
			"ref",
			"return",
			"sbyte",
			"sealed",
			"short",
			"sizeof",
			"stackalloc",
			"static",
			"string",
			"struct",
			"switch",
			"this",
			"throw",
			"true",
			"try",
			"typeof",
			"uint",
			"ulong",
			"unchecked",
			"unsafe",
			"ushort",
			"using",
			"virtual",
			"void",
			"volatile",
			"while",
			"add",
			"alias",
			"ascending",
			"async",
			"await",
			"descending",
			"dynamic",
			"源",
			"get",
			"global",
			"group",
			"into",
			"join",
			"let",
			"orderby",
			"partial",
			"partial",
			"remove",
			"select",
			"set",
			"value",
			"var",
			"where",
			"where",
			"yield",                         
		};

		public static bool IsChildOfTreeNode (TreeNode ChileNode, TreeNode RootNode)
		{
			TreeNode tNode = ChileNode;
			while (tNode != null) {
				if (tNode.Parent == RootNode)
					return true;
				else
					tNode = tNode.Parent;
			}
			return false;
		}

		public static System.Net.IPAddress GetLocalIPAddress ()
		{
			System.Net.IPAddress[] ips = System.Net.Dns.GetHostByName (System.Net.Dns.GetHostName ()).AddressList;
			if (ips.Length > 0)
				return ips [0];
			return System.Net.IPAddress.None;
		}

		public static System.Net.IPAddress[] GetIPAddresses ()
		{
			NetworkInterface[] NetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces ();
			return null;
		}

		/// <summary>
		/// Trace Error Info
		/// </summary>
		/// <param name="ex"></param>
		/// <returns></returns>
		public static string EnhancedStackTrace (Exception ex)
		{
			string strTrace = ex.Message;
			strTrace += EnhancedStackTrace (new System.Diagnostics.StackTrace (ex, true));
			PMSFileClass.AddToTraceFile (strTrace);
			return strTrace;
		}

		private static string EnhancedStackTrace (System.Diagnostics.StackTrace st)
		{
			StringBuilder sb = new StringBuilder ();
			sb.Append (Environment.NewLine);
			sb.Append ("---- Trace Time ----");
			sb.Append (Environment.NewLine);
			sb.Append (System.DateTime.Now.ToString ());
			sb.Append (Environment.NewLine);
			sb.Append ("---- Trace User ----");
			sb.Append (Environment.NewLine);
			sb.Append (CurrentPrjInfo.CurrentLoginUserID);
			sb.Append (Environment.NewLine);
			sb.Append ("---- Trace Source ----");
			sb.Append (Environment.NewLine);
			sb.Append (CurrentPrjInfo.LoacalIPAddress.ToString ());
			sb.Append (Environment.NewLine);
			sb.Append ("---- Stack Trace ----");
			sb.Append (Environment.NewLine);
			for (int i = 0; i < st.FrameCount; i++) {
				System.Diagnostics.StackFrame sf = st.GetFrame (i);
				System.Reflection.MemberInfo mi = sf.GetMethod ();
				sb.Append (StackFrameToString (sf));
			}
			sb.Append (Environment.NewLine);
			return sb.ToString ();
		}

		private static string StackFrameToString (System.Diagnostics.StackFrame sf)
		{
			StringBuilder sb = new StringBuilder ();
			sb.Append (Environment.NewLine);
			sb.Append ("---- Trace File ----");
			sb.Append (Environment.NewLine);
			sb.Append (sf.GetFileName ());
			sb.Append (Environment.NewLine);
			sb.Append ("---- Trace Method ----");
			sb.Append (Environment.NewLine);
			sb.Append (sf.GetMethod ().Name);
			sb.Append (Environment.NewLine);
			sb.Append ("---- Trace Line Number ----");
			sb.Append (Environment.NewLine);
			sb.Append (sf.GetFileLineNumber ());
			sb.Append (Environment.NewLine);
			sb.Append ("---- Trace Column Number ----");
			sb.Append (Environment.NewLine);
			sb.Append (sf.GetFileColumnNumber ());
			sb.Append (Environment.NewLine);

			int intParam;
			System.Reflection.MemberInfo mi = sf.GetMethod ();
			sb.Append ("   ");
			sb.Append (mi.DeclaringType.Namespace);
			sb.Append (".");
			sb.Append (mi.DeclaringType.Name);
			sb.Append (".");
			sb.Append (mi.Name);
			// -- build method params            
			sb.Append ("(");
			intParam = 0;
			foreach (System.Reflection.ParameterInfo param in sf.GetMethod().GetParameters()) {
				intParam += 1;
				sb.Append (param.ParameterType.Name);
				sb.Append (" ");
				sb.Append (param.Name);
				sb.Append (", ");
			}
			if (intParam > 0)
				sb.Remove (sb.Length - 2, 2);
			sb.Append (")");
			sb.Append (Environment.NewLine);
			// -- if source code is available, append location info            
			sb.Append ("       ");
			if (string.IsNullOrEmpty (sf.GetFileName ())) {
				sb.Append ("(unknown file)");
				//-- native code offset is always available                
				sb.Append (": N ");
				sb.Append (String.Format ("{0:#00000}", sf.GetNativeOffset ()));
			} else {
				sb.Append (System.IO.Path.GetFileName (sf.GetFileName ()));
				sb.Append (": line ");
				sb.Append (String.Format ("{0:#0000}", sf.GetFileLineNumber ()));
				sb.Append (", col ");
				sb.Append (String.Format ("{0:#00}", sf.GetFileColumnNumber ()));
				if (sf.GetILOffset () != System.Diagnostics.StackFrame.OFFSET_UNKNOWN) {
					sb.Append (", IL ");
					sb.Append (String.Format ("{0:#0000}", sf.GetILOffset ()));
				}
			}
			sb.Append (Environment.NewLine);
			return sb.ToString ();
		}

		public static string CodeRunningPosition ()
		{
			//创建当前跟踪堆栈
			System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace ();
			//从堆栈中获得一帧运行信息，1代表上一个函数调用帧，如果是0的话，代表是当前方法调用帧（CodeRunningPosition）
			MethodBase mb = st.GetFrame (1).GetMethod ();
			//将获得的方法信息拼成类名.方法名这种形式，ReflectedType为方法所在的类名
			return "{" + mb.ReflectedType.FullName + "." + mb.Name + "[" + mb.Module.Name + "]}:";
		}

		public static string CodeRunningPosition2 ()
		{
			//创建当前跟踪堆栈
			System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace ();
			//从堆栈中获得一帧运行信息，1代表上一个函数调用帧，如果是0的话，代表是当前方法调用帧（CodeRunningPosition）
			MethodBase mb = st.GetFrame (2).GetMethod ();
			//将获得的方法信息拼成类名.方法名这种形式，ReflectedType为方法所在的类名
			return "{" + mb.ReflectedType.FullName + "." + mb.Name + "[" + mb.Module.Name + "]}:";
		}

		public static RefDBType GetRefDBType (int itype)
		{
			switch (itype) {
			case 0:
				return RefDBType.MSAccess;
			case 1:
				return RefDBType.MSSqlServer;
			case 2:
				return RefDBType.Oracle;
			case 3:
				return RefDBType.OleDB;
			default:
				break;
			}
			return RefDBType.MSSqlServer;
		}

		public static ConnectType GetConnectType (string strProtocol)
		{
			switch (strProtocol) {
			case "namepipe":
				return ConnectType.namepipe;
			case "tcpip":
				return ConnectType.tcpip;
			default:
				break;
			}
			return ConnectType.namepipe;
		}

		public static void SetCurrentDocumentModified ()
		{
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_DOCMODIFIED;
			IntPtr handle = PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle;
			PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (handle, msgID, IntPtr.Zero, IntPtr.Zero);
		}

		public static string SetDocumentModified (string docName)
		{
			if (GetDocumentModified (docName))
				return docName;
			return string.Format ("{0}{1}", docName, "*");
		}

		public static bool GetDocumentModified (string docName)
		{
			if (docName.EndsWith ("*"))
				return true;
			return false;
		}

		public static string GetDocumentName (string TabName)
		{
			return TabName.TrimEnd ('*');
		}

		public static string GetTabText (s_CfgFileInfo ConfigFileInfo)
		{
			if (ConfigFileInfo == null)
				return null;
			return string.Format (GetStringFromPublicResourceClass.GetStringFromPublicResource ("PMSUserViewNavigateBarControl_FormatTabText"), ConfigFileInfo.Name, ConfigFileInfo.Version);
		}

		public static string GetNewTabText (string fileName)
		{
			if (fileName == null)
				return null;
			return string.Format (GetStringFromPublicResourceClass.GetStringFromPublicResource ("PMSUserViewNavigateBarControl_FormatTabText"), fileName, 0);
		}

		public static string GetTabText (string fileName, int version)
		{
			if (fileName == null)
				return null;
			return string.Format (GetStringFromPublicResourceClass.GetStringFromPublicResource ("PMSUserViewNavigateBarControl_FormatTabText"), fileName, version);
		}

		public static string GetTabToolTipText (s_CfgFileInfo ConfigFileInfo)
		{
			if (ConfigFileInfo == null)
				return null;
			string strF = GetStringFromPublicResourceClass.GetStringFromPublicResource ("PMSUserViewNavigateBarControl_FormatTabToolTipText");
			return string.Format (strF/*"视图ID:[{0}]\r\n视图名:[{1}]\r\n描述:[{2}]\r\n版本号:[{3}]\r\n签出状态:[{4}]\r\n活动状态:[{5}]\r\n签出用户:[{6}]\r\n最后修改时间:[{7}]"*/,
				ConfigFileInfo.FID,
				ConfigFileInfo.Name,
				ConfigFileInfo.Description,
				ConfigFileInfo.Version,
				ConfigFileInfo.Check_,
				ConfigFileInfo.Current_,
				ConfigFileInfo.CheckUserID,
				ConfigFileInfo.TimeStamp);
		}

		public static string GetTabToolTipText (s_CfgFTplInfo ConfigFileTemplateInfo)
		{
			if (ConfigFileTemplateInfo == null)
				return null;
			string strF = GetStringFromPublicResourceClass.GetStringFromPublicResource ("PMSUserViewNavigateBarControl_TemplateFormatTabToolTipText");
			return string.Format (strF/*"视图ID:[{0}]\r\n视图名:[{1}]\r\n描述:[{2}]\r\n签出状态:[{3}]\r\n签出用户:[{4}]\r\n最后修改时间:[{5}]"*/,
				ConfigFileTemplateInfo.TID,
				ConfigFileTemplateInfo.Name,
				ConfigFileTemplateInfo.Description,
				ConfigFileTemplateInfo.Check_,
				ConfigFileTemplateInfo.CheckUserID,
				ConfigFileTemplateInfo.TimeStamp);
		}

		public static string GetTestRunTimeTabToolTipText (s_CfgFileInfo ConfigFileInfo)
		{
			if (ConfigFileInfo == null)
				return null;
			string strF = GetStringFromPublicResourceClass.GetStringFromPublicResource ("PMSUserViewNavigateBarControl_RuntimeFormatTabToolTipText");
			return string.Format (strF/*"运行时\r\n视图ID:[{0}]\r\n视图名:[{1}]\r\n描述:[{2}]\r\n版本号:[{3}]\r\n签出状态:[{4}]\r\n活动状态:[{5}]\r\n签出用户:[{6}]\r\n最后修改时间:[{7}]"*/,
				ConfigFileInfo.FID,
				ConfigFileInfo.Name,
				ConfigFileInfo.Description,
				ConfigFileInfo.Version,
				ConfigFileInfo.Check_,
				ConfigFileInfo.Current_,
				ConfigFileInfo.CheckUserID,
				ConfigFileInfo.TimeStamp);
		}

		public static string GetViewFilePath (s_CfgFileInfo ConfigFileInfo)
		{
			string strFilePath = Path.Combine (PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.UserCustomPath, ConfigFileInfo.FID.ToString () + Path.GetExtension (ConfigFileInfo.Name));
			return strFilePath;
		}

		public static DocType GetDocTypeFromTypeInt (int iType)
		{
			DocType docType = DocType.Custom;
			switch (iType) {
			case 0:
				docType = DocType.Custom;
				break;
			case 1:
				docType = DocType.ReportDesigner;
				break;
			case 2:
				docType = DocType.FormDesigner;
				break;
			case 3:
				docType = DocType.NavigatorDesigner;
				break;
			//case 4: docType = DocType.MesSheet;
			//    break;
			default:
				break;
			}
			return docType;
		}

		public static byte GetByteTypeFromDocType (DocType docType)
		{
			byte type = 0;
			switch (docType) {
			case DocType.All:
				type = byte.MaxValue;
				break;
			case DocType.Custom:
				type = 0;
				break;
			case DocType.ReportDesigner:
				type = 1;
				break;
			case DocType.FormDesigner:
				type = 2;
				break;
			case DocType.NavigatorDesigner:
				type = 3;
				break;
			//case DocType.MesSheet: type = 4;
			//    break;
			default:
				type = 0;
				break;
			}
			return type;
		}

		public static byte GetByteTypeFromPostfix (string postfix)
		{
			return GetByteTypeFromDocType (GetDocTypeFromPostfix (postfix));
		}

		public static DocType GetDocTypeFromPostfix (string postfix)
		{
			switch (postfix) {
			case PMS.Libraries.ToolControls.PMSPublicInfo.PublicFunctionClass.CustomFilePostfix:
				return DocType.Custom;
			case PMS.Libraries.ToolControls.PMSPublicInfo.PublicFunctionClass.ReportFilePostfix:
				return DocType.ReportDesigner;
			case PMS.Libraries.ToolControls.PMSPublicInfo.PublicFunctionClass.FormFilePostfix:
				return DocType.FormDesigner;
			case PMS.Libraries.ToolControls.PMSPublicInfo.PublicFunctionClass.NavigatorFilePostfix:
				return DocType.NavigatorDesigner;
			}
			return DocType.All;
		}

		public static string GetTypeFilePath (DocType docType)
		{
			string typeFilePath = string.Empty;
			switch (docType) {
			case DocType.Report:
				typeFilePath = PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.ReportPath;
				break;
			case DocType.Sheet:
				typeFilePath = PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.SheetPath;
				break;
			case DocType.Static:
				typeFilePath = PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.StaticPath;
				break;
			case DocType.Custom:
			case DocType.ReportDesigner:
			case DocType.FormDesigner:
			case DocType.MesSheet:
				typeFilePath = PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.UserCustomPath;
				break;
			case DocType.OrgnizationStruct:
				typeFilePath = PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.OrgPath;
				break;
			case DocType.Template:
				typeFilePath = PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.TemplatePath;
				break;
			default:
				typeFilePath = PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.UserCustomPath;
				break;
			}

			return typeFilePath;
		}

		public static string GetTypeFilePostfix (DocType docType)
		{
			string Postfix = string.Empty;
			switch (docType) {
			case DocType.Report:
				Postfix = ReportFilePostfix;
				break;
			case DocType.Sheet:
				Postfix = SheetFilePostfix;
				break;
			case DocType.Static:
				Postfix = StaticFilePostfix;
				break;
			case DocType.Custom:
				Postfix = CustomFilePostfix;
				break;
			case DocType.ReportDesigner:
				Postfix = ReportFilePostfix;
				break;
			case DocType.FormDesigner:
				Postfix = FormFilePostfix;
				break;
			case DocType.MesSheet:
				Postfix = CustomFilePostfix;
				break;
			case DocType.OrgnizationStruct:
				Postfix = OrgFilePostfix;
				break;
			case DocType.NavigatorDesigner:
				Postfix = NavigatorFilePostfix;
				break;
			default:
				Postfix = CustomFilePostfix;
				break;
			}
			return Postfix;
		}

		public static string GetTypeFileRelativePath (DocType docType)
		{
			string relativePath = string.Empty;
			switch (docType) {
			case DocType.Report:
				relativePath = "Report" + System.IO.Path.DirectorySeparatorChar;
				break;
			case DocType.Sheet:
				relativePath = "Sheet" + System.IO.Path.DirectorySeparatorChar;
				break;
			case DocType.Static:
				relativePath = "Stati" + System.IO.Path.DirectorySeparatorChar;
				break;
			case DocType.Custom:
			case DocType.ReportDesigner:
			case DocType.FormDesigner:
			case DocType.MesSheet:
			case DocType.TestRunTime:
			case DocType.RunTime:
				relativePath = "UserCustom" + System.IO.Path.DirectorySeparatorChar;
				break;
			case DocType.OrgnizationStruct:
				relativePath = "Org" + System.IO.Path.DirectorySeparatorChar;
				break;
			default: 
				relativePath = "UserCustom" + System.IO.Path.DirectorySeparatorChar;
				break;
			}
			return relativePath;
		}

		public static CfgType GetCfgType (s_ServerCfgFiles cfg)
		{
			if (cfg.FID == PMS.Libraries.ToolControls.PMSPublicInfo.ServerConfigFileGuid.RefDBSourcesFileConfig)
				return CfgType.RefDBSource;
			else if (cfg.FID == PMS.Libraries.ToolControls.PMSPublicInfo.ServerConfigFileGuid.MapTableFileConfig)
				return CfgType.MapTable;
			else if (cfg.FID == PMS.Libraries.ToolControls.PMSPublicInfo.ServerConfigFileGuid.CustomMsgConfig)
				return CfgType.CustomMsg;
			else if (cfg.FID == PMS.Libraries.ToolControls.PMSPublicInfo.ServerConfigFileGuid.SystemStructConfig)
				return CfgType.SystemStruct;
             
			return CfgType.None;
		}

		public static Form GetForm (IntPtr handle)
		{
			return handle == IntPtr.Zero ?
                null :
                Control.FromHandle (handle) as Form;
		}

		public static Control GetControl (IntPtr handle)
		{
			return handle == IntPtr.Zero ?
                null :
                Control.FromHandle (handle);
		}

		/// <summary>
		/// 根据字符串（type的name或者fullname）获取C#类型
		/// 不支持的类型返回object类型
		/// </summary>
		/// <param name="?"></param>
		/// <returns></returns>
		public static Type GetTypeFormString (string type)
		{
			Type result = typeof(string);
			switch (type.ToLower ().Trim ()) {
			case "int32":
			case "system.int32":
				result = typeof(System.Int32);
				break;
			case "int16":
			case "system.int16":
				result = typeof(System.Int16);
				break;
			case "int64":
			case "system.int64":
				result = typeof(System.Int64);
				break;
			case "uint32":
			case "system.uint32":
				result = typeof(System.UInt32);
				break;
			case "uint16":
			case "system.uint16":
				result = typeof(System.UInt16);
				break;
			case "uint64":
			case "system.uint64":
				result = typeof(System.UInt64);
				break;
			case "string":
			case "system.string":
				result = typeof(System.String);
				break;
			case "bool":
			case "system.bool":
				result = typeof(bool);
				break;
			case "boolean":
			case "system.boolean":
				result = typeof(Boolean);
				break;
			case "float":
			case "system.float":
				result = typeof(float);
				break;
			case "byte":
			case "system.byte":
				result = typeof(Byte);
				break;
			case "int":
			case "system.int":
				result = typeof(int);
				break;
			case "datetime":
			case "system.datetime":
				result = typeof(DateTime);
				break;
			case "byte[]":
			case "system.byte[]":
				result = typeof(Byte[]);
				break;
			case "guid":
			case "system.guid":
				result = typeof(Guid);
				break;
			case "decimal":
			case "system.decimal":
				result = typeof(Decimal);
				break;
			case "double":
			case "system.double":
				result = typeof(Double);
				break;
			case "object":
			case "system.object":
				result = typeof(object);
				break;
			case "single":
			case "system.single":
				result = typeof(Single);
				break;
			case "char":
			case "system.char":
				result = typeof(Char);
				break;
			case "sbyte":
			case "system.sbyte":
				result = typeof(System.SByte);
				break;
			default:
				result = typeof(object);
				break;
			}
			return result;
		}

		public static bool HasEventSubscribed (Delegate delg, EventHandler desteh)
		{
			Delegate[] delDirectory = delg.GetInvocationList ();
			if (delDirectory != null && delDirectory.Length > 0) {
				foreach (EventHandler handle in delDirectory) {
					if (handle == desteh) {
						//已注册
						return true;
					} else {
						//未注册
						return false;
					}
				}
			}
            
			return false;
		}

		public static bool HasEventSubscribed (Control subject, string eventName, string delegateName)
		{
			PropertyInfo pi = (subject.GetType ()).GetProperty ("Events", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
			EventHandlerList ehl = (EventHandlerList)pi.GetValue (subject, null);//subject
			FieldInfo fi = (typeof(Control)).GetField (eventName, BindingFlags.Static | BindingFlags.NonPublic);
			Delegate d = ehl [fi.GetValue (null)];
			if (d != null) {
				System.Delegate[] dels = d.GetInvocationList ();
				for (int i = 0; i < dels.Length; i++) {
					if (dels [i].Method.Name == delegateName)
						return true;
				}
			}
			return false;
		}

		public static int GetEventSubscribedCount (Control subject, string eventName)
		{
			PropertyInfo pi = (subject.GetType ()).GetProperty ("Events", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
			EventHandlerList ehl = (EventHandlerList)pi.GetValue (subject, null);//subject
			FieldInfo fi = (typeof(Control)).GetField (eventName, BindingFlags.Static | BindingFlags.NonPublic);
			Delegate d = ehl [fi.GetValue (null)];
			if (d != null) {
				System.Delegate[] dels = d.GetInvocationList ();
				if (null != dels)
					return dels.Count ();
			}
			return 0;
		}

		/// <summary>
		/// 获取像素值对应的厘米尺寸
		/// </summary> 
		/// <returns></returns>
		public static float ConvertPixelToCentimeter (float size, float Dpi = 96.0f)
		{
			if (Dpi != 0.0f) {
				return (float)((size / Dpi) * 2.54);
			}
			return size;
		}

		/// <summary>
		/// 获取厘米尺寸对应的像素值
		/// </summary> 
		/// <returns></returns>
		public static float ConvertCentimeterToPixel (float size, float Dpi = 96.0f)
		{
			return (float)((size / 2.54) * Dpi);
		}

		public static string GetRandomId ()
		{
			byte[] buffer = Guid.NewGuid ().ToByteArray ();
			return Math.Abs (BitConverter.ToInt64 (buffer, 0)).ToString ();
		}

		public static bool IsKeyword (string str)
		{
			string key = str.ToLower ();
			if (KeywordArray.Contains (key))
				return true;
			return false;
		}

		public static bool IsVarNameValidate (string str)
		{
			try {
				Regex regex = new Regex ("^[a-zA-Z_][a-zA-Z0-9_]*$");
				return regex.IsMatch (str);
			} catch {
				return false;
			}
		}

		public static bool IsValidIdentifier (string name)
		{
			System.CodeDom.Compiler.CodeDomProvider comp = System.CodeDom.Compiler.CodeDomProvider.CreateProvider ("CSharp");
			return comp.IsValidIdentifier (name);
		}

		public static string GetValidVarName (string name)
		{
			if (!IsValidIdentifier (name)) {
				string[] arr = name.Split (
					               new char[] { 
						(char)32, (char)33, (char)34, (char)35, (char)36, (char)37, (char)38, (char)39, (char)40, (char)41, (char)42, (char)43, (char)44, (char)45, (char)46, (char)47,
						(char)58, (char)59, (char)60, (char)61, (char)62, (char)63, (char)64,
						(char)92, (char)93, (char)94, 
						(char)96,
						(char)123, (char)124, (char)125, (char)126, (char)127
					}
				               );
				string ret = string.Empty;
				foreach (string s in arr) {
					ret += s;
				}
				return ret;
			} else
				return name;
		}

		public static bool InitPluginInstallMode ()
		{
			try {
				string ModulePath = Path.GetDirectoryName (Process.GetCurrentProcess ().MainModule.FileName);
				string iniPath = Path.Combine (ModulePath, PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.SetupIniName);
				// 读取安装包Setup版本号
				PMS.Libraries.ToolControls.PMSPublicInfo.INIClass ini = new PMS.Libraries.ToolControls.PMSPublicInfo.INIClass (iniPath);
				if (ini.ExistINIFile ()) {
					string strIsPlugin = ini.IniReadValue ("SoftInfo", "IsPlugin");
					PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.IsPlugin = strIsPlugin == "1";
					string strInstallMode = ini.IniReadValue ("SoftInfo", "InstallMode");
					int iInstallMode = int.Parse (strInstallMode);
					PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentInstallMode = (PMS.Libraries.ToolControls.PMSPublicInfo.InstallMode)iInstallMode;

					PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.ProjectPath = ModulePath;
					switch (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentInstallMode) {
					case InstallMode.MESBroadcastClient_NSPlugin:
						PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.CreateBroadcastClientDirectory ();
						break;
					default:
						break;
					}
                    
					return true;
				}
			} catch (Exception ex) {
				PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.IsPlugin = true;
				PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentInstallMode = PMS.Libraries.ToolControls.PMSPublicInfo.InstallMode.MESReportDesigner_NSPlugin;
				PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error (ex.Message);
			}
			return false;
		}

		public static void RestartApplication ()
		{
			Process.Start (Application.ExecutablePath);
			Environment.Exit (0);
		}
	}

	/// <summary>
	/// 文件相关
	/// </summary>
	public class PMSFileClass
	{
		[Serializable]
		public class PvfFileObjClass
		{
			public Guid _guid;
			public System.Nullable<byte> _type;
			public System.Nullable<System.Guid> _modelid;
			public byte[] _content;
		}

		public static bool AddToTextFile (string filePath, string strText)
		{
			try {
				if (filePath == null)
					return false;
				if (!File.Exists (filePath)) {
					System.IO.Directory.CreateDirectory (PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.TraceLogPath);
				}
				StreamWriter sw = new StreamWriter (filePath, true, Encoding.UTF8);
				sw.Write (strText + Environment.NewLine);
				sw.Close ();
				sw.Dispose ();

				FileInfo fi = new FileInfo (filePath);
				if (fi.Length > 2 * 1024 * 1024) {
					string strFileName = Path.GetFileNameWithoutExtension (filePath);
					string strFilePath = Path.GetDirectoryName (filePath);
					string strNewFullPath = string.Format ("{0}_{1}.{2}", Path.Combine (strFilePath, strFileName), string.Format ("{0:yyyyMMddHHmmss}", DateTime.Now), ProjectPathClass.TraceLogFileExtName);
					//if (File.Exists(ProjectPathClass.TraceLogFileBackupPath))
					//    File.Delete(ProjectPathClass.TraceLogFileBackupPath);
					//File.Move(filePath, ProjectPathClass.TraceLogFileBackupPath);
					if (string.Compare (filePath, strNewFullPath, true) != 0) {
						if (File.Exists (strNewFullPath))
							File.Delete (strNewFullPath);
						File.Move (filePath, strNewFullPath);
					}
				}
			} catch (IOException ex) {
				//MessageBox.Show(ex.Message, "AddToTextFile",
				//   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
			return true;
		}

		public static bool AddToTraceFile (string strTrace)
		{
			return AddToTextFile (ProjectPathClass.TraceLogFilePath, strTrace);
		}

		public static bool SaveFile (string filePath, object ob)
		{
			try {
				if (filePath == null)
					return false;
				using (FileStream fs = new FileStream (filePath, FileMode.OpenOrCreate, FileAccess.Write)) {
					BinaryFormatter formatter = new BinaryFormatter ();
					//formatter.TypeFormat = System.Runtime.Serialization.Formatters.FormatterTypeStyle.TypesWhenNeeded;
					//formatter.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
					formatter.Serialize (fs, ob);
					fs.Close ();
					fs.Dispose ();
				}
			} catch (IOException ex) {
				MessageBox.Show (ex.Message, "SaveFile",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
			return true;
		}

		//public static bool SaveCompressFile(string filePath, object ob)
		//{
		//    try
		//    {
		//        if (filePath == null)
		//            return false;
		//        using (MemoryStream fs = new MemoryStream())
		//        {
		//            BinaryFormatter formatter = new BinaryFormatter();
		//            formatter.TypeFormat = System.Runtime.Serialization.Formatters.FormatterTypeStyle.TypesWhenNeeded;
		//            formatter.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
		//            formatter.Serialize(fs, ob);
		//            System.IO.Compression.GZipStream gzs = new System.IO.Compression.GZipStream(fs, System.IO.Compression.CompressionMode.Compress);
                    
		//            fs.Close();
		//        }
		//    }
		//    catch (IOException ex)
		//    {
		//        MessageBox.Show(ex.Message, "SaveFile",
		//           MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		//        return false;
		//    }
		//    return true;
		//}

		public static MemoryStream SaveAsMemoryStream (object ob)
		{
			try {
				MemoryStream ms = new MemoryStream ();
				BinaryFormatter formatter = new BinaryFormatter ();
				formatter.Serialize (ms, ob);
				ms.Close ();
				ms.Dispose ();
			} catch (IOException ex) {
				MessageBox.Show (ex.Message, "SaveFile",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return null;
		}

		public static bool SaveFile (string filePath, byte[] data)
		{
			try {
				System.IO.File.WriteAllBytes (filePath, data);
			} catch (IOException ex) {
				MessageBox.Show (ex.Message, "SaveFile(string filePath, byte[] data)",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
			return true;
		}

		public static bool LoadFile (string filePath, ref object ob, SerializationBinder binder = null)
		{
			try {
				if (!System.IO.File.Exists (filePath))
					return false;
				using (FileStream fs = new FileStream (filePath, FileMode.Open, FileAccess.Read)) {
					BinaryFormatter formatter = new BinaryFormatter ();
					if (null != binder) {
						formatter.Binder = binder;
					}
					ob = formatter.Deserialize (fs);//在这里要注意,返回值是object
					fs.Close ();
					fs.Dispose ();
				}
			} catch (IOException ex) {
				MessageBox.Show (ex.Message, "LoadFile",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
			return true;
		}

		public static bool LoadFile (string filePath, ref FileStream stream)
		{
			try {
				if (!System.IO.File.Exists (filePath))
					return false;
				using (FileStream fs = new FileStream (filePath, FileMode.Open, FileAccess.Read)) {
					fs.CopyTo (stream);
					fs.Close ();
					fs.Dispose ();
				}
			} catch (IOException ex) {
				MessageBox.Show (ex.Message, "LoadFile",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
			return true;
		}

		public static bool LoadPublicKeyTokenFile (string filePath, ref object ob)
		{
			try {
				if (!System.IO.File.Exists (filePath))
					return false;
				using (FileStream fs = new FileStream (filePath, FileMode.Open, FileAccess.Read)) {
					byte[] temp = new byte[fs.Length];
					fs.Read (temp, 0, temp.Length);
					fs.Close ();
					fs.Dispose ();
					for (int i = 0; i + 31 < temp.Length; i++) {
						if (temp [i] == System.Convert.ToByte ('P')) {
							if (temp [i + 1] == System.Convert.ToByte ('u') && temp [i + 2] == System.Convert.ToByte ('b') &&
							    temp [i + 3] == System.Convert.ToByte ('l') && temp [i + 4] == System.Convert.ToByte ('i') &&
							    temp [i + 5] == System.Convert.ToByte ('c') && temp [i + 6] == System.Convert.ToByte ('K') &&
							    temp [i + 7] == System.Convert.ToByte ('e') && temp [i + 8] == System.Convert.ToByte ('y') &&
							    temp [i + 9] == System.Convert.ToByte ('T') && temp [i + 10] == System.Convert.ToByte ('o') &&
							    temp [i + 11] == System.Convert.ToByte ('k') && temp [i + 12] == System.Convert.ToByte ('e') &&
							    temp [i + 13] == System.Convert.ToByte ('n') && temp [i + 14] == System.Convert.ToByte ('=') &&
							    temp [i + 15] == System.Convert.ToByte ('6') && temp [i + 16] == System.Convert.ToByte ('f') &&
							    temp [i + 17] == System.Convert.ToByte ('2') && temp [i + 18] == System.Convert.ToByte ('6') &&
							    temp [i + 19] == System.Convert.ToByte ('a') && temp [i + 20] == System.Convert.ToByte ('2') &&
							    temp [i + 21] == System.Convert.ToByte ('b') && temp [i + 22] == System.Convert.ToByte ('4') &&
							    temp [i + 23] == System.Convert.ToByte ('b') && temp [i + 24] == System.Convert.ToByte ('0') &&
							    temp [i + 25] == System.Convert.ToByte ('3') && temp [i + 26] == System.Convert.ToByte ('1') &&
							    temp [i + 27] == System.Convert.ToByte ('f') && temp [i + 28] == System.Convert.ToByte ('c') &&
							    temp [i + 29] == System.Convert.ToByte ('8') && temp [i + 30] == System.Convert.ToByte ('9')) {
								temp [i] = System.Convert.ToByte ('P');
								temp [i + 1] = System.Convert.ToByte ('u');
								temp [i + 2] = System.Convert.ToByte ('b');
								temp [i + 3] = System.Convert.ToByte ('l');
								temp [i + 4] = System.Convert.ToByte ('i');
								temp [i + 5] = System.Convert.ToByte ('c');
								temp [i + 6] = System.Convert.ToByte ('K');
								temp [i + 7] = System.Convert.ToByte ('e');
								temp [i + 8] = System.Convert.ToByte ('y');
								temp [i + 9] = System.Convert.ToByte ('T');
								temp [i + 10] = System.Convert.ToByte ('o');
								temp [i + 11] = System.Convert.ToByte ('k');
								temp [i + 12] = System.Convert.ToByte ('e');
								temp [i + 13] = System.Convert.ToByte ('n');
								temp [i + 14] = System.Convert.ToByte ('=');
								temp [i + 15] = System.Convert.ToByte ('n');
								temp [i + 16] = System.Convert.ToByte ('u');
								temp [i + 17] = System.Convert.ToByte ('l');
								temp [i + 18] = System.Convert.ToByte ('l');
								temp [i + 19] = System.Convert.ToByte (' ');
								temp [i + 20] = System.Convert.ToByte (' ');
								temp [i + 21] = System.Convert.ToByte (' ');
								temp [i + 22] = System.Convert.ToByte (' ');
								temp [i + 23] = System.Convert.ToByte (' ');
								temp [i + 24] = System.Convert.ToByte (' ');
								temp [i + 25] = System.Convert.ToByte (' ');
								temp [i + 26] = System.Convert.ToByte (' ');
								temp [i + 27] = System.Convert.ToByte (' ');
								temp [i + 28] = System.Convert.ToByte (' ');
								temp [i + 29] = System.Convert.ToByte (' ');
								temp [i + 30] = System.Convert.ToByte (' ');
								i += 29;
							}
						}
					}

                    


					//string strt = ASCIIEncoding.ASCII.GetString(temp);
					////strt = strt.Replace("PublicKeyToken=6f26a2b4b031fc89", "PublicKeyToken=null");
					MemoryStream stream = new MemoryStream (temp);

					BinaryFormatter formatter = new BinaryFormatter ();
					formatter.Binder = new UBinder ();
					//formatter.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;

					//formatter.Binder = new ConvertAssemblyBinder();
					// Create a SurrogateSelector.
					//SurrogateSelector ss = new SurrogateSelector();

					//// Tell the SurrogateSelector that Employee objects are serialized and deserialized 
					//// using the EmployeeSerializationSurrogate object.
					//ss.AddSurrogate(typeof(PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.FieldTreeViewData),
					//new StreamingContext(StreamingContextStates.All),
					//new EmployeeSerializationSurrogate());

					//formatter.SurrogateSelector = ss;
					//formatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
					//formatter.FilterLevel = TypeFilterLevel.Full;
					//formatter.TypeFormat = FormatterTypeStyle.TypesAlways;

					ob = formatter.Deserialize (stream);//在这里要注意,返回值是object
					stream.Close ();
					stream.Dispose ();
				}
			} catch (IOException ex) {
				MessageBox.Show (ex.Message, "LoadPublicKeyTokenFile",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
			return true;
		}

		public static bool LoadPublicKeyTokenFile (byte[] data, ref object ob)
		{
			try {
				if (null == data)
					return false;

				byte[] temp = data;
				for (int i = 0; i + 31 < temp.Length; i++) {
					if (temp [i] == System.Convert.ToByte ('P')) {
						if (temp [i + 1] == System.Convert.ToByte ('u') && temp [i + 2] == System.Convert.ToByte ('b') &&
						    temp [i + 3] == System.Convert.ToByte ('l') && temp [i + 4] == System.Convert.ToByte ('i') &&
						    temp [i + 5] == System.Convert.ToByte ('c') && temp [i + 6] == System.Convert.ToByte ('K') &&
						    temp [i + 7] == System.Convert.ToByte ('e') && temp [i + 8] == System.Convert.ToByte ('y') &&
						    temp [i + 9] == System.Convert.ToByte ('T') && temp [i + 10] == System.Convert.ToByte ('o') &&
						    temp [i + 11] == System.Convert.ToByte ('k') && temp [i + 12] == System.Convert.ToByte ('e') &&
						    temp [i + 13] == System.Convert.ToByte ('n') && temp [i + 14] == System.Convert.ToByte ('=') &&
						    temp [i + 15] == System.Convert.ToByte ('6') && temp [i + 16] == System.Convert.ToByte ('f') &&
						    temp [i + 17] == System.Convert.ToByte ('2') && temp [i + 18] == System.Convert.ToByte ('6') &&
						    temp [i + 19] == System.Convert.ToByte ('a') && temp [i + 20] == System.Convert.ToByte ('2') &&
						    temp [i + 21] == System.Convert.ToByte ('b') && temp [i + 22] == System.Convert.ToByte ('4') &&
						    temp [i + 23] == System.Convert.ToByte ('b') && temp [i + 24] == System.Convert.ToByte ('0') &&
						    temp [i + 25] == System.Convert.ToByte ('3') && temp [i + 26] == System.Convert.ToByte ('1') &&
						    temp [i + 27] == System.Convert.ToByte ('f') && temp [i + 28] == System.Convert.ToByte ('c') &&
						    temp [i + 29] == System.Convert.ToByte ('8') && temp [i + 30] == System.Convert.ToByte ('9')) {
							temp [i] = System.Convert.ToByte ('P');
							temp [i + 1] = System.Convert.ToByte ('u');
							temp [i + 2] = System.Convert.ToByte ('b');
							temp [i + 3] = System.Convert.ToByte ('l');
							temp [i + 4] = System.Convert.ToByte ('i');
							temp [i + 5] = System.Convert.ToByte ('c');
							temp [i + 6] = System.Convert.ToByte ('K');
							temp [i + 7] = System.Convert.ToByte ('e');
							temp [i + 8] = System.Convert.ToByte ('y');
							temp [i + 9] = System.Convert.ToByte ('T');
							temp [i + 10] = System.Convert.ToByte ('o');
							temp [i + 11] = System.Convert.ToByte ('k');
							temp [i + 12] = System.Convert.ToByte ('e');
							temp [i + 13] = System.Convert.ToByte ('n');
							temp [i + 14] = System.Convert.ToByte ('=');
							temp [i + 15] = System.Convert.ToByte ('n');
							temp [i + 16] = System.Convert.ToByte ('u');
							temp [i + 17] = System.Convert.ToByte ('l');
							temp [i + 18] = System.Convert.ToByte ('l');
							temp [i + 19] = System.Convert.ToByte (' ');
							temp [i + 20] = System.Convert.ToByte (' ');
							temp [i + 21] = System.Convert.ToByte (' ');
							temp [i + 22] = System.Convert.ToByte (' ');
							temp [i + 23] = System.Convert.ToByte (' ');
							temp [i + 24] = System.Convert.ToByte (' ');
							temp [i + 25] = System.Convert.ToByte (' ');
							temp [i + 26] = System.Convert.ToByte (' ');
							temp [i + 27] = System.Convert.ToByte (' ');
							temp [i + 28] = System.Convert.ToByte (' ');
							temp [i + 29] = System.Convert.ToByte (' ');
							temp [i + 30] = System.Convert.ToByte (' ');
							i += 29;
						}
					}
				}




				//string strt = ASCIIEncoding.ASCII.GetString(temp);
				////strt = strt.Replace("PublicKeyToken=6f26a2b4b031fc89", "PublicKeyToken=null");
				MemoryStream stream = new MemoryStream (temp);

				BinaryFormatter formatter = new BinaryFormatter ();
				formatter.Binder = new UBinder ();
				//formatter.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;

				//formatter.Binder = new ConvertAssemblyBinder();
				// Create a SurrogateSelector.
				//SurrogateSelector ss = new SurrogateSelector();

				//// Tell the SurrogateSelector that Employee objects are serialized and deserialized 
				//// using the EmployeeSerializationSurrogate object.
				//ss.AddSurrogate(typeof(PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.FieldTreeViewData),
				//new StreamingContext(StreamingContextStates.All),
				//new EmployeeSerializationSurrogate());

				//formatter.SurrogateSelector = ss;
				//formatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
				//formatter.FilterLevel = TypeFilterLevel.Full;
				//formatter.TypeFormat = FormatterTypeStyle.TypesAlways;

				ob = formatter.Deserialize (stream);//在这里要注意,返回值是object
				stream.Close ();
				stream.Dispose ();
			} catch (IOException ex) {
				MessageBox.Show (ex.Message, "LoadPublicKeyTokenFile",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
			return true;
		}

		public static bool LoadFile (string filePath, ref byte[] data)
		{
			try {
				if (System.IO.File.Exists (filePath))
					data = System.IO.File.ReadAllBytes (filePath);
				else
					return false;
			} catch (IOException ex) {
				MessageBox.Show (ex.Message, "SaveFile(string filePath, byte[] data)",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
			return true;
		}

		public static bool SaveSoapFile (string filePath, object ob)
		{
			try {
				if (filePath == null)
					return false;
				using (FileStream fs = new FileStream (filePath, FileMode.Create, FileAccess.Write)) {
					SoapFormatter formatter = new SoapFormatter ();
					formatter.Serialize (fs, ob);
					fs.Close ();
					fs.Dispose ();
				}
			} catch (IOException ex) {
				MessageBox.Show (ex.Message, "SaveSoapFile",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
			return true;
		}

		public static bool LoadSoapFile (string filePath, ref object ob)
		{
			try {
				if (!System.IO.File.Exists (filePath))
					return false;
				using (FileStream fs = new FileStream (filePath, FileMode.Open, FileAccess.Read)) {
					SoapFormatter formatter = new SoapFormatter ();
					ob = formatter.Deserialize (fs);
					fs.Close ();
					fs.Dispose ();
				}
			} catch (IOException ex) {
				MessageBox.Show (ex.Message, "SaveSoapFile",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
			return true;
		}

		public static byte[] ObjToByte (object ob)
		{
			MemoryStream stream = new MemoryStream ();
			IFormatter formatter = new BinaryFormatter ();
			formatter.Serialize (stream, ob);
			stream.Flush ();
			byte[] theBytes = stream.ToArray ();
			stream.Close ();
			stream.Dispose ();
			return theBytes;
		}

		public static object ByteToObj (byte[] theBytes)
		{
			IFormatter formatter = new BinaryFormatter ();
			MemoryStream stream = new MemoryStream (theBytes);   //theBytes是序列化后的结果
			object ob = formatter.Deserialize (stream);
			stream.Close ();
			stream.Dispose ();
			return ob;
		}

		public static object CloneObj (object ob)
		{
			if (ob == null)
				return null;
			return ByteToObj (ObjToByte (ob));
		}

		public static byte[] GetContentFromPathWithGuid (System.Guid guid, string path)
		{
			string[] fileNameArray = Directory.GetFiles (path, guid.ToString () + ".*", SearchOption.AllDirectories);
			if (fileNameArray.Length == 0)
				return null;
			List<string> fileNameList = fileNameArray.ToList<string> ();
			string err = string.Empty;
			string strZipPath = Path.Combine (path, guid.ToString () + ".zip");
			if (PMS.Libraries.ToolControls.PMSPublicInfo.ZipClass.ZipFile (fileNameList, strZipPath, out err)) {
				byte[] fileContent = File.ReadAllBytes (strZipPath);
				if (File.Exists (strZipPath))
					File.Delete (strZipPath);
				return fileContent;
			} else {
				PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, err, true);
				return null;
			}
		}

		public static bool ZipFilesWithFileName (string filename, string srcPath, string destZipFileName)
		{
			string err = string.Empty;
			try {
				string[] fileNameArray = Directory.GetFiles (srcPath, filename + ".*", SearchOption.TopDirectoryOnly);
				if (fileNameArray.Length == 0)
					return false;
				List<string> fileNameList = fileNameArray.ToList<string> ();
				return PMS.Libraries.ToolControls.PMSPublicInfo.ZipClass.ZipFile (fileNameList, destZipFileName, out err);
			} catch (System.Exception ex) {
				PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, err, true);
				return false;
			}
		}

		public static bool DeleteFilesWithFileName (string filename, string srcPath)
		{
			string err = string.Empty;
			try {
				string[] fileNameArray = Directory.GetFiles (srcPath, filename + ".*", SearchOption.TopDirectoryOnly);
				if (fileNameArray.Length == 0)
					return true;
				List<string> fileNameList = fileNameArray.ToList<string> ();
				foreach (string strpath in fileNameList) {
					File.Delete (strpath);
				}
                
				return true;
			} catch (System.Exception ex) {
				PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, err, true);
				return false;
			}
		}

		public static bool ZipFiles (string srcPath, string destZipFileName)
		{
			string err = string.Empty;
			try {
				string[] fileNameArray = Directory.GetFiles (srcPath);
				if (fileNameArray.Length == 0)
					return false;
				List<string> fileNameList = fileNameArray.ToList<string> ();
				return PMS.Libraries.ToolControls.PMSPublicInfo.ZipClass.ZipFile (fileNameList, destZipFileName, out err);
			} catch (System.Exception ex) {
				err = ex.Message;
				PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, err, true);
				return false;
			}
		}

		public static bool ZipToPVFFileWithGuid (s_CfgFileInfo info, string path)
		{
			string SaveAsPath = System.IO.Path.GetDirectoryName (path) + System.IO.Path.DirectorySeparatorChar;
			string[] fileNameArray = Directory.GetFiles (SaveAsPath, info.FID.ToString () + ".*", SearchOption.AllDirectories);
			if (fileNameArray.Length == 0)
				return false;
			List<string> fileNameList = fileNameArray.ToList<string> ();
			string err = string.Empty;
			string strZipPath = Path.Combine (SaveAsPath, info.FID.ToString () + ".zip");
			if (File.Exists (path))
				File.Delete (path);
			if (PMS.Libraries.ToolControls.PMSPublicInfo.ZipClass.ZipFile (fileNameList, strZipPath, out err)) {
				PvfFileObjClass _PvfFileObj = new PvfFileObjClass ();
				_PvfFileObj._guid = info.FID;
				_PvfFileObj._type = info.Type;
				_PvfFileObj._modelid = info.ModelID;
				_PvfFileObj._content = System.IO.File.ReadAllBytes (strZipPath);

				if (PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.SaveFile (path, _PvfFileObj)) {                    
					foreach (string filepath in fileNameList) {
						System.IO.File.Delete (filepath);
					}
					System.IO.File.Delete (strZipPath);
				}
				return true;
			}
			return false;
		}

		public static MemoryStream ZipStream (Stream sIn)
		{
			string err = string.Empty;
			return PMS.Libraries.ToolControls.PMSPublicInfo.ZipClass.ZipStream (sIn, out err);
		}

		public static MemoryStream UnZipStream (Stream sToUnzip)
		{
			string err = string.Empty;
			return PMS.Libraries.ToolControls.PMSPublicInfo.ZipClass.UnZipStream (sToUnzip, out err);
		}

		public static MemoryStream GZipStream (Stream sIn)
		{
			sIn.Position = 0;
			MemoryStream dest = new MemoryStream ();
			using (GZipStream zipStream = new GZipStream (dest, CompressionMode.Compress, true)) {
				byte[] buf = new byte[1024];
				int len;
				while ((len = sIn.Read (buf, 0, buf.Length)) > 0) {
					zipStream.Write (buf, 0, len);
				}
			}
			return dest;
		}

		public static MemoryStream GUnZipStream (Stream sToUnzip)
		{
			sToUnzip.Position = 0;
			MemoryStream dest = new MemoryStream ();
			using (GZipStream zipStream = new GZipStream (sToUnzip, CompressionMode.Decompress, true)) {
				byte[] buf = new byte[1024];
				int len;
				while ((len = zipStream.Read (buf, 0, buf.Length)) > 0) {
					dest.Write (buf, 0, len);
				}
			}
			return dest;
		}


        

		public static bool RenameFiles (System.Guid oldguid, System.Guid newguid, string path)
		{
			string[] fileNameArray = Directory.GetFiles (path, oldguid.ToString () + ".*", SearchOption.AllDirectories);
			if (fileNameArray.Length == 0)
				return false;
			List<string> fileNameList = fileNameArray.ToList<string> ();
			string err = string.Empty;
			foreach (string fileName in fileNameList) {
				if (string.Compare (fileName, fileName.Replace (oldguid.ToString (), newguid.ToString ()), true) != 0) {
					if (File.Exists (fileName.Replace (oldguid.ToString (), newguid.ToString ())))
						File.Delete (fileName.Replace (oldguid.ToString (), newguid.ToString ()));
					File.Move (fileName, fileName.Replace (oldguid.ToString (), newguid.ToString ()));
				}
			}

			return true;
		}

		public static bool CopyFiles (System.Guid oldguid, System.Guid newguid, string oldpath, string newpath)
		{
			string[] fileNameArray = Directory.GetFiles (oldpath, oldguid.ToString () + ".*", SearchOption.AllDirectories);
			if (fileNameArray.Length == 0)
				return false;
			List<string> fileNameList = fileNameArray.ToList<string> ();
			string err = string.Empty;
			foreach (string fileName in fileNameList) {
				string newName = Path.GetFileName (fileName);
				newName = newName.Replace (oldguid.ToString (), newguid.ToString ());
				File.Copy (fileName, Path.Combine (newpath, newName), true);
			}

			return true;
		}

		public static DateTime GetMostRecentModifiedTimeStampWithGuid (System.Guid guid, string path)
		{
			string[] fileNameArray = Directory.GetFiles (path, guid.ToString () + ".*", SearchOption.TopDirectoryOnly);
			DateTime dtMostRecent = DateTime.MinValue;
			foreach (string fileName in fileNameArray) {
				if (dtMostRecent < File.GetLastWriteTime (fileName))
					dtMostRecent = File.GetLastWriteTime (fileName);
			}
			return dtMostRecent;
		}

		public static DateTime GetFileModifiedTimeStamp (string filePath)
		{
			if (File.Exists (filePath)) {
				return File.GetLastWriteTime (filePath);
			}
			return DateTime.MinValue;
		}

		public static bool CopyReportFile (string strSource, string strDest)
		{
			MESReportFileObj rptFileObj = null;
			if (DBFileManager.LoadReportFile (strSource, ref rptFileObj)) {
				if (null != rptFileObj) {
					string strGuid = Path.GetFileNameWithoutExtension (strDest);
					Guid guid = Guid.Empty;
					if (Guid.TryParse (strGuid, out guid))
						rptFileObj.identity = guid;
					else
						rptFileObj.identity = Guid.NewGuid ();
					return DBFileManager.SaveReportFile (strDest, rptFileObj);
				}
			}
			return false;
		}

		public static bool CopyFormFile (string strSource, string strDest)
		{
			MESFormFileObj frmFileObj = null;
			if (DBFileManager.LoadFormFile (strSource, ref frmFileObj)) {
				if (null != frmFileObj) {
					string strGuid = Path.GetFileNameWithoutExtension (strDest);
					Guid guid = Guid.Empty;
					if (Guid.TryParse (strGuid, out guid))
						frmFileObj.identity = guid;
					else
						frmFileObj.identity = Guid.NewGuid ();
					return DBFileManager.SaveFormFile (strDest, frmFileObj);
				}
			}
			return false;
		}

		public static bool CopyNavigatorFile (string strSource, string strDest)
		{
			MESNavigatorFileObj navFileObj = null;
			if (DBFileManager.LoadNavigatorFile (strSource, ref navFileObj)) {
				if (null != navFileObj) {
					string strGuid = Path.GetFileNameWithoutExtension (strDest);
					Guid guid = Guid.Empty;
					if (Guid.TryParse (strGuid, out guid))
						navFileObj.identity = guid;
					else
						navFileObj.identity = Guid.NewGuid ();
					return DBFileManager.SaveNavigatorFile (strDest, navFileObj);
				}
			}
			return false;
		}

		public static string GetMD5 (string path)
		{
			FileStream myfile = new FileStream (path, FileMode.Open, FileAccess.Read, FileShare.Read);
			System.Security.Cryptography.MD5CryptoServiceProvider fileMD5 = new System.Security.Cryptography.MD5CryptoServiceProvider ();
			byte[] hashbyte = fileMD5.ComputeHash (myfile);
			myfile.Close ();
			string result = System.BitConverter.ToString (hashbyte);
			result = result.Replace ("-", "");
			return result;
		}
	}

	/// <summary>
	/// ini文件操作类
	/// </summary>
	public class INIClass
	{
		public string inipath;

		[DllImport ("kernel32")]
		private static extern long WritePrivateProfileString (string section, string key, string val, string filePath);

		[DllImport ("kernel32")]
		private static extern int GetPrivateProfileString (string section, string key, string def, StringBuilder retVal, int size, string filePath);

		/// <summary>
		/// 构造方法
		/// </summary>
		/// <param name="INIPath">文件路径</param>
		public INIClass (string INIPath)
		{
			inipath = INIPath;
		}

		/// <summary>
		/// 写入INI文件
		/// </summary>
		/// <param name="Section">项目名称(如 [TypeName] )</param>
		/// <param name="Key">键</param>
		/// <param name="Value">值</param>
		public void IniWriteValue (string Section, string Key, string Value)
		{
			WritePrivateProfileString (Section, Key, Value, this.inipath);
		}

		/// <summary>
		/// 读出INI文件
		/// </summary>
		/// <param name="Section">项目名称(如 [TypeName] )</param>
		/// <param name="Key">键</param>
		public string IniReadValue (string Section, string Key)
		{
			StringBuilder temp = new StringBuilder (500);
			int i = GetPrivateProfileString (Section, Key, "", temp, 500, this.inipath);
			return temp.ToString ();
		}

		/// <summary>
		/// 验证文件是否存在
		/// </summary>
		/// <returns>布尔值</returns>
		public bool ExistINIFile ()
		{
			return File.Exists (inipath);
		}
	}

	/// <summary>
	/// 项目路径信息
	/// </summary>
	public class ProjectPathClass
	{
		private static string _prjpath = null;

		private static string _ReportPath = null;
		private static string _ReportCfgFilePath = null;

		private static string _StaticPath = null;
		private static string _StaticCfgFilePath = null;

		private static string _SheetPath = null;
		private static string _SheetCfgFilePath = null;

		private static string _UserCustomPath = null;
		private static string _UserCustomCfgFilePath = null;
		private static string _UserCustomRelativePath = "UserCustom";
		private static string _UserCustomLocalInfoFilePath = null;

		private static string _OrgPath = null;
		private static string _OrgCfgFilePath = null;

		private static string _TemplatePath = null;
		private static string _TemplateCfgFilePath = null;

		private static string _TemplateUserCustomPath = null;
		private static string _TemplateUserCustomCfgFilePath = null;

		private static string _TemplateOrgPath = null;
		private static string _TemplateOrgCfgFilePath = null;
		private static string _OrgChartTemplateFilePath = null;

		private static string _TemplatePrjPath = null;
		private static string _TemplatePrjCfgFilePath = null;

		private static string _DockPanelCfgFilePath = null;
		private static string _DeveloperDockPanelCfgFilePath = null;
		private static string _ClientDockPanelCfgFilePath = null;
		private static string _MESReportDesignerDockPanelCfgFilePath = null;

		private static string _PrjSettingPath = null;

		private static string _FormsSettingFilePath = null;

		private static string _TraceLogPath = null;
		private static string _TraceLogFilePath = null;
		private static string _TraceLogFileBackupPath = null;

		private static string _RefDBConfigSettingFilePath = null;

		private static string _RefDBSourcesFilePath = null;

		private static string _ServerRefDBSourcesFilePath = null;

		private static string _SysCustomizeViewPath = null;

		private static string _PrjResourcePath = null;

		private static string _PrjMapTableFilePath = null;

		private static string _OptionsSettingFilePath = null;

		private static string _ShuangLiangOptionsSettingFilePath = null;

		private static string _NavigatorSettingFilePath = null;

		private static string _CustomMsgFilePath = null;

		private static string _SystemStructFilePath = null;

		private static string _WorkFlowPath = null;

		private static string _TempPath = null;

		private static string _AttachmentPath = null;

		#region 文件服务器上配置文件路径及各模块路径

		private static string _ServerSettingPath = null;

		#endregion

		#region property

		public const string ReportCfgFileName = "ReportTree.cfg";
		public const string SheetCfgFileName = "SheetTree.cfg";
		public const string StaticCfgFileName = "StaticTree.cfg";
		public const string UserCustomCfgFileName = "UserCustomTree.cfg";
		public const string UserCustomLocalinfoCfgFileName = "LocalInfo.cfg";
		public const string OrgCfgFileName = "OrgTree.cfg";
		public const string TemplateCfgFileName = "TemplateTree.cfg";
		public const string TemplateUserCustomCfgFileName = "TemplateUserCustomTree.cfg";
		public const string TemplateOrgCfgFileName = "TemplateOrgTree.cfg";
		public const string TemplatePrjCfgFileName = "TemplatePrjTree.cfg";

		public const string DockPanelCfgFileName = "PMSDockPanel.config";
		public const string DeveloperDockPanelCfgFileName = "PMSDeveloperDockPanel.config";
		public const string ClientDockPanelCfgFileName = "PMSClientDockPanel.config";
		public const string MESReportDesignerDockPanelCfgFileName = "MESReportDesignerDockPanel.config";
		public const string OrgChartTemplateFileName = "OrgChartTemplate.ini";

		// 各Form配置文件cfg
		public const string FormsSettingFileName = "FormsSetting.cfg";

		public const string TraceLogFileName = "TraceLog.tl";
		public const string TraceLogBackupFileName = "TraceLog_BC.tl";
		public const string TraceLogFileExtName = "tl";

		// 报表连接数据库配置文件
		public const string RefDBConfigSettingFileName = "RefDBConfig.Settings";

		// 报表引用数据源配置文件
		public const string RefDBSourcesFilePathName = "RefDBSources.dbs";

		// 视图文件另存为后缀名
		public const string ViewFileSaveAsExt = "pvf";

		public const string MESDeveloperToolStripConfigFileName = "MesDeveloperToolStrip.config";

		public const string MESPrjMapTableFileName = "MapTable.xml";

		// 存储安装包相关信息的 ini
		public const string SetupIniName = "config.ini";

		// 宏定义[%PRJPATH%]
		public const string MACRO_PRJPATH = "[%PRJPATH%]";

		// 系统信息配置文件
		public const string OptionsSettingFileName = "Options.Settings";

		// 双良系统信息配置文件
		public const string ShuangLiangOptionsSettingFileName = "ShuangLiangOptions.Settings";

		// 客户端导航配置文件
		public const string NavigatorFilePathName = "Navigator.nav";

		// 自定义消息配置文件名
		public const string CustomMsgFilePathName = "CustomMsgConfig.xml";

		// 系统结构配置文件名
		public const string SystemStructFilePathName = "SystemStructConfig.xml";

		//todo:qiuleilei
		/// <summary>
		/// Conf
		/// </summary>
		public const string MesReportServerConf = "Conf";
		public const string MesReportServerServerWeb = "ServerWeb";
		public const string MesReportServerQuery = "Query";
		public const string MesReportServerExport = "Export";


		#region 文件服务端路径

		// WorkFlow
		public const string Server_WFPath = "WorkFlow";
		public static readonly string Server_WFInstancesPath = Server_WFPath + System.IO.Path.DirectorySeparatorChar + "WorkFlow_Instances";
		public static readonly string Server_WFVersionPath = Server_WFPath + System.IO.Path.DirectorySeparatorChar + "WorkFlow_Version";
		public static readonly string Server_WFVersionArchivePath = Server_WFPath + System.IO.Path.DirectorySeparatorChar + "WorkFlow_VersionArchive";
		public static readonly string Server_WFVersionReleasePath = Server_WFPath + System.IO.Path.DirectorySeparatorChar + "WorkFlow_VersionRelease";

		// Attachment
		public const string Server_AttachmentPath = "Attachment";

		#endregion

		// 若是报表控件，NETSCADA传过来时是带MES\的
		public static string ProjectPath {
			get { return _prjpath; }
			set { 
				_prjpath = value;
				if ((PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentEnvironment == MESEnvironment.MESClient ||
				    PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentEnvironment == MESEnvironment.MESDeveloper) &&
				    PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentPackageFor == PackageFor.None)
					_prjpath = Path.Combine (_prjpath, "UserCustom", PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID);
				var sep = System.IO.Path.DirectorySeparatorChar.ToString ();
				if (!_prjpath.EndsWith (sep))
					_prjpath += sep;
				_ReportPath = _prjpath + "Report" + sep;
				_ReportCfgFilePath = _ReportPath + ReportCfgFileName;

				_SheetPath = _prjpath + "Sheet" + sep;
				_SheetCfgFilePath = _SheetPath + SheetCfgFileName;

				_StaticPath = _prjpath + "Static" + sep;
				_StaticCfgFilePath = _StaticPath + StaticCfgFileName;

				_UserCustomPath = _prjpath + "UserCustom" + sep;
				_UserCustomCfgFilePath = _UserCustomPath + UserCustomCfgFileName;
				_UserCustomRelativePath = "UserCustom" + sep;
				_UserCustomLocalInfoFilePath = _UserCustomPath + UserCustomLocalinfoCfgFileName;
				_WorkFlowPath = _UserCustomPath + "WorkFlow" + sep;

				_OrgPath = _prjpath + "Org" + sep;
				_OrgCfgFilePath = _OrgPath + OrgCfgFileName;

				_TemplatePath = _prjpath + "Template" + sep;
				_TemplateCfgFilePath = _TemplatePath + TemplateCfgFileName;

				_TempPath = _prjpath + "Temp" + sep;

				_TemplateUserCustomPath = _prjpath + "Template" + sep + "UserCustom" + sep;
				_TemplateUserCustomCfgFilePath = _TemplateUserCustomPath + TemplateUserCustomCfgFileName;

				_TemplateOrgPath = _prjpath + "Template" + sep + "Org" + sep;
				_TemplateOrgCfgFilePath = _TemplateOrgPath + TemplateOrgCfgFileName;
				_OrgChartTemplateFilePath = _TemplateOrgPath + OrgChartTemplateFileName;

				_TemplatePrjPath = _prjpath + "Template" + sep + "Prj" + sep;
				_TemplatePrjCfgFilePath = _TemplatePrjPath + TemplatePrjCfgFileName;

				_DockPanelCfgFilePath = _prjpath + DockPanelCfgFileName;
				_DeveloperDockPanelCfgFilePath = _prjpath + DeveloperDockPanelCfgFileName;
				_ClientDockPanelCfgFilePath = _prjpath + ClientDockPanelCfgFileName;
				_MESReportDesignerDockPanelCfgFilePath = _prjpath + MESReportDesignerDockPanelCfgFileName;

				_PrjSettingPath = _prjpath + "ConfigFiles" + sep;
				_FormsSettingFilePath = _PrjSettingPath + FormsSettingFileName;
				_OptionsSettingFilePath = Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "ConfigFiles" + sep, OptionsSettingFileName);
				_ServerRefDBSourcesFilePath = _PrjSettingPath + RefDBSourcesFilePathName;
				_NavigatorSettingFilePath = _PrjSettingPath + NavigatorFilePathName;
				_CustomMsgFilePath = _PrjSettingPath + CustomMsgFilePathName;
				_SystemStructFilePath = _PrjSettingPath + SystemStructFilePathName;
				_ShuangLiangOptionsSettingFilePath = _PrjSettingPath + ShuangLiangOptionsSettingFileName;
				_ServerSettingPath = "ConfigFiles" + sep;

				_TraceLogPath = _prjpath + "Trace" + sep;
				_TraceLogFilePath = _TraceLogPath + TraceLogFileName;
				_TraceLogFileBackupPath = _TraceLogPath + TraceLogBackupFileName;

				_SysCustomizeViewPath = _prjpath + "SysCustomizeView" + sep;
				_PrjResourcePath = _prjpath + "PrjResource" + sep;
				_AttachmentPath = _prjpath + "Attachment" + sep;

				//todo:qiuleilei
				_Conf = Path.Combine (_prjpath, MesReportServerConf);
				_ServerWeb = Path.Combine (_prjpath, MesReportServerServerWeb);
				_ServerWebQuery = Path.Combine (_ServerWeb, MesReportServerQuery);
				_ServerWebExport = Path.Combine (_ServerWeb, MesReportServerExport);

				if (CurrentPrjInfo.IsPlugin ||
				    PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentPackageFor != PackageFor.None ||
				    PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentEnvironment == MESEnvironment.MESReportRunner ||
				    PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentEnvironment == MESEnvironment.MESGeneralSevice) {
					_RefDBConfigSettingFilePath = _prjpath + RefDBConfigSettingFileName;
					_RefDBSourcesFilePath = _prjpath + RefDBSourcesFilePathName;
					_PrjMapTableFilePath = _PrjResourcePath + MESPrjMapTableFileName;
				} else {
					_RefDBConfigSettingFilePath = _PrjSettingPath + RefDBConfigSettingFileName;
					_RefDBSourcesFilePath = _PrjSettingPath + RefDBSourcesFilePathName;
					_PrjMapTableFilePath = _PrjSettingPath + MESPrjMapTableFileName;
				}
				//todo:qiuleilei
				if (CurrentPrjInfo.CurrentEnvironment == MESEnvironment.MESReportServer) {
					_RefDBSourcesFilePath = Path.Combine (_Conf, RefDBSourcesFilePathName);
				}
			}
		}


		//todo:qiuleilei
		private static string _Conf = string.Empty;
		private static string _ServerWeb = string.Empty;
		private static string _ServerWebQuery = string.Empty;
		private static string _ServerWebExport = string.Empty;

		public static string Conf {
			get {
				return _Conf;
			}

			set {
				_Conf = value;
			}
		}

		public static string ServerWeb {
			get {
				return _ServerWeb;
			}

			set {
				_ServerWeb = value;
			}
		}

		public static string ServerWebQuery {
			get {
				return _ServerWebQuery;
			}

			set {
				_ServerWebQuery = value;
			}
		}

		public static string ServerWebExport {
			get {
				return _ServerWebExport;
			}

			set {
				_ServerWebExport = value;
			}
		}


		// Report
		public static string ReportPath {
			get { return _ReportPath; }
			set { _ReportPath = value; }
		}

		public static string ReportCfgFilePath {
			get { return _ReportCfgFilePath; }
			set { _ReportCfgFilePath = value; }
		}

		public static string GetReportFilePath (string filename)
		{
			return _ReportPath + filename + ".xml";
		}

		// Static
		public static string StaticPath {
			get { return _StaticPath; }
			set { _StaticPath = value; }
		}

		public static string StaticCfgFilePath {
			get { return _StaticCfgFilePath; }
			set { _StaticCfgFilePath = value; }
		}

		public static string GetStaticFilePath (string filename)
		{
			return _StaticPath + filename + ".xml";
		}

		// Sheet
		public static string SheetPath {
			get { return _SheetPath; }
			set { _SheetPath = value; }
		}

		public static string SheetCfgFilePath {
			get { return _SheetCfgFilePath; }
			set { _SheetCfgFilePath = value; }
		}

		public static string GetSheetFilePath (string filename)
		{
			return _SheetPath + filename + ".xml";
		}

		// UserCustom
		public static string UserCustomPath {
			get { return _UserCustomPath; }
			set { _UserCustomPath = value; }
		}

		public static string UserCustomCfgFilePath {
			get { return _UserCustomCfgFilePath; }
			set { _UserCustomCfgFilePath = value; }
		}

		public static string UserCustomRelativePath {
			get { return _UserCustomRelativePath; }
			set { _UserCustomRelativePath = value; }
		}

		public static string UserCustomLocalInfoFilePath {
			get { return _UserCustomLocalInfoFilePath; }
			set { _UserCustomLocalInfoFilePath = value; }
		}

		public static string WorkFlowPath {
			get { return _WorkFlowPath; }
			set { _WorkFlowPath = value; }
		}

		public static string GetUserCustomFilePath (string filename)
		{
			return _UserCustomPath + filename + ".xml";
		}

		// Org
		public static string OrgPath {
			get { return _OrgPath; }
			set { _OrgPath = value; }
		}

		public static string OrgCfgFilePath {
			get { return _OrgCfgFilePath; }
			set { _OrgCfgFilePath = value; }
		}

		public static string GetOrgFilePath (string filename)
		{
			return _OrgPath + filename + ".xml";
		}

		// Template
		public static string TemplatePath {
			get { return _TemplatePath; }
			set { _TemplatePath = value; }
		}

		public static string TemplateCfgFilePath {
			get { return _TemplateCfgFilePath; }
			set { _TemplateCfgFilePath = value; }
		}

		public static string GeTemplateFilePath (string filename)
		{
			return _TemplatePath + filename + ".xml";
		}

		// Template - UserCustom
		public static string TemplateUserCustomPath {
			get { return _TemplateUserCustomPath; }
			set { _TemplateUserCustomPath = value; }
		}

		public static string TemplateUserCustomCfgFilePath {
			get { return _TemplateUserCustomCfgFilePath; }
			set { _TemplateUserCustomCfgFilePath = value; }
		}

		public static string GeTemplateUserCustomFilePath (string filename)
		{
			return _TemplateUserCustomPath + filename + ".xml";
		}

		// Template - Org
		public static string TemplateOrgPath {
			get { return _TemplateOrgPath; }
			set { _TemplateOrgPath = value; }
		}

		public static string TemplateOrgCfgFilePath {
			get { return _TemplateOrgCfgFilePath; }
			set { _TemplateOrgCfgFilePath = value; }
		}

		public static string OrgChartTemplateFilePath {
			get { return _OrgChartTemplateFilePath; }
			set { _OrgChartTemplateFilePath = value; }
		}

		public static string GeTemplateOrgFilePath (string filename)
		{
			return _TemplateOrgPath + filename + ".xml";
		}

		// Template - Prj
		public static string TemplatePrjPath {
			get { return _TemplatePrjPath; }
			set { _TemplatePrjPath = value; }
		}

		public static string TemplatePrjCfgFilePath {
			get { return _TemplatePrjCfgFilePath; }
			set { _TemplatePrjCfgFilePath = value; }
		}

		public static string GeTemplatePrjFilePath (string filename)
		{
			return _TemplatePrjPath + filename + ".xml";
		}


		public static string DockPanelCfgFilePath {
			get { return ProjectPathClass._DockPanelCfgFilePath; }
			set { ProjectPathClass._DockPanelCfgFilePath = value; }
		}

		public static string DeveloperDockPanelCfgFilePath {
			get { return ProjectPathClass._DeveloperDockPanelCfgFilePath; }
			set { ProjectPathClass._DeveloperDockPanelCfgFilePath = value; }
		}

		public static string ClientDockPanelCfgFilePath {
			get { return ProjectPathClass._ClientDockPanelCfgFilePath; }
			set { ProjectPathClass._ClientDockPanelCfgFilePath = value; }
		}

		public static string MESReportDesignerDockPanelCfgFilePath {
			get { return ProjectPathClass._MESReportDesignerDockPanelCfgFilePath; }
			set { ProjectPathClass._MESReportDesignerDockPanelCfgFilePath = value; }
		}

		/// <summary>
		/// 项目各种配置文件存放路径
		/// </summary>
		public static string PrjSettingPath {
			get { return ProjectPathClass._PrjSettingPath; }
			set { ProjectPathClass._PrjSettingPath = value; }
		}

		// 各Form配置文件
		public static string FormsSettingFilePath {
			get { return ProjectPathClass._FormsSettingFilePath; }
			set { ProjectPathClass._FormsSettingFilePath = value; }
		}

		// TraceLog
		public static string TraceLogPath {
			get { return _TraceLogPath; }
			set { _TraceLogPath = value; }
		}

		public static string TraceLogFilePath {
			get { return _TraceLogFilePath; }
			set { _TraceLogFilePath = value; }
		}

		public static string TraceLogFileBackupPath {
			get { return _TraceLogFileBackupPath; }
			set { _TraceLogFileBackupPath = value; }
		}

		public static string RefDBConfigSettingFilePath {
			get { return _RefDBConfigSettingFilePath; }
			set { _RefDBConfigSettingFilePath = value; }
		}

		public static string RefDBSourcesFilePath {
			get { return _RefDBSourcesFilePath; }
			set { _RefDBSourcesFilePath = value; }
		}

		public static string ServerRefDBSourcesFilePath {
			get { return _ServerRefDBSourcesFilePath; }
			set { _ServerRefDBSourcesFilePath = value; }
		}

		public static string SysCustomizeViewPath {
			get { return _SysCustomizeViewPath; }
		}

		// 项目中生成的公共资源存放路径
		public static string PrjResourcePath {
			get { return _PrjResourcePath; }
		}

		// 项目映射表文件存放路径
		public static string PrjMapTableFilePath {
			get { return _PrjMapTableFilePath; }
			set { _PrjMapTableFilePath = value; }
		}

		public static string OptionsSettingFilePath {
			get {
				if (string.IsNullOrEmpty (_OptionsSettingFilePath)) {
					System.IO.Directory.CreateDirectory (Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "ConfigFiles" + System.IO.Path.VolumeSeparatorChar));
					return Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "ConfigFiles" + System.IO.Path.DirectorySeparatorChar, OptionsSettingFileName);
				}
				return _OptionsSettingFilePath; 
			}
			set { _OptionsSettingFilePath = value; }
		}

		public static string ShuangLiangOptionsSettingFilePath {
			get { return _ShuangLiangOptionsSettingFilePath; }
			set { _ShuangLiangOptionsSettingFilePath = value; }
		}

		public static string ServerSettingPath {
			get { return _ServerSettingPath; }
			set { _ServerSettingPath = value; }
		}

		// 导航界面配置文件路径
		public static string NavigatorSettingFilePath {
			get { return _NavigatorSettingFilePath; }
			set { _NavigatorSettingFilePath = value; }
		}

		// 自定义消息配置文件路径
		public static string CustomMsgFilePath {
			get { return _CustomMsgFilePath; }
			set { _CustomMsgFilePath = value; }
		}

		// 系统结构配置文件路径
		public static string SystemStructFilePath {
			get { return _SystemStructFilePath; }
			set { _SystemStructFilePath = value; }
		}

		public static string TempPath {
			get { return _TempPath; }
			set { _TempPath = value; }
		}

		// 附件路径
		public static string AttachmentPath {
			get { return _AttachmentPath; }
			set { _AttachmentPath = value; }
		}

		#endregion

		#region Function

		public static void CreateAllPrjDirectory ()
		{
			System.IO.Directory.CreateDirectory (PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.ReportPath);
			System.IO.Directory.CreateDirectory (PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.SheetPath);
			System.IO.Directory.CreateDirectory (PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.StaticPath);
			System.IO.Directory.CreateDirectory (PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.UserCustomPath);
			System.IO.Directory.CreateDirectory (PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.OrgPath);
			System.IO.Directory.CreateDirectory (PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.TemplatePath);
			System.IO.Directory.CreateDirectory (PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.TemplateOrgPath);
			System.IO.Directory.CreateDirectory (PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.TemplateUserCustomPath);
			System.IO.Directory.CreateDirectory (PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.TemplatePrjPath);
			System.IO.Directory.CreateDirectory (PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.PrjSettingPath);
			System.IO.Directory.CreateDirectory (PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.TraceLogPath);
			System.IO.Directory.CreateDirectory (PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.SysCustomizeViewPath);
			System.IO.Directory.CreateDirectory (PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.PrjResourcePath);
		}

		public static void CreateReportPrjDirectory ()
		{
			System.IO.Directory.CreateDirectory (PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.ReportPath);
			System.IO.Directory.CreateDirectory (PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.UserCustomPath);
			System.IO.Directory.CreateDirectory (PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.PrjSettingPath);
			System.IO.Directory.CreateDirectory (PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.TraceLogPath);
			System.IO.Directory.CreateDirectory (PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.SysCustomizeViewPath);
			System.IO.Directory.CreateDirectory (PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.PrjResourcePath);
			System.IO.Directory.CreateDirectory (PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.TempPath);
		}

		public static void CreateBroadcastClientDirectory ()
		{
			if (!File.Exists (PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.SystemStructFilePath))
				System.IO.Directory.CreateDirectory (PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.SystemStructFilePath);
		}

		#endregion

		public static string ParseStringWithMacro (string str)
		{
			if (str == null)
				return null;
			string stre = System.IO.Path.DirectorySeparatorChar.ToString ();
			string tmp = PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.ProjectPath.TrimEnd (stre.ToCharArray ());
			if (tmp.EndsWith ("MES")) {
				tmp = tmp.Remove (tmp.Length - 3);
			}
			return str.Replace (MACRO_PRJPATH, tmp);
		}

		public static bool IsValidFullPath (string strPath)
		{
			var str = @"^\\\\[0-9]{1,3}(\.[0-9]{1,3}){3}(\\[^\\/:*?""<>|]+)+\\?$";
			if (Environment.OSVersion.Platform == PlatformID.Unix)
				str = @"^([\/] [\w-]+)*$";
			Regex re = new Regex (str);
			if (re.IsMatch (strPath))
				return false;
			return true;
		}
	}

	public interface ICustomControlInterface
	{
		bool IsModified {
			get;
			set;
		}

		bool Save ();

		bool RefreshItself ();

		event System.EventHandler ModifyEvent;

		event System.EventHandler SaveEvent;
	}

	public interface IDocument
	{
		DocType DocumentType { get; set; }
	}

	public interface IGetCurrent
	{
		object DataDefine { get; }
	}

	/// <summary>
	/// 设计时需要快捷键的控件继承此接口，处理了快捷键返回true，未处理返回false
	/// </summary>
	public interface IProcessCmdKey
	{
		bool ProcessCmdKey (ref System.Windows.Forms.Message msg, Keys keyData);
	}

	#region MESAttribute
    
	public enum MESAttributeType
	{
		// 仅序列化控件自身，不再递归其子控件
		OnlyControlItselfSerializable = 1,
		// 需要自定义属性序列化顺序的类标签
		// 如果类标示了此标记则其属性按照自定义顺序(由Att从小到大排序决定)进行序列化
		// 否则按照默认顺序进行序列化
		CustomSerializableOrder,
		// 设计时自定义组件标签
		MESComponent,
		// 控件的Controls属性按照先Y后X排序，e.g: TableLayout需要按此顺序排列序列化
		ControlsSortByYXCoordinate,
		// 属性不序列化
		NonSerialized,
	}

	//自定义特性
	[System.AttributeUsage (AttributeTargets.All)]
	public class MESAllAttributeAttribute : Attribute
	{
		private System.AttributeTargets _Attribute;

		public System.AttributeTargets Attribute {
			get { return _Attribute; }
			set { _Attribute = value; }
		}

		public MESAllAttributeAttribute (System.AttributeTargets att)
		{
			this._Attribute = att;
		}
	}

	[System.AttributeUsage (AttributeTargets.Assembly)]
	public class MESAssemblyAttributeAttribute : Attribute
	{
		private string _strAtt;

		public string StrAtt {
			get { return _strAtt; }
			set { _strAtt = value; }
		}

		public MESAssemblyAttributeAttribute (string att)
		{
			this._strAtt = att;
		}
	}

	[System.AttributeUsage (AttributeTargets.Class)]
	public class MESClassAttributeAttribute : Attribute
	{
		private string _strAtt;

		public string StrAtt {
			get { return _strAtt; }
			set { _strAtt = value; }
		}

		public MESClassAttributeAttribute (string att)
		{
			this._strAtt = att;
		}

		private MESAttributeType _attType;

		public MESAttributeType AttType {
			get { return _attType; }
			set { _attType = value; }
		}

		public MESClassAttributeAttribute (string att, MESAttributeType attType)
		{
			this._strAtt = att;
			this._attType = attType;
		}
	}

	[System.AttributeUsage (AttributeTargets.Constructor)]
	public class MESConstructorAttributeAttribute : Attribute
	{
		private string _strAtt;

		public string StrAtt {
			get { return _strAtt; }
			set { _strAtt = value; }
		}

		public MESConstructorAttributeAttribute (string att)
		{
			this._strAtt = att;
		}
	}

	[System.AttributeUsage (AttributeTargets.Delegate)]
	public class MESDelegateAttributeAttribute : Attribute
	{
		private string _strAtt;

		public string StrAtt {
			get { return _strAtt; }
			set { _strAtt = value; }
		}

		public MESDelegateAttributeAttribute (string att)
		{
			this._strAtt = att;
		}
	}

	[System.AttributeUsage (AttributeTargets.Enum)]
	public class MESEnumAttributeAttribute : Attribute
	{
		private string _strAtt;

		public string StrAtt {
			get { return _strAtt; }
			set { _strAtt = value; }
		}

		public MESEnumAttributeAttribute (string att)
		{
			this._strAtt = att;
		}
	}

	[System.AttributeUsage (AttributeTargets.Event)]
	public class MESEventAttributeAttribute : Attribute
	{
		private string _strAtt;

		public string StrAtt {
			get { return _strAtt; }
			set { _strAtt = value; }
		}

		public MESEventAttributeAttribute (string att)
		{
			this._strAtt = att;
		}
	}

	[System.AttributeUsage (AttributeTargets.Field)]
	public class MESFieldAttributeAttribute : Attribute
	{
		private string _strAtt;

		public string StrAtt {
			get { return _strAtt; }
			set { _strAtt = value; }
		}

		public MESFieldAttributeAttribute (string att)
		{
			this._strAtt = att;
		}
	}

	[System.AttributeUsage (AttributeTargets.Interface)]
	public class MESInterfaceAttributeAttribute : Attribute
	{
		private string _strAtt;

		public string StrAtt {
			get { return _strAtt; }
			set { _strAtt = value; }
		}

		public MESInterfaceAttributeAttribute (string att)
		{
			this._strAtt = att;
		}
	}

	[System.AttributeUsage (AttributeTargets.Method)]
	public class MESMethodAttributeAttribute : Attribute
	{
		private string _strAtt;

		public string StrAtt {
			get { return _strAtt; }
			set { _strAtt = value; }
		}

		public MESMethodAttributeAttribute (string att)
		{
			this._strAtt = att;
		}
	}

	[System.AttributeUsage (AttributeTargets.Property)]
	public class MESPropertyAttributeAttribute : Attribute
	{
		private string _strAtt;

		public string StrAtt {
			get { return _strAtt; }
			set { _strAtt = value; }
		}

		public MESPropertyAttributeAttribute (string att)
		{
			this._strAtt = att;
		}

		private MESAttributeType _attType;

		public MESAttributeType AttType {
			get { return _attType; }
			set { _attType = value; }
		}

		private int _attOrder = 0;

		public int AttOrder {
			get { return _attOrder; }
			set { _attOrder = value; }
		}

		public MESPropertyAttributeAttribute (int attOrder, MESAttributeType attType)
		{
			this._attOrder = attOrder;
			this._attType = attType;
		}

		public MESPropertyAttributeAttribute (MESAttributeType attType)
		{
			this._attType = attType;
		}

		public MESPropertyAttributeAttribute (int attOrder)
		{
			this._attOrder = attOrder;
		}
	}

	[System.AttributeUsage (AttributeTargets.Struct)]
	public class MESStructAttributeAttribute : Attribute
	{
		private string _strAtt;

		public string StrAtt {
			get { return _strAtt; }
			set { _strAtt = value; }
		}

		public MESStructAttributeAttribute (string att)
		{
			this._strAtt = att;
		}
	}

	#endregion

	#region Multilanguage 多语言
	public class MESDescription : System.ComponentModel.DescriptionAttribute
	{
		public MESDescription (string descriptionKey)
		{
			try {
				string temp = GetStringFromPublicResourceClass.GetStringFromPublicResource (descriptionKey);
				if (temp != null && temp.Length > 0) {
					this.DescriptionValue = temp;
				} else {
					this.DescriptionValue = descriptionKey;
				}
			} catch (System.Exception) {
				this.DescriptionValue = descriptionKey;
			}

		}
	}

	public class MESDisplayName : System.ComponentModel.DisplayNameAttribute
	{
		public MESDisplayName (string displayNameKey)
		{
			try {
				string temp = GetStringFromPublicResourceClass.GetStringFromPublicResource (displayNameKey);
				if (temp != null && temp.Length > 0) {
					this.DisplayNameValue = temp;
				} else {
					this.DisplayNameValue = displayNameKey;
				}
			} catch (System.Exception) {
				this.DisplayNameValue = displayNameKey;
			}

		}
	}

	public class MESCategory : System.ComponentModel.CategoryAttribute
	{
		private string _category = null;

		public MESCategory (string categoryKey)
			: base (categoryKey)
		{
		}

		public new string Category {
			get { return GetLocalizedString (_category); }
		}

		protected override string GetLocalizedString (string value)
		{
			try {
				string temp = GetStringFromPublicResourceClass.GetStringFromPublicResource (value);
				if (temp != null && temp.Length > 0) {
					return temp;
				}
			} catch (System.Exception) {
				return value;
			}
			return value;
		}
	}

	public class MESCategoryOrderList : Attribute
	{
		public MESCategoryOrderList ()
		{

		}

		List<string> _OrderList = new List<string> ();
	}

	public class MESMultilanguage
	{
		public const string MESControlProp = "MES控件属性";

		public static string GetstringByLanguage (string str)
		{
			try {
				string temp = GetStringFromPublicResourceClass.GetStringFromPublicResource (str);
				if (temp != null && temp.Length > 0) {
					return temp;
				} else {
					return str;
				}
			} catch (System.Exception ex) {
				return str;
			}
		}
	}

	public class GlobalizedPropertyDescriptor : PropertyDescriptor
	{
		private PropertyDescriptor basePropertyDescriptor;

		public GlobalizedPropertyDescriptor (PropertyDescriptor basePropertyDescriptor)
			: base (basePropertyDescriptor)
		{
			this.basePropertyDescriptor = basePropertyDescriptor;
		}

		public override bool CanResetValue (object component)
		{
			return basePropertyDescriptor.CanResetValue (component);
		}

		public override Type ComponentType {
			get { return basePropertyDescriptor.ComponentType; }
		}

		public override string Category {
			get {
				string category = string.Empty;
				foreach (Attribute oAttrib in this.basePropertyDescriptor.Attributes) {
					if (oAttrib.GetType ().Equals (typeof(CategoryAttribute))) {
						category = ((CategoryAttribute)oAttrib).Category;
						return MESMultilanguage.GetstringByLanguage (category);
					}
				}
				return base.Category;
			}
		}

		public override string DisplayName {
			get {
				string displayName = string.Empty;
				foreach (Attribute oAttrib in this.basePropertyDescriptor.Attributes) {
					if (oAttrib.GetType ().Equals (typeof(DisplayNameAttribute))) {
						displayName = ((DisplayNameAttribute)oAttrib).DisplayName;
						return MESMultilanguage.GetstringByLanguage (displayName);
					}
				}
				return base.DisplayName;
			}
		}

		public override string Description {
			get {
				string description = string.Empty;
				foreach (Attribute oAttrib in this.basePropertyDescriptor.Attributes) {
					if (oAttrib.GetType ().Equals (typeof(DescriptionAttribute))) {
						description = ((DescriptionAttribute)oAttrib).Description;
						return MESMultilanguage.GetstringByLanguage (description);
					}
				}
				return base.DisplayName;
			}
		}

		public override object GetValue (object component)
		{
			return this.basePropertyDescriptor.GetValue (component);
		}

		public override bool IsReadOnly {
			get { return this.basePropertyDescriptor.IsReadOnly; }
		}

		public override string Name {
			get { return this.basePropertyDescriptor.Name; }
		}

		public override Type PropertyType {
			get { return this.basePropertyDescriptor.PropertyType; }
		}

		public override void ResetValue (object component)
		{
			this.basePropertyDescriptor.ResetValue (component);
		}

		public override bool ShouldSerializeValue (object component)
		{
			return this.basePropertyDescriptor.ShouldSerializeValue (component);
		}

		public override void SetValue (object component, object value)
		{
			this.basePropertyDescriptor.SetValue (component, value);
		}
	}

	[AttributeUsage (AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class GlobalizedPropertyAttribute : Attribute
	{
		private String resourceName = "";
		private String resourceDescription = "";
		private String resourceCategory = "";

		public GlobalizedPropertyAttribute ()
		{
		}

		public String Category {
			get { return resourceCategory; }
			set { resourceCategory = value; }
		}

		public String DisplayName {
			get { return resourceName; }
			set { resourceName = value; }
		}

		public String Description {
			get { return resourceDescription; }
			set { resourceDescription = value; }
		}

        

	}
	#endregion

	#region PropertyGrid 属性排序
	public class PropertySorterConverter : ExpandableObjectConverter
	{
		#region Methods

		public override bool GetPropertiesSupported (ITypeDescriptorContext context)
		{
			return true;
		}

		public override PropertyDescriptorCollection GetProperties (ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			//
			// This override returns a list of properties in order
			//
			PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties (value, attributes);
			ArrayList orderedProperties = new ArrayList ();
			foreach (PropertyDescriptor pd in pdc) {
				Attribute attribute = pd.Attributes [typeof(MESPropertyOrderAttribute)];
				if (attribute != null) {
					//
					// If the attribute is found, then create an pair object to hold it
					//
					MESPropertyOrderAttribute poa = (MESPropertyOrderAttribute)attribute;
					orderedProperties.Add (new PropertyOrderPair (pd.Name, poa.Order));
				} else {
					//
					// If no order attribute is specifed then given it an order of 0
					//
					orderedProperties.Add (new PropertyOrderPair (pd.Name, 0));
				}
			}
			//
			// Perform the actual order using the value PropertyOrderPair classes
			// implementation of IComparable to sort
			//
			orderedProperties.Sort ();
			//
			// Build a string list of the ordered names
			//
			ArrayList propertyNames = new ArrayList ();
			foreach (PropertyOrderPair pop in orderedProperties) {
				propertyNames.Add (pop.Name);
			}
			//
			// Pass in the ordered list for the PropertyDescriptorCollection to sort by
			//
			return pdc.Sort ((string[])propertyNames.ToArray (typeof(string)));
		}

		#endregion
	}

	#region Helper Class - PropertyOrderAttribute
	[AttributeUsage (AttributeTargets.Property)]
	public class MESPropertyOrderAttribute : Attribute
	{
		//
		// Simple attribute to allow the order of a property to be specified
		//
		private int _order;

		public MESPropertyOrderAttribute (int order)
		{
			_order = order;
		}

		public int Order {
			get {
				return _order;
			}
		}
	}
	#endregion

	#region Helper Class - PropertyOrderPair
	public class PropertyOrderPair : IComparable
	{
		private int _order;
		private string _name;

		public string Name {
			get {
				return _name;
			}
		}

		public PropertyOrderPair (string name, int order)
		{
			_order = order;
			_name = name;
		}

		public int CompareTo (object obj)
		{
			//
			// Sort the pair objects by ordering by order value
			// Equal values get the same rank
			//
			int otherOrder = ((PropertyOrderPair)obj)._order;
			if (otherOrder == _order) {
				//
				// If order not specified, sort by name
				//
				string otherName = ((PropertyOrderPair)obj)._name;
				return string.Compare (_name, otherName);
			} else if (otherOrder > _order) {
				return -1;
			}
			return 1;
		}
	}
	#endregion

	public class PropertyOrderComparer : IComparer
	{
		#region IComparer Member

		public int Compare (object x, object y)
		{
			PropertyDescriptor p1 = x as PropertyDescriptor;
			PropertyDescriptor p2 = y as PropertyDescriptor;

			MESPropertyOrderAttribute patt1 = null;
			MESPropertyOrderAttribute patt2 = null;

			Attribute att1 = p1.Attributes [typeof(MESPropertyOrderAttribute)];
			if (att1 != null) {
				patt1 = (MESPropertyOrderAttribute)att1;
			}

			Attribute att2 = p2.Attributes [typeof(MESPropertyOrderAttribute)];
			if (att2 != null) {
				patt2 = (MESPropertyOrderAttribute)att2;
			}

			if (patt1 == null && patt2 != null) {
				return -patt2.Order;
			} else if (patt1 != null && patt2 == null) {
				return patt1.Order;
			} else if ((patt1 == null && patt2 == null) || patt1.Order == patt2.Order) {
				return 0;
			} else {
				return CompareInt (patt1.Order, patt2.Order);
			}
		}

		/// <summary>
		/// 比较两个数字的大小
		/// </summary>
		/// <param name="ipx">要比较的第一个对象</param>
		/// <param name="ipy">要比较的第二个对象</param>
		/// <returns>比较的结果.如果相等返回0，如果x大于y返回1，如果x小于y返回-1</returns>
		private int CompareInt (int x, int y)
		{
			if (x > y) {
				return 1;
			} else if (x < y) {
				return -1;
			} else {
				return 0;
			}
		}

		#endregion
	}
	#endregion

	/// <summary>
	/// 显示变量配置接口
	/// </summary>
	public interface IVarConfig
	{
		DialogResult ShowVarConfigFrm ();
	}

	#region 工作流相关接口
	/// <summary>
	/// WorkFlow控件工作流设计模式接口
	/// </summary>
	public interface IMESWorkFlowControlInterface : IVarConfig
	{
		int NewWorkFlow (Guid wfid);

		int OpenWorkFlow (Guid wfid, bool bPublished);

	}

	/// <summary>
	/// WorkFlow控件自定义设计模式接口
	/// </summary>
	public interface IMESWorkFlowCustomControlInterface : IVarConfig
	{
		int NewWorkFlowCustom (Guid wfid);

		int OpenWorkFlowCustom (Guid wfid, bool bPublished);

		int OpenWorkFlowCustom (Guid wfid);

	}

	/// <summary>
	/// WorkFlow控件工作流监视模式接口
	/// </summary>
	public interface IMESWorkFlowMonitorControlInterface
	{
		int StartWorkFlowMonitor (Guid wf_instance_id);
	}

	public interface IMESWorkFlowMonitorInterface
	{
		int StartWorkFlowInstanceMonitor (string formname, Guid wf_instance_id);

		int StartWorkFlowMonitor (string formname, Guid wf_id);
	}

	public interface IMESWorkFlowManagerInterface
	{
		int NewWorkFlow (string formname, Guid wfid, bool bOpen = true);

		int OpenWorkFlow (string formname, Guid wfid, bool bPublished, bool bReadOnly = false);

		int DeleteWorkFlow (List<Guid> wfidlist);

		int SaveWorkFlow (List<Guid> wfidlist);
	}

	public interface IMESWorkFlowCustomInterface
	{
		int NewWorkFlowCustom (string formname, Guid wfid, bool bOpen = true);

		int OpenWorkFlowCustom (string formname, Guid wfid, bool bPublished, bool bReadOnly = false);

		int DeleteWorkFlowCustom (List<Guid> wfidlist);

		int SaveWorkFlowCustom (List<Guid> wfidlist);
	}

	public class MESWorkFlowManagerObj : IMESWorkFlowManagerInterface, IMESWorkFlowMonitorInterface, IMESWorkFlowCustomInterface
	{
		public int NewWorkFlow (string formname, Guid wfid, bool bOpen = true)
		{
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_NEWWORKFLOWFORM;
			byte[] theBytes = PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.ObjToByte (new List<string> {
				formname,
				wfid.ToString (),
				bOpen.ToString ()
			});
			if (PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle != IntPtr.Zero)
				return PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendCopyData (PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle, msgID, theBytes);
			return -1;
		}

		public int OpenWorkFlow (string formname, Guid wfid, bool bPublished, bool bReadOnly = false)
		{
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_OPENWORKFLOWFORM;
			byte[] theBytes = PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.ObjToByte (new List<string> {
				formname,
				wfid.ToString (),
				bPublished.ToString (),
				bReadOnly.ToString ()
			});
			if (PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle != IntPtr.Zero)
				return PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendCopyData (PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle, msgID, theBytes);

			return -1;
		}

		public int DeleteWorkFlow (List<Guid> wfidlist)
		{
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_DELETEWORKFLOWFORM;
			byte[] theBytes = PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.ObjToByte (wfidlist);
			if (PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle != IntPtr.Zero)
				return PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendCopyData (PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle, msgID, theBytes);

			return -1;
		}

		public int SaveWorkFlow (List<Guid> wfidlist)
		{
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_SAVEWORKFLOWFORM;
			byte[] theBytes = PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.ObjToByte (wfidlist);
			if (PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle != IntPtr.Zero)
				return PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendCopyData (PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle, msgID, theBytes);

			return -1;
		}

		public int StartWorkFlowInstanceMonitor (string formname, Guid wf_instance_id)
		{
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_STARTWORKFLOWINSTANCEMONITOR;
			byte[] theBytes = PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.ObjToByte (new List<string> {
				formname,
				wf_instance_id.ToString ()
			});
			if (PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle != IntPtr.Zero)
				return PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendCopyData (PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle, msgID, theBytes);

			return -1;
		}

		public int StartWorkFlowMonitor (string formname, Guid wf_id)
		{
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_STARTWORKFLOWMONITOR;
			byte[] theBytes = PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.ObjToByte (new List<string> {
				formname,
				wf_id.ToString ()
			});
			if (PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle != IntPtr.Zero)
				return PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendCopyData (PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle, msgID, theBytes);

			return -1;
		}

		public int NewWorkFlowCustom (string formname, Guid wfid, bool bOpen = true)
		{
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_NEWWORKFLOWCUSTOM;
			byte[] theBytes = PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.ObjToByte (new List<string> {
				formname,
				wfid.ToString (),
				bOpen.ToString ()
			});
			if (PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle != IntPtr.Zero)
				return PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendCopyData (PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle, msgID, theBytes);
            
			return -1;
		}

		public int OpenWorkFlowCustom (string formname, Guid wfid, bool bPublished, bool bReadOnly = false)
		{
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_OPENWORKFLOWCUSTOM;
			byte[] theBytes = PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.ObjToByte (new List<string> {
				formname,
				wfid.ToString (),
				bPublished.ToString (),
				bReadOnly.ToString ()
			});
			if (PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle != IntPtr.Zero)
				return PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendCopyData (PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle, msgID, theBytes);

			return -1;
		}

		public int OpenWorkFlowCustom (string formname, Guid wfid)
		{
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_OPENWORKFLOWCUSTOM_P;
			byte[] theBytes = PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.ObjToByte (new List<string> {
				formname,
				wfid.ToString ()
			});
			if (PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle != IntPtr.Zero)
				return PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendCopyData (PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle, msgID, theBytes);

			return -1;
		}

		public int DeleteWorkFlowCustom (List<Guid> wfidlist)
		{
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_DELETEWORKFLOWCUSTOM;
			byte[] theBytes = PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.ObjToByte (wfidlist);
			if (PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle != IntPtr.Zero)
				return PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendCopyData (PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle, msgID, theBytes);

			return -1;
		}

		public int SaveWorkFlowCustom (List<Guid> wfidlist)
		{
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_SAVEWORKFLOWCUSTOM;
			byte[] theBytes = PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.ObjToByte (wfidlist);
			if (PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle != IntPtr.Zero)
				return PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendCopyData (PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle, msgID, theBytes);

			return -1;
		}

		public int CloseDoc (List<Guid> wfidlist)
		{
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_CLOSEDOC;
			byte[] theBytes = PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.ObjToByte (wfidlist);
			if (PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle != IntPtr.Zero)
				return PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendCopyData (PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle, msgID, theBytes);

			return -1;
		}
        
	}

	#endregion

	public class Win32DC
	{
		public static int GetDpix ()
		{
			//SetProcessDPIAware();  //重要
			IntPtr screenDC = GetDC (IntPtr.Zero);
			int dpi_x = GetDeviceCaps (screenDC, /*DeviceCap.*/LOGPIXELSX);
			//_scaleUI.X = dpi_x / 96.0;
			//_scaleUI.Y = dpi_y / 96.0;
			ReleaseDC (IntPtr.Zero, screenDC);
			return dpi_x;
		}

		public static int GetDpiy ()
		{
			//SetProcessDPIAware();  //重要
			IntPtr screenDC = GetDC (IntPtr.Zero);
			int dpi_y = GetDeviceCaps (screenDC, /*DeviceCap.*/LOGPIXELSY);
			//_scaleUI.X = dpi_x / 96.0;
			//_scaleUI.Y = dpi_y / 96.0;
			ReleaseDC (IntPtr.Zero, screenDC);
			return dpi_y;
		}

		[DllImport ("user32.dll")]
		static extern IntPtr GetDC (IntPtr ptr);

		[DllImport ("user32.dll", EntryPoint = "ReleaseDC")]
		public static extern IntPtr ReleaseDC (IntPtr hWnd, IntPtr hDc);

		[DllImport ("gdi32.dll")]
		public static extern IntPtr CreateDC (
			string lpszDriver, // driver name
			string lpszDevice, // device name
			string lpszOutput, // not used; should be NULL
			Int64 lpInitData // optional printer data
		);

		[DllImport ("gdi32.dll")]
		public static extern int GetDeviceCaps (
			IntPtr hdc, // handle to DC
			int nIndex // index of capability
		);

		[DllImport ("user32.dll")]
		internal static extern bool SetProcessDPIAware ();

		const int DRIVERVERSION = 0;
		const int TECHNOLOGY = 2;
		const int HORZSIZE = 4;
		const int VERTSIZE = 6;
		const int HORZRES = 8;
		const int VERTRES = 10;
		const int BITSPIXEL = 12;
		const int PLANES = 14;
		const int NUMBRUSHES = 16;
		const int NUMPENS = 18;
		const int NUMMARKERS = 20;
		const int NUMFONTS = 22;
		const int NUMCOLORS = 24;
		const int PDEVICESIZE = 26;
		const int CURVECAPS = 28;
		const int LINECAPS = 30;
		const int POLYGONALCAPS = 32;
		const int TEXTCAPS = 34;
		const int CLIPCAPS = 36;
		const int RASTERCAPS = 38;
		const int ASPECTX = 40;
		const int ASPECTY = 42;
		const int ASPECTXY = 44;
		const int SHADEBLENDCAPS = 45;
		const int LOGPIXELSX = 88;
		const int LOGPIXELSY = 90;
		const int SIZEPALETTE = 104;
		const int NUMRESERVED = 106;
		const int COLORRES = 108;
		const int PHYSICALWIDTH = 110;
		const int PHYSICALHEIGHT = 111;
		const int PHYSICALOFFSETX = 112;
		const int PHYSICALOFFSETY = 113;
		const int SCALINGFACTORX = 114;
		const int SCALINGFACTORY = 115;
		const int VREFRESH = 116;
		const int DESKTOPVERTRES = 117;
		const int DESKTOPHORZRES = 118;
		const int BLTALIGNMENT = 119;
	}

	[Serializable]
	public class MESClipboardObj
	{
		public Type Type {
			get;
			set;
		}

		public string Name {
			get;
			set;
		}

		public DateTime ClipboardTime {
			get;
			set;
		}

		public object Obj {
			get;
			set;
		}
	}

	public class MESClipboardObjManager
	{
		private static List<MESClipboardObj> _MESClipboardObjList = new List<MESClipboardObj> ();

		[DefaultValue (null)]
		public static object CurrentObj {
			get;
			set;
		}

		public static MESClipboardObj GetClipboardObj (int index)
		{
			if (_MESClipboardObjList.Count () > index)
				return _MESClipboardObjList.ElementAt (index);

			return null;
		}

		/// <summary>
		/// 获取粘贴对象
		/// </summary>
		/// <returns></returns>
		public static object GetPasteObj ()
		{
			if (CurrentObj != null)
				return CurrentObj;
			return GetRecentlyClipboardObj ();
		}

		/// <summary>
		/// 直接粘贴时使用，取最近一次复制或者剪切的对象
		/// </summary>
		/// <returns></returns>
		private static object GetRecentlyClipboardObj ()
		{
			if (_MESClipboardObjList.Count () > 0)
				return _MESClipboardObjList.Last ().Obj;

			return null;
		}

		public static int AddToClipboardManager (object obj)
		{
			if (!obj.GetType ().IsSerializable)
				return -1;
			MESClipboardObj clipobj = new MESClipboardObj ();
			clipobj.ClipboardTime = DateTime.Now;
			clipobj.Type = obj.GetType ();
			clipobj.Name = obj.ToString ();
			clipobj.Obj = obj;
			_MESClipboardObjList.Add (clipobj);
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_ADDTOCLIPBOARDMANAGER;
			byte[] theBytes = PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.ObjToByte (clipobj);
			if (PMS.Libraries.ToolControls.PMSPublicInfo.Message.ClipboardFormWindowHandle != IntPtr.Zero)
				PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendCopyData (PMS.Libraries.ToolControls.PMSPublicInfo.Message.ClipboardFormWindowHandle, msgID, theBytes);
			return 1;
		}

		private static int RemoveFromClipboardManager (int index)
		{
			_MESClipboardObjList.RemoveAt (index);
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_REMOVEFROMCLIPBOARDMANAGER;
			if (PMS.Libraries.ToolControls.PMSPublicInfo.Message.ClipboardFormWindowHandle != IntPtr.Zero)
				PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (PMS.Libraries.ToolControls.PMSPublicInfo.Message.ClipboardFormWindowHandle, msgID, IntPtr.Zero, (IntPtr)index);
			return 1;
		}

		private static int RemoveFromClipboardManager (object obj)
		{
			if (!obj.GetType ().IsSerializable)
				return -1;
			foreach (MESClipboardObj ob in _MESClipboardObjList) {
				if (ob.Obj.Equals (obj)) {
					_MESClipboardObjList.Remove (ob);
					int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_REMOVEFROMCLIPBOARDMANAGER;
					byte[] theBytes = PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.ObjToByte (ob);
					if (PMS.Libraries.ToolControls.PMSPublicInfo.Message.ClipboardFormWindowHandle != IntPtr.Zero)
						PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendCopyData (PMS.Libraries.ToolControls.PMSPublicInfo.Message.ClipboardFormWindowHandle, msgID, theBytes);
					return 1;
				}
			}
            
			return 0;
		}
	}

	public delegate void SayHandler (string word);

	#region FastReflect
	public delegate object FastInvokeHandler (object target,object[] paramters);

	public class FastReflect
	{
		public static FastInvokeHandler GetMethodInvoker (MethodInfo methodInfo)
		{
			DynamicMethod dynamicMethod = new DynamicMethod (string.Empty, typeof(object), new Type[] {
				typeof(object),
				typeof(object[])
			}, methodInfo.DeclaringType.Module);
			ILGenerator il = dynamicMethod.GetILGenerator ();
			ParameterInfo[] ps = methodInfo.GetParameters ();
			Type[] paramTypes = new Type[ps.Length];
			for (int i = 0; i < paramTypes.Length; i++) {
				if (ps [i].ParameterType.IsByRef)
					paramTypes [i] = ps [i].ParameterType.GetElementType ();
				else
					paramTypes [i] = ps [i].ParameterType;
			}
			LocalBuilder[] locals = new LocalBuilder[paramTypes.Length];

			for (int i = 0; i < paramTypes.Length; i++) {
				locals [i] = il.DeclareLocal (paramTypes [i], true);
			}
			for (int i = 0; i < paramTypes.Length; i++) {
				il.Emit (OpCodes.Ldarg_1);
				EmitFastInt (il, i);
				il.Emit (OpCodes.Ldelem_Ref);
				EmitCastToReference (il, paramTypes [i]);
				il.Emit (OpCodes.Stloc, locals [i]);
			}
			if (!methodInfo.IsStatic) {
				il.Emit (OpCodes.Ldarg_0);
			}
			for (int i = 0; i < paramTypes.Length; i++) {
				if (ps [i].ParameterType.IsByRef)
					il.Emit (OpCodes.Ldloca_S, locals [i]);
				else
					il.Emit (OpCodes.Ldloc, locals [i]);
			}
			if (methodInfo.IsStatic)
				il.EmitCall (OpCodes.Call, methodInfo, null);
			else
				il.EmitCall (OpCodes.Callvirt, methodInfo, null);
			if (methodInfo.ReturnType == typeof(void))
				il.Emit (OpCodes.Ldnull);
			else
				EmitBoxIfNeeded (il, methodInfo.ReturnType);

			for (int i = 0; i < paramTypes.Length; i++) {
				if (ps [i].ParameterType.IsByRef) {
					il.Emit (OpCodes.Ldarg_1);
					EmitFastInt (il, i);
					il.Emit (OpCodes.Ldloc, locals [i]);
					if (locals [i].LocalType.IsValueType)
						il.Emit (OpCodes.Box, locals [i].LocalType);
					il.Emit (OpCodes.Stelem_Ref);
				}
			}

			il.Emit (OpCodes.Ret);
			FastInvokeHandler invoder = (FastInvokeHandler)dynamicMethod.CreateDelegate (typeof(FastInvokeHandler));
			return invoder;
		}

		private static void EmitCastToReference (ILGenerator il, System.Type type)
		{
			if (type.IsValueType) {
				il.Emit (OpCodes.Unbox_Any, type);
			} else {
				il.Emit (OpCodes.Castclass, type);
			}
		}

		private static void EmitBoxIfNeeded (ILGenerator il, System.Type type)
		{
			if (type.IsValueType) {
				il.Emit (OpCodes.Box, type);
			}
		}

		private static void EmitFastInt (ILGenerator il, int value)
		{
			switch (value) {
			case -1:
				il.Emit (OpCodes.Ldc_I4_M1);
				return;
			case 0:
				il.Emit (OpCodes.Ldc_I4_0);
				return;
			case 1:
				il.Emit (OpCodes.Ldc_I4_1);
				return;
			case 2:
				il.Emit (OpCodes.Ldc_I4_2);
				return;
			case 3:
				il.Emit (OpCodes.Ldc_I4_3);
				return;
			case 4:
				il.Emit (OpCodes.Ldc_I4_4);
				return;
			case 5:
				il.Emit (OpCodes.Ldc_I4_5);
				return;
			case 6:
				il.Emit (OpCodes.Ldc_I4_6);
				return;
			case 7:
				il.Emit (OpCodes.Ldc_I4_7);
				return;
			case 8:
				il.Emit (OpCodes.Ldc_I4_8);
				return;
			}

			if (value > -129 && value < 128) {
				il.Emit (OpCodes.Ldc_I4_S, (SByte)value);
			} else {
				il.Emit (OpCodes.Ldc_I4, value);
			}
		}
	}
	#endregion

	#region -控制将序列化对象绑定到类型的过程

	/// <summary>
	/// 二进制序列化的对象的类型或者Assembly变更兼容处理
	/// </summary>
	public class UBinder : SerializationBinder
	{
		public override Type BindToType (string assemblyName, string typeName)
		{
			//Assembly ass = Assembly.GetExecutingAssembly();
			Assembly ass = Assembly.Load (assemblyName);
			Type tp = ass.GetType (typeName);
			if (null == tp) {
				tp = Type.GetType (typeName);
				if (null == tp) {
					#region Form
					if (typeName.Contains ("MES.FormLib.Controls.IBOHoster")) {
						tp = Type.GetType (typeName.Replace (", MES.FormLib,", ", MES.DbDriver,"));
						if (null == tp) {
							ass = Assembly.LoadFrom (GetAssemblePath () + System.IO.Path.DirectorySeparatorChar + @"MES.DbDriver.dll");
							tp = ass.GetType ("MES.FormLib.Data.IBOHoster");
						}
					} else if (typeName.Contains ("MES.FormLib.Data") || typeName.Contains ("MES.FormLib.SQL")) {
						tp = Type.GetType (typeName.Replace (", MES.FormLib,", ", MES.DbDriver,"));
						if (null == tp) {
							ass = Assembly.LoadFrom (GetAssemblePath () + System.IO.Path.DirectorySeparatorChar + @"MES.DbDriver.dll");
							tp = ass.GetType (typeName.Replace (", MES.FormLib,", ", MES.DbDriver,"));
						}
					}

                    #endregion

                    #region MESOrgChart

                    else if (typeName.Contains ("PMS.Libraries.ToolControls.OrgChart.SingleTableFlowChart.DS") ||
					                        typeName.Contains ("PMS.Libraries.ToolControls.OrgChart.NodeModeAgent")) {
						tp = Type.GetType (typeName.Replace (", MES.OrgChart,", ", MES.DataModel,"));
						if (null == tp) {
							ass = Assembly.LoadFrom (System.IO.Path.Combine (GetAssemblePath (), @"MES.DataModel.dll"));
							tp = ass.GetType (typeName.Replace (", MES.OrgChart,", ", MES.DataModel,"));
						}
					}

                    #endregion

                    #region Report

                    else if (typeName.Contains ("PMS.Libraries.ToolControls.PMSReport.PMSPrintPara")) {
						tp = Type.GetType (typeName.Replace (", PMS.PMSReport,", ", PMS.ReportControls,"));
						if (null == tp) {
							ass = Assembly.LoadFrom (System.IO.Path.Combine (GetAssemblePath (), @"PMS.ReportControls.dll"));
							tp = ass.GetType (typeName.Replace (", PMS.PMSReport,", ", PMS.ReportControls,"));
						}
					} else if (typeName.Contains ("PMS.Libraries.ToolControls.PMSReport.ReportViewerToolBar")) {
						tp = Type.GetType (typeName.Replace (", PMS.PMSReport,", ", PMS.ReportControls,"));
						if (null == tp) {
							ass = Assembly.LoadFrom (System.IO.Path.Combine (GetAssemblePath (), @"PMS.ReportControls.dll"));
							tp = ass.GetType (typeName.Replace (", PMS.PMSReport,", ", PMS.ReportControls,"));
						}
					} else if (typeName.Contains ("PMS.Libraries.ToolControls.PMSReport.SourceFieldDataField")) {
						tp = Type.GetType (typeName.Replace (", PMS.PMSReport,", ", PMS.PmsPublicData,"));
						if (null == tp) {
							ass = Assembly.LoadFrom (System.IO.Path.Combine (GetAssemblePath (), @"PMS.PmsPublicData.dll"));
							tp = ass.GetType ("PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceFieldDataField");
						}
					} else if (typeName.Contains ("PMS.Libraries.ToolControls.PMSReport.SourceFieldDataTable")) {
						tp = Type.GetType (typeName.Replace (", PMS.PMSReport,", ", PMS.PmsPublicData,"));
						if (null == tp) {
							ass = Assembly.LoadFrom (System.IO.Path.Combine (GetAssemblePath (), @"PMS.PmsPublicData.dll"));
							tp = ass.GetType ("PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceFieldDataTable");
						}
					}
					#endregion
				}
			}
			return tp;
		}

		public static string GetAssemblePath ()
		{
			return System.IO.Path.GetDirectoryName (System.Reflection.Assembly.GetExecutingAssembly ().Location);
		}
	}

	/// <summary>
	/// 控制将序列化对象绑定到类型的过程
	/// </summary>
	public sealed class ConvertAssemblyBinder : SerializationBinder
	{
		/// <summary>
		/// 控制二进制序列化类型转换
		/// 在序列化过程中，格式化程序传输创建正确类型和版本的对象的实例所需的信息。
		/// 此信息通常包括对象的完整类型名称和程序集名称。
		/// 程序集名称包含程序集的名称、版本和强名称（请参见具有强名称的程序集）散列。
		/// 默认情况下，反序列化将使用此信息创建等同对象的实例（安全策略所限制的任何程序集加载属于例外）。
		/// 因为类已经在程序集之间移动或者因为在服务器和客户端上要求类的不同版本，有些用户需要控制要加载哪些类。
		/// 这是一个抽象基类。所有联编程序都扩展此类。
		/// 给继承者的说明 从 SerializationBinder 继承时，必须重写以下成员：BindToType。 
		/// 由于服务器和客户端共同使用同一个类型,进行数据传递,但是服务器和客户端的同一类型处于不同的
		/// 程序集,需要在反序列化的时候,提供强制类型转换
		/// </summary>
		/// <param name="assemblyName"></param>
		/// <param name="typeName"></param>
		/// <returns></returns>
		public override Type BindToType (string assemblyName, string typeName)
		{
			try {
				// 取得当前的程序集名称,取得传递过来,类型名称不修改,都是"TestPro.CSFTcpMsg"
				string strCurAssembly = assemblyName.Replace ("PublicKeyToken=6f26a2b4b031fc89", "PublicKeyToken=null");
				string strCurTypeName = typeName.Replace ("PublicKeyToken=6f26a2b4b031fc89", "PublicKeyToken=null");

				// 返回当前对应的类型
				//Assembly ass = Assembly.Load(strCurAssembly);
				//return ass.GetType(strCurTypeName);
				Type CurType = Type.GetType (String.Format ("{0}, {1}", strCurTypeName, strCurAssembly));
				if (null == CurType) {

				}
				return CurType;
			} catch (System.Exception ex) {
				return null;
			}
            
		}

		//public void BindToName(Type serializedType, out string assemblyName, out string typeName)
		//{
		//    assemblyName = "";
		//    typeName = "";
		//}
	}

	/// <summary>
	/// This class can manually serialize an Employee object.
	/// </summary>
	public sealed class EmployeeSerializationSurrogate : ISerializationSurrogate
	{

		// Serialize the Employee object to save the object�s name and address fields.
		public void GetObjectData (Object obj,
		                           SerializationInfo info, StreamingContext context)
		{

			PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.PageData emp = (PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.PageData)obj;
			//info.AddValue("name", emp.name);
			//info.AddValue("address", emp.address);
		}

		// Deserialize the Employee object to set the object�s name and address fields.
		public Object SetObjectData (Object obj,
		                             SerializationInfo info, StreamingContext context,
		                             ISurrogateSelector selector)
		{

			PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.FieldTreeViewData emp = (PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.FieldTreeViewData)obj;
			//emp.TableName = info.GetString("TableName");
			SerializationInfoEnumerator sie = info.GetEnumerator ();
			while (sie.MoveNext ()) {
				PropertyInfo pi = emp.GetType ().GetProperty (sie.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetField | BindingFlags.SetProperty);
				if (null != pi)
					pi.SetValue (emp, sie.Value, null);
				else {
					FieldInfo field = emp.GetType ().GetField (sie.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetField | BindingFlags.SetProperty);
					if (null != field)
						field.SetValue (emp, sie.Value);
				}
			}
			//PublicObject.RvFileObj = emp;
			return emp;
		}
	}

	#endregion

	/// <summary>
	/// xml 文档操作的摘要说明。
	/// </summary>
	public class XML
	{
		public enum enumXmlPathType
		{
			AbsolutePath,
			VirtualPath
		}

		private string xmlFilePath;
		private enumXmlPathType xmlFilePathType;
		private XmlDocument xmlDoc = new XmlDocument ();

		public string XmlFilePath {
			set {
				xmlFilePath = value;
			}
		}

		public enumXmlPathType XmlFilePathType {
			set {
				xmlFilePathType = value;
			}
		}

		public XML (string tempXmlFilePath)
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			this.xmlFilePathType = enumXmlPathType.VirtualPath;
			this.xmlFilePath = tempXmlFilePath;
			GetXmlDocument ();
			//xmlDoc.Load( xmlFilePath ) ;
		}

		public XML (string tempXmlFilePath, enumXmlPathType tempXmlFilePathType)
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			this.xmlFilePathType = tempXmlFilePathType;
			this.xmlFilePath = tempXmlFilePath;
			GetXmlDocument ();
		}

		/// </summary>
		/// <param name="strEntityTypeName">实体类的名称</param>
		/// <returns>指定的XML描述文件的路径</returns>
		private XmlDocument GetXmlDocument ()
		{
			XmlDocument doc = null;
			if (this.xmlFilePathType == enumXmlPathType.AbsolutePath) {
				doc = GetXmlDocumentFromFile (xmlFilePath);
			} else if (this.xmlFilePathType == enumXmlPathType.VirtualPath) {
				doc = GetXmlDocumentFromFile (HttpContext.Current.Server.MapPath (xmlFilePath));
			}
			return doc;
		}

		private XmlDocument GetXmlDocumentFromFile (string tempXmlFilePath)
		{
			string xmlFileFullPath = tempXmlFilePath;
			xmlDoc.Load (xmlFileFullPath);
			return xmlDoc;
		}

		#region 读取指定节点的指定属性值

		/// <summary>
		/// 功能:
		/// 读取指定节点的指定属性值
		/// 
		/// 参数:
		/// 参数一:节点名称
		/// 参数二:此节点的属性
		/// </summary>
		/// <param name="strNode"></param>
		/// <param name="strAttribute"></param>
		/// <returns></returns>
		public string GetXmlNodeValue (string strNode, string strAttribute)
		{
			string strReturn = "";
			try {
				//根据指定路径获取节点
				XmlNode xmlNode = xmlDoc.SelectSingleNode (strNode);            
				//获取节点的属性，并循环取出需要的属性值
				XmlAttributeCollection xmlAttr = xmlNode.Attributes;
				for (int i = 0; i < xmlAttr.Count; i++) {
					if (xmlAttr.Item (i).Name == strAttribute)
						strReturn = xmlAttr.Item (i).Value;
				}
			} catch (XmlException xmle) {
				throw xmle;
			}
			return strReturn;
		}

		#endregion

		#region

		/// <summary>
		/// 功能:
		/// 读取指定节点的值
		/// 
		/// 参数:
		/// 参数:节点名称
		/// </summary>
		/// <param name="strNode"></param>
		/// <returns></returns>
		public string GetXmlNodeValue (string strNode)
		{
			string strReturn = String.Empty;
			try {
				//根据路径获取节点
				XmlNode xmlNode = xmlDoc.SelectSingleNode (strNode);
				strReturn = xmlNode.InnerText;
			} catch (XmlException xmle) {
				System.Console.WriteLine (xmle.Message);
			}
			return strReturn;
		}

		#endregion

		#region 设置节点值

		/**/
		/// <summary>
		/// 功能:
		/// 设置节点值
		/// 
		/// 参数:
		///    参数一:节点的名称
		///    参数二:节点值
		///    
		/// </summary>
		/// <param name="strNode"></param>
		/// <param name="newValue"></param>
		public void SetXmlNodeValue (string xmlNodePath, string xmlNodeValue)
		{
			try {
				//根据指定路径获取节点
				XmlNode xmlNode = xmlDoc.SelectSingleNode (xmlNodePath);            
				//设置节点值
				xmlNode.InnerText = xmlNodeValue;
			} catch (XmlException xmle) {
				throw xmle;
			}
		}

		#endregion

		#region 设置节点的属性值

		/**/
		/// <summary>
		/// 功能:
		/// 设置节点的属性值
		/// 
		/// 参数:
		/// 参数一:节点名称
		/// 参数二:属性名称
		/// 参数三:属性值
		/// 
		/// </summary>
		/// <param name="xmlNodePath"></param>
		/// <param name="xmlNodeAttribute"></param>
		/// <param name="xmlNodeAttributeValue"></param>
		public void SetXmlNodeValue (string xmlNodePath, string xmlNodeAttribute, string xmlNodeAttributeValue)
		{
			try {
				//根据指定路径获取节点
				XmlNode xmlNode = xmlDoc.SelectSingleNode (xmlNodePath);
            
				//获取节点的属性，并循环取出需要的属性值
				XmlAttributeCollection xmlAttr = xmlNode.Attributes;
				for (int i = 0; i < xmlAttr.Count; i++) {
					if (xmlAttr.Item (i).Name == xmlNodeAttribute) {
						xmlAttr.Item (i).Value = xmlNodeAttributeValue;
						break;
					}
				}
			} catch (XmlException xmle) {
				throw xmle;
			}
		}

		#endregion

		/// <summary>
		/// 获取XML文件的根元素
		/// </summary>
		public XmlNode GetXmlRoot ()
		{
			return xmlDoc.DocumentElement;
		}

		/// <summary>
		/// 在根节点下添加父节点
		/// </summary>
		public void AddParentNode (string parentNode)
		{
			XmlNode root = GetXmlRoot ();
			XmlNode parentXmlNode = xmlDoc.CreateElement (parentNode);
			root.AppendChild (parentXmlNode);
		}

		/// <summary>
		/// 向一个已经存在的父节点中插入一个子节点
		/// </summary>
		public void AddChildNode (string parentNodePath, string childNodePath)
		{
			XmlNode parentXmlNode = xmlDoc.SelectSingleNode (parentNodePath);
			XmlNode childXmlNode = xmlDoc.CreateElement (childNodePath);
			parentXmlNode.AppendChild (childXmlNode);
		}

		/// <summary>
		/// 向一个节点添加属性
		/// </summary>
		public void AddAttribute (string NodePath, string NodeAttribute)
		{
			XmlAttribute nodeAttribute = xmlDoc.CreateAttribute (NodeAttribute);
			XmlNode nodePath = xmlDoc.SelectSingleNode (NodePath);
			nodePath.Attributes.Append (nodeAttribute);
		}

		/// <summary>
		/// 删除一个节点的属性
		/// </summary>
		public void DeleteAttribute (string NodePath, string NodeAttribute, string NodeAttributeValue)
		{
			XmlNodeList nodePath = xmlDoc.SelectSingleNode (NodePath).ChildNodes;
			foreach (XmlNode xn in nodePath) {
				XmlElement xe = (XmlElement)xn;
				if (xe.GetAttribute (NodeAttribute) == NodeAttributeValue) {
					xe.RemoveAttribute (NodeAttribute);//删除属性
				}
			}
		}

		/// <summary>
		/// 删除一个节点
		/// </summary>
		public void DeleteXmlNode (string tempXmlNode)
		{
			//XmlNodeList xmlNodePath=xmlDoc.SelectSingleNode(tempXmlNode).ChildNodes;
			XmlNode xmlNodePath = xmlDoc.SelectSingleNode (tempXmlNode);
			xmlNodePath.ParentNode.RemoveChild (xmlNodePath);
			//foreach(XmlNode xn in xmlNodePath)
			//{
			//XmlElement xe=(XmlElement)xn;
			//xe.RemoveAll();
			//xe.RemoveChild(xn);
			//xn.RemoveAll();
			//if(xe.HasChildNodes)
			//{
			//foreach(XmlNode xn in xe)
			//{
			//xn.RemoveAll();//删除所有子节点和属性
			//}
			//}
			//}
		}

		#region 保存XML文件

		/**/
		/// <summary>
		/// 功能: 
		/// 保存XML文件
		/// 
		/// </summary>
		public void SaveXmlDocument ()
		{
			try {
				//保存设置的结果
				xmlDoc.Save (HttpContext.Current.Server.MapPath (xmlFilePath));
			} catch (XmlException xmle) {
				throw xmle;
			}
		}

		#endregion

		#region 保存XML文件

		/**/
		/// <summary>
		/// 功能: 
		/// 保存XML文件
		/// 
		/// </summary>
		public void SaveXmlDocument (string tempXMLFilePath)
		{
			try {
				//保存设置的结果
				xmlDoc.Save (tempXMLFilePath);
			} catch (XmlException xmle) {
				throw xmle;
			}
		}

		#endregion
	}

	#region 报表保存的序列化对象
	[Serializable]
	public class MESReportFileObj : ISerializable, IDisposable
	{
		public int iVersion = 1;

		public Guid identity = Guid.NewGuid ();

		public string strXMLDoc = string.Empty;

		public PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.FieldTreeViewData dataSource = null;

		public MESReportFileObj ()
		{
		}

		public void Dispose ()
		{
			if (null != dataSource) {
				dataSource.Dispose ();
				dataSource = null;
			}
		}

		protected MESReportFileObj (SerializationInfo info, StreamingContext context)
		{
			iVersion = info.GetInt32 ("iVersion");
			identity = (Guid)(info.GetValue ("identity", identity.GetType ()));
			strXMLDoc = info.GetString ("strXMLDoc");
			dataSource = (PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.FieldTreeViewData)(info.GetValue ("dataSource", typeof(PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.FieldTreeViewData)));
		}

		[SecurityPermissionAttribute (SecurityAction.Demand, SerializationFormatter = true)]
		public virtual void GetObjectData (SerializationInfo info, StreamingContext context)
		{
			info.AddValue ("iVersion", iVersion);
			info.AddValue ("identity", identity);
			info.AddValue ("strXMLDoc", strXMLDoc);
			info.AddValue ("dataSource", dataSource);
		}
	}
	#endregion

	#region 脚本保存序列化对象
	[Serializable]
	public class ScriptObject
	{
		public ScriptLanguage _scriptLanguage;
		public ArrayList ProjectincludeArrayList;

		public string SourceCode;
		public string SysCode;
	}
	#endregion

	#region 表单保存的序列化对象
	/// <summary>
	/// iVersion = 2 增加sco成员
	/// </summary>
	[Serializable]
	public class MESFormFileObj : ISerializable
	{
		// iVersion = 1

		public int iVersion = 2;

		public Guid identity = Guid.NewGuid ();

		public string strXMLDoc = string.Empty;

		// iVersion >=2
		public ScriptObject sco = null;

		public MESFormFileObj ()
		{
		}

		protected MESFormFileObj (SerializationInfo info, StreamingContext context)
		{
			int iV = info.GetInt32 ("iVersion");
			identity = (Guid)(info.GetValue ("identity", identity.GetType ()));
			strXMLDoc = info.GetString ("strXMLDoc");
			if (iV >= 2)
				sco = info.GetValue ("sco", typeof(ScriptObject)) as ScriptObject;
		}

		[SecurityPermissionAttribute (SecurityAction.Demand, SerializationFormatter = true)]
		public virtual void GetObjectData (SerializationInfo info, StreamingContext context)
		{
			info.AddValue ("iVersion", iVersion);
			info.AddValue ("identity", identity);
			info.AddValue ("strXMLDoc", strXMLDoc);
			info.AddValue ("sco", sco);
		}
	}
	#endregion

	#region Drpt保存的序列化对象
	[Serializable]
	public class MESDrptFileObj : ISerializable
	{
		public int iVersion = 1;

		public Guid identity = Guid.NewGuid ();

		public MESReportFileObj rptObj = new MESReportFileObj ();

		public string mapFileXml = string.Empty;

		public DataSet ds = new DataSet ();

		public DataSet filterds = new DataSet ();

		public MESDrptFileObj ()
		{
		}

		protected MESDrptFileObj (SerializationInfo info, StreamingContext context)
		{
			iVersion = info.GetInt32 ("iVersion");
			identity = (Guid)(info.GetValue ("identity", identity.GetType ()));
			rptObj = info.GetValue ("rptObj", typeof(MESReportFileObj)) as MESReportFileObj;
			mapFileXml = info.GetString ("mapFileXml");
			ds = info.GetValue ("ds", typeof(DataSet)) as DataSet;
			filterds = info.GetValue ("filterds", typeof(DataSet)) as DataSet;
		}

		[SecurityPermissionAttribute (SecurityAction.Demand, SerializationFormatter = true)]
		public virtual void GetObjectData (SerializationInfo info, StreamingContext context)
		{
			info.AddValue ("iVersion", iVersion);
			info.AddValue ("identity", identity);
			info.AddValue ("rptObj", rptObj);
			info.AddValue ("mapFileXml", mapFileXml);
			info.AddValue ("ds", ds);
			info.AddValue ("filterds", filterds);
		}
	}
	#endregion

	#region 客户端导航序列化对象
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public class MESNavigatorFileObj : ISerializable
	{
		public int iVersion = 1;

		public Guid identity = Guid.NewGuid ();

		public string strXMLDoc = string.Empty;

		public MESNavigatorFileObj ()
		{
		}

		protected MESNavigatorFileObj (SerializationInfo info, StreamingContext context)
		{
			int iV = info.GetInt32 ("iVersion");
			identity = (Guid)(info.GetValue ("identity", identity.GetType ()));
			strXMLDoc = info.GetString ("strXMLDoc");
		}

		[SecurityPermissionAttribute (SecurityAction.Demand, SerializationFormatter = true)]
		public virtual void GetObjectData (SerializationInfo info, StreamingContext context)
		{
			info.AddValue ("iVersion", iVersion);
			info.AddValue ("identity", identity);
			info.AddValue ("strXMLDoc", strXMLDoc);
		}
	}
	#endregion

	#region MES变量
	[Serializable]
	public class MESVariable : ICloneable
	{
		public MESVariable ()
		{
		}

		public string Name { get; set; }

		public string Description { get; set; }

		public object InitialValue { get; set; }

		public object Value { get; set; }

		public string ValueType { get; set; }

		public bool IsSystemVar{ get; set; }

		public object Clone ()
		{
			MESVariable obj = new MESVariable ();
			obj.Name = this.Name;
			obj.Description = this.Description;
			obj.InitialValue = this.InitialValue;
			obj.Value = this.Value;
			obj.ValueType = this.ValueType;
			obj.IsSystemVar = this.IsSystemVar;
			return obj;
		}
	}
	#endregion

	#region 表单快照保存的序列化对象
	/// <summary>
	/// iVersion = 2 增加sco成员
	/// </summary>
	[Serializable]
	public class MESFormSnapShotFileObj : ISerializable
	{
		// iVersion = 1

		public int iVersion = 1;

		public Guid identity = Guid.NewGuid ();

		public string strXMLDoc = string.Empty;

		public ScriptObject sco = null;

		public MESFormSnapShotFileObj ()
		{
		}

		protected MESFormSnapShotFileObj (SerializationInfo info, StreamingContext context)
		{
			int iV = info.GetInt32 ("iVersion");
			identity = (Guid)(info.GetValue ("identity", identity.GetType ()));
			strXMLDoc = info.GetString ("strXMLDoc");
			sco = info.GetValue ("sco", typeof(ScriptObject)) as ScriptObject;
		}

		[SecurityPermissionAttribute (SecurityAction.Demand, SerializationFormatter = true)]
		public virtual void GetObjectData (SerializationInfo info, StreamingContext context)
		{
			info.AddValue ("iVersion", iVersion);
			info.AddValue ("identity", identity);
			info.AddValue ("strXMLDoc", strXMLDoc);
			info.AddValue ("sco", sco);
		}
	}
	#endregion

	///报表导出Rptx文件协议
	///                  数据区(分页GZipStream压缩流)
	///page1|page2         ...                  pagen 目录8byte DpiX4byte DpiY4byte ver4byte
	///----------------------------------------------|---------|---------|---------|---------|

	public interface ILogToLocate_Item
	{
		bool MakeSureItemVisible (string CtrlName, object Tag);
	}
}
