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
    public partial class FormTableRelation : Form
    {
        public FormTableRelation()
        {
            InitializeComponent();
        }
        private List<TableJoinRelation> _pmsJoinRelation = null;

        private bool listBoxTableNoChange = false;
        private bool compareNoChange = false;

        public List<TableJoinRelation> PmsJoinRelation
        {
            get { return _pmsJoinRelation; }
            set { _pmsJoinRelation = value; }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxTableNoChange)
                return;
            int oldIndex = listBoxTable.SelectedIndex;
            if (oldIndex < 0)
                return;
            TableJoinRelation tjr = _pmsJoinRelation[oldIndex];
            string abc = "[" + tjr.mainTable + "].";
            abc += "[" + tjr.mainColumn + "] ";
            abc += comboBoxCompare.Text;
            abc += " [" + tjr.secondaryTable + "].";
            abc += "[" + tjr.secondaryColumn + "] ";
            compareNoChange = true;
            listBoxTable.Items[oldIndex] = abc;
            compareNoChange = false;

            tjr.compare = comboBoxCompare.Text;
            _pmsJoinRelation[oldIndex] = tjr;

            listBoxTable.Invalidate();
            buttonOk.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FormTableRelation_Load(object sender, EventArgs e)
        {
            listBoxTable.Items.Clear();
            foreach (TableJoinRelation tjr in _pmsJoinRelation)
            {
                string abc = "[" + tjr.mainTable + "].";
                abc += "[" + tjr.mainColumn + "] ";
                abc += tjr.compare;
                abc += " [" + tjr.secondaryTable + "].";
                abc += "[" + tjr.secondaryColumn + "] ";
                listBoxTable.Items.Add(abc);
            }

            if (listBoxTable.Items.Count > 0)
                listBoxTable.SelectedIndex = 0;

            buttonOk.Enabled = false;
        }

        private void buttonDeleteRelation_Click(object sender, EventArgs e)
        {
            if (listBoxTable.SelectedIndex < 0)
                return;
            int oldIndex = listBoxTable.SelectedIndex;
            listBoxTable.Items.RemoveAt(listBoxTable.SelectedIndex);
            _pmsJoinRelation.RemoveAt(oldIndex);

            if (listBoxTable.Items.Count <= oldIndex)
            {
                listBoxTable.SelectedIndex = listBoxTable.Items.Count - 1;
            }
            else
            {
                listBoxTable.SelectedIndex = oldIndex;
            }
            buttonOk.Enabled = true;
        }

        private void listBoxTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (compareNoChange)
                return;
            int oldIndex = listBoxTable.SelectedIndex;
            if (oldIndex < 0)
                return;

            TableJoinRelation tjr = _pmsJoinRelation[oldIndex];

            textBoxTableMain.Text = tjr.mainTable;
            textBoxColumnMain.Text = tjr.mainColumn;
            textBoxTableSecondary.Text = tjr.secondaryTable;
            textBoxColumnSecondary.Text = tjr.secondaryColumn;
            listBoxTableNoChange = true;
            comboBoxCompare.Text = tjr.compare;
            listBoxTableNoChange = false;
            checkBoxTableMain.Text = tjr.mainTable + "表的所有行";
            checkBoxTableSecondary.Text = tjr.secondaryTable + "表的所有行";
            setCheckBox(tjr.joinType);
        }

        private void checkBoxTableMain_CheckedChanged(object sender, EventArgs e)
        {
            int oldIndex = listBoxTable.SelectedIndex;
            if (oldIndex < 0)
                return;

            setNewJoinType(oldIndex);
            
            buttonOk.Enabled = true;
        }

        private void checkBoxTableSecondary_CheckedChanged(object sender, EventArgs e)
        {
            int oldIndex = listBoxTable.SelectedIndex;
            if (oldIndex < 0)
                return;

            setNewJoinType(oldIndex);
            buttonOk.Enabled = true;
        }
        private void setCheckBox(int index)
        {
            switch (index)
            {
                case 1:
                    checkBoxTableMain.Checked=true;
                    checkBoxTableSecondary.Checked = false;
                    break;
                case 2:
                    checkBoxTableMain.Checked = false;
                    checkBoxTableSecondary.Checked = true;
                    break;
                case 3:
                    checkBoxTableMain.Checked = true;
                    checkBoxTableSecondary.Checked = true;
                    break;
                case 0:
                    checkBoxTableMain.Checked = false;
                    checkBoxTableSecondary.Checked = false;
                    break;
            }
        }
        private void setNewJoinType(int index)
        {
            TableJoinRelation tjr = _pmsJoinRelation[index];
            if (checkBoxTableMain.Checked && checkBoxTableSecondary.Checked)
            {
                tjr.joinType = 3;
            }
            else if (!checkBoxTableMain.Checked && checkBoxTableSecondary.Checked)
            {
                tjr.joinType = 2;
            }
            else if (checkBoxTableMain.Checked && !checkBoxTableSecondary.Checked)
            {
                tjr.joinType = 1;
            }
            else if (!checkBoxTableMain.Checked && !checkBoxTableSecondary.Checked)
            {
                tjr.joinType = 0;
            }
            _pmsJoinRelation[index] = tjr;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
