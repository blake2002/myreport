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
using MES.Report;
using System.Collections;
using PMS.Libraries.ToolControls.PMSPublicInfo;

namespace PMS.Libraries.ToolControls.PMSChart
{
    [ToolboxBitmap(typeof(SectionChart), "Resources.SectionChart.png")]
    [Designer(typeof(MESDesigner))]
    [DisplayName("SectionCurve")]
    [DefaultProperty("SourceField")]
    public partial class SectionChart : UserControl, IChartSection, IResizable, ICloneable, ISuspensionable, IElementTranslator, IElement,
        IBindDataTableManager, IDirectDrawable, IElementExtended, IBindReportExpressionEngine, System.ComponentModel.ICustomTypeDescriptor
        , IProcessCmdKey
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

        [Category("通用")]
        [Description("绑定数据")]
        [DisplayName("Binding")]
        [Editor(typeof(DataTableEditor), typeof(UITypeEditor))]
        public SourceField SourceField
        {
            get
            {
                return _DataTable;
            }
            set
            {
                //if (value != null)
                {
                    _DataTable = value;
                }
            }
        }

        [Category("通用")]
        [Description("外观设置")]
        [Editor(typeof(ApperenceEditor), typeof(UITypeEditor))]
        public DataSource Apperence
        {
            get
            {
                return _Apperence;
            }
            set
            {
                _Apperence = value;
                InitailColumnData();
            }
        }
        #endregion

        #region IProcessCmdKey

        bool IProcessCmdKey.ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            bool bProcessed = false;
            bool bAltKey = (((ushort)PMS.Libraries.ToolControls.PMSPublicInfo.APIs.APIsUser32.GetAsyncKeyState(0x12)) & 0xffff) != 0;
            if (bAltKey)
            {
                if ((int)msg.WParam > 0 && (int)msg.WParam < 255)
                {
                    switch ((char)(msg.WParam))
                    {
                        case 'D':
                            DealWithDataTable();
                            bProcessed = true;
                            break;
                    }
                }
            }
            return bProcessed;
        }

        #endregion
        public SectionChart()
        {
            InitializeComponent();
        }

        public SectionChart(MemoryStream Aim)
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
        DataTable reportData;
        public bool isIntial = false;
        //总数据源
        private DataTable _ReportData;

        //运行状态
        private int _RunMode = 0;
        SourceField _DataTable;
        DataSource _Apperence;

        int pageNum;
        [Browsable(false)]
        public int PageNum
        {
            get { return pageNum; }
            set { pageNum = value; }
        }


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

        void DrawVirtualChart()
        {
            try
            {
                chart1.ChartAreas["ChartArea1"].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                chart1.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = false;
                chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                chart1.ChartAreas["ChartArea1"].AxisX.MajorTickMark.Enabled = false;
                chart1.Series.Clear();
                chart1.Titles.Clear();
                chart1.Legends.Clear();
                chart1.Annotations.Clear();
                Series series1 = new Series();
                series1.ChartType = SeriesChartType.Line;
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
                        if ((this._Apperence.SeriesList[0] as SectionSeries).LabelStyle == sectionClass.enumLabelStyle.WordWrap)
                            autoAxisXlabels(chart1);
                        else
                            chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 90;
                    }
                    else { series1.Color = Color.Blue; }


                }
                string[] Xvalues = new string[11];
                double[] Yvalues = new double[11];
                double[] YvaluesX = new double[11];
                double[] YvaluesLimit = new double[11];
                double YMax = 50;
                if (this.Apperence != null && this.Apperence.SeriesList != null && this.Apperence.SeriesList.Count > 0)
                {
                    if (!(this._Apperence.SeriesList[0] as SectionSeries).AxisMum.Enable && (this._Apperence.SeriesList[0] as SectionSeries).AxisMum.YaxisMaxmum > (this._Apperence.SeriesList[0] as SectionSeries).AxisMum.YaxisMinmum)
                    {
                        if ((this._Apperence.SeriesList[0] as SectionSeries).AxisMum.YaxisMinmum != (this._Apperence.SeriesList[0] as SectionSeries).AxisMum.YaxisMaxmum)
                        {
                            chart1.ChartAreas["ChartArea1"].AxisY.Maximum = (this._Apperence.SeriesList[0] as SectionSeries).AxisMum.YaxisMaxmum;
                            chart1.ChartAreas["ChartArea1"].AxisY.Minimum = (this._Apperence.SeriesList[0] as SectionSeries).AxisMum.YaxisMinmum;
                            YMax = (this._Apperence.SeriesList[0] as SectionSeries).AxisMum.YaxisMaxmum;
                        }
                    }
                }
                Random random = new Random();
                Series seriesX = new Series();
                for (int j = 0; j < 11; j++)
                {
                    if ((j + 1) % 4 == 0)
                    {
                        Yvalues[j] = double.NaN;
                    }
                    else
                    {
                        Yvalues[j] = (YMax * random.NextDouble());
                    }
                    Xvalues[j] = " ";
                    YvaluesX[j] = 0;
                }

                Xvalues[0] = getTimeFormat(DateTime.Now);
                Xvalues[2] = getTimeFormat(DateTime.Now);
                Xvalues[4] = getTimeFormat(DateTime.Now);
                Xvalues[6] = getTimeFormat(DateTime.Now);
                Xvalues[8] = getTimeFormat(DateTime.Now);
                Xvalues[10] = getTimeFormat(DateTime.Now);

                series1.Points.DataBindXY(Xvalues, Yvalues);
                seriesX.Points.DataBindXY(Xvalues, YvaluesX);
                chart1.Series.Add(seriesX);
                chart1.Series.Add(series1);
                if (this.Apperence.SeriesList.Count - 1 > 0)
                {
                    for (int i = 0; i < this._Apperence.SeriesList.Count - 1; i++)
                    {
                        if ((this._Apperence.SeriesList[i + 1] as SectionSeries).Enabled && (this._Apperence.SeriesList[i + 1] as SectionSeries).Limit != 0)
                        {
                            Series serieLimit = this._Apperence.SeriesList[i + 1].ToSeries();
                            for (int j = 0; j < 11; j++)
                                YvaluesLimit[j] = (50 / (this.Apperence.SeriesList.Count - 1) * (i + 1) + 3);

                            serieLimit.ChartType = SeriesChartType.Line;
                            serieLimit.Points.DataBindXY(Xvalues, YvaluesLimit);
                            chart1.Series.Add(serieLimit);
                        }
                    }
                }

                if (this._Apperence != null)
                {
                    if (this._Apperence.annotationList.Count != 0)
                    {
                        if (this._Apperence.annotationList[0].enable)
                        {
                            LineAnnotation sla1 = new LineAnnotation();
                            LineAnnotation sla2 = new LineAnnotation();
                            LineAnnotation sla3 = new LineAnnotation();
                            this.Apperence.annotationList[0].SetAnnotation(sla1);
                            this.Apperence.annotationList[0].SetAnnotation(sla2);
                            this.Apperence.annotationList[0].SetAnnotation(sla3);
                            sla1.SetAnchor(series1.Points[0], seriesX.Points[0]);
                            sla2.SetAnchor(series1.Points[4], seriesX.Points[4]);
                            sla3.SetAnchor(series1.Points[8], seriesX.Points[8]);
                            chart1.Annotations.Add(sla1);
                            chart1.Annotations.Add(sla2);
                            chart1.Annotations.Add(sla3);
                        }
                        if (this._Apperence.annotationList[1].enable)
                        {
                            LineAnnotation ela1 = new LineAnnotation();
                            LineAnnotation ela2 = new LineAnnotation();
                            LineAnnotation ela3 = new LineAnnotation();
                            this.Apperence.annotationList[1].SetAnnotation(ela1);
                            this.Apperence.annotationList[1].SetAnnotation(ela2);
                            this.Apperence.annotationList[1].SetAnnotation(ela3);
                            ela1.SetAnchor(series1.Points[2], seriesX.Points[2]);
                            ela2.SetAnchor(series1.Points[6], seriesX.Points[6]);
                            ela3.SetAnchor(series1.Points[10], seriesX.Points[10]);
                            chart1.Annotations.Add(ela1);
                            chart1.Annotations.Add(ela2);
                            chart1.Annotations.Add(ela3);
                        }
                    }
                }

            }
            catch { throw new Exception("DrawVirtualChart"); }

        }

        public void InitailColumnData()
        {
            if (this.DesignMode == true)
            {
                DrawVirtualChart();
            }
            #region 重绘图表区（废弃）
            //else if (reportData == null || reportData.Rows.Count == 0)
            //{
            //    DrawVirtualChart();
            //}
            //else
            //{
            //    try
            //    {
            //        chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Clear();
            //        chart1.Series.Clear();
            //        chart1.Titles.Clear();
            //        chart1.Legends.Clear();
            //        if (this.Apperence.TitleList.Count != 0)
            //        {
            //            foreach (PMSTitle item in this.Apperence.TitleList)
            //            {
            //                Title title1 = new Title();
            //                item.SetTitle(title1);
            //                chart1.Titles.Add(title1);
            //            }
            //        }
            //        List<string> Xvalues = new List<string>();
            //        List<double> Yvalues = new List<double>();
            //        List<double> YvaluesLimit = new List<double>();
            //        // List<double> YvaluesLower = new List<double>();
            //        List<double> YzeroValues = new List<double>();
            //        List<int> StartIndex = new List<int>();
            //        List<int> EndIndex = new List<int>();
            //        Series series1 = new Series();
            //        if (this._Apperence.SeriesList.Count != 0)
            //        {
            //            series1 = this._Apperence.SeriesList[0].ToSeries();

            //            //SortedList<DateTime, double> XYvaluesTime = new SortedList<DateTime, double>();
            //            //SortedList<double, double> XYvalues = new SortedList<double, double>();
            //            List<SectionValueXY> XYvalues = new List<SectionValueXY>();

            //            List<double> sectionValue = new List<double>();
            //            List<DateTime> sectionTime = new List<DateTime>();
            //            int distance = 1;
            //            int axisLable = 1;

            //            if (_ReportData.Rows[0][(this._Apperence.SeriesList[0] as SectionSeries).SectionField].GetType() != typeof(DateTime))
            //            {
            //                foreach (DataRow cateRow in _ReportData.Rows)
            //                {
            //                    XYvalues.Add(new SectionValueXY(cateRow[(this._Apperence.SeriesList[0] as SectionSeries).SectionField].ToString(), Convert.ToDouble(cateRow[(this._Apperence.SeriesList[0] as SectionSeries).BindingField])));
            //                    sectionValue.Add(Convert.ToDouble(cateRow[(this._Apperence.SeriesList[0] as SectionSeries).SectionField]));
            //                }
            //            }
            //            else
            //            {
            //                foreach (DataRow cateRow in _ReportData.Rows)
            //                {
            //                    XYvalues.Add(new SectionValueXY(cateRow[(this._Apperence.SeriesList[0] as SectionSeries).SectionField].ToString(), Convert.ToDouble(cateRow[(this._Apperence.SeriesList[0] as SectionSeries).BindingField])));
            //                    sectionTime.Add(Convert.ToDateTime(cateRow[(this._Apperence.SeriesList[0] as SectionSeries).SectionField]));
            //                }
            //            }
            //            if (_ReportData.Rows.Count < 100)
            //            {
            //                chart1.ChartAreas["ChartArea1"].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            //                chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            //                //chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
            //                distance = 1;
            //            }
            //            else
            //            {
            //                if (sectionValue.Count != 0)
            //                {
            //                    if (sectionValue[0].ToString().Length > 3)
            //                        chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 90;
            //                }
            //                if (sectionTime.Count != 0)
            //                {
            //                    if (sectionTime[0].ToString().Length > 3)
            //                        chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 90;
            //                }
            //                distance = Convert.ToInt32(_ReportData.Rows.Count / 50);
            //                axisLable = Convert.ToInt32(_ReportData.Rows.Count / 25);
            //            }


            //            if (sectionValue.Count != 0)
            //            {
            //                RadarAlertApp.resetList(sectionValue);
            //                double section = (this._Apperence.SeriesList[0] as SectionSeries).Distance;
            //                if (section == 0)
            //                {
            //                    for (int i = 0; i < sectionValue.Count; i++)
            //                    {
            //                        Xvalues.Add(sectionValue[i].ToString());
            //                        Yvalues.Add(getYvalue(sectionValue[i].ToString(), XYvalues));
            //                    }
            //                    StartIndex.Add(0);
            //                    EndIndex.Add(Yvalues.Count - 1);
            //                    if (axisLable != 1)
            //                    {
            //                        chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(-axisLable, axisLable + 2, sectionValue[0].ToString());
            //                        chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(sectionValue.Count - axisLable, sectionValue.Count + axisLable, sectionValue[sectionValue.Count - 1].ToString());
            //                    }
            //                }
            //                else
            //                {
            //                    StartIndex.Add(0);
            //                    if (axisLable != 1)
            //                    {
            //                        chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(-axisLable, axisLable + 2, sectionValue[0].ToString());
            //                    }
            //                    Xvalues.Add(sectionValue[0].ToString());
            //                    Yvalues.Add(getYvalue(sectionValue[0].ToString(), XYvalues));
            //                    for (int i = 1; i < sectionValue.Count; i++)
            //                    {
            //                        if ((sectionValue[i] - sectionValue[i - 1]) <= section)
            //                        {
            //                            Xvalues.Add(" ");
            //                            //Xvalues.Add(sectionValue[i].ToString());
            //                            Yvalues.Add(getYvalue(sectionValue[i].ToString(), XYvalues));
            //                        }
            //                        else
            //                        {
            //                            EndIndex.Add(Xvalues.Count - 1);
            //                            Xvalues[Xvalues.Count - 1] = sectionValue[i - 1].ToString();
            //                            if (axisLable != 1)
            //                            {
            //                                chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(Xvalues.Count - 1 - axisLable, Xvalues.Count - 1 + axisLable, sectionValue[i - 1].ToString());
            //                            }
            //                            for (int j = 0; j < distance; j++)
            //                            {
            //                                Xvalues.Add(" ");
            //                                Yvalues.Add(double.NaN);
            //                            }
            //                            Xvalues.Add(sectionValue[i].ToString());
            //                            Yvalues.Add(getYvalue(sectionValue[i].ToString(), XYvalues));
            //                            StartIndex.Add(Xvalues.Count - 1);
            //                            if (axisLable != 1)
            //                            {
            //                                chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(Xvalues.Count - 1 - axisLable, Xvalues.Count - 1 + axisLable, sectionValue[i].ToString());
            //                            }
            //                        }
            //                    }
            //                    Xvalues[Xvalues.Count - 1] = sectionValue[sectionValue.Count - 1].ToString();
            //                    EndIndex.Add(Xvalues.Count - 1);
            //                    if (axisLable != 1)
            //                        chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(Xvalues.Count - axisLable, Xvalues.Count + axisLable, sectionValue[sectionValue.Count - 1].ToString());
            //                }
            //            }
            //            if (sectionTime.Count != 0)
            //            {
            //                double section = 0;
            //                RadarAlertApp.resetList(sectionTime);
            //                if ((this._Apperence.SeriesList[0] as SectionSeries).TimeType == SectionSeries.enumTimeType.second)
            //                    section = (this._Apperence.SeriesList[0] as SectionSeries).Distance;
            //                else if ((this._Apperence.SeriesList[0] as SectionSeries).TimeType == SectionSeries.enumTimeType.minute)
            //                    section = (this._Apperence.SeriesList[0] as SectionSeries).Distance * 60;
            //                else if ((this._Apperence.SeriesList[0] as SectionSeries).TimeType == SectionSeries.enumTimeType.hour)
            //                    section = (this._Apperence.SeriesList[0] as SectionSeries).Distance * 3600;
            //                if (section == 0)
            //                {
            //                    for (int i = 0; i < sectionTime.Count; i++)
            //                    {
            //                        Xvalues.Add(sectionTime[i].ToString());
            //                        Yvalues.Add(getYvalue(sectionTime[i].ToString(), XYvalues));
            //                    }
            //                    StartIndex.Add(0);
            //                    EndIndex.Add(Yvalues.Count - 1);
            //                    if (axisLable != 1)
            //                    {
            //                        chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(-axisLable, axisLable + 2, sectionTime[0].ToString());
            //                        chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(sectionTime.Count - axisLable, sectionTime.Count + axisLable, sectionTime[sectionTime.Count - 1].ToString());
            //                    }
            //                }
            //                else
            //                {
            //                    StartIndex.Add(0);
            //                    if (axisLable != 1)
            //                    {
            //                        chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(-axisLable, axisLable + 2, sectionTime[0].ToString());
            //                    }
            //                    Xvalues.Add(sectionTime[0].ToString());
            //                    Yvalues.Add(getYvalue(sectionTime[0].ToString(), XYvalues));
            //                    for (int i = 1; i < sectionTime.Count; i++)
            //                    {
            //                        if ((sectionTime[i] - sectionTime[i - 1]).TotalSeconds <= section)
            //                        {
            //                            Xvalues.Add(" ");
            //                            //Xvalues.Add(sectionTime[i].ToString());
            //                            Yvalues.Add(getYvalue(sectionTime[i].ToString(), XYvalues));
            //                        }
            //                        else
            //                        {
            //                            EndIndex.Add(Xvalues.Count - 1);
            //                            Xvalues[Xvalues.Count - 1] = sectionTime[i - 1].ToString();
            //                            if (axisLable != 1)
            //                            {
            //                                chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(Xvalues.Count - 1 - axisLable, Xvalues.Count - 1 + axisLable, sectionTime[i - 1].ToString());
            //                            }
            //                            for (int j = 0; j < distance; j++)
            //                            {
            //                                Xvalues.Add(" ");
            //                                Yvalues.Add(double.NaN);
            //                            }
            //                            Xvalues.Add(sectionTime[i].ToString());
            //                            Yvalues.Add(getYvalue(sectionTime[i].ToString(), XYvalues));
            //                            StartIndex.Add(Xvalues.Count - 1);
            //                            if (axisLable != 1)
            //                            {
            //                                chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(Xvalues.Count - 1 - axisLable, Xvalues.Count - 1 + axisLable, sectionTime[i].ToString());
            //                            }
            //                        }
            //                    }
            //                    Xvalues[Xvalues.Count - 1] = sectionTime[sectionTime.Count - 1].ToString();
            //                    EndIndex.Add(Xvalues.Count - 1);
            //                    if (axisLable != 1)
            //                    {
            //                        chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(Xvalues.Count - axisLable, Xvalues.Count + axisLable, sectionTime[sectionTime.Count - 1].ToString());
            //                    }
            //                }
            //            }
            //            if (Xvalues.Count != 0 && Yvalues.Count != 0)
            //            {
            //                series1.Points.DataBindXY(Xvalues, Yvalues);
            //                series1.ChartArea = "ChartArea1";
            //                series1.Name = "series1";
            //            }

            //            chart1.Series.Add(series1);
            //            switch ((this._Apperence.SeriesList[0] as SectionSeries).SectionChartType)
            //            {
            //                case SectionSeries.enumSectionChartType.Line:
            //                    series1.ChartType = SeriesChartType.Line;
            //                    break;
            //                case SectionSeries.enumSectionChartType.Spline:
            //                    series1.ChartType = SeriesChartType.Spline;
            //                    break;
            //                case SectionSeries.enumSectionChartType.Stepline:
            //                    series1.ChartType = SeriesChartType.StepLine;
            //                    break;
            //                case SectionSeries.enumSectionChartType.Fastline:
            //                    series1.ChartType = SeriesChartType.FastLine;
            //                    break;
            //                case SectionSeries.enumSectionChartType.Area:
            //                    series1.ChartType = SeriesChartType.Area;
            //                    break;
            //            }
            //            Xvalues.Add(" ");
            //            Yvalues.Add(double.NaN);
            //            for (int i = 0; i < this._Apperence.SeriesList.Count - 1; i++)
            //            {
            //                if ((this._Apperence.SeriesList[i + 1] as SectionSeries).Enabled)
            //                {
            //                    YvaluesLimit.Clear();
            //                    for (int j = 0; j < Yvalues.Count; j++)
            //                        YvaluesLimit.Add((this._Apperence.SeriesList[i + 1] as SectionSeries).Limit);
            //                    Series serieLimit = this._Apperence.SeriesList[i + 1].ToSeries();
            //                    //serieLimit.Name = "serieUpper";
            //                    serieLimit.Points.DataBindXY(Xvalues, YvaluesLimit);
            //                    serieLimit.ChartType = SeriesChartType.Line;
            //                    chart1.Series.Add(serieLimit);
            //                }
            //            }


            //            Series serieZero = new Series();
            //            for (int i = 0; i < Yvalues.Count; i++)
            //                YzeroValues.Add(double.NaN);
            //            serieZero.Points.DataBindXY(Xvalues, YzeroValues);
            //            serieZero.Name = "serieZero";
            //            serieZero.ChartType = SeriesChartType.Line;
            //            chart1.Series.Add(serieZero);
            //            //LineAnnotation Start = new LineAnnotation();
            //            //Start.SetAnchor(series1.Points[2], serieZero.Points[2]);
            //            //chart1.Annotations.Add(Start);
            //            if (this._Apperence.annotationList[0].enable)
            //            {

            //                for (int i = 0; i < StartIndex.Count; i++)
            //                {
            //                    LineAnnotation laStart = new LineAnnotation();
            //                    this._Apperence.annotationList[0].SetAnnotation(laStart);
            //                    if (StartIndex[i] != EndIndex[i])
            //                    {
            //                        laStart.SetAnchor(series1.Points[StartIndex[i]], serieZero.Points[StartIndex[i]]);
            //                        chart1.Annotations.Add(laStart);
            //                    }
            //                }
            //            }
            //            if (this._Apperence.annotationList[1].enable)
            //            {
            //                for (int i = 0; i < EndIndex.Count; i++)
            //                {
            //                    LineAnnotation laEnd = new LineAnnotation();
            //                    this._Apperence.annotationList[1].SetAnnotation(laEnd);

            //                    laEnd.SetAnchor(series1.Points[EndIndex[i]], serieZero.Points[EndIndex[i]]);
            //                    chart1.Annotations.Add(laEnd);
            //                }
            //            }
            //            chart1.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = false;

            //            chart1.ChartAreas["ChartArea1"].AxisX.MajorTickMark.Enabled = false;//刻度尺
            //            chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;//刻度线
            //            if (!(this._Apperence.SeriesList[0] as SectionSeries).AxisMum.Enable)
            //            {
            //                if ((this._Apperence.SeriesList[0] as SectionSeries).AxisMum.YaxisMinmum != (this._Apperence.SeriesList[0] as SectionSeries).AxisMum.YaxisMaxmum)
            //                {
            //                    chart1.ChartAreas["ChartArea1"].AxisY.Maximum = (this._Apperence.SeriesList[0] as SectionSeries).AxisMum.YaxisMaxmum;
            //                    chart1.ChartAreas["ChartArea1"].AxisY.Minimum = (this._Apperence.SeriesList[0] as SectionSeries).AxisMum.YaxisMinmum;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            chart1.Series.Clear();
            //            chart1.Titles.Clear();
            //            chart1.Annotations.Clear();
            //            chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            //        }
            //    }
            //    catch 
            //    { 
            //        chart1.Series.Clear();
            //        chart1.Titles.Clear();
            //        chart1.Annotations.Clear();
            //        chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            //    }
            //}
            #endregion
        }

        double getYvalue(string KEY, List<SectionValueXY> XYvalue)
        {
            foreach (SectionValueXY xy in XYvalue)
            {
                if (xy.Xvalue == KEY)
                {
                    return xy.Yvalue;
                }
            }
            return double.NaN;
        }



        public int GetPagesFromData(DataTable Aim)
        {
            if (Aim == null)
            {
                return 0;
            }
            if ((this.Apperence.SeriesList[0] as SectionSeries).PointsCount == 0)
            {
                return 1;
            }
            if ((Aim.Rows.Count % (this.Apperence.SeriesList[0] as SectionSeries).PointsCount) != 0)
                return Convert.ToInt32(Aim.Rows.Count / (this.Apperence.SeriesList[0] as SectionSeries).PointsCount) + 1;
            else
                return Convert.ToInt32(Aim.Rows.Count / (this.Apperence.SeriesList[0] as SectionSeries).PointsCount);
        }
        private DataTable SortDataTable(DataTable source, string SortField, MESSortType sortType)
        {
            DataTable dt2 = null;
            try
            {
                DataView dv = source.Copy().DefaultView;
                if (!string.IsNullOrEmpty(SortField) && source.Columns.Contains(SortField))
                {
                    dv.Sort = SortField + " " + sortType.ToString();
                }
                dt2 = dv.ToTable();
            }
            catch (Exception e)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, "排序异常:" + e.Message + "  " + e.GetBaseException().ToString(), true);
            }
            //if(Value"√";
            return dt2;
        }
        public void SetData(DataTable Aim, int Index)
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
                    if (this._Apperence != null && this._Apperence.SeriesList != null && this._Apperence.SeriesList.Count > 0 && this._Apperence.SeriesList[0] is SectionSeries)
                    {
                        Aim = SortDataTable(Aim, (this._Apperence.SeriesList[0] as SectionSeries).SectionField, MESSortType.ASC);
                        if (string.IsNullOrEmpty((this._Apperence.SeriesList[0] as SectionSeries).SectionField))
                        {
                            PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(Properties.Resources.ResourceManager.GetString("message0016"));
                            return;
                        }
                        if (!Aim.Columns.Contains((this._Apperence.SeriesList[0] as SectionSeries).SectionField))
                        {
                            PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(Properties.Resources.ResourceManager.GetString("message0017"));
                            return;
                        }
                    }
                    int count = (this._Apperence.SeriesList[0] as SectionSeries).PointsCount;
                    chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Clear();
                    chart1.Series.Clear();
                    chart1.Titles.Clear();
                    chart1.Legends.Clear();
                    if (this.Apperence.TitleList.Count != 0)
                    {
                        foreach (PMSTitle item in this.Apperence.TitleList)
                        {
                            Title title1 = new Title();
                            item.SetTitle(title1);
                            chart1.Titles.Add(title1);
                        }
                    }
                    List<string> Xvalues = new List<string>();
                    List<double> Yvalues = new List<double>();
                    List<double> YvaluesLimit = new List<double>();
                    // List<double> YvaluesLower = new List<double>();
                    List<double> YzeroValues = new List<double>();
                    List<int> StartIndex = new List<int>();
                    List<int> EndIndex = new List<int>();
                    Series series1 = new Series();
                    if (this._Apperence.SeriesList.Count != 0)
                    {
                        series1 = this._Apperence.SeriesList[0].ToSeries();

                        //SortedList<DateTime, double> XYvaluesTime = new SortedList<DateTime, double>();
                        //SortedList<double, double> XYvalues = new SortedList<double, double>();
                        List<SectionValueXY> XYvalues = new List<SectionValueXY>();

                        List<double> sectionValue = new List<double>();
                        List<DateTime> sectionTime = new List<DateTime>();
                        int distance = 1;
                        int axisLable = 1;
                        if (count == 0)
                        {
                            if (Aim.Rows[0][(this._Apperence.SeriesList[0] as SectionSeries).SectionField].GetType() != typeof(DateTime))
                            {
                                foreach (DataRow cateRow in Aim.Rows)
                                {
                                    XYvalues.Add(new SectionValueXY(cateRow[(this._Apperence.SeriesList[0] as SectionSeries).SectionField].ToString(), Convert.ToDouble(cateRow[(this._Apperence.SeriesList[0] as SectionSeries).BindingField])));
                                    sectionValue.Add(Convert.ToDouble(cateRow[(this._Apperence.SeriesList[0] as SectionSeries).SectionField]));
                                }
                            }
                            else
                            {
                                foreach (DataRow cateRow in Aim.Rows)
                                {
                                    XYvalues.Add(new SectionValueXY(cateRow[(this._Apperence.SeriesList[0] as SectionSeries).SectionField].ToString(), Convert.ToDouble(cateRow[(this._Apperence.SeriesList[0] as SectionSeries).BindingField])));
                                    sectionTime.Add(Convert.ToDateTime(cateRow[(this._Apperence.SeriesList[0] as SectionSeries).SectionField]));
                                }
                            }
                            chart1.ChartAreas["ChartArea1"].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                            chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                            if (Aim.Rows.Count < 100)
                            {
                                //chart1.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.LabelsAngleStep30;
                                //chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                                distance = 1;
                            }
                            else
                            {
                                //if (sectionValue.Count != 0)
                                //{
                                //    if (sectionValue[0].ToString().Length > 3)
                                //        chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 90;
                                //}
                                //if (sectionTime.Count != 0)
                                //{
                                //    if (sectionTime[0].ToString().Length > 3)
                                //        chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 90;
                                //}
                                distance = Convert.ToInt32(Aim.Rows.Count / 35);
                                axisLable = Convert.ToInt32(Aim.Rows.Count / 15);
                            }
                        }
                        else if (Aim.Rows.Count - count * Index < count)
                        {
                            if (Aim.Rows[0][(this._Apperence.SeriesList[0] as SectionSeries).SectionField].GetType() != typeof(DateTime))
                            {
                                for (int i = count * Index; i - count * Index < Aim.Rows.Count - count * Index; i++)
                                {
                                    XYvalues.Add(new SectionValueXY(Aim.Rows[i][(this._Apperence.SeriesList[0] as SectionSeries).SectionField].ToString(), Convert.ToDouble(Aim.Rows[i][(this._Apperence.SeriesList[0] as SectionSeries).BindingField])));
                                    sectionValue.Add(Convert.ToDouble(Aim.Rows[i][(this._Apperence.SeriesList[0] as SectionSeries).SectionField]));
                                }
                            }
                            else
                            {
                                for (int i = count * Index; i - count * Index < Aim.Rows.Count - count * Index; i++)
                                {
                                    XYvalues.Add(new SectionValueXY(Aim.Rows[i][(this._Apperence.SeriesList[0] as SectionSeries).SectionField].ToString(), Convert.ToDouble(Aim.Rows[i][(this._Apperence.SeriesList[0] as SectionSeries).BindingField])));
                                    sectionTime.Add(Convert.ToDateTime(Aim.Rows[i][(this._Apperence.SeriesList[0] as SectionSeries).SectionField]));
                                }
                            }
                        }
                        else
                        {
                            if (Aim.Rows[0][(this._Apperence.SeriesList[0] as SectionSeries).SectionField].GetType() != typeof(DateTime))
                            {
                                for (int i = count * Index; i - count * Index < count; i++)
                                {
                                    XYvalues.Add(new SectionValueXY(Aim.Rows[i][(this._Apperence.SeriesList[0] as SectionSeries).SectionField].ToString(), Convert.ToDouble(Aim.Rows[i][(this._Apperence.SeriesList[0] as SectionSeries).BindingField])));
                                    sectionValue.Add(Convert.ToDouble(Aim.Rows[i][(this._Apperence.SeriesList[0] as SectionSeries).SectionField]));
                                }
                            }
                            else
                            {
                                for (int i = count * Index; i - count * Index < count; i++)
                                {
                                    XYvalues.Add(new SectionValueXY(Aim.Rows[i][(this._Apperence.SeriesList[0] as SectionSeries).SectionField].ToString(), Convert.ToDouble(Aim.Rows[i][(this._Apperence.SeriesList[0] as SectionSeries).BindingField])));
                                    sectionTime.Add(Convert.ToDateTime(Aim.Rows[i][(this._Apperence.SeriesList[0] as SectionSeries).SectionField]));
                                }
                            }
                        }
                        if (Aim.Rows.Count - count * Index > count && count != 0)
                        {
                            chart1.ChartAreas["ChartArea1"].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                            chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                            if (count < 100)
                            {

                                //chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                                distance = 1;
                            }
                            else
                            {
                                //if (sectionValue.Count != 0)
                                //{
                                //    if (sectionValue[0].ToString().Length > 3)
                                //        chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 30;
                                //}
                                //if (sectionTime.Count != 0)
                                //{
                                //    if (sectionTime[0].ToString().Length > 3)
                                //        chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                                //}

                                distance = Convert.ToInt32(count / 35);
                                axisLable = Convert.ToInt32(count / 15);

                            }
                        }
                        else
                        {
                            chart1.ChartAreas["ChartArea1"].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                            chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                            if (Aim.Rows.Count - count * Index < 100)
                            {

                                //chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                                distance = 1;
                            }
                            else
                            {
                                //if (sectionValue.Count != 0)
                                //{
                                //    if (sectionValue[0].ToString().Length > 3)
                                //        chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 90;
                                //}
                                //if (sectionTime.Count != 0)
                                //{
                                //    if (sectionTime[0].ToString().Length > 3)
                                //chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 0;
                                //}


                                distance = Convert.ToInt32((Aim.Rows.Count - count * Index) / 35);
                                axisLable = Convert.ToInt32((Aim.Rows.Count - count * Index) / 15);

                            }
                        }


                        if (sectionValue.Count != 0)
                        {
                            RadarAlertApp.resetList(sectionValue);
                            double section = (this._Apperence.SeriesList[0] as SectionSeries).Distance;
                            if (section == 0)
                            {
                                for (int i = 0; i < sectionValue.Count; i++)
                                {
                                    Xvalues.Add(sectionValue[i].ToString());
                                    Yvalues.Add(getYvalue(sectionValue[i].ToString(), XYvalues));
                                }
                                StartIndex.Add(0);
                                EndIndex.Add(Yvalues.Count - 1);
                                if (axisLable != 1)
                                {
                                    chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(-axisLable, axisLable + 2, sectionValue[0].ToString());
                                    chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(sectionValue.Count - axisLable, sectionValue.Count + axisLable, sectionValue[sectionValue.Count - 1].ToString());
                                }
                            }
                            else
                            {
                                if (Index != 0)
                                {
                                    if (Convert.ToDouble(Aim.Rows[Index * count][(this._Apperence.SeriesList[0] as SectionSeries).SectionField]) - Convert.ToDouble(Aim.Rows[Index * count][(this._Apperence.SeriesList[0] as SectionSeries).SectionField]) > section)
                                    {
                                        StartIndex.Add(0);
                                        if (axisLable != 1)
                                        {
                                            chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(-axisLable, axisLable + 2, sectionValue[0].ToString());
                                        }
                                        Xvalues.Add(sectionValue[0].ToString());
                                    }
                                    else
                                    {
                                        Xvalues.Add(" ");
                                    }
                                }
                                else
                                {
                                    StartIndex.Add(0);
                                    if (axisLable != 1)
                                    {
                                        chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(-axisLable, axisLable + 2, sectionValue[0].ToString());
                                    }
                                    Xvalues.Add(sectionValue[0].ToString());
                                }


                                Yvalues.Add(getYvalue(sectionValue[0].ToString(), XYvalues));
                                for (int i = 1; i < sectionValue.Count; i++)
                                {
                                    if ((sectionValue[i] - sectionValue[i - 1]) <= section)
                                    {
                                        Xvalues.Add(" ");
                                        //Xvalues.Add(sectionValue[i].ToString());
                                        Yvalues.Add(getYvalue(sectionValue[i].ToString(), XYvalues));
                                    }
                                    else
                                    {
                                        EndIndex.Add(Xvalues.Count - 1);
                                        Xvalues[Xvalues.Count - 1] = sectionValue[i - 1].ToString();
                                        if (axisLable != 1)
                                        {
                                            chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(Xvalues.Count - 1 - axisLable, Xvalues.Count - 1 + axisLable, sectionValue[i - 1].ToString());
                                        }
                                        for (int j = 0; j < distance; j++)
                                        {
                                            Xvalues.Add(" ");
                                            Yvalues.Add(double.NaN);
                                        }
                                        Xvalues.Add(sectionValue[i].ToString());
                                        Yvalues.Add(getYvalue(sectionValue[i].ToString(), XYvalues));
                                        StartIndex.Add(Xvalues.Count - 1);
                                        if (axisLable != 1)
                                        {
                                            chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(Xvalues.Count - 1 - axisLable, Xvalues.Count - 1 + axisLable, sectionValue[i].ToString());
                                        }
                                    }
                                }

                                if (Index != 0)
                                {
                                    if ((Index + 1) * count >= Aim.Rows.Count && Index != 0)
                                    {
                                        Xvalues[Xvalues.Count - 1] = sectionValue[sectionValue.Count - 1].ToString();
                                        EndIndex.Add(Xvalues.Count - 1);
                                        if (axisLable != 1)
                                            chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(Xvalues.Count - axisLable, Xvalues.Count + axisLable, sectionValue[sectionValue.Count - 1].ToString());
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(Aim.Rows[Index * (count + 1)][(this._Apperence.SeriesList[0] as SectionSeries).SectionField]) - Convert.ToDouble(Aim.Rows[Index * (count + 1) - 1][(this._Apperence.SeriesList[0] as SectionSeries).SectionField]) > section)
                                        {
                                            Xvalues[Xvalues.Count - 1] = sectionValue[sectionValue.Count - 1].ToString();
                                            EndIndex.Add(Xvalues.Count - 1);
                                            if (axisLable != 1)
                                                chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(Xvalues.Count - axisLable, Xvalues.Count + axisLable, sectionValue[sectionValue.Count - 1].ToString());
                                        }
                                        else
                                        {
                                            Xvalues[Xvalues.Count - 1] = " ";
                                        }
                                    }
                                }
                                else
                                {
                                    Xvalues[Xvalues.Count - 1] = sectionValue[sectionValue.Count - 1].ToString();
                                    EndIndex.Add(Xvalues.Count - 1);
                                    if (axisLable != 1)
                                        chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(Xvalues.Count - axisLable, Xvalues.Count + axisLable, sectionValue[sectionValue.Count - 1].ToString());
                                }
                            }
                        }
                        if (sectionTime.Count != 0)
                        {
                            double section = 0;
                            RadarAlertApp.resetList(sectionTime);
                            if ((this._Apperence.SeriesList[0] as SectionSeries).TimeType == SectionSeries.enumTimeType.second)
                                section = (this._Apperence.SeriesList[0] as SectionSeries).Distance;
                            else if ((this._Apperence.SeriesList[0] as SectionSeries).TimeType == SectionSeries.enumTimeType.minute)
                                section = (this._Apperence.SeriesList[0] as SectionSeries).Distance * 60;
                            else if ((this._Apperence.SeriesList[0] as SectionSeries).TimeType == SectionSeries.enumTimeType.hour)
                                section = (this._Apperence.SeriesList[0] as SectionSeries).Distance * 3600;
                            if (section == 0)
                            {
                                for (int i = 0; i < sectionTime.Count; i++)
                                {
                                    Xvalues.Add(sectionTime[i].ToString());
                                    Yvalues.Add(getYvalue(sectionTime[i].ToString(), XYvalues));
                                }
                                StartIndex.Add(0);
                                EndIndex.Add(Yvalues.Count - 1);
                                if (axisLable != 1)
                                {
                                    chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(-axisLable, axisLable + 2, getTimeFormat(sectionTime[0]));
                                    chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(sectionTime.Count - axisLable, sectionTime.Count + axisLable, getTimeFormat(sectionTime[sectionTime.Count - 1]));
                                }
                            }
                            else
                            {
                                if (Index != 0)
                                {
                                    if (Index * count - 1 >= 0 && (Convert.ToDateTime(Aim.Rows[Index * count][(this._Apperence.SeriesList[0] as SectionSeries).SectionField]) - Convert.ToDateTime(Aim.Rows[Index * count - 1][(this._Apperence.SeriesList[0] as SectionSeries).SectionField])).TotalSeconds > section)
                                    {
                                        Xvalues.Add(sectionTime[0].ToString());
                                        StartIndex.Add(0);
                                        if (axisLable != 1)
                                        {
                                            chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(-axisLable, axisLable + 2, getTimeFormat(sectionTime[0]));
                                        }
                                    }
                                    else
                                    {
                                        Xvalues.Add(" ");
                                    }
                                }
                                else
                                {
                                    Xvalues.Add(sectionTime[0].ToString());
                                    StartIndex.Add(0);
                                    if (axisLable != 1)
                                    {
                                        chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(-axisLable, axisLable + 2, getTimeFormat(sectionTime[0]));
                                    }
                                }


                                Yvalues.Add(getYvalue(sectionTime[0].ToString(), XYvalues));
                                for (int i = 1; i < sectionTime.Count; i++)
                                {
                                    if ((sectionTime[i] - sectionTime[i - 1]).TotalSeconds <= section)
                                    {
                                        Xvalues.Add(" ");
                                        //Xvalues.Add(sectionTime[i].ToString());
                                        Yvalues.Add(getYvalue(sectionTime[i].ToString(), XYvalues));
                                    }
                                    else
                                    {
                                        EndIndex.Add(Xvalues.Count - 1);
                                        Xvalues[Xvalues.Count - 1] = sectionTime[i - 1].ToString();
                                        if (axisLable != 1)
                                        {
                                            chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(Xvalues.Count - 1 - axisLable, Xvalues.Count - 1 + axisLable, getTimeFormat(sectionTime[i - 1]));
                                        }
                                        for (int j = 0; j < distance; j++)
                                        {
                                            Xvalues.Add(" ");
                                            Yvalues.Add(double.NaN);
                                        }
                                        Xvalues.Add(sectionTime[i].ToString());
                                        Yvalues.Add(getYvalue(sectionTime[i].ToString(), XYvalues));
                                        StartIndex.Add(Xvalues.Count - 1);
                                        if (axisLable != 1)
                                        {
                                            chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(Xvalues.Count - 1 - axisLable, Xvalues.Count - 1 + axisLable, getTimeFormat(sectionTime[i]));
                                        }
                                    }
                                }

                                if (Index != 0)
                                {
                                    if ((Index + 1) * count >= Aim.Rows.Count)
                                    {
                                        Xvalues[Xvalues.Count - 1] = sectionTime[sectionTime.Count - 1].ToString();
                                        EndIndex.Add(Xvalues.Count - 1);
                                        if (axisLable != 1)
                                            chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(Xvalues.Count - axisLable, Xvalues.Count + axisLable, getTimeFormat(sectionTime[sectionTime.Count - 1]));
                                    }
                                    else
                                    {
                                        if ((Convert.ToDateTime(Aim.Rows[(Index + 1) * count][(this._Apperence.SeriesList[0] as SectionSeries).SectionField]) - Convert.ToDateTime(Aim.Rows[count * (Index + 1) - 1][(this._Apperence.SeriesList[0] as SectionSeries).SectionField])).TotalSeconds > section)
                                        {
                                            Xvalues[Xvalues.Count - 1] = sectionTime[sectionTime.Count - 1].ToString();
                                            EndIndex.Add(Xvalues.Count - 1);
                                            if (axisLable != 1)
                                                chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(Xvalues.Count - axisLable, Xvalues.Count + axisLable, getTimeFormat(sectionTime[sectionTime.Count - 1]));
                                        }
                                        else
                                        {
                                            bool has = false;
                                            if (StartIndex != null && StartIndex.Count > 0)
                                            {
                                                for (int i = 0; i < StartIndex.Count; i++)
                                                {
                                                    if (StartIndex[i] == Xvalues.Count - 1)
                                                    {
                                                        has = true;
                                                        break;
                                                    }
                                                }
                                            }
                                            if (has == false)
                                            {
                                                Xvalues[Xvalues.Count - 1] = " ";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (Aim.Rows.Count > (Index + 1) * count)
                                    {
                                        if ((Convert.ToDateTime(Aim.Rows[(Index + 1) * count][(this._Apperence.SeriesList[0] as SectionSeries).SectionField]) - Convert.ToDateTime(Aim.Rows[count * (Index + 1) - 1][(this._Apperence.SeriesList[0] as SectionSeries).SectionField])).TotalSeconds > section)
                                        {
                                            Xvalues[Xvalues.Count - 1] = sectionTime[sectionTime.Count - 1].ToString();
                                            EndIndex.Add(Xvalues.Count - 1);
                                            if (axisLable != 1)
                                                chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(Xvalues.Count - axisLable, Xvalues.Count + axisLable, getTimeFormat(sectionTime[sectionTime.Count - 1]));
                                        }
                                        else
                                        {
                                            Xvalues[Xvalues.Count - 1] = " ";
                                        }
                                    }
                                    else
                                    {
                                        Xvalues[Xvalues.Count - 1] = sectionTime[sectionTime.Count - 1].ToString();
                                        EndIndex.Add(Xvalues.Count - 1);
                                        if (axisLable != 1)
                                            chart1.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(Xvalues.Count - axisLable, Xvalues.Count + axisLable, getTimeFormat(sectionTime[sectionTime.Count - 1]));
                                    }
                                }
                            }
                        }
                        if (Xvalues.Count != 0 && Yvalues.Count != 0)
                        {
                            series1.Points.DataBindXY(Xvalues, Yvalues);
                            series1.ChartArea = "ChartArea1";
                            series1.Name = "series1";
                        }

                        chart1.Series.Add(series1);
                        switch ((this._Apperence.SeriesList[0] as SectionSeries).SectionChartType)
                        {
                            case SectionSeries.enumSectionChartType.Line:
                                series1.ChartType = SeriesChartType.Line;
                                break;
                            case SectionSeries.enumSectionChartType.Spline:
                                series1.ChartType = SeriesChartType.Spline;
                                break;
                            case SectionSeries.enumSectionChartType.Stepline:
                                series1.ChartType = SeriesChartType.StepLine;
                                break;
                            case SectionSeries.enumSectionChartType.Fastline:
                                series1.ChartType = SeriesChartType.FastLine;
                                break;
                            case SectionSeries.enumSectionChartType.Area:
                                series1.ChartType = SeriesChartType.Area;
                                break;
                        }
                        //Xvalues.Add(" ");
                        //Yvalues.Add(double.NaN);
                        for (int i = 0; i < this._Apperence.SeriesList.Count - 1; i++)
                        {
                            if ((this._Apperence.SeriesList[i + 1] as SectionSeries).Enabled)
                            {
                                YvaluesLimit.Clear();
                                for (int j = 0; j < Yvalues.Count; j++)
                                    YvaluesLimit.Add((this._Apperence.SeriesList[i + 1] as SectionSeries).Limit);
                                Series serieLimit = this._Apperence.SeriesList[i + 1].ToSeries();
                                //serieLimit.Name = "serieUpper";
                                serieLimit.Points.DataBindXY(Xvalues, YvaluesLimit);
                                serieLimit.ChartType = SeriesChartType.Line;
                                chart1.Series.Add(serieLimit);
                            }
                        }

                        double XaxisMum = 0;
                        if (!(this._Apperence.SeriesList[0] as SectionSeries).AxisMum.Enable && (this._Apperence.SeriesList[0] as SectionSeries).AxisMum.YaxisMaxmum > (this._Apperence.SeriesList[0] as SectionSeries).AxisMum.YaxisMinmum)
                        {
                            if ((this._Apperence.SeriesList[0] as SectionSeries).AxisMum.YaxisMinmum != (this._Apperence.SeriesList[0] as SectionSeries).AxisMum.YaxisMaxmum)
                            {
                                chart1.ChartAreas["ChartArea1"].AxisY.Maximum = (this._Apperence.SeriesList[0] as SectionSeries).AxisMum.YaxisMaxmum;
                                chart1.ChartAreas["ChartArea1"].AxisY.Minimum = (this._Apperence.SeriesList[0] as SectionSeries).AxisMum.YaxisMinmum;
                                XaxisMum = (this._Apperence.SeriesList[0] as SectionSeries).AxisMum.YaxisMinmum;
                            }
                        }

                        Series serieZero = new Series();
                        for (int i = 0; i < Yvalues.Count; i++)
                            YzeroValues.Add(XaxisMum);
                        serieZero.Points.DataBindXY(Xvalues, YzeroValues);
                        //serieZero.Points.AddXY(" ", XaxisMum);
                        serieZero.Name = "serieZero";
                        serieZero.ChartType = SeriesChartType.Line;
                        serieZero.Color = Color.Transparent;
                        chart1.Series.Add(serieZero);
                        //LineAnnotation Start = new LineAnnotation();
                        //Start.SetAnchor(series1.Points[2], serieZero.Points[2]);
                        //chart1.Annotations.Add(Start);
                        if (this._Apperence.annotationList[0].enable)
                        {

                            for (int i = 0; i < StartIndex.Count; i++)
                            {
                                LineAnnotation laStart = new LineAnnotation();
                                this._Apperence.annotationList[0].SetAnnotation(laStart);
                                //if (StartIndex[i] != EndIndex[i])
                                //{
                                laStart.SetAnchor(series1.Points[StartIndex[i]], serieZero.Points[StartIndex[i]]);
                                chart1.Annotations.Add(laStart);
                                //}
                            }
                        }
                        if (this._Apperence.annotationList[1].enable)
                        {
                            for (int i = 0; i < EndIndex.Count; i++)
                            {
                                LineAnnotation laEnd = new LineAnnotation();
                                this._Apperence.annotationList[1].SetAnnotation(laEnd);

                                laEnd.SetAnchor(series1.Points[EndIndex[i]], serieZero.Points[EndIndex[i]]);
                                if (EndIndex[i] == serieZero.Points.Count - 1)
                                {
                                    serieZero.Points.AddXY(" ", XaxisMum);
                                }
                                chart1.Annotations.Add(laEnd);
                            }
                        }
                        chart1.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = false;

                        chart1.ChartAreas["ChartArea1"].AxisX.MajorTickMark.Enabled = false;//刻度尺
                        chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;//刻度线

                        if ((this._Apperence.SeriesList[0] as SectionSeries).LabelStyle == sectionClass.enumLabelStyle.WordWrap)
                            autoAxisXlabels(chart1);
                        else
                            chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 90;
                        //XaxisLabelFormat((this._Apperence.SeriesList[0] as SectionSeries).Format, this.chart1);
                    }
                    else
                    {
                        chart1.Series.Clear();
                        chart1.Titles.Clear();
                        chart1.Annotations.Clear();
                        chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                    }
                }
                catch (System.Exception ex)
                {
                    chart1.Series.Clear();
                    chart1.Titles.Clear();
                    chart1.Annotations.Clear();
                    chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                    PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(ex.Message);
                }
            }
        }

        string getTimeFormat(DateTime dt)
        {
            try
            {
                if ((this._Apperence.SeriesList[0] as SectionSeries).Format.Trim() != "" && (this._Apperence.SeriesList[0] as SectionSeries).Format != null)
                {
                    //return string.Format("{0:" + (this._Apperence.SeriesList[0] as SectionSeries).Format + "}", dt.ToString());
                    return dt.ToString((this._Apperence.SeriesList[0] as SectionSeries).Format);
                }
                else
                    return dt.ToString();
            }
            catch { return dt.ToString(); }
        }

        void XaxisLabelFormat(string format, Chart chart)
        {
            try
            {
                if (format.Trim() != "")
                    for (int i = 0; i < chart.ChartAreas["ChartArea1"].AxisX.CustomLabels.Count; i++)
                    {
                        // chart.ChartAreas["ChartArea1"].AxisX.CustomLabels[i].Text = string.Format("{0:"+format+"}", chart.ChartAreas["ChartArea1"].AxisX.CustomLabels[i].Text);
                        chart.ChartAreas["ChartArea1"].AxisX.CustomLabels[i].Text = string.Format("{0:" + format + "}", chart.ChartAreas["ChartArea1"].AxisX.CustomLabels[i].Text);
                    }
            }
            catch { }
        }

        void autoAxisXlabels(Chart chart)
        {

            if (chart.ChartAreas["ChartArea1"].AxisX.CustomLabels.Count != 0)
            {
                //chart.ChartAreas["ChartArea1"].AxisX.CustomLabels[0].RowIndex = 1;
                //chart.ChartAreas["ChartArea1"].AxisX.IsLabelAutoFit = false;
                double length = chart.ChartAreas["ChartArea1"].AxisX.CustomLabels[0].ToPosition - chart.ChartAreas["ChartArea1"].AxisX.CustomLabels[0].FromPosition;
                for (int i = 0; i < chart.ChartAreas["ChartArea1"].AxisX.CustomLabels.Count; i++)
                {
                    //if (chart.ChartAreas["ChartArea1"].AxisX.CustomLabels[i].RowIndex == 0) 
                    //{
                    //    chart.ChartAreas["ChartArea1"].AxisX.CustomLabels[i].RowIndex = 1;
                    //}

                    for (int j = i + 1; j < chart.ChartAreas["ChartArea1"].AxisX.CustomLabels.Count; j++)
                    {
                        if (chart.ChartAreas["ChartArea1"].AxisX.CustomLabels[j].ToPosition - chart.ChartAreas["ChartArea1"].AxisX.CustomLabels[i].FromPosition > length * 2)
                        {
                            chart.ChartAreas["ChartArea1"].AxisX.CustomLabels[j].RowIndex = chart.ChartAreas["ChartArea1"].AxisX.CustomLabels[i].RowIndex;
                            i = j - 1;
                            break;
                        }
                        else
                        {
                            chart.ChartAreas["ChartArea1"].AxisX.CustomLabels[j].RowIndex = chart.ChartAreas["ChartArea1"].AxisX.CustomLabels[j - 1].RowIndex + 1;
                        }
                    }
                }
            }
        }

        public void PrintPaint(Graphics graphics, Rectangle position)
        {

            try
            {
                chart1.Printing.PrintPaint(graphics, position);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "分段曲线信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public object Clone()
        {
            SectionChart pcc = new SectionChart();
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

        #region IResizable
        private Point OriginPosition;
        private int OriginWidth;
        private int OriginHeight;
        [Browsable(false)]
        public float HorizontalScale { get; set; }
        [Browsable(false)]
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
        #endregion

        public SuspensionItem[] ListSuspensionItems()
        {
            SuspensionItem[] result = new SuspensionItem[2];
            result[0] = new SuspensionItem(Properties.Resources.Data, Properties.Resources.ResourceManager.GetString("context0021"), Properties.Resources.ResourceManager.GetString("context0021"), new Action(DealWithDataTable));
            result[1] = new SuspensionItem(Properties.Resources.OPEN, Properties.Resources.ResourceManager.GetString("context0022"), Properties.Resources.ResourceManager.GetString("context0022"), new Action(DealWithApperence));
            //result[2] = new SuspensionItem(Properties.Resources.OPEN, Properties.Resources.ResourceManager.GetString("context0022"), Properties.Resources.ResourceManager.GetString("context0022"), new Action(DealWithApperence1));
            return result;
        }
        private void DealWithDataTable()
        {
            if (null != this)
            {
                SectionChart element = this as SectionChart;
                if (null != element)
                {
                    if (element.Parent == null ||
                       (element.Parent != null && element.Parent as IPmsReportDataBind == null))
                    {
                        PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField sf = (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetCurrentReportDataDefine()) as PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField;
                        //SourceBindDialog fbd = new SourceBindDialog(sfAll, element.SourceField);
                        PMS.Libraries.ToolControls.Report.Controls.EditorDialog.SourceBindDialog fbd = new PMS.Libraries.ToolControls.Report.Controls.EditorDialog.SourceBindDialog(sf, element.SourceField, true);
                        if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            bool contain = false;
                            FieldTreeViewData sfAll = (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetCurrentReportDataDefine()) as FieldTreeViewData;
                            if (fbd.SourceField != null)
                            {
                                List<SourceField> lpdb = fbd.SourceField.GetSubSourceField(sfAll);
                                foreach (SourceField pdb in lpdb)
                                {
                                    try
                                    {
                                        if (!string.IsNullOrEmpty(pdb.DataType))
                                        {
                                            string typ = pdb.DataType.ToUpper();
                                            if (typ.Equals("INT", StringComparison.InvariantCultureIgnoreCase) ||
                                            typ.Equals("FLOAT", StringComparison.InvariantCultureIgnoreCase) ||
                                            typ.Equals("REAL", StringComparison.InvariantCultureIgnoreCase) ||
                                            typ.Equals("INT32", StringComparison.InvariantCultureIgnoreCase) ||
                                            typ.Equals("INT16", StringComparison.InvariantCultureIgnoreCase) ||
                                            typ.Equals("INT64", StringComparison.InvariantCultureIgnoreCase) ||
                                            typ.Equals("SYSTEM.SINGLE", StringComparison.InvariantCultureIgnoreCase) ||
                                            typ.Equals("SYSTEM.DOUBLE", StringComparison.InvariantCultureIgnoreCase) ||
                                            typ.Equals("SYSTEM.INT32", StringComparison.InvariantCultureIgnoreCase) ||
                                            typ.Equals("SYSTEM.DECIMAL", StringComparison.InvariantCultureIgnoreCase) ||
                                            typ.Equals("SYSTEM.DateTime", StringComparison.InvariantCultureIgnoreCase))
                                            {
                                                contain = true;
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        throw new Exception("lpdb");
                                    }
                                }
                                if (contain)
                                    SourceField = fbd.SourceField;
                                else
                                    MessageBox.Show("所选数据集无法分段！");
                            }
                            else
                                SourceField = null;
                            NotifyDesignSurfaceChange();
                        }
                    }
                    else
                    {
                        PMS.Libraries.ToolControls.Report.Controls.EditorDialog.SourceBindDialog fbd = new PMS.Libraries.ToolControls.Report.Controls.EditorDialog.SourceBindDialog(GetSourceField(element.Parent as IElement), element.SourceField, true);
                        if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            SourceField = fbd.SourceField;
                            NotifyDesignSurfaceChange();
                        }
                    }
                }
            }
        }

        //private void DealWithApperence() 
        //{
        //    if (this != null)
        //    {
        //        SectionChart control = null;
        //        if (this is SectionChart)
        //            control = this as SectionChart;

        //        SectionApperence form1 = new SectionApperence();
        //        form1.ChartParent = control as SectionChart;
        //        form1.isIntial = this.isIntial;
        //        DataSource ds = control.Apperence.Clone();
        //        form1.Annotations = ds.annotationList;
        //        form1.seriesList = ds.SeriesList;
        //        form1.titleList = ds.TitleList;
        //        DialogResult dr = form1.ShowDialog();
        //        if (DialogResult.OK == dr)
        //        {
        //            control.Apperence.annotationList = form1.Annotations;
        //            control.Apperence.SeriesList = form1.seriesList;
        //            control.Apperence.TitleList = form1.titleList;
        //            isIntial = true;
        //            if (null != Site)
        //            {
        //                IComponentChangeService cs = Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
        //                if (null != cs)
        //                {
        //                    cs.OnComponentChanged(this, null, null, null);
        //                }
        //            }
        //        }
        //        else if (DialogResult.Cancel == dr) 
        //        {
        //            if (form1.isApply) 
        //            {
        //                if (form1.dsApply != null) 
        //                control.Apperence = form1.dsApply.Clone();
        //                isIntial = true;
        //            }
        //            if (null != Site)
        //            {
        //                IComponentChangeService cs = Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
        //                if (null != cs)
        //                {
        //                    cs.OnComponentChanged(this, null, null, null);
        //                }
        //            }
        //        }
        //    }
        //}

        private void DealWithApperence()
        {
            if (this != null)
            {
                SectionChart control = null;
                if (this is SectionChart)
                    control = this as SectionChart;

                FormSectionApperence form1 = new FormSectionApperence();
                form1.ChartParent = control as SectionChart;
                form1.isIntial = this.isIntial;
                DataSource ds = control.Apperence.Clone();
                form1.Annotations = ds.annotationList;
                form1.seriesList = ds.SeriesList;
                form1.titleList = ds.TitleList;
                DialogResult dr = form1.ShowDialog();
                if (DialogResult.OK == dr)
                {
                    control.Apperence.annotationList = form1.Annotations;
                    control.Apperence.SeriesList = form1.seriesList;
                    control.Apperence.TitleList = form1.titleList;
                    isIntial = true;
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
                        if (form1.dsApply != null)
                            control.Apperence = form1.dsApply.Clone();
                        isIntial = true;
                    }
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
            SectionSerializerClass result = new SectionSerializerClass();
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

        #region IElement
        [Browsable(false)]
        public ElementBorder Border { get; set; }
        [Browsable(false)]
        public string BorderName { get; set; }
        [Browsable(false)]
        public bool CanInvalidate { get; set; }
        [Browsable(false)]
        public ExtendObject ExtendObject { get; set; }
        [Browsable(false)]
        public List<ExternData> ExternDatas { get; set; }
        [Browsable(false)]
        public bool HasBorder { get; set; }
        [Browsable(false)]
        public bool HasLeftBorder { get; set; }
        [Browsable(false)]
        public bool HasTopBorder { get; set; }
        [Browsable(false)]
        public bool HasBottomBorder { get; set; }
        [Browsable(false)]
        public bool HasRightBorder { get; set; }
        [Browsable(false)]
        public MESVarType MESType { get; set; }
        [Browsable(false)]
        public float MoveX { get; set; }
        [Browsable(false)]
        public float MoveY { get; set; }
        [Browsable(false)]
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
        [Browsable(false)]
        string IElement.Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                base.Name = value;
            }
        }
        #endregion

        private void SectionChart_Load(object sender, EventArgs e)
        {
            if (_Apperence == null)
            {
                _Apperence = new DataSource(null);
                InitailColumnData();
            }
        }

        public int BindDataTableManager(IDataTableManager dtm, string bindPath)
        {
            string strTableName = SourceField.Name;
            if (!string.IsNullOrEmpty(bindPath))
            {
                strTableName = string.Format("{0}.{1}", bindPath, SourceField.Name);
            }
            DataTable dt = dtm.GetDataTable(strTableName);
            if (null != dt)
            {
                return GetPagesFromData(dt);
            }
            return 0;
        }

        public void DirectDraw(Canvas ca, float x, float y, float dpiZoom = 1f)
        {
            PrintPaint(ca.Graphics, new Rectangle((int)x, (int)y, Size.Width, Size.Height));
        }

        #region IElementExtended
        int IElementExtended.Height { get; set; }
        Point IElementExtended.Location { get; set; }
        int IElementExtended.Width { get; set; }
        #endregion

        IReportExpressionEngine IBindReportExpressionEngine.ExpressionEngine { get; set; }

        private void NotifyDesignSurfaceChange()
        {
            if (null != Site)
            {
                IComponentChangeService cs = Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                if (null != cs)
                {
                    cs.OnComponentChanged(this, null, null, null);
                }
            }
        }


        #region   ICustomTypeDescriptor   显式接口定义
        AttributeCollection ICustomTypeDescriptor.GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        string ICustomTypeDescriptor.GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        string ICustomTypeDescriptor.GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        TypeConverter ICustomTypeDescriptor.GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            if (null != this.Site)
                return this.FilterProperties(TypeDescriptor.GetProperties(this.GetType()));
            else
                return TypeDescriptor.GetProperties(this.GetType());
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
        {
            if (null != this.Site)
            {
                return this.FilterProperties(TypeDescriptor.GetProperties(this.GetType(), attributes));
            }
            else
                return TypeDescriptor.GetProperties(this.GetType(), attributes);
        }

        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        #region 属性过滤

        private PropertyDescriptorCollection FilterProperties(PropertyDescriptorCollection properties)
        {
            if (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.ReadingXmlInDesignerTime == true)
                return TypeDescriptor.GetProperties(this.GetType());

            PropertyDescriptorCollection tmpPDC = properties;
            System.Reflection.PropertyInfo[] pis = this.GetType().GetProperties();
            ArrayList props = new ArrayList();
            foreach (PropertyDescriptor pdes in tmpPDC)
            {
                System.Reflection.PropertyInfo pi = pis.First(o => o.Name == pdes.Name);
                if (pdes.Name == "Width" || pdes.Name == "Height" || pi.DeclaringType == this.GetType())
                    props.Add(new GlobalizedPropertyDescriptor(pdes));
            }

            GlobalizedPropertyDescriptor[] propArray =
                (GlobalizedPropertyDescriptor[])props.ToArray(typeof(GlobalizedPropertyDescriptor));
            tmpPDC = new PropertyDescriptorCollection(propArray);
            return tmpPDC;

        }

        #endregion

        #endregion


    }
    internal class DataTableEditor : UITypeEditor
    {
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            try
            {
                if (null != context && null != context.Instance && null != context.Container)
                {
                    SectionChart element = context.Instance as SectionChart;
                    if (null != element)
                    {
                        PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField sf = (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetCurrentReportDataDefine()) as PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField;
                        //SourceBindDialog fbd = new SourceBindDialog(sfAll, element.SourceField);
                        PMS.Libraries.ToolControls.Report.Controls.EditorDialog.SourceBindDialog fbd = new PMS.Libraries.ToolControls.Report.Controls.EditorDialog.SourceBindDialog(sf, element.SourceField, true);
                        if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            bool contain = false;
                            if (fbd.SourceField != null)
                            {
                                FieldTreeViewData sfAll = (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetCurrentReportDataDefine()) as FieldTreeViewData;
                                List<SourceField> lpdb = fbd.SourceField.GetSubSourceField(sfAll);
                                foreach (SourceField pdb in lpdb)
                                {
                                    try
                                    {
                                        if (!string.IsNullOrEmpty(pdb.DataType))
                                        {
                                            string typ = pdb.DataType.ToUpper();
                                            if (typ.Equals("INT", StringComparison.InvariantCultureIgnoreCase) ||
                                            typ.Equals("FLOAT", StringComparison.InvariantCultureIgnoreCase) ||
                                            typ.Equals("REAL", StringComparison.InvariantCultureIgnoreCase) ||
                                            typ.Equals("INT32", StringComparison.InvariantCultureIgnoreCase) ||
                                            typ.Equals("INT16", StringComparison.InvariantCultureIgnoreCase) ||
                                            typ.Equals("INT64", StringComparison.InvariantCultureIgnoreCase) ||
                                            typ.Equals("SYSTEM.SINGLE", StringComparison.InvariantCultureIgnoreCase) ||
                                            typ.Equals("SYSTEM.DOUBLE", StringComparison.InvariantCultureIgnoreCase) ||
                                            typ.Equals("SYSTEM.INT32", StringComparison.InvariantCultureIgnoreCase) ||
                                            typ.Equals("SYSTEM.DECIMAL", StringComparison.InvariantCultureIgnoreCase) ||
                                            typ.Equals("SYSTEM.DateTime", StringComparison.InvariantCultureIgnoreCase))
                                            {
                                                contain = true;
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        throw new Exception("lpdb");
                                    }
                                }
                                if (contain)
                                    value = fbd.SourceField;
                                else
                                    MessageBox.Show("所选数据集无法分段！");
                            }
                            else
                                value = null;
                            IComponentChangeService cs = provider.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                            if (null != cs)
                            {
                                cs.OnComponentChanged(this, null, null, null);
                            }
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
                    SectionChart control = null;
                    if (context.Instance is SectionChart)
                        control = context.Instance as SectionChart;

                    SectionApperence form1 = new SectionApperence();

                    form1.ChartParent = control as SectionChart;
                    form1.isIntial = control.isIntial;
                    DataSource ds = control.Apperence.Clone();
                    form1.Annotations = ds.annotationList;
                    form1.seriesList = ds.SeriesList;
                    form1.titleList = ds.TitleList;
                    DialogResult dr = editorService.ShowDialog(form1);
                    if (DialogResult.OK == dr)
                    {
                        value = ds;
                        control.isIntial = true;
                    }
                    else if (DialogResult.Cancel == dr)
                    {
                        if (form1.isApply)
                        {
                            if (form1.dsApply != null)
                                value = ds;
                            control.isIntial = true;
                        }

                    }
                    return value;
                }
            }

            return value;
        }
    }

    internal class SectionValueXY
    {
        public SectionValueXY(string X, double Y)
        {
            Xvalue = X;
            Yvalue = Y;
        }

        string xvalue;

        public string Xvalue
        {
            get { return xvalue; }
            set { xvalue = value; }
        }

        double yvalue;

        public double Yvalue
        {
            get { return yvalue; }
            set { yvalue = value; }
        }
    }
}
