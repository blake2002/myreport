using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using PMS.Libraries.ToolControls.PMSPublicInfo;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using PMS.Libraries.ToolControls.Report.Controls.Editor;
using PMS.Libraries.ToolControls.Report.Controls.TypeConvert;
using PMS.Libraries.ToolControls.Report.Elements.Util;

namespace PMS.Libraries.ToolControls.Report.Element
{
    /// <summary>
    /// 报表控件元素基类
    /// </summary>
    public abstract class ElementBase : Control, IElement, IBindField, IPmsReportDataBind,
        IPrintable, ILightClone, IDeepDefinitionClone, ICloneable, IResizable, IElementContent, IBindDataTableManager, IDirectDrawable, IElementExtended
        , IVisibleExpression
        , System.ComponentModel.ICustomTypeDescriptor
    {
        protected int _orginalWidth = -1;
        protected int _orginalHeight = -1;
        protected Point _orginalLocation = Point.Empty;
        protected float _orginalFontSize = -1f;
        protected string _orginalFontName = string.Empty;

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
        [Category("通用")]
        [DesignOnly(true)]
        public bool HasTopBorder
        {
            get
            {
                return _hasTopBorder;
            }
            set
            {
                _hasTopBorder = value;
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
        [Category("通用")]
        [DesignOnly(true)]
        public bool HasLeftBorder
        {
            get
            {
                return _hasLeftBorder;
            }
            set
            {
                _hasLeftBorder = value;
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
        [Category("通用")]
        [DesignOnly(true)]
        public bool HasBottomBorder
        {
            get
            {
                return _hasBottomBorder;
            }
            set
            {
                _hasBottomBorder = value;
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
        [Category("通用")]
        [DesignOnly(true)]
        public bool HasRightBorder
        {
            get
            {
                return _hasRightBorder;
            }
            set
            {
                _hasRightBorder = value;

            }
        }


        /// <summary>
        /// 数据源
        /// </summary>
        [Editor(typeof(SourceEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        [Category("通用")]
        [TypeConverter(typeof(SourceConverter))]
        public virtual SourceField SourceField
        {
            get;
            set;
        }


        /// <summary>
        /// 用来在第一次paint的时候重新检测边框，解决设计时候确定边框的问题
        /// </summary>
        [NonSerialized]
        private bool _isInit = true;

        protected float _horizontalScale = 1;
        /// <summary>
        /// 横向比例因子
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(true)]
        [Browsable(false)]
        [Description("是否拥有右边框")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Localizable(true)]
        [Category("通用")]
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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(true)]
        [Browsable(false)]
        [Description("是否拥有右边框")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Localizable(true)]
        [Category("通用")]
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
        /// 属性赋值刷新控制
        /// </summary>
        protected bool _canInvalidate = true;
        [Browsable(false)]
        public bool CanInvalidate
        {
            get
            {
                return _canInvalidate;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(false)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(true)]
        [Category("通用")]
        public override bool AutoSize
        {
            get
            {
                return base.AutoSize;
            }
            set
            {
                base.AutoSize = value;
                if (!value)
                {
                    this.Width = this.Width + 100;
                    this.Height = this.Height + 100;
                }
                if (PMSPublicInfo.CurrentPrjInfo.CurrentRunMode == PMSPublicInfo.RunMode.Develope)
                {
                    Invalidate();
                }
            }
        }


        /// <summary>
        /// 初始为null,是为了可以让具体的元素
        /// 内部可以更方便的指定具体的border类型
        /// 如方型的Border或者椭圆等等
        /// </summary>
        protected ElementBorder _border = null;
        [Category("通用")]
        [Editor(typeof(BorderEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Browsable(true)]
        [TypeConverter(typeof(BorderConverter))]
        public virtual ElementBorder Border
        {
            get
            {
                if (null == _border)
                {
                    _border = new RectangleBorder(this);
                }
                return _border;
            }
            set
            {
                _border = value;
                //if (null != value)
                //{
                //    this.BorderName = value.Name;
                //}
                Invalidate();
            }
        }

        private string _borderName = string.Empty;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        [Localizable(true)]
        public virtual string BorderName
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


        /// <summary>
        /// X偏移量
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(true)]
        [Browsable(false)]
        [Description("是否拥有右边框")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Localizable(true)]
        [Category("通用")]
        public float MoveX
        {
            get;
            set;
        }

        /// <summary>
        /// Y偏移量
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(true)]
        [Browsable(false)]
        [Description("是否拥有右边框")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Localizable(true)]
        [Category("通用")]
        public float MoveY
        {
            get;
            set;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(true)]
        [Editor(typeof(PMS.Libraries.ToolControls.Report.
            Controls.Editor.ExpressionEditor), typeof(UITypeEditor))]
        [Category("通用")]
        [Description("显示表达式")]
        string IVisibleExpression.VisibleExpression
        {
            get;
            set;
        }

        /// <summary>
        /// 打印或者显示在指定画布<see cref="IPrintable"/>
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// <param name="x">X横坐标偏移位置</param>
        /// <param name="y">Y坐标偏移位置</param>
        public abstract void Print(Canvas ca, float x, float y);

        /// <summary>
        /// 根据给定的画布和开始坐标绘制报表控件,不对控件值再做处理，直接绘制
        /// </summary>
        /// <param name="ca">画布</param>
        /// <param name="x">横坐标位置的偏移值</param>
        /// <param name="y">纵坐标位置的偏移值</param>
        /// <param name="dpiZoom">设计时dpix与最终绘图所在介质dpix的比例</param>
        public virtual void DirectDraw(Canvas ca, float x, float y, float dpiZoom)
        {

        }

        /// <summary>
        /// 是否拥有边框
        /// </summary>
        public abstract bool HasBorder { get; set; }


        [Editor(typeof(TextEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        [Category("通用")]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                RealText = value;
                if (null != Site)
                {
                    Invalidate();
                }
            }
        }

        protected List<ExternData> _externDatas = new List<ExternData>();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Browsable(false)]
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

        protected string _realText = null;

        [Browsable(false)]
        public string RealText
        {
            get
            {
                if (string.IsNullOrEmpty(_realText))
                {
                    return Text;
                }
                return _realText;
            }
            set
            {
                _realText = value;
            }
        }

        protected Color _backColor = Color.Transparent;
        /// <summary>
        /// 背景色
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(true)]
        [Browsable(true)]
        [Description("背景颜色")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(true)]
        [Category("通用")]
        public override Color BackColor
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

        private MESVarType _mestType = MESVarType.MESString;
        /// <summary>
        /// 数据类型
        /// </summary>
        [Category("通用")]
        public virtual MESVarType MESType
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
                return Parent as IElement;
            }
            set
            {
                Parent = value as Control;
            }
        }

        [Browsable(false)]
        ExtendObject IElement.ExtendObject
        {
            get;
            set;
        }


        public ElementBase()
            : base()
        {
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //if (null == this.Border)
            //{
            //    this.Border = BorderFactory.Instacne.CreateBorder(this.BorderName, this);
            //}
        }

        public ElementBase(string text)
            : base(text)
        {
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            if (null == this.Border)
            {

                this.Border = BorderFactory.Instacne.CreateBorder(this.BorderName, this);
            }
        }

        public ElementBase(Control parent, string text)
            : base(parent, text)
        {
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            if (null == this.Border)
            {

                this.Border = BorderFactory.Instacne.CreateBorder(this.BorderName, this);
            }
        }

        public ElementBase(string text, int left, int top, int width, int height)
            : base(text, left, top, width, height)
        {
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            if (null == this.Border)
            {
                this.Border = BorderFactory.Instacne.CreateBorder(this.BorderName, this);
            }
        }

        public ElementBase(Control parent, string text, int left, int top, int width, int height)
            : base(parent, text, left, top, width, height)
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            if (null == this.Border)
            {
                this.Border = BorderFactory.Instacne.CreateBorder(this.BorderName, this);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //设计器是先New了Border在之后向 ExterDatas中添加数据
            //所以BorderFactory中的CreateBorder中对Border的属性赋值不会被执行
            //故将他放在重绘中
            //使用_canInvalidate防止以下代买赋值过程有反复重绘制
            Print(new Canvas(e.Graphics), 0, 0);
            base.OnPaint(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_externDatas != null)
                {
                    _externDatas.Clear();
                }
            }
            base.Dispose(disposing);
        }
        //public virtual void Print(Canvas ca, float x, float y)
        //{
        //    SetBorder();
        //    OnPrint(ca, 0, 0);
        //}


        protected void SetBorder()
        {
            if (_isInit)
            {
                if (null != Border && Border.Name != this.BorderName)
                {

                    ElementBorder eb = Border;
                    this.Border = BorderFactory.Instacne.CreateBorder(this.BorderName, this);
                    this.Border.OwnerElement = this;
                    this.Border.BorderWidth = eb.BorderWidth;
                    this.Border.BorderColor = eb.BorderColor;
                    this.Border.DashStyle = eb.DashStyle;
                }
                if (null != ExternDatas && ExternDatas.Count > 0 && null != Border)
                {
                    _canInvalidate = false;
                    for (int i = 0; i < ExternDatas.Count; i++)
                    {
                        ExternData data = ExternDatas[i];
                        if (null != data)
                        {
                            if (!string.IsNullOrEmpty(data.Key))
                            {
                                PropertyInfo pi = Border.GetType().GetProperty(data.Key);
                                if (null != pi)
                                {
                                    pi.SetValue(Border, data.Value, null);
                                }
                            }
                        }

                    }
                    _canInvalidate = true;
                }

                _isInit = false;
            }
        }

        /// <summary>
        /// 缩放
        /// </summary>
        /// <param name="hScale">横向比例因子</param>
        /// <param name="vScale">纵向比例因子</param>
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
            if (_orginalHeight == -1)
            {
                this._orginalHeight = this.Height;
            }
            if (_orginalWidth == -1)
            {
                _orginalWidth = this.Width;
            }
            if (_orginalLocation == Point.Empty)
            {
                _orginalLocation = this.Location;
            }
            if (_orginalFontSize == -1)
            {
                _orginalFontSize = this.Font.Size;
            }
            //if (string.IsNullOrEmpty(_orginalFontName))
            //{
            //    _orginalFontName = this.Font.FontFamily.Name;
            //}
            this.Width = (int)(hScale * this._orginalWidth);
            this.Height = (int)(hScale * this._orginalHeight);
            this.Location = new Point((int)(this._orginalLocation.X * hScale), (int)(this._orginalLocation.Y * vScale));
            float fontSize = hScale * _orginalFontSize;
            this.Font = new Font(this.Font.FontFamily, fontSize, this.Font.Style);
            _horizontalScale = hScale;
            _vericalScale = vScale;
            if (null != Border)
            {
                Border.Zoom(hScale, vScale);
            }
        }

        /// <summary>
        /// 缩放
        /// </summary>
        public void Zoom()
        {
            Zoom(_horizontalScale, _vericalScale);
        }

        /// <summary>
        /// 将变量绑定值 
        /// </summary>
        /// <param name="values">将变量绑定值</param>
        public virtual void BindValue(IDictionary<string, Object> values)
        {

        }

        public virtual int BindDataTableManager(IDataTableManager dtm, string bindPath)
        {
            return 0;
        }


        object ILightClone.Clone()
        {
            PropertyInfo[] pis = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);//| BindingFlags.Static | BindingFlags.NonPublic 
            PmsLabel pl = new PmsLabel();
            if (null != pis && pis.Length > 0)
            {
                foreach (PropertyInfo pi in pis)
                {
                    try
                    {
                        pi.SetValue(pl, pi.GetValue(this, null), null);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            return pl;
        }

        [Obsolete("尚未实现")]
        object IDeepDefinitionClone.Clone()
        {
            //该方法有问题
            return DeepClone.Clone(this);
        }

        [Category("通用")]
        [Browsable(false)]
        public virtual bool EnableMapping
        {
            get;
            set;
        }

        [Category("通用")]
        [Editor(typeof(DataMappingEditor), typeof(UITypeEditor))]
        [Browsable(false)]
        public virtual string MappingTable
        {
            get;
            set;
        }

        #region IElementExtended
        /// <summary>
        /// 扩展元素宽
        /// </summary>
        [Browsable(false)]
        int IElementExtended.Width { get; set; }

        /// <summary>
        /// 扩展元素高
        /// </summary>
        [Browsable(false)]
        int IElementExtended.Height { get; set; }

        /// <summary>
        /// 扩展元素坐标
        /// </summary>
        [Browsable(false)]
        Point IElementExtended.Location { get; set; }

        #endregion

        public virtual object Clone()
        {
            return null;
        }

        public string GetMapValue(string key)
        {
            if (string.IsNullOrEmpty(MappingTable) || string.IsNullOrEmpty(key))
            {
                return null;
            }

            return PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetReplaceValue(MappingTable, key);
        }

        #region   ICustomTypeDescriptor   显式接口定义
        AttributeCollection ICustomTypeDescriptor.GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        string ICustomTypeDescriptor.GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        string ICustomTypeDescriptor.GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        TypeConverter ICustomTypeDescriptor.GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            if (null != this.Site)
                return this.FilterProperties(TypeDescriptor.GetProperties(this.GetType()));
            else
                return TypeDescriptor.GetProperties(this.GetType());
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
        {
            if (null != this.Site)
            {
                return this.FilterProperties(TypeDescriptor.GetProperties(this.GetType(), attributes));
            }
            else
                return TypeDescriptor.GetProperties(this.GetType(), attributes);
        }

        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        #region 属性过滤

        private PropertyDescriptorCollection FilterProperties(PropertyDescriptorCollection properties)
        {
            if (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.ReadingXmlInDesignerTime == true)
                return TypeDescriptor.GetProperties(this.GetType());

            PropertyDescriptorCollection tmpPDC = properties;
            System.Reflection.PropertyInfo[] pis = this.GetType().GetProperties();
            ArrayList props = new ArrayList();
            foreach (PropertyDescriptor pdes in tmpPDC)
            {
                System.Reflection.PropertyInfo pi = pis.First(o => o.Name == pdes.Name);
                if (pdes.Name == "Width" || pdes.Name == "Height" || pdes.Name == "ExternDatas" || pi.DeclaringType == this.GetType())
                    props.Add(new GlobalizedPropertyDescriptor(pdes));
            }

            GlobalizedPropertyDescriptor[] propArray =
                (GlobalizedPropertyDescriptor[])props.ToArray(typeof(GlobalizedPropertyDescriptor));
            tmpPDC = new PropertyDescriptorCollection(propArray);
            return tmpPDC;

        }

        #endregion

        #endregion

    }
}
