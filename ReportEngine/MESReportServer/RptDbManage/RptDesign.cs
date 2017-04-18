using System;
using Dapper.Contrib.Extensions;
using System.Dynamic;
using System.Configuration;

namespace NetSCADA.MESReportServer
{
	[Table ("rpt_design")]
	public class RptDesign
	{
		[ExplicitKey]
		public string Id { get; set; }

		public string RptName { get; set; }

		public string UploadPerson { get; set; }

		public string UploadTime { get; set; }

		public string DirId { get; set; }
	}
}

