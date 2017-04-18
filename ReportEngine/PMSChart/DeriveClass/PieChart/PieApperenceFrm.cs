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
using System.Collections;

namespace PMS.Libraries.ToolControls.PMSChart
{
    public partial class PieApperenceFrm : Form
    {
        public PieApperenceFrm()
        {
            InitializeComponent();
            cboTitlePositionInit();
            cboTextOrientationInit();
            cboLabelBorderType.Init();
            cboPieLabelStyleInit();
            cboLightStyleInit();
            cboPaletteInit();
            cboPieDrawingStyleInit();
            cboLegendAlignmentInit();
            cboLegendDockingInit();
            cboLegendStyleInit();
            cboTableStyleInit();
            cboValueFxInit();
            cboMajorSortInit();
            cboMinorSortInit();
        }

        public DataSource dsApply;
        public PieChart ChartParent;
        public GroupSource groupSource;
        public PMSChartApp PMSChartAppearance;
        public List<PMSLegend> legendList = new List<PMSLegend>();
        public List<PMSSeries> seriesList = new List<PMSSeries>();
        public List<PMSTitle> titleList = new List<PMSTitle>();
        public List<PMSChartArea> chartAreaList = new List<PMSChartArea>();

        private ChartArea ChartArea1 = new ChartArea("ChartArea1");
        private Series series1 = new Series();
        private Legend legend1 = new Legend();

        private Font titleFont;
        private Color titleForeColor;
        private Font labelFont;
        private Color labelForeColor;
        private Font legendFont;
        private Color legendForeColor;

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
            GroupSourceDataCommit();
            TitleListCommit();
            series1.Palette = (ChartColorPalette)cboPalette.SelectedItem;
            series1.SetCustomProperty("PieDrawingStyle", cboPieDrawingStyle.SelectedItem.ToString());

            if (rbtnNoLabel.Checked)
            {
                series1.IsValueShownAsLabel = false;
                series1.Label = string.Empty;
                series1.SetCustomProperty("PieLabelStyle", "Disabled");
            }
            if (rbtnValueAsLabel.Checked)
            {
                series1.IsValueShownAsLabel = true;
                series1.Label = string.Empty;
                series1.SetCustomProperty("PieLabelStyle", cboPieLabelStyle.SelectedItem.ToString());
            }
            if (rbtnLabel.Checked)
            {
                series1.IsValueShownAsLabel = false;
                series1.Label = txtLabel.Text;
                series1.SetCustomProperty("PieLabelStyle", cboPieLabelStyle.SelectedItem.ToString());
            }
            series1.Font = labelFont;
            series1.LabelForeColor = labelForeColor;
            series1.LabelBackColor = ccboLabelBackColor.SelectedItem;
            series1.LabelBorderColor = ccboLabelBorderColor.SelectedItem;
            series1.LabelBorderDashStyle = (ChartDashStyle)cboLabelBorderType.SelectedItem;
            series1.LabelBorderWidth = (int)nudLabelBorderWidth.Value;
            series1.SetCustomProperty("LabelsHorizontalLineSize", "1");
            series1.SetCustomProperty("LabelsRadialLineSize", "1");
            series1.SetCustomProperty("PieLineColor", Convert.ToString(cboPieLineColor.SelectedItem.ToArgb()));

            series1.SetCustomProperty("CollectedSliceExploded", chkCollectedSliceExpoded.Checked.ToString());
            series1.SetCustomProperty("CollectedThresholdUsePercent", chkCollectedThrecholdUsePercent.Checked.ToString());
            series1.SetCustomProperty("CollectedThreshold", nudCollectedThrechold.Value.ToString());
            series1.SetCustomProperty("CollectedColor", Convert.ToString(ccboCollectedColor.SelectedItem.ToArgb()));
            series1.SetCustomProperty("CollectedLabel", txtCollectedLabel.Text);
            series1.SetCustomProperty("CollectedLegendText", txtCollectedLegendText.Text);

            series1.LegendText = txtLegendText.Text;

            series1.SetCustomProperty("XBindingField", (cboXBindingField.SelectedItem == null) ? string.Empty : cboXBindingField.SelectedItem.ToString());
            series1.SetCustomProperty("YBindingField", (cboYBindingField.SelectedItem == null) ? string.Empty : cboYBindingField.SelectedItem.ToString());

            seriesList.Clear();
            seriesList.Add(new PMSSeries(series1));

            ChartArea1.Area3DStyle.Enable3D = chkEnable3D.Checked;
            ChartArea1.Area3DStyle.LightStyle = (LightStyle)cboLightStyle.SelectedItem;
            ChartArea1.Area3DStyle.Inclination = (int)nudInclination.Value;
            ChartArea1.Area3DStyle.Rotation = (int)nudRotation.Value;
            chartAreaList.Clear();
            chartAreaList.Add(new PMSChartArea(ChartArea1));

            legend1.Enabled = chkLegendEnable.Checked;
            legend1.IsDockedInsideChartArea = chkLegendDockedInside.Checked;
            legend1.DockedToChartArea = "ChartArea1";
            legend1.Alignment = (StringAlignment)cboLegendAlignment.SelectedItem;
            legend1.Docking = (Docking)cboLegendDocking.SelectedItem;
            legend1.Font = legendFont;
            legend1.ForeColor = legendForeColor;
            legend1.LegendStyle = (LegendStyle)cboLegendStyle.SelectedItem;
            legend1.TableStyle = (LegendTableStyle)cboTableStyle.SelectedItem;
            legendList.Clear();
            legendList.Add(new PMSLegend(legend1));

            dsApply = new DataSource(null);
            dsApply.PMSChartAppearance = this.PMSChartAppearance;
            dsApply.ChartAreaList = this.chartAreaList;
            dsApply.TitleList = this.titleList;
            dsApply.SeriesList = this.seriesList;
            dsApply.LegendList = this.legendList;
            ChartParent.Apperence = dsApply.Clone();
        }

        private void PieApperenceFrm_Load(object sender, EventArgs e)
        {
            InitChartAreaList();
            InitSeiesList();
            InitLegendList();
            InitTitleList();
            InitGroupSource();
            //propertyTree1.SelectedPaneNode = ppPane7.PaneNode;//默认选择“数据源”页
        }

        private void InitChartAreaList()
        {
            if (chartAreaList.Count != 0)
                chartAreaList[0].SetChartArea(ChartArea1);
            chkEnable3D.Checked = gbx3DStyle.Visible = ChartArea1.Area3DStyle.Enable3D;
            cboLightStyle.SelectedItem = ChartArea1.Area3DStyle.LightStyle;
            nudInclination.Value = ChartArea1.Area3DStyle.Inclination;
            nudRotation.Value = ChartArea1.Area3DStyle.Rotation;
        }

        private void InitSeiesList()
        {
            #region 由于分组情况堆叠的位置不正确，暂时屏蔽一些功能
            if (ChartParent.GroupSource.Enable)
            {
                chkCollectedSliceExpoded.Enabled = false;
                chkCollectedSliceExpoded.Checked = false;

                cboPieLabelStyle.Items.Clear();
                cboPieLabelStyle.Items.Add("Inside");
                cboPieLabelStyle.SelectedIndex = 0;
            }
            else
            {
                chkCollectedSliceExpoded.Enabled = true;

                cboPieLabelStyle.Items.Clear();
                cboPieLabelStyle.Items.Add("Inside");
                cboPieLabelStyle.Items.Add("Outside");
                cboPieLabelStyle.SelectedIndex = 0;
            }
            #endregion

            cboYBindingFieldInit();
            cboXBindingFieldInit();
            series1.ChartType = (SeriesChartType)ChartParent.ChartType;
            if (seriesList.Count != 0)
                seriesList[0].SetSeriesValue(series1);

            if (!string.IsNullOrEmpty(series1.Label))
            {
                rbtnLabel.Checked = true;
                txtLabel.Visible = true;
                btnLabelEditor.Visible = true;
                txtLabel.Text = series1.Label;
            }
            else if (series1.IsValueShownAsLabel)
            {
                rbtnValueAsLabel.Checked = true;
            }
            else
            {
                rbtnNoLabel.Checked = true;
                gbxLabel.Visible = false;
            }
            labelFont = series1.Font;
            labelForeColor = series1.LabelForeColor;
            ccboLabelBackColor.SelectedItem = series1.LabelBackColor;
            ccboLabelBorderColor.SelectedItem = series1.LabelBorderColor;
            cboLabelBorderType.SelectedItem = series1.LabelBorderDashStyle;
            nudLabelBorderWidth.Value = series1.LabelBorderWidth;
            cboPieLabelStyle.SelectedItem = (series1.GetCustomProperty("PieLabelStyle") == null) ? "Inside" : series1.GetCustomProperty("PieLabelStyle");
            cboPieLineColor.SelectedItem = (series1.GetCustomProperty("PieLineColor") == null) ? Color.Black : Color.FromArgb(Convert.ToInt32(series1.GetCustomProperty("PieLineColor")));

            chkCollectedSliceExpoded.Checked = (series1.GetCustomProperty("CollectedSliceExploded") == null) ? true : Boolean.Parse(series1.GetCustomProperty("CollectedSliceExploded"));
            chkCollectedThrecholdUsePercent.Checked = (series1.GetCustomProperty("CollectedThresholdUsePercent") == null) ? true : Boolean.Parse(series1.GetCustomProperty("CollectedThresholdUsePercent"));
            nudCollectedThrechold.Value = (series1.GetCustomProperty("CollectedThreshold") == null) ? 0 : Convert.ToDecimal(series1.GetCustomProperty("CollectedThreshold"));
            ccboCollectedColor.SelectedItem = (series1.GetCustomProperty("CollectedColor") == null) ? Color.Empty : Color.FromArgb(Convert.ToInt32(series1.GetCustomProperty("CollectedColor")));
            txtCollectedLabel.Text = (series1.GetCustomProperty("CollectedLabel") == null) ? string.Empty : series1.GetCustomProperty("CollectedLabel");
            txtCollectedLegendText.Text = (series1.GetCustomProperty("CollectedLegendText") == null) ? string.Empty : series1.GetCustomProperty("CollectedLegendText");

            txtLegendText.Text = series1.LegendText;

            cboXBindingField.SelectedItem = (series1.GetCustomProperty("XBindingField") == null) ? string.Empty : series1.GetCustomProperty("XBindingField");
            cboYBindingField.SelectedItem = (series1.GetCustomProperty("YBindingField") == null) ? string.Empty : series1.GetCustomProperty("YBindingField");

            cboPalette.SelectedItem = series1.Palette;
            cboPieDrawingStyle.SelectedItem = (series1.GetCustomProperty("PieDrawingStyle") == null) ? "Default" : series1.GetCustomProperty("PieDrawingStyle");
        }

        #region 初始化数据源下拉列表
        /// <summary>
        /// 初始化Y轴数据源绑定ComboBox
        /// </summary>
        private void cboYBindingFieldInit()
        {
            //测试用数据
            //cboYBindingField.Items.Add("YBindingField1");
            //cboYBindingField.Items.Add("YBindingField2");
            //cboYBindingField.Items.Add("YBindingField3");  

            cboYBindingField.Items.Clear();
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
                        cboYBindingField.Items.Add(pf.fieldName);
                    }
                }
            }
        }

        /// <summary>
        /// 初始化X轴数据源绑定ComboBox
        /// </summary>
        private void cboXBindingFieldInit()
        {
            //测试用数据
            //cboXBindingField.Items.Add("XBindingField1");
            //cboXBindingField.Items.Add("XBindingField2");
            //cboXBindingField.Items.Add("XBindingField3");

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
                    PmsField pf = new PmsField();
                    pf.fieldName = sf.RecordField;
                    pf.fieldDescription = sf.Name;
                    lp.Add(pf);
                    cboXBindingField.Items.Add(pf.fieldName);
                }
            }
        }
        #endregion

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

        #region 图表标题
        /// <summary>
        /// 初始化图表标题
        /// </summary>
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

        private void TitleListCommit()
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

                lvwTitle.Items[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].SubItems[1].Text = txtTitleName.Text;
                lvwTitle.Items[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].SubItems[2].Text = txtTitle.Text;
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

        private string _titleSelectedIndex = string.Empty;
        private void lvwTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwTitle.SelectedItems.Count == 0)
            { gbxTitle.Visible = false; }
            else
            {
                gbxTitle.Visible = true;
                if (lvwTitle.SelectedItems[0].SubItems[0].Text != _titleSelectedIndex)
                {
                    if (!string.IsNullOrEmpty(_titleSelectedIndex))
                    {
                        titleList[Convert.ToInt32(_titleSelectedIndex)].Visible = chkTitleVisible.Checked;
                        titleList[Convert.ToInt32(_titleSelectedIndex)].Name = txtTitleName.Text;
                        titleList[Convert.ToInt32(_titleSelectedIndex)].Docking = (Docking)cboTitlePosition.SelectedItem;
                        titleList[Convert.ToInt32(_titleSelectedIndex)].Text = txtTitle.Text;
                        titleList[Convert.ToInt32(_titleSelectedIndex)].Font = titleFont;
                        titleList[Convert.ToInt32(_titleSelectedIndex)].ForeColor = titleForeColor;
                        titleList[Convert.ToInt32(_titleSelectedIndex)].TextOrientation = (TextOrientation)cboTextOrientation.SelectedItem;

                        lvwTitle.Items[Convert.ToInt32(_titleSelectedIndex)].SubItems[1].Text = txtTitleName.Text;
                        lvwTitle.Items[Convert.ToInt32(_titleSelectedIndex)].SubItems[2].Text = txtTitle.Text;
                    }
                    chkTitleVisible.Checked = titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].Visible;
                    txtTitleName.Text = titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].Name;
                    cboTitlePosition.SelectedItem = titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].Docking;
                    txtTitle.Text = titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].Text;
                    titleFont = titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].Font;
                    titleForeColor = titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].ForeColor;
                    cboTextOrientation.SelectedItem = titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].TextOrientation;

                    _titleSelectedIndex = lvwTitle.SelectedItems[0].SubItems[0].Text;
                }
            }

        }

        private void btnAddTitle_Click(object sender, EventArgs e)
        {
            PMSTitle pt = new PMSTitle(null);
            pt.Name = GetNameFromList(titleList, "标题");
            pt.Text = pt.Name;
            titleList.Add(pt);
            _titleSelectedIndex = string.Empty;
            InitTitleList();
            lvwTitle.Items[lvwTitle.Items.Count - 1].Selected = true;
        }

        private void btnDelTitle_Click(object sender, EventArgs e)
        {
            if (lvwTitle.SelectedItems.Count != 0)
            {
                titleList.RemoveAt(Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text));
                _titleSelectedIndex = string.Empty;
                InitTitleList();
            }
        }

        private void btnTitleStyle_Click(object sender, EventArgs e)
        {
            using (BorderAndBackEditor fd = new BorderAndBackEditor())
            {
                fd.BorderColor = titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].BorderColor;
                fd.BorderDashStyle = titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].BorderDashStyle;
                fd.BorderWidth = titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].BorderWidth;
                fd.BackColor = titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].BackColor;
                fd.BackSecondColor = titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].BackSecondaryColor;
                fd.BackHatchStyle = titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].BackHatchStyle;
                fd.BackGradientStyle = titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].BackGradientStyle;
                if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].BorderColor = fd.BorderColor;
                    titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].BorderDashStyle = fd.BorderDashStyle;
                    titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].BorderWidth = fd.BorderWidth;
                    titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].BackColor = fd.BackColor;
                    titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].BackSecondaryColor = fd.BackSecondColor;
                    titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].BackHatchStyle = fd.BackHatchStyle;
                    titleList[Convert.ToInt32(lvwTitle.SelectedItems[0].SubItems[0].Text)].BackGradientStyle = fd.BackGradientStyle;
                }
            }
        }

        #endregion

        #region 数据标签
        private void cboPieLabelStyleInit()
        {
            cboPieLabelStyle.Items.Add("Inside");
            cboPieLabelStyle.Items.Add("Outside");
            cboPieLabelStyle.SelectedIndex = 0;
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
        #endregion

        #region 外观
        private void cboLightStyleInit()
        {
            cboLightStyle.Items.Add(LightStyle.None);
            cboLightStyle.Items.Add(LightStyle.Realistic);
            cboLightStyle.Items.Add(LightStyle.Simplistic);
            cboLightStyle.SelectedIndex = 0;
        }

        private void cboPaletteInit()
        {
            cboPalette.Items.Add(ChartColorPalette.None);
            cboPalette.Items.Add(ChartColorPalette.Berry);
            cboPalette.Items.Add(ChartColorPalette.Bright);
            cboPalette.Items.Add(ChartColorPalette.BrightPastel);
            cboPalette.Items.Add(ChartColorPalette.Chocolate);
            cboPalette.Items.Add(ChartColorPalette.EarthTones);
            cboPalette.Items.Add(ChartColorPalette.Excel);
            cboPalette.Items.Add(ChartColorPalette.Fire);
            cboPalette.Items.Add(ChartColorPalette.Grayscale);
            cboPalette.Items.Add(ChartColorPalette.Light);
            cboPalette.Items.Add(ChartColorPalette.Pastel);
            cboPalette.Items.Add(ChartColorPalette.SeaGreen);
            cboPalette.Items.Add(ChartColorPalette.SemiTransparent);
            cboPalette.SelectedIndex = 0;
        }

        private void cboPieDrawingStyleInit()
        {
            //cboPieDrawingStyle.Items.Add(PieDrawingStyle.Default);//PieDrawingStyle不能调用
            cboPieDrawingStyle.Items.Add("Default");
            cboPieDrawingStyle.Items.Add("SoftEdge");
            cboPieDrawingStyle.Items.Add("Concave");
            cboPieDrawingStyle.SelectedIndex = 0;
        }

        private void chkEnable3D_CheckedChanged(object sender, EventArgs e)
        {
            gbx3DStyle.Visible = chkEnable3D.Checked;
        }

        private void btnChartStyle_Click(object sender, EventArgs e)
        {
            using (BorderAndBackEditor fd = new BorderAndBackEditor())
            {
                fd.BorderColor = PMSChartAppearance.BorderlineColor;
                fd.BorderDashStyle = PMSChartAppearance.BorderlineDashStyle;
                fd.BorderWidth = PMSChartAppearance.BorderlineWidth;
                fd.BackColor = PMSChartAppearance.BackColor;
                fd.BackSecondColor = PMSChartAppearance.BackSecondaryColor;
                fd.BackHatchStyle = PMSChartAppearance.BackHatchStyle;
                fd.BackGradientStyle = PMSChartAppearance.BackGradientStyle;
                if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    PMSChartAppearance.BorderlineColor = fd.BorderColor;
                    PMSChartAppearance.BorderlineDashStyle = fd.BorderDashStyle;
                    PMSChartAppearance.BorderlineWidth = fd.BorderWidth;
                    PMSChartAppearance.BackColor = fd.BackColor;
                    PMSChartAppearance.BackSecondaryColor = fd.BackSecondColor;
                    PMSChartAppearance.BackHatchStyle = fd.BackHatchStyle;
                    PMSChartAppearance.BackGradientStyle = fd.BackGradientStyle;
                }
            }
        }

        private void btnChartAreaStyle_Click(object sender, EventArgs e)
        {
            using (BorderAndBackEditor fd = new BorderAndBackEditor())
            {
                fd.BorderColor = ChartArea1.BorderColor;
                fd.BorderDashStyle = ChartArea1.BorderDashStyle;
                fd.BorderWidth = ChartArea1.BorderWidth;
                fd.BackColor = ChartArea1.BackColor;
                fd.BackSecondColor = ChartArea1.BackSecondaryColor;
                fd.BackHatchStyle = ChartArea1.BackHatchStyle;
                fd.BackGradientStyle = ChartArea1.BackGradientStyle;
                if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ChartArea1.BorderColor = fd.BorderColor;
                    ChartArea1.BorderDashStyle = fd.BorderDashStyle;
                    ChartArea1.BorderWidth = fd.BorderWidth;
                    ChartArea1.BackColor = fd.BackColor;
                    ChartArea1.BackSecondaryColor = fd.BackSecondColor;
                    ChartArea1.BackHatchStyle = fd.BackHatchStyle;
                    ChartArea1.BackGradientStyle = fd.BackGradientStyle;
                }
            }
        }
        #endregion

        #region 图例
        private void InitLegendList()
        {
            legend1.DockedToChartArea = "ChartArea1";
            legend1.IsDockedInsideChartArea = false;
            if (legendList.Count != 0)
                legendList[0].SetLegend(legend1);
            chkLegendEnable.Checked = legend1.Enabled;
            chkLegendDockedInside.Checked = legend1.IsDockedInsideChartArea;
            cboLegendAlignment.SelectedItem = legend1.Alignment;
            cboLegendDocking.SelectedItem = legend1.Docking;
            legendFont = legend1.Font;
            legendForeColor = legend1.ForeColor;
            cboLegendStyle.SelectedItem = legend1.LegendStyle;
            cboTableStyle.SelectedItem = legend1.TableStyle;
        }

        private void cboLegendAlignmentInit()
        {
            cboLegendAlignment.Items.Add(StringAlignment.Near);
            cboLegendAlignment.Items.Add(StringAlignment.Center);
            cboLegendAlignment.Items.Add(StringAlignment.Far);
            cboLegendAlignment.SelectedIndex = 0;
        }

        private void cboLegendDockingInit()
        {
            cboLegendDocking.Items.Add(Docking.Top);
            cboLegendDocking.Items.Add(Docking.Right);
            cboLegendDocking.Items.Add(Docking.Bottom);
            cboLegendDocking.Items.Add(Docking.Left);
            cboLegendDocking.SelectedIndex = 0;
        }

        private void cboLegendStyleInit()
        {
            cboLegendStyle.Items.Add(LegendStyle.Table);
            cboLegendStyle.Items.Add(LegendStyle.Row);
            cboLegendStyle.Items.Add(LegendStyle.Column);
            cboLegendStyle.SelectedIndex = 0;
        }

        private void cboTableStyleInit()
        {
            cboTableStyle.Items.Add(LegendTableStyle.Auto);
            cboTableStyle.Items.Add(LegendTableStyle.Wide);
            cboTableStyle.Items.Add(LegendTableStyle.Tall);
            cboTableStyle.SelectedIndex = 0;
        }

        private void btnLegendFont_Click(object sender, EventArgs e)
        {
            using (FontDialog fd = new FontDialog())
            {
                fd.ShowColor = true;
                fd.Font = legendFont;
                fd.Color = legendForeColor;
                if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    legendFont = fd.Font;
                    legendForeColor = fd.Color;
                }
            }
        }

        private void btnLegendStyle_Click(object sender, EventArgs e)
        {
            using (BorderAndBackEditor fd = new BorderAndBackEditor())
            {
                fd.BorderColor = legend1.BorderColor;
                fd.BorderDashStyle = legend1.BorderDashStyle;
                fd.BorderWidth = legend1.BorderWidth;
                fd.BackColor = legend1.BackColor;
                fd.BackSecondColor = legend1.BackSecondaryColor;
                fd.BackHatchStyle = legend1.BackHatchStyle;
                fd.BackGradientStyle = legend1.BackGradientStyle;
                if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    legend1.BorderColor = fd.BorderColor;
                    legend1.BorderDashStyle = fd.BorderDashStyle;
                    legend1.BorderWidth = fd.BorderWidth;
                    legend1.BackColor = fd.BackColor;
                    legend1.BackSecondaryColor = fd.BackSecondColor;
                    legend1.BackHatchStyle = fd.BackHatchStyle;
                    legend1.BackGradientStyle = fd.BackGradientStyle;
                }
            }
        }

        private void btnLegendText_Click(object sender, EventArgs e)
        {
            KeywordsStringEditorForm dialog = new KeywordsStringEditorForm(txtLegendText.Text, "LegendCellColumn", "LegendText", 9);
            dialog.KeywordsRegistry = new KeywordsRegistry();
            dialog.ShowDialog();
            txtLegendText.Text = dialog.ResultString;
        }
        #endregion

        #region 分组
        /// <summary>
        /// 初始化主统计数据源绑定ComboBox
        /// </summary>
        private void cboMajorBindingInit()
        {
            ArrayList al = new ArrayList();
            cboMajorBinding.Items.Clear();
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
                    PmsField pf = new PmsField();
                    pf.fieldName = sf.RecordField;
                    pf.fieldDescription = sf.Name;
                    lp.Add(pf);
                    al.Add(new ComboxItem(pf.fieldDescription, pf.fieldName));
                    //cboMajorBinding.Items.Add(pf.fieldName);

                    string typ = sf.DataType.ToUpper();
                    if (typ.Equals("SYSTEM.DATETIME", StringComparison.InvariantCultureIgnoreCase))
                    {
                        SplitDateTime(lp, sf, cboMajorBinding, al, "Year");
                        SplitDateTime(lp, sf, cboMajorBinding, al, "Month");
                        SplitDateTime(lp, sf, cboMajorBinding, al, "Day");
                        SplitDateTime(lp, sf, cboMajorBinding, al, "Hour");
                        SplitDateTime(lp, sf, cboMajorBinding, al, "Minute");
                        SplitDateTime(lp, sf, cboMajorBinding, al, "Second");
                    }
                }
            }
            cboMajorBinding.DataSource = al;
            cboMajorBinding.DisplayMember = "Name";
            cboMajorBinding.ValueMember = "Value";
        }

        /// <summary>
        /// 初始化次统计数据源绑定ComboBox
        /// </summary>
        private void cboMinorBindingInit()
        {
            ArrayList al = new ArrayList();
            cboMinorBinding.Items.Clear();
            //cboMinorBinding.Items.Add("");
            al.Add(new ComboxItem());
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
                    PmsField pf = new PmsField();
                    pf.fieldName = sf.RecordField;
                    pf.fieldDescription = sf.Name;
                    lp.Add(pf);
                    al.Add(new ComboxItem(pf.fieldDescription, pf.fieldName));
                    //cboMinorBinding.Items.Add(pf.fieldName);

                    string typ = sf.DataType.ToUpper();
                    if (typ.Equals("SYSTEM.DATETIME", StringComparison.InvariantCultureIgnoreCase))
                    {
                        SplitDateTime(lp, sf, cboMinorBinding, al, "Year");
                        SplitDateTime(lp, sf, cboMinorBinding, al, "Month");
                        SplitDateTime(lp, sf, cboMinorBinding, al, "Day");
                        SplitDateTime(lp, sf, cboMinorBinding, al, "Hour");
                        SplitDateTime(lp, sf, cboMinorBinding, al, "Minute");
                        SplitDateTime(lp, sf, cboMinorBinding, al, "Second");
                    }
                }
            }
            cboMinorBinding.DataSource = al;
            cboMinorBinding.DisplayMember = "Name";
            cboMinorBinding.ValueMember = "Value";
        }

        private void SplitDateTime(List<PmsField> lp, SourceField sf, ComboBox comboBox, ArrayList al, string dateType)
        {
            PmsField pf = new PmsField();
            pf.fieldName = sf.RecordField + "_" + dateType;
            pf.fieldDescription = sf.Name + "." + dateType;
            lp.Add(pf);
            al.Add(new ComboxItem(pf.fieldDescription, pf.fieldName));
            //comboBox.Items.Add(pf.fieldName);
        }

        /// <summary>
        /// 初始化统计字段数据源绑定ComboBox
        /// </summary>
        private void cboValueBindingInit()
        {
            cboValueBinding.Items.Clear();
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
                        cboValueBinding.Items.Add(pf.fieldName);
                    }
                }
            }
        }
        private void cboValueFxInit()
        {
            cboValueFx.Items.Add(Functions.Count);
            cboValueFx.Items.Add(Functions.Sum);
            cboValueFx.Items.Add(Functions.Avg);
            cboValueFx.Items.Add(Functions.Max);
            cboValueFx.Items.Add(Functions.Min);
            cboValueFx.SelectedIndex = 0;
        }
        private void cboMajorSortInit()
        {
            ArrayList al = new ArrayList();
            al.Add(new ComboxItem("不排序", SortType.NoSort));
            al.Add(new ComboxItem("升序", SortType.Asc));
            al.Add(new ComboxItem("降序", SortType.Desc));

            cboMajorSort.DataSource = al;
            cboMajorSort.DisplayMember = "Name";
            cboMajorSort.ValueMember = "Value";
            cboMajorSort.SelectedIndex = 0;
        }
        private void cboMinorSortInit()
        {
            ArrayList al = new ArrayList();
            al.Add(new ComboxItem("不排序", SortType.NoSort));
            al.Add(new ComboxItem("升序", SortType.Asc));
            al.Add(new ComboxItem("降序", SortType.Desc));

            cboMinorSort.DataSource = al;
            cboMinorSort.DisplayMember = "Name";
            cboMinorSort.ValueMember = "Value";
            cboMinorSort.SelectedIndex = 0;
        }

        private void InitGroupSource()
        {
            groupSource = ChartParent.GroupSource.Clone();
            cboMajorBindingInit();
            cboMinorBindingInit();
            cboValueBindingInit();
            chkEnableGroup.Checked = groupSource.Enable;
            if (groupSource.MajorBinding != null)
                cboMajorBinding.SelectedValue = groupSource.MajorBinding;
            if (groupSource.MinorBinding != null)
                cboMinorBinding.SelectedValue = groupSource.MinorBinding;
            cboValueBinding.SelectedItem = groupSource.ValueBinding;
            cboValueFx.SelectedItem = groupSource.ValueFx;
            cboMajorSort.SelectedValue = groupSource.MajorSort;
            cboMinorSort.SelectedValue = groupSource.MinorSort;
        }

        private void GroupSourceDataCommit()
        {
            groupSource.Enable = chkEnableGroup.Checked;
            groupSource.MajorBinding = (string)cboMajorBinding.SelectedValue;
            groupSource.MinorBinding = (string)cboMinorBinding.SelectedValue;
            groupSource.ValueBinding = (string)cboValueBinding.SelectedItem;
            groupSource.ValueFx = (Functions)cboValueFx.SelectedItem;
            groupSource.MajorSort = (SortType)cboMajorSort.SelectedValue;
            groupSource.MinorSort = (SortType)cboMinorSort.SelectedValue;
            ChartParent.GroupSource = groupSource.Clone();
        }
        #endregion

        #region 由于分组情况堆叠的位置不正确，暂时屏蔽一些功能
        private void chkEnableGroup_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnableGroup.Checked)
            {
                chkCollectedSliceExpoded.Enabled = false;
                chkCollectedSliceExpoded.Checked = false;

                cboPieLabelStyle.Items.Clear();
                cboPieLabelStyle.Items.Add("Inside");
                cboPieLabelStyle.SelectedIndex = 0;
            }
            else
            {
                chkCollectedSliceExpoded.Enabled = true;

                cboPieLabelStyle.Items.Clear();
                cboPieLabelStyle.Items.Add("Inside");
                cboPieLabelStyle.Items.Add("Outside");
                cboPieLabelStyle.SelectedIndex = 0;
            }
        }
        #endregion
    }
}
