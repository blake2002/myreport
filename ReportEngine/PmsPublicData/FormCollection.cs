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
    public partial class FormCollection : Form
    {
        public FormCollection()
        {
            InitializeComponent();
        }
        private List<ValueMap> dataList;
        public List<ValueMap> DataList
        {
            get
            {
                if (dataList == null)
                    dataList = new List<ValueMap>();
                dataList.Clear();

                foreach (var obj in listBox1.Items)
                {
                    dataList.Add((ValueMap)obj);
                }
                return dataList;
            }
            set
            {
                dataList = value;
                if (dataList == null)
                    return;
                listBox1.Items.Clear();
                foreach (var obj in dataList)
                {
                    listBox1.Items.Add(obj);
                }
                if (listBox1.Items.Count > 0)
                    listBox1.SelectedIndex = 0;
            }
        }
        private void FormCollection_Load(object sender, EventArgs e)
        {
            listBox1.Focus();
        }
        //+
        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(new ValueMap());
            listBox1.SelectedIndex = listBox1.Items.Count-1;
        }
        //-
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.Items.Count <= 0)
                    return;

                int iSelect = listBox1.SelectedIndex;
                listBox1.Items.RemoveAt(iSelect);

                listBox1.SelectedIndex = (iSelect == 0) ? 0 : iSelect - 1;

                button4.Enabled = (listBox1.Items.Count > 0);
            }
            catch
            {
                listBox1.SelectedIndex = -1;
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
        //up
        private void button5_Click(object sender, EventArgs e)
        {
            int iSelect = listBox1.SelectedIndex;
            if (iSelect == 0)
                return;
            ValueMap now = (ValueMap)listBox1.Items[iSelect];
            ValueMap nowUp = (ValueMap)listBox1.Items[iSelect - 1];

            listBox1.Items[iSelect] = nowUp;
            listBox1.Items[iSelect - 1] = now;

            listBox1.SelectedIndex = iSelect - 1;
        }
        //down
        private void button6_Click(object sender, EventArgs e)
        {
            int iSelect = listBox1.SelectedIndex;
            if (iSelect + 1 >= listBox1.Items.Count)
                return;
            ValueMap now = (ValueMap)listBox1.Items[iSelect];
            ValueMap nowUp = (ValueMap)listBox1.Items[iSelect + 1];

            listBox1.Items[iSelect] = nowUp;
            listBox1.Items[iSelect + 1] = now;

            listBox1.SelectedIndex = iSelect + 1;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iSelect = listBox1.SelectedIndex;
            if (iSelect <= 0)
                button5.Enabled = false;
            else
                button5.Enabled = true;

            if(iSelect+1>=listBox1.Items.Count)
                button6.Enabled = false;
            else
                button6.Enabled = true;
            if(iSelect<0)
                button4.Enabled = false;
            else
                button4.Enabled = true;

            if (iSelect >= 0)
                propertyGrid1.SelectedObject = listBox1.SelectedItem;
            else
                propertyGrid1.SelectedObject = null;
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            int i = listBox1.SelectedIndex;
            List<ValueMap> list = DataList;
            DataList = list;

            listBox1.SelectedIndex = i;
        }
    }
}
