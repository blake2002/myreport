using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMS.Libraries.ToolControls.Report.Element;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Drawing.Design;
using PMS.Libraries.ToolControls.Report.Controls.Editor;

namespace PMS.Libraries.ToolControls.Report
{
    public class MyTestControl : ElementBase
    {
        private ContentAlignment _textAlign = ContentAlignment.MiddleLeft;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(0)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(true)]
        [Category("PMS控件属性")]
        public virtual ContentAlignment TextAlign
        {
            get
            {
                return _textAlign;
            }
            set
            {
                _textAlign = value;
                Invalidate();
            }
        }

        private bool _hasBorder = true;
        [Browsable(true)]
        [Category("PMS控件属性")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(false)]
        public override bool HasBorder
        {
            get
            {
                return _hasBorder;
            }
            set
            {  
                _hasBorder = value;
                Invalidate();
            }
        }

        //private Color _borderColor = Color.White;
        //[Browsable(true)]
        //[Category("PMS控件属性")]
        //[EditorBrowsable(EditorBrowsableState.Always)]
        //public override Color BorderColor
        //{
        //    get
        //    {
        //        return _borderColor;
        //    }
        //    set
        //    {
        //        _borderColor = value;
        //        Invalidate();
        //    }
        //}

        private string _borderName = string.Empty;
        public override string BorderName
        {
            get
            {
                if (string.IsNullOrEmpty(_borderName))
                {
                    return new RectangleBorder(null).Name;
                }
                return _borderName;
            }
            set
            {
                _borderName = value;
            }
        }


        public MyTestControl():base()
        {
            //背景的绘制是与Border相关联的
            //如果有Border那么Border中应该绘制背景
            //否则在控件内部绘制
            //Border = new RectangleBorder(this);
            //Border = new EllipseBorder(this);
        }

        public override void Print(Canvas ca, float x, float y)
        {
            //#region 暂时注释的代码
            //MoveX = x;
            //MoveY = y;
            //Graphics g = ca.Graphics;
            //SizeF textSize = g.MeasureString(this.Text, this.Font);
            //textSize.Width = textSize.Width * HorizontalScale;
            //textSize.Height = textSize.Height * VerticalScale;
            //float left = x;
            //float top = y;
            //if (AutoSize)
            //{
            //    this.Width = (int)textSize.Width;
            //    this.Height = (int)textSize.Height;
            //}
            //else
            //{
            //    //非自动大小的情况下的文本位置

            //    #region   计算文本的 XY坐标
            //    switch (TextAlign)
            //    {
            //        case ContentAlignment.BottomCenter:
            //            left += (this.Width - 2 * (null == Border?0:Border.BorderWidth) - textSize.Width) / 2 < 0 ? (null == Border?0:Border.BorderWidth) : (this.Width - 2 * (null == Border?0:Border.BorderWidth) - textSize.Width) / 2 + (null == Border?0:Border.BorderWidth);
            //            top += this.Height - textSize.Height - (null == Border?0:Border.BorderWidth) < 0 ? (null == Border?0:Border.BorderWidth) : this.Height - textSize.Height - (null == Border?0:Border.BorderWidth);
            //            break;
            //        case ContentAlignment.BottomLeft:
            //            top += this.Height - textSize.Height -2 * (null == Border?0:Border.BorderWidth) < 0 ? (null == Border?0:Border.BorderWidth) : this.Height - textSize.Height - (null == Border?0:Border.BorderWidth);
            //            left += (null == Border?0:Border.BorderWidth);
            //            break;
            //        case ContentAlignment.BottomRight:
            //            left += this.Width - textSize.Width - 2*(null == Border?0:Border.BorderWidth) < (null == Border?0:Border.BorderWidth) ? (null == Border?0:Border.BorderWidth) : this.Width - textSize.Width - (null == Border?0:Border.BorderWidth);
            //            top += this.Height - textSize.Height - (null == Border?0:Border.BorderWidth) < 0 ? (null == Border?0:Border.BorderWidth) : this.Height - textSize.Height - (null == Border?0:Border.BorderWidth); ;
            //            break;
            //        case ContentAlignment.MiddleCenter:
            //            left += (this.Width - 2 * (null == Border?0:Border.BorderWidth) - textSize.Width) / 2 < 0 ? (null == Border?0:Border.BorderWidth) : (this.Width - 2 * (null == Border?0:Border.BorderWidth) - textSize.Width) / 2 + (null == Border?0:Border.BorderWidth);
            //            top += (this.Height - 2 * (null == Border?0:Border.BorderWidth) - textSize.Height) / 2 < 0 ? (null == Border?0:Border.BorderWidth) : (this.Height  - 2 * (null == Border?0:Border.BorderWidth) - textSize.Height) / 2 + (null == Border?0:Border.BorderWidth);
            //            break;
            //        case ContentAlignment.MiddleLeft:
            //            top += (this.Height - 2 * (null == Border?0:Border.BorderWidth) - textSize.Height) / 2 < 0 ? (null == Border?0:Border.BorderWidth) : (this.Height - 2 * (null == Border?0:Border.BorderWidth) - textSize.Height) / 2 + (null == Border?0:Border.BorderWidth);
            //            left += (null == Border?0:Border.BorderWidth); ;
            //            break;
            //        case ContentAlignment.MiddleRight:
            //            left += this.Width - textSize.Width - (null == Border?0:Border.BorderWidth) < (null == Border?0:Border.BorderWidth) ? (null == Border?0:Border.BorderWidth) : this.Width - textSize.Width - (null == Border?0:Border.BorderWidth);
            //            top += (this.Height - 2 * (null == Border?0:Border.BorderWidth) - textSize.Height) / 2 < 0 ? (null == Border?0:Border.BorderWidth) : (this.Height - 2 * (null == Border?0:Border.BorderWidth) - textSize.Height) / 2 + (null == Border?0:Border.BorderWidth);
            //            break;
            //        case ContentAlignment.TopCenter:
            //            left += (this.Width - textSize.Width) / 2 < 0 ? (null == Border?0:Border.BorderWidth) : (this.Width - textSize.Width) / 2 + (null == Border?0:Border.BorderWidth);
            //            top += (null == Border?0:Border.BorderWidth);
            //            break;
            //        case ContentAlignment.TopLeft:
            //            top += (null == Border?0:Border.BorderWidth);
            //            left += (null == Border?0:Border.BorderWidth);
            //            break;
            //        case ContentAlignment.TopRight:
            //            left += this.Width - textSize.Width - (null == Border?0:Border.BorderWidth) < (null == Border?0:Border.BorderWidth) ? (null == Border?0:Border.BorderWidth) : this.Width - textSize.Width - (null == Border?0:Border.BorderWidth);
            //            top += (null == Border?0:Border.BorderWidth);
            //            break;

            //    }
            //    #endregion
            //}

            //// 上面的计算可能会改变控件的大小，所以将
            //// 边框的绘制放在放在这里
            //if (null != Border)
            //{
            //    Border.Print(ca, x, y);
            //}
            //else
            //{
            //    Brush brush = new SolidBrush(this.BackColor);
            //    g.FillRectangle(brush, x, y, x + this.Width, y + this.Height);
            //    brush.Dispose();
            //}
            //Brush foreBrush = new SolidBrush(this.ForeColor);
            //float tempWidth = this.Width - left + MoveX - (null == Border?0:Border.BorderWidth) - textSize.Width <= 0 ? this.Width - left + MoveX - (null == Border?0:Border.BorderWidth) : textSize.Width;
            //float tempHeight = this.Height - top + MoveY - (null == Border?0:Border.BorderWidth) - textSize.Height <= 0 ? this.Height - top + MoveY - (null == Border?0:Border.BorderWidth) : textSize.Height;
            //if (tempWidth != 0 && tempHeight != 0)
            //{
            //    g.DrawString(this.Text, this.Font, foreBrush, new RectangleF(left, top, tempWidth, tempHeight));
            //}
            //foreBrush.Dispose();
            //#endregion

        }
    }
}
