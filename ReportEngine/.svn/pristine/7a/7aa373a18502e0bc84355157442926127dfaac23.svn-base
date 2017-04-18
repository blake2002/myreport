using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using PMS.Libraries.ToolControls.ToolBox;
using Loader;
using MES.PublicInterface;
using PMS.Libraries.ToolControls.PMSPublicInfo;

namespace Host
{
	/// <summary>
	/// Inherits from DesignSurface and hosts the RootComponent and 
	/// all other designers. It also uses loaders (BasicDesignerLoader
	/// or CodeDomDesignerLoader) when required. It also provides various
	/// services to the designers. Adds MenuCommandService which is used
	/// for Cut, Copy, Paste, etc.
	/// </summary>
	public class HostSurface : DesignSurface
	{
		private const string _Name_ = "HostSurface";

		private DesignerLoader _loader = null;
		private ISelectionService _selectionService;
		private bool _ShouldUpdateSelectableObjects = false;
		private string _FilePath = null;
		private bool _Modified = false;
		private string _customFilePath = null;

		public Form rootForm {
			get { return ((MyCodeAndXMLHostLoader)_loader).rootForm; }
		}

		public string FilePath {
			get { return _FilePath; }
		}

		public string CustomFilePath {
			get { return _customFilePath; }
			set { _customFilePath = value; }
		}

		#region Function

		public Control SetSheetDesignerControlDock ()
		{
			foreach (Control ct in rootForm.Controls) {
				if (ct.Name.CompareTo (ToolboxLibrary.ToolboxItems.PmsSheetCtrlName) == 0) {
					ct.Dock = DockStyle.Fill;
					return ct;
				}
			}
			return null;
		}

		public Control SetOrgChartControlDock ()
		{
			foreach (Control ct in rootForm.Controls) {
				if (ct.Name.CompareTo (ToolboxLibrary.ToolboxItems.PmsOrgChartCtrlName) == 0) {
					ct.Dock = DockStyle.Fill;
					return ct;
				}
			}
			return null;
		}

		public void MakeSureControlVisible (string strCtrlName)
		{
			Control control = GetControl (strCtrlName);
			if (null != control) {
				Control ctrlView = this.View as Control;
				System.Windows.Forms.ScrollableControl scrollView = (((System.Windows.Forms.ScrollableControl)(ctrlView.Controls [0])));
				scrollView.ScrollControlIntoView (control);
				_selectionService.SetSelectedComponents (new object[] { control });

                
				//int x = 0 - ((System.Windows.Forms.ScrollProperties)(((System.Windows.Forms.ScrollableControl)(ctrlView.Controls[0])).HorizontalScroll)).Value;
				//int y = 0 - ((System.Windows.Forms.ScrollProperties)(((System.Windows.Forms.ScrollableControl)(ctrlView.Controls[0])).VerticalScroll)).Value;

			}
		}

		public Control GetControl (string strCtrlName)
		{
			foreach (Control ct in rootForm.Controls) {
				if (ct.Name.CompareTo (strCtrlName) == 0) {
					return ct;
				} else if (ct.HasChildren) {
					Control control = GetControl (strCtrlName, ct);
					if (null != control)
						return control;
				}
			}
			return null;
		}

		private Control GetControl (string strCtrlName, Control ctrl)
		{
			foreach (Control ct in ctrl.Controls) {
				if (ct.Name.CompareTo (strCtrlName) == 0) {
					return ct;
				} else if (ct.HasChildren) {
					Control control = GetControl (strCtrlName, ct);
					if (null != control)
						return control;
				}
			}
			return null;
		}

		//public Control GetSheetDesignerControl()
		//{
		//    foreach (Control ct in rootForm.Controls)
		//    {
		//        if (ct.Name.CompareTo(ToolboxLibrary.ToolboxItems.PmsSheetCtrlName) == 0)
		//        {
		//            ct.Dock = DockStyle.Fill;
		//            return ct;
		//        }
		//    }
		//    return null;
		//}

		public void Run ()
		{
			if (_loader != null)
				((MyCodeAndXMLHostLoader)_loader).Run ();
		}

		public string GetCode (string contex)
		{
			if (_loader != null)
				return ((MyCodeAndXMLHostLoader)_loader).GetCode (contex);
			return string.Empty;
		}

		public void SaveToXML (string fileFullPath)
		{
			if (_loader != null) {
				((MyCodeAndXMLHostLoader)_loader).Save (fileFullPath);
				_Modified = false;
			}
		}

		public void SetFilePath ()
		{
			//if (_loader != null)
			//    ((CodeDomHostLoader)_loader).Save();
		}

		#region IDeignSurfaceExt Members

		public void SwitchTabOrder ()
		{
			if (false == IsTabOrderMode) {
				InvokeTabOrder ();
				IsTabOrderMode = true;
			} else {
				DisposeTabOrder ();
				IsTabOrderMode = false;
			}
		}

		public void UseSnapLines ()
		{
			IServiceContainer serviceProvider = this.GetService (typeof(IServiceContainer)) as IServiceContainer;
			DesignerOptionService opsService = serviceProvider.GetService (typeof(DesignerOptionService)) as DesignerOptionService;
			if (null != opsService) {
				serviceProvider.RemoveService (typeof(DesignerOptionService));
			}
			DesignerOptionService opsService2 = new DesignerOptionServiceExt4SnapLines ();
			serviceProvider.AddService (typeof(DesignerOptionService), opsService2);
		}

		public void UseGrid (Size gridSize)
		{
			IServiceContainer serviceProvider = this.GetService (typeof(IServiceContainer)) as IServiceContainer;
			DesignerOptionService opsService = serviceProvider.GetService (typeof(DesignerOptionService)) as DesignerOptionService;
			if (null != opsService) {
				serviceProvider.RemoveService (typeof(DesignerOptionService));
			}
			DesignerOptionService opsService2 = new DesignerOptionServiceExt4Grid (gridSize);
			serviceProvider.AddService (typeof(DesignerOptionService), opsService2);
		}

		public void UseGridWithoutSnapping (Size gridSize)
		{
			IServiceContainer serviceProvider = this.GetService (typeof(IServiceContainer)) as IServiceContainer;
			DesignerOptionService opsService = serviceProvider.GetService (typeof(DesignerOptionService)) as DesignerOptionService;
			if (null != opsService) {
				serviceProvider.RemoveService (typeof(DesignerOptionService));
			}
			DesignerOptionService opsService2 = new DesignerOptionServiceExt4GridWithoutSnapping (gridSize);
			serviceProvider.AddService (typeof(DesignerOptionService), opsService2);
		}

		public void UseNoGuides ()
		{
			IServiceContainer serviceProvider = this.GetService (typeof(IServiceContainer)) as IServiceContainer;
			DesignerOptionService opsService = serviceProvider.GetService (typeof(DesignerOptionService)) as DesignerOptionService;
			if (null != opsService) {
				serviceProvider.RemoveService (typeof(DesignerOptionService));
			}
			DesignerOptionService opsService2 = new DesignerOptionServiceExt4NoGuides ();
			serviceProvider.AddService (typeof(DesignerOptionService), opsService2);

		}

		public UndoEngineExt GetUndoEngineExt ()
		{
			return this._undoEngine;
		}

		#endregion

		public IComponent CreateRootComponent (Type controlType, Size controlSize)
		{
			try {
				//- step.1
				//- get the IDesignerHost
				//- if we are not not able to get it 
				//- then rollback (return without do nothing)
				IDesignerHost host = GetIDesignerHost ();
				if (null == host)
					return null;
				//- check if the root component has already been set
				//- if so then rollback (return without do nothing)
				if (null != host.RootComponent)
					return null;
				//-
				//-
				//- step.2
				//- create a new root component and initialize it via its designer
				//- if the component has not a designer
				//- then rollback (return without do nothing)
				//- else do the initialization
                
				this.BeginLoad (controlType);
				if (this.LoadErrors.Count > 0)
					throw new Exception ("the BeginLoad() failed! Some error during " + controlType.ToString () + " loding");
				//-
				//-
				//- step.3
				//- try to modify the Size of the object just created
				IDesignerHost ihost = GetIDesignerHost ();
				//- Set the backcolor and the Size
				Control ctrl = null;
				Type hostType = host.RootComponent.GetType ();
				if (hostType == typeof(Form)) {
					ctrl = this.View as Control;
					ctrl.BackColor = Color.LightGray;
					//- set the Size
					PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties (ctrl);
					//- Sets a PropertyDescriptor to the specific property.
					PropertyDescriptor pdS = pdc.Find ("Size", false);
					if (null != pdS)
						pdS.SetValue (ihost.RootComponent, controlSize);
				} else if (hostType == typeof(UserControl)) {
					ctrl = this.View as Control;
					ctrl.BackColor = Color.DarkGray;
					//- set the Size
					PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties (ctrl);
					//- Sets a PropertyDescriptor to the specific property.
					PropertyDescriptor pdS = pdc.Find ("Size", false);
					if (null != pdS)
						pdS.SetValue (ihost.RootComponent, controlSize);
				} else if (hostType == typeof(Control)) {
					ctrl = this.View as Control;
					ctrl.BackColor = Color.LightGray;
					//- set the Size
					PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties (ctrl);
					//- Sets a PropertyDescriptor to the specific property
					PropertyDescriptor pdS = pdc.Find ("Size", false);
					if (null != pdS)
						pdS.SetValue (ihost.RootComponent, controlSize);
				} else if (hostType == typeof(Component)) {
					ctrl = this.View as Control;
					//ctrl.BackColor = Color.White;
					//- don't set the Size
				} else {
					throw new Exception ("Undefined Host Type: " + hostType.ToString ());
				}

				return ihost.RootComponent;
			}//end_try
            catch (Exception ex) {
				throw new Exception (_Name_ + "::CreateRootComponent() - Exception: (see Inner Exception)", ex);
			}//end_catch
		}

		public Control CreateControl (Type controlType, Size controlSize, Point controlLocation)
		{
			try {
				//- step.1
				//- get the IDesignerHost
				//- if we are not able to get it 
				//- then rollback (return without do nothing)
				IDesignerHost host = GetIDesignerHost ();
				if (null == host)
					return null;
				//- check if the root component has already been set
				//- if not so then rollback (return without do nothing)
				if (null == host.RootComponent)
					return null;
				//-
				//-
				//- step.2
				//- create a new component and initialize it via its designer
				//- if the component has not a designer
				//- then rollback (return without do nothing)
				//- else do the initialization
				IComponent newComp = host.CreateComponent (controlType);
				if (null == newComp)
					return null;
				IDesigner designer = host.GetDesigner (newComp);
				if (null == designer)
					return null;
				if (designer is IComponentInitializer)
					((IComponentInitializer)designer).InitializeNewComponent (null);
				//-
				//-
				//- step.3
				//- try to modify the Size/Location of the object just created
				PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties (newComp);
				//- Sets a PropertyDescriptor to the specific property.
				PropertyDescriptor pdS = pdc.Find ("Size", false);
				if (null != pdS)
					pdS.SetValue (newComp, controlSize);
				PropertyDescriptor pdL = pdc.Find ("Location", false);
				if (null != pdL)
					pdL.SetValue (newComp, controlLocation);
				//-
				//-
				//- step.4
				//- commit the Creation Operation
				//- adding the control to the DesignSurface's root component
				//- and return the control just created to let further initializations
				((Control)newComp).Parent = host.RootComponent as Control;
				return newComp as Control;
			}//end_try
            catch (Exception ex) {
				throw new Exception (_Name_ + "::CreateControl() - Exception: (see Inner Exception)", ex);
			}//end_catch
		}

		public IDesignerHost GetIDesignerHost ()
		{
			return (IDesignerHost)(this.GetService (typeof(IDesignerHost)));
		}

		public Control GetView ()
		{
			Control ctrl = this.View as Control;
			ctrl.Dock = DockStyle.Fill;
			return ctrl;
		}

		#endregion



		#region TabOrder

		private TabOrderHooker _tabOrder = null;
		private bool _tabOrderMode = false;

		public bool IsTabOrderMode {
			get { return _tabOrderMode; }
			set { _tabOrderMode = value; }
		}

		public TabOrderHooker TabOrder {
			get {
				if (_tabOrder == null)
					_tabOrder = new TabOrderHooker ();
				return _tabOrder;
			}
			set { _tabOrder = value; }
		}

		public void InvokeTabOrder ()
		{
			TabOrder.HookTabOrder (this.GetIDesignerHost ());
			_tabOrderMode = true;
		}

		public void DisposeTabOrder ()
		{
			TabOrder.HookTabOrder (this.GetIDesignerHost ());
			_tabOrderMode = false;
		}

		#endregion



		#region  UndoEngine

		//private UndoEngineImpl _undoEngine = null;
		private UndoEngineExt _undoEngine = null;
		private NameCreationServiceImpl _nameCreationService = null;
		private DesignerSerializationServiceImpl _designerSerializationService = null;
		private CodeDomComponentSerializationService _codeDomComponentSerializationService = null;

		// 自定义服务
		private MESDesignerService _MESDesignerService = null;

		#endregion

		private MenuCommandServiceImpl _menuCommandService = null;
		private TypeResolutionService _TypeResolutionService = null;
		private EventBindingServiceImpl _EventBindingService = null;

        

		//private bool _UseGrid = false;

		//[Category("xin")]
		//[Description("Gets or Sets the environment use grid.")]
		//[DefaultValue(false)]
		//[Browsable(true)]
		////[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		//public bool Usegrid
		//{
		//    get { return _UseGrid; }

		//    set
		//    {
		//        _UseGrid = value;
		//        if (_UseGrid == true)
		//        {
		//            UseGrid(new System.Drawing.Size(16, 16));
		//        }
		//        else
		//        {
		//            UseSnapLines();
		//        }
		//    }
		//}

		public HostSurface () : base ()
		{
			Initialize ();
		}

		public HostSurface (string strFilePath) : base ()
		{
			_FilePath = strFilePath;
			Initialize ();
		}

		private string _strXml = null;
		private int _justflag = -1;

		public HostSurface (string strXml, int justflag)
			: base ()
		{
			_strXml = strXml;
			_justflag = 1;
			Initialize ();
		}

		public HostSurface (string strXml, string strFilePath, int justflag)
			: base ()
		{
			_strXml = strXml;
			_justflag = 1;
			_customFilePath = strFilePath;
			Initialize ();
		}

		public HostSurface (string strXml, int justflag, bool UseAloneOnly)
			: base ()
		{
			_strXml = strXml;
			_justflag = 1;
			_UseAloneOnly = UseAloneOnly;
			Initialize ();
		}

		private bool _UseAloneOnly = false;

		public HostSurface (string strFilePath, bool UseAloneOnly)
			: base ()
		{
			_FilePath = strFilePath;
			_UseAloneOnly = UseAloneOnly;
			Initialize ();
		}

		protected override void Dispose (bool disposing)
		{
			try {
				if (disposing) {
					UnInitialize ();
					_loader.Dispose ();
					_loader = null;
					RemoveService (typeof(PropertyGrid), false);
					RemoveService (typeof(System.Drawing.Design.IToolboxService), false);
				}
            	
				base.Dispose (disposing);
			} catch (Exception ex) {
            	
			}
		}

		public void BeginLoad ()
		{
			Initialize ();
		}

		internal void Initialize ()
		{
			//- each DesignSurface has its own default services
			//- We can leave the default services in their present state,
			//- or we can remove them and replace them with our own.
			//- Now add our own services using IServiceContainer
			//-
			//-
			//- Note
			//- before loading the root control in the design surface
			//- we must add an instance of naming service to the service container.
			//- otherwise the root component did not have a name and this caused
			//- troubles when we try to use the UndoEngine
			//TODO: qiuleilei
			if (CurrentPrjInfo.CurrentEnvironment != MESEnvironment.MESReportServer) {
				//-
				//-
				//- 1. NameCreationService
				_nameCreationService = new NameCreationServiceImpl ();
				if (_nameCreationService != null) {
					RemoveService (typeof(INameCreationService), false);
					AddService (typeof(INameCreationService), _nameCreationService);
				}
				
				//-
				//-
				//- 2. IMenuCommandService
				_menuCommandService = new MenuCommandServiceImpl (this.ServiceContainer);
				_menuCommandService.DesignSurface = this;
				if (_menuCommandService != null) {
					RemoveService (typeof(IMenuCommandService), false);
					AddService (typeof(IMenuCommandService), _menuCommandService);
				}
				
				//-
				//-
				//- 3. CodeDomComponentSerializationService
				_codeDomComponentSerializationService = new CodeDomComponentSerializationService (this.ServiceContainer);
				if (_codeDomComponentSerializationService != null) {
					//- the CodeDomComponentSerializationService is ready to be replaced
					RemoveService (typeof(ComponentSerializationService), false);
					AddService (typeof(ComponentSerializationService), _codeDomComponentSerializationService);
				}
				//-
				//-
				//- 4. IDesignerSerializationService
				_designerSerializationService = new DesignerSerializationServiceImpl (this.ServiceContainer);
				if (_designerSerializationService != null) {
					//- the DesignerSerializationServiceImpl is ready to be replaced
					RemoveService (typeof(IDesignerSerializationService), false);
					AddService (typeof(IDesignerSerializationService), _designerSerializationService);
				}
				
				//-
				//-
				//- 5. UndoEngine
				//_undoEngine = new UndoEngineImpl(this.ServiceContainer);
				_undoEngine = new UndoEngineExt (this.ServiceContainer);
				//- disable the UndoEngine
				_undoEngine.Enabled = false;
				if (_undoEngine != null) {
					//- the UndoEngine is ready to be replaced
					RemoveService (typeof(UndoEngine), false);
					AddService (typeof(UndoEngine), _undoEngine);
				}
				
				//-
				//-
				//- 6. IEventBindingService
				//this.AddService(typeof(IEventBindingService), new PMS.Libraries.ToolControls.ToolBox.EventBindingService(this.ServiceContainer));
				_EventBindingService = new EventBindingServiceImpl (this.ServiceContainer);
				if (_EventBindingService != null) {
					RemoveService (typeof(IEventBindingService), false);
					AddService (typeof(IEventBindingService), _EventBindingService);
				}
				         
				//-
				//-
				//- 7. ITypeResolutionService
				//_TypeResolutionService = new TypeResolutionService();
				//if (_TypeResolutionService != null)
				//{
				//    //- the TypeResolutionService is ready to be replaced
				//    this.ServiceContainer.RemoveService(typeof(ITypeResolutionService), false);
				//    this.ServiceContainer.AddService(typeof(ITypeResolutionService), _TypeResolutionService);
				//}
				
				// 增/删/重命名组件的事件IComponentChangeService
				if (_UseAloneOnly == false) {
					IComponentChangeService componentChangeService = (IComponentChangeService)this.ServiceContainer.GetService (typeof(IComponentChangeService));
					componentChangeService.ComponentAdded += new ComponentEventHandler (componentChangeService_ComponentAdded);
					componentChangeService.ComponentRemoved += new ComponentEventHandler (componentChangeService_ComponentRemoved);
					componentChangeService.ComponentAdding += new ComponentEventHandler (componentChangeService_ComponentAdding);
					componentChangeService.ComponentRename += new ComponentRenameEventHandler (componentChangeService_ComponentRename);
					componentChangeService.ComponentChanged += new ComponentChangedEventHandler (ComponentChanged);
					this.GetIDesignerHost ().TransactionClosed += new DesignerTransactionCloseEventHandler (TransactionClosed);
				}
				
				UseSnapLines ();
			}
			if (_justflag > 0) {
				_loader = new MyCodeAndXMLHostLoader (_strXml, 1);
			} else {
				if (_FilePath == null)
					_loader = new MyCodeAndXMLHostLoader ();
				else
					_loader = new MyCodeAndXMLHostLoader (_FilePath);
			}
            
			this.BeginLoad (_loader);
			if (CurrentPrjInfo.CurrentEnvironment != MESEnvironment.MESReportServer) {
				// 如果是打开原文件发送控件列表至propertygrid
				if (_FilePath != null) {
					//foreach (Control ct in rootForm.Controls)
					//{
					//    scriptControl1.AddObject(ct.Name, ct);
					//}
				
					//int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_ROOTFORMCONTROLS;
					//byte[] theBytes = PMS.Libraries.ToolControls.PMSPublicInfo.PMSFileClass.ObjToByte(rootForm.Controls);
					//PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendCopyData(PMS.Libraries.ToolControls.PMSPublicInfo.Message.PropertyGridFormHandle, msgID, theBytes);
				}
				
				Control control = null;
				IDesignerHost host = (IDesignerHost)this.GetService (typeof(IDesignerHost));
				
				if (host == null)
					return;
				_MESDesignerService = new MESDesignerService ();
				_MESDesignerService.Host = host;
				_MESDesignerService.HostSurfaceView = this.View;
				
				if (_MESDesignerService != null) {
					RemoveService (typeof(MESDesignerService), false);
					AddService (typeof(MESDesignerService), _MESDesignerService);
				}
				
				try {
					// Set the backcolor
					Type hostType = host.RootComponent.GetType ();
				
					if (hostType == typeof(Form)) {
						Form f = (Form)host.RootComponent;
						f.FormBorderStyle = FormBorderStyle.FixedSingle;
						//f.AutoScroll = true;
						f.ControlBox = false;
						f.Location = new Point (0, 0);
						//f.Dock = DockStyle.Fill;
				
						//设置Locked属性为true
						PropertyDescriptor lockedValue = TypeDescriptor.GetProperties (f) ["Locked"];
				
						if (lockedValue != null)
							lockedValue.SetValue (f, true);
				
						if (f.RestoreBounds.Size != f.Size) {
							//PMS.Libraries.ToolControls.PMSPublicInfo.Message.Info("f.Size=" + f.Size.ToString() + "f.RestoreBounds=" + f.RestoreBounds.ToString());
							MES.PublicInterface.DesignerForm.SetFormSize (f, f.Left, f.Top, f.RestoreBounds.Width, f.RestoreBounds.Height);
						}
						// 如果是新建文件使用默认属性
						// 如果是打开原文件使用保存的属性
						if (_FilePath == null && _justflag <= 0) {
							f.Size = new Size (800, 600);
							//f.BackColor = Color.White;
						}
						control = this.View as Control;     
						//control.BackColor = Color.White;
					} else if (hostType == typeof(UserControl)) {
						control = this.View as Control;
						//control.BackColor = Color.White;
					} else if (hostType == typeof(Component)) {
						control = this.View as Control;
						//control.BackColor = Color.FloralWhite;
					} else {
						throw new Exception ("Undefined Host Type: " + hostType.ToString ());
					}
				
					// Set SelectionService - SelectionChanged event handler
					_selectionService = (ISelectionService)(this.ServiceContainer.GetService (typeof(ISelectionService)));
					_selectionService.SelectionChanged += new EventHandler (selectionService_SelectionChanged);
				             
					ComponentListChanged (null, null);
					if (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentRunMode == PMS.Libraries.ToolControls.PMSPublicInfo.RunMode.Develope)
						TransactionClosed (null, null);
				
					_undoEngine.Enabled = true;
				} catch (Exception ex) {
					Trace.WriteLine (ex.ToString ());
				}
			}

		}

		internal void UnInitialize ()
		{
			try {
				//-
				//-
				//- 1. NameCreationService
				if (_nameCreationService != null) {
					RemoveService (typeof(INameCreationService), false);
				}
				
				//-
				//-
				//- 2. IMenuCommandService
				if (_menuCommandService != null) {
					RemoveService (typeof(IMenuCommandService), false);
				}
				
				//-
				//-
				//- 3. CodeDomComponentSerializationService
				if (_codeDomComponentSerializationService != null) {
					//- the CodeDomComponentSerializationService is ready to be replaced
					RemoveService (typeof(ComponentSerializationService), false);
				}
				
				//-
				//-
				//- 4. IDesignerSerializationService
				if (_designerSerializationService != null) {
					//- the DesignerSerializationServiceImpl is ready to be replaced
					RemoveService (typeof(IDesignerSerializationService), false);
				}
				
				//-
				//-
				//- 5. UndoEngine
				//- disable the UndoEngine
				_undoEngine.Enabled = false;
				if (_undoEngine != null) {
					//- the UndoEngine is ready to be replaced
					RemoveService (typeof(UndoEngine), false);
				}
				
				//-
				//-
				//- 6. IEventBindingService
				if (_EventBindingService != null) {
					RemoveService (typeof(IEventBindingService), false);
				}
				
				//-
				//-
				//- 7. ITypeResolutionService
				if (_TypeResolutionService != null) {
					//- the TypeResolutionService is ready to be replaced
					RemoveService (typeof(ITypeResolutionService), false);
				}
				
				if (_MESDesignerService != null) {
					RemoveService (typeof(MESDesignerService), false);
				}
				
				// 增/删/重命名组件的事件IComponentChangeService
				IComponentChangeService componentChangeService = (IComponentChangeService)this.ServiceContainer.GetService (typeof(IComponentChangeService));
				if (componentChangeService != null) {
					componentChangeService.ComponentAdded -= componentChangeService_ComponentAdded;
					componentChangeService.ComponentRemoved -= componentChangeService_ComponentRemoved;
					componentChangeService.ComponentAdding -= componentChangeService_ComponentAdding;
					componentChangeService.ComponentRename -= componentChangeService_ComponentRename;
					componentChangeService.ComponentChanged -= ComponentChanged;
					this.GetIDesignerHost ().TransactionClosed -= new DesignerTransactionCloseEventHandler (TransactionClosed);
				
					//this.ServiceContainer.RemoveService(typeof(IComponentChangeService), false);
				}
				
				IDesignerHost host = (IDesignerHost)this.GetService (typeof(IDesignerHost));
				if (host != null) {
					//this.ServiceContainer.RemoveService(typeof(IDesignerHost), false);
				}
				
				try {
					// Set SelectionService - SelectionChanged event handler
					//_selectionService = (ISelectionService)(this.ServiceContainer.GetService(typeof(ISelectionService)));
					if (_selectionService != null) {
						_selectionService.SelectionChanged -= selectionService_SelectionChanged;
						//this.ServiceContainer.RemoveService(typeof(ISelectionService), false);
					}
				} catch (Exception ex) {
					Trace.WriteLine (ex.ToString ());
				}
			} catch (Exception ex) {
				Trace.WriteLine (ex.ToString ());
			}
		}

		void UpdateDocOutLine ()
		{
			// 通知DocumentOutlineForm 更新treeView 列表
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_UPDATEDOCOUTLINE;
			IntPtr handle = PMS.Libraries.ToolControls.PMSPublicInfo.Message.DocumentOutlineFormHandle;
			PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (handle, msgID, IntPtr.Zero, IntPtr.Zero);
		}

		void SetSaved ()
		{
			// 通知当前窗口设置为非修改
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_SETDOCSAVED;
			IntPtr handle = PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle;
			PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (handle, msgID, IntPtr.Zero, IntPtr.Zero);
		}

		void componentChangeService_ComponentRemoved (object sender, ComponentEventArgs e)
		{
			_ShouldUpdateSelectableObjects = true;
			if (sender != null)
				_Modified = true;
			UpdateDocOutLine ();
		}

		void componentChangeService_ComponentAdded (object sender, ComponentEventArgs e)
		{
			_ShouldUpdateSelectableObjects = true;
			if (sender != null) {
				// 以下两行代码是用于解决设计时，从工具栏拖放继承于UserControl的自定义控件至设计时Surface时，大纲视图显示控件名称不正确的问题
				if (e.Component is UserControl)
					(e.Component as UserControl).Name = e.Component.Site.Name;
				_Modified = true;
			}
		}

		void componentChangeService_ComponentAdding (object sender, ComponentEventArgs e)
		{
            
		}

		void componentChangeService_ComponentRename (object sender, ComponentRenameEventArgs e)
		{
			_ShouldUpdateSelectableObjects = true;
			if (sender != null) {
				// 以下两行代码是用于解决设计时，修改继承于UserControl的自定义控件的Name时，大纲视图显示控件名称不正确的问题
				if (e.Component is UserControl)
					(e.Component as UserControl).Name = (e.Component as IComponent).Site.Name;
				_Modified = true;
			}
		}

		public DesignerLoader Loader {
			get {
				return _loader;
			}
			set {
				_loader = value;
			}
		}

		public object GetSelectObj ()
		{
			if (_selectionService != null) {
				ICollection selectedComponents = _selectionService.GetSelectedComponents ();
				if (selectedComponents.Count > 0) {
					object[] comps = new object[selectedComponents.Count];
					selectedComponents.CopyTo (comps, 0);
					return comps [0];
				}
			}
			return null;
		}

		public void SelectionChanged ()
		{
			if (_selectionService != null) {
				ICollection selectedComponents = _selectionService.GetSelectedComponents ();
				EventsPropertyGrid propertyGrid = (EventsPropertyGrid)this.GetService (typeof(PropertyGrid));
				if (propertyGrid == null)
					return;
				IDesignerHost designerHost = (IDesignerHost)this.GetService (typeof(IDesignerHost));

				if (selectedComponents.Count == 0)
					propertyGrid.SelectedObject = null;
				else {
					object[] comps = new object[selectedComponents.Count];

					int i = 0;
					foreach (object o in selectedComponents) {
						if (i == 0) {
							//object ob = (object)this.GetService(typeof(object));
							//ob = o;
							int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_DOCOUTLINESELCHANGE;
							IntPtr handle = PMS.Libraries.ToolControls.PMSPublicInfo.Message.DocumentOutlineFormHandle;
							PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (handle, msgID, IntPtr.Zero, IntPtr.Zero);

							msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_PROPGRIDSELCHANGE;
							handle = PMS.Libraries.ToolControls.PMSPublicInfo.Message.PropertyGridFormHandle;
							PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (handle, msgID, IntPtr.Zero, IntPtr.Zero);
							break;
						}
					}
					selectedComponents.CopyTo (comps, 0);
					if (propertyGrid.SelectedObjects != comps) {
						//Form f1 = new Form();
						//PropertyGrid g1 = new PropertyGrid();
						//g1.Dock = DockStyle.Fill;
						////Label b1 = new Label();
						////b1.Visible = false;
						////rootForm.Controls[2].Controls[1].Controls[0].Controls.Add(b1);
						//f1.Controls.Add(g1);
						////g1.SelectedObjects = comps;
						//g1.SelectedObject = rootForm.Controls[2].Controls[1].Controls[0].Controls[0];
						////g1.SelectedObject = b1;
						//f1.ShowDialog();
						propertyGrid.SelectedObjects = comps;
					}

					if (selectedComponents.Count == 1) {
						PMS.Libraries.ToolControls.ToolBox.EventBindingService ebs = designerHost.GetService (typeof(IEventBindingService)) as PMS.Libraries.ToolControls.ToolBox.EventBindingService;

					}

				}

				if (designerHost != null) {
					propertyGrid.Site = (new IDEContainer (designerHost)).CreateSite (propertyGrid);
					propertyGrid.PropertyTabs.AddTabType (typeof(System.Windows.Forms.Design.EventsTab), PropertyTabScope.Document);
					propertyGrid.ShowEvents (true);
				} else {
					propertyGrid.Site = null;
				}
			}
		}

		private void ComponentListChanged (object sender, EventArgs e)
		{
			_ShouldUpdateSelectableObjects = true;
			if (sender != null)
				_Modified = true;
		}


		private void ComponentChanged (object sender, EventArgs e)
		{
			if (e is System.ComponentModel.Design.ComponentChangedEventArgs) {
				System.ComponentModel.Design.ComponentChangedEventArgs ee = (System.ComponentModel.Design.ComponentChangedEventArgs)e;

				if (ee.Component is Form) {
				}
				//if (ee.Component is Form)
				//{
				//    if (ee.Member == null)
				//        return;
				//    Form f = (Form)ee.Component;

				//    if (ee.Member.Name == "Width")
				//    {
				//        int w = (int)(ee.NewValue);
				//        if (w > 32767)
				//            w = 32767;
				//        Control ctrl = this.View as Control;

				//        //改变视图尺寸时默认将滚动条设置至初始位置（0,0）
				//        int x = 0 - ((System.Windows.Forms.ScrollProperties)(((System.Windows.Forms.ScrollableControl)(ctrl.Controls[0])).HorizontalScroll)).Value;
				//        int y = 0 - ((System.Windows.Forms.ScrollProperties)(((System.Windows.Forms.ScrollableControl)(ctrl.Controls[0])).VerticalScroll)).Value;
				//        DesignerForm.SetFormSize(f.Handle, x, y, w, f.Height);
				//    }
				//    else if (ee.Member.Name == "Height")
				//    {
				//        int h = (int)(ee.NewValue);
				//        if (h > 32767)
				//            h = 32767;
				//        Control ctrl = this.View as Control;

				//        //改变视图尺寸时默认将滚动条设置至跏嘉恢茫?0）
				//        int x = 0 - ((System.Windows.Forms.ScrollProperties)(((System.Windows.Forms.ScrollableControl)(ctrl.Controls[0])).HorizontalScroll)).Value;
				//        int y = 0 - ((System.Windows.Forms.ScrollProperties)(((System.Windows.Forms.ScrollableControl)(ctrl.Controls[0])).VerticalScroll)).Value;
				//        DesignerForm.SetFormSize(f.Handle, x, y, f.Width, h);
				//    }
				//    else if (ee.Member.Name == "Size")
				//    {
				//        if (ee.NewValue is Size)
				//        {
				//            int h = ((Size)ee.NewValue).Height;
				//            int w = ((Size)ee.NewValue).Width;
				//            if (h > 32767)
				//                h = 32767;
				//            if (w > 32767)
				//                w = 32767;
				//            Control ctrl = this.View as Control;
                            
				//            //改变视图尺寸时默认将滚动条设置至初始位置（0,0）
				//            int x = 0 - ((System.Windows.Forms.ScrollProperties)(((System.Windows.Forms.ScrollableControl)(ctrl.Controls[0])).HorizontalScroll)).Value;
				//            int y = 0 - ((System.Windows.Forms.ScrollProperties)(((System.Windows.Forms.ScrollableControl)(ctrl.Controls[0])).VerticalScroll)).Value;
                            
				//            //((System.Windows.Forms.ScrollProperties)(((System.Windows.Forms.ScrollableControl)(ctrl.Controls[0])).HorizontalScroll)).Value = 0;
				//            //((System.Windows.Forms.ScrollProperties)(((System.Windows.Forms.ScrollableControl)(ctrl.Controls[0])).VerticalScroll)).Value = 0;

				//            DesignerForm.SetFormSize(f.Handle, x, y, w, h);
				//        }
				//    }
				//}
				//else if(ee.Component is MES.PublicInterface.CustomPropertyCollection)
				//{
				//    MES.PublicInterface.CustomPropertyCollection coc = (MES.PublicInterface.CustomPropertyCollection)(ee.Component);
				//    foreach (MES.PublicInterface.CustomProperty cp in coc)
				//    {
				//        if (cp.ObjectSource is Form)
				//        {
				//            Form f = (Form)cp.ObjectSource;

				//            if (cp.Name == "Width")
				//            {
				//                int w = (int)(cp.Value);
				//                if (w > 32767)
				//                    w = 32767;
				//                Control ctrl = this.View as Control;

				//                //改变视图尺寸时默认将滚动条设置至初始位茫?0）
				//                int x = 0 - ((System.Windows.Forms.ScrollProperties)(((System.Windows.Forms.ScrollableControl)(ctrl.Controls[0])).HorizontalScroll)).Value;
				//                int y = 0 - ((System.Windows.Forms.ScrollProperties)(((System.Windows.Forms.ScrollableControl)(ctrl.Controls[0])).VerticalScroll)).Value;
				//                DesignerForm.SetFormSize(f.Handle, x, y, w, f.Height);
				//            }
				//            else if (cp.Name == "Height")
				//            {
				//                int h = (int)(cp.Value);
				//                if (h > 32767)
				//                    h = 32767;
				//                Control ctrl = this.View as Control;

				//                //改变视图尺寸时默认将滚动条设置至初始位置（0,0）
				//                int x = 0 - ((System.Windows.Forms.ScrollProperties)(((System.Windows.Forms.ScrollableControl)(ctrl.Controls[0])).HorizontalScroll)).Value;
				//                int y = 0 - ((System.Windows.Forms.ScrollProperties)(((System.Windows.Forms.ScrollableControl)(ctrl.Controls[0])).VerticalScroll)).Value;
				//                DesignerForm.SetFormSize(f.Handle, x, y, f.Width, h);
				//            }
				//            if (cp.Name == "Size")
				//            {
				//                if (ee.NewValue is Size)
				//                {
				//                    int h = ((Size)ee.NewValue).Height;
				//                    int w = ((Size)ee.NewValue).Width;
				//                    if (h > 32767)
				//                        h = 32767;
				//                    if (w > 32767)
				//                        w = 32767;
				//                    Control ctrl = this.View as Control;

				//                    //改变视图尺寸时默认将滚动条设置至初始位置（0,0ㄉ                //                    int x = 0 - ((System.Windows.Forms.ScrollProperties)(((System.Windows.Forms.ScrollableControl)(ctrl.Controls[0])).HorizontalScroll)).Value;
				//                    int y = 0 - ((System.Windows.Forms.ScrollProperties)(((System.Windows.Forms.ScrollableControl)(ctrl.Controls[0])).VerticalScroll)).Value;

				//                    //((System.Windows.Forms.ScrollProperties)(((System.Windows.Forms.ScrollableControl)(ctrl.Controls[0])).HorizontalScroll)).Value = 0;
				//                    //((System.Windows.Forms.ScrollProperties)(((System.Windows.Forms.ScrollableControl)(ctrl.Controls[0])).VerticalScroll)).Value = 0;

				//                    DesignerForm.SetFormSize(f.Handle, x, y, w, h);
				//                }
				//            }
				//        }
				//    }
				//}
			}
			_Modified = true;
			int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_DOCMODIFIED;
			IntPtr handle = PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle;
			PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (handle, msgID, IntPtr.Zero, IntPtr.Zero);

			UpdateDocOutLine ();
		}

		private void TransactionClosed (object sender, DesignerTransactionCloseEventArgs e)
		{
			if (_ShouldUpdateSelectableObjects) {
				int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_UPDATECOMBOBOX;
				IntPtr handle = PMS.Libraries.ToolControls.PMSPublicInfo.Message.PropertyGridFormHandle;
				PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (handle, msgID, IntPtr.Zero, IntPtr.Zero);

				_ShouldUpdateSelectableObjects = false;
			}

			if (_Modified == true) {
				int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_DOCMODIFIED;
				IntPtr handle = PMS.Libraries.ToolControls.PMSPublicInfo.Message.PMSDeveloperControlHandle;
				PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (handle, msgID, IntPtr.Zero, IntPtr.Zero);
			}
		}

		/// <summary>
		/// When the selection changes this sets the PropertyGrid's selected component 
		/// </summary>
		/// 
		public void selectionService_SelectionChanged (object sender, EventArgs e)
		{
			// 以下为测试设计时Enable属性是否有效
			//if (_selectionService.PrimarySelection.ToString().Contains("UserControl"))
			//{
			//    System.Reflection.PropertyInfo pi = _selectionService.PrimarySelection.GetType().GetProperty("Enabled");
			//    pi.SetValue(_selectionService.PrimarySelection, false, null);
			//    (_selectionService.PrimarySelection as Control).Enabled = false;
			//}

			// 
			//if (_selectionService.PrimarySelection.ToString().Contains("UserControl"))
			//{
			//    AttributeCollection attributes = TypeDescriptor.GetAttributes(_selectionService.PrimarySelection);

			//    /* Prints the name of the designer by retrieving the DesignerAttribute
			//     * from the AttributeCollection. */
			//    DesignerAttribute myAttribute =
			//       (DesignerAttribute)attributes[typeof(DesignerAttribute)];

			// 以下方法设置有效
			//    PropertyDescriptorCollection properties = System.ComponentModel.TypeDescriptor.GetProperties(_selectionService.PrimarySelection);
			//    PropertyDescriptor prop = properties.Find("Visible", true);
			//    prop.SetValue(_selectionService.PrimarySelection, false);
			//}
            

			SelectionChanged ();
            
            
			//if (_selectionService.PrimarySelection.ToString() != "PmsReportTable1 [PMS.Libraries.ToolControls.Report.PmsReportTable]")
			//    return;
			//TextBox tb = new TextBox();
			//tb.BackColor = Color.Pink;
			//tb.Location = new Point(10, 10);

			//_MESDesignerService.CreateControl(tb);
		}

		public void RemoveService (Type type, bool promote)
		{
			if (null != this.ServiceContainer.GetService (type))
				this.ServiceContainer.RemoveService (type, promote);
		}

		public void AddService (Type serviceType, object serviceInstance)
		{
			this.ServiceContainer.AddService (serviceType, serviceInstance);
		}

		public void SetRootFormSelected ()
		{
			if (rootForm != null) {
				if (_selectionService != null) {
					_selectionService.SetSelectedComponents (new object[] { this.rootForm });
					int count = this.rootForm.Controls.Count;
					if (count > 0)
						_selectionService.SetSelectedComponents (new object[] { this.rootForm.Controls [count - 1] });
				}
                
			}
		}

		public bool DoAction (Keys keyData)
		{
			try {
				if (_MESDesignerService != null)
				if (_MESDesignerService.FocusControls.Count > 0)
					return false;

				int msgID = PMS.Libraries.ToolControls.PMSPublicInfo.Message.USER_DOCOUTLINEREFRESHSWITCH;
				IntPtr handle = PMS.Libraries.ToolControls.PMSPublicInfo.Message.DocumentOutlineFormHandle;

				MenuCommandServiceImpl ims = _menuCommandService;

				switch (keyData) {
				case Keys.Control | Keys.X:
					PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (handle, msgID, IntPtr.Zero, IntPtr.Zero);
					ims.GlobalInvoke (StandardCommands.Cut);
					PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (handle, msgID, IntPtr.Zero, (IntPtr)1);
					return true;
				case Keys.Control | Keys.C:
					ims.GlobalInvoke (StandardCommands.Copy);
					return true;
				case Keys.Control | Keys.V:
					PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (handle, msgID, IntPtr.Zero, IntPtr.Zero);
					Point oldPoint = Point.Empty;
					if (_selectionService != null) {
						Control ctrl = _selectionService.PrimarySelection as Control;
						if (null != ctrl)
							oldPoint = ctrl.Location;
					}
					IDesignerHost host1 = (IDesignerHost)this.GetService (typeof(IDesignerHost));
					DesignerTransaction tran1 = host1.CreateTransaction ("Paste");
                        //ims.GlobalInvoke(StandardCommands.Paste);
					ims.Paste ();
					ICollection selectedComponents1 = null;
					if (_selectionService != null) {
						Control ctrl = _selectionService.PrimarySelection as Control;
						Point newPoint = ctrl.Location;
						int xOffset = newPoint.X - oldPoint.X;
						int yOffset = newPoint.Y - oldPoint.Y;
						selectedComponents1 = _selectionService.GetSelectedComponents ();
						_selectionService.SetSelectedComponents (null);
						foreach (object ob in selectedComponents1) {
							if (ob is Control) {
								IComponentChangeService componentChangeService = (IComponentChangeService)this.ServiceContainer.GetService (typeof(IComponentChangeService));
								Control co = ob as Control;
								componentChangeService.OnComponentChanging (co, null);
								Point oldValue = co.Location;
								Point newValue = new Point (co.Location.X - xOffset + 10, co.Location.Y - yOffset + 10);
								co.Location = newValue;
								componentChangeService.OnComponentChanged (co, null, oldValue, newValue);
							}
						}
					}
					tran1.Commit ();
					PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (handle, msgID, IntPtr.Zero, (IntPtr)1);
					return true;
				case Keys.Delete:
					PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (handle, msgID, IntPtr.Zero, IntPtr.Zero);
					ims.GlobalInvoke (StandardCommands.Delete);
					PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (handle, msgID, IntPtr.Zero, (IntPtr)1);
					return true;
				case Keys.Control | Keys.Z:
					PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (handle, msgID, IntPtr.Zero, IntPtr.Zero);
					this.GetUndoEngineExt ().Undo ();
					if (false == this.GetUndoEngineExt ().EnableUndo)
						SetSaved ();
                        //ims.GlobalInvoke(StandardCommands.Undo);
					PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (handle, msgID, IntPtr.Zero, (IntPtr)1);
					return true;
				case Keys.Control | Keys.Y:
					PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (handle, msgID, IntPtr.Zero, IntPtr.Zero);
					this.GetUndoEngineExt ().Redo ();
                        //ims.GlobalInvoke(StandardCommands.Redo);
					PMS.Libraries.ToolControls.PMSPublicInfo.Message.SendMsgToMainForm (handle, msgID, IntPtr.Zero, (IntPtr)1);
					return true;
				case Keys.Control | Keys.A:
					ims.GlobalInvoke (StandardCommands.SelectAll);
					if (_selectionService != null) {
						ICollection selectedComponents2 = null;
						selectedComponents2 = _selectionService.GetSelectedComponents ();
						ArrayList objArray = new ArrayList ();
						foreach (object ob in selectedComponents2) {
							object[] obas = ob.GetType ().GetCustomAttributes (false);
							foreach (object oba in obas) {
								// 不删除设计时的基本控件带属性标签MES.PublicInterface.MESBasicDesignerControlAttributeAttribute
								if (oba.ToString () == "MES.PublicInterface.MESBasicDesignerControlAttributeAttribute")
									objArray.Add (ob);
								else if (ob is Form)
									objArray.Add (ob);
							}
						}
						if (objArray.Count > 0)
							_selectionService.SetSelectedComponents (objArray, SelectionTypes.Remove);
					}
					return true;
				case Keys.Up:
					if (_selectionService != null) {
						ICollection selectedComponents = _selectionService.GetSelectedComponents ();
						_selectionService.SetSelectedComponents (null);
						foreach (object ob in selectedComponents) {
							if (ob is Control) {
								IComponentChangeService componentChangeService = (IComponentChangeService)this.ServiceContainer.GetService (typeof(IComponentChangeService));
								IDesignerHost host = (IDesignerHost)this.GetService (typeof(IDesignerHost));
								DesignerTransaction tran = host.CreateTransaction ("Up");
								Control co = ob as Control;
								componentChangeService.OnComponentChanging (co, null);
								Point oldValue = co.Location;
								Point newValue = new Point (co.Location.X, co.Location.Y - 1);
								co.Location = newValue;
								componentChangeService.OnComponentChanged (co, null, oldValue, newValue);
								tran.Commit ();
							}
						}
						_selectionService.SetSelectedComponents (selectedComponents);
					}
					return true;
				case Keys.Down:
					if (_selectionService != null) {
						ICollection selectedComponents = _selectionService.GetSelectedComponents ();
						_selectionService.SetSelectedComponents (null);
						foreach (object ob in selectedComponents) {
							if (ob is Control) {
								IComponentChangeService componentChangeService = (IComponentChangeService)this.ServiceContainer.GetService (typeof(IComponentChangeService));
								IDesignerHost host = (IDesignerHost)this.GetService (typeof(IDesignerHost));
								DesignerTransaction tran = host.CreateTransaction ("Down");
								Control co = ob as Control;
								componentChangeService.OnComponentChanging (co, null);
								Point oldValue = co.Location;
								Point newValue = new Point (co.Location.X, co.Location.Y + 1);
								co.Location = newValue;
								componentChangeService.OnComponentChanged (co, null, oldValue, newValue);
								tran.Commit ();
							}
						}
						_selectionService.SetSelectedComponents (selectedComponents);
					}
					return true;
				case Keys.Left:
					if (_selectionService != null) {
						ICollection selectedComponents = _selectionService.GetSelectedComponents ();
						_selectionService.SetSelectedComponents (null);
						foreach (object ob in selectedComponents) {
							if (ob is Control) {
								IComponentChangeService componentChangeService = (IComponentChangeService)this.ServiceContainer.GetService (typeof(IComponentChangeService));
								IDesignerHost host = (IDesignerHost)this.GetService (typeof(IDesignerHost));
								DesignerTransaction tran = host.CreateTransaction ("Left");
								Control co = ob as Control;
								componentChangeService.OnComponentChanging (co, null);
								Point oldValue = co.Location;
								Point newValue = new Point (co.Location.X - 1, co.Location.Y);
								co.Location = newValue;
								componentChangeService.OnComponentChanged (co, null, oldValue, newValue);
								tran.Commit ();
							}
						}
						_selectionService.SetSelectedComponents (selectedComponents);
					}
					return true;
				case Keys.Right:
					if (_selectionService != null) {
						ICollection selectedComponents = _selectionService.GetSelectedComponents ();
						_selectionService.SetSelectedComponents (null);
						foreach (object ob in selectedComponents) {
							if (ob is Control) {
								IComponentChangeService componentChangeService = (IComponentChangeService)this.ServiceContainer.GetService (typeof(IComponentChangeService));
								IDesignerHost host = (IDesignerHost)this.GetService (typeof(IDesignerHost));
								DesignerTransaction tran = host.CreateTransaction ("Right");
								Control co = ob as Control;
								componentChangeService.OnComponentChanging (co, null);
								Point oldValue = co.Location;
								Point newValue = new Point (co.Location.X + 1, co.Location.Y);
								co.Location = newValue;
								componentChangeService.OnComponentChanged (co, null, oldValue, newValue);
								tran.Commit ();
							}
						}
						_selectionService.SetSelectedComponents (selectedComponents);
					}
					return true;
				default:
                        // do nothing;
					break;
				}
			}//end_try
            catch (Exception ex) {
				throw new Exception (_Name_ + "::DoAction() - Exception: error in performing the action: " + keyData.ToString () + "(see Inner Exception)", ex);
			}//end_catch

			return false;
		}

		//- do some Edit menu command using the MenuCommandService
		public void DoAction (string command)
		{
			if (string.IsNullOrEmpty (command))
				return;

			MenuCommandServiceImpl ims = _menuCommandService;

			try {
				switch (command.ToUpper ()) {
				case "CUT":
                    //case "剪切":
					ims.GlobalInvoke (StandardCommands.Cut);
					break;
				case "COPY":
                   // case "复制":
					ims.GlobalInvoke (StandardCommands.Copy);
					break;
				case "PASTE":
				case "粘贴":
					ims.GlobalInvoke (StandardCommands.Paste);
					break;
				case "DELETE":
                   // case "删除":
					ims.GlobalInvoke (StandardCommands.Delete);
					break;
				case "UNDO":
                    //case "撤销":
					this.GetUndoEngineExt ().Undo ();
                        //ims.GlobalInvoke(StandardCommands.Undo);
					break;
				case "REDO":
                    //case "重做":
					this.GetUndoEngineExt ().Redo ();
                        //ims.GlobalInvoke(StandardCommands.Redo);
					break;
				case "SELECT ALL":
                    //case "全选":
					ims.GlobalInvoke (StandardCommands.SelectAll);
					break;
				case "LEFTS":
                    //case "左对齐":
					ims.GlobalInvoke (StandardCommands.AlignLeft);
					break;
				case "CENTERS":
                    //case "相对水平居中":
					ims.GlobalInvoke (StandardCommands.AlignHorizontalCenters);
					break;
				case "RIGHTS":
                    //case "右对齐":
					ims.GlobalInvoke (StandardCommands.AlignRight);
					break;
				case "TOPS":
                    //case "顶端对齐":
					ims.GlobalInvoke (StandardCommands.AlignTop);
					break;
				case "MIDDLES":
                    //case "相对垂直居中":
					ims.GlobalInvoke (StandardCommands.AlignVerticalCenters);
					break;
				case "BOTTOMS":
                    //case "底端对齐":
					ims.GlobalInvoke (StandardCommands.AlignBottom);
					break;
				case "BRINGTOFRONT":
                    //case "置于顶层":
					ims.GlobalInvoke (StandardCommands.BringToFront);
					break;
				case "SENDTOBACK":
                    //case "置于底层":
					ims.GlobalInvoke (StandardCommands.SendToBack);
					break;
				case "BRINGFORWARD":
                    //case "上移一层":
					ims.GlobalInvoke (StandardCommands.BringToFront);
					break;
				case "SENDBACKWARD":
                    //case "下移一层":
					ims.GlobalInvoke (StandardCommands.SendBackward);
					break;
				case "GROUP":
                    //case "组合":
					ims.GlobalInvoke (StandardCommands.Group);
					break;
				case "UNGROUP":
                    //case "取消组合":
					ims.GlobalInvoke (StandardCommands.Ungroup);
					break;
				case "SIZETOCONTROLWIDTH":
                    //case "相同宽度":
					ims.GlobalInvoke (StandardCommands.SizeToControlWidth);
					break;
				case "SIZETOCONTROLHEIGHT":
                    //case "相同高度":
					ims.GlobalInvoke (StandardCommands.SizeToControlHeight);
					break;
				case "SIZETOCONTROL":
                    //case "相同大小":
					ims.GlobalInvoke (StandardCommands.SizeToControl);
					break;
				case "SHOWGRID":
                    //case "显示格子":
					ims.GlobalInvoke (StandardCommands.ShowGrid); 
					break;
				case "LOCKCONTROLS":
                    //case "锁定控件":
					ims.GlobalInvoke (StandardCommands.LockControls);
					break;
				case "HorizSpaceMakeEqual":
                    //case "平均水平空间":
					ims.GlobalInvoke (StandardCommands.HorizSpaceMakeEqual);
					break;
				case "HorizSpaceIncrease":
                    //case "扩大水平空间":
					ims.GlobalInvoke (StandardCommands.HorizSpaceIncrease);
					break;
				case "HorizSpaceDecrease":
                    //case "缩小水平空间":
					ims.GlobalInvoke (StandardCommands.HorizSpaceDecrease);
					break;
				case "HorizSpaceConcatenate":
                    //case "删除水平空间":
					ims.GlobalInvoke (StandardCommands.HorizSpaceConcatenate);
					break;
				case "VertSpaceMakeEqual":
                    //case "平均垂直空间":
					ims.GlobalInvoke (StandardCommands.VertSpaceMakeEqual);
					break;
				case "VertSpaceIncrease":
                    //case "扩大垂直空间":
					ims.GlobalInvoke (StandardCommands.VertSpaceIncrease);
					break;
				case "VertSpaceDecrease":
                    //case "缩小垂直空间":
					ims.GlobalInvoke (StandardCommands.VertSpaceDecrease);
					break;
				case "VertSpaceConcatenate":
                    //case "删除垂直空间":
					ims.GlobalInvoke (StandardCommands.VertSpaceConcatenate);
					break;
//                    case "水平居中":
//                        if (_selectionService != null)
//                        {
//                            ICollection selectedComponents = _selectionService.GetSelectedComponents();
//                            _selectionService.SetSelectedComponents(null);
//                            foreach (object ob in selectedComponents)
//                            {
//                                if (ob is Control)
//                                {
//                                    Control co = ob as Control;
//                                    if(null != co.Parent)
//                                    {
//                                        int coCenterX = co.Left + co.Width / 2;
//                                        int pcoCenterX = co.Parent.Width / 2;
//                                        co.Left -= coCenterX - pcoCenterX;
//                                    }
//                                }
//                            }
//                            _selectionService.SetSelectedComponents(selectedComponents);
//                        }
//                        break;
//                    case "垂直居中":
//                        if (_selectionService != null)
//                        {
//                            ICollection selectedComponents = _selectionService.GetSelectedComponents();
//                            _selectionService.SetSelectedComponents(null);
//                            foreach (object ob in selectedComponents)
//                            {
//                                if (ob is Control)
//                                {
//                                    Control co = ob as Control;
//                                    if (null != co.Parent)
//                                    {
//                                        int coCenterY = co.Top + co.Height / 2;
//                                        int pcoCenterY = co.Parent.Height / 2;
//                                        co.Top -= coCenterY - pcoCenterY;
//                                    }
//                                }
//                            }
//                            _selectionService.SetSelectedComponents(selectedComponents);
//                        }
//                        break;
				default:
                        // do nothing;
					break;
				}//end_switch
			}//end_try
            catch (Exception ex) {
				throw new Exception (_Name_ + "::DoAction() - Exception: error in performing the action: " + command + "(see Inner Exception)", ex);
			}//end_catch
		}

		public bool ProcessCmdKey (ref System.Windows.Forms.Message msg, Keys keyData)
		{
			object obj = GetSelectObj ();
			if (null != obj && obj is PMS.Libraries.ToolControls.PMSPublicInfo.IProcessCmdKey) {
				return (obj as PMS.Libraries.ToolControls.PMSPublicInfo.IProcessCmdKey).ProcessCmdKey (ref msg, keyData);
			}
			return false;
		}
	}
	// class
}
// namespace
