using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMS.Libraries.ToolControls.PMSPublicInfo;
using System.IO;

namespace PMS.Libraries.ToolControls.PMSPublicInfo
{
    public partial class SelectedForm : Form
    {
        private int _doctype = 0;
        public SelectedForm(DocType doctype)
        {
            InitializeComponent();
            _doctype = PMS.Libraries.ToolControls.PMSPublicInfo.PublicFunctionClass.GetByteTypeFromDocType(doctype);
        }

        public bool IsNewButtonVisible
        {
            get { return this.buttonNew.Visible; }
            set { this.buttonNew.Visible = value;}
        }

        private TreeNode SelectedNode
        {
            get;
            set;
        }

        private Guid SelectedID
        {
            get; 
            set;
        }

        private MESCustomViewIdentity _Identity = null;
        public MESCustomViewIdentity Identity
        {
            get { return _Identity; }
            set
            {
                _Identity = value;
            }
        }

        private void SelectedForm_Load(object sender, EventArgs e)
        {
            switch (_doctype)
            {
                case byte.MaxValue:
                    this.Text = GetStringFromPublicResourceClass.GetStringFromPublicResource("MESPleaseSelectView");
                    break;
                case 1:
                    this.Text = GetStringFromPublicResourceClass.GetStringFromPublicResource("MESPleaseSelectReport");
                    break;
                case 2:
                    this.Text = GetStringFromPublicResourceClass.GetStringFromPublicResource("MESPleaseSelectForm");
                    break;
            }

            CurrentPrjInfo.GetFormTreeView(_doctype, ref this.treeView1);
            if (null != Identity)
            {

                TreeNode node = FindNodeOnPath(this.treeView1.Nodes,Identity.FullPath);
                if(null != node)
                {
                    this.treeView1.SelectedNode = node;
                    node.EnsureVisible();
                }
            }
        }

        private TreeNode FindNodeOnPath(TreeNodeCollection tn, string nodepath)
        {
            string str1 = null;
            string str2 = null;
            if (tn.Count < 1)
                return null;
            TreeNode TempTn = null;
            int pos = nodepath.IndexOf('\\', 0);
            if (pos > 0)
            {
                //当前要找的结点  
                str1 = nodepath.Substring(0, pos);
                //下一级目录的结点  
                str2 = nodepath.Substring(pos + 1);
            }
            else
                str1 = nodepath;
            //从当前一级树目录中查找结点  
            for (int i = 0; i < tn.Count; i++)
            {
                //找到了  
                if (tn[i].Text == str1)
                {
                    TempTn = tn[i];
                    //是要找树结点的最终位置  
                    if (str2 == null)
                        break;
                    else
                    {
                        //继续往下查找  
                        TreeNode tnd = FindNodeOnPath(tn[i].Nodes, str2);
                        if (tnd != null)
                            TempTn = tnd;
                    }
                    break;
                }
            }
            //没找到返回空 
            if (TempTn != null)
            {
                if (TempTn.Parent != null)
                    TempTn.Parent.Expand();
            }
            return TempTn;
        }  

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MESCustomViewIdentity idobj = GetSelectedIdentity();
                if (null != idobj)
                {
                    _Identity = idobj;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                //if (this.treeView1.SelectedNode == null)
                //    return;
                //// 版本节点
                //if (this.treeView1.SelectedNode.ImageIndex == 4 || this.treeView1.SelectedNode.ImageIndex == 5)
                //{
                //    SelectedNode = this.treeView1.SelectedNode;
                //    if (null != (this.treeView1.SelectedNode.Tag as s_CfgFileInfo))
                //        SelectedID = (this.treeView1.SelectedNode.Tag as s_CfgFileInfo).FID;
                //    _Identity = new MESCustomViewIdentity();
                //    _Identity.IsSpecifiedVersion = true;
                //    _Identity.ViewID = SelectedID;
                //    _Identity.ViewName = SelectedNode.Text;
                //    _Identity.FullPath = SelectedNode.FullPath;
                //    this.DialogResult = DialogResult.OK;
                //    this.Close();
                //}
                //// 视图节点
                //else if (this.treeView1.SelectedNode.ImageIndex == 0)
                //{
                //    SelectedNode = this.treeView1.SelectedNode;
                //    _Identity = new MESCustomViewIdentity();
                //    _Identity.IsSpecifiedVersion = false;
                //    _Identity.ViewName = SelectedNode.Text;
                //    _Identity.FullPath = SelectedNode.FullPath;
                //    if (null != SelectedNode.Parent)
                //    {
                //        s_CfgFInfoMap mapinfo = SelectedNode.Parent.Tag as s_CfgFInfoMap;
                //        if (null != mapinfo)
                //            _Identity.ParentID = mapinfo.MAPID;
                //    }
                //    this.DialogResult = DialogResult.OK;
                //    this.Close();
                //}
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            MESCustomViewIdentity idobj = GetSelectedIdentity();
            if (null != idobj)
            {
                _Identity = idobj;
                this.DialogResult = DialogResult.OK;
            }
            //if (this.treeView1.SelectedNode == null)
            //    return;
            //// 版本节点
            //if (this.treeView1.SelectedNode.ImageIndex == 4 || this.treeView1.SelectedNode.ImageIndex == 5)
            //{
            //    SelectedNode = this.treeView1.SelectedNode;
            //    if (null != (this.treeView1.SelectedNode.Tag as s_CfgFileInfo))
            //        SelectedID = (this.treeView1.SelectedNode.Tag as s_CfgFileInfo).FID;
            //    _Identity = new MESCustomViewIdentity();
            //    _Identity.IsSpecifiedVersion = true;
            //    _Identity.ViewID = SelectedID;
            //    _Identity.ViewName = SelectedNode.Text;
            //    _Identity.FullPath = SelectedNode.FullPath;
            //    this.DialogResult = DialogResult.OK;
            //}
            //// 视图节点
            //else if (this.treeView1.SelectedNode.ImageIndex == 0)
            //{
            //    SelectedNode = this.treeView1.SelectedNode;
            //    _Identity = new MESCustomViewIdentity();
            //    _Identity.IsSpecifiedVersion = false;
            //    _Identity.ViewName = SelectedNode.Text;
            //    _Identity.FullPath = SelectedNode.FullPath;
            //    if (null != SelectedNode.Parent)
            //    {
            //        s_CfgFInfoMap mapinfo = SelectedNode.Parent.Tag as s_CfgFInfoMap;
            //        if (null != mapinfo)
            //            _Identity.ParentID = mapinfo.MAPID;
            //    }
            //    this.DialogResult = DialogResult.OK;
            //}
        }

        private MESCustomViewIdentity GetSelectedIdentity()
        {
            if (this.treeView1.SelectedNode == null)
                return null;
            // 版本节点
            if (this.treeView1.SelectedNode.ImageIndex == 4 || this.treeView1.SelectedNode.ImageIndex == 5)
            {
                SelectedNode = this.treeView1.SelectedNode;
                if (null != (this.treeView1.SelectedNode.Tag as s_CfgFileInfo))
                    SelectedID = (this.treeView1.SelectedNode.Tag as s_CfgFileInfo).FID;
                MESCustomViewIdentity Id = new MESCustomViewIdentity();
                Id.IsSpecifiedVersion = true;
                Id.ViewID = SelectedID;
                Id.ViewName = SelectedNode.Text;
                Id.FullPath = SelectedNode.FullPath;
                return Id;
            }
            // 视图节点
            else if (this.treeView1.SelectedNode.ImageIndex == 0)
            {
                SelectedNode = this.treeView1.SelectedNode;
                MESCustomViewIdentity Id = new MESCustomViewIdentity();
                Id.IsSpecifiedVersion = false;
                Id.ViewName = SelectedNode.Text;
                Id.FullPath = SelectedNode.FullPath;
                if (null != SelectedNode.Parent)
                {
                    s_CfgFInfoMap mapinfo = SelectedNode.Parent.Tag as s_CfgFInfoMap;
                    if (null != mapinfo)
                        Id.ParentID = mapinfo.MAPID;
                }
                return Id;
            }
            return null;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode == null)
                return;
            if (this.treeView1.SelectedNode.ImageIndex == 2 || this.treeView1.SelectedNode.ImageIndex == 3)
            {
                int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_NEWFORM;
                byte[] theBytes = PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.ObjToByte(this.treeView1.SelectedNode.Tag);
                if (PMS.Libraries.ToolControls.PMSPublicInfo.Message.UserViewNavigationBarFormHandle != IntPtr.Zero)
                    PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendCopyData(PMS.Libraries.ToolControls.PMSPublicInfo.Message.UserViewNavigationBarFormHandle, msgID, theBytes);
                if(null != CurrentPrjInfo.CurrentNewFormCfgFileInfo as s_CfgFileInfo)
                {
                    SelectedNode = this.treeView1.SelectedNode;
                    s_CfgFileInfo info = CurrentPrjInfo.CurrentNewFormCfgFileInfo as s_CfgFileInfo;
                    
                    _Identity = new MESCustomViewIdentity();
                    _Identity.IsSpecifiedVersion = false;
                    _Identity.ViewName = info.Name;
                    _Identity.FullPath = Path.Combine(SelectedNode.FullPath, info.Name);
                    if (null != SelectedNode)
                    {
                        s_CfgFInfoMap mapinfo = SelectedNode.Tag as s_CfgFInfoMap;
                        if (null != mapinfo)
                            _Identity.ParentID = mapinfo.MAPID;
                    }
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            MESCustomViewIdentity id = GetSelectedIdentity();
            if(null != id)
            {
                s_CfgFileInfo info = CurrentPrjInfo.GetCfgFileInfo(id);
                if(null != info)
                {
                    int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_OPENDOC;
                    byte[] theBytes = PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.ObjToByte(info);
                    if (PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle != IntPtr.Zero)
                        PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendCopyData(PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle, msgID, theBytes);
                }
            }
        }
    }
}
