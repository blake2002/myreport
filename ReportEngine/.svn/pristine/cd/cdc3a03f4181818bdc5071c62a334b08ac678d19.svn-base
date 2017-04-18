using System;
using System.IO;
using PMS.Libraries.ToolControls.PMSPublicInfo;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NetSCADA.MESReportServer
{
	public interface ILogin
	{
		bool Login (string user, string pwd);
	}

	public class ReportLogin :ILogin
	{
		#region ILogin implementation

		static bool CreateUsers (string usrconf, string user, string pwd)
		{
			var users = new List<UserInfo> ();
			users.Add (new UserInfo () {
				UserName = "admin",
				Pwd = "admin"
			});
			var json = JsonConvert.SerializeObject (users);
			using (var conf = File.Create (usrconf)) {
				using (var sw = new StreamWriter (conf)) {
					sw.Write (json);
					sw.Flush ();
				}
			}
			if (user == "admin" && pwd == "admin") {
				return true;
			} else
				return false;
		}

		public bool Login (string user, string pwd)
		{
			if (string.IsNullOrEmpty (user) || string.IsNullOrEmpty (pwd))
				return false;
			var usrconf = Path.Combine (ProjectPathClass.Conf, "usr.conf");
			var userStr = File.ReadAllText (usrconf);
			List<UserInfo> users = null;
			if (string.IsNullOrEmpty (userStr) || string.IsNullOrWhiteSpace (userStr)) {
				return CreateUsers (usrconf, user, pwd);
			}
			users = JsonConvert.DeserializeObject (userStr, typeof(List<UserInfo>)) as List<UserInfo>;
			if (users == null || users.Count == 0) {
				return CreateUsers (usrconf, user, pwd);
			}

			var index = users.FindIndex ((u) => u.UserName == user && u.Pwd == pwd);
			if (index == -1)
				return false;
				
			return true;
		}

		#endregion
	}

	public class UserInfo
	{
		public string UserName { get; set; }

		public string Pwd { get; set; }
	}
}

