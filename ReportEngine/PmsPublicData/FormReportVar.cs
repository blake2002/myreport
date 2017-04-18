using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PMS.Libraries.ToolControls.PmsSheet.PmsPublicData
{
    public partial class FormReportVar : Form
    {
        public FormReportVar()
        {
            InitializeComponent();
            InitalActionMode();
            _OperateType = ReportVarOperate.Create;
        }
        private List<PMSVar> _PMSVarList;
        private int _SelectRowIndex = -1;
        private bool _DataGridViewChanged = false;
        [DllImport("user32.dll", EntryPoint = "GetKeyboardState")]//获取user32.dll函数
        public static extern int GetKeyboardState(byte[] pbKeyState);//获取虚拟按键状态函数
        public List<PMSVar> PMSVarList
        {
            get
            {
                if (_PMSVarList == null || _DataGridViewChanged==true)
                    _PMSVarList = new List<PMSVar>();

                _PMSVarList.Clear();
                foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                {
                    PMSVar pv = new PMSVar();
                    try
                    {
                        if (dgvr.Cells[1].Value != null)
                        {
                            switch (dgvr.Cells[1].Value.ToString())
                            {
                                case "字符串":
                                    pv.VarType = MESVarType.MESString;
                                    break;
                                case "整型":
                                    pv.VarType = MESVarType.MESInt;
                                    break;
                                case "时间日期":
                                    pv.VarType = MESVarType.MESDateTime;
                                    break;
                                case "浮点型":
                                    pv.VarType = MESVarType.MESReal;
                                    break;
                                case "布尔":
                                    pv.VarType = MESVarType.MESBool;
                                    break;
                                case "货币":
                                    pv.VarType = MESVarType.MESDecimal;
                                    break;
                                default:
                                    pv.VarType = MESVarType.MESNodefined;
                                    break;
                            }
                            if (dgvr.Cells[0].Value != null)
                            {
                                if (string.IsNullOrEmpty(dgvr.Cells[0].Value.ToString()) == false)
                                {
                                    pv.VarName = dgvr.Cells[0].Value.ToString();
                                    if (dgvr.Cells[2].Value != null)
                                    {
                                        pv.VarValue = dgvr.Cells[2].Value;
                                    }
                                    if (dgvr.Cells[3].Value != null)
                                    {
                                        pv.VarDesc = dgvr.Cells[3].Value.ToString();
                                    }
                                    _PMSVarList.Add(pv);
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                return _PMSVarList; 
            }
            set
            {
                _PMSVarList = value;

                if (_PMSVarList == null)
                    return;
                dataGridView1.Rows.Clear();
                if(_PMSVarList.Count>0)
                    dataGridView1.Rows.Add(_PMSVarList.Count);
                int index = 0;
                string type  = "";
                foreach (PMSVar pv in _PMSVarList)
                {
                    switch (pv.VarType)
                    {
                        case MESVarType.MESString:
                            type = "字符串";
                            break;
                        case MESVarType.MESInt:
                            type = "整型";
                            break;
                        case MESVarType.MESDateTime:
                            type = "时间日期";
                            break;
                        case MESVarType.MESReal:
                            type = "浮点型";
                            break;
                        case MESVarType.MESBool:
                            type = "布尔";
                            break;
                        case MESVarType.MESDecimal:
                            type = "货币";
                            break;
                        default:
                            type = "";
                            break;
                    }
                    if (string.IsNullOrEmpty(pv.VarName) == false)
                    {
                        dataGridView1.Rows[index].Cells[0].Value = pv.VarName;
                        dataGridView1.Rows[index].Cells[1].Value = type;
                        dataGridView1.Rows[index].Cells[2].Value = pv.VarValue;
                        dataGridView1.Rows[index].Cells[3].Value = pv.VarDesc;
                        index++;
                    }
                }
            }
        }

        private ReportVarOperate _OperateType;

        public ReportVarOperate OperateType
        {
            get { return _OperateType; }
            set { _OperateType = value; }
        }

        private void AddRow(PMSVar pv)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FormReportVar_Load(object sender, EventArgs e)
        {
            if (_OperateType == ReportVarOperate.Initial)
            {
                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[2].ReadOnly = false;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = true;
                this.Text = "过滤条件";
            }
        }

        #region 2011.12.26 增加
        private void InitalActionMode()
        {
            DeleteVar.Text = Properties.Resources.ResourceManager.GetString("context0002");
        }
        private void DeleteVar_Click(object sender, EventArgs e)
        {
            if (_SelectRowIndex>-1 && dataGridView1.Rows.Count >= _SelectRowIndex && dataGridView1.Rows[_SelectRowIndex].IsNewRow == false)
            {
                //DataGridViewRowStateChangedEventArgs temp = new DataGridViewRowStateChangedEventArgs(dataGridView1.Rows[_SelectRowIndex], DataGridViewElementStates.Displayed);
                //dataGridView1_RowStateChanged(dataGridView1, temp);
                dataGridView1.Rows.RemoveAt(_SelectRowIndex);
                _SelectRowIndex = -1;
            }
        }
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex>-1 && dataGridView1.Rows.Count >= e.RowIndex && dataGridView1.Rows[e.RowIndex].IsNewRow == false)
                {
                    int totalheight = 0;
                    totalheight += dataGridView1.ColumnHeadersHeight;
                    for (int i = 0; i < e.RowIndex; i++)
                    {
                        totalheight += dataGridView1.Rows[i].Height;
                    }
                    contextMenuStrip1.Show(dataGridView1,new Point(e.X,e.Y+totalheight));
                    _SelectRowIndex = e.RowIndex;
                }
            }

        }
        #endregion
        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            _DataGridViewChanged = true;
        }

        #region 2012.12.18 处理报表变量的复制与拷贝 这里只考虑用户变量不考虑系统变量
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys)Shortcut.CtrlV)  // 快捷键 Ctrl+V 粘贴操作    
            {
                if (dataGridView1.Focus())
                {
                    DealWithVarPaste();
                    return true;
                }
            }
            else if (keyData == (Keys)Shortcut.CtrlC) //快捷键 Ctrl+C 复制操作
            {
                if (dataGridView1.Focus())
                {
                    DealWithVarCopy();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        /// <summary>
        /// 2012.12.18 董平增加
        /// 目的:处理变量粘贴操作
        /// </summary>
        private void DealWithVarPaste()
        {
            IDataObject temp = Clipboard.GetDataObject();
            object result = temp.GetData(DataFormats.Serializable);
            if (result != null && result is List<PMSVar>)
            {
                List<PMSVar> vartemp = result as List<PMSVar>;
                if (dataGridView1.SelectedRows.Count <= 1)
                {
                    int m = 0;
                    if (dataGridView1.SelectedRows.Count == 1)
                    {
                        m = dataGridView1.SelectedRows[0].Index;
                    }
                    for (int i = 0; i < vartemp.Count; i++)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridView1);
                        row.Cells[0].Value = vartemp[i].VarName;
                        string type = string.Empty;
                        switch (vartemp[i].VarType)
                        {
                            case MESVarType.MESString:
                                type = "字符串";
                                break;
                            case MESVarType.MESInt:
                                type = "整型";
                                break;
                            case MESVarType.MESDateTime:
                                type = "时间日期";
                                break;
                            case MESVarType.MESReal:
                                type = "浮点型";
                                break;
                            case MESVarType.MESBool:
                                type = "布尔";
                                break;
                            case MESVarType.MESDecimal:
                                type = "货币";
                                break;
                            default:
                                type = "";
                                break;
                        }
                        row.Cells[1].Value = type;
                        row.Cells[2].Value = vartemp[i].VarValue;
                        row.Cells[3].Value = vartemp[i].VarDesc;
                        dataGridView1.Rows.Insert(m + i, row);
                    }
                }
                Clipboard.Clear();
            }
        }
        /// <summary>
        /// 2012.12.18 董平增加
        /// 目的:处理变量复制操作
        /// </summary>
        private void DealWithVarCopy()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                List<PMSVar> tempvars = new List<PMSVar>();

                for (int i = dataGridView1.SelectedRows.Count-1; i >=0; i--)
                {
                    DataGridViewRow tempRow = dataGridView1.SelectedRows[i];
                    if (!tempRow.IsNewRow)
                    {
                        if (tempRow.Cells[0].Value == null && tempRow.Cells[0].Value.ToString() == "")
                        {
                            tempRow.Cells[0].Selected = true;
                            MessageBox.Show("变量名称不能为空!");
                            return;
                        }

                        if (tempRow.Cells[1].Value == null && tempRow.Cells[1].Value.ToString() == "")
                        {
                            tempRow.Cells[1].Selected = true;
                            MessageBox.Show("数据类型不能为空!");
                            return;
                        }

                        PMSVar var1 = new PMSVar();
                        switch (tempRow.Cells[1].Value.ToString())
                        {
                            case "字符串":
                                var1.VarType = MESVarType.MESString;
                                break;
                            case "整型":
                                var1.VarType = MESVarType.MESInt;
                                break;
                            case "时间日期":
                                var1.VarType = MESVarType.MESDateTime;
                                break;
                            case "浮点型":
                                var1.VarType = MESVarType.MESReal;
                                break;
                            case "布尔":
                                var1.VarType = MESVarType.MESBool;
                                break;
                            case "货币":
                                var1.VarType = MESVarType.MESDecimal;
                                break;
                            default:
                                var1.VarType = MESVarType.MESNodefined;
                                break;
                        }
                        var1.VarName = tempRow.Cells[0].Value.ToString();
                        if (tempRow.Cells[2].Value != null)
                        {
                            var1.VarValue = tempRow.Cells[2].Value;
                        }
                        if (tempRow.Cells[3].Value != null)
                        {
                            var1.VarDesc = tempRow.Cells[3].Value.ToString();
                        }

                        tempvars.Add(var1);
                    }
                }
                Clipboard.SetData(DataFormats.Serializable, tempvars);
            }
        }
        #endregion

        private void dataGridView1_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if(dataGridView1.Rows[e.RowIndex].IsNewRow)
                return;
            if (e.ColumnIndex == 0)
            {
                string valueStr = e.FormattedValue.ToString();
                if (PMS.Libraries.ToolControls.PMSPublicInfo.PublicFunctionClass.IsKeyword(valueStr))
                {
                    string strError = string.Format("变量名[{0}]为系统保留关键字，请使用其他变量名!",valueStr);
                    MessageBox.Show(strError, "变量名冲突");
                    e.Cancel = true;
                }
                else if (!PMS.Libraries.ToolControls.PMSPublicInfo.PublicFunctionClass.IsVarNameValidate(valueStr))
                {
                    string strError = string.Format("变量名[{0}]非法，请重新输入变量名!", valueStr);
                    MessageBox.Show(strError, "变量名非法");
                    e.Cancel = true;
                }
                else
                {
                    e.Cancel = false;
                }
            }
        }

        private void dataGridView1_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].IsNewRow)
                return;
            if (null == dataGridView1.Rows[e.RowIndex].Cells[1].Value)
            {
                MessageBox.Show("变量类型不可为空!");
                e.Cancel = true;
            }
            else
            {
                string valueStr = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                if (string.IsNullOrEmpty(valueStr))
                {
                    MessageBox.Show("变量类型不可为空!");
                    e.Cancel = true;
                }
            }
        }
    }

    public enum ReportVarOperate
    {
        Create,
        Initial
    }
}
