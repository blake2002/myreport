using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using PMS.Libraries.ToolControls.Report.Element;
using PMS.Libraries.ToolControls.Report.Controls.EditorDialog;

namespace PMS.Libraries.ToolControls.Report.Controls.Editor
{
    public class EditTextEditor : UITypeEditor
    {
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (null != context && null != context.Instance && null != context.Container)
            {
                if (null != context.Instance && context.Instance is PmsEdit)
                {
                    EditTextBindDialog fbd = new EditTextBindDialog();
                    if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        return fbd.Var;
                    }
                }
            }
            return base.EditValue(context, provider, value);
        }

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            if (null != context && null != context.Instance && null != context.Container)
            {
                if (context.Instance is PmsEdit)
                {

                   return UITypeEditorEditStyle.Modal;
                }
            }

            return base.GetEditStyle(context);
        }

        
    }
}
