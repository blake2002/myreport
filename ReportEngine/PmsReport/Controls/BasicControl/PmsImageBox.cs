using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using MES.Controls.Design;
using MES.Report;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using PMS.Libraries.ToolControls.Report.Controls.Editor;
using PMS.Libraries.ToolControls.Report.Controls.EditorDialog;
using PMS.Libraries.ToolControls.Report.Element;
using PMS.Libraries.ToolControls.Report.Elements.Util;
using PMS.Libraries.ToolControls.Report.Controls.TypeConvert;
using PMS.Libraries.ToolControls.PMSPublicInfo;

namespace PMS.Libraries.ToolControls.Report
{
    [ToolboxBitmap(typeof(PmsImageBox), "Resources.Image.png")]
    //[Designer(typeof(PmsImageBoxDesigner))]
    [DisplayName("PictureBox")]
    [Designer(typeof(MESDesigner))]
    [DefaultProperty("DbField")]
    public class PmsImageBox : ElementBase, ICloneable, IElement, IExpression, IBindField, IElementTranslator, IBindReportExpressionEngine, ISuspensionable, IProcessCmdKey
    {
        #region Public Property
        [Category("通用")]
        [Description("控件名字")]
        [Browsable(true)]
        public new string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }

        [Category("通用")]
        [Description("控件位置")]
        [Browsable(true)]
        public new Point Location
        {
            get { return base.Location; }
            set { base.Location = value; }
        }

        [Category("通用")]
        [Description("控件大小")]
        [Browsable(true)]
        public new Size Size
        {
            get { return base.Size; }
            set { base.Size = value; }
        }

        private bool _hasBorder = true;
        [Category("通用")]
        [Description("是否显示边框")]
        [DisplayName("EnableBorder")]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(true)]
        public override bool HasBorder
        {
            get
            {
                return _hasBorder;
            }
            set
            {
                _hasBorder = value;
                if (DesignMode)
                {
                    Invalidate();
                }
            }
        }

        [Category("通用")]
        [Description("边框配置")]
        [Browsable(true)]
        public override ElementBorder Border
        {
            get
            {
                return base.Border;
            }
            set
            {
                base.Border = value;
            }
        }

        private string _dbField = string.Empty;
        [Editor(typeof(TextEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        [Category("通用")]
        [Description("数据绑定")]
        [DisplayName("Binding")]
        public string DbField
        {
            get
            {
                return _dbField;
            }
            set
            {
                _dbField = value;
            }
        }

        private string _expression = string.Empty;
        [Category("通用")]
        [Browsable(true)]
        [Description("表达式")]
        [Editor(typeof(PMS.Libraries.ToolControls.Report.Controls.Editor.ExpressionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [TypeConverter(typeof(ExpressionConverter))]
        public string Expression
        {
            get
            {
                return _expression;
            }
            set
            {
                _expression = value;
                if (DesignMode)
                {
                    RealText = Text;
                    Invalidate();
                }
            }
        }

        [Browsable(true)]
        [Editor(typeof(BitmapEditor), typeof(UITypeEditor))]
        [Category("通用")]
        [Description("错误时显示的图片")]
        public Image ErrorImage
        {
            get;
            set;
        }

        private Image _image = null;
        /// <summary>
        /// 图片对象
        /// </summary>
        [Browsable(true)]
        [Editor(typeof(BitmapEditor), typeof(UITypeEditor))]
        [RefreshProperties(RefreshProperties.All)]
        [Localizable(true)]
        [Category("通用")]
        [Description("图片")]
        public Image Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                if (null != Site)
                {
                    Invalidate();
                }
            }
        }

        private PictureBoxSizeMode _mode = PictureBoxSizeMode.StretchImage;
        /// <summary>
        /// 图像显示模式
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        [Category("通用")]
        [Description("图片位置")]
        [DisplayName("SizeMode")]
        public PictureBoxSizeMode Mode
        {
            get
            {
                return _mode;
            }
            set
            {
                _mode = value;
                if (null != Site)
                {
                    Invalidate();
                }
            }
        }
        #endregion
        [Browsable(false)]
        [Category("MES控件属性")]
        public override SourceField SourceField
        {
            get;
            set;
        }

        [Browsable(false)]
        [Category("MES控件属性")]
        public new string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                if (null != Site)
                {
                    Invalidate();
                }
            }
        }

        private Image _errorImage
        {
            get;
            set;
        }

        ExtendObject IElement.ExtendObject
        {
            get;
            set;
        }

        [Browsable(false)]
        IReportExpressionEngine IBindReportExpressionEngine.ExpressionEngine
        {
            get;
            set;
        }

        public PmsImageBox()
            : base()
        {
            //默认大小
            Size = new Size(100, 50);
            Border = new RectangleBorder(this);
            this.BorderName = Border.Name;
            //this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
        }

        public PmsImageBox(IElement element)
            : base()
        {
            PmsImageBoxWrapper pib = element as PmsImageBoxWrapper;
            if (null != pib.Border)
            {
                Border = pib.Border.Clone() as ElementBorder;
                Border.OwnerElement = this;
            }
            BorderName = pib.BorderName;
            if (null != pib.Image)
            {
                Image = pib.Image.Clone() as Image;
            }
            if (null != pib.ErrorImage)
            {
                ErrorImage = pib.ErrorImage.Clone() as Image;
            }
            ExternDatas = new List<PMS.Libraries.ToolControls.Report.Elements.Util.ExternData>();
            Mode = pib.Mode;
            if (null != pib.ExternDatas)
            {
                foreach (ExternData ed in pib.ExternDatas)
                {
                    object value = ed.Value;
                    if (null != value && value is ICloneable)
                    {
                        value = ((ICloneable)value).Clone();
                    }
                    ExternDatas.Add(new ExternData(ed.Key, value));
                }
            }
            if (null != pib.Border)
            {
                Border = pib.Border.Clone() as ElementBorder;
                Border.OwnerElement = this;
            }

            if (null != pib.SourceField)
            {
                SourceField = pib.SourceField.Clone() as SourceField;
            }

            DbField = pib.DbField;
            Width = pib.Width;
            Height = pib.Height;
            HasLeftBorder = pib.HasLeftBorder;
            HasTopBorder = pib.HasTopBorder;
            HasRightBorder = pib.HasRightBorder;
            HasBottomBorder = pib.HasBottomBorder;
            HasBorder = pib.HasBorder;
            Location = pib.Location;
            BackColor = pib.BackColor;
            Visible = pib.Visible;
            MESType = pib.MESType;
            Name = pib.Name;
            if (null != ((IElement)pib).ExtendObject)
            {
                ((IElement)this).ExtendObject = ((IElement)pib).ExtendObject.Clone() as ExtendObject;
            }
            //return pib;
        }

        protected override void Dispose(bool disposing)
        {

            if (null != Image)
            {
                Image.Dispose();
                Image = null;
            }
            if (null != ErrorImage)
            {
                ErrorImage.Dispose();
                ErrorImage = null;
            }
            base.Dispose(disposing);
        }

        public new void BindValue(IDictionary<string, Object> values)
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

        public override int BindDataTableManager(IDataTableManager dtm, string bindPath)
        {
            IReportExpressionEngine expEngin = (this as IBindReportExpressionEngine).ExpressionEngine;
            bool newFlag = false;
            // 计算显示值
            if (null == expEngin)
            {
                expEngin = (this as IBindReportExpressionEngine).ExpressionEngine = new ReportExpressionEngine();
                newFlag = true;
            }
            object obj = null;
            if (!string.IsNullOrEmpty(Expression) && Expression.Trim().StartsWith("="))
            {
                // 启用表达式
                obj = expEngin.Execute(Expression.Trim().TrimStart("=".ToCharArray()), dtm, bindPath);
            }
            else if (!string.IsNullOrEmpty(DbField))
            {
                // 替换显示
                obj = expEngin.ExcuteBindObj(DbField, dtm, bindPath);
                if(null == obj)
                {
                    obj = expEngin.ExcuteBindText(DbField, dtm, bindPath);
                }
            }
            if (null != obj)
            {
                if (obj is byte[])
                {
                    obj = GetImageBytesFromOLEField(obj as byte[]);
                    // 二进制图片数据
                    RealText = Convert.ToBase64String(obj as byte[]);
                }
                else if (obj is string)
                {
                    string path = obj.ToString();
                    if (File.Exists(path))
                    {
                        try
                        {
                            Image = System.Drawing.Image.FromFile(path);
                        }
                        catch (System.Exception ex)
                        {
                            Image = null;
                        }
                    }
                }
            }
            
            if (newFlag)
                expEngin.Dispose();
            return 0;
        }

        private byte[] GetImageBytesFromOLEField(byte[] oleFieldBytes)
        {
            const string BITMAP_ID_BLOCK = "BM";
            const string JPG_ID_BLOCK = "\u00FF\u00D8\u00FF";
            const string PNG_ID_BLOCK = "\u0089PNG\r\n\u001a\n";
            const string GIF_ID_BLOCK = "GIF8";
            const string TIFF_ID_BLOCK = "II*\u0000";

            byte[] imageBytes;

            // Get a UTF7 Encoded string version
            System.Text.Encoding u8 = System.Text.Encoding.UTF7;
            string strTemp = u8.GetString(oleFieldBytes);

            // Get the first 300 characters from the string
            string strVTemp = strTemp.Substring(0, 300);

            // Search for the block
            int iPos = -1;
            if (strVTemp.IndexOf(BITMAP_ID_BLOCK) != -1)
                iPos = strVTemp.IndexOf(BITMAP_ID_BLOCK);
            else if (strVTemp.IndexOf(JPG_ID_BLOCK) != -1)
                iPos = strVTemp.IndexOf(JPG_ID_BLOCK);
            else if (strVTemp.IndexOf(PNG_ID_BLOCK) != -1)
                iPos = strVTemp.IndexOf(PNG_ID_BLOCK);
            else if (strVTemp.IndexOf(GIF_ID_BLOCK) != -1)
                iPos = strVTemp.IndexOf(GIF_ID_BLOCK);
            else if (strVTemp.IndexOf(TIFF_ID_BLOCK) != -1)
                iPos = strVTemp.IndexOf(TIFF_ID_BLOCK);
            else
                throw new Exception("Unable to determine header size for the OLE Object");

            // From the position above get the new image
            if (iPos == -1)
                throw new Exception("Unable to determine header size for the OLE Object");

            //Array.Copy(
            imageBytes = new byte[oleFieldBytes.LongLength - iPos];
            MemoryStream ms = new MemoryStream();
            ms.Write(oleFieldBytes, iPos, oleFieldBytes.Length - iPos);
            imageBytes = ms.ToArray();
            ms.Close();
            ms.Dispose();
            return imageBytes;
        }

        public override void Print(Canvas ca, float x, float y)
        {
            SetBorder();
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
                            int w = this.Width > ErrorImage.Width ? ErrorImage.Width : this.Width;
                            int h = this.Height > ErrorImage.Height ? ErrorImage.Height : this.Height;
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

        public override void DirectDraw(Canvas ca, float x, float y, float dpiZoom)
        {
            SetBorder();
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
                if(!string.IsNullOrEmpty(RealText))
                {
                    byte[] imageArr = Convert.FromBase64String(RealText);
                    if (null != imageArr)
                        this.Image = System.Drawing.Image.FromStream(new MemoryStream(imageArr));
                }

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
                if (null != ErrorImage)
                {
                    g.DrawImage(ErrorImage, x, y, this.Width, this.Height);
                }
            }

            if (null != Border)
            {
                Border.DirectDraw(ca, x, y, dpiZoom);
            }
        }


        public override object Clone()
        {
            PmsImageBox pib = new PmsImageBox();
            if (null != this.Border)
            {
                pib.Border = this.Border.Clone() as ElementBorder;
                pib.Border.OwnerElement = this;
            }
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
            foreach (ExternData data in ExternDatas)
            {
                object value = data.Value;
                if (null != value && value is ICloneable)
                {
                    value = ((ICloneable)value).Clone();
                }
                pib.ExternDatas.Add(new ExternData(data.Key, value));
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
            pib.DbField = this.DbField;
            if (null != Expression)
            {
                pib.Expression = Expression.Clone() as string;
            }
            pib.RealText = this.RealText;
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
            if (null != Tag)
            {
                if (Tag is ICloneable)
                {
                    pib.Tag = ((ICloneable)Tag).Clone();
                }
                else
                {
                    pib.Tag = Tag;
                }
            }

            if (null != ((IElement)this).ExtendObject)
            {
                ((IElement)pib).ExtendObject = ((IElement)this).ExtendObject.Clone() as ExtendObject;
            }

            return pib;
        }

        public IControlTranslator ToElement(bool transferChild)
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
            foreach (ExternData data in ExternDatas)
            {
                object value = data.Value;
                if (null != value && value is ICloneable)
                {
                    value = ((ICloneable)value).Clone();
                }
                pib.ExternDatas.Add(new ExternData(data.Key, value));
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
            if (_orginalFontSize > 0)
            {
                pib.Font = new Font(Font.FontFamily, _orginalFontSize);
            }
            else
            {
                pib.Font = new Font(this.Font.FontFamily, this.Font.Size);
            }
            pib.VerticalScale = 1f;
            pib.HorizontalScale = 1f;
            pib.DbField = DbField;
            pib.Visible = Visible;
            pib.MESType = MESType;
            pib.Name = Name;
            if (_orginalHeight > 0)
            {
                pib.Height = this._orginalHeight;
            }
            else
            {
                pib.Height = this.Height;
            }
            if (_orginalWidth > 0)
            {
                pib.Width = _orginalWidth;
            }
            else
            {
                pib.Width = this.Width;
            }
            //pib.VerticalScale = this.VerticalScale;
            //pib.HorizontalScale = this.HorizontalScale;
            if (_orginalLocation != Point.Empty)
            {
                pib.Location = this._orginalLocation;
            }
            else
            {
                pib.Location = this.Location;
            }
            pib.HasLeftBorder = this.HasLeftBorder;
            pib.HasTopBorder = this.HasTopBorder;
            pib.HasRightBorder = this.HasRightBorder;
            pib.HasBottomBorder = this.HasBottomBorder;
            pib.HasBorder = this.HasBorder;
            pib.BackColor = this.BackColor;
            if (null != ((IElement)this).ExtendObject)
            {
                ((IElement)pib).ExtendObject = ((IElement)this).ExtendObject.Clone() as ExtendObject;
            }

            return pib;
        }

        public SuspensionItem[] ListSuspensionItems()
        {
            SuspensionItem[] items = new SuspensionItem[2];
            using (Stream stream = Assembly.GetExecutingAssembly().
                                   GetManifestResourceStream(
                                   "PMS.Libraries.ToolControls.Report.Resources.Field.ico"))
            {
                Image img = Image.FromStream(stream);
                items[0] = new SuspensionItem(img, "字段", "字段", FieldBindAction);
                stream.Dispose();
            }

            using (Stream stream = Assembly.GetExecutingAssembly().
                                    GetManifestResourceStream(
                                    "PMS.Libraries.ToolControls.Report.Resources.Fx.png"))
            {
                Image img = Image.FromStream(stream);
                items[1] = new SuspensionItem(img, "表达式", "表达式", FunctionAction);
                stream.Dispose();
            }
            return items;
        }
        bool IProcessCmdKey.ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            bool bProcessed = false;
            bool bAltKey = (((ushort)PMS.Libraries.ToolControls.PMSPublicInfo.APIs.APIsUser32.GetAsyncKeyState(0x12)) & 0xffff) != 0;
            if (bAltKey)
            {
                if ((int)msg.WParam > 0 && (int)msg.WParam < 255)
                {
                    switch ((char)(msg.WParam))
                    {
                        case 'B'://
                            FieldBindAction();
                            bProcessed = true;
                            break;
                        case 'D'://

                            break;
                        case 'F':
                            FunctionAction();
                            bProcessed = true;
                            break;
                    }
                }
            }
            return bProcessed;
        }
        private void FieldBindAction()
        {
            SourceField sf = GetSourceField(this);
            using (FieldBindDialog fbd = new FieldBindDialog(sf, null != DbField ? DbField : null))
            {
                if (sf == null)
                {
                    fbd.BindingSourceField = false;
                }
                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    IPmsReportDataBind dataBind = this as IPmsReportDataBind;
                    if (null != dataBind)
                    {
                        dataBind.SourceField = fbd.SourceField;
                    }
                    DbField = fbd.Value;
                    NotifyDesignSurfaceChange();
                }
            }
        }

        private void FunctionAction()
        {
            using (MES.Report.MESReportExpressionEditor expEditor = new MES.Report.MESReportExpressionEditor(Expression))
            {
                expEditor.ControlName = Name;
                if (expEditor.ShowDialog() == DialogResult.OK)
                {
                    Expression = expEditor.ExpressionText;
                    NotifyDesignSurfaceChange();
                }
            }
        }

        private SourceField GetSourceField(IElement element)
        {
            if (null == element)
            {
                return null;
            }
            IPmsReportDataBind parent = element.Parent as IPmsReportDataBind;
            if (null == parent)
            {
                return null;
            }
            if (null == parent.SourceField)
            {
                return GetSourceField(parent as IElement);
            }

            return parent.SourceField;
        }

        private void NotifyDesignSurfaceChange()
        {
            if (null != Site)
            {
                IComponentChangeService cs = Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                if (null != cs)
                {
                    cs.OnComponentChanged(this, null, null, null);
                }
            }
        }
    }
}
