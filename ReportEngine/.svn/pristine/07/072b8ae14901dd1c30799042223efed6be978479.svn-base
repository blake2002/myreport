using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PMS.Libraries.ToolControls
{
    public partial class ReportConfigForm : Form
    {
        private MES.DataMapTable.MapTableForm _mapTableFrm;

        public ReportConfigForm()
        {
            InitializeComponent();
        }

        public bool InitialWindow()
        {
            string strFile = PMS.Libraries.ToolControls.PMSPublicInfo.ProjectPathClass.PrjMapTableFilePath;
            this._mapTableFrm = new MES.DataMapTable.MapTableForm(strFile);
            // 
            // bOCfgFrm
            //
            this._mapTableFrm.TopLevel = false;
            this._mapTableFrm.Parent = ppPane_MapTable;
            this._mapTableFrm.Dock = DockStyle.Fill;
            this._mapTableFrm.ShowInTaskbar = false;
            this._mapTableFrm.FormBorderStyle = FormBorderStyle.None;

            this._mapTableFrm.SetOKCancleBtnVisible(false);
            this._mapTableFrm.Show();
            this.ppPane_MapTable.Controls.Add(_mapTableFrm);
            
            return true;
        }

        private void ReportConfigForm_Load(object sender, EventArgs e)
        {
            InitialWindow();
        }

        private void button_Apply_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            Save();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private bool Save()
        {
            return this.dbSourceDefineControl1.Save();
        }
    }
}
