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
    public partial class FieldCollection : Form
    {
        private List<string> _RecordFields;
        public List<string> RecordFields
        {
            get
            {
                return _RecordFields;
            }
            set
            {
                _RecordFields = value;
            }
        }
        public FieldCollection()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _RecordFields = new List<string>();
            foreach (string item in richTextBox1.Lines)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    if (!_RecordFields.Contains(item))
                    {
                        _RecordFields.Add(item);
                    }
                }
            }
            this.DialogResult = DialogResult.OK;
        }

        private void FieldCollection_Load(object sender, EventArgs e)
        {
            if (_RecordFields != null)
            {
                richTextBox1.Lines = _RecordFields.ToArray();
            }
        }
    }
}
