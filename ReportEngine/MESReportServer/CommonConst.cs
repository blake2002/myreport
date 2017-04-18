using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Resources;


namespace NetSCADA.MESReportServer
{
	public static class CommonConst
	{
		//public static readonly string RptPath = @"/home/administrator/Documents/";
		//Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "");
		public static int Port = 8192;
		public static string Domain = "http://localhost";
		public static string IsUsedLogin = "0";
		public static string ConStr = "server=192.167.8.51;database=rptdb;uid=root;pwd=root;pooling=false;charset='utf8';port=3306";
	}
}

