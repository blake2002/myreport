using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using PMS.Libraries.Report.DataBinding;
using PMS.Libraries.ToolControls.PMSPublicInfo;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using PMS.Libraries.ToolControls.Report.Controls.Editor;
using PMS.Libraries.ToolControls.Report.Controls.TypeConvert;
using PMS.Libraries.ToolControls.Report.Elements.Util;

namespace PMS.Libraries.ToolControls.Report.Element
{
    /// <summary>
    /// 容器元素
    /// </summary>
    public abstract class ElementPanel : ScrollableControl, IPanelElement, IBindField, IPmsReportDataBind,
        IBindingSource, IPrintable, IRepeatable, ILightClone, IDeepDefinitionClone, ICloneable, IResizable, IElementContainer, IElementExtended, IDirectDrawable
        , IBindDataTableManager
        , System.ComponentModel.ICustomTypeDescriptor
    {
        protected int _orginalWidth = -1;
        protected int _orginalHeight = -1;
        protected Point _orginalLocation = Point.Empty;
        protected float _orginalFontSize = -1f;
        protected string _orginalFontName = string.Empty;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(true)]
        [Browsable(false)]
        [Description("原始坐标")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Localizable(true)]
        [Category("MES控件属性")]
        public Point OrginalLocation
        {
            get;
            set;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(true)]
        [Browsable(false)]
        [Description("已经绘制总高度")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Localizable(true)]
        [Category("MES控件属性")]
        public int TotalHeight
        {
            get;
            set;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(true)]
        [Browsable(false)]
        [Description("原始高度")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Localizable(true)]
        [Category("MES控件属性")]
        public int OrignalHeight
        {
            get;
            set;
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
        [Category("MES控件属性")]
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
        [Category("MES控件属性")]
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
        [Category("MES控件属性")]
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
        [Category("MES控件属性")]
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
        [Category("MES控件属性")]
        [TypeConverter(typeof(SourceConverter))]
        public virtual SourceField SourceField
        {
            get;
            set;
        }

        /// <summary>
        /// 用来在第一次paint的时候重新检测边框，解决设计时候确定边框的问题
        /// </summary>
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
        [Category("MES控件属性")]
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
        [Category("MES控件属性")]
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
        [Browsable(false)]
        public bool CanInvalidate
        {
            get
            {
                return _canInvalidate;
            }
        }


        //protected ElementCollection<IElement> _elements = new ElementCollection<IElement>();
        ///// <summary>
        ///// 内部报表元素
        ///// </summary>
        //[Browsable(false)]
        //[DesignOnly(true)]
        //public ElementCollection<IElement> Elements
        //{
        //    get
        //    {
        //        return _elements;
        //    }
        //}

        protected PmsReportLayout _elemntsLayout = PmsReportLayout.UserDefine;
        /// <summary>
        /// 内部元素布局
        /// </summary>
        [Browsable(false)]
        public PmsReportLayout ElementsLayout
        {
            get
            {
                return _elemntsLayout;
            }
            set
            {
                _elemntsLayout = value;
            }
        }

        /// <summary>
        /// 是否拥有边框
        /// </summary>
        public abstract bool HasBorder { get; set; }

        ///// <summary>
        ///// 边框颜色
        ///// </summary>
        //public abstract Color BorderColor { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(false)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(true)]
        [Category("MES控件属性")]
        public override bool AutoSize
        {
            get
            {
                return base.AutoSize;
            }
            set
            {
                base.AutoSize = value;
                Invalidate();
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
        [Category("MES控件属性")]
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
        [Category("MES控件属性")]
        public float MoveY
        {
            get;
            set;
        }

        /// <summary>
        /// 初始为null,是为了可以让具体的元素
        /// 内部可以更方便的指定具体的border类型
        /// 如方型的Border或者椭圆等等
        /// </summary>
        protected ElementBorder _border = null;
        [Editor(typeof(BorderEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Browsable(true)]
        [Category("MES控件属性")]
        [TypeConverter(typeof(BorderConverter))]
        public virtual ElementBorder Border
        {
            get
            {
                return _border;
            }
            set
            {
                _border = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 数据源
        /// </summary>
        protected IDataSource _dataSource = null;
        [Browsable(false)]
        public IDataSource DataSource
        {
            get
            {
                return _dataSource;
            }
            set
            {
                _dataSource = value;
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
                //if (null != ExternDatas && ExternDatas.Count > 0 && null != Border)
                //{
                //    _canInvalidate = false;
                //    for (int i = 0; i < ExternDatas.Count; i++)
                //    {
                //        ExternData data = ExternDatas[i];
                //        if (null != data)
                //        {
                //            if (!string.IsNullOrEmpty(data.Key))
                //            {
                //                PropertyInfo pi = Border.GetType().GetProperty(data.Key);
                //                if (null != pi)
                //                {
                //                    pi.SetValue(Border, data.Value, null);
                //                }
                //            }
                //        }

                //    }

                //    if (null == Border || this.BorderName != this.Border.Name)
                //    {
                //        Color borderColor = this.Border.BorderColor;
                //        float borderWidth = this.Border.BorderWidth;
                //        DashStyle ds = this.Border.DashStyle;
                //        this.Border = BorderFactory.Instacne.CreateBorder(this.BorderName, this);
                //        this.Border.BorderColor = borderColor;
                //        this.Border.BorderWidth = borderWidth;
                //        this.Border.DashStyle = ds;
                //    }
                //    _canInvalidate = true;
                //}
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

        private MESVarType _mestType = MESVarType.MESString;
        /// <summary>
        /// 数据类型
        /// </summary>
        [Category("MES控件属性")]
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
        ExtendObject IElement.ExtendObject
        {
            get;
            set;
        }


        /// <summary>
        /// 子元素集合
        /// </summary>
        public IList<IElement> Elements
        {
            get
            {
                List<IElement> list = new List<IElement>();
                foreach (Control ctrl in Controls)
                {
                    //if (ctrl is IElementContainer)
                    //{
                    //    IList<IElement> tmpList = ((IElementContainer)ctrl).Elements;
                    //    if (null != tmpList && tmpList.Count > 0)
                    //    {
                    //        list.AddRange(tmpList);
                    //    }
                    //}
                    //else if (ctrl is IElement)
                    //{
                    //    list.Add(ctrl as IElement);
                    //}

                    if (ctrl is IElement)
                    {
                        list.Add(ctrl as IElement);
                    }
                }
                return list;
            }
        }

        /// <summary>
        /// 根据给定的画布和开始坐标绘制报表控件,不对控件值再做处理，直接绘制
        /// </summary>
        /// <param name="ca">画布</param>
        /// <param name="x">横坐标位置的偏移值</param>
        /// <param name="y">纵坐标位置的偏移值</param>
        public virtual void DirectDraw(Canvas ca, float x, float y, float dpiZoom = 1)
        {

        }

        public virtual int BindDataTableManager(IDataTableManager dtm, string bindPath)
        {
            return 0;
        }


        public ElementPanel()
            : base()
        {
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            if (null == this.Border)
            {
                this.Border = BorderFactory.Instacne.CreateBorder(this.BorderName, this);
            }
        }

        /// <summary>
        /// 绘制图面
        /// </summary>
        /// <param name="g">绘图图面类</param>
        /// <param name="x">x的偏移</param>
        /// <param name="y">y的偏移</param>
        public virtual void Print(Canvas ca, float x, float y)
        {

            this.MoveX = x;
            this.MoveY = y;
            if (null != Border)
            {
                Border.Print(ca, MoveX, MoveY);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            SetBorder();
            //这里主要用于设计时候的显示
            //对于报表的显示将在报表展示器中调用Print方法
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
        protected void SetBorder()
        {
            if (_isInit)
            {
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

                    if (null == Border || this.BorderName != this.Border.Name)
                    {
                        Color borderColor = this.Border.BorderColor;
                        float borderWidth = this.Border.BorderWidth;
                        DashStyle ds = this.Border.DashStyle;
                        this.Border = BorderFactory.Instacne.CreateBorder(this.BorderName, this);
                        this.Border.BorderColor = borderColor;
                        this.Border.BorderWidth = borderWidth;
                        this.Border.DashStyle = ds;
                    }
                    _canInvalidate = true;
                }
                _isInit = false;
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            IElement element = this.Controls[this.Controls.Count - 1] as IElement;
            if (null != element)
            {
                Elements.Add(element);
            }
            base.OnControlAdded(e);
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            //IElement element = e.Control as IElement;
            //if (null != element)
            //{
            //    this.Controls.
            //    Elements.Remove(element);
            //}
            base.OnControlRemoved(e);
        }

        /// <summary>
        /// 按比例缩放
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
            if (string.IsNullOrEmpty(_orginalFontName))
            {
                _orginalFontName = this.Font.FontFamily.Name;
            }
            this.Width = (int)(hScale * this._orginalWidth);
            this.Height = (int)(hScale * this._orginalHeight);
            //if (null != this.Border)
            //{
            //    this.Height += (int)this.Border.BorderWidth;
            //}
            int x = (int)(this._orginalLocation.X * hScale);
            int y = (int)(this._orginalLocation.Y * vScale);
            //if (null != this.Border)
            //{
            //    y += (int)this.Border.BorderWidth;
            //}
            this.Location = new Point(x, y);
            float fontSize = hScale * _orginalFontSize;
            this.Font = new Font(_orginalFontName, fontSize);
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

        public virtual void BindValue(IDictionary<string, Object> values)
        {

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

        object IDeepDefinitionClone.Clone()
        {
            return DeepClone.Clone(this);
        }

        public virtual object Clone()
        {
            return null;
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
                if (pdes.Name == "Width" || pdes.Name == "Height"
                    || pdes.Name == "HasChildren"
                    || pdes.Name == "Parent"
                    || pdes.Name == "Controls"
                    || pi.DeclaringType == this.GetType())
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

    [Flags]
    public enum PmsReportLayout
    {
        /// <summary>
        /// 横向
        /// </summary>
        Horizontal = 0,
        /// <summary>
        /// 纵向
        /// </summary>
        Vertical = 1,
        /// <summary>
        /// 横向流式
        /// </summary>
        HFlow = 2,
        /// <summary>
        /// 纵向流式
        /// </summary>
        VFlow = 3,
        /// <summary>
        ///  没有特殊布局，根据用户的设置布局
        /// </summary>
        UserDefine = 4
    }
}
