using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.Drawing;
using PMS.Libraries.ToolControls.Report.Controls.EditorDialog;
using PMS.Libraries.ToolControls.Report.Element;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using MES.Controls.Design;
using System.ComponentModel.Design;

namespace PMS.Libraries.ToolControls.PMSChart
{
    /// <summary>
    /// 2011.12.8 修改
    /// 目的:为图表控件添加悬浮的工具栏(实现接口ISuspensionable的内容)
    /// </summary>
    [ToolboxBitmap(typeof(CurveChart), "Resources.CurveChart.png")]
    [Designer(typeof(MES.Controls.Design.MESDesigner))]
    public class CurveChart : PMSChartCtrl, IChartProperty,ISuspensionable
    {
        private int _Distance = 10;
        public CurveChart()
            : base()
        {
            base.ChartType = 1;
            base.IsReport = true;
        }
        #region 2011年10月24日 屏蔽基类中在此派生类中不需要的属性
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
        [Category("MES报表属性")]
        [Description("设置多轴间距")]
        [DefaultValue(10)]
        public int Distance
        {
            get
            {
                return _Distance;
            }
            set
            {
                if (value > 0)
                {
                    _Distance = value;
                }
                else
                {
                    throw new NotImplementedException(Properties.Resources.ResourceManager.GetString("message0007"));
                }
            }
        }
        #endregion

        #region  2011.12.8 增加 实现悬浮工具栏效果
        public virtual SuspensionItem[] ListSuspensionItems()
        {
            SuspensionItem[] result = new SuspensionItem[2];
            result[0] = new SuspensionItem(Properties.Resources.Data, Properties.Resources.ResourceManager.GetString("context0021"), Properties.Resources.ResourceManager.GetString("context0021"), new Action(DealWithDataTable));
            result[1] = new SuspensionItem(Properties.Resources.OPEN, Properties.Resources.ResourceManager.GetString("context0022"), Properties.Resources.ResourceManager.GetString("context0022"), new Action(DealWithApperence));
            return result;
        }
        private void DealWithDataTable()
        {
            if (null != this)
            {
                PMSChartCtrl element = this as PMSChartCtrl;
                if (null != element)
                {
                    if (element.Parent == null ||
                       (element.Parent != null && element.Parent as IPmsReportDataBind == null))
                    {
                        PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField sfAll = (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetCurrentReportDataDefine()) as PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField;
                        //SourceBindDialog fbd = new SourceBindDialog(sfAll, element.SourceField);
                        PMS.Libraries.ToolControls.Report.Controls.EditorDialog.SourceBindDialog fbd = new PMS.Libraries.ToolControls.Report.Controls.EditorDialog.SourceBindDialog(sfAll, element.SourceField, true);
                        if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            DataTable = fbd.SourceField;
                            if (null != Site)
                            {
                                IComponentChangeService cs = Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                                if (null != cs)
                                {
                                    cs.OnComponentChanged(this, null, null, null);
                                }
                            }
                        }
                    }
                    else
                    {
                        PMS.Libraries.ToolControls.Report.Controls.EditorDialog.SourceBindDialog fbd = new PMS.Libraries.ToolControls.Report.Controls.EditorDialog.SourceBindDialog(GetSourceField(element.Parent as IElement), element.SourceField, true);
                        if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            DataTable = fbd.SourceField;
                            if (null != Site)
                            {
                                IComponentChangeService cs = Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                                if (null != cs)
                                {
                                    cs.OnComponentChanged(this, null, null, null);
                                }
                            }
                        }
                    }
                }
            }
        }
        private void DealWithApperence()
        {

            if (this != null)
            {
                CurveChart control = null;
                if (this is CurveChart)
                    control = this as CurveChart;

                CurveApperence form1 = new CurveApperence();
                form1.IsReport = control.IsReport;
                form1.ChartParent = control as PMSChartCtrl;
                DataSource ds = control.Apperence.Clone();
                form1.SqlSource = control.Apperence;
                form1.Distance = control.Distance;
                if (DialogResult.OK == form1.ShowDialog())
                {
                    //2012.04.25 注释 属性异常点不对,在于快捷方式没有使用Clone方法,正常属性没问题
                    //李琦 4.23 添加
                    //System.Windows.Forms.DataVisualization.Charting.ChartArea ChartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
                    //control.Apperence.ChartAreaList[0].SetChartArea(ChartArea);
                    //PMSChartArea pca = new PMSChartArea(ChartArea);
                    //form1.SqlSource.ChartAreaList[0] = pca;
                    //


                    Apperence = form1.SqlSource.Clone();
                    control.SelectRecordFields = form1.SqlSource.YAixs;
                    control.XRecordField = form1.XAixs;
                    if (null != Site)
                    {
                        IComponentChangeService cs = Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                        if (null != cs)
                        {
                            cs.OnComponentChanged(this, null, null, null);
                        }
                    }
                }
                else
                    Apperence = ds;
            }
        }
        private SourceField GetSourceField(IElement element)
        {
            if (null == element)
            {
                return null;
            }
            IPmsReportDataBind parent = element as IPmsReportDataBind;
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
        #endregion

        #region 2011.10.24 属性编辑器
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
                        CurveChart control = null;
                        if (context.Instance is CurveChart)
                            control = context.Instance as CurveChart;

                        CurveApperence form1 = new CurveApperence();
                        form1.IsReport = control.IsReport;
                        form1.ChartParent = control as PMSChartCtrl;
                        DataSource ds = control.Apperence.Clone();
                        form1.SqlSource = control.Apperence;
                        form1.Distance = control.Distance;
                        if (DialogResult.OK == editorService.ShowDialog(form1))
                        {
                            //2012.04.25 注释 属性异常点不对,在于快捷方式没有使用Clone方法,正常属性没问题
                            ////李琦 4.23 添加
                            //System.Windows.Forms.DataVisualization.Charting.ChartArea ChartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
                            //control.Apperence.ChartAreaList[0].SetChartArea(ChartArea);
                            //PMSChartArea pca = new PMSChartArea(ChartArea);
                            //form1.SqlSource.ChartAreaList[0] = pca;
                            ////

                            value = form1.SqlSource.Clone();
                            control.SelectRecordFields = form1.SqlSource.YAixs;
                            control.XRecordField = form1.XAixs;
                        }
                        else
                            value = ds;
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
                        if (element.Parent == null ||
                           (element.Parent != null && element.Parent as IPmsReportDataBind == null))
                        {
                            PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField sfAll = (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetCurrentReportDataDefine()) as PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField;
                            //SourceBindDialog fbd = new SourceBindDialog(sfAll, element.SourceField);
                            PMS.Libraries.ToolControls.Report.Controls.EditorDialog.SourceBindDialog fbd = new PMS.Libraries.ToolControls.Report.Controls.EditorDialog.SourceBindDialog(sfAll, element.SourceField, true);
                            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                value = fbd.SourceField;
                            }
                        }
                        else
                        {
                            PMS.Libraries.ToolControls.Report.Controls.EditorDialog.SourceBindDialog fbd = new PMS.Libraries.ToolControls.Report.Controls.EditorDialog.SourceBindDialog(GetSourceField(element.Parent as IElement), element.SourceField, true);
                            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                value = fbd.SourceField;
                            }
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
            private SourceField GetSourceField(IElement element)
            {
                if (null == element)
                {
                    return null;
                }
                IPmsReportDataBind parent = element as IPmsReportDataBind;
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
        #endregion
    }
}
