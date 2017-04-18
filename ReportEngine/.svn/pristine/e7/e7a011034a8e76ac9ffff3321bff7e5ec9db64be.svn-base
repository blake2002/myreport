using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.Drawing;
using ControlLib.EditorDialog;
using ControlLib.MarkupAnnotation;

namespace ControlLib.Editor
{
    public class TestEditor:UITypeEditor
    {
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (null != context && null != context.Container && null != context.Instance)
            {
                //if (context.Instance is Line || context.Instance is LineActionList)
                //{
                //    TestFileDialog td = new TestFileDialog((Point)value);
                //    if (td.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //    {
                //        return td.EditPoint;
                //    }
                //}
            }

            return base.EditValue(context, provider, value);
        }

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            //if (null != context && null != context.Container && null != context.Instance)
            //{
            //    if (context.Instance is Line || context.Instance is LineActionList)
            //    {
            //        return UITypeEditorEditStyle.Modal;
            //    }
                
            //}
            return base.GetEditStyle(context);
        }
    }
}
