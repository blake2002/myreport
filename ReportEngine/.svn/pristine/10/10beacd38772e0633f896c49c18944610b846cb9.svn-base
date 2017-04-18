using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using PMS.Libraries.ToolControls.PMSPublicInfo;

namespace PMS.Libraries.ToolControls.PMSReport
{
	[Serializable]
	[DisplayName ("ToolBar")]
	[DefaultProperty ("CollocateToolBar")]
	public class ReportViewerToolBar : Component, System.ComponentModel.ICustomTypeDescriptor
	{
		#region Public Property

		private string toolBarName;

		[Category ("通用")]
		[Description ("控件名字")]
		[Browsable (true)]
		public string Name { //todo:qiuleilei 20161214
			get {
				if (CurrentPrjInfo.CurrentEnvironment == MESEnvironment.MESReportServer)
					return toolBarName;
				return base.Site.Name;
			}
			set {
				if (CurrentPrjInfo.CurrentEnvironment == MESEnvironment.MESReportServer) {
					toolBarName = value;
				} else {
					base.Site.Name = value;
				}

			}
		}

		[Editor (typeof(ToolBarCollocate), typeof(UITypeEditor))]
		[Category ("通用")]
		[Description ("工具条配置")]
		[DisplayName ("Items")]
		public List<PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.CollocateData> CollocateToolBar {
			get;
			set;
		}

		private int _Size = 24;

		[DefaultValue (24)]
		[Category ("通用")]
		[Description ("工具条大小")]
		public int Size {
			get {
				return _Size;
			}
			set {
				_Size = value;
			}
		}

		[Category ("通用")]
		[Description ("工具条停靠位置")]
		[DefaultValue (InitialPosition.Top)]
		[DisplayName ("Dock")]
		public InitialPosition ToolBarDock {
			get;
			set;
		}

		private bool _Visible = true;

		[Category ("通用")]
		[Description ("工具条可见性")]
		[DefaultValue (true)]
		public bool Visible {
			get {
				return _Visible;
			}
			set {
				_Visible = value;
			}
		}

		#endregion

		private ToolStrip _myToolStrip;

		public  ReportViewerToolBar ()
		{
			//_myToolStrip = (new NetSCADA.ReportEngine.ReportViewer ()).ToolBar;
		}

		#region 工具栏配置

		internal class ToolBarCollocate : UITypeEditor
		{
			public override UITypeEditorEditStyle GetEditStyle (ITypeDescriptorContext context)
			{
				if (context != null && context.Instance != null) {
					return UITypeEditorEditStyle.Modal;
				}

				return base.GetEditStyle (context);
			}

			public override object EditValue (ITypeDescriptorContext context, IServiceProvider provider, object value)
			{
				IWindowsFormsEditorService editorService = null;

				if (context != null && context.Instance != null && provider != null) {
					editorService = (IWindowsFormsEditorService)provider.GetService (typeof(IWindowsFormsEditorService));
					if (editorService != null) {
						ReportViewerToolBar control = (ReportViewerToolBar)context.Instance;
						PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.CollocateToolBar form1 = new PmsSheet.PmsPublicData.CollocateToolBar ();
						form1.StartPosition = FormStartPosition.CenterScreen;
						form1.CollocateResult = control.CollocateToolBar;
						form1.ToolStrip = control._myToolStrip;
						if (DialogResult.OK == editorService.ShowDialog (form1)) {
							value = form1.CollocateResult;
						}
						return value;
					}
				}
				return value;
			}
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
				if (pi.DeclaringType == this.GetType ())
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

	public enum InitialPosition
	{
		Top,
		Bottom,
		Left,
		Right
	}
}
