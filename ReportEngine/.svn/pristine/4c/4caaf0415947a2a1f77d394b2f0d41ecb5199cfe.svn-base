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
	[ToolboxBitmap (typeof(BarChart), "Resources.BarChart.png")]
	[Designer (typeof(MESDesigner))]
	[DefaultProperty ("SourceField")]
	public partial class BarChart : ChartBase
	{
		#region Public Property

		[Category ("通用")]
		[Description ("控件名字")]
		[Browsable (true)]
		public new string Name {
			get { return base.Name; }
			set { base.Name = value; }
		}

		[Category ("通用")]
		[Description ("控件位置")]
		[Browsable (true)]
		public new Point Location {
			get { return base.Location; }
			set { base.Location = value; }
		}

		[Category ("通用")]
		[Description ("控件大小")]
		[Browsable (true)]
		public new Size Size {
			get { return base.Size; }
			set { base.Size = value; }
		}

		#region 字段和属性

		[Category ("通用")]
		[Description ("外观设置")]
		[Editor (typeof(BarApperenceEditor), typeof(UITypeEditor))]
		public new DataSource Apperence {
			get { return base.Apperence; }
			set { base.Apperence = value; }
		}

		[Category ("通用")]
		[Description ("分组")]
		[Browsable (false)]
		public new GroupSource GroupSource {
			get { return base.GroupSource; }
			set { base.GroupSource = value; }
		}

		private YAxisArea _YAxisArea = new YAxisArea ();

		[Category ("通用")]
		[Description ("多轴")]
		[Browsable (false)]
		public YAxisArea YAxisAreas {
			get { return _YAxisArea; }
			set {
				_YAxisArea = value;
				InitailColumnData ();
			}
		}

		[Category ("通用")]
		[Description ("绑定数据")]
		[DisplayName ("Binding")]
		public new SourceField SourceField {
			get { return base.SourceField; }
			set { base.SourceField = value; }
		}

		private BarChartType _ChartType = BarChartType.Column;

		[Category ("通用")]
		[Description ("图表类型")]
		public BarChartType ChartType {
			get {
				return _ChartType;
			}
			set {
				_ChartType = value;
				InitailColumnData ();
			}
		}

		#endregion

		#endregion

		public BarChart ()
			: base ()
		{
			InitializeComponent ();
		}

		public BarChart (MemoryStream Aim)
			: base (Aim)
		{
		}

		public override object Clone ()
		{
			BarChart pcc = new BarChart ();
			if (this.SourceField != null)
				pcc.SourceField = this.SourceField.Clone ();

			pcc.Apperence = this.Apperence.Clone ();
			if (this.Apperence.SeriesList.Count != 0) {
				pcc.Apperence.SeriesList [0].Legend = this.Apperence.SeriesList [0].Legend;
			}

			pcc.RunMode = this.RunMode;
			pcc.Location = new Point (this.Location.X, this.Location.Y);
			pcc.OriginPosition = new Point (this.Location.X, this.Location.Y);
			pcc.Height = this.Height;
			pcc.Width = this.Width;
			pcc.ChartType = this.ChartType;
			pcc.GroupSource = this.GroupSource.Clone ();
			pcc.YAxisAreas = this.YAxisAreas.Clone ();
			if (this.OriginHeight > 0 || this.OriginWidth > 0) {
				pcc.OriginHeight = this.OriginHeight;
				pcc.OriginWidth = this.OriginWidth;
			} else {
				pcc.OriginWidth = this.Width;
				pcc.OriginHeight = this.Height;
			}

			return pcc;
		}

		protected override void DrawVirtualChart ()
		{
			try {
				InitChart ();
				ChartArea ChartArea1 = new ChartArea ("ChartArea1");
				Legend legend1 = new Legend ();
				if (this.Apperence != null) {
					//绑定绘图区
					if (this.Apperence.ChartAreaList.Count != 0) {
						this.Apperence.ChartAreaList [0].SetChartArea (ChartArea1);
					}
					//绑定标题
					if (this.Apperence.TitleList.Count != 0) {
						foreach (PMSTitle item in this.Apperence.TitleList) {
							Title title1 = new Title ();
							item.SetTitle (title1);
							chart1.Titles.Add (title1);
						}
					}
					//绑定图例
					if (this.Apperence.LegendList.Count != 0) {
						this.Apperence.LegendList [0].SetLegend (legend1);
					}

					//绑定数据
					string[] Xvalues = {
						"Australia",
						"Brasil",
						"Canada",
						"Danmark",
						"Ecuador"
					};
					Random random = new Random ();
					for (int i = 0; i < 3; i++) {
						Series series1 = new Series ();
						series1.SetCustomProperty ("BarLabelStyle", "Disabled");
						if (this.Apperence.SeriesList.Count != 0)
							this.Apperence.SeriesList [0].SetSeriseStyle (series1);
						series1 ["PointWidth"] = ((series1 ["TotalPointWidth"] == null) ? 0.8 : Convert.ToDouble (series1 ["TotalPointWidth"])).ToString ();

						double[] Yvalues = new double[5];
						for (int j = 0; j < 5; j++) {
							Yvalues [j] = (50.0 * random.Next (0, 1000) / 1000.0);
						}
						series1.ChartType = (SeriesChartType)ChartType;
						series1.Points.DataBindXY (Xvalues, Yvalues);
						chart1.Series.Add (series1);
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
				chart1.ChartAreas.Add (ChartArea1);
				chart1.Legends.Add (legend1);
			} catch (Exception e) {
				throw e; // new Exception ("DrawVirtualChart");
			}
		}

		protected override void PrintPaint (Graphics graphics, Rectangle position)
		{
			try {
				chart1.Printing.PrintPaint (graphics, position);

				//轴描述（可以理解为单位）
				if (YAxisAreas.Enable) {
					for (int i = 1; i < chart1.ChartAreas.Count; i += 2) {
						if (chart1.ChartAreas [i + 1].AxisY.Tag != null) {
							RectangleF rec = new RectangleF (position.X + position.Width * (chart1.ChartAreas [i].Position.X - 5) / 100,
								                 position.Y + position.Height * (chart1.ChartAreas [i].Position.Y - 10) / 100,
								                 position.Width * 10 / 100,
								                 position.Height * 10 / 100);
							StringFormat sf = new StringFormat ();
							sf.Alignment = StringAlignment.Far;
							sf.LineAlignment = StringAlignment.Far;
							graphics.DrawString (chart1.ChartAreas [i + 1].AxisY.Tag.ToString (), chart1.ChartAreas [i].AxisY.LabelStyle.Font, new SolidBrush (chart1.ChartAreas [i].AxisY.LabelStyle.ForeColor), rec, sf);
						}
					}
				}
			} catch (Exception ex) {
				PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error (ex.Message);
			}
		}

		protected override void SetData (DataTable Aim, int Index)
		{
			if (GroupSource.Enable)
				SetGroupData (Aim);
			else if (YAxisAreas.Enable)
				SetYAxisData (Aim);
			else
				SetUnGroupData (Aim);
		}

		/// <summary>
		/// 采用多轴方式配置数据源
		/// </summary>
		/// <param name="Aim">数据源</param>
		private void SetYAxisData (DataTable Aim)
		{
			OriginPosition = new Point (this.Location.X, this.Location.Y);
			if (this.DesignMode == true) {
				DrawVirtualChart ();
			} else if (Aim == null || Aim.Rows.Count == 0) {
				DrawVirtualChart ();
			} else {
				try {
					if (this.Apperence != null && this.Apperence.SeriesList != null && this.Apperence.SeriesList.Count > 0) {
						if (ChartType != BarChartType.Column) {
							PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error ("当前图表类型不支持多轴！");
							return;
						}
						Series series1 = new Series ();
						//是否绑定数据源，绑定的数据源是否存在
						this.Apperence.SeriesList [0].SetSeriesValue (series1);
						if (string.IsNullOrEmpty (series1.GetCustomProperty ("XBindingField"))) {
							PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error (Properties.Resources.ResourceManager.GetString ("message0016"));
							return;
						}
						if (!Aim.Columns.Contains (series1.GetCustomProperty ("XBindingField"))) {
							PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error (Properties.Resources.ResourceManager.GetString ("message0017"));
							return;
						}
					}

					InitChart ();
					ChartArea markerArea = new ChartArea ("ChartArea1");//作为参考的ChartArea
					markerArea.BackColor = Color.Transparent;
					markerArea.AxisX.MajorGrid.LineColor = Color.Transparent;
					markerArea.AxisX.MajorTickMark.LineColor = Color.Transparent;
					markerArea.AxisX.LineColor = Color.Transparent;
					markerArea.AxisX.LabelStyle.ForeColor = Color.Transparent;
					markerArea.AxisY.MajorGrid.LineColor = Color.Transparent;
					markerArea.AxisY.MajorTickMark.LineColor = Color.Transparent;
					markerArea.AxisY.LineColor = Color.Transparent;
					markerArea.AxisY.LabelStyle.ForeColor = Color.Transparent;
					chart1.ChartAreas.Add (markerArea);

					Legend legend1 = new Legend ();
					//绑定图例
					if (this.Apperence.LegendList.Count != 0) {
						this.Apperence.LegendList [0].SetLegend (legend1);
					}
					chart1.Legends.Add (legend1);

					Series markerSeries = new Series ("MarkerSeries");//用于填充markerArea
					markerSeries.ChartArea = markerArea.Name;
					markerSeries.Legend = legend1.Name;
					markerSeries.Color = Color.Transparent;
					markerSeries.Points.AddY (10.0);
					chart1.Series.Add (markerSeries);

					//绑定标题
					if (this.Apperence.TitleList.Count != 0) {
						foreach (PMSTitle item in this.Apperence.TitleList) {
							Title title1 = new Title ();
							item.SetTitle (title1);
							chart1.Titles.Add (title1);
						}
					}

					//先绘制一遍，获取到绘图区域的大小
					chart1.Printing.PrintPaint (this.CreateGraphics (), this.ClientRectangle);

					//隐藏作为参照的曲线
					markerSeries.IsVisibleInLegend = false;
					//chart1.Series.Clear();

					//绑定数据
					if (this.Apperence.SeriesList.Count != 0) {
						int yAxisCount = 0;
						List<string> Xvalues = new List<string> ();
						for (int i = this.Apperence.SeriesList.Count; i > 0; i--) {
							Series series1 = new Series ();
							this.Apperence.SeriesList [i - 1].SetSeriesValue (series1);
							if (!string.IsNullOrEmpty (series1 ["YAxisAreaName"])) {
								Xvalues.Clear ();
								List<double> Yvalues = new List<double> ();
								//series1["PointWidth"] = ((series1["TotalPointWidth"] == null) ? 0.8 / this.Apperence.SeriesList.Count : Convert.ToDouble(series1["TotalPointWidth"]) / this.Apperence.SeriesList.Count).ToString();
								for (int j = 0; j < Aim.Rows.Count; j++) {
									Xvalues.Add (Aim.Rows [j] [series1.GetCustomProperty ("XBindingField")].ToString ());
									Yvalues.Add (Convert.ToDouble (Aim.Rows [j] [series1.GetCustomProperty ("YBindingField")]));
								}

								if (Xvalues.Count != 0 && Yvalues.Count != 0) {
									series1.Points.DataBindXY (Xvalues, Yvalues);
								}
								series1.ChartType = (SeriesChartType)ChartType;
								chart1.Series.Add (series1);

								//准备绘图区，初始化显示位置
								//轴区域
								ChartArea yAxisArea = YAxisAreas.GetYAxisAreaByName (this.Apperence.ChartAreaList, series1 ["YAxisAreaName"]);
								yAxisArea.InnerPlotPosition.Auto = false;
								yAxisArea.InnerPlotPosition.FromRectangleF (markerArea.InnerPlotPosition.ToRectangleF ());
								yAxisArea.Position.Auto = false;
								yAxisArea.Position.FromRectangleF (markerArea.Position.ToRectangleF ());

								//曲线区域
								ChartArea seriesArea = YAxisAreas.GetYAxisAreaByName (this.Apperence.ChartAreaList, "Series_" + series1 ["YAxisAreaName"]);
								seriesArea.InnerPlotPosition.Auto = false;
								seriesArea.InnerPlotPosition.FromRectangleF (markerArea.InnerPlotPosition.ToRectangleF ());
								seriesArea.Position.Auto = false;
								seriesArea.Position.FromRectangleF (markerArea.Position.ToRectangleF ());

								//数据序列备份，用于填充轴区域
								Series seriesCopy = new Series (series1.Name + "_Copy");
								seriesCopy.ChartType = series1.ChartType;
								seriesCopy.Color = Color.Transparent;
								seriesCopy.IsVisibleInLegend = false;
								if (Xvalues.Count != 0 && Yvalues.Count != 0) {
									seriesCopy.Points.DataBindXY (Xvalues, Yvalues);
								}
								chart1.Series.Add (seriesCopy);

								if (chart1.ChartAreas.FindByName (series1 ["YAxisAreaName"]) == null) {
									chart1.ChartAreas.Add (yAxisArea);
									chart1.ChartAreas.Add (seriesArea);
									yAxisCount++;
									chart1.ChartAreas.FindByName (seriesArea.Name).Tag = (0).ToString ();
								}
								series1.ChartArea = seriesArea.Name;
								seriesCopy.ChartArea = yAxisArea.Name;
								chart1.ChartAreas.FindByName (seriesArea.Name).Tag = Convert.ToInt32 (chart1.ChartAreas.FindByName (seriesArea.Name).Tag) + 1;
							}
						}

						//设置单个柱体的宽度比例
						for (int i = 1; i < this.chart1.Series.Count; i += 2) {
							this.chart1.Series [i] ["PointWidth"] = (Convert.ToDouble (chart1.ChartAreas.FindByName (this.chart1.Series [i].ChartArea).Tag)
							/ this.Apperence.SeriesList.Count
							* ((this.chart1.Series [i] ["TotalPointWidth"] == null) ? 0.8 : Convert.ToDouble (this.chart1.Series [i] ["TotalPointWidth"]))).ToString ();
						}

						for (int i = yAxisCount; i > 0; i--) {
							//轴区域宽度调整
							chart1.ChartAreas [i * 2 - 1].Position.Width -= ((yAxisCount - 1) * YAxisAreas.Offset);
							//曲线区域宽度调整
							chart1.ChartAreas [i * 2].Position.Width -= ((yAxisCount - 1) * YAxisAreas.Offset);
							//轴区域位置偏移
							chart1.ChartAreas [i * 2 - 1].Position.X += ((yAxisCount - i) * YAxisAreas.Offset);
						}

						//应用外观设置
						if (this.Apperence.ChartAreaList.Count != 0) {
							//应用轴外观到各个轴
							for (int i = 1; i < chart1.ChartAreas.Count; i += 2) {
								PMSChartArea yAxisArea = new PMSChartArea (YAxisAreas.GetYAxisAreaByName (this.Apperence.ChartAreaList, chart1.ChartAreas [i].Name));
								//chart1.ChartAreas[i].AxisY = this.Apperence.ChartAreaList[0].AxisY.ToAxis();
								chart1.ChartAreas [i].AxisY = yAxisArea.AxisY.ToAxis ();
								chart1.ChartAreas [i].AxisY.MajorGrid.LineColor = Color.Transparent;
								chart1.ChartAreas [i].AxisY.MinorGrid.LineColor = Color.Transparent;
								//chart1.ChartAreas[i].AxisY.LineColor = yAxisArea.AxisY.LineColor;
								//chart1.ChartAreas[i].AxisY.MajorTickMark.LineColor = yAxisArea.AxisY.MajorTickMark.LineColor;
								//chart1.ChartAreas[i].AxisY.MinorTickMark.LineColor = yAxisArea.AxisY.MinorTickMark.LineColor;
								//chart1.ChartAreas[i].AxisY.IsLabelAutoFit = yAxisArea.AxisY.IsLabelAutoFit;
								//chart1.ChartAreas[i].AxisY.LabelStyle = yAxisArea.AxisY.LabelStyle.ToLabelStyle();

								//应用最大值，最小值等到曲线区域
								chart1.ChartAreas [i + 1].AxisY.Maximum = yAxisArea.AxisY.Maximum;
								chart1.ChartAreas [i + 1].AxisY.Minimum = yAxisArea.AxisY.Minimum;
								chart1.ChartAreas [i + 1].AxisY.Interval = yAxisArea.AxisY.Interval;
								chart1.ChartAreas [i + 1].AxisY.IntervalType = yAxisArea.AxisY.IntervalType;
								chart1.ChartAreas [i + 1].AxisY.MajorGrid.Interval = yAxisArea.AxisY.MajorGrid.Interval;
								chart1.ChartAreas [i + 1].AxisY.MajorGrid.IntervalType = yAxisArea.AxisY.MajorGrid.IntervalType;
								chart1.ChartAreas [i + 1].AxisY.MinorGrid.Interval = yAxisArea.AxisY.MinorGrid.Interval;
								chart1.ChartAreas [i + 1].AxisY.MinorGrid.IntervalType = yAxisArea.AxisY.MinorGrid.IntervalType;
								chart1.ChartAreas [i + 1].AxisY.MajorTickMark.Interval = yAxisArea.AxisY.MajorTickMark.Interval;
								chart1.ChartAreas [i + 1].AxisY.MajorTickMark.IntervalType = yAxisArea.AxisY.MajorTickMark.IntervalType;
								chart1.ChartAreas [i + 1].AxisY.MinorTickMark.Interval = yAxisArea.AxisY.MinorTickMark.Interval;
								chart1.ChartAreas [i + 1].AxisY.MinorTickMark.IntervalType = yAxisArea.AxisY.MinorTickMark.IntervalType;
							}

							//应用整体外观设置到最低层的轴区域
							chart1.ChartAreas [1].BackColor = this.Apperence.ChartAreaList [0].BackColor;
							chart1.ChartAreas [1].BackGradientStyle = this.Apperence.ChartAreaList [0].BackGradientStyle;
							chart1.ChartAreas [1].BackHatchStyle = this.Apperence.ChartAreaList [0].BackHatchStyle;
							chart1.ChartAreas [1].BackSecondaryColor = this.Apperence.ChartAreaList [0].BackSecondaryColor;
							chart1.ChartAreas [1].BorderColor = this.Apperence.ChartAreaList [0].BorderColor;
							chart1.ChartAreas [1].BorderDashStyle = this.Apperence.ChartAreaList [0].BorderDashStyle;
							chart1.ChartAreas [1].BorderWidth = this.Apperence.ChartAreaList [0].BorderWidth;
							//chart1.ChartAreas[1].Area3DStyle = this.Apperence.ChartAreaList[0].Area3DStyle.ToChartArea3DStyle();

							chart1.ChartAreas [1].AxisY.MajorGrid.Enabled = this.Apperence.ChartAreaList [0].AxisY.MajorGrid.Enabled;
							chart1.ChartAreas [1].AxisY.MajorGrid.LineColor = this.Apperence.ChartAreaList [0].AxisY.MajorGrid.LineColor;
							chart1.ChartAreas [1].AxisY.MajorGrid.LineDashStyle = this.Apperence.ChartAreaList [0].AxisY.MajorGrid.LineDashStyle;
							chart1.ChartAreas [1].AxisY.MajorGrid.LineWidth = this.Apperence.ChartAreaList [0].AxisY.MajorGrid.LineWidth;

							chart1.ChartAreas [1].AxisY.MinorGrid.Enabled = this.Apperence.ChartAreaList [0].AxisY.MinorGrid.Enabled;
							chart1.ChartAreas [1].AxisY.MinorGrid.LineColor = this.Apperence.ChartAreaList [0].AxisY.MinorGrid.LineColor;
							chart1.ChartAreas [1].AxisY.MinorGrid.LineDashStyle = this.Apperence.ChartAreaList [0].AxisY.MinorGrid.LineDashStyle;
							chart1.ChartAreas [1].AxisY.MinorGrid.LineWidth = this.Apperence.ChartAreaList [0].AxisY.MinorGrid.LineWidth;

							chart1.ChartAreas [1].AxisX = this.Apperence.ChartAreaList [0].AxisX.ToAxis ();

							// Set X axis custom labels
							double dLableStart = 0.5;
							double dInterval = 1.0;
							foreach (string sLabel in Xvalues) {
								CustomLabel element = chart1.ChartAreas [1].AxisX.CustomLabels.Add (dLableStart, dLableStart + dInterval, sLabel);
								element.GridTicks = GridTickTypes.TickMark;
								dLableStart += dInterval;
							}

							PMSChartArea yaxis1 = new PMSChartArea (YAxisAreas.GetYAxisAreaByName (this.Apperence.ChartAreaList, chart1.ChartAreas [1].Name));
							chart1.ChartAreas [1].AxisY.LineColor = yaxis1.AxisY.LineColor;
							chart1.ChartAreas [1].AxisY.MajorTickMark.LineColor = yaxis1.AxisY.MajorTickMark.LineColor;
							chart1.ChartAreas [1].AxisY.MinorTickMark.LineColor = yaxis1.AxisY.MinorTickMark.LineColor;
							chart1.ChartAreas [1].AxisY.IsLabelAutoFit = yaxis1.AxisY.IsLabelAutoFit;
							chart1.ChartAreas [1].AxisY.LabelStyle = yaxis1.AxisY.LabelStyle.ToLabelStyle ();

							////Y轴的轴标题只显示最左边一个
							//for (int i = yAxisCount; i > 0; i--)
							//{
							//    if (i != yAxisCount)
							//        chart1.ChartAreas[i * 2 - 1].AxisY.Title = string.Empty;
							//}


						}

						float majorStep = chart1.ChartAreas [1].Position.Width / (Aim.Rows.Count + 1);
						float barLenth = majorStep * Convert.ToSingle (chart1.Series [1] ["TotalPointWidth"]) / this.Apperence.SeriesList.Count;

						//曲线位置偏移
						float seriesOffset = 0;
						for (int i = 1; i <= yAxisCount; i++) {
							chart1.ChartAreas [i * 2].Position.X += ((yAxisCount - 1) * YAxisAreas.Offset);
							chart1.ChartAreas [i * 2].Position.X -= (this.Apperence.SeriesList.Count - Convert.ToSingle (chart1.ChartAreas [i * 2].Tag)) / 2 * barLenth;
							chart1.ChartAreas [i * 2].Position.X += seriesOffset;
							seriesOffset += barLenth * Convert.ToInt32 (chart1.ChartAreas [i * 2].Tag);
						}

					}

					if (this.Apperence.PMSChartAppearance != null) {
						chart1.BorderlineColor = this.Apperence.PMSChartAppearance.BorderlineColor;
						chart1.BorderlineDashStyle = this.Apperence.PMSChartAppearance.BorderlineDashStyle;
						chart1.BorderlineWidth = this.Apperence.PMSChartAppearance.BorderlineWidth;
						chart1.BackColor = this.Apperence.PMSChartAppearance.BackColor;
						chart1.BackSecondaryColor = this.Apperence.PMSChartAppearance.BackSecondaryColor;
						chart1.BackHatchStyle = this.Apperence.PMSChartAppearance.BackHatchStyle;
						chart1.BackGradientStyle = this.Apperence.PMSChartAppearance.BackGradientStyle;
					}
				} catch (System.Exception ex) {
					InitChart ();
					PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error (ex.Message);
				}
			}
		}

		/// <summary>
		/// 采用自定义方式配置数据源
		/// </summary>
		/// <param name="Aim">数据源</param>
		private void SetUnGroupData (DataTable Aim)
		{
			OriginPosition = new Point (this.Location.X, this.Location.Y);
			if (this.DesignMode == true) {
				DrawVirtualChart ();
			} else if (Aim == null || Aim.Rows.Count == 0) {
				DrawVirtualChart ();
			} else {
				try {
					ChartArea ChartArea1 = new ChartArea ("ChartArea1");
					Legend legend1 = new Legend ();
					if (this.Apperence != null && this.Apperence.SeriesList != null && this.Apperence.SeriesList.Count > 0) {
						Series series1 = new Series ();
						//是否绑定数据源，绑定的数据源是否存在
						this.Apperence.SeriesList [0].SetSeriesValue (series1);
						if (string.IsNullOrEmpty (series1.GetCustomProperty ("XBindingField"))) {
							PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error (Properties.Resources.ResourceManager.GetString ("message0016"));
							return;
						}
						if (!Aim.Columns.Contains (series1.GetCustomProperty ("XBindingField"))) {
							PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error (Properties.Resources.ResourceManager.GetString ("message0017"));
							return;
						}
					}

					InitChart ();
					//绑定绘图区
					if (this.Apperence.ChartAreaList.Count != 0) {
						this.Apperence.ChartAreaList [0].SetChartArea (ChartArea1);
					}
					//绑定标题
					if (this.Apperence.TitleList.Count != 0) {
						foreach (PMSTitle item in this.Apperence.TitleList) {
							Title title1 = new Title ();
							item.SetTitle (title1);
							chart1.Titles.Add (title1);
						}
					}
					//绑定图例
					if (this.Apperence.LegendList.Count != 0) {
						this.Apperence.LegendList [0].SetLegend (legend1);
					}
					//绑定数据
					if (this.Apperence.SeriesList.Count != 0) {
						foreach (PMSSeries item in this.Apperence.SeriesList) {
							Series series1 = new Series ();
							List<string> Xvalues = new List<string> ();
							List<double> Yvalues = new List<double> ();
							item.SetSeriesValue (series1);
							series1 ["PointWidth"] = ((series1 ["TotalPointWidth"] == null) ? 0.8 : Convert.ToDouble (series1 ["TotalPointWidth"])).ToString ();
							for (int i = 0; i < Aim.Rows.Count; i++) {
								Xvalues.Add (Aim.Rows [i] [series1.GetCustomProperty ("XBindingField")].ToString ());
								Yvalues.Add (Convert.ToDouble (Aim.Rows [i] [series1.GetCustomProperty ("YBindingField")]));
							}

							if (Xvalues.Count != 0 && Yvalues.Count != 0) {
								series1.Points.DataBindXY (Xvalues, Yvalues);
							}
							series1.ChartType = (SeriesChartType)ChartType;
							chart1.Series.Add (series1);
						}
					}
					chart1.ChartAreas.Add (ChartArea1);
					chart1.Legends.Add (legend1);
					if (this.Apperence.PMSChartAppearance != null) {
						chart1.BorderlineColor = this.Apperence.PMSChartAppearance.BorderlineColor;
						chart1.BorderlineDashStyle = this.Apperence.PMSChartAppearance.BorderlineDashStyle;
						chart1.BorderlineWidth = this.Apperence.PMSChartAppearance.BorderlineWidth;
						chart1.BackColor = this.Apperence.PMSChartAppearance.BackColor;
						chart1.BackSecondaryColor = this.Apperence.PMSChartAppearance.BackSecondaryColor;
						chart1.BackHatchStyle = this.Apperence.PMSChartAppearance.BackHatchStyle;
						chart1.BackGradientStyle = this.Apperence.PMSChartAppearance.BackGradientStyle;
					}
				} catch (System.Exception ex) {
					InitChart ();
					PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error (ex.Message);
				}
			}
		}

		/// <summary>
		/// 采用分组方式配置数据源
		/// </summary>
		/// <param name="Aim">数据源</param>
		private void SetGroupData (DataTable Aim)
		{
			if (this.Apperence != null && this.Apperence.SeriesList != null && this.Apperence.SeriesList.Count > 0) {
				//是否绑定数据源，绑定的数据源是否存在
				if (string.IsNullOrEmpty (GroupSource.MajorBinding)) {
					PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error ("未绑定分组-主分类字段！");
					return;
				}
				if (string.IsNullOrEmpty (GroupSource.ValueBinding)) {
					PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error ("未绑定分组-统计字段！");
					return;
				}
			}
			ArrayList columnList = new ArrayList ();
			if (!Aim.Columns.Contains (GroupSource.MajorBinding)) {
				if (GroupSource.MajorBinding.EndsWith ("_Year")) {
					CreateSplitDateFiled (Aim, GroupSource.MajorBinding, columnList, "Year");
				} else if (GroupSource.MajorBinding.EndsWith ("Month")) {
					CreateSplitDateFiled (Aim, GroupSource.MajorBinding, columnList, "Month");
				} else if (GroupSource.MajorBinding.EndsWith ("Day")) {
					CreateSplitDateFiled (Aim, GroupSource.MajorBinding, columnList, "Day");
				} else if (GroupSource.MajorBinding.EndsWith ("Hour")) {
					CreateSplitDateFiled (Aim, GroupSource.MajorBinding, columnList, "Hour");
				} else if (GroupSource.MajorBinding.EndsWith ("Minute")) {
					CreateSplitDateFiled (Aim, GroupSource.MajorBinding, columnList, "Minute");
				} else if (GroupSource.MajorBinding.EndsWith ("Second")) {
					CreateSplitDateFiled (Aim, GroupSource.MajorBinding, columnList, "Second");
				} else
					return;
			}

			string strValueFx;
			switch (GroupSource.ValueFx) {
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

			DataSetHelper ds = new DataSetHelper ();
			List<DataTable> dtList = new List<DataTable> ();
			DataTable tbMajor = Aim.DefaultView.ToTable (true, GroupSource.MajorBinding);
			if (GroupSource.MajorSort == SortType.Asc) {
				tbMajor.DefaultView.Sort = GroupSource.MajorBinding + " Asc";
				tbMajor = tbMajor.DefaultView.ToTable ();
			}
			if (GroupSource.MajorSort == SortType.Desc) {
				tbMajor.DefaultView.Sort = GroupSource.MajorBinding + " Desc";
				tbMajor = tbMajor.DefaultView.ToTable ();
			}
			if (!string.IsNullOrEmpty (GroupSource.MinorBinding)) {
				if (!Aim.Columns.Contains (GroupSource.MinorBinding)) {
					if (GroupSource.MinorBinding.EndsWith ("_Year")) {
						CreateSplitDateFiled (Aim, GroupSource.MinorBinding, columnList, "Year");
					} else if (GroupSource.MinorBinding.EndsWith ("Month")) {
						CreateSplitDateFiled (Aim, GroupSource.MinorBinding, columnList, "Month");
					} else if (GroupSource.MinorBinding.EndsWith ("Day")) {
						CreateSplitDateFiled (Aim, GroupSource.MinorBinding, columnList, "Day");
					} else if (GroupSource.MinorBinding.EndsWith ("Hour")) {
						CreateSplitDateFiled (Aim, GroupSource.MinorBinding, columnList, "Hour");
					} else if (GroupSource.MinorBinding.EndsWith ("Minute")) {
						CreateSplitDateFiled (Aim, GroupSource.MinorBinding, columnList, "Minute");
					} else if (GroupSource.MinorBinding.EndsWith ("Second")) {
						CreateSplitDateFiled (Aim, GroupSource.MinorBinding, columnList, "Second");
					} else
						return;
				}

				DataTable tbMinor = Aim.DefaultView.ToTable (true, GroupSource.MinorBinding);
				if (GroupSource.MinorSort == SortType.Asc) {
					tbMinor.DefaultView.Sort = GroupSource.MinorBinding + " Asc";
					tbMinor = tbMinor.DefaultView.ToTable ();
				}
				if (GroupSource.MinorSort == SortType.Desc) {
					tbMinor.DefaultView.Sort = GroupSource.MinorBinding + " Desc";
					tbMinor = tbMinor.DefaultView.ToTable ();
				}

				foreach (DataRow row in tbMinor.Rows) {
					if (row [0] != DBNull.Value)
						dtList.Add (ds.SelectGroupByInto (row [0].ToString (), Aim, GroupSource.MajorBinding + "," + strValueFx + "(" + GroupSource.ValueBinding + ")", GroupSource.MinorBinding + "='" + row [0].ToString () + "'", GroupSource.MajorBinding));
					else
						dtList.Add (ds.SelectGroupByInto (row [0].ToString (), Aim, GroupSource.MajorBinding + "," + strValueFx + "(" + GroupSource.ValueBinding + ")", GroupSource.MinorBinding + " is null ", GroupSource.MajorBinding));
				}
			} else
				dtList.Add (ds.SelectGroupByInto (Aim.TableName, Aim, GroupSource.MajorBinding + "," + strValueFx + "(" + GroupSource.ValueBinding + ")", "1=1", GroupSource.MajorBinding));


			OriginPosition = new Point (this.Location.X, this.Location.Y);
			if (this.DesignMode == true) {
				DrawVirtualChart ();
			} else if (Aim == null || Aim.Rows.Count == 0) {
				DrawVirtualChart ();
			} else {
				try {
					ChartArea ChartArea1 = new ChartArea ("ChartArea1");
					Legend legend1 = new Legend ();

					InitChart ();
					//绑定绘图区
					if (this.Apperence.ChartAreaList.Count != 0) {
						this.Apperence.ChartAreaList [0].SetChartArea (ChartArea1);
					}
					//绑定标题
					if (this.Apperence.TitleList.Count != 0) {
						foreach (PMSTitle item in this.Apperence.TitleList) {
							Title title1 = new Title ();
							item.SetTitle (title1);
							chart1.Titles.Add (title1);
						}
					}
					//绑定图例
					if (this.Apperence.LegendList.Count != 0) {
						this.Apperence.LegendList [0].SetLegend (legend1);
					}
					//绑定数据
					if (this.Apperence.SeriesList.Count != 0) {
						foreach (DataTable dt in dtList) {
							Series series1 = new Series (dt.TableName);
							this.Apperence.SeriesList [0].SetSeriseStyle (series1);
							series1 ["PointWidth"] = ((series1 ["TotalPointWidth"] == null) ? 0.8 : Convert.ToDouble (series1 ["TotalPointWidth"])).ToString ();
							List<string> Xvalues = new List<string> ();
							List<double> Yvalues = new List<double> ();
							for (int i = 0; i < tbMajor.Rows.Count; i++) {
								Xvalues.Add (tbMajor.Rows [i] [0].ToString ());
								Yvalues.Add (0);
								for (int j = 0; j < dt.Rows.Count; j++) {
									if (tbMajor.Rows [i] [0].ToString () == dt.Rows [j] [0].ToString ())
										Yvalues [i] = Convert.ToDouble (dt.Rows [j] [1]);
								}
							}

							if (Xvalues.Count != 0 && Yvalues.Count != 0) {
								series1.Points.DataBindXY (Xvalues, Yvalues);
							}
							series1.ChartType = (SeriesChartType)ChartType;
							chart1.Series.Add (series1);
						}
					}

					chart1.ChartAreas.Add (ChartArea1);
					chart1.Legends.Add (legend1);
					#region 多轴显示（验证代码,已废弃）
					//多轴显示（验证代码）

					//InitChart();
					//ChartArea markerArea = new ChartArea("ChartArea1");//作为参考的ChartArea
					//markerArea.BackColor = Color.Transparent;
					//markerArea.AxisX.MajorGrid.LineColor = Color.Transparent;
					//markerArea.AxisX.MajorTickMark.LineColor = Color.Transparent;
					//markerArea.AxisX.LineColor = Color.Transparent;
					//markerArea.AxisX.LabelStyle.ForeColor = Color.Transparent;
					//markerArea.AxisY.MajorGrid.LineColor = Color.Transparent;
					//markerArea.AxisY.MajorTickMark.LineColor = Color.Transparent;
					//markerArea.AxisY.LineColor = Color.Transparent;
					//markerArea.AxisY.LabelStyle.ForeColor = Color.Transparent;
					//chart1.ChartAreas.Add(markerArea);

					//Legend legend1 = new Legend();
					////绑定图例
					//if (this.Apperence.LegendList.Count != 0)
					//{
					//    this.Apperence.LegendList[0].SetLegend(legend1);
					//}
					//chart1.Legends.Add(legend1);

					//Series markerSeries = new Series("MarkerSeries");//用于填充markerArea
					//markerSeries.ChartArea = markerArea.Name;
					//markerSeries.Legend = legend1.Name;
					//markerSeries.Color = Color.Transparent;
					//markerSeries.Points.AddY(10.0);
					//chart1.Series.Add(markerSeries);

					////绑定标题
					//if (this.Apperence.TitleList.Count != 0)
					//{
					//    foreach (PMSTitle item in this.Apperence.TitleList)
					//    {
					//        Title title1 = new Title();
					//        item.SetTitle(title1);
					//        chart1.Titles.Add(title1);
					//    }
					//}

					////先绘制一遍，获取到绘图区域的大小
					//chart1.Printing.PrintPaint(this.CreateGraphics(), this.ClientRectangle);

					////隐藏作为参照的曲线 
					//markerSeries.IsVisibleInLegend = false;
					////chart1.Series.Clear();

					////绑定数据
					//if (this.Apperence.SeriesList.Count != 0)
					//{
					//    int seriesCount = 0;
					//    float offset = 5f;
					//    foreach (DataTable dt in dtList)
					//    {
					//        Series series1 = new Series(dt.TableName);
					//        this.Apperence.SeriesList[0].SetSeriseStyle(series1);
					//        List<string> Xvalues = new List<string>();
					//        List<double> Yvalues = new List<double>();
					//        for (int i = 0; i < tbMajor.Rows.Count; i++)
					//        {
					//            Xvalues.Add(tbMajor.Rows[i][0].ToString());
					//            Yvalues.Add(0);
					//            for (int j = 0; j < dt.Rows.Count; j++)
					//            {
					//                if (tbMajor.Rows[i][0].ToString() == dt.Rows[j][0].ToString())
					//                    Yvalues[i] = Convert.ToDouble(dt.Rows[j][1]);
					//            }
					//        }

					//        if (Xvalues.Count != 0 && Yvalues.Count != 0)
					//        {
					//            series1.Points.DataBindXY(Xvalues, Yvalues);
					//        }
					//        series1.ChartType = (SeriesChartType)ChartType;
					//        seriesCount++;
					//        chart1.Series.Add(series1);
					//    }
					//    for (int i = seriesCount; i > 0; i--)
					//    {
					//        CreateArea(markerArea, chart1.Series[i], seriesCount);
					//        chart1.ChartAreas["YAxis_ChartArea_" + chart1.Series[i].Name].Position.Width -= ((seriesCount - 1) * offset);
					//        chart1.ChartAreas["ChartArea_" + chart1.Series[i].Name].Position.Width -= ((seriesCount - 1) * offset);
					//        chart1.ChartAreas["YAxis_ChartArea_" + chart1.Series[i].Name].Position.X += ((i - 1) * offset);
					//    }

					//    chart1.ChartAreas["YAxis_ChartArea_" + chart1.Series[seriesCount].Name].AxisX.MajorGrid.LineColor = Color.Black;
					//    chart1.ChartAreas["YAxis_ChartArea_" + chart1.Series[seriesCount].Name].AxisX.MajorTickMark.LineColor = Color.Black;
					//    chart1.ChartAreas["YAxis_ChartArea_" + chart1.Series[seriesCount].Name].AxisX.LineColor = Color.Black;
					//    chart1.ChartAreas["YAxis_ChartArea_" + chart1.Series[seriesCount].Name].AxisX.LabelStyle.ForeColor = Color.Black;
					//    chart1.ChartAreas["YAxis_ChartArea_" + chart1.Series[seriesCount].Name].AxisY.MajorGrid.LineColor = Color.Black;
					//    if (this.Apperence.ChartAreaList.Count != 0)
					//    {
					//        chart1.ChartAreas["YAxis_ChartArea_" + chart1.Series[seriesCount].Name].BackColor = this.Apperence.ChartAreaList[0].BackColor;
					//        chart1.ChartAreas["YAxis_ChartArea_" + chart1.Series[seriesCount].Name].BackGradientStyle = this.Apperence.ChartAreaList[0].BackGradientStyle;
					//        chart1.ChartAreas["YAxis_ChartArea_" + chart1.Series[seriesCount].Name].BackHatchStyle = this.Apperence.ChartAreaList[0].BackHatchStyle;
					//        chart1.ChartAreas["YAxis_ChartArea_" + chart1.Series[seriesCount].Name].BackSecondaryColor = this.Apperence.ChartAreaList[0].BackSecondaryColor;
					//        chart1.ChartAreas["YAxis_ChartArea_" + chart1.Series[seriesCount].Name].BorderColor = this.Apperence.ChartAreaList[0].BorderColor;
					//        chart1.ChartAreas["YAxis_ChartArea_" + chart1.Series[seriesCount].Name].BorderDashStyle = this.Apperence.ChartAreaList[0].BorderDashStyle;
					//        chart1.ChartAreas["YAxis_ChartArea_" + chart1.Series[seriesCount].Name].BorderWidth = this.Apperence.ChartAreaList[0].BorderWidth;
					//        //chart1.ChartAreas["YAxis_ChartArea_" + chart1.Series[seriesCount].Name].Area3DStyle = this.Apperence.ChartAreaList[0].Area3DStyle.ToChartArea3DStyle(); 

					//        chart1.ChartAreas["YAxis_ChartArea_" + chart1.Series[seriesCount].Name].AxisX = this.Apperence.ChartAreaList[0].AxisX.ToAxis();
					//        chart1.ChartAreas["YAxis_ChartArea_" + chart1.Series[seriesCount].Name].AxisY = this.Apperence.ChartAreaList[0].AxisY.ToAxis();

					//        for (int i = seriesCount; i > 0; i--)
					//        {
					//            chart1.ChartAreas["YAxis_ChartArea_" + chart1.Series[i].Name].AxisY = this.Apperence.ChartAreaList[0].AxisY.ToAxis();
					//            chart1.ChartAreas["YAxis_ChartArea_" + chart1.Series[i].Name].AxisY.MajorGrid.LineColor = Color.Transparent;
					//            chart1.ChartAreas["YAxis_ChartArea_" + chart1.Series[i].Name].AxisY.MinorGrid.LineColor = Color.Transparent;
					//            if (i != 1)
					//                chart1.ChartAreas["YAxis_ChartArea_" + chart1.Series[i].Name].AxisY.Title = string.Empty;
					//        }
					//    }

					//    float majorStep = chart1.ChartAreas["ChartArea_" + chart1.Series[seriesCount].Name].Position.Width / (tbMajor.Rows.Count + 1);
					//    float barLenth = majorStep * Convert.ToSingle(chart1.Series[seriesCount]["PointWidth"]);

					//    for (int i = 0; i < seriesCount; i++)
					//    {
					//        chart1.ChartAreas["ChartArea_" + chart1.Series[i + 1].Name].Position.X += ((seriesCount - 1) * offset);
					//        if (seriesCount % 2 == 0)
					//            chart1.ChartAreas["ChartArea_" + chart1.Series[i + 1].Name].Position.X += (-(seriesCount / 2 - 0.5f) + i) * barLenth;
					//        else
					//            chart1.ChartAreas["ChartArea_" + chart1.Series[i + 1].Name].Position.X += (-(seriesCount / 2) + i) * barLenth;
					//    }
					//}
					#endregion
					if (this.Apperence.PMSChartAppearance != null) {
						chart1.BorderlineColor = this.Apperence.PMSChartAppearance.BorderlineColor;
						chart1.BorderlineDashStyle = this.Apperence.PMSChartAppearance.BorderlineDashStyle;
						chart1.BorderlineWidth = this.Apperence.PMSChartAppearance.BorderlineWidth;
						chart1.BackColor = this.Apperence.PMSChartAppearance.BackColor;
						chart1.BackSecondaryColor = this.Apperence.PMSChartAppearance.BackSecondaryColor;
						chart1.BackHatchStyle = this.Apperence.PMSChartAppearance.BackHatchStyle;
						chart1.BackGradientStyle = this.Apperence.PMSChartAppearance.BackGradientStyle;
					}
				} catch (System.Exception ex) {
					InitChart ();
					PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error (ex.Message);
				}
			}
			foreach (var item in columnList) {
				Aim.Columns.Remove (item.ToString ());
			}
		}

		public override SuspensionItem[] ListSuspensionItems ()
		{
			SuspensionItem[] result = new SuspensionItem[8];
			result [0] = new SuspensionItem (Properties.Resources.Data, Properties.Resources.ResourceManager.GetString ("context0021"), Properties.Resources.ResourceManager.GetString ("context0021"), new Action (DealWithDataTable));
			result [1] = new SuspensionItem (Properties.Resources.OPEN, Properties.Resources.ResourceManager.GetString ("context0022"), Properties.Resources.ResourceManager.GetString ("context0022"), new Action (DealWithApperence));
			result [2] = new SuspensionItem (Properties.Resources.chart_type_bar, "条形图", "条形图", new Action (ChangeToBar));
			result [3] = new SuspensionItem (Properties.Resources.chart_type_bar_stack, "堆积条形图", "堆积条形图", new Action (ChangeToStackedBar));
			result [4] = new SuspensionItem (Properties.Resources.chart_type_bar_percent, "百分比堆积条形图", "百分比堆积条形图", new Action (ChangeToStackedBar100));
			result [5] = new SuspensionItem (Properties.Resources.chart_type_column, "柱形图", "柱形图", new Action (ChangeToColumn));
			result [6] = new SuspensionItem (Properties.Resources.chart_type_column_stack, "堆积柱形图", "堆积柱形图", new Action (ChangeToStackedColumn));
			result [7] = new SuspensionItem (Properties.Resources.chart_type_column_percent, "百分比堆积柱形图", "百分比堆积柱形图", new Action (ChangeToStackedColumn100));
			return result;
		}

		protected override void DealWithApperence ()
		{
			if (this != null) {
				BarApperenceFrm form1 = new BarApperenceFrm ();

				form1.ChartParent = this;
				DataSource ds = this.Apperence.Clone ();
				form1.PMSChartAppearance = this.Apperence.PMSChartAppearance;
				form1.chartAreaList = ds.ChartAreaList;
				form1.legendList = ds.LegendList;
				form1.seriesList = ds.SeriesList;
				form1.titleList = ds.TitleList;
				if (DialogResult.OK == form1.ShowDialog ()) {
					isIntial = true;
					InitailColumnData ();
				}
			}
		}

		private void ChangeToBar ()
		{
			if (ChartType != BarChartType.Bar)
				ChartType = BarChartType.Bar;
		}

		private void ChangeToStackedBar ()
		{
			if (ChartType != BarChartType.StackedBar)
				ChartType = BarChartType.StackedBar;
		}

		private void ChangeToStackedBar100 ()
		{
			if (ChartType != BarChartType.StackedBar100)
				ChartType = BarChartType.StackedBar100;
		}

		private void ChangeToColumn ()
		{
			if (ChartType != BarChartType.Column)
				ChartType = BarChartType.Column;
		}

		private void ChangeToStackedColumn ()
		{
			if (ChartType != BarChartType.StackedColumn)
				ChartType = BarChartType.StackedColumn;
		}

		private void ChangeToStackedColumn100 ()
		{
			if (ChartType != BarChartType.StackedColumn100)
				ChartType = BarChartType.StackedColumn100;
		}
	}

	public enum BarChartType
	{
		//
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
