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
    [ToolboxBitmap(typeof(PieChart), "Resources.PieChart.png")]
    [Designer(typeof(MESDesigner))]
    [DefaultProperty("SourceField")]
    public partial class PieChart : ChartBase
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

        private PieChartType _ChartType = PieChartType.Pie;
        [Category("通用")]
        [Description("图表类型")]
        public PieChartType ChartType
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

        public PieChart()
            : base()
        {
            InitializeComponent();
        }
        public PieChart(MemoryStream Aim)
            : base(Aim)
        {
        }


        public override object Clone()
        {
            PieChart pcc = new PieChart();
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
                Series series1 = new Series();
                //series1.ChartType = (SeriesChartType)ChartType;
                series1.SetCustomProperty("PieLabelStyle", "Disabled");
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
                    if (this.Apperence.SeriesList.Count != 0)
                    {
                        this.Apperence.SeriesList[0].SetSeriesValue(series1);
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
                double[] Yvalues = new double[5];
                string[] Xvalues = { "Australia", "Brasil", "Canada", "Danmark", "Ecuador" };
                Random random = new Random();
                for (int j = 0; j < 5; j++)
                {
                    Yvalues[j] = (50.0 * random.Next(0, 1000) / 1000.0);
                }
                series1.ChartType = (SeriesChartType)ChartType;
                series1.Points.DataBindXY(Xvalues, Yvalues);
                chart1.Series.Add(series1);
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
                if (GroupSource.Enable && !string.IsNullOrEmpty(GroupSource.MinorBinding))
                {
                    if (chart1.ChartAreas.FindByName("ChartArea2") != null)
                    {
                        float offset = (chart1.ChartAreas["ChartArea2"].Area3DStyle.Enable3D == true) ? 5 : 0;
                        chart1.ChartAreas["ChartArea2"].Position.Height = chart1.ChartAreas["ChartArea1"].Position.Height * 2 / 3;
                        chart1.ChartAreas["ChartArea2"].Position.Width = chart1.ChartAreas["ChartArea1"].Position.Width * 2 / 3;
                        chart1.ChartAreas["ChartArea2"].Position.X = chart1.ChartAreas["ChartArea1"].Position.X + chart1.ChartAreas["ChartArea1"].Position.Width / 6;
                        chart1.ChartAreas["ChartArea2"].Position.Y = chart1.ChartAreas["ChartArea1"].Position.Y + chart1.ChartAreas["ChartArea1"].Position.Height / 6 - offset;
                    }
                    chart1.Printing.PrintPaint(graphics, position);
                }
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
                    Series series1 = new Series();
                    Legend legend1 = new Legend();
                    List<string> Xvalues = new List<string>();
                    List<double> Yvalues = new List<double>();
                    if (this.Apperence != null && this.Apperence.SeriesList != null && this.Apperence.SeriesList.Count > 0)
                    {
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
                        this.Apperence.SeriesList[0].SetSeriesValue(series1);
                        for (int i = 0; i < Aim.Rows.Count; i++)
                        {
                            Xvalues.Add(Aim.Rows[i][series1.GetCustomProperty("XBindingField")].ToString());
                            Yvalues.Add(Convert.ToDouble(Aim.Rows[i][series1.GetCustomProperty("YBindingField")]));
                        }

                        if (Xvalues.Count != 0 && Yvalues.Count != 0)
                        {
                            series1.Points.DataBindXY(Xvalues, Yvalues);
                        }
                    }
                    series1.ChartType = (SeriesChartType)ChartType;
                    chart1.Series.Add(series1);
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
            DataTable tbMajorSerise = new DataTable();
            DataTable tbMinorSerise = new DataTable();
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
            tbMajorSerise = ds.SelectGroupByInto(Aim.TableName, Aim, GroupSource.MajorBinding + "," + strValueFx + "(" + GroupSource.ValueBinding + ")", "1=1", GroupSource.MajorBinding);

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

                foreach (DataRow row in tbMajor.Rows)
                {
                    if (row[0] != DBNull.Value)
                        tbMinorSerise.Merge(ds.SelectGroupByInto(GroupSource.MajorBinding, Aim, GroupSource.MinorBinding + "," + strValueFx + "(" + GroupSource.ValueBinding + ")", GroupSource.MajorBinding + "='" + row[0].ToString() + "'", GroupSource.MinorBinding));
                    else
                        tbMinorSerise.Merge(ds.SelectGroupByInto(GroupSource.MajorBinding, Aim, GroupSource.MinorBinding + "," + strValueFx + "(" + GroupSource.ValueBinding + ")", GroupSource.MajorBinding + " is null ", GroupSource.MinorBinding));
                }
            }

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
                    Legend legend1 = new Legend();
                    //if (this._Apperence != null && this._Apperence.SeriesList != null && this._Apperence.SeriesList.Count > 0)
                    //{
                    //    //是否绑定数据源，绑定的数据源是否存在
                    //    this._Apperence.SeriesList[0].SetSeriesValue(series1);
                    //    if (string.IsNullOrEmpty(series1.GetCustomProperty("XBindingField")))
                    //    {
                    //        PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(Properties.Resources.ResourceManager.GetString("message0016"));
                    //        return;
                    //    }
                    //    if (!Aim.Columns.Contains(series1.GetCustomProperty("XBindingField")))
                    //    {
                    //        PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(Properties.Resources.ResourceManager.GetString("message0017"));
                    //        return;
                    //    }
                    //}

                    InitChart();
                    //绑定绘图区
                    if (this.Apperence.ChartAreaList.Count != 0)
                    {
                        ChartArea ChartArea1 = new ChartArea("ChartArea1");
                        this.Apperence.ChartAreaList[0].SetChartAreaStyle(ChartArea1);
                        ChartArea1.InnerPlotPosition.Auto = true;
                        ChartArea1.Position.Auto = true;
                        chart1.ChartAreas.Add(ChartArea1);

                        if (!string.IsNullOrEmpty(GroupSource.MinorBinding))
                        {
                            ChartArea ChartArea2 = new ChartArea("ChartArea2");
                            this.Apperence.ChartAreaList[0].SetChartAreaStyle(ChartArea2);
                            ChartArea2.InnerPlotPosition.Auto = true;
                            ChartArea2.Position.Auto = false;
                            //--临时给定一个坐标，绘制的时候重新赋值
                            ChartArea2.Position.Height = 0;
                            ChartArea2.Position.Width = 0;
                            ChartArea2.Position.X = 0;
                            ChartArea2.Position.Y = 0;
                            //--
                            ChartArea2.BorderColor = Color.Transparent;
                            ChartArea2.BorderDashStyle = ChartDashStyle.NotSet;
                            ChartArea2.BorderWidth = 0;
                            ChartArea2.BackColor = Color.Transparent;
                            ChartArea2.BackSecondaryColor = Color.Transparent;
                            ChartArea2.BackHatchStyle = ChartHatchStyle.None;
                            ChartArea2.BackGradientStyle = GradientStyle.None;
                            chart1.ChartAreas.Add(ChartArea2);
                        }
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
                        //this.Apperence.SeriesList[0].SetSeriesValue(series1);
                        //for (int i = 0; i < Aim.Rows.Count; i++)
                        //{
                        //    Xvalues.Add(Aim.Rows[i][series1.GetCustomProperty("XBindingField")].ToString());
                        //    Yvalues.Add(Convert.ToDouble(Aim.Rows[i][series1.GetCustomProperty("YBindingField")]));
                        //}

                        //if (Xvalues.Count != 0 && Yvalues.Count != 0)
                        //{
                        //    series1.Points.DataBindXY(Xvalues, Yvalues);
                        //}

                        Series series1 = new Series(tbMajorSerise.TableName);
                        this.Apperence.SeriesList[0].SetSeriseStyle(series1);
                        List<string> Xvalues1 = new List<string>();
                        List<double> Yvalues1 = new List<double>();
                        for (int i = 0; i < tbMajorSerise.Rows.Count; i++)
                        {
                            Xvalues1.Add(tbMajorSerise.Rows[i][0].ToString());
                            Yvalues1.Add(Convert.ToDouble(tbMajorSerise.Rows[i][1]));
                        }

                        if (Xvalues1.Count != 0 && Yvalues1.Count != 0)
                        {
                            series1.Points.DataBindXY(Xvalues1, Yvalues1);
                        }
                        series1.ChartType = (SeriesChartType)ChartType;
                        if (ChartType == PieChartType.Doughnut)
                            series1.SetCustomProperty("DoughnutRadius", (100.0 / 3.0 + 1.0).ToString());
                        series1.ChartArea = "ChartArea1";
                        chart1.Series.Add(series1);

                        if (!string.IsNullOrEmpty(GroupSource.MinorBinding))
                        {
                            Series series2 = new Series(tbMinorSerise.TableName);
                            this.Apperence.SeriesList[0].SetSeriseStyle(series2);
                            List<string> Xvalues2 = new List<string>();
                            List<double> Yvalues2 = new List<double>();
                            for (int i = 0; i < tbMinorSerise.Rows.Count; i++)
                            {
                                Xvalues2.Add(tbMinorSerise.Rows[i][0].ToString());
                                Yvalues2.Add(Convert.ToDouble(tbMinorSerise.Rows[i][1]));
                            }

                            if (Xvalues2.Count != 0 && Yvalues2.Count != 0)
                            {
                                series2.Points.DataBindXY(Xvalues2, Yvalues2);
                            }
                            series2.ChartType = (SeriesChartType)ChartType;
                            if (ChartType == PieChartType.Doughnut)
                                series2.SetCustomProperty("DoughnutRadius", (100.0 / 2.0).ToString());
                            series2.ChartArea = "ChartArea2";
                            chart1.Series.Add(series2);
                        }
                    }
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
            tbMajorSerise.Dispose();
            tbMinorSerise.Dispose();
            foreach (var item in columnList)
            {
                Aim.Columns.Remove(item.ToString());
            }
        }

        public override SuspensionItem[] ListSuspensionItems()
        {
            SuspensionItem[] result = new SuspensionItem[4];
            result[0] = new SuspensionItem(Properties.Resources.Data, Properties.Resources.ResourceManager.GetString("context0021"), Properties.Resources.ResourceManager.GetString("context0021"), new Action(DealWithDataTable));
            result[1] = new SuspensionItem(Properties.Resources.OPEN, Properties.Resources.ResourceManager.GetString("context0022"), Properties.Resources.ResourceManager.GetString("context0022"), new Action(DealWithApperence));
            result[2] = new SuspensionItem(Properties.Resources.chart_type_pie, "饼图", "饼图", new Action(ChangeToPie));
            result[3] = new SuspensionItem(Properties.Resources.chart_type_donut, "圆环图", "圆环图", new Action(ChangeToDoughnut));
            return result;
        }

        protected override void DealWithApperence()
        {
            if (this != null)
            {
                PieApperenceFrm form1 = new PieApperenceFrm();

                form1.ChartParent = this;
                DataSource ds = this.Apperence.Clone();
                form1.PMSChartAppearance = this.Apperence.PMSChartAppearance;
                form1.chartAreaList = ds.ChartAreaList;
                form1.legendList = ds.LegendList;
                form1.seriesList = ds.SeriesList;
                form1.titleList = ds.TitleList;
                if (DialogResult.OK == form1.ShowDialog())
                {
                    Invalidate();
                }
            }
        }

        private void ChangeToPie()
        {
            if (ChartType != PieChartType.Pie)
                ChartType = PieChartType.Pie;
        }
        private void ChangeToDoughnut()
        {
            if (ChartType != PieChartType.Doughnut)
                ChartType = PieChartType.Doughnut;
        }
    }

    public enum PieChartType { Pie = 17, Doughnut = 18 }

}
