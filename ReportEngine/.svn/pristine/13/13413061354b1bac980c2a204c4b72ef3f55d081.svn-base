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
    public partial class FormStripLineCollection : Form
    {
        public FormStripLineCollection()
        {
            InitializeComponent();
        }
        private List<string> existName = new List<string>();
        private List<PMSStripLine> dataList;
        public List<PMSStripLine> DataList
        {
            get
            {
                if (dataList == null)
                    dataList = new List<PMSStripLine>();
                dataList.Clear();

                foreach (var obj in listBox1.Items)
                {
                    dataList.Add((PMSStripLine)obj);
                }
                return dataList;
            }
            set
            {
                dataList = value;
                if (dataList == null)
                    return;
                listBox1.Items.Clear();
                existName.Clear();
                foreach (var obj in dataList)
                {
                    existName.Add(obj.Name);
                    obj.Parent = existName;
                    listBox1.Items.Add(obj);
                }
                
                
            }
        }
        private void FormCollection_Load(object sender, EventArgs e)
        {
            listBox1.Focus();
            if (listBox1.Items.Count > 0)
                listBox1.SelectedIndex = 0;
            else
                listBox1.SelectedIndex = -1;

            listBox1_SelectedIndexChanged(null, null);
        }
        //+
        private void button3_Click(object sender, EventArgs e)
        {
            PMSStripLine pca = new PMSStripLine(null);            
            pca.Parent = existName;
            pca.Name = GetNewName("StripLine");
            existName.Add(pca.Name);
            listBox1.Items.Add(pca);
            listBox1.SelectedIndex = listBox1.Items.Count-1;
        }
        private string GetNewName(string primary)
        {
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                string newstring = primary + (i + 1).ToString();
                bool bExist = false;
                foreach (var child in listBox1.Items)
                {
                    PMSStripLine child1 = (PMSStripLine)child;
                    if (child1.Name == newstring)
                    {
                        bExist = true;
                        break;
                    }
                }
                if (!bExist)
                    return newstring;
            }
            return primary + (listBox1.Items.Count + 1).ToString();
        }
        //-
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.Items.Count <= 0)
                    return;
                PMSStripLine pca = (PMSStripLine)listBox1.SelectedItem;
                existName.Remove(pca.Name);
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
            button1.Focus();
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
                int i = listBox1.SelectedIndex;
                List<PMSStripLine> list = DataList;
                DataList = list;

                listBox1.SelectedIndex = i;
            }
        }
    }
}
