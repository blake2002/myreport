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
    public partial class FormSqlEdit : Form
    {
        public FormSqlEdit()
        {
            InitializeComponent();
        }
        public string SqlText
        {
            get
            {
                return this.richTextBoxParser1.Text;
            }
            set
            {
                this.richTextBoxParser1.Text = value;
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
    }
}
