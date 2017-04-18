using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using PMS.Libraries.ToolControls.Report.Element;
using PMS.Libraries.ToolControls.Report.Controls.EditorDialog;

namespace PMS.Libraries.ToolControls.PMSChart
{
    internal class BindingEditor : UITypeEditor
    {
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            try
            {
                if (null != context && null != context.Instance && null != context.Container)
                {
                    ChartBase element = context.Instance as ChartBase;
                    if (null != element)
                    {
                        SourceField parent = GetSourceField(element);
                        using (SourceBindDialog fbd = new SourceBindDialog(parent, element.SourceField, true))
                        {
                            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                bool contain = false;
                                if (fbd.SourceField != null)
                                {
                                    FieldTreeViewData sfAll = (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetCurrentReportDataDefine()) as FieldTreeViewData;
                                    List<SourceField> lpdb = fbd.SourceField.GetSubSourceField(sfAll);
                                    foreach (SourceField pdb in lpdb)
                                    {
                                        try
                                        {
                                            if (!string.IsNullOrEmpty(pdb.DataType))
                                            {
                                                string typ = pdb.DataType.ToUpper();
                                                if (typ.Equals("INT", StringComparison.InvariantCultureIgnoreCase) ||
                                                typ.Equals("FLOAT", StringComparison.InvariantCultureIgnoreCase) ||
                                                typ.Equals("REAL", StringComparison.InvariantCultureIgnoreCase) ||
                                                typ.Equals("INT32", StringComparison.InvariantCultureIgnoreCase) ||
                                                typ.Equals("INT16", StringComparison.InvariantCultureIgnoreCase) ||
                                                typ.Equals("INT64", StringComparison.InvariantCultureIgnoreCase) ||
                                                typ.Equals("SYSTEM.SINGLE", StringComparison.InvariantCultureIgnoreCase) ||
                                                typ.Equals("SYSTEM.DOUBLE", StringComparison.InvariantCultureIgnoreCase) ||
                                                typ.Equals("SYSTEM.INT32", StringComparison.InvariantCultureIgnoreCase) ||
                                                typ.Equals("SYSTEM.DECIMAL", StringComparison.InvariantCultureIgnoreCase) ||
                                                typ.Equals("SYSTEM.DateTime", StringComparison.InvariantCultureIgnoreCase))
                                                {
                                                    contain = true;
                                                }
                                            }
                                        }
                                        catch
                                        {
                                            throw new Exception("lpdb");
                                        }
                                    }
                                    if (contain)
                                        value = fbd.SourceField;
                                    else
                                        MessageBox.Show("没有合适的数据集！");
                                }
                                else
                                    value = null;
                                IComponentChangeService cs = provider.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                                if (null != cs)
                                {
                                    cs.OnComponentChanged(this, null, null, null);
                                }
                            }
                        }
                    }
                    return value;
                }
                return base.EditValue(context, provider, value);
            }
            catch { throw new Exception("BindingEditor"); }
        }

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            if (null != context && null != context.Instance && null != context.Container)
            {

                return UITypeEditorEditStyle.Modal;
            }

            return base.GetEditStyle(context);
        }

        private SourceField GetSourceField(IElement element)
        {
            if (null == element)
            {
                return null;
            }
            IPmsReportDataBind parent = element.Parent as IPmsReportDataBind;
            if (null == parent)
            {
                return null;
            }
            if (null == parent.SourceField)
            {
                return GetSourceField(element.Parent as IElement);
            }

            return parent.SourceField;
        }
    }

}
