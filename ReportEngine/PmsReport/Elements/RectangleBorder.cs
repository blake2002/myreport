using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PMS.Libraries.ToolControls.Report.Element
{
    /// <summary>
    /// 矩形状的边框
    /// </summary>
    [Serializable]
    public class RectangleBorder : ElementBorder 
    {
        public override GraphicsPath GraphicsPath
        {
            get
            {
                if (null != _ownerElement)
                {
                    GraphicsPath gp = new GraphicsPath();
                    gp.AddRectangle(new Rectangle((int)_ownerElement.MoveX, (int)_ownerElement.MoveY,
                                        _ownerElement.Width, _ownerElement.Height));
                    return gp;
                }

                return null;
            }
        }

        protected bool _hasTopBorder = true;
        /// <summary>
        /// 是否拥有上边框
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(true)]
        [Browsable(false)]
        [Description("是否拥有左边框")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(true)]
        [Category("PMS控件属性")]
        //[DesignOnly(true)]
        public bool HasTopBorder 
        {
            get
            {
                return _hasTopBorder;
            }
            set
            {
                _hasTopBorder = value;
                if (null != _ownerElement && _ownerElement.CanInvalidate)
                {
                    _ownerElement.Invalidate();
                }
            }
        }

        protected bool _hasLeftBorder = true;
        /// <summary>
        /// 是否拥有左边框
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(true)]
        [Browsable(false)]
        [Description("是否拥有左边框")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(true)]
        [Category("PMS控件属性")]
        //[DesignOnly(true)]
        public bool HasLeftBorder 
        {
            get
            {
                return _hasLeftBorder;
            }
            set
            {
                _hasLeftBorder = value;
                if (null != _ownerElement && _ownerElement.CanInvalidate)
                {
                    _ownerElement.Invalidate();
                }
            }
        }

        protected bool _hasBottomBorder = true;
        /// <summary>
        /// 是否拥有下边框
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(true)]
        [Browsable(false)]
        [Description("是否拥有下边框")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(true)]
        [Category("PMS控件属性")]
        //[DesignOnly(true)]
        public bool HasBottomBorder 
        {
            get
            {
                return _hasBottomBorder;
            }
            set
            {
                _hasBottomBorder = value;
                if (null != _ownerElement && _ownerElement.CanInvalidate )
                {
                    _ownerElement.Invalidate();
                }
            }
        }

        protected bool _hasRightBorder = true;
        /// <summary>
        /// 是否拥有右边框
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(true)]
        [Browsable(false)]
        [Description("是否拥有右边框")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(true)]
        [Category("PMS控件属性")]
        //[DesignOnly(true)]
        public bool HasRightBorder
        {
            get
            {
                return _hasRightBorder;
            }
            set
            {
                _hasRightBorder = value;
                if (null != _ownerElement && _ownerElement.CanInvalidate)
                {
                    _ownerElement.Invalidate();
                }
                
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public int MoveX
        {
            get;
            set;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public int MoveY
        {
            get;
            set;
        }

        public override string Name
        {
            get
            {
                return "矩形";
            }            
        }

        //public RectangleBorder()
        //{ 
            
        //}

         public RectangleBorder()
         {
         }

        public RectangleBorder(IElement element) 
        {
            OwnerElement = element;
        }

        /// <summary>
        /// 打印边框
        /// </summary>
        /// <param name="g">需要绘制在绘图图面<see cref="IPrintable"/></param>
        /// <param name="x">x偏移位置</param>
        /// <param name="y">y的偏移位置</param>
        public override void Print(Canvas ca, float x, float y)
        {
            if (null == ca)
            {
                return;
            }

            if (null == _ownerElement)
            {
                return;
            }
            if (!_ownerElement.HasBorder)
            {
                return;
            }

            Graphics g = ca.Graphics;
            if (null == g)
            {
                return;
            }

            if (this.BorderWidth > 0)
            {
                if (HasLeftBorder || HasTopBorder
                        || HasRightBorder || HasBottomBorder)
                {
                    Pen pen = new Pen(this.BorderColor, this.BorderWidth);
                    pen.DashStyle = this.DashStyle;
                    float halfPenWidth = (float)(pen.Width / 2.0f);
                    if (HasTopBorder && _ownerElement.HasTopBorder)
                    {
                        g.DrawLine(pen, x , y + halfPenWidth,
                            x + _ownerElement.Width - halfPenWidth, y + halfPenWidth);
                    }
                    if (HasLeftBorder && _ownerElement.HasLeftBorder)
                    {
                        g.DrawLine(pen, x + halfPenWidth, y, 
                            x + halfPenWidth, y + _ownerElement.Height - halfPenWidth);
                    }
                    if (HasBottomBorder && _ownerElement.HasBottomBorder)
                    {
                        g.DrawLine(pen, x, y + _ownerElement.Height - halfPenWidth, 
                            x + _ownerElement.Width-halfPenWidth , y + _ownerElement.Height  - halfPenWidth);
                    }
                    if (HasRightBorder && _ownerElement.HasRightBorder)
                    {
                        g.DrawLine(pen, x + _ownerElement.Width  - halfPenWidth, y,
                            x + _ownerElement.Width  - halfPenWidth, y + _ownerElement.Height-(pen.Width == 1?1:0));
                    }
                    pen.Dispose();
                }
            }
            Control ctrl = _ownerElement as Control;
            if (null != ctrl)
            {
                //GraphicsPath gp = new GraphicsPath();
                //gp.AddRectangle(new RectangleF(0, 0, _ownerElement.Width, _ownerElement.Height));
                //Region region = new Region(gp);
                //((Control)_ownerElement).Region = region.Clone();
                //gp.Dispose();
            }
        }

        /// <summary>
        /// 打印边框
        /// </summary>
        /// <param name="g">需要绘制在绘图图面<see cref="IPrintable"/></param>
        /// <param name="x">x偏移位置</param>
        /// <param name="y">y的偏移位置</param>
        public override void DirectDraw(Canvas ca, float x, float y, float dpiZoom)
        {
            if (null == ca)
            {
                return;
            }

            if (null == _ownerElement)
            {
                return;
            }
            if (!_ownerElement.HasBorder)
            {
                return;
            }

            Graphics g = ca.Graphics;
            if (null == g)
            {
                return;
            }

            if (this.BorderWidth > 0)
            {
                if (HasLeftBorder || HasTopBorder
                        || HasRightBorder || HasBottomBorder)
                {
                    Pen pen = new Pen(this.BorderColor, this.BorderWidth * dpiZoom);
                    pen.DashStyle = this.DashStyle;
                    float halfPenWidth = (float)(pen.Width / 2.0f);
                    if (HasTopBorder && _ownerElement.HasTopBorder)
                    {
                        g.DrawLine(pen, x, y + halfPenWidth,
                            x + _ownerElement.Width, y + halfPenWidth);
                    }
                    if (HasLeftBorder && _ownerElement.HasLeftBorder)
                    {
                        g.DrawLine(pen, x + halfPenWidth, y,
                            x + halfPenWidth, y + _ownerElement.Height);
                    }
                    if (HasBottomBorder && _ownerElement.HasBottomBorder)
                    {
                        g.DrawLine(pen, x, y + _ownerElement.Height - halfPenWidth,
                            x + _ownerElement.Width, y + _ownerElement.Height - halfPenWidth);
                    }
                    if (HasRightBorder && _ownerElement.HasRightBorder)
                    {
                        g.DrawLine(pen, x + _ownerElement.Width - halfPenWidth, y,
                            x + _ownerElement.Width - halfPenWidth, y + _ownerElement.Height);
                    }
                    pen.Dispose();
                }
            }
            Control ctrl = _ownerElement as Control;
            if (null != ctrl)
            {
                //GraphicsPath gp = new GraphicsPath();
                //gp.AddRectangle(new RectangleF(0, 0, _ownerElement.Width, _ownerElement.Height));
                //Region region = new Region(gp);
                //((Control)_ownerElement).Region = region.Clone();
                //gp.Dispose();
            }
        }


        public override void FillAreaBackground(Graphics g,float x,float y)
        {
            if (null != _ownerElement)
            {
                if (Color.Transparent != _ownerElement.BackColor)
                {
                    using (Brush brush = new SolidBrush(_ownerElement.BackColor))
                    {
                        g.FillRectangle(brush, x, y, _ownerElement.Width, _ownerElement.Height);
                    }
                }
            }
        }

        public override object Clone()
        {
            RectangleBorder border = new RectangleBorder(null);
            border.DashStyle = this.DashStyle;
            border.BorderWidth = this.BorderWidth;
            border.BorderColor = this.BorderColor;
            border.VerticalScale = this.VerticalScale;
            border.HasBottomBorder = this.HasBottomBorder;
            border.HasLeftBorder = this.HasLeftBorder;
            border.HasRightBorder = this.HasRightBorder;
            border.HasTopBorder = this.HasTopBorder;
            border.HorizontalScale = this.HorizontalScale;
            border.MoveX = this.MoveX;
            border.MoveY = this.MoveY;
            return border;
        }
    }
}
