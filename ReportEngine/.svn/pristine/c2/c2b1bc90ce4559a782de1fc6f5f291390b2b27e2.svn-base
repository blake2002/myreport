using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.ComponentModel.Design.Serialization;
using PMS.Libraries.ToolControls.PMSPublicInfo;
using PMS.Libraries.ToolControls.ToolBox;
using System.Collections;
using System.Data.OleDb;
using System.Dynamic;

namespace PMS.Libraries.ToolControls.PmsSheet.PmsPublicData
{
	public partial class CustomPage : UserControl
	{
		public CustomPage ()
		{
			InitializeComponent ();
		}

		private ISelectionService selectionService;
		private MenuCommandService menuCommandService;
		private SheetToolboxService toolBoxService;
		private DesignSurface surface;
		private IDesignerHost formDesignerHost;
		private List<CustomCtrlProperty> _pmsFieldPageList = new List<CustomCtrlProperty> ();
		//页面控件布局信息
		public List<PmsField> pmsFieldList = null;
		private Dictionary<string, string> _OkCancelText = null;

		//1 动态查询条件使用
		//0 关联字段使用
		public int IsQuery;

		public delegate void SelectionChangeEventHandler (object sender,EventArgs e);

		public event SelectionChangeEventHandler SelectionChange;

		protected virtual void CustomPage_SelectionChange (object sender, EventArgs e)
		{
		}

		private static object[] controlCopy = null;
		//临时粘贴板

		private TabControl tabControl1;
		// = new TabControl();
		private bool bReplace;

		private PageData _pageDatal;

		public PageData SheetConfig {
			get {
				_pageDatal = new PageData (tabControl1, bReplace);
				return _pageDatal;
			}
			set {
				_pageDatal = value;
				if (_pageDatal != null) {
					bReplace = _pageDatal.BReplace;
				}
			}
		}

		public List<CustomCtrlProperty> pmsFieldPageList {
			get { return this._pmsFieldPageList; }
			set {
				this._pmsFieldPageList = value;
			}
		}

		public Dictionary<string, string> OkCancelText {
			get {
				return this._OkCancelText;
			}
			set {
				this._OkCancelText = value;
			}
		}

		public void RemoveService ()
		{
			IServiceContainer container = surface.GetService (typeof(IServiceContainer)) as IServiceContainer;
			container.RemoveService (typeof(IToolboxService));
			container.RemoveService (typeof(IMenuCommandService));
			container.RemoveService (typeof(ComponentSerializationService));
		}

		private void CustomPage_Load (object sender, EventArgs e)
		{
			surface = new DesignSurface ();
			toolBoxService = new SheetToolboxService (surface, IsQuery);

			if (IsQuery == 1) {
				try {
					toolStripForeignMap.Enabled = false;
					splitContainer2.SplitterDistance = 110;                    
				} catch {
				}
			}

			IServiceContainer container = surface.GetService (typeof(IServiceContainer)) as IServiceContainer;
			menuCommandService = new MenuCommandService (surface);
			if (container != null) {
				container.AddService (typeof(IToolboxService), toolBoxService);
				container.AddService (typeof(IMenuCommandService), menuCommandService);
				container.AddService (typeof(ComponentSerializationService), new CodeDomComponentSerializationService (container));
			}

			this.formDesignerHost = this.surface.GetService (typeof(IDesignerHost)) as IDesignerHost;
			surface.BeginLoad (typeof(Form));
			Control view = (Control)surface.View;
			view.Dock = DockStyle.Fill;
			view.AllowDrop = true;
			toolBoxService.ToolBox.Dock = DockStyle.Fill;
            
			(this.formDesignerHost.RootComponent as Form).FormBorderStyle = FormBorderStyle.None;
			//(this.formDesignerHost.RootComponent as Form).BackColor = Color.FromArgb(192, 192, 192);

            
			if (_pageDatal == null)
				_pageDatal = new PageData (null, true);

			tabControl1 = (TabControl)formDesignerHost.CreateComponent (typeof(TabControl));
			_pageDatal.populateTab (tabControl1, formDesignerHost, 0, false);

			if (bReplace == true)
				toolStripForeignMap.Image = Properties.Resources.check;
			else
				toolStripForeignMap.Image = Properties.Resources.nosort;

			(this.formDesignerHost.RootComponent as Form).Controls.Add (tabControl1);
			(this.formDesignerHost.RootComponent as Form).Size = new Size (tabControl1.Size.Width, tabControl1.Size.Height);
           
			tabControl1.Dock = DockStyle.Fill;
            
			(this.formDesignerHost.RootComponent as Form).KeyPreview = true;

			selectionService = surface.GetService (typeof(ISelectionService)) as ISelectionService;
			selectionService.SelectionChanged += new EventHandler (selectionService_SelectionChanged);            

			splitContainer1.Panel1.Controls.Add (view);
			this.splitContainer2.Panel1.Controls.Add (toolBoxService.ToolBox);
            
			this.SelectionChange += new SelectionChangeEventHandler (CustomPage_SelectionChange);

			if (tabControl1.TabPages.Count > 0)
				selectionService.SetSelectedComponents (new object[] { tabControl1.TabPages [0] });

			IComponentChangeService componentChangeService = (IComponentChangeService)container.GetService (typeof(IComponentChangeService));
            
			componentChangeService.ComponentChanged += new ComponentChangedEventHandler (componentChangeService_ComponentChanged);
			;
		}

		void componentChangeService_ComponentChanged (object sender, ComponentChangedEventArgs e)
		{
			CustomPage_MouseUp ();
		}

        
		private void selectionService_SelectionChanged (object sender, EventArgs e)//选中后更改属性
		{
			object[] selection = new object[selectionService.SelectionCount];
			selectionService.GetSelectedComponents ().CopyTo (selection, 0);            
			if (selectionService.SelectionCount == 0) {
				propertyGrid1.SelectedObject = null;
				//propertyGrid1.SelectedObjects = selection;
			} else {
				CustomPropertyCollection collection = new CustomPropertyCollection ();
				#region control properties
				if (((Control)selection [0]) is TextBoxEx) {
					collection.Add (new CustomProperty ("关联字段", "RField", "数据", "该控件关联数据表中的字段", selection [0], typeof(FieldRelationEditor)));//, typeof(ScopeDropDownEditor)
					collection.Add (new CustomProperty ("字段列表", new string[] { "RelationFields" }, typeof(List<PmsField>), pmsFieldList, pmsFieldList, true, false, "数据", "该控件所在表单字段集合", selection [0], null));
					collection.Add (new CustomProperty ("控件类型", new string[] { "CtrlType" }, typeof(string), "TextBox", "TextBox", true, false, "数据", "该控件所类型", selection [0], null));
					collection.Add (new CustomProperty ("背景色", "BackColor", "外观", "背景色。", selection [0]));
					collection.Add (new CustomProperty ("颜色", "ForeColor", "外观", "前景色。", selection [0]));
					collection.Add (new CustomProperty ("字体", "Font", "外观", "字体。", selection [0]));
					collection.Add (new CustomProperty ("密码字符", "PasswordChar", "数据", "字体。", selection [0]));
					collection.Add (new CustomProperty ("位置", "Location", "外观", "位置。", selection [0]));
					collection.Add (new CustomProperty ("客户区大小", "ClientSize", "外观", "客户区大小。", selection [0]));
					collection.Add (new CustomProperty ("自动显示软键盘", "AutoShowScreenKeyboard", "行为", "在编辑时自动显示软键盘。", selection [0]));
					collection.CtrlType = (Control)selection [0];
					propertyGrid1.SelectedObject = collection;
				} else if (((Control)selection [0]) is ComboBoxEx) {
					collection.Add (new CustomProperty ("关联字段", "RField", "数据", "该控件关联数据表中的字段", selection [0], typeof(FieldRelationEditor)));//, 
					collection.Add (new CustomProperty ("字段列表", new string[] { "RelationFields" }, typeof(List<PmsField>), pmsFieldList, pmsFieldList, true, false, "数据", "该控件所在表单字段集合", selection [0], null));
					collection.Add (new CustomProperty ("控件类型", new string[] { "CtrlType" }, typeof(string), "ComboBox", "ComboBox", true, false, "数据", "该控件所类型", selection [0], null));
					collection.Add (new CustomProperty ("背景色", "BackColor", "外观", "背景色。", selection [0]));
					collection.Add (new CustomProperty ("颜色", "ForeColor", "外观", "前景色。", selection [0]));
					collection.Add (new CustomProperty ("字体", "Font", "外观", "字体。", selection [0]));
					collection.Add (new CustomProperty ("列表项", "Items", "数据", "该控件关联数据表中的字段，并对该字段进行解释", selection [0], typeof(ScopeDropDownEditor)));//, typeof(ScopeDropDownEditor)   , typeof(CollectionEditor)                 
					//collection.Add(new CustomProperty("下拉方式", "DropDownStyle", "外观", "下拉方式。", selection[0]));
					collection.Add (new CustomProperty ("位置", "Location", "外观", "位置。", selection [0]));
					collection.Add (new CustomProperty ("客户区大小", "ClientSize", "外观", "客户区大小。", selection [0]));
					//collection.Add(new CustomProperty("排序", "Sorted", "行为", "指定是否对组合框的列表部分进行排序。", selection[0]));
					//collection.Add(new CustomProperty("自动显示软键盘", "AutoShowScreenKeyboard", "行为", "在编辑时自动显示软键盘。", selection[0]));
					propertyGrid1.SelectedObject = collection;
				} else if (((Control)selection [0]) is CheckBoxEx) {
					collection.Add (new CustomProperty ("关联字段", "RField", "数据", "该控件关联数据表中的字段", selection [0], typeof(FieldRelationEditor)));//, typeof(ScopeDropDownEditor)                    
					collection.Add (new CustomProperty ("字段列表", new string[] { "RelationFields" }, typeof(List<PmsField>), pmsFieldList, pmsFieldList, true, false, "数据", "该控件所在表单字段集合", selection [0], null));
					collection.Add (new CustomProperty ("控件类型", new string[] { "CtrlType" }, typeof(string), "CheckBox", "CheckBox", true, false, "数据", "该控件所类型", selection [0], null));
					collection.Add (new CustomProperty ("标题", "Text", "数据", "标题", selection [0]));
					collection.Add (new CustomProperty ("背景色", "BackColor", "外观", "背景色。", selection [0]));
					collection.Add (new CustomProperty ("颜色", "ForeColor", "外观", "前景色。", selection [0]));
					collection.Add (new CustomProperty ("字体", "Font", "外观", "字体。", selection [0]));
					collection.Add (new CustomProperty ("位置", "Location", "外观", "位置。", selection [0]));
					collection.Add (new CustomProperty ("客户区大小", "ClientSize", "外观", "客户区大小。", selection [0]));
					propertyGrid1.SelectedObject = collection;
				} else if (((Control)selection [0]) is RadioButtonEx) {
					collection.Add (new CustomProperty ("关联字段", "RField", "数据", "该控件关联数据表中的字段", selection [0], typeof(FieldRelationEditor)));//, typeof(ScopeDropDownEditor)                    
					collection.Add (new CustomProperty ("字段列表", new string[] { "RelationFields" }, typeof(List<PmsField>), pmsFieldList, pmsFieldList, true, false, "数据", "该控件所在表单字段集合", selection [0], null));
					collection.Add (new CustomProperty ("控件类型", new string[] { "CtrlType" }, typeof(string), "RadioButton", "RadioButton", true, false, "数据", "该控件所类型", selection [0], null));
					collection.Add (new CustomProperty ("标题", "Text", "数据", "标题", selection [0]));
					collection.Add (new CustomProperty ("背景色", "BackColor", "外观", "背景色。", selection [0]));
					collection.Add (new CustomProperty ("颜色", "ForeColor", "外观", "前景色。", selection [0]));
					collection.Add (new CustomProperty ("字体", "Font", "外观", "字体。", selection [0]));
					collection.Add (new CustomProperty ("位置", "Location", "外观", "位置。", selection [0]));
					collection.Add (new CustomProperty ("客户区大小", "ClientSize", "外观", "客户区大小。", selection [0]));
					propertyGrid1.SelectedObject = collection;
				} else if (((Control)selection [0]) is GroupBoxEx) {
					collection.Add (new CustomProperty ("控件类型", new string[] { "CtrlType" }, typeof(string), "GroupBox", "GroupBox", true, false, "数据", "该控件所类型", selection [0], null));
					collection.Add (new CustomProperty ("标题", "Text", "数据", "标题", selection [0]));
					collection.Add (new CustomProperty ("背景色", "BackColor", "外观", "背景色。", selection [0]));
					collection.Add (new CustomProperty ("颜色", "ForeColor", "外观", "前景色。", selection [0]));
					collection.Add (new CustomProperty ("字体", "Font", "外观", "字体。", selection [0]));
					collection.Add (new CustomProperty ("位置", "Location", "外观", "位置。", selection [0]));
					collection.Add (new CustomProperty ("客户区大小", "ClientSize", "外观", "客户区大小。", selection [0]));
					propertyGrid1.SelectedObject = collection;
				} else if (((Control)selection [0]) is NumericUpDownEx) {
					collection.Add (new CustomProperty ("关联字段", "RField", "数据", "该控件关联数据表中的字段", selection [0], typeof(FieldRelationEditor)));//, typeof(ScopeDropDownEditor)                    
					collection.Add (new CustomProperty ("字段列表", new string[] { "RelationFields" }, typeof(List<PmsField>), pmsFieldList, pmsFieldList, true, false, "数据", "该控件所在表单字段集合", selection [0], null));
					collection.Add (new CustomProperty ("控件类型", new string[] { "CtrlType" }, typeof(string), "NumericUpDown", "NumericUpDown", true, false, "数据", "该控件所类型", selection [0], null));
					collection.Add (new CustomProperty ("背景色", "BackColor", "外观", "背景色。", selection [0]));
					collection.Add (new CustomProperty ("颜色", "ForeColor", "外观", "前景色。", selection [0]));
					collection.Add (new CustomProperty ("字体", "Font", "外观", "字体。", selection [0]));
					collection.Add (new CustomProperty ("位置", "Location", "外观", "位置。", selection [0]));
					collection.Add (new CustomProperty ("小数位数", "DecimalPlaces", "数据", "小数位数。", selection [0]));
					collection.Add (new CustomProperty ("最大值", "Maximum", "数据", "最大值", selection [0]));
					collection.Add (new CustomProperty ("最小值", "Minimum", "数据", "最大值", selection [0]));
					collection.Add (new CustomProperty ("步长", "Increment", "数据", "步长", selection [0]));
					collection.Add (new CustomProperty ("值", "Value", "数据", "值", selection [0]));
					collection.Add (new CustomProperty ("自动显示软键盘", "AutoShowScreenKeyboard", "行为", "在编辑时自动显示软键盘。", selection [0]));
					propertyGrid1.SelectedObject = collection;
				} else if (((Control)selection [0]) is LabelEx) {
					//collection.Add(new CustomProperty("标题", new string[] { "Text" }, typeof(string), "Label", ((LabelEx)selection[0]).Text, true, false, "数据", "标题", selection[0], null));
					collection.Add (new CustomProperty ("标题", "Text", "数据", "标题", selection [0]));
					collection.Add (new CustomProperty ("背景色", "BackColor", "外观", "背景色。", selection [0]));
					collection.Add (new CustomProperty ("颜色", "ForeColor", "外观", "前景色。", selection [0]));
					collection.Add (new CustomProperty ("字体", "Font", "外观", "字体。", selection [0]));
					collection.Add (new CustomProperty ("对齐方式", "TextAlign", "外观", "对齐方式。", selection [0]));
					collection.Add (new CustomProperty ("位置", "Location", "外观", "位置。", selection [0]));
					collection.Add (new CustomProperty ("客户区大小", "ClientSize", "外观", "客户区大小。", selection [0]));
					propertyGrid1.SelectedObject = collection;
				} else if (((Control)selection [0]) is TabPage) {
					collection.Add (new CustomProperty ("标签名", "Text", "外观", "该页标签", selection [0]));
					collection.Add (new CustomProperty ("背景色", "BackColor", "外观", "背景色。", selection [0]));
					propertyGrid1.SelectedObject = collection;
				} else if (((Control)selection [0]) is FileDisplay) {
					collection.Add (new CustomProperty ("关联字段", new string[] { "RField" }, typeof(string), "", "", false, true, "数据", "该控件关联数据表中的字段", selection [0], typeof(FieldRelationEditor)));
					collection.Add (new CustomProperty ("字段列表", new string[] { "RelationFields" }, typeof(List<PmsField>), pmsFieldList, pmsFieldList, true, false, "数据", "该控件所在表单字段集合", selection [0], null));
					collection.Add (new CustomProperty ("控件类型", new string[] { "CtrlType" }, typeof(string), "PictureBox", "PictureBox", true, false, "数据", "该控件所类型", selection [0], null));
					//collection.Add(new CustomProperty("关联字段", "RField", "外观", "该控件关联数据表中的字段", selection[0]));//, typeof(FieldRelationEditor)                     
					collection.Add (new CustomProperty ("背景色", "BackColor", "外观", "背景色。", selection [0]));
					collection.Add (new CustomProperty ("可操作", new string[] { "IsOperable" }, typeof(bool), true, true, false, true, "数据", "该控件是否可进行上传下载操作", selection [0], null));
					collection.Add (new CustomProperty ("位置", "Location", "外观", "位置。", selection [0]));
					collection.Add (new CustomProperty ("客户区大小", "ClientSize", "外观", "客户区大小。", selection [0]));

					propertyGrid1.SelectedObject = collection;
				} else if (((Control)selection [0]) is TabControl) {
					collection.Add (new CustomProperty ("标题", "Text", "外观", "弹出式对话框标题", selection [0]));
					collection.Add (new CustomProperty ("文本", new string[] { "Tag" }, typeof(object), null, _OkCancelText, false, true, "设置", "设置对话框确定、取消文本", selection [0], typeof(OkCancelTextEditor)));
					//collection.Add(new CustomProperty("背景色", "BackColor", "外观", "背景色。", selection[0]));
					propertyGrid1.SelectedObject = collection;
				} else if (((Control)selection [0]) is ForeignKeyCtrlEx) {
					collection.Add (new CustomProperty ("关联字段", "RField", "数据", "该控件关联数据表中的字段", selection [0], typeof(FieldRelationEditor)));//, 
					collection.Add (new CustomProperty ("字段列表", new string[] { "RelationFields" }, typeof(List<PmsField>), pmsFieldList, pmsFieldList, true, false, "数据", "该控件所在表单字段集合", selection [0], null));
					collection.Add (new CustomProperty ("控件类型", new string[] { "CtrlType" }, typeof(string), "ForeignKeyCtrl", "ForeignKeyCtrl", true, false, "数据", "该控件所类型", selection [0], null));
					collection.Add (new CustomProperty ("背景色", "BackColor", "外观", "背景色。", selection [0]));
					collection.Add (new CustomProperty ("颜色", "ForeColor", "外观", "前景色。", selection [0]));
					collection.Add (new CustomProperty ("字体", "Font", "外观", "字体。", selection [0]));
					//collection.Add(new CustomProperty("列表项", "Items", "数据", "该控件关联数据表中的字段，并对该字段进行解释", selection[0], typeof(ScopeDropDownEditor)));//, typeof(ScopeDropDownEditor)   , typeof(CollectionEditor)                 
					//collection.Add(new CustomProperty("下拉方式", "DropDownStyle", "外观", "下拉方式。", selection[0]));
					collection.Add (new CustomProperty ("位置", "Location", "外观", "位置。", selection [0]));
					collection.Add (new CustomProperty ("客户区大小", "ClientSize", "外观", "客户区大小。", selection [0]));
					propertyGrid1.SelectedObject = collection;
				}
				SelectionChange (this, new EventArgs ());
				//propertyGrid1.Tag = pmsFieldList;
				#endregion
			}
		}

		private void NewPage_Click (object sender, EventArgs e)
		{
			if (tabControl1 == null)
				return;
			bool bExec = false;
			for (int i = 0; i < tabControl1.TabPages.Count; i++) {
				bool bFind = false;
				string sheetName = string.Format ("sheet{0}", i + 1);
				for (int j = 0; j < tabControl1.TabPages.Count; j++) {
					TabPage tp = tabControl1.TabPages [j];
					if (tp.Text == sheetName) {
						bFind = true;
						break;
					}
				}
				if (bFind == true)
					continue;
				else {
					TabPage tp = new TabPage (sheetName);
					//tp.BackColor = Color.FromArgb(192, 192, 192);
					tabControl1.TabPages.Add (tp);
					bExec = true;
					break;
				}
			}
			if (bExec == false) {
				string sheetName = string.Format ("sheet{0}", tabControl1.TabPages.Count + 1);
				TabPage tp = new TabPage (sheetName);
				//tp.BackColor = Color.FromArgb(192, 192, 192);
				tabControl1.TabPages.Add (tp);
			}
		}

		private void DeletePage_Click (object sender, EventArgs e)
		{
			if (MessageBox.Show ("确实要删除该页吗？", "警告", MessageBoxButtons.YesNo) == DialogResult.No)
				return;
			//if (tabControl1.SelectedTab < 0)
			//return;
			tabControl1.TabPages.Remove (tabControl1.SelectedTab);
		}

		private void toolStripButton1_Click (object sender, EventArgs e)
		{
			propertyGrid1.SelectedObject = null;
			selectionService.SetSelectedComponents (new object[] { this.formDesignerHost.RootComponent });
		}

		#region 配置动态过滤条件接口

		public object[] GetSelectedCtrl ()
		{
			object[] controlCopy1 = new object[selectionService.SelectionCount];
			selectionService.GetSelectedComponents ().CopyTo (controlCopy1, 0);
			return controlCopy1;
		}

		public List<Control> GetCurPageCtrl ()
		{
			List<Control> lr = new List<Control> ();
			try {
				TabPage tp = tabControl1.SelectedTab;
				foreach (Control cmp in tp.Controls) {
					if (cmp is TextBoxEx || cmp is ComboBoxEx || cmp is CheckBoxEx) {
						lr.Add (cmp);
					}
				}
			} catch (Exception e) {
				MessageBox.Show (e.Message);
				lr = new List<Control> ();
			}
			return lr;
		}

		public bool SetSelectedCtrlTag (Guid id, string RField, string RType)
		{
			bool bret = false;
			try {
				IEnumerator ie = selectionService.GetSelectedComponents ().GetEnumerator ();
				while (ie.MoveNext ()) {
					Control cmp = (Control)ie.Current;
					if (cmp is TextBoxEx) {
						((TextBoxEx)cmp).RNode = id;
						((TextBoxEx)cmp).RField = RField;
						((TextBoxEx)cmp).Text = RField;
						((TextBoxEx)cmp).RType = RType;
						selectionService.SetSelectedComponents (new object[] { this.formDesignerHost.RootComponent });
						selectionService.SetSelectedComponents (new object[] { cmp });
						bret = true;
						break;
					}
					if (cmp is ComboBoxEx) {
						((ComboBoxEx)cmp).RNode = id;
						((ComboBoxEx)cmp).RField = RField;
						((ComboBoxEx)cmp).Text = RField;
						selectionService.SetSelectedComponents (new object[] { this.formDesignerHost.RootComponent });
						selectionService.SetSelectedComponents (new object[] { cmp });
						bret = true;
						break;
					}
					if (cmp is CheckBoxEx) {
						((CheckBoxEx)cmp).RNode = id;
						((CheckBoxEx)cmp).RField = RField;
						selectionService.SetSelectedComponents (new object[] { this.formDesignerHost.RootComponent });
						selectionService.SetSelectedComponents (new object[] { cmp });
						bret = true;
						break;
					}
				}
			} catch (Exception e) {
				MessageBox.Show (e.Message);
			}
			return bret;
		}

		public bool SetCtrlTagEmpty (Guid id)
		{
			foreach (TabPage tp in tabControl1.TabPages) {
				foreach (Control cmp in tp.Controls) {
					if (cmp is TextBoxEx) {
						if (((TextBoxEx)cmp).RNode == id) {
							((TextBoxEx)cmp).RNode = Guid.Empty;
							((TextBoxEx)cmp).Text = "";
							((TextBoxEx)cmp).RField = "";
							((TextBoxEx)cmp).RType = "";
							return true;
						}
					} else if (cmp is ComboBoxEx) {
						if (((ComboBoxEx)cmp).RNode == id) {
							((ComboBoxEx)cmp).RNode = Guid.Empty;
							((ComboBoxEx)cmp).Text = "";
							((ComboBoxEx)cmp).RField = "";
							return true;
						}
					} else if (cmp is CheckBoxEx) {
						if (((CheckBoxEx)cmp).RNode == id) {
							((CheckBoxEx)cmp).RNode = Guid.Empty;
							//((CheckBoxEx)cmp).Text = "";
							((CheckBoxEx)cmp).RField = "";
							return true;
						}
					}
				}
			}
			return false;
		}

		/// <summary>
		/// 根据id找到绑定控件
		/// </summary>
		/// <param name="id">guid</param>
		/// <param name="NoSelect">找到后是否选择找到控件</param>
		/// <returns>True，找到</returns>
		public bool FindRelatedCtrl (Guid id, bool NoSelect)
		{
			int iPage = 0;
			foreach (TabPage tabPage in tabControl1.TabPages) {
				IEnumerator enumerator = tabPage.Controls.GetEnumerator ();

				while (enumerator.MoveNext ()) {
					Control cmp = (Control)enumerator.Current;
					if (cmp is TextBoxEx) {
						TextBoxEx tb = (TextBoxEx)cmp;
						if (tb.RNode == id) {
							if (!NoSelect) {
								tabControl1.SelectedIndex = iPage;
								selectionService.SetSelectedComponents (new object[] { tb });
							}
							return true;
						}
					} else if (cmp is ComboBoxEx) {
						ComboBoxEx tb = (ComboBoxEx)cmp;
						if (tb.RNode == id) {
							if (!NoSelect) {
								tabControl1.SelectedIndex = iPage;
								selectionService.SetSelectedComponents (new object[] { tb });
							}
							return true;
						}
					} else if (cmp is CheckBoxEx) {
						CheckBoxEx tb = (CheckBoxEx)cmp;
						if (tb.RNode == id) {
							if (!NoSelect) {
								tabControl1.SelectedIndex = iPage;
								selectionService.SetSelectedComponents (new object[] { tb });
							}
							return true;
						}
					}
				}
				iPage++;
			}
			return false;
		}

		#endregion

		private void toolStripButton2_Click (object sender, EventArgs e)
		{
			controlCopy = new object[selectionService.SelectionCount];
			selectionService.GetSelectedComponents ().CopyTo (controlCopy, 0);
		}

		private void toolStripButton3_Click (object sender, EventArgs e)
		{
			foreach (object select in controlCopy) {
				if (select != null)
					CreateControl ((Control)select);
			}
		}

		private void CreateControl (Control tC)
		{
			if (tC is Control) {
				Type controlType = tC.GetType ();
				if (typeof(Control).IsAssignableFrom (controlType) && !typeof(Form).IsAssignableFrom (controlType)) {
					Control control = this.formDesignerHost.CreateComponent (controlType) as Control;
					control.Text = control.Name;
					Point pt = new Point (0, 0);
					pt.X = tC.Location.X + 15;
					pt.Y = tC.Location.Y + 15;
					control.Location = pt;
					control.ClientSize = tC.ClientSize;
					control.Enabled = true;
					control.BackColor = tC.BackColor;
					control.Font = tC.Font;
					control.ForeColor = tC.ForeColor;
					if (tC is TextBoxEx) {
						TextBoxEx c1 = (TextBoxEx)control;
						TextBoxEx c2 = (TextBoxEx)tC;
						c1.Text = c2.Text;
						c1.RField = c2.RField;
					} else if (tC is CheckBoxEx) {
						CheckBoxEx c1 = (CheckBoxEx)control;
						CheckBoxEx c2 = (CheckBoxEx)tC;
						c1.RField = c2.RField;
					} else if (tC is PictureBoxEx) {
						PictureBoxEx c1 = (PictureBoxEx)control;
						PictureBoxEx c2 = (PictureBoxEx)tC;
						c1.RField = c2.RField;
						c1.RName = c2.RName;
					} else if (tC is ComboBoxEx) {
						ComboBoxEx c1 = (ComboBoxEx)control;
						ComboBoxEx c2 = (ComboBoxEx)tC;
						c1.RField = c2.RField;
						string[] strList = new string[c2.Items.Count];
						c2.Items.CopyTo (strList, 0);
						c1.Items.AddRange (strList);
					} else if (tC is LabelEx) {
						LabelEx c1 = (LabelEx)control;
						LabelEx c2 = (LabelEx)tC;
						c1.Text = c2.Text;
						c1.TextAlign = c2.TextAlign;
					} else
						return;
					tabControl1.SelectedTab.Controls.Add (control);
					//(this.formDesignerHost.RootComponent as Form).Controls.Add(control);
				}
			}
		}

		private void toolStripButton4_Click (object sender, EventArgs e)
		{
			deleteControl ();
		}

		public void deleteControl ()
		{
			if (selectionService.GetComponentSelected (tabControl1))
				return;
			menuCommandService.GlobalInvoke (StandardCommands.Delete);
		}

		private void toolStripForeignMap_Click (object sender, EventArgs e)
		{
			if (bReplace == true) {
				bReplace = false;
				toolStripForeignMap.Image = Properties.Resources.nosort;
			} else {
				bReplace = true;
				toolStripForeignMap.Image = Properties.Resources.check;
				//设置关联字段
			}
		}
		//0x0312这个是window消息定义的   注册的热键消息
		protected override void WndProc (ref System.Windows.Forms.Message m)
		{
			switch (m.Msg) {
			case 0x0100://keydown  
				OnKeyDown (new KeyEventArgs ((Keys)(int)m.WParam));
                    
				break;
			}   
			base.WndProc (ref m);   
		}

		protected override void OnKeyDown (KeyEventArgs e)
		{
			Keys key = e.KeyCode;
			switch (key) {
			case Keys.Down:                    
				break;
			case Keys.Up: 
				break;
			case Keys.Enter:
				break;
			case Keys.Left:
				break;
			case Keys.Right:
				break;
			case Keys.Delete:
				if (!propertyGrid1.Focus ()) {
					if (selectionService.GetComponentSelected (tabControl1))
						return;
					menuCommandService.GlobalInvoke (StandardCommands.Delete);
				}
				break;
			}
			base.OnKeyDown (e);
		}

		private void ToolButtonDown (MouseEventArgs e, int tool)
		{
			ToolboxItem toolBoxItem;
			switch (tool) {
			case 1:
				toolBoxItem = new ToolboxItem (typeof(LabelEx));
				break;
			case 2:
				toolBoxItem = new ToolboxItem (typeof(TextBoxEx));
				break;
			case 3:
				toolBoxItem = new ToolboxItem (typeof(ComboBoxEx));
				break;
			case 4:
				toolBoxItem = new ToolboxItem (typeof(CheckBoxEx));
				break;
			case 5:
				toolBoxItem = new ToolboxItem (typeof(FileDisplay));
				break;
			case 6:
				toolBoxItem = new ToolboxItem (typeof(ForeignKeyCtrlEx));
				break;
			case 7:
				toolBoxItem = new ToolboxItem (typeof(GroupBoxEx));
				break;
			case 8:
				toolBoxItem = new ToolboxItem (typeof(RadioButtonEx));
				break;
			case 9:
				toolBoxItem = new ToolboxItem (typeof(NumericUpDownEx));
				break;
			default:
				toolBoxService.ToolBox.SelectedIndex = 0;
				return;
			}
			if (e.Clicks == 1) {
				IToolboxService tbs = toolBoxService;
				DataObject d = tbs.SerializeToolboxItem (toolBoxItem) as DataObject;

				try {
					DragDropEffects de = toolStripButton6.DoDragDrop (d, DragDropEffects.Copy);
					if (de == DragDropEffects.None) {
						toolBoxService.ToolBox.SelectedIndex = tool;
					}
				} catch (Exception ex) {
					MessageBox.Show (ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			} else if (e.Clicks == 2) {
				IDesignerHost idh = (IDesignerHost)this.surface.GetService (typeof(IDesignerHost));
				IToolboxUser tbu = idh.GetDesigner (idh.RootComponent as IComponent) as IToolboxUser;

				if (tbu != null) {
					tbu.ToolPicked (toolBoxItem);
				}
			}
		}

		private void toolStripButton5_Click (object sender, EventArgs e)
		{
			toolBoxService.ToolBox.SelectedIndex = 0;
		}

		private void toolStripButton10_MouseDown (object sender, MouseEventArgs e)
		{
			ToolStripButton button = (ToolStripButton)sender;

			if (button.Name == "toolStripButton6") {
				ToolButtonDown (e, 2);
			} else if (button.Name == "toolStripButton10") {
				ToolButtonDown (e, 1);
			} else if (button.Name == "toolStripButton7") {
				ToolButtonDown (e, 3);
			} else if (button.Name == "toolStripButton8") {
				ToolButtonDown (e, 5);
			} else if (button.Name == "toolStripButton9") {
				ToolButtonDown (e, 6);
			} else if (button.Name == "toolStripButton11") {
				ToolButtonDown (e, 4);
			} else if (button.Name == "toolStripButton12") {
				ToolButtonDown (e, 7);
			} else if (button.Name == "toolStripButton13") {
				ToolButtonDown (e, 8);
			} else if (button.Name == "toolStripButton14") {
				ToolButtonDown (e, 9);
			}
		}

		private void propertyGrid1_PropertyValueChanged (object s, PropertyValueChangedEventArgs e)
		{
			object[] selects = new object[selectionService.SelectionCount];
			selectionService.GetSelectedComponents ().CopyTo (selects, 0);
            
			selectionService.SetSelectedComponents (new object[] { this.formDesignerHost.RootComponent });
			selectionService.SetSelectedComponents (selects);

		}

		void CustomPage_MouseUp ()
		{
			object[] selects = new object[selectionService.SelectionCount];
			selectionService.GetSelectedComponents ().CopyTo (selects, 0);

			selectionService.SetSelectedComponents (new object[] { this.formDesignerHost.RootComponent });
			selectionService.SetSelectedComponents (selects);
		}
	}


	[Serializable]
	public class PageData : IDisposable
	{
		public PageData (TabControl tc, bool bReplace)
		{
			if (tc == null) {
				_pageSize.Height = 300;
				_pageSize.Width = 450;
				_bReplace = false;
				sheets = new TabSheet[1];// ("sheet1");
				sheets [0] = new TabSheet ();
				//DialogText = "详细信息";
				return;
			}
			sheets = new TabSheet[tc.TabPages.Count];
			int i = 0;
			foreach (TabPage tp in tc.TabPages) {
				TabSheet newSheet = new TabSheet (tp);

				sheets [i] = newSheet;
				i++;
			}
			_bReplace = bReplace;
			_pageSize = new Size (tc.Size.Width, tc.Size.Height);
			DialogText = tc.Text;
			OkCancelText = tc.Tag as Dictionary<string, string>;
		}
		//todo:qiuleilei
		public dynamic GetCtlInfo ()
		{
			dynamic form = new ExpandoObject ();
			form.Form = new ExpandoObject ();
			form.Form.Properties = new ExpandoObject ();
			form.Name = "Form1";
			form.Description = "过滤条件";
			form.Form.Type = "Form";
			form.Form.Properties.height = _pageSize.Height;
			form.Form.Properties.width = _pageSize.Width;

			List<dynamic> list = new List<dynamic> ();
			foreach (var s in sheets) {
				//s.controlList.Reverse ();
				dynamic tab = new ExpandoObject ();
				tab.Name = s._name;
				tab.Description = s._name;
				tab.Form = new ExpandoObject ();
				//tab.Form.Properties = new ExpandoObject ();
				tab.Form.Type = "TabControl";
				tab.Controls = GetCtls (s, s.controlList);
				list.Add (tab);
			}
			form.Controls = list;
			return form;
		}

		public int FontSize2Px (float fontSize)
		{
			return (int)System.Math.Floor (fontSize / 72 * 96);
		}

		public List<dynamic> GetCtls (TabSheet ts, CustomCtrlProperty[] ctls)
		{
			List<dynamic> ctlList = new List<dynamic> ();
			foreach (var c in ctls) {
				dynamic content = new ExpandoObject ();
				content.Form = new ExpandoObject ();
				content.Form = new ExpandoObject ();
				content.Form.Properties = new ExpandoObject ();
				content.Form.Properties.backgroundcolor = ColorTranslator.ToHtml (Color.FromArgb (c.BackColor.R, c.BackColor.G, c.BackColor.B));
				content.Form.Properties.color = ColorTranslator.ToHtml (Color.FromArgb (c.ForeColor.R, c.ForeColor.G, c.ForeColor.B));
				content.Form.Properties.left = c.Location.X;
				content.Form.Properties.top = c.Location.Y;
				content.Form.Properties.height = c.ClientSize.Height;
				content.Form.Properties.width = c.ClientSize.Width;
				content.Form.Properties.fontsize = FontSize2Px (c.Font1.Size);
				content.Form.Properties.fontfamily = c.Font1.FontFamily.Name;
				content.Name = "";
				content.Description = c.Text;
				content.BindVar = c.RField;
				content.DefaultValue = c.Value;

				var ctlType = c.CtrlType.ToString ();
				if (ctlType.EndsWith ("LabelEx")) {
					content.Form.Type = "Label";

				} else if (ctlType.EndsWith ("TextBoxEx")) {
					content.Form.Type = "SingleLine-Text";
				} else if (ctlType.EndsWith ("ComboBoxEx")) {
					content.Form.Type = "ComboBox";
					content.Form.Options = GetCombBoxItems (ts, c);
				} else if (ctlType.EndsWith ("CheckBoxEx")) {
					content.Form.Type = "CheckBox";
					List<dynamic> ops = new List<dynamic> ();
					dynamic item = new ExpandoObject ();
					item.value = c.Text;
					item.key = false;
					ops.Add (item);
					content.Form.Options = ops;
				} else if (ctlType.EndsWith ("GroupBoxEx")) {
					content.Form.Type = "GroupBox";
					if (c.Childs != null && c.Childs.Count > 0) {
						content.Controls = GetCtls (ts, c.Childs.ToArray ());
					}
				} else if (ctlType.EndsWith ("RadioButtonEx")) {
					content.Form.Type = "RadioButton";
					List<dynamic> ops = new List<dynamic> ();
					dynamic item = new ExpandoObject ();
					item.value = c.Text;
					item.key = false;
					ops.Add (item);
					content.Form.Options = ops;
				} else if (ctlType.EndsWith ("NumericUpDownEx")) {
					content.Form.Type = "NumericUpDown";
					content.Form.Properties.min = c.Minimum;
					content.Form.Properties.max = c.Maximum;
				} 

				if (c.RType == "DATETIME") {
					content.Form.Type = "DateTimePicker";
				}
				ctlList.Insert (0, content);
			}
			//ctlList.Sort ((x, y) => Comparer.Default.Compare (x.Form.Properties.top, y.Form.Properties.top) * Comparer.Default.Compare (x.Form.Properties.left, y.Form.Properties.left));
			return ctlList;
		}

		private List<dynamic> GetCombBoxItems (TabSheet ts, CustomCtrlProperty ccp)
		{
			List<dynamic> ops = new List<dynamic> ();
			if (null != ccp.ExplainData) {
				if (ccp.ExplainData.IsSolid) {
					foreach (TableField tf in ccp.ExplainData.ExplainList) {
						dynamic item = new ExpandoObject ();
						if (string.IsNullOrEmpty (tf.fieldName)) {
							item.value = tf.tableName;
						} else {
							item.value = tf.fieldName;
						}
						item.key = tf.tableName;
						ops.Add (item);
					}
				} else {

					try {
						DataTable dt = ts.queryData (ccp.ExplainData);
						if (dt != null && dt.Rows.Count > 0) {
							foreach (DataRow dr in dt.Rows) {
								dynamic item = new ExpandoObject ();
								item.value = item.key = dr [0].ToString ();
								ops.Add (item);
							}
						}
					} catch (Exception ex) {
						
					} 
				}
			}
			return ops;
		}

		public void Dispose ()
		{
			if (null != Connections) {
				Connections.Clear ();
			}
			if (null != pmsDisplayList) {
				pmsDisplayList.Clear ();
			}

			if (null != sheets) {
				foreach (TabSheet sheet in sheets) {
					sheet.Dispose ();
				}
			}
		}

		public void populateTab (TabControl tc, IDesignerHost formDesignerHost, int runMode, bool popDisplay)
		{
			if (this.sheets == null || this.sheets.Length == 0) {
				return;
			}
			tc.TabPages.Clear ();
			for (int i = 0; i <= this.sheets.Length - 1; i++) {
				this.sheets [i].TableName = TableName;
				this.sheets [i].pmsDisplayList = pmsDisplayList;
				this.sheets [i].FilterDataSet = FilterDataSet;
				this.sheets [i].Connections = Connections;
				TabPage tp = this.sheets [i].ToTabPage (formDesignerHost, runMode, _bReplace, popDisplay);
				if (runMode == 1) {//运行时,让焦点**
					if (FilterDataSet == null) {
						FilterDataSet = new DataSet ();
					}
					if (this.sheets [i].FilterDataSet != null) {
						FilterDataSet.Merge (this.sheets [i].FilterDataSet);
					}
					tp.Click += new EventHandler (tp_Click);
				}
				tc.TabPages.Add (tp);
			}
			tc.Size = new Size (_pageSize.Width, _pageSize.Height);
			tc.Text = DialogText;
			tc.Tag = this.OkCancelText;
		}

		void tp_Click (object sender, EventArgs e)
		{
			TabPage tp = (TabPage)sender;
			tp.Focus ();
		}

		public List<string> GetRelatedFieldName ()
		{
			List<string> rfn = new List<string> ();
			for (int i = 0; i < sheets.Length; i++) {
				for (int j = 0; j < sheets [i].controlList.Length; j++) {
					rfn.Add (sheets [i].controlList [j].RField);
				}
			}
			return rfn;
		}

		public override string ToString ()
		{
			return "详细信息配置";
		}

		/// <summary>
		/// 是否有控件
		/// 2010-6-5
		/// </summary>
		/// <returns></returns>
		public bool IsNoCtrl ()
		{
			for (int i = 0; i < sheets.Length; i++) {
				TabSheet ts = sheets [i];
                
				if (ts.controlList.Length > 0)
					return true;
			}
			return false;
		}

		/// <summary>
		/// 是否有控件绑定活动查询条件
		/// 2010-3-29
		/// </summary>
		/// <returns></returns>
		public bool IsBindingActive ()
		{
			for (int i = 0; i < sheets.Length; i++) {
				TabSheet ts = sheets [i];
				for (int j = 0; j < ts.controlList.Length; j++) {
					CustomCtrlProperty ccp = ts.controlList [j];
					if (ccp.RNode != Guid.Empty)
						return true;
				}
			}
			return false;
		}
		//是否有枚举节点绑定组合框，是否枚举在外层判断
		public bool IsBindingComboBox (Guid id)
		{
			for (int i = 0; i < sheets.Length; i++) {
				TabSheet ts = sheets [i];
				for (int j = 0; j < ts.controlList.Length; j++) {
					CustomCtrlProperty ccp = ts.controlList [j];
					if (ccp.RNode != Guid.Empty && id == ccp.RNode && ccp.CtrlType.Name == "ComboBoxEx")
						return true;
				}
			}
			return false;
		}
		//重新设置绑定了枚举节点的组合框的下拉列表信息，是否枚举在外层判断
		public bool SetBindingComboBoxList (Guid id, List<string> list)
		{
			for (int i = 0; i < sheets.Length; i++) {
				TabSheet ts = sheets [i];
				for (int j = 0; j < ts.controlList.Length; j++) {
					CustomCtrlProperty ccp = ts.controlList [j];
					if (ccp.RNode != Guid.Empty && id == ccp.RNode) {
						if (ccp.ExplainData == null)
							ccp.ExplainData = new ComboBoxItemData ();

						ccp.ExplainData.IsSolid = true;
						foreach (string v in list) {
							TableField tf = new TableField ();
							tf.tableName = v;
							ccp.ExplainData.ExplainList.Add (tf);
						}
						return true;
					}
				}
			}
			return false;
		}
		//tab size
		private Size _pageSize;

		public Size PageSize {
			get {
				return _pageSize;
			}
		}

		public string DialogText { get; set; }

		public Dictionary<string, string> OkCancelText { get; set; }

		private bool _bReplace;

		public bool BReplace {
			get { return _bReplace; }
			set { _bReplace = value; }
		}

		private TabSheet[] sheets;

		[NonSerialized]
		public List<PmsDisplay> pmsDisplayList;
		public string TableName;
		//2012.04.24增加 传递内存连接
		public List<PMSRefDBConnectionObj> Connections {
			get;
			set;
		}
		//2012.04.25增加 传递过滤条件数据集
		public DataSet FilterDataSet {
			get;
			set;
		}
	}

	[Serializable]
	public class TabSheet : IDisposable
	{
		public string _name;
		public Color _backColor;
		public CustomCtrlProperty[] controlList;
		[NonSerialized]
		public List<PmsDisplay> pmsDisplayList;
		public string TableName;
		private bool _bReplace = false;
		private bool _bPopDisplay = false;
		//2012.04.24增加 传递内存连接
		public List<PMSRefDBConnectionObj> Connections {
			get;
			set;
		}
		//2012.04.25增加 传递过滤条件数据集
		public DataSet FilterDataSet {
			get;
			set;
		}

		public void Dispose ()
		{
			if (null != Connections)
				Connections.Clear ();
			if (null != pmsDisplayList)
				pmsDisplayList.Clear ();
			if (null != FilterDataSet)
				FilterDataSet.Tables.Clear ();
		}

		public TabSheet ()
		{
			_name = "sheet1";
			//_backColor = Color.FromArgb(192, 192, 192);
			controlList = new CustomCtrlProperty[0];
		}

		/// <summary>
		/// 2012.1.30 修改
		/// 目的:增加一个GroupBox和一个RadioButton控件后需要调整相应影响代码
		/// 董平
		public TabSheet (TabPage tp)
		{
			_name = tp.Text;
			_backColor = tp.BackColor;
			controlList = new CustomCtrlProperty[tp.Controls.Count];

			for (int i = 0; i < tp.Controls.Count; i++) {
				Control cmp = tp.Controls [i];
				controlList [i] = new CustomCtrlProperty ();
				controlList [i].BackColor = cmp.BackColor;
				controlList [i].ClientSize = cmp.ClientSize;
				controlList [i].CtrlType = cmp.GetType ();
				controlList [i].Font1 = cmp.Font;
				controlList [i].ForeColor = cmp.ForeColor;
				controlList [i].Location = cmp.Location;
				controlList [i].Text = cmp.Text;
				if (controlList [i].CtrlType.Name == "CheckBoxEx") {
					controlList [i].RField = ((CheckBoxEx)cmp).RField;
					controlList [i].RNode = ((CheckBoxEx)cmp).RNode;                    
				} else if (controlList [i].CtrlType.Name == "RadioButtonEx") {
					controlList [i].RField = ((RadioButtonEx)cmp).RField;
					controlList [i].RNode = ((RadioButtonEx)cmp).RNode;   
				} else if (controlList [i].CtrlType.Name == "NumericUpDownEx") {
					controlList [i].RField = ((NumericUpDownEx)cmp).RField;
					controlList [i].RNode = ((NumericUpDownEx)cmp).RNode;
					controlList [i].DecimalPlaces = ((NumericUpDownEx)cmp).DecimalPlaces;
					controlList [i].Maximum = ((NumericUpDownEx)cmp).Maximum;
					controlList [i].Minimum = ((NumericUpDownEx)cmp).Minimum;
					controlList [i].Increment = ((NumericUpDownEx)cmp).Increment;
					controlList [i].Value = ((NumericUpDownEx)cmp).Value;
					controlList [i].AutoShowScreenKeyboard = ((NumericUpDownEx)cmp).AutoShowScreenKeyboard;
				} else if (controlList [i].CtrlType.Name == "GroupBoxEx") {
					if (cmp.HasChildren && cmp.Controls != null) {
						DealWithChild (cmp.Controls, controlList [i]);
					}
				} else if (controlList [i].CtrlType.Name == "TextBoxEx") {
					controlList [i].RField = ((TextBoxEx)cmp).RField;
					controlList [i].RType = ((TextBoxEx)cmp).RType;
					controlList [i].PasswordChar = ((TextBoxEx)cmp).PasswordChar;
					controlList [i].RNode = ((TextBoxEx)cmp).RNode;
					controlList [i].AutoShowScreenKeyboard = ((TextBoxEx)cmp).AutoShowScreenKeyboard;
				} else if (controlList [i].CtrlType.Name == "ComboBoxEx") {
					controlList [i].RField = ((ComboBoxEx)cmp).RField;                    
					if (((ComboBoxEx)cmp).Items.Count > 0)
						controlList [i].ExplainData = (((ComboBoxEx)cmp).Items [0]) as ComboBoxItemData;
					controlList [i].DropDownStyle = ((ComboBoxEx)cmp).DropDownStyle;
					controlList [i].RNode = ((ComboBoxEx)cmp).RNode;
					controlList [i].Sorted = ((ComboBoxEx)cmp).Sorted;
					controlList [i].AutoShowScreenKeyboard = ((ComboBoxEx)cmp).AutoShowScreenKeyboard;
				} else if (controlList [i].CtrlType.Name == "PictureBoxEx") {
					controlList [i].RField = ((PictureBoxEx)cmp).RField;
					controlList [i].RName = ((PictureBoxEx)cmp).RName;
					controlList [i].SizeMode = ((PictureBoxEx)cmp).SizeMode;
				} else if (controlList [i].CtrlType.Name == "FileDisplay") {
					controlList [i].RField = ((FileDisplay)cmp).RField;
				} else if (controlList [i].CtrlType.Name == "ForeignKeyCtrlEx") {
					controlList [i].RField = ((ForeignKeyCtrlEx)cmp).RField;
				} else if (controlList [i].CtrlType.Name == "LabelEx") {
					controlList [i].TextAlign = ((LabelEx)cmp).TextAlign;
				}
			}
		}

		/// <summary>
		/// 2012.1.30 增加
		/// 目的:以前在考虑控件时,没有考虑过容器控件,现在要加入容器控件,就需要考虑
		/// 递归处理
		/// </summary>
		/// <param name="Aim">子控件集合</param>
		/// <param name="father">序列化结构父节点</param>
		/// <returns>返回处理结果</returns>
		private int DealWithChild (Control.ControlCollection Aim, CustomCtrlProperty father)
		{
			int result = 0;
			if (Aim != null && father != null) {
				if (father.Childs == null) {
					father.Childs = new List<CustomCtrlProperty> ();
				} else {
					father.Childs.Clear ();
				}
				foreach (Control ctr in Aim) {
					CustomCtrlProperty controlList = new CustomCtrlProperty ();
					controlList.BackColor = ctr.BackColor;
					controlList.ClientSize = ctr.ClientSize;
					controlList.CtrlType = ctr.GetType ();
					controlList.Font1 = ctr.Font;
					controlList.ForeColor = ctr.ForeColor;
					controlList.Location = ctr.Location;
					controlList.Text = ctr.Text;
					if (controlList.CtrlType.Name == "CheckBoxEx") {
						controlList.RField = ((CheckBoxEx)ctr).RField;
						controlList.RNode = ((CheckBoxEx)ctr).RNode;
					} else if (controlList.CtrlType.Name == "RadioButtonEx") {
						controlList.RField = ((RadioButtonEx)ctr).RField;
						controlList.RNode = ((RadioButtonEx)ctr).RNode;
					} else if (controlList.CtrlType.Name == "GroupBoxEx") {
						if (ctr.HasChildren && ctr.Controls != null) {
							DealWithChild (ctr.Controls, controlList);
						}
					} else if (controlList.CtrlType.Name == "NumericUpDownEx") {
						controlList.RField = ((NumericUpDownEx)ctr).RField;
						controlList.RNode = ((NumericUpDownEx)ctr).RNode;
						controlList.DecimalPlaces = ((NumericUpDownEx)ctr).DecimalPlaces;
						controlList.Maximum = ((NumericUpDownEx)ctr).Maximum;
						controlList.Minimum = ((NumericUpDownEx)ctr).Minimum;
						controlList.Increment = ((NumericUpDownEx)ctr).Increment;
						controlList.Value = ((NumericUpDownEx)ctr).Value;
					} else if (controlList.CtrlType.Name == "TextBoxEx") {
						controlList.RField = ((TextBoxEx)ctr).RField;
						controlList.RType = ((TextBoxEx)ctr).RType;
						controlList.PasswordChar = ((TextBoxEx)ctr).PasswordChar;
						controlList.RNode = ((TextBoxEx)ctr).RNode;
						controlList.AutoShowScreenKeyboard = ((TextBoxEx)ctr).AutoShowScreenKeyboard;
					} else if (controlList.CtrlType.Name == "ComboBoxEx") {
						controlList.RField = ((ComboBoxEx)ctr).RField;
						if (((ComboBoxEx)ctr).Items.Count > 0)
							controlList.ExplainData = (((ComboBoxEx)ctr).Items [0]) as ComboBoxItemData;
						controlList.DropDownStyle = ((ComboBoxEx)ctr).DropDownStyle;
						controlList.RNode = ((ComboBoxEx)ctr).RNode;
						controlList.Sorted = ((ComboBoxEx)ctr).Sorted;
					} else if (controlList.CtrlType.Name == "PictureBoxEx") {
						controlList.RField = ((PictureBoxEx)ctr).RField;
						controlList.RName = ((PictureBoxEx)ctr).RName;
						controlList.SizeMode = ((PictureBoxEx)ctr).SizeMode;
					} else if (controlList.CtrlType.Name == "FileDisplay") {
						controlList.RField = ((FileDisplay)ctr).RField;
					} else if (controlList.CtrlType.Name == "ForeignKeyCtrlEx") {
						controlList.RField = ((ForeignKeyCtrlEx)ctr).RField;
					} else if (controlList.CtrlType.Name == "LabelEx") {
						controlList.TextAlign = ((LabelEx)ctr).TextAlign;
					}
					father.Childs.Add (controlList);
				}
			}
			return result;
		}

		/// <summary>
		/// 2012.1.30 修改
		/// 目的:增加一个GroupBox和一个RadioButton控件后需要调整相应影响代码
		/// 董平
		/// </summary>
		/// <param name="formDesignerHost"></param>
		/// <param name="runMode"></param>
		/// <param name="bReplace"></param>
		/// <param name="popDisplay"></param>
		/// <returns></returns>
		/// 2012.1.8 修改
		/// 目的:增加一个数字控件后需要调整相应的影响代码
		public TabPage ToTabPage (IDesignerHost formDesignerHost, int runMode, bool bReplace, bool popDisplay)
		{
			_bReplace = bReplace;
			_bPopDisplay = popDisplay;
			TabPage tp = new TabPage ();
			tp.Text = this._name;
			tp.BackColor = this._backColor;
			DateTimePicker dateTimePicker1 = null;
			if (runMode == 1) {
				tp.Text = this._name;
				tp.BackColor = this._backColor;

				dateTimePicker1 = new DateTimePicker ();
				dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
				dateTimePicker1.Format = DateTimePickerFormat.Custom;
				dateTimePicker1.Visible = false;
				//dateTimePicker1.VisibleChanged += new EventHandler(dateTimePicker1_VisibleChanged);
				dateTimePicker1.Leave += new EventHandler (dateTimePicker1_Leave);
				dateTimePicker1.Enter += new EventHandler (dateTimePicker1_Enter);
				dateTimePicker1.CloseUp += new EventHandler (dateTimePicker1_CloseUp);
				tp.Controls.Add (dateTimePicker1);
			}

			tp.AutoScroll = true;

			for (int i = 0; i < controlList.Length; i++) {
				CustomCtrlProperty ccp = controlList [i];
				object instance;
				if (formDesignerHost != null)
					instance = formDesignerHost.CreateComponent (ccp.CtrlType);
				else
					instance = null;

				#region control copy
				if (ccp.CtrlType.Name == "TextBoxEx") {
					TextBoxEx textBox = null;
					if (instance == null)
						textBox = new TextBoxEx ();
					else
						textBox = instance as TextBoxEx;
					textBox.BackColor = ccp.BackColor;
					textBox.ClientSize = ccp.ClientSize;
					if (runMode == 0) {
						textBox.Enabled = false;
						textBox.Text = ccp.Text;
					} else if (runMode == 1) {
						textBox.Enabled = true;
						textBox.Enter += new EventHandler (textBox_Enter);
						textBox.Leave += new EventHandler (textBox_Leave);
						bool bFind1 = false;
						foreach (var pd in pmsDisplayList) {
							//作为活动查询条件使用时，关联字段包含查询比较符和提示
							if (ccp.RField.StartsWith (pd.fieldName)) {
								textBox.RType = pd.fieldType;
								textBox.TableName = TableName;
								bFind1 = true;
								break;
							}
						}
						if (bFind1 == false)
							textBox.RType = ccp.RType;
					}

					textBox.Font = ccp.Font1;
					textBox.ForeColor = ccp.ForeColor;
					textBox.Location = ccp.Location;
					textBox.PasswordChar = ccp.PasswordChar;
					textBox.RField = ccp.RField;
					textBox.RNode = ccp.RNode;
					textBox.AutoShowScreenKeyboard = ccp.AutoShowScreenKeyboard;
					if (textBox.RType == "DATETIME")
					if (textBox.RField.IndexOf (".Year", StringComparison.CurrentCultureIgnoreCase) > 0 ||
					    textBox.RField.IndexOf (".Month", StringComparison.CurrentCultureIgnoreCase) > 0 ||
					    textBox.RField.IndexOf (".Day", StringComparison.CurrentCultureIgnoreCase) > 0 ||
					    textBox.RField.IndexOf (".Hour", StringComparison.CurrentCultureIgnoreCase) > 0 ||
					    textBox.RField.IndexOf (".Minute", StringComparison.CurrentCultureIgnoreCase) > 0 ||
					    textBox.RField.IndexOf (".Second", StringComparison.CurrentCultureIgnoreCase) > 0 ||
					    textBox.RField.IndexOf (".[Year]", StringComparison.CurrentCultureIgnoreCase) > 0 ||
					    textBox.RField.IndexOf (".[Month]", StringComparison.CurrentCultureIgnoreCase) > 0 ||
					    textBox.RField.IndexOf (".[Day]", StringComparison.CurrentCultureIgnoreCase) > 0 ||
					    textBox.RField.IndexOf (".[Hour]", StringComparison.CurrentCultureIgnoreCase) > 0 ||
					    textBox.RField.IndexOf (".[Minute]", StringComparison.CurrentCultureIgnoreCase) > 0 ||
					    textBox.RField.IndexOf (".[Second]", StringComparison.CurrentCultureIgnoreCase) > 0) {
						textBox.RType = "INT";
					}

					tp.Controls.Add ((Control)textBox);
				} else if (ccp.CtrlType.Name == "ComboBoxEx") {
					ComboBoxEx comboBoxEx = null;
					if (instance == null)
						comboBoxEx = new ComboBoxEx ();
					else
						comboBoxEx = instance as ComboBoxEx;
					comboBoxEx.BackColor = ccp.BackColor;
					comboBoxEx.ClientSize = ccp.ClientSize;
					comboBoxEx.Sorted = ccp.Sorted;

					if (runMode == 0) {
						comboBoxEx.Enabled = false;
						comboBoxEx.Text = ccp.Text;

						if (null != ccp.ExplainData) {
							comboBoxEx.Items.Add (ccp.ExplainData);
						}
					} else if (runMode == 1) {
						comboBoxEx.Enabled = true;
						comboBoxEx.Enter += new EventHandler (comboBoxEx_Enter);
						comboBoxEx.Leave += new EventHandler (comboBoxEx_Leave);
						if (null != ccp.ExplainData) {
							if (ccp.ExplainData.IsSolid) {
								foreach (TableField tf in ccp.ExplainData.ExplainList) {
									if (string.IsNullOrEmpty (tf.fieldName)) {
										comboBoxEx.Items.Add (tf.tableName);
									} else {
										comboBoxEx.Items.Add (tf.tableName + " -> " + tf.fieldName);
									}
								}
							} else {
								//2012.04.25 增加 对数据集本地序列化过滤条件的处理
								//2012.11.09 修改 以前在考虑是否重新查询的时候只考虑了判定表名 没有看此表中是否包括了所需要的字段
								if (FilterDataSet != null && FilterDataSet.Tables != null && FilterDataSet.Tables.Contains (ccp.ExplainData.tableName) && FilterDataSet.Tables [ccp.ExplainData.tableName].Columns.Contains (ccp.ExplainData.fieldName)) {
									DataTable dt = FilterDataSet.Tables [ccp.ExplainData.tableName];
									if (dt != null) {
										try {
											foreach (DataRow dr in dt.Rows) {
												comboBoxEx.Items.Add (dr [0].ToString ());
											}
										} catch {
										}
									}
								} else {
									DataTable dt = queryData (ccp.ExplainData);
									if (dt != null) {
										try {
											foreach (DataRow dr in dt.Rows) {
												comboBoxEx.Items.Add (dr [0].ToString ());
											}
											if (comboBoxEx.Items.Count > 0)
												comboBoxEx.SelectedIndex = 0;
										} catch {
										}
										if (FilterDataSet == null) {
											FilterDataSet = new DataSet ();
										}
										dt.TableName = ccp.ExplainData.tableName;
										if (!FilterDataSet.Tables.Contains (ccp.ExplainData.tableName)) {
											FilterDataSet.Tables.Add (dt);
										}
									}
								}   
							}
						}
					}
					comboBoxEx.Font = ccp.Font1;
					comboBoxEx.ForeColor = ccp.ForeColor;
					comboBoxEx.Location = ccp.Location;
					comboBoxEx.RField = ccp.RField;
					comboBoxEx.RNode = ccp.RNode;
					comboBoxEx.DropDownStyle = ComboBoxStyle.DropDownList;
					comboBoxEx.AutoShowScreenKeyboard = ccp.AutoShowScreenKeyboard;

					tp.Controls.Add ((Control)comboBoxEx);
				} else if (ccp.CtrlType.Name == "CheckBoxEx") {
					CheckBoxEx checkBoxEx = null;
					if (instance == null)
						checkBoxEx = new CheckBoxEx ();
					else
						checkBoxEx = instance as CheckBoxEx;
					checkBoxEx.BackColor = ccp.BackColor;
					checkBoxEx.ClientSize = ccp.ClientSize;
					if (runMode == 0) {
						checkBoxEx.Enabled = false;
						//checkBoxEx.Text = ccp.Text;
					} else if (runMode == 1)
						checkBoxEx.Enabled = true;
					checkBoxEx.Font = ccp.Font1;
					checkBoxEx.ForeColor = ccp.ForeColor;
					checkBoxEx.Location = ccp.Location;
					checkBoxEx.RField = ccp.RField;
					checkBoxEx.RNode = ccp.RNode;
					checkBoxEx.Text = ccp.Text;
					checkBoxEx.AllowDrop = true;
					tp.Controls.Add ((Control)checkBoxEx);
				} else if (ccp.CtrlType.Name == "RadioButtonEx") {
					RadioButtonEx checkBoxEx = null;
					if (instance == null)
						checkBoxEx = new RadioButtonEx ();
					else
						checkBoxEx = instance as RadioButtonEx;
					checkBoxEx.BackColor = ccp.BackColor;
					checkBoxEx.ClientSize = ccp.ClientSize;
					if (runMode == 0) {
						checkBoxEx.Enabled = false;
						//checkBoxEx.Text = ccp.Text;
					} else if (runMode == 1)
						checkBoxEx.Enabled = true;
					checkBoxEx.Font = ccp.Font1;
					checkBoxEx.ForeColor = ccp.ForeColor;
					checkBoxEx.Location = ccp.Location;
					checkBoxEx.RField = ccp.RField;
					checkBoxEx.RNode = ccp.RNode;
					checkBoxEx.Text = ccp.Text;
					checkBoxEx.AllowDrop = true;
					tp.Controls.Add ((Control)checkBoxEx);
				} else if (ccp.CtrlType.Name == "GroupBoxEx") {
					GroupBoxEx checkBoxEx = null;
					if (instance == null)
						checkBoxEx = new GroupBoxEx ();
					else
						checkBoxEx = instance as GroupBoxEx;
					checkBoxEx.BackColor = ccp.BackColor;
					checkBoxEx.ClientSize = ccp.ClientSize;
					if (runMode == 0) {
						checkBoxEx.Enabled = false;
						//checkBoxEx.Text = ccp.Text;
					} else if (runMode == 1)
						checkBoxEx.Enabled = true;
					checkBoxEx.Font = ccp.Font1;
					checkBoxEx.ForeColor = ccp.ForeColor;
					checkBoxEx.Location = ccp.Location;
					checkBoxEx.Text = ccp.Text;
					checkBoxEx.AllowDrop = true;
					checkBoxEx.MouseClick += new MouseEventHandler (checkBoxEx_MouseClick);
					checkBoxEx.MouseDown += new MouseEventHandler (checkBoxEx_MouseClick);
					if (ccp.Childs != null) {
						ToControl (checkBoxEx, ccp.Childs, runMode, bReplace, popDisplay, formDesignerHost);
					}
					tp.Controls.Add ((Control)checkBoxEx);
				} else if (ccp.CtrlType.Name == "NumericUpDownEx") {
					NumericUpDownEx checkBoxEx = null;
					if (instance == null)
						checkBoxEx = new NumericUpDownEx ();
					else
						checkBoxEx = instance as NumericUpDownEx;
					checkBoxEx.BackColor = ccp.BackColor;
					checkBoxEx.ClientSize = ccp.ClientSize;
					if (runMode == 0) {
						checkBoxEx.Enabled = false;
						//checkBoxEx.Text = ccp.Text;
					} else if (runMode == 1) {
						checkBoxEx.Enabled = true;
						checkBoxEx.Enter += new EventHandler (NumericUpDownEx_Enter);
						checkBoxEx.Leave += new EventHandler (NumericUpDownEx_Leave);
					}
					checkBoxEx.Font = ccp.Font1;
					checkBoxEx.ForeColor = ccp.ForeColor;
					checkBoxEx.Location = ccp.Location;
					checkBoxEx.RField = ccp.RField;
					checkBoxEx.RNode = ccp.RNode;
					checkBoxEx.AllowDrop = true;
					checkBoxEx.Maximum = ccp.Maximum;
					checkBoxEx.DecimalPlaces = ccp.DecimalPlaces;
					checkBoxEx.Minimum = ccp.Minimum;
					checkBoxEx.Increment = ccp.Increment;
					checkBoxEx.Value = ccp.Value;
					checkBoxEx.AutoShowScreenKeyboard = ccp.AutoShowScreenKeyboard;
					tp.Controls.Add ((Control)checkBoxEx);
				} else if (ccp.CtrlType.Name == "LabelEx") {
					LabelEx label = null;
					if (instance == null)
						label = new LabelEx ();
					else
						label = instance as LabelEx;
					label.BackColor = ccp.BackColor;
					label.ClientSize = ccp.ClientSize;
					if (runMode == 0)
						label.Enabled = false;
					else if (runMode == 1)
						label.Enabled = true;
					label.Font = ccp.Font1;
					label.ForeColor = ccp.ForeColor;
					label.Location = ccp.Location;
					label.Text = ccp.Text;
					label.TextAlign = ccp.TextAlign;
					tp.Controls.Add ((Control)label);
				} else if (ccp.CtrlType.Name == "PictureBoxEx") {
					PictureBoxEx picture = null;
					if (instance == null)
						picture = new PictureBoxEx (tp);
					else
						picture = instance as PictureBoxEx;
					picture.BackColor = ccp.BackColor;
					picture.ClientSize = ccp.ClientSize;
					picture.BorderStyle = BorderStyle.Fixed3D;
					if (runMode == 0)
						picture.Enabled = false;
					else if (runMode == 1) {
						picture.Enabled = true;
					}
					picture.Location = ccp.Location;
					picture.Text = ccp.Text;
					picture.RField = ccp.RField;
					picture.RName = ccp.RName;
					picture.SizeMode = ccp.SizeMode;
					tp.Controls.Add ((Control)picture);
				} else if (ccp.CtrlType.Name == "FileDisplay") {
					FileDisplay picture = null;
					if (instance == null)
						picture = new FileDisplay ();
					else
						picture = instance as FileDisplay;
					picture.BackColor = ccp.BackColor;
					picture.ClientSize = ccp.ClientSize;
					picture.BorderStyle = BorderStyle.Fixed3D;
					if (runMode == 0)
						picture.Enabled = false;
					else if (runMode == 1) {
						picture.Enabled = true;
						picture.BRunMode = true;

					}
					picture.Location = ccp.Location;
					picture.RField = ccp.RField;
					tp.Controls.Add ((Control)picture);
				} else if (ccp.CtrlType.Name == "ForeignKeyCtrlEx") {
					ForeignKeyCtrlEx comboBoxEx = null;
					if (instance == null)
						comboBoxEx = new ForeignKeyCtrlEx ();
					else
						comboBoxEx = instance as ForeignKeyCtrlEx;
					comboBoxEx.BackColor = ccp.BackColor;
					comboBoxEx.ClientSize = ccp.ClientSize;
					if (runMode == 0)
						comboBoxEx.Enabled = false;
					else if (runMode == 1) {
						comboBoxEx.Enabled = true;

						//TableField tableField = new TableField();
						//tableField.fieldName = ccp.RField;
						//tableField.tableName = TableName;
						//TableField strMainKey = SqlStructure.getMainKeyTable(tableField);

						//if (strMainKey.tableName == null || strMainKey.tableName.Length == 0)
						//continue;
						comboBoxEx.BSame = true;// (strMainKey.tableName == tableField.tableName);
						comboBoxEx.BReplace = this._bReplace;
						comboBoxEx.TableName = TableName;// strMainKey.tableName;
						//comboBoxEx.Enter += new EventHandler(comboBoxEx_Enter);
					}
					comboBoxEx.Font = ccp.Font1;
					comboBoxEx.RunMode = runMode;
					comboBoxEx.ForeColor = ccp.ForeColor;
					comboBoxEx.Location = ccp.Location;
					comboBoxEx.RField = ccp.RField;
					tp.Controls.Add ((Control)comboBoxEx);
				}
				#endregion
			}
			return tp;
		}

		void NumericUpDownEx_Leave (object sender, EventArgs e)
		{
			Environment.Osk.Hide ();
		}

		void NumericUpDownEx_Enter (object sender, EventArgs e)
		{
			if (null != sender && sender is NumericUpDownEx) {
				NumericUpDownEx dtp = sender as NumericUpDownEx;
				if (!dtp.AutoShowScreenKeyboard)
					return;
				Point pScreen = dtp.PointToScreen (new Point (0, 0));
				pScreen.Offset (0, dtp.Height + 10);
				Environment.Osk.ShowEx (0, pScreen.X, pScreen.Y);
				dtp.Focus ();
			}
		}

		void comboBoxEx_Leave (object sender, EventArgs e)
		{
			Environment.Osk.Hide ();
		}

		void dateTimePicker1_CloseUp (object sender, EventArgs e)
		{
			//throw new NotImplementedException();
			if (sender as Control != null) {
				(sender as Control).Focus ();
			}
		}

		void checkBoxEx_MouseClick (object sender, MouseEventArgs e)
		{
			//throw new NotImplementedException();
			if (sender as Control != null) {
				(sender as Control).Focus ();
			}
		}

		/// <summary>
		/// 2012.1.30 增加
		/// 目的:增加容器控件后,先要将控件层的东西通过一个结构序列化成文件
		/// 还要能通过序列化文件返回生成相应的控件
		/// </summary>
		/// <param name="father">容器控件</param>
		/// <param name="child">子控件信息</param>
		/// <returns>返回处理结果</returns>
		private int ToControl (Control father, List<CustomCtrlProperty> child, int runMode, bool bReplace, bool popDisplay, IDesignerHost formDesignerHost)
		{
			int result = 0;
			if (father != null && child != null) {
				if (father.Controls != null) {
					father.Controls.Clear ();
				}
				for (int i = 0; i < child.Count; i++) {
					Control Aim = null;
					CustomCtrlProperty ccp = child [i];
					object instance;
					if (formDesignerHost != null)
						instance = formDesignerHost.CreateComponent (ccp.CtrlType);
					else
						instance = null;

					if (ccp.CtrlType.Name == "TextBoxEx") {
						TextBoxEx textBox = null;
						if (instance == null)
							textBox = new TextBoxEx ();
						else
							textBox = instance as TextBoxEx;
						textBox.BackColor = ccp.BackColor;
						textBox.ClientSize = ccp.ClientSize;
						if (runMode == 0) {
							textBox.Enabled = false;
							textBox.Text = ccp.Text;
						} else if (runMode == 1) {
							textBox.Enabled = true;
							textBox.Enter += new EventHandler (textBox_Enter);
							textBox.Leave += new EventHandler (textBox_Leave);
							bool bFind1 = false;
							foreach (var pd in pmsDisplayList) {
								//作为活动查询条件使用时，关联字段包含查询比较符和提示
								if (ccp.RField.StartsWith (pd.fieldName)) {
									textBox.RType = pd.fieldType;
									textBox.TableName = TableName;
									bFind1 = true;
									break;
								}
							}
							if (bFind1 == false)
								textBox.RType = ccp.RType;
						}

						textBox.Font = ccp.Font1;
						textBox.ForeColor = ccp.ForeColor;
						textBox.Location = ccp.Location;
						textBox.PasswordChar = ccp.PasswordChar;
						textBox.RField = ccp.RField;
						textBox.RNode = ccp.RNode;
						textBox.AutoShowScreenKeyboard = ccp.AutoShowScreenKeyboard;
						if (textBox.RType == "DATETIME")
						if (textBox.RField.IndexOf (".Year", StringComparison.CurrentCultureIgnoreCase) > 0 ||
						    textBox.RField.IndexOf (".Month", StringComparison.CurrentCultureIgnoreCase) > 0 ||
						    textBox.RField.IndexOf (".Day", StringComparison.CurrentCultureIgnoreCase) > 0 ||
						    textBox.RField.IndexOf (".Hour", StringComparison.CurrentCultureIgnoreCase) > 0 ||
						    textBox.RField.IndexOf (".Minute", StringComparison.CurrentCultureIgnoreCase) > 0 ||
						    textBox.RField.IndexOf (".Second", StringComparison.CurrentCultureIgnoreCase) > 0 ||
						    textBox.RField.IndexOf (".[Year]", StringComparison.CurrentCultureIgnoreCase) > 0 ||
						    textBox.RField.IndexOf (".[Month]", StringComparison.CurrentCultureIgnoreCase) > 0 ||
						    textBox.RField.IndexOf (".[Day]", StringComparison.CurrentCultureIgnoreCase) > 0 ||
						    textBox.RField.IndexOf (".[Hour]", StringComparison.CurrentCultureIgnoreCase) > 0 ||
						    textBox.RField.IndexOf (".[Minute]", StringComparison.CurrentCultureIgnoreCase) > 0 ||
						    textBox.RField.IndexOf (".[Second]", StringComparison.CurrentCultureIgnoreCase) > 0) {
							textBox.RType = "INT";
						}

						Aim = (Control)textBox;
					} else if (ccp.CtrlType.Name == "ComboBoxEx") {
						ComboBoxEx comboBoxEx = null;
						if (instance == null)
							comboBoxEx = new ComboBoxEx ();
						else
							comboBoxEx = instance as ComboBoxEx;
						comboBoxEx.BackColor = ccp.BackColor;
						comboBoxEx.ClientSize = ccp.ClientSize;
						comboBoxEx.Sorted = ccp.Sorted;
						if (runMode == 0) {
							comboBoxEx.Enabled = false;
							comboBoxEx.Text = ccp.Text;

							if (null != ccp.ExplainData) {
								comboBoxEx.Items.Add (ccp.ExplainData);
							}
						} else if (runMode == 1) {
							comboBoxEx.Enabled = true;
							comboBoxEx.Enter += new EventHandler (comboBoxEx_Enter);
							comboBoxEx.Leave += new EventHandler (comboBoxEx_Leave);
							if (null != ccp.ExplainData) {
								if (ccp.ExplainData.IsSolid) {
									foreach (TableField tf in ccp.ExplainData.ExplainList) {
										if (string.IsNullOrEmpty (tf.fieldName)) {
											comboBoxEx.Items.Add (tf.tableName);
										} else {
											comboBoxEx.Items.Add (tf.tableName + " -> " + tf.fieldName);
										}
									}
								} else {
									DataTable dt = queryData (ccp.ExplainData);
									if (dt != null) {
										try {
											foreach (DataRow dr in dt.Rows) {
												comboBoxEx.Items.Add (dr [0].ToString ());
											}
										} catch {
										}
									}

								}
							}
						}
						comboBoxEx.Font = ccp.Font1;
						comboBoxEx.ForeColor = ccp.ForeColor;
						comboBoxEx.Location = ccp.Location;
						comboBoxEx.RField = ccp.RField;
						comboBoxEx.RNode = ccp.RNode;
						comboBoxEx.DropDownStyle = ComboBoxStyle.DropDownList;
						comboBoxEx.AutoShowScreenKeyboard = ccp.AutoShowScreenKeyboard;

						Aim = (Control)comboBoxEx;
					} else if (ccp.CtrlType.Name == "CheckBoxEx") {
						CheckBoxEx checkBoxEx = null;
						if (instance == null)
							checkBoxEx = new CheckBoxEx ();
						else
							checkBoxEx = instance as CheckBoxEx;
						checkBoxEx.BackColor = ccp.BackColor;
						checkBoxEx.ClientSize = ccp.ClientSize;
						if (runMode == 0) {
							checkBoxEx.Enabled = false;
							//checkBoxEx.Text = ccp.Text;
						} else if (runMode == 1)
							checkBoxEx.Enabled = true;
						checkBoxEx.Font = ccp.Font1;
						checkBoxEx.ForeColor = ccp.ForeColor;
						checkBoxEx.Location = ccp.Location;
						checkBoxEx.RField = ccp.RField;
						checkBoxEx.RNode = ccp.RNode;
						checkBoxEx.Text = ccp.Text;
						checkBoxEx.AllowDrop = true;
						Aim = (Control)checkBoxEx;
					} else if (ccp.CtrlType.Name == "RadioButtonEx") {
						RadioButtonEx checkBoxEx = null;
						if (instance == null)
							checkBoxEx = new RadioButtonEx ();
						else
							checkBoxEx = instance as RadioButtonEx;
						checkBoxEx.BackColor = ccp.BackColor;
						checkBoxEx.ClientSize = ccp.ClientSize;
						if (runMode == 0) {
							checkBoxEx.Enabled = false;
							//checkBoxEx.Text = ccp.Text;
						} else if (runMode == 1)
							checkBoxEx.Enabled = true;
						checkBoxEx.Font = ccp.Font1;
						checkBoxEx.ForeColor = ccp.ForeColor;
						checkBoxEx.Location = ccp.Location;
						checkBoxEx.RField = ccp.RField;
						checkBoxEx.RNode = ccp.RNode;
						checkBoxEx.Text = ccp.Text;
						checkBoxEx.AllowDrop = true;
						Aim = (Control)checkBoxEx;
					} else if (ccp.CtrlType.Name == "GroupBoxEx") {
						GroupBoxEx checkBoxEx = null;
						if (instance == null)
							checkBoxEx = new GroupBoxEx ();
						else
							checkBoxEx = instance as GroupBoxEx;
						checkBoxEx.BackColor = ccp.BackColor;
						checkBoxEx.ClientSize = ccp.ClientSize;
						if (runMode == 0) {
							checkBoxEx.Enabled = false;
							//checkBoxEx.Text = ccp.Text;
						} else if (runMode == 1)
							checkBoxEx.Enabled = true;
						checkBoxEx.Font = ccp.Font1;
						checkBoxEx.ForeColor = ccp.ForeColor;
						checkBoxEx.Location = ccp.Location;
						checkBoxEx.Text = ccp.Text;
						checkBoxEx.AllowDrop = true;
						Aim = (Control)checkBoxEx;
						if (ccp.Childs != null) {
							ToControl (Aim, ccp.Childs, runMode, bReplace, popDisplay, formDesignerHost);
						}
					} else if (ccp.CtrlType.Name == "NumericUpDownEx") {
						NumericUpDownEx checkBoxEx = null;
						if (instance == null)
							checkBoxEx = new NumericUpDownEx ();
						else
							checkBoxEx = instance as NumericUpDownEx;
						checkBoxEx.BackColor = ccp.BackColor;
						checkBoxEx.ClientSize = ccp.ClientSize;
						if (runMode == 0) {
							checkBoxEx.Enabled = false;
							//checkBoxEx.Text = ccp.Text;
						} else if (runMode == 1) {
							checkBoxEx.Enabled = true;
							checkBoxEx.Enter += new EventHandler (NumericUpDownEx_Enter);
							checkBoxEx.Leave += new EventHandler (NumericUpDownEx_Leave);
						}
						checkBoxEx.Font = ccp.Font1;
						checkBoxEx.ForeColor = ccp.ForeColor;
						checkBoxEx.Location = ccp.Location;
						checkBoxEx.RField = ccp.RField;
						checkBoxEx.RNode = ccp.RNode;
						checkBoxEx.Value = ccp.Value;
						checkBoxEx.Maximum = ccp.Maximum;
						checkBoxEx.Minimum = ccp.Minimum;
						checkBoxEx.DecimalPlaces = ccp.DecimalPlaces;
						checkBoxEx.Increment = ccp.Increment;
						checkBoxEx.AllowDrop = true;
						checkBoxEx.AutoShowScreenKeyboard = ccp.AutoShowScreenKeyboard;
						Aim = (Control)checkBoxEx;
					} else if (ccp.CtrlType.Name == "LabelEx") {
						LabelEx label = null;
						if (instance == null)
							label = new LabelEx ();
						else
							label = instance as LabelEx;
						label.BackColor = ccp.BackColor;
						label.ClientSize = ccp.ClientSize;
						if (runMode == 0)
							label.Enabled = false;
						else if (runMode == 1)
							label.Enabled = true;
						label.Font = ccp.Font1;
						label.ForeColor = ccp.ForeColor;
						label.Location = ccp.Location;
						label.Text = ccp.Text;
						label.TextAlign = ccp.TextAlign;
						Aim = (Control)label;
					} else if (ccp.CtrlType.Name == "PictureBoxEx") {
						PictureBoxEx picture = null;
						if (instance == null)
							picture = new PictureBoxEx (father);
						else
							picture = instance as PictureBoxEx;
						picture.BackColor = ccp.BackColor;
						picture.ClientSize = ccp.ClientSize;
						picture.BorderStyle = BorderStyle.Fixed3D;
						if (runMode == 0)
							picture.Enabled = false;
						else if (runMode == 1) {
							picture.Enabled = true;
						}
						picture.Location = ccp.Location;
						picture.Text = ccp.Text;
						picture.RField = ccp.RField;
						picture.RName = ccp.RName;
						picture.SizeMode = ccp.SizeMode;
						Aim = (Control)picture;
					} else if (ccp.CtrlType.Name == "FileDisplay") {
						FileDisplay picture = null;
						if (instance == null)
							picture = new FileDisplay ();
						else
							picture = instance as FileDisplay;
						picture.BackColor = ccp.BackColor;
						picture.ClientSize = ccp.ClientSize;
						picture.BorderStyle = BorderStyle.Fixed3D;
						if (runMode == 0)
							picture.Enabled = false;
						else if (runMode == 1) {
							picture.Enabled = true;
							picture.BRunMode = true;

						}
						picture.Location = ccp.Location;
						picture.RField = ccp.RField;
						Aim = (Control)picture;
					} else if (ccp.CtrlType.Name == "ForeignKeyCtrlEx") {
						ForeignKeyCtrlEx comboBoxEx = null;
						if (instance == null)
							comboBoxEx = new ForeignKeyCtrlEx ();
						else
							comboBoxEx = instance as ForeignKeyCtrlEx;
						comboBoxEx.BackColor = ccp.BackColor;
						comboBoxEx.ClientSize = ccp.ClientSize;
						if (runMode == 0)
							comboBoxEx.Enabled = false;
						else if (runMode == 1) {
							comboBoxEx.Enabled = true;

							//TableField tableField = new TableField();
							//tableField.fieldName = ccp.RField;
							//tableField.tableName = TableName;
							//TableField strMainKey = SqlStructure.getMainKeyTable(tableField);

							//if (strMainKey.tableName == null || strMainKey.tableName.Length == 0)
							//continue;
							comboBoxEx.BSame = true;// (strMainKey.tableName == tableField.tableName);
							comboBoxEx.BReplace = this._bReplace;
							comboBoxEx.TableName = TableName;// strMainKey.tableName;
							//comboBoxEx.Enter += new EventHandler(comboBoxEx_Enter);
						}
						comboBoxEx.Font = ccp.Font1;
						comboBoxEx.RunMode = runMode;
						comboBoxEx.ForeColor = ccp.ForeColor;
						comboBoxEx.Location = ccp.Location;
						comboBoxEx.RField = ccp.RField;
						Aim = (Control)comboBoxEx;
					}
					if (Aim != null) {
						father.Controls.Add (Aim);
					}
				}
			}
			return result;
		}

		public DataTable queryData (ComboBoxItemData datas)
		{
			DataTable dt = null;
			if (string.IsNullOrEmpty (datas.tableName) || string.IsNullOrEmpty (datas.fieldName))
				return dt;

			string DBString = datas.DBString;
			string sql = "SELECT DISTINCT " + datas.fieldName + " FROM " + datas.tableName;
			if (datas.SortType == SortType.ASC)
				sql += " ORDER BY " + datas.fieldName;
			else if (datas.SortType == SortType.DESC)
				sql += " ORDER BY " + datas.fieldName + " DESC";
			if (!string.IsNullOrEmpty (DBString)) {
				try {

					List<PMSRefDBConnectionObj> lpdb;
					if (Connections != null) {
						lpdb = Connections;
					} else {
						lpdb = PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetRefDBConnectionObjList ();
					}
					foreach (PMSRefDBConnectionObj pdb in lpdb) {
						if (pdb.StrName == DBString) {
							if (pdb.RefDBConnection == null)
								return null;

							OleDbConnection SqlConnection1 = pdb.RefDBConnection.GetOleConnection ();
							OleDbCommand sc = SqlConnection1.CreateCommand ();

							sc.CommandText = sql;
							OleDbDataReader reader = sc.ExecuteReader ();
							dt = new DataTable ();
							dt.Load (reader);
							reader.Close ();
							sc.Dispose ();
							return dt;
						}
					}
					SourceField.ReportError ("参数配置数据源设置错误，找不到", DBString);
                    
					return dt;
				} catch (Exception ex) {
					SourceField.ReportError ("参数配置查询数据源错误", ex.Message + sql);                    
					return dt;
				}
			} else {
				try {
					dt = PMSDBConnection.ExecuteCommand (sql);
				} catch (Exception ex) {
					SourceField.ReportError ("参数配置查询数据源错误", ex.Message + sql);
					return dt;
				}
			}
			return dt;
		}

		void comboBoxEx_Enter (object sender, EventArgs e)
		{
			//ForeignKeyCtrlEx fx = (ForeignKeyCtrlEx)sender;
			//fx.ResetTree();
			if (null != sender && sender is ComboBoxEx) {
				ComboBoxEx dtp = sender as ComboBoxEx;
				if (!dtp.AutoShowScreenKeyboard)
					return;
				Point pScreen = dtp.PointToScreen (new Point (0, 0));
				pScreen.Offset (0, dtp.Height + 10);
				Environment.Osk.ShowEx (1, pScreen.X, pScreen.Y);
				dtp.Focus ();
			}
		}

		private void dateTimePicker1_Leave (object sender, EventArgs e)
		{
			DateTimePicker foreignKeySelect1 = (DateTimePicker)sender;
			TextBoxEx tb = (TextBoxEx)foreignKeySelect1.Tag;
			tb.Text = foreignKeySelect1.Value.ToString ();
			Control tabP = tb.Parent;
			while (tabP != null) {
				if (tabP.GetType () == typeof(TabPage)) {
					foreignKeySelect1.Parent = tabP;
					break;
				}
				tabP = tabP.Parent;
			}
			foreignKeySelect1.Visible = false;
		}

		private void dateTimePicker1_Enter (object sender, EventArgs e)
		{
			DateTimePicker foreignKeySelect1 = (DateTimePicker)sender;
			if (foreignKeySelect1.Visible == false) {
				foreignKeySelect1.Visible = true;
				foreignKeySelect1.Focus ();
				TextBoxEx tb = (TextBoxEx)foreignKeySelect1.Tag;
				foreignKeySelect1.Parent = tb.Parent;
			}
		}

		private void textBox_Leave (object sender, EventArgs e)
		{
			TextBoxEx tb = (TextBoxEx)sender;
			//TabPage tabP = (TabPage)tb.Parent;
			Control tabP = null;
			if (tb.Parent != null && tb.Parent is Control) {
				tabP = tb.Parent as Control;
			}
			DateTimePicker dtp = null;

			while (tabP != null) {
				foreach (Control control in tabP.Controls) {
					if (control.GetType ().Name == "DateTimePicker")
						dtp = (DateTimePicker)control;
				}
				if (tabP.GetType () == typeof(TabPage)) {
					break;
				}
				tabP = tabP.Parent;
			}
			if (dtp != null && dtp.Visible == true) {
				tabP.Focus ();
				dtp.Visible = false;
				dtp.Parent = tabP;
			}
			Environment.Osk.Hide ();
		}

		private void textBox_Enter (object sender, EventArgs e)
		{
			TextBoxEx tb = (TextBoxEx)sender;

			int DateTimePickerHeight = 0;

			if (tb.RType == "DATETIME") {
				Control tabP = tb.Parent;
				DateTimePicker dtp = null;

				while (tabP != null) {
					foreach (Control control in tabP.Controls) {
						if (control.GetType ().Name == "DateTimePicker")
							dtp = (DateTimePicker)control;
					}
					tabP = tabP.Parent;
				}
				if (dtp != null) {
					dtp.Parent = tb.Parent;
					System.Drawing.Point pt = new System.Drawing.Point (tb.Location.X, tb.Location.Y);
					pt.X = tb.Location.X;
					pt.Y = tb.Location.Y + tb.Height + 1;
					dtp.Location = pt;
					try {
						dtp.Value = Convert.ToDateTime (tb.Text);//DateTime.ParseExact(
					} catch {
					}
					dtp.Visible = false;
					dtp.Visible = true;
					dtp.Tag = tb;

					DateTimePickerHeight = dtp.Height;
					if (tb.AutoShowScreenKeyboard && tb.Enabled && !tb.ReadOnly) {
						dtp.Enter += new EventHandler (dtp_Enter);
						dtp.Leave += new EventHandler (dtp_Leave);
					}
				}
			}

			if (tb.AutoShowScreenKeyboard && tb.Enabled && !tb.ReadOnly) {
				Point pScreen = tb.PointToScreen (new Point (0, 0));
				pScreen.Offset (0, tb.Height + DateTimePickerHeight + 10);
				Environment.Osk.ShowEx (1, pScreen.X, pScreen.Y);
				tb.Focus ();
			}
		}

		void dtp_Leave (object sender, EventArgs e)
		{
			Environment.Osk.Hide ();
		}

		void dtp_Enter (object sender, EventArgs e)
		{
			if (null != sender && sender is DateTimePicker) {
				DateTimePicker dtp = sender as DateTimePicker;
				Point pScreen = dtp.PointToScreen (new Point (0, 0));
				pScreen.Offset (0, dtp.Height + 10);
				Environment.Osk.ShowEx (1, pScreen.X, pScreen.Y);
				dtp.Focus ();
			}
            
		}

	}
}
