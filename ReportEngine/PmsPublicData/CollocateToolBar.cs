using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PMS.Libraries.ToolControls.PmsSheet.PmsPublicData
{
    public partial class CollocateToolBar : Form
    {
        private object _ToolStrip;
        private List<CollocateData> _CollocateResult;
        public object ToolStrip
        {
            get
            {
                return _ToolStrip;
            }
            set
            {
                _ToolStrip = value;
            }
        }
        public List<CollocateData> CollocateResult
        {
            get
            {
                return _CollocateResult;
            }
            set
            {
                _CollocateResult = value;
            }
        }
        public CollocateToolBar()
        {
            InitializeComponent();
        }

        private void CollocateToolBar_Load(object sender, EventArgs e)
        {
            if (_ToolStrip != null)
            {
                if (_ToolStrip.GetType() == typeof(System.Windows.Forms.ToolStrip))
                {
                    //if (((System.Windows.Forms.ToolStrip)_ToolStrip).Visible == true)
                    //{
                        imageList1.Images.Clear();
                        foreach (ToolStripItem item in ((System.Windows.Forms.ToolStrip)_ToolStrip).Items)
                        {
                            if (item.Image == null)
                            {
                                if (item.GetType() == typeof(System.Windows.Forms.ToolStripSeparator))
                                {
                                    imageList1.Images.Add(global::PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.Properties.Resources.Split);
                                }
                                else if (item.GetType() == typeof(System.Windows.Forms.ToolStripComboBox))
                                {
                                    imageList1.Images.Add(global::PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.Properties.Resources.combobox);
                                }
                                else if (item.GetType() == typeof(System.Windows.Forms.ToolStripLabel))
                                {
                                    imageList1.Images.Add(global::PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.Properties.Resources.Lable);
                                }
                                else if (item.GetType() == typeof(System.Windows.Forms.ToolStripTextBox))
                                {
                                    imageList1.Images.Add(global::PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.Properties.Resources.Text);
                                }
                                else if (item.GetType() == typeof(System.Windows.Forms.ToolStripProgressBar))
                                {
                                    imageList1.Images.Add(global::PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.Properties.Resources.PrograssBar);
                                }
                                else if (item.GetType() == typeof(System.Windows.Forms.ToolStripDropDownButton))
                                {
                                    imageList1.Images.Add(global::PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.Properties.Resources.DropDownButton);
                                }
                                else
                                {
                                    imageList1.Images.Add(global::PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.Properties.Resources._default);
                                }
                            }
                            else
                            {
                                imageList1.Images.Add(item.Image);
                            }
                        }
                        int count = 0;
                        foreach (ToolStripItem item in ((System.Windows.Forms.ToolStrip)_ToolStrip).Items)
                        {
                            ListViewItem temp = new ListViewItem();
                            CollocateData collocatetemp = GetCollocate(item.Name);
                            if (!string.IsNullOrEmpty(collocatetemp.ToolBarName))
                            {
                                temp.Checked = collocatetemp.IsVisible;
                            }
                            else
                            {
                                temp.Checked = item.Visible;
                            }
                            temp.ImageIndex = count;
                            temp.Text = item.Name;
                            listView1.Items.Add(temp);
                            ++count;
                        }
                        if (listView1.Items.Count > 1)
                        {
                            button1.Enabled = true;
                            button2.Enabled = true;
                        }
                        else
                        {
                            button1.Enabled = false;
                            button2.Enabled = false;
                        }
                    //}
                    //else
                    //{
                    //    button1.Enabled = false;
                    //    button2.Enabled = false;
                    //}
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                if (listView1.SelectedItems[0].Index > 0)
                {
                    ListViewItem temp = listView1.SelectedItems[0];
                    int i = temp.Index;
                    listView1.Items.Remove(temp);
                    listView1.Items.Insert(i - 1, temp);
                    listView1.Focus();
                    listView1.Items[i - 1].Selected = true;
                    listView1.Items[i - 1].Focused = true;
                    listView1.Invalidate();
                }
                else
                {
                    listView1.Focus();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                if (listView1.SelectedItems[0].Index !=listView1.Items.Count-1)
                {
                    ListViewItem temp = listView1.SelectedItems[0];
                    int i = temp.Index;
                    listView1.Items.Remove(temp);
                    listView1.Items.Insert(i + 1, temp);
                    listView1.Focus();
                    listView1.Items[i + 1].Selected = true;
                    listView1.Items[i + 1].Focused = true;
                    listView1.Invalidate();
                }
                else
                {
                    listView1.Focus();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_CollocateResult != null)
            {
                _CollocateResult = new List<CollocateData>();
            }
            else
            {
                _CollocateResult = new List<CollocateData>();
            }
            foreach (ListViewItem item in listView1.Items)
            {
                CollocateData temp = new CollocateData();
                temp.ToolBarName = item.Text;
                temp.IsVisible = item.Checked;
                temp.Index = item.Index;
                _CollocateResult.Add(temp);
            }
            this.DialogResult = DialogResult.OK;
        }
        /// <summary>
        /// 2011.09.20 增加
        /// 根据名称获取配置数据结构体
        /// </summary>
        /// <param name="Name">要查询的名字</param>
        /// <returns>返回查询结果</returns>
        private CollocateData GetCollocate(string Name)
        {
            CollocateData result = new CollocateData();
            if (!string.IsNullOrEmpty(Name) && _CollocateResult != null)
            {
                for (int i = 0; i < _CollocateResult.Count; i++)
                {
                    CollocateData temp = _CollocateResult[i];
                    if (temp.ToolBarName == Name)
                    {
                        result = temp;
                        break;
                    }
                }
            }
            return result;
        }
    }
    [Serializable]
    public struct CollocateData
    {
        public string ToolBarName;//工具按钮名称
        public bool IsVisible; //是否起用
        public int Index;//序号
    }
}
