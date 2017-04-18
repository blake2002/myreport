using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using PMS.Libraries.ToolControls.Report.Element;
using System.Windows.Forms;
using PMS.Libraries.ToolControls.Report.Elements.Util;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;

namespace PMS.Libraries.ToolControls.Report
{
    [Serializable]
    public class PmsPanelWrapper : IElement, ICloneable, IVisibleExpression, IElementContainer,
        IRepeatable, IBindField, IPrintable, IResizable,IControlTranslator
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
        /// 格式化字符串
        /// </summary>
        public string StrFormate { get; set; }

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
        /// 真实文本
        /// </summary>
        public string RealText { get; set; }

        /// <summary>
        /// 表达式
        /// </summary>
        [NonSerialized]
        private string _expression = string.Empty;
        public string Expression
        {
            get
            {
                return _expression;
            }
            set
            {
                _expression = value;
            }
        }

        /// <summary>
        /// 文本对齐方式
        /// </summary>
        public ContentAlignment TextAlign { get; set; }

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

        /// <summary>
        /// 是否显示的表达式
        /// </summary>
        [NonSerialized]
        private string _visibleExpression = string.Empty;
        public string VisibleExpression
        {
            get
            {
                return _visibleExpression;
            }
            set
            {
                _visibleExpression = value;
            }
        }

        private List<IElement> _elements = new List<IElement>();
        /// <summary>
        /// 子元素集合
        /// </summary>
        public IList<IElement> Elements 
        {
            get
            {
                return _elements;
            }
        }

        /// <summary>
        /// 原始坐标,报表绘图逻辑
        /// </summary>
        public Point OrginalLocation { get; set; }

        /// <summary>
        /// 总高度,报表绘图逻辑
        /// </summary>
        public int TotalHeight { get; set; }

        /// <summary>
        /// 原始高度,报表绘图逻辑
        /// </summary>
        public int OrignalHeight { get; set; }

        /// <summary>
        /// 是否绘制文本
        /// </summary>
        public bool IsRedrawText { get; set; }

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
        /// 报表查看或者打印时是否透明
        /// </summary>
        public bool Transparent { get; set; }

        /// <summary>
        /// 刷新
        /// </summary>
        public void Invalidate()
        { 
            
        }

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            PmsPanelWrapper panel = new PmsPanelWrapper();
            panel.BackColor = this.BackColor;
            panel.BorderName = this.BorderName;
            if (null != ExternDatas && ExternDatas.Count > 0)
            {
                if (null == panel.ExternDatas)
                {
                    panel.ExternDatas = new List<ExternData>();
                }
                foreach (ExternData ed in this.ExternDatas)
                {
                    panel.ExternDatas.Add(ed);
                }
            }
            if (null != this.Border)
            {
                panel.Border = this.Border.Clone() as ElementBorder;
                panel.Border.OwnerElement = panel;
            }
            panel.Transparent = this.Transparent;
            panel.ForeColor = this.ForeColor;
            panel.HasBottomBorder = this.HasBottomBorder;
            panel.HasLeftBorder = this.HasLeftBorder;
            panel.HasRightBorder = this.HasRightBorder;
            panel.HasTopBorder = this.HasTopBorder;
            panel.Height = this.Height;
            panel.Width = this.Width;
            panel.VerticalScale = this.VerticalScale;
            panel.HorizontalScale = this.HorizontalScale;
            panel.Location = this.Location;
            panel.Text = this.Text;
            panel.HasBorder = this.HasBorder;
            panel.AutoSize = AutoSize;
            panel.Visible = Visible;
            panel.MESType = MESType;
            try
            {
                if (null != Font)
                {
                    panel.Font = new Font(this.Font.FontFamily, this.Font.Size);
                }
            }
            catch (Exception)
            {

            }
            if (null != this.SourceField)
            {
                panel.SourceField = this.SourceField.Clone() as SourceField;
            }
            panel.MoveX = this.MoveX;
            panel.MoveY = this.MoveY;
            panel.Name = this.Name;
            panel.TotalHeight = this.TotalHeight;
            panel.OrignalHeight = this.OrignalHeight;
            panel.VisibleExpression = VisibleExpression;
            if (null != ((IElement)this).ExtendObject)
            {
                ((IElement)panel).ExtendObject = ((IElement)this).ExtendObject.Clone() as ExtendObject;
            }
            return panel;
        }

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
            if (!Transparent)
                return;
            MoveX = x;
            MoveY = y;
            Graphics g = ca.Graphics;
            SizeF textSize = g.MeasureString(Text, this.Font);
            //Size textSize = TextRenderer.MeasureText(Text, this.Font);
            //textSize.Width = textSize.Width;
            //textSize.Height = textSize.Height;
            float left = x;
            float top = y;

            // 上面的计算可能会改变控件的大小，所以将
            // 边框的绘制放在放在这里
            Border.FillAreaBackground(g, x, y);
            Border.Print(ca, x, y);
            if (IsRedrawText)
            {
                //非自动大小的情况下的文本位置
                #region   计算文本的 XY坐标
                switch (TextAlign)
                {
                    case ContentAlignment.BottomCenter:
                        left += (this.Width - 2 * Border.BorderWidth - textSize.Width) / 2 < 0 ? Border.BorderWidth : (this.Width - 2 * Border.BorderWidth - textSize.Width) / 2 + Border.BorderWidth;
                        top += this.Height - textSize.Height - Border.BorderWidth < 0 ? Border.BorderWidth : this.Height - textSize.Height - Border.BorderWidth;
                        break;
                    case ContentAlignment.BottomLeft:
                        top += this.Height - textSize.Height - 2 * Border.BorderWidth < 0 ? Border.BorderWidth : this.Height - textSize.Height - Border.BorderWidth;
                        left += Border.BorderWidth;
                        break;
                    case ContentAlignment.BottomRight:
                        left += this.Width - textSize.Width - 2 * Border.BorderWidth < Border.BorderWidth ? Border.BorderWidth : this.Width - textSize.Width - Border.BorderWidth;
                        top += this.Height - textSize.Height - Border.BorderWidth < 0 ? Border.BorderWidth : this.Height - textSize.Height - Border.BorderWidth; ;
                        break;
                    case ContentAlignment.MiddleCenter:
                        left += (this.Width - 2 * Border.BorderWidth - textSize.Width) / 2 < 0 ? Border.BorderWidth : (this.Width - 2 * Border.BorderWidth - textSize.Width) / 2 + Border.BorderWidth;
                        top += (this.Height - 2 * Border.BorderWidth - textSize.Height) / 2 < 0 ? Border.BorderWidth : (this.Height - 2 * Border.BorderWidth - textSize.Height) / 2 + Border.BorderWidth;
                        break;
                    case ContentAlignment.MiddleLeft:
                        top += (this.Height - 2 * Border.BorderWidth - textSize.Height) / 2 < 0 ? Border.BorderWidth : (this.Height - 2 * Border.BorderWidth - textSize.Height) / 2 + Border.BorderWidth;
                        left += Border.BorderWidth; ;
                        break;
                    case ContentAlignment.MiddleRight:
                        left += this.Width - textSize.Width - Border.BorderWidth < Border.BorderWidth ? Border.BorderWidth : this.Width - textSize.Width - Border.BorderWidth;
                        top += (this.Height - 2 * Border.BorderWidth - textSize.Height) / 2 < 0 ? Border.BorderWidth : (this.Height - 2 * Border.BorderWidth - textSize.Height) / 2 + Border.BorderWidth;
                        break;
                    case ContentAlignment.TopCenter:
                        left += (this.Width - textSize.Width) / 2 < 0 ? Border.BorderWidth : (this.Width - textSize.Width) / 2 + Border.BorderWidth;
                        top += Border.BorderWidth;
                        break;
                    case ContentAlignment.TopLeft:
                        top += Border.BorderWidth;
                        left += Border.BorderWidth;
                        break;
                    case ContentAlignment.TopRight:
                        left += this.Width - textSize.Width - Border.BorderWidth < Border.BorderWidth ? Border.BorderWidth : this.Width - textSize.Width - Border.BorderWidth;
                        top += Border.BorderWidth;
                        break;

                }
                #endregion
                Brush foreBrush = new SolidBrush(this.ForeColor);
                try
                {
                    float tempWidth = this.Width - left + MoveX - Border.BorderWidth - textSize.Width <= 0 ? this.Width - left + MoveX - Border.BorderWidth : textSize.Width;
                    float tempHeight = this.Height - top + MoveY - Border.BorderWidth - textSize.Height <= 0 ? this.Height - top + MoveY - Border.BorderWidth : textSize.Height;
                    if (tempWidth != 0 && tempHeight != 0)
                    {
                        g.DrawString(Text, this.Font, foreBrush, new RectangleF(left, top, tempWidth, tempHeight));
                    }
                }
                finally
                {
                    foreBrush.Dispose();
                }
            }
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
            if (string.IsNullOrEmpty(_orginalFontName))
            {
                _orginalFontName = this.Font.FontFamily.Name;
            }
            this.Width = (int)(hScale * this._orginalWidth);
            this.Height = (int)(hScale * this._orginalHeight);
            int x = (int)(this._orginalLocation.X * hScale);
            int y = (int)(this._orginalLocation.Y * vScale);
            this.Location = new Point(x, y);
            float fontSize = hScale * _orginalFontSize;
            this.Font = new Font(_orginalFontName, fontSize);
            HorizontalScale = hScale;
            VerticalScale = vScale;
            if (null != Border)
            {
                Border.Zoom(hScale, vScale);
            }
        }

        /// <summary>
        /// 转换成控件
        /// </summary>
        /// <returns></returns>
        public Control ToControl(bool childTranslate= false)
        {
            PmsPanel panel = new PmsPanel(this);
            if (null != Elements)
            {
                foreach (IElement element in Elements)
                { 
                    IControlTranslator ct = element as IControlTranslator;
                    if(null != ct)
                    {
                        Control ctrl = ct.ToControl(childTranslate);
                        if(null != ctrl)
                        {
                            panel.Controls.Add(ctrl);
                        }
                    }
                }
            }
            return panel;
        }
    }
}
