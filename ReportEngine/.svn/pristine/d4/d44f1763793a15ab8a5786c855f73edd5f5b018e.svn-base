using System;
using System.Windows.Forms;
using PMS.Libraries.ToolControls;
using System.Threading;
using HtmlTags;

namespace test
{
	class MainClass
	{
		[STAThread]
		public static void Main (string[] args)
		{
			Application.ThreadException += Application_ThreadException;
			Application.SetUnhandledExceptionMode (UnhandledExceptionMode.CatchException);
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			Application.Run (new PMS.Libraries.ToolControls.test ());
		}

		/// <summary>
		/// 处理应用程序域内的未处理异常（非UI线程异常）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		static void CurrentDomain_UnhandledException (object sender, UnhandledExceptionEventArgs e)
		{           
			try {
				Exception ex = e.ExceptionObject as Exception;
				MessageBox.Show (ex.InnerException.Message);
			} catch {
			}
		}

		/// <summary>
		/// 处理应用程序的未处理异常（UI线程异常）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		static void Application_ThreadException (object sender, ThreadExceptionEventArgs e)
		{
			try {              
				MessageBox.Show (e.Exception.Message);
			} catch {
			}
		}
	}
}
