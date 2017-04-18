using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using PMS.Libraries.ToolControls.Report.Element;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using System.Windows.Forms;

namespace PMS.Libraries.ToolControls.Report.Controls.Editor
{
    public class ExpressionEditor:UITypeEditor
    {
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (null != context && null != context.Instance && null != context.Container)
            {
                IElement element = context.Instance as IElement;
                if (null != element)
                {
                    if (element is IExpression || element is IVisibleExpression)
                    {
                        SourceField sf = GetSourceField(element);
                        string ep = null;
                        if (element is IExpression)
                        {
                            ep = ((IExpression)element).Expression;
                        }
                        else
                        {
                            ep = ((IVisibleExpression)element).VisibleExpression;
                        }

                        using (MES.Report.MESReportExpressionEditor expEditor = new MES.Report.MESReportExpressionEditor(ep))
                        {
                            expEditor.ControlName = element.Name;
                            if (expEditor.ShowDialog() == DialogResult.OK)
                            {
                                return expEditor.ExpressionText;
                            }
                        }
                    }
                }
            }
            return base.EditValue(context, provider, value);
        }

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            if (null != context && null != context.Instance && null != context.Container)
            {

                Type type = context.Instance.GetType();
                if (null != type.GetInterface(typeof(IElement).FullName))
                {
                    return UITypeEditorEditStyle.Modal;
                }
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
                return GetSourceField(parent as IElement);
            }

            return parent.SourceField;
        }
    }
}
