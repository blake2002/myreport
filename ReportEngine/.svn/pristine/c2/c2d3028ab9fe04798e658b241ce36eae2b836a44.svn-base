using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms.Design;
using PMS.Libraries.ToolControls.PMSChart;
using System.Windows.Forms;

namespace PMS.Libraries.ToolControls.PMSChart
{
    internal class PieApperenceEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context != null && context.Instance != null)
            {
                return UITypeEditorEditStyle.Modal;
            }

            return base.GetEditStyle(context);
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService editorService = null;

            if (context != null && context.Instance != null && provider != null)
            {
                editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (editorService != null)
                {
                    PieChart control = null;
                    if (context.Instance is PieChart)
                        control = context.Instance as PieChart;

                    PieApperenceFrm form1 = new PieApperenceFrm();

                    form1.ChartParent = control as PieChart;
                    DataSource ds = control.Apperence.Clone();
                    form1.PMSChartAppearance = ds.PMSChartAppearance;
                    form1.chartAreaList = ds.ChartAreaList;
                    form1.legendList = ds.LegendList;
                    form1.seriesList = ds.SeriesList;
                    form1.titleList = ds.TitleList;
                    DialogResult dr = editorService.ShowDialog(form1);
                    if (DialogResult.OK == dr)
                    {
                        value = ds;
                        control.isIntial = true;
                    }
                    return value;
                }
            }
            return value;
        }
    }
}
