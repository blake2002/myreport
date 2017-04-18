using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PMS.Libraries.ToolControls.PMSChart.designDialog
{
    public partial class FormAppearance : Form
    {
        public FormAppearance()
        {
            InitializeComponent();
        }
        public PMSChartCtrl PMSParent;
        public PMSSeries PMSSeries1;
        public PMSChartArea PMSChartArea1;
        public PMSLegend PMSLegend1;
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (PMSParent != null)
                PMSParent.InitailColumnData();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = listBox1.SelectedItem;
        }

        private void FormAppearance_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (PMSSeries1 == null)
                PMSSeries1 = new PMSSeries(null);

            if (PMSChartArea1 == null)
                PMSChartArea1 = new PMSChartArea(null);
            if (PMSLegend1 == null)
                PMSLegend1 = new PMSLegend(null);

            listBox1.Items.Add(PMSSeries1);
            listBox1.Items.Add(PMSChartArea1);
            listBox1.Items.Add(PMSLegend1);

            if (listBox1.Items.Count > 0)
                listBox1.SelectedIndex = 0;

        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            //PMSSeries1.SetSeriesValue(chart1.Series[0]);
            //PMSChartArea1.SetChartArea(chart1.ChartAreas[0]);
        }
    }
}
