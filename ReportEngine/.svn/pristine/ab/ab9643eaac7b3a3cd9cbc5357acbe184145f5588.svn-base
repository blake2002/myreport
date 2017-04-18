using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using PMS.Libraries.ToolControls.PMSPublicInfo;
using PMS.Libraries.ToolControls.PMSReport;

namespace PMS.Libraries.ToolControls.Report.Controls.EditorDialog
{
    public partial class SourceBindDialog : Form
    {
        //所有数据源
        private SourceField _allField = null;
        private bool _isFilterField = false;

        private ImageList _imageList = null;

        /// <summary>
        /// 当前绑定的数据源
        /// </summary>
        public SourceField SourceField
        {
            get;
            set;
        }

        public SourceBindDialog(SourceField allField,SourceField currentField):this(allField,currentField,false)
        {
            
        }
        public SourceBindDialog(SourceField allField, SourceField currentField, bool isFilterField)
        {
            InitializeComponent();
            SourceField = currentField;
            _allField = allField;
            _isFilterField = isFilterField;
        }


        private void SourceBindDialogLoad(object sender, EventArgs e)
        {
            if (null != SourceField)
            {
                cb_CustomMode.Checked = SourceField.CustomMode;
                tb_TablePath.Text = SourceField.CustomTablePath;
            }
            RefreshTree();
            
            //FieldTreeViewData fvd = CurrentPrjInfo.GetCurrentReportDataDefine() as FieldTreeViewData;
            //if (null == fvd)
            //{
            //    return;
            //}
            //if (null != _allField)
            //{
            //    FieldTreeNodeData currentNode = fvd.FindNodeBySource(_allField);
            //    if (null != currentNode)
            //    {
            //        fvd.PopulateTreeAddRoot(this.DataSourceTreeView, currentNode);
            //    }
            //}
            //else
            //{
            //    fvd.PopulateTree(this.DataSourceTreeView);
            //}
            //FilterTreeView();

            //bool isSelect = false;
            //foreach (TreeNode node in this.DataSourceTreeView.Nodes)
            //{
            //    isSelect |= SelectNode(SourceField, node);
            //}
            //if (!isSelect)
            //{
            //    if (this.DataSourceTreeView.Nodes.Count > 0)
            //    {
            //        this.DataSourceTreeView.ExpandAll();
            //    }
            //}
            //if (null != this.DataSourceTreeView.SelectedNode)
            //    this.DataSourceTreeView.SelectedNode.EnsureVisible();
        }

        private void OkBtnClick(object sender, EventArgs e)
        {
            TreeNode node = this.DataSourceTreeView.SelectedNode;

            if (0 == node.Level)
            {
                SourceField = null;
                this.DialogResult = DialogResult.OK;
                return;
            }

            if (null != node &&
                ((null != _allField && node.Level > 1) || null == _allField))
            {
                SourceField  tmp = node.Tag as SourceField;
                if (tmp is SourceFieldDataTable)
                {
                    if(cb_CustomMode.Checked)
                    {
                        tmp.CustomMode = true;
                        tmp.CustomTablePath = tb_TablePath.Text;
                    }
                    else
                    {
                        tmp.CustomMode = false;
                        tmp.CustomTablePath = tb_TablePath.Text;
                    }
                    SourceField = tmp;
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("无效选择，只能选择子数据表");
                }
            }
            else
            {
                MessageBox.Show("无效选择，只能选择子数据表");
            }
        }

        private void CancleBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private bool SelectNode(SourceField sf,TreeNode node)
        {
            if (null != sf && null != node)
            {
                if (node.Text == sf.Name)
                {
                    this.DataSourceTreeView.SelectedNode = node;
                    node.Expand();
                    return true;
                }
                foreach (TreeNode temp in node.Nodes)
                {
                    if (temp.Text != sf.Name)
                    {
                        if (SelectNode(sf, temp))
                        {
                            return true;
                        }
                    }
                    else
                    {
                        this.DataSourceTreeView.SelectedNode = temp;
                        temp.Expand();
                        return true;
                    }
                }
            }
            else
            {
                this.DataSourceTreeView.SelectedNode = this.DataSourceTreeView.Nodes[0];
            }
            return false;
        }

        /// <summary>
        ///剔除字段 
        /// </summary>
        private void FilterTreeView()
        {
            if (!_isFilterField)
            {
                return;
            }

            Queue<TreeNode> queue = new Queue<TreeNode>();
            if (DataSourceTreeView.Nodes.Count > 0)
            {
                foreach (TreeNode tn in DataSourceTreeView.Nodes[0].Nodes)
                {
                    queue.Enqueue(tn);
                }
            }

            while (queue.Count > 0)
            {
                TreeNode tn = queue.Dequeue();
                if (null != tn.Tag)
                {
                    if (tn.Tag is SourceFieldDataField)
                    {
                        if (null != tn.Parent)
                        {
                            tn.Remove();
                        }
                    }
                    else if(tn.Tag is SourceFieldDataTable && tn.Nodes.Count>0)
                    {
                        foreach (TreeNode tmpTn in tn.Nodes)
                        {
                            queue.Enqueue(tmpTn);
                        }
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            RefreshTree();
        }

        private void RefreshTree()
        {
            FieldTreeViewData fvd = CurrentPrjInfo.GetCurrentReportDataDefine() as FieldTreeViewData;
            if (null == fvd)
            {
                return;
            }
            if (cb_CustomMode.Checked)
            {
                this.tb_TablePath.Enabled = true;
                fvd.PopulateTree(this.DataSourceTreeView);
            }
            else
            {
                this.tb_TablePath.Enabled = false;
                if (null != _allField)
                {
                    FieldTreeNodeData currentNode = fvd.FindNodeBySource(_allField);
                    if (null != currentNode)
                    {
                        fvd.PopulateTreeAddRoot(this.DataSourceTreeView, currentNode);
                    }
                }
                else
                {
                    fvd.PopulateTree(this.DataSourceTreeView);
                }
            }
            FilterTreeView();
            bool isSelect = false;
            foreach (TreeNode node in this.DataSourceTreeView.Nodes)
            {
                isSelect |= SelectNode(SourceField, node);
            }
            if (!isSelect)
            {
                if (this.DataSourceTreeView.Nodes.Count > 0)
                {
                    this.DataSourceTreeView.ExpandAll();
                }
            }
            if (null != this.DataSourceTreeView.SelectedNode)
                this.DataSourceTreeView.SelectedNode.EnsureVisible();
        }

        private void DataSourceTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (cb_CustomMode.Checked)
            {
                TreeNode node = this.DataSourceTreeView.SelectedNode;

                if (0 == node.Level)
                {
                    return;
                }

                this.tb_TablePath.Text = node.FullPath.TrimStart("数据集\\".ToCharArray()).Replace('\\', '.');
            }
        }
       
    }
}
