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
    public partial class OkCancelText : Form
    {
        private Dictionary<string, string> _Texts = null;
        public Dictionary<string, string> Texts
        {
            get
            {
                return _Texts;
            }
            set
            {
                _Texts = value;
            }
        }
        public OkCancelText()
        {
            InitializeComponent();
        }

        private void Sure_Click(object sender, EventArgs e)
        {
            if (_Texts == null)
            {
                _Texts = new Dictionary<string, string>();
            }
            else
            {
                _Texts.Clear();
            }
            if (!string.IsNullOrEmpty(Ok.Text) || !string.IsNullOrEmpty(No.Text))
            {
                _Texts.Add("OK", Ok.Text);
                _Texts.Add("Cancel", No.Text);
            }
            else
            {
                _Texts = null;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void OkCancelText_Load(object sender, EventArgs e)
        {
            if (_Texts != null)
            {
                if (_Texts.ContainsKey("OK"))
                {
                    Ok.Text = _Texts["OK"];
                }
                if (_Texts.ContainsKey("Cancel"))
                {
                    No.Text = _Texts["Cancel"];
                }
            }
        }
        public override string ToString()
        {
            //return base.ToString();
            return Properties.Resources.ResourceManager.GetString("context0003");
        }
    }
}
