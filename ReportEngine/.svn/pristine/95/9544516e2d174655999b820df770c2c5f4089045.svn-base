using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using PMS.Libraries.ToolControls.ToolBox;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;

namespace PMS.Libraries.ToolControls.PMSChart
{
    public partial class FormQueryFormula : Form
    {
        public FormQueryFormula()
        {
            InitializeComponent();
            OperaterType = 1;
        }
        private List<PmsField> _pmsFieldList = null;

        public FormSql FormSql1 { set; get; }
        public int OperaterType { set; get; }
        

        public List<PmsField> PmsFieldList
        {
            get { return _pmsFieldList; }
            set { _pmsFieldList = value; }
        }
        public string FormulaText{set;get;}
        private List<string> _tableList;

        public List<string> TableList
        {
            get { return _tableList; }
            set { _tableList = value; }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("");

            PmsField pf = null;
            foreach (PmsField Field in _pmsFieldList)
            {
                if (Field.fieldName == (string)comboBox2.SelectedItem)
                {
                    pf = Field;
                    break;
                }
            }
            comboBox1.SelectedIndex = 0;
            if(pf==null)
                return;
            if (pf.fieldType.Equals("FLOAT") || pf.fieldType.Equals("REAL") || pf.fieldType.Equals("INT"))
            {
                comboBox1.Items.Add("Avg");
                comboBox1.Items.Add("Count");
                comboBox1.Items.Add("Max");
                comboBox1.Items.Add("Min");
                comboBox1.Items.Add("Sum");                
            }
            else if (pf.fieldType.Equals("BIT"))
            {
                comboBox1.Items.Add("Count");
            }
            else if (pf.fieldType.Equals("DATETIME")||pf.fieldType.StartsWith("VARCHAR"))
            {
                comboBox1.Items.Add("Count");
                comboBox1.Items.Add("Max");
                comboBox1.Items.Add("Min");
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            int pos = textBoxFormula.SelectionStart;
            textBoxFormula.Text = textBoxFormula.Text.Insert(pos, "+");
            textBoxFormula.SelectionStart = pos + 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormulaText = getFormulaText();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FormQueryFormula_Load(object sender, EventArgs e)
        {
            if (FormulaText != null)
            {
                int nAsPos = FormulaText.IndexOf(" as ");
                if (nAsPos > 0)
                {
                    this.textBoxFormula.Text = FormulaText.Substring(0,nAsPos);
                    this.textBoxAlias.Text = FormulaText.Substring(nAsPos + 4, FormulaText.Length - (nAsPos + 4));
                }
                else
                    this.textBoxFormula.Text = FormulaText;
            }
            comboBox2.Items.Clear();            
            foreach(PmsField pf in _pmsFieldList)
            {
                comboBox2.Items.Add(pf.fieldName);
            }
            if (comboBox2.Items.Count > 0)
                comboBox2.SelectedIndex = 0;

            if (OperaterType != 1)
                buttonApp.Enabled = false;
        }

        private void buttonAddField_Click(object sender, EventArgs e)
        {
            int pos = textBoxFormula.SelectionStart;
            string field="";
            if(comboBox1.Text.Length==0)
                field = comboBox2.Text;
            else
                field = comboBox1.Text + "(" + comboBox2.Text + ")";
            textBoxFormula.Text = textBoxFormula.Text.Insert(textBoxFormula.SelectionStart, field);
            textBoxFormula.SelectionStart = pos + field.Length;
        }

        private void buttonDecrease_Click(object sender, EventArgs e)
        {
            int pos = textBoxFormula.SelectionStart;
            textBoxFormula.Text = textBoxFormula.Text.Insert(pos, "-");
            textBoxFormula.SelectionStart = pos + 1;
        }

        private void buttonMultiply_Click(object sender, EventArgs e)
        {
            int pos = textBoxFormula.SelectionStart;
            textBoxFormula.Text = textBoxFormula.Text.Insert(pos, "*");
            textBoxFormula.SelectionStart = pos + 1;
        }

        private void buttonDivide_Click(object sender, EventArgs e)
        {
            int pos = textBoxFormula.SelectionStart;
            textBoxFormula.Text = textBoxFormula.Text.Insert(pos, "/");
            textBoxFormula.SelectionStart = pos + 1;
        }

        private void buttonMod_Click(object sender, EventArgs e)
        {
            int pos = textBoxFormula.SelectionStart;
            textBoxFormula.Text = textBoxFormula.Text.Insert(pos, "%");
            textBoxFormula.SelectionStart = pos + 1;
        }

        private void buttonApp_Click(object sender, EventArgs e)
        {
            if (FormSql1!=null)
            {
                if (textBoxFormula.Text.Length > 0)
                    FormSql1.AddFormula(getFormulaText(), OperaterType);
            }
            buttonApp.Enabled = false;
        }
        private string getFormulaText()
        {
            string itemText = textBoxAlias.Text;
            
            if (itemText.Length == 0)
            {                
                return textBoxFormula.Text;
            }
            else
                return textBoxFormula.Text + " as " + textBoxAlias.Text;
        }

        private void textBoxFormula_TextChanged(object sender, EventArgs e)
        {
            buttonApp.Enabled = true;
        }

        private void textBoxAlias_TextChanged(object sender, EventArgs e)
        {
            buttonApp.Enabled = true;
        }
        public bool IsAppied()
        {
            return buttonApp.Enabled;
        }
    }
    
}
