using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Globalization;
using System.ComponentModel.Design.Serialization;

namespace PMS.Libraries.ToolControls.PMSChart
{
    public partial class FormAixs : Form
    {
        public FormAixs()
        {
            InitializeComponent();
            if (_axisList == null)
                _axisList = new Dictionary<string, PMSAxis>();
        }
        private Dictionary<string, PMSAxis> _axisList;

        public Dictionary<string, PMSAxis> AxisList
        {
            get
            {
                _axisList.Clear();
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    AxisDic ax = (AxisDic)listBox1.Items[i];
                    _axisList.Add(ax.name, ax.axis);
                }
                return _axisList;
            }
            set
            {
                _axisList = value;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Focus();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FormAixs_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            for(int i = 0;i<_axisList.Count;i++)//Axis axis in _axisList)
            {
                AxisDic an = new AxisDic();
                an.name = _axisList.Keys.ElementAt(i);
                an.axis = _axisList[an.name];
                listBox1.Items.Add(an);
            }
            if (listBox1.Items.Count > 0)
                listBox1.SelectedIndex = 0;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                AxisDic an = (AxisDic)listBox1.SelectedItem;
                propertyGrid1.SelectedObject = an.axis;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }

}
