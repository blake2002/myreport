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
    public partial class BarApperenceFrm : Form
    {
        public BarApperenceFrm()
        {
            InitializeComponent();
            cboTitlePositionInit();
            cboTextOrientationInit();
            cboLabelBorderType.Init();
            cboLightStyleInit();
            cboBarDrawingStyleInit();
            cboLegendAlignmentInit();
            cboLegendDockingInit();
            cboLegendStyleInit();
            cboTableStyleInit();
            cboValueFxInit();
            cboMajorSortInit();
            cboMinorSortInit();
            cboXAxisLineDashStyle.Init();
            cboXMajorGridDashStyle.Init();
            cboXMinorGridDashStyle.Init();
            cboXMajorTickDashStyle.Init();
            cboXMinorTickDashStyle.Init();
            cboXAxisArrowStyleInit();
            cboXMajorTickMarkStyleInit();
            cboXMinorTickMarkStyleInit();
            cboXAxisTitleAlignmentInit();
            cboXAxisTitleTextOrientationInit();
            cboXAxisEnableStyleInit();
            cboYAxisLineDashStyle.Init();
            cboYMajorGridDashStyle.Init();
            cboYMinorGridDashStyle.Init();
            cboYMajorTickDashStyle.Init();
            cboYMinorTickDashStyle.Init();
            cboYAxisEnableStyleInit();
            cboYAxisArrowStyleInit();
            cboYMajorTickMarkStyleInit();
            cboYMinorTickMarkStyleInit();
            cboYAxisTitleAlignmentInit();
            cboYAxisTitleTextOrientationInit();
        }

        public DataSource dsApply;
        public BarChart ChartParent;
        public PMSChartApp PMSChartAppearance;
        public GroupSource groupSource;
        public YAxisArea YAxisAreas;
        public List<PMSLegend> legendList = new List<PMSLegend>();
        public List<PMSSeries> seriesList = new List<PMSSeries>();
        public List<PMSTitle> titleList = new List<PMSTitle>();
        public List<PMSChartArea> chartAreaList = new List<PMSChartArea>();

        private ChartArea ChartArea1 = new ChartArea("ChartArea1");
        private List<Series> mySeriesList = new List<Series>();
        public List<ChartArea> yAreaList = new List<ChartArea>();
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
            SeriesListCommit();
            TitleListCommit();
            GroupSourceDataCommit();
            ChartAreaCommit();
            YAxisAreaCommit();

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


        private void BarApperenceFrm_Load(object sender, EventArgs e)
        {
            InitChartAreaList();
            InitYAxisArea();
            InitSeiesList();
            InitLegendList();
            InitTitleList();
            InitGroupSource();
            //propertyTree1.SelectedPaneNode = ppPane7.PaneNode;//默认选择“数据源”页
        }

        private void ChartAreaCommit()
        {
            AxisCommit();
            ChartArea1.Area3DStyle.Enable3D = chkEnable3D.Checked;
            ChartArea1.Area3DStyle.LightStyle = (LightStyle)cboLightStyle.SelectedItem;
            ChartArea1.Area3DStyle.Inclination = (int)nudInclination.Value;
            ChartArea1.Area3DStyle.Rotation = (int)nudRotation.Value;
            ChartArea1.Area3DStyle.WallWidth = (int)nudWallWidth.Value;
            ChartArea1.Area3DStyle.IsClustered = true;
            chartAreaList.Clear();
            chartAreaList.Add(new PMSChartArea(ChartArea1));
        }

        private void InitChartAreaList()
        {
            if (chartAreaList.Count != 0)
                chartAreaList[0].SetChartArea(ChartArea1);
            chkEnable3D.Checked = gbx3DStyle.Visible = ChartArea1.Area3DStyle.Enable3D;
            cboLightStyle.SelectedItem = ChartArea1.Area3DStyle.LightStyle;
            nudInclination.Value = ChartArea1.Area3DStyle.Inclination;
            nudRotation.Value = ChartArea1.Area3DStyle.Rotation;
            nudWallWidth.Value = ChartArea1.Area3DStyle.WallWidth;
            InitAxis();
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
            else if (data is List<ChartArea>)
            {
                List<ChartArea> areatemp = data as List<ChartArea>;
                if (areatemp != null)
                {
                    foreach (ChartArea item in areatemp)
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
            else if (data is List<Series>)
            {
                List<Series> areatemp = data as List<Series>;
                if (areatemp != null)
                {
                    foreach (Series item in areatemp)
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
        private void cboBarLabelStyleInit()
        {
            if (ChartParent.ChartType == BarChartType.Bar
                || ChartParent.ChartType == BarChartType.StackedBar
                || ChartParent.ChartType == BarChartType.StackedBar100)
            {
                cboBarLabelStyle.Items.Add("Outside");
                cboBarLabelStyle.Items.Add("Left");
                cboBarLabelStyle.Items.Add("Right");
                cboBarLabelStyle.Items.Add("Center");
            }
            else
            {
                cboBarLabelStyle.Items.Add("Auto");
                cboBarLabelStyle.Items.Add("Top");
                cboBarLabelStyle.Items.Add("Bottom");
                cboBarLabelStyle.Items.Add("Right");
                cboBarLabelStyle.Items.Add("Left");
                cboBarLabelStyle.Items.Add("TopLeft");
                cboBarLabelStyle.Items.Add("TopRight");
                cboBarLabelStyle.Items.Add("BottomLeft");
                cboBarLabelStyle.Items.Add("BottomRight");
                cboBarLabelStyle.Items.Add("Center");
            }
            cboBarLabelStyle.SelectedIndex = 0;
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

        private void cboBarDrawingStyleInit()
        {
            cboBarDrawingStyle.Items.Add(BarDrawingStyle.Default);
            cboBarDrawingStyle.Items.Add(BarDrawingStyle.Cylinder);
            cboBarDrawingStyle.Items.Add(BarDrawingStyle.Emboss);
            cboBarDrawingStyle.Items.Add(BarDrawingStyle.LightToDark);
            cboBarDrawingStyle.Items.Add(BarDrawingStyle.Wedge);
            cboBarDrawingStyle.SelectedIndex = 0;
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

        #region 曲线
        private void SeriesListCommit()
        {
            seriesList.Clear();
            if (lvwSeries.SelectedItems.Count != 0)
            {
                mySeriesList[Convert.ToInt32(lvwSeries.SelectedItems[0].SubItems[0].Text)].Enabled = chkSeriesEnable.Checked;
                mySeriesList[Convert.ToInt32(lvwSeries.SelectedItems[0].SubItems[0].Text)].Name = txtSeriesName.Text;
                mySeriesList[Convert.ToInt32(lvwSeries.SelectedItems[0].SubItems[0].Text)].SetCustomProperty("YBindingField", (cboYBindingField.SelectedItem == null) ? string.Empty : cboYBindingField.SelectedItem.ToString());
                mySeriesList[Convert.ToInt32(lvwSeries.SelectedItems[0].SubItems[0].Text)].Color = ccboSeriesColor.SelectedItem;
                //if (chkYAxisAreaEnable.Checked)
                mySeriesList[Convert.ToInt32(lvwSeries.SelectedItems[0].SubItems[0].Text)]["YAxisAreaName"] = Convert.ToString(cboYAxisList.SelectedItem);


                lvwSeries.Items[Convert.ToInt32(lvwSeries.SelectedItems[0].SubItems[0].Text)].SubItems[1].Text = txtSeriesName.Text;
            }
            for (int i = 0; i < mySeriesList.Count; i++)
            {
                Series series1 = mySeriesList[i];
                series1.SetCustomProperty("DrawingStyle", cboBarDrawingStyle.SelectedItem.ToString());

                if (rbtnNoLabel.Checked)
                {
                    series1.IsValueShownAsLabel = false;
                    series1.Label = string.Empty;
                }
                if (rbtnValueAsLabel.Checked)
                {
                    series1.IsValueShownAsLabel = true;
                    series1.Label = string.Empty;
                }
                if (rbtnLabel.Checked)
                {
                    series1.IsValueShownAsLabel = false;
                    series1.Label = txtLabel.Text;
                }
                series1.Font = labelFont;
                series1.LabelForeColor = labelForeColor;
                series1.LabelBackColor = ccboLabelBackColor.SelectedItem;
                series1.LabelBorderColor = ccboLabelBorderColor.SelectedItem;
                series1.LabelBorderDashStyle = (ChartDashStyle)cboLabelBorderType.SelectedItem;
                series1.LabelBorderWidth = (int)nudLabelBorderWidth.Value;
                if (ChartParent.ChartType == BarChartType.Bar || ChartParent.ChartType == BarChartType.StackedBar || ChartParent.ChartType == BarChartType.StackedBar100)
                    series1.SetCustomProperty("BarLabelStyle", cboBarLabelStyle.SelectedItem.ToString());
                else
                    series1.SetCustomProperty("LabelStyle", cboBarLabelStyle.SelectedItem.ToString());
                series1.SetCustomProperty("TotalPointWidth", nudPointWidth.Value.ToString());

                series1.LegendText = txtLegendText.Text;

                series1.SetCustomProperty("XBindingField", (cboXBindingField.SelectedItem == null) ? string.Empty : cboXBindingField.SelectedItem.ToString());

                seriesList.Add(new PMSSeries(series1));
            }
        }

        private void cboYAxisListInit()
        {
            cboYAxisList.Items.Clear();
            for (int i = 0; i < yAreaList.Count; i++)
            {
                if (yAreaList[i].Name.StartsWith("YAxis"))
                {
                    cboYAxisList.Items.Add(yAreaList[i].Name);
                }
            }
            if (cboYAxisList.Items.Count != 0)
                cboYAxisList.SelectedIndex = 0;
        }

        private void InitSeiesList()
        {
            cboYBindingFieldInit();
            cboXBindingFieldInit();
            cboBarLabelStyleInit();
            cboYAxisListInit();
            ConvertToSeriseList(seriesList);
            InitlvwSeries();

            Series series1 = new Series();
            if (mySeriesList.Count != 0)
                series1 = mySeriesList[0];
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
            if (ChartParent.ChartType == BarChartType.Bar || ChartParent.ChartType == BarChartType.StackedBar || ChartParent.ChartType == BarChartType.StackedBar100)
                cboBarLabelStyle.SelectedItem = (series1.GetCustomProperty("BarLabelStyle") == null) ? "Outside" : series1.GetCustomProperty("BarLabelStyle");
            else
                cboBarLabelStyle.SelectedItem = (series1.GetCustomProperty("LabelStyle") == null) ? "Auto" : series1.GetCustomProperty("LabelStyle");
            nudPointWidth.Value = (series1.GetCustomProperty("TotalPointWidth") == null) ? (decimal)0.8 : Convert.ToDecimal(series1.GetCustomProperty("TotalPointWidth"));

            txtLegendText.Text = series1.LegendText;

            cboXBindingField.SelectedItem = (series1.GetCustomProperty("XBindingField") == null) ? string.Empty : series1.GetCustomProperty("XBindingField");

            cboBarDrawingStyle.SelectedItem = (series1.GetCustomProperty("DrawingStyle") == null) ? BarDrawingStyle.Default : (BarDrawingStyle)Enum.Parse(typeof(BarDrawingStyle), series1.GetCustomProperty("DrawingStyle"));

            //lblYAxisList.Visible = chkYAxisAreaEnable.Checked;
            //cboYAxisList.Visible = chkYAxisAreaEnable.Checked;
        }

        /// <summary>
        /// 转换List&lt;PMSSeries&gt;到List&lt;Series&gt;
        /// </summary>
        /// <param name="seriesList"></param>
        private void ConvertToSeriseList(List<PMSSeries> seriesList)
        {
            if (seriesList.Count == 0)
            {
                Series pt = new Series();
                pt.ChartType = (SeriesChartType)ChartParent.ChartType;
                pt.Name = GetNameFromList(mySeriesList, "序列");
                PMSSeries series = new PMSSeries(pt);
                seriesList.Add(series);
            }
            for (int i = 0; i < seriesList.Count; i++)
            {
                Series series = new Series();
                seriesList[i].SetSeriesValue(series);
                mySeriesList.Add(series);
            }
        }

        private void InitlvwSeries()
        {
            gbxSeries.Visible = false;
            lvwSeries.Items.Clear();
            for (int i = 0; i < mySeriesList.Count; i++)
            {
                string[] value = { i.ToString(), mySeriesList[i].Name };
                ListViewItem lvm = new ListViewItem(value);
                lvwSeries.Items.Add(lvm);
            }
        }

        private string _seriesSelectedIndex = string.Empty;
        private void lvwSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwSeries.SelectedItems.Count == 0)
            { gbxSeries.Visible = false; }
            else
            {
                gbxSeries.Visible = true;
                if (lvwSeries.SelectedItems[0].SubItems[0].Text != _seriesSelectedIndex)
                {
                    if (!string.IsNullOrEmpty(_seriesSelectedIndex))
                    {
                        mySeriesList[Convert.ToInt32(_seriesSelectedIndex)].Enabled = chkSeriesEnable.Checked;
                        mySeriesList[Convert.ToInt32(_seriesSelectedIndex)].Name = txtSeriesName.Text;
                        mySeriesList[Convert.ToInt32(_seriesSelectedIndex)].SetCustomProperty("YBindingField", (cboYBindingField.SelectedItem == null) ? string.Empty : cboYBindingField.SelectedItem.ToString());
                        mySeriesList[Convert.ToInt32(_seriesSelectedIndex)].Color = ccboSeriesColor.SelectedItem;
                        //if (chkYAxisAreaEnable.Checked)
                        mySeriesList[Convert.ToInt32(_seriesSelectedIndex)]["YAxisAreaName"] = Convert.ToString(cboYAxisList.SelectedItem);

                        lvwSeries.Items[Convert.ToInt32(_seriesSelectedIndex)].SubItems[1].Text = txtSeriesName.Text;
                    }
                    Series series = mySeriesList[Convert.ToInt32(lvwSeries.SelectedItems[0].SubItems[0].Text)];
                    chkSeriesEnable.Checked = series.Enabled;
                    txtSeriesName.Text = series.Name;
                    cboYBindingField.SelectedItem = (series.GetCustomProperty("YBindingField") == null) ? string.Empty : series.GetCustomProperty("YBindingField");
                    ccboSeriesColor.SelectedItem = series.Color;
                    cboYAxisList.SelectedItem = series["YAxisAreaName"];

                    _seriesSelectedIndex = lvwSeries.SelectedItems[0].SubItems[0].Text;
                }
            }
        }

        private void btnSeriesAdd_Click(object sender, EventArgs e)
        {
            Series pt = new Series();
            pt.ChartType = (SeriesChartType)ChartParent.ChartType;
            pt.Name = GetNameFromList(mySeriesList, "序列");
            mySeriesList.Add(pt);
            _seriesSelectedIndex = string.Empty;
            InitlvwSeries();
            lvwSeries.Items[lvwSeries.Items.Count - 1].Selected = true;
            if (cboYAxisList.Items.Count != 0)
                cboYAxisList.SelectedIndex = 0;
        }

        private void btnSeriesDel_Click(object sender, EventArgs e)
        {
            if (lvwSeries.Items.Count > 1 && lvwSeries.SelectedItems.Count != 0)
            {
                mySeriesList.RemoveAt(Convert.ToInt32(lvwSeries.SelectedItems[0].SubItems[0].Text));
                _seriesSelectedIndex = string.Empty;
                InitlvwSeries();
            }
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

        #region 坐标轴
        private void cboXAxisArrowStyleInit()
        {
            ArrayList al = new ArrayList();
            al.Add(new ComboxItem("无", AxisArrowStyle.None));
            al.Add(new ComboxItem("线条型", AxisArrowStyle.Lines));
            al.Add(new ComboxItem("尖三角型", AxisArrowStyle.SharpTriangle));
            al.Add(new ComboxItem("三角形型", AxisArrowStyle.Triangle));

            cboXAxisArrowStyle.DataSource = al;
            cboXAxisArrowStyle.DisplayMember = "Name";
            cboXAxisArrowStyle.ValueMember = "Value";
            cboXAxisArrowStyle.SelectedIndex = 0;
        }
        private void cboYAxisArrowStyleInit()
        {
            ArrayList al = new ArrayList();
            al.Add(new ComboxItem("无", AxisArrowStyle.None));
            al.Add(new ComboxItem("线条型", AxisArrowStyle.Lines));
            al.Add(new ComboxItem("尖三角型", AxisArrowStyle.SharpTriangle));
            al.Add(new ComboxItem("三角形型", AxisArrowStyle.Triangle));

            cboYAxisArrowStyle.DataSource = al;
            cboYAxisArrowStyle.DisplayMember = "Name";
            cboYAxisArrowStyle.ValueMember = "Value";
            cboYAxisArrowStyle.SelectedIndex = 0;
        }
        private void cboXMajorTickMarkStyleInit()
        {
            ArrayList al = new ArrayList();
            al.Add(new ComboxItem("不显示", TickMarkStyle.None));
            al.Add(new ComboxItem("内部", TickMarkStyle.InsideArea));
            al.Add(new ComboxItem("外部", TickMarkStyle.OutsideArea));
            al.Add(new ComboxItem("交错", TickMarkStyle.AcrossAxis));

            cboXMajorTickMarkStyle.DataSource = al;
            cboXMajorTickMarkStyle.DisplayMember = "Name";
            cboXMajorTickMarkStyle.ValueMember = "Value";
            cboXMajorTickMarkStyle.SelectedIndex = 0;
        }
        private void cboYMajorTickMarkStyleInit()
        {
            ArrayList al = new ArrayList();
            al.Add(new ComboxItem("不显示", TickMarkStyle.None));
            al.Add(new ComboxItem("内部", TickMarkStyle.InsideArea));
            al.Add(new ComboxItem("外部", TickMarkStyle.OutsideArea));
            al.Add(new ComboxItem("交错", TickMarkStyle.AcrossAxis));

            cboYMajorTickMarkStyle.DataSource = al;
            cboYMajorTickMarkStyle.DisplayMember = "Name";
            cboYMajorTickMarkStyle.ValueMember = "Value";
            cboYMajorTickMarkStyle.SelectedIndex = 0;
        }
        private void cboXMinorTickMarkStyleInit()
        {
            ArrayList al = new ArrayList();
            al.Add(new ComboxItem("不显示", TickMarkStyle.None));
            al.Add(new ComboxItem("内部", TickMarkStyle.InsideArea));
            al.Add(new ComboxItem("外部", TickMarkStyle.OutsideArea));
            al.Add(new ComboxItem("交错", TickMarkStyle.AcrossAxis));

            cboXMinorTickMarkStyle.DataSource = al;
            cboXMinorTickMarkStyle.DisplayMember = "Name";
            cboXMinorTickMarkStyle.ValueMember = "Value";
            cboXMinorTickMarkStyle.SelectedIndex = 0;
        }
        private void cboYMinorTickMarkStyleInit()
        {
            ArrayList al = new ArrayList();
            al.Add(new ComboxItem("不显示", TickMarkStyle.None));
            al.Add(new ComboxItem("内部", TickMarkStyle.InsideArea));
            al.Add(new ComboxItem("外部", TickMarkStyle.OutsideArea));
            al.Add(new ComboxItem("交错", TickMarkStyle.AcrossAxis));

            cboYMinorTickMarkStyle.DataSource = al;
            cboYMinorTickMarkStyle.DisplayMember = "Name";
            cboYMinorTickMarkStyle.ValueMember = "Value";
            cboYMinorTickMarkStyle.SelectedIndex = 0;
        }
        private void cboXAxisTitleAlignmentInit()
        {
            ArrayList al = new ArrayList();
            al.Add(new ComboxItem("Near", StringAlignment.Near));
            al.Add(new ComboxItem("Center", StringAlignment.Center));
            al.Add(new ComboxItem("Far", StringAlignment.Far));

            cboXAxisTitleAlignment.DataSource = al;
            cboXAxisTitleAlignment.DisplayMember = "Name";
            cboXAxisTitleAlignment.ValueMember = "Value";
            cboXAxisTitleAlignment.SelectedIndex = 0;
        }
        private void cboYAxisTitleAlignmentInit()
        {
            ArrayList al = new ArrayList();
            al.Add(new ComboxItem("Near", StringAlignment.Near));
            al.Add(new ComboxItem("Center", StringAlignment.Center));
            al.Add(new ComboxItem("Far", StringAlignment.Far));

            cboYAxisTitleAlignment.DataSource = al;
            cboYAxisTitleAlignment.DisplayMember = "Name";
            cboYAxisTitleAlignment.ValueMember = "Value";
            cboYAxisTitleAlignment.SelectedIndex = 0;
        }
        private void cboXAxisTitleTextOrientationInit()
        {
            ArrayList al = new ArrayList();
            al.Add(new ComboxItem("Auto", TextOrientation.Auto));
            al.Add(new ComboxItem("Horizontal", TextOrientation.Horizontal));
            al.Add(new ComboxItem("Rotated270", TextOrientation.Rotated270));
            al.Add(new ComboxItem("Rotated90", TextOrientation.Rotated90));
            al.Add(new ComboxItem("Stacked", TextOrientation.Stacked));

            cboXAxisTitleTextOrientation.DataSource = al;
            cboXAxisTitleTextOrientation.DisplayMember = "Name";
            cboXAxisTitleTextOrientation.ValueMember = "Value";
            cboXAxisTitleTextOrientation.SelectedIndex = 0;
        }
        private void cboYAxisTitleTextOrientationInit()
        {
            ArrayList al = new ArrayList();
            al.Add(new ComboxItem("Auto", TextOrientation.Auto));
            al.Add(new ComboxItem("Horizontal", TextOrientation.Horizontal));
            al.Add(new ComboxItem("Rotated270", TextOrientation.Rotated270));
            al.Add(new ComboxItem("Rotated90", TextOrientation.Rotated90));
            al.Add(new ComboxItem("Stacked", TextOrientation.Stacked));

            cboYAxisTitleTextOrientation.DataSource = al;
            cboYAxisTitleTextOrientation.DisplayMember = "Name";
            cboYAxisTitleTextOrientation.ValueMember = "Value";
            cboYAxisTitleTextOrientation.SelectedIndex = 0;
        }
        private void cboXAxisEnableStyleInit()
        {
            ArrayList al = new ArrayList();
            al.Add(new ComboxItem("自动", AxisEnabled.Auto));
            al.Add(new ComboxItem("显示", AxisEnabled.True));
            al.Add(new ComboxItem("不显示", AxisEnabled.False));

            cboXAxisEnableStyle.DataSource = al;
            cboXAxisEnableStyle.DisplayMember = "Name";
            cboXAxisEnableStyle.ValueMember = "Value";
            cboXAxisEnableStyle.SelectedIndex = 0;
        }
        private void cboYAxisEnableStyleInit()
        {
            ArrayList al = new ArrayList();
            al.Add(new ComboxItem("自动", AxisEnabled.Auto));
            al.Add(new ComboxItem("显示", AxisEnabled.True));
            al.Add(new ComboxItem("不显示", AxisEnabled.False));

            cboYAxisEnableStyle.DataSource = al;
            cboYAxisEnableStyle.DisplayMember = "Name";
            cboYAxisEnableStyle.ValueMember = "Value";
            cboYAxisEnableStyle.SelectedIndex = 0;
        }
        private void InitAxis()
        {
            InitXAxis();
        }

        private void AxisCommit()
        {
            XAxisCommit();
        }

        private void InitXAxis()
        {
            Axis axis = ChartArea1.AxisX;
            cboXAxisEnableStyle.SelectedValue = axis.Enabled;

            chkXAxisLabelEnable.Checked = axis.LabelStyle.Enabled;
            chkXAxisEndLabel.Checked = axis.LabelStyle.IsEndLabelVisible;
            chkXAxislabelAutoFit.Checked = axis.IsLabelAutoFit;
            pnlXAxisLabel.Enabled = !axis.IsLabelAutoFit;
            nudXAxisLabelAngle.Value = axis.LabelStyle.Angle;

            cboXAxisArrowStyle.SelectedValue = axis.ArrowStyle;
            ccboXAxisLineColor.SelectedItem = axis.LineColor;
            cboXAxisLineDashStyle.SelectedItem = axis.LineDashStyle;
            nudXAxisLineWidth.Value = axis.LineWidth;

            chkXMajorGridEnable.Checked = axis.MajorGrid.Enabled;
            ccboXMajorGridColor.SelectedItem = axis.MajorGrid.LineColor;
            cboXMajorGridDashStyle.SelectedItem = axis.MajorGrid.LineDashStyle;
            nudXMajorGridWidth.Value = axis.MajorGrid.LineWidth;

            chkXMinorGridEnable.Checked = axis.MinorGrid.Enabled;
            ccboXMinorGridColor.SelectedItem = axis.MinorGrid.LineColor;
            cboXMinorGridDashStyle.SelectedItem = axis.MinorGrid.LineDashStyle;
            nudXMinorGridWidth.Value = axis.MinorGrid.LineWidth;

            chkXMajorTickEnable.Checked = axis.MajorTickMark.Enabled;
            ccboXMajorTickColor.SelectedItem = axis.MajorTickMark.LineColor;
            cboXMajorTickDashStyle.SelectedItem = axis.MajorTickMark.LineDashStyle;
            nudXMajorTickWidth.Value = axis.MajorTickMark.LineWidth;
            cboXMajorTickMarkStyle.SelectedValue = axis.MajorTickMark.TickMarkStyle;

            chkXMinorTickEnable.Checked = axis.MinorTickMark.Enabled;
            ccboXMinorTickColor.SelectedItem = axis.MinorTickMark.LineColor;
            cboXMinorTickDashStyle.SelectedItem = axis.MinorTickMark.LineDashStyle;
            nudXMinorTickWidth.Value = axis.MinorTickMark.LineWidth;
            cboXMinorTickMarkStyle.SelectedValue = axis.MinorTickMark.TickMarkStyle;

            txtXAxisTitle.Text = axis.Title;
            cboXAxisTitleAlignment.SelectedValue = axis.TitleAlignment;
            cboXAxisTitleTextOrientation.SelectedValue = axis.TextOrientation;
        }

        private void XAxisCommit()
        {
            Axis axis = ChartArea1.AxisX;
            axis.Enabled = (AxisEnabled)cboXAxisEnableStyle.SelectedValue;

            axis.LabelStyle.Enabled = chkXAxisLabelEnable.Checked;
            axis.LabelStyle.IsEndLabelVisible = chkXAxisEndLabel.Checked;
            axis.IsLabelAutoFit = chkXAxislabelAutoFit.Checked;
            axis.LabelStyle.Angle = (int)nudXAxisLabelAngle.Value;

            axis.ArrowStyle = (AxisArrowStyle)cboXAxisArrowStyle.SelectedValue;
            axis.LineColor = ccboXAxisLineColor.SelectedItem;
            axis.LineDashStyle = (ChartDashStyle)cboXAxisLineDashStyle.SelectedItem;
            axis.LineWidth = (int)nudXAxisLineWidth.Value;

            axis.MajorGrid.Enabled = chkXMajorGridEnable.Checked;
            axis.MajorGrid.LineColor = ccboXMajorGridColor.SelectedItem;
            axis.MajorGrid.LineDashStyle = (ChartDashStyle)cboXMajorGridDashStyle.SelectedItem;
            axis.MajorGrid.LineWidth = (int)nudXMajorGridWidth.Value;

            axis.MinorGrid.Enabled = chkXMinorGridEnable.Checked;
            axis.MinorGrid.LineColor = ccboXMinorGridColor.SelectedItem;
            axis.MinorGrid.LineDashStyle = (ChartDashStyle)cboXMinorGridDashStyle.SelectedItem;
            axis.MinorGrid.LineWidth = (int)nudXMinorGridWidth.Value;

            axis.MajorTickMark.Enabled = chkXMajorTickEnable.Checked;
            axis.MajorTickMark.LineColor = ccboXMajorTickColor.SelectedItem;
            axis.MajorTickMark.LineDashStyle = (ChartDashStyle)cboXMajorTickDashStyle.SelectedItem;
            axis.MajorTickMark.LineWidth = (int)nudXMajorTickWidth.Value;
            axis.MajorTickMark.TickMarkStyle = (TickMarkStyle)cboXMajorTickMarkStyle.SelectedValue;

            axis.MinorTickMark.Enabled = chkXMinorTickEnable.Checked;
            axis.MinorTickMark.LineColor = ccboXMinorTickColor.SelectedItem;
            axis.MinorTickMark.LineDashStyle = (ChartDashStyle)cboXMinorTickDashStyle.SelectedItem;
            axis.MinorTickMark.LineWidth = (int)nudXMinorTickWidth.Value;
            axis.MinorTickMark.TickMarkStyle = (TickMarkStyle)cboXMinorTickMarkStyle.SelectedValue;

            axis.Title = txtXAxisTitle.Text;
            axis.TitleAlignment = (StringAlignment)cboXAxisTitleAlignment.SelectedValue;
            axis.TextOrientation = (TextOrientation)cboXAxisTitleTextOrientation.SelectedValue;

        }

        private void chkXAxislabelAutoFit_CheckedChanged(object sender, EventArgs e)
        {
            pnlXAxisLabel.Enabled = !chkXAxislabelAutoFit.Checked;
        }

        private void btnXAxisLabelFont_Click(object sender, EventArgs e)
        {
            using (FontDialog fd = new FontDialog())
            {
                Axis axis = ChartArea1.AxisX;
                fd.ShowColor = true;
                fd.Font = axis.LabelStyle.Font;
                fd.Color = axis.LabelStyle.ForeColor;
                if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    axis.LabelStyle.Font = fd.Font;
                    axis.LabelStyle.ForeColor = fd.Color;
                }
            }
        }

        private void btnXAxisTitleFont_Click(object sender, EventArgs e)
        {
            using (FontDialog fd = new FontDialog())
            {
                Axis axis = ChartArea1.AxisX;
                fd.ShowColor = true;
                fd.Font = axis.TitleFont;
                fd.Color = axis.TitleForeColor;
                if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    axis.TitleFont = fd.Font;
                    axis.TitleForeColor = fd.Color;
                }
            }
        }
        #endregion

        #region 多轴
        private void btnAddYAxis_Click(object sender, EventArgs e)
        {
            ChartArea yAxisArea = new ChartArea(null);
            yAxisArea.Name = GetNameFromList(yAreaList, "YAxis");
            yAxisArea.BackColor = Color.Transparent;
            yAxisArea.AxisX.MajorGrid.LineColor = Color.Transparent;
            yAxisArea.AxisX.MajorTickMark.LineColor = Color.Transparent;
            yAxisArea.AxisX.LineColor = Color.Transparent;
            yAxisArea.AxisX.LabelStyle.ForeColor = Color.Transparent;
            yAxisArea.AxisY.MajorGrid.LineColor = Color.Transparent;
            yAreaList.Add(yAxisArea);

            ChartArea seriesArea = new ChartArea(null);
            seriesArea.Name = "Series_" + yAxisArea.Name;
            seriesArea.BackColor = Color.Transparent;
            seriesArea.AxisX.MajorGrid.LineColor = Color.Transparent;
            seriesArea.AxisX.MajorTickMark.LineColor = Color.Transparent;
            seriesArea.AxisX.LineColor = Color.Transparent;
            seriesArea.AxisX.LabelStyle.ForeColor = Color.Transparent;
            seriesArea.AxisY.MajorGrid.LineColor = Color.Transparent;
            seriesArea.AxisY.MajorTickMark.LineColor = Color.Transparent;
            seriesArea.AxisY.LineColor = Color.Transparent;
            seriesArea.AxisY.LabelStyle.ForeColor = Color.Transparent;
            yAreaList.Add(seriesArea);

            _yAxisSelectedIndex = string.Empty;
            InitlvwYAxis();
            lvwYAxis.Items[lvwYAxis.Items.Count - 1].Selected = true;
        }

        private void btnDelYAxis_Click(object sender, EventArgs e)
        {
            if (lvwYAxis.Items.Count > 1 && lvwYAxis.SelectedItems.Count != 0)
            {
                yAreaList.RemoveAt(Convert.ToInt32(lvwYAxis.SelectedItems[0].SubItems[0].Text) * 2 + 1);
                yAreaList.RemoveAt(Convert.ToInt32(lvwYAxis.SelectedItems[0].SubItems[0].Text) * 2);
                _yAxisSelectedIndex = string.Empty;
                InitlvwYAxis();
            }
        }

        private void btnYAxisLabelFont_Click(object sender, EventArgs e)
        {
            using (FontDialog fd = new FontDialog())
            {
                fd.ShowColor = true;
                fd.Font = yAreaList[Convert.ToInt32(lvwYAxis.SelectedItems[0].SubItems[0].Text) * 2].AxisY.LabelStyle.Font;
                fd.Color = yAreaList[Convert.ToInt32(lvwYAxis.SelectedItems[0].SubItems[0].Text) * 2].AxisY.LabelStyle.ForeColor;
                if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    yAreaList[Convert.ToInt32(lvwYAxis.SelectedItems[0].SubItems[0].Text) * 2].AxisY.LabelStyle.Font = fd.Font;
                    yAreaList[Convert.ToInt32(lvwYAxis.SelectedItems[0].SubItems[0].Text) * 2].AxisY.LabelStyle.ForeColor = fd.Color;

                    ChartArea1.AxisY.LabelStyle.Font = fd.Font;
                    ChartArea1.AxisY.LabelStyle.ForeColor = fd.Color;

                    chartAreaList[0].AxisY.LabelStyle.Font = fd.Font;
                    chartAreaList[0].AxisY.LabelStyle.ForeColor = fd.Color;
                }
            }
        }

        private string _yAxisSelectedIndex = string.Empty;
        private void lvwYAxis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwYAxis.SelectedItems.Count == 0)
            { gbxYAxis.Visible = false; }
            else
            {
                gbxYAxis.Visible = true;
                if (lvwYAxis.SelectedItems[0].SubItems[0].Text != _yAxisSelectedIndex)
                {
                    if (!string.IsNullOrEmpty(_yAxisSelectedIndex))
                    {
                        Axis axisSave = yAreaList[Convert.ToInt32(_yAxisSelectedIndex) * 2].AxisY;
                        axisSave.Enabled = (AxisEnabled)cboYAxisEnableStyle.SelectedValue;

                        axisSave.Maximum = (rbtnYAxisMaxAuto.Checked == true) ? double.NaN : Convert.ToDouble(txtYAxisMax.Text);
                        axisSave.Minimum = (rbtnYAxisMinAuto.Checked == true) ? double.NaN : Convert.ToDouble(txtYAxisMin.Text);

                        axisSave.Interval = (rbtnYAxisMajorAuto.Checked == true) ? double.NaN : Convert.ToDouble(txtYAxisMajor.Text);
                        axisSave.MajorGrid.Interval = (rbtnYAxisMajorAuto.Checked == true) ? double.NaN : Convert.ToDouble(txtYAxisMajor.Text);
                        axisSave.MajorTickMark.Interval = (rbtnYAxisMajorAuto.Checked == true) ? double.NaN : Convert.ToDouble(txtYAxisMajor.Text);
                        axisSave.IntervalType = DateTimeIntervalType.Auto;
                        axisSave.MajorGrid.IntervalType = DateTimeIntervalType.Auto;
                        axisSave.MajorTickMark.IntervalType = DateTimeIntervalType.Auto;

                        axisSave.MinorGrid.Interval = (rbtnYAxisMinorAuto.Checked == true) ? 0 : Convert.ToDouble(txtYAxisMinor.Text);
                        axisSave.MinorTickMark.Interval = (rbtnYAxisMinorAuto.Checked == true) ? 0 : Convert.ToDouble(txtYAxisMinor.Text);
                        axisSave.MinorGrid.IntervalType = DateTimeIntervalType.Auto;
                        axisSave.MinorTickMark.IntervalType = DateTimeIntervalType.Auto;

                        axisSave.LabelStyle.Enabled = chkYAxisLabelEnable.Checked;
                        axisSave.LabelStyle.IsEndLabelVisible = chkYAxisEndLabel.Checked;
                        axisSave.IsLabelAutoFit = chkYAxislabelAutoFit.Checked;
                        axisSave.LabelStyle.Angle = (int)nudYAxisLabelAngle.Value;

                        axisSave.ArrowStyle = (AxisArrowStyle)cboYAxisArrowStyle.SelectedValue;
                        axisSave.LineColor = ccboYAxisLineColor.SelectedItem;
                        axisSave.LineDashStyle = (ChartDashStyle)cboYAxisLineDashStyle.SelectedItem;
                        axisSave.LineWidth = (int)nudYAxisLineWidth.Value;

                        axisSave.MajorGrid.Enabled = chkYMajorGridEnable.Checked;
                        axisSave.MajorGrid.LineColor = ccboYMajorGridColor.SelectedItem;
                        axisSave.MajorGrid.LineDashStyle = (ChartDashStyle)cboYMajorGridDashStyle.SelectedItem;
                        axisSave.MajorGrid.LineWidth = (int)nudYMajorGridWidth.Value;

                        axisSave.MinorGrid.Enabled = chkYMinorGridEnable.Checked;
                        axisSave.MinorGrid.LineColor = ccboYMinorGridColor.SelectedItem;
                        axisSave.MinorGrid.LineDashStyle = (ChartDashStyle)cboYMinorGridDashStyle.SelectedItem;
                        axisSave.MinorGrid.LineWidth = (int)nudYMinorGridWidth.Value;

                        axisSave.MajorTickMark.Enabled = chkYMajorTickEnable.Checked;
                        axisSave.MajorTickMark.LineColor = ccboYMajorTickColor.SelectedItem;
                        axisSave.MajorTickMark.LineDashStyle = (ChartDashStyle)cboYMajorTickDashStyle.SelectedItem;
                        axisSave.MajorTickMark.LineWidth = (int)nudYMajorTickWidth.Value;
                        axisSave.MajorTickMark.TickMarkStyle = (TickMarkStyle)cboYMajorTickMarkStyle.SelectedValue;

                        axisSave.MinorTickMark.Enabled = chkYMinorTickEnable.Checked;
                        axisSave.MinorTickMark.LineColor = ccboYMinorTickColor.SelectedItem;
                        axisSave.MinorTickMark.LineDashStyle = (ChartDashStyle)cboYMinorTickDashStyle.SelectedItem;
                        axisSave.MinorTickMark.LineWidth = (int)nudYMinorTickWidth.Value;
                        axisSave.MinorTickMark.TickMarkStyle = (TickMarkStyle)cboYMinorTickMarkStyle.SelectedValue;

                        axisSave.Title = txtYAxisTitle.Text;
                        axisSave.TitleAlignment = (StringAlignment)cboYAxisTitleAlignment.SelectedValue;
                        axisSave.TextOrientation = (TextOrientation)cboYAxisTitleTextOrientation.SelectedValue;

                        yAreaList[Convert.ToInt32(_yAxisSelectedIndex) * 2 + 1].AxisY.Tag = txtYAxixDescription.Text;

                        if (_yAxisSelectedIndex == "0")
                        {
                            CopyAxisInfo(chartAreaList[0].AxisY, axisSave);
                        }
                    }
                    Axis axisInit = yAreaList[Convert.ToInt32(lvwYAxis.SelectedItems[0].SubItems[0].Text) * 2].AxisY;
                    if (lvwYAxis.SelectedItems[0].SubItems[0].Text == "0")
                    {
                        pnlYAxisGrid.Enabled = true;

                        CopyAxisInfo(axisInit, chartAreaList[0].AxisY);
                    }
                    else
                    {
                        pnlYAxisGrid.Enabled = false;
                    }
                    txtYAxixName.Text = yAreaList[Convert.ToInt32(lvwYAxis.SelectedItems[0].SubItems[0].Text) * 2].Name;

                    cboYAxisEnableStyle.SelectedValue = axisInit.Enabled;

                    rbtnYAxisMaxAuto.Checked = (double.IsNaN(axisInit.Maximum)) ? true : false;
                    rbtnYAxisMinAuto.Checked = (double.IsNaN(axisInit.Minimum)) ? true : false;
                    rbtnYAxisMajorAuto.Checked = (double.IsNaN(axisInit.MajorGrid.Interval)) ? true : false;
                    rbtnYAxisMinorAuto.Checked = (axisInit.MinorGrid.Interval == 0) ? true : false;

                    txtYAxisMax.Text = (double.IsNaN(axisInit.Maximum)) ? "Auto" : axisInit.Maximum.ToString();
                    txtYAxisMin.Text = (double.IsNaN(axisInit.Minimum)) ? "Auto" : axisInit.Minimum.ToString();
                    txtYAxisMajor.Text = (double.IsNaN(axisInit.MajorGrid.Interval)) ? "Auto" : axisInit.MajorGrid.Interval.ToString();
                    txtYAxisMinor.Text = (axisInit.MinorGrid.Interval == 0) ? "Auto" : axisInit.MinorGrid.Interval.ToString();

                    chkYAxisLabelEnable.Checked = axisInit.LabelStyle.Enabled;
                    chkYAxisEndLabel.Checked = axisInit.LabelStyle.IsEndLabelVisible;
                    chkYAxislabelAutoFit.Checked = axisInit.IsLabelAutoFit;
                    pnlYAxisLabel.Enabled = !axisInit.IsLabelAutoFit;
                    nudYAxisLabelAngle.Value = axisInit.LabelStyle.Angle;

                    cboYAxisArrowStyle.SelectedValue = axisInit.ArrowStyle;
                    ccboYAxisLineColor.SelectedItem = axisInit.LineColor;
                    cboYAxisLineDashStyle.SelectedItem = axisInit.LineDashStyle;
                    nudYAxisLineWidth.Value = axisInit.LineWidth;

                    chkYMajorGridEnable.Checked = axisInit.MajorGrid.Enabled;
                    ccboYMajorGridColor.SelectedItem = axisInit.MajorGrid.LineColor;
                    cboYMajorGridDashStyle.SelectedItem = axisInit.MajorGrid.LineDashStyle;
                    nudYMajorGridWidth.Value = axisInit.MajorGrid.LineWidth;

                    chkYMinorGridEnable.Checked = axisInit.MinorGrid.Enabled;
                    ccboYMinorGridColor.SelectedItem = axisInit.MinorGrid.LineColor;
                    cboYMinorGridDashStyle.SelectedItem = axisInit.MinorGrid.LineDashStyle;
                    nudYMinorGridWidth.Value = axisInit.MinorGrid.LineWidth;

                    chkYMajorTickEnable.Checked = axisInit.MajorTickMark.Enabled;
                    ccboYMajorTickColor.SelectedItem = axisInit.MajorTickMark.LineColor;
                    cboYMajorTickDashStyle.SelectedItem = axisInit.MajorTickMark.LineDashStyle;
                    nudYMajorTickWidth.Value = axisInit.MajorTickMark.LineWidth;
                    cboYMajorTickMarkStyle.SelectedValue = axisInit.MajorTickMark.TickMarkStyle;

                    chkYMinorTickEnable.Checked = axisInit.MinorTickMark.Enabled;
                    ccboYMinorTickColor.SelectedItem = axisInit.MinorTickMark.LineColor;
                    cboYMinorTickDashStyle.SelectedItem = axisInit.MinorTickMark.LineDashStyle;
                    nudYMinorTickWidth.Value = axisInit.MinorTickMark.LineWidth;
                    cboYMinorTickMarkStyle.SelectedValue = axisInit.MinorTickMark.TickMarkStyle;

                    txtYAxisTitle.Text = axisInit.Title;
                    cboYAxisTitleAlignment.SelectedValue = axisInit.TitleAlignment;
                    cboYAxisTitleTextOrientation.SelectedValue = axisInit.TextOrientation;

                    txtYAxixDescription.Text = Convert.ToString(yAreaList[Convert.ToInt32(lvwYAxis.SelectedItems[0].SubItems[0].Text) * 2 + 1].AxisY.Tag);

                    _yAxisSelectedIndex = lvwYAxis.SelectedItems[0].SubItems[0].Text;
                }
            }
        }

        private void chkYAxislabelAutoFit_CheckedChanged(object sender, EventArgs e)
        {
            pnlYAxisLabel.Enabled = !chkYAxislabelAutoFit.Checked;
        }

        private void InitlvwYAxis()
        {
            gbxYAxis.Visible = false;
            lvwYAxis.Items.Clear();
            for (int i = 0, j = 0; i < yAreaList.Count; i++)
            {
                if (yAreaList[i].Name.StartsWith("YAxis"))
                {
                    string[] value = { j.ToString(), yAreaList[i].Name };
                    ListViewItem lvm = new ListViewItem(value);
                    lvwYAxis.Items.Add(lvm);
                    j++;
                }
            }
            cboYAxisListInit();
        }

        private void InitYAxisArea()
        {
            YAxisAreas = ChartParent.YAxisAreas.Clone();
            for (int i = 1; i < chartAreaList.Count; i++)
            {
                ChartArea ca = new ChartArea();
                chartAreaList[i].SetChartArea(ca);
                yAreaList.Add(ca);
            }
            InitlvwYAxis();
            //chkYAxisAreaEnable.Checked = YAxisAreas.Enable;
            YAxisAreas.Offset = 5f;

            if (lvwYAxis.Items.Count == 0)
            {
                ChartArea yAxisArea = new ChartArea(null);
                yAxisArea.Name = GetNameFromList(yAreaList, "YAxis");
                yAxisArea.BackColor = Color.Transparent;
                yAxisArea.AxisX.MajorGrid.LineColor = Color.Transparent;
                yAxisArea.AxisX.MajorTickMark.LineColor = Color.Transparent;
                yAxisArea.AxisX.LineColor = Color.Transparent;
                yAxisArea.AxisX.LabelStyle.ForeColor = Color.Transparent;
                yAxisArea.AxisY.MajorGrid.LineColor = Color.Transparent;
                yAreaList.Add(yAxisArea);

                ChartArea seriesArea = new ChartArea(null);
                seriesArea.Name = "Series_" + yAxisArea.Name;
                seriesArea.BackColor = Color.Transparent;
                seriesArea.AxisX.MajorGrid.LineColor = Color.Transparent;
                seriesArea.AxisX.MajorTickMark.LineColor = Color.Transparent;
                seriesArea.AxisX.LineColor = Color.Transparent;
                seriesArea.AxisX.LabelStyle.ForeColor = Color.Transparent;
                seriesArea.AxisY.MajorGrid.LineColor = Color.Transparent;
                seriesArea.AxisY.MajorTickMark.LineColor = Color.Transparent;
                seriesArea.AxisY.LineColor = Color.Transparent;
                seriesArea.AxisY.LabelStyle.ForeColor = Color.Transparent;
                yAreaList.Add(seriesArea);
                for (int i = 0; i < yAreaList.Count; i++)
                {
                    chartAreaList.Add(new PMSChartArea(yAreaList[i]));
                }
                InitlvwYAxis();
            }
        }

        private void YAxisAreaCommit()
        {
            if (lvwYAxis.SelectedItems.Count != 0)
            {
                Axis axisSave = yAreaList[Convert.ToInt32(lvwYAxis.SelectedItems[0].SubItems[0].Text) * 2].AxisY;
                axisSave.Enabled = (AxisEnabled)cboYAxisEnableStyle.SelectedValue;

                axisSave.Maximum = (rbtnYAxisMaxAuto.Checked == true) ? double.NaN : Convert.ToDouble(txtYAxisMax.Text);
                axisSave.Minimum = (rbtnYAxisMinAuto.Checked == true) ? double.NaN : Convert.ToDouble(txtYAxisMin.Text);

                axisSave.Interval = (rbtnYAxisMajorAuto.Checked == true) ? double.NaN : Convert.ToDouble(txtYAxisMajor.Text);
                axisSave.MajorGrid.Interval = (rbtnYAxisMajorAuto.Checked == true) ? double.NaN : Convert.ToDouble(txtYAxisMajor.Text);
                axisSave.MajorTickMark.Interval = (rbtnYAxisMajorAuto.Checked == true) ? double.NaN : Convert.ToDouble(txtYAxisMajor.Text);
                axisSave.IntervalType = DateTimeIntervalType.Auto;
                axisSave.MajorGrid.IntervalType = DateTimeIntervalType.Auto;
                axisSave.MajorTickMark.IntervalType = DateTimeIntervalType.Auto;

                axisSave.MinorGrid.Interval = (rbtnYAxisMinorAuto.Checked == true) ? 0 : Convert.ToDouble(txtYAxisMinor.Text);
                axisSave.MinorTickMark.Interval = (rbtnYAxisMinorAuto.Checked == true) ? 0 : Convert.ToDouble(txtYAxisMinor.Text);
                axisSave.MinorGrid.IntervalType = DateTimeIntervalType.Auto;
                axisSave.MinorTickMark.IntervalType = DateTimeIntervalType.Auto;

                axisSave.LabelStyle.Enabled = chkYAxisLabelEnable.Checked;
                axisSave.LabelStyle.IsEndLabelVisible = chkYAxisEndLabel.Checked;
                axisSave.IsLabelAutoFit = chkYAxislabelAutoFit.Checked;
                axisSave.LabelStyle.Angle = (int)nudYAxisLabelAngle.Value;

                axisSave.ArrowStyle = (AxisArrowStyle)cboYAxisArrowStyle.SelectedValue;
                axisSave.LineColor = ccboYAxisLineColor.SelectedItem;
                axisSave.LineDashStyle = (ChartDashStyle)cboYAxisLineDashStyle.SelectedItem;
                axisSave.LineWidth = (int)nudYAxisLineWidth.Value;

                axisSave.MajorGrid.Enabled = chkYMajorGridEnable.Checked;
                axisSave.MajorGrid.LineColor = ccboYMajorGridColor.SelectedItem;
                axisSave.MajorGrid.LineDashStyle = (ChartDashStyle)cboYMajorGridDashStyle.SelectedItem;
                axisSave.MajorGrid.LineWidth = (int)nudYMajorGridWidth.Value;

                axisSave.MinorGrid.Enabled = chkYMinorGridEnable.Checked;
                axisSave.MinorGrid.LineColor = ccboYMinorGridColor.SelectedItem;
                axisSave.MinorGrid.LineDashStyle = (ChartDashStyle)cboYMinorGridDashStyle.SelectedItem;
                axisSave.MinorGrid.LineWidth = (int)nudYMinorGridWidth.Value;

                axisSave.MajorTickMark.Enabled = chkYMajorTickEnable.Checked;
                axisSave.MajorTickMark.LineColor = ccboYMajorTickColor.SelectedItem;
                axisSave.MajorTickMark.LineDashStyle = (ChartDashStyle)cboYMajorTickDashStyle.SelectedItem;
                axisSave.MajorTickMark.LineWidth = (int)nudYMajorTickWidth.Value;
                axisSave.MajorTickMark.TickMarkStyle = (TickMarkStyle)cboYMajorTickMarkStyle.SelectedValue;

                axisSave.MinorTickMark.Enabled = chkYMinorTickEnable.Checked;
                axisSave.MinorTickMark.LineColor = ccboYMinorTickColor.SelectedItem;
                axisSave.MinorTickMark.LineDashStyle = (ChartDashStyle)cboYMinorTickDashStyle.SelectedItem;
                axisSave.MinorTickMark.LineWidth = (int)nudYMinorTickWidth.Value;
                axisSave.MinorTickMark.TickMarkStyle = (TickMarkStyle)cboYMinorTickMarkStyle.SelectedValue;

                axisSave.Title = txtYAxisTitle.Text;
                axisSave.TitleAlignment = (StringAlignment)cboYAxisTitleAlignment.SelectedValue;
                axisSave.TextOrientation = (TextOrientation)cboYAxisTitleTextOrientation.SelectedValue;
                yAreaList[Convert.ToInt32(lvwYAxis.SelectedItems[0].SubItems[0].Text) * 2 + 1].AxisY.Tag = txtYAxixDescription.Text;

                if (lvwYAxis.SelectedItems[0].SubItems[0].Text == "0")
                {
                    CopyAxisInfo(chartAreaList[0].AxisY, axisSave);
                }
            }
            //YAxisAreas.Enable = chkYAxisAreaEnable.Checked;
            YAxisAreas.Enable = true;
            YAxisAreas.Offset = 5f;
            for (int i = 0; i < yAreaList.Count; i++)
            {
                chartAreaList.Add(new PMSChartArea(yAreaList[i]));
            }
            ChartParent.YAxisAreas = YAxisAreas.Clone();
        }

        public static void CopyAxisInfo(PMSAxis toAxis, Axis fromAxis)
        {
            toAxis.Enabled = fromAxis.Enabled;

            toAxis.Maximum = fromAxis.Maximum;
            toAxis.Minimum = fromAxis.Minimum;
            toAxis.Interval = fromAxis.Interval;
            toAxis.IntervalType = fromAxis.IntervalType;
            toAxis.MajorGrid.Interval = fromAxis.MajorGrid.Interval;
            toAxis.MajorGrid.IntervalType = fromAxis.MajorGrid.IntervalType;
            toAxis.MinorGrid.Interval = fromAxis.MinorGrid.Interval;
            toAxis.MinorGrid.IntervalType = fromAxis.MinorGrid.IntervalType;
            toAxis.MajorTickMark.Interval = fromAxis.MajorTickMark.Interval;
            toAxis.MajorTickMark.IntervalType = fromAxis.MajorTickMark.IntervalType;
            toAxis.MinorTickMark.Interval = fromAxis.MinorTickMark.Interval;
            toAxis.MinorTickMark.IntervalType = fromAxis.MinorTickMark.IntervalType;

            toAxis.LabelStyle.Enabled = fromAxis.LabelStyle.Enabled;
            toAxis.LabelStyle.IsEndLabelVisible = fromAxis.LabelStyle.IsEndLabelVisible;
            toAxis.IsLabelAutoFit = fromAxis.IsLabelAutoFit;
            toAxis.LabelStyle.Angle = fromAxis.LabelStyle.Angle;

            toAxis.ArrowStyle = fromAxis.ArrowStyle;
            toAxis.LineColor = fromAxis.LineColor;
            toAxis.LineDashStyle = fromAxis.LineDashStyle;
            toAxis.LineWidth = fromAxis.LineWidth;

            toAxis.MajorGrid.Enabled = fromAxis.MajorGrid.Enabled;
            toAxis.MajorGrid.LineColor = fromAxis.MajorGrid.LineColor;
            toAxis.MajorGrid.LineDashStyle = fromAxis.MajorGrid.LineDashStyle;
            toAxis.MajorGrid.LineWidth = fromAxis.MajorGrid.LineWidth;

            toAxis.MinorGrid.Enabled = fromAxis.MinorGrid.Enabled;
            toAxis.MinorGrid.LineColor = fromAxis.MinorGrid.LineColor;
            toAxis.MinorGrid.LineDashStyle = fromAxis.MinorGrid.LineDashStyle;
            toAxis.MinorGrid.LineWidth = fromAxis.MinorGrid.LineWidth;

            toAxis.MajorTickMark.Enabled = fromAxis.MajorTickMark.Enabled;
            toAxis.MajorTickMark.LineColor = fromAxis.MajorTickMark.LineColor;
            toAxis.MajorTickMark.LineDashStyle = fromAxis.MajorTickMark.LineDashStyle;
            toAxis.MajorTickMark.LineWidth = fromAxis.MajorTickMark.LineWidth;
            toAxis.MajorTickMark.TickMarkStyle = fromAxis.MajorTickMark.TickMarkStyle;

            toAxis.MinorTickMark.Enabled = fromAxis.MinorTickMark.Enabled;
            toAxis.MinorTickMark.LineColor = fromAxis.MinorTickMark.LineColor;
            toAxis.MinorTickMark.LineDashStyle = fromAxis.MinorTickMark.LineDashStyle;
            toAxis.MinorTickMark.LineWidth = fromAxis.MinorTickMark.LineWidth;
            toAxis.MinorTickMark.TickMarkStyle = fromAxis.MinorTickMark.TickMarkStyle;

            toAxis.Title = fromAxis.Title;
            toAxis.TitleAlignment = fromAxis.TitleAlignment;
            toAxis.TextOrientation = fromAxis.TextOrientation;
        }

        public static void CopyAxisInfo(Axis toAxis, PMSAxis fromAxis)
        {
            toAxis.Enabled = fromAxis.Enabled;

            toAxis.Maximum = fromAxis.Maximum;
            toAxis.Minimum = fromAxis.Minimum;
            toAxis.Interval = fromAxis.Interval;
            toAxis.IntervalType = fromAxis.IntervalType;
            toAxis.MajorGrid.Interval = fromAxis.MajorGrid.Interval;
            toAxis.MajorGrid.IntervalType = fromAxis.MajorGrid.IntervalType;
            toAxis.MinorGrid.Interval = fromAxis.MinorGrid.Interval;
            toAxis.MinorGrid.IntervalType = fromAxis.MinorGrid.IntervalType;
            toAxis.MajorTickMark.Interval = fromAxis.MajorTickMark.Interval;
            toAxis.MajorTickMark.IntervalType = fromAxis.MajorTickMark.IntervalType;
            toAxis.MinorTickMark.Interval = fromAxis.MinorTickMark.Interval;
            toAxis.MinorTickMark.IntervalType = fromAxis.MinorTickMark.IntervalType;

            toAxis.LabelStyle.Enabled = fromAxis.LabelStyle.Enabled;
            toAxis.LabelStyle.IsEndLabelVisible = fromAxis.LabelStyle.IsEndLabelVisible;
            toAxis.IsLabelAutoFit = fromAxis.IsLabelAutoFit;
            toAxis.LabelStyle.Angle = fromAxis.LabelStyle.Angle;

            toAxis.ArrowStyle = fromAxis.ArrowStyle;
            toAxis.LineColor = fromAxis.LineColor;
            toAxis.LineDashStyle = fromAxis.LineDashStyle;
            toAxis.LineWidth = fromAxis.LineWidth;

            toAxis.MajorGrid.Enabled = fromAxis.MajorGrid.Enabled;
            toAxis.MajorGrid.LineColor = fromAxis.MajorGrid.LineColor;
            toAxis.MajorGrid.LineDashStyle = fromAxis.MajorGrid.LineDashStyle;
            toAxis.MajorGrid.LineWidth = fromAxis.MajorGrid.LineWidth;

            toAxis.MinorGrid.Enabled = fromAxis.MinorGrid.Enabled;
            toAxis.MinorGrid.LineColor = fromAxis.MinorGrid.LineColor;
            toAxis.MinorGrid.LineDashStyle = fromAxis.MinorGrid.LineDashStyle;
            toAxis.MinorGrid.LineWidth = fromAxis.MinorGrid.LineWidth;

            toAxis.MajorTickMark.Enabled = fromAxis.MajorTickMark.Enabled;
            toAxis.MajorTickMark.LineColor = fromAxis.MajorTickMark.LineColor;
            toAxis.MajorTickMark.LineDashStyle = fromAxis.MajorTickMark.LineDashStyle;
            toAxis.MajorTickMark.LineWidth = fromAxis.MajorTickMark.LineWidth;
            toAxis.MajorTickMark.TickMarkStyle = fromAxis.MajorTickMark.TickMarkStyle;

            toAxis.MinorTickMark.Enabled = fromAxis.MinorTickMark.Enabled;
            toAxis.MinorTickMark.LineColor = fromAxis.MinorTickMark.LineColor;
            toAxis.MinorTickMark.LineDashStyle = fromAxis.MinorTickMark.LineDashStyle;
            toAxis.MinorTickMark.LineWidth = fromAxis.MinorTickMark.LineWidth;
            toAxis.MinorTickMark.TickMarkStyle = fromAxis.MinorTickMark.TickMarkStyle;

            toAxis.Title = fromAxis.Title;
            toAxis.TitleAlignment = fromAxis.TitleAlignment;
            toAxis.TextOrientation = fromAxis.TextOrientation;
        }


        //private void chkYAxisAreaEnable_CheckedChanged(object sender, EventArgs e)
        //{
        //    lblYAxisList.Visible = chkYAxisAreaEnable.Checked;
        //    cboYAxisList.Visible = chkYAxisAreaEnable.Checked;
        //}

        private void btnYAxisTitleFont_Click(object sender, EventArgs e)
        {
            using (FontDialog fd = new FontDialog())
            {
                Axis axis = yAreaList[Convert.ToInt32(lvwYAxis.SelectedItems[0].SubItems[0].Text) * 2].AxisY;
                fd.ShowColor = true;
                fd.Font = axis.TitleFont;
                fd.Color = axis.TitleForeColor;
                if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    axis.TitleFont = fd.Font;
                    axis.TitleForeColor = fd.Color;

                    ChartArea1.AxisY.TitleFont = fd.Font;
                    ChartArea1.AxisY.TitleForeColor = fd.Color;

                    chartAreaList[0].AxisY.TitleFont = fd.Font;
                    chartAreaList[0].AxisY.TitleForeColor = fd.Color;
                }
            }
        }

        private void rbtnYAxisMaxAuto_CheckedChanged(object sender, EventArgs e)
        {
            txtYAxisMax.Enabled = !rbtnYAxisMaxAuto.Checked;
            rbtnYAxisMax.Checked = !rbtnYAxisMaxAuto.Checked;
        }

        private void rbtnYAxisMinAuto_CheckedChanged(object sender, EventArgs e)
        {
            txtYAxisMin.Enabled = !rbtnYAxisMinAuto.Checked;
            rbtnYAxisMin.Checked = !rbtnYAxisMinAuto.Checked;
        }

        private void rbtnYAxisMajorAuto_CheckedChanged(object sender, EventArgs e)
        {
            txtYAxisMajor.Enabled = !rbtnYAxisMajorAuto.Checked;
            rbtnYAxisMajor.Checked = !rbtnYAxisMajorAuto.Checked;
        }

        private void rbtnYAxisMinorAuto_CheckedChanged(object sender, EventArgs e)
        {
            txtYAxisMinor.Enabled = !rbtnYAxisMinorAuto.Checked;
            rbtnYAxisMinor.Checked = !rbtnYAxisMinorAuto.Checked;
        }

        #endregion
    }

    public enum BarDrawingStyle
    {
        Default,
        Cylinder,
        Emboss,
        LightToDark,
        Wedge
    }
}
