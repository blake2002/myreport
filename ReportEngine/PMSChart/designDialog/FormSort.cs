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
    public partial class FormSort : Form
    {
        public FormSort()
        {
            InitializeComponent();
            if (_sortList == null)
                _sortList = new List<SortClass>();
        }
        private List<SortClass> _sortList;
        public List<PmsField> FieldList;
        public List<SortClass> SortList
        {
            get
            {
                
                _sortList.Clear();
                foreach (var sort in listBox1.Items)
                {
                    _sortList.Add((SortClass)sort);
                }
                return _sortList;
            }
            set
            {
                _sortList = value;
                if (_sortList == null)
                    _sortList = new List<SortClass>();
                listBox1.Items.Clear();
                foreach (var sort in _sortList)
                {
                    listBox1.Items.Add(sort);
                }
            }
        }
        private void FormSort_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            foreach (var pf in FieldList)
            {
                comboBox1.Items.Add(pf.fieldName);
            }
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            if (listBox1.Items.Count > 0)
                listBox1.SelectedIndex = 0;
            else
            {
                button3.Enabled = false;
                button4.Enabled = false;
            }                        
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            int oldIndex = listBox1.SelectedIndex;
            if (oldIndex < 0)
                return;

            listBox1.Items.RemoveAt(oldIndex);
            if (listBox1.Items.Count <= oldIndex)
            {
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }
            else
            {
                listBox1.SelectedIndex = oldIndex;
            }
        }


        private void buttonAdd_Click(object sender, EventArgs e)
        {
            bool bFind = false;
            foreach (var sort in listBox1.Items)
            {
                SortClass sc = (SortClass)sort;
                if (sc.fieldName == comboBox1.Text)
                {
                    bFind = true;
                    break;
                }
            }
            if (!bFind)
            {
                SortClass sc = new SortClass();
                sc.fieldName = comboBox1.Text;
                if (comboBox2.SelectedIndex == 0)
                    sc.sortType = SortType.Asc;
                else
                    sc.sortType = SortType.Desc;

                listBox1.Items.Add(sc);
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }
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

        private void button3_Click(object sender, EventArgs e)//up
        {
            int oldIndex = listBox1.SelectedIndex;
            if (oldIndex <= 0)
                return;

            SortClass sc = (SortClass)listBox1.SelectedItem;
            SortClass sc_up = (SortClass)listBox1.Items[oldIndex-1];
            listBox1.Items[oldIndex - 1] = sc;
            listBox1.Items[oldIndex] = sc_up;
            listBox1.SelectedIndex = oldIndex - 1;
        }

        private void button4_Click(object sender, EventArgs e)//down
        {
            int oldIndex = listBox1.SelectedIndex;
            if (oldIndex < 0)
                return;
            if (oldIndex > listBox1.Items.Count-2)
                return;

            SortClass sc = (SortClass)listBox1.SelectedItem;
            SortClass sc_down = (SortClass)listBox1.Items[oldIndex + 1];
            listBox1.Items[oldIndex + 1] = sc;
            listBox1.Items[oldIndex] = sc_down;
            listBox1.SelectedIndex = oldIndex + 1;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)//显示
        {
            if (listBox1.SelectedIndex < 0)
                return;
            if (listBox1.SelectedIndex == 0)
            {
                button3.Enabled = false;
            }
            else
            {
                button3.Enabled = true;
            }
            if (listBox1.SelectedIndex == listBox1.Items.Count-1)
            {
                button4.Enabled = false;
            }
            else
            {
                button4.Enabled = true;
            }
            SortClass sc = (SortClass)listBox1.SelectedItem;
            comboBox1.SelectedIndex = comboBox1.FindString(sc.fieldName);
            if (sc.sortType == SortType.Desc)
                comboBox2.SelectedIndex = 1;
            else
                comboBox2.SelectedIndex = 0;            
        }
    }
}
