using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMS.Libraries.ToolControls.Report.Element;
using PMS.Libraries.ToolControls.Report.Win32;
using System.Drawing;
using System.ComponentModel;
using PMS.Libraries.ToolControls.Report.Elements.Util;
using System.Reflection;
using PMS.Libraries.ToolControls.Report.Controls.Editor;
using System.Drawing.Design;
using PMS.Libraries.ToolControls.Report.Controls.TypeConvert;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using PMS.Libraries.ToolControls.Report.Controls.EditorDialog;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using MES.PublicInterface;
using System.Xml;

namespace PMS.Libraries.ToolControls.Report
{
    [ToolboxBitmap(typeof(PmsEdit), "Resources.Edit.png")]
    [DisplayName("Edit")]
    public class PmsEdit : TextBox, IResizable, IPrintable, IBindField, ICloneable, IElement, IDataMapping, IUIDesignExpStruct, IElementTranslator, IDataXmlNodes
    {
        protected int _orginalWidth = -1;
        protected int _orginalHeight = -1;
        protected Point _orginalLocation = Point.Empty;
        protected float _orginalFontSize = -1f;
        protected string _orginalFontName = string.Empty;
        
        /// <summary>
        /// 横向比例因子
        /// </summary>
       [Browsable(false)]
        public float HorizontalScale { get; set; }

        /// <summary>
        /// 纵向比例因子
        /// </summary>
        [Browsable(false)]
        public float VerticalScale { get; set; }

        protected string _realText = string.Empty;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(false)]
        public string RealText
        {
            get
            {
                if (string.IsNullOrEmpty(_realText))
                {
                    return this.Text;
                }
                return _realText;
            }
            set
            {
                _realText = value;
                Text = value;
            }
        }

        [Browsable(false)]
        [Category("MES控件属性")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(true)]
        bool IElement.HasBorder
        {
            get;
            set;
        }

        [Browsable(false)]
        [Category("MES控件属性")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(true)]
        bool IElement.HasBottomBorder
        {
            get;
            set;
        }

        [Browsable(false)]
        [Category("MES控件属性")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(true)]
        bool IElement.HasLeftBorder
        {
            get;
            set;
        }

        [Browsable(false)]
        [Category("MES控件属性")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(true)]
        bool IElement.HasRightBorder
        {
            get;
            set;
        }

        [Browsable(false)]
        [Category("MES控件属性")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(true)]
        bool IElement.HasTopBorder
        {
            get;
            set;
        }

        [Browsable(false)]
        [Category("MES控件属性")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(true)]
        string IElement.BorderName
        {
            get;
            set;
        }

        [Browsable(false)]
        [Category("MES控件属性")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(true)]
        float IElement.MoveX
        {
            get;
            set;
        }

        [Browsable(false)]
        [Category("MES控件属性")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(true)]
        float IElement.MoveY
        {
            get;
            set;
        }

        [Browsable(false)]
        [Category("MES控件属性")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(true)]
        bool IElement.CanInvalidate
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 额外的数据
        /// </summary>
        [Browsable(false)]
        [Category("MES控件属性")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(true)]
        List<ExternData> IElement.ExternDatas 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// 元素边框
        /// </summary>
        [Browsable(false)]
        [Category("MES控件属性")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(true)]
        ElementBorder IElement.Border
        { 
            get; 
            set; 
        }

        private MESVarType _mestType = MESVarType.MESString;
        /// <summary>
        /// 数据类型
        /// </summary>
        [Category("MES控件属性")]
        public MESVarType MESType
        {
            get
            {
                return _mestType;
            }
            set
            {
                _mestType = value;
            }
        }

        [Browsable(false)]
        IElement IElement.Parent
        {
            get
            {
                return Parent as IElement;
            }
            set
            {
                Parent = value as Control;
            }
        }


        /// <summary>
        /// 缩放
        /// </summary>
        public void Zoom()
        {
            Zoom(VerticalScale, VerticalScale);
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
            if (_orginalWidth == -1)
            {
                _orginalWidth = this.Width;
            }
            if (_orginalHeight == -1)
            {
                _orginalHeight = this.Height;
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
            if (this.Multiline)
            {
                this.Height = (int)(hScale * this._orginalHeight);
            }
            this.Location = new Point((int)(this._orginalLocation.X * hScale), (int)(this._orginalLocation.Y * vScale));
            this.Font.Dispose();
            this.Font = null;
            float fontSize = hScale * _orginalFontSize;
            this.Font = new Font(_orginalFontName, fontSize);
            HorizontalScale = hScale;
            VerticalScale = vScale;
        }

        [Editor(typeof(EditTextEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(false)]
        [Category("MES控件属性")]
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


        protected string _var = string.Empty;
        [Editor(typeof(EditTextEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(EditConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        [Category("MES控件属性")]
        public string Var
        {
            get
            {
                return _var;
            }
            set
            {
                _var = value;
                if (!string.IsNullOrEmpty(value))
                {
                    Text = "[%" + _var + "%]";
                    RealText = Text;
                }
                Invalidate();
            }
        }

        [Category("MES控件属性")]
        [Browsable(true)]
        public  bool EnableMapping
        {
            get;
            set;
        }

        [Category("MES控件属性")]
        [Editor(typeof(DataMappingEditor), typeof(UITypeEditor))]
        [Browsable(true)]
        public  string MappingTable
        {
            get;
            set;
        }

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

        public PmsEdit()
            : base()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BorderStyle = BorderStyle.FixedSingle;
        }
        public PmsEdit(IElement element)
            : base()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            PmsEditWrapper edit = element as  PmsEditWrapper;
            Name = edit.Name;
            AutoSize = edit.AutoSize;
            BackColor = edit.BackColor;
            if (null != edit.BackgroundImage)
            {
                BackgroundImage = edit.BackgroundImage.Clone() as Image;
            }
            BackgroundImageLayout = edit.BackgroundImageLayout;
            if (null != edit.Border)
            {
                ((IElement)this).Border = edit.Border.Clone() as ElementBorder;
                ((IElement)this).Border.OwnerElement = this as IElement;
            }
            ((IElement)this).BorderName = edit.BorderName;
            if (null != edit.ExternDatas)
            {
                ((IElement)this).ExternDatas = new List<ExternData>();
                foreach (ExternData ed in edit.ExternDatas)
                {
                    object value = ed.Value;
                    if (null != value && value is ICloneable)
                    {
                        value = ((ICloneable)value).Clone();
                    }
                    ((IElement)this).ExternDatas.Add(new ExternData(ed.Key, value));
                }
            }
            if (null != edit.Font)
            {
                Font = edit.Font.Clone() as Font;
            }
            ((IElement)this).HasBorder = edit.HasBorder;
            ((IElement)this).HasBottomBorder = edit.HasBottomBorder;
            ((IElement)this).HasLeftBorder = edit.HasLeftBorder;
            ((IElement)this).HasRightBorder = edit.HasRightBorder;
            ((IElement)this).HasTopBorder = edit.HasTopBorder;
            Height = edit.Height;
            HorizontalScale =edit.HorizontalScale;
            ((IElement)this).Location = edit.Location;
            ((IElement)this).MESType = edit.MESType;
            ((IElement)this).MoveX = edit.MoveX;
            ((IElement)this).MoveY = edit.MoveY;
            MESType = edit.MESType;
            Text = edit.Text;
            Var = edit.Var;
            VerticalScale = edit.VerticalScale;
            Width = edit.Width;
            RealText = edit.RealText;
            BorderStyle = edit.BorderStyle;
            Visible = edit.Visible;
            if (null != ((IElement)edit).ExtendObject)
            {
                ((IElement)this).ExtendObject = ((IElement)edit).ExtendObject.Clone() as ExtendObject;
            }
        }


        public void Print(Canvas ca, float x, float y)
        {
                Graphics g = ca.Graphics;
                SolidBrush backBrush = new SolidBrush(this.BackColor);
                try
                {
                    g.FillRectangle(backBrush, 0, 0, this.Width, this.Height);
                }
                finally
                {
                    backBrush.Dispose();
                }
                if (this.BorderStyle == BorderStyle.FixedSingle)
                {
                    System.Drawing.Pen pen = new Pen(Color.Black, 1);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
                    pen.Dispose();
                }

                if (EnableMapping)
                {
                    string result = GetMapValue(RealText);
                    if (null != result)
                    {
                        RealText = result;
                    }
                }
                SizeF size = g.MeasureString(this.RealText, this.Font);
                SolidBrush sb = new SolidBrush(this.ForeColor);
                try
                {
                    g.DrawString(RealText, this.Font, sb, new RectangleF(x, y + 2, this.Width, size.Height));
                }
                finally
                {
                    sb.Dispose();
                }
        }

        protected override void WndProc(ref Message m)
        {
           
            base.WndProc(ref m);
            //switch (m.Msg)
            //{
            //    case Win32Message.WM_PAINT:
            //        Graphics g = Graphics.FromHwnd(this.Handle);
            //        Print(new Canvas(g), 0, 0);
            //        g.Dispose();
            //        break;
            //}
        }

         /// <summary>
        /// 将变量绑定值 
        /// </summary>
        /// <param name="values">将变量绑定值</param>
        public virtual void BindValue(IDictionary<string, Object> values)
        {
            if (null != values && values.Count > 0 && !string.IsNullOrEmpty(Var))
            {
                RealText = string.Format("[%{0}%]",Var);
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
                            string newStr = values[key] as string;
                            if (string.IsNullOrEmpty(newStr))
                            {
                                newStr = " ";
                            }
                            RealText = RealText.Replace(m.Value, newStr);
                        }
                    }
                }
                 
            }
        }

        public object Clone()
        {
            PmsEdit edit = new PmsEdit();
            edit.BackColor = this.BackColor;
            edit.Anchor = this.Anchor;
            edit.Bounds = this.Bounds;
            edit.ForeColor = this.ForeColor;
            edit.Height = this.Height;
            edit.Width = this.Width;
            edit.VerticalScale = this.VerticalScale;
            edit.HorizontalScale = this.HorizontalScale;
            edit.Location = this.Location;
            edit.Left = this.Left;
            edit.Top = this.Top;
            edit.Var = this.Var;
            edit.Text = this.Text;
            edit.Region = this.Region;
            edit.RightToLeft = this.RightToLeft;
            edit.Site = this.Site;
            edit.RealText = this.RealText;
            edit.Size = this.Size;
            edit.Name = this.Name;
            edit.TextAlign = this.TextAlign;
            edit.Font = this.Font.Clone() as Font;
            edit.EnableMapping = EnableMapping;
            edit.MappingTable = MappingTable;
            edit.Visible = Visible;
            edit.MESType = MESType;
            edit.Multiline = this.Multiline;
            if (null != ((IElement)this).Border)
            {
                ((IElement)edit).Border = ((IElement)this).Border.Clone() as ElementBorder;
                ((IElement)edit).Border.OwnerElement = edit;
            }
            ((IElement)edit).BorderName = ((IElement)this).BorderName;
            if (null != ((IElement)this).ExternDatas)
            {
                ((IElement)edit).ExternDatas = new List<ExternData>();
                foreach (ExternData ed in ((IElement)this).ExternDatas)
                {
                    object value = ed.Value;
                    if (null != value && value is ICloneable)
                    {
                        value = ((ICloneable)value).Clone();
                    }
                    ((IElement)edit).ExternDatas.Add(new ExternData(ed.Key, value));
                }
            }

            if (null != Tag)
            {
                if (Tag is ICloneable)
                {
                    edit.Tag = ((ICloneable)Tag).Clone();
                }
                else
                {
                    edit.Tag = Tag;
                }
            }

            if (null != ((IElement)this).ExtendObject)
            {
                ((IElement)edit).ExtendObject = ((IElement)this).ExtendObject.Clone() as ExtendObject;
            }
            return edit;
        }

        public IControlTranslator ToElement(bool transferChild)
        {
            PmsEditWrapper edit = new PmsEditWrapper();
            edit.Name = Name;
            edit.AutoSize = AutoSize;
            edit.BackColor = BackColor;
            if (null != BackgroundImage)
            {
                edit.BackgroundImage = BackgroundImage.Clone() as Image;
            }
            edit.BackgroundImageLayout = BackgroundImageLayout;
            if (null != ((IElement)this).Border)
            {
                edit.Border = ((IElement)this).Border.Clone() as ElementBorder;
                edit.Border.OwnerElement = edit;
            }
            edit.BorderName = ((IElement)this).BorderName;
            if (null != ((IElement)this).ExternDatas)
            {
                edit.ExternDatas = new List<ExternData>();
                foreach (ExternData ed in ((IElement)this).ExternDatas)
                {
                    object value = ed.Value;
                    if (null != value && value is ICloneable)
                    {
                        value = ((ICloneable)value).Clone();
                    }
                    edit.ExternDatas.Add(new ExternData(ed.Key, value));
                }
            }
            if (_orginalFontSize > 0)
            {
                edit.Font = new Font(Font.FontFamily, _orginalFontSize);
            }
            else
            {
                edit.Font = new Font(this.Font.FontFamily, this.Font.Size);
            }
            edit.VerticalScale = 1f;
            edit.HorizontalScale = 1f;
            edit.HasBorder = ((IElement)this).HasBorder;
            edit.HasBottomBorder = ((IElement)this).HasBottomBorder;
            edit.HasLeftBorder = ((IElement)this).HasLeftBorder;
            edit.HasRightBorder = ((IElement)this).HasRightBorder;
            edit.HasTopBorder = ((IElement)this).HasTopBorder;
            edit.Visible = Visible;
            edit.MESType = MESType;
            if (_orginalHeight > 0)
            {
                edit.Height = this._orginalHeight;
            }
            else
            {
                edit.Height = this.Height;
            }
            if (_orginalWidth > 0)
            {
                edit.Width = _orginalWidth;
            }
            else
            {
                edit.Width = this.Width;
            }
            //edit.VerticalScale = this.VerticalScale;
            //edit.HorizontalScale = this.HorizontalScale;
            if (_orginalLocation != Point.Empty)
            {
                edit.Location = this._orginalLocation;
            }
            else
            {
                edit.Location = this.Location;
            }
            edit.MESType = MESType;
            edit.MoveX = ((IElement)this).MoveX;
            edit.MoveY = ((IElement)this).MoveY;
            edit.Text = Text;
            edit.VerticalScale = VerticalScale;

            edit.RealText = RealText;
            edit.BorderStyle = BorderStyle;
            edit.Var = Var;
             
            if (null != ((IElement)this).ExtendObject)
            {
                ((IElement)edit).ExtendObject = ((IElement)this).ExtendObject.Clone() as ExtendObject;
            }
            return edit;
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
                if (null != Tag)
                {
                    PmsEdit edit = Tag as PmsEdit;
                    if (null != edit)
                    {
                        name = edit.Name;
                    }
                    else
                    {
                        if (null != ((IElement)this).ExtendObject
                             && !string.IsNullOrEmpty(((IElement)this).ExtendObject.Name))
                        {
                            name = ((IElement)this).ExtendObject.Name;
                        }
                    }
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
                element.InnerText = Text;
                list.Add(element);
            }
            catch (Exception ex)
            {
                PMSPublicInfo.Message.Error(ex.Message);
            }
            return list;
        }

        public string GetMapValue(string key)
        {
            if (string.IsNullOrEmpty(MappingTable) || string.IsNullOrEmpty(key))
            {
                return null;
            }

            return PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetReplaceValue(MappingTable, key);

        }        
    }
}
