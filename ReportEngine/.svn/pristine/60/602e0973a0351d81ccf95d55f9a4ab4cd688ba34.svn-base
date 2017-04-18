using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using PMS.Libraries.ToolControls.Report.Element;
using System.Windows.Forms;
using PMS.Libraries.ToolControls.Report.Controls.EditorDialog;
using PMS.Libraries.ToolControls.Report.Elements.Util;

namespace PMS.Libraries.ToolControls.Report.Controls.Editor
{
    public class BorderEditor : UITypeEditor
    {
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (null != context && null != context.Instance && null != context.Container)
            {
                if (null != context.Instance)
                {
                    IElement element = context.Instance as IElement;
                    if (null != element)
                    {

                        ElementBorder border = value as ElementBorder;
                        BorderEditorDialog editor = new BorderEditorDialog(border);
                        if (DialogResult.OK == editor.ShowDialog())
                        {
                            if (null != border && border.Equals(editor.Border))
                            {
                                return editor.Border;
                            }
                            editor.Border.OwnerElement = context.Instance as IElement;
                            element.BorderName = editor.Border.Name;
                            ExternData data = new ExternData("BorderName", editor.Border.Name);
                            int index = -1;
                            if (null != element.ExternDatas)
                            {
                                for (int i = 0; i < element.ExternDatas.Count; i++)
                                {
                                    if (data.Equals(element.ExternDatas[i]))
                                    {
                                        element.ExternDatas[i] = data;
                                        index = i;
                                        break;
                                    }
                                }
                                if (index == -1)
                                {
                                    element.ExternDatas.Add(data);
                                }
                            }
                            return editor.Border;
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

        public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return base.GetPaintValueSupported(context);
        }


    }
}
