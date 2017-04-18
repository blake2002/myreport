using System;
using System.Configuration;
using Topshelf.Logging;
using NLog;
using System.Threading.Tasks;
using System.Threading;

namespace NetSCADA.MESReportServer
{
	public class ServiceBoot
	{
		LogWriter log = HostLogger.Get ("MESReportServer");

		public void Start ()
		{
			try {
				log.Info ("MESReportServer is started");
				CommonConst.IsUsedLogin = ConfigurationManager.AppSettings.Get ("isUsedLogin");
				Task.Run (() => {
					Thread.Sleep (10000);
					ReportJobManage.InitialJobs ();
					ReportJobManage.DealWithDirs ();
					ReportJobManage.ClearQueryFiles ();
				});
			} catch (Exception ex) {
				log.Error (string.Format ("message:{0},StackTrace:{1}", ex.Message, ex.StackTrace));
			}
		}

		public void Stop ()
		{
			log.Info ("MESReportServer is stopped");
		}
	}
}

