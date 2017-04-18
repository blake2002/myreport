using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Xml;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using PMS.Libraries.ToolControls.PMSPublicInfo;
using System.Text.RegularExpressions;

namespace NetSCADA.ReportEngine
{
	/// <summary>
	/// 数据表管理类
	/// </summary>
	public class DataTableManager : IDataTableManager
	{

		#region 基本属性

		/// <summary>
		/// 报表引擎
		/// </summary>
		private ReportRuntime _ReportRuntime = null;

		public ReportRuntime ReportRuntime {
			set { _ReportRuntime = value; }
			get { return _ReportRuntime; }
		}

		#endregion

		#region 数据源连接管理

		private Dictionary<string, IDbConnection> _DbConnections = new Dictionary<string, IDbConnection> ();

		public bool AddDBConnect (string name, IDbConnection con)
		{
			if (string.IsNullOrEmpty (name) || con == null) {
				return false;
			}
			string nameTemp = name.ToLower ();
			if (!_DbConnections.ContainsKey (nameTemp)) {
				_DbConnections [nameTemp] = con;
				return true;
			}
			return false;
		}

		public bool DeleteDBConnect (string name)
		{
			if (string.IsNullOrEmpty (name)) {
				return false;
			}
			string nameTemp = name.ToLower ();
			if (_DbConnections.ContainsKey (nameTemp)) {
				try {
					_DbConnections [nameTemp].Close ();
				} catch {
				}
				_DbConnections.Remove (nameTemp);
				return true;
			}
			return false;
		}

		public IDbConnection GetDBConnect (string name)
		{
			if (string.IsNullOrEmpty (name)) {
				return null;
			}
			string nameTemp = name.ToLower ();
			if (_DbConnections.ContainsKey (nameTemp)) {
				return _DbConnections [nameTemp];
			}
			return null;
		}

		public void CloseAllDBConnects ()
		{
			try {
				foreach (KeyValuePair<string, IDbConnection> con in _DbConnections) {
					if (con.Value != null) {
						con.Value.Close ();
					}
				}
			} catch (System.Exception ex) {

			} finally {
				this.ReportRuntime = null;//todo:qiuleilei
				_DbConnections.Clear ();
			}
		}

		#endregion


		#region 数据表管理

		/// <summary>
		/// 数据集
		/// </summary>
		private DataSet _DataSet = new DataSet ();

		/// <summary>
		/// 添加数据表
		/// </summary>
		/// <param name="newDataTable">新增数据表</param>
		/// <param name="bMerge">如果数据表已经存在的情况下，将新增数据表合并到已存在的数据表中</param>
		/// <returns>添加成功则返回true</returns>
		public bool AddDataTable (DataTable newDataTable, bool bMerge)
		{
			if (null == newDataTable) {
				return false;
			}
			lock (this) {
				DataTable tempDataTable = _DataSet.Tables [newDataTable.TableName];
				if (null == tempDataTable) {
					_DataSet.Tables.Add (newDataTable);
					return true;
				} else {
					if (bMerge == true) {
						tempDataTable.Merge (newDataTable);
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// 删除数据表
		/// </summary>
		/// <param name="tableName">待删除的数据表名</param>
		/// <returns></returns>
		public bool RemoveDataTable (string tableName)
		{
			lock (this) {
				DataTable tempDataTable = _DataSet.Tables [tableName];
				_DataSet.Tables.Remove (tableName);
				if (tempDataTable != null) {
					tempDataTable.Dispose ();
				}
				return true;
			}
			return false;
		}

		/// <summary>
		/// 删除所有的数据表
		/// </summary> 
		/// <returns></returns>
		public bool RemoveAllDataTables ()
		{
			lock (this) {
				#if DEBUG
				return true;
				#endif
				foreach (DataTable tb in _DataSet.Tables) {
					if (tb != null) {
						tb.Dispose ();
					}
				}
				_DataSet.Tables.Clear ();
				return true;
			}
			return false;
		}

		/// <summary>
		/// 获取数据表
		/// </summary>
		/// <param name="tableName">待返回的数据表名</param>
		/// <returns></returns>
		public DataTable GetDataTable (string tableName)
		{
			DataTable tempDataTable = null;
			lock (this) {
				tempDataTable = _DataSet.Tables [tableName];
			}
			return tempDataTable;
		}

		/// <summary>
		/// 查找数据表
		/// </summary>
		/// <param name="tableName">待查找的数据表名</param>
		/// <returns></returns>
		public bool FindDataTable (string tableName)
		{
			bool bRet = false;
			lock (this) {
				bRet = _DataSet.Tables.Contains (tableName);
			}
			return bRet;
		}

		/// <summary>
		/// 获取数据管理类中的所有表集合
		/// </summary>
		/// <returns></returns>
		public DataTableCollection GetDataTables ()
		{
			lock (this) {
				return _DataSet.Tables;
			}
		}

		#endregion

		#region 系统变量管理

		private DataTable _VariableDataTable = new DataTable ();

		public DataTable VariableDataTable {
			get {
				return _VariableDataTable;
			}
		}

		public void ResetVariableDataTable ()
		{
			if (null != _VariableDataTable) {
				_VariableDataTable.Dispose ();
				_VariableDataTable = null;
			}
			_VariableDataTable = new DataTable ();

		}

		public object GetVariableObjValue (string varName)
		{
			return GetVariableObjValue (_VariableDataTable.Columns.IndexOf (varName));
		}

		public object GetVariableObjValue (int colIndex)
		{
			if (_VariableDataTable.Rows.Count > 0 && colIndex >= 0 &&
			    colIndex < _VariableDataTable.Columns.Count) {
				return _VariableDataTable.Rows [0] [colIndex];
			}
			return null;
		}

		public string GetVariableStringValue (string varName)
		{
			return GetVariableStringValue (_VariableDataTable.Columns.IndexOf (varName));
		}

		public string GetVariableStringValue (int colIndex)
		{
			if (_VariableDataTable.Rows.Count > 0 && colIndex >= 0 &&
			    colIndex < _VariableDataTable.Columns.Count) {
				return _VariableDataTable.Rows [0] [colIndex].ToString ();
			}
			return null;
		}

		public int GetVariableCount ()
		{
			return _VariableDataTable.Columns.Count;
		}

		public void SetVariableValue (string varName, object value)
		{
			int colIndex = _VariableDataTable.Columns.IndexOf (varName);
			if (_VariableDataTable.Rows.Count > 0 && colIndex >= 0 &&
			    colIndex < _VariableDataTable.Columns.Count) {
				_VariableDataTable.Rows [0] [colIndex] = value;
			}
		}

		/// <summary>
		/// 初始化系统变量表
		/// </summary>
		/// <param name="varList">系统变量链表</param>
		/// <returns></returns>
		public bool InitialVariableDataTable (List<PMSVar> varList)
		{
			lock (this) {

				ResetVariableDataTable ();
				_VariableDataTable.TableName = "DE60CD9D-B959-4f91-A57C-50009AA052D6";//Properties.Resources.ResourceManager.GetString("context0001");
				_VariableDataTable.Columns.Clear ();
				DataColumn col = null;
				if (null != varList && varList.Count > 0) {
					foreach (PMSVar pv in varList) {
						if (_VariableDataTable.Columns.Contains (pv.VarName) == true) {
							continue;
						}

						col = new DataColumn ();
						col.ColumnName = pv.VarName;
						switch (pv.VarType) {
						case MESVarType.MESBool:
							col.DataType = typeof(bool);
							break;
						case MESVarType.MESDateTime:
							col.DataType = typeof(DateTime);
							break;
						case MESVarType.MESInt:
							col.DataType = typeof(int);
							break;
						case MESVarType.MESReal:
							col.DataType = typeof(double);
							break;
						case MESVarType.MESString:
							col.DataType = typeof(string);
							break;
						default:
							col.DataType = typeof(string);
							break;

						}
						if (col.DataType == typeof(bool)) {
							if (pv.VarValue == null || string.IsNullOrEmpty (pv.VarValue.ToString ().Trim ())) {
								col.DefaultValue = false;
							} else {
								col.DefaultValue = pv.VarValue;
							}
						} else {
							col.DefaultValue = pv.VarValue;
						}
						_VariableDataTable.Columns.Add (col);
					}
				}
				//页码先不添加，由报表解析完成后添加
				/*
                col = new DataColumn();
                col.ColumnName = "#PageIndex#";
                col.DataType = typeof(int);
                col.DefaultValue = 0;
                _VariableDataTable.Columns.Add(col);

                col = new DataColumn();
                col.ColumnName = "#PageCount#";
                col.DataType = typeof(int);
                col.DefaultValue = 0;
                _VariableDataTable.Columns.Add(col);
                */

				col = new DataColumn ();
				col.ColumnName = "#Year#";
				col.DataType = typeof(int);
				col.DefaultValue = DateTime.Now.Year;
				_VariableDataTable.Columns.Add (col);

				col = new DataColumn ();
				col.ColumnName = "#Month#";
				col.DataType = typeof(int);
				col.DefaultValue = DateTime.Now.Month;
				_VariableDataTable.Columns.Add (col);

				col = new DataColumn ();
				col.ColumnName = "#Day#";
				col.DataType = typeof(int);
				col.DefaultValue = DateTime.Now.Day;
				_VariableDataTable.Columns.Add (col);

				col = new DataColumn ();
				col.ColumnName = "#Hour#";
				col.DataType = typeof(int);
				col.DefaultValue = DateTime.Now.Hour;
				_VariableDataTable.Columns.Add (col);

				col = new DataColumn ();
				col.ColumnName = "#Minute#";
				col.DataType = typeof(int);
				col.DefaultValue = DateTime.Now.Minute;
				_VariableDataTable.Columns.Add (col);

				col = new DataColumn ();
				col.ColumnName = "#Second#";
				col.DataType = typeof(int);
				col.DefaultValue = DateTime.Now.Second;
				_VariableDataTable.Columns.Add (col);

				_VariableDataTable.Rows.Add (_VariableDataTable.NewRow ());
				/*/
                if(_DataSet.Tables.Contains(_VariableDataTable.TableName) == false)
                {
                    _DataSet.Tables.Add(_VariableDataTable);
                } 
                /*/
			}
			return true;
		}

		/// <summary>
		/// 添加页码列
		/// </summary>
		/// <param name="pageCount"></param>
		/// <returns></returns>
		public bool AddPageParameter (int pageCount)
		{
			if (pageCount < 0) {
				pageCount = 0;
			}
			lock (this) {
				DataColumn col = new DataColumn ();
				col = new DataColumn ();
				col.ColumnName = "#PageIndex#";
				col.DataType = typeof(int);
				col.DefaultValue = 0;
				_VariableDataTable.Columns.Add (col);
				col = new DataColumn ();
				col.ColumnName = "#PageCount#";
				col.DataType = typeof(int);
				col.DefaultValue = pageCount;
				_VariableDataTable.Columns.Add (col);
			}
			return true;
		}

		#endregion

		#region XmlDataDocument 管理

		private XmlDataDocument _DataSetXmlDataDocument = null;
		//数据集关联的XmlDataDocument

		/// <summary>
		/// 数据集关联的XmlDataDocument
		/// </summary>
		public XmlDataDocument DataSetXmlDataDocument {
			get {
				return _DataSetXmlDataDocument;
			}
		}

		/// <summary>
		/// 创建数据集关联的XmlDataDocument
		/// </summary>
		/// <returns></returns>
		public XmlDataDocument CtrateDataSetXmlDataDocument ()
		{
			lock (this) {
				_DataSetXmlDataDocument = new XmlDataDocument (_DataSet);
			}
			return _DataSetXmlDataDocument;
		}

		#endregion

		#region 数据库查询

		private List<PMSRefDBConnectionObj> _DBSourceConfigObjList = null;

		/// <summary>
		/// 初始化数据表管理器
		/// </summary>
		/// <param name="fieldTreeViewData">数据集定义</param>
		/// <param name="varList">变量链表</param>
		public void Initial (List<PMSRefDBConnectionObj> dbSourceConfigObjList, FieldTreeViewData fieldTreeViewData, List<PMSVar> varList)
		{
			if (fieldTreeViewData == null) {
				return;
			}
			lock (this) {
				CloseAllDBConnects ();
				_DBSourceConfigObjList = dbSourceConfigObjList;
				RemoveAllDataTables ();
				InitialVariableDataTable (varList);
				QueryNodeDataTable (fieldTreeViewData.Nodes [0], null);
			}
		}

		/// <summary>
		/// 查询数据集节点数据表
		/// </summary>
		/// <param name="node">数据集节点</param>
		/// <param name="parentNodeDataTale">数据集节点父节点对应的数据表</param>
		private void QueryNodeDataTable (FieldTreeNodeData node, DataTable parentNodeDataTale)
		{
			if (node == null || node.Nodes == null) {
				return;
			}
			foreach (FieldTreeNodeData childNode in node.Nodes) {
				if (null != ReportRuntime && ReportRuntime.StopAnalyseReport)
					return;
				if (childNode.Tag == null) {
					continue;
				}
				SourceField tempsf = childNode.Tag as SourceField;
				if (!string.IsNullOrEmpty (tempsf.SqlText)) {
					if (parentNodeDataTale != null) {
						if (parentNodeDataTale.Rows.Count > 0) {//父表如果没有记录则不查询子表
							int iRowIndex = 0;
							foreach (DataRow dr in parentNodeDataTale.Rows) {
								if (null != ReportRuntime && ReportRuntime.StopAnalyseReport)
									return;
								DataTable tempChildTable = QueryDataTable (tempsf, dr);
								if (tempChildTable != null) {
									tempChildTable.TableName = string.Format ("{0}[{1:d}].{2}", parentNodeDataTale.TableName, iRowIndex, tempsf.Name);
									AddDataTable (tempChildTable, true);
									QueryNodeDataTable (childNode, tempChildTable);
								}
								iRowIndex++;
							}
						} else {
							System.Exception ex = new InformationException (string.Format ("Since DataTables[{0}].Rows.Count = 0,can not query the child table [{1}]", parentNodeDataTale.TableName, tempsf.Name));
							_ReportRuntime.AddReportLog (ex);
						}
					} else {
						DataTable tempChildTable = QueryDataTable (tempsf, null);
						if (tempChildTable != null) {
							tempChildTable.TableName = tempsf.Name;
							AddDataTable (tempChildTable, true);
							QueryNodeDataTable (childNode, tempChildTable);
						}
					}
				}
			}
		}

		/// <summary>
		/// 查询数据表
		/// </summary>
		/// <param name="sf">数据表定义</param>
		/// <param name="relationDataRow">数据表外联的父表数据行</param>
		/// <returns></returns>
		private DataTable QueryDataTable (SourceField sf, DataRow relationDataRow)
		{
			if (null == sf) {
				return null;
			}
			if (string.IsNullOrEmpty (sf.SqlText) == true) {
				return null;
			}

			string sqlCmd = sf.SqlText;

			//关联字段值替换
			if (relationDataRow != null && (sqlCmd.IndexOf ("[#") > 0 && sqlCmd.IndexOf ("#]") > 0)) {
				foreach (DataColumn col in relationDataRow.Table.Columns) {
					string strFieldName = "[#" + col.ColumnName + "#]";
					int indexOf = sqlCmd.IndexOf (strFieldName, StringComparison.CurrentCultureIgnoreCase);
					if (indexOf > 0) {
						//不能用strFieldName替换，存在不区分大小写的问题
						sqlCmd = sqlCmd.Replace (sqlCmd.Substring (indexOf, strFieldName.Length), relationDataRow [col].ToString ());
						if (sqlCmd.IndexOf ("[#") <= 0 || sqlCmd.IndexOf ("#]") <= 0) {
							break;
						}
					}
				}
			}

			//关联变量值替换
			if (VariableDataTable.Rows.Count > 0 && (sqlCmd.IndexOf ("[%") > 0 && sqlCmd.IndexOf ("%]") > 0)) {
				foreach (DataColumn col in VariableDataTable.Rows[0].Table.Columns) {
					string strFieldName = "[%" + col.ColumnName + "%]";
					int indexOf = sqlCmd.IndexOf (strFieldName, StringComparison.CurrentCultureIgnoreCase);
					if (indexOf > 0) {
						//不能用strFieldName替换，存在不区分大小写的问题
						sqlCmd = sqlCmd.Replace (sqlCmd.Substring (indexOf, strFieldName.Length), VariableDataTable.Rows [0] [col].ToString ());
						if (sqlCmd.IndexOf ("[%") <= 0 || sqlCmd.IndexOf ("%]") <= 0) {
							break;
						}
					}
				}
			}
			if ((sqlCmd.IndexOf ("[%") > 0 && sqlCmd.IndexOf ("%]") > 0) ||
			    (sqlCmd.IndexOf ("[#") > 0 && sqlCmd.IndexOf ("#]") > 0)) {
				System.Exception ex = new System.Exception ("No variable or nested query field!");
				_ReportRuntime.AddReportLog (ex);
				return null;
			}

			DataTable retTable = null;
			if (null != sf.MultiDataSource && sf.MultiDataSource.Count > 0) {
				// 启用多数据源不同语句配置模式,不配置sql语句则为相同语句模式
				foreach (DSSqlPair dsp in sf.MultiDataSource) {
					if (null != ReportRuntime && ReportRuntime.StopAnalyseReport)
						return null;
					if (string.IsNullOrEmpty (dsp.Sql))
						dsp.Sql = sf.SqlText;
					DataTable tempTable = GetDataTableFromDataSource (dsp.DataSource, dsp.Sql);
					if (tempTable != null) {
						if (sf.IsSampling) {//间隔筛选
							DataTable intervalTable = GetIntervalDataTable (sf, tempTable);
							if (retTable == null) {
								retTable = intervalTable;
							} else {
								retTable.Merge (intervalTable);
							}
						} else {
							if (retTable == null) {
								retTable = tempTable;
							} else {
								retTable.Merge (tempTable);
							}
						}
					}
				}
			} else {
				// 单数据源模式或者多数据源相同语句模式
				string[] dataSourceNameArray = sf.DBSource.Split (',');
				foreach (string dataSourceName in dataSourceNameArray) {
					if (null != ReportRuntime && ReportRuntime.StopAnalyseReport)
						return null;
					DataTable tempTable = GetDataTableFromDataSource (dataSourceName, sqlCmd);
					if (tempTable != null) {
						if (sf.IsSampling) {//间隔筛选
							DataTable intervalTable = GetIntervalDataTable (sf, tempTable);
							if (retTable == null) {
								retTable = intervalTable;
							} else {
								retTable.Merge (intervalTable);
							}
						} else {
							if (retTable == null) {
								retTable = tempTable;
							} else {
								retTable.Merge (tempTable);
							}
						}
					}
				}
			}

			if (null != retTable) {
				if (!string.IsNullOrEmpty (sf.SecondarySort)) {
					DataView dv = retTable.DefaultView;
					dv.Sort = sf.SecondarySort;
					retTable = dv.ToTable ();
				}
				return retTable;
			}

			return null;
		}

		/// <summary>
		/// 从数据源获取数据表
		/// </summary>
		/// <param name="dataSourceName">数据源名</param>
		/// <param name="sqlCmd">SQL语句</param>
		/// <returns></returns>
		private DataTable GetDataTableFromDataSource (string dataSourceName, string sqlCmd)
		{
			if (_DBSourceConfigObjList == null) {
				return null;
			}
			PMSRefDBConnectionObj curConnection = null;
			if (!string.IsNullOrEmpty (dataSourceName)) {
				foreach (PMSRefDBConnectionObj db in _DBSourceConfigObjList) {
					if (string.Compare (db.StrName, dataSourceName, true) == 0) {
						curConnection = db;
						break;
					}
				}
			} else {//如果没有配置数据源，则启用默认数据源
				foreach (PMSRefDBConnectionObj db in _DBSourceConfigObjList) {
					if (db.BDefault) {
						curConnection = db;
						break;
					}
				}
			}

			if (curConnection == null || curConnection.RefDBConnection == null) {
				System.Exception ex = new System.Exception (string.Format ("Don't found data source:{0}", dataSourceName));
				_ReportRuntime.AddReportLog (ex);
				return null;
			}

			try {
				lock (curConnection) {

					IDbConnection tempDbConnection = GetDBConnect (curConnection.StrName);
					if (tempDbConnection == null) {
						tempDbConnection = curConnection.RefDBConnection.GetConnection (true);
						if (tempDbConnection == null) {
							System.Exception ex = new System.Exception (string.Format ("Failed to connect data source:{0}", dataSourceName));
							_ReportRuntime.AddReportLog (ex);
							return null;
						} else {
							AddDBConnect (curConnection.StrName, tempDbConnection);
						}
					}

					IDbCommand tempDbCommand = tempDbConnection.CreateCommand ();
					tempDbCommand.CommandText = sqlCmd;
					IDataReader tempDbDataReader = tempDbCommand.ExecuteReader ();
					DataTable dt = new DataTable ();
					dt.Load (tempDbDataReader);
					tempDbDataReader.Close ();
					tempDbCommand.Dispose ();
					DeleteDBConnect (curConnection.StrName);
					return dt;
				}
			} catch (Exception ex) {
				System.Exception ex1 = new System.Exception (string.Format ("Query error:{0}{1}", ex.Message, sqlCmd));
				_ReportRuntime.AddReportLog (ex1);
			}
			return null;
		}

		/// <summary>
		/// 数据表排序
		/// </summary>
		/// <param name="sourceTable">待排序的数据表</param>
		/// <param name="sortField">排序</param>
		/// <param name="sortType">排序类型</param>
		/// <returns></returns>
		public DataTable SortDataTable (DataTable sourceTable, string sortField, MESSortType sortType)
		{
			DataTable dt2 = null;
			try {
				DataView dv = sourceTable.Copy ().DefaultView;
				dv.Sort = sortField + " " + sortType.ToString ();
				dt2 = dv.ToTable ();
			} catch (Exception e) {
				System.Exception ex = new System.Exception (string.Format ("SortDataTable error:{0}", e.Message));
				_ReportRuntime.AddReportLog (ex);
			}
			return dt2;
		}

		/// <summary> 
		/// 间隔时间计算
		/// </summary>
		/// <param name="start"></param>
		/// <param name="sf"></param>
		/// <returns></returns>
		public DateTime GetAddedDateTime (DateTime start, SourceField sf)
		{
			DateTime reTime = new DateTime ();
			switch (sf.TimeUnit) {
			case MESTimePart.Year:
				reTime = start.AddYears (sf.SamplingDistance);
				break;
			case MESTimePart.Month:
				reTime = start.AddMonths (sf.SamplingDistance);
				break;
			case MESTimePart.Day:
				reTime = start.AddDays (sf.SamplingDistance);
				break;
			case MESTimePart.Hour:
				reTime = start.AddHours (sf.SamplingDistance);
				break;
			case MESTimePart.Minute:
				reTime = start.AddMinutes (sf.SamplingDistance);
				break;
			case MESTimePart.Second:
				reTime = start.AddSeconds (sf.SamplingDistance);
				break;
			}

			//SourceFieldDataTable增加了一个属性SampleDistanceVar(用变量来控制筛选间隔)
			SourceFieldDataTable temp = sf as SourceFieldDataTable;

			if (!string.IsNullOrEmpty (temp.SampleDistanceVar)) {
				object tempValue = GetVariableObjValue (temp.SampleDistanceVar);
				if (tempValue != null) {
					try {
						int inttemp = System.Convert.ToInt32 (tempValue);
						switch (sf.TimeUnit) {
						case MESTimePart.Year:
							reTime = start.AddYears (inttemp);
							break;
						case MESTimePart.Month:
							reTime = start.AddMonths (inttemp);
							break;
						case MESTimePart.Day:
							reTime = start.AddDays (inttemp);
							break;
						case MESTimePart.Hour:
							reTime = start.AddHours (inttemp);
							break;
						case MESTimePart.Minute:
							reTime = start.AddMinutes (inttemp);
							break;
						case MESTimePart.Second:
							reTime = start.AddSeconds (inttemp);
							break;
						}
					} catch {
					}
				}
			}
			return reTime;
		}

		/// 间隔时间计算
		/// </summary>
		/// <param name="start"></param>
		/// <param name="sf"></param>
		/// <returns></returns>
		public DateTime GetSubedDateTime (DateTime start, SourceField sf)
		{
			DateTime reTime = new DateTime ();
			switch (sf.TimeUnit) {
			case MESTimePart.Year:
				reTime = start.AddYears (-sf.SamplingDistance);
				break;
			case MESTimePart.Month:
				reTime = start.AddMonths (-sf.SamplingDistance);
				break;
			case MESTimePart.Day:
				reTime = start.AddDays (-sf.SamplingDistance);
				break;
			case MESTimePart.Hour:
				reTime = start.AddHours (-sf.SamplingDistance);
				break;
			case MESTimePart.Minute:
				reTime = start.AddMinutes (-sf.SamplingDistance);
				break;
			case MESTimePart.Second:
				reTime = start.AddSeconds (-sf.SamplingDistance);
				break;
			}

			//SourceFieldDataTable增加了一个属性SampleDistanceVar(用变量来控制筛选间隔)
			SourceFieldDataTable temp = sf as SourceFieldDataTable;

			if (!string.IsNullOrEmpty (temp.SampleDistanceVar)) {
				object tempValue = GetVariableObjValue (temp.SampleDistanceVar);
				if (tempValue != null) {
					try {
						int inttemp = System.Convert.ToInt32 (tempValue);
						switch (sf.TimeUnit) {
						case MESTimePart.Year:
							reTime = start.AddYears (-inttemp);
							break;
						case MESTimePart.Month:
							reTime = start.AddMonths (-inttemp);
							break;
						case MESTimePart.Day:
							reTime = start.AddDays (-inttemp);
							break;
						case MESTimePart.Hour:
							reTime = start.AddHours (-inttemp);
							break;
						case MESTimePart.Minute:
							reTime = start.AddMinutes (-inttemp);
							break;
						case MESTimePart.Second:
							reTime = start.AddSeconds (-inttemp);
							break;
						}
					} catch {
					}
				}
			}
			return reTime;
		}

		/// <summary>
		/// 获取间隔筛选数据表
		/// </summary>
		/// <param name="sf">间隔筛选定义</param>
		/// <param name="sourceTable">待筛选的数据表</param>
		/// <returns></returns>
		public DataTable GetIntervalDataTable (SourceField sf, DataTable sourceTable)
		{
			if (!sf.IsSampling || string.IsNullOrEmpty (sf.SamplingField) ||
			    sourceTable == null || sourceTable.Rows.Count <= 0) {
				return sourceTable;
			}

			DataTable newTable = SortDataTable (sourceTable, sf.SamplingField, MESSortType.ASC);

			//采样开始与结束时间判断
			DateTime start = DateTime.Now;
			DateTime end = DateTime.Now;
			DateTime nullDate = new DateTime (1, 1, 1, 0, 0, 0);

			bool bEndNotNull = false;

			if (sf.SamplingEnd == nullDate && sf.SamplingStart == nullDate) {
				if (newTable.Rows [0] [sf.SamplingField].GetType () != typeof(System.DBNull)) {
					start = Convert.ToDateTime (newTable.Rows [0] [sf.SamplingField]);
					end = GetAddedDateTime (start, sf);
				} else {
					return newTable;
				}
			} else if (sf.SamplingEnd == null && sf.SamplingStart != null) {
				if (newTable.Rows [0] [sf.SamplingField].GetType () != typeof(System.DBNull)) {
					start = Convert.ToDateTime (newTable.Rows [0] [sf.SamplingField]);

					if (sf.SamplingStart > start)
						start = sf.SamplingStart;

					end = GetAddedDateTime (start, sf);
				} else {
					return newTable;
				}
			} else if (sf.SamplingEnd != null && sf.SamplingStart == null) {
				if (newTable.Rows [0] [sf.SamplingField].GetType () != typeof(System.DBNull)) {
					start = Convert.ToDateTime (newTable.Rows [0] [sf.SamplingField]);

					bEndNotNull = true;
					//第一条记录超过了采样结束时间
					if (start > sf.SamplingEnd)
						return null;
					end = GetAddedDateTime (start, sf);
					if (end > sf.SamplingEnd)
						end = sf.SamplingEnd;
				} else {
					return newTable;
				}
			} else if (sf.SamplingEnd != null && sf.SamplingStart != null) {
				if (newTable.Rows [0] [sf.SamplingField].GetType () != typeof(System.DBNull)) {
					start = Convert.ToDateTime (newTable.Rows [0] [sf.SamplingField]);

					bEndNotNull = true;
					//第一条记录超过了采样结束时间
					if (start > sf.SamplingEnd)
						return null;
					if (sf.SamplingStart > start)
						start = sf.SamplingStart;
					end = GetAddedDateTime (start, sf);
					if (end > sf.SamplingEnd)
						end = sf.SamplingEnd;
				} else {
					return newTable;
				}
			}

			DateTime actualEnd = DateTime.Now;
			int iValid = 0;
			DataTable returnTable = newTable.Clone ();
			try {
				DataRow lastrow = null;
				int currentRow = 0;
				TimeSpan NearestDistance = new TimeSpan ();
				TimeSpan NextDistance = new TimeSpan ();
				DateTime lastStart;
				DateTime lastend;
				foreach (DataRow dr in newTable.Rows) {
					DateTime now = Convert.ToDateTime (dr [sf.SamplingField]);

					DataRow drnew = returnTable.NewRow ();

					int next = currentRow + 1;
					DateTime nextime1;
					if (next == newTable.Rows.Count) {
						next -= 1;
					}
					nextime1 = Convert.ToDateTime (newTable.Rows [next] [sf.SamplingField]);
					/*===================================================*/
					switch (sf.SamplingMethods) {
					case MESSamplingMethods.Backward:
						{
							if (iValid < sf.SamplingCount && now >= start && now < end) {
								drnew.ItemArray = (object[])dr.ItemArray.Clone ();
								returnTable.Rows.Add (drnew);
								iValid++;
								actualEnd = now;
							} else if (now >= end) {
								if (bEndNotNull && now > sf.SamplingEnd) {
									break;
								}
								if (sf.SamplingType == MESSamplingType.Regularize) {
									start = end;
								} else if (sf.SamplingType == MESSamplingType.Drift) {
									start = now;
								}
								end = GetAddedDateTime (start, sf);
								if (end > sf.SamplingEnd && bEndNotNull) {
									end = sf.SamplingEnd;
								}
								while (end < now) {
									start = end;
									end = GetAddedDateTime (start, sf);
								}
								drnew.ItemArray = (object[])dr.ItemArray.Clone ();
								returnTable.Rows.Add (drnew);
								iValid = 1;
								actualEnd = now;
							}
							if (now <= sf.SamplingEnd || sf.SamplingEnd == nullDate)
								lastrow = dr;
							break;
						}
					case MESSamplingMethods.Forward:
						{
							if (iValid < sf.SamplingCount && now >= start && now < end) {
								drnew.ItemArray = (object[])dr.ItemArray.Clone ();
								returnTable.Rows.Add (drnew);
								iValid++;
								actualEnd = now;
							} else if (now <= end) {
								lastStart = start;
								lastend = end;
								if (bEndNotNull && now > sf.SamplingEnd) {
									break;
								}
								if (sf.SamplingType == MESSamplingType.Regularize) {
									start = end;
								} else if (sf.SamplingType == MESSamplingType.Drift) {
									start = now;
								}
								end = GetAddedDateTime (start, sf);
								if (end > sf.SamplingEnd && bEndNotNull) {
									end = sf.SamplingEnd;
								}
								while (end < now) {
									start = end;
									end = GetAddedDateTime (start, sf);
								}
								drnew.ItemArray = (object[])dr.ItemArray.Clone ();
								DateTime NextTime;
								if (currentRow < newTable.Rows.Count - 1) {
									NextTime = Convert.ToDateTime (newTable.Rows [currentRow + 1] [sf.SamplingField]);

									if (NextTime > start) {
										returnTable.Rows.Add (drnew);
									} else {
										//end = start;
										//start = GetSubedDateTime(end, sf);
										start = lastStart;
										end = lastend;
									}
								} else
									returnTable.Rows.Add (drnew);

								iValid = 1;
								actualEnd = now;
							} else if (now > end) {
								if (bEndNotNull && now > sf.SamplingEnd) {
									break;
								}
								if (sf.SamplingType == MESSamplingType.Regularize) {
									start = end;
								} else if (sf.SamplingType == MESSamplingType.Drift) {
									start = now;
								}
								end = GetAddedDateTime (start, sf);
								if (end > sf.SamplingEnd && bEndNotNull) {
									end = sf.SamplingEnd;
								}
								while (end < now) {
									start = end;
									end = GetAddedDateTime (start, sf);
								}
								drnew.ItemArray = (object[])dr.ItemArray.Clone ();
								DateTime NextTime;
								if (currentRow <= newTable.Rows.Count - 1) {
									NextTime = Convert.ToDateTime (newTable.Rows [currentRow + 1] [sf.SamplingField]);
									if (NextTime > end) {
										returnTable.Rows.Add (drnew);
									}
								}
                                    
								iValid = 1;
								actualEnd = now;
							}
							if (now <= sf.SamplingEnd || sf.SamplingEnd == nullDate)
								lastrow = dr;
							break;
						}
					case MESSamplingMethods.Nearest:
						{
							if (sf.SamplingType == MESSamplingType.Regularize) {
								if (iValid < sf.SamplingCount && now >= start && now < end) {
									drnew.ItemArray = (object[])dr.ItemArray.Clone ();
									returnTable.Rows.Add (drnew);
									iValid++;
									actualEnd = now;
								} else if (now >= end) {
									NearestDistance = now - end;
									if (bEndNotNull && now > sf.SamplingEnd) {
										break;
									}
									if (sf.SamplingType == MESSamplingType.Regularize) {
										start = end;
									}

									end = GetAddedDateTime (start, sf);
									if (end > sf.SamplingEnd && bEndNotNull) {
										end = sf.SamplingEnd;
									}
									while (end < now) {
										start = end;
										end = GetAddedDateTime (start, sf);
									}

									drnew.ItemArray = (object[])dr.ItemArray.Clone ();
									returnTable.Rows.Add (drnew);
									iValid = 1;
									actualEnd = now;
								} else if (now < end) {
									NearestDistance = end - now;
                                       
									if (nextime1 >= end) {
										NextDistance = nextime1 - end;
									} else {
										NextDistance = end - nextime1;
									}
									bool IsSameLeftSide = false;
									if (nextime1 < end) {
										IsSameLeftSide = true;
									}
									if (bEndNotNull && now > sf.SamplingEnd) {
										break;
									}
									if (sf.SamplingType == MESSamplingType.Regularize) {
										start = end;
									}

									end = GetAddedDateTime (start, sf);
									if (end > sf.SamplingEnd && bEndNotNull) {
										end = sf.SamplingEnd;
									}
									while (end < now) {
										start = end;
										end = GetAddedDateTime (start, sf);
									}
									if (NextDistance <= NearestDistance) {
										drnew.ItemArray = (object[])newTable.Rows [next].ItemArray.Clone ();
									} else {
										drnew.ItemArray = (object[])dr.ItemArray.Clone ();
									}
                                       
									bool isContain = false;
									foreach (DataRow row in returnTable.Rows) {
										if (row == drnew) {
											isContain = true;
											break;
										}
									}
									if (IsSameLeftSide) {
										end = GetSubedDateTime (end, sf);
										start = GetSubedDateTime (end, sf);
									}
									if (!isContain && !IsSameLeftSide) {
										returnTable.Rows.Add (drnew);
									}
									iValid = 1;
									actualEnd = now;
								}
							}
							if (now <= sf.SamplingEnd || sf.SamplingEnd == nullDate)
								lastrow = dr;
							break;

                               
						}
					}
					#endregion
					/*===================================================*/

					currentRow += 1;
				}

				if (returnTable.Rows.Count > 0) {
					if (null != lastrow) {
						DateTime endtmp = Convert.ToDateTime (lastrow [sf.SamplingField]);
						if (endtmp > actualEnd) {
							DataRow tmp = returnTable.Rows [returnTable.Rows.Count - 1];
							for (int index = 0; index < tmp.ItemArray.Count (); index++) {
								if (!object.ReferenceEquals (lastrow [index], tmp [index])) {
									returnTable.Rows.Add ((object[])lastrow.ItemArray.Clone ());
									break;
								}
							}
						}
					}
				}
			} catch (Exception ex) {
				_ReportRuntime.AddReportLog (ex);
			}
			return returnTable;
		}


		#region 数据表操作

		public object GetDataFromTable (string tableName, int rowIndex, string colName, string filter = null)
		{
			if (string.IsNullOrEmpty (tableName) || rowIndex < 0 || string.IsNullOrEmpty (colName)) {
				return null;
			}
			DataTable dataTable = this.GetDataTable (tableName);

			if (dataTable == null) {
				return null;
			}
			DataColumn col = dataTable.Columns [colName];
			if (rowIndex < dataTable.Rows.Count && col != null) {
				if (string.IsNullOrEmpty (filter))
					return dataTable.Rows [rowIndex] [col];
				else {
					System.Data.DataRow[] arr = dataTable.Select (filter);
					if (arr.Count () > 0 && rowIndex < arr.Count ()) {
						return arr [rowIndex] [col];
					}
				}
			}
			return null;
		}

		public object GetDataFromTable (string tableName, int rowIndex, int colIndex)
		{
			if (string.IsNullOrEmpty (tableName) || rowIndex < 0 || colIndex < 0) {
				return null;
			}

			DataTable dataTable = this.GetDataTable (tableName);
			if (dataTable == null) {
				return null;
			}
			if (rowIndex < dataTable.Rows.Count && colIndex < dataTable.Columns.Count) {
				return dataTable.Rows [rowIndex] [colIndex];
			}
			return null;
		}

		public string GetStringDataFromTable (string tableName, int rowIndex, string colName)
		{
			if (string.IsNullOrEmpty (tableName) || rowIndex < 0 || string.IsNullOrEmpty (colName)) {
				return null;
			}
			string[] subTableNameArray = colName.Split (new char[] { '/' });
			if (subTableNameArray.Length >= 2) {
				tableName = string.Format ("{0}[{1:d}]", tableName, rowIndex + 1);
				rowIndex = 0;
				for (int i = 0; i < subTableNameArray.Length; i++) {
					if (i == (subTableNameArray.Length - 1)) {
						colName = subTableNameArray [i];
					} else if (i == (subTableNameArray.Length - 2)) {
						int pos = subTableNameArray [i].IndexOf ('[');
						if (pos > 0 && subTableNameArray [i] [subTableNameArray [i].Length - 1] == ']') {
							tableName = string.Format ("{0}.{1}", tableName, subTableNameArray [i].Substring (0, pos));
							rowIndex = System.Convert.ToInt32 (subTableNameArray [i].Substring (pos + 1, subTableNameArray [i].Length - pos - 2));
						} else {
							tableName = string.Format ("{0}.{1}", tableName, subTableNameArray [i]);
						}
					} else {
						if (subTableNameArray [i].IndexOf ('[') > 0 && subTableNameArray [i] [subTableNameArray [i].Length - 1] == ']') {
							tableName = string.Format ("{0}.{1}", tableName, subTableNameArray [i]);
						} else {
							tableName = string.Format ("{0}.{1}[1]", tableName, subTableNameArray [i]);
						}

					}
				}
			}

			DataTable dataTable = this.GetDataTable (tableName);
			if (dataTable == null) {
				return null;
			}
			DataColumn col = dataTable.Columns [colName];
			if (rowIndex < dataTable.Rows.Count && col != null) {
				return dataTable.Rows [rowIndex] [col].ToString ();
			}
			return null;
		}

		public string GetStringDataFromTable (string tableName, int rowIndex, int colIndex)
		{
			if (string.IsNullOrEmpty (tableName) || rowIndex < 0 || colIndex < 0) {
				return null;
			}
			DataTable dataTable = this.GetDataTable (tableName);
			if (dataTable == null) {
				return null;
			}
			if (rowIndex < dataTable.Rows.Count && colIndex < dataTable.Columns.Count) {
				return dataTable.Rows [rowIndex] [colIndex].ToString ();
			}
			return null;
		}

		public object GetDataByPath (string path, string filter = null)
		{
			if (string.IsNullOrEmpty (path)) {
				return null;
			}
			int pos1 = path.LastIndexOf ('.');
			if (pos1 <= 0) {//变量
				return GetVariableObjValue (path);
			} else { //表格取值
				string tableName = path.Substring (0, pos1);
				string fieldName = path.Substring (pos1 + 1);
				tableName = tableName.Trim ();
				fieldName = fieldName.Trim ();

				if (string.IsNullOrEmpty (tableName) || string.IsNullOrEmpty (fieldName)) {
					return null;
				}

				int pos2 = tableName.LastIndexOf ('[');
				int pos3 = tableName.LastIndexOf (']');
				if (pos3 == (tableName.Length - 1) && pos2 > 0 && pos2 < pos3) {//取某行某列的值
					int rowIndex = System.Convert.ToInt32 (tableName.Substring (pos2 + 1, pos3 - pos2 - 1));
					tableName = tableName.Substring (0, pos2);
					return GetDataFromTable (tableName, rowIndex, fieldName, filter);
				} else {//取第一行某列的值
					return null; //return GetDataFromTable(tableName, 0, fieldName);
				}
			}
			return null;
		}

		#endregion

		public string GetSubTableName (string tableName, string parentTableName, int parentTableRowIndex)
		{
			string tempTableName = tableName;
			if (false == string.IsNullOrEmpty (parentTableName)) {
				tempTableName = string.Format ("{0}[{1:d}].{2}", parentTableName, parentTableRowIndex, tableName);
			}

			return tempTableName;
		}
	}
}
