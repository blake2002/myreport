using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MES.Controls.Design;
using MES.Report;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using PMS.Libraries.ToolControls.Report.Element;
using PMS.Libraries.ToolControls.Report.Elements.Util;
using System.ComponentModel.Design;
using PMS.Libraries.ToolControls.Report.Controls.EditorDialog;
using System.Collections;
using PMS.Libraries.ToolControls.PMSPublicInfo;

namespace PMS.Libraries.ToolControls.PMSChart
{
    [ToolboxBitmap(typeof(NewRadarChart), "Resources.RadarChart.png")]
    [DisplayName("RadarChart")]
    [Designer(typeof(MESDesigner))]
    [DefaultProperty("SourceField")]
    public partial class NewRadarChart : ChartBase
    {
        #region Public Property
        [Category("通用")]
        [Description("控件名字")]
        [Browsable(true)]
        public new string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }

        [Category("通用")]
        [Description("控件位置")]
        [Browsable(true)]
        public new Point Location
        {
            get { return base.Location; }
            set { base.Location = value; }
        }

        [Category("通用")]
        [Description("控件大小")]
        [Browsable(true)]
        public new Size Size
        {
            get { return base.Size; }
            set { base.Size = value; }
        }

        #region 字段和属性
        [Category("通用")]
        [Description("外观设置")]
        [Editor(typeof(RadarApperenceEditor), typeof(UITypeEditor))]
        public new DataSource Apperence
        {
            get { return base.Apperence; }
            set { base.Apperence = value; }
        }

        [Category("通用")]
        [Description("绑定数据")]
        [DisplayName("Binding")]
        public new SourceField SourceField
        {
            get { return base.SourceField; }
            set { base.SourceField = value; }
        }

        private RadarChartType _ChartType = RadarChartType.Radar;
        [Category("通用")]
        [Description("图表类型")]
        [Browsable(false)]
        public RadarChartType ChartType
        {
            get
            {
                return _ChartType;
            }
            set
            {
                _ChartType = value;
                InitailColumnData();
            }
        }

        private List<RadarClassify> _RadarClassifyList = new List<RadarClassify>();
        [Category("通用")]
        [Description("分类")]
        [Browsable(false)]
        public List<RadarClassify> RadarClassifyList
        {
            get
            {
                return _RadarClassifyList;
            }
            set
            {
                _RadarClassifyList = value;
            }
        }

        private RadarSeriesColor _RadarSeriesColor;
        [Category("通用")]
        [Description("名称和颜色")]
        [Browsable(false)]
        public RadarSeriesColor RadarSeriesColor
        {
            get
            {
                return _RadarSeriesColor;
            }
            set
            {
                _RadarSeriesColor = value;
                InitailColumnData();
            }
        }
        #endregion
        #endregion

        public NewRadarChart()
            : base()
        {
            InitializeComponent();
        }

        public NewRadarChart(MemoryStream Aim)
            : base(Aim)
        {
        }

        public override object Clone()
        {
            NewRadarChart pcc = new NewRadarChart();
            if (this.SourceField != null)
                pcc.SourceField = this.SourceField.Clone();

            pcc.Apperence = this.Apperence.Clone();
            if (this.Apperence.SeriesList.Count != 0)
            {
                pcc.Apperence.SeriesList[0].Legend = this.Apperence.SeriesList[0].Legend;
            }

            pcc.RunMode = this.RunMode;
            pcc.Location = new Point(this.Location.X, this.Location.Y);
            pcc.OriginPosition = new Point(this.Location.X, this.Location.Y);
            pcc.Height = this.Height;
            pcc.Width = this.Width;
            pcc.ChartType = this.ChartType;
            if (this.RadarSeriesColor != null)
                pcc.RadarSeriesColor = this.RadarSeriesColor.Clone();
            for (int i = 0; i < RadarClassifyList.Count; i++)
            {
                pcc.RadarClassifyList.Add(this.RadarClassifyList[i].Clone());
            }
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

        protected override void DrawVirtualChart()
        {
            try
            {
                InitChart();
                ChartArea ChartArea1 = new ChartArea("ChartArea1");
                Legend legend1 = new Legend();
                if (this.Apperence != null)
                {
                    //绑定绘图区
                    if (this.Apperence.ChartAreaList.Count != 0)
                    {
                        this.Apperence.ChartAreaList[0].SetChartArea(ChartArea1);
                    }
                    //绑定标题
                    if (this.Apperence.TitleList.Count != 0)
                    {
                        foreach (PMSTitle item in this.Apperence.TitleList)
                        {
                            Title title1 = new Title();
                            item.SetTitle(title1);
                            chart1.Titles.Add(title1);
                        }
                    }
                    //绑定图例
                    if (this.Apperence.LegendList.Count != 0)
                    {
                        this.Apperence.LegendList[0].SetLegend(legend1);
                    }

                    //绑定数据
                    string[] Xvalues = { "Australia", "Brasil", "Canada", "Danmark", "Ecuador" };
                    Random random = new Random();
                    for (int i = 0; i < 3; i++)
                    {
                        Series series1 = new Series();
                        if (this.Apperence.SeriesList.Count != 0)
                            this.Apperence.SeriesList[0].SetSeriseStyle(series1);

                        double[] Yvalues = new double[5];
                        for (int j = 0; j < 5; j++)
                        {
                            Yvalues[j] = (50.0 * random.Next(1000 * (3 - 1 - i) / 3, 1000 * (3 - i) / 3) / 1000.0);
                        }
                        series1.ChartType = (SeriesChartType)ChartType;
                        series1.Points.DataBindXY(Xvalues, Yvalues);
                        chart1.Series.Add(series1);
                    }
                    if (this.RadarSeriesColor != null)
                    {
                        chart1.Series[0].Name = RadarSeriesColor.ClassifyMaxName;
                        chart1.Series[0].Color = RadarSeriesColor.ClassifyMaxColor;
                        chart1.Series[1].Name = RadarSeriesColor.ClassifyMainName;
                        chart1.Series[1].Color = RadarSeriesColor.ClassifyMainColor;
                        chart1.Series[2].Name = RadarSeriesColor.ClassifyMinName;
                        chart1.Series[2].Color = RadarSeriesColor.ClassifyMinColor;
                    }

                    //绑定图表区的边框和背景
                    chart1.BorderlineColor = this.Apperence.PMSChartAppearance.BorderlineColor;
                    chart1.BorderlineDashStyle = this.Apperence.PMSChartAppearance.BorderlineDashStyle;
                    chart1.BorderlineWidth = this.Apperence.PMSChartAppearance.BorderlineWidth;
                    chart1.BackColor = this.Apperence.PMSChartAppearance.BackColor;
                    chart1.BackSecondaryColor = this.Apperence.PMSChartAppearance.BackSecondaryColor;
                    chart1.BackHatchStyle = this.Apperence.PMSChartAppearance.BackHatchStyle;
                    chart1.BackGradientStyle = this.Apperence.PMSChartAppearance.BackGradientStyle;
                }
                chart1.ChartAreas.Add(ChartArea1);
                chart1.Legends.Add(legend1);
            }
            catch { throw new Exception("DrawVirtualChart"); }
        }

        protected override void PrintPaint(Graphics graphics, Rectangle position)
        {
            try
            {
                chart1.Printing.PrintPaint(graphics, position);
            }
            catch (Exception ex)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(ex.Message);
            }
        }

        public override int BindDataTableManager(IDataTableManager dtm, string bindPath)
        {
            _dtm = dtm;
            _bindPath = bindPath;
            if (SourceField == null)
                SetClassifyData();
            else
            {
                string strTableName = SourceField.Name;
                if (!string.IsNullOrEmpty(bindPath))
                {
                    strTableName = string.Format("{0}.{1}", bindPath, SourceField.Name);
                }
                DataTable dt = dtm.GetDataTable(strTableName);
                SetData(dt, 1);
            }
            return 1;
        }

        protected override void SetData(DataTable Aim, int Index)
        {
            SetClassifyData();
        }

        /// <summary>
        /// 采用分类方式配置数据源
        /// </summary>
        /// <param name="Aim">数据源</param>
        private void SetClassifyData()
        {
            OriginPosition = new Point(this.Location.X, this.Location.Y);
            if (this.DesignMode == true)
            {
                DrawVirtualChart();
            }
            else
            {
                try
                {
                    if (this.RadarClassifyList.Count == 0)
                        return;
                    ChartArea ChartArea1 = new ChartArea("ChartArea1");
                    Legend legend1 = new Legend();
                    InitChart();
                    //绑定绘图区
                    if (this.Apperence.ChartAreaList.Count != 0)
                    {
                        this.Apperence.ChartAreaList[0].SetChartArea(ChartArea1);
                    }
                    //绑定标题
                    if (this.Apperence.TitleList.Count != 0)
                    {
                        foreach (PMSTitle item in this.Apperence.TitleList)
                        {
                            Title title1 = new Title();
                            item.SetTitle(title1);
                            chart1.Titles.Add(title1);
                        }
                    }
                    //绑定图例
                    if (this.Apperence.LegendList.Count != 0)
                    {
                        this.Apperence.LegendList[0].SetLegend(legend1);
                    }
                    //绑定数据
                    if (this.RadarClassifyList.Count != 0)
                    {
                        Series seriesMain = new Series("实际值");
                        Series seriesMax = new Series("上限");
                        Series seriesMin = new Series("下限");
                        if (this.Apperence.SeriesList.Count != 0)
                        {
                            this.Apperence.SeriesList[0].SetSeriseStyle(seriesMain);
                            this.Apperence.SeriesList[0].SetSeriseStyle(seriesMax);
                            this.Apperence.SeriesList[0].SetSeriseStyle(seriesMin);
                        }
                        foreach (RadarClassify item in this.RadarClassifyList)
                        {
                            AddSeriesByClassify(seriesMain, item, item.ClassifyLabel, item.ClassifyValue);
                            AddSeriesByClassify(seriesMax, item, item.ClassifyLabel, item.ClassifyMaxValue);
                            AddSeriesByClassify(seriesMin, item, item.ClassifyLabel, item.ClassifyMinValue);
                        }
                        chart1.Series.Add(seriesMax);
                        chart1.Series.Add(seriesMain);
                        chart1.Series.Add(seriesMin);
                    }
                    if (this.RadarSeriesColor != null)
                    {
                        chart1.Series[0].Name = RadarSeriesColor.ClassifyMaxName;
                        chart1.Series[0].Color = RadarSeriesColor.ClassifyMaxColor;
                        chart1.Series[1].Name = RadarSeriesColor.ClassifyMainName;
                        chart1.Series[1].Color = RadarSeriesColor.ClassifyMainColor;
                        chart1.Series[2].Name = RadarSeriesColor.ClassifyMinName;
                        chart1.Series[2].Color = RadarSeriesColor.ClassifyMinColor;
                    }
                    chart1.ChartAreas.Add(ChartArea1);
                    chart1.Legends.Add(legend1);
                    if (this.Apperence.PMSChartAppearance != null)
                    {
                        chart1.BorderlineColor = this.Apperence.PMSChartAppearance.BorderlineColor;
                        chart1.BorderlineDashStyle = this.Apperence.PMSChartAppearance.BorderlineDashStyle;
                        chart1.BorderlineWidth = this.Apperence.PMSChartAppearance.BorderlineWidth;
                        chart1.BackColor = this.Apperence.PMSChartAppearance.BackColor;
                        chart1.BackSecondaryColor = this.Apperence.PMSChartAppearance.BackSecondaryColor;
                        chart1.BackHatchStyle = this.Apperence.PMSChartAppearance.BackHatchStyle;
                        chart1.BackGradientStyle = this.Apperence.PMSChartAppearance.BackGradientStyle;
                    }
                }
                catch (System.Exception ex)
                {
                    InitChart();
                    PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(ex.Message);
                }
            }
        }

        private void AddSeriesByClassify(Series series, RadarClassify item, string classifyLabel, string classifyValue)
        {
            if (!item.Enable)
                return;
            string bindPath = _bindPath;
            if (SourceField.CustomMode)
            {
                bindPath = (this as IBindReportExpressionEngine).ExpressionEngine.GetExpressionPath(SourceField.CustomTablePath, bindPath, _dtm);
            }
            else
            {
                if (!string.IsNullOrEmpty(_bindPath))
                {
                    if (this.SourceField != null)
                        bindPath = _bindPath + "." + this.SourceField.Name;
                }
                else
                {
                    if (this.SourceField != null)
                        bindPath = this.SourceField.Name;
                }
            }
            string value = ExpressionExecute(classifyValue, "", _dtm, bindPath);
            double yvalue;
            if (!string.IsNullOrEmpty(classifyLabel) && !string.IsNullOrEmpty(value) && Double.TryParse(value, out yvalue))
            {
                series.Points.AddXY(classifyLabel, yvalue);
            }
            series.ChartType = (SeriesChartType)ChartType;
        }

        public override SuspensionItem[] ListSuspensionItems()
        {
            SuspensionItem[] result = new SuspensionItem[4];
            result[0] = new SuspensionItem(Properties.Resources.Data, Properties.Resources.ResourceManager.GetString("context0021"), Properties.Resources.ResourceManager.GetString("context0021"), new Action(DealWithDataTable));
            result[1] = new SuspensionItem(Properties.Resources.OPEN, Properties.Resources.ResourceManager.GetString("context0022"), Properties.Resources.ResourceManager.GetString("context0022"), new Action(DealWithApperence));
            //result[2] = new SuspensionItem(Properties.Resources.chart_type_radar, "雷达图", "雷达图", new Action(ChangeToRadar));
            //result[3] = new SuspensionItem(Properties.Resources.chart_type_polar, "极坐标图", "极坐标图", new Action(ChangeToPolar));
            return result;
        }

        protected override void DealWithApperence()
        {
            if (this != null)
            {
                RadarApperenceFrm form1 = new RadarApperenceFrm();

                form1.ChartParent = this;
                DataSource ds = this.Apperence.Clone();
                form1.PMSChartAppearance = this.Apperence.PMSChartAppearance;
                form1.chartAreaList = ds.ChartAreaList;
                form1.legendList = ds.LegendList;
                form1.seriesList = ds.SeriesList;
                form1.titleList = ds.TitleList;
                if (DialogResult.OK == form1.ShowDialog())
                {
                    isIntial = true;
                    InitailColumnData();
                }
            }
        }

        private void ChangeToRadar()
        {
            if (ChartType != RadarChartType.Radar)
                ChartType = RadarChartType.Radar;
        }
        private void ChangeToPolar()
        {
            if (ChartType != RadarChartType.Polar)
                ChartType = RadarChartType.Polar;
        }
    }

    public enum RadarChartType
    {
        //
        // 摘要:
        //     雷达图类型。
        Radar = 25,
        //
        // 摘要:
        //     极坐标图类型。
        Polar = 26,
    }

}
