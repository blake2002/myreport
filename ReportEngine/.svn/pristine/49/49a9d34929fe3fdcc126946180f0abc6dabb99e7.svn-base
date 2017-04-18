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
using System.Text.RegularExpressions;
namespace PMS.Libraries.ToolControls.Report.Controls.EditorDialog
{
    public partial class FieldBindDialog : Form
    {
        public SourceField SourceField = null;
        private bool _BindingSoureceField = true;//是否绑定SoureceField 2013.1.8增加
        public string Value
        {
            get;
            set;
        }
        public bool BindingSourceField
        {
            get
            {
                return _BindingSoureceField;
            }
            set
            {
                _BindingSoureceField = value;
            }
        }
        public FieldBindDialog(SourceField sf, string value)
        {
            InitializeComponent();
            SourceField = sf;
            Value = value;
            this.FieldTextTb.Text = value;

        }

        private void FieldBindDialogLoad(object sender, EventArgs e)
        {
            FieldTreeViewData fvd = CurrentPrjInfo.GetCurrentReportDataDefine() as FieldTreeViewData;
            if (null == fvd)
            {
                return;
            }
            if (null != SourceField)
            {
                FieldTreeNodeData currentNode = fvd.FindNodeBySource(SourceField);
                if (null != currentNode)
                {
                    fvd.PopulateTree(this.DataSourceTreeView, currentNode, true);
                }
            }
            else
            {
                fvd.PopulateTree(this.DataSourceTreeView, true, BindingSourceField);
            }
            AddParamVar();
            //LoadSysParams();
            SelectNode();
            //this.DataSourceTreeView.ExpandAll();
            if (!string.IsNullOrEmpty(this.FieldTextTb.Text))
            {
                this.FieldTextTb.Focus();
                this.FieldTextTb.Select(0, this.FieldTextTb.Text.Length);
            }
        }

        private void AddParamVar()
        {
            TreeNode node = new TreeNode("参数");
            DataSourceTreeView.Nodes.Add(node);
            TreeNode childNode = new TreeNode("PageIndex");
            childNode.Tag = "Param";
            node.Nodes.Add(childNode);
            childNode = new TreeNode("PageCount");
            childNode.Tag = "Param";
            node.Nodes.Add(childNode);
            childNode = new TreeNode("Year");
            childNode.Tag = "Param";
            node.Nodes.Add(childNode); ;
            childNode = new TreeNode("Month");
            childNode.Tag = "Param";
            node.Nodes.Add(childNode);
            childNode = new TreeNode("Day");
            childNode.Tag = "Param";
            node.Nodes.Add(childNode);
            childNode = new TreeNode("Hour");
            childNode.Tag = "Param";
            node.Nodes.Add(childNode);
            childNode = new TreeNode("Minute");
            childNode.Tag = "Param";
            node.Nodes.Add(childNode);
            childNode = new TreeNode("Second");
            childNode.Tag = "Param";
            node.Nodes.Add(childNode);
        }

        //private void LoadSysParams()
        //{
        //    this.DocumentParamCmb.Items.Add("PageIndex");
        //    this.DocumentParamCmb.Items.Add("PageCount");
        //    this.DocumentParamCmb.Items.Add("Year");
        //    this.DocumentParamCmb.Items.Add("Month");
        //    this.DocumentParamCmb.Items.Add("Hour");
        //    this.DocumentParamCmb.Items.Add("Day");
        //    this.DocumentParamCmb.Items.Add("Minute");
        //    this.DocumentParamCmb.Items.Add("Second");
        //    this.DocumentParamCmb.SelectedIndex = 0;
        //}

        private void DataSourceTreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeNode node = e.Item as TreeNode;
            if (node.Nodes.Count > 0)
            {
                return;
            }

            string fieldText = FindPahtByTreeNode(node);
            if (!string.IsNullOrEmpty(fieldText))
            {
                this.DataSourceTreeView.DoDragDrop(fieldText, DragDropEffects.Copy);
            }
        }

        private void TextTb_DragDrop(object sender, DragEventArgs e)
        {
            string fieldText = e.Data.GetData(DataFormats.StringFormat).ToString();
            InsertField(fieldText);
        }

        private void TextTb_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //private void InsertBtnClick(object sender, EventArgs e)
        //{
        //    if (null != this.DocumentParamCmb.SelectedItem)
        //    {
        //        string tempText = "[#" + this.DocumentParamCmb.SelectedItem.ToString() + "#]";
        //        InsertField(tempText);
        //    }
        //}

        private string FindSourceParentPath(TreeNode node)
        {
            if (null == node)
            {
                return string.Empty;
            }
            SourceField sf = node.Tag as SourceField;
            if (null == sf)
            {
                return string.Empty;
            }
            string path = sf.Name;
            if (null != node.Parent && node.Parent.Level != 0)
            {
                return FindSourceParentPath(node.Parent) + "/" + path;
            }
            return path;
        }

        private void InsertField(string fieldText)
        {
            int curosrPos = FieldTextTb.SelectionStart;
            if (this.FieldTextTb.Text.Length > 0)
            {
                string tempText = this.FieldTextTb.Text.Remove(curosrPos, FieldTextTb.SelectionLength);
                if (curosrPos != 0)
                {
                    tempText = tempText.Insert(curosrPos, fieldText);
                }
                else
                {
                    tempText += fieldText;
                }
                FieldTextTb.Text = tempText;
            }
            else
            {
                FieldTextTb.Text = fieldText;
            }
        }

        private void OKClick(object sender, EventArgs e)
        {
            Value = this.FieldTextTb.Text;
            if (null != this.DataSourceTreeView.SelectedNode)
            {
                //Label要求知道数据源
                SourceField = this.DataSourceTreeView.SelectedNode.Tag as SourceField;
            }
            this.DialogResult = DialogResult.OK;
        }

        private string FindPahtByTreeNode(TreeNode node)
        {
            if (null != node)
            {

                string fieldText = node.Text;

                SourceField parentSf = null;
                TreeNode nodeParent = node.Parent;
                if (null != nodeParent)
                {
                    parentSf = nodeParent.Tag as SourceField;
                }

                if (null == SourceField || (null != parentSf && !parentSf.ID.Equals(SourceField.ID)))
                {
                    if (null != node.Tag && (node.Tag is PMSVar))
                    {
                        fieldText = "[%" + fieldText + "%]";
                    }
                    else if (null != node.Tag && (node.Tag.ToString() == "Param"))
                    {
                        fieldText = "[#" + fieldText + "#]";
                    }
                    else
                    {
                        string path = FindSourceParentPath(node);
                        fieldText = "[" + path + "]";
                    }
                }
                else
                {
                    if (null != node.Tag && (node.Tag is PMSVar))
                    {
                        fieldText = "[%" + fieldText + "%]";
                    }
                    else if (null != node.Tag && (node.Tag.ToString() == "Param"))
                    {
                        fieldText = "[#" + fieldText + "#]";
                    }
                    else
                    {
                        fieldText = "[" + fieldText + "]";
                    }
                }

                return fieldText;
            }
            return string.Empty;
        }

        private void DataSourceTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            if (node.Nodes.Count > 0)
            {
                return;
            }
            string fieldText = FindPahtByTreeNode(node);
            if (!string.IsNullOrEmpty(fieldText))
            {
                InsertField(fieldText);
            }
        }

        private void CancleBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void SelectNode()
        {
            if (DataSourceTreeView.Nodes.Count > 0)
            {
                //if (null != SourceField)
                //{
                //for (int i = 0; i < DataSourceTreeView.Nodes.Count;i++ )
                // {
                //TreeNode node = this.DataSourceTreeView.Nodes[i];
                //if (node.Text != SourceField.Name)
                //{
                //    continue;
                //}
                //DataSourceTreeView.SelectedNode = node;
                //if (node.Nodes.Count == 0)
                //{
                //    continue;
                //}
                //node.Expand();
                string regexExpression = @"\[#?%?(p{0,}/?\w){1,}%?#?\]";//@"\[#?%?(([\u4E00-\u9FFF]){0,}/?\w){1,}%?#?\]";
                Regex regex = new Regex(regexExpression);
                MatchCollection mc = regex.Matches(this.FieldTextTb.Text, 0);
                Match[] matches = new Match[mc.Count];
                mc.CopyTo(matches, 0);
                //foreach (Match mt in matches)
                //{
                //    foreach (TreeNode tempNode in node.Nodes)
                //    { 
                //        if(("["+tempNode.Text+"]").Equals(mt.Value))
                //        {
                //            this.DataSourceTreeView.SelectedNode = tempNode;
                //            if (tempNode.Nodes.Count > 0)
                //            {
                //                tempNode.Expand();
                //            }
                //        }
                //    }
                //}
                if (matches.Length > 0)
                {
                    string path = matches[0].Value;
                    path = path.Substring(1);
                    path = path.Substring(0, path.Length - 1);
                    SelectNode(path);
                }
                else
                {
                    this.DataSourceTreeView.ExpandAll();
                    this.DataSourceTreeView.SelectedNode = this.DataSourceTreeView.Nodes[0];
                    this.DataSourceTreeView.SelectedNode.EnsureVisible();
                }
                //}
                //}
            }

        }

        private void SelectNode(string strPath)
        {
            if (this.DataSourceTreeView.Nodes.Count == 0)
            {
                return;
            }
            int index = strPath.IndexOf("/");
            string str = strPath;
            TreeNode tn = null;
            while (!string.IsNullOrEmpty(str))
            {
                if (index > 0)
                {
                    str = strPath.Substring(0, index);
                    strPath = strPath.Substring(index + 1);
                }
                else
                {
                    strPath = string.Empty;
                }
                TreeNodeCollection tc = null;
                if (tn == null)
                {
                    tc = DataSourceTreeView.Nodes[0].Nodes;
                }
                else
                {
                    tc = tn.Nodes;
                }
                foreach (TreeNode tmp in tc)
                {
                    if (tmp.Text == str)
                    {
                        tn = tmp;
                        tn.Expand();
                        break;
                    }
                }
                str = strPath;
                index = strPath.IndexOf("/");
            }
            if (null != tn)
            {
                this.DataSourceTreeView.SelectedNode = tn;
                this.DataSourceTreeView.SelectedNode.EnsureVisible();
            }
        }
    }
}
