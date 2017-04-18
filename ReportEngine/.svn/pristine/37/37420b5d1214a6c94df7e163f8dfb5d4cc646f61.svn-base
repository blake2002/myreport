using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using PMS.Libraries.ToolControls.Report.Element;
using System.Drawing;
using PMS.Libraries.ToolControls.Report.Elements.Util;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using System.Windows.Forms;
using PMS.Libraries.ToolControls.Report.Designer;
using PMS.Libraries.ToolControls.Report.Controls.Editor;
using System.Drawing.Design;
using MES.Controls.Design;
using PMS.Libraries.ToolControls.Report.Controls.EditorDialog;
using System.ComponentModel.Design;
using System.IO;
using System.Reflection;
using MES.PublicInterface;
using System.Xml;
using PMS.Libraries.ToolControls.PMSPublicInfo;
using PMS.Libraries.ToolControls.Report.Controls.TypeConvert;
using MES.Report;

namespace PMS.Libraries.ToolControls.Report
{
    [Serializable]
    [DisplayName("Panel")]
    [ToolboxBitmap(typeof(PmsPanel), "Resources.Panel.png")]
    [Designer(typeof(MESParentDesigner))]
    [DefaultProperty("SourceField")]
    public class PmsPanel : ElementPanel, ISuspensionable, IVisibleExpression, IElementContainer, IUIDesignExpStruct, IElementTranslator, IDataXmlNodes, IElement, IProcessCmdKey
        , IExpression, IBindReportExpressionEngine
    {
        [NonSerialized]
        private string _guid = string.Empty;
        [Browsable(false)]
        public string Guid
        {
            get { return _guid; }
            set { _guid = value; }
        }

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
                Invalidate();
            }
        }

        private new ElementBorder _border = null;
        [Category("通用")]
        [Description("边框配置")]
        [Browsable(true)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override ElementBorder Border
        {
            get
            {
                return _border;
            }
            set
            {
                _border = value;
                _border.OwnerElement = this;
                Invalidate();
            }
        }

        [Category("通用")]
        [Description("绑定数据")]
        [DisplayName("Binding")]
        [Browsable(true)]
        public override SourceField SourceField
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
        public string VisibleExpression
        {
            get;
            set;
        }

        [Category("通用")]
        [Description("背景颜色")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [EditorBrowsable(EditorBrowsableState.Always)]
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

        private bool _transparent = false;
        /// <summary>
        /// 运行时是否画边框
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        [Category("通用")]
        [DefaultValue(false)]
        [Description("报表查看或者打印时透明")]
        public bool Transparent
        {
            get
            {
                return _transparent;
            }
            set
            {
                _transparent = value;
            }
        }

        private bool _DisplayNullRecord = true;
        [Description("行绑定数据集无记录或为空时是否显示为设计时内容")]
        [Category("通用")]
        [DefaultValue(true)]
        [Browsable(true)]
        public bool DisplayNullRecord
        {
            get
            {
                return _DisplayNullRecord;
            }
            set
            {
                _DisplayNullRecord = value;
            }
        }

        private string _expression = string.Empty;
        [Category("通用")]
        [Browsable(false)]
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

        #endregion

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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(false)]
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

        private bool _isRedrawText = false;
        /// <summary>
        /// 是否画文本
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(false)]
        [Category("通用")]
        public bool IsRedrawText
        {
            get
            {
                return _isRedrawText;
            }
            set
            {
                _isRedrawText = value;
                Invalidate();
            }
        }

        //[Editor(typeof(ExpressionEditor),typeof(UITypeEditor))]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        //[Browsable(true)]
        //[Category("MES控件属性")]
        //public Expression Expression
        //{
        //    get;
        //    set;
        //}

        private ContentAlignment _textAlign = ContentAlignment.MiddleLeft;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(16)]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
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
                Invalidate();
            }
        }

        ExtendObject IElement.ExtendObject
        {
            get;
            set;
        }

        [Browsable(false)]
        public TreeNode ExpStructNode
        {
            get
            {
                TreeNode node = new TreeNode();
                node.Text = Name;
                node.Name = Name;
                node.Tag = MESVarType.MESNodefined;
                return node;
            }
        }

        [Browsable(false)]
        IReportExpressionEngine IBindReportExpressionEngine.ExpressionEngine
        {
            get;
            set;
        }

        public PmsPanel()
            : base()
        {
            this.Size = new Size(200, 100);
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
        }

        public PmsPanel(IElement element)
            : base()
        {
            PmsPanelWrapper panel = element as PmsPanelWrapper;
            BackColor = panel.BackColor;
            BorderName = panel.BorderName;
            if (null != panel.ExternDatas && panel.ExternDatas.Count > 0)
            {
                ExternDatas = new List<ExternData>();

                foreach (ExternData ed in panel.ExternDatas)
                {
                    object value = ed.Value;
                    if (null != value && value is ICloneable)
                    {
                        value = ((ICloneable)value).Clone();
                    }
                    ExternDatas.Add(new ExternData(ed.Key, value));
                }
            }
            if (null != panel.Border)
            {
                Border = panel.Border.Clone() as ElementBorder;
                Border.OwnerElement = this;
            }
            Transparent = panel.Transparent;
            ForeColor = panel.ForeColor;
            HasBottomBorder = panel.HasBottomBorder;
            HasLeftBorder = panel.HasLeftBorder;
            HasRightBorder = panel.HasRightBorder;
            HasTopBorder = panel.HasTopBorder;
            Height = panel.Height;
            Width = panel.Width;
            VerticalScale = panel.VerticalScale;
            HorizontalScale = panel.HorizontalScale;
            Location = panel.Location;
            Text = panel.Text;
            RealText = panel.RealText;
            HasBorder = panel.HasBorder;
            AutoSize = panel.AutoSize;
            try
            {
                Font = new Font(panel.Font.FontFamily, panel.Font.Size);
            }
            catch (Exception)
            {

            }
            if (null != panel.SourceField)
            {
                SourceField = panel.SourceField.Clone() as SourceField;
            }
            MoveX = panel.MoveX;
            MoveY = panel.MoveY;
            Name = panel.Name;
            TotalHeight = panel.TotalHeight;
            OrignalHeight = panel.OrignalHeight;
            VisibleExpression = panel.VisibleExpression;
            Visible = panel.Visible;
            MESType = panel.MESType;
            if (null != ((IElement)panel).ExtendObject)
            {
                ((IElement)this).ExtendObject = ((IElement)panel).ExtendObject.Clone() as ExtendObject;
            }
        }

        public override void Print(Canvas ca, float x, float y)
        {
            if (Transparent && this.Site == null)
                return;
            SetBorder();
            MoveX = x;
            MoveY = y;
            Graphics g = ca.Graphics;
            if (null != this.Site)
            {
                if (!string.IsNullOrEmpty(Expression) && Expression.Trim().StartsWith("="))
                {
                    RealText = Expression;
                }
            }
            float left = x;
            float top = y;

            // 上面的计算可能会改变控件的大小，所以将
            // 边框的绘制放在放在这里
            Border.FillAreaBackground(g, x, y);
            Border.Print(ca, x, y);
            if (null != RealText && IsRedrawText)
            {
                SizeF textSize = g.MeasureString(RealText, this.Font);

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
                        g.DrawString(RealText, this.Font, foreBrush, new RectangleF(left, top, tempWidth, tempHeight));
                    }
                }
                finally
                {
                    foreBrush.Dispose();
                }
            }
        }


        public override void DirectDraw(Canvas ca, float x, float y, float dpiZoom)
        {
            if (Transparent && this.Site == null)
                return;
            SetBorder();
            MoveX = x;
            MoveY = y;
            Graphics g = ca.Graphics;
            float left = x;
            float top = y;

            // 上面的计算可能会改变控件的大小，所以将
            // 边框的绘制放在放在这里
            Border.FillAreaBackground(g, x, y);
            Border.DirectDraw(ca, x, y, dpiZoom);
            if (null != RealText && IsRedrawText)
            {
                SizeF textSize = g.MeasureString(RealText, this.Font);

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
                        g.DrawString(RealText, this.Font, foreBrush, new RectangleF(left, top, tempWidth, tempHeight));
                    }
                }
                finally
                {
                    foreBrush.Dispose();
                }
            }
            base.DirectDraw(ca, x, y, dpiZoom);
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
            RealText = expEngin.Execute(Expression, Text, dtm, this as IPMSFormate, this as IDataMapping, bindPath);
            if (newFlag)
                expEngin.Dispose();
            return 0;
        }


        public override object Clone()
        {
            PmsPanel panel = new PmsPanel();
            panel.BackColor = this.BackColor;
            panel.Anchor = this.Anchor;
            panel.Bounds = this.Bounds;
            panel.BorderName = this.BorderName;
            if (null != ExternDatas && ExternDatas.Count > 0)
            {
                if (null == panel.ExternDatas)
                {
                    panel.ExternDatas = new List<ExternData>();
                }
                foreach (ExternData ed in this.ExternDatas)
                {
                    object value = ed.Value;
                    if (null != value && value is ICloneable)
                    {
                        value = ((ICloneable)value).Clone();
                    }
                    panel.ExternDatas.Add(new ExternData(ed.Key, value));
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
            panel.Left = this.Left;
            panel.Top = this.Top;
            panel.Text = this.Text;
            panel.RealText = this.RealText;
            panel.Region = this.Region;
            panel.RightToLeft = this.RightToLeft;
            panel.HasBorder = this.HasBorder;
            panel.OrginalLocation = this.Location;
            panel.AutoSize = AutoSize;
            panel.Visible = Visible;
            panel.MESType = MESType;
            try
            {
                panel.Font = new Font(this.Font.FontFamily, this.Font.Size);
            }
            catch (Exception)
            {

            }
            if (null != this.SourceField)
            {
                panel.SourceField = this.SourceField.Clone() as SourceField;
            }
            panel.Size = this.Size;

            foreach (Control ctrl in this.Controls)
            {
                if (null != ctrl)
                {
                    Type type = ctrl.GetType();
                    if (null != type.GetInterface(typeof(ICloneable).FullName))
                    {
                        panel.Controls.Add(((ICloneable)ctrl).Clone() as Control);
                    }
                }
            }
            panel.MoveX = this.MoveX;
            panel.MoveY = this.MoveY;
            panel.Name = this.Name;
            panel.TotalHeight = this.TotalHeight;
            panel.OrignalHeight = this.OrignalHeight;
            panel.VisibleExpression = VisibleExpression;
            panel.DisplayNullRecord = DisplayNullRecord;
            if (null != Tag)
            {
                if (Tag is ICloneable)
                {
                    panel.Tag = ((ICloneable)Tag).Clone();
                }
                else
                {
                    panel.Tag = Tag;
                }
            }

            if (null != ((IElement)this).ExtendObject)
            {
                ((IElement)panel).ExtendObject = ((IElement)this).ExtendObject.Clone() as ExtendObject;
            }
            return panel;
        }

        private SuspensionItem[] items = new SuspensionItem[4];
        public SuspensionItem[] ListSuspensionItems()
        {
            
            using (Stream stream = Assembly.GetExecutingAssembly().
                                     GetManifestResourceStream(
                                     "PMS.Libraries.ToolControls.Report.Resources.source.png"))
            {
                Image img = Image.FromStream(stream);
                items[0] = new SuspensionItem(img, "数据源", "数据源", SourceBindAction);
                stream.Dispose();
            }
            
            using (Stream stream = Assembly.GetExecutingAssembly().
                                     GetManifestResourceStream(
                                     "PMS.Libraries.ToolControls.Report.Resources.Fx.png"))
            {
                Image img = Image.FromStream(stream);
                items[1] = new SuspensionItem(img, "显示隐藏表达式", "显示隐藏表达式", VisibleExpressionAction);
                stream.Dispose();
            }

            using (Stream stream = Assembly.GetExecutingAssembly().
                                     GetManifestResourceStream(
                                     "PMS.Libraries.ToolControls.Report.Resources.Border.png"))
            {
                Image img = Image.FromStream(stream);
                items[2] = new SuspensionItem(img, "边框", "边框", BorderAction);
                stream.Dispose();
            }

            items[3] = new SuspensionItem(null, this.Name, this.Name, null);

            return items;
        }

        public IControlTranslator ToElement(bool transferChild)
        {
            PmsPanelWrapper panel = new PmsPanelWrapper();
            panel.BackColor = this.BackColor;
            panel.BorderName = this.BorderName;
            if (null != ExternDatas && ExternDatas.Count > 0)
            {
                panel.ExternDatas = new List<ExternData>();

                foreach (ExternData ed in this.ExternDatas)
                {
                    object value = ed.Value;
                    if (null != value && value is ICloneable)
                    {
                        value = ((ICloneable)value).Clone();
                    }
                    panel.ExternDatas.Add(new ExternData(ed.Key, value));
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
            panel.HasBorder = HasBorder;
            panel.Visible = Visible;
            panel.MESType = MESType;
            if (_orginalHeight > 0)
            {
                panel.Height = this._orginalHeight;
            }
            else
            {
                panel.Height = this.Height;
            }
            if (_orginalWidth > 0)
            {
                panel.Width = _orginalWidth;
            }
            else
            {
                panel.Width = this.Width;
            }
            panel.VerticalScale = 1f;
            panel.HorizontalScale = 1f;
            if (_orginalLocation != Point.Empty)
            {
                panel.Location = this._orginalLocation;
            }
            else
            {
                panel.Location = this.Location;
            }
            panel.Text = this.Text;
            panel.RealText = RealText;
            panel.AutoSize = AutoSize;
            try
            {
                if (_orginalFontSize > 0)
                {
                    panel.Font = new Font(Font.FontFamily, _orginalFontSize);
                }
                else
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

            if (transferChild)
            {
                IList<IElement> elements = Elements;
                if (null != elements && elements.Count > 0)
                {
                    foreach (IElement element in elements)
                    {
                        if (element is IElementTranslator)
                        {
                            IElement tmp = ((IElementTranslator)element).ToElement(transferChild) as IElement;
                            if (null != tmp)
                            {
                                panel.Elements.Add(tmp);
                                tmp.Parent = panel;
                            }
                        }
                    }
                }
            }
            if (null != ((IElement)this).ExtendObject)
            {
                ((IElement)panel).ExtendObject = ((IElement)this).ExtendObject.Clone() as ExtendObject;
            }
            return panel;
        }

        private void BorderAction()
        {

            BorderEditorDialog editor = new BorderEditorDialog(Border);
            if (DialogResult.OK == editor.ShowDialog())
            {
                if (null != Border && !Border.Equals(editor.Border))
                {
                    Border = editor.Border;
                    editor.Border.OwnerElement = this as IElement;
                    this.BorderName = editor.Border.Name;
                    ExternData data = new ExternData("BorderName", editor.Name);
                    int index = -1;
                    if (null != this.ExternDatas)
                    {
                        for (int i = 0; i < this.ExternDatas.Count; i++)
                        {
                            if (data.Equals(this.ExternDatas[i]))
                            {
                                this.ExternDatas[i] = data;
                                index = i;
                                break;
                            }
                        }
                        if (index == -1)
                        {
                            this.ExternDatas.Add(data);
                        }
                    }
                    NotifyDesignSurfaceChange();
                }
            }
        }

        private void SourceBindAction()
        {
            IPmsReportDataBind pd = this as IPmsReportDataBind;
            SourceField parent = GetSourceField(this);
            using (SourceBindDialog fbd = new SourceBindDialog(parent, pd.SourceField, true))
            {
                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (null != pd)
                    {
                        if (null != fbd.SourceField)
                        {
                            pd.SourceField = fbd.SourceField.Clone();
                        }
                        else
                        {
                            pd.SourceField = null;
                        }
                        NotifyDesignSurfaceChange();
                    }

                }
            }
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
                           
                            break;
                        case 'D'://
                            SourceBindAction();
                            bProcessed = true;
                            break;
                        case 'F':
                            VisibleExpressionAction();
                            bProcessed = true;
                            break;
                    }
                }
            }
            return bProcessed;
        }
        private void VisibleExpressionAction()
        {
            using (MES.Report.MESReportExpressionEditor expEditor = new MES.Report.MESReportExpressionEditor(VisibleExpression))
            {
                expEditor.ControlName = Name;
                if (expEditor.ShowDialog() == DialogResult.OK)
                {
                    VisibleExpression = expEditor.ExpressionText;
                    NotifyDesignSurfaceChange();
                }
            }
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

        public List<XmlNode> GetDataNodes(XmlDocument xdoc)
        {
            if (null == xdoc)
            {
                return null;
            }

            List<XmlNode> list = new List<XmlNode>();
            string name = string.Empty;
            if (null != ((IElement)this).ExtendObject
                && !string.IsNullOrEmpty(((IElement)this).ExtendObject.Name))
            {
                name = ((IElement)this).ExtendObject.Name;
            }
            else
            {
                name = Name;
            }

            try
            {
                XmlElement element = xdoc.CreateElement(name);
                XmlAttribute xa = xdoc.CreateAttribute("id");
                xa.InnerText = Name;
                element.Attributes.Append(xa);
                if (null != Controls)
                {
                    foreach (Control ctrl in Controls)
                    {
                        if (ctrl is IDataXmlNodes)
                        {
                            List<XmlNode> tmpList = ((IDataXmlNodes)ctrl).GetDataNodes(xdoc);
                            if (null != tmpList)
                            {
                                foreach (XmlNode xn in tmpList)
                                {
                                    element.AppendChild(xn);
                                }
                            }
                        }
                    }
                }
                list.Add(element);
            }
            catch (Exception ex)
            {
                PMSPublicInfo.Message.Error(ex.Message);
            }

            return list;
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
                return GetSourceField(element.Parent as IElement);
            }

            return parent.SourceField;
        }

    }
}
