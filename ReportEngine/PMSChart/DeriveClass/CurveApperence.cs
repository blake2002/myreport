using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PMS.Libraries.ToolControls.PMSChart
{
    public partial class CurveApperence : Form
    {
        public CurveApperence()
        {
            InitializeComponent();
            InitialText();
        }
        #region 2011.10.31 私有成员变量
        private List<string> selectFieldList = new List<string>();
        private List<string> groupByList = new List<string>();
        private List<SortClass> sortList = new List<SortClass>();
        //数据列表
        private List<string> seriesData = new List<string>();
        //外观已选数据列表
        private List<string> seriesDataForAppearance = new List<string>();
        //区域已选数据列表
        private List<string> seriesDataForChartArea = new List<string>();
        //图例已选数据列表
        private List<string> seriesDataForLegend = new List<string>();

        //标题已选数据列表
        private List<string> titleDataForChartArea = new List<string>();
        //标题已选数据列表
        private List<string> legendDataForChartArea = new List<string>();

        //区域管理
        private List<PMSChartArea> chartAreaList = new List<PMSChartArea>();

        //数据外观管理
        private List<PMSSeries> seriesList = new List<PMSSeries>();

        //图例管理
        private List<PMSLegend> legendList = new List<PMSLegend>();

        //标题管理
        private List<PMSTitle> titleList = new List<PMSTitle>();

        //x坐标管理
        private List<string> _Xaixs;

        //y坐标管理
        private List<string> _yAixs;

        //所有数据源表
        List<string> _pmsTableList = null;

        //已选数据源表
        List<string> _selectedTableList = new List<string>();

        //表达式，包括单列字段
        List<string> _formulaList = new List<string>();

        ////已选表之间的关系，新增时默认为内联
        //private List<TableJoinRelation> _pmsJoinRelation = null;

        ////记录表关系，缓存
        //private Dictionary<TableField, TableJoinRelation> tableRelationCollection = new Dictionary<TableField, TableJoinRelation>();

        ////查询条件，多表，需改造
        //private TreeViewDataAccess.TreeViewData whereData;

        //选定的节点
        private TreeNode _SelectedNode = null;

        private DataSource _sqlSource;

        private PMSChartApp chartApp;
        private bool _IsReport;

        //对多Y轴进行管理
        //前面是Y轴所属区域的名称,后面是当前区域Y轴的个数
        //2011.11.02 调整
        //微软本身的控件对多Y轴的处理的机制是这样的:
        //创建一个Y轴其是利用2个区域来变相完成,一个区域的坐标与
        //与主区域坐标重合,并将绑定序列的父区域设为这个区域,这个做的目的
        //是主区域计算量程时,不用考虑这个绑定序列,另外一个区域用来模拟Y轴
        //其数据序列为绑定序列的备份,并且此序列不绘制
        private Dictionary<string, List<string>> _MultiY = new Dictionary<string, List<string>>();
        //用来记录Y轴与其相应的拷贝区域的关系
        private Dictionary<string, string> _YaxisAndCopyChart = new Dictionary<string, string>();
        ///2011.11.02 增加
        ///目的:在多Y轴的时候字段在同一
        ///图中并非唯一的因此要加如一个属性
        private List<string> _MultiYaxis = new List<string>();

        private Dictionary<string, List<string>> _MultiField = new Dictionary<string, List<string>>();

        private int _Distance = 10;
        #endregion

        #region 2011.10.25 公有成员变量
        public PMSChartCtrl ChartParent;

        public PMSChartApp PMSChartAppearance
        {
            get
            {
                return chartApp;
            }
            set
            {
                chartApp = value;
            }
        }

        [Description("是否嵌入报表")]
        [Category("MES报表属性")]
        [DefaultValue(false)]
        public bool IsReport
        {
            get
            {
                return _IsReport;
            }
            set
            {
                _IsReport = value;
            }
        }

        public List<PMSChartArea> ChartAreaList
        {
            get
            {
                return chartAreaList;
            }
            set
            {
                chartAreaList = value;

            }
        }

        public List<PMSSeries> SeriesList
        {
            get
            {
                return seriesList;
            }
            set
            {
                seriesList = value;

            }
        }


        public List<PMSLegend> LegendList
        {
            get
            {
                return legendList;
            }
            set
            {
                legendList = value;

            }
        }

        public List<PMSTitle> TitleList
        {
            get
            {
                return titleList;
            }
            set
            {
                titleList = value;

            }
        }

        public string XAixs
        {
            get;
            set;
        }
        public List<string> XAixsies
        {
            get
            {
                return _Xaixs;
            }
            set
            {
                _Xaixs = value;
            }
        }
        public List<string> YAixs
        {
            get
            {
                return _yAixs;
            }
            set
            {
                _yAixs = value;
            }
        }

        public List<string> MultiYaxis
        {
            get
            {
                return _MultiYaxis;
            }
            set
            {
                if (value != null)
                {
                    _MultiYaxis = value;
                }
            }
        }

        public DataSource SqlSource
        {
            get
            {
                DealWithConvert();
                _sqlSource = new DataSource(this, true);
                return _sqlSource;
            }
            set
            {
                _sqlSource = value;
                if (_sqlSource != null)
                {
                    _sqlSource.populateFormSql(this, true);
                }
            }
        }

        public List<string> SelectedTableList
        {
            get
            {
                return _selectedTableList;
            }
            set
            {
                _selectedTableList = value;
            }
        }

        public List<string> FormulaList
        {
            get
            {
                return _formulaList;
            }
            set
            {
                _formulaList = value;
            }
        }

        //public List<TableJoinRelation> PmsJoinRelation
        //{
        //    get { return _pmsJoinRelation; }
        //    set
        //    {
        //        _pmsJoinRelation = value;
        //    }
        //}

        //public TreeViewDataAccess.TreeViewData WhereData
        //{
        //    get { return whereData; }
        //    set { whereData = value; }
        //}

        //排序
        public List<SortClass> SortList
        {
            get { return sortList; }
            set { sortList = value; }
        }

        public TreeNode SelectedNode
        {
            get
            {
                return _SelectedNode;
            }
            set
            {
                _SelectedNode = value;
            }
        }
        public Dictionary<string, List<string>> MultiYRelation
        {
            get
            {
                return _MultiY;
            }
            set
            {
                if (value != null)
                {
                    _MultiY = value;
                }
            }

        }
        public Dictionary<string, string> YaxisAndCopyChart
        {
            get
            {
                return _YaxisAndCopyChart;
            }
            set
            {
                _YaxisAndCopyChart = value;
            }
        }
        public Dictionary<string, List<string>> MultiField
        {
            get
            {
                return _MultiField;
            }
            set
            {
                _MultiField = value;
            }
        }
        public int Distance
        {
            get
            {
                return _Distance;
            }
            set
            {
                _Distance = value;
            }
        }
        #endregion

        #region 2011.10.31 内部功能函数
        /// <summary>
        /// 2011.10.31 增加
        /// 目的:在初始化的时候,从资源文件中读取字符串,为多语言做准备
        /// </summary>
        private void InitialText()
        {
            AddArea.Text = Properties.Resources.ResourceManager.GetString("context0003");
            DeleteArea.Text = Properties.Resources.ResourceManager.GetString("context0004");
            AddLegend.Text = Properties.Resources.ResourceManager.GetString("context0006");
            AddTitile.Text = Properties.Resources.ResourceManager.GetString("context0007");
            AddField.Text = Properties.Resources.ResourceManager.GetString("context0017");
            DeleteField.Text = Properties.Resources.ResourceManager.GetString("context0011");
            DeleteLegend.Text = Properties.Resources.ResourceManager.GetString("context0012");
            DeleteTitle.Text = Properties.Resources.ResourceManager.GetString("context0013");
            AddY.Text = Properties.Resources.ResourceManager.GetString("context0019");
            YDelete.Text = Properties.Resources.ResourceManager.GetString("context0020");
        }

        ///// <summary>
        ///// 2011.10.31 增加
        ///// 目的:为指定的曲线序列创建一个Y轴
        ///// Creates Y axis for the specified series.
        ///// </summary>
        ///// <param name="chart">Chart control.</param>
        ///// <param name="area">Original chart area.</param>
        ///// <param name="series">Series.</param>
        ///// <param name="axisOffset">New Y axis offset in relative coordinates.</param>
        ///// <param name="labelsSize">Extra space for new Y axis labels in relative coordinates.</param>
        //private void CreateYAxis(Chart chart, ChartArea area, Series series, float axisOffset, float labelsSize)
        //{
        //    // Create new chart area for original series
        //    ChartArea areaSeries = chart.ChartAreas.Add("ChartArea_" + series.Name);
        //    areaSeries.BackColor = Color.Transparent;
        //    areaSeries.BorderColor = Color.Transparent;
        //    areaSeries.Position.FromRectangleF(area.Position.ToRectangleF());
        //    areaSeries.InnerPlotPosition.FromRectangleF(area.InnerPlotPosition.ToRectangleF());
        //    areaSeries.AxisX.MajorGrid.Enabled = false;
        //    areaSeries.AxisX.MajorTickMark.Enabled = false;
        //    areaSeries.AxisX.LabelStyle.Enabled = false;
        //    areaSeries.AxisY.MajorGrid.Enabled = false;
        //    areaSeries.AxisY.MajorTickMark.Enabled = false;
        //    areaSeries.AxisY.LabelStyle.Enabled = false;
        //    areaSeries.AxisY.IsStartedFromZero = area.AxisY.IsStartedFromZero;

        //    series.ChartArea = areaSeries.Name;

        //    // Create new chart area for axis
        //    ChartArea areaAxis = chart.ChartAreas.Add("AxisY_" + series.ChartArea);
        //    areaAxis.BackColor = Color.Transparent;
        //    areaAxis.BorderColor = Color.Transparent;
        //    areaAxis.Position.FromRectangleF(chart.ChartAreas[series.ChartArea].Position.ToRectangleF());
        //    areaAxis.InnerPlotPosition.FromRectangleF(chart.ChartAreas[series.ChartArea].InnerPlotPosition.ToRectangleF());

        //    // Create a copy of specified series
        //    Series seriesCopy = chart.Series.Add(series.Name + "_Copy");
        //    seriesCopy.ChartType = series.ChartType;
        //    foreach (DataPoint point in series.Points)
        //    {
        //        seriesCopy.Points.AddXY(point.XValue, point.YValues[0]);
        //    }

        //    // Hide copied series
        //    seriesCopy.IsVisibleInLegend = false;
        //    seriesCopy.Color = Color.Transparent;
        //    seriesCopy.BorderColor = Color.Transparent;
        //    seriesCopy.ChartArea = areaAxis.Name;

        //    // Disable grid lines & tickmarks
        //    areaAxis.AxisX.LineWidth = 0;
        //    areaAxis.AxisX.MajorGrid.Enabled = false;
        //    areaAxis.AxisX.MajorTickMark.Enabled = false;
        //    areaAxis.AxisX.LabelStyle.Enabled = false;
        //    areaAxis.AxisY.MajorGrid.Enabled = false;
        //    areaAxis.AxisY.IsStartedFromZero = area.AxisY.IsStartedFromZero;

        //    // Adjust area position
        //    areaAxis.Position.X -= axisOffset;
        //    areaAxis.InnerPlotPosition.X += labelsSize;
        //}

        /// <summary>
        /// 2011.10.31 增加
        /// 目的:在新增一个区域的时候有些子项自动添加在里面
        /// 曲线区域中的内容包括:
        ///                     x轴
        ///                     y轴管理节点(默认有一个y轴切最后一个y轴不允许删除)
        ///                     曲线管理节点
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="imageindex"></param>
        /// <param name="selectedimageindex"></param>
        /// <returns></returns>
        private TreeNode CreatChartArea(int imageindex, int selectedimageindex)
        {
            TreeNode result = null;
            PMSChartArea areatemp = new PMSChartArea(null);
            areatemp.Name = GetNameFromList(chartAreaList, "chartArea");
            areatemp.Area3DStyle.Enable3D = false;
            result = new TreeNode(areatemp.Name, 1, 1);
            result.Name = areatemp.Name;
            result.Tag = areatemp;
            result.Text = Properties.Resources.ResourceManager.GetString("context0023");
            if (!chartAreaList.Contains(areatemp))
            {
                chartAreaList.Add(areatemp);
            }
            TreeNode curvex = new TreeNode(Properties.Resources.ResourceManager.GetString("context0014"), 8, 8);
            curvex.Name = Properties.Resources.ResourceManager.GetString("context0014");
            CurveX x = new CurveX(null);
            x.SourceField = this.ChartParent.SourceField;
            curvex.Tag = x;
            result.Nodes.Add(curvex);
            TreeNode multiy = new TreeNode(Properties.Resources.ResourceManager.GetString("context0015"), 8, 8);
            multiy.Name = Properties.Resources.ResourceManager.GetString("context0015");
            CurveY y = new CurveY(null);
            y.Name = GetNameFromList(chartAreaList, "Y");
            y.IsCopyChart = false;
            TreeNode curvey = new TreeNode(y.Name, 8, 8);
            curvey.Name = y.Name;
            curvey.Tag = y;
            AddYaxisRelation(areatemp, y);
            multiy.Nodes.Add(curvey);
            result.Nodes.Add(multiy);
            TreeNode field = new TreeNode(Properties.Resources.ResourceManager.GetString("context0016"), 7, 7);
            field.Name = Properties.Resources.ResourceManager.GetString("context0016");
            result.Nodes.Add(field);
            result.ExpandAll();
            return result;
        }
        /// <summary>
        /// 2011.11.01 增加
        /// 目的:为树型结构增加一个Y轴节点
        /// </summary>
        /// <returns></returns>
        /// 2011.11.18 修改
        /// 目的：在应用后再增加轴的时候忽略了对默认第一个Y轴区域的考虑
        private TreeNode CreateYaxis(TreeNode Aim)
        {
            TreeNode result = null;
            PMSChartArea fatherarea = SearchFatherArea(Aim);
            if (fatherarea != null)
            {
                //看区域中有几个Y轴
                //第一个为Y轴 无偏离 不计算
                int count = 0;
                if (_MultiY.ContainsKey(fatherarea.Name))
                {
                    if (_MultiY[fatherarea.Name] != null)
                    {
                        count = _MultiY[fatherarea.Name].Count-1;
                    }
                }
                if (count == 0)
                {
                    fatherarea.Position = new PMSElementPosition(new ElementPosition(_Distance + count * _Distance, 10, 68, 85));
                    fatherarea.InnerPlotPosition = new PMSElementPosition(new ElementPosition(10, 0, 90, 90));
                }
                else
                {
                    fatherarea.Position = new PMSElementPosition(new ElementPosition(_Distance + count * _Distance, 10, fatherarea.Position.Width, fatherarea.Position.Height));
                    fatherarea.InnerPlotPosition = new PMSElementPosition(new ElementPosition(10, 0, 90, 90));
                }
                if (fatherarea.Position.X + fatherarea.Position.Width > 100)
                {
                    float widthtemp = fatherarea.Position.Width - (fatherarea.Position.X + fatherarea.Position.Width + 5 - 100);
                    if (widthtemp >= 0)
                    {
                        fatherarea.Position.Width -= fatherarea.Position.X + fatherarea.Position.Width + 5 - 100;
                    }
                    else
                    {
                        throw new NotImplementedException(Properties.Resources.ResourceManager.GetString("message0008"));
                    }
                }
                CurveY ytemp = new CurveY(null);
                ytemp.Area3DStyle.Enable3D = false;
                ytemp.Name = GetNameFromList(chartAreaList, "copychart");
                if (!chartAreaList.Contains(ytemp as PMSChartArea))
                {
                    chartAreaList.Add(ytemp as PMSChartArea);
                }
                ElementPosition temp = ytemp.Position.ToElementPosition();
                temp.FromRectangleF(fatherarea.Position.ToElementPosition().ToRectangleF());
                ElementPosition bb = ytemp.InnerPlotPosition.ToElementPosition();
                bb.FromRectangleF(fatherarea.InnerPlotPosition.ToElementPosition().ToRectangleF());
                ytemp.Position = new PMSElementPosition(temp);
                ytemp.InnerPlotPosition = new PMSElementPosition(bb);
                ytemp.AxisY.IsStartedFromZero = fatherarea.AxisY.IsStartedFromZero;
                ytemp.IsCopyChart = true;
                ytemp.AxisY.LabelStyle.Enabled = false;
                ytemp.AxisY.MajorTickMark.Enabled = false;
                ytemp.AxisY.LineColor = Color.Transparent;
                CurveY y2 = new CurveY(null);
                y2.Area3DStyle.Enable3D = false;
                y2.AxisX.LineWidth = 0;
                //y2.Name = GetNameFromList(chartAreaList, "Y");
                if (_MultiY[fatherarea.Name] != null && _MultiY[fatherarea.Name].Count > 0)
                {
                    y2.Name = GetNameFromList(_MultiY[fatherarea.Name], "Y");
                }
                else
                {
                    y2.Name = GetNameFromList(chartAreaList, "Y");
                }
                temp = y2.Position.ToElementPosition();
                temp.FromRectangleF(ytemp.Position.ToElementPosition().ToRectangleF());
                bb = y2.Position.ToElementPosition();
                bb.FromRectangleF(ytemp.InnerPlotPosition.ToElementPosition().ToRectangleF());
                y2.Position = new PMSElementPosition(temp);
                y2.InnerPlotPosition = new PMSElementPosition(bb);
                y2.AxisY.IsStartedFromZero = fatherarea.AxisY.IsStartedFromZero;
                y2.IsCopyChart = false;
                y2.Position.X = y2.Position.X - (_Distance + _Distance * count);
                result = new TreeNode(y2.Name, 8, 8);
                result.Name = y2.Name;
                result.Tag = y2;
                DealWithCopyAreaPosition(fatherarea);
                DealWithOtherYaxisPosition(fatherarea, _Distance, y2.Name);
                AddYaxisRelation(fatherarea, y2);
                AddYaxisAndCopyChartRelation(ytemp, y2);

            }
            return result;
        }
        /// <summary>
        /// 2011.11.01 增加
        /// 目的;寻找最近的管理父节点
        /// </summary>
        /// <param name="Aim"></param>
        /// <returns></returns>
        private PMSChartArea SearchFatherArea(TreeNode Aim)
        {
            PMSChartArea result = null;
            if(Aim.Tag!=null && Aim.Tag is PMSChartArea && !(Aim.Tag is CurveY))
            {
                result = Aim.Tag as PMSChartArea;
            }
            else if (Aim.Parent != null)
            {
                result = SearchFatherArea(Aim.Parent);
            }
            return result;
        }
        /// <summary>
        /// 2011.11.01 增加
        /// 目的:在树型结构中为一个区域增加一个图例
        /// </summary>
        /// <returns></returns>
        private TreeNode CreatChartLegend()
        {
            TreeNode result = null;
            MESLegend pca = new MESLegend(null);
            //pca.Name = GetNameFromList(legendList, "legend");
            pca.Name = GetNameFromList(legendList, Properties.Resources.ResourceManager.GetString("context0024"));
            pca.SourceField = this.ChartParent.SourceField;
            pca.seriesDataForLegend = this.seriesDataForLegend;
            result = new TreeNode(pca.Name, 2, 2);
            result.Name = pca.Name;
            result.Tag = pca;
            if (!legendList.Contains(pca as PMSLegend))
            {
                legendList.Add(pca as PMSLegend);
            }
            return result;
        }
        /// <summary>
        /// 2011.10.27 增加
        /// 目的:在树型结构中为一个区域增加标题
        /// </summary>
        /// <returns></returns>
        private TreeNode CreatChartTiTle()
        {
            TreeNode result = null;
            PMSTitle pca = new PMSTitle(null);
            //pca.Name = GetNameFromList(titleList, "title");
            pca.Name = GetNameFromList(titleList, Properties.Resources.ResourceManager.GetString("context0025"));
            result = new TreeNode(pca.Name, 0, 0);
            result.Name = pca.Name;
            result.Tag = pca;
            if (!titleList.Contains(pca))
            {
                titleList.Add(pca);
            }
            return result;
        }
        /// <summary>
        /// 2011.11.01 增加
        /// 目的:在树型结构中增加一个序列
        /// </summary>
        /// <returns></returns>
        private TreeNode CreatChartSeries()
        {
            TreeNode result = null;
            CurveSeries seriestemp = new CurveSeries(null);
            seriestemp.ChartType = SeriesChartType.Line;
            seriestemp.SourceField = this.ChartParent.SourceField;
            seriestemp.seriesDataForAppearance = this.seriesDataForAppearance;
            //seriestemp.Name = GetNameFromList(seriesList, "series");
            seriestemp.Name = GetNameFromList(seriesList, Properties.Resources.ResourceManager.GetString("context0016"));
            result = new TreeNode(seriestemp.Name, 3, 3);
            result.Name = seriestemp.Name;
            result.Tag = seriestemp;
            seriestemp.Tag = result;
            if (!seriesList.Contains(seriestemp as PMSSeries))
            {
                seriesList.Add(seriestemp as PMSSeries);
            }
            return result;
        }
        /// <summary>
        /// 2011.10.26 增加
        /// 目的:从已有的类型列表中找到系统自动增加的最大的索引号
        /// </summary>
        /// <param name="data"></param>
        /// <param name="aim"></param>
        /// <returns></returns>
        private string GetNameFromList(object data, string aim)
        {
            string result = "";
            int max = 0;
            if (data is List<PMSChartArea>)
            {
                List<PMSChartArea> areatemp = data as List<PMSChartArea>;
                if (areatemp != null)
                {
                    for (int i = 0; i < areatemp.Count; i++)
                    {
                        PMSChartArea strtemp = areatemp[i];
                        if (strtemp.Name.IndexOf(aim) >= 0)
                        {
                            string str = strtemp.Name.Replace(aim, "");
                            if (IsAllNum(str))
                            {
                                int inttemp = System.Convert.ToInt32(str);
                                if (inttemp > max)
                                {
                                    max = inttemp;
                                }
                            }
                        }
                    }
                }
                result = aim + (max + 1).ToString();
            }
            else if (data is List<PMSSeries>)
            {
                List<PMSSeries> areatemp = data as List<PMSSeries>;
                if (areatemp != null)
                {
                    for (int i = 0; i < areatemp.Count; i++)
                    {
                        PMSSeries strtemp = areatemp[i];
                        if (strtemp.Name.IndexOf(aim) >= 0)
                        {
                            string str = strtemp.Name.Replace(aim, "");
                            if (IsAllNum(str))
                            {
                                int inttemp = System.Convert.ToInt32(str);
                                if (inttemp > max)
                                {
                                    max = inttemp;
                                }
                            }
                        }
                    }
                }
                result = aim + (max + 1).ToString();
            }
            else if (data is List<PMSLegend>)
            {
                List<PMSLegend> areatemp = data as List<PMSLegend>;
                if (areatemp != null)
                {
                    for (int i = 0; i < areatemp.Count; i++)
                    {
                        PMSLegend strtemp = areatemp[i];
                        if (strtemp.Name.IndexOf(aim) >= 0)
                        {
                            string str = strtemp.Name.Replace(aim, "");
                            if (IsAllNum(str))
                            {
                                int inttemp = System.Convert.ToInt32(str);
                                if (inttemp > max)
                                {
                                    max = inttemp;
                                }
                            }
                        }
                    }
                }
                result = aim + (max + 1).ToString();
            }
            else if (data is List<PMSTitle>)
            {
                List<PMSTitle> areatemp = data as List<PMSTitle>;
                if (areatemp != null)
                {
                    for (int i = 0; i < areatemp.Count; i++)
                    {
                        PMSTitle strtemp = areatemp[i];
                        if (strtemp.Name.IndexOf(aim) >= 0)
                        {
                            string str = strtemp.Name.Replace(aim, "");
                            if (IsAllNum(str))
                            {
                                int inttemp = System.Convert.ToInt32(str);
                                if (inttemp > max)
                                {
                                    max = inttemp;
                                }
                            }
                        }
                    }
                }
                result = aim + (max + 1).ToString();
            }
            else if (data is List<string>)
            {
                List<string> areatemp = data as List<string>;
                if (areatemp != null)
                {
                    for (int i = 0; i < areatemp.Count; i++)
                    {
                        string strtemp = areatemp[i];
                        if (strtemp.IndexOf(aim) >= 0)
                        {
                            string str = strtemp.Replace(aim, "");
                            if (IsAllNum(str))
                            {
                                int inttemp = System.Convert.ToInt32(str);
                                if (inttemp > max)
                                {
                                    max = inttemp;
                                }
                            }
                        }
                    }
                }
                result = aim + (max + 1).ToString();
            }
            return result;
        }
        /// <summary>
        /// 2011.11.01 增加
        /// 在增加的时候要校验名字 并且要在区域中增加一条记录 
        /// 同时在区域的Y轴管理中增加一条关系记录
        /// </summary>
        /// <param name="fathernode"></param>
        /// <param name="yaxis"></param>
        private void AddYaxisRelation(PMSChartArea fathernode, CurveY yaxis)
        {
            if (!chartAreaList.Contains(yaxis as PMSChartArea))
            {
                chartAreaList.Add(yaxis as PMSChartArea);
            }
            if (_MultiY.ContainsKey(fathernode.Name))
            {
                if (_MultiY[fathernode.Name] != null)
                {
                    if (!_MultiY[fathernode.Name].Contains(yaxis.Name))
                    {
                        _MultiY[fathernode.Name].Add(yaxis.Name);
                    }
                }
                else
                {
                    _MultiY[fathernode.Name] = new List<string>();
                    _MultiY[fathernode.Name].Add(yaxis.Name);
                }
            }
            else
            {
                List<string> strtemp = new List<string>();
                strtemp.Add(yaxis.Name);
                _MultiY.Add(fathernode.Name, strtemp);
            }
        }
        /// <summary>
        /// 2011.11.01 增加
        /// 在删除Y轴的时候要将区域中的对象删除,
        /// 并且要在Y轴管理中删除一条关系记录
        /// </summary>
        /// <param name="fathernode"></param>
        /// <param name="yaxis"></param>
        private void DeleteYaxisRelation(PMSChartArea fathernode, CurveY yaxis)
        {
            chartAreaList.Remove(yaxis as PMSChartArea);
            if (_MultiY.ContainsKey(fathernode.Name))
            {
                if (_MultiY[fathernode.Name] != null)
                {
                    _MultiY[fathernode.Name].Remove(yaxis.Name);
                }
            }
        }
        /// <summary>
        /// 2011.11.02 增加
        /// 目的:记录Y轴与其拷贝区域的关系
        /// </summary>
        /// <param name="copychart"></param>
        /// <param name="yaxis"></param>
        private void AddYaxisAndCopyChartRelation(CurveY copychart, CurveY yaxis)
        {
            if (copychart != null && yaxis != null)
            {
                if (!_YaxisAndCopyChart.ContainsKey(yaxis.Name) && !_YaxisAndCopyChart.ContainsValue(copychart.Name))
                {
                    _YaxisAndCopyChart.Add(yaxis.Name, copychart.Name);
                }
            }
        }
        /// <summary>
        /// 2011.11.02 增加
        /// 目的:删除Y轴与其拷贝区域的关系
        /// </summary>
        /// <param name="copychart"></param>
        /// <param name="yaxis"></param>
        private void DeleteYaxisAndCopyChartRelation( CurveY yaxis)
        {
            if (yaxis != null)
            {
                _YaxisAndCopyChart.Remove(yaxis.Name);
            }
        }
        /// <summary>
        /// 2011.10.26 增加
        /// 目的:判断字符串中是否全是数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private bool IsAllNum(string str)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(str))
            {
                char[] strCopy = str.ToCharArray();
                for (int i = 0; i < strCopy.Length; i++)
                {
                    if (strCopy[i] > 47 && strCopy[i] < 58)
                    {
                        continue;
                    }
                    else
                    {
                        return result;
                    }
                }
            }
            else
            {
                return result;
            }
            result = true;
            return result;
        }
        /// <summary>
        /// 2011.11.01 增加
        /// 在增加的时候要校验名字 并且要在区域中增加一条记录 
        /// 同时在区域的Y轴管理中增加一条关系记录
        /// </summary>
        /// <param name="Aim"></param>
        private void AddYaxis(TreeNode Aim)
        {
            if (Aim.Tag == null && Aim.Text == Properties.Resources.ResourceManager.GetString("context0015"))
            {
                TreeNode nodetemp = CreateYaxis(Aim);
                Aim.Nodes.Add(nodetemp);
                Aim.ExpandAll();
            }
        }
        /// <summary>
        /// 2011.11.01 增加
        /// 目的;在删除Y轴时要先校验是否能删除这个Y轴
        /// (要删除Y轴 要保证此至少有2个Y轴
        /// </summary>
        /// <param name="Aim"></param>
        /// 2011.11.02 修改
        /// 目的:在删除Y轴时 要先删除此Y轴对应的实际序列对应的拷贝区域
        /// 并且在删除后将第一个Y轴特别处理(第一个Y轴没有拷贝区域)
        /// 2011.12.26 修改
        /// 目的:在删除Y轴时 只删除了拷贝区域 但对轴本身却没有删除
        private void DeleteYaxis(TreeNode Aim)
        {
            if (Aim.Tag != null && Aim.Tag is CurveY)
            {
                PMSChartArea temp = SearchFatherArea(Aim);
                if (temp != null)
                {
                    if (_MultiY.ContainsKey(temp.Name) && _MultiY[temp.Name] != null && _MultiY[temp.Name].Count > 1)
                    {
                        if (_YaxisAndCopyChart.ContainsKey((Aim.Tag as CurveY).Name))
                        {
                            CurveY copytemp = GetCurveYByName(_YaxisAndCopyChart[(Aim.Tag as CurveY).Name]);
                            if (copytemp != null)
                            {
                                chartAreaList.Remove(copytemp as PMSChartArea);
                            }
                            _YaxisAndCopyChart.Remove((Aim.Tag as CurveY).Name);
                        }
                        temp.Position.X -= _Distance;
                        if (temp.Position.X + temp.Position.Width < 100-5)
                        {
                            temp.Position.Width += 95 - (temp.Position.X + temp.Position.Width);
                        }
                        DealWithCopyAreaPosition(temp);
                        DealWithOtherYaxisPosition(temp, -_Distance, (Aim.Tag as CurveY).Name);
                        DeleteYaxisRelation(temp, Aim.Tag as CurveY);
                        TreeNode parent = Aim.Parent;
                        parent.Nodes.Remove(Aim);
                        if (parent.Nodes!=null && parent.Nodes.Count > 0)
                        {
                            TreeNode y1 = parent.Nodes[0];
                            if (y1.Tag != null && y1.Tag is CurveY)
                            {
                                if (_YaxisAndCopyChart.ContainsKey((y1.Tag as CurveY).Name))
                                {
                                    CurveY copytemp2 = GetCurveYByName(_YaxisAndCopyChart[(y1.Tag as CurveY).Name]);
                                    if (copytemp2 != null)
                                    {
                                        chartAreaList.Remove(copytemp2 as PMSChartArea);
                                    }
                                    _YaxisAndCopyChart.Remove((y1.Tag as CurveY).Name);
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(Properties.Resources.ResourceManager.GetString("message0003"));
                    }
                }
               
            }
        }
        /// <summary>
        /// 2011.11.01 增加
        /// 目的:增加一个图例
        /// </summary>
        /// <param name="Aim"></param>
        private void AddChartLegend(TreeNode Aim)
        {
            if (Aim.Tag != null && Aim.Tag is PMSChartArea)
            {
                TreeNode nodetemp = CreatChartLegend();
                Aim.Nodes.Add(nodetemp);
                Aim.ExpandAll();
            }
        }
        /// <summary>
        /// 2011.11.01 增加
        /// 目的:删除一个图例
        /// </summary>
        /// <param name="Aim"></param>
        private void DeleteChartLegend(TreeNode Aim)
        {
            if (Aim.Parent != null)
            {
                if (Aim.Parent.Nodes.Count > 0)
                {
                    if (Aim.Tag != null && Aim.Tag is MESLegend)
                    {
                        seriesDataForLegend.Remove((Aim.Tag as MESLegend).BindingField);
                    }
                    if (Aim.Tag != null && Aim.Tag is PMSLegend)
                    {
                        legendList.Remove(Aim.Tag as PMSLegend);
                    }
                    Aim.Parent.Nodes.Remove(Aim);
                }
            }
            else
            {
            }
        }
        /// <summary>
        /// 2011.11.01 增加
        /// 目的:增加一个标题
        /// </summary>
        /// <param name="Aim"></param>
        private void AddChartTitile(TreeNode Aim)
        {
            if (Aim.Tag != null && Aim.Tag is PMSChartArea)
            {
                TreeNode nodetemp = CreatChartTiTle();
                Aim.Nodes.Add(nodetemp);
                Aim.ExpandAll();
            }
        }
        /// <summary>
        /// 2011.11.01 增加
        /// 目的:删除一个标题
        /// </summary>
        /// <param name="Aim"></param>
        private void DeleteChartTitle(TreeNode Aim)
        {
            if (Aim.Parent != null)
            {
                if (Aim.Parent.Nodes.Count > 0)
                {
                    if (Aim.Tag != null && Aim.Tag is PMSTitle)
                    {
                        titleList.Remove(Aim.Tag as PMSTitle);
                    }
                    Aim.Parent.Nodes.Remove(Aim);
                }
            }
            else
            {
            }
        }
        /// <summary>
        /// 2011.11.01 增加
        /// 目的:增加一个绑定字段
        /// </summary>
        /// <param name="Aim"></param>
        private void AddChartField(TreeNode Aim)
        {
            string rootname = Properties.Resources.ResourceManager.GetString("context0016");
            if (Aim.Tag == null && Aim.Name == rootname)
            {
                TreeNode nodetemp = CreatChartSeries();
                Aim.Nodes.Add(nodetemp);
                Aim.ExpandAll();
            }
        }
        /// <summary>
        /// 2011.11.01 增加
        /// 目的:删除一个绑定字段
        /// </summary>
        /// <param name="Aim"></param>
        private void DeleteChartField(TreeNode Aim)
        {
            if (Aim.Parent != null)
            {
                if (Aim.Parent.Nodes.Count > 0)
                {
                    if (Aim.Tag != null && Aim.Tag is CurveSeries)
                    {
                        seriesDataForAppearance.Remove((Aim.Tag as CurveSeries).BindingField);
                    }
                    if (Aim.Tag != null && Aim.Tag is PMSSeries)
                    {
                        seriesList.Remove(Aim.Tag as PMSSeries);
                    }
                    Aim.Parent.Nodes.Remove(Aim);
                }
            }
            else
            {
            }
        }
        /// <summary>
        /// 2011.11.01 增加
        /// 目的:由于目前处理是以小马哥以前的处理方式为基础,因此自己扩展出来的东西要以小
        /// 马哥的格式存储,这样的好处是能快速处理
        /// </summary>
        private void DealWithConvert()
        {
            if (treeView1.Nodes != null)
            {
                if (_MultiY != null)
                {
                    _MultiY.Clear();
                }
                else
                {
                    _MultiY = new Dictionary<string, List<string>>();
                }
                if (_MultiField != null)
                {
                    _MultiField.Clear();
                }
                else
                {
                    _MultiField = new Dictionary<string, List<string>>();
                }
                if (_MultiYaxis != null)
                {
                    _MultiYaxis.Clear();
                }
                else
                {
                    _MultiYaxis = new List<string>();
                }
                for (int n = 0; n < treeView1.Nodes.Count; n++)
                {
                    TreeNode temp1 = treeView1.Nodes[n];
                    if (temp1.Name == Properties.Resources.ResourceManager.GetString("context0002") && temp1.Nodes != null)
                    {
                        for (int m = 0; m < temp1.Nodes.Count; m++)
                        {
                            TreeNode temp2 = temp1.Nodes[m];
                            if (temp2.Tag != null && temp2.Tag is PMSChartArea)
                            {
                                DealWithChartArea(temp2);
                            }
                            else if (temp2.Tag != null && temp2.Tag is XGroup)
                            {
                                XAixs = (temp2.Tag as XGroup).xRecordField;
                            }
                        }
                    }
                }
            }
            _yAixs.Clear();
            if (seriesList != null)
            {
                for (int i = 0; i < seriesList.Count; i++)
                {
                    PMSSeries temp = seriesList[i];
                    if (temp is CurveSeries)
                    {
                        CurveSeries mestemp = temp as CurveSeries;
                        string str = mestemp.BindingField;
                        if (temp.SeriesDataList == null)
                        {
                            temp.SeriesDataList = new List<string>();
                        }
                        if (temp.SeriesDataList is List<string>)
                        {
                            (temp.SeriesDataList as List<string>).Clear();
                            (temp.SeriesDataList as List<string>).Add(str);
                        }
                        if (!_yAixs.Contains(str))
                        {
                            _yAixs.Add(str);
                        }
                    }
                }
            }
            if (legendList != null)
            {
                for (int i = 0; i < legendList.Count; i++)
                {
                    PMSLegend temp = legendList[i];
                    if (temp is MESLegend)
                    {
                        MESLegend mestemp = temp as MESLegend;
                        string str = mestemp.BindingField;
                        if (temp.SeriesDataList == null)
                        {
                            temp.SeriesDataList = new List<string>();
                        }
                        if (temp.SeriesDataList is List<string>)
                        {
                            (temp.SeriesDataList as List<string>).Clear();
                            (temp.SeriesDataList as List<string>).Add(str);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 2011.11.01 增加
        /// 目的:对数据区域进行处理
        /// </summary>
        /// <param name="Aim"></param>
        private void DealWithChartArea(TreeNode Aim)
        {
            if (Aim.Tag != null && Aim.Tag is PMSChartArea && !(Aim.Tag is CurveY))
            {
                PMSChartArea temp = Aim.Tag as PMSChartArea;
                List<string> yaxistemp = new List<string>();
                List<string> fields = new List<string>();
                if (temp.LegendDataList == null)
                {
                    temp.LegendDataList = new List<string>();
                }
                if (temp.LegendDataList is List<string>)
                {
                    (temp.LegendDataList as List<string>).Clear();
                }
                if (temp.TitleDataList == null)
                {
                    temp.TitleDataList = new List<string>();
                }
                if (temp.TitleDataList is List<string>)
                {
                    (temp.TitleDataList as List<string>).Clear();
                }
                if (temp.SeriesDataList == null)
                {
                    temp.SeriesDataList = new List<string>();
                }
                else if (temp.SeriesDataList is List<string>)
                {
                    (temp.SeriesDataList as List<string>).Clear();
                }
                if (Aim.Nodes != null)
                {
                    for (int i = 0; i < Aim.Nodes.Count; i++)
                    {
                        TreeNode nodetemp = Aim.Nodes[i];
                        if (nodetemp.Tag != null && nodetemp.Tag is CurveX)
                        {
                            CurveX x = nodetemp.Tag as CurveX;
                            temp.AxisX = x as PMSAxis;
                            XAixs = x.xRecordField;
                        }
                        else if (nodetemp.Tag == null && nodetemp.Name == Properties.Resources.ResourceManager.GetString("context0015"))
                        {
                            if (nodetemp.Nodes != null && nodetemp.Nodes.Count > 0)
                            {
                                for (int m = 0; m < nodetemp.Nodes.Count; m++)
                                {
                                    if (m == 0)
                                    {
                                        TreeNode y1 = nodetemp.Nodes[m];
                                        if (y1.Tag != null && y1.Tag is CurveY)
                                        {
                                            temp.AxisY = (y1.Tag as CurveY).AxisY;
                                            chartAreaList.Remove(y1.Tag as PMSChartArea);
                                            if (!yaxistemp.Contains((y1.Tag as CurveY).Name))
                                            {
                                                yaxistemp.Add((y1.Tag as CurveY).Name);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        TreeNode y1 = nodetemp.Nodes[m];
                                        if (y1.Tag != null && y1.Tag is CurveY)
                                        {
                                            if (!yaxistemp.Contains((y1.Tag as CurveY).Name))
                                            {
                                                yaxistemp.Add((y1.Tag as CurveY).Name);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (nodetemp.Tag == null && nodetemp.Name == Properties.Resources.ResourceManager.GetString("context0016"))
                        {
                            if (nodetemp.Nodes != null)
                            {
                                for (int j = 0; j < nodetemp.Nodes.Count; j++)
                                {
                                    TreeNode fieldtemp = nodetemp.Nodes[j];
                                    if (fieldtemp.Tag is CurveSeries)
                                    {
                                        CurveSeries mestemp = fieldtemp.Tag as CurveSeries;
                                        if (!fields.Contains(mestemp.Name))
                                        {
                                            fields.Add(mestemp.Name);
                                        }
                                        if (!string.IsNullOrEmpty(mestemp.BindingField) && !string.IsNullOrEmpty(mestemp.BindingYaxis))
                                        {
                                            if (IsTheFirstYaxis(Aim, mestemp.BindingYaxis))
                                            {
                                                if (temp.SeriesDataList == null)
                                                {
                                                    temp.SeriesDataList = new List<string>();
                                                }
                                                if (temp.SeriesDataList is List<string> && !(temp.SeriesDataList as List<string>).Contains(mestemp.BindingField))
                                                {
                                                    (temp.SeriesDataList as List<string>).Add(mestemp.BindingField);
                                                }
                                            }
                                            else
                                            {
                                                if (_YaxisAndCopyChart.ContainsKey(mestemp.BindingYaxis))
                                                {
                                                    string copychart = _YaxisAndCopyChart[mestemp.BindingYaxis];
                                                    CurveY copyarea = GetCurveYByName(copychart);
                                                    if (copyarea != null)
                                                    {
                                                        if (copyarea.SeriesDataList == null)
                                                        {
                                                            copyarea.SeriesDataList = new List<string>();
                                                        }
                                                        if (copyarea.SeriesDataList is List<string> && !(copyarea.SeriesDataList as List<string>).Contains(mestemp.BindingField))
                                                        {
                                                            (copyarea.SeriesDataList as List<string>).Add(mestemp.BindingField);
                                                        }
                                                        CurveY ytemp = GetCurveYByName(mestemp.BindingYaxis);
                                                        if (ytemp != null)
                                                        {
                                                            if (ytemp.SeriesDataList == null)
                                                            {
                                                                ytemp.SeriesDataList = new List<string>();
                                                            }
                                                            if (ytemp.SeriesDataList is List<string> && !(ytemp.SeriesDataList as List<string>).Contains(mestemp.BindingField))
                                                            {
                                                                (ytemp.SeriesDataList as List<string>).Add(mestemp.BindingField);
                                                            }
                                                        }
                                                        if (!_MultiYaxis.Contains(mestemp.BindingField))
                                                        {
                                                            _MultiYaxis.Add(mestemp.BindingField);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show(Properties.Resources.ResourceManager.GetString("message0005"));
                                        }
                                    }
                                }
                            }
                        }
                        else if (nodetemp.Tag != null && nodetemp.Tag is MESLegend)
                        {
                            MESLegend legtemp = nodetemp.Tag as MESLegend;
                            if (temp.LegendDataList is List<string>)
                            {
                                if (!(temp.LegendDataList as List<string>).Contains(legtemp.Name))
                                {
                                    (temp.LegendDataList as List<string>).Add(legtemp.Name);
                                }
                            }
                        }
                        else if (nodetemp.Tag != null && nodetemp.Tag is PMSTitle)
                        {
                            PMSTitle titletemp = nodetemp.Tag as PMSTitle;
                            if (temp.TitleDataList is List<string>)
                            {
                                if (!(temp.TitleDataList as List<string>).Contains(titletemp.Name))
                                {
                                    (temp.TitleDataList as List<string>).Add(titletemp.Name);
                                }
                            }
                        }
                    }
                }
                if(!_MultiY.ContainsKey(temp.Name) && !_MultiY.ContainsValue(yaxistemp))
                {
                    _MultiY.Add(temp.Name,yaxistemp);
                }
                if (!_MultiField.ContainsKey(temp.Name) && !_MultiField.ContainsValue(fields))
                {
                    _MultiField.Add(temp.Name, fields);
                }
            }
        }
        /// <summary>
        /// 2011.11.02 增加
        /// 目的:判定此轴是否是当前区域中的第一根轴
        /// </summary>
        /// <param name="Aim"></param>
        /// <param name="YaxisName"></param>
        /// <returns></returns>
        private bool IsTheFirstYaxis(TreeNode Aim, string YaxisName)
        {
            bool result = false;
            if (Aim.Tag != null && Aim.Tag is PMSChartArea && !(Aim.Tag is CurveY))
            {
                if (Aim.Nodes[Properties.Resources.ResourceManager.GetString("context0015")] != null)
                {
                    TreeNode temp = Aim.Nodes[Properties.Resources.ResourceManager.GetString("context0015")];
                    if (temp.Nodes != null && temp.Nodes.Count > 0)
                    {
                        if (temp.Nodes[0].Name == YaxisName)
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 2011.11.01 增加
        /// 目的:根据名字获取Y轴区域
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private CurveY GetCurveYByName(string str)
        {
            CurveY result = null;
            if (chartAreaList != null)
            {
                for (int i = 0; i < chartAreaList.Count; i++)
                {
                    PMSChartArea temp = chartAreaList[i];
                    if (temp is CurveY)
                    {
                        if ((temp as CurveY).Name == str)
                        {
                            return temp as CurveY;
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 2011.11.03 增加
        /// 目的:根据名字获取数据序列
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private CurveSeries GetCurveSeriesByName(string str)
        {
            CurveSeries result = null;
            if (seriesList != null)
            {
                for (int i = 0; i < seriesList.Count; i++)
                {
                    PMSSeries temp = seriesList[i];
                    if (temp is CurveSeries)
                    {
                        if ((temp as CurveSeries).Name == str)
                        {
                            return temp as CurveSeries;
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 2011.11.02 增加
        /// 根据序列化信息生成树型结构
        /// (根据区域节点信息要派生生成x轴节点以及第一个Y轴节点 
        /// 其余Y轴根据MultYaxis的字段信息 找到相应的CurveY并且不是拷贝的Y轴
        /// (因为这个顺序决定了Y轴顺序,因此要在最后生成DataSource的时候才是其真正的顺序
        /// </summary>
        /// <param name="areatemp"></param>
        private TreeNode CreateChartArea(PMSChartArea Aim)
        {
             TreeNode result = null;
             if (Aim != null)
             {
                 result = new TreeNode(Aim.Name, 1, 1);
                 result.Name = Aim.Name;
                 result.Tag = Aim;
                 TreeNode curvex = new TreeNode(Properties.Resources.ResourceManager.GetString("context0014"), 8, 8);
                 curvex.Name = Properties.Resources.ResourceManager.GetString("context0014");
                 CurveX x = new CurveX(Aim.AxisX.ToAxis());
                 x.SourceField = this.ChartParent.SourceField;
                 x.xRecordField = this.XAixs;
                 x.FixPoint = Aim.AxisX.FixPoint;
                 curvex.Tag = x;
                 result.Nodes.Add(curvex);
                 TreeNode multiy = new TreeNode(Properties.Resources.ResourceManager.GetString("context0015"), 8, 8);
                 multiy.Name = Properties.Resources.ResourceManager.GetString("context0015");
                 CreateYaxis(Aim, ref multiy);
                 result.Nodes.Add(multiy);
                 TreeNode field = new TreeNode(Properties.Resources.ResourceManager.GetString("context0016"), 7, 7);
                 field.Name = Properties.Resources.ResourceManager.GetString("context0016");
                 CreateField(Aim, ref field);
                 result.Nodes.Add(field);

                 if (Aim.LegendDataList != null && Aim.LegendDataList is List<string>)
                 {
                     List<string> strtemp = Aim.LegendDataList as List<string>;
                     for (int i = 0; i < strtemp.Count; i++)
                     {
                         string str = strtemp[i];
                         TreeNode newnode = CreatChartLegend(str);
                         if (newnode != null)
                         {
                             result.Nodes.Add(newnode);
                         }
                     }
                 }
                 if (Aim.TitleDataList != null && Aim.TitleDataList is List<string>)
                 {
                     List<string> strtemp = Aim.TitleDataList as List<string>;
                     for (int i = 0; i < strtemp.Count; i++)
                     {
                         string str = strtemp[i];
                         TreeNode newnode = CreatChartTiTle(str);
                         if (newnode != null)
                         {
                             result.Nodes.Add(newnode);
                         }
                     }
                 }
             }
             return result;
        }
        /// <summary>
        /// 2011.11.02 增加
        /// 目的:根据主要区域生成第一个Y轴并根据多Y轴字段信息生成其他的Y轴信息
        /// </summary>
        /// <param name="fatherarea"></param>
        /// <param name="Ynode"></param>
        private void CreateYaxis(PMSChartArea fatherarea, ref TreeNode Ynode)
        {
            if (_MultiY != null)
            {
                if (_MultiY.ContainsKey(fatherarea.Name))
                {
                    List<string> temp = _MultiY[fatherarea.Name];
                    for (int i = 0; i < temp.Count; i++)
                    {
                        if (i == 0)
                        {
                            CurveY y = new CurveY(null);
                            y.Name = temp[i];
                            y.AxisY = fatherarea.AxisY;
                            y.IsCopyChart = false;
                            TreeNode curvey = new TreeNode(y.Name, 8, 8);
                            curvey.Name = y.Name;
                            curvey.Tag = y;
                            Ynode.Nodes.Add(curvey);
                            if (!chartAreaList.Contains(y as PMSChartArea))
                            {
                                chartAreaList.Add(y as PMSChartArea);
                            }
                        }
                        else
                        {
                            CurveY y=GetCurveYByName(temp[i]);
                            if (y != null)
                            {
                                TreeNode curvey = new TreeNode(y.Name, 8, 8);
                                curvey.Name = y.Name;
                                curvey.Tag = y;
                                Ynode.Nodes.Add(curvey);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 2011.11.03 增加
        /// 目的:根据序列化信息生成字段信息
        /// </summary>
        /// <param name="fatherarea"></param>
        /// <param name="Fields"></param>
        private void CreateField(PMSChartArea fatherarea, ref TreeNode Fields)
        {
            if (_MultiField!=null)
            {
                if (_MultiField.ContainsKey(fatherarea.Name))
                {
                    List<string> temp = _MultiField[fatherarea.Name];
                    for (int i = 0; i < temp.Count; i++)
                    {

                        CurveSeries y = GetCurveSeriesByName(temp[i]);
                        if (y != null)
                        {
                            TreeNode curvey = new TreeNode(y.Name, 3, 3);
                            curvey.Name = y.Name;
                            curvey.Tag = y;
                            y.Tag = curvey;
                            Fields.Nodes.Add(curvey);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 2011.11.03 增加
        /// 目的:当主区域的坐标改动,所有隐藏的拷贝区域的坐标也要改动,隐藏区域的
        /// 坐标要与主区域坐标一直相同 并且其他的Y轴坐标也要前移
        /// </summary>
        /// <param name="fatherarea"></param>
        private void DealWithCopyAreaPosition(PMSChartArea fatherarea)
        {
            if (chartAreaList != null)
            {
                for (int i = 0; i < chartAreaList.Count; i++)
                {
                    PMSChartArea areatemp = chartAreaList[i];
                    if (areatemp is CurveY && (areatemp as CurveY).IsCopyChart == true)
                    {
                        CurveY ytemp = areatemp as CurveY;
                        ElementPosition temp = ytemp.Position.ToElementPosition();
                        temp.FromRectangleF(fatherarea.Position.ToElementPosition().ToRectangleF());
                        ElementPosition bb = ytemp.InnerPlotPosition.ToElementPosition();
                        bb.FromRectangleF(fatherarea.InnerPlotPosition.ToElementPosition().ToRectangleF());
                        ytemp.Position = new PMSElementPosition(temp);
                        ytemp.InnerPlotPosition = new PMSElementPosition(bb);
                    }
                }
            }
        }
        /// <summary>
        /// 2011.11.03 增加
        /// 目的:当有轴增加或者删除的时候 对其他的轴的坐标要自动调整
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="currentyaxisindex"></param>
        private void DealWithOtherYaxisPosition(PMSChartArea fatherarea,int distance, string currentyaxisname)
        {
            if (_MultiY != null)
            {
                if (_MultiY.ContainsKey(fatherarea.Name))
                {
                    List<string> yaxistemp = _MultiY[fatherarea.Name];
                    if (yaxistemp != null)
                    {
                        if (distance > 0)
                        {
                            for (int i = 0; i < yaxistemp.Count; i++)
                            {
                                string str = yaxistemp[i];
                                CurveY ytemp = GetCurveYByName(str);
                                if (ytemp != null)
                                {
                                    ytemp.Position.X += distance;
                                    if (ytemp.Position.X + ytemp.Position.Width > 100)
                                    {
                                        float widthtemp = ytemp.Position.Width - (ytemp.Position.X + ytemp.Position.Width + 5 - 100);
                                        if (widthtemp >= 0)
                                        {
                                            ytemp.Position.Width -= ytemp.Position.X + ytemp.Position.Width + 5 - 100;
                                        }
                                        else
                                        {
                                            throw new NotImplementedException(Properties.Resources.ResourceManager.GetString("message0008"));
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < yaxistemp.Count; i++)
                            {
                                string str = yaxistemp[i];
                                if (str != currentyaxisname)
                                {
                                    CurveY ytemp = GetCurveYByName(str);
                                    if (ytemp != null)
                                    {
                                        ytemp.Position.X += distance;
                                        if (ytemp.Position.X + ytemp.Position.Width > 100)
                                        {
                                            float widthtemp = ytemp.Position.Width - (ytemp.Position.X + ytemp.Position.Width + 5 - 100);
                                            if (widthtemp >= 0)
                                            {
                                                ytemp.Position.Width -= ytemp.Position.X + ytemp.Position.Width + 5 - 100;
                                            }
                                            else
                                            {
                                                throw new NotImplementedException(Properties.Resources.ResourceManager.GetString("message0008"));
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 2011.10.28 重载
        /// 目的:根据序列化信息文件生成一个图例的树型结构
        /// </summary>
        /// <param name="Aim"></param>
        /// <returns></returns>
        private TreeNode CreatChartLegend(string str)
        {
            TreeNode result = null;
            //MESLegend temp = GetMESLegendByBindingName(str);
            MESLegend temp = GetMESLegendByName(str);
            if (temp != null)
            {
                result = new TreeNode(temp.Name, 2, 2);
                result.Name = temp.Name;
                result.Tag = temp;
            }
            return result;
        }
        /// <summary>
        /// 2011.10.28 增加
        /// 目的:根据名字获取MES图例
        /// (这也就是要求字段与数据序列要限制成一一对应的关系)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private MESLegend GetMESLegendByName(string str)
        {
            MESLegend result = null;
            if (legendList != null)
            {
                for (int i = 0; i < legendList.Count; i++)
                {
                    PMSLegend temp = legendList[i];
                    if (temp is MESLegend)
                    {
                        if ((temp as MESLegend).Name == str)
                        {
                            return temp as MESLegend;
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 2011.10.28 增加
        /// 目的:根据序列化信息文件为区域增加标题
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private TreeNode CreatChartTiTle(string str)
        {
            TreeNode result = null;
            PMSTitle pca = GetPMSTitleByName(str);
            if (pca != null)
            {
                result = new TreeNode(pca.Name, 0, 0);
                result.Name = pca.Name;
                result.Tag = pca;
            }
            return result;
        }
        /// <summary>
        /// 2011.10.28 增加
        /// 目的:根据名字获取PMS标题
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private PMSTitle GetPMSTitleByName(string str)
        {
            PMSTitle result = null;
            if (titleList != null)
            {
                for (int i = 0; i < titleList.Count; i++)
                {
                    PMSTitle temp = titleList[i];
                    if (temp.Name == str)
                    {
                        return temp;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 2011.10.27 增加
        /// 目的:校验名字是否有重复有返回true无返回false
        /// </summary>
        /// <param name="Aim"></param>
        /// <returns></returns>
        private bool CheckNameFromList(object Aim)
        {
            bool result = false;
            if (Aim != null)
            {
                if (Aim is PMSChartArea)
                {
                    if (chartAreaList != null)
                    {
                        for (int i = 0; i < chartAreaList.Count; i++)
                        {
                            PMSChartArea temp = chartAreaList[i];
                            if (temp != (Aim as PMSChartArea) && temp.Name == (Aim as PMSChartArea).Name)
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
                else if (Aim is PMSSeries)
                {
                    if (seriesList != null)
                    {
                        for (int i = 0; i < seriesList.Count; i++)
                        {
                            PMSSeries temp = seriesList[i];
                            if (temp != (Aim as PMSSeries) && temp.Name == (Aim as PMSSeries).Name)
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
                else if (Aim is PMSLegend)
                {
                    if (legendList != null)
                    {
                        for (int i = 0; i < legendList.Count; i++)
                        {
                            PMSLegend temp = legendList[i];
                            if (temp != (Aim as PMSLegend) && temp.Name == (Aim as PMSLegend).Name)
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
                else if (Aim is PMSTitle)
                {
                    if (titleList != null)
                    {
                        for (int i = 0; i < titleList.Count; i++)
                        {
                            PMSTitle temp = titleList[i];
                            if (temp != (Aim as PMSTitle) && temp.Name == (Aim as PMSTitle).Name)
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
            }
            return result;
        }
        #endregion

        #region 2011.10.26 控件响应的各种事件
        /// <summary>
        /// 2011.10.31 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurveApperence_Load(object sender, EventArgs e)
        {
            if (_YaxisAndCopyChart == null)
            {
                _YaxisAndCopyChart = new Dictionary<string, string>();
            }
            if (_MultiY == null)
            {
                _MultiY = new Dictionary<string, List<string>>();
            }
            if (_MultiYaxis == null)
            {
                _MultiYaxis = new List<string>();
            }
            if (_MultiField == null)
            {
                _MultiField = new Dictionary<string, List<string>>();
            }
            if (chartAreaList.Count == 0)
            {
                treeView1.Nodes.Clear();
                string rootname = Properties.Resources.ResourceManager.GetString("context0002");
                TreeNode rootnode = new TreeNode(rootname, 1, 1);
                rootnode.Name = rootname;
                treeView1.Nodes.Add(rootnode);
                TreeNode nodetemp = CreatChartArea(1, 1);
                if (treeView1.Nodes[rootname] != null)
                {
                    treeView1.Nodes[rootname].Nodes.Add(nodetemp);
                    treeView1.Nodes[rootname].ExpandAll();
                }
            }
            else
            {
                seriesDataForAppearance.Clear();
                if (seriesList != null)
                {
                    for (int m = 0; m < seriesList.Count; m++)
                    {
                        PMSSeries aa = seriesList[m];
                        if (aa is CurveSeries)
                        {
                            if (this.ChartParent != null)
                            {
                                (aa as CurveSeries).SourceField = this.ChartParent.SourceField;
                                (aa as CurveSeries).seriesDataForAppearance = this.seriesDataForAppearance;
                                if (!seriesDataForAppearance.Contains((aa as CurveSeries).BindingField))
                                {
                                    seriesDataForAppearance.Add((aa as CurveSeries).BindingField);
                                }
                            }
                        }
                    }
                }
                seriesDataForLegend.Clear();
                if (legendList != null)
                {
                    for (int m = 0; m < legendList.Count; m++)
                    {
                        PMSLegend aa = legendList[m];
                        if (aa is MESLegend)
                        {
                            if (this.ChartParent != null)
                            {
                                (aa as MESLegend).SourceField = this.ChartParent.SourceField;
                                (aa as MESLegend).seriesDataForLegend = this.seriesDataForLegend;
                                if (!seriesDataForLegend.Contains((aa as MESLegend).BindingField))
                                {
                                    seriesDataForLegend.Add((aa as MESLegend).BindingField);
                                }
                            }
                        }
                    }
                }
                treeView1.Nodes.Clear();
                string rootname = Properties.Resources.ResourceManager.GetString("context0002");
                TreeNode rootnode = new TreeNode(rootname, 1, 1);
                rootnode.Name = rootname;
                treeView1.Nodes.Add(rootnode);
                if (chartAreaList != null)
                {
                    for (int i = 0; i < chartAreaList.Count; i++)
                    {
                        PMSChartArea areatemp = chartAreaList[i];
                        if (!(areatemp is CurveY))
                        {
                            TreeNode areanode = CreateChartArea(areatemp);
                            if (treeView1.Nodes[rootname] != null)
                            {
                                treeView1.Nodes[rootname].Nodes.Add(areanode);
                            }
                        }
                    }
                    treeView1.ExpandAll();
                }
            }
        }
        /// <summary>
        /// 2011.10.31 选择树节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            propertyGrid1.SelectedObject = e.Node.Tag;
        }
        /// <summary>
        /// 2011.10.26 按下确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sure_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }
        /// <summary>
        /// 2011.10.31 按下取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }
        /// <summary>
        /// 2011.10.31 按下应用按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Apply_Click(object sender, EventArgs e)
        {
            if (this.ChartParent != null)
            {
                DealWithConvert();
                ChartParent.SqlSource = this.SqlSource;
                ChartParent.InitailColumnData();
            }
        }
        /// <summary>
        /// 2011.10.31 左右鼠标点击节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeView1.SelectedNode = e.Node;
                if (e.Node != null)
                {
                    _SelectedNode = e.Node;
                    if (e.Node.Tag == null && e.Node.Parent == null)
                    {
                        //treeView1.ContextMenuStrip = ManageArea;
                        //ManageArea.Visible = true;
                        treeView1.ContextMenuStrip = null;
                    }
                    else if (e.Node.Tag != null && e.Node.Tag is PMSChartArea && !(e.Node.Tag is CurveY))
                    {
                        treeView1.ContextMenuStrip = AreaChildManage;
                        AreaChildManage.Visible = true;
                    }
                    else if (e.Node.Tag != null && e.Node.Tag is CurveX)
                    {
                        treeView1.ContextMenuStrip = null;
                    }
                    else if (e.Node.Tag == null && e.Node.Text == Properties.Resources.ResourceManager.GetString("context0015"))
                    {
                        treeView1.ContextMenuStrip = MultiY;
                        MultiY.Visible = true;
                    }
                    else if (e.Node.Tag != null && e.Node.Tag is CurveY)
                    {
                        treeView1.ContextMenuStrip = DeleteY;
                        DeleteY.Visible = true;
                    }
                    else if (e.Node.Tag == null && e.Node.Text == Properties.Resources.ResourceManager.GetString("context0016"))
                    {
                        treeView1.ContextMenuStrip = ManageSeries;
                        ManageSeries.Visible = true;
                    }
                    else if (e.Node.Tag != null && e.Node.Tag is CurveSeries)
                    {
                         treeView1.ContextMenuStrip = FieldDelete;
                         FieldDelete.Visible = true;
                    }
                    else if (e.Node.Tag != null && e.Node.Tag is PMSLegend)
                    {
                        treeView1.ContextMenuStrip = LegendDelete;
                        LegendDelete.Visible = true;
                    }
                    else if (e.Node.Tag != null && e.Node.Tag is PMSTitle)
                    {
                        treeView1.ContextMenuStrip = TitleDelete;
                        TitleDelete.Visible = true;
                    }
                    else
                    {
                        treeView1.ContextMenuStrip = null;
                    }
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                propertyGrid1.SelectedObject = e.Node.Tag;
            }
        }
        /// <summary>
        /// 2011.10.26 属性窗口值变化
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "Name")
            {
                if (this.treeView1.SelectedNode.Tag != null)
                {
                    if (CheckNameFromList(this.treeView1.SelectedNode.Tag) == false)
                    {
                        this.treeView1.SelectedNode.Text = e.ChangedItem.Value.ToString();
                        this.treeView1.Name = e.ChangedItem.Value.ToString();
                    }
                    else
                    {
                        if (this.treeView1.SelectedNode.Tag is PMSChartArea)
                        {
                            (this.treeView1.SelectedNode.Tag as PMSChartArea).Name = e.OldValue.ToString();
                            MessageBox.Show(Properties.Resources.ResourceManager.GetString("message0002"));
                        }
                        else if (this.treeView1.SelectedNode.Tag is PMSSeries)
                        {
                            (this.treeView1.SelectedNode.Tag as PMSSeries).Name = e.OldValue.ToString();
                            MessageBox.Show(Properties.Resources.ResourceManager.GetString("message0002"));
                        }
                        else if (this.treeView1.SelectedNode.Tag is PMSLegend)
                        {
                            (this.treeView1.SelectedNode.Tag as PMSLegend).Name = e.OldValue.ToString();
                            MessageBox.Show(Properties.Resources.ResourceManager.GetString("message0002"));
                        }
                        else if (this.treeView1.SelectedNode.Tag is PMSTitle)
                        {
                            (this.treeView1.SelectedNode.Tag as PMSTitle).Name = e.OldValue.ToString();
                            MessageBox.Show(Properties.Resources.ResourceManager.GetString("message0002"));
                        }
                    }
                }
            }
            else if (e.ChangedItem.Label == "BindingField")
            {
                if (this.treeView1.SelectedNode.Tag != null)
                {
                    if (this.treeView1.SelectedNode.Tag is PMSChartArea)
                    {
                    }
                    else if (this.treeView1.SelectedNode.Tag is CurveSeries)
                    {
                        seriesDataForAppearance.Remove(e.OldValue.ToString());
                        CurveSeries temp = treeView1.SelectedNode.Tag as CurveSeries;
                        if (!seriesDataForAppearance.Contains(temp.BindingField))
                        {
                            seriesDataForAppearance.Add(temp.BindingField);
                        }
                    }
                    else if (this.treeView1.SelectedNode.Tag is MESLegend)
                    {
                        seriesDataForLegend.Remove(e.OldValue.ToString());
                        MESLegend temp = treeView1.SelectedNode.Tag as MESLegend;
                        if (!seriesDataForLegend.Contains(temp.BindingField))
                        {
                            seriesDataForLegend.Add(temp.BindingField);
                        }
                    }
                    else if (this.treeView1.SelectedNode.Tag is PMSTitle)
                    {
                    }
                }
            }
            else if (e.ChangedItem.Label == "BindingYaxis")
            {
                if (this.treeView1.SelectedNode.Tag != null)
                {
                    if (this.treeView1.SelectedNode.Tag is CurveSeries)
                    {
                        CurveSeries temp = treeView1.SelectedNode.Tag as CurveSeries;
                    }
                }
            }
        }
        /// <summary>
        /// 2011.11.01 新增区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddArea_Click(object sender, EventArgs e)
        {
            //if (_SelectedNode != null)
            //{
            //    AddChartArea(_SelectedNode);
            //}
        }
        /// <summary>
        /// 2011.11.01 删除区域
        /// 删除的时候要校验不能删最后一个,默认至少要有一个区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteArea_Click(object sender, EventArgs e)
        {
            //if (_SelectedNode != null)
            //{
            //    DeleteChartArea(_SelectedNode);
            //}
        }
        /// <summary>
        /// 2011.11.1 新增图例
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddLegend_Click(object sender, EventArgs e)
        {
            if (_SelectedNode != null)
            {
                AddChartLegend(_SelectedNode);
            }
        }
        /// <summary>
        /// 2011.11.1 新增标题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddTitile_Click(object sender, EventArgs e)
        {
            if (_SelectedNode != null)
            {
                AddChartTitile(_SelectedNode);
            }
        }
        /// <summary>
        /// 2011.11.1 新增字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddField_Click(object sender, EventArgs e)
        {
            if (_SelectedNode != null)
            {
                AddChartField(_SelectedNode);
            }
        }
        /// <summary>
        /// 2011.11.1 删除绑定字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteField_Click(object sender, EventArgs e)
        {
            if (_SelectedNode != null)
            {
                DeleteChartField(_SelectedNode);
            }
        }
        /// <summary>
        /// 2011.11.01 删除图例
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteLegend_Click(object sender, EventArgs e)
        {
            if (_SelectedNode != null)
            {
                DeleteChartLegend(_SelectedNode);
            }
        }
        /// <summary>
        /// 2011.11.1 删除标题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteTitle_Click(object sender, EventArgs e)
        {
            if (_SelectedNode != null)
            {
                DeleteChartTitle(_SelectedNode);
            }
        }
        /// <summary>
        /// 2011.11.01 增加Y轴
        /// 在增加的时候要校验名字 并且要在区域中增加一条记录 
        /// 同时在区域的Y轴管理中增加一条关系记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddY_Click(object sender, EventArgs e)
        {
            if (_SelectedNode != null)
            {
                AddYaxis(_SelectedNode);
            }
        }
        /// <summary>
        /// 2011.11.01 删除Y轴
        /// 在删除的时候要确保删除后至少还有一个Y轴
        /// 如果能删除 要在区域集合中移除相应的Y轴区域,并且在
        /// 区域的Y轴管理中删除相应的关系记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YDelete_Click(object sender, EventArgs e)
        {
            if (_SelectedNode != null)
            {
                DeleteYaxis(_SelectedNode);
            }
        }
        #endregion

    }
}
