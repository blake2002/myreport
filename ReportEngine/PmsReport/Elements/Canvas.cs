using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using PMS.Libraries.ToolControls.Report.Element;
using System.ComponentModel;
using PMS.Libraries.ToolControls.Report.Elements.Util;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;

namespace PMS.Libraries.ToolControls.Report.Element
{
    /// <summary>
    /// 显示报表的画布,虚拟画布
    /// </summary>
    public  class Canvas:IElement
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否拥有上边框
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(true)]
        [Browsable(false)]
        [Description("是否拥有左边框")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Localizable(true)]
        [Category("PMS控件属性")]
        public bool HasTopBorder
        {
            get;
            set;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(true)]
        [Browsable(false)]
        [Description("是否拥有左边框")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Localizable(true)]
        [Category("PMS控件属性")]
        public bool HasLeftBorder
        {
            get;
            set;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(true)]
        [Browsable(false)]
        [Description("是否拥有下边框")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Localizable(true)]
        [Category("PMS控件属性")]
        public bool HasBottomBorder
        {
            get;
            set;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(true)]
        [Browsable(false)]
        [Description("是否拥有右边框")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Localizable(true)]
        [Category("PMS控件属性")]
        public bool HasRightBorder
        {
            get;
            set;
        }

        /// <summary>
        /// 数据源
        /// </summary>
        public SourceField SourceField
        {
            get;
            set;
        }

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


        protected bool _canInvalidate = false;
        public bool CanInvalidate
        {
            get
            {
                return _canInvalidate;
            }
        }

        /// <summary>
        /// 元素的背景颜色
        /// </summary>
        public Color ForeColor { get; set; }

        #region  报表元素成员
        protected int _width = 0; 
        /// <summary>
        /// 元素宽
        /// </summary>
        public int Width 
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }

        protected int _height = 0;
        /// <summary>
        /// 元素高
        /// </summary>
        public int Height 
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        protected Control _parent = null;
        /// <summary>
        /// 报表元素的父元素
        /// <remarks>在报表模块都是<see cref="IElement"/>的派生类</remarks>
        /// </summary>
        public Control Parent 
        {
            get
            {
                return _parent;
            }
            set
            {
                if (null != value.GetType().GetInterface(typeof(IElement).FullName, false))
                {
                    throw new ArgumentException("值类型不匹配，必须为IElement的子类型");
                }
                _parent = value;
            }
        }

        protected Color _backColor = Color.White;
        /// <summary>
        /// 元素的背景颜色
        /// </summary>
        public Color BackColor 
        {
            get
            {
                return _backColor;
            }
            set
            {
                _backColor = value;
            }
        }

        protected Image _backgroundImange = null;
        /// <summary>
        /// 背景图片
        /// </summary>
        public Image BackgroundImage 
        {
            get
            {
                return _backgroundImange;
            }
            set
            {
                _backgroundImange = value;
            }
        }

        protected ImageLayout _backgroundImageLayout = ImageLayout.Zoom;
        /// <summary>
        /// 背景图片的布局
        /// </summary>
        public ImageLayout BackgroundImageLayout 
        {
            get
            {
                return _backgroundImageLayout;
            }
            set
            {
                _backgroundImageLayout = value;
            }
        }

        protected bool _hasBorder = false;
        /// <summary>
        /// 是否拥有边框
        /// </summary>
        public bool HasBorder 
        {
            get
            {
                return _hasBorder;
            }
            set
            {
                _hasBorder = value;
            }
        }

        protected Color _borderColor = Color.White;
        /// <summary>
        /// 边框颜色
        /// </summary>
        public Color BorderColor 
        {
            get
            {
                return _borderColor;
            }
            set
            {
                _borderColor = value;
            }
        }

        protected int _borderWidth = 0;
        /// <summary>
        /// 边框的线条的宽度
        /// </summary>
        public int BorderWidth 
        {
            get
            {
                return _borderWidth;
            }
            set
            {
                _borderWidth = value;
            }
        }

        protected ElementBorder _border = null;
        /// <summary>
        /// 元素边框
        /// </summary>
        public ElementBorder Border 
        {
            get
            {
                return _border;
            }
            set
            {
                _border = value;
            }
        }

        protected bool _autoSize = false;
        /// <summary>
        /// 是否自动伸展大小
        /// </summary>
        public bool AutoSize 
        {
            get
            {
                return _autoSize;
            }
            set
            {
                _autoSize = value;
            }
        }

        #endregion 
 
        /// <summary>
        /// 文本的宽度
        /// </summary>
        public int ContentWidth
        {
            get;
            set;
        }

        public int ContentHeight
        {
            get;
            set;
        }

        /// <summary>
        /// 与边框的距离
        /// </summary>
        public int Padding
        {
            get;
            set;
        }

        /// <summary>
        /// 是否还有需要打印的
        /// </summary>
        public bool HasMore
        {
            get;
            set;
        }

        /// <summary>
        /// X偏移量
        /// </summary>
        public float MoveX
        {
            get;
            set;
        }

        /// <summary>
        /// Y偏移量
        /// </summary>
        public float MoveY
        {
            get;
            set;
        }

        /// <summary>
        /// 需要绘制到的对象的图画画面类
        /// </summary>
        public Graphics Graphics
        {
            get;
            set;
        }

        protected Point _location = new Point();
        public Point Location
        {
            get 
            {
                return _location;
            }
            set
            {
                _location = value;
            }
        }

        protected List<ExternData> _externDatas = new List<ExternData>();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<ExternData> ExternDatas
        {
            get
            {
                return _externDatas;
            }
            set
            {
                _externDatas = value;
            }
        }

        public string Text
        {
            get;
            set;
        }

        public Font Font
        {
            get;
            set;
        }

        public virtual string BorderName
        {
            get;
            set;
        }

        private bool _visible = true;
        /// <summary>
        /// 是否可见
        /// </summary>
        public bool Visible
        {
            get
            {
                return _visible;
            }
            set
            {
                _visible = value;
            }
        }

        private MESVarType _mestType = MESVarType.MESString;
        /// <summary>
        /// 数据类型
        /// </summary>
        [Category("MES控件属性")]
        public MESVarType MESType
        {
            get
            {
                return _mestType;
            }
            set
            {
                _mestType = value;
            }
        }

        [Browsable(false)]
        IElement IElement.Parent
        {
            get
            {
                return null;
            }
            set
            { 
                
            }
        }

        /// <summary>
        /// 额外的存储结构
        /// </summary>
        public ExtendObject ExtendObject { get; set; }

        public void Invalidate()
        { 
            //
        }

        public Canvas(Graphics g):this(g,0,0)
        {
        }

        public Canvas(Graphics g, int width, int height)
        {
            Graphics = g;
            this.Width = width;
            this.Height = height;
            this.ContentWidth = this.Width;
            this.ContentHeight = this.Height;
            PrepareCanvas();
        }
 
        public void PrepareCanvas()
        {
            if (null != Graphics)
            {
                Brush brush = new SolidBrush(this.BackColor);
                Graphics.FillRectangle(brush, 0, 0, this.Width, this.Height);
                if (null != Border)
                {
                    Border.Print(this, 0, 0);
                }
            }
        }

        public void Zoom(float hScale, float vScale)
        {
            if (hScale < 0 || vScale < 0)
            {
                throw new ArgumentException("任意因子不可小于0");
            }
            if (hScale == 0)
            {
                hScale = 1;
            }
            if (vScale == 0)
            {
                vScale = 1;
            } 
            this.Width = (int)(hScale / _horizontalScale * this.Width);
            this.Height = (int)(hScale / _vericalScale * this.Height);
            _horizontalScale = hScale;
            _vericalScale = vScale;
        }

        public void Zoom()
        {
            Zoom(_horizontalScale, _vericalScale);
        }

        public virtual void BindValue(IDictionary<string, Object> values)
        {

        }
    }
}
