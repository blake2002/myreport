using System;
using Topshelf.Logging;

namespace NetSCADA.MESReportServer
{
	using System;
	using NLog;

	public class NLogLogWriterFactory :
	LogWriterFactory
	{
		readonly LogFactory _logFactory;

		public NLogLogWriterFactory (LogFactory logFactory)
		{
			_logFactory = logFactory;
		}

		public NLogLogWriterFactory ()
			: this (new LogFactory ())
		{
		}

		public LogWriter Get (string name)
		{
			return new NLogLogWriter (_logFactory.GetLogger (name), name);
		}

		public void Shutdown ()
		{
			_logFactory.Flush ();
			//_logFactory.SuspendLogging ();
			_logFactory.DisableLogging ();
		}

		public static void Use ()
		{
			HostLogger.UseLogger (new NLogHostLoggerConfigurator ());
		}

		public static void Use (LogFactory factory)
		{
			HostLogger.UseLogger (new NLogHostLoggerConfigurator (factory));
		}


		[Serializable]
		public class NLogHostLoggerConfigurator :
		HostLoggerConfigurator
		{
			readonly LogFactory _factory;

			public NLogHostLoggerConfigurator (LogFactory factory)
			{
				_factory = factory;
			}

			public NLogHostLoggerConfigurator ()
			{
			}

			public LogWriterFactory CreateLogWriterFactory ()
			{
				if (_factory != null)
					return new NLogLogWriterFactory (_factory);

				return new NLogLogWriterFactory ();
			}
		}
	}
}

