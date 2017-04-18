using System;
using Dapper.Contrib.Extensions;
using System.Dynamic;

namespace NetSCADA.MESReportServer
{
	[Table ("rpt_runtime")]
	public class RptRuntime
	{
		[ExplicitKey]
		public string Id { get; set; }

		public int QueryCount { get; set; }

		public int ExportCount { get; set; }

		public string LastQeuryPerson { get; set; }

		public string LastQueryTime { get; set; }

		public string RptId { get; set; }
	}
}

