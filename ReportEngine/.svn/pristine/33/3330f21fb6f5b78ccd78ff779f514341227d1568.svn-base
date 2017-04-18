using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using Loader;
using MES.PublicInterface;
using PMS.Libraries.ToolControls.PMSPublicInfo;

namespace Host
{
	/// <summary>
	/// Hosts the HostSurface which inherits from DesignSurface.
	/// </summary>
	public class DesignerControl : System.Windows.Forms.UserControl
	{
		#region property

		public HostSurface HostSurface {
			get {
				return _hostSurface;
			}
		}

		public IDesignerHost DesignerHost {
			get {
				if (null == _hostSurface)
					return null;
				return (IDesignerHost)_hostSurface.GetService (typeof(IDesignerHost));
			}
		}

		#endregion

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private ToolStripMenuItem undoToolStripMenuItem;
		private ToolStripMenuItem redoToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator5;
		private ToolStripMenuItem cutToolStripMenuItem;
		private ToolStripMenuItem copyToolStripMenuItem;
		private ToolStripMenuItem pasteToolStripMenuItem;
		private ToolStripMenuItem ToolStripMenuItem_Del;
		private ToolStripSeparator toolStripSeparator6;
		private ToolStripMenuItem selectAllToolStripMenuItem;
		private ToolStripMenuItem ToolStripMenuItem_Align;
		private ToolStripMenuItem ToolStripMenuItem_Lefts;
		private ToolStripMenuItem ToolStripMenuItem_Centers;
		private ToolStripMenuItem ToolStripMenuItem_Rights;
		private ToolStripMenuItem ToolStripMenuItem_Tops;
		private ToolStripMenuItem ToolStripMenuItem_Middles;
		private ToolStripMenuItem ToolStripMenuItem_Bottoms;
		private HostSurface _hostSurface;

		public DesignerControl ()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent ();
			//_hostSurface = new HostSurface();
			HostSurface hostSurface = new HostSurface ();
			InitializeHost (hostSurface);
		}

		public DesignerControl (string strFilePath)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent ();
			//_hostSurface = new HostSurface(strFilePath);
			HostSurface hostSurface = new HostSurface (strFilePath);
			InitializeHost (hostSurface);
		}

		public DesignerControl (string strXml, int justflag)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent ();
			//_hostSurface = new HostSurface(strXml, 1);
			HostSurface hostSurface = new HostSurface (strXml, 1);
			InitializeHost (hostSurface);
		}

		public DesignerControl (string strXml, string strFilepath, int justflag)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent ();
			//_hostSurface = new HostSurface(strXml, strFilepath, 1);
			HostSurface hostSurface = new HostSurface (strXml, strFilepath, 1);
			InitializeHost (hostSurface);
		}

		public DesignerControl (string strXml, int justflag, bool UseAloneOnly)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent ();
			//_hostSurface = new HostSurface(strXml, 1, UseAloneOnly);
			HostSurface hostSurface = new HostSurface (strXml, 1, UseAloneOnly);
			InitializeHost (hostSurface);
		}

		public DesignerControl (string strFilePath, bool UseAloneOnly)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent ();
			//_hostSurface = new HostSurface(strFilePath, UseAloneOnly);
			HostSurface hostSurface = new HostSurface (strFilePath, UseAloneOnly);
			InitializeHost (hostSurface);
		}

		public void BeginLoad ()
		{
			_hostSurface.BeginLoad ();
			InitializeHost (_hostSurface);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose (bool disposing)
		{
			if (disposing) {
				if (components != null)
					components.Dispose ();
				if (_hostSurface != null) {
					_hostSurface.Dispose ();
					_hostSurface = null;
				}
			}
			base.Dispose (disposing);
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ()
		{
			// todo:qiuleilei 20161213
			if (CurrentPrjInfo.CurrentEnvironment == MESEnvironment.MESReportServer)
				return;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager (typeof(DesignerControl));
			this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator ();
			this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.ToolStripMenuItem_Del = new System.Windows.Forms.ToolStripMenuItem ();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator ();
			this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
			this.ToolStripMenuItem_Align = new System.Windows.Forms.ToolStripMenuItem ();
			this.ToolStripMenuItem_Lefts = new System.Windows.Forms.ToolStripMenuItem ();
			this.ToolStripMenuItem_Centers = new System.Windows.Forms.ToolStripMenuItem ();
			this.ToolStripMenuItem_Rights = new System.Windows.Forms.ToolStripMenuItem ();
			this.ToolStripMenuItem_Tops = new System.Windows.Forms.ToolStripMenuItem ();
			this.ToolStripMenuItem_Middles = new System.Windows.Forms.ToolStripMenuItem ();
			this.ToolStripMenuItem_Bottoms = new System.Windows.Forms.ToolStripMenuItem ();
			this.SuspendLayout ();
			// 
			// undoToolStripMenuItem
			// 
			this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
			this.undoToolStripMenuItem.Size = new System.Drawing.Size (118, 22);
			this.undoToolStripMenuItem.Text = "撤销";
			// 
			// redoToolStripMenuItem
			// 
			this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
			this.redoToolStripMenuItem.Size = new System.Drawing.Size (118, 22);
			this.redoToolStripMenuItem.Text = "重做";
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size (115, 6);
			// 
			// cutToolStripMenuItem
			// 
			this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject ("cutToolStripMenuItem.Image")));
			this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			this.cutToolStripMenuItem.Size = new System.Drawing.Size (118, 22);
			this.cutToolStripMenuItem.Text = "剪切";
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject ("copyToolStripMenuItem.Image")));
			this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size (118, 22);
			this.copyToolStripMenuItem.Text = "复制";
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject ("pasteToolStripMenuItem.Image")));
			this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size (118, 22);
			this.pasteToolStripMenuItem.Text = "粘贴";
			// 
			// ToolStripMenuItem_Del
			// 
			this.ToolStripMenuItem_Del.Name = "ToolStripMenuItem_Del";
			this.ToolStripMenuItem_Del.Size = new System.Drawing.Size (118, 22);
			this.ToolStripMenuItem_Del.Text = "删除";
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size (115, 6);
			// 
			// selectAllToolStripMenuItem
			// 
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.Size = new System.Drawing.Size (118, 22);
			this.selectAllToolStripMenuItem.Text = "全选";
			// 
			// ToolStripMenuItem_Align
			// 
			this.ToolStripMenuItem_Align.DropDownItems.AddRange (new System.Windows.Forms.ToolStripItem[] {
				this.ToolStripMenuItem_Lefts,
				this.ToolStripMenuItem_Centers,
				this.ToolStripMenuItem_Rights,
				this.ToolStripMenuItem_Tops,
				this.ToolStripMenuItem_Middles,
				this.ToolStripMenuItem_Bottoms
			});
			this.ToolStripMenuItem_Align.Name = "ToolStripMenuItem_Align";
			this.ToolStripMenuItem_Align.Size = new System.Drawing.Size (118, 22);
			this.ToolStripMenuItem_Align.Text = "对齐方式";
			// 
			// ToolStripMenuItem_Lefts
			// 
			this.ToolStripMenuItem_Lefts.Name = "ToolStripMenuItem_Lefts";
			this.ToolStripMenuItem_Lefts.Size = new System.Drawing.Size (118, 22);
			this.ToolStripMenuItem_Lefts.Text = "左对齐";
			// 
			// ToolStripMenuItem_Centers
			// 
			this.ToolStripMenuItem_Centers.Name = "ToolStripMenuItem_Centers";
			this.ToolStripMenuItem_Centers.Size = new System.Drawing.Size (118, 22);
			this.ToolStripMenuItem_Centers.Text = "水平居中";
			// 
			// ToolStripMenuItem_Rights
			// 
			this.ToolStripMenuItem_Rights.Name = "ToolStripMenuItem_Rights";
			this.ToolStripMenuItem_Rights.Size = new System.Drawing.Size (118, 22);
			this.ToolStripMenuItem_Rights.Text = "右对齐";
			// 
			// ToolStripMenuItem_Tops
			// 
			this.ToolStripMenuItem_Tops.Name = "ToolStripMenuItem_Tops";
			this.ToolStripMenuItem_Tops.Size = new System.Drawing.Size (118, 22);
			this.ToolStripMenuItem_Tops.Text = "顶端对齐";
			// 
			// ToolStripMenuItem_Middles
			// 
			this.ToolStripMenuItem_Middles.Name = "ToolStripMenuItem_Middles";
			this.ToolStripMenuItem_Middles.Size = new System.Drawing.Size (118, 22);
			this.ToolStripMenuItem_Middles.Text = "垂直居中";
			// 
			// ToolStripMenuItem_Bottoms
			// 
			this.ToolStripMenuItem_Bottoms.Name = "ToolStripMenuItem_Bottoms";
			this.ToolStripMenuItem_Bottoms.Size = new System.Drawing.Size (118, 22);
			this.ToolStripMenuItem_Bottoms.Text = "底端对齐";
			// 
			// DesignerControl
			// 
			this.Name = "DesignerControl";
			this.Size = new System.Drawing.Size (800, 600);
			this.Load += new System.EventHandler (this.DesignerControl_Load);
			this.ResumeLayout (false);

		}

		#endregion

		internal void InitializeHost (HostSurface hostSurface)
		{
			try {
				if (hostSurface == null)
					return;

				_hostSurface = hostSurface;
				// todo:qiuleilei 20161213
				if (CurrentPrjInfo.CurrentEnvironment == MESEnvironment.MESReportServer)
					return;
				Control control = _hostSurface.View as Control;
                
				control.Parent = this;
				control.Dock = DockStyle.Fill;
				control.Visible = true;
				control.Invalidate ();
				control.Refresh ();

				//TextBox tb = new TextBox();
				//tb.BackColor = Color.Pink;
				//tb.Location = new Point(100, 100);
				//control.Controls[0].Controls.Add(tb);
				//tb.BringToFront();
			} catch (Exception ex) {
				Trace.WriteLine (ex.ToString ());
			}
		}

		private void DesignerControl_Load (object sender, EventArgs e)
		{
			if (_hostSurface == null)
				return;
			_hostSurface.SelectionChanged ();
		}

		private void toolStripButton_Click (object sender, EventArgs e)
		{
			if (_hostSurface == null)
				return;
			_hostSurface.DoAction ((sender as ToolStripMenuItem).Text);
		}

		private void toolStripButton_TabOrder_Click (object sender, EventArgs e)
		{
			if (_hostSurface == null)
				return;
			_hostSurface.SwitchTabOrder ();
		}

		private void toolStripMenuItem4_Click (object sender, EventArgs e)
		{

		}

		private void OnMenuClick (object sender, EventArgs e)
		{
			_hostSurface.DoAction ((sender as ToolStripMenuItem).Text);
		}

		public TreeView GetUIExpressionTreeView ()
		{
			TreeView tv = new TreeView ();
			TreeNode tn = PMS.Libraries.ToolControls.ToolBox.LinqToWindowsForms.TreeExtensions.GetTreeViewUIExpressionNode (GetRootControl (this), ref tv, ToolboxLibrary.ToolboxItems.userControlsToolTypes);
			if (tv.ImageList.Images.ContainsKey ("Column") == false)
				tv.ImageList.Images.Add ("Column", Properties.Resources.field);
			tn = ProcessUIExpressionTreeNode (tn);
			tv.Nodes.Add (tn);
			return tv;
		}

		private Control GetRootControl (Host.DesignerControl dsf)
		{
			return dsf.HostSurface.rootForm;
		}

		private TreeNode ProcessUIExpressionTreeNode (TreeNode item)
		{
			if (null == item)
				return null;
			if (item.Tag is Form) {
				// 传递给表达式计算的结构树顶节点为UI
				item.Name = "UI";
				item.Text = "UI";
				item.Tag = null;
			}

			// 去除页眉（PageHeader）页脚（PageFooter）两銮?颍?饬礁銮?虿恢С直泶锸继
			item.Nodes.RemoveByKey ("PageHeader");
			item.Nodes.RemoveByKey ("PageFooter");

			foreach (TreeNode treenode in item.Nodes) {
				treenode.Tag = null;
				ProcessTreeViewUIExpressionNextNode (treenode);
			}
			return item;
		}

		private void ProcessTreeViewUIExpressionNextNode (TreeNode item)
		{
			foreach (TreeNode treenode in item.Nodes) {
				if (treenode.Tag is IUIDesignExpStruct) {
					IUIDesignExpStruct uides = treenode.Tag as IUIDesignExpStruct;
					treenode.Tag = uides.ExpStructNode.Tag;
					if (uides.ExpStructNode.Nodes.Count > 0) {
						foreach (TreeNode tn in uides.ExpStructNode.Nodes) {
							tn.Name = tn.Text;
							tn.ImageKey = tn.SelectedImageKey = "Column";
							treenode.Nodes.Add (tn);
						}
					}
				}

				ProcessTreeViewUIExpressionNextNode (treenode);
			}
		}

		//protected override bool ProcessDialogKey(Keys keyData)
		//{
		//PMS.WinFormsUI.Docking.IDockContent dc = this.dockPanel1.ActiveContent;

		//if (dc != null)
		//{
		//    if (dc != CurrentDesignAndScriptForm)
		//    {
		//        // ToolForm
		//        PMS.Libraries.ToolControls.PMSPublicInfo.Message msg = new PMS.Libraries.ToolControls.PMSPublicInfo.Message();
		//        msg.SendMsgToMainForm(dc.DockHandler.Form.Handle, PMS.Libraries.ToolControls.PMSPublicInfo.Message.WM_KEYDOWN, (IntPtr)keyData, IntPtr.Zero);
		//    }
		//    else
		//    {
		//        if (CurrentRunTimeSimulatorForm != null)
		//        {
		//            if (CurrentDesignAndScriptForm != null)
		//            {
		//                if (CurrentDesignAndScriptForm.CurrentTab == GlobalNameSpace.MyGlobalClass.TabType.Designer)
		//                    CurrentDesignAndScriptForm.DesignerControl.HostSurface.DoAction(keyData);
		//            }
		//        }
		//    }
		//}
		//HostSurface.DoAction(keyData);
		//return base.ProcessDialogKey(keyData);
		//}
	}
	// class
}
// namespace