using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PMS.Libraries.ToolControls.Report.Controls.EditorDialog
{
    public partial class MappingTableEditorPanel : UserControl
    {
        public event SelectValue OnSelectValue;
        public MappingTableEditorPanel()
        {
            InitializeComponent();
            EnumTables();
        }

        private void EnumTables()
        {
            List<string> list = PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.EnumDataMapTable();
            if (null != list)
            {
                foreach (string str in list)
                {
                    MappingTableLb.Items.Add(str);
                }
            }
        }

        private void MappingTableLb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != OnSelectValue)
            {
                try
                {
                    OnSelectValue(this,new SelectValueEventArgs(this.MappingTableLb.SelectedItem as string));
                }
                catch
                { 
                    
                }
            }
        }
    }

    public delegate void SelectValue(object sender, SelectValueEventArgs e);

    public class SelectValueEventArgs : EventArgs
    {
        public string SelectValue
        {
            get;
            set;
        }

        public SelectValueEventArgs(string value)
        {
            SelectValue = value;
        }
    }
}
