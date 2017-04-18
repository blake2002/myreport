using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMS.Libraries.ToolControls.Report.Element;
using System.Drawing;
using PMS.Libraries.ToolControls.Report.Elements.Util;
using System.Windows.Forms;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using System.Reflection;
using System.IO;

namespace PMS.Libraries.ToolControls.Report
{
    [Serializable]
    public class PmsSplitterWrapper : IElement, IBindField, IPmsReportDataBind,
        IPrintable, ICloneable, IResizable, IControlTranslator, IPageSplitter,IVisibleExpression
    {
        protected int _orginalWidth = -1;
        protected int _orginalHeight = -1;
        protected Point _orginalLocation = Point.Empty;
        protected float _orginalFontSize = -1f;
        protected string _orginalFontName = string.Empty;

        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 元素宽
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 元素高
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 报表元素的父元素
        /// <remarks>在报表模块都是<see cref="IElement"/>的派生类</remarks>
        /// </summary>
        public IElement Parent { get; set; }
        /// <summary>
        /// 元素的背景颜色
        /// </summary>
        public Color BackColor { get; set; }
        /// <summary>
        /// 元素的背景颜色
        /// </summary>
        public Color ForeColor { get; set; }
        /// <summary>
        /// 背景图片
        /// </summary>
        public Image BackgroundImage { get; set; }
        /// <summary>
        /// 背景图片的布局
        /// </summary>
        public ImageLayout BackgroundImageLayout { get; set; }
        /// <summary>
        /// 是否拥有边框
        /// </summary>
        public bool HasBorder { get; set; }
        /// <summary>
        /// 元素边框
        /// </summary>
        public ElementBorder Border { get; set; }
        /// <summary>
        /// 是否自动伸展大小
        /// </summary>
        public bool AutoSize { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public Point Location { get; set; }
        /// <summary>
        /// X偏移量
        /// </summary>
        public float MoveX { get; set; }
        /// <summary>
        /// Y偏移量
        /// </summary>
        public float MoveY { get; set; }

        /// <summary>
        /// 额外的数据
        /// </summary>
        public List<ExternData> ExternDatas { get; set; }
        /// <summary>
        /// 是否能刷新
        /// </summary>
        public bool CanInvalidate
        {
            get
            {
                return true;
            }
        }
        /// <summary>
        /// 文本信息
        /// </summary>
        public string Text { get; set; }

        private Font _font = new Font("宋体", 9);
        /// <summary>
        /// 文本字体
        /// </summary>
        public Font Font
        {
            get
            {
                return _font;
            }
            set
            {
                _font = value;
            }
        }
        /// <summary>
        /// 边框名
        /// </summary>
        public string BorderName { get; set; }

        ///// <summary>
        ///// 将绑定的字段根据传入的参数替换
        ///// </summary>
        ///// <param name="values">参数，值对</param>
        //void BindValue(IDictionary<string, Object> values);
        /// <summary>
        /// 是否拥有左边框
        /// </summary>
        public bool HasLeftBorder { get; set; }
        /// <summary>
        /// 是否拥有上边框
        /// </summary>
        public bool HasTopBorder { get; set; }

        /// <summary>
        /// 是否拥有下边框
        /// </summary>
        public bool HasBottomBorder { get; set; }
        /// <summary>
        /// 是否拥有右边框
        /// </summary>
        public bool HasRightBorder { get; set; }

        /// 横向比例因子
        /// </summary>
        public float HorizontalScale { get; set; }

        /// <summary>
        /// 纵向比例因子
        /// </summary>
        public float VerticalScale { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public MESVarType MESType { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        [NonSerialized]
        private SourceField _sourceFiled = null;
        public SourceField SourceField
        {
            get
            {
                return _sourceFiled;
            }
            set
            {
                _sourceFiled = value;
            }
        }

        private bool _enableSplitter = true;
        public bool EnableSplitter
        {
            get
            {
                return _enableSplitter;
            }
            set
            {
                _enableSplitter = value;
            }
        }
     

        /// <summary>
        /// 额外的存储结构
        /// </summary>
        public ExtendObject ExtendObject { get; set; }

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

        /// <summary>
        /// 表达式
        /// </summary>
        public string VisibleExpression
        {
            get;
            set;
        }

      

        /// <summary>
        /// 绑定值
        /// </summary>
        /// <param name="values"></param>
        public void BindValue(IDictionary<string, Object> values)
        { 
            
        }

        /// <summary>
        /// 打印或者显示在指定画布,该方法的调用用在显示报表
        /// <remarks>使用x,y偏移量是为了绘图转换坐标系方便</remarks>
        /// </summary>
        /// <param name="g">绘图图面,拥有该参数是为了可以灵活的转移图形的输出画面</param>
        /// <param name="x">横坐标位置的偏移值</param>
        /// <param name="y">纵坐标位置的偏移值</param>
        public void Print(Canvas ca, float x, float y)
        {
            Graphics g = ca.Graphics;
            if (null != g)
            {
                if (null != Border)
                {
                    Border.Print(ca, x, y);
                }
                Assembly assm = Assembly.GetExecutingAssembly();
                Stream stream = assm.GetManifestResourceStream("PMS.Libraries.ToolControls.Report.Resources.PageSplit.ico");
                Image img = Image.FromStream(stream, true);
                g.DrawImage(img, 0, 0);
                stream.Dispose();
                g.DrawString(this.Name, this.Font, Brushes.Black, x + img.Width, 0);
                img.Dispose();
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public void Invalidate()
        {

        }

        public object Clone()
        {
            PmsPageSplitter layout = new PmsPageSplitter();
            layout.Name = Name;
            layout.AutoSize = AutoSize;
            layout.BackColor = BackColor;
            if (null != BackgroundImage)
            {
                layout.BackgroundImage = BackgroundImage.Clone() as Image;
            }
            layout.BackgroundImageLayout = BackgroundImageLayout;
            if (null != Border)
            {
                layout.Border = Border.Clone() as ElementBorder;
                layout.Border.OwnerElement = layout;
            }
            layout.BorderName = BorderName;
            if (null != ExternDatas)
            {
                layout.ExternDatas = new List<ExternData>();
                foreach (ExternData ed in ExternDatas)
                {
                    object value = ed.Value;
                    if (null != value && value is ICloneable)
                    {
                        value = ((ICloneable)value).Clone();
                    }
                    layout.ExternDatas.Add(new ExternData(ed.Key, value));
                }
            }
            if (null != Font)
            {
                layout.Font = Font.Clone() as Font;
            }
            layout.HasBorder = HasBorder;
            layout.HasBottomBorder = HasBottomBorder;
            layout.HasLeftBorder = HasLeftBorder;
            layout.HasRightBorder = HasRightBorder;
            layout.HasTopBorder = HasTopBorder;
            layout.Height = Height;
            layout.HorizontalScale = HorizontalScale;
            layout.Location = Location;
            layout.MESType = MESType;
            layout.MoveX = MoveX;
            layout.MoveY = MoveY;
            layout.Text = Text;
            layout.VerticalScale = VerticalScale;
            layout.Width = Width;
            layout.Visible = Visible;
            layout.MESType = MESType;
            layout.EnableSplitter = EnableSplitter;
            layout.VisibleExpression = VisibleExpression;
            if (null != ((IElement)this).ExtendObject)
            {
                ((IElement)layout).ExtendObject = ((IElement)this).ExtendObject.Clone() as ExtendObject;
            }
            return layout;
        }

        /// <summary>
        /// 转换成控件
        /// </summary>
        /// <returns></returns>
        public Control ToControl(bool childTranslate = false)
        {
            return new PmsPageSplitter(this);
        }

        /// <summary>
        /// 缩放
        /// </summary>
        public void Zoom()
        {
            Zoom(HorizontalScale, VerticalScale);
        }

        /// <summary>
        /// 按照指定的因子比例缩放
        /// </summary>
        /// <param name="hScale">横向比例</param>
        /// <param name="vScale">纵向比例</param>
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
            this.Width = (int)(hScale * this._orginalWidth);
            this.Height = (int)(hScale * this._orginalHeight);
            this.Location = new Point((int)(this._orginalLocation.X * hScale), (int)(this._orginalLocation.Y * vScale));
            float fontSize = hScale * _orginalFontSize;
            this.Font = new Font(this.Font.FontFamily, fontSize, this.Font.Style);
            HorizontalScale = hScale;
            VerticalScale = vScale;
            if (null != Border)
            {
                Border.Zoom(hScale, vScale);
            }
        }

    }
}
