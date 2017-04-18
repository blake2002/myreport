using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using System.ComponentModel;
using System.Drawing;

namespace PMS.Libraries.ToolControls.PMSChart
{
    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class CurveY:PMSChartArea
    {
        public CurveY(ChartArea Aim)
            : base(Aim)
        {
            base.BackColor = Color.Transparent;
            base.BorderColor = Color.Transparent;
            base.AxisX.MajorGrid.Enabled = false;
            base.AxisX.MajorTickMark.Enabled = false;
            base.AxisX.LabelStyle.Enabled = false;
            base.AxisY.MajorGrid.Enabled = false;
        }

        #region 2011年11月1日 屏蔽基类中在此派生类中不需要的属性
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override AreaAlignmentOrientations AlignmentOrientation
        {
            get
            {
                return base.AlignmentOrientation;
            }
            set
            {
                base.AlignmentOrientation = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override AreaAlignmentStyles AlignmentStyle
        {
            get
            {
                return base.AlignmentStyle;
            }
            set
            {
                base.AlignmentStyle = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string AlignWithChartArea
        {
            get
            {
                return base.AlignWithChartArea;
            }
            set
            {
                base.AlignWithChartArea = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override PMSChartArea3DStyle Area3DStyle
        {
            get
            {
                return base.Area3DStyle;
            }
            set
            {
                base.Area3DStyle = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string Axes
        {
            get
            {
                return base.Axes;
            }
            set
            {
                base.Axes = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override PMSAxis AxisX
        {
            get
            {
                return base.AxisX;
            }
            set
            {
                base.AxisX = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override PMSAxis AxisX2
        {
            get
            {
                return base.AxisX2;
            }
            set
            {
                base.AxisX2 = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override PMSAxis AxisY2
        {
            get
            {
                return base.AxisY2;
            }
            set
            {
                base.AxisY2 = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override System.Drawing.Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override GradientStyle BackGradientStyle
        {
            get
            {
                return base.BackGradientStyle;
            }
            set
            {
                base.BackGradientStyle = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override ChartHatchStyle BackHatchStyle
        {
            get
            {
                return base.BackHatchStyle;
            }
            set
            {
                base.BackHatchStyle = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string BackImage
        {
            get
            {
                return base.BackImage;
            }
            set
            {
                base.BackImage = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override ChartImageAlignmentStyle BackImageAlignment
        {
            get
            {
                return base.BackImageAlignment;
            }
            set
            {
                base.BackImageAlignment = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override System.Drawing.Color BackImageTransparentColor
        {
            get
            {
                return base.BackImageTransparentColor;
            }
            set
            {
                base.BackImageTransparentColor = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override ChartImageWrapMode BackImageWrapMode
        {
            get
            {
                return base.BackImageWrapMode;
            }
            set
            {
                base.BackImageWrapMode = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override System.Drawing.Color BackSecondaryColor
        {
            get
            {
                return base.BackSecondaryColor;
            }
            set
            {
                base.BackSecondaryColor = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override System.Drawing.Color BorderColor
        {
            get
            {
                return base.BorderColor;
            }
            set
            {
                base.BorderColor = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override ChartDashStyle BorderDashStyle
        {
            get
            {
                return base.BorderDashStyle;
            }
            set
            {
                base.BorderDashStyle = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int BorderWidth
        {
            get
            {
                return base.BorderWidth;
            }
            set
            {
                base.BorderWidth = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override PMSCursor CursorX
        {
            get
            {
                return base.CursorX;
            }
            set
            {
                base.CursorX = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override PMSCursor CursorY
        {
            get
            {
                return base.CursorY;
            }
            set
            {
                base.CursorY = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override PMSElementPosition InnerPlotPosition
        {
            get
            {
                return base.InnerPlotPosition;
            }
            set
            {
                base.InnerPlotPosition = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool IsSameFontSizeForAllAxes
        {
            get
            {
                return base.IsSameFontSizeForAllAxes;
            }
            set
            {
                base.IsSameFontSizeForAllAxes = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override PMSElementPosition Position
        {
            get
            {
                return base.Position;
            }
            set
            {
                base.Position = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override System.Drawing.Color ShadowColor
        {
            get
            {
                return base.ShadowColor;
            }
            set
            {
                base.ShadowColor = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int ShadowOffset
        {
            get
            {
                return base.ShadowOffset;
            }
            set
            {
                base.ShadowOffset = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                base.Visible = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                base.Name = value;
            }
        }
        #endregion

        #region 需要的属性
        [Browsable(true)]
        public override PMSAxis AxisY
        {
            get
            {
                return base.AxisY;
            }
            set
            {
                base.AxisY = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsCopyChart
        {
            get;
            set;
        }
        #endregion
    }
}
