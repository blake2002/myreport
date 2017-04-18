using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using PMS.Libraries.ToolControls.PMSPublicInfo;
using PMS.Libraries.ToolControls.ToolBox;

namespace PMS.Libraries.ToolControls.PmsSheet.PmsPublicData
{
    public partial class RelationFieldChoose : UserControl
    {
        public List<PmsField> pmsFieldList = null;
        public string strRField;
        public string strType;
        public string CtrlType;
        public bool BTable { get; set; }
        private IWindowsFormsEditorService service = null;
        public RelationFieldChoose(IWindowsFormsEditorService service1)
        {
            InitializeComponent();
            strRField = "";
            CtrlType = "";
            service = service1;
            BTable = false;
            //pmsFieldList = PmsSheetCtrl.pmsFieldList;
        }
        private void RelationFieldChoose_Load(object sender, EventArgs e)
        {
            if (pmsFieldList == null)
                return;
            if(BTable == false)
                this.columnHeader1.Text = "名称";
            
            foreach (PmsField pf in pmsFieldList)
            {
                if (CtrlType == "CheckBox")//只能选择布尔型关联字段
                {
                    if (pf.fieldType != "BIT")
                    {
                        continue;
                    }
                }
                else if (CtrlType == "PictureBox")//只能选择Image型关联字段
                {
                    if (pf.fieldType != "IMAGE")
                    {
                        continue;
                    }
                }
                else if (CtrlType == "VARCHAR")//只能选择字符串型关联字段
                {
                    if (!pf.fieldType.StartsWith("VARCHAR"))
                    {
                        continue;
                    }
                }
                else if (CtrlType == "TextBox")//不能选择Image型关联字段
                {
                    if (pf.fieldType == "IMAGE")
                    {
                        continue;
                    }
                    if ((pf.fieldForeigner && pf.fieldForeignerType == PMS.Libraries.ToolControls.PMSPublicInfo.ColumnType.MapTree))
                    {
                        continue;
                    }
                }
                else if (CtrlType == "ComboBox")//不能选择Image型关联字段
                {
                    if (pf.fieldType == "GUID")
                    {
                        continue;
                    }
                    if ((pf.fieldForeigner && pf.fieldForeignerType == PMS.Libraries.ToolControls.PMSPublicInfo.ColumnType.MapTree))
                    {
                        continue;
                    }
                }
                else if (CtrlType == "ForeignKeyCtrl")//只能选择guid类型的外键关系字段
                {
                    //if (!(pf.fieldForeigner && pf.fieldForeignerType == PMS.Libraries.ToolControls.PMSPublicInfo.ColumnType.MapTree) || pf.fieldType != "GUID")
                    if(!pf.fieldName.Equals("ParentID",StringComparison.CurrentCultureIgnoreCase))
                    {
                        continue;
                    }
                }
                ListViewItem lvi = new ListViewItem(pf.fieldName);
                lvi.Tag = pf.fieldType;
                try
                {
                    if (BTable)
                    {
                        lvi.SubItems.Add(PMSDBStructure.GetTablePropertie(pf.fieldName, "Description"));
                    }
                    else
                    {
                        if(!string.IsNullOrEmpty(pf.fieldDescription))
                            lvi.SubItems.Add(pf.fieldDescription);
                    }
                }
                catch
                {
                }
                listBox1.Items.Add(lvi);
            }

            if (strRField != null)
            {
                foreach (ListViewItem tvi in listBox1.Items)
                {
                    if(tvi.Text == strRField)
                    {
                        tvi.Selected = true;
                        break;
                    }
                }
            }
            else
            {
                if (listBox1.Items.Count > 0)
                    listBox1.Items[0].Selected = true;                
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

        private void listBox1_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (listBox1.SelectedItems.Count >= 1)
                {
                    strRField = (string)listBox1.SelectedItems[0].Text;
                    try
                    {
                        strType = (string)listBox1.SelectedItems[0].Tag.ToString();

                    }
                    catch { }
                }
                if (strRField == null)
                    strRField = "";
                service.CloseDropDown();
            }
        }
    }
}
