using System;
using Dapper.Contrib.Extensions;

namespace NetSCADA.MESReportServer
{
	[Table ("directory")]
	public class RptDirectory
	{
		[ExplicitKey]
		public string Id { get; set; }

		public string DirName { get; set; }

		public string DirPath { get; set; }

		public string CreateTime { get; set; }

		public string LastModifyTime { get; set; }

		public string LastModifyUser { get; set; }
	}
}

