using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using PMS.Libraries.ToolControls.Report.Element;
using MES.Controls.Design;
using System.ComponentModel.Design;
using System.IO;
using PMS.Libraries.ToolControls.Report.Elements.Util;

namespace PMS.Libraries.ToolControls.PMSChart
{
    [ToolboxBitmap(typeof(RadarAlertChart), "Resources.RadarAlertChart.png")]
    [Designer(typeof(MESDesigner))]
    public partial class RadarAlertChart : UserControl, IChartFunction, IResizable, ICloneable, ISuspensionable,IElementTranslator,IElement
    {
        public RadarAlertChart() 
        {
            InitializeComponent();
        }
        public RadarAlertChart(MemoryStream Aim) 
        {
            try
            {
                InitializeComponent();

                if (Aim != null)
                {
                    Aim.Seek(0, SeekOrigin.Begin);
                    chart1.Serializer.Load(Aim);
                }
                
            }
            catch (System.Exception ex)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, Properties.Resources.ResourceManager.GetString("message0006") + ex.Message, false);
            }
        }
        
        List<AlertVar> AlertList = new List<AlertVar>();

        //数据外观管理
        private List<PMSSeries> seriesList = new List<PMSSeries>();

        //量程计算
        List<double> getMaxValue = new List<double>();
        List<double> getMinValue = new List<double>();
        //标题管理
        private List<PMSTitle> titleList = new List<PMSTitle>();

        List<string> DecName = new List<string>();
        
        double[] yValues;

        private Point OriginPosition;
        private int OriginWidth;
        private int OriginHeight;

        //总数据源
        private DataTable _ReportData;

        //运行状态
        private int _RunMode = 0;
        SourceField _DataTable;
        DataSource _Apperence;

        [Browsable(false)]
        public virtual DataTable ReportData
        {
            get { return _ReportData; }
            set
            {
                _ReportData = value;

                OriginPosition = new Point(this.Location.X, this.Location.Y);
            }
        }

        [Browsable(false)]
        public int RunMode
        {
            get { return _RunMode; }
            set { _RunMode = value; }
        }

        [Category("MES报表属性")]
        [Description("设置数据表")]
        [Editor(typeof(DataTableEditor), typeof(UITypeEditor))]
        public SourceField SourceField
        {
            get
            {
                return _DataTable;
            }
            set
            {
                if (value != null)
                {
                    if (!value.Equals(_DataTable) && _DataTable != null)
                    {
                        this.AlertList.Clear();
                        _Apperence.allAlertList.Clear();
                        InitailColumnData();
                        _DataTable = value;
                    }
                    else
                    {
                        _DataTable = value;
                    }
                }
                else
                {
                    _DataTable = value;

                    try
                    {
                        this.AlertList.Clear();
                        _Apperence.allAlertList.Clear();
                    }
                    catch { }
                    InitailColumnData();
                }
            }
        }

        [Category("MES报表属性")]
        [Description("外观设置")]
        [Editor(typeof(ApperenceEditor), typeof(UITypeEditor))]
        public DataSource Apperence
        {
            get {
                return _Apperence;
            }
            set
            {
                
                _Apperence = value;
                InitailColumnData();
            }
        }



        //[Category("MES报表属性")]
        //public PmsPowerGroupCheck.ActionInfo ActionConfig
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 对属性窗口配置的属性进行传递
        /// </summary>
        void upData()
        {
            try
            {
                this.seriesList = this.Apperence.SeriesList;
                this.titleList = this.Apperence.TitleList;
                this.AlertList = this.Apperence.alertList;
            }
            catch { }
        }

        /// <summary>
        /// 绘制一个虚拟的图表外观
        /// </summary>
        void DrawVirtualChart() 
        {
            try
            {
                chart1.Series.Clear();
                chart1.Titles.Clear();
                chart1.Legends.Clear();
                Series series1 = new Series();
                if (this.Apperence != null)
                {
                    if (this.Apperence.TitleList.Count != 0)
                    {
                        foreach (PMSTitle item in this.Apperence.TitleList)
                        {
                            Title title1 = new Title();
                            item.SetTitle(title1);
                            chart1.Titles.Add(title1);
                        }
                    }
                    if (this.Apperence.SeriesList.Count != 0)
                    {
                        this.Apperence.SeriesList[0].SetSeriesValue(series1);
                    }
                    if (this.Apperence.LegendList.Count != 0)
                    {
                        Legend legend1 = new Legend();
                        legend1.IsDockedInsideChartArea = false;
                        chart1.Legends.Add(legend1);

                        series1.LegendText = "虚拟外观";
                    }
                    if (this.Apperence.ChartAreaList.Count != 0)
                    {
                        this.Apperence.ChartAreaList[0].SetChartArea(chart1.ChartAreas[0]);
                        if (chart1.ChartAreas[0].AxisY.IntervalOffset == 1)
                        {
                            chart1.ChartAreas[0].AxisY.IntervalOffset = 10;
                        }
                    }
                    else { chart1.ChartAreas[0].Area3DStyle.Enable3D = true; }
                }

                chart1.ChartAreas[0].AxisY.LabelStyle.Enabled = false;
                series1.ChartType = SeriesChartType.Radar;
                series1.IsValueShownAsLabel = false;
                chart1.Series.Add(series1);
                Random random = new Random();
                if (this.Apperence != null && this.Apperence.alertList.Count != 0)
                {
                    for (int i = 0; i < this.Apperence.alertList.Count; i++)
                    {
                        series1.Points.AddXY(this.Apperence.alertList[i].DescribeName, 10.0 * random.NextDouble());

                    }
                }
                else
                {
                    for (int j = 0; j < 8; j++)
                        series1.Points.Add(10.0 * random.NextDouble());
                }
            }
            catch { throw new Exception("DrawVirtualChart"); }
           
        }

        /// <summary>
        /// 控件初始化，获取并组织配置的属性
        /// </summary>
        public void InitailColumnData()
        {
            try
            {

                if (this.DesignMode == true)
                {
                    //return DrawVirtualChart(0);
                    DrawVirtualChart();
                    upData();
                   
                }
                else if (ReportData == null || ReportData.Rows.Count == 0)
                {
                    DrawVirtualChart();
                }
                else if (AlertList.Count==0)
                {
                    DrawVirtualChart();
                }
                else
                {
                    if (AlertList.Count != 0 && ReportData != null)
                    {
                        #region 2011.12.19 获取统计的数据

                        yValues = new double[AlertList.Count];
                        string vname = "";
                        for (int i = 0; i < AlertList.Count; i++)
                        {
                            List<double> getVarFun = new List<double>();
                            vname = AlertList[i].Name;
                            //1.11 新
                            switch (AlertList[i].AlertFunction)
                            {
                                case AlertVar.enumAlertFunction.Sum:
                                    yValues[i] = Convert.ToDouble(ReportData.Compute("Sum(" + vname + ")", ""));
                                    break;
                                case AlertVar.enumAlertFunction.Max:
                                    yValues[i] = Convert.ToDouble(ReportData.Compute("Max(" + vname + ")", ""));
                                    break;
                                case AlertVar.enumAlertFunction.Min:
                                    yValues[i] = Convert.ToDouble(ReportData.Compute("Min(" + vname + ")", ""));
                                    break;
                                case AlertVar.enumAlertFunction.Average:
                                    yValues[i] = Convert.ToDouble(ReportData.Compute("Avg(" + vname + ")", ""));
                                    break;
                            }

                            
                        }
                        #endregion
                    }
                    #region 2012.2.15 将组织数据的过程与获取数据衔接
                    if (yValues != null)
                    {
                        chart1.Legends.Clear();
                        
                        for (int i = 0; i < this.Apperence.LegendList.Count; i++) 
                        {
                            Legend legend1 = new Legend();
                            this.Apperence.LegendList[i].SetLegend(legend1);
                            chart1.Legends.Add(legend1);
                        }

                        if (this.Apperence.SeriesList[0].LegendText == "")
                        {
                            this.Apperence.SeriesList[0].LegendText = "上限外观";
                        }
                        if (this.Apperence.SeriesList[1].LegendText == "")
                        {
                            this.Apperence.SeriesList[1].LegendText = "下限外观";
                        }
                        if (this.Apperence.SeriesList[2].LegendText == "")
                        {
                            this.Apperence.SeriesList[2].LegendText = "实际值";
                        }

                        chart1.Titles.Clear();
                        foreach (PMSTitle item in this.Apperence.TitleList)
                        {
                            Title title1 = new Title();
                            item.SetTitle(title1);
                            chart1.Titles.Add(title1);
                        }
                        chart1.ChartAreas.Clear();
                        chart1.Series.Clear();
                        ChartArea chartAreaMain = new ChartArea();
                        if (this.Apperence.ChartAreaList.Count != 0)
                        {
                            this.Apperence.ChartAreaList[0].SetChartArea(chartAreaMain);
                        }
                        chartAreaMain.Name = "reportData";
                        chart1.ChartAreas.Add(chartAreaMain);
                        chart1.ChartAreas["reportData"].AxisY.LabelStyle.Enabled = false;
                        chart1.ChartAreas["reportData"].AxisY.Crossing = 0;//具体用途忘记
                        chart1.ChartAreas["reportData"].Position = new ElementPosition(3, 3, 94, 94);
                        chart1.ChartAreas["reportData"].InnerPlotPosition = new ElementPosition(5, 5, 90, 90);
                        Series series1 = new Series();
                        Series series2 = new Series();
                        Series series3 = new Series();
                        this.Apperence.SeriesList[0].SetSeriesValue(series1);
                        this.Apperence.SeriesList[1].SetSeriesValue(series2);
                        this.Apperence.SeriesList[2].SetSeriesValue(series3);
                        series1.Legend = this.Apperence.SeriesList[0].Legend;
                        series2.Legend = this.Apperence.SeriesList[1].Legend;
                        series3.Legend = this.Apperence.SeriesList[2].Legend;
                        chart1.Series.Add(series1);
                        chart1.Series.Add(series2);
                        chart1.Series.Add(series3);
                        series1.ChartArea = "reportData";
                        series2.ChartArea = "reportData";
                        series3.ChartArea = "reportData";
                        for (int i = 0; i < AlertList.Count; i++)
                        {
                            DecName.Add(AlertList[i].DescribeName);
                        }
                        string[] Virtual = { "a", "b", "c" };
                        for (int i = 0; i < AlertList.Count; i++)
                        {
                            double[] yValue;
                            if (AlertList.Count <= 2)
                                yValue = new double[3];
                            else
                                yValue = new double[AlertList.Count];
                            for (int j = 0; j < AlertList.Count; j++)
                                yValue[j] = 0;
                            yValue[i] = yValues[i];
                            yValue[(i + 1) % yValue.Length] = AlertList[i].MaxValue;
                            yValue[(i + 2) % yValue.Length] = AlertList[i].MinValue;
                            Series series0 = new Series();
                            chart1.Series.Add(series0);
                            series0.ChartType = SeriesChartType.Radar;
                            series0.CustomProperties = "RadarDrawingStyle=Marker";
                            series0.MarkerSize = 0;
                            series0.IsVisibleInLegend = false;
                            if (AlertList.Count <= 2)
                                series0.Points.DataBindXY(Virtual, yValue);
                            else
                                series0.Points.DataBindXY(DecName, yValue);
                            ChartArea chartArea1 = new ChartArea();
                            chart1.ChartAreas.Add(chartArea1);
                            chartArea1.InnerPlotPosition = chart1.ChartAreas[0].InnerPlotPosition;
                            chartArea1.Position = chart1.ChartAreas[0].Position;
                            chartArea1.Name = "ChartArea" + (i + 1);
                            series0.ChartArea = "ChartArea" + (i + 1);
                            chartArea1.AlignWithChartArea = "reportData";
                            if (i == 0)
                            {
                                chart1.ChartAreas[0].AlignWithChartArea = "ChartArea1";
                            }
                            chartArea1.AxisX.Enabled = AxisEnabled.False;
                            chartArea1.AxisY.MajorGrid.Enabled = false;
                            chartArea1.AxisY.MajorTickMark.Enabled = false;
                            chartArea1.AxisY.LabelStyle.Enabled = false;
                            chartArea1.BorderColor = Color.Transparent;
                            chartArea1.BackColor = Color.Transparent;


                        }
                    }

                    if (chart1.Series.Count != 3)
                    {
                        chart1.Printing.PrintPaint(chart1.CreateGraphics(), new Rectangle(3,3,94,94));
                        try
                        {
                            if (DecName.Count != 0 && yValues != null && chart1.ChartAreas.Count != 1)
                            {
                                if (!double.IsNaN(chart1.ChartAreas["ChartArea1"].AxisY.Maximum))
                                {
                                    double[] yValuesNow = new double[AlertList.Count];
                                    double[] yValuesMax = new double[AlertList.Count];
                                    double[] yValuesMin = new double[AlertList.Count];
                                    if (AlertList.Count <= 2)
                                        for (int i = 1; i < chart1.ChartAreas.Count; i++)
                                        {
                                            if (AlertList[i - 1].AlertReportMode == AlertVar.enumAlertReportMode.Absolute)
                                            {
                                                yValuesNow[i - 1] = chart1.Series[i + 2].Points[i - 1].YValues[0];
                                                yValuesMax[i - 1] = chart1.Series[i + 2].Points[i % 3].YValues[0];
                                                yValuesMin[i - 1] = chart1.Series[i + 2].Points[(i + 1) % 3].YValues[0];
                                            }
                                            else
                                            {
                                                yValuesNow[i - 1] = (chart1.Series[i + 2].Points[i - 1].YValues[0] + Math.Abs(chart1.ChartAreas["ChartArea" + i].AxisY.Minimum)) / (Math.Abs(chart1.ChartAreas["ChartArea" + i].AxisY.Maximum) + Math.Abs(chart1.ChartAreas["ChartArea" + i].AxisY.Minimum));
                                                yValuesMax[i - 1] = (chart1.Series[i + 2].Points[i % 3].YValues[0] + Math.Abs(chart1.ChartAreas["ChartArea" + i].AxisY.Minimum)) / (Math.Abs(chart1.ChartAreas["ChartArea" + i].AxisY.Maximum) + Math.Abs(chart1.ChartAreas["ChartArea" + i].AxisY.Minimum));
                                                yValuesMin[i - 1] = (chart1.Series[i + 2].Points[(i + 1) % 3].YValues[0] + Math.Abs(chart1.ChartAreas["ChartArea" + i].AxisY.Minimum)) / (Math.Abs(chart1.ChartAreas["ChartArea" + i].AxisY.Maximum) + Math.Abs(chart1.ChartAreas["ChartArea" + i].AxisY.Minimum));
                                            }
                                            if (AlertList[i - 1].HasMaximum)
                                            {
                                                if (AlertList[i - 1].AlertReportMode == AlertVar.enumAlertReportMode.Percent)
                                                    DecName[i - 1] += "\n" + "100%";
                                                else
                                                    DecName[i - 1] += "\n" + chart1.ChartAreas["ChartArea" + i].AxisY.Maximum;
                                            }
                                        }
                                    else
                                        for (int i = 1; i < chart1.ChartAreas.Count; i++)
                                        {
                                            if (AlertList[i - 1].AlertReportMode == AlertVar.enumAlertReportMode.Absolute)
                                            {
                                                yValuesNow[i - 1] = chart1.Series[i + 2].Points[i - 1].YValues[0];
                                                yValuesMax[i - 1] = chart1.Series[i + 2].Points[i % AlertList.Count].YValues[0];
                                                yValuesMin[i - 1] = chart1.Series[i + 2].Points[(i + 1) % AlertList.Count].YValues[0];
                                            }
                                            else
                                            {
                                                yValuesNow[i - 1] = (chart1.Series[i + 2].Points[i - 1].YValues[0] + Math.Abs(chart1.ChartAreas["ChartArea" + i].AxisY.Minimum)) / (Math.Abs(chart1.ChartAreas["ChartArea" + i].AxisY.Maximum) + Math.Abs(chart1.ChartAreas["ChartArea" + i].AxisY.Minimum));
                                                yValuesMax[i - 1] = (chart1.Series[i + 2].Points[i % AlertList.Count].YValues[0] + Math.Abs(chart1.ChartAreas["ChartArea" + i].AxisY.Minimum)) / (Math.Abs(chart1.ChartAreas["ChartArea" + i].AxisY.Maximum) + Math.Abs(chart1.ChartAreas["ChartArea" + i].AxisY.Minimum));
                                                yValuesMin[i - 1] = (chart1.Series[i + 2].Points[(i + 1) % AlertList.Count].YValues[0] + Math.Abs(chart1.ChartAreas["ChartArea" + i].AxisY.Minimum)) / (Math.Abs(chart1.ChartAreas["ChartArea" + i].AxisY.Maximum) + Math.Abs(chart1.ChartAreas["ChartArea" + i].AxisY.Minimum));

                                            }
                                            if (AlertList[i - 1].HasMaximum)
                                            {
                                                if (AlertList[i - 1].AlertReportMode == AlertVar.enumAlertReportMode.Percent)
                                                    DecName[i - 1] += "\n" + "100%";
                                                else
                                                    DecName[i - 1] += "\n" + chart1.ChartAreas["ChartArea" + i].AxisY.Maximum;
                                            }
                                        }

                                    for (int i = 0; i < this.Apperence.alertList.Count; i++)
                                    {
                                        if (yValuesMax[i] < 0 && yValuesMin[i] < 0 && yValuesNow[i] < 0)
                                        {
                                            chart1.Series[0].Points.AddXY(DecName[i], 1 + yValuesMax[i]);
                                            chart1.Series[1].Points.AddXY(DecName[i], 1 + yValuesMin[i]);
                                            chart1.Series[2].Points.AddXY(DecName[i], 1 + yValuesNow[i]);
                                        }
                                        else
                                        {
                                            chart1.Series[0].Points.AddXY(DecName[i], yValuesMax[i]);
                                            chart1.Series[1].Points.AddXY(DecName[i], yValuesMin[i]);
                                            chart1.Series[2].Points.AddXY(DecName[i], yValuesNow[i]);
                                        }
                                    }

                                    //chart1.Series[0].Points.DataBindXY(DecName, yValuesMax);
                                    //chart1.Series[1].Points.DataBindXY(DecName, yValuesMin);
                                    //chart1.Series[2].Points.DataBindXY(DecName, yValuesNow);
                                    for (int i = 0; i < yValuesMax.Length; i++)
                                    {
                                        if (AlertList[i].AlertReportMode == AlertVar.enumAlertReportMode.Relative)
                                        {
                                            if (double.IsNaN(yValuesMax[i]))
                                                yValuesMax[i] = 0;
                                            chart1.Series[0].Points[i].Label = ((yValuesMax[i] - Math.Abs(chart1.ChartAreas["ChartArea" + (i + 1)].AxisY.Minimum) / (Math.Abs(chart1.ChartAreas["ChartArea" + (i + 1)].AxisY.Maximum) + Math.Abs(chart1.ChartAreas["ChartArea" + (i + 1)].AxisY.Minimum))) * (Math.Abs(chart1.ChartAreas["ChartArea" + (i + 1)].AxisY.Maximum) + Math.Abs(chart1.ChartAreas["ChartArea" + (i + 1)].AxisY.Minimum))).ToString();
                                            if (chart1.Series[0].Points[i].Label == "非数字")
                                                chart1.Series[0].Points[i].Label = "0";
                                        }
                                        else if (AlertList[i].AlertReportMode == AlertVar.enumAlertReportMode.Absolute)
                                            chart1.Series[0].Points[i].Label = yValuesMax[i].ToString();
                                        else
                                            chart1.Series[0].Points[i].Label = ((yValuesMax[i] * 100) + "%");
                                        getMaxValue.Add(Math.Abs(chart1.ChartAreas["ChartArea" + (i + 1)].AxisY.Maximum));
                                        getMinValue.Add(Math.Abs(chart1.ChartAreas["ChartArea" + (i + 1)].AxisY.Minimum));
                                    }
                                    for (int i = 0; i < yValuesMin.Length; i++)
                                    {
                                        if (AlertList[i].AlertReportMode == AlertVar.enumAlertReportMode.Relative)
                                        {
                                            if (double.IsNaN(yValuesMin[i]))
                                                yValuesMin[i] = 0;
                                            chart1.Series[1].Points[i].Label = ((yValuesMin[i] - Math.Abs(chart1.ChartAreas["ChartArea" + (i + 1)].AxisY.Minimum) / (Math.Abs(chart1.ChartAreas["ChartArea" + (i + 1)].AxisY.Maximum) + Math.Abs(chart1.ChartAreas["ChartArea" + (i + 1)].AxisY.Minimum))) * (Math.Abs(chart1.ChartAreas["ChartArea" + (i + 1)].AxisY.Maximum) + Math.Abs(chart1.ChartAreas["ChartArea" + (i + 1)].AxisY.Minimum))).ToString();
                                            if (chart1.Series[1].Points[i].Label == "非数字")
                                                chart1.Series[1].Points[i].Label = "0";
                                        }
                                        else if (AlertList[i].AlertReportMode == AlertVar.enumAlertReportMode.Absolute)
                                            chart1.Series[1].Points[i].Label = yValuesMin[i].ToString();
                                        else
                                            chart1.Series[1].Points[i].Label = ((yValuesMin[i] * 100) + "%");
                                    }
                                    for (int i = 0; i < yValuesNow.Length; i++)
                                    {
                                        if (double.IsNaN(yValuesNow[i]))
                                            yValuesNow[i] = 0;
                                        if (AlertList[i].AlertReportMode == AlertVar.enumAlertReportMode.Relative)
                                        {
                                            chart1.Series[2].Points[i].Label = ((yValuesNow[i] - Math.Abs(chart1.ChartAreas["ChartArea" + (i + 1)].AxisY.Minimum) / (Math.Abs(chart1.ChartAreas["ChartArea" + (i + 1)].AxisY.Maximum) + Math.Abs(chart1.ChartAreas["ChartArea" + (i + 1)].AxisY.Minimum))) * (Math.Abs(chart1.ChartAreas["ChartArea" + (i + 1)].AxisY.Maximum) + Math.Abs(chart1.ChartAreas["ChartArea" + (i + 1)].AxisY.Minimum))).ToString();
                                            if (chart1.Series[2].Points[i].Label == "非数字")
                                                chart1.Series[2].Points[i].Label = "0";
                                        }
                                        else if (AlertList[i].AlertReportMode == AlertVar.enumAlertReportMode.Absolute)
                                            chart1.Series[2].Points[i].Label = yValuesNow[i].ToString();
                                        else
                                            chart1.Series[2].Points[i].Label = ((yValuesNow[i] * 100) + "%");
                                    }

                                    if (chart1.ChartAreas[0].AxisY.IntervalOffset == 1)
                                    {
                                        if (AlertList[0].AlertReportMode == AlertVar.enumAlertReportMode.Absolute)
                                            chart1.ChartAreas[0].AxisY.IntervalOffset = getMax(getMaxValue) + getMax(getMinValue);
                                        else 
                                        {
                                            chart1.Printing.PrintPaint(chart1.CreateGraphics(), new Rectangle(3, 3, 94, 94));
                                            chart1.ChartAreas[0].AxisY.IntervalOffset = Math.Abs(chart1.ChartAreas[0].AxisY.Maximum) + Math.Abs(chart1.ChartAreas[0].AxisY.Minimum);
                                        }
                                    }
                                    if (chart1.ChartAreas.Count != 1)
                                    {
                                        int count = chart1.ChartAreas.Count;
                                        for (int i = 1; i < count; i++)
                                        {
                                            chart1.ChartAreas.Remove(chart1.ChartAreas[1]);
                                        }
                                    }
                                    if (chart1.Series.Count != 3)
                                    {
                                        for (int i = chart1.Series.Count - 1; i > 2; i--)
                                        {
                                            chart1.Series.RemoveAt(i);
                                        }
                                    }
                                }
                            }
                        }
                        catch { throw new Exception("组织数据"); }
                    }
                    #endregion

                    
                }
            }
            catch { throw new Exception("InitailColumnData"); }
                

            
        }

        #region 2011.12.19 实现继承的接口方法
        public void PrintPaint(Graphics graphics, Rectangle position)
        {

            try
            {
                //chart1.Printing.PrintPaint(graphics, position);
                #region 2011.12.19 重绘图表区（废弃）
                //try
                //{
                //    if (DecName.Count != 0 && yValues != null && chart1.ChartAreas.Count != 1)
                //    {
                //        if (!double.IsNaN(chart1.ChartAreas["ChartArea1"].AxisY.Maximum))
                //        {
                //            double[] yValuesNow = new double[AlertList.Count];
                //            double[] yValuesMax = new double[AlertList.Count];
                //            double[] yValuesMin = new double[AlertList.Count];
                //            if (AlertList.Count<=2)
                //                for (int i = 1; i < chart1.ChartAreas.Count; i++)
                //                {
                //                    if (AlertList[i - 1].AlertReportMode == AlertVar.enumAlertReportMode.Absolute)
                //                    {
                //                        yValuesNow[i - 1] = chart1.Series[i + 2].Points[i - 1].YValues[0];
                //                        yValuesMax[i - 1] = chart1.Series[i + 2].Points[i % 3].YValues[0];
                //                        yValuesMin[i - 1] = chart1.Series[i + 2].Points[(i + 1) % 3].YValues[0];
                //                    }
                //                    else
                //                    {
                //                        yValuesNow[i - 1] = chart1.Series[i + 2].Points[i - 1].YValues[0] / chart1.ChartAreas["ChartArea" + i].AxisY.Maximum;
                //                        yValuesMax[i - 1] = chart1.Series[i + 2].Points[i % 3].YValues[0] / chart1.ChartAreas["ChartArea" + i].AxisY.Maximum;
                //                        yValuesMin[i - 1] = chart1.Series[i + 2].Points[(i + 1) % 3].YValues[0] / chart1.ChartAreas["ChartArea" + i].AxisY.Maximum;
                //                    }
                //                    if (AlertList[i - 1].HasMaximum) 
                //                    {
                //                        if (AlertList[i - 1].AlertReportMode == AlertVar.enumAlertReportMode.Percent)
                //                            DecName[i - 1] += "\n" + "100%";
                //                        else
                //                            DecName[i - 1] += "\n" + chart1.ChartAreas["ChartArea" + i].AxisY.Maximum;
                //                    }
                //                }
                //            else
                //                for (int i = 1; i < chart1.ChartAreas.Count; i++)
                //                {
                //                    if (AlertList[i-1].AlertReportMode == AlertVar.enumAlertReportMode.Absolute) 
                //                    {
                //                        yValuesNow[i - 1] = chart1.Series[i + 2].Points[i - 1].YValues[0];
                //                        yValuesMax[i - 1] = chart1.Series[i + 2].Points[i % AlertList.Count].YValues[0];
                //                        yValuesMin[i - 1] = chart1.Series[i + 2].Points[(i + 1) % AlertList.Count].YValues[0];
                //                    }
                //                    else
                //                    {
                //                        yValuesNow[i - 1] = chart1.Series[i + 2].Points[i - 1].YValues[0] / chart1.ChartAreas["ChartArea" + i].AxisY.Maximum;
                //                        yValuesMax[i - 1] = chart1.Series[i + 2].Points[i % AlertList.Count].YValues[0] / chart1.ChartAreas["ChartArea" + i].AxisY.Maximum;
                //                        yValuesMin[i - 1] = chart1.Series[i + 2].Points[(i + 1) % AlertList.Count].YValues[0] / chart1.ChartAreas["ChartArea" + i].AxisY.Maximum;
                //                    }
                //                    if (AlertList[i - 1].HasMaximum)
                //                    {
                //                        if (AlertList[i - 1].AlertReportMode == AlertVar.enumAlertReportMode.Percent)
                //                            DecName[i - 1] += "\n" + "100%";
                //                        else
                //                            DecName[i - 1] += "\n" + chart1.ChartAreas["ChartArea" + i].AxisY.Maximum;
                //                    }
                //                }

                //            chart1.Series[0].Points.DataBindXY(DecName, yValuesMax);
                //            chart1.Series[1].Points.DataBindXY(DecName, yValuesMin);
                //            chart1.Series[2].Points.DataBindXY(DecName, yValuesNow);
                //            for (int i = 0; i < yValuesMax.Length; i++)
                //            {
                //                if (AlertList[i].AlertReportMode == AlertVar.enumAlertReportMode.Relative)
                //                    chart1.Series[0].Points[i].Label = (yValuesMax[i] * chart1.ChartAreas["ChartArea" + (i + 1)].AxisY.Maximum).ToString();
                //                else if (AlertList[i].AlertReportMode == AlertVar.enumAlertReportMode.Absolute)
                //                    chart1.Series[0].Points[i].Label = yValuesMax[i].ToString();
                //                else
                //                    chart1.Series[0].Points[i].Label = ((yValuesMax[i] * 100) + "%");
                //                getMaxValue.Add(chart1.ChartAreas["ChartArea" + (i + 1)].AxisY.Maximum);
                //            }
                //            for (int i = 0; i < yValuesMin.Length; i++)
                //            {
                //                if (AlertList[i].AlertReportMode == AlertVar.enumAlertReportMode.Relative)
                //                    chart1.Series[1].Points[i].Label = (yValuesMin[i] * chart1.ChartAreas["ChartArea" + (i + 1)].AxisY.Maximum).ToString();
                //                else if (AlertList[i].AlertReportMode == AlertVar.enumAlertReportMode.Absolute) 
                //                    chart1.Series[1].Points[i].Label = yValuesMin[i].ToString();
                //                else
                //                    chart1.Series[1].Points[i].Label = ((yValuesMin[i] * 100) + "%");
                //            }
                //            for (int i = 0; i < yValuesNow.Length; i++)
                //            {
                //                if (AlertList[i].AlertReportMode == AlertVar.enumAlertReportMode.Relative)
                //                    chart1.Series[2].Points[i].Label = (yValuesNow[i] * chart1.ChartAreas["ChartArea" + (i + 1)].AxisY.Maximum).ToString();
                //                else if (AlertList[i].AlertReportMode == AlertVar.enumAlertReportMode.Absolute) 
                //                    chart1.Series[2].Points[i].Label = yValuesNow[i].ToString();
                //                else
                //                    chart1.Series[2].Points[i].Label = ((yValuesNow[i] * 100) + "%");
                //            }

                //            if (chart1.ChartAreas[0].AxisY.IntervalOffset == 1 && AlertList[0].AlertReportMode == AlertVar.enumAlertReportMode.Absolute)
                //            chart1.ChartAreas[0].AxisY.IntervalOffset = getMax(getMaxValue);
                //            if (chart1.ChartAreas.Count != 1)
                //            {
                //                int count = chart1.ChartAreas.Count;
                //                for (int i = 1; i < count; i++)
                //                {
                //                    chart1.ChartAreas.Remove(chart1.ChartAreas[1]);
                //                }
                //            }
                //            if (chart1.Series.Count != 3) 
                //            {
                //                for (int i = chart1.Series.Count - 1; i > 2; i--) 
                //                {
                //                    chart1.Series.RemoveAt(i);
                //                }
                //            }
                //        }
                //    }                  
                //}
                //catch { throw new Exception("chart1_Paint"); }
                #endregion
                chart1.Printing.PrintPaint(graphics, position);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "统计图打印信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public object Clone()
        {
            RadarAlertChart pcc = new RadarAlertChart();
            if (this.SourceField != null)
                pcc.SourceField = this.SourceField.Clone();

            pcc.Apperence = this.Apperence.Clone();
            if (this.Apperence.SeriesList.Count != 0)
            {
                pcc.Apperence.SeriesList[0].Legend = this.Apperence.SeriesList[0].Legend;
                pcc.Apperence.SeriesList[1].Legend = this.Apperence.SeriesList[1].Legend;
                pcc.Apperence.SeriesList[2].Legend = this.Apperence.SeriesList[2].Legend;
            }
            if (this.AlertList != null)
            {
                pcc.AlertList = this.AlertList;


            }
            pcc.RunMode = this.RunMode;
            pcc.Location = new Point(this.Location.X, this.Location.Y);
            pcc.OriginPosition = new Point(this.Location.X, this.Location.Y);
            pcc.Height = this.Height;
            pcc.Width = this.Width;
            if (this.OriginHeight > 0 || this.OriginWidth > 0)
            {
                pcc.OriginHeight = this.OriginHeight;
                pcc.OriginWidth = this.OriginWidth;
            }
            else
            {
                pcc.OriginWidth = this.Width;
                pcc.OriginHeight = this.Height;
            }

            return pcc;
        }

        public float HorizontalScale { get; set; }
        public float VerticalScale { get; set; }
        public void Zoom(float hScale, float vScale)
        {
            if (this.OriginHeight > 0 || this.OriginWidth > 0)
            {
            }
            else
            {
                this.OriginWidth = this.Width;
                this.OriginHeight = this.Height;
            }
            if ((this.OriginPosition.X == 0 && this.OriginPosition.Y == 0) && (this.Location != new Point()))
            {
                this.OriginPosition = this.Location;
            }
            this.Location = new Point((int)(this.OriginPosition.X * vScale), (int)(this.OriginPosition.Y * hScale));
            this.Width = (int)(this.OriginWidth * vScale);
            this.Height = (int)(this.OriginHeight * hScale);
        }
        public void Zoom()
        {
        }

        public SuspensionItem[] ListSuspensionItems()
        {
            SuspensionItem[] result = new SuspensionItem[2];
            result[0] = new SuspensionItem(Properties.Resources.Data, Properties.Resources.ResourceManager.GetString("context0021"), Properties.Resources.ResourceManager.GetString("context0021"), new Action(DealWithDataTable));
            result[1] = new SuspensionItem(Properties.Resources.OPEN, Properties.Resources.ResourceManager.GetString("context0022"), Properties.Resources.ResourceManager.GetString("context0022"), new Action(DealWithApperence));
            return result;
        }
        private void DealWithDataTable()
        {
            if (null != this)
            {
                RadarAlertChart element = this as RadarAlertChart;
                if (null != element)
                {
                    if (element.Parent == null ||
                       (element.Parent != null && element.Parent as IPmsReportDataBind == null))
                    {
                        PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField sfAll = (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetCurrentReportDataDefine()) as PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField;
                        //SourceBindDialog fbd = new SourceBindDialog(sfAll, element.SourceField);
                        PMS.Libraries.ToolControls.Report.Controls.EditorDialog.SourceBindDialog fbd = new PMS.Libraries.ToolControls.Report.Controls.EditorDialog.SourceBindDialog(sfAll, element.SourceField, true);
                        if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                             SourceField = fbd.SourceField;
                            if (null != Site)
                            {
                                IComponentChangeService cs = Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                                if (null != cs)
                                {
                                    cs.OnComponentChanged(this, null, null, null);
                                }
                            }
                        }
                    }
                    else
                    {
                        PMS.Libraries.ToolControls.Report.Controls.EditorDialog.SourceBindDialog fbd = new PMS.Libraries.ToolControls.Report.Controls.EditorDialog.SourceBindDialog(GetSourceField(element.Parent as IElement), element.SourceField, true);
                        if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            SourceField = fbd.SourceField;
                            if (null != Site)
                            {
                                IComponentChangeService cs = Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                                if (null != cs)
                                {
                                    cs.OnComponentChanged(this, null, null, null);
                                }
                            }
                        }
                    }
                }
            }
        }
        private void DealWithApperence()
        {

            if (this != null)
            {
                RadarAlertChart control = null;
                if (this is RadarAlertChart)
                    control = this as RadarAlertChart;

                RadarAlertApp form1 = new RadarAlertApp();
                
                form1.ChartParent = control as RadarAlertChart;
                DataSource dS = control.Apperence.Clone();
                if (control.Apperence.SeriesList.Count != 0)
                {
                    dS.SeriesList[0].Legend = control.Apperence.SeriesList[0].Legend;
                    dS.SeriesList[1].Legend = control.Apperence.SeriesList[1].Legend;
                    dS.SeriesList[2].Legend = control.Apperence.SeriesList[2].Legend;
                }
                form1.TitleList = dS.TitleList;
                form1.SeriesList = dS.SeriesList;
                form1.AlertList = dS.alertList;
                form1.AllAlertList = dS.allAlertList;
                form1.LegendList = dS.LegendList;
                if (dS.SeriesList.Count != 0)
                {
                    form1.SeriesList[0].Legend = dS.SeriesList[0].Legend;
                    form1.SeriesList[1].Legend = dS.SeriesList[1].Legend;
                    form1.SeriesList[2].Legend = dS.SeriesList[2].Legend;
                }
                DialogResult dr = form1.ShowDialog();
                if (DialogResult.OK == dr)
                {
                    control.Apperence.LegendList = form1.LegendList;
                    control.Apperence.TitleList = form1.TitleList;
                    control.Apperence.SeriesList = form1.SeriesList;
                    control.Apperence.alertList = form1.AlertList;
                    control.Apperence.allAlertList = form1.AllAlertList;
                    control.Apperence.ChartAreaList = form1.ChartParent.Apperence.ChartAreaList;
                    DataSource ds = control.Apperence.Clone() as DataSource;
                    ds.SeriesList[0].Legend = form1.SeriesList[0].Legend;
                    ds.SeriesList[1].Legend = form1.SeriesList[1].Legend;
                    ds.SeriesList[2].Legend = form1.SeriesList[2].Legend;


                    if (null != Site)
                    {
                        IComponentChangeService cs = Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                        if (null != cs)
                        {
                            cs.OnComponentChanged(this, null, null, null);
                        }
                    }
                }
                else if (DialogResult.Cancel == dr) 
                {
                    if (form1.isApply) 
                    {
                        Apperence = form1.ds;
                        if (null != Site)
                        {
                            IComponentChangeService cs = Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                            if (null != cs)
                            {
                                cs.OnComponentChanged(this, null, null, null);
                            }
                        }
                    }
                    //else
                    //    Apperence = dS;
                }

            }
        }
        private SourceField GetSourceField(IElement element)
        {
            if (null == element)
            {
                return null;
            }
            IPmsReportDataBind parent = element as IPmsReportDataBind;
            if (null == parent)
            {
                return null;
            }
            if (null == parent.SourceField)
            {
                return GetSourceField(element.Parent as IElement);
            }

            return parent.SourceField;
        }

        public IControlTranslator ToElement(bool transferChild)
        {
            ChartRadarSerializerClass result = new ChartRadarSerializerClass();
            result.Location = this.OriginPosition;
            //result.Location = this.Location;
            if (this.OriginHeight > 0 || this.OriginWidth > 0)
            {
                result.Width = this.OriginWidth;
                result.Height = this.OriginHeight;
            }
            else
            {
                result.Width = this.Width;
                result.Height = this.Height;
            }
            MemoryStream temp = new MemoryStream();
            this.chart1.Serializer.Save(temp);
            result.Context = temp.ToArray();
            temp.Dispose();
            return result;
        }

        public MESVarType MESType
        {
            get;
            set;
        }
        public bool HasRightBorder
        {
            get;
            set;
        }
        [Browsable(false)]
        public ExtendObject ExtendObject { get; set; }
        public bool HasLeftBorder { get; set; }
        public bool HasTopBorder { get; set; }
        public bool HasBottomBorder { get; set; }
        public string BorderName { get; set; }
        public bool CanInvalidate { get; set; }
        public List<ExternData> ExternDatas { get; set; }
        public float MoveX { get; set; }
        public float MoveY { get; set; }
        public ElementBorder Border { get; set; }
        public bool HasBorder { get; set; }
        IElement IElement.Parent
        {
            get
            {
                return Parent as IElement;
            }
            set
            {
                Parent = value as Control;
            }
        }
        #endregion


        /// <summary>
        /// 图表控件的Load事件，在该方法中整合数据并对各图表区域初始化
        /// </summary>
        private void RadarAlertChart_Load(object sender, EventArgs e)
        {
            if (_Apperence == null)
            {
                _Apperence = new DataSource(null);
                InitailColumnData();
            }

        }

        /// <summary>
        /// 对指定序列进行求和
        /// </summary>
        double getSum(List<double> getVarFun)
        {
            double sum = 0;
            for (int i = 0; i < getVarFun.Count; i++)
            {
                sum += getVarFun[i];
            }
            return sum;
        }

        /// <summary>
        /// 对指定序列进行求最大值
        /// </summary>
        double getMax(List<double> getVarFun)
        {
            double maxValue = getVarFun[0];
            for (int i = 0; i < getVarFun.Count; i++) 
            {
                maxValue = Math.Max(maxValue, getVarFun[i]);
            }
                return maxValue;
        }

        /// <summary>
        /// 对指定序列进行求最小值
        /// </summary>
        double getMin(List<double> getVarFun)
        {
            double minValue = getVarFun[0];
            for (int i = 0; i < getVarFun.Count; i++)
            {
                minValue = Math.Min(minValue, getVarFun[i]);
            }
            return minValue;
        }

        /// <summary>
        /// 对指定序列进行求平均值
        /// </summary>
        double getAverage(List<double> getVarFun)
        {
            double sum = 0;
            for (int i = 0; i < getVarFun.Count; i++)
            {
                sum += getVarFun[i];
            }
            return sum / getVarFun.Count;
        }


        #region 2011.11.29 属性编辑器：对应雷达图
        internal class ApperenceEditor : UITypeEditor
        {
            public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            {
                if (context != null && context.Instance != null)
                {
                    return UITypeEditorEditStyle.Modal;
                }

                return base.GetEditStyle(context);
            }

            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                IWindowsFormsEditorService editorService = null;

                if (context != null && context.Instance != null && provider != null)
                {
                    editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                    if (editorService != null)
                    {
                        RadarAlertChart control = null;
                        if (context.Instance is RadarAlertChart)
                            control = context.Instance as RadarAlertChart;

                        RadarAlertApp form1 = new RadarAlertApp();
                        form1.ChartParent = control as RadarAlertChart;
                        DataSource dS = control.Apperence.Clone() as DataSource;
                        if (control.Apperence.SeriesList.Count != 0)
                        {
                            dS.SeriesList[0].Legend = control.Apperence.SeriesList[0].Legend;
                            dS.SeriesList[1].Legend = control.Apperence.SeriesList[1].Legend;
                            dS.SeriesList[2].Legend = control.Apperence.SeriesList[2].Legend;
                        }
                        form1.TitleList = dS.TitleList;
                        form1.SeriesList = dS.SeriesList;
                        form1.AlertList = dS.alertList;
                        form1.AllAlertList = dS.allAlertList;
                        form1.LegendList = dS.LegendList;
                        if (dS.SeriesList.Count != 0)
                        {
                            form1.SeriesList[0].Legend = dS.SeriesList[0].Legend;
                            form1.SeriesList[1].Legend = dS.SeriesList[1].Legend;
                            form1.SeriesList[2].Legend = dS.SeriesList[2].Legend;
                        }
                        DialogResult dr = editorService.ShowDialog(form1);
                        if (DialogResult.OK == dr)
                        {
                            control.Apperence.LegendList = form1.LegendList;
                            control.Apperence.TitleList = form1.TitleList;
                            control.Apperence.SeriesList = form1.SeriesList;
                            control.Apperence.alertList = form1.AlertList;
                            control.Apperence.allAlertList = form1.AllAlertList;
                            control.Apperence.ChartAreaList = form1.ChartParent.Apperence.ChartAreaList;
                            DataSource ds = control.Apperence.Clone() as DataSource;
                            ds.SeriesList[0].Legend = form1.SeriesList[0].Legend;
                            ds.SeriesList[1].Legend = form1.SeriesList[1].Legend;
                            ds.SeriesList[2].Legend = form1.SeriesList[2].Legend;
                            value = ds;
                        }
                        else if (DialogResult.Cancel == dr)
                        {
                            if (form1.isApply)
                            {

                                value = form1.ds;
                            }

                        }

                        return value;
                    }
                }


                return value;
            }
        }
        internal class DataTableEditor : UITypeEditor
        {
            public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                try
                {
                    if (null != context && null != context.Instance && null != context.Container)
                    {
                        RadarAlertChart element = context.Instance as RadarAlertChart;
                        if (null != element)
                        {
                            PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField sfAll = (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetCurrentReportDataDefine()) as PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField;
                            //SourceBindDialog fbd = new SourceBindDialog(sfAll, element.SourceField);
                            PMS.Libraries.ToolControls.Report.Controls.EditorDialog.SourceBindDialog fbd = new PMS.Libraries.ToolControls.Report.Controls.EditorDialog.SourceBindDialog(sfAll, element.SourceField, true);
                            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                value = fbd.SourceField;
                            }
                        }
                        return value;
                    }
                    return base.EditValue(context, provider, value);
                }
                catch { throw new Exception("DataTableEditor"); }
            }

            public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
            {
                if (null != context && null != context.Instance && null != context.Container)
                {

                    return UITypeEditorEditStyle.Modal;
                }

                return base.GetEditStyle(context);
            }
        }
        #endregion





    }
}
