using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing; 

using PMS.Libraries.ToolControls.PMSPublicInfo;
using PMS.Libraries.ToolControls.Report; 
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData; 

namespace NetSCADA.ReportEngine
{
    public class ReportPages
    { 
        ReportPage _currentPage = null; 
        public ReportPages()
        { 
        }

        #region 基本属性

        
        public ReportRuntime _ReportRuntime = null;
        /// <summary>
        /// 关联的报表引擎
        /// </summary>
        public ReportRuntime ReportRuntime
        {
            get { return _ReportRuntime; }
            set { _ReportRuntime = value; }
        }  
        
        #endregion


        #region 打印参数


        private bool _EnablePrintZoom = true;
        /// <summary>
        /// 启用打印缩放
        /// </summary>
        public bool EnablePrintZoom
        {
            get { return _EnablePrintZoom; }
            set { _EnablePrintZoom = value; }
        }

        private bool _Landscape = false; 
        /// <summary>
        /// 打印方向:false：纵向；true：横向
        /// </summary>
        public bool Landscape
        {
            get { return _Landscape; }
            set { _Landscape = value; }
        }

        private float _PageWidth = 8.2677f;//英寸
        /// <summary>
        /// 页宽
        /// </summary>
        public float PageWidth
        {
            get{return _PageWidth;}
            set{_PageWidth = value;}
        }

        private float _PageHeight = 11.6929f;
        /// <summary>
        /// 页高
        /// </summary>
        public float PageHeight
        {
            get{return _PageHeight;}
            set{_PageHeight = value;}
        }
        /// <summary>
        /// 页面包含工具条
        /// </summary>
        public List<string> ToolBarItemNames
        {
            get;
            set;
        }
        #endregion

        #region 页眉、页脚属性

        private float _PageHeadHeight = 0.0f;
        /// <summary>
        /// 页眉高
        /// </summary>
        public float PageHeadHeight
        {
            get{return _PageHeadHeight;  }
            set{_PageHeadHeight = value;}
        }
        private List<ReportElement> _PageHeadReportElementList = new List<ReportElement>();
        /// <summary>
        /// 页眉关联的报表元素
        /// </summary>
        public List<ReportElement> PageHeadReportElements
        {
            get
            {
                return _PageHeadReportElementList;
            }
        }
        private float _PageFootHeight= 0.0f;
        /// <summary>
        /// 页脚高
        /// </summary>
        public float PageFootHeight
        {
            get { return _PageFootHeight; }
            set { _PageFootHeight = value; }
        }
        private List<ReportElement> _PageFootReportElementList = new List<ReportElement>();
        /// <summary>
        /// 页脚关联的报表元素
        /// </summary>
        public List<ReportElement> PageFootReportElements
        {
            get
            {
                return _PageFootReportElementList;
            }
        }

        #endregion

        #region 背景色

        private Color _ReportHeadBackColor = Color.White;
        /// <summary>
        /// 报表头背景色
        /// </summary>
        public Color ReportHeadBackColor
        {
            set{_ReportHeadBackColor = value;}            
            get{return _ReportHeadBackColor;}    
        }
        private Color _PageHeadBackColor = Color.White;
        /// <summary>
        /// 页眉背景色
        /// </summary>
        public Color PageHeadBackColor
        {
            set{_PageHeadBackColor = value;}            
            get{return _PageHeadBackColor;}    
        }

        private Color _DataRegionBackColor = Color.White;
        /// <summary>
        /// 数据区背景色
        /// </summary>
        public Color DataRegionBackColor
        {
            set{_DataRegionBackColor = value;}            
            get{return _DataRegionBackColor;}    
        }
        private Color _PageFootBackColor = Color.White;
        /// <summary>
        /// 页脚背景色
        /// </summary>
        public Color PageFootBackColor
        {
            set{_PageFootBackColor = value;}            
            get{return _PageFootBackColor;}    
        }
        private Color _ReportFootBackColor = Color.White;
        /// <summary>
        /// 报表尾背景色
        /// </summary>
        public Color ReportFootBackColor
        {
            set{_ReportFootBackColor = value;}            
            get{return _ReportFootBackColor;}    
        }

        #endregion

        #region Margins

        private float _PageLeftMargin = 0.1f;
        /// <summary>
        /// Left Margin
        /// </summary>
        public float PageLeftMargin
        {
            get { return _PageLeftMargin; }
            set { _PageLeftMargin = value; }
        }

        private float _PageRightMargin = 0.1f;
        /// <summary>
        /// Right Margin
        /// </summary>
        public float PageRightMargin
        {
            get { return _PageRightMargin; }
            set { _PageRightMargin = value; }
        }

        private float _PageTopMargin = 0.1f;
        /// <summary>
        /// Top Margin
        /// </summary>
        public float PageTopMargin
        {
            get { return _PageTopMargin; }
            set { _PageTopMargin = value; }
        }

        private float _PageBottomMargin = 0.1f;
        /// <summary>
        /// Bottom Margin
        /// </summary>
        public float PageBottomMargin
        {
            get { return _PageBottomMargin; }
            set { _PageBottomMargin = value; }
        }
         
        private float _ReportConfigDpiX = 96.0f;
        /// <summary>
        /// 配置报表的机器对应的水平方向的DPI
        /// </summary>
        public float ReportConfigDpiX
        {
            get { return _ReportConfigDpiX; }
            set { _ReportConfigDpiX = value; }
        }

        private float _ReportConfigDpiY = 96.0f;
        /// <summary>
        /// 配置报表的机器对应的垂直方向的DPI
        /// </summary>
        public float ReportConfigDpiY
        {
            get { return _ReportConfigDpiY; }
            set { _ReportConfigDpiY = value; }
        }
        

        #endregion


        #region  页参数 

        private float _PageDataRegionHeight = 0.0f;
        /// <summary>
        /// 报表数据区绘图区的高度(英寸）
        /// </summary>
        public float PageDataRegionHeight
        {
            get
            {
                return _PageDataRegionHeight;
            }
        }


        private float _PageDrawRegionHeight = 0.0f;
        /// <summary>
        /// 报表绘图区的高度(英寸）
        /// </summary>
        public float PageDrawRegionHeight
        {
            get
            {
                return _PageDrawRegionHeight;
            }
        }

        private float _PageDrawRegionWidth = 0.0f;
        /// <summary>
        /// 报表绘图区的高度(英寸）
        /// </summary>
        public float PageDrawRegionWidth
        {
            get
            {
                return _PageDrawRegionWidth;
            }
        }

        private int _SelectedPageNum = 1;
        public int SelectedPageNum
        {
            set
            {
                _SelectedPageNum = value;
            }
            get
            {
                return _SelectedPageNum;
            }
        }
        public void ReSetReportSize()
        {
            _PageDrawRegionWidth = _PageWidth - _PageLeftMargin - _PageRightMargin;
            _PageDrawRegionHeight = _PageHeight - _PageTopMargin - _PageBottomMargin;
            _PageDataRegionHeight = _PageDrawRegionHeight - _PageFootHeight - _PageHeadHeight;  
        } 
        #endregion

        #region 页管理

        private List<ReportPage> _ReportPageList = new List<ReportPage>();
        /// <summary>
        /// 报表页集合
        /// </summary>
        public List<ReportPage> Pages
        {
            get { return _ReportPageList; }
        }
        public int PageCount
        {
            get { return _ReportPageList.Count; }
        }

        private int _ReportHeadPageCount;
        public int ReportHeadPageCount
        {
            get { return _ReportHeadPageCount; }
            set { _ReportHeadPageCount = value; }
        }

        private int _ReportDataPageCount;
        public int ReportDataPageCount
        {
            get { return _ReportDataPageCount; }
            set { _ReportDataPageCount = value; }
        }

        private int _ReportFootPageCount;
        public int ReportFootPageCount
        {
            get { return _ReportFootPageCount; }
            set { _ReportFootPageCount = value; }
        }
        public void AddPage(ReportPage p)
        {
            _ReportPageList.Add(p);
            _currentPage = p;
        }
        public void RemovePage(ReportPage p)
        {
            if (p != null)
            {
                p.Release();
            }
            _ReportPageList.Remove(p);
        }
        public void RemoveNoElementPage()
        {
            for (int i = PageCount - 1; i >= 0; i--)
            {
                if (_ReportPageList[i].Elements.Count < 0)
                {
                    RemovePage(_ReportPageList[i]);
                }
                else
                {
                    break;
                }
            }
        } 
        
       
        
        /// <summary>
        /// 添加报表元素
        /// 在元素跨页并不分割的情况下：
        ///    1.如果元素的高度超过页高，则丢弃元素 
        ///    2.如果元素的高度不超过页高，并且 Location 对应的页高度不够的情况下，则增加一页，
        ///      这样会额外增加整个报表的高度，导致后续元素自动后移
        /// </summary>
        /// <param name="element">待添加报表元素</param>
        /// <param name="splitElement">报表元素跨页的情况下是否启用分割</param>
        /// <param name="simulate">模拟添加报表元素标识</param>
        /// <returns>添加元素后，额外增加的高度 /// </returns>
        public float AddReportElement(ref ReportElement element, bool splitElement, ReportControlRegionType controlRegionType, bool simulate = false)
        {
            if (null == element)
            {
                return 0.0f;
            }
            float addedY = 0.0f;
            switch (controlRegionType)
            {
                case ReportControlRegionType.Data:
                case ReportControlRegionType.ReportHead:
                case ReportControlRegionType.ReportFoot:
                    {
                        addedY = ReportHeadOrFootOrDataReportElement(ref element, splitElement, controlRegionType, simulate);
                    }
                    break;
                case ReportControlRegionType.PageHead:
                case ReportControlRegionType.PageFoot:
                default:
                    {
                        addedY = AddPageHeadOrFootReportElement(element, controlRegionType, simulate);
                    }
                    break;
            }
            return addedY;
        }
        /// <summary>
        /// 添加报表元素
        /// 在元素跨页并不分割的情况下：
        ///    1.如果元素的高度超过页高，则丢弃元素 
        ///    2.如果元素的高度不超过页高，并且 Location 对应的页高度不够的情况下，则增加一页，
        ///      这样会额外增加整个报表的高度，导致后续元素自动后移
        /// </summary>
        /// <param name="element">待添加报表元素</param>
        /// <param name="splitElement">报表元素跨页的情况下是否启用分割</param>
        /// <param name="simulate">模拟添加报表元素标识</param>
        /// <returns>添加元素后，额外增加的高度 /// </returns>
        public float ReportHeadOrFootOrDataReportElement(ref ReportElement element, bool splitElement, ReportControlRegionType controlRegionType, bool simulate)
        {
            if (null == element)
            {
                return 0.0f;
            }  
            float addedY = 0.0f; 
            float regionHeight;
            if (controlRegionType == ReportControlRegionType.Data) 
            {
               regionHeight= _PageDataRegionHeight ;
            }
            else
            {
                regionHeight= _PageDrawRegionHeight ;
            } 
            int elemenStartPageNo = (int)(element.Location.Y/regionHeight); 
            if (element.Location.Y % regionHeight >0)
            {
                elemenStartPageNo = elemenStartPageNo + 1;
            }
            if (elemenStartPageNo<=0)
            {
                elemenStartPageNo = 1;
            }
            //容器强制分割
            if (splitElement == true)//控件启用分割
            {
                if (true == simulate)
                {
                    return addedY;
                }

                float elementLocationHeight = element.Location.Y + element.Height;
                int elemenEndPageNo = (int)(elementLocationHeight / regionHeight);
                if ((elementLocationHeight % regionHeight) > 0)
                {
                    elemenEndPageNo = elemenEndPageNo + 1;
                }
                for (int i = PageCount; i < elemenEndPageNo; i++)
                {
                    if (controlRegionType == ReportControlRegionType.Data) 
                    {
                        AddPage(new ReportPage(this, i + 1, ReportPageType.Data));
                    }
                    else if (controlRegionType == ReportControlRegionType.ReportHead)
                    {
                        AddPage(new ReportPage(this, i + 1, ReportPageType.ReportHead));
                    }  
                    else 
                    {
                        AddPage(new ReportPage(this, i + 1, ReportPageType.ReportFoot));
                    }  
                }
                float originalY = element.Location.Y;
                for (int i = elemenStartPageNo; i <= elemenEndPageNo; i++)
                {
                    if (i == elemenStartPageNo)
                    {
                        element.Location = new PointF(element.Location.X, originalY - regionHeight * (i - 1));
                        _ReportPageList[i - 1].AddReportElement(element);
                    }
                    else
                    {
                        ReportElement newElement = new ReportElement(element);
                        newElement.Location = new PointF(newElement.Location.X, originalY - regionHeight * (i - 1));
                        _ReportPageList[i - 1].AddReportElement(newElement);
                        if (i == elemenEndPageNo)
                        {
                            element = newElement;
                        }
                    } 
                } 
            }
            else//不启用分割
            {
                if (element.Height > _PageDrawRegionHeight) //超过1页的高度，容器控件解析时已经进行了强制分割
                {  
                    return addedY;
                } 
                 
                element.Location = new PointF(element.Location.X, element.Location.Y - regionHeight * (elemenStartPageNo - 1));
                if ((element.Location.Y + element.Height) > regionHeight) //超过当前页的高度
                {
                    addedY = regionHeight - element.Location.Y;
                    element.Location = new PointF(element.Location.X, 0.0f);
                    elemenStartPageNo = elemenStartPageNo + 1;
                }

                if (true == simulate)
                {
                    return addedY;
                }
               
                for (int i = PageCount; i < elemenStartPageNo; i++)
                {
                    if (controlRegionType == ReportControlRegionType.Data)
                    {
                        AddPage(new ReportPage(this, i + 1, ReportPageType.Data));
                    }
                    else if (controlRegionType == ReportControlRegionType.ReportHead)
                    {
                        AddPage(new ReportPage(this, i + 1, ReportPageType.ReportHead));
                    }
                    else
                    {
                        AddPage(new ReportPage(this, i + 1, ReportPageType.ReportFoot));
                    }  
                }
                _ReportPageList[elemenStartPageNo - 1].AddReportElement(element);
            }
            return addedY;
        }

        /// <summary>
        /// 不启用分割前提下，模拟判断添加报表元素是否正好跨页
        /// 在元素跨页并不分割的情况下：
        ///    1.如果元素的高度超过页高，则丢弃元素 
        ///    2.如果元素的高度不超过页高，并且 Location 对应的页高度不够的情况下，则增加一页，
        ///      这样会额外增加整个报表的高度，导致后续元素自动后移
        /// </summary>
        /// <param name="element">待添加报表元素</param>
        /// <param name="splitElement">报表元素跨页的情况下是否启用分割</param>
        /// <returns>跨页</returns>
        public bool SimulateElementCrossPage(ReportElement element, ReportControlRegionType controlRegionType)
        {
            if (null == element)
            {
                return false;
            }
            float addedY = 0.0f;
            float regionHeight;
            if (controlRegionType == ReportControlRegionType.Data)
            {
                regionHeight = _PageDataRegionHeight;
            }
            else
            {
                regionHeight = _PageDrawRegionHeight;
            }
            int elemenStartPageNo = (int)(element.Location.Y / regionHeight);
            if (element.Location.Y % regionHeight > 0)
            {
                elemenStartPageNo = elemenStartPageNo + 1;
            }
            if (elemenStartPageNo <= 0)
            {
                elemenStartPageNo = 1;
            }
            //不启用分割
            {
                if (element.Height > _PageDrawRegionHeight) //超过1页的高度，容器控件解析时已经进行了强制分割
                {
                    return true;
                }

                element.Location = new PointF(element.Location.X, element.Location.Y - regionHeight * (elemenStartPageNo - 1));
                if ((element.Location.Y + element.Height) > regionHeight) //超过当前页的高度
                {
                    addedY = regionHeight - element.Location.Y;
                    element.Location = new PointF(element.Location.X, 0.0f);
                    elemenStartPageNo = elemenStartPageNo + 1;
                    return true;
                }
            }
            return false;
        }

        public float AddPageHeadOrFootReportElement(ReportElement element, ReportControlRegionType controlRegionType, bool simulate)
        {
            if (null == element || true == simulate)
            {
                return 0.0f;
            }
            if (controlRegionType == ReportControlRegionType.PageHead)
            {
                _PageHeadReportElementList.Add(element);
            }
            else
            {
                //element.Location = new PointF(element.Location.X, element.Location.Y + (_PageDrawRegionHeight - _PageFootHeight));
                _PageFootReportElementList.Add(element);
            }
            return 0.0f;
        }
        public void ChangePanelReportElementHeight(ReportElement element, ReportControlRegionType controlRegionType)
        {
            if (null == element || (element.Tag is PmsPanel) == false ||
                ReportControlRegionType.PageHead == controlRegionType
                || ReportControlRegionType.PageFoot == controlRegionType)
            {
                return;
            }
            float regionHeight;
            if (controlRegionType == ReportControlRegionType.Data)
            {
                regionHeight = _PageDataRegionHeight;
            }
            else
            {
                regionHeight = _PageDrawRegionHeight;
            }
            //计算控件原始Y轴坐标
            float originalY = element.Location.Y + regionHeight * (element.Page.PageNumber-1); 
            float elementLocationHeight = originalY + element.Height;
            int elemenEndPageNo = (int)(elementLocationHeight / regionHeight);
            if ((elementLocationHeight % regionHeight) > 0)
            {
                elemenEndPageNo = elemenEndPageNo + 1;
            }
            for (int i = PageCount; i < elemenEndPageNo; i++)
            {
                if (controlRegionType == ReportControlRegionType.Data)
                {
                    AddPage(new ReportPage(this, i + 1, ReportPageType.Data));
                }
                else if (controlRegionType == ReportControlRegionType.ReportHead)
                {
                    AddPage(new ReportPage(this, i + 1, ReportPageType.ReportHead));
                }
                else
                {
                    AddPage(new ReportPage(this, i + 1, ReportPageType.ReportFoot));
                }
            }
            if (elemenEndPageNo < element.Page.PageNumber && elemenEndPageNo>0)//高度缩小
            {
                int pageNumber = element.Page.PageNumber;
                element.Page = _ReportPageList[elemenEndPageNo - 1];
                for (int i = pageNumber; i > elemenEndPageNo; i--)
                {
                    _ReportPageList[i - 1].RemoveReportElement(element);
                    if (_ReportPageList[i - 1].Elements.Count==0)
                    {
                       _ReportPageList.Remove(_ReportPageList[i - 1]);
                    }
                    string guidtemp=element.Guid;
                    for (int j = 0; j < _ReportPageList.Count;j++ )
                    {
                        if (_ReportPageList[j].Elements.Count == 1 )
                        {
                            if (_ReportPageList[j].Elements[0].Guid == guidtemp)
                            {
                                //_ReportPageList[j].Elements[0].Height = 0;
                                //_ReportPageList[j].Elements[0].Width = 0;
                               // _ReportPageList.Remove(_ReportPageList[j]);
                            }
                        }
                        else
                        {
                            //_ReportPageList[j].SetGuidPanelElementHeight(guidtemp);
                        }
                    }
                } 
            }
            else//高度增加
            {
                for (int i = element.Page.PageNumber + 1; i <= elemenEndPageNo; i++)
                {
                    ReportElement newElement = new ReportElement(element);
                    newElement.Location = new PointF(newElement.Location.X, originalY - regionHeight * (i - 1));
                    int iIndex = 0;
                    for (int j = 0; j < _ReportPageList[i - 1].Elements.Count; j++)
                    {
                        if (_ReportPageList[i - 1].Elements[j].Location.Y > newElement.Location.Y)
                        {
                            iIndex = j;
                            break;
                        }
                    }
                    _ReportPageList[i - 1].AddReportElement(newElement, iIndex);
                }
            }  
        }
        public float GetNewSplitterReportElementLocationY(ReportControlRegionType controlRegionType)
        {  
            float locationY = 0; 
            if (PageCount > 0)
            {
                if (_ReportPageList[PageCount - 1].Elements.Count > 0)
                {
                    if (controlRegionType == ReportControlRegionType.Data)
                    {
                        locationY = _PageDataRegionHeight * PageCount;
                    }
                    else
                    {
                        locationY = _PageDrawRegionHeight * PageCount;
                    }
                }
            } 
            return SizeConversion.ConvertInchesToPixel(locationY, _ReportConfigDpiY);
        }
        public void Release()
        {
            foreach (ReportPage p in _ReportPageList)
            {
                if (p != null)
                {
                    p.Release();
                }
            }
            _ReportPageList.Clear();
        }
        #endregion 

    }
    /// <summary>
    /// 打印方向
    /// </summary>
    public enum PrintDirection
    {
        Horizontal,
        Vertical 
    } 
}
