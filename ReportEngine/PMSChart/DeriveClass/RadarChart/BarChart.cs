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
    [ToolboxBitmap(typeof(BarChart), "Resources.BarChart.png")]
    [Designer(typeof(MESDesigner))]
    [DefaultProperty("SourceField")]
    public partial class BarChart : ChartBase
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
        [Editor(typeof(BarApperenceEditor), typeof(UITypeEditor))]
        public new DataSource Apperence
        {
            get { return base.Apperence; }
            set { base.Apperence = value; }
        }

        [Category("通用")]
        [Description("分组")]
        [Browsable(false)]
        public new GroupSource GroupSource
        {
            get { return base.GroupSource; }
            set { base.GroupSource = value; }
        }

        [Category("通用")]
        [Description("绑定数据")]
        [DisplayName("Binding")]
        public new SourceField SourceField
        {
            get { return base.SourceField; }
            set { base.SourceField = value; }
        }

        private BarChartType _ChartType = BarChartType.Column;
        [Category("通用")]
        [Description("图表类型")]
        public BarChartType ChartType
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
        #endregion
        #endregion

        public BarChart()
            : base()
        {
            InitializeComponent();
        }

        public BarChart(MemoryStream Aim)
            : base(Aim)
        {
        }

        public override object Clone()
        {
            BarChart pcc = new BarChart();
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
            pcc.GroupSource = this.GroupSource.Clone();
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
                        series1.SetCustomProperty("BarLabelStyle", "Disabled");
                        if (this.Apperence.SeriesList.Count != 0)
                            this.Apperence.SeriesList[0].SetSeriseStyle(series1);

                        double[] Yvalues = new double[5];
                        for (int j = 0; j < 5; j++)
                        {
                            Yvalues[j] = (50.0 * random.Next(0, 1000) / 1000.0);
                        }
                        series1.ChartType = (SeriesChartType)ChartType;
                        series1.Points.DataBindXY(Xvalues, Yvalues);
                        chart1.Series.Add(series1);
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

        protected override void SetData(DataTable Aim, int Index)
        {
            if (GroupSource.Enable)
                SetGroupData(Aim);
            else
                SetUnGroupData(Aim);
        }

        /// <summary>
        /// 采用自定义方式配置数据源
        /// </summary>
        /// <param name="Aim">数据源</param>
        private void SetUnGroupData(DataTable Aim)
        {
            OriginPosition = new Point(this.Location.X, this.Location.Y);
            if (this.DesignMode == true)
            {
                DrawVirtualChart();
            }
            else if (Aim == null || Aim.Rows.Count == 0)
            {
                DrawVirtualChart();
            }
            else
            {
                try
                {
                    ChartArea ChartArea1 = new ChartArea("ChartArea1");
                    Legend legend1 = new Legend();
                    List<string> Xvalues = new List<string>();
                    List<double> Yvalues = new List<double>();
                    if (this.Apperence != null && this.Apperence.SeriesList != null && this.Apperence.SeriesList.Count > 0)
                    {
                        Series series1 = new Series();
                        //是否绑定数据源，绑定的数据源是否存在
                        this.Apperence.SeriesList[0].SetSeriesValue(series1);
                        if (string.IsNullOrEmpty(series1.GetCustomProperty("XBindingField")))
                        {
                            PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(Properties.Resources.ResourceManager.GetString("message0016"));
                            return;
                        }
                        if (!Aim.Columns.Contains(series1.GetCustomProperty("XBindingField")))
                        {
                            PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(Properties.Resources.ResourceManager.GetString("message0017"));
                            return;
                        }
                    }

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
                    if (this.Apperence.SeriesList.Count != 0)
                    {
                        foreach (PMSSeries item in this.Apperence.SeriesList)
                        {
                            Series series1 = new Series();
                            item.SetSeriesValue(series1);
                            for (int i = 0; i < Aim.Rows.Count; i++)
                            {
                                Xvalues.Add(Aim.Rows[i][series1.GetCustomProperty("XBindingField")].ToString());
                                Yvalues.Add(Convert.ToDouble(Aim.Rows[i][series1.GetCustomProperty("YBindingField")]));
                            }

                            if (Xvalues.Count != 0 && Yvalues.Count != 0)
                            {
                                series1.Points.DataBindXY(Xvalues, Yvalues);
                            }
                            series1.ChartType = (SeriesChartType)ChartType;
                            chart1.Series.Add(series1);
                        }
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

        /// <summary>
        /// 采用分组方式配置数据源
        /// </summary>
        /// <param name="Aim">数据源</param>
        private void SetGroupData(DataTable Aim)
        {
            if (this.Apperence != null && this.Apperence.SeriesList != null && this.Apperence.SeriesList.Count > 0)
            {
                //是否绑定数据源，绑定的数据源是否存在
                if (string.IsNullOrEmpty(GroupSource.MajorBinding))
                {
                    PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error("未绑定分组-主分类字段！");
                    return;
                }
                if (string.IsNullOrEmpty(GroupSource.ValueBinding))
                {
                    PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error("未绑定分组-统计字段！");
                    return;
                }
            }
            ArrayList columnList = new ArrayList();
            if (!Aim.Columns.Contains(GroupSource.MajorBinding))
            {
                if (GroupSource.MajorBinding.EndsWith("_Year"))
                {
                    CreateSplitDateFiled(Aim, GroupSource.MajorBinding, columnList, "Year");
                }
                else if (GroupSource.MajorBinding.EndsWith("Month"))
                {
                    CreateSplitDateFiled(Aim, GroupSource.MajorBinding, columnList, "Month");
                }
                else if (GroupSource.MajorBinding.EndsWith("Day"))
                {
                    CreateSplitDateFiled(Aim, GroupSource.MajorBinding, columnList, "Day");
                }
                else if (GroupSource.MajorBinding.EndsWith("Hour"))
                {
                    CreateSplitDateFiled(Aim, GroupSource.MajorBinding, columnList, "Hour");
                }
                else if (GroupSource.MajorBinding.EndsWith("Minute"))
                {
                    CreateSplitDateFiled(Aim, GroupSource.MajorBinding, columnList, "Minute");
                }
                else if (GroupSource.MajorBinding.EndsWith("Second"))
                {
                    CreateSplitDateFiled(Aim, GroupSource.MajorBinding, columnList, "Second");
                }
                else
                    return;
            }

            string strValueFx;
            switch (GroupSource.ValueFx)
            {
                case Functions.Count:
                    strValueFx = "count";
                    break;
                case Functions.Sum:
                    strValueFx = "sum";
                    break;
                case Functions.Avg:
                    strValueFx = "avg";
                    break;
                case Functions.Max:
                    strValueFx = "max";
                    break;
                case Functions.Min:
                    strValueFx = "min";
                    break;
                default:
                    strValueFx = "";
                    break;
            }

            DataSetHelper ds = new DataSetHelper();
            List<DataTable> dtList = new List<DataTable>();
            DataTable tbMajor = Aim.DefaultView.ToTable(true, GroupSource.MajorBinding);
            if (GroupSource.MajorSort == SortType.Asc)
            {
                tbMajor.DefaultView.Sort = GroupSource.MajorBinding + " Asc";
                tbMajor = tbMajor.DefaultView.ToTable();
            }
            if (GroupSource.MajorSort == SortType.Desc)
            {
                tbMajor.DefaultView.Sort = GroupSource.MajorBinding + " Desc";
                tbMajor = tbMajor.DefaultView.ToTable();
            }
            if (!string.IsNullOrEmpty(GroupSource.MinorBinding))
            {
                if (!Aim.Columns.Contains(GroupSource.MinorBinding))
                {
                    if (GroupSource.MinorBinding.EndsWith("_Year"))
                    {
                        CreateSplitDateFiled(Aim, GroupSource.MinorBinding, columnList, "Year");
                    }
                    else if (GroupSource.MinorBinding.EndsWith("Month"))
                    {
                        CreateSplitDateFiled(Aim, GroupSource.MinorBinding, columnList, "Month");
                    }
                    else if (GroupSource.MinorBinding.EndsWith("Day"))
                    {
                        CreateSplitDateFiled(Aim, GroupSource.MinorBinding, columnList, "Day");
                    }
                    else if (GroupSource.MinorBinding.EndsWith("Hour"))
                    {
                        CreateSplitDateFiled(Aim, GroupSource.MinorBinding, columnList, "Hour");
                    }
                    else if (GroupSource.MinorBinding.EndsWith("Minute"))
                    {
                        CreateSplitDateFiled(Aim, GroupSource.MinorBinding, columnList, "Minute");
                    }
                    else if (GroupSource.MinorBinding.EndsWith("Second"))
                    {
                        CreateSplitDateFiled(Aim, GroupSource.MinorBinding, columnList, "Second");
                    }
                    else
                        return;
                }

                DataTable tbMinor = Aim.DefaultView.ToTable(true, GroupSource.MinorBinding);
                if (GroupSource.MinorSort == SortType.Asc)
                {
                    tbMinor.DefaultView.Sort = GroupSource.MinorBinding + " Asc";
                    tbMinor = tbMinor.DefaultView.ToTable();
                }
                if (GroupSource.MinorSort == SortType.Desc)
                {
                    tbMinor.DefaultView.Sort = GroupSource.MinorBinding + " Desc";
                    tbMinor = tbMinor.DefaultView.ToTable();
                }

                foreach (DataRow row in tbMinor.Rows)
                {
                    if (row[0] != DBNull.Value)
                        dtList.Add(ds.SelectGroupByInto(row[0].ToString(), Aim, GroupSource.MajorBinding + "," + strValueFx + "(" + GroupSource.ValueBinding + ")", GroupSource.MinorBinding + "='" + row[0].ToString() + "'", GroupSource.MajorBinding));
                    else
                        dtList.Add(ds.SelectGroupByInto(row[0].ToString(), Aim, GroupSource.MajorBinding + "," + strValueFx + "(" + GroupSource.ValueBinding + ")", GroupSource.MinorBinding + " is null ", GroupSource.MajorBinding));
                }
            }
            else
                dtList.Add(ds.SelectGroupByInto(Aim.TableName, Aim, GroupSource.MajorBinding + "," + strValueFx + "(" + GroupSource.ValueBinding + ")", "1=1", GroupSource.MajorBinding));


            OriginPosition = new Point(this.Location.X, this.Location.Y);
            if (this.DesignMode == true)
            {
                DrawVirtualChart();
            }
            else if (Aim == null || Aim.Rows.Count == 0)
            {
                DrawVirtualChart();
            }
            else
            {
                try
                {
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
                    if (this.Apperence.SeriesList.Count != 0)
                    {
                        foreach (DataTable dt in dtList)
                        {
                            Series series1 = new Series(dt.TableName);
                            this.Apperence.SeriesList[0].SetSeriseStyle(series1);
                            List<string> Xvalues = new List<string>();
                            List<double> Yvalues = new List<double>();
                            for (int i = 0; i < tbMajor.Rows.Count; i++)
                            {
                                Xvalues.Add(tbMajor.Rows[i][0].ToString());
                                Yvalues.Add(0);
                                for (int j = 0; j < dt.Rows.Count; j++)
                                {
                                    if (tbMajor.Rows[i][0].ToString() == dt.Rows[j][0].ToString())
                                        Yvalues[i] = Convert.ToDouble(dt.Rows[j][1]);
                                }
                            }

                            if (Xvalues.Count != 0 && Yvalues.Count != 0)
                            {
                                series1.Points.DataBindXY(Xvalues, Yvalues);
                            }
                            series1.ChartType = (SeriesChartType)ChartType;
                            chart1.Series.Add(series1);
                        }
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
            foreach (var item in columnList)
            {
                Aim.Columns.Remove(item.ToString());
            }
        }

        public override SuspensionItem[] ListSuspensionItems()
        {
            SuspensionItem[] result = new SuspensionItem[8];
            result[0] = new SuspensionItem(Properties.Resources.Data, Properties.Resources.ResourceManager.GetString("context0021"), Properties.Resources.ResourceManager.GetString("context0021"), new Action(DealWithDataTable));
            result[1] = new SuspensionItem(Properties.Resources.OPEN, Properties.Resources.ResourceManager.GetString("context0022"), Properties.Resources.ResourceManager.GetString("context0022"), new Action(DealWithApperence));
            result[2] = new SuspensionItem(Properties.Resources.chart_type_bar, "条形图", "条形图", new Action(ChangeToBar));
            result[3] = new SuspensionItem(Properties.Resources.chart_type_bar_stack, "堆积条形图", "堆积条形图", new Action(ChangeToStackedBar));
            result[4] = new SuspensionItem(Properties.Resources.chart_type_bar_percent, "百分比堆积条形图", "百分比堆积条形图", new Action(ChangeToStackedBar100));
            result[5] = new SuspensionItem(Properties.Resources.chart_type_column, "柱形图", "柱形图", new Action(ChangeToColumn));
            result[6] = new SuspensionItem(Properties.Resources.chart_type_column_stack, "堆积柱形图", "堆积柱形图", new Action(ChangeToStackedColumn));
            result[7] = new SuspensionItem(Properties.Resources.chart_type_column_percent, "百分比堆积柱形图", "百分比堆积柱形图", new Action(ChangeToStackedColumn100));
            return result;
        }

        protected override void DealWithApperence()
        {
            if (this != null)
            {
                BarApperenceFrm form1 = new BarApperenceFrm();

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

        private void ChangeToBar()
        {
            if (ChartType != BarChartType.Bar)
                ChartType = BarChartType.Bar;
        }
        private void ChangeToStackedBar()
        {
            if (ChartType != BarChartType.StackedBar)
                ChartType = BarChartType.StackedBar;
        }
        private void ChangeToStackedBar100()
        {
            if (ChartType != BarChartType.StackedBar100)
                ChartType = BarChartType.StackedBar100;
        }
        private void ChangeToColumn()
        {
            if (ChartType != BarChartType.Column)
                ChartType = BarChartType.Column;
        }
        private void ChangeToStackedColumn()
        {
            if (ChartType != BarChartType.StackedColumn)
                ChartType = BarChartType.StackedColumn;
        }
        private void ChangeToStackedColumn100()
        {
            if (ChartType != BarChartType.StackedColumn100)
                ChartType = BarChartType.StackedColumn100;
        }
    }

    public enum BarChartType
    {   //
        // 摘要:
        //     条形图类型。
        Bar = 7,
        //
        // 摘要:
        //     堆积条形图类型。
        StackedBar = 8,
        //
        // 摘要:
        //     百分比堆积条形图类型。
        StackedBar100 = 9,
        //
        // 摘要:
        //     柱形图类型。
        Column = 10,
        //
        // 摘要:
        //     堆积柱形图类型。
        StackedColumn = 11,
        //
        // 摘要:
        //     百分比堆积柱形图类型。
        StackedColumn100 = 12,
    }

}
