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
using PMS.Libraries.ToolControls.BarcodeLib;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using PMS.Libraries.ToolControls.Report.Controls.Editor;
using PMS.Libraries.ToolControls.Report.Controls.EditorDialog;
using PMS.Libraries.ToolControls.Report.Controls.TypeConvert;
using PMS.Libraries.ToolControls.Report.Element;
using PMS.Libraries.ToolControls.Report.Elements.Util;
using PMS.Libraries.ToolControls.TwoDCodeLib;
using PMS.Libraries.ToolControls.PMSPublicInfo;

namespace PMS.Libraries.ToolControls.Report
{
    [ToolboxBitmap(typeof(QRCode), "Resources.QRCode.png")]
    [Designer(typeof(MESDesigner))]
    [DefaultProperty("Text")]
    public class QRCode : ElementBase, IExpression, ISuspensionable, IUIDesignExpStruct, IElementTranslator, IDataXmlNodes, IElement, IBindReportExpressionEngine, IProcessCmdKey
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


        private int _QRCodeScale = 4;
        [Category("通用")]
        [Description("控件大小")]
        [Browsable(true)]
        public new Size Size
        {
            get { return base.Size; }
            set 
            {
                int h = value.Height;
                int w = value.Width;
                int scale = Math.Min(h, w);
                scale = scale / 45;
                _QRCodeScale = scale;
                base.Size = value;
                if (DesignMode)
                {
                    Invalidate();
                }
            }
        }

        private bool _hasBorder = true;
        [Category("通用")]
        [Description("是否显示边框")]
        [DisplayName("EnableBorder")]
        [Browsable(false)]
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
        [Browsable(false)]
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
                if (DesignMode)
                {
                    Invalidate();
                }
            }
        }

        private PMS.Libraries.ToolControls.TwoDCodeLib.Codec.QRCodeEncoder.ERROR_CORRECTION _CorrectionLevel = TwoDCodeLib.Codec.QRCodeEncoder.ERROR_CORRECTION.M;
        /// <summary>
        /// 纠错等级
        /// </summary>
        [Browsable(true)]
        [Category("通用")]
        [Description("纠错等级")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(1)]
        public PMS.Libraries.ToolControls.TwoDCodeLib.Codec.QRCodeEncoder.ERROR_CORRECTION CorrectionLevel
        {
            get
            {
                return _CorrectionLevel;
            }
            set
            {
                _CorrectionLevel = value;
                if (DesignMode)
                {
                    Invalidate();
                }
            }
        }

        private PMS.Libraries.ToolControls.TwoDCodeLib.Codec.QRCodeEncoder.ENCODE_MODE _EncodedMode = TwoDCodeLib.Codec.QRCodeEncoder.ENCODE_MODE.BYTE;
        /// <summary>
        /// 编码模式
        /// </summary>
        [Browsable(true)]
        [Category("通用")]
        [Description("编码模式")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(2)]
        public PMS.Libraries.ToolControls.TwoDCodeLib.Codec.QRCodeEncoder.ENCODE_MODE EncodedMode
        {
            get { return _EncodedMode; }
            set { _EncodedMode = value; }
        }

        private int _Version = 7;
        /// <summary>
        /// 编码版本
        /// </summary>
        [Browsable(true)]
        [Category("通用")]
        [Description("编码版本")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(7)]
        [Editor(typeof(PMS.Libraries.ToolControls.TwoDCodeLib.QRCodeControl.VersionSelectEditor), typeof(UITypeEditor))]
        public int Version
        {
            get { return _Version; }
            set { _Version = value; }
        }

        private bool _includeLabel = false;
        [Browsable(false)]
        [Category("通用")]
        [Description("是否包括条形码标签文字说明")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(false)]
        public bool IncludeLabel
        {
            get
            {
                return _includeLabel;
            }
            set
            {
                _includeLabel = value;
                if (DesignMode)
                {
                    Invalidate();
                }
            }
        }

        private string _errorText = "错误条形码";
        [Browsable(true)]
        [Category("通用")]
        [Description("条形码错误时显示文字")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ErrorText
        {
            get
            {
                return _errorText;
            }
            set
            {
                _errorText = value;
                Invalidate();
            }
        }

        private string _expression = string.Empty;
        [Editor(typeof(PMS.Libraries.ToolControls.Report.Controls.Editor.ExpressionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [TypeConverter(typeof(ExpressionConverter))]
        [Browsable(true)]
        [Category("通用")]
        [Description("表达式")]
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
                    if (string.IsNullOrEmpty(value))
                    {
                        RealText = Text;
                    }
                    Invalidate();
                }
            }
        }

        private Color _foreColor = Color.Black;
        [Browsable(true)]
        [Category("通用")]
        [Description("前景色")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override Color ForeColor
        {
            get
            {
                return _foreColor;
            }
            set
            {
                base.ForeColor = _foreColor = value;
                if (DesignMode)
                {
                    Invalidate();
                }
            }
        }

        private Font _labelFont = new Font("宋体", 9, FontStyle.Regular);
        [Browsable(false)]
        [Category("通用")]
        [Description("标签字体")]
        public Font LabelFont
        {
            get
            {
                return _labelFont;
            }
            set
            {
                _labelFont = value;
                if (DesignMode)
                {
                    Refresh();
                }
            }
        }
        #endregion
        /// <summary>
        /// 已经被绑定变量的值
        /// </summary>
        //private IDictionary<string, object> _bindedValues = new Dictionary<string, object>();

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

        private string _borderName = string.Empty;
        /// <summary>
        /// 边框名字
        /// </summary>
        [Browsable(false)]
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
        IReportExpressionEngine IBindReportExpressionEngine.ExpressionEngine
        {
            get;
            set;
        }


        QRCodeControl _QRCodeControl = null;

        public QRCode()
            : base()
        {
            Size = new Size(180, 180);
            Border = null;
            HasBorder = false;
            Text = string.Empty;
        }

        public QRCode(IElement element)
            : base()
        {
            QRCodeWrapper bcw = element as QRCodeWrapper;
            Name = bcw.Name;
            AutoSize = bcw.AutoSize;
            BackColor = bcw.BackColor;
            if (null != bcw.BackgroundImage)
            {
                BackgroundImage = bcw.BackgroundImage.Clone() as Image;
            }
            BackgroundImageLayout = bcw.BackgroundImageLayout;
            if (null != bcw.Border)
            {
                Border = bcw.Border.Clone() as ElementBorder;
                Border.OwnerElement = this;
            }
            BorderName = bcw.BorderName;
            if (null != bcw.ExternDatas)
            {
                ExternDatas = new List<ExternData>();
                foreach (ExternData ed in bcw.ExternDatas)
                {
                    object value = ed.Value;
                    if (null != value && value is ICloneable)
                    {
                        value = ((ICloneable)value).Clone();
                    }
                    ExternDatas.Add(new ExternData(ed.Key, value));
                }
            }
            if (null != bcw.Font)
            {
                Font = bcw.Font.Clone() as Font;
            }
            HasBorder = bcw.HasBorder;
            HasBottomBorder = bcw.HasBottomBorder;
            HasLeftBorder = bcw.HasLeftBorder;
            HasRightBorder = bcw.HasRightBorder;
            HasTopBorder = bcw.HasTopBorder;
            Height = bcw.Height;
            HorizontalScale = bcw.HorizontalScale;
            Location = bcw.Location;
            MESType = bcw.MESType;
            MoveX = bcw.MoveX;
            MoveY = bcw.MoveY;
            //bcw.Parent = Parent;
            Text = bcw.Text;
            VerticalScale = bcw.VerticalScale;
            Width = bcw.Width;
            if (null != bcw.SourceField)
            {
                SourceField = bcw.SourceField.Clone();
            }
            EnableMapping = bcw.EnableMapping;
            MappingTable = bcw.MappingTable;
            Expression = bcw.Expression;
            RealText = bcw.RealText;
            IncludeLabel = bcw.IncludeLabel;
            CorrectionLevel = bcw.CorrectionLevel;
            Visible = bcw.Visible;
            MESType = bcw.MESType;
            if (null != ((IElement)bcw).ExtendObject)
            {
                ((IElement)this).ExtendObject = ((IElement)bcw).ExtendObject.Clone() as ExtendObject;
            }
        }

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="ca">画布</param>
        /// <param name="x">x偏移</param>
        /// <param name="y">y偏移</param>
        public override void Print(Canvas ca, float x, float y)
        {
            SetBorder();
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
            
            try
            {
                if (null == _QRCodeControl)
                    _QRCodeControl = new QRCodeControl();
                _QRCodeControl.IncludeLabel = IncludeLabel;
                _QRCodeControl.backColor = BackColor;
                _QRCodeControl.foreColor = ForeColor;
                _QRCodeControl.CorrectionLevel = CorrectionLevel;
                _QRCodeControl.EncodedMode = EncodedMode;
                _QRCodeControl.QRCodeScale = _QRCodeScale;
                _QRCodeControl.Version = Version;
                _QRCodeControl.RawData = RealText;
                Image img = _QRCodeControl.PictureBoxImage;
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
            finally
            {
                if (null != _QRCodeControl)
                {
                    _QRCodeControl.Dispose();
                    _QRCodeControl = null;
                }
            }
            if (null != Border)
            {
                Border.Print(ca, x, y);
            }

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
            
            try
            {
                if (null == _QRCodeControl)
                {
                    _QRCodeControl = new QRCodeControl();
                    _QRCodeControl.IncludeLabel = IncludeLabel;
                    _QRCodeControl.backColor = BackColor;
                    _QRCodeControl.foreColor = ForeColor;
                    _QRCodeControl.CorrectionLevel = CorrectionLevel;
                    _QRCodeControl.EncodedMode = EncodedMode;
                    _QRCodeControl.QRCodeScale = _QRCodeScale;
                    _QRCodeControl.Version = Version;
                    _QRCodeControl.RawData = RealText;
                }
                Image img = _QRCodeControl.PictureBoxImage;
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
            finally
            {
                if (null != _QRCodeControl)
                {
                    _QRCodeControl.Dispose();
                    _QRCodeControl = null;
                }
            }
            if (null != Border)
            {
                Border.DirectDraw(ca, x, y, dpiZoom);
            }
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

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            QRCode bc = new QRCode();
            bc.ForeColor = ForeColor;
            bc.BackColor = this.BackColor;
            bc.EncodedMode = this.EncodedMode;
            bc.CorrectionLevel = this.CorrectionLevel;
            if (null != Border)
            {
                bc.Border = Border.Clone() as ElementBorder;
                bc.Border.OwnerElement = bc;
            }
            if (null != ExternDatas && ExternDatas.Count > 0)
            {
                if (null == bc.ExternDatas)
                {
                    bc.ExternDatas = new List<ExternData>();
                }
                foreach (ExternData ed in this.ExternDatas)
                {
                    object value = ed.Value;
                    if (null != value && value is ICloneable)
                    {
                        value = ((ICloneable)value).Clone();
                    }
                    bc.ExternDatas.Add(new ExternData(ed.Key, value));
                }
            }
            bc.HasBottomBorder = this.HasBottomBorder;
            bc.HasLeftBorder = this.HasLeftBorder;
            bc.HasRightBorder = this.HasRightBorder;
            bc.HasTopBorder = this.HasTopBorder;
            if (_orginalHeight > 0)
            {
                bc.Height = this._orginalHeight;
            }
            else
            {
                bc.Height = this.Height;
            }
            if (_orginalWidth > 0)
            {
                bc.Width = _orginalWidth;
            }
            else
            {
                bc.Width = this.Width;
            }
            //edit.VerticalScale = this.VerticalScale;
            //edit.HorizontalScale = this.HorizontalScale;
            if (_orginalLocation != Point.Empty)
            {
                bc.Location = this._orginalLocation;
            }
            else
            {
                bc.Location = this.Location;
            }
            bc.Left = this.Left;
            bc.Top = this.Top;
            bc.Text = this.Text;
            bc.Region = this.Region;
            bc.RightToLeft = this.RightToLeft;
            bc.HasBorder = this.HasBorder;
            bc.RealText = this.RealText;
            bc.IncludeLabel = this.IncludeLabel;
            if (null != this.SourceField)
            {
                bc.SourceField = this.SourceField.Clone() as SourceField;
            }
            if (null != Expression)
            {
                bc.Expression = Expression.Clone() as string;
            }
            bc.Size = this.Size;
            bc.MoveX = this.MoveX;
            bc.MoveY = this.MoveY;
            bc.Name = this.Name;
            bc.VerticalScale = 1f;
            bc.HorizontalScale = 1f;
            bc.Visible = Visible;
            bc.MESType = MESType;
            if (_orginalFontSize > 0)
            {
                bc.Font = new Font(Font.FontFamily, _orginalFontSize);
            }
            else
            {
                bc.Font = new Font(this.Font.FontFamily, this.Font.Size);
            }
            if (null != Tag)
            {
                if (Tag is ICloneable)
                {
                    bc.Tag = ((ICloneable)Tag).Clone();
                }
                else
                {
                    bc.Tag = Tag;
                }
            }
            if (null != ((IElement)this).ExtendObject)
            {
                bc.Tag = ((ICloneable)((IElement)this).ExtendObject).Clone();
            }
            return bc;
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

        public IControlTranslator ToElement(bool transferChild)
        {
            QRCodeWrapper bcw = new QRCodeWrapper();
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
            bcw.Font = Font.Clone() as Font;
            bcw.HasBorder = HasBorder;
            bcw.HasBottomBorder = HasBottomBorder;
            bcw.HasLeftBorder = HasLeftBorder;
            bcw.HasRightBorder = HasRightBorder;
            bcw.HasTopBorder = HasTopBorder;
            // bcw.Height = Height;
            bcw.HorizontalScale = 1f;
            //bcw.Location = Location;
            bcw.MESType = MESType;
            bcw.MoveX = MoveX;
            bcw.MoveY = MoveY;
            //bcw.Parent = Parent;
            bcw.Text = Text;
            bcw.VerticalScale = 1f;
            bcw.Visible = Visible;
            bcw.MESType = MESType;
            //bcw.Width = Width;
            if (null != SourceField)
            {
                bcw.SourceField = SourceField.Clone();
            }

            if (_orginalHeight > 0)
            {
                bcw.Height = this._orginalHeight;
            }
            else
            {
                bcw.Height = this.Height;
            }
            if (_orginalWidth > 0)
            {
                bcw.Width = _orginalWidth;
            }
            else
            {
                bcw.Width = this.Width;
            }
            //label.VerticalScale = this.VerticalScale;
            //label.HorizontalScale = this.HorizontalScale;
            if (_orginalLocation != Point.Empty)
            {
                bcw.Location = this._orginalLocation;
            }
            else
            {
                bcw.Location = this.Location;
            }

            bcw.EnableMapping = EnableMapping;
            bcw.MappingTable = MappingTable;
            bcw.Expression = Expression;
            bcw.RealText = RealText;
            bcw.IncludeLabel = IncludeLabel;
            bcw.CorrectionLevel = CorrectionLevel;
            bcw.EncodedMode = EncodedMode;

            if (null != ((IElement)this).ExtendObject)
            {
                bcw.ExtendObject = ((IElement)this).ExtendObject.Clone() as ExtendObject;
            }

            return bcw;
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
                    name = ((IElement)this).ExtendObject.Name.ToString();
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
