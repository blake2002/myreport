using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PMS.Libraries.ToolControls.PmsSheet.PmsPublicData
{
    public partial class MultiDataSourceConfigForm : Form
    {

        public List<DSSqlPair> MultiDataSource
        {
            get;
            set;
        }

        public MultiDataSourceConfigForm()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
        }

        private void MultiDataSourceConfigForm_Load(object sender, EventArgs e)
        {
            if (null == MultiDataSource)
                MultiDataSource = new List<DSSqlPair>();
            foreach (DSSqlPair dsp in MultiDataSource)
            {
                this.dataGridView1.Rows.Add(dsp.DataSource, dsp.Sql);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            MultiDataSource.Clear();
            foreach (DataGridViewRow r in this.dataGridView1.Rows)
            {
                string ds = r.Cells[0].Value.ToString();
                if (string.IsNullOrEmpty(ds))
                {
                    MessageBox.Show("数据源不可为空");
                    return;
                }
                string sql = r.Cells[1].Value.ToString();
                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Sql语句不可为空");
                    return;
                }
                DSSqlPair dsp = new DSSqlPair(ds, sql);
                MultiDataSource.Add(dsp);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void toolStripButton_Add_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Add(string.Empty,string.Empty);
        }

        private void toolStripButton_Del_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count >0)
                this.dataGridView1.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
        }

        

    }

    public class DataGridViewDSColumn : DataGridViewColumn
    {
        private object m_dataSoruce = null;

        public DataGridViewDSColumn()
            : base(new DataGridViewDSCell())
        {
        }
        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if (value != null && !value.GetType().IsAssignableFrom(typeof(DataGridViewDSCell)))
                {
                    throw new InvalidCastException("不是DataGridViewDataWindowCell");
                }
                base.CellTemplate = value;
            }
        }
        private DataGridViewDSCell ComboBoxCellTemplate
        {
            get
            {
                return (DataGridViewDSCell)this.CellTemplate;
            }
        }
        public Object DataSource
        {
            get
            {
                return m_dataSoruce;
            }
            set
            {
                if (ComboBoxCellTemplate != value)
                {
                    m_dataSoruce = value;
                }
            }
        }
    }

    public class DataGridViewDSCell : DataGridViewTextBoxCell
    {
        public override void InitializeEditingControl(int rowIndex, object
              initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);
            DSDataGridViewEditingControl dataWindowControl =
                DataGridView.EditingControl as DSDataGridViewEditingControl;
            dataWindowControl.PopupGridAutoSize = true;
            dataWindowControl.RowFilterVisible = false;
            DataGridViewDSColumn dataWindowColumn =
                (DataGridViewDSColumn)OwningColumn;
            //dataWindowControl.DataSource = dataWindowColumn.DataSource;
            dataWindowControl.Text = (string)this.Value;
        }
        [Browsable(false)]
        public override Type EditType
        {
            get
            {
                return typeof(DSDataGridViewEditingControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                return typeof(string);
            }
        }
        private DSDataGridViewEditingControl EditingDataWindow
        {
            get
            {
                DSDataGridViewEditingControl dataWindowControl =
                    DataGridView.EditingControl as DSDataGridViewEditingControl;

                return dataWindowControl;
            }
        }
   }

    public class DSDataGridViewEditingControl : DSDataWindow, IDataGridViewEditingControl
    {
        protected int rowIndex;
        protected DataGridView dataGridView;
        protected bool valueChanged = false;

        public DSDataGridViewEditingControl()
        {
        }
        //重写基类
        protected override void OnTextChanged(System.EventArgs e)
        {
            base.OnTextChanged(e);
            NotifyDataGridViewOfValueChange();
        }
        //  当text值发生变化时，通知DataGridView
        private void NotifyDataGridViewOfValueChange()
        {
            valueChanged = true;
            dataGridView.NotifyCurrentCellDirty(true);
        }
        /// <summary>
        /// 在Cell被编辑的时候光标显示
        /// </summary>
        public Cursor EditingPanelCursor
        {
            get
            {
                return Cursors.IBeam;
            }
        }
        /// <summary>
        /// 获取或设置所在的DataGridView
        /// </summary>
        public DataGridView EditingControlDataGridView
        {
            get
            {
                return dataGridView;
            }

            set
            {
                dataGridView = value;
            }
        }

        /// <summary>
        /// 获取或设置格式化后的值
        /// </summary>
        public object EditingControlFormattedValue
        {
            set
            {
                Text = value.ToString();
                NotifyDataGridViewOfValueChange();
            }
            get
            {
                return this.Text;
            }

        }
        /// <summary>
        /// 获取控件的Text值
        /// </summary>
        /// <param name="context">错误上下文</param>
        /// <returns></returns>
        public virtual object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return Text;
        }

        /// <summary>
        /// 编辑键盘
        /// </summary>
        /// <param name="keyData"></param>
        /// <param name="dataGridViewWantsInputKey"></param>
        /// <returns></returns>
        public bool EditingControlWantsInputKey(
       Keys key, bool dataGridViewWantsInputKey)
        {
            // Let the DateTimePicker handle the keys listed.
            switch (key & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                    return true;
                default:
                    return false;
            }
        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {
        }
        public virtual bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// 控件所在行
        /// </summary>
        public int EditingControlRowIndex
        {
            get
            {
                return this.rowIndex;
            }

            set
            {
                this.rowIndex = value;
            }
        }
        /// <summary>
        /// 设置样式
        /// </summary>
        /// <param name="dataGridViewCellStyle"></param>
        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            this.ForeColor = dataGridViewCellStyle.ForeColor;
            this.BackColor = dataGridViewCellStyle.BackColor;
        }
        /// <summary>
        /// 是否值发生了变化
        /// </summary>
        public bool EditingControlValueChanged
        {
            get
            {
                return valueChanged;
            }

            set
            {
                this.valueChanged = value;
            }
        }
    }

    public class DataGridViewSqlColumn : DataGridViewColumn
    {
        private object m_dataSoruce = null;

        public DataGridViewSqlColumn()
            : base(new DataGridViewSqlCell())
        {
        }
        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if (value != null && !value.GetType().IsAssignableFrom(typeof(DataGridViewSqlCell)))
                {
                    throw new InvalidCastException("不是DataGridViewDataWindowCell");
                }
                base.CellTemplate = value;
            }
        }
        private DataGridViewSqlCell ComboBoxCellTemplate
        {
            get
            {
                return (DataGridViewSqlCell)this.CellTemplate;
            }
        }
        public Object DataSource
        {
            get
            {
                return m_dataSoruce;
            }
            set
            {
                if (ComboBoxCellTemplate != value)
                {
                    m_dataSoruce = value;
                }
            }
        }
    }

    public class DataGridViewSqlCell : DataGridViewTextBoxCell
    {
        public override void InitializeEditingControl(int rowIndex, object
              initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);
            SqlDataGridViewEditingControl dataWindowControl =
                DataGridView.EditingControl as SqlDataGridViewEditingControl;
            dataWindowControl.PopupGridAutoSize = true;
            dataWindowControl.RowFilterVisible = false;
            DataGridViewSqlColumn dataWindowColumn =
                (DataGridViewSqlColumn)OwningColumn;
            //dataWindowControl.DataSource = dataWindowColumn.DataSource;
            dataWindowControl.Text = (string)this.Value;
        }
        [Browsable(false)]
        public override Type EditType
        {
            get
            {
                return typeof(SqlDataGridViewEditingControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                return typeof(string);
            }
        }
        private SqlDataGridViewEditingControl EditingDataWindow
        {
            get
            {
                SqlDataGridViewEditingControl dataWindowControl =
                    DataGridView.EditingControl as SqlDataGridViewEditingControl;

                return dataWindowControl;
            }
        }
    }

    public class SqlDataGridViewEditingControl : SqlDataWindow, IDataGridViewEditingControl
    {
        protected int rowIndex;
        protected DataGridView dataGridView;
        protected bool valueChanged = false;

        public SqlDataGridViewEditingControl()
        {
        }
        //重写基类
        protected override void OnTextChanged(System.EventArgs e)
        {
            base.OnTextChanged(e);
            NotifyDataGridViewOfValueChange();
        }
        //  当text值发生变化时，通知DataGridView
        private void NotifyDataGridViewOfValueChange()
        {
            valueChanged = true;
            dataGridView.NotifyCurrentCellDirty(true);
        }
        /// <summary>
        /// 在Cell被编辑的时候光标显示
        /// </summary>
        public Cursor EditingPanelCursor
        {
            get
            {
                return Cursors.IBeam;
            }
        }
        /// <summary>
        /// 获取或设置所在的DataGridView
        /// </summary>
        public DataGridView EditingControlDataGridView
        {
            get
            {
                return dataGridView;
            }

            set
            {
                dataGridView = value;
            }
        }

        /// <summary>
        /// 获取或设置格式化后的值
        /// </summary>
        public object EditingControlFormattedValue
        {
            set
            {
                Text = value.ToString();
                NotifyDataGridViewOfValueChange();
            }
            get
            {
                return this.Text;
            }

        }
        /// <summary>
        /// 获取控件的Text值
        /// </summary>
        /// <param name="context">错误上下文</param>
        /// <returns></returns>
        public virtual object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return Text;
        }

        /// <summary>
        /// 编辑键盘
        /// </summary>
        /// <param name="keyData"></param>
        /// <param name="dataGridViewWantsInputKey"></param>
        /// <returns></returns>
        public bool EditingControlWantsInputKey(
       Keys key, bool dataGridViewWantsInputKey)
        {
            // Let the DateTimePicker handle the keys listed.
            switch (key & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                    return true;
                default:
                    return false;
            }
        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {
        }
        public virtual bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// 控件所在行
        /// </summary>
        public int EditingControlRowIndex
        {
            get
            {
                return this.rowIndex;
            }

            set
            {
                this.rowIndex = value;
            }
        }
        /// <summary>
        /// 设置样式
        /// </summary>
        /// <param name="dataGridViewCellStyle"></param>
        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            this.ForeColor = dataGridViewCellStyle.ForeColor;
            this.BackColor = dataGridViewCellStyle.BackColor;
        }
        /// <summary>
        /// 是否值发生了变化
        /// </summary>
        public bool EditingControlValueChanged
        {
            get
            {
                return valueChanged;
            }

            set
            {
                this.valueChanged = value;
            }
        }
    }
}
