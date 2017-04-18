using System;
using System.Collections.Generic;
using PMS.Libraries.ToolControls.Report.Element;
using System.Drawing;
using System.Windows.Forms;
using PMS.Libraries.ToolControls.Report.Elements.Util;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using PMS.Libraries.ToolControls.BarcodeLib;
using System.Text.RegularExpressions;

namespace PMS.Libraries.ToolControls.Report
{
    [Serializable]
    public class BarCodeWrapper : IElement,IBindField, IPmsReportDataBind,
        IPrintable,IControlTranslator,
        ICloneable, IResizable, IDataMapping,IExpression,IElementContent
    {
        protected int _orginalWidth = -1;
        protected int _orginalHeight = -1;
        protected Point _orginalLocation = Point.Empty;
        protected float _orginalFontSize = -1f;
        protected string _orginalFontName = string.Empty;


        public string ErrorText
        {
            get;
            set;
        }

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

        /// <summary>
        /// 真实文本
        /// </summary>
        public string RealText { get; set;}

        /// <summary>
        /// 是否启用映射
        /// </summary>
        public  bool EnableMapping { get; set;}

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
        /// 映射表名
        /// </summary>
        public string MappingTable {  get; set; }

        public AlignmentPositions BarCodeAlign{ get; set;}

        public TYPE BarCodeType { get; set; }

        public bool IncludeLabel { get; set; }

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

        private Font _labelFont = new Font("宋体", 9, FontStyle.Regular);
        public Font LabelFont
        {
            get
            {
                return _labelFont;
            }
            set
            {
                _labelFont = value;
            }
        }


        /// <summary>
        /// 额外的存储结构
        /// </summary>
        public ExtendObject ExtendObject { get; set; }

        /// <summary>
        /// 获取映射值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetMapValue(string key)
        {
            if (string.IsNullOrEmpty(MappingTable) || string.IsNullOrEmpty(key))
            {
                return null;
            }

            return PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetReplaceValue(MappingTable, key);
        }

        public void BindValue(IDictionary<string, Object> values)
        {
            if (null != values && values.Count > 0)
            {
                RealText = Text;
                string str = @"\[#?%?(p{0,}/?\w){1,}%?#?\]";//@"\[#?%?(([\u4E00-\u9FFF]){0,}/?\w){1,}%?#?\]";
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
                            object value = values[key];
                            string newStr = " ";
                            if (null != value)
                            {
                                newStr = value.ToString();
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

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns>对象</returns>
        public object Clone()
        {
            BarCodeWrapper bcw = new BarCodeWrapper();
            bcw.Name = Name;
            bcw.AutoSize = AutoSize;
            bcw.BackColor = BackColor;
            if (null != BackgroundImage)
            {
                bcw.BackgroundImage = BackgroundImage.Clone() as Image;
            }
            bcw.BackgroundImageLayout = BackgroundImageLayout;
            if (null != Border)
            {
                bcw.Border = Border.Clone() as ElementBorder;
                bcw.Border.OwnerElement = bcw;
            }
            bcw.BorderName = BorderName;
            if (null != ExternDatas)
            {
                bcw.ExternDatas = new List<ExternData>();
                foreach (ExternData ed in ExternDatas)
                {
                    object value = ed.Value;
                    if (null != value && value is ICloneable)
                    {
                        value = ((ICloneable)value).Clone();
                    }
                    bcw.ExternDatas.Add(new ExternData(ed.Key, value));
                }
            }
            if (null != Font)
            {
                bcw.Font = Font.Clone() as Font;
            }
            bcw.HasBorder = HasBorder;
            bcw.HasBottomBorder = HasBottomBorder;
            bcw.HasLeftBorder = HasLeftBorder;
            bcw.HasRightBorder = HasRightBorder;
            bcw.HasTopBorder = HasTopBorder;
            bcw.Height = Height;
            bcw.HorizontalScale = HorizontalScale;
            bcw.Location = Location;
            bcw.MESType = MESType;
            bcw.MoveX = MoveX;
            bcw.MoveY = MoveY;
            bcw.Parent = Parent;
            bcw.Text = Text;
            bcw.VerticalScale = VerticalScale;
            bcw.Width = Width;
            if (null != SourceField)
            {
                bcw.SourceField = SourceField.Clone();
            }
            bcw.EnableMapping = EnableMapping;
            bcw.MappingTable = MappingTable;
            bcw.Expression = Expression;
            bcw.RealText = RealText;
            bcw.IncludeLabel = IncludeLabel;
            bcw.BarCodeAlign = BarCodeAlign;
            bcw.BarCodeType = BarCodeType;
            bcw.Visible = Visible;
            bcw.MESType = MESType;
            if (null != ((IElement)this).ExtendObject)
            {
                ((IElement)bcw).ExtendObject = ((IElement)this).ExtendObject.Clone() as ExtendObject;
            }
            return bcw;
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
            if (null == ca || null == ca.Graphics)
            {
                return;
            }
            if (string.IsNullOrEmpty(RealText) && !string.IsNullOrEmpty(Text))
            {
                RealText = Text;
            }
            else if (string.IsNullOrEmpty(RealText))
            {
                return;
            }
            Barcode bc = null;
            try
            {
                bc = new Barcode();
                bc.Alignment = this.BarCodeAlign;
                bc.IncludeLabel = IncludeLabel;
                bc.LabelFont = LabelFont;
                Image img = bc.Encode(BarCodeType, RealText, this.ForeColor, this.BackColor, this.Width, this.Height);
                if (null != img)
                {
                    ca.Graphics.DrawImage(img, x, y, this.Width, this.Height);
                    img.Dispose();
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                if (!string.IsNullOrEmpty(message))
                {
                    if (!message.StartsWith("EENCODE-1:") && !message.StartsWith("EENCODE-2:"))
                    {
                        SizeF size = ca.Graphics.MeasureString(ErrorText, this.Font);
                        float left = ((float)this.Width - size.Width) / 2;
                        if (left < 0)
                        {
                            left = x;
                        }
                        else
                        {
                            left += x;
                        }
                        float top = ((float)this.Height - size.Height) / 2;
                        if (top < 0)
                        {
                            top = y;
                        }
                        else
                        {
                            top += y;
                        }
                        float width = this.Width > size.Width ? size.Width : this.Width;
                        float height = this.Height > size.Height ? size.Height : this.Height;
                        ca.Graphics.DrawString(ErrorText, this.Font, Brushes.Red, new RectangleF(left, top, width, height));
                    }
                }
            }
            finally
            {
                if (null != bc)
                {
                    bc.Dispose();
                }
            }
            if (null != Border)
            {
                Border.Print(ca, x, y);
            }

        }

        /// <summary>
        /// 转换成控件
        /// </summary>
        /// <returns></returns>
        public Control ToControl(bool childTranslate = false)
        {
            return new BarCode(this);
        }
    }
}
