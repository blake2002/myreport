using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMS.Libraries.ToolControls.PMSPublicInfo;

namespace PMS.Libraries.ToolControls.PMSPublicInfo.DBSourceDefine
{
    public partial class NewDBSourceForm : Form
    {
        public string sourceName
        {
            get { return textBoxName.Text; }
        }

        public string description
        {
            get { return textBoxDescription.Text; }
        }

        public object SDForm
        {
            get;
            set;
        }

        public NewDBSourceForm()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(sourceName))
            {
                string strformat = string.Format(GetStringFromPublicResourceClass.GetStringFromPublicResource("NewDBSourceForm_DBSourceCantNull"));
                MessageBox.Show(strformat, GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Warnning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (IsDBSourceNameExisted(sourceName))
            {
                string strformat = string.Format(GetStringFromPublicResourceClass.GetStringFromPublicResource("NewDBSourceForm_DBSourceExisted"), sourceName);
                MessageBox.Show(strformat, GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Warnning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private bool IsDBSourceNameExisted(string Name)
        {
            DBSourceDefineControl form = this.SDForm as DBSourceDefineControl;
            if (form == null)
                return false;
            return form.IsDBSourceNameExisted(Name);
        }
    }
}
