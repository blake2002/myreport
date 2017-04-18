using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMS.Libraries.ToolControls.Report.Element;

namespace PMS.Libraries.ToolControls.Report.Controls.EditorDialog
{
    public partial class MappingFrm : Form
    {
        public bool EnableMapping
        {
            get;
            set;
        }

        public string TableName
        {
            get;
            set;
        }

        public MappingFrm()
        {
            InitializeComponent();
            List<string> list = PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.EnumDataMapTable();
            this.MappingCmb.Items.Add("");
            foreach (string str in list)
            {
                this.MappingCmb.Items.Add(str);
            }
        }

        public void Bind(IDataMapping mapping)
        {
            if (null != mapping)
            {
                EnableCb.Checked = mapping.EnableMapping;
                try
                {
                    this.MappingCmb.SelectedItem = mapping.MappingTable;
                }
                catch
                { 
                 
                }
            }
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            EnableMapping = EnableCb.Checked;
            TableName = MappingCmb.SelectedItem as string;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void CancleBtn_Click(object sender, EventArgs e)
        {
            
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void MappingFrm_Load(object sender, EventArgs e)
        {
           
        }

        private void EnableCb_CheckedChanged(object sender, EventArgs e)
        {
            this.MappingCmb.Enabled = EnableCb.Checked;
        }


    }
}
