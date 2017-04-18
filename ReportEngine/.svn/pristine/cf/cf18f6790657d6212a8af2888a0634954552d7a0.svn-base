using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using PMS.Libraries.ToolControls.PMSChart;
using System.Windows.Forms.DataVisualization.Charting;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using System.Drawing.Drawing2D;

namespace PMS.Libraries.ToolControls.PMSChart
{
    public partial class FormSectionApperence : Form
    {
        public FormSectionApperence()
        {
            InitializeComponent();
            cboTextOrientationInit();
            cboLimitStyle.Init();
            cboAnnoStyle.Init();
            cboCurveStyle.Init();
            cboLabelBorderType.Init();
            AnnoStyleComboBoxInit(cboAnnoStartStyle);
            AnnoStyleComboBoxInit(cboAnnoEndStyle);
            cboTitlePositionInit();
            cboSectionChartTypeInit();
            cboLabelStyleInit();
            cboTimeTypeInit();
            cboMarkerStyleInit();
        }

        public bool isIntial = false;
        public bool isApply = false;
        public DataSource dsApply;
        public SectionChart ChartParent;
        public List<PMSAnnotation> Annotations = new List<PMSAnnotation>();
        public List<PMSSeries> seriesList = new List<PMSSeries>();
        public List<PMSTitle> titleList = new List<PMSTitle>();
        public List<sectionLimit> limitList = new List<sectionLimit>();
        backSeries seriesMain = new backSeries(null);
        PMSAnnotation upper = new PMSAnnotation(null);
        PMSAnnotation lower = new PMSAnnotation(null);
        PMSAnnotation startAn = new PMSAnnotation(null);
        PMSAnnotation endAn = new PMSAnnotation(null);
        SectionChartProperties scp = new SectionChartProperties();
        sectionClass scX = new sectionClass();
        sectionClassY scY = new sectionClassY();
        Section section = new Section();

        private Font titleFont;
        private Color titleForeColor;
        private Font labelFont;
        private Color labelForeColor;

        private void SectionApperence_Load(object sender, EventArgs e)
        {
            InitSeiesList();
            InitTitleList();
            InitAnnotation();
        }

        private void InitSeiesList()
        {
            if (seriesList.Count != 0)
            {
                seriesMain = getSectionInfo(seriesList[0], scX, scY, section);
            }
            else
            {
                seriesMain.Color = Color.Blue;
            }

            if (seriesList.Count - 1 > 0)
            {
                for (int i = 0; i < seriesList.Count - 1; i++)
                {
                    limitList.Add(setLimit(seriesList[i + 1]));
                }
                initlvwLimit();
            }
            scX.SourceField = ChartParent.SourceField;
            section.SourceField = ChartParent.SourceField;
            cboXBindingFieldInit();
            cboSectionBindingFieldInit();
            cboXBindingField.SelectedItem = scX.BindingField;
            cboSectionBindingField.SelectedItem = section.BindingField;
            cboSectionChartType.SelectedItem = seriesMain.SectionChartType;
            cboLabelStyle.SelectedItem = scX.LabelStyle;

            chkCurveEnable.Checked = seriesMain.Enabled;
            ccboCurveColor.SelectedItem = seriesMain.Color;
            cboCurveStyle.SelectedItem = seriesMain.BorderDashStyle;
            nudCurveWidth.Value = seriesMain.BorderWidth;

            cboXBindingField.SelectedItem = scX.BindingField;
            chkAtuoScale.Checked = scY.AutoScale;
            txtScaleMax.Text = scY.Max.ToString();
            txtScaleMin.Text = scY.Min.ToString();
            cboSectionBindingField.SelectedItem = section.BindingField;
            nudPointCount.Value = section.PointsCount;
            txtTimeDistance.Text = section.Distance.ToString();
            cboTimeType.SelectedItem = section.TimeType;

            if (!string.IsNullOrEmpty(seriesMain.Label))
            {
                rbtnLabel.Checked = true;
                txtLabel.Visible = true;
                btnLabelEditor.Visible = true;
                txtLabel.Text = seriesMain.Label;
            }
            else if (seriesMain.IsValueShownAsLabel)
            {
                rbtnValueAsLabel.Checked = true;
            }
            else
            {
                rbtnNoLabel.Checked = true;
                gbxLabel.Visible = false;
            }
            labelFont = seriesMain.Font;
            labelForeColor = seriesMain.LabelForeColor;
            ccboLabelBackColor.SelectedItem = seriesMain.LabelBackColor;
            ccboLabelBorderColor.SelectedItem = seriesMain.LabelBorderColor;
            cboLabelBorderType.SelectedItem = seriesMain.LabelBorderDashStyle;
            nudLabelBorderWidth.Value = seriesMain.LabelBorderWidth;

            cboMarkerStyle.SelectedItem = seriesMain.MarkerStyle;
            ccboMarkerColor.SelectedItem = seriesMain.MarkerColor;
            nudMarkerSize.Value = seriesMain.MarkerSize;
            ccboMarkerBorderColor.SelectedItem = seriesMain.MarkerBorderColor;
            nudMarkerBorderWidth.Value = seriesMain.MarkerBorderWidth;
            nudMarkerStep.Value = seriesMain.MarkerStep;
        }

        private void InitTitleList()
        {
            gbxTitle.Visible = false;
            lvwTitle.Items.Clear();
            for (int i = 0; i < titleList.Count; i++)
            {
                string[] value = { i.ToString(), titleList[i].Name, titleList[i].Text };
                ListViewItem lvm = new ListViewItem(value);
                lvwTitle.Items.Add(lvm);
            }
        }

        private void InitAnnotation()
        {
            if (Annotations.Count == 0)
            {
                startAn.LineDashStyle = ChartDashStyle.DashDot;
                startAn.Width = 1;
                startAn.EndStyle = LineAnchorCapStyle.None;
                startAn.StartStyle = LineAnchorCapStyle.None;
                startAn.Color = Color.Black;
                startAn.enable = false;
                endAn.LineDashStyle = ChartDashStyle.DashDot;
                endAn.Width = 1;
                endAn.EndStyle = LineAnchorCapStyle.None;
                endAn.StartStyle = LineAnchorCapStyle.None;
                endAn.Color = Color.Black;
                endAn.enable = false;
                Annotations.Add(startAn);
                Annotations.Add(endAn);
            }
            else
            {
                startAn = Annotations[0];
                endAn = Annotations[1];
            }
        }

        /// <summary>
        /// 初始化X轴，Y轴，分段信息
        /// </summary>
        /// <param name="ps"></param>
        /// <param name="scX"></param>
        /// <param name="scY"></param>
        /// <param name="sec"></param>
        /// <returns>分段曲线信息</returns>
        backSeries getSectionInfo(PMSSeries ps, sectionClass scX, sectionClassY scY, Section sec)
        {
            backSeries bs = new backSeries(ps.ToSeries());
            bs.SectionChartType = (ps as SectionSeries).SectionChartType; ;
            sec.Distance = (ps as SectionSeries).Distance;
            sec.TimeType = (ps as SectionSeries).TimeType;
            sec.PointsCount = (ps as SectionSeries).PointsCount;
            scX.BindingField = (ps as SectionSeries).SectionField;
            scX.SourceField = (ps as SectionSeries).SourceField;
            scX.LabelStyle = (ps as SectionSeries).LabelStyle;
            scX.Format = (ps as SectionSeries).Format;
            sec.BindingField = (ps as SectionSeries).BindingField;
            if ((ps as SectionSeries).AxisMum != null)
            {
                scY.AutoScale = (ps as SectionSeries).AxisMum.Enable;
                scY.Max = (ps as SectionSeries).AxisMum.YaxisMaxmum;
                scY.Min = (ps as SectionSeries).AxisMum.YaxisMinmum;
            }
            return bs;
        }

        /// <summary>
        /// 初始化分段数据源绑定ComboBox
        /// </summary>
        private void cboSectionBindingFieldInit()
        {
            cboSectionBindingField.Items.Clear();
            if (ChartParent.SourceField == null)
                return;
            FieldTreeViewData ftvd = PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetCurrentReportDataDefine() as FieldTreeViewData;
            List<SourceField> fields = ChartParent.SourceField.GetSubSourceField(ftvd);
            if (fields == null)
                return;
            List<PmsField> lp = new List<PmsField>();
            foreach (SourceField sf in fields)
            {
                if (!string.IsNullOrEmpty(sf.DataType))
                {
                    string typ = sf.DataType.ToUpper();
                    if (typ.Equals("INT", StringComparison.InvariantCultureIgnoreCase) ||
                          typ.Equals("FLOAT", StringComparison.InvariantCultureIgnoreCase) ||
                          typ.Equals("REAL", StringComparison.InvariantCultureIgnoreCase) ||
                          typ.Equals("INT32", StringComparison.InvariantCultureIgnoreCase) ||
                          typ.Equals("INT16", StringComparison.InvariantCultureIgnoreCase) ||
                          typ.Equals("INT64", StringComparison.InvariantCultureIgnoreCase) ||
                          typ.Equals("SYSTEM.SINGLE", StringComparison.InvariantCultureIgnoreCase) ||
                          typ.Equals("SYSTEM.DOUBLE", StringComparison.InvariantCultureIgnoreCase) ||
                          typ.Equals("SYSTEM.INT32", StringComparison.InvariantCultureIgnoreCase) ||
                          typ.Equals("SYSTEM.DECIMAL", StringComparison.InvariantCultureIgnoreCase))
                    {
                        PmsField pf = new PmsField();
                        pf.fieldName = sf.RecordField;
                        pf.fieldDescription = sf.Name;
                        lp.Add(pf);
                        cboSectionBindingField.Items.Add(pf.fieldName);
                    }
                }
            }
        }

        /// <summary>
        /// 初始化X轴数据源绑定ComboBox
        /// </summary>
        private void cboXBindingFieldInit()
        {
            cboXBindingField.Items.Clear();
            if (ChartParent.SourceField == null)
                return;
            FieldTreeViewData ftvd = PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetCurrentReportDataDefine() as FieldTreeViewData;
            List<SourceField> fields = ChartParent.SourceField.GetSubSourceField(ftvd);
            if (fields == null)
                return;
            List<PmsField> lp = new List<PmsField>();
            foreach (SourceField sf in fields)
            {
                if (!string.IsNullOrEmpty(sf.DataType))
                {
                    string typ = sf.DataType.ToUpper();
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
                        typ.Equals("SYSTEM.DATETIME", StringComparison.InvariantCultureIgnoreCase))
                    {
                        PmsField pf = new PmsField();
                        pf.fieldName = sf.RecordField;
                        pf.fieldDescription = sf.Name;
                        lp.Add(pf);
                        cboXBindingField.Items.Add(pf.fieldName);
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DataCommit();
            this.DialogResult = DialogResult.OK;
            this.Dispose();
            ChartParent.InitailColumnData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
            ChartParent.InitailColumnData();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            DataCommit();
            ChartParent.InitailColumnData();
        }

        private void DataCommit()
        {
            seriesMain.SectionChartType = (SectionSeries.enumSectionChartType)cboSectionChartType.SelectedItem;
            scX.LabelStyle = (sectionClass.enumLabelStyle)cboLabelStyle.SelectedItem;
            seriesMain.Enabled = chkCurveEnable.Checked;
            seriesMain.Color = ccboCurveColor.SelectedItem;
            seriesMain.BorderDashStyle = (ChartDashStyle)cboCurveStyle.SelectedItem;
            seriesMain.BorderWidth = (int)nudCurveWidth.Value;

            if (null != cboXBindingField.SelectedItem)
                scX.BindingField = cboXBindingField.SelectedItem.ToString();
            scY.AutoScale = chkAtuoScale.Checked;
            scY.Max = Convert.ToDouble(txtScaleMax.Text);
            scY.Min = Convert.ToDouble(txtScaleMin.Text);
            if (null != cboSectionBindingField.SelectedItem)
                section.BindingField = cboSectionBindingField.SelectedItem.ToString();
            section.PointsCount = (int)nudPointCount.Value;
            section.Distance = Convert.ToDouble(txtTimeDistance.Text);
            section.TimeType = (SectionSeries.enumTimeType)cboTimeType.SelectedItem;

            if (rbtnNoLabel.Checked)
            {
                seriesMain.IsValueShownAsLabel = false;
                seriesMain.Label = string.Empty;
            }
            if (rbtnValueAsLabel.Checked)
            {
                seriesMain.IsValueShownAsLabel = true;
                seriesMain.Label = string.Empty;
            }
            if (rbtnLabel.Checked)
            {
                seriesMain.IsValueShownAsLabel = false;
                seriesMain.Label = txtLabel.Text;
            }
            seriesMain.Font = labelFont;
            seriesMain.LabelForeColor = labelForeColor;
            seriesMain.LabelBackColor = ccboLabelBackColor.SelectedItem;
            seriesMain.LabelBorderColor = ccboLabelBorderColor.SelectedItem;
            seriesMain.LabelBorderDashStyle = (ChartDashStyle)cboLabelBorderType.SelectedItem;
            seriesMain.LabelBorderWidth = (int)nudLabelBorderWidth.Value;

            seriesMain.MarkerStyle = (MarkerStyle)cboMarkerStyle.SelectedItem;
            seriesMain.MarkerColor = ccboMarkerColor.SelectedItem;
            seriesMain.MarkerSize = (int)nudMarkerSize.Value;
            seriesMain.MarkerBorderColor = ccboMarkerBorderColor.SelectedItem;
            seriesMain.MarkerBorderWidth = (int)nudMarkerBorderWidth.Value;
            seriesMain.MarkerStep = (int)nudMarkerStep.Value;

            seriesList.Clear();
            seriesList.Add(setSectionSeries(seriesMain, scX, scY, section));
            for (int i = 0; i < limitList.Count; i++)
            {

                seriesList.Add(getSeriesFromLimit(limitList[i]));
            }

            dsApply = new DataSource(null);
            dsApply.TitleList = this.titleList;
            dsApply.SeriesList = this.seriesList;
            dsApply.annotationList = this.Annotations;
            ChartParent.Apperence = dsApply.Clone();
        }

        /// <summary>
        /// 初始化标题文字排列方向ComboBox
        /// </summary>
        private void cboTextOrientationInit()
        {
            cboTextOrientation.Items.Add(TextOrientation.Auto);
            cboTextOrientation.Items.Add(TextOrientation.Horizontal);
            cboTextOrientation.Items.Add(TextOrientation.Rotated270);
            cboTextOrientation.Items.Add(TextOrientation.Rotated90);
            cboTextOrientation.Items.Add(TextOrientation.Stacked);
            cboTextOrientation.SelectedIndex = 0;
        }

        private void btnTitleFont_Click(object sender, EventArgs e)
        {
            using (FontDialog fd = new FontDialog())
            {
                fd.ShowColor = true;
                fd.Font = titleFont;
                fd.Color = titleForeColor;
                if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    titleFont = fd.Font;
                    titleForeColor = fd.Color;
                }
            }
        }

        SectionSeries setSectionSeries(backSeries ps, sectionClass scX, sectionClassY scY, Section sec)
        {
            SectionSeries ss = new SectionSeries(ps.ToSeries());
            ss.SectionChartType = ps.SectionChartType;
            ss.Distance = sec.Distance;
            ss.TimeType = sec.TimeType;
            ss.PointsCount = sec.PointsCount;
            ss.SectionField = scX.BindingField;
            ss.LabelStyle = scX.LabelStyle;
            ss.Format = scX.Format;
            ss.BindingField = sec.BindingField;
            ss.AxisMum.Enable = scY.AutoScale;
            ss.AxisMum.YaxisMaxmum = scY.Max;
            ss.AxisMum.YaxisMinmum = scY.Min;
            return ss;
        }

        /// <summary>
        /// 复制警戒线
        /// </summary>
        /// <param name="ps"></param>
        /// <returns></returns>
        private sectionLimit setLimit(PMSSeries ps)
        {
            sectionLimit lt = new sectionLimit();
            lt.Color = ps.Color;
            lt.Width = ps.BorderWidth;
            lt.Style = ps.BorderDashStyle;
            lt.Limit = (ps as SectionSeries).Limit;
            lt.Enable = ps.Enabled;
            lt.Name = ps.Name;
            return lt;
        }

        private void lvwLimit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwLimit.SelectedItems.Count == 0)
            { gbxLimit.Visible = false; }
            else
            {
                gbxLimit.Visible = true;
                chkEnable.Checked = limitList[Convert.ToInt32(lvwLimit.SelectedItems[0].SubItems[0].Text)].Enable;
                txtLimitName.Text = limitList[Convert.ToInt32(lvwLimit.SelectedItems[0].SubItems[0].Text)].Name;
                txtLimit.Text = limitList[Convert.ToInt32(lvwLimit.SelectedItems[0].SubItems[0].Text)].Limit.ToString();
                ccboLimitColor.SelectedItem = limitList[Convert.ToInt32(lvwLimit.SelectedItems[0].SubItems[0].Text)].Color;
                cboLimitStyle.SelectedItem = limitList[Convert.ToInt32(lvwLimit.SelectedItems[0].SubItems[0].Text)].Style;
                nudLimitWidth.Value = limitList[Convert.ToInt32(lvwLimit.SelectedItems[0].SubItems[0].Text)].Width;
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (lvwLimit.SelectedItems.Count != 0)
            {
                limitList[Convert.ToInt32(lvwLimit.SelectedItems[0].SubItems[0].Text)].Enable = chkEnable.Checked;
                limitList[Convert.ToInt32(lvwLimit.SelectedItems[0].SubItems[0].Text)].Name = txtLimitName.Text;
                limitList[Convert.ToInt32(lvwLimit.SelectedItems[0].SubItems[0].Text)].Limit = Convert.ToDouble(txtLimit.Text);
                limitList[Convert.ToInt32(lvwLimit.SelectedItems[0].SubItems[0].Text)].Color = ccboLimitColor.SelectedItem;
                limitList[Convert.ToInt32(lvwLimit.SelectedItems[0].SubItems[0].Text)].Style = (ChartDashStyle)cboLimitStyle.SelectedItem;
                limitList[Convert.ToInt32(lvwLimit.SelectedItems[0].SubItems[0].Text)].Width = (int)nudLimitWidth.Value;
                initlvwLimit();
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (lvwLimit.SelectedItems.Count != 0)
            {
                limitList.RemoveAt(Convert.ToInt32(lvwLimit.SelectedItems[0].SubItems[0].Text));
                initlvwLimit();
            }
        }

        private void initlvwLimit()
        {
            gbxLimit.Visible = false;
            lvwLimit.Items.Clear();
            for (int i = 0; i < limitList.Count; i++)
            {
                string[] value = { i.ToString(), limitList[i].Name };
                ListViewItem lvm = new ListViewItem(value);
                lvwLimit.Items.Add(lvm);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string aim = "警戒线";
            sectionLimit pt = new sectionLimit();
            pt.Name = GetNameFromList(limitList, aim);
            pt.Color = Color.Red;
            pt.Enable = true;
            pt.Style = ChartDashStyle.Solid;
            pt.Width = 1;
            pt.Limit = 0;
            limitList.Add(pt);
            initlvwLimit();
            lvwLimit.Items[lvwLimit.Items.Count - 1].Selected = true;
        }


        /// <summary>
        /// 获取标题、图例、警戒线名字（递增）
        /// </summary>
        /// <param name="data"></param>
        /// <param name="aim"></param>
        /// <returns></returns>
        private string GetNameFromList(object data, string aim)
        {
            string result = aim;
            int NO = 0;
            List<int> nameNO = new List<int>();
            if (data is List<PMSTitle>)
            {
                List<PMSTitle> areatemp = data as List<PMSTitle>;
                if (areatemp != null)
                {
                    foreach (PMSTitle item in areatemp)
                    {
                        if (item.Name.StartsWith(aim))
                        {
                            int i;
                            if (int.TryParse(item.Name.Substring(aim.Length), out i))
                            {
                                nameNO.Add(i);
                            }
                        }
                    }
                    RadarAlertApp.resetList(nameNO);
                    for (int i = 0; i < nameNO.Count; i++)
                    {
                        if (nameNO[i] != i + 1)
                        {
                            NO = i + 1;
                            break;
                        }
                    }
                    if (NO == 0 && nameNO.Count != 0)
                    {
                        NO = nameNO[nameNO.Count - 1] + 1;
                    }
                    else if (nameNO.Count == 0) { NO = 1; }
                }
            }
            else if (data is List<PMSLegend>)
            {
                List<PMSLegend> areatemp = data as List<PMSLegend>;
                if (areatemp != null)
                {
                    foreach (PMSLegend item in areatemp)
                    {
                        if (item.Name.StartsWith(aim))
                        {
                            int i;
                            if (int.TryParse(item.Name.Substring(aim.Length), out i))
                            {
                                nameNO.Add(i);
                            }
                        }
                    }
                    RadarAlertApp.resetList(nameNO);
                    for (int i = 0; i < nameNO.Count; i++)
                    {
                        if (nameNO[i] != i + 1)
                        {
                            NO = i + 1;
                            break;
                        }
                    }
                    if (NO == 0 && nameNO.Count != 0)
                    {
                        NO = nameNO[nameNO.Count - 1] + 1;
                    }
                    else if (nameNO.Count == 0) { NO = 1; }
                }
            }
            else if (data is List<sectionLimit>)
            {
                List<sectionLimit> areatemp = data as List<sectionLimit>;
                if (areatemp != null)
                {
                    foreach (sectionLimit item in areatemp)
                    {
                        if (item.Name.StartsWith(aim))
                        {
                            int i;
                            if (int.TryParse(item.Name.Substring(aim.Length), out i))
                            {
                                nameNO.Add(i);
                            }
                        }
                    }
                    RadarAlertApp.resetList(nameNO);
                    for (int i = 0; i < nameNO.Count; i++)
                    {
                        if (nameNO[i] != i + 1)
                        {
                            NO = i + 1;
                            break;
                        }
                    }
                    if (NO == 0 && nameNO.Count != 0)
                    {
                        NO = nameNO[nameNO.Count - 1] + 1;
                    }
                    else if (nameNO.Count == 0) { NO = 1; }
                }
            }
            return result + NO;
        }

        /// <summary>
        /// 复制警戒线到默认曲线
        /// </summary>
        /// <param name="lt"></param>
        /// <returns></returns>
        SectionSeries getSeriesFromLimit(sectionLimit lt)
        {
            SectionSeries ps = new SectionSeries(null);
            ps.Color = lt.Color;
            ps.BorderWidth = lt.Width;
            ps.BorderDashStyle = lt.Style;
            ps.Limit = lt.Limit;
            ps.Enabled = lt.Enable;
            ps.Name = lt.Name;
            return ps;
        }

        /// <summary>
        /// 初始化垂直线始末端形状
        /// </summary>
        /// <param name="cbo"></param>
        private void AnnoStyleComboBoxInit(ComboBox cbo)
        {
            cbo.Items.Add(LineAnchorCapStyle.None);
            cbo.Items.Add(LineAnchorCapStyle.Arrow);
            cbo.Items.Add(LineAnchorCapStyle.Diamond);
            cbo.Items.Add(LineAnchorCapStyle.Square);
            cbo.Items.Add(LineAnchorCapStyle.Round);
            cbo.SelectedIndex = 0;
        }

        private void btnAnno_Click(object sender, EventArgs e)
        {
            gbxAnnotation.Visible = false;
            if (gbxAnnotation.Text == "末尾端参数")
            {
                endAn.enable = chkAnnoEnable.Checked;
                endAn.Color = ccboAnnoColor.SelectedItem;
                endAn.LineDashStyle = (ChartDashStyle)cboAnnoStyle.SelectedItem;
                endAn.Width = (int)nudAnnoWidth.Value;
                endAn.StartStyle = (LineAnchorCapStyle)cboAnnoStartStyle.SelectedItem;
                endAn.EndStyle = (LineAnchorCapStyle)cboAnnoEndStyle.SelectedItem;
            }
            if (gbxAnnotation.Text == "起始端参数")
            {
                startAn.enable = chkAnnoEnable.Checked;
                startAn.Color = ccboAnnoColor.SelectedItem;
                startAn.LineDashStyle = (ChartDashStyle)cboAnnoStyle.SelectedItem;
                startAn.Width = (int)nudAnnoWidth.Value;
                startAn.StartStyle = (LineAnchorCapStyle)cboAnnoStartStyle.SelectedItem;
                startAn.EndStyle = (LineAnchorCapStyle)cboAnnoEndStyle.SelectedItem;
            }
        }

        private void lvwAnno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwAnno.SelectedItems.Count == 0)
            { gbxAnnotation.Visible = false; }
            else
            {
                gbxAnnotation.Visible = true;
                if (lvwAnno.SelectedItems[0].SubItems[1].Text == "起始端")
                {
                    gbxAnnotation.Text = "起始端参数";
                    chkAnnoEnable.Checked = startAn.enable;
                    ccboAnnoColor.SelectedItem = startAn.Color;
                    cboAnnoStyle.SelectedItem = startAn.LineDashStyle;
                    nudAnnoWidth.Value = startAn.Width;
                    cboAnnoStartStyle.SelectedItem = startAn.StartStyle;
                    cboAnnoEndStyle.SelectedItem = startAn.EndStyle;
                }
                if (lvwAnno.SelectedItems[0].SubItems[1].Text == "末尾端")
                {
                    gbxAnnotation.Text = "末尾端参数";
                    chkAnnoEnable.Checked = endAn.enable;
                    ccboAnnoColor.SelectedItem = endAn.Color;
                    cboAnnoStyle.SelectedItem = endAn.LineDashStyle;
                    nudAnnoWidth.Value = endAn.Width;
                    cboAnnoStartStyle.SelectedItem = endAn.StartStyle;
                    cboAnnoEndStyle.SelectedItem = endAn.EndStyle;
                }
            }
        }

        private void lvwTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwTitle.SelectedItems.Count == 0)
            { gbxTitle.Visible = false; }
            else
            {
                gbxTitle.Visible = true;
                chkTitleVisible.Checked = titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].Visible;
                txtTitleName.Text = titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].Name;
                cboTitlePosition.SelectedItem = titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].Docking;
                txtTitle.Text = titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].Text;
                titleFont = titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].Font;
                titleForeColor = titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].ForeColor;
                cboTextOrientation.SelectedItem = titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].TextOrientation;
            }

        }

        private void btnTitleModify_Click(object sender, EventArgs e)
        {
            if (lvwTitle.SelectedItems.Count != 0)
            {
                titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].Visible = chkTitleVisible.Checked;
                titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].Name = txtTitleName.Text;
                titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].Docking = (Docking)cboTitlePosition.SelectedItem;
                titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].Text = txtTitle.Text;
                titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].Font = titleFont;
                titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].ForeColor = titleForeColor;
                titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].TextOrientation = (TextOrientation)cboTextOrientation.SelectedItem;
                InitTitleList();
            }
        }

        /// <summary>
        /// 初始化标题停靠位置ComboBox
        /// </summary>
        private void cboTitlePositionInit()
        {
            cboTitlePosition.Items.Add(Docking.Top);
            cboTitlePosition.Items.Add(Docking.Right);
            cboTitlePosition.Items.Add(Docking.Bottom);
            cboTitlePosition.Items.Add(Docking.Left);
            cboTitlePosition.SelectedIndex = 0;
        }

        private void btnAddTitle_Click(object sender, EventArgs e)
        {
            PMSTitle pt = new PMSTitle(null);
            pt.Name = GetNameFromList(titleList, "标题");
            pt.Text = pt.Name;
            titleList.Add(pt);
            InitTitleList();
            lvwTitle.Items[lvwTitle.Items.Count - 1].Selected = true;
        }

        private void btnDelTitle_Click(object sender, EventArgs e)
        {
            if (lvwTitle.SelectedItems.Count != 0)
            {
                titleList.RemoveAt(Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text));
                InitTitleList();
            }
        }

        private void cboSectionChartTypeInit()
        {
            cboSectionChartType.Items.Add(SectionSeries.enumSectionChartType.Line);
            cboSectionChartType.Items.Add(SectionSeries.enumSectionChartType.Spline);
            cboSectionChartType.Items.Add(SectionSeries.enumSectionChartType.Stepline);
            cboSectionChartType.Items.Add(SectionSeries.enumSectionChartType.Fastline);
            cboSectionChartType.Items.Add(SectionSeries.enumSectionChartType.Area);
        }

        private void cboLabelStyleInit()
        {
            cboLabelStyle.Items.Add(sectionClass.enumLabelStyle.WordWrap);
            cboLabelStyle.Items.Add(sectionClass.enumLabelStyle.LabelsAngleStep90);
        }

        private void cboTimeTypeInit()
        {
            cboTimeType.Items.Add(SectionSeries.enumTimeType.second);
            cboTimeType.Items.Add(SectionSeries.enumTimeType.minute);
            cboTimeType.Items.Add(SectionSeries.enumTimeType.hour);
        }

        private void cboMarkerStyleInit()
        {
            cboMarkerStyle.Items.Add(MarkerStyle.None);
            cboMarkerStyle.Items.Add(MarkerStyle.Square);
            cboMarkerStyle.Items.Add(MarkerStyle.Circle);
            cboMarkerStyle.Items.Add(MarkerStyle.Diamond);
            cboMarkerStyle.Items.Add(MarkerStyle.Triangle);
            cboMarkerStyle.Items.Add(MarkerStyle.Cross);
            cboMarkerStyle.Items.Add(MarkerStyle.Star4);
            cboMarkerStyle.Items.Add(MarkerStyle.Star5);
            cboMarkerStyle.Items.Add(MarkerStyle.Star6);
            cboMarkerStyle.Items.Add(MarkerStyle.Star10);
        }
        private void rbtnNoLabel_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnNoLabel.Checked)
                gbxLabel.Visible = false;
            else
                gbxLabel.Visible = true;
        }

        private void rbtnLabel_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnLabel.Checked)
            {
                txtLabel.Visible = true;
                btnLabelEditor.Visible = true;
            }
            else
            {
                txtLabel.Visible = false;
                btnLabelEditor.Visible = false;
            }
        }

        private void btnLabelFont_Click(object sender, EventArgs e)
        {
            using (FontDialog fd = new FontDialog())
            {
                fd.ShowColor = true;
                fd.Font = labelFont;
                fd.Color = labelForeColor;
                if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    labelFont = fd.Font;
                    labelForeColor = fd.Color;
                }
            }
        }

        private void btnLabelEditor_Click(object sender, EventArgs e)
        {
            KeywordsStringEditorForm dialog = new KeywordsStringEditorForm(txtLabel.Text, "Series", "Label", 9);
            dialog.KeywordsRegistry = new KeywordsRegistry();
            dialog.ShowDialog();
            txtLabel.Text = dialog.ResultString;
        }
    }
}
