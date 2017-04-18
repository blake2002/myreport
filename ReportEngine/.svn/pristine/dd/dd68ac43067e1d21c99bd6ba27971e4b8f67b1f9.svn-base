using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMS.Libraries.ToolControls.Report.Element;
using System.Drawing;
using PMS.Libraries.ToolControls.Report.Elements.Util;
using System.Windows.Forms;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using System.Text.RegularExpressions;

namespace PMS.Libraries.ToolControls.Report
{
    [Serializable]
    public class PmsLabelWrapper : IElement, IBindField, IPmsReportDataBind,
        IPrintable, ICloneable, IResizable, IElementContent, 
        IExpression, IDataMapping, IPMSFormate,IControlTranslator
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
        /// 映射表
        /// </summary>
        public string MappingTable { get; set; }

        /// <summary>
        /// 是否启用映射
        /// </summary>
        public bool EnableMapping { get; set; }

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
        public string RealText { get;set; }

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

        public string FormateToString(object obj)
        {
            return FormateUtil.Formate(StrFormate, obj);
        }

        
        public void BindValue(IDictionary<string, Object> values)
        {
            if (null != values && values.Count > 0)
            {
                RealText = Text;
                string str = @"\[#?%?(p{0,}/?\w){1,}%?#?\]";
                Regex regex = new Regex(str);
                MatchCollection mc = regex.Matches(RealText);
                if (null != mc && mc.Count > 0)
                {
                    foreach (Match m in mc)
                    {
                        string key = m.Value.Remove(0, 1);
                        key = key.Remove(key.Length - 1, 1);
                        if (values.ContainsKey(key))
                        {
                            string newStr = string.Empty;
                            object o = values[key];
                            if (null != o)
                            {
                                if (string.IsNullOrEmpty(StrFormate))
                                {
                                    newStr = o.ToString();
                                }
                                else
                                {
                                    newStr = FormateToString(o);
                                }
                            }
                            if (newStr == string.Empty)
                            {
                                newStr = " ";
                            }
                            RealText = RealText.Replace(m.Value, newStr);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public void Invalidate()
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
            MoveX = x;
            MoveY = y;
            Graphics g = ca.Graphics;
            if ( EnableMapping)
            {
                string result = GetMapValue(RealText);
                if (null != result)
                {
                    RealText = result;
                }
            }
            SizeF textSize = g.MeasureString(RealText, this.Font);
            float left = x;
            float top = y;
            if (AutoSize)
            {
                this.Width = (int)textSize.Width;
                this.Height = (int)textSize.Height;
            }

            // 上面的计算可能会改变控件的大小，所以将
            // 边框的绘制放在放在这里
            if (null != Border)
            {
                Border.FillAreaBackground(g, x, y);
                Border.Print(ca, x, y);
                left += Border.BorderWidth;
                top += Border.BorderWidth;
            }
            Brush foreBrush = new SolidBrush(this.ForeColor);
            try
            {
                float tempWidth = this.Width - Border.BorderWidth;
                float tempHeight = this.Height - Border.BorderWidth;
                if (tempWidth != 0 && tempHeight != 0)
                {
                    StringFormat sf = GetStringFormat();
                    try
                    {
                        g.DrawString(RealText, this.Font, foreBrush, new RectangleF(left, top, tempWidth, tempHeight), sf);
                    }
                    finally
                    {
                        sf.Dispose();
                    }
                }
            }
            finally
            {
                foreBrush.Dispose();
            }
        }

        /// <summary>
        /// 缩放
        /// </summary>
        public void Zoom()
        {
            Zoom(HorizontalScale, HorizontalScale);
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

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns>对象</returns>
        public object Clone()
        {
            PmsLabelWrapper label = new PmsLabelWrapper();
            label.BackColor = this.BackColor;
            label.StrFormate = StrFormate;
            label.BorderName = this.BorderName;
            if (null != ExternDatas && ExternDatas.Count > 0)
            {
                if (null == label.ExternDatas)
                {
                    label.ExternDatas = new List<ExternData>();
                }
                foreach (ExternData ed in this.ExternDatas)
                {
                    object value = ed.Value;
                    if (null != value && value is ICloneable)
                    {
                        value = ((ICloneable)value).Clone();
                    }
                    label.ExternDatas.Add(new ExternData(ed.Key, value));
                }
            }
            if (null != this.Border)
            {
                label.Border = Border.Clone() as ElementBorder;
                label.Border.OwnerElement = label;
            }
            label.ForeColor = this.ForeColor;
            label.HasBottomBorder = this.HasBottomBorder;
            label.HasLeftBorder = this.HasLeftBorder;
            label.HasRightBorder = this.HasRightBorder;
            label.HasTopBorder = this.HasTopBorder;
            label.Height = this.Height;
            label.Width = this.Width;
            label.VerticalScale = this.VerticalScale;
            label.HorizontalScale = this.HorizontalScale;
            label.Location = this.Location;
            label.HasBorder = this.HasBorder;
            label.RealText = this.RealText;
            label.Text = this.Text;
            label.Visible = Visible;
            if (null != this.SourceField)
            {
                label.SourceField = this.SourceField.Clone() as SourceField;
            }
            if (null != Font)
            {
                label.Font = new Font(this.Font.FontFamily, this.Font.Size, this.Font.Style);
            }
            label.MoveX = this.MoveX;
            label.MoveY = this.MoveY;
            label.Name = this.Name;
            label.TextAlign = this.TextAlign;
            label.Expression = Expression;
            label.EnableMapping = EnableMapping;
            label.MappingTable = MappingTable;
            label.MESType = MESType;
            if (null != ((IElement)this).ExtendObject)
            {
                ((IElement)label).ExtendObject = ((IElement)this).ExtendObject.Clone() as ExtendObject;
            }
            return label;
        }

        /// <summary>
        /// 转换成控件
        /// </summary>
        /// <returns></returns>
        public Control ToControl(bool childTranslate = false)
        {
            return new PmsLabel(this);
        }

        public string GetMapValue(string key)
        {
            if (string.IsNullOrEmpty(MappingTable) || string.IsNullOrEmpty(key))
            {
                return null;
            }

            return PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetReplaceValue(MappingTable, key);
        }

        /// <summary>
        /// 获取字符串对齐格式
        /// </summary>
        /// <returns></returns>
        protected StringFormat GetStringFormat()
        {
            StringFormat sf = new StringFormat();
            int n = ((int)TextAlign) >> 3;
            if (n == 0)
            {
                sf.LineAlignment = StringAlignment.Near;
            }
            else if (n <= 8)
            {
                sf.LineAlignment = StringAlignment.Center;
            }
            else
            {
                sf.LineAlignment = StringAlignment.Far;
            }
            switch (sf.LineAlignment)
            {
                case StringAlignment.Near:
                    sf.Alignment = (StringAlignment)(((int)TextAlign) >> 1);
                    break;
                case StringAlignment.Center:
                    sf.Alignment = (StringAlignment)(((int)TextAlign) >> 5);
                    break;
                case StringAlignment.Far:
                    sf.Alignment = (StringAlignment)(((int)TextAlign) >> 9);
                    break;
            }
            return sf;
        }
    }
}
