using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMS.Libraries.ToolControls.PMSPublicInfo;

namespace PMS.Libraries.ToolControls.PMSPublicInfo.DBSourceDefine
{
    public partial class DSSelectControl : UserControl
    {

        public event OKEventHander OKEvent;
        public delegate void OKEventHander();

        public event CancelEventHander CancelEvent;
        public delegate void CancelEventHander();

        private string _SelectedRefName = null;
        public string SelectedRefName
        {
            get { return _SelectedRefName; }
        }

        public DSSelectControl()
        {
            InitializeComponent();
        }

        private void DSSelectControl_Load(object sender, EventArgs e)
        {
            LoadDBSource();
        }

        private void LoadDBSource()
        {
            List<PMSRefDBConnectionObj> DBSourceDefines = PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetRefDBConnectionObjListFromLocalFile();
            if (DBSourceDefines == null)
                return;

            foreach (PMSRefDBConnectionObj source in DBSourceDefines)
            {
                // 为拖放数据源时带入数据源名
                source.RefDBConnection.StrSourceName = source.StrName;
                InsertSource(source);
            }
        }

        public void InsertSource(PMSRefDBConnectionObj source)
        {
            string name = source.StrName;
            string description = source.StrDescription;
            PMSRefDBConnection rc = source.RefDBConnection;
            ListViewGroup group = null;

            switch (rc.RefDBType)
            {
                case RefDBType.MSAccess: group = AddGroup(RefDBType.MSAccess); break;
                case RefDBType.MSSqlServer: group = AddGroup(RefDBType.MSSqlServer); break;
                case RefDBType.Oracle: group = AddGroup(RefDBType.Oracle); break;
                case RefDBType.OleDB: group = AddGroup(RefDBType.OleDB); break;
                default:
                    break;
            }
            this.listView1.BeginUpdate();
            ListViewItem lvitem = new ListViewItem(name, group);
            lvitem.SubItems.Add(description);
            lvitem.Tag = rc;
            if (source.BDefault == true)
                lvitem.ForeColor = Color.Purple;
            this.listView1.Items.Add(lvitem);
            this.listView1.EndUpdate();
        }

        public ListViewGroup AddGroup(RefDBType refDBType)
        {
            string TypeName = null;
            switch (refDBType)
            {
                case RefDBType.MSAccess: TypeName = RefDBType.MSAccess.ToString(); break;
                case RefDBType.MSSqlServer: TypeName = RefDBType.MSSqlServer.ToString(); break;
                case RefDBType.Oracle: TypeName = RefDBType.Oracle.ToString(); break;
                case RefDBType.OleDB: TypeName = RefDBType.OleDB.ToString(); break;
                default:
                    break;
            }
            if (listView1.Groups[TypeName] == null)
            {
                ListViewGroup group = new ListViewGroup(TypeName, TypeName);
                listView1.Groups.Add(group);
                return group;
            }
            else
                return listView1.Groups[TypeName];
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 0)
            {
                return;
            }

            _SelectedRefName = this.listView1.SelectedItems[0].Text;
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 0)
            {
                return;
            }

            _SelectedRefName = this.listView1.SelectedItems[0].Text;
            if (null != OKEvent)
                OKEvent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (null != OKEvent)
                OKEvent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (null != CancelEvent)
                CancelEvent();
        }

    }
}
