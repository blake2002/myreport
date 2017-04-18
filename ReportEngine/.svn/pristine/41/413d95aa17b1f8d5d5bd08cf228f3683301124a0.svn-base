using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PMS.Libraries.ToolControls.PMSChart
{
    public partial class FormCollection : Form
    {
        public FormCollection()
        {
            InitializeComponent();
        }
        private List<object> dataList;
        public List<object> DataList
        {
            get
            {
                if (dataList == null)
                    dataList = new List<object>();
                dataList.Clear();

                foreach (var obj in listBox1.Items)
                {
                    dataList.Add(obj);
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
            listBox1.Items.Add(new object());
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
            object now = (object)listBox1.Items[iSelect];
            object nowUp = (object)listBox1.Items[iSelect-1];

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
            object now = (object)listBox1.Items[iSelect];
            object nowUp = (object)listBox1.Items[iSelect + 1];

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
            if (e.ChangedItem.Label.Equals("Name", StringComparison.CurrentCultureIgnoreCase))
            {
            }
        }
    }
}
