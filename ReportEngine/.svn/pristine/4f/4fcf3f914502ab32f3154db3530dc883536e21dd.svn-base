using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using PMS.Libraries.ToolControls.Report.Win32;
using System.Windows.Forms;

namespace PMS.Libraries.ToolControls.Report.Element
{
    [Serializable]
    public class EllipseBorder : ElementBorder
    {
        public override GraphicsPath GraphicsPath
        {
            get
            {
                if (null != _ownerElement)
                {
                    GraphicsPath gp = new GraphicsPath();
                    gp.AddRectangle(new Rectangle(0, 0, _ownerElement.Width, _ownerElement.Height));
                    return gp;
                }

                return null;
            }
        }

        public override string Name
        {
            get
            {
                return "椭圆";
            }
        }

        public EllipseBorder()
        { 
        
        }

        public EllipseBorder(IElement element)
        {
            _ownerElement = element;
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

            if (!_ownerElement.HasBorder)
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


            Pen pen = new Pen(this.BorderColor, this.BorderWidth);
            pen.DashStyle = this.DashStyle;
            g.DrawEllipse(pen, x, y,  _ownerElement.Width,  _ownerElement.Height);
            pen.Dispose();
            Control ctrl = _ownerElement as Control;
            if (null != ctrl)
            {
                //GraphicsPath gp = new GraphicsPath();
                //gp.AddEllipse(x, y, x + _ownerElement.Width, y + _ownerElement.Height);
                //Region region = new Region(gp);
                //Region oldRegion = null;
                //if (null != ctrl.Region)
                //{
                //    oldRegion = ctrl.Region;
                //}
                //((Control)_ownerElement).Region = region.Clone();
                //if (null != oldRegion)
                //{
                //    oldRegion.Dispose();
                //}
                //gp.Dispose();
            }

        }

        /// <summary>
        /// 打印边框
        /// </summary>
        /// <param name="g">需要绘制在绘图图面<see cref="IPrintable"/></param>
        /// <param name="x">x偏移位置</param>
        /// <param name="y">y的偏移位置</param>
        public override void DirectDraw(Canvas ca, float x, float y, float dpiZoom = 1)
        {
            if (null == ca)
            {
                return;
            }

            if (!_ownerElement.HasBorder)
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


            Pen pen = new Pen(this.BorderColor, this.BorderWidth * dpiZoom);
            pen.DashStyle = this.DashStyle;
            g.DrawEllipse(pen, x, y, _ownerElement.Width, _ownerElement.Height);
            pen.Dispose();
            Control ctrl = _ownerElement as Control;
            if (null != ctrl)
            {
                //GraphicsPath gp = new GraphicsPath();
                //gp.AddEllipse(x, y, x + _ownerElement.Width, y + _ownerElement.Height);
                //Region region = new Region(gp);
                //Region oldRegion = null;
                //if (null != ctrl.Region)
                //{
                //    oldRegion = ctrl.Region;
                //}
                //((Control)_ownerElement).Region = region.Clone();
                //if (null != oldRegion)
                //{
                //    oldRegion.Dispose();
                //}
                //gp.Dispose();
            }

        }

        public override void FillAreaBackground(Graphics g, float x, float y)
        {
            if (null == _ownerElement)
            {
                return;
            }
            if (Color.Transparent != _ownerElement.BackColor)
            {
                Brush brush = new SolidBrush(_ownerElement.BackColor);
                g.FillEllipse(brush, x, y,  _ownerElement.Width,  _ownerElement.Height);
                brush.Dispose();
            }
        }

        public override object Clone()
        {
            EllipseBorder border = new EllipseBorder(null);
            border.BorderColor = this.BorderColor;
            border.BorderWidth = this.BorderWidth;
            border.DashStyle = this.DashStyle;
            border.HorizontalScale = this.HorizontalScale;
            border.VerticalScale = this.VerticalScale;
            return border;
        }
    }
}
