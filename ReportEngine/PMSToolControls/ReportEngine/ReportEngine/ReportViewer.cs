using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Threading;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using PMS.Libraries.ToolControls.PMSPublicInfo;
using PMS.Libraries.ToolControls.PMSReport;

namespace NetSCADA.ReportEngine
{
	public partial class ReportViewer : UserControl
	{
		private bool _MainReportDrawingIsFocus = true;
		private ReportRuntime _ReportRuntime = null;

		public ReportViewer ()
		{ 
			InitializeComponent ();
			this.splitContainer1.Panel1Collapsed = true;
			this.splitContainer2.Panel1Collapsed = true;

			subReportDrawing.BackColor = Color.FromArgb (255, 200, 200, 200);
			mainReportDrawing.BackColor = Color.FromArgb (255, 200, 200, 200);
			subMainReportDrawing.BackColor = Color.FromArgb (255, 200, 200, 200);

			subReportDrawing.OnSelectPage += new ReportDrawing.OnSelectPageDelegate (OnSubReportDrawingSelectPage);
			mainReportDrawing.OnVScrollChanged += new ReportDrawing.OnVScrollChangedDelegate (OnMainReportDrawingVScrollChanged);
			mainReportDrawing.OnSelectPage += new ReportDrawing.OnSelectPageDelegate (OnMainReportDrawingSelectPage);
			mainReportDrawing.OnZoomChanged += new ReportDrawing.OnZoomChangedDelegate (OnMainReportDrawingZoomChanged);
			subMainReportDrawing.OnVScrollChanged += new ReportDrawing.OnVScrollChangedDelegate (OnSubMainReportDrawingVScrollChanged);
			subMainReportDrawing.OnSelectPage += new ReportDrawing.OnSelectPageDelegate (OnSubMainReportDrawingSelectPage);
			subMainReportDrawing.OnZoomChanged += new ReportDrawing.OnZoomChangedDelegate (OnSubMainReportDrawingZoomChanged); 
		}

		private bool _RunMode = true;

		/// <summary>
		/// 运行模式：true:控件运行模式不出现打开报表工具栏，false:独立运行模式出现打开报表工具栏
		/// </summary> 
		public bool RunMode {
			get { return _RunMode; }
			set { 
				_RunMode = value;
				if (!_RunMode) {
					if (!toolStrip1.Items.Contains (this.openfile))
						toolStrip1.Items.Insert (0, this.openfile);
					else
						this.openfile.Visible = true;
					toolStrip1.Visible = true;
				}
			}
		}

        
		#region 报表配置属性

		/// <summary>
		/// 数据区控件集合
		/// </summary>
		public List<Control> DataControls {
			set {
				if (_ReportRuntime != null) {
					_ReportRuntime.DataControls = value;
				} 
			}
			get {
				if (_ReportRuntime != null) {
					return _ReportRuntime.DataControls;
				} 
				return null; 
			}
		}

		/// <summary>
		/// 数据区控件父容器（Panel）
		/// </summary>
		public Panel DataPanel {
			set {
				if (_ReportRuntime != null) {
					_ReportRuntime.DataPanel = value;
				}
			}
			get {
				if (_ReportRuntime != null) {
					return _ReportRuntime.DataPanel;
				}
				return null;
			}
		}

		/// <summary>
		/// 页眉控件集合
		/// </summary>
		public List<Control> PageHeaderControls {
			set {
				if (_ReportRuntime != null) {
					_ReportRuntime.PageHeaderControls = value;
				}
			}
			get {
				if (_ReportRuntime != null) {
					return _ReportRuntime.PageHeaderControls;
				}
				return null;
			}
		}

		/// <summary>
		/// 页眉控件父容器（Panel）
		/// </summary>
		public Panel PageHeaderPanel {
			set {
				if (_ReportRuntime != null) {
					_ReportRuntime.PageHeaderPanel = value;
				}
			}
			get {
				if (_ReportRuntime != null) {
					return _ReportRuntime.PageHeaderPanel;
				}
				return null;
			}
		}

		/// <summary>
		/// 页脚控件集合
		/// </summary>
		public List<Control> PageFooterControls {
			set {
				if (_ReportRuntime != null) {
					_ReportRuntime.PageFooterControls = value;
				}
			}
			get {
				if (_ReportRuntime != null) {
					return _ReportRuntime.PageFooterControls;
				}
				return null;
			}
		}

		/// <summary>
		/// 页脚控件父容器（Panel）
		/// </summary>
		public Panel PageFooterPanel {
			set {
				if (_ReportRuntime != null) {
					_ReportRuntime.PageFooterPanel = value;
				}
			}
			get {
				if (_ReportRuntime != null) {
					return _ReportRuntime.PageFooterPanel;
				}
				return null;
			}
		}

         
		/// <summary>
		/// 报表头控件集合
		/// </summary>
		public List<Control> ReportHeaderControls {
			set {
				if (_ReportRuntime != null) {
					_ReportRuntime.ReportHeaderControls = value;
				}
			}
			get {
				if (_ReportRuntime != null) {
					return _ReportRuntime.ReportHeaderControls;
				}
				return null;
			}
		}

		/// <summary>
		///  报表头控件父容器（Panel）
		/// </summary>
		public Panel ReportHeaderPanel {
			set {
				if (_ReportRuntime != null) {
					_ReportRuntime.ReportHeaderPanel = value;
				}
			}
			get {
				if (_ReportRuntime != null) {
					return _ReportRuntime.ReportHeaderPanel;
				}
				return null;
			}
		}

		/// <summary>
		/// 报表尾控件集合
		/// </summary>
		public List<Control> ReportFooterControls {
			set {
				if (_ReportRuntime != null) {
					_ReportRuntime.ReportFooterControls = value;
				}
			}
			get {
				if (_ReportRuntime != null) {
					return _ReportRuntime.ReportFooterControls;
				}
				return null;
			}
		}

		/// <summary>
		///  报表尾控件父容器（Panel）
		/// </summary>
		public Panel ReportFooterPanel {
			set {
				if (_ReportRuntime != null) {
					_ReportRuntime.ReportFooterPanel = value;
				}
			}
			get {
				if (_ReportRuntime != null) {
					return _ReportRuntime.ReportFooterPanel;
				}
				return null;
			}
		}

		/// <summary>
		///  数据源集合
		/// </summary>
		public List<PMSRefDBConnectionObj> Connections {
			set {
				if (_ReportRuntime != null) {
					_ReportRuntime.DBSourceConfigObjList = value;
				}
			}
			get {
				if (_ReportRuntime != null) {
					return _ReportRuntime.DBSourceConfigObjList;
				}
				return null;
			}
		}

		/// <summary>
		///  数据集合配置树
		/// </summary>
		public FieldTreeViewData FieldTreeViewData {
			set {
				if (_ReportRuntime != null) {
					_ReportRuntime.FieldTreeViewData = value;
				}
			}
			get {
				if (_ReportRuntime != null) {
					return _ReportRuntime.FieldTreeViewData;
				}
				return null;
			}
		}

		/// <summary>
		/// 打印参数
		/// </summary>
		public PMSPrintPara PrintPara {
			set {
				if (_ReportRuntime != null) {
					_ReportRuntime.PrintPara = value;
				}
			}
			get {
				if (_ReportRuntime != null) {
					return _ReportRuntime.PrintPara;
				}
				return null;
			}
		}


		private ReportViewerToolBar _ReportViewerToolBar = null;

		/// <summary>
		/// 工具栏配置
		/// </summary>
		public ReportViewerToolBar ReportViewerToolBar { 
			set {
				_ReportViewerToolBar = value; 
				int buttonSize = 24;
				bool toolBarIsVisible = false; 
                
				List<CollocateData> toolBarItems = null;
				if (_ReportViewerToolBar != null) {
					toolBarIsVisible = _ReportViewerToolBar.Visible;
					toolBarItems = _ReportViewerToolBar.CollocateToolBar;
					buttonSize = _ReportViewerToolBar.Size;
					switch (_ReportViewerToolBar.ToolBarDock) {
					case InitialPosition.Left:
						this.toolStrip1.Dock = DockStyle.Left;
						break;
					case InitialPosition.Right:
						this.toolStrip1.Dock = DockStyle.Right;
						break;
					case InitialPosition.Bottom:
						this.toolStrip1.Dock = DockStyle.Bottom;
						break;  
					case InitialPosition.Top:
					default:
						this.toolStrip1.Dock = DockStyle.Top; 
						break;  
					}
                    
				}  
				if (buttonSize < 24) {
					buttonSize = 24;
				} 
              
				if (toolBarItems != null) {  
					this.toolStrip1.Items.Clear ();  
					this.toolStrip1.ImageScalingSize = new System.Drawing.Size (buttonSize, buttonSize);
					foreach (CollocateData toolItem in toolBarItems) {
						if (!toolItem.IsVisible) {
							continue;
						}
						switch (toolItem.ToolBarName.ToLower ()) {
						case "openfile":
							{
								if (false == _RunMode) {
									if (!toolStrip1.Items.Contains (this.openfile))
										toolStrip1.Items.Add (this.openfile);
								}
							}
							break;
						case "refresh":
							{
								toolStrip1.Items.Add (refresh); 
							}
							break;
						case "printer":
							{
								toolStrip1.Items.Add (Printer); 
							}
							break;
						case "saveas":
							{
								toolStrip1.Items.Add (saveas); 
							}
							break;
						case "preview":
							{
								toolStrip1.Items.Add (preview); 
							}
							break;
						case "subview":
							{
								toolStrip1.Items.Add (subview); 
							}
							break;
						case "firstpage":
							{
								toolStrip1.Items.Add (firstpage); 
							}
							break;
						case "prepage":
							{
								toolStrip1.Items.Add (prepage); 
							}
							break;
						case "pagenumtextbox":
							{
								toolStrip1.Items.Add (pagenumtextbox); 
							}
							break;
						case "nextpage":
							{
								toolStrip1.Items.Add (nextpage); 
							}
							break;
						case "lastpage":
							{
								toolStrip1.Items.Add (lastpage); 
							}
							break;
						case "fitsize":
							{
								toolStrip1.Items.Add (fitsize); 
							}
							break;
						case "fitpage":
							{
								toolStrip1.Items.Add (fitpage); 
							}
							break;
						case "fitwidth":
							{
								toolStrip1.Items.Add (fitwidth); 
							}
							break;
						case "zoomout":
							{
								toolStrip1.Items.Add (zoomout); 
							}
							break;
						case "zoomcombobox":
							{
								toolStrip1.Items.Add (zoomcombobox); 
							}
							break;
						case "zoomin":
							{
								toolStrip1.Items.Add (zoomin); 
							}
							break;
						case "log":
							{
								toolStrip1.Items.Add (log); 
							}
							break;
						default:
							break;
						}
					}
				} else { 
					this.toolStrip1.Items.Clear ();
					this.toolStrip1.ImageScalingSize = new System.Drawing.Size (buttonSize, buttonSize);
					if (false == _RunMode) {
						if (!toolStrip1.Items.Contains (this.openfile))
							toolStrip1.Items.Add (this.openfile); 
					}
					this.toolStrip1.Items.AddRange (new System.Windows.Forms.ToolStripItem[] { 
						this.refresh, 
						this.Printer,
						this.saveas, 
						this.preview, 
						this.subview,
						this.firstpage,
						this.prepage,
						this.pagenumtextbox,
						this.nextpage,
						this.lastpage, 
						this.fitsize,
						this.fitpage,
						this.fitwidth, 
						this.zoomout,
						this.zoomcombobox,
						this.zoomin, 
						this.log
					}); 
				}
				if (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentEnvironment != PMS.Libraries.ToolControls.PMSPublicInfo.MESEnvironment.MESReportRunner) {
					if (!toolStrip1.Items.Contains (this.openfile))
						toolStrip1.Items.Insert (0, this.openfile);
					if (!toolStrip1.Items.Contains (this.Configer))
						toolStrip1.Items.Add (this.Configer);
					toolBarIsVisible = true;
				}
				if (this.toolStrip1.Items.Count == 0)
					toolBarIsVisible = false;
				this.toolStrip1.Visible = toolBarIsVisible;
			}
			get {
				return _ReportViewerToolBar;
			}
		}

		/// <summary>
		/// 工具栏
		/// </summary>
		public ToolStrip ToolBar {
			get { return toolStrip1; }
		}

		/// <summary>
		/// 页眉高度
		/// </summary>
		public float PageHeaderHeight {
			set {
				if (_ReportRuntime != null) {
					_ReportRuntime.PageHeaderHeight = value;
				}
			} 
		}

		/// <summary>
		/// 页脚高度
		/// </summary>
		public float PageFooterHeight {
			set {
				if (_ReportRuntime != null) {
					_ReportRuntime.PageFooterHeight = value;
				}
			} 
		}


		#endregion

		#region  脚本操作接口

        
		public delegate void RptQueryEndEventHandler (object sender,EventArgs e);

		public event RptQueryEndEventHandler RptQueryEnd = null;

		public delegate void RptExportEndEventHandler (object sender,EventArgs e);

		public event RptExportEndEventHandler RptExportEnd = null;

		public delegate void RptPrintEndEventHandler (object sender,EventArgs e);

		public event RptPrintEndEventHandler RptPrintEnd = null;

		public delegate void OnOpenReportDelegate (object sender,string fileName);

		public event OnOpenReportDelegate OnOpenReport = null;

		public delegate void OnRefreshReportDelegate (object sender);

		public event OnRefreshReportDelegate OnRefreshReport = null;

		public void InitialReport ()
		{
			Release ();

			_ReportRuntime = new ReportRuntime ();
			_MainReportDrawingIsFocus = true; 
		}

		public bool CancelQueryReport ()
		{
			if (_ReportRuntime != null) {
				_ReportRuntime.StopAnalyseReportThread ();
			}
			return true;
		}

		public bool QueryReport ()
		{
			try {
				_MainReportDrawingIsFocus = true;
				if (_ReportRuntime != null) {
					_ReportRuntime.QueryReport (this.FindForm ());

					subReportDrawing.Pages = _ReportRuntime.Pages;
					mainReportDrawing.Pages = _ReportRuntime.Pages;
					subMainReportDrawing.Pages = _ReportRuntime.Pages;
					switch (_ReportRuntime.PrintPara.ZoomMode) {
					case ZoomMode.FitHeight:
						mainReportDrawing.Initialize (-1, ZoomEnum.FitSize);
						subMainReportDrawing.Initialize (-1, ZoomEnum.FitSize);
						break;
					case ZoomMode.FitWidth:
						mainReportDrawing.Initialize (-1, ZoomEnum.FitWidth);
						subMainReportDrawing.Initialize (-1, ZoomEnum.FitWidth);
						break;
					case ZoomMode.FitPage:
					default:
						mainReportDrawing.Initialize (-1, ZoomEnum.FitPage);
						subMainReportDrawing.Initialize (-1, ZoomEnum.FitPage);
						break;
					}
				} else {
					subReportDrawing.Pages = null;
					mainReportDrawing.Pages = null;
					subMainReportDrawing.Pages = null;
					mainReportDrawing.Initialize (-1, ZoomEnum.FitPage);
					subMainReportDrawing.Initialize (-1, ZoomEnum.FitPage);
				}
				subReportDrawing.Initialize (-1, ZoomEnum.FitWidth);

				UpdatePageNumTextBoxText ();
				UpdateZoomComboBoxText ();
			} catch (System.Exception ex) {
            	
			} 
			if (RptQueryEnd != null) {
				RptQueryEnd (this, null);
			}
			return true;
		}

		public void Print ()
		{
			if (_ReportRuntime == null) {
				return;
			} 
			PrintDocument pd = new PrintDocument ();
			pd.PrinterSettings.FromPage = 1;
			pd.PrinterSettings.ToPage = _ReportRuntime.Pages.PageCount;
			pd.PrinterSettings.MaximumPage = _ReportRuntime.Pages.PageCount;
			pd.PrinterSettings.MinimumPage = 1;
			//pd.DefaultPageSettings.Landscape = _ReportRuntime.Pages.PageWidth > _ReportRuntime.Pages.PageHeight ? true : false;//是否横向打印
            
			pd.DefaultPageSettings.Landscape = _ReportRuntime.Pages.Landscape;//打印方向
			pd.EndPrint += new PrintEventHandler (PrintDocument_EndPrint);
			if (_ReportRuntime.PrintPara.PrintSet) {
				using (PrintDialog dlg = new PrintDialog ()) {
					dlg.Document = pd;
					dlg.AllowSelection = true;
					dlg.AllowSomePages = true;
					if (dlg.ShowDialog () == DialogResult.OK) {
						try {
							if (pd.PrinterSettings.PrintRange == PrintRange.Selection) {
								pd.PrinterSettings.FromPage = mainReportDrawing.PageCurrent;
							}
							mainReportDrawing.Print (pd);
						} catch (Exception ex) {
							_ReportRuntime.AddReportLog (ex.Message);
						}
					}
				}
			} else {
				try { 
					mainReportDrawing.Print (pd);
				} catch (Exception ex) {
					_ReportRuntime.AddReportLog (ex.Message);
				}
			} 
		}

		public void PrintEx (string filename)
		{
			if (string.IsNullOrEmpty (filename) || _ReportRuntime == null) {
				return;
			}
			try {
				PrintDocument pd = new PrintDocument ();
				pd.DocumentName = filename;
				pd.PrinterSettings.FromPage = 1;
				pd.PrinterSettings.ToPage = _ReportRuntime.Pages.PageCount;
				pd.PrinterSettings.MaximumPage = _ReportRuntime.Pages.PageCount;
				pd.PrinterSettings.MinimumPage = 1;
				//pd.DefaultPageSettings.Landscape = _ReportRuntime.Pages.PageWidth > _ReportRuntime.Pages.PageHeight ? true : false;//是否横向打印
				pd.DefaultPageSettings.Landscape = _ReportRuntime.Pages.Landscape;//打印方向
				pd.EndPrint += new PrintEventHandler (PrintDocument_EndPrint);
				mainReportDrawing.Print (pd); 
			} catch (Exception ex) {
				_ReportRuntime.AddReportLog (ex.Message);
			} 
		}

		public void PrintPreview ()
		{
		}

		void PrintDocument_EndPrint (object sender, PrintEventArgs e)
		{
			if (RptPrintEnd != null) {
				RptPrintEnd (this, null);
			} 
		}

		public void ExportReport ()
		{
			if (_ReportRuntime == null || null == mainReportDrawing) {
				return;
			}
			SaveFileDialog fileDialog = new SaveFileDialog ();
			fileDialog.Filter = "报表文档(*.rptx)|*.rptx|pdf文档(*.pdf)|*.pdf";
			if (fileDialog.ShowDialog () == DialogResult.OK) {
				ExportReport (fileDialog.FileName);
			}   
		}

		public void ExportReport (string filename)
		{
			if (null != mainReportDrawing) {
				mainReportDrawing.ExportReport (filename);
			}
			if (RptExportEnd != null) {
				RptExportEnd (this, null);
			} 
		}

		public void Release ()
		{
			if (null != _ReportRuntime) {
				_ReportRuntime.Release ();
				_ReportRuntime = null;
			}
			subReportDrawing.Pages = null;
			mainReportDrawing.Pages = null; 
			subMainReportDrawing.Pages = null; 
		}

		public int SetParameter (string strParaName, string strParaValue)
		{
			if (null != _ReportRuntime) {
				return _ReportRuntime.SetParameter (strParaName, strParaValue); 
			}
			return -1;
		}

		public object GetParameter (string strParaName)
		{
			object ret = null;

			return ret;
		}


		public int CallFunction (string strParaName, string strParaValue)
		{
			if (string.IsNullOrEmpty (strParaName) || string.IsNullOrEmpty (strParaValue))
				return -1;
			try {
				switch (strParaName.ToLower ()) {
				case "zoom":
					Zoom (Convert.ToSingle (strParaValue));
					break;
				case "zoomin":
					zoomin_Click (null, null);
					break;
				case "zoomout":
					zoomout_Click (null, null);
					break;
				case "fullpagedisplay":
					fitpage_Click (null, null);
					break;
				case "refresh":
					refresh_Click (null, null);
					break;
				case "print":
					Print ();
					break;
				case "printpreview":
					PrintPreview ();
					break;
				case "export":
					if (string.IsNullOrEmpty (strParaValue)) {
						ExportReport ();
					} else {
						ExportReport (strParaValue);
					}
					break;
				default:
					break;
				}
			} catch (System.Exception ex) {
				return -1;
			}
			return 0;
		}

		public int SetMultiDataSetParameter (String strDatasetPath, String strDSName, String strSQL)
		{
			if (null != _ReportRuntime) {
				if (null != _ReportRuntime.FieldTreeViewData) {
					FieldTreeNodeData node = _ReportRuntime.FieldTreeViewData.FindNodeByPath ("数据集." + strDatasetPath.Replace ('\\', '.'));
					if (null != node && node.Tag is SourceField) {
						SourceField sf = node.Tag as SourceField;
						if (null == sf.MultiDataSource)
							sf.MultiDataSource = new List<DSSqlPair> ();
						DSSqlPair sp = sf.MultiDataSource.Find (o => o.DataSource == strDSName);
						if (null != sp) {
							sp.Sql = strSQL;
						} else {
							sf.MultiDataSource.Add (new DSSqlPair (strDSName, strSQL));
						}
						return 1;
					}
				}
			}
			return 0;
		}

		public int SetDataSetParameter (String strDatasetPath, String strDSName)
		{
			if (null != _ReportRuntime) {
				if (null != _ReportRuntime.FieldTreeViewData) {
					FieldTreeNodeData node = _ReportRuntime.FieldTreeViewData.FindNodeByPath ("数据集." + strDatasetPath.Replace ('\\', '.'));
					if (null != node && node.Tag is SourceField) {
						SourceField sf = node.Tag as SourceField;
						if (strDSName.Contains (':')) {
							string[] strArr = strDSName.Split (":".ToArray ());
							if (strArr.Count () == 3 && strArr [1].Trim () == ":") {
								switch (strArr [0]) {
								case "SecondarySort":
									sf.SecondarySort = strArr [2];
									break;
								}
							}
						}
						sf.DBSource = strDSName;
						return 1;
					}
				}
			}
			return 0;
		}

		#endregion


		private void ReportViewer_Resize (object sender, EventArgs e)
		{
			int panel1Width = this.Width / 3;
			if (panel1Width > 250) {
				panel1Width = 250;
			}
			if (panel1Width > 0) {
				this.splitContainer1.SplitterDistance = panel1Width; 
			}
			UpdateZoomComboBoxText ();
		}

       
		public void OpenReportConfig (string fileName)
		{
			InitialReport ();
			QueryReport ();
		}

		private bool _SubReportDrawingSelectPage = false;

		void OnSubReportDrawingSelectPage (object sender, int pageNum)
		{
			_SubReportDrawingSelectPage = true;
			if (_MainReportDrawingIsFocus) {
				mainReportDrawing.ScrollToPage (pageNum);
			} else {
				subMainReportDrawing.ScrollToPage (pageNum);
			}
			_SubReportDrawingSelectPage = false;
		}

		void OnMainReportDrawingVScrollChanged (object sender, int vSScrollValue)
		{
			_MainReportDrawingIsFocus = true;
			if (_SubReportDrawingSelectPage == false) {
				subReportDrawing.ChangeVScrollValue (vSScrollValue);
			}
		}

		void OnMainReportDrawingSelectPage (object sender, int pageNum)
		{
			_MainReportDrawingIsFocus = true;
			UpdatePageNumTextBoxText ();
		}

		void OnMainReportDrawingZoomChanged (object sender, float zoom)
		{
			_MainReportDrawingIsFocus = true;
			UpdateZoomComboBoxText ();
		}

		void OnSubMainReportDrawingVScrollChanged (object sender, int vSScrollValue)
		{
			_MainReportDrawingIsFocus = false;
			if (_SubReportDrawingSelectPage == false) { 
				subReportDrawing.ChangeVScrollValue (vSScrollValue);
			}
		}

		void OnSubMainReportDrawingSelectPage (object sender, int pageNum)
		{
			_MainReportDrawingIsFocus = false;
			UpdatePageNumTextBoxText ();
		}

		void OnSubMainReportDrawingZoomChanged (object sender, float zoom)
		{
			_MainReportDrawingIsFocus = false;
			UpdateZoomComboBoxText ();
		}


		private void openfile_Click (object sender, EventArgs e)
		{
			OpenFileDialog fileDialog = new OpenFileDialog ();
			fileDialog.Multiselect = true;
			fileDialog.Filter = "(*.rpt)|*.rpt";
			if (fileDialog.ShowDialog () == DialogResult.OK) {
				if (OnOpenReport == null) {
					OpenReportConfig (fileDialog.FileName);
				} else {
					OnOpenReport (this, fileDialog.FileName);
				} 
			}     
		}

		private void refresh_Click (object sender, EventArgs e)
		{ 
			if (null != _ReportRuntime) {
				if (OnRefreshReport == null) {
					OpenReportConfig (_ReportRuntime.ReportFileName);
				} else {
					OnRefreshReport (this);
				}
			} 
		}

		private void saveas_Click (object sender, EventArgs e)
		{
			ExportReport (); 
		}

		private void Printer_Click (object sender, EventArgs e)
		{
			Print ();
		}

		private void preview_Click (object sender, EventArgs e)
		{
			this.splitContainer1.Panel1Collapsed = !this.splitContainer1.Panel1Collapsed; 
		}

		private void splitContainer1_MouseDoubleClick (object sender, EventArgs e)
		{
			this.splitContainer1.Panel1Collapsed = true;
		}

		private void subview_Click (object sender, EventArgs e)
		{
			this.splitContainer2.Panel1Collapsed = !this.splitContainer2.Panel1Collapsed;
			if (this.splitContainer2.Panel1Collapsed) {
				_MainReportDrawingIsFocus = true;
				mainReportDrawing.Focus ();
			} else {
				_MainReportDrawingIsFocus = false;
				subMainReportDrawing.Focus ();
				subMainReportDrawing.ScrollToPage (mainReportDrawing.PageCurrent);
				subMainReportDrawing.Zoom = mainReportDrawing.Zoom;
			}
           
		}

		private void splitContainer2_MouseDoubleClick (object sender, EventArgs e)
		{
			this.splitContainer2.Panel1Collapsed = true;
			_MainReportDrawingIsFocus = true;
			mainReportDrawing.Focus ();
		}

        
         
		private void firstpage_Click (object sender, EventArgs e)
		{
			if (_ReportRuntime == null) {
				return;
			} 

			if (_MainReportDrawingIsFocus) {
				if (mainReportDrawing.PageCurrent != 1) {
					mainReportDrawing.ScrollToPage (1);
					UpdatePageNumTextBoxText ();
				}
			} else {
				if (subMainReportDrawing.PageCurrent > 1) {
					subMainReportDrawing.ScrollToPage (1);
					UpdatePageNumTextBoxText ();
				}
			}
		}

		private void prepage_Click (object sender, EventArgs e)
		{
			if (_ReportRuntime == null) {
				return;
			} 

			if (_MainReportDrawingIsFocus) {
				if (mainReportDrawing.PageCurrent > 1) {
					mainReportDrawing.ScrollToPage (mainReportDrawing.PageCurrent - 1);
					UpdatePageNumTextBoxText ();
				}
			} else {
				if (subMainReportDrawing.PageCurrent > 1) {
					subMainReportDrawing.ScrollToPage (subMainReportDrawing.PageCurrent - 1);
					UpdatePageNumTextBoxText ();
				}
			}
           
		}

		private void nextpage_Click (object sender, EventArgs e)
		{
			if (_ReportRuntime == null) {
				return;
			}
            
			if (_MainReportDrawingIsFocus) {
				if (mainReportDrawing.PageCurrent < _ReportRuntime.Pages.PageCount) {
					mainReportDrawing.ScrollToPage (mainReportDrawing.PageCurrent + 1);
					UpdatePageNumTextBoxText ();
				}
			} else {
				if (subMainReportDrawing.PageCurrent < _ReportRuntime.Pages.PageCount) {
					subMainReportDrawing.ScrollToPage (subMainReportDrawing.PageCurrent + 1);
					UpdatePageNumTextBoxText ();
				}
			}
		}

		private void lastpage_Click (object sender, EventArgs e)
		{
			if (_ReportRuntime == null) {
				return;
			}
			if (_MainReportDrawingIsFocus) {
				if (mainReportDrawing.PageCurrent < _ReportRuntime.Pages.PageCount) {
					mainReportDrawing.ScrollToPage (_ReportRuntime.Pages.PageCount);
					UpdatePageNumTextBoxText ();
				}
			} else {
				if (subMainReportDrawing.PageCurrent < _ReportRuntime.Pages.PageCount) {
					subMainReportDrawing.ScrollToPage (_ReportRuntime.Pages.PageCount);
					UpdatePageNumTextBoxText ();
				}
			}
		}

		private void UpdatePageNumTextBoxText ()
		{

			if (_ReportRuntime == null) {
				return;
			}
			try {
				if (_MainReportDrawingIsFocus) {
					if (mainReportDrawing.PageCurrent > _ReportRuntime.Pages.PageCount) {
						pagenumtextbox.Text = "0/0";
					} else {
						pagenumtextbox.Text = string.Format ("{0}/{1}", mainReportDrawing.PageCurrent, _ReportRuntime.Pages.PageCount);
					} 
				} else {
					if (subMainReportDrawing.PageCurrent > _ReportRuntime.Pages.PageCount) {
						pagenumtextbox.Text = "0/0";
					} else {
						pagenumtextbox.Text = string.Format ("{0}/{1}", subMainReportDrawing.PageCurrent, _ReportRuntime.Pages.PageCount);
					} 
				}
                
                 
			} catch {

			}
		}

		private void ShowPage (string strPageNumAndPageCount)
		{
			if (string.IsNullOrEmpty (strPageNumAndPageCount)) {
				return;
			}
			try {
				string[] str = strPageNumAndPageCount.Split ("//".ToCharArray ());
				if (string.IsNullOrEmpty (str [0]) == false) {
					int pageNum = 1;
					if (int.TryParse (str [0], out pageNum)) {
						if (_MainReportDrawingIsFocus) {

							if (mainReportDrawing.PageCurrent != pageNum && pageNum > 0 &&
							                         pageNum <= _ReportRuntime.Pages.PageCount) {
								mainReportDrawing.ScrollToPage (pageNum);
							}
						} else {

							if (subMainReportDrawing.PageCurrent != pageNum && pageNum > 0 &&
							                         pageNum <= _ReportRuntime.Pages.PageCount) {
								subMainReportDrawing.ScrollToPage (pageNum);
							} 
						}

					} 
				}
			} catch {

			} 
		}

		private void pagenumtextbox_KeyDown (object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter) {
				if (_ReportRuntime == null) {
					return;
				}
				if (_MainReportDrawingIsFocus) {
					mainReportDrawing.Focus ();
				} else {
					subMainReportDrawing.Focus ();
				}
			}
		}

		private void pagenumtextbox_Leave (object sender, EventArgs e)
		{
			ShowPage (pagenumtextbox.Text);   
		}

		private void fitsize_Click (object sender, EventArgs e)
		{

			if (_ReportRuntime == null) {
				return;
			}
			if (_MainReportDrawingIsFocus) {
				if (mainReportDrawing.ZoomMode != ZoomEnum.FitSize) {
					mainReportDrawing.ZoomMode = ZoomEnum.FitSize;
				}
			} else { 
				if (subMainReportDrawing.ZoomMode != ZoomEnum.FitSize) {
					subMainReportDrawing.ZoomMode = ZoomEnum.FitSize;
				}
			} 
		}

		private void fitpage_Click (object sender, EventArgs e)
		{
			if (_ReportRuntime == null) {
				return;
			} 
			if (_MainReportDrawingIsFocus) {
				if (mainReportDrawing.ZoomMode != ZoomEnum.FitPage) {
					mainReportDrawing.ZoomMode = ZoomEnum.FitPage;
				}
			} else {
				if (subMainReportDrawing.ZoomMode != ZoomEnum.FitPage) {
					subMainReportDrawing.ZoomMode = ZoomEnum.FitPage;
				}
			} 
            
		}

		private void fitwidth_Click (object sender, EventArgs e)
		{
			if (_ReportRuntime == null) {
				return;
			}

			if (_MainReportDrawingIsFocus) {
				if (mainReportDrawing.ZoomMode != ZoomEnum.FitWidth) {
					mainReportDrawing.ZoomMode = ZoomEnum.FitWidth;
				}
			} else {
				if (subMainReportDrawing.ZoomMode != ZoomEnum.FitWidth) {
					subMainReportDrawing.ZoomMode = ZoomEnum.FitWidth;
				}
			} 
              
		}

		private void Zoom (float zoom)
		{
			if (zoom <= 0.1f) {
				zoom = 0.1f;
			}
			if (zoom > 64.0f) {
				zoom = 64.0f;
			}
			if (_MainReportDrawingIsFocus) {
				if (mainReportDrawing.Zoom != zoom) {
					mainReportDrawing.Zoom = zoom;
				}
			} else {
				if (subMainReportDrawing.Zoom != zoom) {
					subMainReportDrawing.Zoom = zoom;
				} 
			} 
          
		}

		private void zoomout_Click (object sender, EventArgs e)
		{
			if (_ReportRuntime == null) {
				return;
			}
			if (_MainReportDrawingIsFocus) {
				float zoom = (mainReportDrawing.Zoom * 100 - 10) / 100;
				Zoom (zoom); 
			} else {
				float zoom = (subMainReportDrawing.Zoom * 100 - 10) / 100;
				Zoom (zoom);  
			} 
            
		}

		private void zoomin_Click (object sender, EventArgs e)
		{
			if (_ReportRuntime == null) {
				return;
			}

			if (_MainReportDrawingIsFocus) {
				float zoom = (mainReportDrawing.Zoom * 100 + 10) / 100;
				Zoom (zoom);
			} else {
				float zoom = (subMainReportDrawing.Zoom * 100 + 10) / 100;
				Zoom (zoom);
			}  
		}

		private void UpdateZoomComboBoxText ()
		{
			if (_ReportRuntime == null) {
				return;
			}
			if (_MainReportDrawingIsFocus) {
				zoomcombobox.Text = string.Format ("{0:0}%", mainReportDrawing.Zoom * 100);
			} else {
				zoomcombobox.Text = string.Format ("{0:0}%", subMainReportDrawing.Zoom * 100); 
			}   
		}

		private void zoomcombobox_SelectedIndexChanged (object sender, EventArgs e)
		{
			if (_ReportRuntime == null) {
				return;
			}
			string str = zoomcombobox.Text.TrimEnd ("%%".ToCharArray ());
			float zoom = 10.0f;
			if (float.TryParse (str, out zoom)) {
				zoom = zoom / 100;
				Zoom (zoom); 
			}
		}

		private void zoomcombobox_Leave (object sender, EventArgs e)
		{
			if (_ReportRuntime == null) {
				return;
			}
			string str = zoomcombobox.Text.TrimEnd ("%%".ToCharArray ());
			float zoom = 10.0f;
			if (float.TryParse (str, out zoom)) {
				zoom = zoom / 100;
				Zoom (zoom); 
			} 
		}

		private void zoomcombobox_KeyDown (object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter) {
				if (_ReportRuntime == null) {
					return;
				}
				if (_MainReportDrawingIsFocus) {
					mainReportDrawing.Focus ();
				} else {
					subMainReportDrawing.Focus ();
				}    
			} 
		}

		private void log_Click (object sender, EventArgs e)
		{
			if (_ReportRuntime != null) {
				LogMessageForm logFrm = new LogMessageForm ();
				logFrm.LogMessage = _ReportRuntime.ReportLogMessage;
				logFrm.ShowDialog ();
			}
		}

		public delegate void OnConfigDelegate (object sender);

		public event OnConfigDelegate OnConfig = null;

		private void Configer_Click (object sender, EventArgs e)
		{
			if (OnConfig != null) {
				OnConfig (this);
			}
		}

		private void subMainReportDrawing_Enter (object sender, EventArgs e)
		{
			_MainReportDrawingIsFocus = false;
		}

		private void mainReportDrawing_Enter (object sender, EventArgs e)
		{
			_MainReportDrawingIsFocus = true;
		}

		private void ReportViewer_Load (object sender, EventArgs e)
		{
			if (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentEnvironment == PMS.Libraries.ToolControls.PMSPublicInfo.MESEnvironment.MESReportRunner) {
				if (!toolStrip1.Items.Contains (this.Configer))
					toolStrip1.Items.Add (this.Configer);
			}
		}
         
         
	}
}
