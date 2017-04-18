using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMS.Libraries.ToolControls.Report.Element;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using PMS.Libraries.ToolControls.Report.Elements.Util;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace PMS.Libraries.ToolControls.Report
{
    [Serializable]
    public class PmsImageBoxWrapper : IElement, IBindField, IPmsReportDataBind,
        IPrintable, ICloneable, IResizable,IControlTranslator
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
        /// 绑定的图片
        /// </summary>
        public Image Image { get; set; }

        /// <summary>
        /// 绑定数据错误是显示的图片
        /// </summary>
        public Image ErrorImage { get; set; }

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
        /// 缩放模式
        /// </summary>
        public PictureBoxSizeMode Mode { get; set; }
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
        /// 绑定的字段
        /// </summary>
        public string DbField
        {
            get;
            set;
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
        /// 刷新
        /// </summary>
        public void Invalidate()
        {

        }

        /// <summary>
        /// 绑定值
        /// </summary>
        /// <param name="values"></param>
        public void BindValue(IDictionary<string, Object> values)
        {
            if (string.IsNullOrEmpty(DbField))
            {
                return;
            }

            string field = DbField;
            if (field.Contains("["))
            {
                field = field.Substring(field.LastIndexOf("[") + 1);
            }
            if (field.Contains("]"))
            {
                field = field.Substring(0, field.IndexOf("]"));
            }
            if (null != values && values.Keys.Contains(field))
            {
                Image bmp = values[field] as Image;
                if (null != bmp)
                {
                    if (null != Image)
                    {
                        Image.Dispose();
                        Image = null;
                    }
                    Image = bmp;
                }
                else
                {
                    try
                    {
                        Byte[] bytes = (Byte[])values[field];
                        if (null != bytes && bytes.Length > 0)
                        {
                            MemoryStream ms = new MemoryStream(bytes);
                            Image img = Image.FromStream(ms);
                            ms.Dispose();
                            if (null != Image)
                            {
                                Image.Dispose();
                                Image = null;
                            }
                            Image = img;
                        }
                    }
                    catch
                    {
                        if (null != Image)
                        {
                            Image.Dispose();
                            Image = null;
                        }
                    }
                }
            }
        }

        public object Clone()
        {
            PmsImageBoxWrapper pib = new PmsImageBoxWrapper();
            pib.Border = this.Border;
            pib.BorderName = this.BorderName;
            if (null != this.Image)
            {
                pib.Image = this.Image.Clone() as Image;
            }
            if (null != ErrorImage)
            {
                pib.ErrorImage = this.ErrorImage.Clone() as Image;
            }
            pib.ExternDatas = new List<PMS.Libraries.ToolControls.Report.Elements.Util.ExternData>();
            pib.Mode = this.Mode;
            foreach (ExternData ed in ExternDatas)
            {
                object value = ed.Value;
                if (null != value && value is ICloneable)
                {
                    value = ((ICloneable)value).Clone();
                }
                pib.ExternDatas.Add(new ExternData(ed.Key, value));
            }
            if (null != this.Border)
            {
                pib.Border = this.Border.Clone() as ElementBorder;
                pib.Border.OwnerElement = pib;
            }
            if (null != SourceField)
            {
                pib.SourceField = SourceField.Clone() as SourceField;
            }
            pib.DbField = DbField;
            pib.Width = this.Width;
            pib.Height = this.Height;
            pib.HasLeftBorder = this.HasLeftBorder;
            pib.HasTopBorder = this.HasTopBorder;
            pib.HasRightBorder = this.HasRightBorder;
            pib.HasBottomBorder = this.HasBottomBorder;
            pib.HasBorder = this.HasBorder;
            pib.Location = this.Location;
            pib.BackColor = this.BackColor;
            pib.Visible = Visible;
            pib.MESType = MESType;
            pib.Name = Name;
            if (null != ((IElement)this).ExtendObject)
            {
                ((IElement)pib).ExtendObject = ((IElement)this).ExtendObject.Clone() as ExtendObject;
            }

            return pib;
        }

        /// <summary>
        /// 转换成控件
        /// </summary>
        /// <returns></returns>
        public Control ToControl(bool childTranslate = false)
        {
            return new PmsImageBox(this);
        }

        /// <summary>
        /// 缩放
        /// </summary>
        public void Zoom()
        {
             Zoom( HorizontalScale, VerticalScale);
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
        /// 打印或者显示在指定画布,该方法的调用用在显示报表
        /// <remarks>使用x,y偏移量是为了绘图转换坐标系方便</remarks>
        /// </summary>
        /// <param name="g">绘图图面,拥有该参数是为了可以灵活的转移图形的输出画面</param>
        /// <param name="x">横坐标位置的偏移值</param>
        /// <param name="y">纵坐标位置的偏移值</param>
        public void Print(Canvas ca, float x, float y)
        {
            Graphics g = ca.Graphics;
            if (null == g)
            {
                return;
            }

            if (null != Border)
            {
                Border.FillAreaBackground(ca.Graphics, x, y);
            }
            try
            {
                switch (Mode)
                {
                    case PictureBoxSizeMode.Normal:
                        if (null != Image)
                        {
                            int w = this.Width > Image.Width ? Image.Width : this.Width;
                            int h = this.Height > Image.Height ? Image.Height : this.Height;
                            g.DrawImage(this.Image, x, y, new Rectangle(0, 0, w, h), GraphicsUnit.Pixel);

                        }
                        else if (null != ErrorImage)
                        {
                            int w = this.Width > Image.Width ? Image.Width : this.Width;
                            int h = this.Height > Image.Height ? Image.Height : this.Height;
                            g.DrawImage(this.ErrorImage, x, y, new Rectangle(0, 0, w, h), GraphicsUnit.Pixel);
                        }
                        break;
                    case PictureBoxSizeMode.CenterImage:
                        float imageLeft = 0, imageTop = 0;
                        float bckLeft = 0, bckTop = 0;
                        float imgDrawWidth = 0, imgDrawHeight = 0;
                        if (this.Width >= Image.Width)
                        {
                            //imageLeft = x;
                            imageLeft = 0;
                            bckLeft = x + ((this.Width - Image.Width) / 2);
                            imgDrawWidth = Image.Width;
                        }
                        else
                        {
                            imageLeft = (Image.Width - this.Width) / 2;
                            bckLeft = x;
                            imgDrawWidth = this.Width;
                        }

                        if (this.Height >= Image.Height)
                        {
                            //imageTop = y;
                            imageTop = 0;
                            bckTop = y + ((this.Height - Image.Height) / 2);
                            imgDrawHeight = Image.Height;
                        }
                        else
                        {
                            imageTop = (Image.Height - this.Height) / 2;
                            bckTop = y;
                            imgDrawHeight = this.Height;
                        }
                        if (null != Image)
                        {
                            g.DrawImage(Image, bckLeft, bckTop,
                            new Rectangle((int)imageLeft, (int)imageTop, (int)imgDrawWidth, (int)imgDrawHeight), GraphicsUnit.Pixel);
                        }
                        else if (null != ErrorImage)
                        {
                            g.DrawImage(ErrorImage, bckLeft, bckTop,
                            new Rectangle((int)imageLeft, (int)imageTop, (int)imgDrawWidth, (int)imgDrawHeight), GraphicsUnit.Pixel);
                        }

                        break;
                    case PictureBoxSizeMode.AutoSize:
                        this.Width = Image.Width;
                        this.Height = Image.Height;
                        if (null != Image)
                        {
                            g.DrawImage(Image, x, y, Image.Width, Image.Height);
                        }
                        else if (null != ErrorImage)
                        {
                            g.DrawImage(ErrorImage, x, y, Image.Width, Image.Height);
                        }
                        break;
                    case PictureBoxSizeMode.StretchImage:
                        if (null != Image)
                        {
                            g.DrawImage(Image, x, y, this.Width, this.Height);
                        }
                        else if (null != ErrorImage)
                        {
                            g.DrawImage(ErrorImage, x, y, this.Width, this.Height);
                        }
                        break;
                    case PictureBoxSizeMode.Zoom:
                        float imgWidthHeightScale = (float)Image.Width / Image.Height;
                        float imgWidth = 0, imgHeight = 0;
                        float drawLeft = 0, drawTop = 0;
                        float ctrlWidthHeightScale = (float)this.Width / this.Height;
                        if (ctrlWidthHeightScale > imgWidthHeightScale)
                        {
                            imgHeight = this.Height;
                            imgWidth = imgHeight * imgWidthHeightScale;
                            drawLeft = ((float)this.Width - imgWidth) / 2 + x;
                            drawTop = y;
                        }
                        else
                        {
                            imgWidth = this.Width;
                            imgHeight = imgWidth / imgWidthHeightScale;
                            drawLeft = x;
                            drawTop = ((float)this.Height - imgHeight) / 2 + y;
                        }
                        if (null != Image)
                        {
                            g.DrawImage(Image, drawLeft, drawTop, imgWidth, imgHeight);
                        }
                        else if (null != ErrorImage)
                        {
                            g.DrawImage(ErrorImage, drawLeft, drawTop, imgWidth, imgHeight);
                        }

                        break;
                }
            }
            catch
            {

            }

            if (null != Border)
            {
                Border.Print(ca, x, y);
            }
        }
    }
}
