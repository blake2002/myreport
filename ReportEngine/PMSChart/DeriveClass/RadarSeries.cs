using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;


namespace PMS.Libraries.ToolControls.PMSChart
{

    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class RadarSeries : PMSSeries
    {
        public RadarSeries(Series Aim)
            : base(Aim)
        {
        }
        /// <summary>
        /// 在特殊图表控件中禁用图表类型属性
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override SeriesChartType ChartType {
            get
            {
                return SeriesChartType.Radar;
            }
            set 
            {
                base.ChartType = SeriesChartType.Radar;
            }
        }

        [Browsable(false)]
        public override string CustomProperties
        {
            get
            {
                return this.rp.CustomProperties;
            }
        }

        private string _BindingField = "";

        [Description("绑定字段")]
        [Category("MES报表属性")]
        [Editor(typeof(RadarSeriesEditor), typeof(UITypeEditor))]
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
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public List<string> seriesDataForAppearance
        {
            get;
            set;
        }

        RadarProperties rp;
        [Bindable(true)]
        [Category("MES报表属性")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("获取或设置雷达图相关的属性")]
        [TypeConverter(typeof(RadarPropertiesConverter))]
        public virtual RadarProperties RadarProperties
        {
            get
            {
                if (rp == null)
                {
                    rp = new RadarProperties();
                }
                if (rp.CustomProperties == "") 
                {
                    rp.CustomProperties = "AreaDrawingStyle=" + rp.RadarAreaDrawingStyle.ToString() + ",CircularLabelsStyle=" + rp.RadarCircularLabelsStyle.ToString() + ",RadarDrawingStyle=" + rp.RadarDrawingStyle.ToString();
                }
                return rp;
            }

        }

        #region 属性配置器
        class RadarSeriesEditor : UITypeEditor
        {
            public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                IWindowsFormsEditorService editorService = null;
                if (null != context && null != context.Instance)
                {
                    RadarSeries pcc = context.Instance as RadarSeries;
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
                                            if (pcc.seriesDataForAppearance != null && !pcc.seriesDataForAppearance.Contains(pdb.RecordField))
                                            {
                                                PmsField pf = new PmsField();
                                                pf.fieldName = pdb.RecordField;
                                                pf.fieldDescription = pdb.Name;
                                                lp.Add(pf);
                                            }
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
        #endregion
    }

    [Serializable]
    public class RadarProperties
    {
        [Browsable(false)]
        public string CustomProperties
        {
            set 
            {
                if (value != "")
                {
                    string[] raProperties = value.Split(',');
                    string a = raProperties[0].Substring(18);
                    string b = raProperties[1].Substring(17);
                    string c = raProperties[2].Substring(20);
                    if (raProperties[0].Substring(18) == RadarProperties.enumRadarDrawingStyle.Area.ToString())
                        this.RadarDrawingStyle = RadarProperties.enumRadarDrawingStyle.Area;
                    if (raProperties[0].Substring(18) == RadarProperties.enumRadarDrawingStyle.Line.ToString())
                        this.RadarDrawingStyle = RadarProperties.enumRadarDrawingStyle.Line;
                    if (raProperties[0].Substring(18) == RadarProperties.enumRadarDrawingStyle.Marker.ToString())
                        this.RadarDrawingStyle = RadarProperties.enumRadarDrawingStyle.Marker;
                    if (raProperties[1].Substring(18) == RadarProperties.enumRadarAreaDrawingStyle.Circle.ToString())
                        this.RadarAreaDrawingStyle = RadarProperties.enumRadarAreaDrawingStyle.Circle;
                    if (raProperties[1].Substring(18) == RadarProperties.enumRadarAreaDrawingStyle.Polygon.ToString())
                        this.RadarAreaDrawingStyle = RadarProperties.enumRadarAreaDrawingStyle.Polygon;
                    if (raProperties[2].Substring(21) == RadarProperties.enumRadarCircularLabelsStyle.Auto.ToString())
                        this.RadarCircularLabelsStyle = RadarProperties.enumRadarCircularLabelsStyle.Auto;
                    if (raProperties[2].Substring(21) == RadarProperties.enumRadarCircularLabelsStyle.Circular.ToString())
                        this.RadarCircularLabelsStyle = RadarProperties.enumRadarCircularLabelsStyle.Circular;
                    if (raProperties[2].Substring(21) == RadarProperties.enumRadarCircularLabelsStyle.Horizontal.ToString())
                        this.RadarCircularLabelsStyle = RadarProperties.enumRadarCircularLabelsStyle.Horizontal;
                    if (raProperties[2].Substring(21) == RadarProperties.enumRadarCircularLabelsStyle.Radial.ToString())
                        this.RadarCircularLabelsStyle = RadarProperties.enumRadarCircularLabelsStyle.Radial;

                }


            }
            get
            {
                return "AreaDrawingStyle=" + radarAreaDrawingStyle.ToString() + ",CircularLabelsStyle=" + radarCircularLabelsStyle.ToString() +  ",RadarDrawingStyle=" + radarDrawingStyle.ToString();
            }
        }

        public enum enumRadarAreaDrawingStyle { Circle, Polygon };
        enumRadarAreaDrawingStyle radarAreaDrawingStyle;
        /// <summary>
        /// 获取或设置雷达图的图标区形状
        /// </summary>
        [Description("获取或设置雷达图的图标区形状")]
        public virtual enumRadarAreaDrawingStyle RadarAreaDrawingStyle
        {
            get { return radarAreaDrawingStyle; }
            set { radarAreaDrawingStyle = value; }
        }


        public enum enumRadarCircularLabelsStyle { Auto, Circular, Horizontal, Radial };
        enumRadarCircularLabelsStyle radarCircularLabelsStyle;
        /// <summary>
        /// 获取或设置雷达图的标签绘制样式
        /// </summary>
        [Description("获取或设置雷达图的标签绘制样式")]
        public virtual enumRadarCircularLabelsStyle RadarCircularLabelsStyle
        {
            get { return radarCircularLabelsStyle; }
            set { radarCircularLabelsStyle = value; }
        }

        public enum enumRadarEmptyPointValue { Average, Zero };
        //enumRadarEmptyPointValue radarEmptyPointValue;
        /// <summary>
        /// 确定在绘制图表时如何处理空点
        /// </summary>
        //[Description("确定在绘制图表时如何处理空点")]
        //public enumRadarEmptyPointValue RadarEmptyPointValue
        //{
        //    get { return radarEmptyPointValue; }
        //    set { radarEmptyPointValue = value; }
        //}

        public enum enumRadarLableStyle { Auto, Top, Bottom, Right, Left, TopLeft, TopRight, BottomLeft, BottomRight, Center };
        //enumRadarLableStyle radarLableStyle;
        /// <summary>
        /// 获取或设置数据点标签位置
        /// </summary>
        //[Description("获取或设置数据点标签位置")]
        //public enumRadarLableStyle RadarLableStyle
        //{
        //    get { return radarLableStyle; }
        //    set { radarLableStyle = value; }
        //}

        public enum enumRadarDrawingStyle { Area, Line, Marker };
        enumRadarDrawingStyle radarDrawingStyle;
        /// <summary>
        /// 获取或设置雷达图的绘制样式
        /// </summary>
        [Description("获取或设置雷达图的绘制样式")]
        public virtual enumRadarDrawingStyle RadarDrawingStyle
        {
            get { return radarDrawingStyle; }
            set { radarDrawingStyle = value; }
        }
        
    }
    /// <summary>
    /// 自定义转换器：在父属性中显示所有子属性的属性值集合
    /// </summary>
    public class RadarPropertiesConverter : ExpandableObjectConverter
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
                if (context.Instance is RadarSeries)
                {
                    RadarSeries control = null;
                    control = context.Instance as RadarSeries;
                    return control.RadarProperties.CustomProperties;
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    /// <summary>
    /// 自定义转换器：在父属性中显示所有子属性的属性值集合
    /// </summary>
    public class RadarAlertPropertiesConverter : ExpandableObjectConverter
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
                if (context.Instance is RadarAlertSeries)
                {
                    RadarAlertSeries control = null;
                    control = context.Instance as RadarAlertSeries;
                    return control.RadarAlertProperties.CustomProperties;
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    /// <summary>
    /// 李琦 12.14定义派生类用于报警雷达图：屏蔽绑定字段属性，开放对外观的配置
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class RadarAlertSeries : RadarSeries
    {

        internal List<PMSLegend> legendList = new List<PMSLegend>();

        

        [Browsable(false)]
        public override string CustomProperties
        {
            get
            {
                return this.rp.CustomProperties;
            }
        }

        public RadarAlertSeries(Series Aim)
            : base(Aim)
        {
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string BindingField
        {
            get;
            set;
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override RadarProperties RadarProperties
        {
            get
            {
                return base.RadarProperties;
            }
        }

        [Browsable(false)]
        public new string Name 
        {
            get
            {
            return base.Name;
            }
            set { base.Name= value; }
        }

        [Bindable(true)]
        [Browsable(true)]
        [Category("MES报表属性")]
        [Description("获取或设置雷达图对应的图例")]
        [TypeConverter(typeof(CustomCollectionPropertyConverter))]
        public new string Legend
        {
            get 
            {
                bool contain = false;
                foreach (PMSLegend item in this.legendList)
                {
                    if (item.Name == base.Legend)
                        contain = true;
                }
                if (contain)
                    return base.Legend;
                else
                    return "";
            }
            set { base.Legend = value; }
        }

        RadarAlertProperties rp;
        [Bindable(true)]
        [Category("MES报表属性")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("获取或设置雷达图相关的属性")]
        [TypeConverter(typeof(RadarAlertPropertiesConverter))]
        public RadarAlertProperties RadarAlertProperties
        {
            get
            {
                if (rp == null)
                {
                    rp = new RadarAlertProperties();
                }
                if (rp.CustomProperties == "")
                {
                    rp.CustomProperties = "AreaDrawingStyle=" + rp.RadarAreaDrawingStyle.ToString() + ",CircularLabelsStyle=" + rp.RadarCircularLabelsStyle.ToString() + ",RadarDrawingStyle=" + rp.RadarDrawingStyle.ToString();
                }
                return rp;
            }

        }

    }

    [Serializable]
    public class RadarAlertProperties : RadarProperties
    {
        [ReadOnly(true)]
        [Description("获取雷达图的图标区形状")]
        public override RadarProperties.enumRadarAreaDrawingStyle RadarAreaDrawingStyle
        {
            get
            {
                return base.RadarAreaDrawingStyle;
            }
            set
            {
                base.RadarAreaDrawingStyle = value;
            }
        }
        [ReadOnly(true)]
        [Description("获取雷达图的标签绘制样式")]
        public override RadarProperties.enumRadarCircularLabelsStyle RadarCircularLabelsStyle
        {
            get
            {
                return base.RadarCircularLabelsStyle;
            }
            set
            {
                base.RadarCircularLabelsStyle = value;
            }
        }
        
    }

    public class CustomCollectionPropertyConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            RadarAlertSeries control = new RadarAlertSeries(null);
            if (context.Instance is RadarAlertSeries) 
            {
                control = context.Instance as RadarAlertSeries;
            }

            string[] strArray = new string[control.legendList.Count];
            for (int i = 0; i < strArray.Length; i++) 
            {
                strArray[i] = control.legendList[i].Name;
            }
            StandardValuesCollection returnStandardValuesCollection = new StandardValuesCollection(strArray);
            return returnStandardValuesCollection;
        }
    }

}
