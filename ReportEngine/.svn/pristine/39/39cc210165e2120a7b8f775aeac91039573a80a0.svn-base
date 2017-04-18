using System;
using Topshelf.Logging;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using PMS.Libraries.ToolControls.PMSPublicInfo;
using System.IO;
using System.Linq;

namespace NetSCADA.MESReportServer
{
	public class RptDb
	{
		static LogWriter log = HostLogger.Get ("RptManageModule");

		public static bool SaveDir (RptDirectory dir)
		{
			IDbTransaction tran = null;
			try {
				if (dir == null)
					return false;
				dir.DirPath = dir.DirPath.Replace (Path.DirectorySeparatorChar, '$');
				using (var conn = new MySqlConnection (CommonConst.ConStr)) {
					conn.Open ();
					tran = conn.BeginTransaction ();
                    RptDirectory d = null;
                    if (!string.IsNullOrEmpty(dir.Id))
                        d = conn.Get<RptDirectory>(dir.Id);
                    else
                        d = GetRptDirectoryByName(dir.DirPath).FirstOrDefault();

                    if (d == null) {
						dir.Id = Guid.NewGuid ().ToString ("N");
						dir.LastModifyTime = dir.CreateTime = DateTime.Now.ToString ("yyyyMMddHHmmss");
						var flag = conn.Insert (dir, tran);
						return flag >= 0;
					} else {
						
						var allDirs = GetAllRptDirectory ();
                        var dbDirPath = d.DirPath.Replace(Path.DirectorySeparatorChar, '$');

                        var flag = true;
						foreach (var alDir in allDirs) {
							if (alDir.Id == d.Id)
								continue;
                            var path = alDir.DirPath.Replace(Path.DirectorySeparatorChar, '$');

                            if (path.StartsWith (dbDirPath)&& dbDirPath!= dir.DirPath) {
								alDir.DirPath = path.Replace (dbDirPath, dir.DirPath);
                                flag &= conn.Update(alDir, tran);
                            }	
						}

                        if (d.DirName != dir.DirName)
                        {
                            d.DirName = dir.DirName;
                            d.DirPath = dir.DirPath;
                            d.LastModifyUser = dir.LastModifyUser;
                            d.LastModifyTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                            flag &= conn.Update(d, tran);
                        }
						
						return flag;
					}

					tran.Commit ();
                    conn.Close();

                }
			} catch (Exception ex) {
				if (tran != null) {
					tran.Rollback ();
				}
				log.Error (string.Format ("message:{0},StackTrace:{1}", ex.Message, ex.StackTrace));
				return false;
			}
		}

        public static bool InsertDir(RptDirectory dir)
        {
            IDbTransaction tran = null;
            try
            {
                if (dir == null)
                    return false;
                dir.DirPath = dir.DirPath.Replace(Path.DirectorySeparatorChar, '$');
                using (var conn = new MySqlConnection(CommonConst.ConStr))
                {
                    conn.Open();
                    tran = conn.BeginTransaction();
                    RptDirectory d = null;
                    if (!string.IsNullOrEmpty(dir.Id))
                        d = conn.Get<RptDirectory>(dir.Id);
                    else
                        d = GetRptDirectoryByName(dir.DirPath).FirstOrDefault();

                    if (d == null)
                    {
                        dir.Id = Guid.NewGuid().ToString("N");
                        dir.LastModifyTime = dir.CreateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                        var flag = conn.Insert(dir, tran);
                        return flag >= 0;
                    }
                   
                    tran.Commit();

                    return true;

                }
            }
            catch (Exception ex)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
                log.Error(string.Format("message:{0},StackTrace:{1}", ex.Message, ex.StackTrace));
                return false;
            }
        }

        public static bool SaveRptDesign (RptDesign rpt)
		{
			try {
				if (rpt == null)
					return false;
				var flag = 0L;
				using (var conn = new MySqlConnection (CommonConst.ConStr)) {
					var d = conn.Query<RptDesign> ("select * from rpt_design where RptName=@RptName", new {RptName = rpt.RptName}).FirstOrDefault ();

					if (d == null) {
						rpt.Id = Guid.NewGuid ().ToString ("N");
						flag = conn.Insert (rpt);
						return flag >= 0;
					} else {
						var oldRptName = d.RptName;
						d.RptName = rpt.RptName;

						//d.UploadPerson=rpt.UploadPerson;
						var dirPath = GetRptDirectoryById (d.DirId);
						var result = conn.Update (d);
						if (result)
							rpt.Id = d.Id;
						if (result && !string.IsNullOrEmpty (dirPath.DirPath)) {
							var path = Path.Combine (ProjectPathClass.UserCustomPath, dirPath.DirPath.TrimStart (Path.DirectorySeparatorChar));
							var file = Path.Combine (path, string.Format ("{0}.rpt", oldRptName));
							var target = Path.Combine (path, string.Format ("{0}.rpt", d.RptName));
							if (File.Exists (file)) {
								File.Move (file, target);
							}
						}
						return result;
					}
				}


			} catch (Exception ex) {
				log.Error (string.Format ("message:{0},StackTrace:{1}", ex.Message, ex.StackTrace));
				return false;
			}
		}

		public static bool SaveRptRuntime (RptRuntime rpt)
		{
			try {
				if (rpt == null || string.IsNullOrEmpty (rpt.RptId))
					return false;
				using (var conn = new MySqlConnection (CommonConst.ConStr)) {
					var d = conn.Query<RptRuntime> ("select * from rpt_runtime where RptId=@RptId", new {RptId = rpt.RptId}).FirstOrDefault ();
	
					if (d == null) {
						rpt.Id = Guid.NewGuid ().ToString ("N");
						rpt.QueryCount = 0;
						rpt.ExportCount = 0;
						rpt.LastQueryTime = DateTime.Now.ToString ("yyyyMMddHHmmss");
						var flag = conn.Insert (rpt);
						return flag >= 0;
					} else {
						if (rpt.ExportCount != 0)
							d.ExportCount = rpt.ExportCount;
						if (rpt.LastQeuryPerson != null)
							d.LastQeuryPerson = rpt.LastQeuryPerson;
						if (rpt.QueryCount != 0)
							d.QueryCount = rpt.QueryCount;
						if (!string.IsNullOrEmpty (rpt.RptId))
							d.RptId = rpt.RptId;
						d.LastQueryTime = DateTime.Now.ToString ("yyyyMMddHHmmss");
						return conn.Update (d);
					}
				}

			} catch (Exception ex) {
				log.Error (string.Format ("message:{0},StackTrace:{1}", ex.Message, ex.StackTrace));
				return false;
			}
		}

		public static void RecordRuntime (string rptName, string user, int flag)
		{
			if (string.IsNullOrEmpty (rptName))
				return;
			RptRuntime runtime = GetRptRuntimeByName (rptName);
			if (runtime == null) {
				var design = GetRptDesignByName (rptName);
				if (design == null)
					return;
				SaveRptRuntime (new RptRuntime () {
					QueryCount = 1,
					ExportCount = 1,
					LastQeuryPerson = user,
					RptId = design.Id
				});
			} else {
				if (flag == 0) {
					runtime.QueryCount += 1;
				} else if (flag == 1) {
					runtime.ExportCount += 1;
				}
				runtime.LastQeuryPerson = user;
				SaveRptRuntime (runtime);
			}
		}

		public static bool ExistsRptDesignByName (string rptName)
		{
			try {
				using (var conn = new MySqlConnection (CommonConst.ConStr)) {
					int count = conn.Query<int> ("select count(1) count from rpt_design where rptName=@RptName", new {RptName = rptName}).FirstOrDefault ();
					return count > 0;
				}

			} catch (Exception ex) {
				log.Error (string.Format ("message:{0},StackTrace:{1}", ex.Message, ex.StackTrace));
				return true;
			}
		}

		public static List<RptRuntime> GetAllRptRuntime ()
		{
			try {
				using (var conn = new MySqlConnection (CommonConst.ConStr)) {
					return conn.GetAll<RptRuntime> ().AsList ();
				}

			} catch (Exception ex) {
				log.Error (string.Format ("message:{0},StackTrace:{1}", ex.Message, ex.StackTrace));
				return null;
			}
		}

		public static List<RptRuntime> GetRptRuntimeByDirId (string dirId)
		{
			try {
				using (var conn = new MySqlConnection (CommonConst.ConStr)) {
					return conn.Query<RptRuntime> ("select * from rpt_runtime r where exists(select d.Id from rpt_design d where r.RptId=d.Id and d.DirId=@DirId)", new {DirId = dirId}).AsList ();
				}

			} catch (Exception ex) {
				log.Error (string.Format ("message:{0},StackTrace:{1}", ex.Message, ex.StackTrace));
				return null;
			}
		}

		public static RptRuntime GetRptRuntimeByName (string rptName)
		{
			try {
				using (var conn = new MySqlConnection (CommonConst.ConStr)) {
					return conn.Query<RptRuntime> ("select * from rpt_runtime r where exists(select d.Id from rpt_design d where r.RptId=d.Id and d.RptName=@RptName)", new {RptName = rptName}).FirstOrDefault ();
				}

			} catch (Exception ex) {
				log.Error (string.Format ("message:{0},StackTrace:{1}", ex.Message, ex.StackTrace));
				return null;
			}
		}

		public static RptDesign GetRptDesignByName (string rptName)
		{
			try {
				using (var conn = new MySqlConnection (CommonConst.ConStr)) {
					return conn.Query<RptDesign> ("select * from rpt_design where RptName=@RptName", new {RptName = rptName}).AsList ().FirstOrDefault ();
				}

			} catch (Exception ex) {
				log.Error (string.Format ("message:{0},StackTrace:{1}", ex.Message, ex.StackTrace));
				return null;
			}
		}

		public static List<RptDesign> GetRptDesignByIds (List<string> ids)
		{
			try {
				if (ids == null || ids.Count == 0)
					return null;
				string idsStr = string.Join (",", ids);
				using (var conn = new MySqlConnection (CommonConst.ConStr)) {
					return conn.Query<RptDesign> ("select * from rpt_design where id in (@ids)", new {ids = idsStr}).AsList ();
				}

			} catch (Exception ex) {
				log.Error (string.Format ("message:{0},StackTrace:{1}", ex.Message, ex.StackTrace));
				return null;
			}
		}

		public static List<RptDesign> GetRptDesignByDirId (string dirId)
		{
			try {
				using (var conn = new MySqlConnection (CommonConst.ConStr)) {
					var list = conn.Query<RptDesign> ("select * from rpt_design where DirId=@DirId", new {DirId = dirId}).AsList ();
					if (list != null && list.Count > 0) {
						foreach (var l in list) {
							DateTime dt = DateTime.MinValue;
							if (DateTime.TryParseExact (l.UploadTime, "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.AssumeLocal, out dt)) {
								l.UploadTime = dt.ToString ("yyyy-MM-dd HH:mm:ss");
							}
						}
					}
					return list;
				}

			} catch (Exception ex) {
				log.Error (string.Format ("message:{0},StackTrace:{1}", ex.Message, ex.StackTrace));
				return null;
			}
		}

		public static List<RptDesign> GetAllRptDesign ()
		{
			try {
				using (var conn = new MySqlConnection (CommonConst.ConStr)) {
					return conn.GetAll<RptDesign> ().AsList ();
				}

			} catch (Exception ex) {
				log.Error (string.Format ("message:{0},StackTrace:{1}", ex.Message, ex.StackTrace));
				return null;
			}
		}

		public static List<RptDirectory> GetAllRptDirectory ()
		{
			try {
				using (var conn = new MySqlConnection (CommonConst.ConStr)) {
					var list = conn.GetAll<RptDirectory> ().AsList ();
					foreach (var dir in list) {
						dir.DirPath = dir.DirPath.Replace ('$', Path.DirectorySeparatorChar);
					}

					return list;
				}

			} catch (Exception ex) {
				log.Error (string.Format ("message:{0},StackTrace:{1}", ex.Message, ex.StackTrace));
				return null;
			}
		}

		public static RptDirectory GetRptDirectoryById (string id)
		{
			try {
				using (var conn = new MySqlConnection (CommonConst.ConStr)) {
					var dir = conn.Get<RptDirectory> (id);
					if (dir != null) {
						dir.DirPath = dir.DirPath.Replace ('$', Path.DirectorySeparatorChar);
					}
					return dir;
				}

			} catch (Exception ex) {
				log.Error (string.Format ("message:{0},StackTrace:{1}", ex.Message, ex.StackTrace));
				return null;
			}
		}

		public static List<RptDirectory> GetRptDirectoryByName (string name)
		{
			try {
				if (string.IsNullOrEmpty (name))
					return null;
				
				using (var conn = new MySqlConnection (CommonConst.ConStr)) {
					var list = conn.Query<RptDirectory> ("select * from directory where dirpath=@Name", new {Name = name}).AsList ();
					foreach (var dir in list) {
						dir.DirPath = dir.DirPath.Replace ('$', Path.DirectorySeparatorChar);
					}

					return list;
				}

			} catch (Exception ex) {
				log.Error (string.Format ("message:{0},StackTrace:{1}", ex.Message, ex.StackTrace));
				return null;
			}
		}

		public static bool DeleteRptDirectory (RptDirectory dir)
		{
			IDbTransaction tran = null;
			try {
				if (dir == null || string.IsNullOrEmpty (dir.Id))
					return false;
				using (var conn = new MySqlConnection (CommonConst.ConStr)) {
					conn.Open ();
					tran = conn.BeginTransaction ();
					var flag = false;
					var list = conn.Query<RptDesign> ("select * from rpt_design where DirId=@DirId", new {DirId = dir.Id}, tran).AsList ();
					if (list == null || list.Count == 0) {
						flag = conn.Delete (dir, tran);
					} else {
						foreach (var rpt in list) {
							var result = conn.Execute ("delete from rpt_runtime where RptId=@RptId", new {RptId = rpt.Id}, tran);
							flag &= conn.Delete (rpt, tran);
							flag &= result > 0;
						}

						flag &= conn.Delete (dir, tran);
					}
					tran.Commit ();
					return flag;
				}

			} catch (Exception ex) {
				log.Error (string.Format ("message:{0},StackTrace:{1}", ex.Message, ex.StackTrace));
				if (tran != null) {
					tran.Rollback ();
				}
				return false;
			}
		}

		public static bool DeleteRptDesign (List<string> rptIds)
		{
			IDbTransaction tran = null;
			try {
				if (rptIds == null || rptIds.Count == 0)
					return false;
				using (var conn = new MySqlConnection (CommonConst.ConStr)) {
					conn.Open ();
					tran = conn.BeginTransaction ();
					var flag = false;
					foreach (var rpt in rptIds) {
						flag = conn.Delete (new RptDesign (){ Id = rpt });
						var result = conn.Execute ("delete from rpt_runtime where RptId=@RptId", new {RptId = rpt}, tran);
						flag &= result > 0;
					}

					tran.Commit ();
					return flag;
				}

			} catch (Exception ex) {
				log.Error (string.Format ("message:{0},StackTrace:{1}", ex.Message, ex.StackTrace));
				if (tran != null) {
					tran.Rollback ();
				}
				return false;
			}
		}

		public static bool DeleteRptRuntime (RptRuntime rpt)
		{
			IDbTransaction tran = null;
			try {
				if (rpt == null || string.IsNullOrEmpty (rpt.Id))
					return false;
				using (var conn = new MySqlConnection (CommonConst.ConStr)) {
					conn.Open ();
					tran = conn.BeginTransaction ();
					var flag = conn.Delete (rpt);
					var result = conn.Execute ("delete from rpt_design where Id=@Id", new {Id = rpt.RptId}, tran);
					flag &= result > 0;
					
					tran.Commit ();
					return flag;
				}

			} catch (Exception ex) {
				log.Error (string.Format ("message:{0},StackTrace:{1}", ex.Message, ex.StackTrace));
				if (tran != null) {
					tran.Rollback ();
				}
				return false;
			}
		}

		public static bool DeleteDir ()
		{
			return false;
		}
	}
}

