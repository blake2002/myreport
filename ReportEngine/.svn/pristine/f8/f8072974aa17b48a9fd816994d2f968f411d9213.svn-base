using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMS.Libraries.ToolControls.PMSPublicInfo;

namespace PMS.Libraries.ToolControls.PmsSheet.PmsPublicData
{
    public partial class FormComboBoxItem : Form
    {
        public FormComboBoxItem()
        {
            InitializeComponent();
            listBox1.HorizontalScrollbar = true;
            if (_ComboBoxItemData == null)
                _ComboBoxItemData = new ComboBoxItemData();

            radioButton1.Checked = _ComboBoxItemData.IsSolid;
            radioButton2.Checked = !_ComboBoxItemData.IsSolid;

            listBox2.Items.Add("");

            List<PMSRefDBConnectionObj> lpdb = PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetRefDBConnectionObjList();
            foreach (PMSRefDBConnectionObj pdb in lpdb)
            {
                listBox2.Items.Add(pdb.StrName);
            }
        }
        private ComboBoxItemData _ComboBoxItemData;

        public ComboBoxItemData ExplainData
        {
            get
            {
                _ComboBoxItemData.IsSolid = radioButton1.Checked;
                _ComboBoxItemData.DBString = listBox2.SelectedItem as string;
                _ComboBoxItemData.ExplainList.Clear();

                foreach (string str in listBox1.Items)
                {
                    int pos = str.IndexOf(" -> ");
                    TableField tf = new TableField();
                    if (pos > 0)
                    {
                        tf.tableName = str.Substring(0, pos);
                        tf.fieldName = str.Substring(pos + 4, str.Length - pos - 4);
                    }
                    else
                    {
                        tf.tableName = str;
                        tf.fieldName = "";
                    }
                    _ComboBoxItemData.ExplainList.Add(tf);
                }
                _ComboBoxItemData.fieldName = textBox3.Text;
                _ComboBoxItemData.tableName = textBox4.Text;
                _ComboBoxItemData.SortType = (SortType)btnSort.ImageIndex;
                return this._ComboBoxItemData; 
            }
            set
            {
                this._ComboBoxItemData = value;

                radioButton1.Checked = _ComboBoxItemData.IsSolid;
                radioButton2.Checked = !(_ComboBoxItemData.IsSolid);
                int index = 0;
                foreach (string str in listBox2.Items)
                {
                    if (str == _ComboBoxItemData.DBString)
                    {
                        listBox2.SelectedItem = str;
                        listBox2.SelectedIndex = index;
                        break;
                    }
                    index++;
                }
                listBox1.Items.Clear();

                foreach (TableField tf in _ComboBoxItemData.ExplainList)
                {
                    string item = "";
                    
                    if (string.IsNullOrEmpty(tf.fieldName))
                    {
                        item = tf.tableName;
                    }
                    else
                    {
                        item = tf.tableName+" -> "+tf.fieldName;
                    }
                    listBox1.Items.Add(item);
                }
                if (listBox1.Items.Count > 0)
                    listBox1.SelectedIndex = 0;
                textBox3.Text = _ComboBoxItemData.fieldName;
                textBox4.Text = _ComboBoxItemData.tableName;
                SetSortAppearance(_ComboBoxItemData.SortType);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 0)
            {
                MessageBox.Show("原始数据不能为空");
                textBox2.Focus();
                return;
            }
            foreach (TableField originData in _ComboBoxItemData.ExplainList)
            {
                if (originData.tableName == textBox2.Text)
                {
                    MessageBox.Show("原始数据"+textBox2.Text+"已经存在");
                    textBox2.Focus();
                    return;
                }
            }
            TableField tf = new TableField();
            tf.tableName = textBox2.Text;
            tf.fieldName = textBox1.Text;
            _ComboBoxItemData.ExplainList.Add(tf);
            string text = string.IsNullOrEmpty(textBox1.Text) ? (textBox2.Text) : (textBox2.Text + " -> " + textBox1.Text);
            listBox1.Items.Add(text);            
        }

        private void button4_Click(object sender, EventArgs e)
        {            
            int select = listBox1.SelectedIndex;
            if (select < 0)
                return;
            _ComboBoxItemData.ExplainList.RemoveAt(select);
            
            listBox1.Items.RemoveAt(select);
            listBox1.SelectedIndex = select - 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FormComboBoxItem_Load(object sender, EventArgs e)
        {
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                groupBox2.Enabled = true;
                groupBox1.Enabled = false;
            }
            else
            {
                groupBox1.Enabled = true;
                groupBox2.Enabled = false;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = listBox1.SelectedItem as string;

            if (string.IsNullOrEmpty(str))
            {
                textBox1.Text = textBox2.Text = "";
                return;
            }
            int pos = str.IndexOf(" -> ");
            
            if (pos > 0)
            {
                textBox2.Text = str.Substring(0, pos);
                textBox1.Text = str.Substring(pos + 4, str.Length - pos - 4);
            }
            else
            {
                textBox2.Text = str;
                textBox1.Text = "";
            }
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            switch (this.btnSort.ImageIndex)
            {
                case 0:
                    this.btnSort.ImageIndex = 1;
                    this.lbSort.Text = "升序";
                    break;
                case 1:
                    this.btnSort.ImageIndex = 2;
                    this.lbSort.Text = "降序";
                    break;
                case 2:
                    this.btnSort.ImageIndex = 0;
                    this.lbSort.Text = "不排序";
                    break;
                default:
                    this.btnSort.ImageIndex = 0;
                    this.lbSort.Text = "不排序";
                    break;
            }
            
        }

        private void SetSortAppearance(SortType st)
        {
            switch (st)
            {
                case SortType.None:
                    this.btnSort.ImageIndex = 0;
                    this.lbSort.Text = "不排序";
                    break;
                case SortType.ASC:
                    this.btnSort.ImageIndex = 1;
                    this.lbSort.Text = "升序";
                    break;
                case SortType.DESC:
                    this.lbSort.Text = "降序";
                    this.btnSort.ImageIndex = 2;
                    break;
                default:
                    this.btnSort.ImageIndex = 0;
                    this.lbSort.Text = "不排序";
                    break;
            }
        }
    }
}
