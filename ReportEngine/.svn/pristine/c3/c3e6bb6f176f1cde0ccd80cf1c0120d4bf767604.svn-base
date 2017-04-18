using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace PMS.Libraries.ToolControls.PMSChart
{
    public partial class YaxisChoose : UserControl
    {
        public List<string> Yaxis = null;
        public string CurrentYaxis = "";
        private IWindowsFormsEditorService service = null;
        public YaxisChoose(IWindowsFormsEditorService service1)
        {
            InitializeComponent();
            service = service1;
        }

        private void YaxisChoose_Load(object sender, EventArgs e)
        {
            if (Yaxis == null)
                return;
            foreach (string str in Yaxis)
            {
                ListViewItem lvi = new ListViewItem(str);
                listView1.Items.Add(lvi);
            }

            if (!string.IsNullOrEmpty(CurrentYaxis))
            {
                foreach (ListViewItem tvi in listView1.Items)
                {
                    if (tvi.Text == CurrentYaxis)
                    {
                        tvi.Selected = true;
                        break;
                    }
                }
            }
            else
            {
                if (listView1.Items.Count > 0)
                    listView1.Items[0].Selected = true;
            }
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                service.CloseDropDown();
                return true; ;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (listView1.SelectedItems.Count >= 1)
                {
                    CurrentYaxis = listView1.SelectedItems[0].Text;

                }
                if (CurrentYaxis == null)
                    CurrentYaxis = "";
                service.CloseDropDown();
            }
        }
    }
}
