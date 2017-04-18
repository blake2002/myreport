using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PMS.Libraries.ToolControls.PMSPublicInfo.DBSourceDefine
{
	/// <summary>
	/// 数据源定义控件
	/// ProjectPathClass.RefDBSourcesFilePath 外部配置文件路径
	/// ProjectPathClass.RefDBConfigSettingFilePath 最近一次配置
	/// </summary>
	public partial class DBSourceDefineControl : UserControl
	{

		private bool IsModified = false;

		private PMSRefDBConnectionManager _DBDefineManager = null;

		public PMSRefDBConnectionManager DBDefineManager {
			get;
			set;
		}

		public DBSourceDefineControl ()
		{
			InitializeComponent ();
			if (string.IsNullOrEmpty (ProjectPathClass.ProjectPath)) {
				string assemblePath = System.IO.Path.GetDirectoryName (System.Reflection.Assembly.GetExecutingAssembly ().Location);
				if (!assemblePath.EndsWith (System.IO.Path.DirectorySeparatorChar.ToString ()))
					assemblePath += System.IO.Path.DirectorySeparatorChar;
				ProjectPathClass.ProjectPath = assemblePath;
			}
		}

		private void toolStripButton_AddNew_Click (object sender, EventArgs e)
		{
			using (DBRefStructForm dbrf = new DBRefStructForm ()) {
				if (dbrf.ShowDialog () == DialogResult.OK) {
					NewDBSourceForm form = new NewDBSourceForm ();
					form.SDForm = this;
					if (form.ShowDialog () == DialogResult.OK) {
						PMSRefDBConnectionObj source = new PMSRefDBConnectionObj ();
						source.StrName = form.sourceName;
						source.StrDescription = form.description;
						source.RefDBConnection = dbrf.RefDBConnection;
						switch (dbrf.SelectDBType) {
						case RefDBType.MSSqlServer:
							InsertSource (source);
							break;
						case RefDBType.MSAccess:
							InsertSource (source);
							break;
						case RefDBType.Oracle:
							InsertSource (source);
							break;
						case RefDBType.OleDB:
							InsertSource (source);
							break;
						default:
							break;
						}
						SetModified ();
					}
				}
			}
		}

		public ListViewGroup AddGroup (RefDBType refDBType)
		{
			string TypeName = null;
			switch (refDBType) {
			case RefDBType.MSAccess:
				TypeName = RefDBType.MSAccess.ToString ();
				break;
			case RefDBType.MSSqlServer:
				TypeName = RefDBType.MSSqlServer.ToString ();
				break;
			case RefDBType.Oracle:
				TypeName = RefDBType.Oracle.ToString ();
				break;
			case RefDBType.OleDB:
				TypeName = RefDBType.OleDB.ToString ();
				break;
			default:
				break;
			}
			if (listView1.Groups [TypeName] == null) {
				ListViewGroup group = new ListViewGroup (TypeName, TypeName);
				listView1.Groups.Add (group);
				return group;
			} else
				return listView1.Groups [TypeName];
		}

		public void InsertSource (PMSRefDBConnectionObj source)
		{
			string name = source.StrName;
			string description = source.StrDescription;
			PMSRefDBConnection rc = source.RefDBConnection;
			ListViewGroup group = null;

			switch (rc.RefDBType) {
			case RefDBType.MSAccess:
				group = AddGroup (RefDBType.MSAccess);
				break;
			case RefDBType.MSSqlServer:
				group = AddGroup (RefDBType.MSSqlServer);
				break;
			case RefDBType.Oracle:
				group = AddGroup (RefDBType.Oracle);
				break;
			case RefDBType.OleDB:
				group = AddGroup (RefDBType.OleDB);
				break;
			default:
				break;
			}
			this.listView1.BeginUpdate ();
			ListViewItem lvitem = new ListViewItem (name, group);
			lvitem.SubItems.Add (description);
			lvitem.Tag = rc;
			if (source.BDefault == true)
				lvitem.ForeColor = Color.Purple;
			this.listView1.Items.Add (lvitem);
			this.listView1.EndUpdate ();
		}

		public void InsertSource (PMSRefDBConnectionObj source, bool bRecord)
		{
			InsertSource (source);
			if (bRecord) {
				string name = source.StrName;
				string description = source.StrDescription;
				PMSRefDBConnection rc = source.RefDBConnection;
				s_DBSourceDefine sourceDefine = new s_DBSourceDefine ();
				sourceDefine.Name = name;
				sourceDefine.Description = description;
				sourceDefine.DBType = (int)(rc.RefDBType);
				sourceDefine.DBServer = rc.StrServerName;
				sourceDefine.DBName = rc.StrDBName;
				sourceDefine.UserID = rc.StrUserID;
				sourceDefine.Password = rc.StrPassWord;
				sourceDefine.Protocol = rc.EConnectType.ToString ();
				sourceDefine.PortID = rc.StrPortID;
				sourceDefine.ConnectAs = rc.StrConnectAs;
				sourceDefine.Direct = rc.BDirect;
				sourceDefine.Path = rc.StrDBPath;
				sourceDefine.ConnectString = rc.ConnectString;

				PMSDBStructure.PMSCenterDataContext.AddTos_DBSourceDefine (sourceDefine);
				PMSDBStructure.PMSCenterDataContext.SaveChanges ();
			}
		}

		private void DBSourceDefineForm_Load (object sender, EventArgs e)
		{
			LoadDBSource ();
		}

		private void LoadDBSource ()
		{
			List<PMSRefDBConnectionObj> DBSourceDefines = null;
			if (null == _DBDefineManager) {
				DBSourceDefines = PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetRefDBConnectionObjListFromLocalFile ();
                
			} else {
				DBSourceDefines = _DBDefineManager.DBSourceConfigObjList;
			}
			if (DBSourceDefines == null)
				return;
			foreach (PMSRefDBConnectionObj source in DBSourceDefines) {
				InsertSource (source);
			}
		}

		public bool IsDBSourceNameExisted (string Name)
		{
			foreach (ListViewItem item in listView1.Items) {
				if (string.Compare (item.SubItems [0].Text, Name, true) == 0)
					return true;
			}
			return false;
		}

		private void toolStripButton_Del_Click (object sender, EventArgs e)
		{
			if (this.listView1.FocusedItem == null)
				return;
			try {
				string name = this.listView1.FocusedItem.Text;
				System.Resources.ResourceManager rm = new System.Resources.ResourceManager (typeof(DBSourceDefineControl));
				string strWarning = rm.GetString ("DeleteWarning");
				string strFormat = string.Format (strWarning, name);
				if (MessageBox.Show (strFormat, "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
					this.listView1.FocusedItem.Remove ();
					SetModified ();
				}
			} catch (System.Exception ex) {
				PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, ex.Message, true);
			}
		}

		private void listView1_ItemSelectionChanged (object sender, ListViewItemSelectionChangedEventArgs e)
		{
			//SetSelectObj(e.Item.Tag);
		}

		public bool Save ()
		{
			try {
				if (this.IsModified) {
					List<PMSRefDBConnectionObj> obl = new List<PMSRefDBConnectionObj> ();
					foreach (ListViewItem item in listView1.Items) {
						PMSRefDBConnection rc = (PMSRefDBConnection)(item.Tag);
						PMSRefDBConnectionObj ob = new PMSRefDBConnectionObj ();
						ob.StrName = item.SubItems [0].Text;
						ob.StrDescription = item.SubItems [1].Text;
						if (item.ForeColor == Color.Purple)
							ob.BDefault = true;
						ob.RefDBConnection = rc;
						obl.Add (ob);
					}
					if (null != _DBDefineManager) {
						_DBDefineManager.CloseAllDBConnects ();
						_DBDefineManager.DBSourceConfigObjList = obl;
					}
					PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.SaveRefDBConnectionObjList (obl);
					// MES开发器环境 中 应保存数据源定义文件至文件服务器
					//if (CurrentPrjInfo.CurrentEnvironment == MESEnvironment.MESDeveloper)
					//    RaiseSomeEvent(this, new SaveCompleteEventArgs(ProjectPathClass.RefDBSourcesFilePath));
					SetSaved ();
				}
			} catch (Exception ex) {
				PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error (PMS.Libraries.ToolControls.PMSPublicInfo.PublicFunctionClass.EnhancedStackTrace (ex));
				return false;
			}

			return true;
		}

		private void toolStripButton_SetDefault_Click (object sender, EventArgs e)
		{
			if (listView1.SelectedItems.Count == 0)
				return;
			foreach (ListViewItem item in listView1.Items) {
				item.ForeColor = Color.Black;
			}
			listView1.SelectedItems [0].ForeColor = Color.Purple;
			listView1.Invalidate ();
			SetModified ();
		}

		private void listView1_MouseDoubleClick (object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left) {
				if (listView1.SelectedItems.Count == 0)
					return;
				PMSRefDBConnection refdbc = listView1.SelectedItems [0].Tag as PMSRefDBConnection;
				using (DBRefStructForm dbrf = new DBRefStructForm ()) {
					dbrf.RefDBConnection = refdbc;
					if (dbrf.ShowDialog () == DialogResult.OK) {
						listView1.SelectedItems [0].Tag = dbrf.RefDBConnection;
						//SetSelectObj(listView1.SelectedItems[0].Tag);
						SetModified ();
					}
				}
			}
		}

		void listView1_AfterLabelEdit (object sender, System.Windows.Forms.LabelEditEventArgs e)
		{

			SetModified ();
		}

		private void SetModified ()
		{
			if (!IsModified)
				IsModified = true;
		}

		private void SetSaved ()
		{
			IsModified = false;
		}
	}
}
