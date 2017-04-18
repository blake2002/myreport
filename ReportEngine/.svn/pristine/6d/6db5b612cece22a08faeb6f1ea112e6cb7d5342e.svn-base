using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MES.Controls.Design;
using MES.Report;
using PMS.Libraries.ToolControls.PMSPublicInfo;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using PMS.Libraries.ToolControls.Report.Controls.EditorDialog;
using PMS.Libraries.ToolControls.Report.Element;
using PMS.Libraries.ToolControls.Report.Elements.Util;
using System.Net.Security;

namespace PMS.Libraries.ToolControls.PMSChart
{
	/// <summary>
	/// ChartBase：图表基类
	/// 继承或实现了所有图表运行所需的接口，并包含公共方法或虚方法、公共属性。
	/// </summary>
	public abstract class ChartBase : UserControl, IElement, IElementTranslator, IResizable, ICloneable, ISuspensionable,
        IPmsReportDataBind, IPrintable, IChartElement, System.ComponentModel.ICustomTypeDescriptor
        , IProcessCmdKey
	{
		protected System.Windows.Forms.DataVisualization.Charting.Chart chart1;

		private void InitializeComponent ()
		{

			this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart ();
			((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit ();
			this.SuspendLayout ();
			// 
			// chart1
			// 
			this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chart1.Location = new System.Drawing.Point (0, 0);
			this.chart1.Name = "chart1";
			this.chart1.Size = new System.Drawing.Size (300, 300);
			this.chart1.TabIndex = 1;
			this.chart1.Text = "chart1";
			// 
			// ChartBase
			// 
			this.Controls.Add (this.chart1);
			this.Name = "ChartBase";
			this.Size = new System.Drawing.Size (300, 300);
			this.Load += new System.EventHandler (this.ChartBase_Load);
			((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit ();
			this.ResumeLayout (false);

		}

		public ChartBase ()
		{
			InitializeComponent ();
		}

		public ChartBase (MemoryStream Aim)
		{
			try {
				InitializeComponent ();

				if (Aim != null) {
					Aim.Seek (0, SeekOrigin.Begin);
					chart1.Serializer.Load (Aim);
				}


			} catch (System.Exception ex) {
				PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, Properties.Resources.ResourceManager.GetString ("message0006") + ex.Message, false);
			}
		}

		protected void ChartBase_Load (object sender, EventArgs e)
		{
			Initial ();
		}

		private void Initial ()
		{
			if (Apperence == null) {
				Apperence = new DataSource (null);
				InitailColumnData ();
			}
		}

		/// <summary>
		/// 设计时使用，该方法用于提示编辑器内容有更改， 
		/// 任何内容的变化均需调用这个方法才可以进行保存
		/// </summary>
		public void NotifyDesignSurfaceChange ()
		{
			if (null != Site) {
				IComponentChangeService cs = Site.GetService (typeof(IComponentChangeService)) as IComponentChangeService;
				if (null != cs) {
					cs.OnComponentChanged (this, null, null, null);
				}
			}
		}

		public void InitailColumnData ()
		{
			if (this.DesignMode == true) {
				DrawVirtualChart ();
				NotifyDesignSurfaceChange ();
			}
		}

		/// <summary>
		/// 根据表达式计算出实际值
		/// </summary>
		/// <param name="expression">表达式</param>
		/// <param name="text">Text</param>
		/// <param name="dtm">数据源</param>
		/// <param name="bindPath">绑定路径</param>
		/// <returns>计算后的实际值</returns>
		public string ExpressionExecute (string expression, string text, IDataTableManager dtm, string bindPath)
		{
			IReportExpressionEngine expEngin = (this as IBindReportExpressionEngine).ExpressionEngine;
			bool newFlag = false;
			if (null == expEngin) {
				expEngin = (this as IBindReportExpressionEngine).ExpressionEngine = new ReportExpressionEngine ();
				newFlag = true;
			}
			string RealText = expEngin.Execute (expression, text, dtm, this as PMS.Libraries.ToolControls.Report.IPMSFormate, this as IDataMapping, bindPath);

			if (newFlag)
				expEngin.Dispose ();
			return RealText;
		}

		/// <summary>
		/// 对日期字段进行拆分，生成新的列（如：年、月、日、时、分、秒）
		/// </summary>
		/// <param name="Aim">源</param>
		/// <param name="feild">要拆成的字段</param>
		/// <param name="dateType">拆出来的日期类型</param>
		protected static void CreateSplitDateFiled (DataTable Aim, string feild, ArrayList columnList, string dateType)
		{
			Aim.Columns.Add (feild, typeof(Int32));
			columnList.Add (feild);
			foreach (DataRow item in Aim.Rows) {
				string sourceFeild = feild.Substring (0, feild.IndexOf ("_" + dateType));
				if (item [sourceFeild] != null) {
					switch (dateType) {
					case "Year":
						item [feild] = ((DateTime)item [sourceFeild]).Year;
						break;
					case "Month":
						item [feild] = ((DateTime)item [sourceFeild]).Month;
						break;
					case "Day":
						item [feild] = ((DateTime)item [sourceFeild]).Day;
						break;
					case "Hour":
						item [feild] = ((DateTime)item [sourceFeild]).Hour;
						break;
					case "Minute":
						item [feild] = ((DateTime)item [sourceFeild]).Minute;
						break;
					case "Second":
						item [feild] = ((DateTime)item [sourceFeild]).Second;
						break;
					default:
						break;
					}
				}

			}
		}

		/// <summary>
		/// 初始化Chart（全部清空）
		/// </summary>
		public void InitChart ()
		{
			chart1.ChartAreas.Clear ();
			chart1.Series.Clear ();
			chart1.Titles.Clear ();
			chart1.Legends.Clear ();
			chart1.Annotations.Clear ();
		}

		/// <summary>
		/// 设计时的绘制参数配置
		/// </summary>
		protected abstract void DrawVirtualChart ();

		/// <summary>
		/// 运行时的绘制参数配置
		/// </summary>
		/// <param name="Aim">数据源</param>
		/// <param name="Index">无效字段</param>
		protected abstract void SetData (DataTable Aim, int Index);

		/// <summary>
		/// 绘制图表控件
		/// </summary>
		/// <param name="graphics">画布</param>
		/// <param name="position">绘制区域</param>
		protected abstract void PrintPaint (Graphics graphics, Rectangle position);

		#region 字段和属性

		public System.Windows.Forms.DataVisualization.Charting.SeriesCollection ChartSeries {
			get {
				return this.chart1.Series;
			}
		}

		/// <summary>
		/// 默认情况下也需要绘制示例图形
		/// </summary>
		public bool isIntial = false;

		protected IDataTableManager _dtm;
		protected string _bindPath;

		private int _RunMode = 0;

		[Browsable (false)]
		public virtual int RunMode {
			get { return _RunMode; }
			set { _RunMode = value; }
		}

		private DataSource _Apperence;

		[Category ("通用")]
		[Description ("外观设置")]
		public DataSource Apperence {
			get {
				return _Apperence;
			}
			set {
				_Apperence = value;
				InitailColumnData ();
			}
		}

		private GroupSource _GroupSource = new GroupSource ();

		[Category ("通用")]
		[Description ("分组")]
		[Browsable (false)]
		public GroupSource GroupSource {
			get {
				return _GroupSource;
			}
			set {
				_GroupSource = value;
			}
		}

		#endregion

		#region IElement

		[Browsable (false)]
		public ElementBorder Border { get; set; }

		[Browsable (false)]
		public string BorderName { get; set; }

		[Browsable (false)]
		public bool CanInvalidate { get; set; }

		[Browsable (false)]
		public ExtendObject ExtendObject { get; set; }

		[Browsable (false)]
		public List<ExternData> ExternDatas { get; set; }

		[Browsable (false)]
		public bool HasBorder { get; set; }

		[Browsable (false)]
		public bool HasLeftBorder { get; set; }

		[Browsable (false)]
		public bool HasTopBorder { get; set; }

		[Browsable (false)]
		public bool HasBottomBorder { get; set; }

		[Browsable (false)]
		public bool HasRightBorder { get; set; }

		[Browsable (false)]
		public MESVarType MESType { get; set; }

		[Browsable (false)]
		public float MoveX { get; set; }

		[Browsable (false)]
		public float MoveY { get; set; }

		[Browsable (false)]
		IElement IElement.Parent {
			get {
				return Parent as IElement;
			}
			set {
				Parent = value as Control;
			}
		}

		[Browsable (false)]
		string IElement.Name {
			get {
				return base.Name;
			}
			set {
				base.Name = value;
			}
		}

		#endregion

		#region IElementTranslator

		public IControlTranslator ToElement (bool transferChild)
		{
			ChartSerializerClass result = new ChartSerializerClass ();
			result.Location = this.OriginPosition;
			//result.Location = this.Location;
			if (this.OriginHeight > 0 || this.OriginWidth > 0) {
				result.Width = this.OriginWidth;
				result.Height = this.OriginHeight;
			} else {
				result.Width = this.Width;
				result.Height = this.Height;
			}
			MemoryStream temp = new MemoryStream ();
			this.chart1.Serializer.Save (temp);
			result.Context = temp.ToArray ();
			temp.Dispose ();
			return result;
		}

		#endregion

		#region IResizable

		protected Point OriginPosition;
		protected int OriginWidth;
		protected int OriginHeight;

		[Browsable (false)]
		public float HorizontalScale { get; set; }

		[Browsable (false)]
		public float VerticalScale { get; set; }

		public void Zoom (float hScale, float vScale)
		{
			if (this.OriginHeight > 0 || this.OriginWidth > 0) {
			} else {
				this.OriginWidth = this.Width;
				this.OriginHeight = this.Height;
			}
			if ((this.OriginPosition.X == 0 && this.OriginPosition.Y == 0) && (this.Location != new Point ())) {
				this.OriginPosition = this.Location;
			}
			this.Location = new Point ((int)(this.OriginPosition.X * vScale), (int)(this.OriginPosition.Y * hScale));
			this.Width = (int)(this.OriginWidth * vScale);
			this.Height = (int)(this.OriginHeight * hScale);
		}

		public void Zoom ()
		{
		}

		#endregion

		#region ICloneable

		public abstract object Clone ();

		#endregion

		#region ISuspensionable

		public abstract SuspensionItem[] ListSuspensionItems ();

		protected abstract void DealWithApperence ();

		protected virtual void DealWithDataTable ()
		{
			IPmsReportDataBind pd = this as IPmsReportDataBind;
			SourceField parent = GetSourceField (this);
			using (SourceBindDialog fbd = new SourceBindDialog (parent, pd.SourceField, true)) {
				if (fbd.ShowDialog () == System.Windows.Forms.DialogResult.OK) {
					bool contain = false;
					if (fbd.SourceField != null) {
						FieldTreeViewData sfAll = (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetCurrentReportDataDefine ()) as FieldTreeViewData;
						List<SourceField> lpdb = fbd.SourceField.GetSubSourceField (sfAll);
						foreach (SourceField pdb in lpdb) {
							try {
								if (!string.IsNullOrEmpty (pdb.DataType)) {
									string typ = pdb.DataType.ToUpper ();
									if (typ.Equals ("INT", StringComparison.InvariantCultureIgnoreCase) ||
									    typ.Equals ("FLOAT", StringComparison.InvariantCultureIgnoreCase) ||
									    typ.Equals ("REAL", StringComparison.InvariantCultureIgnoreCase) ||
									    typ.Equals ("INT32", StringComparison.InvariantCultureIgnoreCase) ||
									    typ.Equals ("INT16", StringComparison.InvariantCultureIgnoreCase) ||
									    typ.Equals ("INT64", StringComparison.InvariantCultureIgnoreCase) ||
									    typ.Equals ("SYSTEM.SINGLE", StringComparison.InvariantCultureIgnoreCase) ||
									    typ.Equals ("SYSTEM.DOUBLE", StringComparison.InvariantCultureIgnoreCase) ||
									    typ.Equals ("SYSTEM.INT32", StringComparison.InvariantCultureIgnoreCase) ||
									    typ.Equals ("SYSTEM.DECIMAL", StringComparison.InvariantCultureIgnoreCase) ||
									    typ.Equals ("SYSTEM.DateTime", StringComparison.InvariantCultureIgnoreCase)) {
										contain = true;
									}
								}
							} catch {
								throw new Exception ("lpdb");
							}
						}
						if (contain)
							this.SourceField = fbd.SourceField;
						else
							MessageBox.Show ("没有合适的数据集！");
					} else
						this.SourceField = null;
					NotifyDesignSurfaceChange ();
				}
			}
		}

		public SourceField GetSourceField (IElement element)
		{
			if (null == element) {
				return null;
			}
			IPmsReportDataBind parent = element.Parent as IPmsReportDataBind;
			if (null == parent) {
				return null;
			}
			if (null == parent.SourceField) {
				return GetSourceField (element.Parent as IElement);
			}

			return parent.SourceField;
		}

		#endregion

		#region IPmsReportDataBind

		private SourceField _DataTable;

		[Category ("通用")]
		[Description ("绑定数据")]
		[DisplayName ("Binding")]
		[Editor (typeof(BindingEditor), typeof(UITypeEditor))]
		public virtual SourceField SourceField {
			get { return _DataTable; }
			set { _DataTable = value; }
		}

		#endregion

		#region IPrintable

		public void Print (Canvas ca, float x, float y)
		{
			try {
				Rectangle rect = new Rectangle ((int)x, (int)y, Size.Width, Size.Height);
				chart1.Printing.PrintPaint (ca.Graphics, rect);
			} catch (Exception ex) {
				PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error (ex.Message);
			}
		}

		#endregion

		#region IChartElement

		public virtual int BindDataTableManager (IDataTableManager dtm, string bindPath)
		{
			_dtm = dtm;
			_bindPath = bindPath;
			if (SourceField == null)
				return 0;
			string strTableName = SourceField.Name;
			if (SourceField.CustomMode) {
				strTableName = (this as IBindReportExpressionEngine).ExpressionEngine.GetExpressionPath (SourceField.CustomTablePath, bindPath, dtm);
			} else {
				if (!string.IsNullOrEmpty (bindPath)) {
					strTableName = string.Format ("{0}.{1}", bindPath, SourceField.Name);
				}
			}
			DataTable dt = dtm.GetDataTable (strTableName);
			if (null != dt)
				SetData (dt, 1);
			return 1;
		}

		void IDirectDrawable.DirectDraw (Canvas ca, float x, float y, float dpiZoom)
		{
			PrintPaint (ca.Graphics, new Rectangle ((int)x, (int)y, (this as IElementExtended).Width, (this as IElementExtended).Height));
		}

		IReportExpressionEngine IBindReportExpressionEngine.ExpressionEngine { get; set; }

		int IElementExtended.Height { get; set; }

		Point IElementExtended.Location { get; set; }

		int IElementExtended.Width { get; set; }

		#endregion

		#region IProcessCmdKey

		bool IProcessCmdKey.ProcessCmdKey (ref System.Windows.Forms.Message msg, Keys keyData)
		{
			bool bProcessed = false;
			bool bAltKey = (((ushort)PMS.Libraries.ToolControls.PMSPublicInfo.APIs.APIsUser32.GetAsyncKeyState (0x12)) & 0xffff) != 0;
			if (bAltKey) {
				if ((int)msg.WParam > 0 && (int)msg.WParam < 255) {
					switch ((char)(msg.WParam)) {
					case 'D':
						DealWithDataTable ();
						bProcessed = true;
						break;
					}
				}
			}
			return bProcessed;
		}

		#endregion

		#region   ICustomTypeDescriptor   显式接口定义

		AttributeCollection ICustomTypeDescriptor.GetAttributes ()
		{
			return TypeDescriptor.GetAttributes (this, true);
		}

		string ICustomTypeDescriptor.GetClassName ()
		{
			return TypeDescriptor.GetClassName (this, true);
		}

		string ICustomTypeDescriptor.GetComponentName ()
		{
			return TypeDescriptor.GetComponentName (this, true);
		}

		TypeConverter ICustomTypeDescriptor.GetConverter ()
		{
			return TypeDescriptor.GetConverter (this, true);
		}

		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent ()
		{
			return TypeDescriptor.GetDefaultEvent (this, true);
		}

		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty ()
		{
			return TypeDescriptor.GetDefaultProperty (this, true);
		}

		object ICustomTypeDescriptor.GetEditor (Type editorBaseType)
		{
			return TypeDescriptor.GetEditor (this, editorBaseType, true);
		}

		EventDescriptorCollection ICustomTypeDescriptor.GetEvents ()
		{
			return TypeDescriptor.GetEvents (this, true);
		}

		EventDescriptorCollection ICustomTypeDescriptor.GetEvents (Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents (this, attributes, true);
		}

		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties ()
		{
			if (null != this.Site)
				return this.FilterProperties (TypeDescriptor.GetProperties (this.GetType ()));
			else
				return TypeDescriptor.GetProperties (this.GetType ());
		}

		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties (Attribute[] attributes)
		{
			if (null != this.Site) {
				return this.FilterProperties (TypeDescriptor.GetProperties (this.GetType (), attributes));
			} else
				return TypeDescriptor.GetProperties (this.GetType (), attributes);
		}

		object ICustomTypeDescriptor.GetPropertyOwner (PropertyDescriptor pd)
		{
			return this;
		}

		#region 属性过滤

		private PropertyDescriptorCollection FilterProperties (PropertyDescriptorCollection properties)
		{
			if (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.ReadingXmlInDesignerTime == true)
				return TypeDescriptor.GetProperties (this.GetType ());

			PropertyDescriptorCollection tmpPDC = properties;
			System.Reflection.PropertyInfo[] pis = this.GetType ().GetProperties ();
			ArrayList props = new ArrayList ();
			foreach (PropertyDescriptor pdes in tmpPDC) {
				System.Reflection.PropertyInfo pi = pis.First (o => o.Name == pdes.Name);
				if (pdes.Name == "Width" || pdes.Name == "Height" || pi.DeclaringType == this.GetType ())
					props.Add (new GlobalizedPropertyDescriptor (pdes));
			}

			GlobalizedPropertyDescriptor[] propArray =
				(GlobalizedPropertyDescriptor[])props.ToArray (typeof(GlobalizedPropertyDescriptor));
			tmpPDC = new PropertyDescriptorCollection (propArray);
			return tmpPDC;
		}

		#endregion

		#endregion

	}
}
