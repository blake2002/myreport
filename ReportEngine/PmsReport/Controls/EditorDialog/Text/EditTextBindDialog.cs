using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using PMS.Libraries.ToolControls.PMSPublicInfo;

namespace PMS.Libraries.ToolControls.Report.Controls.EditorDialog
{
    public partial class EditTextBindDialog : Form
    {
        public string Var
        {
            get;
            set;
        }
        public EditTextBindDialog()
        {
            InitializeComponent();
        }

        private void EditTextBindiDialogLoad(object sender, EventArgs e)
        {
            FieldTreeViewData fvd = CurrentPrjInfo.GetCurrentReportDataDefine() as FieldTreeViewData;
            if (null != fvd && fvd.Nodes.Length>0)
            {
                ReportVar rv = fvd.Nodes[0].Tag as ReportVar;
                if (null != rv)
                {
                    foreach (PMSVar pmsVar in rv.PMSVarList)
                    { 
                        this.VarTreeView.Nodes.Add(new TreeNode(pmsVar.VarName,0,0));
                    }
                }
            }

        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            if (null != this.VarTreeView.SelectedNode)
            {
                this.Var = this.VarTreeView.SelectedNode.Text;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void CancleBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void VarTreeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OkBtn_Click(null, null);
        }
    }
}
