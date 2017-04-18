using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ICSharpCode.Core;
using ICSharpCode.Core.Services;

namespace PMS.Libraries.ToolControls.PMSPublicInfo
{
	[Serializable ()]
	public class LogMsgObj
	{
		public enum LogMsgType
		{
			Info = 1,
			Warnning,
			Error
		}

		private LogMsgType _LogType = LogMsgType.Info;

		public LogMsgType LogType {
			get { return _LogType; }
			set { _LogType = value; }
		}

		private DateTime _dt = DateTime.Now;

		public DateTime Dt {
			get { return _dt; }
			set { _dt = value; }
		}

		private string _user = string.Empty;

		public string User {
			get { return _user; }
			set { _user = value; }
		}

		private string _msg = string.Empty;

		public string Msg {
			get { return _msg; }
			set { _msg = value; }
		}

		// 当前文档CurrentDocument标识，必须可序列化
		private object _currentDocIdentity = null;

		public object CurrentDocIdentity {
			get { return _currentDocIdentity; }
			set { _currentDocIdentity = value; }
		}

		private string _ctrlName = string.Empty;

		public string CtrlName {
			get { return _ctrlName; }
			set { _ctrlName = value; }
		}

		//Tag必须可序列化
		private object _tag = null;

		public object Tag {
			get { return _tag; }
			set { _tag = value; }
		}
	}

	public class Message
	{
		public static IntPtr PropertyGridFormHandle = IntPtr.Zero;
		public static IntPtr PMSDeveloperControlHandle = IntPtr.Zero;
		public static IntPtr ErrorListHandle = IntPtr.Zero;
		public static IntPtr OutputFormHandle = IntPtr.Zero;
		public static IntPtr LogFormHandle = IntPtr.Zero;
		public static IntPtr ToolBoxFormHandle = IntPtr.Zero;
		public static IntPtr DocumentOutlineFormHandle = IntPtr.Zero;
		public static IntPtr WorkFlowManagerFormHandle = IntPtr.Zero;
		public static IntPtr WorkFlowMonitoringFormHandle = IntPtr.Zero;
		public static IntPtr SourceConfigToolWindowHandle = IntPtr.Zero;
		public static IntPtr DataSourceToolWindowHandle = IntPtr.Zero;
		public static IntPtr NodeTemplateWindowHandle = IntPtr.Zero;
		public static IntPtr ClipboardFormWindowHandle = IntPtr.Zero;
		public static IntPtr WorkFlowCustomFormHandle = IntPtr.Zero;
		public static IntPtr DBStructAnalyseFormWindowHandle = IntPtr.Zero;
		public static IntPtr UserViewNavigationBarFormHandle = IntPtr.Zero;
		public static IntPtr WorkFlowProcessListFormHandle = IntPtr.Zero;
		public static IntPtr WorkFlowDesignFormHandle = IntPtr.Zero;
		public static IntPtr WorkFlowRunFormHandle = IntPtr.Zero;
		public static IntPtr SystemConfigFormHandle = IntPtr.Zero;
        

		//系统消息
		public const int WM_COPYDATA = 0x004A;
		public const int WM_KEYDOWN = 0x100;

		//定义消息常数用来获取消息
		public const int USER_UPDATECOMBOBOX = 0x500;
		public const int USER_DOCMODIFIED = 0x501;
		public const int USER_SAVECURRENTDOC = 0x502;
		public const int USER_CLOSEALLDOCEXCEPTTHIS = 0x503;
		public const int USER_CLOSEALLDOC = 0x504;
		public const int USER_CLOSECURRENTDOC = 0x505;
		public const int USER_ROOTFORMCONTROLS = 0x506;
		// 现已不用
		public const int USER_OUTPUTFORMMSG = 0x507;
		public const int USER_LOGFORMMSG = 0x508;
		public const int USER_TESTRUNMSG = 0x509;
		public const int USER_UPDATETOOLBOXMSG = 0x50a;
		public const int USER_UPDATEDOCOUTLINE = 0x50b;
		public const int USER_DOCOUTLINESELCHANGE = 0x50c;
		public const int USER_PROPGRIDSELCHANGE = 0x50d;
		public const int USER_CURRENTDOCCLOSED = 0x50e;
		public const int USER_PROPGRIDSHOWOBJ = 0x50f;
		public const int USER_GETCURRENTREPORTDATADEF = 0x510;
		public const int USER_TOGGLEFULLSCREEN = 0x511;
		public const int USER_EXITFULLSCREEN = 0x512;
		public const int USER_REFRESHCURRENT = 0x513;
		public const int USER_NEWWORKFLOWFORM = 0x514;
		public const int USER_OPENWORKFLOWFORM = 0x515;
		public const int USER_DELETEWORKFLOWFORM = 0x516;
		public const int USER_SAVEWORKFLOWFORM = 0x517;
		public const int USER_STARTWORKFLOWINSTANCEMONITOR = 0x518;
		public const int USER_ADDTOCLIPBOARDMANAGER = 0x519;
		public const int USER_REMOVEFROMCLIPBOARDMANAGER = 0x51a;
		public const int USER_STARTWORKFLOWMONITOR = 0x51b;
		public const int USER_NEWWORKFLOWCUSTOM = 0x51c;
		public const int USER_OPENWORKFLOWCUSTOM = 0x51d;
		public const int USER_DELETEWORKFLOWCUSTOM = 0x51e;
		public const int USER_SAVEWORKFLOWCUSTOM = 0x51f;
		public const int USER_OPENWORKFLOWCUSTOM_P = 0x52f;
		public const int USER_GETCURRENTREPORTDATADEFEx = 0x530;
		public const int USER_GETCURRENTREPORTUIEXPRESSIONTREEVIEW = 0x531;
		public const int USER_GETCURRENTRPTCTRLEXPRESSIONS = 0x532;
		public const int USER_SETDOCSAVED = 0x533;
		public const int USER_SHOWPROPERTYGRID = 0x534;
		public const int USER_NEWFORM = 0x535;
		public const int USER_OPENDOC = 0x536;
		public const int USER_NORMALRUNVIEW = 0x537;
		public const int USER_WFPUSHFORM = 0x538;
		public const int USER_DOCOUTLINEREFRESHSWITCH = 0x539;
		public const int USER_GETCURRENTFORMDATADEF = 0x53a;
		public const int USER_GETCURRENTWORKFLOWDATADEF = 0x53b;
		public const int USER_GETCURRENTOBJ = 0x53c;
		public const int USER_GETCURRENTHOST = 0x53d;
		public const int USER_GETCURRENTDOCUMENT = 0x53e;
		public const int USER_GETCURRENT = 0x53f;
		public const int USER_GETCURRENTDOCUMENTTYPE = 0x540;
		public const int USER_CLOSEDOC = 0x541;
		public const int USER_GETCURRENTDOCUMENTIDENTITY = 0x542;
		public const int USER_CHECKNEEDDOWNLOAD = 0x543;


		#region NetSCADA 消息

		public const int USER_PARENTFORMHANDLE = 0x465;

		public static IntPtr NSPLUG_EVENT_ENDOPEN = (IntPtr)101;
		public static IntPtr NSPLUG_EVENT_ENDEXPORT = (IntPtr)102;
		public static IntPtr NSPLUG_EVENT_ENDPRINT = (IntPtr)103;

		#endregion

		/// <summary>

		/// 发送 WM_COPYDATA 消息的数据结构体

		/// </summary>

		[StructLayout (LayoutKind.Sequential)]

		public struct COPYDATASTRUCT
		{

			/// <summary>
			/// 用户自定义数据
			/// </summary>
			public IntPtr dwData;

			/// <summary>
			/// 数据长度
			/// </summary>
			public int cbData;

			/// <summary>
			/// 数据地址指针
			/// </summary>
			public IntPtr lpData;

		}


		/// <summary>
		/// 通过 SendMessage 向指定句柄发送数据
		/// </summary>
		/// <param name="hWnd">接收方的窗口句柄</param>
		/// <param name="dwData">附加数据</param>
		/// <param name="lpdata">发送的数据</param>
		public static int SendCopyData (IntPtr hWnd, int dwData, byte[] lpdata)
		{
			COPYDATASTRUCT cds = new COPYDATASTRUCT ();

			cds.dwData = (IntPtr)dwData;

			cds.cbData = lpdata.Length;
            
			cds.lpData = Marshal.AllocHGlobal (lpdata.Length);

			Marshal.Copy (lpdata, 0, cds.lpData, lpdata.Length);

			IntPtr lParam = Marshal.AllocHGlobal (Marshal.SizeOf (cds));

			Marshal.StructureToPtr (cds, lParam, true);



			int result = 0;

			try {

				result = SendMessage (hWnd, WM_COPYDATA, IntPtr.Zero, lParam);

			} finally {

				Marshal.FreeHGlobal (cds.lpData);

				Marshal.DestroyStructure (lParam, typeof(COPYDATASTRUCT));

				Marshal.FreeHGlobal (lParam);

			}

			return result;

		}

		/// <summary>
		/// 获取消息类型为 WM_COPYDATA 中的数据
		/// </summary>
		/// <param name="m"></param>
		/// <param name="dwData">附加数据</param>
		/// <param name="lpdata">接收到的发送数据</param>

		public static void ReceivCopyData (ref System.Windows.Forms.Message m, out int dwData, out byte[] lpdata)
		{

			COPYDATASTRUCT cds = (COPYDATASTRUCT)m.GetLParam (typeof(COPYDATASTRUCT));

			dwData = cds.dwData.ToInt32 ();

			lpdata = new byte[cds.cbData];

			Marshal.Copy (cds.lpData, lpdata, 0, cds.cbData);

			m.Result = (IntPtr)0;

		}

		//声明 API 函数
		[DllImport ("User32.dll", EntryPoint = "SendMessage")]
		private static extern int SendMessage (
			IntPtr hWnd,   // 窗体句柄 handle to destination window
			int Msg,    // 消息 message 
			IntPtr wParam, // 参数1 first message parameter
			IntPtr lParam // 参数2 second message parameter
		);

		//声明 API 函数
		[DllImport ("User32.dll", EntryPoint = "PostMessage")]
		private static extern int PostMessage (
			IntPtr hWnd,   // 窗体句柄 handle to destination window
			int Msg,    // 消息 message 
			IntPtr wParam, // 参数1 first message parameter
			IntPtr lParam // 参数2 second message parameter
		);

		[DllImport ("User32.dll", EntryPoint = "FindWindow")]
		private static extern int FindWindow (string lpClassName, string lpWindowName);

		/// <summary>
		/// 向窗体发送消息的函数(同步)
		/// </summary>
		/// <param name="MSG">信息</param>
		public int SendMsgToMainForm (int MSG, string lpClassNmae, string lpWindowName, IntPtr wParam, IntPtr lParam)
		{
			int WINDOW_HANDLER = FindWindow (lpClassNmae, lpWindowName);
			if (WINDOW_HANDLER == 0) {
				throw new Exception ("Could not find Main window!");
			}
			return SendMessage ((IntPtr)WINDOW_HANDLER, MSG, wParam, lParam);
		}

		/// <summary>
		/// 向窗体发送消息的函数(同步)
		/// </summary>
		/// <param name="MSG">信息</param>
		public static int SendMsgToMainForm (IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam)
		{
			return SendMessage (hWnd, Msg, wParam, lParam);
		}

		/// <summary>
		/// 向窗体发送消息的函数(异步)
		/// </summary>
		/// <param name="MSG">信息</param>
		public int PostMsgToMainForm (int MSG, string lpClassNmae, string lpWindowName, IntPtr wParam, IntPtr lParam)
		{
			int WINDOW_HANDLER = FindWindow (lpClassNmae, lpWindowName);
			if (WINDOW_HANDLER == 0) {
				throw new Exception ("Could not find Main window!");
			}
			return PostMessage ((IntPtr)WINDOW_HANDLER, MSG, wParam, lParam);
		}

		/// <summary>
		/// 向窗体发送消息的函数(异步)
		/// </summary>
		/// <param name="MSG">信息</param>
		public static int PostMsgToMainForm (IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam)
		{
			return PostMessage (hWnd, Msg, wParam, lParam);
		}

		public static void OutPut (string strMsg)
		{
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_OUTPUTFORMMSG;
			byte[] theBytes = PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.ObjToByte (strMsg);
			if (PMS.Libraries.ToolControls.PMSPublicInfo.Message.OutputFormHandle != IntPtr.Zero)
				PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendCopyData (PMS.Libraries.ToolControls.PMSPublicInfo.Message.OutputFormHandle, msgID, theBytes);
		}

		public static void LogMsg (LogMsgObj logMsgObj)
		{
			//todo:qiuleilei
			if (CurrentPrjInfo.CurrentEnvironment == MESEnvironment.MESReportServer) {
				var log = NLog.LogManager.GetLogger ("MESReportServer");
				var mes = string.Format ("user:{0},msg:{1}", logMsgObj.User, logMsgObj.Msg);
				switch (logMsgObj.LogType) {
				case PMS.Libraries.ToolControls.PMSPublicInfo.LogMsgObj.LogMsgType.Info:
					log.Info (mes);
					break;
				case PMS.Libraries.ToolControls.PMSPublicInfo.LogMsgObj.LogMsgType.Warnning:
					log.Warn (mes);
					break;
				case PMS.Libraries.ToolControls.PMSPublicInfo.LogMsgObj.LogMsgType.Error:
					log.Error (mes);
					break;
				}

				return;
			}
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_LOGFORMMSG;
			byte[] theBytes = PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.ObjToByte (logMsgObj);
			if (PMS.Libraries.ToolControls.PMSPublicInfo.Message.LogFormHandle != IntPtr.Zero)
				PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendCopyData (PMS.Libraries.ToolControls.PMSPublicInfo.Message.LogFormHandle, msgID, theBytes);
		}

		public static void LogMsg (LogMsgObj.LogMsgType logMsgType, string user, string msg)
		{
			LogMsgObj lmo = new LogMsgObj ();
			lmo.LogType = logMsgType;
			lmo.User = user;
			lmo.Msg = msg;
			lmo.Dt = DateTime.Now;
			LogMsg (lmo);
		}

		public static void Info (string user, string msg, bool bRecord)
		{
			LogMsgObj lmo = new LogMsgObj ();
			lmo.LogType = LogMsgObj.LogMsgType.Info;
			lmo.User = user;
			lmo.Msg = msg;
			lmo.Dt = DateTime.Now;
			LogMsg (lmo);
			if (bRecord) {
				LoggingService.Info (string.Format ("user:{0} msg:{1}", user, msg));
				LogManager.Info (msg);
			}
		}

		public static void Warnning (string user, string msg, bool bRecord)
		{
			LogMsgObj lmo = new LogMsgObj ();
			lmo.LogType = LogMsgObj.LogMsgType.Warnning;
			lmo.User = user;
			lmo.Msg = msg;
			lmo.Dt = DateTime.Now;
			LogMsg (lmo);
			if (bRecord) {
				LoggingService.Warn (string.Format ("user:{0} msg:{1}", user, msg));
				LogManager.Warn (msg);
			}
		}

		public static void Error (string user, string msg, bool bRecord)
		{
			LogMsgObj lmo = new LogMsgObj ();
			lmo.LogType = LogMsgObj.LogMsgType.Error;
			lmo.User = user;
			lmo.Msg = msg;
			lmo.Dt = DateTime.Now;
			LogMsg (lmo);
			if (bRecord) {
				LoggingService.Error (string.Format ("user:{0} msg:{1}", user, msg));
				LogManager.Error (msg);
			}
		}

		public static void Info (string msg)
		{
			LogMsgObj lmo = new LogMsgObj ();
			lmo.LogType = LogMsgObj.LogMsgType.Info;
			lmo.User = CurrentPrjInfo.CurrentLoginUserID;
			lmo.Msg = msg;
			lmo.Dt = DateTime.Now;
			LogMsg (lmo);
		}

		public static void Warnning (string msg)
		{
			LogMsgObj lmo = new LogMsgObj ();
			lmo.LogType = LogMsgObj.LogMsgType.Warnning;
			lmo.User = CurrentPrjInfo.CurrentLoginUserID;
			lmo.Msg = msg;
			lmo.Dt = DateTime.Now;
			LogMsg (lmo);
		}

		public static void Error (string msg)
		{
			LogMsgObj lmo = new LogMsgObj ();
			lmo.LogType = LogMsgObj.LogMsgType.Error;
			lmo.User = CurrentPrjInfo.CurrentLoginUserID;
			lmo.Msg = PMS.Libraries.ToolControls.PMSPublicInfo.PublicFunctionClass.CodeRunningPosition2 () + msg;
			lmo.Dt = DateTime.Now;
			PMSFileClass.AddToTraceFile (msg);
			LogMsg (lmo);
		}

		public static void Info (string msg, string ctrlName)
		{
			LogMsgObj lmo = new LogMsgObj ();
			lmo.LogType = LogMsgObj.LogMsgType.Info;
			lmo.User = CurrentPrjInfo.CurrentLoginUserID;
			lmo.Msg = msg;
			lmo.Dt = DateTime.Now;
			object identity = CurrentPrjInfo.GetCurrentDocumentIdentity ();
			if (null != identity) {
				lmo.CurrentDocIdentity = identity;
			}
			lmo.CtrlName = ctrlName;
			LogMsg (lmo);
		}

		public static void Warnning (string msg, string ctrlName)
		{
			LogMsgObj lmo = new LogMsgObj ();
			lmo.LogType = LogMsgObj.LogMsgType.Warnning;
			lmo.User = CurrentPrjInfo.CurrentLoginUserID;
			lmo.Msg = msg;
			lmo.Dt = DateTime.Now;
			object identity = CurrentPrjInfo.GetCurrentDocumentIdentity ();
			if (null != identity) {
				lmo.CurrentDocIdentity = identity;
			}
			lmo.CtrlName = ctrlName;
			LogMsg (lmo);
		}

		public static void Error (string msg, string ctrlName)
		{
			LogMsgObj lmo = new LogMsgObj ();
			lmo.LogType = LogMsgObj.LogMsgType.Error;
			lmo.User = CurrentPrjInfo.CurrentLoginUserID;
			lmo.Msg = msg;
			lmo.Dt = DateTime.Now;
			object identity = CurrentPrjInfo.GetCurrentDocumentIdentity ();
			if (null != identity) {
				lmo.CurrentDocIdentity = identity;
			}
			lmo.CtrlName = ctrlName;
			LogMsg (lmo);
		}

		public static void Info (string msg, string ctrlName, object tag)
		{
			LogMsgObj lmo = new LogMsgObj ();
			lmo.LogType = LogMsgObj.LogMsgType.Info;
			lmo.User = CurrentPrjInfo.CurrentLoginUserID;
			lmo.Msg = msg;
			lmo.Dt = DateTime.Now;
			object identity = CurrentPrjInfo.GetCurrentDocumentIdentity ();
			if (null != identity) {
				lmo.CurrentDocIdentity = identity;
			}
			lmo.CtrlName = ctrlName;
			lmo.Tag = tag;
			LogMsg (lmo);
		}

		public static void Warnning (string msg, string ctrlName, object tag)
		{
			LogMsgObj lmo = new LogMsgObj ();
			lmo.LogType = LogMsgObj.LogMsgType.Warnning;
			lmo.User = CurrentPrjInfo.CurrentLoginUserID;
			lmo.Msg = msg;
			lmo.Dt = DateTime.Now;
			object identity = CurrentPrjInfo.GetCurrentDocumentIdentity ();
			if (null != identity) {
				lmo.CurrentDocIdentity = identity;
			}
			lmo.CtrlName = ctrlName;
			lmo.Tag = tag;
			LogMsg (lmo);
		}

		public static void Error (string msg, string ctrlName, object tag)
		{
			LogMsgObj lmo = new LogMsgObj ();
			lmo.LogType = LogMsgObj.LogMsgType.Error;
			lmo.User = CurrentPrjInfo.CurrentLoginUserID;
			lmo.Msg = msg;
			lmo.Dt = DateTime.Now;
			object identity = CurrentPrjInfo.GetCurrentDocumentIdentity ();
			if (null != identity) {
				lmo.CurrentDocIdentity = identity;
			}
			lmo.CtrlName = ctrlName;
			lmo.Tag = tag;
			LogMsg (lmo);
		}

		public static void Debug (string strMsg, bool bRecord)
		{
			OutPut (strMsg);
			if (bRecord) {
				LoggingService.Debug (strMsg);
			}
		}

		public static void Trace (string msg)
		{
			string msgInfo = string.Format ("{0:yyyy-MM-dd hh:mm:ss.ff};{1};{2}", DateTime.Now, CurrentPrjInfo.CurrentLoginUserID, msg);
			PMSFileClass.AddToTextFile (System.IO.Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "Trace"), msgInfo);
		}
	}
}
