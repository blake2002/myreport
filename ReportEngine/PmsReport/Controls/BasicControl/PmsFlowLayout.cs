using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using PMS.Libraries.ToolControls.Report.Controls.Editor;
using PMS.Libraries.ToolControls.Report.Controls.TypeConvert;
using PMS.Libraries.ToolControls.Report.Element;
using PMS.Libraries.ToolControls.Report.Elements.Util;

namespace PMS.Libraries.ToolControls.Report
{
    [Serializable]
    [ToolboxBitmap(typeof(PmsLabel), "Resources.Flow.png")]
    [DisplayName("Layout")]
    [DefaultProperty("IsVerticalLayout")]
    public class PmsFlowLayout : ElementBase, IElement, IElementTranslator
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(false)]
        [Browsable(true)]
        [Description("是否垂直排版")]
        [Localizable(true)]
        [Category("通用")]
        [DisplayName("VerticalLayout")]
        public bool IsVerticalLayout
        {
            get;
            set;
        }

        [Category("通用")]
        [DisplayName("Margin")]
        [Description("容器边距")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Padding MESMargin
        {
            get;
            set;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(false)]
        [Browsable(true)]
        [Description("控件间距")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(true)]
        [Category("通用")]
        [DisplayName("ControlPadding")]
        public int ObjectPadding
        {
            get;
            set;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(false)]
        [Browsable(true)]
        [Description("行间距")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(true)]
        [Category("通用")]
        public int RowPadding
        {
            get;
            set;
        }
        #endregion
        private const int _penWidth = 8;

        protected SourceField _sourceField = null;
        /// <summary>
        /// 数据源
        /// </summary>
        [Editor(typeof(SourceEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(false)]
        [Category("MES控件属性")]
        [TypeConverter(typeof(SourceConverter))]
        public override SourceField SourceField
        {
            get
            {
                return _sourceField;
            }
            set
            {
                _sourceField = value;
            }
        }

        private ContentAlignment _textAlign = ContentAlignment.MiddleLeft;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(0)]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Localizable(true)]
        [Category("MES控件属性")]
        public virtual ContentAlignment TextAlign
        {
            get
            {
                return _textAlign;
            }
            set
            {
                _textAlign = value;
                if (DesignMode)
                {
                    Invalidate();
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(false)]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Localizable(true)]
        [Category("MES控件属性")]
        public override bool AutoSize
        {
            get
            {
                return false;
            }
            set
            {
                base.AutoSize = value;
                if (DesignMode)
                {
                    Invalidate();
                }
            }
        }

        private string _borderName = string.Empty;
        public override string BorderName
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

        protected AlignStyle _rowElementsAlign = AlignStyle.Left;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(false)]
        [Browsable(false)]
        [Description("行内元素对齐方式")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(true)]
        [Category("MES控件属性")]
        public AlignStyle RowElementsAlign
        {
            get
            {
                return _rowElementsAlign;
            }
            set
            {
                _rowElementsAlign = value;
            }
        }

        protected AlignStyle _verticalAlign = AlignStyle.Left;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(false)]
        [Browsable(false)]
        [Description("垂直对齐方式")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(true)]
        [Category("MES控件属性")]
        public AlignStyle VerticalAlign
        {
            get
            {
                return _verticalAlign;
            }
            set
            {
                _verticalAlign = value;
            }
        }

        protected AlignStyle _align = AlignStyle.Left;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(false)]
        [Browsable(false)]
        [Description("对齐方式")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(true)]
        [Category("MES控件属性")]
        public AlignStyle Align
        {
            get
            {
                return _align;
            }
            set
            {
                _align = value;
            }
        }

        [Editor(typeof(TextEditor), typeof(UITypeEditor))]
        [Browsable(false)]
        [Category("MES控件属性")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(true)]
        [Browsable(false)]
        [Description("背景颜色")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Localizable(true)]
        [Category("MES控件属性")]
        public new Color BackColor
        {
            get
            {
                return base._backColor;
            }
            set
            {
                base._backColor = value;
            }
        }

        ExtendObject IElement.ExtendObject
        {
            get;
            set;
        }

        public PmsFlowLayout()
        {
            Border = new RectangleBorder(this);
            this.MinimumSize = new Size(110, 16);
            //this.MaximumSize = this.MinimumSize;
        }

        public PmsFlowLayout(IElement element)
        {
            this.MinimumSize = new Size(110, 16);
            this.MaximumSize = this.MinimumSize;
            PmsFlowLayoutWrapper layout = element as PmsFlowLayoutWrapper;
            Name = layout.Name;
            AutoSize = layout.AutoSize;
            BackColor = layout.BackColor;
            if (null != layout.BackgroundImage)
            {
                BackgroundImage = layout.BackgroundImage.Clone() as Image;
            }
            BackgroundImageLayout = layout.BackgroundImageLayout;
            if (null != layout.Border)
            {
                Border = layout.Border.Clone() as ElementBorder;
                Border.OwnerElement = this;
            }
            BorderName = layout.BorderName;
            if (null != layout.ExternDatas)
            {
                ExternDatas = new List<ExternData>();
                foreach (ExternData ed in layout.ExternDatas)
                {
                    object value = ed.Value;
                    if (null != value && value is ICloneable)
                    {
                        value = ((ICloneable)value).Clone();
                    }
                    ExternDatas.Add(new ExternData(ed.Key, value));
                }
            }
            if (null != layout.Font)
            {
                Font = layout.Font.Clone() as Font;
            }
            ObjectPadding = layout.ObjectPadding;
            RowPadding = layout.RowPadding;
            IsVerticalLayout = layout.IsVerticalLayout;
            HasBorder = layout.HasBorder;
            HasBottomBorder = layout.HasBottomBorder;
            HasLeftBorder = layout.HasLeftBorder;
            HasRightBorder = layout.HasRightBorder;
            HasTopBorder = layout.HasTopBorder;
            Height = layout.Height;
            HorizontalScale = layout.HorizontalScale;
            Location = layout.Location;
            MESType = layout.MESType;
            MoveX = layout.MoveX;
            MoveY = layout.MoveY;

            //layout.Parent = Parent;
            Text = layout.Text;
            VerticalScale = layout.VerticalScale;
            Width = layout.Width;
            MESMargin = new Padding(layout.MESMargin.Left, layout.MESMargin.Top, layout.MESMargin.Right, layout.MESMargin.Bottom);
            Visible = layout.Visible;
            MESType = layout.MESType;
            if (null != ((IElement)layout).ExtendObject)
            {
                ((IElement)this).ExtendObject = ((IElement)layout).ExtendObject.Clone() as ExtendObject;
            }
        }

        public override void Print(Canvas ca, float x, float y)
        {
            SetBorder();
            Graphics g = ca.Graphics;
            if (null != g)
            {
                if (null != Border)
                {
                    Border.Print(ca, x, y);
                }
                Assembly assm = Assembly.GetExecutingAssembly();
                Stream stream = assm.GetManifestResourceStream("PMS.Libraries.ToolControls.Report.Resources.Flow.png");
                Image img = Image.FromStream(stream, true);
                g.DrawImage(img, 0, 0);
                stream.Dispose();
                g.DrawString(this.Name, this.Font, Brushes.Black, x + img.Width, 0);
                img.Dispose();
            }
        }

        public override object Clone()
        {
            PmsFlowLayout layout = new PmsFlowLayout();
            layout.BackColor = this.BackColor;
            layout.Anchor = this.Anchor;
            layout.Bounds = this.Bounds;
            layout.BorderName = this.BorderName;
            if (null != ExternDatas && ExternDatas.Count > 0)
            {
                if (null == layout.ExternDatas)
                {
                    layout.ExternDatas = new List<ExternData>();
                }
                foreach (ExternData ed in this.ExternDatas)
                {
                    layout.ExternDatas.Add(ed);
                }
            }
            if (null != Border)
            {
                layout.Border = Border.Clone() as ElementBorder;
                layout.Border.OwnerElement = layout;
            }
            layout.Font = Font.Clone() as Font;
            layout.ObjectPadding = this.ObjectPadding;
            layout.RowPadding = this.RowPadding;
            layout.Height = this.Height;
            layout.Width = this.Width;
            layout.VerticalScale = this.VerticalScale;
            layout.HorizontalScale = this.HorizontalScale;
            layout.Location = this.Location;
            layout.Left = this.Left;
            layout.Top = this.Top;
            layout.Text = this.Text;
            layout.Region = this.Region;
            layout.RightToLeft = this.RightToLeft;
            layout.Size = this.Size;
            layout.MoveX = this.MoveX;
            layout.MoveY = this.MoveY;
            layout.Name = this.Name;
            layout.IsVerticalLayout = this.IsVerticalLayout;
            layout.MESMargin = new Padding(MESMargin.Left, MESMargin.Top, MESMargin.Right, MESMargin.Bottom);
            layout.Visible = Visible;
            layout.MESType = MESType;
            if (null != Tag)
            {
                if (Tag is ICloneable)
                {
                    layout.Tag = ((ICloneable)Tag).Clone();
                }
                else
                {
                    layout.Tag = Tag;
                }
            }

            if (null != ((IElement)this).ExtendObject)
            {
                ((IElement)layout).ExtendObject = ((IElement)this).ExtendObject.Clone() as ExtendObject;
            }
            return layout;
        }

        public IControlTranslator ToElement(bool transferChild)
        {
            PmsFlowLayoutWrapper layout = new PmsFlowLayoutWrapper();
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
            layout.ObjectPadding = this.ObjectPadding;
            layout.RowPadding = this.RowPadding;
            layout.IsVerticalLayout = IsVerticalLayout;
            if (_orginalFontSize > 0)
            {
                layout.Font = new Font(Font.FontFamily, _orginalFontSize);
            }
            else
            {
                layout.Font = new Font(this.Font.FontFamily, this.Font.Size);
            }
            layout.VerticalScale = 1f;
            layout.HorizontalScale = 1f;
            layout.HasBorder = HasBorder;
            layout.HasBottomBorder = HasBottomBorder;
            layout.HasLeftBorder = HasLeftBorder;
            layout.HasRightBorder = HasRightBorder;
            layout.HasTopBorder = HasTopBorder;
            layout.Location = Location;
            layout.MESType = MESType;
            layout.MoveX = MoveX;
            layout.MoveY = MoveY;
            layout.Text = Text;
            layout.Visible = Visible;
            layout.MESType = MESType;
            if (_orginalHeight > 0)
            {
                layout.Height = this._orginalHeight;
            }
            else
            {
                layout.Height = this.Height;
            }
            if (_orginalWidth > 0)
            {
                layout.Width = _orginalWidth;
            }
            else
            {
                layout.Width = this.Width;
            }
            //layout.VerticalScale = this.VerticalScale;
            //layout.HorizontalScale = this.HorizontalScale;
            if (_orginalLocation != Point.Empty)
            {
                layout.Location = this._orginalLocation;
            }
            else
            {
                layout.Location = this.Location;
            }
            layout.MESMargin = new Padding(MESMargin.Left, MESMargin.Top, MESMargin.Right, MESMargin.Bottom);
            if (null != ((IElement)this).ExtendObject)
            {
                ((IElement)layout).ExtendObject = ((IElement)this).ExtendObject.Clone() as ExtendObject;
            }
            return layout;
        }

    }

    [Flags]
    public enum AlignStyle
    {
        Left = 1,
        Center,
        Right,
        Justify
    }
}
