using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Globalization;
using System.ComponentModel.Design.Serialization;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace PMS.Libraries.ToolControls.PMSChart
{

    //[Serializable]
    public class AxisDic
    {
        public string name;
        public PMSAxis axis;

        public override string ToString()
        {
            return name;
        }
    }

    internal class LabelConverter : TypeConverter
    {/*/
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }
            else if (destinationType == typeof(InstanceDescriptor))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return base.ConvertTo(context, culture, value, destinationType);
        }/*/
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] filter)
        {
            return TypeDescriptor.GetProperties(value, filter);
        }
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class PMSLabelStyle
    {/*/
        public int Angle { get; set; }
        public bool Enabled { get; set; }
        public Font Font { get; set; }
        public Color ForeColor { get; set; }
        public string Format { get; set; }
        public double Interval { get; set; }
        public double IntervalOffset { get; set; }
        public DateTimeIntervalType IntervalOffsetType { get; set; }
        public DateTimeIntervalType IntervalType { get; set; }
        public bool IsEndLabelVisible { get; set; }
        public bool IsStaggered { get; set; }
        public bool TruncatedLabels { get; set; }
/*/
        // Summary:
        //     Gets or sets a value that represents the angle at which the font is drawn.
        //
        // Returns:
        //     An integer value.
        [DefaultValue(0)]
        [RefreshProperties(RefreshProperties.All)]
        [Bindable(true)]
        public int Angle { get; set; }
        [Bindable(true)]
        [DefaultValue(true)]
        public bool Enabled { get; set; }
        //
        // Summary:
        //     Gets or sets the font of the label.
        //
        // Returns:
        //     A System.Drawing.Font object.
        [DefaultValue(typeof(Font), "Microsoft Sans Serif, 8pt")]
        [Bindable(true)]
        public Font Font { get; set; }
        //
        // Summary:
        //     Gets or sets the color of the label.
        //
        // Returns:
        //     A System.Drawing.Color value.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [DefaultValue(typeof(Color), "Black")]
        [NotifyParentProperty(true)]
        [TypeConverter(typeof(ColorConverter))]
        [Bindable(true)]
        public Color ForeColor { get; set; }
        //
        // Summary:
        //     Gets or sets the formatting string for the label text.
        //
        // Returns:
        //     A string value.
        [Bindable(true)]
        [DefaultValue("")]
        public string Format { get; set; }
        //
        // Summary:
        //     Gets or sets the size of the label interval.
        //
        // Returns:
        //     A double value.
        [DefaultValue(double.NaN)]
        [Bindable(true)]
        public double Interval { get; set; }
        //
        // Summary:
        //     Gets or sets the offset of the label interval.
        //
        // Returns:
        //     A double value.
        [Bindable(true)]
        [DefaultValue(double.NaN)]
        [RefreshProperties(RefreshProperties.All)]
        public double IntervalOffset { get; set; }
        //
        // Summary:
        //     Gets or sets the unit of measurement of the label interval offset.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType enumeration
        //     value.
        [RefreshProperties(RefreshProperties.All)]
        [Bindable(true)]
        [DefaultValue(DateTimeIntervalType.NotSet)]
        public DateTimeIntervalType IntervalOffsetType { get; set; }
        //
        // Summary:
        //     Gets or sets the unit of measurement for the size of a label interval.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType enumeration
        //     value.
        [Bindable(true)]
        [DefaultValue(DateTimeIntervalType.NotSet)]
        [RefreshProperties(RefreshProperties.All)]
        public DateTimeIntervalType IntervalType { get; set; }
        [DefaultValue(true)]
        [Bindable(true)]
        public bool IsEndLabelVisible { get; set; }
        [Bindable(true)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(false)]
        public bool IsStaggered { get; set; }
        [Bindable(true)]
        [DefaultValue(false)]
        public bool TruncatedLabels { get; set; }

        public PMSLabelStyle(LabelStyle labelStyle)
        {
            if (labelStyle == null)
            {
                Angle = 0;
                Enabled = true;
                Font = new Font("Microsoft Sans Serif", 8);
                ForeColor = Color.Black;
                Format = "";
                Interval = double.NaN;
                IntervalOffset = double.NaN;

                IntervalOffsetType = DateTimeIntervalType.NotSet;
                IntervalType = DateTimeIntervalType.NotSet;
                IsEndLabelVisible = true;
                IsStaggered = false;
                TruncatedLabels = false;
            }
            else
            {
                Angle = labelStyle.Angle;
                Enabled = labelStyle.Enabled;
                Font = labelStyle.Font;
                ForeColor = labelStyle.ForeColor;
                Format = labelStyle.Format;
                Interval = labelStyle.Interval;
                IntervalOffset = labelStyle.IntervalOffset;

                IntervalOffsetType = labelStyle.IntervalOffsetType;
                IntervalType = labelStyle.IntervalType;
                IsEndLabelVisible = labelStyle.IsEndLabelVisible;
                IsStaggered = labelStyle.IsStaggered;
                TruncatedLabels = labelStyle.TruncatedLabels;
            }
        }
        public override string ToString()
        {
            return "";
        }
        public LabelStyle ToLabelStyle()
        {
            LabelStyle labelStyle = new LabelStyle();
            labelStyle.Angle = Angle;
            labelStyle.Enabled = Enabled;
            labelStyle.Font = Font;
            labelStyle.ForeColor = ForeColor;
            labelStyle.Format = Format;
            labelStyle.Interval = Interval;
            labelStyle.IntervalOffset = IntervalOffset;

            labelStyle.IntervalOffsetType = IntervalOffsetType;
            labelStyle.IntervalType = IntervalType;
            labelStyle.IsEndLabelVisible = IsEndLabelVisible;
            labelStyle.IsStaggered = IsStaggered;
            labelStyle.TruncatedLabels = TruncatedLabels;
            return labelStyle;
        }
    }

    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class PMSGrid
    {
        /*/
        public bool Enabled { get; set; }
        public double Interval { get; set; }
        public double IntervalOffset { get; set; }
        public DateTimeIntervalType IntervalOffsetType { get; set; }
        public DateTimeIntervalType IntervalType { get; set; }
        public Color LineColor { get; set; }
        public ChartDashStyle LineDashStyle { get; set; }
        public int LineWidth { get; set; }
/*/


        // Summary:
        //     Gets or sets a flag that determines whether major or minor grid lines are
        //     enabled.
        //
        // Returns:
        //     True if enabled, false if disabled. The default is true for major grid objects
        //     and false for minor grid objects.
        [Bindable(true)]
        [DefaultValue(true)]
        public bool Enabled { get; set; }
        //
        // Summary:
        //     Gets or sets the interval between major or minor grid lines.
        //
        // Returns:
        //     A double value that represents the interval between grid lines. By default,
        //     the value is not set (System.Double.NaN) for major grid lines. For minor
        //     grid lines, the default value is zero (0).
        [Bindable(true)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(double.NaN)]
        public double Interval { get; set; }
        //
        // Summary:
        //     Gets or sets the offset of grid lines.
        //
        // Returns:
        //     A double value that represents the interval offset.
        [Bindable(true)]
        [DefaultValue(double.NaN)]
        public double IntervalOffset { get; set; }
        //
        // Summary:
        //     Gets or sets the interval offset type of major and minor grid lines.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType enumeration
        //     value that indicates the interval type. By default it is not set for major
        //     grid lines. The default value for minor grid lines is System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto.

        [Bindable(true)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(DateTimeIntervalType.NotSet)]
        public DateTimeIntervalType IntervalOffsetType { get; set; }
        //
        // Summary:
        //     Gets or sets the interval type for major or minor grid lines.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType enumeration
        //     value that indicates the interval type. By default it is not set for axis
        //     labels, major tick marks and major grid lines, by using the System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.NotSet
        //     enumeration value. The default value for minor tick marks and grid lines
        //     is System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto.

        [RefreshProperties(RefreshProperties.All)]
        [Bindable(true)]
        [DefaultValue(DateTimeIntervalType.NotSet)]
        public DateTimeIntervalType IntervalType { get; set; }
        //
        // Summary:
        //     Gets or sets the line color of a grid.
        //
        // Returns:
        //     A System.Drawing.Color object. The default value is System.Drawing.Color.Black.        
        [TypeConverter(typeof(ColorConverter))]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [DefaultValue(typeof(Color), "Black")]
        public Color LineColor { get; set; }
        //
        // Summary:
        //     Gets or sets the line style of a grid.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartDashStyle enumeration
        //     value. The default is System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid.
        [Bindable(true)]
        [DefaultValue(ChartDashStyle.Solid)]
        public ChartDashStyle LineDashStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the line width of major and minor grid lines.
        //
        // Returns:
        //     An integer that represents the line width in pixels. The default value is
        //     one (1).
        [DefaultValue(1)]
        [Bindable(true)]
        public int LineWidth { get; set; }
        public override string ToString()
        {
            return "";
        }
        public PMSGrid(Grid labelStyle)
        {
            if (labelStyle == null)
            {
                Enabled = true;
                Interval = double.NaN;
                IntervalOffset = double.NaN;
                IntervalOffsetType = DateTimeIntervalType.NotSet;
                IntervalType = DateTimeIntervalType.NotSet;
                LineColor = Color.Black;
                LineDashStyle = ChartDashStyle.Solid;
                LineWidth = 1;
            }
            else
            {
                Enabled = labelStyle.Enabled;
                Interval = labelStyle.Interval;
                IntervalOffset = labelStyle.IntervalOffset;
                IntervalOffsetType = labelStyle.IntervalOffsetType;
                IntervalType = labelStyle.IntervalType;
                LineColor = labelStyle.LineColor;
                LineDashStyle = labelStyle.LineDashStyle;
                LineWidth = labelStyle.LineWidth;
            }
        }
        public Grid ToGrid()
        {
            Grid labelStyle = new Grid();
            labelStyle.Enabled = Enabled;
            labelStyle.Interval = Interval;
            labelStyle.IntervalOffset = IntervalOffset;
            labelStyle.IntervalOffsetType = IntervalOffsetType;
            labelStyle.IntervalType = IntervalType;
            labelStyle.LineColor = LineColor;
            labelStyle.LineDashStyle = LineDashStyle;
            labelStyle.LineWidth = LineWidth;
            return labelStyle;
        }
    }

    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class PMSTickMark : PMSGrid
    {
        [DefaultValue(1f)]
        [Bindable(true)]
        public float Size { get; set; }
        [Bindable(true)]
        [DefaultValue(TickMarkStyle.OutsideArea)]
        public TickMarkStyle TickMarkStyle { get; set; }


        public override string ToString()
        {
            return "";
        }
        public PMSTickMark(TickMark labelStyle)
            : base(labelStyle)
        {
            if (labelStyle == null)
            {
                Size = 1;
                TickMarkStyle = TickMarkStyle.OutsideArea;
                Enabled = true;
                Interval = double.NaN;
                IntervalOffset = double.NaN;
                IntervalOffsetType = DateTimeIntervalType.NotSet;
                IntervalType = DateTimeIntervalType.NotSet;
                LineColor = Color.Black;
                LineDashStyle = ChartDashStyle.Solid;
                LineWidth = 1;
            }
            else
            {
                Size = labelStyle.Size;
                TickMarkStyle = labelStyle.TickMarkStyle;
                Enabled = labelStyle.Enabled;
                Interval = labelStyle.Interval;
                IntervalOffset = labelStyle.IntervalOffset;
                IntervalOffsetType = labelStyle.IntervalOffsetType;
                IntervalType = labelStyle.IntervalType;
                LineColor = labelStyle.LineColor;
                LineDashStyle = labelStyle.LineDashStyle;
                LineWidth = labelStyle.LineWidth;
            }
        }
        public TickMark ToTickMark()
        {
            TickMark labelStyle = new TickMark();
            labelStyle.Size = Size;
            labelStyle.TickMarkStyle = TickMarkStyle;
            labelStyle.Enabled = Enabled;
            labelStyle.Interval = Interval;
            labelStyle.IntervalOffset = IntervalOffset;
            labelStyle.IntervalOffsetType = IntervalOffsetType;
            labelStyle.IntervalType = IntervalType;
            labelStyle.LineColor = LineColor;
            labelStyle.LineDashStyle = LineDashStyle;
            labelStyle.LineWidth = LineWidth;
            return labelStyle;
        }
    }

    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class PMSAxisScaleBreakStyle
    {
        // Summary:
        //     Gets or sets the style of the break line that will be used to draw the scale
        //     break.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.BreakLineStyle enumeration
        //     value.
        [DefaultValue(BreakLineStyle.Ragged)]
        public BreakLineStyle BreakLineStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the threshold of space on the chart at which scale breaks are
        //     drawn.
        //
        // Returns:
        //     An integer value that specifies the threshold of space on the chart at which
        //     scale breaks are drawn.
        [DefaultValue(25)]
        public int CollapsibleSpaceThreshold { get; set; }
        //
        // Summary:
        //     Gets or sets a flag that indicates whether scale breaks are enabled.
        //
        // Returns:
        //     A Boolean value that specifies whether scale breaks are enabled.
        [ParenthesizePropertyName(true)]
        [DefaultValue(false)]
        public bool Enabled { get; set; }
        //
        // Summary:
        //     Gets or sets the color of the scale break line.
        //
        // Returns:
        //     A System.Drawing.Color value that represents the color of the scale break
        //     line.
        [TypeConverter(typeof(ColorConverter))]
        [DefaultValue(typeof(Color), "Black")]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]

        public Color LineColor { get; set; }
        //
        // Summary:
        //     Gets or sets the style of the scale break line.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartDashStyle value that
        //     represents the style of the scale break line.
        [DefaultValue(ChartDashStyle.Solid)]
        public ChartDashStyle LineDashStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the width of the scale break line.
        //
        // Returns:
        //     An integer value that represents the width of the scale break line.
        [DefaultValue(1)]
        public int LineWidth { get; set; }
        //
        // Summary:
        //     Gets or sets the maximum number of scale breaks to be displayed on the chart.
        //
        // Returns:
        //     An integer value that represents the maximum number of scale breaks to be
        //     displayed on the chart.
        [DefaultValue(2)]
        public int MaxNumberOfBreaks { get; set; }
        //
        // Summary:
        //     Gets or sets the spacing gap between the lines of the scale break. The spacing
        //     gap is represented as a percentage of the Y-axis.
        //
        // Returns:
        //     A double value that represents the spacing gap between the lines of the scale
        //     break.
        [DefaultValue(1.5)]
        public double Spacing { get; set; }
        //
        // Summary:
        //     Gets or sets a System.Windows.Forms.DataVisualization.Charting.StartFromZero
        //     enumeration value that indicates whether to start the scale break from zero.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.StartFromZero enumeration
        //     value that indicates whether to start the scale break from zero.
        [DefaultValue(StartFromZero.Auto)]
        public StartFromZero StartFromZero { get; set; }

        public override string ToString()
        {
            return "";
        }
        public PMSAxisScaleBreakStyle(AxisScaleBreakStyle labelStyle)
        {
            if (labelStyle == null)
            {
                BreakLineStyle = BreakLineStyle.Ragged;
                CollapsibleSpaceThreshold = 25;
                Enabled = false;
                LineColor = Color.Black;
                LineDashStyle = ChartDashStyle.Solid;
                LineWidth = 1;
                MaxNumberOfBreaks = 2;
                Spacing = 1.5;
                StartFromZero = StartFromZero.Auto;
            }
            else
            {
                BreakLineStyle = labelStyle.BreakLineStyle;
                CollapsibleSpaceThreshold = labelStyle.CollapsibleSpaceThreshold;
                Enabled = labelStyle.Enabled;
                LineColor = labelStyle.LineColor;
                LineDashStyle = labelStyle.LineDashStyle;
                LineWidth = labelStyle.LineWidth;
                MaxNumberOfBreaks = labelStyle.MaxNumberOfBreaks;
                Spacing = labelStyle.Spacing;
                StartFromZero = labelStyle.StartFromZero;
            }
        }
        public AxisScaleBreakStyle ToAxisScaleBreakStyle()
        {
            AxisScaleBreakStyle labelStyle = new AxisScaleBreakStyle();
            labelStyle.BreakLineStyle = BreakLineStyle;
            labelStyle.CollapsibleSpaceThreshold = CollapsibleSpaceThreshold;
            labelStyle.Enabled = Enabled;
            labelStyle.LineColor = LineColor;
            labelStyle.LineDashStyle = LineDashStyle;
            labelStyle.LineWidth = LineWidth;
            labelStyle.MaxNumberOfBreaks = MaxNumberOfBreaks;
            labelStyle.Spacing = Spacing;
            labelStyle.StartFromZero = StartFromZero;
            return labelStyle;
        }
    }

    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class PMSAxisScaleView
    {
        //
        // Summary:
        //     Gets or sets the minimum size of the System.Windows.Forms.DataVisualization.Charting.AxisScaleView
        //     object.
        //
        // Returns:
        //     A double that represents the minimum size.
        [DefaultValue(double.NaN)]
        [Bindable(true)]
        public double MinSize { get; set; }
        //
        // Summary:
        //     Gets or sets the unit of measurement of the System.Windows.Forms.DataVisualization.Charting.AxisScaleView.MinSize
        //     property.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType enumeration
        //     value.
        [Bindable(true)]
        [DefaultValue(DateTimeIntervalType.Auto)]
        public DateTimeIntervalType MinSizeType { get; set; }
        //
        // Summary:
        //     Gets or sets the position of the scale view.
        //
        // Returns:
        //     A double that represents the position of the scale view.-
        [ParenthesizePropertyName(true)]
        [DefaultValue(double.NaN)]
        [Bindable(true)]
        public double Position { get; set; }
        //
        // Summary:
        //     Gets or sets the size of the scale view.
        //
        // Returns:
        //     A double that represents the size of the scale view.
        [Bindable(true)]
        [ParenthesizePropertyName(true)]
        [DefaultValue(double.NaN)]
        public double Size { get; set; }
        //
        // Summary:
        //     Gets or sets the unit of measurement for the System.Windows.Forms.DataVisualization.Charting.AxisScaleView.Size
        //     property.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType enumeration
        //     value.
        [ParenthesizePropertyName(true)]
        [Bindable(true)]
        [DefaultValue(DateTimeIntervalType.Auto)]
        public DateTimeIntervalType SizeType { get; set; }
        //
        // Summary:
        //     Gets or sets the minimum small scrolling size. Used only if the small scrolling
        //     size is not set.
        //
        // Returns:
        //     A double that represents the small scrolling size.
        [DefaultValue(1)]
        [Bindable(true)]
        public double SmallScrollMinSize { get; set; }
        //
        // Summary:
        //     Gets or sets the unit of measurement for the System.Windows.Forms.DataVisualization.Charting.AxisScaleView.SmallScrollMinSize
        //     property.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType enumeration
        //     value.
        [Bindable(true)]
        [DefaultValue(DateTimeIntervalType.Auto)]
        public DateTimeIntervalType SmallScrollMinSizeType { get; set; }
        //
        // Summary:
        //     Gets or sets the small scrolling size.
        //
        // Returns:
        //     A double value.
        [DefaultValue(double.NaN)]
        [Bindable(true)]
        public double SmallScrollSize { get; set; }
        //
        // Summary:
        //     Gets or sets the unit of measurement for the System.Windows.Forms.DataVisualization.Charting.AxisScaleView.SmallScrollMinSize
        //     property.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType enumeration
        //     value.
        [Bindable(true)]
        [DefaultValue(DateTimeIntervalType.Auto)]
        public DateTimeIntervalType SmallScrollSizeType { get; set; }
        [Bindable(true)]
        [DefaultValue(true)]
        public bool Zoomable { get; set; }
        public override string ToString()
        {
            return "";
        }
        public PMSAxisScaleView(AxisScaleView labelStyle)
        {
            if (labelStyle == null)
            {
                MinSize = double.NaN;
                MinSizeType = DateTimeIntervalType.Auto;
                Position = double.NaN;
                Size = double.NaN;
                SizeType = DateTimeIntervalType.Auto;
                SmallScrollMinSize = 1;
                SmallScrollMinSizeType = DateTimeIntervalType.Auto;
                SmallScrollSize = double.NaN;
                SmallScrollSizeType = DateTimeIntervalType.Auto;
                Zoomable = true;
            }
            else
            {
                MinSize = labelStyle.MinSize;
                MinSizeType = labelStyle.MinSizeType;
                Position = labelStyle.Position;
                Size = labelStyle.Size;
                SizeType = labelStyle.SizeType;
                SmallScrollMinSize = labelStyle.SmallScrollMinSize;
                SmallScrollMinSizeType = labelStyle.SmallScrollMinSizeType;
                SmallScrollSize = labelStyle.SmallScrollSize;
                SmallScrollSizeType = labelStyle.SmallScrollSizeType;
                Zoomable = labelStyle.Zoomable;
            }
        }
        public AxisScaleView ToAxisScaleView()
        {
            AxisScaleView labelStyle = new AxisScaleView();
            labelStyle.MinSize = MinSize;
            labelStyle.MinSizeType = MinSizeType;
            labelStyle.Position = Position;
            labelStyle.Size = Size;
            labelStyle.SizeType = SizeType;
            labelStyle.SmallScrollMinSize = SmallScrollMinSize;
            labelStyle.SmallScrollMinSizeType = SmallScrollMinSizeType;
            labelStyle.SmallScrollSize = SmallScrollSize;
            labelStyle.SmallScrollSizeType = SmallScrollSizeType;
            labelStyle.Zoomable = Zoomable;
            return labelStyle;
        }
    }

    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class PMSAxisScrollBar
    {
        //
        // Summary:
        //     Gets or sets the background color of a scrollbar.
        //
        // Returns:
        //     A System.Drawing.Color value that represents the background color of the
        //     scrollbar. The default value is System.Drawing.Color.Empty.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [DefaultValue(typeof(Color), "")]
        [TypeConverter(typeof(ColorConverter))]
        [Bindable(true)]
        public Color BackColor { get; set; }
        //
        // Summary:
        //     Gets or sets the color of the scrollbar buttons.
        //
        // Returns:
        //     A System.Drawing.Color value that represents the color of the scrollbar buttons.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [TypeConverter(typeof(ColorConverter))]
        [Bindable(true)]
        [DefaultValue(typeof(Color), "")]
        public Color ButtonColor { get; set; }
        //
        // Summary:
        //     Gets or sets the style of the scrollbar button.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ScrollBarButtonStyles enumeration
        //     value.
        [Bindable(true)]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.FlagsEnumUITypeEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [DefaultValue(ScrollBarButtonStyles.All)]
        public ScrollBarButtonStyles ButtonStyle { get; set; }
        //
        // Summary:
        //     Gets or sets a flag that determines whether a scrollbar is enabled.
        //
        // Returns:
        //     True if the scrollbar is enabled for an axis, false if the scrollbar is not
        //     enabled. The default value is true.
        [Bindable(true)]
        [DefaultValue(true)]
        public bool Enabled { get; set; }
        [Bindable(true)]
        [DefaultValue(true)]
        public bool IsPositionedInside { get; set; }

        // Summary:
        //     Gets or sets the line color of a scrollbar.
        //
        // Returns:
        //     A System.Drawing.Color value that represents the line color of the scrollbar.
        //     The default value is System.Drawing.Color.Empty.
        [Bindable(true)]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]

        [DefaultValue(typeof(Color), "")]
        [TypeConverter(typeof(ColorConverter))]
        public Color LineColor { get; set; }
        //
        // Summary:
        //     Gets or sets the width of a scrollbar, in pixels.
        //
        // Returns:
        //     A double value that represents the width of a scrollbar in pixels. The default
        //     value is 14 pixels. The value can range from 5 to 20 pixels.
        [Bindable(true)]
        [DefaultValue(14)]
        public double Size { get; set; }
        public override string ToString()
        {
            return "";
        }
        public PMSAxisScrollBar(AxisScrollBar labelStyle)
        {
            if (labelStyle == null)
            {
                //BackColor = Color.White;
                //ButtonColor = Color.;
                ButtonStyle = ScrollBarButtonStyles.All;
                Enabled = true;
                IsPositionedInside = true;
                //LineColor = Color.White;
                Size = 14;
            }
            else
            {
                BackColor = labelStyle.BackColor;
                ButtonColor = labelStyle.ButtonColor;
                ButtonStyle = labelStyle.ButtonStyle;
                Enabled = labelStyle.Enabled;
                IsPositionedInside = labelStyle.IsPositionedInside;
                LineColor = labelStyle.LineColor;
                Size = labelStyle.Size;
            }
        }
        public AxisScrollBar ToAxisScrollBar()
        {
            AxisScrollBar labelStyle = new AxisScrollBar();
            labelStyle.BackColor = BackColor;
            labelStyle.ButtonColor = ButtonColor;
            labelStyle.ButtonStyle = ButtonStyle;
            labelStyle.Enabled = Enabled;
            labelStyle.IsPositionedInside = IsPositionedInside;
            labelStyle.LineColor = LineColor;
            labelStyle.Size = Size;
            return labelStyle;
        }
    }

    /// <summary>
    /// LabelAutoFitStyles
    /// </summary>
    internal class LabelAutoFitStyleEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context != null && context.Instance != null)
            {
                return UITypeEditorEditStyle.DropDown;
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
                    PMSAxis cbx = null;
                    if (context.Instance.GetType() == typeof(PMSAxis))
                        cbx = (PMSAxis)context.Instance;
                    LabelFit rfc = new LabelFit(editorService);

                    rfc.LabelAutoFitStyle = cbx.LabelAutoFitStyle;
                    editorService.DropDownControl(rfc);
                    value = rfc.LabelAutoFitStyle;
                    return value;
                }
            }

            return value;
        }
    }
    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class PMSAxis// : Axis
    {
        #region
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [DefaultValue(AxisArrowStyle.None)]
        [Description("Axis arrow type.")]
        [Category("外观")]
        public AxisArrowStyle ArrowStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the location at which an axis is crossed by its associated axis.
        //
        // Returns:
        //     A double value that represents where an axis is crossed by its associated
        //     axis. The default value is System.Double.NaN.
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [DefaultValue(double.NaN)]
        [Description("Gets or sets the location at which an axis is crossed by its associated axis.")]
        [Category("比例")]
        public virtual double Crossing { get; set; }
        //
        // Summary:
        //     Gets a System.Windows.Forms.DataVisualization.Charting.CustomLabelsCollection
        //     object used to store System.Windows.Forms.DataVisualization.Charting.CustomLabel
        //     objects.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.CustomLabelsCollection
        //     object.
        [Description("CustomLabel objects for an axis")]
        [Category("标签")]
        [Editor(typeof(CustomLabelEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<PMSCustomLabel> CustomLabels { get; set; }
        //
        // Summary:
        //     Gets or sets a value that indicates whether an axis is enabled.
        //
        // Returns:
        //     An System.Windows.Forms.DataVisualization.Charting.AxisEnabled enumeration
        //     value. The default value is System.Windows.Forms.DataVisualization.Charting.AxisEnabled.Auto.
        [DefaultValue(typeof(AxisEnabled), "Auto")]
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [Description(" Gets or sets a value that indicates whether an axis is enabled.")]
        [Category("杂项")]
        public AxisEnabled Enabled { get; set; }
        //
        // Summary:
        //     Gets or sets the color of interlaced strip lines.
        //
        // Returns:
        //     A System.Drawing.Color object that represents the color of interlaced strip
        //     lines.
        [TypeConverter(typeof(ColorConverter))]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [NotifyParentProperty(true)]
        [DefaultValue(typeof(Color), "")]
        [Bindable(true)]
        [Description("Gets or sets the color of interlaced strip lines.")]
        [Category("外观")]
        public Color InterlacedColor { get; set; }
        //
        // Summary:
        //     Gets or sets the interval of an axis.
        //
        // Returns:
        //     A double value that represents the interval of an axis. The default value
        //     is "Auto", which is represented by a value of zero (0).
        [RefreshProperties(RefreshProperties.All)]
        [Bindable(true)]
        [DefaultValue(0)]
        [Description("Gets or sets the interval of an axis.")]
        [Category("间隔")]
        public double Interval { get; set; }

        [RefreshProperties(RefreshProperties.All)]
        [Bindable(true)]
        [DefaultValue(0)]
        [Description("Gets or sets the whole point Label of an axis.")]
        [Category("间隔")]
        public int FixPoint { get; set; }
        //
        // Summary:
        //     Gets or sets a flag that determines if a fixed number of intervals is used
        //     on the axis, or if the number of intervals depends on the axis size.
        //
        // Returns:
        //     An System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode enumeration
        //     value. The default value is System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.FixedCount.

        [Description("Gets or sets a flag that determines if a fixed number of intervals is used on the axis, or if the number of intervals depends on the axis size.")]
        [Category("间隔")]
        [DefaultValue(IntervalAutoMode.FixedCount)]
        public IntervalAutoMode IntervalAutoMode { get; set; }
        //
        // Summary:
        //     Gets or sets the interval offset of an axis.
        //
        // Returns:
        //     A double value that represents the interval offset of an axis. The default
        //     value is "Auto", which is represented by a value of zero (0).
        [DefaultValue(0)]
        [Bindable(true)]
        [Description("Gets or sets the interval offset of an axis.")]
        [Category("间隔")]
        [RefreshProperties(RefreshProperties.All)]
        public double IntervalOffset { get; set; }
        //
        // Summary:
        //     Gets or sets the interval offset type of an axis.
        //
        // Returns:
        //     The interval offset type of an axis. The default value is System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto.
        [RefreshProperties(RefreshProperties.All)]
        [Bindable(true)]
        [Description(" Gets or sets the interval offset type of an axis.")]
        [Category("间隔")]
        [DefaultValue(DateTimeIntervalType.Auto)]
        public DateTimeIntervalType IntervalOffsetType { get; set; }
        //
        // Summary:
        //     Gets or sets the interval type of an axis.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType that
        //     represents the interval type of an axis. The default value is System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto.
        [RefreshProperties(RefreshProperties.All)]
        [Bindable(true)]
        [Description("Axis interval type.")]
        [Category("间隔")]
        [DefaultValue(DateTimeIntervalType.Auto)]
        public DateTimeIntervalType IntervalType { get; set; }
        [DefaultValue(false)]
        [Bindable(true)]
        [Description("Indicates that interlaced strip lines will be displayed for the axis.")]
        [Category("外观")]
        [NotifyParentProperty(true)]
        public bool IsInterlaced { get; set; }
        [DefaultValue(true)]
        [Bindable(true)]
        [Description("Automatic labels fitting flag.")]
        [Category("标记")]
        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.All)]
        public bool IsLabelAutoFit { get; set; }
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [Description("Logarthmic scale recalculates the values shown in the minimum,maximum, boxes as powers of logarithmBase for the value axis, based on the range of data. No zero or negative data values are permitted on logarithmic charts.")]
        [Category("比例")]
        public bool IsLogarithmic { get; set; }
        //
        // Summary:
        //     Gets or sets a flag that determines whether to add a margin to the axis.
        //
        // Returns:
        //     True if a space is added between the first or last data point and the border
        //     of chart area.
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [DefaultValue(true)]
        [Description("If true, a space is added betweenthe first and the last data points and the border f the chart area.")]
        [Category("比例")]
        public bool IsMarginVisible { get; set; }
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [DefaultValue(true)]
        [Description("Tick marks and labels move with axis when the crossing value is changed.")]
        [Category("外观")]
        public virtual bool IsMarksNextToAxis { get; set; }
        [DefaultValue(false)]
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [Description("The values on the axis are in reverse order.")]
        [Category("比例")]
        public bool IsReversed { get; set; }
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [DefaultValue(true)]
        [Description("If true, this property will set auto minimum value to zero if all data point values are positive. Otherwise, minimum value from data points will be used.")]
        [Category("比例")]
        public bool IsStartedFromZero { get; set; }
        //
        // Summary:
        //     Gets or sets the maximum font size that can be used by the label auto-fitting
        //     algorithm.
        //
        // Returns:
        //     An integer value.
        [RefreshProperties(RefreshProperties.All)]
        [Bindable(true)]
        [DefaultValue(10)]
        [Description(" Gets or sets the maximum font size that can be used by the label auto-fitting algorithm.")]
        [Category("标记")]
        [NotifyParentProperty(true)]
        public int LabelAutoFitMaxFontSize { get; set; }
        //
        // Summary:
        //     Gets or sets the minimum font size that can be used by the label auto-fitting
        //     algorithm.
        //
        // Returns:
        //     An integer value.
        [DefaultValue(6)]
        [Bindable(true)]
        [RefreshProperties(RefreshProperties.All)]
        [NotifyParentProperty(true)]
        [Description("Gets or sets the minimum font size that can be used by the label auto-fitting algorithm.")]
        [Category("标记")]
        public int LabelAutoFitMinFontSize { get; set; }
        //
        // Summary:
        //     Gets or sets the allowable label changes that can be made to enable the label
        //     to be fit along an axis.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles enumeration
        //     value.
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [Description("Gets or sets the allowable label changes that can be made to enable the label  to be fit along an axis.")]
        [Category("标记")]
        [Editor(typeof(LabelAutoFitStyleEditor), typeof(UITypeEditor))]
        [DefaultValue(LabelAutoFitStyles.DecreaseFont | LabelAutoFitStyles.IncreaseFont | LabelAutoFitStyles.LabelsAngleStep30 | LabelAutoFitStyles.StaggeredLabels | LabelAutoFitStyles.WordWrap)]
        public LabelAutoFitStyles LabelAutoFitStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the System.Windows.Forms.DataVisualization.Charting.LabelStyle
        //     properties of an axis.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.LabelStyle enumeration
        //     value.
        [NotifyParentProperty(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Bindable(true)]
        [Description("Gets or sets the System.Windows.Forms.DataVisualization.Charting.LabelStyle properties of an axis.")]
        [Category("标记")]
        public PMSLabelStyle LabelStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the line color of an axis.
        //
        // Returns:
        //     A System.Drawing.Color object that represents the line color used to draw
        //     the axis. The default is System.Drawing.Color.Black.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [DefaultValue(typeof(Color), "Black")]
        [NotifyParentProperty(true)]
        [TypeConverter(typeof(ColorConverter))]
        [Description("Gets or sets the line color of an axis.")]
        [Category("外观")]
        public Color LineColor { get; set; }
        //
        // Summary:
        //     Gets or sets the line style of an axis.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartDashStyle enumeration
        //     value.
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [Description("Gets or sets the line style of an axis.")]
        [Category("外观")]
        [DefaultValue(ChartDashStyle.Solid)]
        public ChartDashStyle LineDashStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the line width of an axis, in pixels.
        //
        // Returns:
        //     An integer value that represents the width of an axis line. The default value
        //     is one (1) pixel.
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [DefaultValue(1)]
        [Description("Gets or sets the line width of an axis, in pixels.")]
        [Category("外观")]
        public int LineWidth { get; set; }
        //
        // Summary:
        //     Gets or sets a value for the logarithm base for the logarithmic axis.
        //
        // Returns:
        //     A double value. The default value is base 10.
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [DefaultValue(10)]
        [Description("Gets or sets a value for the logarithm base for the logarithmic axis.")]
        [Category("标签")]
        public double LogarithmBase { get; set; }
        //
        // Summary:
        //     Gets or sets a System.Windows.Forms.DataVisualization.Charting.Grid object
        //     used to set the major grid line properties for an axis.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.Grid object used to get
        //     or set the major grid properties of an axis.
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [Description("A System.Windows.Forms.DataVisualization.Charting.Grid object used to get or set the major grid properties of an axis.")]
        [Category("标签")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PMSGrid MajorGrid { get; set; }
        //
        // Summary:
        //     Gets or sets a System.Windows.Forms.DataVisualization.Charting.TickMark object
        //     used to set the major tick mark properties of an axis.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.TickMark object used to
        //     set the properties of a major tick mark of an axis.
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [Description("Gets or sets a System.Windows.Forms.DataVisualization.Charting.TickMark object used to set the major tick mark properties of an axis.")]
        [Category("标签")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PMSTickMark MajorTickMark { get; set; }
        //
        // Summary:
        //     Gets or sets the maximum value of an axis.
        //
        // Returns:
        //     A double value that represents the maximum value of an axis. The default
        //     value is System.Double.NaN.
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [DefaultValue(double.NaN)]
        [Description(" Gets or sets the maximum value of an axis.")]
        [Category("标签")]
        public double Maximum { get; set; }
        //
        // Summary:
        //     Gets or sets the minimum value of an axis.
        //
        // Returns:
        //     A double value that represents the minimum value of an axis. The default
        //     value is System.Double.NaN.
        [Bindable(true)]
        [DefaultValue(double.NaN)]
        [NotifyParentProperty(true)]
        [Description("Gets or sets the minimum value of an axis.")]
        [Category("标签")]
        public double Minimum { get; set; }
        //
        // Summary:
        //     Gets or sets a System.Windows.Forms.DataVisualization.Charting.Grid object
        //     used to specify the minor grid lines attributes of an axis.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.Grid object used to get
        //     or set the attributes of the minor grid lines of an axis.
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("Gets or sets a System.Windows.Forms.DataVisualization.Charting.Grid object used to specify the minor grid lines attributes of an axis.")]
        [Category("标签")]
        public PMSGrid MinorGrid { get; set; }
        //
        // Summary:
        //     Gets or sets a System.Windows.Forms.DataVisualization.Charting.TickMark object
        //     used to set the minor tick mark properties of an axis.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.TickMark object used for
        //     the minor tick mark properties of an axis.
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("Gets or sets a System.Windows.Forms.DataVisualization.Charting.TickMark object used to set the minor tick mark properties of an axis.")]
        [Category("标签")]
        public PMSTickMark MinorTickMark { get; set; }

        //
        // Summary:
        //     Gets or sets the axis scale break style.
        //
        // Returns:
        //     An System.Windows.Forms.DataVisualization.Charting.AxisScaleBreakStyle enumeration
        //     value.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [NotifyParentProperty(true)]
        [Description("Gets or sets the axis scale break style.")]
        [Category("比例")]
        public virtual PMSAxisScaleBreakStyle ScaleBreakStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the view of an axis.
        //
        // Returns:
        //     An System.Windows.Forms.DataVisualization.Charting.AxisScaleView enumeration
        //     value.
        [Bindable(true)]
        [Description("Gets or sets the view of an axis.")]
        [Category("数据视图")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PMSAxisScaleView ScaleView { get; set; }
        //
        // Summary:
        //     Gets or sets an axis scrollbar.
        //
        // Returns:
        //     An System.Windows.Forms.DataVisualization.Charting.AxisScrollbar object,
        //     which represents the scrollbar of an axis.
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("Gets or sets an axis scrollbar.")]
        [Category("数据视图")]
        public PMSAxisScrollBar ScrollBar { get; set; }
        //
        // Summary:
        //     Gets a System.Windows.Forms.DataVisualization.Charting.StripLinesCollection
        //     object.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.StripLinesCollection object,
        //     which stores all System.Windows.Forms.DataVisualization.Charting.StripLine
        //     objects for an axis.
        //[Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartCollectionEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        //[Bindable(true)]
        [Description("StripLine objects for an axis")]
        [Category("外观")]
        [Editor(typeof(StripLineCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<PMSStripLine> StripLines { get; set; }
        //
        // Summary:
        //     Gets or sets the orientation of the text in the axis title.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.TextOrientation enumeration
        //     value.
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [Description("Gets or sets the orientation of the text in the axis title.")]
        [Category("标题")]
        [DefaultValue(TextOrientation.Auto)]
        public TextOrientation TextOrientation { get; set; }
        //
        // Summary:
        //     Gets or sets the title of the axis.
        //
        // Returns:
        //     A string value that represents the title of the axis.
        [DefaultValue("")]
        [Bindable(true)]
        [Description("Gets or sets the title of the axis.")]
        [Category("标题")]
        [NotifyParentProperty(true)]
        public string Title { get; set; }
        //
        // Summary:
        //     Gets or sets the alignment of an axis title.
        //
        // Returns:
        //     A System.Drawing.StringAlignment enumeration value.
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [DefaultValue(typeof(StringAlignment), "Center")]
        [Description("Gets or sets the alignment of an axis title.")]
        [Category("标题")]
        public StringAlignment TitleAlignment { get; set; }
        //
        // Summary:
        //     Gets or sets the title font properties of an axis.
        //
        // Returns:
        //     A System.Drawing.Font object used for the font properties of a title.
        [DefaultValue(typeof(Font), "Microsoft Sans Serif, 8pt")]
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [Description("Gets or sets the title font properties of an axis.")]
        [Category("标题")]
        public Font TitleFont { get; set; }
        //
        // Summary:
        //     Gets or sets the text color of the axis title.
        //
        // Returns:
        //     A System.Drawing.Color structure. The default color is System.Drawing.Color.Black.
        [DefaultValue(typeof(Color), "Black")]
        [Bindable(true)]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [NotifyParentProperty(true)]
        [TypeConverter(typeof(ColorConverter))]
        [Description("Gets or sets the text color of the axis title.")]
        [Category("标题")]
        public Color TitleForeColor { get; set; }
        //
        // Summary:
        //     Gets or sets the tooltip used for the axis.
        //
        // Returns:
        //     A string value.
        [DefaultValue("")]
        [Bindable(true)]
        [Description("Gets or sets the tooltip used for the axis.")]
        [Category("杂项")]
        public string ToolTip { get; set; }

        public object Tag { get; set; }
        #endregion

        public PMSAxis(Axis axis)
        {
            if (axis == null)
            {
                ArrowStyle = AxisArrowStyle.None;
                Crossing = double.NaN;
                Enabled = AxisEnabled.Auto;

                //InterlacedColor = Color.White;
                Interval = 0;
                IntervalAutoMode = IntervalAutoMode.FixedCount;
                IntervalOffset = 0;

                IntervalOffsetType = DateTimeIntervalType.Auto;
                IntervalType = DateTimeIntervalType.Auto;
                IsInterlaced = false;
                IsLabelAutoFit = true;

                IsLogarithmic = false;
                IsMarginVisible = true;
                IsMarksNextToAxis = true;
                IsReversed = false;

                IsStartedFromZero = true;
                LabelAutoFitMaxFontSize = 10;
                LabelAutoFitMinFontSize = 6;
                LabelAutoFitStyle = LabelAutoFitStyles.DecreaseFont | LabelAutoFitStyles.IncreaseFont | LabelAutoFitStyles.LabelsAngleStep30 | LabelAutoFitStyles.StaggeredLabels | LabelAutoFitStyles.WordWrap;

                LabelStyle = new PMSLabelStyle(null);
                LineColor = Color.Black;
                LineDashStyle = ChartDashStyle.Solid;
                LineWidth = 1;

                LogarithmBase = 10;
                MajorGrid = new PMSGrid(null);
                MajorTickMark = new PMSTickMark(null);
                Maximum = double.NaN;

                Minimum = double.NaN;
                MinorGrid = new PMSGrid(null);
                MinorTickMark = new PMSTickMark(null);

                ScaleBreakStyle = new PMSAxisScaleBreakStyle(null);
                ScaleView = new PMSAxisScaleView(null);
                ScrollBar = new PMSAxisScrollBar(null);
                TextOrientation = TextOrientation.Auto;

                Title = "";
                TitleAlignment = StringAlignment.Center;
                TitleFont = new Font("Microsoft Sans Serif", 8);
                TitleForeColor = Color.Black;

                ToolTip = "";

                Tag = null;

                this.StripLines = new List<PMSStripLine>();
                this.CustomLabels = new List<PMSCustomLabel>();
            }
            else
            {
                ArrowStyle = axis.ArrowStyle;
                Crossing = axis.Crossing;
                Enabled = axis.Enabled;

                InterlacedColor = axis.InterlacedColor;
                Interval = axis.Interval;
                IntervalAutoMode = axis.IntervalAutoMode;
                IntervalOffset = axis.IntervalOffset;

                IntervalOffsetType = axis.IntervalOffsetType;
                IntervalType = axis.IntervalType;
                IsInterlaced = axis.IsInterlaced;
                IsLabelAutoFit = axis.IsLabelAutoFit;

                IsLogarithmic = axis.IsLogarithmic;
                IsMarginVisible = axis.IsMarginVisible;
                IsMarksNextToAxis = axis.IsMarksNextToAxis;
                IsReversed = axis.IsReversed;

                IsStartedFromZero = axis.IsStartedFromZero;
                LabelAutoFitMaxFontSize = axis.LabelAutoFitMaxFontSize;
                LabelAutoFitMinFontSize = axis.LabelAutoFitMinFontSize;
                LabelAutoFitStyle = axis.LabelAutoFitStyle;

                LabelStyle = new PMSLabelStyle(axis.LabelStyle);
                LineColor = axis.LineColor;
                LineDashStyle = axis.LineDashStyle;
                LineWidth = axis.LineWidth;

                LogarithmBase = axis.LogarithmBase;
                MajorGrid = new PMSGrid(axis.MajorGrid);
                MajorTickMark = new PMSTickMark(axis.MajorTickMark);
                Maximum = axis.Maximum;

                Minimum = axis.Minimum;
                MinorGrid = new PMSGrid(axis.MinorGrid);
                MinorTickMark = new PMSTickMark(axis.MinorTickMark);

                ScaleBreakStyle = new PMSAxisScaleBreakStyle(axis.ScaleBreakStyle);
                ScaleView = new PMSAxisScaleView(axis.ScaleView);
                ScrollBar = new PMSAxisScrollBar(axis.ScrollBar);
                TextOrientation = axis.TextOrientation;

                Title = axis.Title;
                TitleAlignment = axis.TitleAlignment;
                TitleFont = axis.TitleFont;
                TitleForeColor = axis.TitleForeColor;

                ToolTip = axis.ToolTip;

                Tag = axis.Tag;

                this.StripLines = new List<PMSStripLine>();

                if (axis.StripLines != null && axis.StripLines.Count > 0)
                {
                    foreach (var node in axis.StripLines)
                    {
                        this.StripLines.Add(new PMSStripLine(node));
                    }
                }
                this.CustomLabels = new List<PMSCustomLabel>();

                if (axis.CustomLabels != null && axis.CustomLabels.Count > 0)
                {
                    foreach (var node in axis.CustomLabels)
                    {
                        this.CustomLabels.Add(new PMSCustomLabel(node));
                    }
                }
            }
        }
        public Axis ToAxis()
        {
            Axis axis = new Axis();
            SetAxisValue(axis);
            return axis;
        }
        public void SetAxisValue(Axis axis)
        {
            if (axis == null)
                return;
            axis.ArrowStyle = ArrowStyle;
            //axis.AxisName = AxisName;
            axis.Crossing = Crossing;
            axis.Enabled = Enabled;

            axis.InterlacedColor = InterlacedColor;
            axis.Interval = Interval;
            axis.IntervalAutoMode = IntervalAutoMode;
            axis.IntervalOffset = IntervalOffset;

            axis.IntervalOffsetType = IntervalOffsetType;
            axis.IntervalType = IntervalType;
            axis.IsInterlaced = IsInterlaced;
            axis.IsLabelAutoFit = IsLabelAutoFit;

            axis.IsLogarithmic = IsLogarithmic;
            axis.IsMarginVisible = IsMarginVisible;
            axis.IsMarksNextToAxis = IsMarksNextToAxis;
            axis.IsReversed = IsReversed;

            axis.IsStartedFromZero = IsStartedFromZero;
            axis.LabelAutoFitMaxFontSize = LabelAutoFitMaxFontSize;
            axis.LabelAutoFitMinFontSize = LabelAutoFitMinFontSize;
            axis.LabelAutoFitStyle = LabelAutoFitStyle;

            axis.LabelStyle = LabelStyle.ToLabelStyle();
            axis.LineColor = LineColor;
            axis.LineDashStyle = LineDashStyle;
            axis.LineWidth = LineWidth;

            axis.LogarithmBase = LogarithmBase;
            axis.MajorGrid = MajorGrid.ToGrid();
            axis.MajorTickMark = MajorTickMark.ToTickMark();
            axis.Maximum = Maximum;

            axis.Minimum = Minimum;
            axis.MinorGrid = MinorGrid.ToGrid();
            axis.MinorTickMark = MinorTickMark.ToTickMark();

            axis.ScaleBreakStyle = ScaleBreakStyle.ToAxisScaleBreakStyle();
            axis.ScaleView = ScaleView.ToAxisScaleView();
            axis.ScrollBar = ScrollBar.ToAxisScrollBar();
            axis.TextOrientation = TextOrientation;

            axis.Title = Title;
            axis.TitleAlignment = TitleAlignment;
            axis.TitleFont = TitleFont;
            axis.TitleForeColor = TitleForeColor;

            axis.ToolTip = ToolTip;

            axis.Tag = Tag;

            if (this.StripLines != null && this.StripLines.Count > 0)
            {
                foreach (var node in this.StripLines)
                {
                    axis.StripLines.Add(node.ToStripLine());
                }
            }
            if (this.CustomLabels != null && this.CustomLabels.Count > 0)
            {
                foreach (var node in this.CustomLabels)
                {
                    axis.CustomLabels.Add(node.ToCustomLabel());
                }
            }
        }
    }

    internal class CustomLabelEditor : UITypeEditor
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
                    PMSAxis control = null;
                    if (context.Instance.GetType() == typeof(PMSAxis))
                        control = (PMSAxis)context.Instance;

                    FormCustomLabelCollection form1 = new FormCustomLabelCollection();
                    form1.DataList = control.CustomLabels;
                    if (DialogResult.OK == editorService.ShowDialog(form1))
                    {
                        control.CustomLabels = form1.DataList;
                    }
                    return value;
                }
            }

            return value;
        }
    }
    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class PMSCustomLabel
    {
        //
        // Summary:
        //     Gets or sets the text color of the custom label.
        //
        // Returns:
        //     A System.Drawing.Color value that represents the text color of a custom label.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [DefaultValue(typeof(Color), "")]
        [NotifyParentProperty(true)]
        [TypeConverter(typeof(ColorConverter))]
        [Category("外观")]
        public Color ForeColor { get; set; }
        //
        // Summary:
        //     Gets or sets the beginning position, in axis coordinates, of the custom label.
        //
        // Returns:
        //     A double value that represents the beginning of the axis range that the label
        //     is applied to.
        [Bindable(true)]
        [DefaultValue(0)]
        [Category("外观")]
        public double FromPosition { get; set; }
        //
        // Summary:
        //     Gets or sets a property that specifies whether custom tick marks and grid
        //     lines will be drawn in the center of the label.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.GridTickTypes enumeration
        //     value.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.FlagsEnumUITypeEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Category("外观")]
        [DefaultValue(GridTickTypes.None)]
        public GridTickTypes GridTicks { get; set; }

        //
        // Summary:
        //     Gets or sets the label mark for a custom label. This applies to labels in
        //     the second row only.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.LabelMarkStyle enumeration
        //     value that determines the type of label mark, if any, to be used.
        [Bindable(true)]
        [Category("外观")]
        [DefaultValue(LabelMarkStyle.None)]
        public LabelMarkStyle LabelMark { get; set; }
        //
        // Summary:
        //     Gets or sets the marker color for the custom label
        //
        // Returns:
        //     A System.Drawing.Color value that represents the marker color of a custom
        //     label.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Category("外观")]
        [DefaultValue(typeof(Color), "")]
        [NotifyParentProperty(true)]
        [TypeConverter(typeof(ColorConverter))]
        public Color MarkColor { get; set; }

        //
        // Summary:
        //     Gets or sets the index of the custom label row.
        //
        // Returns:
        //     An integer value that specifies the index, 0 to 10, of the custom label row.
        [Bindable(true)]
        [Category("外观")]
        [DefaultValue(0)]
        public int RowIndex { get; set; }
        //
        // Summary:
        //     Gets or sets the custom label text.
        //
        // Returns:
        //     A string value that represents the label text.
        [DefaultValue("")]
        [Category("外观")]
        [Bindable(true)]
        public string Text { get; set; }
        //
        // Summary:
        //     Gets or sets the custom label tooltip text.
        //
        // Returns:
        //     A string that represents the tooltip text.
        [DefaultValue("")]
        [Bindable(true)]
        [Category("外观")]
        public string ToolTip { get; set; }
        //
        // Summary:
        //     Gets or sets the starting position of the custom label, in axis coordinates.
        //
        // Returns:
        //     A double value that represents the starting position.
        [Bindable(true)]
        [DefaultValue(0)]
        [Category("外观")]
        public double ToPosition { get; set; }
        //
        // Summary:
        //     Gets the name of the strip line.
        //
        // Returns:
        //     A string value that represents the name of the strip line.
        [Bindable(false)]
        [DefaultValue("StripLine")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category("杂项")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (Parent != null && Parent is List<string>)
                {
                    List<string> parent = (List<string>)Parent;

                    foreach (var node in parent)
                    {
                        if (value == node)
                            throw new Exception("属性名已存在！");
                    }
                    name = value;
                    return;
                }
                name = value;
            }
        }

        private string name;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Parent { get; set; }


        public override string ToString()
        {
            return this.Name;
        }

        public PMSCustomLabel(CustomLabel stripLine)
        {
            if (stripLine == null)
            {
                this.ForeColor = Color.Empty;
                this.FromPosition = 0;
                this.GridTicks = GridTickTypes.None;
                this.LabelMark = LabelMarkStyle.None;
                this.MarkColor = Color.Empty;
                this.Name = "CustomLabel";
                this.RowIndex = 0;
                this.Text = "";
                this.ToolTip = "";
                this.ToPosition = 0;
                this.Name = "CustomLabel1";
            }
            else
            {
                this.ForeColor = stripLine.ForeColor;
                this.FromPosition = stripLine.FromPosition;
                this.GridTicks = stripLine.GridTicks;
                this.LabelMark = stripLine.LabelMark;
                this.MarkColor = stripLine.MarkColor;
                this.Name = stripLine.Name;
                this.RowIndex = stripLine.RowIndex;
                this.Text = stripLine.Text;
                this.ToPosition = stripLine.ToPosition;
                this.ToolTip = stripLine.ToolTip;
                this.Name = stripLine.Name;
            }
        }
        public CustomLabel ToCustomLabel()
        {
            CustomLabel stripLine = new CustomLabel();
            SetCustomLabel(stripLine);
            return stripLine;
        }
        public void SetCustomLabel(CustomLabel stripLine)
        {
            if (stripLine == null)
            {
                return;
            }
            else
            {
                stripLine.ForeColor = this.ForeColor;
                stripLine.FromPosition = this.FromPosition;
                stripLine.GridTicks = this.GridTicks;
                stripLine.LabelMark = this.LabelMark;
                stripLine.Name = this.Name;
                stripLine.RowIndex = this.RowIndex;
                stripLine.Text = this.Text;
                stripLine.ToPosition = this.ToPosition;
                stripLine.ToolTip = this.ToolTip;
                stripLine.Name = this.Name;
            }
        }
    }

    internal class StripLineCollectionEditor : UITypeEditor
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
                    PMSAxis control = null;
                    if (context.Instance.GetType() == typeof(PMSAxis))
                        control = (PMSAxis)context.Instance;

                    FormStripLineCollection form1 = new FormStripLineCollection();
                    if (control != null)
                    {
                        form1.DataList = control.StripLines;
                        if (DialogResult.OK == editorService.ShowDialog(form1))
                        {
                            control.StripLines = form1.DataList;
                        }
                        return value;
                    }
                }
            }

            return value;
        }
    }
    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class PMSStripLine
    {
        // Summary:
        //     Gets or sets the background color of the strip line.
        //
        // Returns:
        //     A System.Drawing.Color structure. The default color is System.Drawing.Color.White.
        [TypeConverter(typeof(ColorConverter))]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [DefaultValue(typeof(Color), "")]
        [Category("外观")]
        public Color BackColor { get; set; }
        //
        // Summary:
        //     Gets or sets the gradient style of the strip line.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.GradientStyle enumeration.
        //     The default value is System.Windows.Forms.DataVisualization.Charting.GradientStyle.None.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.GradientEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Category("外观")]
        [DefaultValue(GradientStyle.None)]
        public GradientStyle BackGradientStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the hatching style of the strip line.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle enumeration.
        //     The default value is System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle.None.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.HatchStyleEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Category("外观")]
        [DefaultValue(ChartHatchStyle.None)]
        public ChartHatchStyle BackHatchStyle { get; set; }

        //
        // Summary:
        //     Gets or sets the secondary color of the strip line background.
        //
        // Returns:
        //     A System.Drawing.Color value used for the secondary color of a background
        //     with hatching or gradient fill. The default color is System.Drawing.Color.Empty.
        [DefaultValue(typeof(Color), "")]
        [TypeConverter(typeof(ColorConverter))]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Category("外观")]
        public Color BackSecondaryColor { get; set; }
        //
        // Summary:
        //     Gets or sets the border color of a strip line.
        //
        // Returns:
        //     A System.Drawing.Color value. The default color is System.Drawing.Color.Empty.
        [TypeConverter(typeof(ColorConverter))]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [DefaultValue(typeof(Color), "")]
        [Category("外观")]
        public Color BorderColor { get; set; }
        //
        // Summary:
        //     Gets or sets the border style of the strip line.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartDashStyle enumeration
        //     value that determines the border style of the strip line.
        [Bindable(true)]
        [Category("外观")]
        public ChartDashStyle BorderDashStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the border width of the strip line.
        //
        // Returns:
        //     An integer value that determines the width of the strip line's border, in
        //     pixels.
        [DefaultValue(1)]
        [Bindable(true)]
        [Category("外观")]
        public int BorderWidth { get; set; }
        //
        // Summary:
        //     Gets or sets the font used for the strip line text.
        //
        // Returns:
        //     A System.Drawing.Font value that represents the font of the strip line text.
        [DefaultValue(typeof(Font), "Microsoft Sans Serif, 8pt")]
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [Category("标题")]
        public Font Font { get; set; }
        //
        // Summary:
        //     Gets or sets the color of the strip line text.
        //
        // Returns:
        //     A System.Drawing.Color value that represents the text color of a strip line.
        //     The default value is System.Drawing.Color.Black.
        [DefaultValue(typeof(Color), "Black")]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [TypeConverter(typeof(ColorConverter))]
        [Category("标题")]
        public Color ForeColor { get; set; }
        //
        // Summary:
        //     Gets or sets the interval for a strip line, and determines if the strip line
        //     is drawn once or repeatedly.
        //
        // Returns:
        //     A double value that represents the interval between strip lines. The default
        //     value is zero.
        [Bindable(true)]
        [DefaultValue(0)]
        [Category("数据")]
        [RefreshProperties(RefreshProperties.All)]
        public double Interval { get; set; }
        //
        // Summary:
        //     Gets or sets the offset of grid lines, tick marks, strip lines and axis labels.
        //
        // Returns:
        //     A double value that represents the interval offset.
        [Bindable(true)]
        [DefaultValue(0)]
        [Category("数据")]
        public double IntervalOffset { get; set; }
        //
        // Summary:
        //     Gets or sets the interval offset type of the strip line.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType enumeration
        //     value that indicates the interval type. The default value is System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto.
        [RefreshProperties(RefreshProperties.All)]
        [Bindable(true)]
        [Category("数据")]
        [DefaultValue(DateTimeIntervalType.Auto)]
        public DateTimeIntervalType IntervalOffsetType { get; set; }
        //
        // Summary:
        //     Gets or sets the interval type of a System.Windows.Forms.DataVisualization.Charting.StripLine
        //     object.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType enumeration
        //     value that indicates the interval type. The default value is System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto.
        [RefreshProperties(RefreshProperties.All)]
        [Bindable(true)]
        [Category("数据")]
        [DefaultValue(DateTimeIntervalType.Auto)]
        public DateTimeIntervalType IntervalType { get; set; }
        //
        // Summary:
        //     Gets the name of the strip line.
        //
        // Returns:
        //     A string value that represents the name of the strip line.
        [Bindable(false)]
        [DefaultValue("StripLine")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category("杂项")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (Parent != null && Parent is List<string>)
                {
                    List<string> parent = (List<string>)Parent;

                    foreach (var node in parent)
                    {
                        if (value == node)
                            throw new Exception("属性名已存在！");
                    }
                    name = value;
                    return;
                }
                name = value;
            }
        }

        private string name;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Parent { get; set; }
        //
        // Summary:
        //     Gets or sets the width of a strip line.
        //
        // Returns:
        //     A double value that determines whether a strip or a line is drawn. The default
        //     value is 0.0.
        [DefaultValue(0)]
        [Bindable(true)]
        [Category("外观")]
        public double StripWidth { get; set; }
        //
        // Summary:
        //     Gets or sets the unit of measurement for the System.Windows.Forms.DataVisualization.Charting.StripLine.StripWidth
        //     property.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType enumeration
        //     value that indicates the width type. The default value is System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto.
        [Bindable(true)]
        [RefreshProperties(RefreshProperties.All)]
        [Category("外观")]
        [DefaultValue(DateTimeIntervalType.Auto)]
        public DateTimeIntervalType StripWidthType { get; set; }
        //
        // Summary:
        //     Gets or sets the text of the strip line.
        //
        // Returns:
        //     A string value that represents the text of a strip line.
        [DefaultValue("")]
        [Category("标题")]
        [NotifyParentProperty(true)]
        [Bindable(true)]
        public string Text { get; set; }
        //
        // Summary:
        //     Gets or sets the text alignment of the strip line.
        //
        // Returns:
        //     A System.Drawing.StringAlignment value that represents the alignment of the
        //     strip line text.
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [DefaultValue(typeof(StringAlignment), "Far")]
        [Category("标题")]
        public StringAlignment TextAlignment { get; set; }
        //
        // Summary:
        //     Gets or sets the text line alignment of the strip line.
        //
        // Returns:
        //     A System.Drawing.StringAlignment value that represents the alignment of the
        //     text line.
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [Category("标题")]
        [DefaultValue(typeof(StringAlignment), "Near")]
        public StringAlignment TextLineAlignment { get; set; }
        //
        // Summary:
        //     Gets or sets the text orientation.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.StripLine.TextOrientation
        //     value that represents the alignment of the text orientation.
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [Category("标题")]
        [DefaultValue(TextOrientation.Auto)]
        public TextOrientation TextOrientation { get; set; }
        //
        // Summary:
        //     Gets or sets the tooltip of a strip line.
        //
        // Returns:
        //     A string value that represents the tooltip of the strip line.
        [Bindable(true)]
        [DefaultValue("")]
        public string ToolTip { get; set; }


        public override string ToString()
        {
            return this.Name;
        }

        public PMSStripLine(StripLine stripLine)
        {
            if (stripLine == null)
            {
                this.BackColor = Color.Empty;
                this.BackGradientStyle = GradientStyle.None;
                this.BackHatchStyle = ChartHatchStyle.None;
                this.BackSecondaryColor = Color.Empty;
                this.BorderColor = Color.Empty;
                this.BorderDashStyle = ChartDashStyle.Solid;
                this.BorderWidth = 1;
                this.Font = new Font("Microsoft Sans Serif", 8);
                this.ForeColor = Color.Black;
                this.Interval = 0;
                this.IntervalOffset = 0;
                this.IntervalOffsetType = DateTimeIntervalType.Auto;
                this.IntervalType = DateTimeIntervalType.Auto;
                this.StripWidth = 0;
                this.StripWidthType = DateTimeIntervalType.Auto;
                this.Text = "";
                this.TextAlignment = StringAlignment.Far;
                this.TextLineAlignment = StringAlignment.Near;
                this.TextOrientation = TextOrientation.Auto;
                this.ToolTip = "";
                this.Name = "StripLine1";
            }
            else
            {
                this.BackColor = stripLine.BackColor;
                this.BackGradientStyle = stripLine.BackGradientStyle;
                this.BackHatchStyle = stripLine.BackHatchStyle;
                this.BackSecondaryColor = stripLine.BackSecondaryColor;
                this.BorderColor = stripLine.BorderColor;
                this.BorderDashStyle = stripLine.BorderDashStyle;
                this.BorderWidth = stripLine.BorderWidth;
                this.Font = stripLine.Font;
                this.ForeColor = stripLine.ForeColor;
                this.Interval = stripLine.Interval;
                this.IntervalOffset = stripLine.IntervalOffset;
                this.IntervalOffsetType = stripLine.IntervalOffsetType;
                this.IntervalType = stripLine.IntervalType;
                this.StripWidth = stripLine.StripWidth;
                this.StripWidthType = stripLine.StripWidthType;
                this.Text = stripLine.Text;
                this.TextAlignment = stripLine.TextAlignment;
                this.TextLineAlignment = stripLine.TextLineAlignment;
                this.TextOrientation = stripLine.TextOrientation;
                this.ToolTip = stripLine.ToolTip;
                this.Name = stripLine.Name;
            }
        }
        public StripLine ToStripLine()
        {
            StripLine stripLine = new StripLine();
            SetStripLine(stripLine);
            return stripLine;
        }
        public void SetStripLine(StripLine stripLine)
        {
            if (stripLine == null)
            {
                return;
            }
            else
            {
                stripLine.BackColor = this.BackColor;
                stripLine.BackGradientStyle = this.BackGradientStyle;
                stripLine.BackHatchStyle = this.BackHatchStyle;
                stripLine.BackSecondaryColor = this.BackSecondaryColor;
                stripLine.BorderColor = this.BorderColor;
                stripLine.BorderDashStyle = this.BorderDashStyle;
                stripLine.BorderWidth = this.BorderWidth;
                stripLine.Font = this.Font;
                stripLine.ForeColor = this.ForeColor;
                stripLine.Interval = this.Interval;
                stripLine.IntervalOffset = this.IntervalOffset;
                stripLine.IntervalOffsetType = this.IntervalOffsetType;
                stripLine.IntervalType = this.IntervalType;
                stripLine.StripWidth = this.StripWidth;
                stripLine.StripWidthType = this.StripWidthType;
                stripLine.Text = this.Text;
                stripLine.TextAlignment = this.TextAlignment;
                stripLine.TextLineAlignment = this.TextLineAlignment;
                stripLine.TextOrientation = this.TextOrientation;
                stripLine.ToolTip = this.ToolTip;
                //stripLine.Name = this.Name;
            }
        }
    }

    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class PMSSeries
    {
        //
        // Summary:
        //     Gets or sets a flag that indicates whether the series will be visible on
        //     the rendered chart.
        //
        // Returns:
        //     True if the series will be visible on the rendered chart, otherwise false.
        //     The default value is true.
        [Bindable(true)]
        [DefaultValue(true)]
        [ParenthesizePropertyName(true)]
        [NotifyParentProperty(true)]
        [Category("外观")]
        [Description("Gets or sets a flag that indicates whether the series will be visible on the rendered chart.")]
        public bool Enabled { get; set; }
        //
        // Summary:
        //     Gets or sets the background gradient style.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.GradientStyle enumeration
        //     value.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.GradientEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Category("外观")]
        [DefaultValue(GradientStyle.None)]
        [Description("Gets or sets the background gradient style.")]
        public GradientStyle BackGradientStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the background hatching style.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle enumeration
        //     value.
        [Bindable(true)]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.HatchStyleEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Category("外观")]
        [DefaultValue(ChartHatchStyle.None)]
        [Description("Gets or sets the background hatching style.")]
        public ChartHatchStyle BackHatchStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the background image of the data point.
        //
        // Returns:
        //     A string value.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ImageValueEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Category("外观")]
        [Browsable(false)]
        [Description("Gets or sets the background image of the data point.")]
        public string BackImage { get; set; }
        //
        // Summary:
        //     Gets or sets the alignment of the background image, which is used with the
        //     System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.Unscaled
        //     drawing mode.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle
        //     enumeration value.
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [Category("外观")]
        [Browsable(false)]
        [Description("Gets or sets the alignment of the background image, which is used with the System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.Unscaled drawing mode.")]
        public ChartImageAlignmentStyle BackImageAlignment { get; set; }
        //
        // Summary:
        //     Gets or sets a color that will be replaced with a transparent color when
        //     the background image is drawn.
        //
        // Returns:
        //     A System.Drawing.Color value that will be replaced with a transparent color
        //     when the image is drawn.
        [TypeConverter(typeof(ColorConverter))]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [Category("外观")]
        [Browsable(false)]
        [Description("Gets or sets a color that will be replaced with a transparent color when the background image is drawn.")]
        public Color BackImageTransparentColor { get; set; }
        //
        // Summary:
        //     Gets or sets the drawing mode of the background image.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode enumeration
        //     value that defines the drawing mode of the image.
        [Bindable(true)]
        [Category("外观")]
        [Browsable(false)]
        [Description("Gets or sets the drawing mode of the background image.")]
        public ChartImageWrapMode BackImageWrapMode { get; set; }
        //
        // Summary:
        //     Gets or sets the secondary background color.
        //
        // Returns:
        //     A System.Drawing.Color value.
        [TypeConverter(typeof(ColorConverter))]
        [Bindable(true)]
        [Category("外观")]
        [Description("Gets or sets the secondary background color.")]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        public Color BackSecondaryColor { get; set; }
        //
        // Summary:
        //     Gets or sets the border color of the data point.
        //
        // Returns:
        //     A System.Drawing.Color value.
        [TypeConverter(typeof(ColorConverter))]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Category("外观")]
        [Description(" Gets or sets the border color of the data point.")]
        public Color BorderColor { get; set; }
        //
        // Summary:
        //     Gets or sets the border style of the data point.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartDashStyle enumeration
        //     value.
        [Bindable(true)]
        [Category("外观")]
        [DefaultValue(ChartDashStyle.Solid)]
        [Description(" Gets or sets the border style of the data point.")]
        public ChartDashStyle BorderDashStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the border width of the data point.
        //
        // Returns:
        //     An integer value.
        [Bindable(true)]
        [Category("外观")]
        [DefaultValue(1)]
        [Description("Gets or sets the border width of the data point.")]
        public int BorderWidth { get; set; }
        //
        // Summary:
        //     Gets or sets the color of the data point.
        //
        // Returns:
        //     A System.Drawing.Color value.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Category("外观")]
        [Description("Gets or sets the color of the data point.")]
        [TypeConverter(typeof(ColorConverter))]
        public Color Color { get; set; }
        //
        // Summary:
        //     Gets or sets the font of the data point.
        //
        // Returns:
        //     A System.Drawing.Font value.
        [Bindable(true)]
        [Category("标签外观")]
        [Description("Gets or sets the font of the data point.")]
        [DefaultValue(typeof(Font), "Microsoft Sans Serif, 8pt")]
        public Font Font { get; set; }
        [Bindable(true)]
        [Category("标签")]
        [Description("If true shows points value as label.")]
        [DefaultValue(false)]
        public bool IsValueShownAsLabel { get; set; }
        [Bindable(true)]
        [Category("图例")]
        [Description("Indicates that item is shown in the legend.")]
        [DefaultValue(true)]
        public bool IsVisibleInLegend { get; set; }
        //
        // Summary:
        //     Gets or sets the text of the data point label.
        //
        // Returns:
        //     A string value.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.KeywordsStringEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Category("标签")]
        [Description("Gets or sets the text of the data point label.")]
        public virtual string Label { get; set; }
        //
        // Summary:
        //     Gets or sets the angle of the data point label.
        //
        // Returns:
        //     An integer value that represents the angle of the label.
        [Bindable(true)]
        [Category("标签外观")]
        [Description("Gets or sets the angle of the data point label.")]
        [DefaultValue(0)]
        public int LabelAngle { get; set; }
        //
        // Summary:
        //     Gets or sets the background color of the data point label.
        //
        // Returns:
        //     A System.Drawing.Color value.
        [TypeConverter(typeof(ColorConverter))]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [DefaultValue(typeof(Color), "")]
        [Bindable(true)]
        [Category("标签外观")]
        [Description("Gets or sets the background color of the data point label.")]
        public Color LabelBackColor { get; set; }
        //
        // Summary:
        //     Gets or sets the border color of the data point label.
        //
        // Returns:
        //     A System.Drawing.Color value.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [TypeConverter(typeof(ColorConverter))]
        [DefaultValue(typeof(Color), "")]
        [Category("标签外观")]
        [Description("Gets or sets the border color of the data point label.")]
        public Color LabelBorderColor { get; set; }
        //
        // Summary:
        //     Gets or sets the border style of the label.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartDashStyle enumeration
        //     value.
        [Bindable(true)]
        [Category("标签外观")]
        [Description("Gets or sets the border style of the label.")]
        [DefaultValue(ChartDashStyle.Solid)]
        public ChartDashStyle LabelBorderDashStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the border width of the label.
        //
        // Returns:
        //     An integer value that represents the width of the border.
        [Bindable(true)]
        [Category("标签外观")]
        [Description("Gets or sets the border width of the label.")]
        [DefaultValue(1)]
        public int LabelBorderWidth { get; set; }
        //
        // Summary:
        //     Gets or sets the text color of the label.
        //
        // Returns:
        //     A System.Drawing.Color value.
        [TypeConverter(typeof(ColorConverter))]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Category("标签外观")]
        [DefaultValue(typeof(Color), "Black")]
        [Description("Gets or sets the text color of the label.")]
        public Color LabelForeColor { get; set; }
        //
        // Summary:
        //     Gets or sets the format of the data point label.
        //
        // Returns:
        //     A string value that represents the format of the label.
        [Bindable(true)]
        [Category("标签")]
        [Description("Gets or sets the format of the data point label.")]
        public string LabelFormat { get; set; }
        //
        // Summary:
        //     Gets or sets the tooltip for the data point label.
        //
        // Returns:
        //     A string value that represents the tooltip for the data point label.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.KeywordsStringEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Category("标签")]
        [Description("Gets or sets the tooltip for the data point label.")]
        public string LabelToolTip { get; set; }
        //
        // Summary:
        //     Gets or sets the text of the item in the legend.
        //
        // Returns:
        //     A string value.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.KeywordsStringEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Category("图例")]
        [Description("Gets or sets the text of the item in the legend.")]
        public string LegendText { get; set; }
        //
        // Summary:
        //     Gets or sets the tooltip of the item in the legend.
        //
        // Returns:
        //     A string value.
        [Bindable(true)]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.KeywordsStringEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Category("图例")]
        [Description("Gets or sets the tooltip of the item in the legend.")]
        public string LegendToolTip { get; set; }
        //
        // Summary:
        //     Gets or sets the border color of the marker.
        //
        // Returns:
        //     A System.Drawing.Color value.
        [TypeConverter(typeof(ColorConverter))]
        [Bindable(true)]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [RefreshProperties(RefreshProperties.All)]
        [Category("标记")]
        [Description("Gets or sets the border color of the marker.")]
        public Color MarkerBorderColor { get; set; }
        //
        // Summary:
        //     Gets or sets the border width of the marker.
        //
        // Returns:
        //     An integer value.
        [Bindable(true)]
        [Category("标记")]
        [Description("Gets or sets the border width of the marker.")]
        [DefaultValue(1)]
        public int MarkerBorderWidth { get; set; }
        //
        // Summary:
        //     Gets or sets the marker color.
        //
        // Returns:
        //     A System.Drawing.Color value.
        [TypeConverter(typeof(ColorConverter))]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [RefreshProperties(RefreshProperties.All)]
        [Bindable(true)]
        [Category("标记")]
        [Description("Gets or sets the marker color.")]
        public Color MarkerColor { get; set; }
        //
        // Summary:
        //     Gets or sets the marker image.
        //
        // Returns:
        //     A string value.
        [RefreshProperties(RefreshProperties.All)]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ImageValueEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Category("标记")]
        [Description("Gets or sets the marker image.")]
        [Browsable(false)]
        public string MarkerImage { get; set; }
        //
        // Summary:
        //     Gets or sets the color that will be replaced with a transparent color when
        //     the marker image is drawn.
        //
        // Returns:
        //     A System.Drawing.Color value.
        [RefreshProperties(RefreshProperties.All)]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [TypeConverter(typeof(ColorConverter))]
        [Bindable(true)]
        [Category("标记")]
        [Browsable(false)]
        [Description("Gets or sets the color that will be replaced with a transparent color when the marker image is drawn.")]
        public Color MarkerImageTransparentColor { get; set; }
        //
        // Summary:
        //     Gets or sets the size of the marker.
        //
        // Returns:
        //     An integer value.
        [Bindable(true)]
        [RefreshProperties(RefreshProperties.All)]
        [Category("标记")]
        [Description("Gets or sets the size of the marker.")]
        [DefaultValue(5)]
        public int MarkerSize { get; set; }
        //
        // Summary:
        //     Gets or sets the marker style.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.MarkerStyle enumeration
        //     value.
        [RefreshProperties(RefreshProperties.All)]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.MarkerStyleEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Category("标记")]
        [Description("Gets or sets the marker style.")]
        [DefaultValue(MarkerStyle.None)]
        public MarkerStyle MarkerStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the tooltip.
        //
        // Returns:
        //     A string value.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.KeywordsStringEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Category("其他")]
        [Description("Gets or sets the tooltip.")]
        public string ToolTip { get; set; }

        //
        // Summary:
        //     Gets or sets the drawing style of points marked as empty.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.DataPointCustomProperties
        //     object.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Bindable(true)]
        [Browsable(false)]
        public PMSDataPointCustomProperties EmptyPointStyle { get; set; }
        //
        // Summary:
        //     Gets or sets a flag that indicates whether data point indices will be used
        //     for the X-values.
        //
        // Returns:
        //     True if the indices of data points that belong to the series will be used
        //     for X-values; false if they will not. The default value is false.
        [Bindable(true)]
        [DefaultValue(false)]
        [Category("数据")]
        [Description("Gets or sets a flag that indicates whether data point indices will be used for the X-values.")]
        public bool IsXValueIndexed { get; set; }
        //
        // Summary:
        //     Gets or sets the name of the series associated with the System.Windows.Forms.DataVisualization.Charting.Legend
        //     object.
        //
        // Returns:
        //     A string value that represents the name of a System.Windows.Forms.DataVisualization.Charting.Legend
        //     object.
        [DefaultValue("")]
        [Bindable(true)]
        [Category("图例")]
        [Browsable(false)]
        [Description("Gets or sets the name of the series associated with the System.Windows.Forms.DataVisualization.Charting.Legend object.")]
        public string Legend { get; set; }
        //
        // Summary:
        //     Gets or sets a value that determines how often to display data point markers.
        //
        // Returns:
        //     An integer value that determines how often to display data point markers.
        //     The default value is one (1).
        [DefaultValue(1)]
        [Bindable(true)]
        [Category("标识")]
        [Description("Gets or sets a value that determines how often to display data point markers.")]
        public int MarkerStep { get; set; }

        //
        // Summary:
        //     Gets or sets the color palette of a System.Windows.Forms.DataVisualization.Charting.Series
        //     object.
        //
        // Returns:
        //     An System.Windows.Forms.DataVisualization.Charting.ChartColorPalette enumeration
        //     value that determines the palette for the data series.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ColorPaletteEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Category("外观")]
        [Description("Gets or sets the color palette of a System.Windows.Forms.DataVisualization.Charting.Series object.")]
        [DefaultValue(ChartColorPalette.None)]
        public ChartColorPalette Palette { get; set; }

        //
        // Summary:
        //     Gets or sets the shadow color of a series.
        //
        // Returns:
        //     A System.Drawing.Color value. The default color is System.Drawing.Color.Black.
        [Bindable(true)]
        [DefaultValue(typeof(Color), "128,0,0,0")]
        [Category("外观")]
        [Description("Gets or sets the shadow color of a series.")]
        public Color ShadowColor { get; set; }
        //
        // Summary:
        //     Gets or sets the shadow offset, in pixels, of a series.
        //
        // Returns:
        //     An integer value that represents the shadow offset of the series, in pixels.
        [DefaultValue(0)]
        [Bindable(true)]
        [Category("外观")]
        [Description("Gets or sets the shadow offset, in pixels, of a series.")]
        public int ShadowOffset { get; set; }
        //
        // Summary:
        //     Gets or sets the style of the smart labels.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.SmartLabelStyle enumeration
        //     value.
        [Bindable(true)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category("标签")]
        [Description("Gets or sets the style of the smart labels.")]
        public PMSSmartLabelStyle SmartLabelStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the X-axis type of the series.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.Series.XAxisType enumeration
        //     value that determines if the series uses the primary or secondary X-axis.
        [Bindable(true)]
        [Category("轴")]
        [Description(" Gets or sets the X-axis type of the series.")]
        [DefaultValue(AxisType.Primary)]
        public AxisType XAxisType { get; set; }
        //
        // Summary:
        //     Gets or sets the value types plotted along the X-axis.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartValueType enumeration
        //     value that determines the type of X-value data stored by the series. The
        //     default value is System.Windows.Forms.DataVisualization.Charting.ChartValueType.Auto.
        [Bindable(true)]
        [Category("数据")]
        [Description(" Gets or sets the value types plotted along the X-axis.")]
        [DefaultValue(ChartValueType.Auto)]
        public ChartValueType XValueType { get; set; }
        //
        // Summary:
        //     Gets or sets the Y-axis type of a series.
        //
        // Returns:
        //     An System.Windows.Forms.DataVisualization.Charting.AxisType enumeration value
        //     that determines if a series uses the primary or secondary Y-axis.
        [Bindable(true)]
        [Category("轴")]
        [Description(" Gets or sets the Y-axis type of the series.")]
        [DefaultValue(AxisType.Primary)]
        public AxisType YAxisType { get; set; }
        //
        // Summary:
        //     Gets or sets the type of Y-value data in the data points stored by a series.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartValueType enumeration
        //     value that determines the type of Y-value data stored by the series. The
        //     default value is System.Windows.Forms.DataVisualization.Charting.ChartValueType.Auto.
        [Bindable(true)]
        [Category("数据")]
        [DefaultValue(ChartValueType.Auto)]
        [Description(" Gets or sets the type of Y-value data in the data points stored by a series.")]
        public ChartValueType YValueType { get; set; }

        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartTypeEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Description("Gets or sets the name of the chart type used to draw the series.")]
        [Category("数据")]
        [DefaultValue(SeriesChartType.Column)]
        public virtual SeriesChartType ChartType { get; set; }


        ////[Editor("System.Windows.Forms.Design.DataVisualization.Charting.PointsEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        ////[Category("数据")]
        ////[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        ////[SRCategory("CategoryAttributeData")]
        //[Editor("System.Windows.Forms.Design.DataVisualization.Charting.DataPointCollectionEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        //[Bindable(true)]
        ////[SRDescription("DescriptionAttributeSeries_Points")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //public DataPointCollection Points { get; set; }

        [Category("杂项")]
        [DefaultValue("series1")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (Parent != null && Parent is TreeNode)
                {
                    TreeNode parent = (TreeNode)Parent;

                    foreach (TreeNode node in parent.Nodes)
                    {
                        if (value == node.Text)
                            throw new Exception("属性名已存在！");
                    }
                    name = value;
                    return;
                }
                name = value;
            }
        }
        [Browsable(false)]
        public object Parent { get; set; }

        [Browsable(false)]
        public object SeriesDataList { get; set; }

        /// <summary>
        /// 11.30 李琦 添加“CustomProperties”属性以控制特定图标的特定属性。
        /// </summary>
        [Browsable(false)]
        public virtual string CustomProperties { get; set; }

        private string name;
        public PMSSeries(Series series)
        {
            SeriesDataList = new List<string>();
            if (series != null)
            {
                this.BackGradientStyle = series.BackGradientStyle;
                this.BackHatchStyle = series.BackHatchStyle;
                this.BackImage = series.BackImage;
                this.BackImageAlignment = series.BackImageAlignment;
                this.BackImageTransparentColor = series.BackImageTransparentColor;
                this.BackImageWrapMode = series.BackImageWrapMode;
                this.BackSecondaryColor = series.BackSecondaryColor;
                this.BorderColor = series.BorderColor;
                this.BorderDashStyle = series.BorderDashStyle;
                this.BorderWidth = series.BorderWidth;
                this.Color = series.Color;
                //this.EmptyPointStyle = new PMSDataPointCustomProperties(series.EmptyPointStyle);
                this.Enabled = series.Enabled;
                this.Font = series.Font;
                this.IsValueShownAsLabel = series.IsValueShownAsLabel;
                this.IsVisibleInLegend = series.IsVisibleInLegend;
                this.IsXValueIndexed = series.IsXValueIndexed;
                this.Label = series.Label;
                this.LabelAngle = series.LabelAngle;
                this.LabelBackColor = series.LabelBackColor;
                this.LabelBorderColor = series.LabelBorderColor;
                this.LabelBorderDashStyle = series.LabelBorderDashStyle;
                this.LabelBorderWidth = series.LabelBorderWidth;
                this.LabelForeColor = series.LabelForeColor;
                this.LabelFormat = series.LabelFormat;
                this.LabelToolTip = series.LabelToolTip;
                //this.Legend = series.Legend;
                this.LegendText = series.LegendText;
                this.LegendToolTip = series.LegendToolTip;
                this.MarkerBorderColor = series.MarkerBorderColor;
                this.MarkerBorderWidth = series.MarkerBorderWidth;
                this.MarkerColor = series.MarkerColor;
                this.MarkerImage = series.MarkerImage;
                this.MarkerImageTransparentColor = series.MarkerImageTransparentColor;
                this.MarkerSize = series.MarkerSize;
                this.MarkerStep = series.MarkerStep;
                this.MarkerStyle = series.MarkerStyle;
                this.Palette = series.Palette;
                this.ShadowColor = series.ShadowColor;
                this.ShadowOffset = series.ShadowOffset;
                this.SmartLabelStyle = new PMSSmartLabelStyle(series.SmartLabelStyle);
                this.ToolTip = series.ToolTip;
                this.XAxisType = series.XAxisType;
                this.XValueType = series.XValueType;
                this.YAxisType = series.YAxisType;
                this.YValueType = series.YValueType;
                try
                {
                    this.ChartType = series.ChartType;
                }
                catch { }
                this.Name = series.Name;
                //11.30 李琦 添加“CustomProperties”属性以控制特定图标的特定属性。
                try
                {
                    this.CustomProperties = series.CustomProperties;
                }
                catch { }
                //if (this.Points != null)
                //{
                //    if (this.Points.Count > 0)
                //    {
                //        this.Points.Clear();
                //    }
                //    if (series.Points != null)
                //    {
                //        foreach (DataPoint point in series.Points)
                //        {
                //            this.Points.AddXY(point.XValue, point.YValues[0]);
                //        }
                //    }
                //}

            }
            else
            {
                Enabled = true;
                BackGradientStyle = GradientStyle.None;
                BackHatchStyle = ChartHatchStyle.None;
                BorderDashStyle = ChartDashStyle.Solid;
                BorderWidth = 1;
                Font = new Font("Microsoft Sans Serif", 8);
                IsValueShownAsLabel = false;
                IsVisibleInLegend = true;
                LabelAngle = 0;
                LabelBorderDashStyle = ChartDashStyle.Solid;
                LabelBorderWidth = 1;
                LabelForeColor = Color.Black;
                MarkerBorderWidth = 1;
                MarkerSize = 5;
                MarkerStyle = MarkerStyle.None;
                IsXValueIndexed = false;
                MarkerStep = 1;
                SmartLabelStyle = new PMSSmartLabelStyle(null);
                try
                {
                    ChartType = SeriesChartType.Spline;
                }
                catch { }
                //this.EmptyPointStyle = new PMSDataPointCustomProperties(null);
                Name = "series1";
            }
        }

        public Series ToSeries()
        {
            Series series = new Series();
            SetSeriesValue(series);
            return series;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="series"></param>
        /// <param name="bClone">clone时给name属性赋值</param>
        public void SetSeriesValue(Series series)
        {
            if (series == null)
                return;
            series.BackGradientStyle = BackGradientStyle;
            series.BackHatchStyle = BackHatchStyle;
            series.BackImage = BackImage;
            series.BackImageAlignment = BackImageAlignment;
            series.BackImageTransparentColor = BackImageTransparentColor;
            series.BackImageWrapMode = BackImageWrapMode;
            series.BackSecondaryColor = BackSecondaryColor;
            series.BorderColor = BorderColor;
            series.BorderDashStyle = BorderDashStyle;
            series.BorderWidth = BorderWidth;
            series.Color = Color;
            //-series.EmptyPointStyle = EmptyPointStyle.ToPMSDataPointCustomProperties();
            series.Enabled = Enabled;
            series.Font = Font;
            series.IsValueShownAsLabel = IsValueShownAsLabel;
            series.IsVisibleInLegend = IsVisibleInLegend;
            series.IsXValueIndexed = IsXValueIndexed;
            series.Label = Label;
            series.LabelAngle = LabelAngle;
            series.LabelBackColor = LabelBackColor;
            series.LabelBorderColor = LabelBorderColor;
            series.LabelBorderDashStyle = LabelBorderDashStyle;
            series.LabelBorderWidth = LabelBorderWidth;
            series.LabelForeColor = LabelForeColor;
            series.LabelFormat = LabelFormat;
            if (!string.IsNullOrEmpty(LabelToolTip))
                series.LabelToolTip = LabelToolTip;
            //series.Legend = Legend;

            if (!string.IsNullOrEmpty(LegendText))
                series.LegendText = LegendText;
            if (!string.IsNullOrEmpty(LegendText))
                series.LegendToolTip = LegendText;
            series.MarkerBorderColor = MarkerBorderColor;
            series.MarkerBorderWidth = MarkerBorderWidth;
            series.MarkerColor = MarkerColor;
            series.MarkerImage = MarkerImage;
            series.MarkerImageTransparentColor = MarkerImageTransparentColor;
            series.MarkerSize = MarkerSize;
            series.MarkerStep = MarkerStep;
            series.MarkerStyle = MarkerStyle;
            series.Palette = Palette;
            series.ShadowColor = ShadowColor;
            series.ShadowOffset = ShadowOffset;
            series.SmartLabelStyle = SmartLabelStyle.ToSmartLabelStyle();
            if (!string.IsNullOrEmpty(ToolTip))
                series.ToolTip = ToolTip;
            series.XAxisType = XAxisType;
            series.XValueType = XValueType;
            series.YAxisType = YAxisType;
            series.YValueType = YValueType;
            series.ChartType = ChartType;
            if (!string.IsNullOrEmpty(Name))
            {
                series.Name = Name;
            }
            try
            {
                series.CustomProperties = CustomProperties;
            }
            catch { }
            //series.Name = Name;

            //if (series.Points != null)
            //{
            //    if (series.Points.Count > 0)
            //    {
            //        series.Points.Clear();
            //    }
            //    if (this.Points != null)
            //    {
            //        foreach (DataPoint point in this.Points)
            //        {
            //            series.Points.AddXY(point.XValue, point.YValues[0]);
            //        }
            //    }
            //}
        }

        public void SetSeriseStyle(Series series)
        {
            if (series == null)
                return;
            series.BackGradientStyle = BackGradientStyle;
            series.BackHatchStyle = BackHatchStyle;
            series.BackImage = BackImage;
            series.BackImageAlignment = BackImageAlignment;
            series.BackImageTransparentColor = BackImageTransparentColor;
            series.BackImageWrapMode = BackImageWrapMode;
            series.BackSecondaryColor = BackSecondaryColor;
            series.BorderColor = BorderColor;
            series.BorderDashStyle = BorderDashStyle;
            series.BorderWidth = BorderWidth;
            //series.Color = Color;
            //-series.EmptyPointStyle = EmptyPointStyle.ToPMSDataPointCustomProperties();
            series.Enabled = Enabled;
            series.Font = Font;
            series.IsValueShownAsLabel = IsValueShownAsLabel;
            series.IsVisibleInLegend = IsVisibleInLegend;
            series.IsXValueIndexed = IsXValueIndexed;
            series.Label = Label;
            series.LabelAngle = LabelAngle;
            series.LabelBackColor = LabelBackColor;
            series.LabelBorderColor = LabelBorderColor;
            series.LabelBorderDashStyle = LabelBorderDashStyle;
            series.LabelBorderWidth = LabelBorderWidth;
            series.LabelForeColor = LabelForeColor;
            series.LabelFormat = LabelFormat;
            if (!string.IsNullOrEmpty(LabelToolTip))
                series.LabelToolTip = LabelToolTip;
            //series.Legend = Legend;

            if (!string.IsNullOrEmpty(LegendText))
                series.LegendText = LegendText;
            if (!string.IsNullOrEmpty(LegendText))
                series.LegendToolTip = LegendText;
            series.MarkerBorderColor = MarkerBorderColor;
            series.MarkerBorderWidth = MarkerBorderWidth;
            series.MarkerColor = MarkerColor;
            series.MarkerImage = MarkerImage;
            series.MarkerImageTransparentColor = MarkerImageTransparentColor;
            series.MarkerSize = MarkerSize;
            series.MarkerStep = MarkerStep;
            series.MarkerStyle = MarkerStyle;
            series.Palette = Palette;
            series.ShadowColor = ShadowColor;
            series.ShadowOffset = ShadowOffset;
            series.SmartLabelStyle = SmartLabelStyle.ToSmartLabelStyle();
            if (!string.IsNullOrEmpty(ToolTip))
                series.ToolTip = ToolTip;
            series.XAxisType = XAxisType;
            series.XValueType = XValueType;
            series.YAxisType = YAxisType;
            series.YValueType = YValueType;
            //series.ChartType = ChartType;
            //if (!string.IsNullOrEmpty(Name))
            //{
            //    series.Name = Name;
            //}
            try
            {
                series.CustomProperties = CustomProperties;
            }
            catch { }
        }

        public override string ToString()
        {
            return "Series";
        }
    }

    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class PMSDataPointCustomProperties
    {
        // Summary:
        //     Gets or sets the text of the X-axis label for the data point, series or an
        //     empty point. This property is only used if a custom label has not been specified
        //     for the relevant System.Windows.Forms.DataVisualization.Charting.Axis object.
        //
        // Returns:
        //     A string value that represents the text of the X-axis label.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.KeywordsStringEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        public string AxisLabel { get; set; }
        //
        // Summary:
        //     Gets or sets the background gradient style.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.GradientStyle enumeration
        //     value.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.GradientEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Description("Gets or sets the background gradient style.")]
        public GradientStyle BackGradientStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the background hatching style.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle enumeration
        //     value.
        [Bindable(true)]
        [Description(" Gets or sets the background hatching style.")]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.HatchStyleEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        public ChartHatchStyle BackHatchStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the background image of the data point.
        //
        // Returns:
        //     A string value.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ImageValueEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Description("Gets or sets the background image of the data point.")]
        public string BackImage { get; set; }
        //
        // Summary:
        //     Gets or sets the alignment of the background image, which is used with the
        //     System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.Unscaled
        //     drawing mode.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle
        //     enumeration value.
        [Bindable(true)]
        [NotifyParentProperty(true)]
        //[Description("Gets or sets the background gradient style.")]
        public ChartImageAlignmentStyle BackImageAlignment { get; set; }
        //
        // Summary:
        //     Gets or sets a color that will be replaced with a transparent color when
        //     the background image is drawn.
        //
        // Returns:
        //     A System.Drawing.Color value that will be replaced with a transparent color
        //     when the image is drawn.
        [TypeConverter(typeof(ColorConverter))]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [NotifyParentProperty(true)]
        [Bindable(true)]
        //[Description("Gets or sets the background gradient style.")]
        public Color BackImageTransparentColor { get; set; }
        //
        // Summary:
        //     Gets or sets the drawing mode of the background image.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode enumeration
        //     value that defines the drawing mode of the image.
        [Bindable(true)]
        [Description("Gets or sets the drawing mode of the background image.")]
        public ChartImageWrapMode BackImageWrapMode { get; set; }
        //
        // Summary:
        //     Gets or sets the secondary background color.
        //
        // Returns:
        //     A System.Drawing.Color value.
        [TypeConverter(typeof(ColorConverter))]
        [Bindable(true)]
        [Description("Gets or sets the secondary background color.")]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        public Color BackSecondaryColor { get; set; }
        //
        // Summary:
        //     Gets or sets the border color of the data point.
        //
        // Returns:
        //     A System.Drawing.Color value.
        [TypeConverter(typeof(ColorConverter))]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Description(" Gets or sets the border color of the data point.")]
        public Color BorderColor { get; set; }
        //
        // Summary:
        //     Gets or sets the border style of the data point.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartDashStyle enumeration
        //     value.
        [Bindable(true)]
        [Description("Gets or sets the border style of the data point.")]
        public ChartDashStyle BorderDashStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the border width of the data point.
        //
        // Returns:
        //     An integer value.
        [Bindable(true)]
        [Description("Gets or sets the background gradient style.")]
        public int BorderWidth { get; set; }
        //
        // Summary:
        //     Gets or sets the color of the data point.
        //
        // Returns:
        //     A System.Drawing.Color value.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [TypeConverter(typeof(ColorConverter))]
        [Description("Gets or sets the background gradient style.")]
        public Color Color { get; set; }


        public PMSDataPointCustomProperties(DataPointCustomProperties CustomProperties)
        {
            if (CustomProperties != null)
            {
                this.AxisLabel = CustomProperties.AxisLabel;
                this.BackGradientStyle = CustomProperties.BackGradientStyle;
                this.BackHatchStyle = CustomProperties.BackHatchStyle;
                this.BackImage = CustomProperties.BackImage;
                this.BackImageAlignment = CustomProperties.BackImageAlignment;
                this.BackImageTransparentColor = CustomProperties.BackImageTransparentColor;
                this.BackImageWrapMode = CustomProperties.BackImageWrapMode;
                this.BackSecondaryColor = CustomProperties.BackSecondaryColor;
                this.BorderColor = CustomProperties.BorderColor;
                this.BorderDashStyle = CustomProperties.BorderDashStyle;
                this.BorderWidth = CustomProperties.BorderWidth;
                this.Color = CustomProperties.Color;
            }

        }
        public DataPointCustomProperties ToPMSDataPointCustomProperties()
        {
            DataPointCustomProperties dataPointCustomProperties = new DataPointCustomProperties();

            return dataPointCustomProperties;
        }
        public void SetCustomProperties(DataPointCustomProperties CustomProperties)
        {
            if (CustomProperties == null)
                return;
            CustomProperties.AxisLabel = AxisLabel;
            CustomProperties.BackGradientStyle = BackGradientStyle;
            CustomProperties.BackHatchStyle = BackHatchStyle;
            CustomProperties.BackImage = BackImage;
            CustomProperties.BackImageAlignment = BackImageAlignment;
            CustomProperties.BackImageTransparentColor = BackImageTransparentColor;
            CustomProperties.BackImageWrapMode = BackImageWrapMode;
            CustomProperties.BackSecondaryColor = BackSecondaryColor;
            CustomProperties.BorderColor = BorderColor;
            CustomProperties.BorderDashStyle = BorderDashStyle;
            CustomProperties.BorderWidth = BorderWidth;
            CustomProperties.Color = Color;
        }
        public override string ToString()
        {
            return "PMSDataPointCustomProperties";
        }
    }

    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class PMSSmartLabelStyle
    {
        // Summary:
        //     Gets or sets a flag that specifies whether a System.Windows.Forms.DataVisualization.Charting.SmartLabelStyle
        //     object can be drawn outside the plotting area.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.LabelOutsidePlotAreaStyle
        //     enumeration value.
        [Bindable(true)]
        [DefaultValue(LabelOutsidePlotAreaStyle.Partial)]
        [Description("Gets or sets a flag that specifies whether a System.Windows.Forms.DataVisualization.Charting.SmartLabelStyle object can be drawn outside the plotting area.")]
        public LabelOutsidePlotAreaStyle AllowOutsidePlotArea { get; set; }
        //
        // Summary:
        //     Gets or sets the background color of the label callout.
        //
        // Returns:
        //     A System.Drawing.Color value.
        [DefaultValue(typeof(Color), "Transparent")]
        [TypeConverter(typeof(ColorConverter))]
        [Bindable(true)]
        [Description("Gets or sets the background color of the label callout.")]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        public Color CalloutBackColor { get; set; }
        //
        // Summary:
        //     Gets or sets the anchor cap style of the label callout line.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.LineAnchorCapStyle enumeration
        //     value.
        [Bindable(true)]
        [DefaultValue(LineAnchorCapStyle.Arrow)]
        [Description("Gets or sets the anchor cap style of the label callout line.")]
        public LineAnchorCapStyle CalloutLineAnchorCapStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the color of the label callout line.
        //
        // Returns:
        //     A System.Drawing.Color value.
        [TypeConverter(typeof(ColorConverter))]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [DefaultValue(typeof(Color), "Black")]
        [Description("Gets or sets the color of the label callout line.")]
        public Color CalloutLineColor { get; set; }
        //
        // Summary:
        //     Gets or sets the style of the label callout line.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartDashStyle enumeration
        //     value.
        [Bindable(true)]
        [DefaultValue(ChartDashStyle.Solid)]
        [Description(" Gets or sets the style of the label callout line.")]
        public ChartDashStyle CalloutLineDashStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the width of the label callout line.
        //
        // Returns:
        //     An integer value.
        [DefaultValue(1)]
        [Bindable(true)]
        [Description(" Gets or sets the width of the label callout line.")]
        public int CalloutLineWidth { get; set; }
        //
        // Summary:
        //     Gets or sets the callout style of the repositioned System.Windows.Forms.DataVisualization.Charting.SmartLabelStyle
        //     object.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.LabelCalloutStyle enumeration
        //     value.
        [Bindable(true)]
        [Description("Gets or sets the callout style of the repositioned System.Windows.Forms.DataVisualization.Charting.SmartLabelStyle object.")]
        [DefaultValue(LabelCalloutStyle.Underlined)]
        public LabelCalloutStyle CalloutStyle { get; set; }
        //
        // Summary:
        //     Gets or sets a flag that indicates whether a System.Windows.Forms.DataVisualization.Charting.SmartLabelStyle
        //     algorithm is enabled.
        //
        // Returns:
        //     True if an algorithm will be applied to prevent data point labels from overlapping.
        //     False if overlapping of data point labels will not be prevented. The default
        //     value is false.
        [Bindable(true)]
        [ParenthesizePropertyName(true)]
        [DefaultValue(true)]
        [Description("Gets or sets a flag that indicates whether a System.Windows.Forms.DataVisualization.Charting.SmartLabelStyle algorithm is enabled.")]
        public bool Enabled { get; set; }
        //
        // Summary:
        //     Gets or sets a flag that specifies whether the point labels are allowed to
        //     overlap a point marker.
        //
        // Returns:
        //     True if data point labels are permitted to overlap point markers. False if
        //     data point labels will be prevented from overlapping the point markers. The
        //     default value is false.
        [Bindable(true)]
        [DefaultValue(false)]
        [Description("Gets or sets a flag that specifies whether the point labels are allowed to overlap a point marker.")]
        public bool IsMarkerOverlappingAllowed { get; set; }
        //
        // Summary:
        //     Gets or sets a flag that indicates whether overlapped labels that cannot
        //     be repositioned will be hidden.
        //
        // Returns:
        //     True if labels will be hidden if they overlap. False if overlapped labels
        //     will not be hidden. The default value is false.
        [DefaultValue(true)]
        [Bindable(true)]
        [Description("Gets or sets a flag that indicates whether overlapped labels that cannot be repositioned will be hidden.")]
        public bool IsOverlappedHidden { get; set; }
        //
        // Summary:
        //     Gets or sets the maximum distance, in pixels, that the overlapped System.Windows.Forms.DataVisualization.Charting.SmartLabelStyle
        //     object can be moved from the marker.
        //
        // Returns:
        //     A double value.
        [Bindable(true)]
        [DefaultValue(30)]
        [Description("Gets or sets the maximum distance, in pixels, that the overlapped System.Windows.Forms.DataVisualization.Charting.SmartLabelStyle object can be moved from the marker.")]
        public double MaxMovingDistance { get; set; }
        //
        // Summary:
        //     Gets or sets the minimum distance, in pixels, that the overlapped System.Windows.Forms.DataVisualization.Charting.SmartLabelStyle
        //     object can be moved from the marker.
        //
        // Returns:
        //     A double value.
        [Bindable(true)]
        [DefaultValue(0)]
        [Description("Gets or sets the minimum distance, in pixels, that the overlapped System.Windows.Forms.DataVisualization.Charting.SmartLabelStyle object can be moved from the marker.")]
        public double MinMovingDistance { get; set; }
        //
        // Summary:
        //     Gets or sets the direction(s) in which the overlapped System.Windows.Forms.DataVisualization.Charting.SmartLabelStyle
        //     object is allowed to be moved.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles enumeration
        //     value or multiple bitwise OR'd System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles
        //     values. The default is the bitwise OR of all System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles
        //     values to allow repositioning in all directions except to the center of the
        //     data point.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.FlagsEnumUITypeEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Description("Gets or sets the direction(s) in which the overlapped System.Windows.Forms.DataVisualization.Charting.SmartLabelStyle object is allowed to be moved.")]
        [DefaultValue(typeof(LabelAlignmentStyles), "Top, Bottom, Right, Left, TopLeft, TopRight, BottomLeft, BottomRight")]
        public LabelAlignmentStyles MovingDirection { get; set; }

        public PMSSmartLabelStyle(SmartLabelStyle smartLabelStyle)
        {
            if (smartLabelStyle != null)
            {
                this.AllowOutsidePlotArea = smartLabelStyle.AllowOutsidePlotArea;
                this.CalloutBackColor = smartLabelStyle.CalloutBackColor;
                this.CalloutLineAnchorCapStyle = smartLabelStyle.CalloutLineAnchorCapStyle;
                this.CalloutLineColor = smartLabelStyle.CalloutLineColor;
                this.CalloutLineDashStyle = smartLabelStyle.CalloutLineDashStyle;
                this.CalloutLineWidth = smartLabelStyle.CalloutLineWidth;
                this.CalloutStyle = smartLabelStyle.CalloutStyle;
                this.Enabled = smartLabelStyle.Enabled;
                this.IsMarkerOverlappingAllowed = smartLabelStyle.IsMarkerOverlappingAllowed;
                this.IsOverlappedHidden = smartLabelStyle.IsOverlappedHidden;
                this.MaxMovingDistance = smartLabelStyle.MaxMovingDistance;
                this.MinMovingDistance = smartLabelStyle.MinMovingDistance;
                this.MovingDirection = smartLabelStyle.MovingDirection;
            }
            else
            {
                this.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Partial;
                this.CalloutBackColor = Color.Transparent;
                this.CalloutLineAnchorCapStyle = LineAnchorCapStyle.Arrow;
                this.CalloutLineColor = Color.Black;
                this.CalloutLineDashStyle = ChartDashStyle.Solid;
                this.CalloutLineWidth = 1;
                this.CalloutStyle = LabelCalloutStyle.Underlined;
                this.Enabled = true;
                this.IsMarkerOverlappingAllowed = false;
                this.IsOverlappedHidden = true;
                this.MaxMovingDistance = 30;
                this.MinMovingDistance = 0;
                this.MovingDirection = LabelAlignmentStyles.Bottom | LabelAlignmentStyles.BottomLeft
                    | LabelAlignmentStyles.BottomRight | LabelAlignmentStyles.Left
                    | LabelAlignmentStyles.Right | LabelAlignmentStyles.Top
                    | LabelAlignmentStyles.TopLeft | LabelAlignmentStyles.TopRight;
            }
        }

        public SmartLabelStyle ToSmartLabelStyle()
        {
            SmartLabelStyle smartLabelStyle = new SmartLabelStyle();
            SetSmartLabelStyle(smartLabelStyle);
            return smartLabelStyle;
        }

        public void SetSmartLabelStyle(SmartLabelStyle smartLabelStyle)
        {
            smartLabelStyle.AllowOutsidePlotArea = AllowOutsidePlotArea;
            smartLabelStyle.CalloutBackColor = CalloutBackColor;
            smartLabelStyle.CalloutLineAnchorCapStyle = CalloutLineAnchorCapStyle;
            smartLabelStyle.CalloutLineColor = CalloutLineColor;
            smartLabelStyle.CalloutLineDashStyle = CalloutLineDashStyle;
            smartLabelStyle.CalloutLineWidth = CalloutLineWidth;
            smartLabelStyle.CalloutStyle = CalloutStyle;
            smartLabelStyle.Enabled = Enabled;
            smartLabelStyle.IsMarkerOverlappingAllowed = IsMarkerOverlappingAllowed;
            smartLabelStyle.IsOverlappedHidden = IsOverlappedHidden;
            smartLabelStyle.MaxMovingDistance = MaxMovingDistance;
            smartLabelStyle.MinMovingDistance = MinMovingDistance;
            smartLabelStyle.MovingDirection = MovingDirection;
        }
        public override string ToString()
        {
            return "SmartLabelStyle";
        }
    }


    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class PMSChartArea
    {
        // Summary:
        //     Gets or sets the alignment orientation of a chart area.
        //
        // Returns:
        //     An System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations
        //     enumeration value.
        [Bindable(true)]
        [Category("对齐方式")]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.FlagsEnumUITypeEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [DefaultValue(AreaAlignmentOrientations.Vertical)]
        [Description("Gets or sets the alignment orientation of a chart area.")]
        public virtual AreaAlignmentOrientations AlignmentOrientation { get; set; }
        //
        // Summary:
        //     Gets or sets the alignment style of the System.Windows.Forms.DataVisualization.Charting.ChartArea.
        //
        // Returns:
        //     An System.Windows.Forms.DataVisualization.Charting.AreaAlignmentStyles enumeration
        //     value that can represent either a single value or the bitwise-OR of multiple
        //     values that define the alignment to use.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.FlagsEnumUITypeEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Category("对齐方式")]
        [DefaultValue(AreaAlignmentStyles.All)]
        [Description("Gets or sets the alignment style of the System.Windows.Forms.DataVisualization.Charting.ChartArea.")]
        public virtual AreaAlignmentStyles AlignmentStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the name of the System.Windows.Forms.DataVisualization.Charting.ChartArea
        //     object to which this chart area should be aligned.
        //
        // Returns:
        //     A string value that represents the name of a System.Windows.Forms.DataVisualization.Charting.ChartArea
        //     object to which this chart area should be aligned. The default is the empty
        //     string.
        [Bindable(true)]
        [DefaultValue("NotSet")]
        [Category("对齐方式")]
        [Description("Gets or sets the name of the System.Windows.Forms.DataVisualization.Charting.ChartArea object to which this chart area should be aligned.")]
        public virtual string AlignWithChartArea { get; set; }
        //
        // Summary:
        //     Gets or sets a System.Windows.Forms.DataVisualization.Charting.ChartArea3DStyle
        //     object, which is used to implement 3D for all series in a chart area.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartArea3DStyle object
        //     that implements 3D for all series plotted in a chart area.
        [Bindable(true)]
        [DefaultValue("")]
        [Category("三维")]
        [Description(" Gets or sets a System.Windows.Forms.DataVisualization.Charting.ChartArea3DStyle object, which is used to implement 3D for all series in a chart area.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PMSChartArea3DStyle Area3DStyle { get; set; }
        //
        // Summary:
        //     Gets or sets an array that represents all axes for a chart area.
        //
        // Returns:
        //     An array of System.Windows.Forms.DataVisualization.Charting.Axis objects
        //     that represents all axes used by a chart area.
        [Editor(typeof(AxesEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Bindable(true)]
        [Category("轴")]
        [Description("Gets or sets an array that represents all axes for a chart area.")]
        public virtual string Axes
        {
            get
            {
                return "Axes";
            }
            set
            {
                ;
            }
        }
        //
        // Summary:
        //     Gets or sets an System.Windows.Forms.DataVisualization.Charting.Axis object
        //     that represents the primary X-axis.
        //
        // Returns:
        //     An System.Windows.Forms.DataVisualization.Charting.Axis object that represents
        //     the primary X-axis.
        [Browsable(false)]
        [Description("")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Bindable(true)]
        public virtual PMSAxis AxisX { get; set; }
        //
        // Summary:
        //     Gets or sets an System.Windows.Forms.DataVisualization.Charting.Axis object
        //     that represents the secondary X-axis.
        //
        // Returns:
        //     An System.Windows.Forms.DataVisualization.Charting.Axis object that represents
        //     the secondary X-axis.
        [Bindable(true)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PMSAxis AxisX2 { get; set; }
        //
        // Summary:
        //     Gets or sets an System.Windows.Forms.DataVisualization.Charting.Axis object
        //     that represents the primary Y-axis.
        //
        // Returns:
        //     An System.Windows.Forms.DataVisualization.Charting.Axis object that represents
        //     the primary Y-axis.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Bindable(true)]
        [Browsable(false)]
        public virtual PMSAxis AxisY { get; set; }
        //
        // Summary:
        //     Gets or sets an System.Windows.Forms.DataVisualization.Charting.Axis object
        //     that represents the secondary Y-axis.
        //
        // Returns:
        //     An System.Windows.Forms.DataVisualization.Charting.Axis object that represents
        //     the secondary Y-axis.
        [Bindable(true)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PMSAxis AxisY2 { get; set; }
        //
        // Summary:
        //     Gets or sets the background color of a System.Windows.Forms.DataVisualization.Charting.ChartArea
        //     object.
        //
        // Returns:
        //     A System.Drawing.Color value. The default color is System.Drawing.Color.White.
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [Category("外观")]
        [TypeConverter(typeof(ColorConverter))]
        [DefaultValue(typeof(Color), "")]
        [Description("Gets or sets the background color of a System.Windows.Forms.DataVisualization.Charting.ChartArea object.")]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        public virtual Color BackColor { get; set; }
        //
        // Summary:
        //     Gets or sets the orientation for the background gradient of a chart area,
        //     and also determines whether or not a gradient is used.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.GradientStyle enumeration.
        //     The default value is System.Windows.Forms.DataVisualization.Charting.GradientStyle.None.
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [Description("Gets or sets the orientation for the background gradient of a chart area,  and also determines whether or not a gradient is used.")]
        [DefaultValue(GradientStyle.None)]
        [Category("外观")]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.GradientEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        public virtual GradientStyle BackGradientStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the hatching style of a System.Windows.Forms.DataVisualization.Charting.ChartArea
        //     object.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle enumeration.
        //     The default value is System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle.None.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.HatchStyleEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Category("外观")]
        [NotifyParentProperty(true)]
        [Description("Gets or sets the hatching style of a System.Windows.Forms.DataVisualization.Charting.ChartArea object.")]
        [DefaultValue(ChartHatchStyle.None)]
        public virtual ChartHatchStyle BackHatchStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the background image of a System.Windows.Forms.DataVisualization.Charting.ChartArea
        //     object.
        //
        // Returns:
        //     A string value that represents the URL of an image file. The default is an
        //     empty string.
        [NotifyParentProperty(true)]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ImageValueEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [DefaultValue("")]
        [Browsable(false)]
        [Category("外观")]
        public virtual string BackImage { get; set; }
        //
        // Summary:
        //     Gets or sets the alignment of a System.Windows.Forms.DataVisualization.Charting.ChartArea
        //     background image.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle
        //     enumeration value. The default value is System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.TopLeft.
        [Bindable(true)]
        [Browsable(false)]
        [Category("外观")]
        [NotifyParentProperty(true)]
        public virtual ChartImageAlignmentStyle BackImageAlignment { get; set; }
        //
        // Summary:
        //     Gets or sets the color of a System.Windows.Forms.DataVisualization.Charting.ChartArea
        //     object background image that will be drawn as transparent.
        //
        // Returns:
        //     A System.Drawing.Color value. The default value is System.Drawing.Color.Empty.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [DefaultValue(typeof(Color), "")]
        [NotifyParentProperty(true)]
        [Browsable(false)]
        [Category("外观")]
        [TypeConverter(typeof(ColorConverter))]
        public virtual Color BackImageTransparentColor { get; set; }
        //
        // Summary:
        //     Gets or sets the drawing mode for the background image of a System.Windows.Forms.DataVisualization.Charting.ChartArea
        //     object.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode enumeration
        //     value. The default value is System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.Tile.
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [Browsable(false)]
        [Category("外观")]
        public virtual ChartImageWrapMode BackImageWrapMode { get; set; }
        //
        // Summary:
        //     Gets or sets the secondary color for the background of a System.Windows.Forms.DataVisualization.Charting.ChartArea
        //     object.
        //
        // Returns:
        //     A System.Drawing.Color value. The default value is System.Drawing.Color.Empty.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Category("外观")]
        [DefaultValue(typeof(Color), "")]
        [NotifyParentProperty(true)]
        [Description("Gets or sets the secondary color for the background of a System.Windows.Forms.DataVisualization.Charting.ChartArea  object.")]
        [TypeConverter(typeof(ColorConverter))]
        public virtual Color BackSecondaryColor { get; set; }
        //
        // Summary:
        //     Gets or sets the border color of a System.Windows.Forms.DataVisualization.Charting.ChartArea
        //     object.
        //
        // Returns:
        //     A T:System.Drawing.Color value. The default color is System.Drawing.Color.Empty.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Category("外观")]
        [DefaultValue(typeof(Color), "Black")]
        [NotifyParentProperty(true)]
        [TypeConverter(typeof(ColorConverter))]
        [Description("Gets or sets the border color of a System.Windows.Forms.DataVisualization.Charting.ChartArea object.")]
        public virtual Color BorderColor { get; set; }
        //
        // Summary:
        //     Gets or sets the border style of a System.Windows.Forms.DataVisualization.Charting.ChartArea
        //     object.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartDashStyle enumeration
        //     value that determines the border style of the chart area.
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [Category("外观")]
        [DefaultValue(ChartDashStyle.Solid)]
        [Description("Gets or sets the border style of a System.Windows.Forms.DataVisualization.Charting.ChartArea object.")]
        public virtual ChartDashStyle BorderDashStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the border width of a System.Windows.Forms.DataVisualization.Charting.ChartArea
        //     object.
        //
        // Returns:
        //     An integer value that determines the border width, in pixels, of the chart
        //     area.
        [NotifyParentProperty(true)]
        [DefaultValue(1)]
        [Bindable(true)]
        [Category("外观")]
        [Description("Gets or sets the border width of a System.Windows.Forms.DataVisualization.Charting.ChartArea  object.")]
        public virtual int BorderWidth { get; set; }
        //
        // Summary:
        //     Gets or sets a System.Windows.Forms.DataVisualization.Charting.Cursor object
        //     that is used for cursors and selected ranges along the X-axis.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.Cursor object used for
        //     cursors and selected ranges along the X-axis.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Bindable(true)]
        [Category("光标")]
        [Description("Gets or sets a System.Windows.Forms.DataVisualization.Charting.Cursor object that is used for cursors and selected ranges along the X-axis.")]
        public virtual PMSCursor CursorX { get; set; }
        //
        // Summary:
        //     Gets or sets a System.Windows.Forms.DataVisualization.Charting.Cursor object
        //     that is used for cursors and selected ranges along the Y-axis.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.Cursor object used for
        //     cursors and selected ranges along the X-axis.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Bindable(true)]
        [DefaultValue("")]
        [Category("光标")]
        [Description("Gets or sets a System.Windows.Forms.DataVisualization.Charting.Cursor object that is used for cursors and selected ranges along the Y-axis.")]
        public virtual PMSCursor CursorY { get; set; }
        //
        // Summary:
        //     Gets or sets an System.Windows.Forms.DataVisualization.Charting.ElementPosition
        //     object, which defines the inner plot position of a chart area object.
        //
        // Returns:
        //     An System.Windows.Forms.DataVisualization.Charting.ElementPosition object,
        //     which defines the inner plot position of a System.Windows.Forms.DataVisualization.Charting.ChartArea
        //     object.
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [Category("外观")]
        [Description(" Gets or sets an System.Windows.Forms.DataVisualization.Charting.ElementPosition object, which defines the inner plot position of a chart area object.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PMSElementPosition InnerPlotPosition { get; set; }
        //
        // Summary:
        //     Gets or sets a flag that determines if the labels of the relevant chart area
        //     axes are of equal size.
        //
        // Returns:
        //     True if all axes that have their System.Windows.Forms.DataVisualization.Charting.Axis.IsLabelAutoFit
        //     property set to true will display their labels using the same font size,
        //     otherwise false.
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [DefaultValue(false)]
        [Category("外观")]
        [Description("Gets or sets a flag that determines if the labels of the relevant chart area axes are of equal size.")]
        public virtual bool IsSameFontSizeForAllAxes { get; set; }

        //
        // Summary:
        //     Gets or sets an System.Windows.Forms.DataVisualization.Charting.ElementPosition
        //     object that defines the position of a System.Windows.Forms.DataVisualization.Charting.ChartArea
        //     object within the System.Windows.Forms.DataVisualization.Charting.Chart.
        //
        // Returns:
        //     An System.Windows.Forms.DataVisualization.Charting.ElementPosition object
        //     that defines the position of a chart area object within the chart image.
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [NotifyParentProperty(true)]
        [Category("外观")]
        [Description("Gets or sets an System.Windows.Forms.DataVisualization.Charting.ElementPosition object that defines the position of a System.Windows.Forms.DataVisualization.Charting.ChartArea object within the System.Windows.Forms.DataVisualization.Charting.Chart.")]
        public virtual PMSElementPosition Position { get; set; }
        //
        // Summary:
        //     Gets or sets the shadow color of a System.Windows.Forms.DataVisualization.Charting.ChartArea
        //     object.
        //
        // Returns:
        //     A System.Drawing.Color value. The default color is System.Drawing.Color.Black.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Category("外观")]
        [DefaultValue(typeof(Color), "128,0,0,0")]
        [NotifyParentProperty(true)]
        [TypeConverter(typeof(ColorConverter))]
        [Description("Gets or sets the shadow color of a System.Windows.Forms.DataVisualization.Charting.ChartArea object.")]
        public virtual Color ShadowColor { get; set; }

        //
        // Summary:
        //     Gets or sets the shadow offset, in pixels, of a System.Windows.Forms.DataVisualization.Charting.ChartArea
        //     object.
        //
        // Returns:
        //     An integer that represents the shadow offset, in pixels, of the chart area.
        [DefaultValue(0)]
        [NotifyParentProperty(true)]
        [Description("Gets or sets the shadow offset, in pixels, of a System.Windows.Forms.DataVisualization.Charting.ChartArea object.")]
        [Bindable(true)]
        [Category("外观")]
        public virtual int ShadowOffset { get; set; }
        //
        // Summary:
        //     Gets or sets a flag that determines if a chart area is visible.
        //
        // Returns:
        //     True if the chart area is visible, otherwise false.
        [DefaultValue(true)]
        [ParenthesizePropertyName(true)]
        [Description("Gets or sets a flag that determines if a chart area is visible.")]
        [Category("外观")]
        public virtual bool Visible { get; set; }

        [DefaultValue("chartArea1")]
        [Description("Gets or sets  chart area name.")]
        [Category("杂项")]
        public virtual string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (Parent != null && Parent is TreeNode)
                {
                    TreeNode parent = (TreeNode)Parent;

                    foreach (TreeNode node in parent.Nodes)
                    {
                        if (value == node.Text)
                            throw new Exception("属性名已存在！");
                    }
                    name = value;
                    return;
                }
                name = value;
            }
        }

        private string name;

        [Browsable(false)]
        public object Parent { get; set; }

        [Browsable(false)]
        public object SeriesDataList { get; set; }

        [Browsable(false)]
        public object TitleDataList { get; set; }

        [Browsable(false)]
        public object LegendDataList { get; set; }

        public PMSChartArea(ChartArea ChartArea1)
        {
            SeriesDataList = new List<string>();
            TitleDataList = new List<string>();
            LegendDataList = new List<string>();
            if (ChartArea1 == null)
            {
                this.AlignmentOrientation = AreaAlignmentOrientations.Vertical;
                this.AlignmentStyle = AreaAlignmentStyles.All;
                this.AlignWithChartArea = "NotSet";
                this.Area3DStyle = new PMSChartArea3DStyle(null);
                //this.BackColor = Color.Transparent;
                this.BackGradientStyle = GradientStyle.None;
                this.BackHatchStyle = ChartHatchStyle.None;
                //this.BackSecondaryColor = Color.Transparent;
                this.BorderColor = Color.Black;
                this.BorderDashStyle = ChartDashStyle.Solid;
                this.BorderWidth = 1;
                this.CursorX = new PMSCursor(null);
                this.CursorY = new PMSCursor(null);
                this.InnerPlotPosition = new PMSElementPosition(null);
                this.InnerPlotPosition.X = 5f;
                this.InnerPlotPosition.Y = 2f;
                this.InnerPlotPosition.Width = 90f;
                this.InnerPlotPosition.Height = 90f;
                this.IsSameFontSizeForAllAxes = false;
                this.ShadowColor = Color.FromArgb(128, 0, 0, 0);
                this.Position = new PMSElementPosition(null);
                this.Position.X = 5f;
                this.Position.Y = 2f;
                this.Position.Width = 90f;
                this.Position.Height = 90f;
                this.ShadowOffset = 0;
                this.Visible = true;
                this.AxisX = new PMSAxis(null);
                this.AxisX2 = new PMSAxis(null);
                this.AxisY = new PMSAxis(null);
                this.AxisY2 = new PMSAxis(null);
                this.Name = "chartArea1";
            }
            else
            {
                this.AlignmentOrientation = ChartArea1.AlignmentOrientation;
                this.AlignmentStyle = ChartArea1.AlignmentStyle;
                this.AlignWithChartArea = ChartArea1.AlignWithChartArea;
                this.Area3DStyle = new PMSChartArea3DStyle(ChartArea1.Area3DStyle);
                this.BackColor = ChartArea1.BackColor;
                this.BackGradientStyle = ChartArea1.BackGradientStyle;
                this.BackHatchStyle = ChartArea1.BackHatchStyle;
                this.BackSecondaryColor = ChartArea1.BackSecondaryColor;
                this.BorderColor = ChartArea1.BorderColor;
                this.BorderDashStyle = ChartArea1.BorderDashStyle;
                this.BorderWidth = ChartArea1.BorderWidth;
                this.CursorX = new PMSCursor(ChartArea1.CursorX);
                this.CursorY = new PMSCursor(ChartArea1.CursorY);
                this.InnerPlotPosition = new PMSElementPosition(ChartArea1.InnerPlotPosition);
                this.IsSameFontSizeForAllAxes = ChartArea1.IsSameFontSizeForAllAxes;
                this.ShadowColor = ChartArea1.ShadowColor;
                this.Position = new PMSElementPosition(ChartArea1.Position);
                this.ShadowOffset = ChartArea1.ShadowOffset;
                this.Visible = ChartArea1.Visible;
                this.AxisX = new PMSAxis(ChartArea1.AxisX);
                this.AxisX2 = new PMSAxis(ChartArea1.AxisX2);
                this.AxisY = new PMSAxis(ChartArea1.AxisY);
                this.AxisY2 = new PMSAxis(ChartArea1.AxisY2);
                this.Name = ChartArea1.Name;
            }
        }

        public ChartArea ToChartArea()
        {
            ChartArea ChartArea1 = new ChartArea();
            SetChartArea(ChartArea1);
            return ChartArea1;
        }
        public void SetChartArea(ChartArea ChartArea1)
        {
            if (ChartArea1 != null)
            {
                ChartArea1.AlignmentOrientation = this.AlignmentOrientation;
                ChartArea1.AlignmentStyle = this.AlignmentStyle;
                ChartArea1.AlignWithChartArea = this.AlignWithChartArea;
                ChartArea1.Area3DStyle = this.Area3DStyle.ToChartArea3DStyle();
                ChartArea1.BackColor = this.BackColor;
                ChartArea1.BackGradientStyle = this.BackGradientStyle;
                ChartArea1.BackHatchStyle = this.BackHatchStyle;
                ChartArea1.BackSecondaryColor = this.BackSecondaryColor;
                ChartArea1.BorderColor = this.BorderColor;
                ChartArea1.BorderDashStyle = this.BorderDashStyle;
                ChartArea1.BorderWidth = this.BorderWidth;
                ChartArea1.CursorX = this.CursorX.ToCursor();
                ChartArea1.CursorY = this.CursorY.ToCursor();
                ChartArea1.InnerPlotPosition = this.InnerPlotPosition.ToElementPosition();/*//*/
                ChartArea1.Position = this.Position.ToElementPosition();
                ChartArea1.IsSameFontSizeForAllAxes = this.IsSameFontSizeForAllAxes;
                ChartArea1.ShadowColor = this.ShadowColor;
                ChartArea1.ShadowOffset = this.ShadowOffset;
                ChartArea1.Visible = this.Visible;
                ChartArea1.AxisX = this.AxisX.ToAxis();
                ChartArea1.AxisX2 = this.AxisX2.ToAxis();
                ChartArea1.AxisY = this.AxisY.ToAxis();
                ChartArea1.AxisY2 = this.AxisY2.ToAxis();
                ChartArea1.Name = this.Name;
            }
        }

        public void SetChartAreaStyle(ChartArea ChartArea1)
        {
            if (ChartArea1 != null)
            {
                ChartArea1.AlignmentOrientation = this.AlignmentOrientation;
                ChartArea1.AlignmentStyle = this.AlignmentStyle;
                ChartArea1.AlignWithChartArea = this.AlignWithChartArea;
                ChartArea1.Area3DStyle = this.Area3DStyle.ToChartArea3DStyle();
                ChartArea1.BackColor = this.BackColor;
                ChartArea1.BackGradientStyle = this.BackGradientStyle;
                ChartArea1.BackHatchStyle = this.BackHatchStyle;
                ChartArea1.BackSecondaryColor = this.BackSecondaryColor;
                ChartArea1.BorderColor = this.BorderColor;
                ChartArea1.BorderDashStyle = this.BorderDashStyle;
                ChartArea1.BorderWidth = this.BorderWidth;
                ChartArea1.CursorX = this.CursorX.ToCursor();
                ChartArea1.CursorY = this.CursorY.ToCursor();
                ChartArea1.InnerPlotPosition = this.InnerPlotPosition.ToElementPosition();/*//*/
                ChartArea1.Position = this.Position.ToElementPosition();
                ChartArea1.IsSameFontSizeForAllAxes = this.IsSameFontSizeForAllAxes;
                ChartArea1.ShadowColor = this.ShadowColor;
                ChartArea1.ShadowOffset = this.ShadowOffset;
                ChartArea1.Visible = this.Visible;
                ChartArea1.AxisX = this.AxisX.ToAxis();
                ChartArea1.AxisX2 = this.AxisX2.ToAxis();
                ChartArea1.AxisY = this.AxisY.ToAxis();
                ChartArea1.AxisY2 = this.AxisY2.ToAxis();
                //ChartArea1.Name = this.Name;
            }
        }

        public override string ToString()
        {
            return "ChartArea";
        }

        internal class AxesEditor : UITypeEditor
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
                        PMSChartArea control = null;
                        if (context.Instance.GetType() == typeof(PMSChartArea))
                            control = (PMSChartArea)context.Instance;

                        FormAixs form1 = new FormAixs();
                        Dictionary<string, PMSAxis> actionList = new Dictionary<string, PMSAxis>();
                        actionList.Add("AxisX", control.AxisX);
                        actionList.Add("AxisX2", control.AxisX2);
                        actionList.Add("AxisY", control.AxisY);
                        actionList.Add("AxisY2", control.AxisY2);
                        form1.AxisList = actionList;
                        if (DialogResult.OK == editorService.ShowDialog(form1))
                        {
                            actionList = form1.AxisList;
                            control.AxisX = actionList["AxisX"];
                            control.AxisX2 = actionList["AxisX2"];
                            control.AxisY = actionList["AxisY"];
                            control.AxisY2 = actionList["AxisY2"];
                        }
                        return value;
                    }
                }

                return value;
            }
        }

    }
    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class PMSChartArea3DStyle
    {
        // Summary:
        //     Gets or sets a flag that toggles the 3D on and off for a chart area.
        //
        // Returns:
        //     true the chart area is displayed using 3D, false if the chart area is displayed
        //     in 2D. The default value is false.
        [ParenthesizePropertyName(true)]
        [DefaultValue(false)]
        [Bindable(true)]
        [Description("Gets or sets a flag that toggles the 3D on and off for a chart area.")]
        public bool Enable3D { get; set; }
        [RefreshProperties(RefreshProperties.All)]
        [Bindable(true)]
        [DefaultValue(30)]
        public int Inclination { get; set; }
        [Bindable(true)]
        [DefaultValue(false)]
        public bool IsClustered { get; set; }
        //
        // Summary:
        //     Gets or sets a flag that determines whether a chart area is displayed using
        //     an isometric projection.
        //
        // Returns:
        //     True if the chart area is displayed using an isometric projection, otherwise
        //     false.
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(true)]
        [Bindable(true)]
        [Description("Gets or sets a flag that determines whether a chart area is displayed using an isometric projection.")]
        public bool IsRightAngleAxes { get; set; }
        //
        // Summary:
        //     Gets or sets the style of lighting for a 3D chart area.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.LightStyle enumeration
        //     value.
        [Bindable(true)]
        [Description("Gets or sets the style of lighting for a 3D chart area.")]
        [DefaultValue(typeof(LightStyle), "Simplistic")]
        public LightStyle LightStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the percent of perspective for a 3D chart area.
        //
        // Returns:
        //     An integer value that represents the percent of perspective for a 3D chart
        //     area. The allowable range is 0-100 percent, and the default is zero (0) percent.
        [RefreshProperties(RefreshProperties.All)]
        [Bindable(true)]
        [DefaultValue(0)]
        [Description("Gets or sets the percent of perspective for a 3D chart area.")]
        public int Perspective { get; set; }
        //
        // Summary:
        //     Gets or sets the depth of data points displayed in a 3D chart area.
        //
        // Returns:
        //     An integer value that represents the depth of data points. The allowable
        //     range is 0-1000 percent. The default is 100 percent.
        [Bindable(true)]
        [Description(" Gets or sets the depth of data points displayed in a 3D chart area.")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(100)]
        public int PointDepth { get; set; }
        //
        // Summary:
        //     Gets or sets the distance between series rows in a 3D chart area.
        //
        // Returns:
        //     An integer value that represents the distance between data rows (that is,
        //     the data series) in the 3D chart area. The allowable range is 0-1000 percent,
        //     and the default is 100 percent.
        [Bindable(true)]
        [Description("Gets or sets the distance between series rows in a 3D chart area.")]
        [DefaultValue(100)]
        [RefreshProperties(RefreshProperties.All)]
        public int PointGapDepth { get; set; }
        //
        // Summary:
        //     Gets or sets the angle of rotation around the vertical axes for 3D chart
        //     areas.
        //
        // Returns:
        //     An integer value.
        [RefreshProperties(RefreshProperties.All)]
        [Bindable(true)]
        [Description("Gets or sets the angle of rotation around the vertical axes for 3D chart areas.")]
        [DefaultValue(30)]
        public int Rotation { get; set; }
        //
        // Summary:
        //     Gets or sets the width of the walls displayed in a 3D chart area.
        //
        // Returns:
        //     An integer value that represents the width, in pixels, of the walls displayed
        //     in a 3D chart area.
        [Bindable(true)]
        [Description("Gets or sets the width of the walls displayed in a 3D chart area.")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(7)]
        public int WallWidth { get; set; }

        public PMSChartArea3DStyle(ChartArea3DStyle ChartArea3DStyle1)
        {
            if (ChartArea3DStyle1 == null)
            {
                this.Enable3D = true;
                this.Inclination = 30;
                this.IsClustered = false;
                this.IsRightAngleAxes = true;
                this.LightStyle = LightStyle.Simplistic;
                this.Perspective = 0;
                this.PointDepth = 100;
                this.PointGapDepth = 100;
                this.Rotation = 30;
                this.WallWidth = 7;
            }
            else
            {
                this.Enable3D = ChartArea3DStyle1.Enable3D;
                this.Inclination = ChartArea3DStyle1.Inclination;
                this.IsClustered = ChartArea3DStyle1.IsClustered;
                this.IsRightAngleAxes = ChartArea3DStyle1.IsRightAngleAxes;
                this.LightStyle = ChartArea3DStyle1.LightStyle;
                this.Perspective = ChartArea3DStyle1.Perspective;
                this.PointDepth = ChartArea3DStyle1.PointDepth;
                this.PointGapDepth = ChartArea3DStyle1.PointGapDepth;
                this.Rotation = ChartArea3DStyle1.Rotation;
                this.WallWidth = ChartArea3DStyle1.WallWidth;
            }
        }

        public ChartArea3DStyle ToChartArea3DStyle()
        {
            ChartArea3DStyle ChartArea3DStyle1 = new ChartArea3DStyle();
            SetChartArea(ChartArea3DStyle1);
            return ChartArea3DStyle1;
        }
        public void SetChartArea(ChartArea3DStyle ChartArea3DStyle1)
        {
            if (ChartArea3DStyle1 != null)
            {
                ChartArea3DStyle1.Enable3D = this.Enable3D;
                ChartArea3DStyle1.Inclination = this.Inclination;
                ChartArea3DStyle1.IsClustered = this.IsClustered;
                ChartArea3DStyle1.IsRightAngleAxes = this.IsRightAngleAxes;
                ChartArea3DStyle1.LightStyle = this.LightStyle;
                ChartArea3DStyle1.Perspective = this.Perspective;
                ChartArea3DStyle1.PointDepth = this.PointDepth;
                ChartArea3DStyle1.PointGapDepth = this.PointGapDepth;
                ChartArea3DStyle1.Rotation = this.Rotation;
                ChartArea3DStyle1.WallWidth = this.WallWidth;
            }
        }

        public override string ToString()
        {
            return "ChartArea3DStyle";
        }
    }

    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class PMSElementPosition
    {
        private float height;
        private float width;
        //
        // Summary:
        //     Gets or sets the height of a chart element.
        //
        // Returns:
        //     A float value that represents the height of the chart element.
        [Bindable(true)]
        [DefaultValue(70f)]
        [RefreshProperties(RefreshProperties.All)]
        [NotifyParentProperty(true)]
        [Description("Gets or sets the height of a chart element.")]
        public float Height
        {
            get
            {
                return height;
            }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new Exception("属性值要大于0且小于等于100!");
                }
                height = value;
            }
        }
        //
        // Summary:
        //     Gets or sets the width of a chart element.
        //
        // Returns:
        //     A float value that represents the width of a chart element.
        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(70f)]
        [Bindable(true)]
        [Description("Gets or sets the width of a chart element.")]
        public float Width
        {
            get
            {
                return width;
            }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new Exception("属性值要大于0且小于等于100!");
                }
                width = value;
            }
        }
        //
        // Summary:
        //     Gets or sets the relative X-coordinate of the top-left corner of an applicable
        //     chart element.
        //
        // Returns:
        //     A float value that represents the X-coordinate of the top-left corner of
        //     an applicable chart element.
        [DefaultValue(0f)]
        [RefreshProperties(RefreshProperties.All)]
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [Description("Gets or sets the relative X-coordinate of the top-left corner of an applicable chart element.")]
        public float X { get; set; }
        //
        // Summary:
        //     Gets or sets the relative Y-coordinate of the top-left corner of an applicable
        //     chart element.
        //
        // Returns:
        //     A float value that represents the Y-coordinate of the top-left corner of
        //     an applicable chart element.
        [RefreshProperties(RefreshProperties.All)]
        [Bindable(true)]
        [DefaultValue(0f)]
        [Description("Gets or sets the relative Y-coordinate of the top-left corner of an applicable  chart element.")]
        [NotifyParentProperty(true)]
        public float Y { get; set; }

        [DefaultValue(true)]
        public bool Auto { get; set; }

        public PMSElementPosition(ElementPosition ChartArea3DStyle1)
        {
            if (ChartArea3DStyle1 == null)
            {
                this.X = 0f;
                this.Y = 0f;
                this.Height = 70f;
                this.Width = 70f;
                Auto = true;
            }
            else
            {
                this.X = ChartArea3DStyle1.X;
                this.Y = ChartArea3DStyle1.Y;
                this.Height = ChartArea3DStyle1.Height;
                this.Width = ChartArea3DStyle1.Width;
                this.Auto = ChartArea3DStyle1.Auto;
            }
        }

        public ElementPosition ToElementPosition()
        {
            ElementPosition ChartArea3DStyle1 = new ElementPosition();
            SetChartArea(ChartArea3DStyle1);
            return ChartArea3DStyle1;
        }
        /// <summary>
        /// 2012.04.20 修改
        /// 目的:在图表控件中其区域坐标值只允许在0至100之间因此需要进行控制
        /// </summary>
        /// <param name="ChartArea1"></param>
        public void SetChartArea(ElementPosition ChartArea3DStyle1)
        {
            if (ChartArea3DStyle1 != null)
            {
                if (this.X >= 0 && this.X <= 100)
                {
                    ChartArea3DStyle1.X = this.X;
                }
                else
                {
                }
                if (this.Y >= 0 && this.Y <= 100)
                {
                    ChartArea3DStyle1.Y = this.Y;
                }
                else
                {
                }
                ChartArea3DStyle1.Height = this.Height;
                ChartArea3DStyle1.Width = this.Width;
                ChartArea3DStyle1.Auto = this.Auto;
            }
        }

        public override string ToString()
        {
            return "";
        }
    }

    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class PMSCursor
    {
        // Summary:
        //     Gets or sets a flag that determines whether scrolling will occur if a range
        //     selection operation extends beyond a boundary of the chart area.
        //
        // Returns:
        //     A Boolean value that represents whether the data view can be scrolled automatically.
        //     The default value is true.
        [DefaultValue(true)]
        [Bindable(true)]
        [Description("Gets or sets a flag that determines whether scrolling will occur if a range selection operation extends beyond a boundary of the chart area.")]
        public bool AutoScroll { get; set; }
        //
        // Summary:
        //     Gets or sets the type of axis that the cursor is attached to.
        //
        // Returns:
        //     An System.Windows.Forms.DataVisualization.Charting.AxisType enumeration value
        //     that represents whether the axis is attached to the primary or secondary
        //     axis.
        [Bindable(true)]
        [Description("Gets or sets the type of axis that the cursor is attached to.")]
        [DefaultValue(AxisType.Primary)]
        public AxisType AxisType { get; set; }
        //
        // Summary:
        //     Gets or sets the cursor interval.
        //
        // Returns:
        //     A double value representing the cursor interval.
        [Bindable(true)]
        [DefaultValue(1f)]
        [Description("Gets or sets the cursor interval.")]
        public double Interval { get; set; }
        //
        // Summary:
        //     Gets or sets the interval offset, which affects where the cursor and range
        //     selection can be drawn when they are set by a user.
        //
        // Returns:
        //     A double value that represents the offset from the interval. The default
        //     value is zero, which signifies no limitations, and negative values are allowed.
        [Bindable(true)]
        [Description("Gets or sets the interval offset, which affects where the cursor and range selection can be drawn when they are set by a user.")]
        [DefaultValue(0f)]
        public double IntervalOffset { get; set; }
        //
        // Summary:
        //     Gets or sets the interval offset type of a cursor and selected range for
        //     an axis.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType enumeration
        //     value that determines the unit of measurement for the interval offset.
        [Bindable(true)]
        [Description("Gets or sets the interval offset type of a cursor and selected range for an axis.")]
        [DefaultValue(DateTimeIntervalType.Auto)]
        public DateTimeIntervalType IntervalOffsetType { get; set; }
        //
        // Summary:
        //     Gets or sets the interval type for the cursor and selected range of an axis.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType enumeration
        //     value.
        [Bindable(true)]
        [DefaultValue(DateTimeIntervalType.Auto)]
        public DateTimeIntervalType IntervalType { get; set; }
        [Bindable(true)]
        [Description("Gets or sets the interval type for the cursor and selected range of an axis.")]
        [DefaultValue(false)]
        public bool IsUserEnabled { get; set; }
        [Bindable(true)]
        [DefaultValue(false)]
        public bool IsUserSelectionEnabled { get; set; }
        //
        // Summary:
        //     Gets or sets the cursor line color.
        //
        // Returns:
        //     A System.Drawing.Color value that represents the line color of the cursor.
        //     The default value is System.Drawing.Color.Red.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Description("Gets or sets the cursor line color.")]
        [DefaultValue(typeof(Color), "Red")]
        [TypeConverter(typeof(ColorConverter))]
        public Color LineColor { get; set; }
        //
        // Summary:
        //     Gets or sets the style of the cursor line.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartDashStyle enumeration
        //     value.
        [Bindable(true)]
        [Description("Gets or sets the style of the cursor line.")]
        [DefaultValue(ChartDashStyle.Solid)]
        public ChartDashStyle LineDashStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the line width, in pixels, of a cursor
        //
        // Returns:
        //     An integer value that represents the line width, in pixels. The default value
        //     is one (1).
        [DefaultValue(1)]
        [Bindable(true)]
        [Description("Gets or sets the line width, in pixels, of a cursor")]
        public int LineWidth { get; set; }
        //
        // Summary:
        //     Gets or sets the position of a cursor.
        //
        // Returns:
        //     A double value that represents the position of a cursor.
        [ParenthesizePropertyName(true)]
        [Bindable(true)]
        [Description("Gets or sets the position of a cursor.")]
        [DefaultValue(double.NaN)]
        public double Position { get; set; }
        //
        // Summary:
        //     Gets or sets a semi-transparent color that highlights a range of data.
        //
        // Returns:
        //     A System.Drawing.Color value represents the color of the highlighted range.
        //     The default value is System.Drawing.Color.LightGray, with an alpha value
        //     of 120.
        [Bindable(true)]
        [Description("Gets or sets a semi-transparent color that highlights a range of data.")]
        [DefaultValue(typeof(Color), "LightGray")]
        [TypeConverter(typeof(ColorConverter))]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        public Color SelectionColor { get; set; }
        //
        // Summary:
        //     Gets or sets the end position of a range selection.
        //
        // Returns:
        //     A double value that represents the end position of a range selection.
        [Bindable(true)]
        [DefaultValue(double.NaN)]
        [Description("Gets or sets the end position of a range selection.")]
        public double SelectionEnd { get; set; }
        //
        // Summary:
        //     Gets or sets the start position of a cursor's selected range.
        //
        // Returns:
        //     A double value that represents the start position of a cursor's range selection.
        [DefaultValue(double.NaN)]
        [Bindable(true)]
        [Description("Gets or sets the start position of a cursor's selected range.")]
        public double SelectionStart { get; set; }

        public PMSCursor(System.Windows.Forms.DataVisualization.Charting.Cursor Cursor1)
        {
            if (Cursor1 == null)
            {
                this.AutoScroll = true;
                this.AxisType = AxisType.Primary;
                this.Interval = 1f;
                this.IntervalOffset = 0f;
                this.IntervalOffsetType = DateTimeIntervalType.Auto;
                this.IntervalType = DateTimeIntervalType.Auto;
                this.IsUserEnabled = false;
                this.IsUserSelectionEnabled = false;
                this.LineColor = Color.Red;
                this.LineDashStyle = ChartDashStyle.Solid;
                this.LineWidth = 1;
                this.Position = double.NaN;
                this.SelectionColor = Color.LightGray;
                this.SelectionEnd = double.NaN;
                this.SelectionStart = double.NaN;
            }
            else
            {
                this.AutoScroll = Cursor1.AutoScroll;
                this.AxisType = Cursor1.AxisType;
                this.Interval = Cursor1.Interval;
                this.IntervalOffset = Cursor1.IntervalOffset;
                this.IntervalOffsetType = Cursor1.IntervalOffsetType;
                this.IntervalType = Cursor1.IntervalType;
                this.IsUserEnabled = Cursor1.IsUserEnabled;
                this.IsUserSelectionEnabled = Cursor1.IsUserSelectionEnabled;
                this.LineColor = Cursor1.LineColor;
                this.LineDashStyle = Cursor1.LineDashStyle;
                this.LineWidth = Cursor1.LineWidth;
                this.Position = Cursor1.Position;
                this.SelectionColor = Cursor1.SelectionColor;
                this.SelectionEnd = Cursor1.SelectionEnd;
                this.SelectionStart = Cursor1.SelectionStart;
            }
        }

        public System.Windows.Forms.DataVisualization.Charting.Cursor ToCursor()
        {
            System.Windows.Forms.DataVisualization.Charting.Cursor ChartArea3DStyle1
                = new System.Windows.Forms.DataVisualization.Charting.Cursor();
            SetCursor(ChartArea3DStyle1);
            return ChartArea3DStyle1;
        }
        public void SetCursor(System.Windows.Forms.DataVisualization.Charting.Cursor ChartArea3DStyle1)
        {
            if (ChartArea3DStyle1 != null)
            {
                ChartArea3DStyle1.AutoScroll = this.AutoScroll;
                ChartArea3DStyle1.AxisType = this.AxisType;
                ChartArea3DStyle1.Interval = this.Interval;
                ChartArea3DStyle1.IntervalOffset = this.IntervalOffset;
                ChartArea3DStyle1.IntervalOffsetType = this.IntervalOffsetType;
                ChartArea3DStyle1.IntervalType = this.IntervalType;
                ChartArea3DStyle1.IsUserEnabled = this.IsUserEnabled;
                ChartArea3DStyle1.IsUserSelectionEnabled = this.IsUserSelectionEnabled;
                ChartArea3DStyle1.LineColor = this.LineColor;
                ChartArea3DStyle1.LineDashStyle = this.LineDashStyle;
                ChartArea3DStyle1.LineWidth = this.LineWidth;
                ChartArea3DStyle1.Position = this.Position;
                ChartArea3DStyle1.SelectionColor = this.SelectionColor;
                ChartArea3DStyle1.SelectionEnd = this.SelectionEnd;
                ChartArea3DStyle1.SelectionStart = this.SelectionStart;
            }
        }

        public override string ToString()
        {
            return "";
        }
    }

    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class PMSLegend
    {
        // Summary:
        //     Gets or sets the alignment of the legend.
        //
        // Returns:
        //     A .NET Framework System.Drawing.StringAlignment enumeration value that represents
        //     the alignment of the legend. The default value is System.Drawing.StringAlignment.Near.
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [DefaultValue(StringAlignment.Near)]
        [Category("停靠")]
        [Description("Gets or sets the alignment of the legend.")]
        public StringAlignment Alignment { get; set; }
        //
        // Summary:
        //     Gets or sets the minimum font size that can be used by the autofitting algorithm
        //     for the legend text.
        //
        // Returns:
        //     An integer value that represents the minimum font size.
        [DefaultValue(7)]
        [Category("外观")]
        [Description("Gets or sets the minimum font size that can be used by the autofitting algorithm for the legend text.")]
        public int AutoFitMinFontSize { get; set; }
        //
        // Summary:
        //     Gets or sets the background color of a legend.
        //
        // Returns:
        //     A System.Drawing.Color value. The default color is System.Drawing.Color.White.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [DefaultValue(typeof(Color), "")]
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [TypeConverter(typeof(ColorConverter))]
        [Category("外观")]
        [Description("Gets or sets the background color of a legend.")]
        public Color BackColor { get; set; }
        //
        // Summary:
        //     Gets or sets the orientation for the background gradient of a legend. Also
        //     determines whether a gradient is used.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.GradientStyle enumeration.
        //     The default value is System.Windows.Forms.DataVisualization.Charting.GradientStyle.None.
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [DefaultValue(GradientStyle.None)]
        [Category("外观")]
        [Description("Gets or sets the orientation for the background gradient of a legend. Also determines whether a gradient is used.")]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.GradientEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        public GradientStyle BackGradientStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the hatching style of a legend.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle enumeration.
        //     The default value is System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle.None.
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [DefaultValue(ChartHatchStyle.None)]
        [Category("外观")]
        [Description("Gets or sets the hatching style of a legend.")]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.HatchStyleEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        public ChartHatchStyle BackHatchStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the background image of a legend.
        //
        // Returns:
        //     A string value that represents the URL of an image file. The default value
        //     is an empty string.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ImageValueEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [DefaultValue("")]
        [Browsable(false)]
        [NotifyParentProperty(true)]
        public string BackImage { get; set; }
        //
        // Summary:
        //     Gets or sets the background image alignment used for the System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.Unscaled
        //     drawing mode.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle
        //     enumeration value. The default value is System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.TopLeft.
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [Browsable(false)]
        public ChartImageAlignmentStyle BackImageAlignment { get; set; }
        //
        // Summary:
        //     Gets or sets a color that will be replaced with a transparent color when
        //     the background image is drawn.
        //
        // Returns:
        //     A System.Drawing.Color value that will be displayed as transparent. The default
        //     value is System.Drawing.Color.Empty.
        [Bindable(true)]
        [TypeConverter(typeof(ColorConverter))]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [DefaultValue(typeof(Color), "")]
        [NotifyParentProperty(true)]
        [Browsable(false)]
        public Color BackImageTransparentColor { get; set; }
        //
        // Summary:
        //     Gets or sets the legend background image drawing mode.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode enumeration
        //     value. The default value is System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.Tile.
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [Browsable(false)]
        public ChartImageWrapMode BackImageWrapMode { get; set; }
        //
        // Summary:
        //     Gets or sets the secondary color of a legend background.
        //
        // Returns:
        //     A System.Drawing.Color value used for the secondary color of background with
        //     hatching or gradient fill. The default value is System.Drawing.Color.Empty.
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [TypeConverter(typeof(ColorConverter))]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [DefaultValue(typeof(Color), "")]
        [Category("外观")]
        [Description("Gets or sets the secondary color of a legend background.")]
        public Color BackSecondaryColor { get; set; }
        //
        // Summary:
        //     Gets or sets the border color of a legend.
        //
        // Returns:
        //     A T:System.Drawing.Color value. The default color is System.Drawing.Color.Empty.
        [DefaultValue(typeof(Color), "")]
        [NotifyParentProperty(true)]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Category("外观")]
        [Description("Gets or sets the border color of a legend.")]
        [TypeConverter(typeof(ColorConverter))]
        public Color BorderColor { get; set; }
        //
        // Summary:
        //     Gets or sets the border style of a legend.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartDashStyle enumeration
        //     value that determines the border style of the chart element.
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [DefaultValue(ChartDashStyle.Solid)]
        [Category("外观")]
        [Description("Gets or sets the border style of a legend.")]
        public ChartDashStyle BorderDashStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the border width of a legend.
        //
        // Returns:
        //     An integer value that determines the border width, in pixels, of the legend.
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [DefaultValue(1)]
        [Category("外观")]
        [Description("Gets or sets the border width of a legend.")]
        public int BorderWidth { get; set; }
        //
        // Summary:
        //     Gets the System.Windows.Forms.DataVisualization.Charting.LegendCellColumnCollection
        //     for a legend.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.LegendCellColumnCollection
        //     object.
        [Editor(typeof(LegendCellColumnCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category("单元列")]
        public List<PMSLegendCellColumn> CellColumns { get; set; }
        //
        // Summary:
        //     Gets a T:System.Windows.Forms.DataVisualization.Charting.LegendItemsCollection
        //     object used for custom legend items.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.LegendItemsCollection object.
        [Editor(typeof(LegendItemCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [Category("外观")]
        public List<PMSLegendItem> CustomItems { get; set; }

        /*//*/
        //
        // Summary:
        //     Gets or sets the name of the System.Windows.Forms.DataVisualization.Charting.ChartArea
        //     where the legend will be docked.
        //
        // Returns:
        //     A string value that represents the name of the System.Windows.Forms.DataVisualization.Charting.ChartArea
        //     where this legend will be docked. The default value is the empty string.
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [DefaultValue("NotSet")]
        [Category("停靠")]
        [Browsable(false)]
        [Description("Gets or sets the name of the System.Windows.Forms.DataVisualization.Charting.ChartArea where the legend will be docked.")]
        public string DockedToChartArea { get; set; }
        //
        // Summary:
        //     Gets or sets a value that determines where the legend is docked.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.Docking enumeration value.
        //     The default value is System.Windows.Forms.DataVisualization.Charting.Docking.Right.
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [DefaultValue(Docking.Right)]
        [Category("停靠")]
        [Description(" Gets or sets a value that determines where the legend is docked.")]
        public Docking Docking { get; set; }
        //
        // Summary:
        //     Gets or sets a value that determines if the legend is enabled.
        //
        // Returns:
        //     True if enabled, false if disabled. The default value is true.
        [ParenthesizePropertyName(true)]
        [Bindable(true)]
        [DefaultValue(true)]
        [Category("外观")]
        [Description("Gets or sets a value that determines if the legend is enabled.")]
        [NotifyParentProperty(true)]
        public bool Enabled { get; set; }
        //
        // Summary:
        //     Gets or sets a System.Drawing.Font object, which is used to set font properties
        //     of the legend.
        //
        // Returns:
        //     A System.Drawing.Font object.
        [Bindable(true)]
        [DefaultValue(typeof(Font), "Microsoft Sans Serif, 8pt")]
        [NotifyParentProperty(true)]
        [Category("外观")]
        [Description("Gets or sets a System.Drawing.Font object, which is used to set font properties of the legend.")]
        public Font Font { get; set; }
        //
        // Summary:
        //     Gets or sets the color of the legend text.
        //
        // Returns:
        //     A System.Drawing.Color value.
        [NotifyParentProperty(true)]
        [TypeConverter(typeof(ColorConverter))]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [DefaultValue(typeof(Color), "Black")]
        [Category("外观")]
        [Description("Gets or sets the color of the legend text.")]
        public Color ForeColor { get; set; }
        //
        // Summary:
        //     Gets or sets the visual separator type for the legend header.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.LegendSeparatorStyle enumeration
        //     value.
        [DefaultValue(typeof(LegendSeparatorStyle), "None")]
        [Category("单元列")]
        [Description("Gets or sets the visual separator type for the legend header.")]
        public LegendSeparatorStyle HeaderSeparator { get; set; }
        //
        // Summary:
        //     Gets or sets the color of the separator for the legend header.
        //
        // Returns:
        //     A System.Drawing.Color value.
        [DefaultValue(typeof(Color), "Black")]
        [TypeConverter(typeof(ColorConverter))]
        [Category("单元列")]
        [Description("Gets or sets the color of the separator for the legend header.")]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        public Color HeaderSeparatorColor { get; set; }

        //
        // Summary:
        //     Gets or sets a flag that indicates if legend rows will be drawn with interlaced
        //     background color.
        //
        // Returns:
        //     A Boolean value that determines whether alternating rows in the legend are
        //     drawn with a specified color.
        [DefaultValue(false)]
        [Category("外观")]
        [Description(" Gets or sets a flag that indicates if legend rows will be drawn with interlaced background color.")]
        public bool InterlacedRows { get; set; }
        //
        // Summary:
        //     Gets or sets the background color of the legend's interlaced rows.
        //
        // Returns:
        //     A System.Drawing.Color value.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [DefaultValue(typeof(Color), "")]
        [TypeConverter(typeof(ColorConverter))]
        [Category("外观")]
        [Description("Gets or sets the background color of the legend's interlaced rows.")]
        public Color InterlacedRowsColor { get; set; }
        //
        // Summary:
        //     Gets or sets a flag that indicates whether the legend is docked inside or
        //     outside the chart area.
        //
        // Returns:
        //     True is the legend is docked inside the chart area, otherwise false if it
        //     is docked outside the chart area. The default value is false.
        [NotifyParentProperty(true)]
        [DefaultValue(true)]
        [Category("停靠")]
        [Description("Gets or sets a flag that indicates whether the legend is docked inside or outside the chart area.")]
        [Bindable(true)]
        public bool IsDockedInsideChartArea { get; set; }
        //
        // Summary:
        //     Gets or sets a flag that indicates whether all legend items are equally spaced.
        //
        // Returns:
        //     True if legend items are equally spaced, false if they are not. The default
        //     value is false.
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [DefaultValue(false)]
        [Category("外观")]
        [Description("Gets or sets a flag that indicates whether all legend items are equally spaced.")]
        public bool IsEquallySpacedItems { get; set; }

        [NotifyParentProperty(true)]
        [Bindable(true)]
        [Category("外观")]
        [Description("Gets or sets a flag that indicates whether legend text is automatic fitting.")]
        [DefaultValue(true)]
        public bool IsTextAutoFit { get; set; }
        //
        // Summary:
        //     Gets or sets the visual separator type for the legend table columns.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.LegendSeparatorStyle enumeration
        //     value.
        [DefaultValue(typeof(LegendSeparatorStyle), "None")]
        [Category("单元列")]
        [Description("Gets or sets the visual separator type for the legend table columns.")]
        public LegendSeparatorStyle ItemColumnSeparator { get; set; }
        //
        // Summary:
        //     Gets or sets the color of the legend table column separator.
        //
        // Returns:
        //     A Color value used to draw the color of the legend table column separator.
        [TypeConverter(typeof(ColorConverter))]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [DefaultValue(typeof(Color), "Black")]
        [Category("单元列")]
        [Description("Gets or sets the color of the legend table column separator.")]
        public Color ItemColumnSeparatorColor { get; set; }
        //
        // Summary:
        //     Gets or sets the legend table column spacing.
        //
        // Returns:
        //     An integer value that represents the table column spacing.
        [DefaultValue(50)]
        [Category("单元列")]
        [Description("Gets or sets the legend table column spacing.")]
        public int ItemColumnSpacing { get; set; }
        //
        // Summary:
        //     Gets or sets a flag that specifies the order in which legend items are displayed.
        //     This property only affects legend items automatically added for the chart
        //     series; it has no effect on custom legend items.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.LegendItemOrder enumeration
        //     value.
        [DefaultValue(LegendItemOrder.Auto)]
        [Category("外观")]
        [Description("Gets or sets a flag that specifies the order in which legend items are displayed.This property only affects legend items automatically added for the chart series; it has no effect on custom legend items.")]
        public LegendItemOrder LegendItemOrder { get; set; }
        //
        // Summary:
        //     Gets or sets the style of the legend.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.LegendStyle enumeration
        //     value that determines the legend style. The default value is System.Windows.Forms.DataVisualization.Charting.LegendStyle.Column.
        [NotifyParentProperty(true)]
        [ParenthesizePropertyName(true)]
        [Bindable(true)]
        [DefaultValue(LegendStyle.Table)]
        [Category("外观")]
        [Description("Gets or sets the style of the legend.")]
        public LegendStyle LegendStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the maximum size of the legend, measured as a percentage of
        //     the chart area. This value is used by the automatic layout algorithm.
        //
        // Returns:
        //     A float value that represents the maximum size of the legend.
        [DefaultValue(50f)]
        [Category("停靠")]
        [Description("Gets or sets the maximum size of the legend, measured as a percentage of the chart area. This value is used by the automatic layout algorithm.")]
        public float MaximumAutoSize { get; set; }
        //
        // Summary:
        //     Gets or sets the name of the legend.
        //
        // Returns:
        //     A string value that represents the name of the legend.
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [DefaultValue("Legend1")]
        [Category("杂项")]
        [Description("Gets or sets the name of the legend.")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (Parent != null && Parent is TreeNode)
                {
                    TreeNode parent = (TreeNode)Parent;

                    foreach (TreeNode node in parent.Nodes)
                    {
                        if (value == node.Text)
                            throw new Exception("属性名已存在！");
                    }
                    name = value;
                    return;
                }
                name = value;
            }
        }
        //
        // Summary:
        //     Gets or sets an System.Windows.Forms.DataVisualization.Charting.ElementPosition
        //     object, which can be used to get or set the position of the legend.
        //
        // Returns:
        //     If the System.Windows.Forms.DataVisualization.Charting.Legend.Position property
        //     is automatic (that is, Legend.Position.Auto = true) the legend position is
        //     calculated automatically by the System.Windows.Forms.DataVisualization.Charting.Chart
        //     control, taking into account the System.Windows.Forms.DataVisualization.Charting.Legend.Docking,
        //     System.Windows.Forms.DataVisualization.Charting.Legend.Alignment and System.Windows.Forms.DataVisualization.Charting.Legend.IsDockedInsideChartArea
        //     property settings.If it is not automatic, the System.Windows.Forms.DataVisualization.Charting.Legend.Docking,
        //     System.Windows.Forms.DataVisualization.Charting.Legend.DockedToChartArea
        //     and System.Windows.Forms.DataVisualization.Charting.Legend.IsDockedInsideChartArea
        //     properties are ignored, and the legend position is solely determined by the
        //     value of the returned System.Windows.Forms.DataVisualization.Charting.ElementPosition
        //     object.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [Category("外观")]
        [Description("Gets or sets an System.Windows.Forms.DataVisualization.Charting.ElementPosition object, which can be used to get or set the position of the legend.")]
        public PMSElementPosition Position { get; set; }
        //
        // Summary:
        //     Gets or sets the shadow color of the legend.
        //
        // Returns:
        //     A System.Drawing.Color value. The default color is System.Drawing.Color.Black.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [DefaultValue(typeof(Color), "128, 0, 0, 0")]
        [NotifyParentProperty(true)]
        [TypeConverter(typeof(ColorConverter))]
        [Category("外观")]
        [Description("Gets or sets the shadow color of the legend.")]
        public Color ShadowColor { get; set; }
        //
        // Summary:
        //     Gets or sets the shadow offset, in pixels, of the legend.
        //
        // Returns:
        //     An integer value that represents the shadow offset, in pixels, of the legend.
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [DefaultValue(0)]
        [Category("外观")]
        [Description("Gets or sets the shadow offset, in pixels, of the legend.")]
        public int ShadowOffset { get; set; }
        //
        // Summary:
        //     Gets or sets the legend table style.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.LegendTableStyle enumeration
        //     value.
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [ParenthesizePropertyName(true)]
        [DefaultValue(LegendTableStyle.Auto)]
        [Category("外观")]
        [Description("Gets or sets the legend table style.")]
        public LegendTableStyle TableStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the number of characters that can be sequentially displayed
        //     in the legend before the text is wrapped.
        //
        // Returns:
        //     An integer value that represents the number of characters that can be placed
        //     in the legend sequentially before the text is wrapped. The default value
        //     is 25.
        [DefaultValue(25)]
        [Category("外观")]
        [Description("Gets or sets the number of characters that can be sequentially displayed in the legend before the text is wrapped.")]
        public int TextWrapThreshold { get; set; }
        //
        // Summary:
        //     Gets or sets the text of the legend title.
        //
        // Returns:
        //     A string value that represents the legend title text. The default value is
        //     a zero-length string.
        [DefaultValue("")]
        [Category("标题")]
        [Description("Gets or sets the text of the legend title.")]
        public string Title { get; set; }
        //
        // Summary:
        //     Gets or sets the alignment of the legend title.
        //
        // Returns:
        //     A System.Drawing.StringAlignment enumeration value that represents the alignment
        //     of the legend title. The default value is System.Drawing.StringAlignment.Center.
        [DefaultValue(typeof(StringAlignment), "Center")]
        [Category("标题")]
        [Description(" Gets or sets the alignment of the legend title.")]
        public StringAlignment TitleAlignment { get; set; }
        //
        // Summary:
        //     Gets or sets the background color of the legend title.
        //
        // Returns:
        //     A System.Drawing.Color value used to draw the background of the legend title.
        //     The default value is System.Drawing.Color.Empty.
        [DefaultValue(typeof(Color), "")]
        [TypeConverter(typeof(ColorConverter))]
        [Category("标题")]
        [Description("Gets or sets the background color of the legend title.")]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        public Color TitleBackColor { get; set; }
        //
        // Summary:
        //     Gets or sets the font for the legend title.
        //
        // Returns:
        //     A System.Drawing.Font object. The default value is "Microsoft Sans Serif,
        //     8pt, Bold".
        [Category("标题")]
        [Description("Gets or sets the font for the legend title.")]
        [DefaultValue(typeof(Font), "Microsoft Sans Serif, 8pt, style=Bold")]
        public Font TitleFont { get; set; }
        //
        // Summary:
        //     Gets or sets the text color of the legend title.
        //
        // Returns:
        //     A System.Drawing.Color value used to draw the title text color. The default
        //     value is System.Drawing.Color.Black.
        [DefaultValue(typeof(Color), "Black")]
        [TypeConverter(typeof(ColorConverter))]
        [Category("标题")]
        [Description("Gets or sets the text color of the legend title.")]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        public Color TitleForeColor { get; set; }
        //
        // Summary:
        //     Gets or sets the visual separator type for the legend title.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.LegendSeparatorStyle value
        //     that represents the type of separator that will be drawn below the title.
        //     The default value is System.Windows.Forms.DataVisualization.Charting.LegendSeparatorStyle.None.
        [DefaultValue(typeof(LegendSeparatorStyle), "None")]
        [Category("标题")]
        [Description("Gets or sets the visual separator type for the legend title.")]
        public LegendSeparatorStyle TitleSeparator { get; set; }
        //
        // Summary:
        //     Gets or sets the color of the legend title separator.
        //
        // Returns:
        //     A Color value used to draw the color of the legend title separator. The default
        //     value is Color.Black.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [DefaultValue(typeof(Color), "Black")]
        [TypeConverter(typeof(ColorConverter))]
        [Category("标题")]
        [Description("Gets or sets the color of the legend title separator.")]
        public Color TitleSeparatorColor { get; set; }

        private string name;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Parent { get; set; }

        [Browsable(false)]
        public object SeriesDataList { get; set; }

        public override string ToString()
        {
            return "Legend";
        }
        public PMSLegend(Legend legend)
        {
            SeriesDataList = new List<string>();
            if (legend == null)
            {
                this.Alignment = StringAlignment.Near;
                this.AutoFitMinFontSize = 7;
                this.BackGradientStyle = GradientStyle.None;
                this.BackHatchStyle = ChartHatchStyle.None;
                this.BorderColor = Color.Empty;
                this.BorderDashStyle = ChartDashStyle.Solid;
                this.BorderWidth = 1;
                this.DockedToChartArea = "NotSet";//
                this.Docking = Docking.Right;
                this.Enabled = true;
                this.Font = new Font("Microsoft Sans Serif", 8);
                this.ForeColor = Color.Black;
                this.HeaderSeparator = LegendSeparatorStyle.None;
                this.HeaderSeparatorColor = Color.Black;
                this.InterlacedRows = false;
                this.InterlacedRowsColor = Color.Empty;
                this.IsDockedInsideChartArea = true;
                this.IsEquallySpacedItems = false;
                this.IsTextAutoFit = true;
                this.ItemColumnSeparator = LegendSeparatorStyle.None;
                this.ItemColumnSeparatorColor = Color.Empty;
                this.ItemColumnSpacing = 50;
                this.LegendItemOrder = LegendItemOrder.Auto;
                this.LegendStyle = LegendStyle.Table;
                this.MaximumAutoSize = 50;
                this.Name = "Legend1";
                this.Position = new PMSElementPosition(null);
                this.ShadowColor = Color.FromArgb(128, 0, 0, 0);
                this.ShadowOffset = 0;
                this.TableStyle = LegendTableStyle.Auto;
                this.TextWrapThreshold = 25;
                this.Title = "";
                this.TitleAlignment = StringAlignment.Center;
                this.TitleBackColor = Color.Empty;
                this.TitleFont = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
                this.TitleForeColor = Color.Black;
                this.TitleSeparator = LegendSeparatorStyle.None;
                this.TitleSeparatorColor = Color.Black;

                this.CellColumns = new List<PMSLegendCellColumn>();
                this.CustomItems = new List<PMSLegendItem>();
            }
            else
            {
                this.Alignment = legend.Alignment;
                this.AutoFitMinFontSize = legend.AutoFitMinFontSize;
                this.BackColor = legend.BackColor;
                this.BackGradientStyle = legend.BackGradientStyle;
                this.BackHatchStyle = legend.BackHatchStyle;
                this.BackSecondaryColor = legend.BackSecondaryColor;
                this.BorderColor = legend.BorderColor;
                this.BorderDashStyle = legend.BorderDashStyle;
                this.BorderWidth = legend.BorderWidth;
                this.DockedToChartArea = legend.DockedToChartArea;
                this.Docking = legend.Docking;
                this.Enabled = legend.Enabled;
                this.Font = legend.Font;
                this.ForeColor = legend.ForeColor;
                this.HeaderSeparator = legend.HeaderSeparator;
                this.HeaderSeparatorColor = legend.HeaderSeparatorColor;
                this.InterlacedRows = legend.InterlacedRows;
                this.InterlacedRowsColor = legend.InterlacedRowsColor;
                this.IsDockedInsideChartArea = legend.IsDockedInsideChartArea;
                this.IsEquallySpacedItems = legend.IsEquallySpacedItems;
                this.IsTextAutoFit = legend.IsTextAutoFit;
                this.ItemColumnSeparator = legend.ItemColumnSeparator;
                this.ItemColumnSeparatorColor = legend.ItemColumnSeparatorColor;
                this.ItemColumnSpacing = legend.ItemColumnSpacing;
                this.LegendItemOrder = legend.LegendItemOrder;
                this.LegendStyle = legend.LegendStyle;
                this.MaximumAutoSize = legend.MaximumAutoSize;
                this.Name = legend.Name;
                this.Position = new PMSElementPosition(legend.Position);
                this.ShadowColor = legend.ShadowColor;
                this.ShadowOffset = legend.ShadowOffset;
                this.TableStyle = legend.TableStyle;
                this.TextWrapThreshold = legend.TextWrapThreshold;
                this.Title = legend.Title;
                this.TitleAlignment = legend.TitleAlignment;
                this.TitleBackColor = legend.TitleBackColor;
                this.TitleFont = legend.TitleFont;
                this.TitleForeColor = legend.TitleForeColor;
                this.TitleSeparator = legend.TitleSeparator;
                this.TitleSeparatorColor = legend.TitleSeparatorColor;
                this.Name = legend.Name;

                this.CellColumns = new List<PMSLegendCellColumn>();
                this.CustomItems = new List<PMSLegendItem>();

                if (legend.CellColumns != null && legend.CellColumns.Count > 0)
                {
                    foreach (var node in legend.CellColumns)
                    {
                        this.CellColumns.Add(new PMSLegendCellColumn(node));
                    }
                }
                if (legend.CustomItems != null && legend.CustomItems.Count > 0)
                {
                    foreach (var node in legend.CustomItems)
                    {
                        this.CustomItems.Add(new PMSLegendItem(node));
                    }
                }
            }
        }

        public Legend ToLegend()
        {
            Legend labelStyle = new Legend();
            SetLegend(labelStyle);
            return labelStyle;
        }
        public void SetLegend(Legend legend)
        {
            if (legend == null)
                return;
            legend.Alignment = this.Alignment;
            legend.AutoFitMinFontSize = this.AutoFitMinFontSize;
            legend.BackColor = this.BackColor;
            legend.BackGradientStyle = this.BackGradientStyle;
            legend.BackHatchStyle = this.BackHatchStyle;
            legend.BackSecondaryColor = this.BackSecondaryColor;
            legend.BorderColor = this.BorderColor;
            legend.BorderDashStyle = this.BorderDashStyle;
            legend.BorderWidth = this.BorderWidth;
            legend.DockedToChartArea = this.DockedToChartArea;
            legend.Docking = this.Docking;
            legend.Enabled = this.Enabled;
            legend.Font = this.Font;
            legend.ForeColor = this.ForeColor;
            legend.HeaderSeparator = this.HeaderSeparator;
            legend.HeaderSeparatorColor = this.HeaderSeparatorColor;
            legend.InterlacedRows = this.InterlacedRows;
            legend.InterlacedRowsColor = this.InterlacedRowsColor;
            legend.IsDockedInsideChartArea = this.IsDockedInsideChartArea;
            legend.IsEquallySpacedItems = this.IsEquallySpacedItems;
            legend.IsTextAutoFit = this.IsTextAutoFit;
            legend.ItemColumnSeparator = this.ItemColumnSeparator;
            legend.ItemColumnSeparatorColor = this.ItemColumnSeparatorColor;
            legend.ItemColumnSpacing = this.ItemColumnSpacing;
            legend.LegendItemOrder = this.LegendItemOrder;
            legend.LegendStyle = this.LegendStyle;
            legend.MaximumAutoSize = this.MaximumAutoSize;
            legend.Name = this.Name;
            legend.Position = this.Position.ToElementPosition();
            legend.ShadowColor = this.ShadowColor;
            legend.ShadowOffset = this.ShadowOffset;
            legend.TableStyle = this.TableStyle;
            legend.TextWrapThreshold = this.TextWrapThreshold;
            legend.Title = this.Title;
            legend.TitleAlignment = this.TitleAlignment;
            legend.TitleBackColor = this.TitleBackColor;
            legend.TitleFont = this.TitleFont;
            legend.TitleForeColor = this.TitleForeColor;
            legend.TitleSeparator = this.TitleSeparator;
            legend.TitleSeparatorColor = this.TitleSeparatorColor;


            legend.CellColumns.Clear();
            if (this.CellColumns != null)
            {
                foreach (var cellItem in this.CellColumns)
                {
                    legend.CellColumns.Add(cellItem.ToPMSLegendCellColumn());
                }
            }
            legend.CustomItems.Clear();
            if (this.CustomItems != null)
            {
                foreach (var cellItem in this.CustomItems)
                {
                    legend.CustomItems.Add(cellItem.ToPMSLegendItem());
                }
            }
        }
    }

    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class PMSLegendItem
    {
        // Summary:
        //     Gets or sets the orientation for the background gradient of a legend item.
        //     Also determines whether a gradient is used.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.GradientStyle enumeration.
        //     The default value is System.Windows.Forms.DataVisualization.Charting.GradientStyle.None.
        [Bindable(true)]
        [Category("外观")]
        [Description("Gets or sets the orientation for the background gradient of a legend item.  Also determines whether a gradient is used.")]
        [DefaultValue(GradientStyle.None)]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.GradientEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        public GradientStyle BackGradientStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the hatching style of a legend item.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle enumeration.
        //     The default value is System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle.None.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.HatchStyleEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Category("外观")]
        [Description("Gets or sets the hatching style of a legend item.")]
        [DefaultValue(ChartHatchStyle.None)]
        public ChartHatchStyle BackHatchStyle { get; set; }

        //
        // Summary:
        //     Gets or sets the secondary color of a legend item.
        //
        // Returns:
        //     A System.Drawing.Color value used for the secondary color of background with
        //     hatching or gradient fill. The default value is System.Drawing.Color.Empty.
        [DefaultValue(typeof(Color), "")]
        [TypeConverter(typeof(ColorConverter))]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Category("外观")]
        [Description(" Gets or sets the secondary color of a legend item.")]
        public Color BackSecondaryColor { get; set; }
        //
        // Summary:
        //     Gets or sets the border color of a legend item.
        //
        // Returns:
        //     A T:System.Drawing.Color value. The default color is System.Drawing.Color.Empty.
        [TypeConverter(typeof(ColorConverter))]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [DefaultValue(typeof(Color), "Black")]
        [Bindable(true)]
        [Description("Gets or sets the border color of a legend item.")]
        [Category("外观")]
        public Color BorderColor { get; set; }
        //
        // Summary:
        //     Gets or sets the border style of a legend item.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartDashStyle enumeration
        //     value that determines the border style of the legend item.
        [Bindable(true)]
        [Category("外观")]
        [DefaultValue(ChartDashStyle.Solid)]
        [Description("Gets or sets the border style of a legend item.")]
        public ChartDashStyle BorderDashStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the border width of a legend item.
        //
        // Returns:
        //     An integer value that determines the width, in pixels, of the legend item
        //     border.
        [DefaultValue(1)]
        [Bindable(true)]
        [Category("外观")]
        [Description("Gets or sets the border width of a legend item.")]
        public int BorderWidth { get; set; }
        /*//*/
        //
        // Summary:
        //     Gets the collection of cells in the legend item.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.LegendCellCollection object.
        [Editor(typeof(LegendCellCollectionEditor), typeof(UITypeEditor))]
        [Category("外观")]
        [Description("Gets the collection of cells in the legend item.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<PMSLegendCell> Cells { get; set; }
        //
        // Summary:
        //     Gets or sets the color of a legend item.
        //
        // Returns:
        //     A T:System.Drawing.Color value. The default color is System.Drawing.Color.Empty.
        [TypeConverter(typeof(ColorConverter))]
        [Bindable(true)]
        [Category("外观")]
        [DefaultValue(typeof(Color), "")]
        [Description(" Gets or sets the color of a legend item.")]
        [NotifyParentProperty(true)]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        public Color Color { get; set; }
        //
        // Summary:
        //     Gets or sets a flag that indicates whether the legend item is enabled.
        //
        // Returns:
        //     True if the legend item is enabled, otherwise false. The default value is
        //     true.
        [DefaultValue(true)]
        [Category("外观")]
        [Description("Gets or sets a flag that indicates whether the legend item is enabled.")]
        [ParenthesizePropertyName(true)]
        public bool Enabled { get; set; }
        //
        // Summary:
        //     Gets or sets the image that will be displayed for a legend item symbol.
        //
        // Returns:
        //     A string value that represents the URL of an image file. The default value
        //     is an empty string.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ImageValueEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [DefaultValue("")]
        [Bindable(true)]
        [Description("Gets or sets the image that will be displayed for a legend item symbol.")]
        [NotifyParentProperty(true)]
        [Category("外观")]
        public string Image { get; set; }
        //
        // Summary:
        //     Gets or sets the display style of the legend item image.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.LegendImageStyle enumeration
        //     value.
        [ParenthesizePropertyName(true)]
        [Bindable(true)]
        [Category("外观")]
        [Description("Gets or sets the display style of the legend item image.")]
        [DefaultValue(typeof(LegendImageStyle), "Rectangle")]
        public LegendImageStyle ImageStyle { get; set; }

        //
        // Summary:
        //     Gets or sets the border color of the markers, if used.
        //
        // Returns:
        //     A System.Drawing.Color value that represents the ARGB (alpha, red, green,
        //     blue) color of a marker border.
        [RefreshProperties(RefreshProperties.All)]
        [TypeConverter(typeof(ColorConverter))]
        [Description("Gets or sets the border color of the markers, if used.")]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Category("标记")]
        [DefaultValue(typeof(Color), "")]
        public Color MarkerBorderColor { get; set; }
        //
        // Summary:
        //     Gets or sets the border width of the markers, if used. Measured in pixels.
        //
        // Returns:
        //     An integer value. The default value is one (1) pixel.
        [DefaultValue(1)]
        [Category("标记")]
        [Description("Gets or sets the border width of the markers, if used. Measured in pixels.")]
        public int MarkerBorderWidth { get; set; }
        //
        // Summary:
        //     Gets or sets the color of the markers, if used.
        //
        // Returns:
        //     A T:System.Drawing.Color value. The default color is System.Drawing.Color.Empty.
        [Bindable(true)]
        [DefaultValue(typeof(Color), "")]
        [RefreshProperties(RefreshProperties.All)]
        [TypeConverter(typeof(ColorConverter))]
        [Category("标记")]
        [Description("Gets or sets the color of the markers, if used.")]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        public Color MarkerColor { get; set; }

        //
        // Summary:
        //     Gets or sets the size of the legend item markers, if used.
        //
        // Returns:
        //     An integer expression that represents the height and width of the plotting
        //     area of markers, in pixels. The default value is 5 pixels.
        [DefaultValue(5)]
        [Description(" Gets or sets the size of the legend item markers, if used.")]
        [RefreshProperties(RefreshProperties.All)]
        [Bindable(true)]
        [Category("标记")]
        public int MarkerSize { get; set; }
        //
        // Summary:
        //     Gets or sets a legend item marker style. Also used to enable or disable the
        //     display of markers.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.LegendItem.MarkerStyle
        //     integer enumeration. The default value is System.Windows.Forms.DataVisualization.Charting.MarkerStyle.None.
        [RefreshProperties(RefreshProperties.All)]
        [Bindable(true)]
        [Category("标记")]
        [DefaultValue(MarkerStyle.None)]
        [Description("Gets or sets a legend item marker style. Also used to enable or disable the display of markers.")]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.MarkerStyleEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        public MarkerStyle MarkerStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the unique name of the legend item.
        //
        // Returns:
        //     A string value that represents the name of the legend item.
        [NotifyParentProperty(true)]
        [Description(" Gets or sets the unique name of the legend item.")]
        [ParenthesizePropertyName(true)]
        [Bindable(true)]
        [Category("外观")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (Parent != null && Parent is List<string>)
                {
                    List<string> parent = (List<string>)Parent;

                    foreach (var node in parent)
                    {
                        if (value == node)
                            throw new Exception("属性名已存在！");
                    }
                    name = value;
                    return;
                }
                name = value;
            }
        }

        private string name;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Parent { get; set; }
        //
        // Summary:
        //     Gets or sets the color of the legend item separator.
        //
        // Returns:
        //     A System.Drawing.Color value used to draw the color of the legend item separator.
        //     The default value is System.Drawing.Color.Black.
        [DefaultValue(typeof(Color), "Black")]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [TypeConverter(typeof(ColorConverter))]
        [Category("外观")]
        [Description("Gets or sets the color of the legend item separator.")]
        public Color SeparatorColor { get; set; }
        //
        // Summary:
        //     Gets or sets the separator style of the legend item.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.LegendSeparatorStyle enumeration
        //     value.
        [DefaultValue(typeof(LegendSeparatorStyle), "None")]
        [Category("外观")]
        [Description("Gets or sets the separator style of the legend item.")]
        public LegendSeparatorStyle SeparatorType { get; set; }

        //
        // Summary:
        //     Gets or sets the shadow color of a legend item.
        //
        // Returns:
        //     A System.Drawing.Color value. The default color is System.Drawing.Color.Black.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [DefaultValue(typeof(Color), "128,0,0,0")]
        [TypeConverter(typeof(ColorConverter))]
        [Category("外观")]
        [Description("Gets or sets the shadow color of a legend item.")]
        public Color ShadowColor { get; set; }
        //
        // Summary:
        //     Gets or sets the shadow offset, in pixels, of a legend item.
        //
        // Returns:
        //     An integer value that represents the shadow offset, in pixels, of the relevant
        //     chart element.
        [DefaultValue(0)]
        [Bindable(true)]
        [Category("外观")]
        [Description("Gets or sets the shadow offset, in pixels, of a legend item.")]
        public int ShadowOffset { get; set; }
        //
        // Summary:
        //     Gets or sets the tooltip of the legend item.
        //
        // Returns:
        //     A string value that represents the tooltip for the legend item.
        [DefaultValue("Gets or sets the tooltip of the legend item.")]
        [Bindable(true)]
        [Category("外观")]
        [Description("")]
        public string ToolTip { get; set; }


        public override string ToString()
        {
            return name;
        }

        public PMSLegendItem(LegendItem LegendItem1)
        {
            if (LegendItem1 == null)
            {
                this.BackGradientStyle = GradientStyle.None;
                this.BackHatchStyle = ChartHatchStyle.None;
                this.BackSecondaryColor = Color.Empty;
                this.BorderColor = Color.Empty;
                this.BorderDashStyle = ChartDashStyle.Solid;
                this.BorderWidth = 1;
                if (this.Cells == null)
                    this.Cells = new List<PMSLegendCell>();
                this.Color = Color.Empty;
                this.Enabled = true;
                this.Image = "";
                this.ImageStyle = LegendImageStyle.Rectangle;
                this.MarkerBorderColor = Color.Empty;
                this.MarkerBorderWidth = 1;
                this.MarkerColor = Color.Empty;
                this.MarkerSize = 5;
                this.MarkerStyle = MarkerStyle.None;
                //this.Name = LegendItem1.Name;
                this.SeparatorColor = Color.Black;
                this.SeparatorType = LegendSeparatorStyle.None;
                this.ShadowColor = Color.FromArgb(128, 0, 0, 0); ;
                this.ShadowOffset = 0;
                this.ToolTip = "";
            }
            else
            {
                this.BackGradientStyle = LegendItem1.BackGradientStyle;
                this.BackHatchStyle = LegendItem1.BackHatchStyle;
                this.BackSecondaryColor = LegendItem1.BackSecondaryColor;
                this.BorderColor = LegendItem1.BorderColor;
                this.BorderDashStyle = LegendItem1.BorderDashStyle;
                this.BorderWidth = LegendItem1.BorderWidth;
                if (LegendItem1.Cells != null)
                {
                    if (this.Cells == null)
                        this.Cells = new List<PMSLegendCell>();
                    this.Cells.Clear();
                    foreach (LegendCell cell in LegendItem1.Cells)
                    {
                        this.Cells.Add(new PMSLegendCell(cell));
                    }
                }
                this.Color = LegendItem1.Color;
                this.Enabled = LegendItem1.Enabled;
                this.Image = LegendItem1.Image;
                this.ImageStyle = LegendItem1.ImageStyle;
                this.MarkerBorderColor = LegendItem1.MarkerBorderColor;
                this.MarkerBorderWidth = LegendItem1.MarkerBorderWidth;
                this.MarkerColor = LegendItem1.MarkerColor;
                this.MarkerSize = LegendItem1.MarkerSize;
                this.MarkerStyle = LegendItem1.MarkerStyle;
                this.Name = LegendItem1.Name;
                this.SeparatorColor = LegendItem1.SeparatorColor;
                this.SeparatorType = LegendItem1.SeparatorType;
                this.ShadowColor = LegendItem1.ShadowColor;
                this.ShadowOffset = LegendItem1.ShadowOffset;
                if (!string.IsNullOrEmpty(LegendItem1.ToolTip))
                    this.ToolTip = LegendItem1.ToolTip;

            }
        }

        public LegendItem ToPMSLegendItem()
        {
            LegendItem LegendItem1 = new LegendItem();
            SetLegendItem(LegendItem1);
            return LegendItem1;
        }
        public void SetLegendItem(LegendItem LegendItem1)
        {
            if (LegendItem1 == null)
                return;

            LegendItem1.BackGradientStyle = this.BackGradientStyle;
            LegendItem1.BackHatchStyle = this.BackHatchStyle;
            LegendItem1.BackSecondaryColor = this.BackSecondaryColor;
            LegendItem1.BorderColor = this.BorderColor;
            LegendItem1.BorderDashStyle = this.BorderDashStyle;
            LegendItem1.BorderWidth = this.BorderWidth;
            if (this.Cells != null)
            {
                LegendItem1.Cells.Clear();
                foreach (PMSLegendCell cell in this.Cells)
                {
                    LegendItem1.Cells.Add(cell.ToPMSLegendCell());
                }
            }
            LegendItem1.Color = this.Color;
            LegendItem1.Enabled = this.Enabled;
            LegendItem1.Image = this.Image;
            LegendItem1.ImageStyle = this.ImageStyle;
            LegendItem1.MarkerBorderColor = this.MarkerBorderColor;
            LegendItem1.MarkerBorderWidth = this.MarkerBorderWidth;
            LegendItem1.MarkerColor = this.MarkerColor;
            LegendItem1.MarkerSize = this.MarkerSize;
            LegendItem1.MarkerStyle = this.MarkerStyle;
            LegendItem1.Name = this.Name;
            LegendItem1.SeparatorColor = this.SeparatorColor;
            LegendItem1.SeparatorType = this.SeparatorType;
            LegendItem1.ShadowColor = this.ShadowColor;
            LegendItem1.ShadowOffset = this.ShadowOffset;
            if (!string.IsNullOrEmpty(this.ToolTip))
                LegendItem1.ToolTip = this.ToolTip;
        }

    }

    internal class LegendItemCollectionEditor : UITypeEditor
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
                    PMSLegend control = null;
                    if (context.Instance.GetType() == typeof(PMSLegend))
                        control = (PMSLegend)context.Instance;

                    FormLegendItemCollection form1 = new FormLegendItemCollection();
                    form1.DataList = control.CustomItems;
                    if (DialogResult.OK == editorService.ShowDialog(form1))
                    {
                        control.CustomItems = form1.DataList;
                    }
                    return value;
                }
            }

            return value;
        }
    }
    [TypeConverter(typeof(LabelConverter))]
    [Serializable]
    public class PMSMargins
    {
        // Summary:
        //     Gets or sets the bottom margin.
        //
        // Returns:
        //     Returns an integer value. The default value is 0.
        [DefaultValue(0)]
        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.All)]
        public int Bottom { get; set; }
        //
        // Summary:
        //     Gets or sets the left margin.
        //
        // Returns:
        //     An integer value. The default value is 0.
        [NotifyParentProperty(true)]
        [DefaultValue(0)]
        [RefreshProperties(RefreshProperties.All)]
        public int Left { get; set; }
        //
        // Summary:
        //     Gets or sets the right margin.
        //
        // Returns:
        //     An integer value. The default value is 0.
        [DefaultValue(0)]
        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.All)]
        public int Right { get; set; }
        //
        // Summary:
        //     Gets or sets the top margin.
        //
        // Returns:
        //     An integer value. The default value is 0.
        [DefaultValue(0)]
        [RefreshProperties(RefreshProperties.All)]
        [NotifyParentProperty(true)]
        public int Top { get; set; }

        public override string ToString()
        {
            return "";
        }

        public PMSMargins(Margins LegendItem1)
        {
            if (LegendItem1 == null)
            {
                this.Bottom = 0;
                this.Left = 0;
                this.Right = 0;
                this.Top = 0;
            }
            else
            {
                this.Bottom = LegendItem1.Bottom;
                this.Left = LegendItem1.Left;
                this.Right = LegendItem1.Right;
                this.Top = LegendItem1.Top;
            }
        }

        public Margins ToPMSMargins()
        {
            Margins LegendItem1 = new Margins();
            SetMargins(LegendItem1);
            return LegendItem1;
        }
        public void SetMargins(Margins LegendItem1)
        {
            if (LegendItem1 == null)
                return;

            LegendItem1.Bottom = this.Bottom;
            LegendItem1.Left = this.Left;
            LegendItem1.Right = this.Right;
            LegendItem1.Top = this.Top;
        }
    }
    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class PMSLegendCell
    {
        // Summary:
        //     Gets or sets the alignment of the legend cell contents.
        //
        // Returns:
        //     A System.Drawing.ContentAlignment value that represents the alignment of
        //     the legend cell contents. The default value is System.Drawing.ContentAlignment.MiddleCenter.
        [DefaultValue(ContentAlignment.MiddleCenter)]
        [Description("Gets or sets the alignment of the legend cell contents.")]
        public ContentAlignment Alignment { get; set; }
        //
        // Summary:
        //     Gets or sets the background color of the legend cell.
        //
        // Returns:
        //     A System.Drawing.Color value used to draw the legend cell's background.
        [DefaultValue(typeof(Color), "")]
        [TypeConverter(typeof(ColorConverter))]
        [Description("Gets or sets the background color of the legend cell.")]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        public Color BackColor { get; set; }
        //
        // Summary:
        //     Gets or sets the number of horizontal cells used to draw the contents of
        //     the legend cell.
        //
        // Returns:
        //     An integer value that represents the number of horizontal cells used to draw
        //     the legend cell contents. The default value is 1.
        [DefaultValue(1)]
        [Description("Gets or sets the number of horizontal cells used to draw the contents of the legend cell.")]
        public int CellSpan { get; set; }
        //
        // Summary:
        //     Gets or sets the legend cell type.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.LegendCellType enumeration
        //     value that represents the type that will be used to define the contents of
        //     the legend cell. The default value is System.Windows.Forms.DataVisualization.Charting.LegendCellType.Text.
        [ParenthesizePropertyName(true)]
        [Description("Gets or sets the legend cell type.")]
        [DefaultValue(LegendCellType.Text)]
        public LegendCellType CellType { get; set; }
        //
        // Summary:
        //     Gets or sets the font for the legend cell.
        //
        // Returns:
        //     A System.Drawing.Font object. The default value is "Microsoft Sans Serif,
        //     8pt".
        [Description("Gets or sets the font for the legend cell.")]
        [DefaultValue("")]
        public Font Font { get; set; }
        //
        // Summary:
        //     Gets or sets the text color of the legend cell.
        //
        // Returns:
        //     A System.Drawing.Color value used to draw the legend cell text. The default
        //     value is System.Drawing.Color.Empty.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [DefaultValue(typeof(Color), "")]
        [Description("Gets or sets the text color of the legend cell.")]
        [TypeConverter(typeof(ColorConverter))]
        public Color ForeColor { get; set; }
        //
        // Summary:
        //     Gets or sets the name of the image in a legend cell.
        //
        // Returns:
        //     A string value that represents the name of the image to be used in the legend
        //     cell.
        [DefaultValue("")]
        [Description(" Gets or sets the name of the image in a legend cell.")]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ImageValueEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        public string Image { get; set; }
        //
        // Summary:
        //     Gets or sets the size of the image in a legend cell.
        //
        // Returns:
        //     A System.Drawing.Size value that represents the size of the image. The default
        //     value is (0,0), which means the original size of the image will be used.
        [DefaultValue(typeof(Size), "0, 0")]
        [Description("Gets or sets the size of the image in a legend cell.")]
        public Size ImageSize { get; set; }
        //
        // Summary:
        //     Gets or sets a color which will be replaced with a transparent color when
        //     the image is drawn.
        //
        // Returns:
        //     A System.Drawing.Color value that is used as a transparent color in a legend
        //     cell image.
        [Description("Gets or sets a color which will be replaced with a transparent color when the image is drawn.")]
        [TypeConverter(typeof(ColorConverter))]
        [DefaultValue(typeof(Color), "")]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        public Color ImageTransparentColor { get; set; }
        //
        // Summary:
        //     Gets or sets the margins of the legend cell.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.Margins object that represents
        //     the top, bottom, left, and right values of the legend cell margins. The default
        //     values for top, bottom, left and right are "0,0,15,15".
        //[DefaultValue(new PMSMargins(null))]
        [NotifyParentProperty(true)]
        [Description("Gets or sets the margins of the legend cell.")]
        public PMSMargins Margins { get; set; }
        //
        // Summary:
        //     Gets or sets the name of the legend cell.
        //
        // Returns:
        //     A string value that represents the text name of the legend cell.
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (Parent != null && Parent is List<string>)
                {
                    List<string> parent = (List<string>)Parent;

                    foreach (var node in parent)
                    {
                        if (value == node)
                            throw new Exception("属性名已存在！");
                    }
                    name = value;
                    return;
                }
                name = value;
            }
        }

        private string name;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Parent { get; set; }
        //
        // Summary:
        //     Gets or sets the size of the series symbol that is displayed in the legend
        //     cell.
        //
        // Returns:
        //     A System.Drawing.Size object that represents the size of the series symbol.
        //     The default values for the width and height of the symbol are 200 and 70,
        //     respectively.
        [DefaultValue(typeof(Size), "200, 70")]
        [Description(" Gets or sets the tooltip text for the legend cell.")]
        public Size SeriesSymbolSize { get; set; }
        //
        // Summary:
        //     Gets or sets the legend cell text.
        //
        // Returns:
        //     A string value that represents the text for the legend cell. The default
        //     value is a zero-length string.
        [DefaultValue("")]
        [Description(" Gets or sets the tooltip text for the legend cell.")]
        public string Text { get; set; }
        //
        // Summary:
        //     Gets or sets the tooltip text for the legend cell.
        //
        // Returns:
        //     A string value that represents the tooltip for the legend cell. The default
        //     value is a zero-length string.
        [DefaultValue("")]
        [Description(" Gets or sets the tooltip text for the legend cell.")]
        public string ToolTip { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public PMSLegendCell(LegendCell LegendItem1)
        {
            if (LegendItem1 == null)
            {
                this.Alignment = ContentAlignment.MiddleCenter;
                this.BackColor = Color.Empty;
                this.CellSpan = 1;
                this.CellType = LegendCellType.Text;
                //this.Font = new Font("Microsoft Sans Serif", 8);
                this.ForeColor = Color.Empty;
                this.Image = "";
                this.ImageSize = new Size(0, 0);
                this.ImageTransparentColor = Color.Empty;
                this.Margins = new PMSMargins(null);
                this.Name = "cell1";
                this.SeriesSymbolSize = new Size(200, 70);
                this.Text = "";
                this.ToolTip = "";
            }
            else
            {
                this.Alignment = LegendItem1.Alignment;
                this.BackColor = LegendItem1.BackColor;
                this.CellSpan = LegendItem1.CellSpan;
                this.CellType = LegendItem1.CellType;
                this.Font = LegendItem1.Font;
                this.ForeColor = LegendItem1.ForeColor;
                this.Image = LegendItem1.Image;
                this.ImageSize = LegendItem1.ImageSize;
                this.ImageTransparentColor = LegendItem1.ImageTransparentColor;
                this.Margins = new PMSMargins(LegendItem1.Margins);
                this.Name = LegendItem1.Name;
                this.SeriesSymbolSize = LegendItem1.SeriesSymbolSize;
                this.Text = LegendItem1.Text;
                if (!string.IsNullOrEmpty(this.ToolTip))
                    this.ToolTip = LegendItem1.ToolTip;
            }
        }

        public LegendCell ToPMSLegendCell()
        {
            LegendCell LegendItem1 = new LegendCell();
            SetLegendCell(LegendItem1);
            return LegendItem1;
        }
        public void SetLegendCell(LegendCell LegendItem1)
        {
            if (LegendItem1 == null)
                return;

            LegendItem1.Alignment = this.Alignment;
            LegendItem1.BackColor = this.BackColor;
            LegendItem1.CellSpan = this.CellSpan;
            LegendItem1.CellType = this.CellType;
            LegendItem1.Font = this.Font;
            LegendItem1.ForeColor = this.ForeColor;
            LegendItem1.Image = this.Image;
            LegendItem1.ImageSize = this.ImageSize;
            LegendItem1.ImageTransparentColor = this.ImageTransparentColor;
            LegendItem1.Margins = this.Margins.ToPMSMargins();
            LegendItem1.Name = this.Name;
            LegendItem1.SeriesSymbolSize = this.SeriesSymbolSize;
            LegendItem1.Text = this.Text;
            LegendItem1.ToolTip = this.ToolTip;
        }
    }
    internal class LegendCellCollectionEditor : UITypeEditor
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
                    PMSLegendItem control = null;
                    if (context.Instance.GetType() == typeof(PMSLegendItem))
                        control = (PMSLegendItem)context.Instance;

                    FormLegendCellCollection form1 = new FormLegendCellCollection();
                    form1.DataList = control.Cells;
                    if (DialogResult.OK == editorService.ShowDialog(form1))
                    {
                        control.Cells = form1.DataList;
                    }
                    return value;
                }
            }

            return value;
        }
    }
    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class PMSLegendCellColumn
    {
        // Summary:
        //     Gets or sets the legend column content alignment.
        //
        // Returns:
        //     A System.Drawing.ContentAlignment value that represents the alignment of
        //     the legend cell contents. The default value is System.Drawing.ContentAlignment.MiddleCenter.
        [DefaultValue(ContentAlignment.MiddleCenter)]
        [Description(" Gets or sets the legend column content alignment.")]
        public ContentAlignment Alignment { get; set; }
        //
        // Summary:
        //     Gets or sets the background color of the legend cell column.
        //
        // Returns:
        //     A System.Drawing.Color value used to draw the legend cell column background.The
        //     default value is System.Drawing.Color.Empty.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [DefaultValue(typeof(Color), "")]
        [TypeConverter(typeof(ColorConverter))]
        [Description("Gets or sets the background color of the legend cell column.")]
        public Color BackColor { get; set; }
        //
        // Summary:
        //     Gets or sets the legend cell column type.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.LegendCellColumnType enumeration
        //     value. This value represents the type that will be used to define the legend
        //     cell column contents. The default value is System.Windows.Forms.DataVisualization.Charting.LegendCellColumnType.Text.
        [ParenthesizePropertyName(true)]
        [DefaultValue(LegendCellColumnType.Text)]
        [Description("Gets or sets the legend cell column type.")]
        public LegendCellColumnType ColumnType { get; set; }
        //
        // Summary:
        //     Gets or sets the font for the legend column text.
        //
        // Returns:
        //     A System.Drawing.Font object. The default value is null.
        [DefaultValue("")]
        [Description(" Gets or sets the font for the legend column text.")]
        public Font Font { get; set; }
        //
        // Summary:
        //     Gets or sets the text color of the legend cell column.
        //
        // Returns:
        //     A System.Drawing.Color value used to draw the legend cell column text. The
        //     default value is System.Drawing.Color.Empty.
        [TypeConverter(typeof(ColorConverter))]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [DefaultValue(typeof(Color), "")]
        [Description("Gets or sets the text color of the legend cell column.")]
        public Color ForeColor { get; set; }
        //
        // Summary:
        //     Gets or sets the horizontal text alignment of the legend cell column header.
        //
        // Returns:
        //     A System.Drawing.StringAlignment enumeration value.
        [DefaultValue(typeof(StringAlignment), "Center")]
        [Description("Gets or sets the horizontal text alignment of the legend cell column header.")]
        public StringAlignment HeaderAlignment { get; set; }
        //
        // Summary:
        //     Gets or sets the background color for the legend cell column header.
        //
        // Returns:
        //     A System.Drawing.Color value used to draw the background of the legend cell
        //     column header. The default value is System.Drawing.Color.Empty.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [TypeConverter(typeof(ColorConverter))]
        [DefaultValue(typeof(Color), "")]
        [Description("Gets or sets the background color for the legend cell column header.")]
        public Color HeaderBackColor { get; set; }
        //
        // Summary:
        //     Gets or sets the font used in the legend column header.
        //
        // Returns:
        //     A System.Drawing.Font object. The default value is "Microsoft Sans Serif,
        //     8pt, Bold".
        [DefaultValue(typeof(Font), "Microsoft Sans Serif, 8pt, style=Bold")]
        [Description("Gets or sets the font used in the legend column header.")]
        public Font HeaderFont { get; set; }
        //
        // Summary:
        //     Gets or sets the color of the legend column header text.
        //
        // Returns:
        //     A System.Drawing.Color value.
        [DefaultValue(typeof(Color), "Black")]
        [TypeConverter(typeof(ColorConverter))]
        [Description("Gets or sets the color of the legend column header text.")]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        public Color HeaderForeColor { get; set; }
        //
        // Summary:
        //     Gets or sets the text for the legend cell column header.
        //
        // Returns:
        //     A string value that represents the legend cell column header text. The default
        //     value is a zero-length string.
        [DefaultValue("")]
        [Description("Gets or sets the text for the legend cell column header.")]
        public string HeaderText { get; set; }

        //
        // Summary:
        //     Gets or sets the margins of the legend cell column.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.Margins object that represents
        //     the top, bottom, left, and right values of the legend cell margins. The default
        //     values for top, bottom, left and right are "0,0,15,15".
        [NotifyParentProperty(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //[DefaultValue(typeof(Margins), "0,0,15,15")]
        [Description("Gets or sets the margins of the legend cell column.")]
        public PMSMargins Margins { get; set; }
        //
        // Summary:
        //     Gets or sets the maximum width of the legend cell column.
        //
        // Returns:
        //     An integer value that represents the maximum width of the legend cell column.
        //     The default value is minus one (-1), which means that the maximum width is
        //     automatically calculated.
        [DefaultValue(-1)]
        [Description(" An integer value that represents the maximum width of the legend cell column.The default value is minus one (-1), which means that the maximum width is automatically calculated.")]
        public int MaximumWidth { get; set; }
        //
        // Summary:
        //     Gets or sets the minimum width of the legend cell column.
        //
        // Returns:
        //     An integer value that represents the minimum width of the legend cell column.
        //     The default value is minus one (-1), which means that the minimum width is
        //     automatically calculated.
        [DefaultValue(-1)]
        [Description("An integer value that represents the minimum width of the legend cell column.The default value is minus one (-1), which means that the minimum width is automatically calculated.")]
        public int MinimumWidth { get; set; }
        //
        // Summary:
        //     Gets or sets the name of the legend cell column.
        //
        // Returns:
        //     A string value that represents the text name of the legend cell column.
        [Description(" Gets or sets the name of the legend cell column.")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (Parent != null && Parent is List<string>)
                {
                    List<string> parent = (List<string>)Parent;

                    foreach (var node in parent)
                    {
                        if (value == node)
                            throw new Exception("属性名已存在！");
                    }
                    name = value;
                    return;
                }
                name = value;
            }
        }

        private string name;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Parent { get; set; }
        //
        // Summary:
        //     Gets or sets the size of the series symbol displayed in a legend cell column.
        //
        // Returns:
        //     A System.Drawing.Size object that represents the size of the series symbol.
        //     The default values for the width and height of the symbol are 200 and 70,
        //     respectively.
        [DefaultValue(typeof(Size), "200, 70")]
        [Description("Gets or sets the size of the series symbol displayed in a legend cell column.")]
        public Size SeriesSymbolSize { get; set; }
        //
        // Summary:
        //     Gets or sets the text of the legend cell column.
        //
        // Returns:
        //     A string value that represents the text for the legend cell column. The default
        //     value is a zero-length string.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.KeywordsStringEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [DefaultValue("#LEGENDTEXT")]
        [Description("Gets or sets the text of the legend cell column.")]
        public string Text { get; set; }
        //
        // Summary:
        //     Gets or sets the tooltip text for the legend cell column.
        //
        // Returns:
        //     A string value that represents the tooltip for the legend cell column. The
        //     default value is a zero-length string.
        [DefaultValue("Gets or sets the tooltip text for the legend cell column.")]
        [Description("")]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.KeywordsStringEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        public string ToolTip { get; set; }


        public override string ToString()
        {
            return Name;
        }

        public PMSLegendCellColumn(LegendCellColumn LegendItem1)
        {
            if (LegendItem1 == null)
            {
                this.Alignment = ContentAlignment.MiddleCenter;
                this.BackColor = Color.Empty;
                this.ColumnType = LegendCellColumnType.Text;
                //this.Font = new Font("Microsoft Sans Serif", 8);
                this.ForeColor = Color.Empty;
                this.HeaderAlignment = StringAlignment.Center;
                this.HeaderBackColor = Color.Empty;
                this.HeaderFont = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
                this.HeaderForeColor = Color.Black;
                this.HeaderText = "";
                this.Margins = new PMSMargins(null);
                this.MaximumWidth = -1;
                this.MinimumWidth = -1;
                this.Name = "column1";
                this.SeriesSymbolSize = new Size(200, 70);
                this.Text = "#LEGENDTEXT";
                this.ToolTip = "";
            }
            else
            {
                this.Alignment = LegendItem1.Alignment;
                this.BackColor = LegendItem1.BackColor;
                this.ColumnType = LegendItem1.ColumnType;
                this.Font = LegendItem1.Font;
                this.ForeColor = LegendItem1.ForeColor;
                this.HeaderAlignment = LegendItem1.HeaderAlignment;
                this.HeaderBackColor = LegendItem1.HeaderBackColor;
                this.HeaderFont = LegendItem1.HeaderFont;
                this.HeaderForeColor = LegendItem1.HeaderForeColor;
                this.HeaderText = LegendItem1.HeaderText;
                this.Margins = new PMSMargins(LegendItem1.Margins);
                this.MaximumWidth = LegendItem1.MaximumWidth;
                this.MinimumWidth = LegendItem1.MinimumWidth;
                this.Name = LegendItem1.Name;
                this.SeriesSymbolSize = LegendItem1.SeriesSymbolSize;
                this.Text = LegendItem1.Text;
                if (!string.IsNullOrEmpty(this.ToolTip))
                    this.ToolTip = LegendItem1.ToolTip;
            }
        }

        public LegendCellColumn ToPMSLegendCellColumn()
        {
            LegendCellColumn LegendItem1 = new LegendCellColumn();
            SetLegendCellColumn(LegendItem1);
            return LegendItem1;
        }
        public void SetLegendCellColumn(LegendCellColumn LegendItem1)
        {
            if (LegendItem1 == null)
                return;

            LegendItem1.Alignment = this.Alignment;
            LegendItem1.BackColor = this.BackColor;
            LegendItem1.ColumnType = this.ColumnType;
            LegendItem1.Font = this.Font;
            LegendItem1.ForeColor = this.ForeColor;
            LegendItem1.HeaderAlignment = this.HeaderAlignment;
            LegendItem1.HeaderBackColor = this.HeaderBackColor;
            LegendItem1.HeaderFont = this.HeaderFont;
            LegendItem1.HeaderForeColor = this.HeaderForeColor;
            LegendItem1.HeaderText = this.HeaderText;
            LegendItem1.Margins = this.Margins.ToPMSMargins();
            LegendItem1.MaximumWidth = this.MaximumWidth;
            LegendItem1.MinimumWidth = this.MinimumWidth;
            LegendItem1.Name = this.Name;
            LegendItem1.SeriesSymbolSize = this.SeriesSymbolSize;
            LegendItem1.Text = this.Text;
            if (!string.IsNullOrEmpty(LegendItem1.ToolTip))
                LegendItem1.ToolTip = this.ToolTip;
        }
    }
    internal class LegendCellColumnCollectionEditor : UITypeEditor
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
                    PMSLegend control = null;
                    if (context.Instance.GetType() == typeof(PMSLegend))
                        control = (PMSLegend)context.Instance;

                    FormLegendCellColumnCollection form1 = new FormLegendCellColumnCollection();
                    form1.DataList = control.CellColumns;
                    if (DialogResult.OK == editorService.ShowDialog(form1))
                    {
                        control.CellColumns = form1.DataList;
                    }
                    return value;
                }
            }

            return value;
        }
    }

    //public enum PMSTitleType { Main, XAxis, YAxis }

    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class PMSTitle
    {
        ///// <summary>
        ///// 区分标题类型（主标题，X轴，Y轴）
        ///// </summary>
        //[Browsable(false)]
        //public PMSTitleType TitleType { get; set; }

        //
        // Summary:
        //     Gets or sets the name of the Title.
        //
        // Returns:
        //     A string value that represents the text name of the legend cell column.
        [Description(" Gets or sets the name of the Title.")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (Parent != null && Parent is TreeNode)
                {
                    TreeNode parent = (TreeNode)Parent;

                    foreach (TreeNode node in parent.Nodes)
                    {
                        if (value == node.Text)
                            throw new Exception("属性名已存在！");
                    }
                    name = value;
                    return;
                }
                name = value;
            }
        }

        private string name;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Parent { get; set; }
        // Summary:
        //     Gets or sets the alignment of the title.
        //
        // Returns:
        //     A System.Drawing.ContentAlignment enumeration value that represents the title
        //     alignment within the text drawing region. The default value is System.Drawing.ContentAlignment.MiddleCenter.
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [Category("外观")]
        [DefaultValue(ContentAlignment.MiddleCenter)]
        public ContentAlignment Alignment { get; set; }
        //
        // Summary:
        //     Gets or sets the background color of the title.
        //
        // Returns:
        //     A System.Drawing.Color value. The default color is System.Drawing.Color.White.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [TypeConverter(typeof(ColorConverter))]
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [Category("外观")]
        [DefaultValue(typeof(Color), "")]
        public Color BackColor { get; set; }
        //
        // Summary:
        //     Gets or sets the orientation for the background gradient of a title. Also
        //     determines whether a gradient is used.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.GradientStyle enumeration.
        //     The default value is System.Windows.Forms.DataVisualization.Charting.GradientStyle.None.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.GradientEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [Category("外观")]
        [DefaultValue(GradientStyle.None)]
        public GradientStyle BackGradientStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the hatching style for the title.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle enumeration.
        //     The default value is System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle.None.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.HatchStyleEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [Category("外观")]
        [DefaultValue(ChartHatchStyle.None)]
        public ChartHatchStyle BackHatchStyle { get; set; }

        //
        // Summary:
        //     Gets or sets the secondary color of the title background.
        //
        // Returns:
        //     A System.Drawing.Color value used for the secondary color of a background
        //     with hatching or gradient fill. The default value is System.Drawing.Color.Empty.
        [NotifyParentProperty(true)]
        [TypeConverter(typeof(ColorConverter))]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Category("外观")]
        [DefaultValue(typeof(Color), "")]
        public Color BackSecondaryColor { get; set; }
        //
        // Summary:
        //     Gets or sets the border color of the title.
        //
        // Returns:
        //     A T:System.Drawing.Color value. The default color is System.Drawing.Color.Empty.
        [TypeConverter(typeof(ColorConverter))]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [Category("外观")]
        [DefaultValue(typeof(Color), "")]
        public Color BorderColor { get; set; }
        //
        // Summary:
        //     Gets or sets the border style of the title.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartDashStyle enumeration
        //     value that determines the border style of the title.
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [Category("外观")]
        [DefaultValue(ChartDashStyle.Solid)]
        public ChartDashStyle BorderDashStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the border width of the title.
        //
        // Returns:
        //     An integer value that determines the border width, in pixels, of the title.
        [DefaultValue(1)]
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [Category("外观")]
        public int BorderWidth { get; set; }
        //
        // Summary:
        //     Gets or sets the name of the System.Windows.Forms.DataVisualization.Charting.ChartArea
        //     where the title will be docked.
        //
        // Returns:
        //     A string value that represents the name of the System.Windows.Forms.DataVisualization.Charting.ChartArea
        //     where this title will be docked. The default value is the empty string.
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [Category("停靠")]
        [Browsable(false)]
        [DefaultValue("NotSet")]
        public string DockedToChartArea { get; set; }
        //
        // Summary:
        //     Gets or sets a value that determines where the title is docked.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.Docking enumeration value.
        //     The default value is System.Windows.Forms.DataVisualization.Charting.Docking.Top.
        [Bindable(true)]
        [Category("停靠")]
        [DefaultValue(Docking.Top)]
        [NotifyParentProperty(true)]
        public Docking Docking { get; set; }
        //
        // Summary:
        //     Gets or sets the positive or negative offset of the docked title position.
        //
        // Returns:
        //     An integer value.
        [NotifyParentProperty(true)]
        [DefaultValue(0)]
        [Bindable(true)]
        [Category("停靠")]
        public int DockingOffset { get; set; }
        //
        // Summary:
        //     Gets or sets the font for the title.
        //
        // Returns:
        //     A System.Drawing.Font object. Defaults to "Microsoft Sans Serif, 8pt".
        [DefaultValue(typeof(Font), "Microsoft Sans Serif, 8pt")]
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [Category("外观")]
        public Font Font { get; set; }
        //
        // Summary:
        //     Gets or sets the color of the title text.
        //
        // Returns:
        //     A System.Drawing.Color value.
        [Bindable(true)]
        [Category("外观")]
        [NotifyParentProperty(true)]
        [TypeConverter(typeof(ColorConverter))]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [DefaultValue(typeof(Color), "Black")]
        public Color ForeColor { get; set; }
        //
        // Summary:
        //     Gets or sets a flag that specifies whether the title should be docked within
        //     a System.Windows.Forms.DataVisualization.Charting.ChartArea object.
        //
        // Returns:
        //     True if the title will be docked within a chart area, false if the title
        //     will be docked outside the chart area. The default value is false.
        [NotifyParentProperty(true)]
        [DefaultValue(true)]
        [Bindable(true)]
        [Category("停靠")]
        public bool IsDockedInsideChartArea { get; set; }
        //
        // Summary:
        //     Gets or sets an System.Windows.Forms.DataVisualization.Charting.ElementPosition
        //     object, which can be used to get or set the position of the title.
        //
        // Returns:
        //     An System.Windows.Forms.DataVisualization.Charting.ElementPosition value
        //     that represents the position of the title. The default value is System.Windows.Forms.DataVisualization.Charting.ElementPosition.Auto.
        [Bindable(true)]
        [Category("外观")]
        [NotifyParentProperty(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PMSElementPosition Position { get; set; }
        //
        // Summary:
        //     Gets or sets the shadow color for the title.
        //
        // Returns:
        //     A System.Drawing.Color value. The default color is System.Drawing.Color.Black.
        [TypeConverter(typeof(ColorConverter))]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Category("外观")]
        [DefaultValue(typeof(Color), "128, 0, 0, 0")]
        [NotifyParentProperty(true)]
        public Color ShadowColor { get; set; }
        //
        // Summary:
        //     Gets or sets the shadow offset, in pixels, of the title.
        //
        // Returns:
        //     An integer value that represents the shadow offset, in pixels, of the title.
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [DefaultValue(0)]
        [Category("外观")]
        public int ShadowOffset { get; set; }
        //
        // Summary:
        //     Gets or sets the text for the title.
        //
        // Returns:
        //     A string value that represents the text for the title.
        [DefaultValue("")]
        [Bindable(true)]
        [ParenthesizePropertyName(true)]
        [NotifyParentProperty(true)]
        [Category("外观")]
        public string Text { get; set; }
        //
        // Summary:
        //     Gets or sets the orientation of the text in the title.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.TextOrientation enumeration
        //     value.
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [Category("外观")]
        [DefaultValue(TextOrientation.Auto)]
        public TextOrientation TextOrientation { get; set; }
        //
        // Summary:
        //     Gets or sets the style of the text for the title.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.TextStyle enumeration value
        //     that determines the style of the text for the title. The default value is
        //     System.Windows.Forms.DataVisualization.Charting.TextStyle.Default.
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [Category("外观")]
        [DefaultValue(TextStyle.Default)]
        public TextStyle TextStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the tooltip for the title.
        //
        // Returns:
        //     A string value that represents the tooltip.
        [Bindable(true)]
        [DefaultValue("")]
        [Category("外观")]
        public string ToolTip { get; set; }
        //
        // Summary:
        //     Gets or sets the visibility flag of the title.
        //
        // Returns:
        //     True if visible, otherwise false.
        [ParenthesizePropertyName(true)]
        [DefaultValue(true)]
        [Category("外观")]
        public virtual bool Visible { get; set; }

        public PMSTitle(Title title)
        {
            if (title == null)
            {
                this.Alignment = ContentAlignment.MiddleCenter;
                this.BackColor = Color.Empty;
                this.BackGradientStyle = GradientStyle.None;
                this.BackHatchStyle = ChartHatchStyle.None;
                this.BackSecondaryColor = Color.Empty;
                this.BorderColor = Color.Empty;
                this.BorderDashStyle = ChartDashStyle.Solid;
                this.BorderWidth = 1;
                this.DockedToChartArea = "";
                this.Docking = Docking.Top;
                this.DockingOffset = 0;
                this.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);

                this.ForeColor = Color.Black;
                this.IsDockedInsideChartArea = true;
                this.Position = new PMSElementPosition(null);
                this.ShadowColor = Color.Empty;
                this.ShadowOffset = 0;
                this.Text = "";
                this.TextOrientation = TextOrientation.Auto;
                this.TextStyle = TextStyle.Default;
                this.ToolTip = "";
                this.Visible = true;
                //this.TitleType = PMSTitleType.Main;
            }
            else
            {
                this.Alignment = title.Alignment;
                this.BackColor = title.BackColor;
                this.BackGradientStyle = title.BackGradientStyle;
                this.BackHatchStyle = title.BackHatchStyle;
                this.BackSecondaryColor = title.BackSecondaryColor;
                this.BorderColor = title.BorderColor;
                this.BorderDashStyle = title.BorderDashStyle;
                this.BorderWidth = title.BorderWidth;
                this.DockedToChartArea = title.DockedToChartArea;
                this.Docking = title.Docking;
                this.DockingOffset = title.DockingOffset;
                this.Font = title.Font;

                this.ForeColor = title.ForeColor;
                this.IsDockedInsideChartArea = title.IsDockedInsideChartArea;
                this.Position = new PMSElementPosition(title.Position);
                this.ShadowColor = title.ShadowColor;
                this.ShadowOffset = title.ShadowOffset;
                this.Text = title.Text;
                this.TextOrientation = title.TextOrientation;
                this.TextStyle = title.TextStyle;
                this.ToolTip = title.ToolTip;
                this.Visible = title.Visible;
                this.Name = title.Name;
                //this.TitleType = (PMSTitleType)title.Tag;
            }
        }
        public Title ToTitle()
        {
            Title title = new Title();
            SetTitle(title);
            return title;
        }
        public void SetTitle(Title title)
        {
            if (title == null)
                return;

            title.Alignment = this.Alignment;
            title.BackColor = this.BackColor;
            title.BackGradientStyle = this.BackGradientStyle;
            title.BackHatchStyle = this.BackHatchStyle;
            title.BackSecondaryColor = this.BackSecondaryColor;
            title.BorderColor = this.BorderColor;
            title.BorderDashStyle = this.BorderDashStyle;
            title.BorderWidth = this.BorderWidth;
            //title.DockedToChartArea = this.DockedToChartArea;
            title.Docking = this.Docking;
            title.DockingOffset = this.DockingOffset;
            title.Font = this.Font;

            title.ForeColor = this.ForeColor;
            title.IsDockedInsideChartArea = this.IsDockedInsideChartArea;
            title.Position = this.Position.ToElementPosition();
            title.ShadowColor = this.ShadowColor;
            title.ShadowOffset = this.ShadowOffset;
            if (!string.IsNullOrEmpty(this.Text))
                title.Text = this.Text;
            title.TextOrientation = this.TextOrientation;
            title.TextStyle = this.TextStyle;
            if (!string.IsNullOrEmpty(this.ToolTip))
                title.ToolTip = this.ToolTip;
            title.Visible = this.Visible;
            //title.Tag = this.TitleType;
        }
    }


    // Summary:
    //     Specifies a chart type for a System.Windows.Forms.DataVisualization.Charting.Series.
    public enum PMSSeriesChartType
    {
        // Summary:
        //     Point chart type.
        Point = 0,
        //
        // Summary:
        //     FastPoint chart type.
        FastPoint = 1,
        //
        // Summary:
        //     Bubble chart type.
        Bubble = 2,
        //
        // Summary:
        //     Line chart type.
        Line = 3,
        Spline = 4,
        StepLine = 5,
        //
        // Summary:
        //     FastLine chart type.
        FastLine = 6,
        //
        // Summary:
        //     Bar chart type.
        Bar = 7,
        StackedBar = 8,
        StackedBar100 = 9,
        //
        // Summary:
        //     Column chart type.
        Column = 10,
        StackedColumn = 11,
        StackedColumn100 = 12,
        //
        // Summary:
        //     Area chart type.
        Area = 13,
        SplineArea = 14,
        StackedArea = 15,
        StackedArea100 = 16,
        //
        // Summary:
        //     Pie chart type.
        Pie = 17,
        //
        // Summary:
        //     Doughnut chart type.
        Doughnut = 18,
        Stock = 19,
        //
        // Summary:
        //     Candlestick chart type.
        Candlestick = 20,
        //
        // Summary:
        //     Range chart type.
        Range = 21,
        SplineRange = 22,
        //
        // Summary:
        //     RangeBar chart type.
        RangeBar = 23,
        //
        // Summary:
        //     Range column chart type.
        RangeColumn = 24,
        //
        // Summary:
        //     Radar chart type.
        Radar = 25,
        //
        // Summary:
        //     Polar chart type.
        Polar = 26,
        //
        // Summary:
        //     Error bar chart type.
        ErrorBar = 27,
        //
        // Summary:
        //     Box plot chart type.
        BoxPlot = 28,
        //
        // Summary:
        //     Renko chart type.
        Renko = 29,
        ThreeLineBreak = 30,
        //
        // Summary:
        //     Kagi chart type.
        Kagi = 31,
        //
        // Summary:
        //     PointAndFigure chart type.
        PointAndFigure = 32,
        //
        // Summary:
        //     Funnel chart type.
        Funnel = 33,
        //
        // Summary:
        //     Pyramid chart type.
        Pyramid = 34,
    }

    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class PMSChartApp
    {
        //
        // Summary:
        //     Gets or sets a value that determines whether anti-aliasing is used when text
        //     and graphics are drawn.
        //
        // Returns:
        //     An System.Windows.Forms.DataVisualization.Charting.AntiAliasingStyles enumeration
        //     value.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.FlagsEnumUITypeEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Description("Gets or sets a value that determines whether anti-aliasing is used when text  and graphics are drawn.")]
        [DefaultValue(typeof(AntiAliasingStyles), "All")]
        public AntiAliasingStyles AntiAliasing { get; set; }
        //
        // Summary:
        //     Gets or sets the background color of the System.Windows.Forms.DataVisualization.Charting.Chart
        //     object.
        //
        // Returns:
        //     A System.Drawing.Color value. The default color is System.Drawing.Color.White.
        [TypeConverter(typeof(ColorConverter))]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [DefaultValue(typeof(Color), "White")]
        [Bindable(true)]
        [Description("Gets or sets the background color of the System.Windows.Forms.DataVisualization.Charting.Chart object.")]
        public Color BackColor { get; set; }
        //
        // Summary:
        //     Gets or sets the orientation for the background gradient of a System.Windows.Forms.DataVisualization.Charting.Chart
        //     control. Also determines whether a gradient is used.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.GradientStyle enumeration.
        //     The default value is System.Windows.Forms.DataVisualization.Charting.GradientStyle.None.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.GradientEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [DefaultValue(GradientStyle.None)]
        [Description("Gets or sets the orientation for the background gradient of a System.Windows.Forms.DataVisualization.Charting.Chart control. Also determines whether a gradient is used.")]
        public GradientStyle BackGradientStyle { get; set; }

        //
        // Summary:
        //     Gets or sets the hatching style of the System.Windows.Forms.DataVisualization.Charting.Chart
        //     control.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle enumeration.
        //     The default value is System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle.None.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.HatchStyleEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [DefaultValue(ChartHatchStyle.None)]
        [Description("Gets or sets the hatching style of the System.Windows.Forms.DataVisualization.Charting.Chart control.")]
        public ChartHatchStyle BackHatchStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the background image of the System.Windows.Forms.DataVisualization.Charting.Chart
        //     control.
        //
        // Returns:
        //     A string value that represents the URL of an image file. The default value
        //     is an empty string.
        [DefaultValue("")]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ImageValueEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [Browsable(false)]
        public string BackImage { get; set; }
        //
        // Summary:
        //     Gets or sets the background image alignment used for the System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.Unscaled
        //     drawing mode.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle
        //     enumeration value. The default value is System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.TopLeft.
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [DefaultValue(ChartImageAlignmentStyle.TopLeft)]
        [Browsable(false)]
        [Description("Gets or sets the background image alignment used for the System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.Unscaled drawing mode.")]
        public ChartImageAlignmentStyle BackImageAlignment { get; set; }
        //
        // Summary:
        //     Gets or sets the color of the System.Windows.Forms.DataVisualization.Charting.Chart
        //     control that will be displayed as transparent.
        //
        // Returns:
        //     A System.Drawing.Color value that will be displayed as transparent when the
        //     chart image is drawn. The default value is System.Drawing.Color.Empty.
        [Bindable(true)]
        [TypeConverter(typeof(ColorConverter))]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [DefaultValue(typeof(Color), "")]
        [NotifyParentProperty(true)]
        [Description("")]
        [Browsable(false)]
        public Color BackImageTransparentColor { get; set; }
        //
        // Summary:
        //     Gets or sets the drawing mode for the background image of the System.Windows.Forms.DataVisualization.Charting.Chart
        //     control.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode enumeration
        //     value. The default value is System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.Tile.
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [Description("")]
        [Browsable(false)]
        public ChartImageWrapMode BackImageWrapMode { get; set; }
        //
        // Summary:
        //     Gets or sets the secondary color of the chart background.
        //
        // Returns:
        //     A System.Drawing.Color value. The default value is System.Drawing.Color.Empty.
        [DefaultValue(typeof(Color), "")]
        [TypeConverter(typeof(ColorConverter))]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Description("Gets or sets the secondary color of the chart background.")]
        public Color BackSecondaryColor { get; set; }

        //
        // Summary:
        //     Gets or sets the color of the border line.
        //
        // Returns:
        //     A System.Drawing.Color value.
        [Bindable(true)]
        [DefaultValue(typeof(Color), "White")]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [TypeConverter(typeof(ColorConverter))]
        [Description("Gets or sets the color of the border line.")]
        public Color BorderlineColor { get; set; }
        //
        // Summary:
        //     Gets or sets the style of the border line.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartDashStyle enumeration
        //     value.
        [Bindable(true)]
        [DefaultValue(ChartDashStyle.NotSet)]
        [Description("Gets or sets the style of the border line.")]
        public ChartDashStyle BorderlineDashStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the width of the border line.
        //
        // Returns:
        //     An integer value.
        [DefaultValue(1)]
        [Bindable(true)]
        [Description("Gets or sets the width of the border line.")]
        public int BorderlineWidth { get; set; }
        //
        // Summary:
        //     Gets or sets a System.Windows.Forms.DataVisualization.Charting.BorderSkin
        //     object, which provides border skin functionality for the System.Windows.Forms.DataVisualization.Charting.Chart
        //     control.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.BorderSkin object.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [Description("Gets or sets a System.Windows.Forms.DataVisualization.Charting.BorderSkin object, which provides border skin functionality for the System.Windows.Forms.DataVisualization.Charting.Chart control.")]
        public PMSBorderSkin BorderSkin { get; set; }

        //
        // Summary:
        //     Gets or sets the cursor that is displayed when the mouse pointer is held
        //     over the control.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.Cursor object

        [Description("Gets or sets the cursor that is displayed when the mouse pointer is held over the control.")]
        public System.Windows.Forms.Cursor Cursor { get; set; }

        //
        // Summary:
        //     Gets or sets a flag that determines if a smooth gradient is applied when
        //     shadows are drawn.
        //
        // Returns:
        //     True if shadows are drawn using smoothing, false if they are not. The default
        //     value is true.
        [Bindable(true)]
        [DefaultValue(true)]
        [Description(" Gets or sets a flag that determines if a smooth gradient is applied when  shadows are drawn.")]
        public bool IsSoftShadows { get; set; }

        //
        // Summary:
        //     Gets or sets the palette for the System.Windows.Forms.DataVisualization.Charting.Chart
        //     control.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartColorPalette enumeration
        //     value that determines the palette to be used.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ColorPaletteEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [DefaultValue(ChartColorPalette.BrightPastel)]
        [Browsable(false)]
        [Description(" Gets or sets the palette for the System.Windows.Forms.DataVisualization.Charting.Chart  control.")]
        public ChartColorPalette Palette { get; set; }
        //
        // Summary:
        //     Gets or sets an array of custom palette colors.
        //
        // Returns:
        //     An array of System.Drawing.Color objects that represent the set of colors
        //     used for series on the chart.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //[DefaultValue()]
        [Browsable(false)]
        [Description("Gets or sets an array of custom palette colors.")]
        public Color[] PaletteCustomColors { get; set; }
        //
        // Summary:
        //     Gets or sets a flag that determines whether non-critical exceptions should
        //     be suppressed.
        //
        // Returns:
        //     True if non-critical exceptions should be suppressed, otherwise false. The
        //     default value is false.
        [DefaultValue(false)]
        [Description("Gets or sets a flag that determines whether non-critical exceptions should be suppressed.")]
        public bool SuppressExceptions { get; set; }
        //
        // Summary:
        //     Gets or sets the System.Windows.Forms.DataVisualization.Charting.TextAntiAliasingQuality
        //     type to use when applying anti-aliasing to text.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.TextAntiAliasingQuality
        //     enumeration value. The default value is System.Windows.Forms.DataVisualization.Charting.TextAntiAliasingQuality.High.
        [DefaultValue(typeof(TextAntiAliasingQuality), "High")]
        [Bindable(true)]
        [Description("Gets or sets the System.Windows.Forms.DataVisualization.Charting.TextAntiAliasingQuality type to use when applying anti-aliasing to text.")]
        public TextAntiAliasingQuality TextAntiAliasingQuality { get; set; }

        public PMSChartApp(Chart Chart1)
        {
            if (Chart1 == null)
            {
                this.AntiAliasing = AntiAliasingStyles.All;
                this.BackColor = Color.White;
                this.BackGradientStyle = GradientStyle.None;
                this.BackHatchStyle = ChartHatchStyle.None;
                this.BackImage = "";
                this.BackImageAlignment = ChartImageAlignmentStyle.TopLeft;
                this.BackImageTransparentColor = Color.Empty;
                this.BackImageWrapMode = ChartImageWrapMode.Tile;
                this.BackSecondaryColor = Color.Empty;
                this.BorderlineColor = Color.White;
                this.BorderlineDashStyle = ChartDashStyle.NotSet;
                this.BorderlineWidth = 1;
                this.BorderSkin = new PMSBorderSkin(null);
                this.IsSoftShadows = true;
                //this.Palette = Chart1.Palette;
                //this.PaletteCustomColors
                this.SuppressExceptions = false;
                this.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
            }
            else
            {
                this.AntiAliasing = Chart1.AntiAliasing;
                this.BackColor = Chart1.BackColor;
                this.BackGradientStyle = Chart1.BackGradientStyle;
                this.BackHatchStyle = Chart1.BackHatchStyle;
                this.BackImage = Chart1.BackImage;
                this.BackImageAlignment = Chart1.BackImageAlignment;
                this.BackImageTransparentColor = Chart1.BackImageTransparentColor;
                this.BackImageWrapMode = Chart1.BackImageWrapMode;
                this.BackSecondaryColor = Chart1.BackSecondaryColor;
                this.BorderlineColor = Chart1.BorderlineColor;
                this.BorderlineDashStyle = Chart1.BorderlineDashStyle;
                this.BorderlineWidth = Chart1.BorderlineWidth;
                this.BorderSkin = new PMSBorderSkin(Chart1.BorderSkin);
                this.Cursor = Chart1.Cursor;
                this.IsSoftShadows = Chart1.IsSoftShadows;
                //this.Palette = Chart1.Palette;
                //this.PaletteCustomColors
                this.SuppressExceptions = Chart1.SuppressExceptions;
                this.TextAntiAliasingQuality = Chart1.TextAntiAliasingQuality;
            }
        }
        public Chart ToChart()
        {
            Chart Chart1 = new Chart();
            SetChart(Chart1);
            return Chart1;
        }
        public void SetChart(Chart Chart1)
        {
            if (Chart1 == null)
                return;

            Chart1.AntiAliasing = this.AntiAliasing;
            Chart1.BackColor = this.BackColor;
            Chart1.BackGradientStyle = this.BackGradientStyle;
            Chart1.BackHatchStyle = this.BackHatchStyle;
            Chart1.BackImage = this.BackImage;
            Chart1.BackImageAlignment = this.BackImageAlignment;
            Chart1.BackImageTransparentColor = this.BackImageTransparentColor;
            Chart1.BackImageWrapMode = this.BackImageWrapMode;
            Chart1.BackSecondaryColor = this.BackSecondaryColor;
            Chart1.BorderlineColor = this.BorderlineColor;
            Chart1.BorderlineDashStyle = this.BorderlineDashStyle;
            Chart1.BorderlineWidth = this.BorderlineWidth;
            Chart1.BorderSkin = this.BorderSkin.ToBorderSkin();
            Chart1.Cursor = this.Cursor;
            Chart1.IsSoftShadows = this.IsSoftShadows;
            //this.Palette = Chart1.Palette;
            //this.PaletteCustomColors
            Chart1.SuppressExceptions = this.SuppressExceptions;
            Chart1.TextAntiAliasingQuality = this.TextAntiAliasingQuality;
        }
    }
    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class PMSBorderSkin
    {
        // Summary:
        //     Gets or sets the background color of a skin frame.
        //
        // Returns:
        //     A System.Drawing.Color value.
        [NotifyParentProperty(true)]
        [DefaultValue(typeof(Color), "Gray")]
        [TypeConverter(typeof(ColorConverter))]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [Description("Gets or sets the background color of a skin frame.")]
        public Color BackColor { get; set; }
        //
        // Summary:
        //     Gets or sets the background gradient style of a skin frame.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.GradientStyle enumeration
        //     value.
        [Bindable(true)]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.GradientEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [NotifyParentProperty(true)]
        [DefaultValue(GradientStyle.None)]
        [Description("Gets or sets the background gradient style of a skin frame.")]
        public GradientStyle BackGradientStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the background hatch style of a skin frame.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle enumeration
        //     value.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.HatchStyleEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [DefaultValue(ChartHatchStyle.None)]
        [Description("Gets or sets the background hatch style of a skin frame.")]
        public ChartHatchStyle BackHatchStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the background image of a skin frame.
        //
        // Returns:
        //     A string value.
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [DefaultValue("")]
        [Description("Gets or sets the background image of a skin frame.")]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ImageValueEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        public string BackImage { get; set; }
        //
        // Summary:
        //     Gets or sets the background image alignment of a skin frame.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle
        //     enumeration value.
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [DefaultValue(ChartImageAlignmentStyle.TopLeft)]
        [Description("Gets or sets the background image alignment of a skin frame.")]
        public ChartImageAlignmentStyle BackImageAlignment { get; set; }
        //
        // Summary:
        //     Gets or sets a color that is replaced with a transparent color when the background
        //     image of a border skin frame is drawn.
        //
        // Returns:
        //     A System.Drawing.Color value.
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [DefaultValue(typeof(Color), "")]
        [TypeConverter(typeof(ColorConverter))]
        [Description("Gets or sets a color that is replaced with a transparent color when the background image of a border skin frame is drawn.")]
        public Color BackImageTransparentColor { get; set; }
        //
        // Summary:
        //     Gets or sets the drawing mode for the background image of a border skin frame.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode enumeration
        //     value.
        [NotifyParentProperty(true)]
        [Bindable(true)]
        [DefaultValue(ChartImageWrapMode.Tile)]
        [Description("Gets or sets the drawing mode for the background image of a border skin frame.")]
        public ChartImageWrapMode BackImageWrapMode { get; set; }
        //
        // Summary:
        //     Gets or sets the secondary background color of a border skin frame.
        //
        // Returns:
        //     A System.Drawing.Color value.
        [NotifyParentProperty(true)]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [TypeConverter(typeof(ColorConverter))]
        [Bindable(true)]
        [DefaultValue(typeof(Color), "")]
        [Description("Gets or sets the secondary background color of a border skin frame.")]
        public Color BackSecondaryColor { get; set; }
        //
        // Summary:
        //     Gets or sets the border color of a skin frame.
        //
        // Returns:
        //     A System.Drawing.Color value.
        [TypeConverter(typeof(ColorConverter))]
        [Bindable(true)]
        [DefaultValue(typeof(Color), "Black")]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [NotifyParentProperty(true)]
        [Description("Gets or sets the border color of a skin frame.")]
        public Color BorderColor { get; set; }
        //
        // Summary:
        //     Gets or sets the style of the border line of a border skin frame.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.ChartDashStyle enumeration
        //     value.
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [DefaultValue(ChartDashStyle.NotSet)]
        [Description("Gets or sets the style of the border line of a border skin frame.")]
        public ChartDashStyle BorderDashStyle { get; set; }
        //
        // Summary:
        //     Gets or sets the width of the border line of a border skin frame.
        //
        // Returns:
        //     An integer value.
        [NotifyParentProperty(true)]
        [DefaultValue(1)]
        [Description("Gets or sets the width of the border line of a border skin frame.")]
        [Bindable(true)]
        public int BorderWidth { get; set; }
        //
        // Summary:
        //     Gets or sets the page color of a border skin.
        //
        // Returns:
        //     A System.Drawing.Color value.
        [DefaultValue(typeof(Color), "White")]
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [TypeConverter(typeof(ColorConverter))]
        [Description("Gets or sets the page color of a border skin.")]
        public Color PageColor { get; set; }
        //
        // Summary:
        //     Gets or sets the style of a border skin.
        //
        // Returns:
        //     A System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle enumeration
        //     value.
        [ParenthesizePropertyName(true)]
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [DefaultValue(BorderSkinStyle.None)]
        [Description("Gets or sets the style of a border skin.")]
        public BorderSkinStyle SkinStyle { get; set; }

        public PMSBorderSkin(BorderSkin BorderSkin1)
        {
            if (BorderSkin1 == null)
            {
                this.BackColor = Color.Gray;
                this.BackGradientStyle = GradientStyle.None;
                this.BackHatchStyle = ChartHatchStyle.None;
                this.BackImage = "";
                this.BackImageAlignment = ChartImageAlignmentStyle.TopLeft;
                this.BackImageTransparentColor = Color.Empty;
                this.BackImageWrapMode = ChartImageWrapMode.Tile;
                this.BackSecondaryColor = Color.Empty;
                this.BorderColor = Color.Black;
                this.BorderDashStyle = ChartDashStyle.NotSet;
                this.BorderWidth = 1;
                this.PageColor = Color.White;
                this.SkinStyle = BorderSkinStyle.None;
            }
            else
            {
                this.BackColor = BorderSkin1.BackColor;
                this.BackGradientStyle = BorderSkin1.BackGradientStyle;
                this.BackHatchStyle = BorderSkin1.BackHatchStyle;
                this.BackImage = BorderSkin1.BackImage;
                this.BackImageAlignment = BorderSkin1.BackImageAlignment;
                this.BackImageTransparentColor = BorderSkin1.BackImageTransparentColor;
                this.BackImageWrapMode = BorderSkin1.BackImageWrapMode;
                this.BackSecondaryColor = BorderSkin1.BackSecondaryColor;
                this.BorderColor = BorderSkin1.BorderColor;
                this.BorderDashStyle = BorderSkin1.BorderDashStyle;
                this.BorderWidth = BorderSkin1.BorderWidth;
                this.PageColor = BorderSkin1.PageColor;
                this.SkinStyle = BorderSkin1.SkinStyle;
            }
        }

        public BorderSkin ToBorderSkin()
        {
            BorderSkin BorderSkin1 = new BorderSkin();
            SetBorderSkin(BorderSkin1);
            return BorderSkin1;
        }
        public void SetBorderSkin(BorderSkin BorderSkin1)
        {
            if (BorderSkin1 == null)
                return;
            BorderSkin1.BackColor = this.BackColor;
            BorderSkin1.BackGradientStyle = this.BackGradientStyle;
            BorderSkin1.BackHatchStyle = this.BackHatchStyle;
            BorderSkin1.BackImage = this.BackImage;
            BorderSkin1.BackImageAlignment = this.BackImageAlignment;
            BorderSkin1.BackImageTransparentColor = this.BackImageTransparentColor;
            BorderSkin1.BackImageWrapMode = this.BackImageWrapMode;
            BorderSkin1.BackSecondaryColor = this.BackSecondaryColor;
            BorderSkin1.BorderColor = this.BorderColor;
            BorderSkin1.BorderDashStyle = this.BorderDashStyle;
            BorderSkin1.BorderWidth = this.BorderWidth;
            BorderSkin1.PageColor = this.PageColor;
            BorderSkin1.SkinStyle = this.SkinStyle;
        }
    }
    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class PMSAnnotation
    {
        public PMSAnnotation(LineAnnotation Annotation1)
        {
            if (Annotation1 == null)
            {
            }
            else
            {
                this.enable = Annotation1.AllowAnchorMoving;
                this.Width = Annotation1.LineWidth;
                this.color = Annotation1.LineColor;
                this.StartStyle = Annotation1.StartCap;
                this.EndStyle = Annotation1.EndCap;
                this.LineDashStyle = Annotation1.LineDashStyle;
            }
        }

        public bool enable = true;

        int width;
        [Description("获取或设置批注的线宽")]
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        Color color;
        [Description("获取或设置批注的颜色")]
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        LineAnchorCapStyle startStyle;
        [Description("获取或设置批注的起点模式")]
        public LineAnchorCapStyle StartStyle
        {
            get { return startStyle; }
            set { startStyle = value; }
        }

        LineAnchorCapStyle endStyle;
        [Description("获取或设置批注的终结模式")]
        public LineAnchorCapStyle EndStyle
        {
            get { return endStyle; }
            set { endStyle = value; }
        }

        ChartDashStyle lineDashStyle;
        [Description("获取或设置批注的显示模式")]
        public ChartDashStyle LineDashStyle
        {
            get { return lineDashStyle; }
            set { lineDashStyle = value; }
        }
        // public Annotation ToAnnotation()
        //{
        //Annotation Annotation1 = new Annotation();
        //}
        public void SetAnnotation(LineAnnotation Annotation1)
        {
            Annotation1.AllowAnchorMoving = this.enable;
            Annotation1.LineWidth = this.Width;
            Annotation1.LineColor = this.color;
            Annotation1.StartCap = this.StartStyle;
            Annotation1.EndCap = this.EndStyle;
            Annotation1.LineDashStyle = lineDashStyle;
        }
    }
}