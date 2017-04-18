using System;
using Topshelf;
using System.IO;
using Topshelf.Nancy;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;
using NLog;
using Topshelf.Logging;


namespace NetSCADA.MESReportServer
{
	class MainClass
	{
		static LogWriter log = HostLogger.Get<MainClass> ();

		public static void Main (string[] args)
		{
			NLogLogWriterFactory.Use ();
			GetCfg ();

			HostFactory.Run (service => {
				try {
					//service.UseNLog ();
					service.Service<ServiceBoot> (hostConfig => {
						try {
							hostConfig.WithNancyEndpoint (service, b => {
								b.AddHost (port: CommonConst.Port);
								b.CreateUrlReservationsOnInstall ();
							});
							hostConfig.ConstructUsing (() => new ServiceBoot ());
							hostConfig.WhenStarted (b => b.Start ()).WhenStopped (b => b.Stop ());
						} catch (Exception ex) {
							log.Error (string.Format ("msg:{0},stacktrack:{1}", ex.Message, ex.StackTrace));
						}
					});
					if (System.Environment.OSVersion.Platform == PlatformID.Unix) {
						service.UseLinuxIfAvailable ();
					}
					service.StartAutomatically ();
					//service.UseLog4Net ("log4net.config", true);
					service.RunAsNetworkService ().SetDescription ("MESReportServerService");
					service.SetDisplayName ("MESReportServer");
					service.SetServiceName ("MESReportServer");
				} catch (Exception ex) {
					log.Error (string.Format ("msg:{0},stacktrack:{1}", ex.Message, ex.StackTrace));
				}
			});
		}

		static void GetCfg ()
		{
			try {
				CommonConst.Domain = ConfigurationManager.AppSettings.Get ("domain");
				CommonConst.Port = 8192;
				if (!int.TryParse (ConfigurationManager.AppSettings.Get ("port"), out CommonConst.Port)) {
					CommonConst.Port = 8192;
				}

				CommonConst.ConStr = ConfigurationManager.AppSettings.Get ("conStr");
			} catch (Exception ex) {
				log.Error (string.Format ("msg:{0},stacktrack:{1}", ex.Message, ex.StackTrace));
			}
		}
	}
}
