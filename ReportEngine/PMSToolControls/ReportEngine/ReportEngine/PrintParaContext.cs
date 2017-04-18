using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using PMS.Libraries.ToolControls.PMSPublicInfo;using System;
using System.ComponentModel;

namespace NetSCADA.ReportEngine
{
	[System.Serializable]
	public class PrintParaContext
	{
		private bool _landscape;

		private int _ScrollWidth = 17;

		private string _ExceptionString = string.Empty;

		private int _width;

		[DefaultValue(false), Description("水平打印")]
		public bool Landscape
		{
			get
			{
				return this._landscape;
			}
			set
			{
				this._landscape = value;
			}
		}

		[DefaultValue(false), Description("运行时先设置报表变量值")]
		public bool PopWhenRun
		{
			get;
			set;
		}

		[DefaultValue(true), Description("是否显示绑定空记录集的控件")]
		public bool NoDisplayNullRecord
		{
			get;
			set;
		}

		[DefaultValue(true), Description("是否弹出打印设置对话")]
		public bool PrintSet
		{
			get;
			set;
		}

		[DefaultValue(""), Description("显示空记录集控件时默认字符串")]
		public string NullRecordDefaultString
		{
			get;
			set;
		}

		[DefaultValue(true), Description("自适应列")]
		public bool FitToPage
		{
			get;
			set;
		}

		[DefaultValue(true), Description("分裂打印")]
		public bool SplitPrint
		{
			get;
			set;
		}

		[Browsable(false), Category("尺寸"), DefaultValue(839), Description("查看报表宽度，像素")]
		public int Width
		{
			get
			{
				return this._width;
			}
			set
			{
				this._width = value;
			}
		}

		[Description("滚动条宽度")]
		public int ScrollWidth
		{
			get
			{
				return this._ScrollWidth;
			}
			set
			{
				this._ScrollWidth = value;
			}
		}

		[Description("异常显示字符串")]
		public string ExceptionString
		{
			get
			{
				return this._ExceptionString;
			}
			set
			{
				this._ExceptionString = value;
			}
		}

		public PrintParaContext()
		{
			this._landscape = false;
			this._width = 749;
			this.PopWhenRun = false;
			this.FitToPage = true;
			this.SplitPrint = true;
			this.NoDisplayNullRecord = true;
			this.PrintSet = true;
		}

		public PMSPrintPara ToPMSPrintPara()
		{
			return new PMSPrintPara
			{
				Width = this.Width,
				SplitPrint = this.SplitPrint,
				FitToPage = this.FitToPage,
				Landscape = this.Landscape,
				NoDisplayNullRecord = this.NoDisplayNullRecord,
				NullRecordDefaultString = this.NullRecordDefaultString,
				PopWhenRun = this.PopWhenRun,
				PrintSet = this.PrintSet,
				ScrollWidth = this.ScrollWidth,
				ExceptionString = this._ExceptionString
			};
		}
	}
}


namespace NetSCADA.ReportEngine
{
	[Serializable]
	[DisplayName("Parameter")]
	[DefaultProperty("PaperSize")]
	public class PMSPrintPara : Component, ICloneable, System.ComponentModel.ICustomTypeDescriptor
	{
		#region Public Property
		[Category("通用")]
		[Description("控件名字")]
		[Browsable(true)]
		public string Name
		{
			get { return base.Site.Name; }
			set { base.Site.Name = value; }
		}

		#region 表达式
		private string _ExceptionString = MES.FormLib.Controls.Expressions.ExpExcuteBase.DefaultString;
		[Category("表达式")]
		[Description("异常显示字符")]
		public string ExceptionString
		{
			get
			{
				return _ExceptionString;
			}
			set
			{
				_ExceptionString = value;
			}
		}

		private string _NullRecordDefaultString = MES.FormLib.Controls.Expressions.ExpExcuteBase.DefaultString;
		[Category("表达式")]
		[DefaultValue("")]
		[Description("显示空记录集控件时默认字符串")]
		[DisplayName("NullRecordString")]
		public string NullRecordDefaultString
		{
			get
			{
				return _NullRecordDefaultString;
			}
			set
			{
				_NullRecordDefaultString = value;
			}
		}
		#endregion

		#region 打印
		//分裂打印
		[DefaultValue(false)]
		[Category("打印")]
		[Description("分裂打印")]
		public bool SplitPrint
		{
			get;
			set;
		}

		[Category("打印")]
		[DefaultValue(true)]
		[Description("是否弹出打印设置对话")]
		[DisplayName("ShowPrintDialog")]
		public bool PrintSet
		{
			get;
			set;
		}

		private PaperSize _PaperSize = new PaperSize("Custom", 21f, 29.7f);
		[Category("打印")]
		[Description("纸张大小,单位cm")]
		[TypeConverter(typeof(PaperSizeTypeConverter))]
		public PaperSize PaperSize
		{
			get { return _PaperSize; }
			set
			{
				//if (_PaperSize.Name != "Custom" && PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.ReadingXmlInDesignerTime == false && PaperSizeValidCheck)
				//    throw new Exception("只有Custom自定义模式允许修改。");
				_PaperSize = value;
				SetFormSize();
				_PaperSize.PropertyChanged += _PaperSize_PropertyChanged;
				PaperSizeValidCheck = true;
			}
		}

		private bool _KeepAspectRatio = true;
		[Category("打印")]
		[Description("保持长宽比")]
		[DefaultValue(true)]
		public bool KeepAspectRatio
		{
			get { return _KeepAspectRatio; }
			set { _KeepAspectRatio = value; }
		}

		private Margins _Margin = new Margins(1.27f, 1.27f, 1.27f, 1.27f);
		[Category("打印")]
		[Description("纸张边距,单位cm")]
		public Margins Margin
		{
			get
			{
				return _Margin;
			}
			set
			{
				_Margin = value;
				SetFormSize();
				_Margin.PropertyChanged += _Margin_PropertyChanged;
			}
		}

		private bool _landscape = false;
		[DefaultValue(false)]
		[Browsable(true)]
		[Category("打印")]
		[Description("水平打印")]
		public bool Landscape
		{
			get { return _landscape; }
			set 
			{ 
				_landscape = value;
				SetFormSize();
			}
		}

		private bool _zoomToPaper = true;
		[Category("打印")]
		[Description("缩放至纸张")]
		[DefaultValue(true)]
		[DisplayName("EnableZoom")]
		public bool ZoomToPaper
		{
			get { return _zoomToPaper; }
			set { _zoomToPaper = value; }
		}
		#endregion

		#region 外观
		[Category("外观")]
		[DefaultValue(false)]
		[Description("运行时弹出参数设置页面")]
		[DisplayName("ShowParamDialog")]
		public bool PopWhenRun
		{
			get;
			set;
		}

		[Category("外观")]
		[DefaultValue(true)]
		[Browsable(false)]
		[Description("是否显示绑定空记录集的控件")]
		public bool NoDisplayNullRecord
		{
			get;
			set;
		}

		private int _ScrollWidth = 17;
		[Category("外观")]
		[Description("滚动条宽度")]
		[DefaultValue(17)]
		public int ScrollWidth
		{
			get
			{
				return _ScrollWidth;
			}
			set
			{
				_ScrollWidth = value;
			}
		}

		private ZoomMode _ZoomMode = ZoomMode.FitWidth;
		[Category("外观")]
		[DefaultValue(ZoomMode.FitWidth)]
		[Description("初始缩放模式")]
		[DisplayName("DefaultZoomMode")]
		public ZoomMode ZoomMode
		{
			get { return _ZoomMode; }
			set { _ZoomMode = value; }
		}

		private int _width;
		[Category("外观")]
		[Browsable(false)]
		[Description("报表设计宽度，像素")]
		public int Width
		{
			get { return _width; }
			set { _width = value; }
		}
		#endregion

		#endregion

		#region 新版本属性
		private float _DpiX = 96.0f;
		[Browsable(false)]
		public float DpiX
		{
			get { return _DpiX; }
			set { _DpiX = value; }
		}

		private float _DpiY = 96.0f;
		[Browsable(false)]
		public float DpiY
		{
			get { return _DpiY; }
			set { _DpiY = value; }
		}


		#endregion

		public PMSPrintPara()
		{
			_landscape = false;
			//_paper = PaperKind.A4;
			_width = 749;
			//Left = 30;
			//Right = 30;
			//Top = 30;
			//Bottom = 30;
			PopWhenRun = false;
			FitToPage = true;
			SplitPrint = false;
			NoDisplayNullRecord = true;
			PrintSet = true;
			_DpiX = 96;//PMS.Libraries.ToolControls.PMSPublicInfo.Win32DC.GetDpix();
			_DpiY = 96;//PMS.Libraries.ToolControls.PMSPublicInfo.Win32DC.GetDpiy();
			_PaperSize.PropertyChanged += new EventHandler<PropertyChagedEventArgs>(_PaperSize_PropertyChanged);
			_Margin.PropertyChanged += new EventHandler<PropertyChagedEventArgs>(_Margin_PropertyChanged);
		}

		private void SetFormSize()
		{
			if (null != this.Site)
			{
				// 设计时,找到Form
				foreach (IComponent c in this.Site.Container.Components)
				{
					if (c is System.Windows.Forms.Form)
					{
						System.Windows.Forms.Form form1 = c as System.Windows.Forms.Form;
						int h = form1.Height;
						int w = 0;
						if(!_landscape)
							w = (int)PMS.Libraries.ToolControls.PMSPublicInfo.PublicFunctionClass.ConvertCentimeterToPixel(_PaperSize.Width - _Margin.Left - _Margin.Right, DpiX);
						else
							w = (int)PMS.Libraries.ToolControls.PMSPublicInfo.PublicFunctionClass.ConvertCentimeterToPixel(_PaperSize.Height - _Margin.Top - _Margin.Bottom, DpiX);
						if (h > 32767)
							h = 32767;
						if (w > 32767)
							w = 32767;

						//改变视图尺寸时默认将滚动条设置至初始位置（0,0）
						int x = 0 - ((System.Windows.Forms.ScrollProperties)(((System.Windows.Forms.ScrollableControl)(form1.Parent)).HorizontalScroll)).Value;
						int y = 0 - ((System.Windows.Forms.ScrollProperties)(((System.Windows.Forms.ScrollableControl)(form1.Parent)).VerticalScroll)).Value;
						MES.PublicInterface.DesignerForm.SetFormSize(form1.Handle, x, y, w, h);
					}
				}
			}
		}

		void _Margin_PropertyChanged(object sender, PropertyChagedEventArgs e)
		{
			if (e.PropertyName == "Left" || e.PropertyName == "Right")
			{
				SetFormSize();
			}
		}

		void _PaperSize_PropertyChanged(object sender, PropertyChagedEventArgs e)
		{
			if (e.PropertyName == "Width")
			{
				if(KeepAspectRatio)
				{
					float newHeight = (float)Math.Round(_PaperSize.Height * (Convert.ToSingle(e.NewValue) / Convert.ToSingle(e.OldValue)), 2);
					if (_PaperSize.Height != newHeight)
					{
						PaperSize = new PaperSize(_PaperSize.Name, _PaperSize.Width, newHeight);
						//_PaperSize.Height = newHeight;
					}
				}
				SetFormSize();
			}
			else if (e.PropertyName == "Height")
			{
				if (KeepAspectRatio)
				{
					float newWidth = (float)Math.Round(_PaperSize.Width * (Convert.ToSingle(e.NewValue) / Convert.ToSingle(e.OldValue)), 2);
					if (_PaperSize.Width != newWidth)
					{
						PaperSize = new PaperSize(_PaperSize.Name, newWidth, _PaperSize.Height);
						//SetFormSize();
						//_PaperSize.Width = newWidth;
					}
				}
			}

		}

		[Browsable(false)]
		public bool PaperSizeValidCheck = true;

		//打印全部
		[DefaultValue(true)]
		[Browsable(false)]
		[Description("自适应列")]
		public bool FitToPage
		{
			get;
			set;
		}

		public PrintParaContext ToContext()
		{
			PrintParaContext result = new PrintParaContext();
			result.Width = this.Width;
			result.SplitPrint = this.SplitPrint;
			result.FitToPage = this.FitToPage;
			result.Landscape = this.Landscape;
			result.NoDisplayNullRecord = this.NoDisplayNullRecord;
			result.NullRecordDefaultString = this.NullRecordDefaultString;
			result.PopWhenRun = this.PopWhenRun;
			result.PrintSet = this.PrintSet;
			result.ScrollWidth = this._ScrollWidth;
			result.ExceptionString = this.ExceptionString;
			return result;
		}

		public object Clone()
		{
			PMSPrintPara copy = new PMSPrintPara();
			copy.Landscape = this.Landscape;
			copy.PopWhenRun = this.PopWhenRun;
			copy.NoDisplayNullRecord = this.NoDisplayNullRecord;
			copy.PrintSet = this.PrintSet;
			copy.NullRecordDefaultString = this.NullRecordDefaultString;
			copy.FitToPage = this.FitToPage;
			copy.SplitPrint = this.SplitPrint;
			copy.Width = this.Width;
			copy.ScrollWidth = this.ScrollWidth;
			copy.ExceptionString = this.ExceptionString;
			copy.DpiX = this.DpiX;
			copy.DpiY = this.DpiY;
			copy.Margin = this.Margin.Clone() as Margins;
			copy.PaperSize = this.PaperSize.Clone() as PaperSize;
			copy.ZoomMode = this.ZoomMode;
			copy.ZoomToPaper = this.ZoomToPaper;
			return copy;
		}

		#region   ICustomTypeDescriptor   显式接口定义
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		string ICustomTypeDescriptor.GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		string ICustomTypeDescriptor.GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			if (null != this.Site)
				return this.FilterProperties(TypeDescriptor.GetProperties(this.GetType()));
			else
				return TypeDescriptor.GetProperties(this.GetType());
		}

		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			if (null != this.Site)
			{
				return this.FilterProperties(TypeDescriptor.GetProperties(this.GetType(), attributes));
			}
			else
				return TypeDescriptor.GetProperties(this.GetType(), attributes);
		}

		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		#region 属性过滤

		private PropertyDescriptorCollection FilterProperties(PropertyDescriptorCollection properties)
		{
			if (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.ReadingXmlInDesignerTime == true)
				return TypeDescriptor.GetProperties(this.GetType());

			PropertyDescriptorCollection tmpPDC = properties;
			System.Reflection.PropertyInfo[] pis = this.GetType().GetProperties();
			ArrayList props = new ArrayList();
			foreach (PropertyDescriptor pdes in tmpPDC)
			{
				System.Reflection.PropertyInfo pi = pis.First(o => o.Name == pdes.Name);
				if (pi.DeclaringType == this.GetType())
					props.Add(new GlobalizedPropertyDescriptor(pdes));
			}

			GlobalizedPropertyDescriptor[] propArray =
				(GlobalizedPropertyDescriptor[])props.ToArray(typeof(GlobalizedPropertyDescriptor));
			tmpPDC = new PropertyDescriptorCollection(propArray);
			return tmpPDC;

		}

		#endregion

		#endregion
	}

	[Serializable]
	[TypeConverter(typeof(MarginsTypeConverter))]
	public class Margins : ICloneable
	{
		[field: NonSerialized]
		public event EventHandler<PropertyChagedEventArgs> PropertyChanged;

		private float _Bottom = 0;
		[NotifyParentProperty(true)]
		[Description("下边距,单位:cm")]
		public float Bottom 
		{
			get { return _Bottom; }
			set
			{
				if (value != _Bottom)
				{
					PropertyChagedEventArgs e = new PropertyChagedEventArgs("Bottom", _Bottom, value);
					_Bottom = value;
					if (null != PropertyChanged)
					{
						PropertyChanged(this, e);
					}
				}
			}
		}

		private float _Left = 0;
		[NotifyParentProperty(true)]
		[Description("左边距,单位:cm")]
		public float Left 
		{
			get { return _Left; }
			set
			{
				if (value != _Left)
				{
					PropertyChagedEventArgs e = new PropertyChagedEventArgs("Left", _Left, value);
					_Left = value;
					if (null != PropertyChanged)
					{
						PropertyChanged(this, e);
					}
				}
			} 
		}

		private float _Right = 0;
		[NotifyParentProperty(true)]
		[Description("右边距,单位:cm")]
		public float Right 
		{
			get { return _Right; }
			set
			{
				if (value != _Right)
				{
					PropertyChagedEventArgs e = new PropertyChagedEventArgs("Right", _Right, value);
					_Right = value;
					if (null != PropertyChanged)
					{
						PropertyChanged(this, e);
					}
				}
			}  
		}

		private float _Top = 0;
		[NotifyParentProperty(true)]
		[Description("上边距,单位:cm")]
		public float Top 
		{
			get { return _Top; }
			set
			{
				if (value != _Top)
				{
					PropertyChagedEventArgs e = new PropertyChagedEventArgs("Top", _Top, value);
					_Top = value;
					if (null != PropertyChanged)
					{
						PropertyChanged(this, e);
					}
				}
			}  
		}

		public Margins()
		{

		}

		public Margins(float left, float right, float top, float bottom)
		{
			Left = left;
			Right = right;
			Top = top;
			Bottom = bottom;
		}

		public static bool operator !=(Margins m1, Margins m2)
		{
			return !(m1 == m2);
		}

		public static bool operator ==(Margins m1, Margins m2)
		{
			// If both are null, or both are same instance, return true.
			if (System.Object.ReferenceEquals(m1, m2))
			{
				return true;
			}

			// If one is null, but not both, return false.
			if (((object)m1 == null) || ((object)m2 == null))
			{
				return false;
			}
			return m1.Left == m2.Left && m1.Right == m2.Right && m1.Top == m2.Top && m1.Bottom == m2.Bottom;
		}

		public object Clone()
		{
			Margins copy = new Margins(this.Left, this.Right, this.Top, this.Bottom);
			return copy;
		}

		public override bool Equals(object obj)
		{
			return obj is Margins && this == (Margins)obj;
		}

		public override int GetHashCode()
		{
			return Left.GetHashCode() ^ Right.GetHashCode() ^ Top.GetHashCode() ^ Bottom.GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("Top:{0}cm,Bottom:{1}cm,Left:{2}cm,Right:{3}cm", Top, Bottom, Left, Right);
		}
	}


	[Serializable]
	public class PaperSize : ICloneable
	{
		[field:NonSerialized]
		public event EventHandler<PropertyChagedEventArgs> PropertyChanged;

		private string _name = "A4";
		[NotifyParentProperty(true)]
		[ReadOnly(true)]
		[Description("名称")]
		public string Name
		{
			get { return _name; }
			set
			{
				if (value != _name)
				{
					if (_name != "Custom" && PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.ReadingXmlInDesignerTime == false && PaperSizeValidCheck)
						throw new Exception("只有Custom自定义模式允许修改。");
					PropertyChagedEventArgs e = new PropertyChagedEventArgs("Name", _name, value);
					_name = value;
					if (null != PropertyChanged)
					{
						PropertyChanged(this, e);
					}
				}
			}
		}

		private float _width;
		[NotifyParentProperty(true)]
		[Description("纸张宽度,单位:cm")]
		public float Width
		{
			get { return _width; }
			set
			{
				if (value != _width)
				{
					if (_name != "Custom" && PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.ReadingXmlInDesignerTime == false && PaperSizeValidCheck)
						throw new Exception("只有Custom自定义模式允许修改。");
					PropertyChagedEventArgs e = new PropertyChagedEventArgs("Width", _width, value);
					_width = value;
					if (null != PropertyChanged)
					{
						PropertyChanged(this, e);
					}
				}
			}
		}

		private float _height;
		[NotifyParentProperty(true)]
		[Description("纸张高度,单位:cm")]
		public float Height
		{
			get { return _height; }
			set
			{
				if (value != _height)
				{
					if (_name != "Custom" && PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.ReadingXmlInDesignerTime == false && PaperSizeValidCheck)
						throw new Exception("只有Custom自定义模式允许修改。");
					PropertyChagedEventArgs e = new PropertyChagedEventArgs("Height", _height, value);
					_height = value;
					if (null != PropertyChanged)
					{
						PropertyChanged(this, e);
					}
				}
			}
		}


		[Browsable(false)]
		public bool PaperSizeValidCheck = true;

		public PaperSize()
		{

		}

		public PaperSize(string name, float width, float height)
		{
			PaperSizeValidCheck = false;
			_name = name;
			_width = width;
			_height = height;
			PaperSizeValidCheck = true;
		}

		public override string ToString()
		{
			return string.Format("Paper:{0},Width:{1}cm,Height:{2}cm", _name, _width, _height);
		}

		public object Clone()
		{
			PaperSize copy = new PaperSize(this.Name, this.Width, this.Height);
			return copy;
		}

		public override bool Equals(object obj)
		{
			return obj is PaperSize && this == (PaperSize)obj;
		}

		public static bool operator ==(PaperSize x, PaperSize y)
		{
			// If both are null, or both are same instance, return true.
			if (System.Object.ReferenceEquals(x, y))
			{
				return true;
			}

			// If one is null, but not both, return false.
			if (((object)x == null) || ((object)y == null))
			{
				return false;
			}
			return x.Name == y.Name && x.Width == y.Width && x.Height == y.Height;
		}

		public static bool operator !=(PaperSize x, PaperSize y)
		{
			return !(x == y);
		}

		public override int GetHashCode()
		{
			return Name.GetHashCode() ^ Width.GetHashCode() ^ Height.GetHashCode();
		}
	}

	public class MarginsTypeConverter : ExpandableObjectConverter
	{
		public override bool CanConvertTo(ITypeDescriptorContext context,
			System.Type destinationType)
		{
			if (destinationType == typeof(Margins))
				return true;
			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, System.Type destinationType)
		{
			if (destinationType == typeof(System.String) && value is Margins)
			{
				Margins so = (Margins)value;
				return so.ToString();
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		public override bool CanConvertFrom(ITypeDescriptorContext context, System.Type sourceType)
		{
			if (sourceType == typeof(string))
				return true;
			return base.CanConvertFrom(context, sourceType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				try
				{
					string s = (string)value;
					int colon = s.IndexOf(':');
					int comma = s.IndexOf("cm,");
					if (colon != -1 && comma != -1)
					{
						string top = s.Substring(colon + 1, (comma - colon - 1));
						colon = s.IndexOf(':', comma + 1);
						comma = s.IndexOf("cm,", comma + 1);
						string bottom = s.Substring(colon + 1, (comma - colon - 1));
						colon = s.IndexOf(':', comma + 1);
						comma = s.IndexOf("cm,", comma + 1);
						string left = s.Substring(colon + 1, (comma - colon - 1));
						colon = s.IndexOf(':', comma + 1);
						comma = s.IndexOf("cm", comma + 1);
						string right = s.Substring(colon + 1, (comma - colon - 1));
						Margins so = new Margins();
						so.Top = float.Parse(top);
						so.Bottom = float.Parse(bottom);
						so.Left = float.Parse(left);
						so.Right = float.Parse(right);
						return so;
					}
				}
				catch
				{
					throw new ArgumentException("Can not convert\"" + (string)value + "\"to type Margins");
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new StandardValuesCollection(new Margins[] { new Margins(12.3f, 5.7f, 6f, 3.17f), new Margins(2.54f, 2.54f, 3.18f, 3.18f), new Margins(1.27f, 1.27f, 1.27f, 1.27f) });
		}

		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}
	}

	public class PaperSizeTypeConverter : ExpandableObjectConverter
	{
		public override bool CanConvertTo(ITypeDescriptorContext context,
			System.Type destinationType)
		{
			if (destinationType == typeof(PaperSize))
				return true;
			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, System.Type destinationType)
		{
			if (destinationType == typeof(System.String) && value is PaperSize)
			{
				PaperSize so = (PaperSize)value;
				return so.ToString();
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		public override bool CanConvertFrom(ITypeDescriptorContext context, System.Type sourceType)
		{
			if (sourceType == typeof(string))
				return true;
			return base.CanConvertFrom(context, sourceType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				try
				{
					string s = (string)value;
					int colon = s.IndexOf(':');
					int comma = s.IndexOf(",");
					if (colon != -1 && comma != -1)
					{
						string name = s.Substring(colon + 1, (comma - colon - 1));
						colon = s.IndexOf(':', comma + 1);
						comma = s.IndexOf("cm,", comma + 1);
						string width = s.Substring(colon + 1, (comma - colon - 1));
						colon = s.IndexOf(':', comma + 1);
						comma = s.IndexOf("cm", comma + 1);
						string height = s.Substring(colon + 1, (comma - colon - 1));
						PaperSize so = new PaperSize();
						//PaperSize so = (context.Instance as PMSPrintPara).PaperSize;
						//}
						//else
						//{
						//    if(context is PMSPrintPara)
						//    {
						//        so = (context as PMSPrintPara).PaperSize;
						//    }
						//}
						if(null != context.Instance)
						{
							(context.Instance as PMSPrintPara).PaperSizeValidCheck = false;
							so.PaperSizeValidCheck = false;
						}
						so.Name = name;
						so.Width = float.Parse(width);
						so.Height = float.Parse(height);
						if (null != context.Instance)
						{
							so.PaperSizeValidCheck = true;
						}
						return so;
					}
				}
				catch
				{
					throw new ArgumentException("Can not convert\"" + (string)value + "\"to type PaperSize");
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new StandardValuesCollection(new PaperSize[] { new PaperSize("A3", 29.7f, 42f), new PaperSize("A4", 21f, 29.7f), new PaperSize("16K", 18.41f, 26.67f), new PaperSize("Custom", 21f, 29.7f) });
		}

		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}
	}

	/// <summary>
	/// 缩放模式
	/// </summary>
	public enum ZoomMode
	{
		FitPage,
		FitHeight,
		FitWidth
	}

	/// <summary>
	/// 通用的类
	/// </summary>
	public class PropertyChagedEventArgs : EventArgs
	{
		public PropertyChagedEventArgs(string propertyName, object oldValue, object newValue)
		{
			PropertyName = propertyName;
			OldValue = oldValue;
			NewValue = newValue;
		}

		public bool Cancel { get; set; }
		public string PropertyName { get; private set; }
		public object OldValue { get; private set; }
		public object NewValue { get; set; }
	}

}
