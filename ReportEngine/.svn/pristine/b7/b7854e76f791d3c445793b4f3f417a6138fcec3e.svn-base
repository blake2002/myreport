using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.Drawing;

namespace PMS.Libraries.ToolControls.PMSChart
{
    [ToolboxBitmap(typeof(RadarChart), "Resources.RadarChart.png")]
    public partial class RadarChart : PMSChartCtrl, IChartProperty
    {
        public RadarChart()
            : base()
        {
            base.ChartType = 2;
            base.IsReport = true;
        }
        #region 2011年10月24日 屏蔽基类中在此派生类中不需要的属性
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int ChartType
        {
            get
            {
                return base.ChartType;
            }
            set
            {
                base.ChartType = value;
            } 
        }


        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override PmsSheet.PmsPublicData.SourceField SourceField
        {
            get
            {
                return base.SourceField;
            }
            set
            {
                base.SourceField = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool IsReport
        {
            get
            {
                return base.IsReport;
            }
            set
            {
                base.IsReport = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override List<string> SelectRecordFields
        {
            get
            {
                return base.SelectRecordFields;
            }
            set
            {
                base.SelectRecordFields = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override List<string> AllRecordFields
        {
            get
            {
                return base.AllRecordFields;
            }
            set
            {
                base.AllRecordFields = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string XRecordField
        {
            get
            {
                return base.XRecordField;
            }
            set
            {
                base.XRecordField = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override System.Data.DataTable ReportData
        {
            get
            {
                return base.ReportData;
            }
            set
            {
                base.ReportData = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override DataSource SqlSource
        {
            get
            {
                return base.SqlSource;
            }
            set
            {
                base.SqlSource = value;
            }
        }
        #endregion


        #region 2011年10月24日 派生类对外公开的属性
        [Category("MES报表属性")]
        [Description("设置数据表")]
        [Editor(typeof(DataTableEditor), typeof(UITypeEditor))]
        public PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField DataTable
        {
            get
            {
                return base.SourceField;
            }
            set
            {
                base.SourceField = value;
            }
        }
        [Category("MES报表属性")]
        [Description("外观设置")]
        [Editor(typeof(ApperenceEditor), typeof(UITypeEditor))]
        public DataSource Apperence
        {
            get
            {
                return base.SqlSource;
            }
            set
            {
                base.SqlSource = value;
            }
        }
        [Category("MES报表属性")]
        public override PmsPowerGroupCheck.ActionInfo ActionConfig
        {
            get
            {
                return base.ActionConfig;
            }
            set
            {
                base.ActionConfig = value;
            }
        }
        #endregion

        #region 2011.11.29 属性编辑器：对应雷达图
        internal class ApperenceEditor : UITypeEditor
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
                        RadarChart control = null;
                        if (context.Instance is RadarChart)
                            control = context.Instance as RadarChart;

                        RadarApperence form1 = new RadarApperence();
                        form1.IsReport = control.IsReport;
                        form1.ChartParent = control as PMSChartCtrl;
                        DataSource ds = control.Apperence.Clone();
                        form1.SqlSource = ds;
                        if (DialogResult.OK == editorService.ShowDialog(form1))
                        {
                            value = form1.SqlSource;
                            control.SelectRecordFields = form1.SqlSource.YAixs;
                            control.XRecordField = form1.XAixs;
                        }
                        return value;
                    }
                }

                return value;
            }
        }
        internal class DataTableEditor : UITypeEditor
        {
            public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                if (null != context && null != context.Instance && null != context.Container)
                {
                    PMSChartCtrl element = context.Instance as PMSChartCtrl;
                    if (null != element)
                    {
                        PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField sfAll = (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetCurrentReportDataDefine()) as PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField;
                        //SourceBindDialog fbd = new SourceBindDialog(sfAll, element.SourceField);
                        PMS.Libraries.ToolControls.Report.Controls.EditorDialog.SourceBindDialog fbd = new PMS.Libraries.ToolControls.Report.Controls.EditorDialog.SourceBindDialog(sfAll, element.SourceField, true);
                        if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            value = fbd.SourceField;
                        }
                    }
                    return value;
                }
                return base.EditValue(context, provider, value);
            }

            public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
            {
                if (null != context && null != context.Instance && null != context.Container)
                {

                    return UITypeEditorEditStyle.Modal;
                }

                return base.GetEditStyle(context);
            }
        }
        #endregion
    }
}
