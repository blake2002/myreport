using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using MES.Controls.Design;
using MES.PublicInterface;
using MES.Report;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using PMS.Libraries.ToolControls.Report.Controls.Editor;
using PMS.Libraries.ToolControls.Report.Controls.EditorDialog;
using PMS.Libraries.ToolControls.Report.Controls.TypeConvert;
using PMS.Libraries.ToolControls.Report.Element;
using PMS.Libraries.ToolControls.Report.Elements.Util;
using PMS.Libraries.ToolControls.PMSPublicInfo;
using PMS.Libraries.ToolControls.Report.Controls;
using System.Drawing.Drawing2D;

namespace PMS.Libraries.ToolControls.Report
{
    [Serializable]
    [DisplayName("Label")]
    [ToolboxBitmap(typeof(PmsLabel), "Resources.Label.png")]
    [Designer(typeof(MESDesignerWithRuntimeAppearance))]
    [DefaultProperty("Text")]
    public class PmsLabel : ElementBase, IPMSFormate, IExpression, IBindReportExpressionEngine, ISuspensionable, IDataMapping, IUIDesignExpStruct, IElementTranslator, IElement, IDataXmlNodes, IDesignEdit, IProcessCmdKey
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

        private ContentAlignment _textAlign = ContentAlignment.MiddleLeft;
        [Category("通用")]
        [Browsable(true)]
        [Description("文本位置")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(ContentAlignment.MiddleLeft)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(true)]
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
        [Category("通用")]
        [Description("格式化")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string StrFormate
        {
            get;
            set;
        }

        [Category("通用")]
        [Browsable(true)]
        [Description("是否启用映射")]
        [DefaultValue(false)]
        public override bool EnableMapping
        {
            get;
            set;
        }

        [Category("通用")]
        [Description("映射表")]
        [Editor(typeof(DataMappingEditor), typeof(UITypeEditor))]
        [Browsable(true)]
        public override string MappingTable
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

        [Category("通用")]
        [Description("前景颜色")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }
        [Category("通用")]
        [Description("字体")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
            }
        }

        [Category("通用")]
        [DisplayName("Binding")]
        [Description("绑定数据")]
        [Browsable(true)]
        [Editor(typeof(TextEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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

        #endregion
        ///// <summary>
        ///// 已经被绑定变量的值
        ///// </summary>
        //private IDictionary<string, object> _bindedValues = new Dictionary<string, object>();
        protected SourceField _sourceField = null;
        /// <summary>
        /// 数据源
        /// </summary>
        [Editor(typeof(SourceEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(false)]
        [Category("通用")]
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

        private float _rotateDegree = 0;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        [Category("通用")]
        [Description("顺时针旋转角度°,目前仅当TextAlign为MiddleCenter时有效")]
        public float RotateDegree
        {
            get
            {
                return _rotateDegree;
            }
            set
            {
                _rotateDegree = value;
                if (this.Site != null)
                    Invalidate();
            }
        }

        [NonSerialized]
        private ISelectionService _selectionService = null;
        [NonSerialized]
        private EventHandler _selectHandler = null;

        [NonSerialized]
        private TextBox _textBox = null;

        [Browsable(false)]
        IReportExpressionEngine IBindReportExpressionEngine.ExpressionEngine
        {
            get;
            set;
        }

        private string _borderName = new RectangleBorder(null).Name;
        public override string BorderName
        {
            get
            {
                return _borderName;
            }
            set
            {
                _borderName = value;
            }
        }

        [Browsable(false)]
        public TreeNode ExpStructNode
        {
            get
            {
                TreeNode node = new TreeNode();
                node.Text = Name;
                node.Name = Name;
                node.Tag = MESType;
                return node;
            }
        }

        ExtendObject IElement.ExtendObject
        {
            get;
            set;
        }

        [Browsable(false)]
        public bool IsEdit
        {
            get;
            private set;
        }

        public void EndEdit()
        {
            if (null != _textBox)
            {
                Text = _textBox.Text;
                try
                {
                    NotifyDesignSurfaceChange();
                    RemoveTextbox();
                }
                finally
                {
                    _textBox.Dispose();
                    _textBox = null;
                    IsEdit = false;
                }
            }
        }
        public new void Dispose()
        {
          // Dispose(true);
//             if (this.Font != null)
//             {
//                                      this.Font.Dispose();
//                                    this.Font = null;
//             }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.Font != null)
                {
                  // this.Font.Dispose();
                  // this.Font = null;
                }
            }
            base.Dispose(disposing);
        }
        public void CancleEdit()
        {
            if (null != _textBox)
            {
                try
                {
                    RemoveTextbox();
                }
                finally
                {
                    _textBox.Dispose();
                    _textBox = null;
                    IsEdit = false;
                }
            }
        }

        private HorizontalAlignment TransformTextAlign()
        {
            switch (this.TextAlign)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.BottomLeft:
                    return HorizontalAlignment.Left;
                case ContentAlignment.TopCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.BottomCenter:
                    return HorizontalAlignment.Center;
                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                    return HorizontalAlignment.Right;
            }
            return HorizontalAlignment.Left;
        }

        public void BeginEdit()
        {
            if (null == _textBox)
            {
                _selectionService = Site.GetService(typeof(ISelectionService)) as ISelectionService;
                if (null == _selectHandler)
                {
                    _selectHandler = new EventHandler(SelectionService_SelectionChanged);
                }
                if (null != _selectionService)
                {
                    _selectionService.SelectionChanged += _selectHandler;
                }
                _textBox = new TextBox();
                _textBox.Width = Width;
                _textBox.Height = Height;
                _textBox.Multiline = true;
                if (BackColor != Color.Transparent)
                    _textBox.BackColor = BackColor;
                if (ForeColor != Color.Transparent)
                    _textBox.ForeColor = ForeColor;
                _textBox.Font = Font.Clone() as Font;
                _textBox.TextAlign = TransformTextAlign();
                _textBox.Location = new Point(0, 0);
                _textBox.KeyDown += TextBox_KeyDown;
                _textBox.Text = Text;
                MESDesignerService ms = Site.
                        GetService(typeof(MESDesignerService)) as MESDesignerService;
                if (null != ms)
                {
                    ms.CreateControl(_textBox);
                }
                IsEdit = true;
            }
        }

        public PmsLabel()
            : base()
        {
            Size = new Size(100, 23);
        }

        public PmsLabel(IElement element)
            : base()
        {
            PmsLabelWrapper label = element as PmsLabelWrapper;
            BackColor = label.BackColor;
            StrFormate = label.StrFormate;
            BorderName = label.BorderName;
            if (null != label.ExternDatas && label.ExternDatas.Count > 0)
            {
                if (null == ExternDatas)
                {
                    ExternDatas = new List<ExternData>();
                }
                foreach (ExternData ed in label.ExternDatas)
                {
                    object value = ed.Value;
                    if (null != value && value is ICloneable)
                    {
                        value = ((ICloneable)value).Clone();
                    }
                    ExternDatas.Add(new ExternData(ed.Key, value));
                }
            }
            if (null != this.Border)
            {
                Border = label.Border.Clone() as ElementBorder;
                Border.OwnerElement = this;
            }
            ForeColor = label.ForeColor;
            HasBottomBorder = label.HasBottomBorder;
            HasLeftBorder = label.HasLeftBorder;
            HasRightBorder = label.HasRightBorder;
            HasTopBorder = label.HasTopBorder;
            HasBorder = label.HasBorder;
            Height = label.Height;
            Width = label.Width;
            VerticalScale = label.VerticalScale;
            HorizontalScale = label.HorizontalScale;
            Location = label.Location;
            HasBorder = label.HasBorder;
            Text = label.Text;
            RealText = label.RealText;
            if (null != label.SourceField)
            {
                SourceField = label.SourceField.Clone() as SourceField;
            }
            MoveX = label.MoveX;
            MoveY = label.MoveY;
            Name = label.Name;
            TextAlign = label.TextAlign;
            Expression = label.Expression;
            EnableMapping = label.EnableMapping;
            MappingTable = label.MappingTable;
            Font = new Font(label.Font.FontFamily, label.Font.Size, label.Font.Style);
            Visible = label.Visible;
            MESType = label.MESType;
            if (null != ((IElement)label).ExtendObject)
            {
                ((IElement)this).ExtendObject = ((IElement)label).ExtendObject.Clone() as ExtendObject;
            }
        }

        public string FormateToString(object obj)
        {
            return FormateUtil.Formate(StrFormate, obj);
        }

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="ca"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public override void Print(Canvas ca, float x, float y)
        {
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

            if (null == this.Site && EnableMapping)
            {
                string result = GetMapValue(RealText);
                if (null != result)
                {
                    RealText = result;
                }
            }
            StringFormat sf = GetStringFormat();
            SizeF textSize = SizeF.Empty;
            if (null != RealText)
            {
                textSize = g.MeasureString(RealText, this.Font, (int)(this.Width - Border.BorderWidth), sf);
            }
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
            float tempWidth = this.Width - Border.BorderWidth;
            float tempHeight = this.Height - Border.BorderWidth;
            if (null != RealText && tempWidth != 0 && tempHeight != 0)
            {
                try
                {
                    //GraphicsContainer container = g.BeginContainer();

                    System.Drawing.Drawing2D.GraphicsState state = g.Save();

                    RectangleF rect = new RectangleF(left, top, tempWidth, tempHeight);

                    Matrix mx = new Matrix();

                    if (RotateDegree != 0)
                    {
                        mx.RotateAt(RotateDegree, new PointF(rect.X + rect.Width / 2, rect.Y + rect.Height / 2));
                        //mx.Translate((float)(Math.Sin(RotateDegree * Math.PI / 180) * textSize.Width), 0);
                        //left += (int)(textSize.Height / 2);

                        //mx.RotateAt(RotateDegree, new PointF(x, y));
                        //if (Math.Sin(RotateDegree * Math.PI / 180) < 0)
                        //    mx.Translate((float)(Math.Sin(RotateDegree * Math.PI / 180) * textSize.Width),0 /*(float)Math.Cos(RotateDegree * Math.PI / 180) * textSize.Width*/ );
                        mx.Multiply(g.Transform, MatrixOrder.Append);
                        g.Transform = mx;
                    }

                    g.DrawString(RealText, this.Font, foreBrush, rect, sf);
                    mx.Reset();
                    mx.Dispose();

                    g.Restore(state);
                    //g.EndContainer(container);
                    //g.DrawString(RealText, this.Font, foreBrush, new RectangleF(left, top, tempWidth, tempHeight), sf);
                }
                finally
                {
                    sf.Dispose();
                }
            }
            foreBrush.Dispose();
        }

        /// <summary>
        /// 直接绘制
        /// </summary>
        /// <param name="ca"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public override void DirectDraw(Canvas ca, float x, float y, float dpiZoom)
        {
            SetBorder();
            MoveX = x;
            MoveY = y;
            Graphics g = ca.Graphics;

            SizeF textSize = SizeF.Empty;
            if (null != RealText)
            {
                textSize = g.MeasureString(RealText, this.Font);
            }
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
                Border.DirectDraw(ca, x, y, dpiZoom);
                left += Border.BorderWidth;
                top += Border.BorderWidth;
            }
            Brush foreBrush = new SolidBrush(this.ForeColor);
            float tempWidth = this.Width - Border.BorderWidth;
            float tempHeight = this.Height - Border.BorderWidth;
            if (null != RealText && tempWidth != 0 && tempHeight != 0)
            {
                StringFormat sf = GetStringFormat();
                try
                {
                    //GraphicsContainer container = g.BeginContainer();
                   
                    System.Drawing.Drawing2D.GraphicsState state = g.Save();

                    RectangleF rect = new RectangleF(left, top, tempWidth, tempHeight);
                    
                    Matrix mx = new Matrix();

                    if (RotateDegree != 0)
                    {
                        mx.RotateAt(RotateDegree, new PointF(rect.X + rect.Width / 2, rect.Y + rect.Height / 2));
                        //mx.Translate(textSize.Height, textSize.Width / 2);
                        mx.Multiply(g.Transform, MatrixOrder.Append);
                        g.Transform = mx;
                    }
                    
                    g.DrawString(RealText, this.Font, foreBrush, rect/*new RectangleF(0, 0, tempWidth, tempHeight)*/, sf);
                    mx.Reset();
                    mx.Dispose();

                    g.Restore(state);
                    //g.EndContainer(container);
                }
                finally
                {
                    sf.Dispose();
                }
            }
            foreBrush.Dispose();
        }

        /// <summary>
        /// 字段绑定值
        /// </summary>
        /// <param name="values"></param>
        public override void BindValue(IDictionary<string, object> values)
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
            //object oValue = null;
            //if (!string.IsNullOrEmpty(Expression) && Expression.StartsWith("="))
            //{
            //    // 启用表达式
            //    oValue = expEngin.Excute(Expression.TrimStart("=".ToCharArray()), dtm, bindPath);
            //}
            //else if (!string.IsNullOrEmpty(Text))
            //{
            //    // 替换显示
            //    oValue = expEngin.ExcuteBindText(Text, dtm, bindPath);
            //}
            //// format
            //if (null != oValue)
            //{
            //    RealText = oValue.ToString();
            //    if (!string.IsNullOrEmpty(StrFormate))
            //    {
            //        RealText = FormateToString(oValue);
            //    }
            //}
            ////mapping
            //if (EnableMapping)
            //{
            //    string result = GetMapValue(RealText);
            //    if (null != result)
            //    {
            //        RealText = result;
            //    }
            //}
            if (newFlag)
                expEngin.Dispose();
            return 0;
        }

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            PmsLabel label = new PmsLabel();
            label.BackColor = this.BackColor;
            label.StrFormate = StrFormate;
            label.Anchor = this.Anchor;
            label.Bounds = this.Bounds;
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
            label.Left = this.Left;
            label.Top = this.Top;
            label.Text = this.Text;
            label.Region = this.Region;
            label.RightToLeft = this.RightToLeft;
            label.HasBorder = this.HasBorder;
            //label.Site = this.Site;
            label.RealText = this.RealText;
            if (null != this.SourceField)
            {
                label.SourceField = this.SourceField.Clone() as SourceField;
            }
            label.Size = this.Size;
            label.MoveX = this.MoveX;
            label.MoveY = this.MoveY;
            label.Name = this.Name;
            label.TextAlign = this.TextAlign;
            if (null != Expression)
            {
                label.Expression = Expression.Clone() as string;
            }
            label.EnableMapping = EnableMapping;
            label.MappingTable = MappingTable;
            label.Font = new Font(this.Font.FontFamily, this.Font.Size, this.Font.Style, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
            label.Visible = Visible;
            label.MESType = MESType;
            label.RotateDegree = RotateDegree;
            //for (int i = 0; i < _bindedValues.Values.Count; i++)
            //{
            //    label._bindedValues[_bindedValues.Keys.ElementAt(i)] = _bindedValues.Values.ElementAt(i).ToString();
            //}
            if (null != ((IElement)this).ExtendObject)
            {
                ((IElement)label).ExtendObject = ((IElement)this).ExtendObject.Clone() as ExtendObject;
            }


            if (null != Tag)
            {
                if (Tag is ICloneable)
                {
                    label.Tag = ((ICloneable)Tag).Clone();
                }
                else
                {
                    label.Tag = Tag;
                }
            }

            return label;
        }


        protected override void OnDoubleClick(EventArgs e)
        {
            if (null != Site)
            {
                BeginEdit();
            }
            base.OnDoubleClick(e);
        }

        private void SelectionService_SelectionChanged(object sender, EventArgs e)
        {
            if (!object.ReferenceEquals(_selectionService.PrimarySelection, this))
            {
                if (null != _textBox)
                {
                    Text = _textBox.Text;
                    try
                    {
                        RemoveTextbox();
                    }
                    finally
                    {
                        _textBox.Dispose();
                        _textBox = null;
                    }
                    _selectionService.SelectionChanged -= _selectHandler;
                    NotifyDesignSurfaceChange();
                }
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                //先触发LoseFocus
                EndEdit();
            }
            else if (e.KeyCode == Keys.Enter && e.Shift)
            {
                _textBox.AppendText("\n");
            }
            else if (e.KeyCode == Keys.Escape)
            {
                CancleEdit();
            }
        }

        private void RemoveTextbox()
        {
            if (null != Site && null != _textBox)
            {
                MESDesignerService ms = Site.
                              GetService(typeof(MESDesignerService)) as MESDesignerService;
                if (null != ms)
                {
                    ms.RemoveControl(_textBox);
                }

            }
        }

        /// <summary>
        /// 获取字符串对齐格式
        /// </summary>
        /// <returns></returns>
        protected StringFormat GetStringFormat()
        {
            StringFormat sf = new StringFormat(StringFormat.GenericTypographic);
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

        public SuspensionItem[] ListSuspensionItems()
        {
            SuspensionItem[] items = new SuspensionItem[14];
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

            using (Stream stream = Assembly.GetExecutingAssembly().
                                    GetManifestResourceStream(
                                    "PMS.Libraries.ToolControls.Report.Resources.Border.png"))
            {
                Image img = Image.FromStream(stream);
                items[2] = new SuspensionItem(img, "边框", "边框", BorderAction);
                stream.Dispose();
            }

            using (Stream stream = Assembly.GetExecutingAssembly().
            GetManifestResourceStream("PMS.Libraries.ToolControls.Report.Resources.Font.png"))
            {
                Image img = Image.FromStream(stream);
                items[3] = new SuspensionItem(img, "字体", "字体", FontAction);
                stream.Dispose();
            }

            using (Stream stream = Assembly.GetExecutingAssembly().
            GetManifestResourceStream("PMS.Libraries.ToolControls.Report.Resources.MapTable.png"))
            {
                Image img = Image.FromStream(stream);
                items[4] = new SuspensionItem(img, "值映射", "值映射", MappingAction);
                stream.Dispose();
            }

            using (Stream stream = Assembly.GetExecutingAssembly().
               GetManifestResourceStream("PMS.Libraries.ToolControls.Report.Resources.TopLeft.png"))
            {
                Image img = Image.FromStream(stream);
                items[5] = new SuspensionItem(img, "左上对齐", "左上对齐", TopLeftAction);
                stream.Dispose();
            }

            using (Stream stream = Assembly.GetExecutingAssembly().
              GetManifestResourceStream("PMS.Libraries.ToolControls.Report.Resources.TopCenter.png"))
            {
                Image img = Image.FromStream(stream);
                items[6] = new SuspensionItem(img, "上中对齐", "上中对齐", TopCenterAction);
                stream.Dispose();
            }

            using (Stream stream = Assembly.GetExecutingAssembly().
             GetManifestResourceStream("PMS.Libraries.ToolControls.Report.Resources.TopRight.png"))
            {
                Image img = Image.FromStream(stream);
                items[7] = new SuspensionItem(img, "右上对齐", "右上对齐", TopRightCenterAction);
                stream.Dispose();
            }

            using (Stream stream = Assembly.GetExecutingAssembly().
             GetManifestResourceStream("PMS.Libraries.ToolControls.Report.Resources.MidLeft.png"))
            {
                Image img = Image.FromStream(stream);
                items[8] = new SuspensionItem(img, "左中对齐", "左中对齐", MiddleLeftAction);
                stream.Dispose();
            }


            using (Stream stream = Assembly.GetExecutingAssembly().
             GetManifestResourceStream("PMS.Libraries.ToolControls.Report.Resources.MidCenter.png"))
            {
                Image img = Image.FromStream(stream);
                items[9] = new SuspensionItem(img, "居中对齐", "居中对齐", MiddleCenterAction);
                stream.Dispose();
            }

            using (Stream stream = Assembly.GetExecutingAssembly().
            GetManifestResourceStream("PMS.Libraries.ToolControls.Report.Resources.MidRight.png"))
            {
                Image img = Image.FromStream(stream);
                items[10] = new SuspensionItem(img, "右中对齐", "右中对齐", MiddleRightAction);
                stream.Dispose();
            }

            using (Stream stream = Assembly.GetExecutingAssembly().
            GetManifestResourceStream("PMS.Libraries.ToolControls.Report.Resources.BottomLeft.png"))
            {
                Image img = Image.FromStream(stream);
                items[11] = new SuspensionItem(img, "左下对齐", "左下对齐", BottomLeftAction);
                stream.Dispose();
            }

            using (Stream stream = Assembly.GetExecutingAssembly().
            GetManifestResourceStream("PMS.Libraries.ToolControls.Report.Resources.BottomCenter.png"))
            {
                Image img = Image.FromStream(stream);
                items[12] = new SuspensionItem(img, "下中对齐", "下中对齐", BottomCenterAction);
                stream.Dispose();
            }

            using (Stream stream = Assembly.GetExecutingAssembly().
            GetManifestResourceStream("PMS.Libraries.ToolControls.Report.Resources.BottomRight.png"))
            {
                Image img = Image.FromStream(stream);
                items[13] = new SuspensionItem(img, "右下对齐", "右下对齐", BottomRightAction);
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


        public IControlTranslator ToElement(bool transferChild)
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
                    label.ExternDatas.Add(ed);
                }
            }
            if (null != this.Border)
            {
                label.Border = Border.Clone() as ElementBorder;
                label.Border.OwnerElement = label;
            }
            label.Text = Text;
            label.ForeColor = this.ForeColor;
            label.HasBottomBorder = this.HasBottomBorder;
            label.HasLeftBorder = this.HasLeftBorder;
            label.HasRightBorder = this.HasRightBorder;
            label.HasTopBorder = this.HasTopBorder;
            label.HasBorder = HasBorder;
            label.Visible = Visible;
            label.MESType = MESType;
            if (_orginalHeight > 0)
            {
                label.Height = this._orginalHeight;
            }
            else
            {
                label.Height = this.Height;
            }
            if (_orginalWidth > 0)
            {
                label.Width = _orginalWidth;
            }
            else
            {
                label.Width = this.Width;
            }
            //label.VerticalScale = this.VerticalScale;
            //label.HorizontalScale = this.HorizontalScale;
            if (_orginalLocation != Point.Empty)
            {
                label.Location = this._orginalLocation;
            }
            else
            {
                label.Location = this.Location;
            }
            label.VerticalScale = 1f;
            label.HorizontalScale = 1f;
            label.MoveX = this.MoveX;
            label.MoveY = this.MoveY;
            label.Name = this.Name;
            label.TextAlign = this.TextAlign;
            label.Expression = Expression;
            label.EnableMapping = EnableMapping;
            label.MappingTable = MappingTable;
            if (_orginalFontSize > 0)
            {
                label.Font = new Font(Font.FontFamily, _orginalFontSize);
            }
            else
            {
                label.Font = new Font(this.Font.FontFamily, this.Font.Size);
            }

            if (EnableMapping)
            {
                label.EnableMapping = false;
                if (!string.IsNullOrEmpty(RealText))
                {
                    label.RealText = GetMapValue(RealText);
                }
            }

            if (null != ((IElement)this).ExtendObject)
            {
                ((IElement)label).ExtendObject = ((IElement)this).ExtendObject.Clone() as ExtendObject;
            }

            label.RealText = RealText;
            return label;
        }

        public List<XmlNode> GetDataNodes(XmlDocument xdoc)
        {
            if (null == xdoc)
            {
                return null;
            }

            List<XmlNode> list = new List<XmlNode>();
            try
            {
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
                XmlElement element = xdoc.CreateElement(name);
                //element.Attributes.Append(
                XmlAttribute xa = xdoc.CreateAttribute("id");
                xa.InnerText = Name;
                element.Attributes.Append(xa);
                element.InnerText = RealText;
                list.Add(element);
            }
            catch (Exception ex)
            {
                PMSPublicInfo.Message.Error(ex.Message);
            }
            return list;
        }

        private void TopLeftAction()
        {
            TextAlign = ContentAlignment.TopLeft;
            NotifyDesignSurfaceChange();
        }

        private void TopCenterAction()
        {
            TextAlign = ContentAlignment.TopCenter;
            NotifyDesignSurfaceChange();
        }

        private void TopRightCenterAction()
        {
            TextAlign = ContentAlignment.TopRight;
            NotifyDesignSurfaceChange();
        }

        private void MiddleLeftAction()
        {
            TextAlign = ContentAlignment.MiddleLeft;
            NotifyDesignSurfaceChange();
        }

        private void MiddleCenterAction()
        {
            TextAlign = ContentAlignment.MiddleCenter;
        }

        private void MiddleRightAction()
        {
            TextAlign = ContentAlignment.MiddleRight;
            NotifyDesignSurfaceChange();
        }

        private void BottomLeftAction()
        {
            TextAlign = ContentAlignment.BottomLeft;
            NotifyDesignSurfaceChange();
        }

        private void BottomCenterAction()
        {
            TextAlign = ContentAlignment.BottomCenter;
            NotifyDesignSurfaceChange();
        }

        private void BottomRightAction()
        {
            TextAlign = ContentAlignment.BottomRight;
            NotifyDesignSurfaceChange();
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

        private void FieldBindAction()
        {
            SourceField sf = GetSourceField(this);
            using (FieldBindDialog fbd = new FieldBindDialog(sf, null != Text ? Text : null))
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
                    Text = fbd.Value;
                    NotifyDesignSurfaceChange();
                }
            }
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
                    ExternData data = new ExternData("BorderName", editor.Border.Name);

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

        private void MappingAction()
        {
            using (MappingFrm frm = new MappingFrm())
            {
                frm.Bind(this);
                if (DialogResult.OK == frm.ShowDialog())
                {
                    this.EnableMapping = frm.EnableMapping;
                    this.MappingTable = frm.TableName;
                    NotifyDesignSurfaceChange();
                }
            }
        }

        private void FontAction()
        {
            FontDialog ShowFontDialog = new FontDialog();
            ShowFontDialog.ShowColor = false;
            ShowFontDialog.Font = this.Font;
            DialogResult resulttemp = ShowFontDialog.ShowDialog(this);
            if (resulttemp == DialogResult.OK)
            {
                this.Font = ShowFontDialog.Font;
                NotifyDesignSurfaceChange();
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
