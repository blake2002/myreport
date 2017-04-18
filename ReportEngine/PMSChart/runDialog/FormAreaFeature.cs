using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PMS.Libraries.ToolControls.PMSChart
{
    public partial class FormAreaFeature : Form
    {
        public FormAreaFeature()
        {
            InitializeComponent();
        }
        private ChartAreaCollection _chartSerials;

        public ChartAreaCollection ChartSerials
        {
            get { return _chartSerials; }
            set { _chartSerials = value; }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
                this.propertyGrid1.SelectedObject = (ChartArea)listBox1.SelectedItem;
            else
                this.propertyGrid1.SelectedObject = null;
        }

        private void FormAreaFeature_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (_chartSerials != null)
            {
                foreach (ChartArea ca in _chartSerials)
                {
                    listBox1.Items.Add(ca);
                }
                if (listBox1.Items.Count > 0)
                    listBox1.SelectedIndex = 0;
            }
        }
    }
}
