using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using System.ComponentModel;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace PMS.Libraries.ToolControls.PMSChart
{
    public class SectionChartProperties
    {
        upperLimit upperLimit;
        [Bindable(true)]
        [Category("分段曲线属性")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("获取或设置上限标记相关的属性")]
        [TypeConverter(typeof(StringPropertiesConverter))]
        public upperLimit UpperLimit 
        {
            get 
            {
                if (upperLimit == null)
                {
                    upperLimit = new upperLimit();
                }
                return upperLimit;
            }
        }

        lowerLimit lowerLimit;
        [Bindable(true)]
        [Category("分段曲线属性")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("获取或设置下限标记相关的属性")]
        [TypeConverter(typeof(StringPropertiesConverter))]
        public lowerLimit LowerLimit
        {
            get
            {
                if (lowerLimit == null)
                {
                    lowerLimit = new lowerLimit();
                }
                return lowerLimit;
            }
        }

        startAnnotation startAnnotation;
        [Bindable(true)]
        [Category("分段曲线属性")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("获取或设置分段起始标记相关的属性")]
        [TypeConverter(typeof(StringPropertiesConverter))]
        public startAnnotation StartAnnotation 
        {
            get 
            {
                if (startAnnotation == null) 
                {
                    startAnnotation = new startAnnotation();
                }
                return startAnnotation;
            }
        }

        endAnnotation endAnnotation;
        [Bindable(true)]
        [Category("分段曲线属性")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("获取或设置分段终结标记相关的属性")]
        [TypeConverter(typeof(StringPropertiesConverter))]
        public endAnnotation EndAnnotation
        {
            get
            {
                if (endAnnotation == null)
                {
                    endAnnotation = new endAnnotation();
                }
                return endAnnotation;
            }
        }


    }

    [Serializable]
    public class upperLimit 
    {
        double limit;
        [Description("获取或设置上限的值")]
        public double Limit
        {
            get { return limit; }
            set { limit = value; }
        }

        bool enable;
        [Description("是否启用")]
        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }

        int width;
        [Description("获取或设置上限标记的线宽")]
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        Color color;
        [Description("获取或设置上限标记的颜色")]
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        ChartDashStyle style;
        [Description("获取或设置上限标记的显示模式")]
        public ChartDashStyle Style
        {
            get { return style; }
            set { style = value; }
        }
    }

    [Serializable]
    public class lowerLimit
    {
        double limit;
        [Description("获取或设置下限的值")]
        public double Limit
        {
            get { return limit; }
            set { limit = value; }
        }

        bool enable;
        [Description("是否启用")]
        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }

        int width;
        [Description("获取或设置下限标记的线宽")]
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        Color color;
        [Description("获取或设置下限标记的颜色")]
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        ChartDashStyle style;
        [Description("获取或设置下限标记的显示模式")]
        public ChartDashStyle Style
        {
            get { return style; }
            set { style = value; }
        }
    }

    [Serializable]
    public class sectionLimit
    {
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        double limit;

        [Description("获取或设置警戒的值")]
        public double Limit
        {
            get { return limit; }
            set { limit = value; }
        }

        bool enable;
        [Description("是否启用")]
        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }

        int width;
        [DefaultValue(1)]
        [Description("获取或设置警戒线的线宽")]
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        Color color;
        [Description("获取或设置警戒线的颜色")]
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        ChartDashStyle style;
        [Description("获取或设置警戒线的显示模式")]
        public ChartDashStyle Style
        {
            get { return style; }
            set { style = value; }
        }
    }

    [Serializable]
    public class startAnnotation
    {
        bool enable;
        [Description("是否启用")]
        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }

        int width =1;
        [Description("获取或设置分段起始标记的线宽")]
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        Color color;
        [Description("获取或设置分段起始标记的颜色")]
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        LineAnchorCapStyle startStyle;
        [Description("获取或设置分段起始标记的起点模式")]
        public LineAnchorCapStyle StartStyle
        {
            get { return startStyle; }
            set { startStyle = value; }
        }

        LineAnchorCapStyle endStyle;
        [Description("获取或设置分段起始标记的终结模式")]
        public LineAnchorCapStyle EndStyle
        {
            get { return endStyle; }
            set { endStyle = value; }
        }

        ChartDashStyle lineDashStyle;
        [Description("获取或设置分段起始标记的显示模式")]
        public ChartDashStyle LineDashStyle
        {
            get { return lineDashStyle; }
            set { lineDashStyle = value; }
        }
    }

    [Serializable]
    public class endAnnotation
    {
        bool enable;
        [Description("是否启用")]
        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }

        int width = 1;
        [Description("获取或设置分段结束标记的线宽")]
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        Color color;
        [Description("获取或设置分段结束标记的颜色")]
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        LineAnchorCapStyle startStyle;
        [Description("获取或设置分段结束标记的起点模式")]
        public LineAnchorCapStyle StartStyle
        {
            get { return startStyle; }
            set { startStyle = value; }
        }

        LineAnchorCapStyle endStyle;
        [Description("获取或设置分段结束标记的终结模式")]
        public LineAnchorCapStyle EndStyle
        {
            get { return endStyle; }
            set { endStyle = value; }
        }

        ChartDashStyle lineDashStyle;
        [Description("获取或设置分段结束标记的显示模式")]
        public ChartDashStyle LineDashStyle
        {
            get { return lineDashStyle; }
            set { lineDashStyle = value; }
        }
    }

    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class backSeries : PMSSeries
    {
        public backSeries(Series Aim)
            : base(Aim)
        {
        }
        //public enum enumSectionChartType { Line, Spline, Stepline, Fastline, Area };
        SectionSeries.enumSectionChartType sectionChartType;
        [Category("MES报表属性")]
        [DefaultValue(SectionSeries.enumSectionChartType.Line)]
        [Description("获取或设置分段曲线的图表类型")]
        public SectionSeries.enumSectionChartType SectionChartType
        {
            get { return sectionChartType; }
            set { sectionChartType = value; }
        }

        /// <summary>
        /// 在特殊图表控件中禁用图表类型属性
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override SeriesChartType ChartType
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
        public new string LegendToolTip { get; set; }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new string LegendText { get; set; }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new bool IsVisibleInLegend { get; set; }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new AxisType XAxisType { get; set; }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new AxisType YAxisType { get; set; }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new string Name { get; set; }
    }

    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class SectionSeries:PMSSeries 
    {
        public SectionSeries(Series Aim)
            : base(Aim)
        {
        }
        public enum enumSectionChartType {Line ,Spline ,Stepline ,Fastline ,Area};
        enumSectionChartType sectionChartType;
        [Category("MES报表属性")]
        [DefaultValue(enumSectionChartType.Line)]
        [Description("获取或设置分段曲线的图表类型")]
        public enumSectionChartType SectionChartType
        {
            get { return sectionChartType; }
            set { sectionChartType = value; }
        }

        string format;
        [Category("MES报表属性")]
        //[Browsable(false)]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        [Description("获取或设置分段曲线的X轴标签的格式化信息")]
        public string Format
        {
            get { return format; }
            set { format = value; }
        }

        sectionClass.enumLabelStyle labelStyle;
        [Category("MES报表属性")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Description("获取或设置分段曲线的X轴标签自适应模式")]
        public sectionClass.enumLabelStyle LabelStyle
        {
            get { return labelStyle; }
            set { labelStyle = value; }
        }

        int pointsCount;
        [Description("分页设置")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Category("MES报表属性")]
        public int PointsCount
        {
            get { return pointsCount; }
            set { pointsCount = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override SeriesChartType ChartType { get; set; }

        public enum enumSortWay { Ascending, Descending };
        enumSortWay sortWay;
        [Category("MES报表属性")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Description("获取或设置数据的按时间字段排序方式")]
        public enumSortWay SortWay
        {
            get { return sortWay; }
            set { sortWay = value; }
        }

        double distance;
        [Category("MES报表属性")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Description("获取或设置分段曲线的时间间隔")]
        public double Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        public enum enumTimeType { second, minute, hour };
        enumTimeType timeType;
        [Category("MES报表属性")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Description("获取或设置分段曲线的时间间隔单位")]
        public enumTimeType TimeType
        {
            get { return timeType; }
            set { timeType = value; }
        }

        private string _SectionField = "";
        [Description("分段字段")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Category("MES报表属性")]
        [Editor(typeof(SectionEditor), typeof(UITypeEditor))]
        public string SectionField
        {
            get { return _SectionField; }
            set { _SectionField = value; }
        }

        private string _BindingField = "";
        [Description("绑定字段")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Category("MES报表属性")]
        [Editor(typeof(SectionFieldEditor), typeof(UITypeEditor))]
        public virtual string BindingField
        {
            get
            {
                return _BindingField;
            }
            set
            {
                _BindingField = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public SourceField SourceField
        {
            get;
            set;
        }

        MaxandMinmum axisMum;
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public MaxandMinmum AxisMum
        {
            get
            {
                if (axisMum == null)
                {
                    axisMum = new MaxandMinmum();
                }
                return axisMum;
            }
            set { axisMum = value; }
        }

        private double limit;
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public double Limit 
        {
            get
            {
                return limit;
            }
            set
            {
                limit = value;
            }
        }
    }

    public class StringPropertiesConverter : ExpandableObjectConverter
    {
        //public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        //{
        //    if (destinationType == typeof(string))
        //    {
        //        return true;
        //    }
        //    return base.CanConvertTo(context, destinationType);
        //}

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return "";
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    class SectionFieldEditor : UITypeEditor
    {
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService editorService = null;
            if (null != context && null != context.Instance)
            {
                sectionClass pcc = context.Instance as sectionClass;
                if (null != pcc && null != pcc.SourceField)
                {
                    editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                    if (editorService != null)
                    {
                        FieldTreeViewData sfAll = (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetCurrentReportDataDefine()) as FieldTreeViewData;
                        RelationFieldChoose rfc = new RelationFieldChoose(editorService);

                        rfc.strRField = pcc.BindingField;
                        List<PmsField> lp = new List<PmsField>();

                        List<SourceField> lpdb = pcc.SourceField.GetSubSourceField(sfAll);
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
                                        typ.Equals("SYSTEM.DECIMAL", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        PmsField pf = new PmsField();
                                        pf.fieldName = pdb.RecordField;
                                        pf.fieldDescription = pdb.Name;
                                        lp.Add(pf);
                                    }

                                }
                            }
                            catch
                            {
                            }
                        }
                        rfc.pmsFieldList = lp;
                        editorService.DropDownControl(rfc);
                        if (!string.IsNullOrEmpty(rfc.strRField))
                        {
                            value = rfc.strRField;
                        }
                        return value;
                    }
                }
            }
            return base.EditValue(context, provider, value);
        }

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            if (null != context && null != context.Instance)
            {

                return UITypeEditorEditStyle.DropDown;
            }

            return base.GetEditStyle(context);
        }
    }

    class SectionEditor : UITypeEditor
    {
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService editorService = null;
            if (null != context && null != context.Instance)
            {
                if (context.Instance is Section)
                {
                    Section pcc = context.Instance as Section;
                    if (null != pcc && null != pcc.SourceField)
                    {
                        editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                        if (editorService != null)
                        {
                            FieldTreeViewData sfAll = (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetCurrentReportDataDefine()) as FieldTreeViewData;
                            RelationFieldChoose rfc = new RelationFieldChoose(editorService);

                            rfc.strRField = pcc.BindingField;
                            List<PmsField> lp = new List<PmsField>();

                            List<SourceField> lpdb = pcc.SourceField.GetSubSourceField(sfAll);
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
                                            typ.Equals("SYSTEM.DECIMAL", StringComparison.InvariantCultureIgnoreCase) )
                                        {
                                            PmsField pf = new PmsField();
                                            pf.fieldName = pdb.RecordField;
                                            pf.fieldDescription = pdb.Name;
                                            lp.Add(pf);
                                        }

                                    }
                                }
                                catch
                                {
                                }
                            }
                            rfc.pmsFieldList = lp;
                            editorService.DropDownControl(rfc);
                            if (!string.IsNullOrEmpty(rfc.strRField))
                            {
                                value = rfc.strRField;
                            }
                            return value;
                        }
                    }
                } 
                else if (context.Instance is sectionClass)
                {
                    sectionClass pcc = context.Instance as sectionClass;
                    if (null != pcc && null != pcc.SourceField)
                    {
                        editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                        if (editorService != null)
                        {
                            FieldTreeViewData sfAll = (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetCurrentReportDataDefine()) as FieldTreeViewData;
                            RelationFieldChoose rfc = new RelationFieldChoose(editorService);

                            rfc.strRField = pcc.BindingField;
                            List<PmsField> lp = new List<PmsField>();

                            List<SourceField> lpdb = pcc.SourceField.GetSubSourceField(sfAll);
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
                                            typ.Equals("SYSTEM.DATETIME", StringComparison.InvariantCultureIgnoreCase))
                                        {
                                            PmsField pf = new PmsField();
                                            pf.fieldName = pdb.RecordField;
                                            pf.fieldDescription = pdb.Name;
                                            lp.Add(pf);
                                        }

                                    }
                                }
                                catch
                                {
                                }
                            }
                            rfc.pmsFieldList = lp;
                            editorService.DropDownControl(rfc);
                            if (!string.IsNullOrEmpty(rfc.strRField))
                            {
                                value = rfc.strRField;
                            }
                            return value;
                        }
                    }
                }
            }
            return base.EditValue(context, provider, value);
        }

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            if (null != context && null != context.Instance)
            {

                return UITypeEditorEditStyle.DropDown;
            }

            return base.GetEditStyle(context);
        }
    }
}
