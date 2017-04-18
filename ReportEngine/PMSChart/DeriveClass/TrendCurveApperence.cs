using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;

namespace PMS.Libraries.ToolControls.PMSChart
{
    public partial class TrendCurveApperence : Form
    {
        #region 私有属性
        private PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField _FatherSource = null;//所属父数据源
        private List<TreeNode> _DeleteNode = new List<TreeNode>();//过滤掉的数据字段
        private TrendLabel _LabelSource = null;
        private bool _Saved = false;//是否已经保存标志
        private bool _Load = false;
        #endregion

        #region 公有属性
        public TrendLabel LabelSource
        {
            get
            {
                return _LabelSource;
            }
            set
            {
                _LabelSource = value;
            }
        }
        public TrendCurve ChartParent;
        #endregion 

        #region 构造函数
        public TrendCurveApperence()
        {
            InitializeComponent();
        }
        public TrendCurveApperence(PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField father)
        {
            InitializeComponent();
            _FatherSource = father;
        }
        #endregion

        #region 控件事件
        private void TrendCurveApperence_Load(object sender, EventArgs e)
        {
            _Load = true;
            PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.FieldTreeViewData fvd = PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetCurrentReportDataDefine() as PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.FieldTreeViewData;
            if (null == fvd)
            {
                return;
            }
            if (null != _FatherSource)
            {
                PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.FieldTreeNodeData currentNode = fvd.FindNodeBySource(_FatherSource);
                if (null != currentNode)
                {
                    fvd.PopulateTreeAddRoot(this.DataSource, currentNode);
                }
            }
            else
            {
                fvd.PopulateTree(this.DataSource);
            }
            _DeleteNode.Clear();
            FilterField(this.DataSource);
            foreach (TreeNode node in _DeleteNode)
            {
                this.DataSource.Nodes.Remove(node);
            }
            this.DataSource.ExpandAll();
            if (_LabelSource != null)
            {
                if (_LabelSource.TrendLabelSource != null)
                {
                    SelectSourceField(_LabelSource.TrendLabelSource);
                }
                if (_LabelSource.FieldFlag != null)
                {
                    SelectFieldFlag(_LabelSource.FieldFlag);
                }
                SelectTextLineAlignment(_LabelSource.TextAlignment);
                SelectTextAlignment(_LabelSource.TextLineAlignment);
                comboBox1.Text = Enum.GetName(typeof(System.Windows.Forms.DataVisualization.Charting.TextOrientation), _LabelSource.TextOrientation);
                colorComBoBox1.SelectedItem = _LabelSource.LineColor;
                numericUpDown1.Value = _LabelSource.LineWidth;
            }
            _Load = false;
        }
        private void DataSource_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (DataField.Rows != null)
            {
                DataField.Rows.Clear();
            }
            if (e.Node != null)
            {
                if (e.Node.Tag != null && e.Node.Tag is SourceFieldDataTable)
                {
                    PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.FieldTreeViewData fvd = PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetCurrentReportDataDefine() as PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.FieldTreeViewData;
                    if (null == fvd)
                    {
                        return;
                    }
                    List<PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField> SubChild = (e.Node.Tag as PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField).GetSubSourceField(fvd);
                    if (SubChild != null)
                    {
                        foreach (PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField filed in SubChild)
                        {
                            int i=DataField.Rows.Add();
                            DataGridViewRow temp = DataField.Rows[i];
                            temp.Cells[0].Tag = filed;
                            temp.Cells[2].Value = filed.Name;
                        }
                    }
                    if (_LabelSource != null)
                    {
                        if(_LabelSource.TrendLabelSource!=null && _LabelSource.TrendLabelSource.Equals(e.Node.Tag))
                        {
                            SelectFieldFlag(_LabelSource.FieldFlag);
                        }
                    }
                }
            }
            Apply.Enabled = true;
            _Saved = false;
        }
        private void Apply_Click(object sender, EventArgs e)
        {
            int temp = SaveLabelInfo();
            if (temp < 0)
            {
                return;
            }
            if (ChartParent != null)
            {
                ChartParent.LabelSource = _LabelSource;
                ChartParent.InitailColumnData();
            }
            Apply.Enabled = false;
            _Saved = true;
        }

        private void Sure_Click(object sender, EventArgs e)
        {
            if (_Saved == true)
            {
            }
            else
            {
                int temp = SaveLabelInfo();
                if (temp < 0)
                {
                    return;
                }
                if (ChartParent != null)
                {
                    ChartParent.LabelSource = _LabelSource;
                    ChartParent.InitailColumnData();
                }
            }
            this.DialogResult = DialogResult.OK;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void DataField_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_Load)
            {
                return;
            }
            if (e.ColumnIndex == 3 && e.RowIndex>-1)
            {
                object obj = DataField.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
                {
                    object sourcetemp = DataField.Rows[e.RowIndex].Cells[0].Tag;
                    if (sourcetemp != null && sourcetemp is PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField)
                    {
                        if ((sourcetemp as PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField).FieldType != PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.MESVarType.MESDateTime)
                        {
                            MessageBox.Show(Properties.Resources.ResourceManager.GetString("message0010"));
                            DataField.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
                            return;
                        }
                    }
                    for (int i = 0; i < DataField.Rows.Count; i++)
                    {
                        if (i != e.RowIndex)
                        {
                            object objtemp = DataField.Rows[i].Cells[e.ColumnIndex].Value;
                            if (objtemp != null)
                            {
                                DataField.Rows[i].Cells[e.ColumnIndex].Value = null;
                            }
                        }
                    }
                }
            }
            Apply.Enabled = true;
            _Saved = false;
        }
        private void colorComBoBox1_SelectColorChanged(object sender, Color OldColor, Color NewColor)
        {
            Apply.Enabled = true;
            _Saved = false;
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Apply.Enabled = true;
            _Saved = false;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Apply.Enabled = true;
            _Saved = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Apply.Enabled = true;
            _Saved = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Apply.Enabled = true;
            _Saved = false;
        }
        #endregion

        #region 辅助私有函数
        /// <summary>
        /// 2012.06.26 增加
        /// 目的:对树进行过滤,过滤掉数据字段只保留数据集
        /// </summary>
        /// <param name="Aim">要过滤的树</param>
        private void FilterField(TreeView Aim)
        {
            if (Aim != null)
            {
                foreach (TreeNode node in Aim.Nodes)
                {
                    if (node !=null && node.Tag != null)
                    {
                        if (node.Tag is SourceFieldDataField)
                        {
                            _DeleteNode.Add(node);
                        }
                        else
                        {
                            if (node.Nodes != null)
                            {
                                FilterField(node.Nodes);
                            }
                        }
                    }
                }
            }
        }
        private void FilterField(TreeNodeCollection Aim)
        {
            if (Aim != null)
            {
                foreach (TreeNode node in Aim)
                {
                    if (node !=null && node.Tag != null)
                    {
                        if (node.Tag is SourceFieldDataField)
                        {
                            _DeleteNode.Add(node);
                        }
                        else
                        {
                            if (node.Nodes != null)
                            {
                                FilterField(node.Nodes);
                            }
                        }
                    }
                }
            }
        }
        private int SaveLabelInfo()
        {
            int result = 0;
            try
            {
                TreeNode node = DataSource.SelectedNode;
                _LabelSource = new TrendLabel();
                if (node.Tag != null && node.Tag is PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField)
                {
                    _LabelSource.TrendLabelSource = node.Tag as PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField;
                }
                else
                {
                    MessageBox.Show(Properties.Resources.ResourceManager.GetString("message0009"));
                    result = -1;
                    return result;
                }
                if (_LabelSource.FieldFlag == null)
                {
                    _LabelSource.FieldFlag = new List<FieldLabel>();
                }
                else
                {
                    _LabelSource.FieldFlag.Clear();
                }
                _LabelSource.LineColor = colorComBoBox1.SelectedItem;
                _LabelSource.LineWidth = System.Convert.ToInt32(numericUpDown1.Value);
                if (radioButton1.Checked)
                {
                    _LabelSource.TextLineAlignment = StringAlignment.Near;
                }
                else if (radioButton2.Checked)
                {
                    _LabelSource.TextLineAlignment = StringAlignment.Center;
                }
                else if (radioButton3.Checked)
                {
                    _LabelSource.TextLineAlignment = StringAlignment.Far;
                }
                if (radioButton4.Checked)
                {
                    _LabelSource.TextAlignment = StringAlignment.Near;
                }
                else if (radioButton5.Checked)
                {
                    _LabelSource.TextAlignment = StringAlignment.Center;
                }
                else if (radioButton6.Checked)
                {
                    _LabelSource.TextAlignment = StringAlignment.Far;
                }
                if (!string.IsNullOrEmpty(comboBox1.Text))
                {
                    _LabelSource.TextOrientation = (System.Windows.Forms.DataVisualization.Charting.TextOrientation)Enum.Parse(typeof(System.Windows.Forms.DataVisualization.Charting.TextOrientation), comboBox1.Text);
                }
                //必需有且只有一个基准字段
                int _FieldNeeded = 0;
                foreach (DataGridViewRow row in DataField.Rows)
                {
                    if (row.Cells[1].ValueType == typeof(bool))
                    {
                        if (Convert.ToBoolean(row.Cells[1].Value) == true)
                        {
                            FieldLabel temp = new FieldLabel();
                            temp.CurrentField = row.Cells[0].Tag as PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField;
                            temp.IsFlag = true;
                            if (row.Cells[3].Value != null)
                            {
                                temp.IsMainField = Convert.ToBoolean(row.Cells[3].Value);
                            }
                            else
                            {
                                temp.IsMainField = false;
                            }
                            if (row.Cells[4].Value != null)
                            {
                                temp.Format = row.Cells[4].Value.ToString();
                            }
                            _LabelSource.FieldFlag.Add(temp);
                        }
                         object obj = row.Cells[3].Value;
                         if (obj != null && Convert.ToBoolean(obj))
                         {
                             ++_FieldNeeded;
                         }
                    }
                }
                if (_FieldNeeded != 1)
                {
                    if (_FieldNeeded == 0)
                    {
                        MessageBox.Show(Properties.Resources.ResourceManager.GetString("message0011"));
                    }
                    else
                    {
                        MessageBox.Show(Properties.Resources.ResourceManager.GetString("message0012"));
                    }
                    result = -1;
                    return result;
                }
            }
            catch
            {
                result = -1;
            }
            return result;
        }
        /// <summary>
        /// 2012.06.29 增加
        /// 目的:根据给定的SourceField找到树中对应的
        /// </summary>
        private void SelectSourceField(PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField Aim)
        {
            if (this.DataSource.Nodes != null)
            {
                foreach (TreeNode node in this.DataSource.Nodes)
                {
                    if (Aim.Equals(node.Tag))
                    {
                        this.DataSource.SelectedNode = node;
                        break;
                    }
                    else
                    {
                        if (node.Nodes != null)
                        {
                            SelectSourceField(Aim, node.Nodes);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 2012.06.29 增加
        /// 目的:根据给定的SourceField找到树中对应的
        /// </summary>
        private void SelectSourceField(PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField Aim,TreeNodeCollection source)
        {
            if (Aim != null && source != null)
            {
                foreach (TreeNode node in source)
                {
                    if (Aim.Equals(node.Tag))
                    {
                        this.DataSource.SelectedNode = node;
                        break;
                    }
                    else
                    {
                        if (node.Nodes != null)
                        {
                            SelectSourceField(Aim, node.Nodes);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 2012.06.29 增加
        /// 目的:标记以前选定的内容
        /// </summary>
        /// <param name="Aim"></param>
        private void SelectFieldFlag(List<FieldLabel> Aim)
        {
            if (Aim != null)
            {
                foreach (FieldLabel label in Aim)
                {
                    if (label.IsFlag)
                    {
                        foreach (DataGridViewRow row in this.DataField.Rows)
                        {
                            if (label.CurrentField.Equals(row.Cells[0].Tag))
                            {
                                row.Cells[1].Value = label.IsFlag;
                                row.Cells[3].Value = label.IsMainField;
                                row.Cells[4].Value = label.Format;
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 2012.06.29 增加
        /// 目的:标记以前选定的内容
        /// </summary>
        /// <param name="Aim"></param>
        private void SelectTextAlignment(StringAlignment Aim)
        {
            if (Aim == StringAlignment.Near)
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
            }
            else if (Aim == StringAlignment.Center)
            {
                radioButton1.Checked = false;
                radioButton2.Checked = true;
                radioButton3.Checked = false;
            }
            else
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = true;
            }
        }
        /// <summary>
        /// 2012.07.09 增加
        /// 目的:标记以前选定的内容
        /// </summary>
        /// <param name="Aim"></param>
        private void SelectTextLineAlignment(StringAlignment Aim)
        {
            if (Aim == StringAlignment.Near)
            {
                radioButton4.Checked = true;
                radioButton5.Checked = false;
                radioButton6.Checked = false;
            }
            else if (Aim == StringAlignment.Center)
            {
                radioButton4.Checked = false;
                radioButton5.Checked = true;
                radioButton6.Checked = false;
            }
            else
            {
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                radioButton6.Checked = true;
            }
        }
        #endregion
    }
}
