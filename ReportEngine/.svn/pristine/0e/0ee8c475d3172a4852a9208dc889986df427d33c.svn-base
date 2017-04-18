using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
//***********************************************************************************
//  文件名:SampleDistanceVar.cs
//修改类型:增加
//修改目的:由于SouceFiledDataTable增加了一个属性 SampleDistanceVar,其目的是允许用户通过此属性
//         绑定报表变量 来调整间隔筛选的时间间隔,因此需要提供一个下拉选择框让用户可以选定报表变量
//         同时还要对报表变量进行过滤,因为此处只能允许选用整型的报表变量
//修改时间:2011年12月19日
//    作者:董平
//***********************************************************************************
namespace PMS.Libraries.ToolControls.PmsSheet.PmsPublicData
{
    public partial class SampleDistanceVar : UserControl
    {
        public List<PMSVar> ReportVarList = null;//变量列表
        public string CurrentVarName;//当前变量名
        private IWindowsFormsEditorService service = null;
        public SampleDistanceVar(IWindowsFormsEditorService service1)
        {
            InitializeComponent();
            service = service1;
        }

        private void SampleDistanceVar_Load(object sender, EventArgs e)
        {
            if (ReportVarList == null)
                return;
            listView1.Items.Clear();
            foreach (PMSVar pf in ReportVarList)
            {
                if (pf.VarType != MESVarType.MESInt)
                {
                    continue;
                }
                ListViewItem lvi = new ListViewItem(new string[]{pf.VarName,pf.VarDesc});
                lvi.Tag = pf;
                listView1.Items.Add(lvi);
            }

            if (CurrentVarName != null)
            {
                foreach (ListViewItem tvi in listView1.Items)
                {
                    if (tvi.Text == CurrentVarName)
                    {
                        tvi.Selected = true;
                        break;
                    }
                }
            }
            else
            {
                if (listView1.Items!=null && listView1.Items.Count > 0)
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
                    CurrentVarName=listView1.SelectedItems[0].Text;
                }
                service.CloseDropDown();
            }
        }
    }
}
