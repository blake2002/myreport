using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using PMS.Libraries.ToolControls.Report.Element;
using System.Windows.Forms;
using PMS.Libraries.ToolControls.Report.Controls.EditorDialog;
using PMS.Libraries.ToolControls.Report.Elements.Util;
using System.Windows.Forms.Design;

namespace PMS.Libraries.ToolControls.Report.Controls.Editor
{
    public class DataMappingEditor : UITypeEditor
    {
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (null != context && null != context.Instance && null != context.Container)
            {
                if (null != context.Instance)
                {
                    IWindowsFormsEditorService ws = provider.GetService(typeof(IWindowsFormsEditorService)) 
                                                    as IWindowsFormsEditorService;
                    if (null != ws)
                    { 
                        MappingTableEditorPanel mappingPanel = new MappingTableEditorPanel();
                        string selectValue = null;
                        mappingPanel.OnSelectValue += (o, e) => {
                            ws.CloseDropDown();
                            selectValue = e.SelectValue;
                        };
                        ws.DropDownControl(mappingPanel);
                        return selectValue;
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
                    return UITypeEditorEditStyle.DropDown;
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
