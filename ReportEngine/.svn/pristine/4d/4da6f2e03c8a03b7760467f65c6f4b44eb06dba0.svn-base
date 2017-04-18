using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMS.Libraries.ToolControls.Report.Element;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace PMS.Libraries.ToolControls.Report.Element
{
    /// <summary>
    /// Border的抽象类,目的是为了方便扩展border的样式
    /// </summary>
    [Serializable]
    public abstract class ElementBorder:IPrintable,IResizable,ICloneable,IDirectDrawable
    {

        protected float _orginalWidth = -1;

        protected float _horizontalScale = 1;
        /// <summary>
        /// 横向比例因子
        /// </summary>
        public float HorizontalScale
        {
            get
            {
                return _horizontalScale;
            }
            set
            {
                _horizontalScale = value;
            }
        }

        protected float _vericalScale = 1;

        /// <summary>
        /// 纵向比例因子
        /// </summary>
        public float VerticalScale
        {
            get
            {
                return _vericalScale;
            }
            set
            {
                _vericalScale = value;
            }
        }


        /// <summary>
        /// 该边框的所有者
        /// </summary>
        [NonSerialized]
        protected IElement _ownerElement = null;
        public IElement OwnerElement
        {
            get
            {
                return _ownerElement;
            }
            set
            {
                _ownerElement = value;
                if (null != value)
                {
                    IResizable resize = value as IResizable;
                    if (null != resize)
                    {
                        HorizontalScale = resize.HorizontalScale;
                        VerticalScale = resize.VerticalScale;
                    }
                }
            }
        }
        /// <summary>
        /// 打印边框
        /// <remarks>如果有Border，应该在Border内部绘制背景
        /// 因为背景应该绘制的符合Border</remarks>
        /// </summary>
        /// <param name="g">需要绘制在绘图图面<see cref="IPrintable"/></param>
        /// <param name="x">x偏移位置</param>
        /// <param name="y">y的偏移位置</param>
        public abstract void Print(Canvas ca, float x, float y);

        public abstract void DirectDraw(Canvas ca, float x, float y, float dpiZoom = 1);

        protected Color _borderColor = Color.Black;

        /// <summary>
        /// 边框颜色
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BorderColor 
        {
            get
            {
                return _borderColor;
            }
            set
            {
                _borderColor = value;
                if (null != _ownerElement && _ownerElement.CanInvalidate)
                {
                    _ownerElement.Invalidate();
                }
            }
        }

        protected DashStyle _dashType = DashStyle.Solid;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DashStyle DashStyle
        {
            get
            {
                return _dashType;
            }
            set
            {
                _dashType = value;
                if (null != _ownerElement && _ownerElement.CanInvalidate)
                {
                    _ownerElement.Invalidate();
                }
            }
        }

        protected float _borderWidth = 1;
        /// <summary>
        /// 边框宽度
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public float BorderWidth 
        {
            get
            {
                return _borderWidth;
            }
            set
            {
                _borderWidth = value;
                if (null != _ownerElement && _ownerElement.CanInvalidate)
                {
                    _ownerElement.Invalidate();
                }
            }
        }

        public abstract string Name
        {
            get;
        }

        public void Zoom(float hScale, float vScale)
        {
            //if (hScale < 0 || vScale < 0)
            //{
            //    throw new ArgumentException("任意因子不可小于0");
            //}
            //if (hScale == 0)
            //{
            //    hScale = 1;
            //}
            //if (vScale == 0)
            //{
            //    vScale = 1;
            //}
            //if (_orginalWidth == -1)
            //{
            //    _orginalWidth = this._borderWidth;
            //}
            //this._borderWidth = (int)(hScale * this._orginalWidth);
            //if (_borderWidth >=0 && _borderWidth<1)
            //{
            //    _borderWidth = 1f;
            //}
            //_horizontalScale = hScale;
            //_vericalScale = vScale;
        }

        public void Zoom()
        {
            Zoom(_horizontalScale, _vericalScale);
        }

        public abstract GraphicsPath GraphicsPath
        {
            get;
        }

        /// <summary>
        /// 绘制边框范围所包围区域内的背景
        /// </summary>
        public abstract void FillAreaBackground(Graphics g,float x,float y);

        public virtual object Clone()
        {
            return null;
        }

    }
}
