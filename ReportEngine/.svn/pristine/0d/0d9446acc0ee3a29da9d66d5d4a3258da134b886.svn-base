using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using PMS.Libraries.ToolControls.PMSPublicInfo; 
using System.Drawing;
using System.Xml;
using PMS.Libraries.ToolControls.ToolBox;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using PMS.Libraries.ToolControls.PMSReport;
namespace NetSCADA.ReportEngine
{ 
    class ReportFileObj:IDisposable
    {
        public ReportFileObj()
        {

        }
        public void Dispose()
        {
            try
            {
                if (null != _DesignerControl)
                {
                    _DesignerControl.Dispose();
                    _DesignerControl = null;
                    GC.Collect();
                }
                
            }
            catch (System.Exception ex)
            { 
            }
        }
       
        private Host.DesignerControl _DesignerControl = null;
        

        public bool LoadReportConfig(string rptFile)
        {
            try
            {
                Dispose();
                if(_FieldTreeViewData != null)
                {
                    _FieldTreeViewData = null;
                }
                MESReportFileObj reportFileObj = new MESReportFileObj();
                if (!DBFileManager.LoadReportFile(rptFile, ref reportFileObj))
                    return false; 
                _DesignerControl = new Host.DesignerControl(reportFileObj.strXMLDoc, 1,false); 
                _FieldTreeViewData = reportFileObj.dataSource;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        } 
        private List<Control> GetReportCollapsiblePanelControls(string strName)
        {
            if (null != _DesignerControl)
            {
                System.Windows.Forms.Control.ControlCollection ctCollection = _DesignerControl.HostSurface.rootForm.Controls;
                foreach (Control child in ctCollection)
                {
                    if (string.Compare(child.Name, strName, true) == 0 &&
                        child.GetType() == typeof(PMS.Libraries.ToolControls.CollapsiblePanel))
                    {
                        Control ctrl = ((PMS.Libraries.ToolControls.CollapsiblePanel)child).WorkingArea;
                        if (ctrl != null)
                        {
                            List<Control> ctrlList = new List<Control>();
                            foreach (Control childCtrl in ctrl.Controls)
                            {
                                ctrlList.Add(childCtrl);
                            }
                            return  CloneControlList(ctrlList);
                        }
                    }
                }
            }
            return null;
        }

        private List<Control> CloneControlList(List<Control> original)
        {
            List<Control> newList = new List<Control>();
            if (original != null)
            {
                foreach (Control old in original)
                {
                    ICloneable ic = old as ICloneable;
                    if (ic != null)
                    {
                        object tempObject = ic.Clone();
                        newList.Add(tempObject as Control);
                    }
                }
            }
            return newList;
        }

        private Control GetReportCollapsiblePanel(string strName, ref int panelHeight)
        {
            if (null != _DesignerControl)
            {
                System.Windows.Forms.Control.ControlCollection ctCollection = _DesignerControl.HostSurface.rootForm.Controls;
                foreach (Control child in ctCollection)
                { 
                    if (string.Compare(child.Name,strName,true) == 0 &&
                        child.GetType() == typeof(PMS.Libraries.ToolControls.CollapsiblePanel))
                    {
                        panelHeight = ((PMS.Libraries.ToolControls.CollapsiblePanel)child).OriginalHeight;
                        return ((PMS.Libraries.ToolControls.CollapsiblePanel)child).WorkingArea; 
                    }
                }
            }
            return null;
        }


        public Control GetReportHeader()
        {
            return GetReportCollapsiblePanel("ReportHeader", ref _ReportHeaderHeight);  
        }
        public Control GetPageHeader()
        {
            return GetReportCollapsiblePanel("PageHeader", ref _PageHeaderHeight);  
        }
        public Control GetDetails()
        {
            int height = 0;
            return GetReportCollapsiblePanel("Details", ref height);  
        } 
        public Control GetPageFooter()
        {
            return GetReportCollapsiblePanel("PageFooter", ref _PageFooterHeight);  
        }
        public Control GetReportFooter()
        {
            return GetReportCollapsiblePanel("ReportFooter", ref _ReportFooterHeight);  
        }


        public List<Control> GetReportHeaderControls()
        { 
           return  GetReportCollapsiblePanelControls("ReportHeader"); 
        }

        public List<Control> GetPageHeaderControls()
        {
            return GetReportCollapsiblePanelControls("PageHeader");  
        }

        public List<Control> GetDetailsControls()
        {
            return GetReportCollapsiblePanelControls("Details");   
        }

        public List<Control> GetPageFooterControls()
        {
            return GetReportCollapsiblePanelControls("PageFooter");   
        }

        public List<Control> GetReportFooterControls()
        {
            return GetReportCollapsiblePanelControls("ReportFooter");   
        }

       
        
        public TreeView GetUIExpressionTreeView()
        {
            if (null != _DesignerControl)
            {
                return _DesignerControl.GetUIExpressionTreeView();
            }
            return null;
        }

        public Component GetPrintPara()
        {
            if (null != _DesignerControl)
            {
                ComponentCollection ccCollection = _DesignerControl.DesignerHost.Container.Components;
                foreach (Component child in ccCollection)
                {
                    if (child.GetType() == typeof(PMSPrintPara))
                    {
                        return child;
                    }
                }
            }
            return new PMSPrintPara();
        }

        public Component GetToolBarPara()
        {
            if (null != _DesignerControl)
            {
                ComponentCollection ccCollection = _DesignerControl.DesignerHost.Container.Components;
                foreach (Component child in ccCollection)
                {
                    //if (child.GetType() == typeof(ReportViewerToolBar))
                    {
                        return child;
                    }
                }
            }
            return null;
        }
        public int ReportWidth
        { 
            get
            {
                int width =0;
                if (null != _DesignerControl)
                {
                    width = _DesignerControl.HostSurface.rootForm.Width;
                }
                return width;
            } 
        }

        private int _ReportHeaderHeight = 0;
        public int ReportHeaderHeight
        {
            get
            {
                return _ReportHeaderHeight;
            }
        }

        private int _PageHeaderHeight = 0;
        public int PageHeaderHeight
        {
            get
            {
                return _PageHeaderHeight;
            }
        }

        private int _PageFooterHeight = 0;
        public int PageFooterHeight
        {
            get
            {
                return _PageFooterHeight;
            }
        }

        private int _ReportFooterHeight = 0;
        public int ReportFooterHeight
        {
            get
            {
                return _ReportFooterHeight;
            }
        }
        private FieldTreeViewData _FieldTreeViewData = null;
        public FieldTreeViewData FieldTreeViewData
        {
            get
            {
                return _FieldTreeViewData;
            }
        }  
    }

    
}
