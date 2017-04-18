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
    public partial class RadarApperenceFrm : Form
    {
        public RadarApperenceFrm()
        {
            InitializeComponent();
            cboTitlePositionInit();
            cboTextOrientationInit();
            cboLabelBorderType.Init();
            cboLegendAlignmentInit();
            cboLegendDockingInit();
            cboLegendStyleInit();
            cboTableStyleInit();
            cboAxisLineDashStyle.Init();
            cboMajorGridDashStyle.Init();
            cboMinorGridDashStyle.Init();
            cboMajorTickDashStyle.Init();
            cboMinorTickDashStyle.Init();
            cboMajorTickMarkStyleInit();
            cboMinorTickMarkStyleInit();
            cboAxisEnableStyleInit();
            cboCircularLabelsStyleInit();
            cboAreaDrawingStyleInit();
        }

        public DataSource dsApply;
        public NewRadarChart ChartParent;
        public PMSChartApp PMSChartAppearance;
        public RadarSeriesColor radarSeriesColor = new RadarSeriesColor();
        public List<RadarClassify> classifyList = new List<RadarClassify>();
        public List<PMSLegend> legendList = new List<PMSLegend>();
        public List<PMSSeries> seriesList = new List<PMSSeries>();
        public List<PMSTitle> titleList = new List<PMSTitle>();
        public List<PMSChartArea> chartAreaList = new List<PMSChartArea>();

        private ChartArea ChartArea1 = new ChartArea("ChartArea1");
        private List<Series> mySeriesList = new List<Series>();
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
            ClassifyCommit();
            AxisCommit();
            ChartArea1.Area3DStyle.Enable3D = chkEnable3D.Checked;
            ChartArea1.Area3DStyle.IsClustered = true;
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


        private void RadarApperenceFrm_Load(object sender, EventArgs e)
        {
            InitChartAreaList();
            InitSeiesList();
            InitLegendList();
            InitTitleList();
            InitClassify();
            propertyTree1.SelectedPaneNode = ppPane7.PaneNode;//默认选择“数据源”页
        }

        private void InitChartAreaList()
        {
            if (chartAreaList.Count != 0)
                chartAreaList[0].SetChartArea(ChartArea1);
            chkEnable3D.Checked = ChartArea1.Area3DStyle.Enable3D;
            InitAxis();
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
            else if (data is List<RadarClassify>)
            {
                List<RadarClassify> areatemp = data as List<RadarClassify>;
                if (areatemp != null)
                {
                    foreach (RadarClassify item in areatemp)
                    {
                        if (item.ClassifyLabel.StartsWith(aim))
                        {
                            int i;
                            if (int.TryParse(item.ClassifyLabel.Substring(aim.Length), out i))
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
        private void cboRadarLabelStyleInit()
        {
            ArrayList al = new ArrayList();
            al.Add(new ComboxItem("Auto", "Auto"));
            al.Add(new ComboxItem("Top", "Top"));
            al.Add(new ComboxItem("Bottom", "Bottom"));
            al.Add(new ComboxItem("Right", "Right"));
            al.Add(new ComboxItem("Left", "Left"));
            al.Add(new ComboxItem("TopLeft", "TopLeft"));
            al.Add(new ComboxItem("TopRight", "TopRight"));
            al.Add(new ComboxItem("BottomLeft", "BottomLeft"));
            al.Add(new ComboxItem("BottomRight", "BottomRight"));
            al.Add(new ComboxItem("Center", "Center"));

            cboRadarLabelStyle.DataSource = al;
            cboRadarLabelStyle.DisplayMember = "Name";
            cboRadarLabelStyle.ValueMember = "Value";
            cboRadarLabelStyle.SelectedIndex = 0;
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
        private void cboRadarDrawingStyleInit()
        {
            ArrayList al = new ArrayList();
            if (ChartParent.ChartType == RadarChartType.Radar)
            {
                al.Add(new ComboxItem("Area", "Area"));
                al.Add(new ComboxItem("Line", "Line"));
                al.Add(new ComboxItem("Marker", "Marker"));
            }
            else
            {
                al.Add(new ComboxItem("Line", "Line"));
                al.Add(new ComboxItem("Marker", "Marker"));
            }

            cboRadarDrawingStyle.DataSource = al;
            cboRadarDrawingStyle.DisplayMember = "Name";
            cboRadarDrawingStyle.ValueMember = "Value";
            cboRadarDrawingStyle.SelectedIndex = 0;
        }

        private void cboAreaDrawingStyleInit()
        {
            ArrayList al = new ArrayList();
            al.Add(new ComboxItem("Circle", "Circle"));
            al.Add(new ComboxItem("Polygon", "Polygon"));

            cboAreaDrawingStyle.DataSource = al;
            cboAreaDrawingStyle.DisplayMember = "Name";
            cboAreaDrawingStyle.ValueMember = "Value";
            cboAreaDrawingStyle.SelectedIndex = 0;
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
            for (int i = 0; i < mySeriesList.Count; i++)
            {
                Series series1 = mySeriesList[i];
                if (ChartParent.ChartType == RadarChartType.Radar)
                    series1.SetCustomProperty("RadarDrawingStyle", cboRadarDrawingStyle.SelectedValue.ToString());
                else
                    series1.SetCustomProperty("PolarDrawingStyle", cboRadarDrawingStyle.SelectedValue.ToString());

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
                series1.SetCustomProperty("LabelStyle", cboRadarLabelStyle.SelectedValue.ToString());
                series1.SetCustomProperty("CircularLabelsStyle", cboCircularLabelsStyle.SelectedValue.ToString());
                series1.SetCustomProperty("AreaDrawingStyle", cboAreaDrawingStyle.SelectedValue.ToString());

                series1.LegendText = txtLegendText.Text;

                seriesList.Add(new PMSSeries(series1));
            }
        }

        private void InitSeiesList()
        {
            cboRadarLabelStyleInit();
            cboRadarDrawingStyleInit();
            ConvertToSeriseList(seriesList);

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
            cboRadarLabelStyle.SelectedValue = (series1.GetCustomProperty("LabelStyle") == null) ? "Auto" : series1.GetCustomProperty("LabelStyle");
            cboCircularLabelsStyle.SelectedValue = (series1.GetCustomProperty("CircularLabelsStyle") == null) ? "Auto" : series1.GetCustomProperty("CircularLabelsStyle");
            cboAreaDrawingStyle.SelectedValue = (series1.GetCustomProperty("AreaDrawingStyle") == null) ? "Circle" : series1.GetCustomProperty("AreaDrawingStyle");

            txtLegendText.Text = series1.LegendText;

            if (ChartParent.ChartType == RadarChartType.Radar)
                cboRadarDrawingStyle.SelectedValue = (series1.GetCustomProperty("RadarDrawingStyle") == null) ? "Area" : series1.GetCustomProperty("RadarDrawingStyle");
            else
                cboRadarDrawingStyle.SelectedValue = (series1.GetCustomProperty("PolarDrawingStyle") == null) ? "Line" : series1.GetCustomProperty("PolarDrawingStyle");
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
                pt.Name = GetNameFromList(mySeriesList, "系列");
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
        #endregion

        #region 坐标轴
        private void cboMajorTickMarkStyleInit()
        {
            ArrayList al = new ArrayList();
            al.Add(new ComboxItem("不显示", TickMarkStyle.None));
            al.Add(new ComboxItem("内部", TickMarkStyle.InsideArea));
            al.Add(new ComboxItem("外部", TickMarkStyle.OutsideArea));
            al.Add(new ComboxItem("交错", TickMarkStyle.AcrossAxis));

            cboMajorTickMarkStyle.DataSource = al;
            cboMajorTickMarkStyle.DisplayMember = "Name";
            cboMajorTickMarkStyle.ValueMember = "Value";
            cboMajorTickMarkStyle.SelectedIndex = 0;
        }
        private void cboMinorTickMarkStyleInit()
        {
            ArrayList al = new ArrayList();
            al.Add(new ComboxItem("不显示", TickMarkStyle.None));
            al.Add(new ComboxItem("内部", TickMarkStyle.InsideArea));
            al.Add(new ComboxItem("外部", TickMarkStyle.OutsideArea));
            al.Add(new ComboxItem("交错", TickMarkStyle.AcrossAxis));

            cboMinorTickMarkStyle.DataSource = al;
            cboMinorTickMarkStyle.DisplayMember = "Name";
            cboMinorTickMarkStyle.ValueMember = "Value";
            cboMinorTickMarkStyle.SelectedIndex = 0;
        }
        private void cboAxisEnableStyleInit()
        {
            ArrayList al = new ArrayList();
            al.Add(new ComboxItem("自动", AxisEnabled.Auto));
            al.Add(new ComboxItem("显示", AxisEnabled.True));
            al.Add(new ComboxItem("不显示", AxisEnabled.False));

            cboAxisEnableStyle.DataSource = al;
            cboAxisEnableStyle.DisplayMember = "Name";
            cboAxisEnableStyle.ValueMember = "Value";
            cboAxisEnableStyle.SelectedIndex = 0;
        }
        private void cboCircularLabelsStyleInit()
        {
            ArrayList al = new ArrayList();
            al.Add(new ComboxItem("自动", "Auto"));
            al.Add(new ComboxItem("环绕", "Circular"));
            al.Add(new ComboxItem("水平", "Horizontal"));
            al.Add(new ComboxItem("发散", "Radial"));

            cboCircularLabelsStyle.DataSource = al;
            cboCircularLabelsStyle.DisplayMember = "Name";
            cboCircularLabelsStyle.ValueMember = "Value";
            cboCircularLabelsStyle.SelectedIndex = 0;
        }
        private void InitAxis()
        {
            InitAxisCommon(ChartArea1.AxisY);
            chkXAxisLabelEnable.Checked = ChartArea1.AxisX.LabelStyle.Enabled;
        }

        private void AxisCommit()
        {
            AxisCommonCommit(ChartArea1.AxisY);
            ChartArea1.AxisX.LabelStyle.Enabled = chkXAxisLabelEnable.Checked;
        }

        private void InitAxisCommon(Axis axis)
        {
            cboAxisEnableStyle.SelectedValue = axis.Enabled;
            chkAxisLabelEnable.Checked = axis.LabelStyle.Enabled;
            chkAxisEndLabel.Checked = axis.LabelStyle.IsEndLabelVisible;
            chkAxislabelAutoFit.Checked = axis.IsLabelAutoFit;
            pnlAxisLabel.Enabled = !axis.IsLabelAutoFit;
            nudAxisLabelAngle.Value = axis.LabelStyle.Angle;
            ccboAxisLineColor.SelectedItem = axis.LineColor;
            cboAxisLineDashStyle.SelectedItem = axis.LineDashStyle;
            nudAxisLineWidth.Value = axis.LineWidth;

            chkMajorGridEnable.Checked = axis.MajorGrid.Enabled;
            ccboMajorGridColor.SelectedItem = axis.MajorGrid.LineColor;
            cboMajorGridDashStyle.SelectedItem = axis.MajorGrid.LineDashStyle;
            nudMajorGridWidth.Value = axis.MajorGrid.LineWidth;

            chkMinorGridEnable.Checked = axis.MinorGrid.Enabled;
            ccboMinorGridColor.SelectedItem = axis.MinorGrid.LineColor;
            cboMinorGridDashStyle.SelectedItem = axis.MinorGrid.LineDashStyle;
            nudMinorGridWidth.Value = axis.MinorGrid.LineWidth;

            chkMajorTickEnable.Checked = axis.MajorTickMark.Enabled;
            ccboMajorTickColor.SelectedItem = axis.MajorTickMark.LineColor;
            cboMajorTickDashStyle.SelectedItem = axis.MajorTickMark.LineDashStyle;
            nudMajorTickWidth.Value = axis.MajorTickMark.LineWidth;
            cboMajorTickMarkStyle.SelectedValue = axis.MajorTickMark.TickMarkStyle;

            chkMinorTickEnable.Checked = axis.MinorTickMark.Enabled;
            ccboMinorTickColor.SelectedItem = axis.MinorTickMark.LineColor;
            cboMinorTickDashStyle.SelectedItem = axis.MinorTickMark.LineDashStyle;
            nudMinorTickWidth.Value = axis.MinorTickMark.LineWidth;
            cboMinorTickMarkStyle.SelectedValue = axis.MinorTickMark.TickMarkStyle;
        }

        private void AxisCommonCommit(Axis axis)
        {
            axis.Enabled = (AxisEnabled)cboAxisEnableStyle.SelectedValue;
            axis.LabelStyle.Enabled = chkAxisLabelEnable.Checked;
            axis.LabelStyle.IsEndLabelVisible = chkAxisEndLabel.Checked;
            axis.IsLabelAutoFit = chkAxislabelAutoFit.Checked;
            axis.LabelStyle.Angle = (int)nudAxisLabelAngle.Value;
            axis.LineColor = ccboAxisLineColor.SelectedItem;
            axis.LineDashStyle = (ChartDashStyle)cboAxisLineDashStyle.SelectedItem;
            axis.LineWidth = (int)nudAxisLineWidth.Value;

            axis.MajorGrid.Enabled = chkMajorGridEnable.Checked;
            axis.MajorGrid.LineColor = ccboMajorGridColor.SelectedItem;
            axis.MajorGrid.LineDashStyle = (ChartDashStyle)cboMajorGridDashStyle.SelectedItem;
            axis.MajorGrid.LineWidth = (int)nudMajorGridWidth.Value;

            axis.MinorGrid.Enabled = chkMinorGridEnable.Checked;
            axis.MinorGrid.LineColor = ccboMinorGridColor.SelectedItem;
            axis.MinorGrid.LineDashStyle = (ChartDashStyle)cboMinorGridDashStyle.SelectedItem;
            axis.MinorGrid.LineWidth = (int)nudMinorGridWidth.Value;

            axis.MajorTickMark.Enabled = chkMajorTickEnable.Checked;
            axis.MajorTickMark.LineColor = ccboMajorTickColor.SelectedItem;
            axis.MajorTickMark.LineDashStyle = (ChartDashStyle)cboMajorTickDashStyle.SelectedItem;
            axis.MajorTickMark.LineWidth = (int)nudMajorTickWidth.Value;
            axis.MajorTickMark.TickMarkStyle = (TickMarkStyle)cboMajorTickMarkStyle.SelectedValue;

            axis.MinorTickMark.Enabled = chkMinorTickEnable.Checked;
            axis.MinorTickMark.LineColor = ccboMinorTickColor.SelectedItem;
            axis.MinorTickMark.LineDashStyle = (ChartDashStyle)cboMinorTickDashStyle.SelectedItem;
            axis.MinorTickMark.LineWidth = (int)nudMinorTickWidth.Value;
            axis.MinorTickMark.TickMarkStyle = (TickMarkStyle)cboMinorTickMarkStyle.SelectedValue;
        }

        private void chkAxislabelAutoFit_CheckedChanged(object sender, EventArgs e)
        {
            pnlAxisLabel.Enabled = !chkAxislabelAutoFit.Checked;
        }

        private void btnAxisLabelFont_Click(object sender, EventArgs e)
        {
            using (FontDialog fd = new FontDialog())
            {
                Axis axis = ChartArea1.AxisY;
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
        #endregion

        #region 分类数据源
        private void btnClassifyValue_Click(object sender, EventArgs e)
        {
            using (MES.Report.MESReportExpressionEditor expEditor = new MES.Report.MESReportExpressionEditor(txtClassifyValue.Text))
            {
                expEditor.ControlName = Name;
                if (expEditor.ShowDialog() == DialogResult.OK)
                {
                    txtClassifyValue.Text = expEditor.ExpressionText;
                }
            }
        }

        private void btnClassifyMaxValue_Click(object sender, EventArgs e)
        {
            using (MES.Report.MESReportExpressionEditor expEditor = new MES.Report.MESReportExpressionEditor(txtClassifyMaxValue.Text))
            {
                expEditor.ControlName = Name;
                if (expEditor.ShowDialog() == DialogResult.OK)
                {
                    txtClassifyMaxValue.Text = expEditor.ExpressionText;
                }
            }
        }

        private void btnClassifyMinValue_Click(object sender, EventArgs e)
        {
            using (MES.Report.MESReportExpressionEditor expEditor = new MES.Report.MESReportExpressionEditor(txtClassifyMinValue.Text))
            {
                expEditor.ControlName = Name;
                if (expEditor.ShowDialog() == DialogResult.OK)
                {
                    txtClassifyMinValue.Text = expEditor.ExpressionText;
                }
            }
        }

        private string _classifySelectedIndex = string.Empty;
        private void lvwClassify_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwClassify.SelectedItems.Count == 0)
            { gbxClassify.Visible = false; }
            else
            {
                gbxClassify.Visible = true;
                if (lvwClassify.SelectedItems[0].SubItems[0].Text != _classifySelectedIndex)
                {
                    if (!string.IsNullOrEmpty(_classifySelectedIndex))
                    {
                        classifyList[Convert.ToInt32(_classifySelectedIndex)].Enable = chkClassifyEnable.Checked;
                        classifyList[Convert.ToInt32(_classifySelectedIndex)].ClassifyLabel = txtClassifyLabel.Text;
                        classifyList[Convert.ToInt32(_classifySelectedIndex)].ClassifyValue = txtClassifyValue.Text;
                        classifyList[Convert.ToInt32(_classifySelectedIndex)].ClassifyMaxValue = txtClassifyMaxValue.Text;
                        classifyList[Convert.ToInt32(_classifySelectedIndex)].ClassifyMinValue = txtClassifyMinValue.Text;

                        lvwClassify.Items[Convert.ToInt32(_classifySelectedIndex)].SubItems[1].Text = txtClassifyLabel.Text;
                    }
                    RadarClassify classify = classifyList[Convert.ToInt32(lvwClassify.SelectedItems[0].SubItems[0].Text)];
                    chkClassifyEnable.Checked = classify.Enable;
                    txtClassifyLabel.Text = classify.ClassifyLabel;
                    txtClassifyValue.Text = classify.ClassifyValue;
                    txtClassifyMaxValue.Text = classify.ClassifyMaxValue;
                    txtClassifyMinValue.Text = classify.ClassifyMinValue;

                    _classifySelectedIndex = lvwClassify.SelectedItems[0].SubItems[0].Text;
                }
            }
        }

        private void btnClassifyAdd_Click(object sender, EventArgs e)
        {
            RadarClassify pt = new RadarClassify();
            pt.ClassifyLabel = GetNameFromList(classifyList, "分类");
            classifyList.Add(pt);
            _classifySelectedIndex = string.Empty;
            InitlvwClassify();
            lvwClassify.Items[lvwClassify.Items.Count - 1].Selected = true;
        }

        private void btnClassifyDel_Click(object sender, EventArgs e)
        {
            if (/*lvwClassify.Items.Count > 1 &&*/ lvwClassify.SelectedItems.Count != 0)
            {
                classifyList.RemoveAt(Convert.ToInt32(lvwClassify.SelectedItems[0].SubItems[0].Text));
                _classifySelectedIndex = string.Empty;
                InitlvwClassify();
            }
        }

        private void btnClassifyUp_Click(object sender, EventArgs e)
        {
            if (lvwClassify.SelectedItems.Count == 0 || lvwClassify.SelectedItems[0].SubItems[0].Text == "0")
                return;
            RadarClassify temp = classifyList[Convert.ToInt32(lvwClassify.SelectedItems[0].SubItems[0].Text)];
            classifyList[Convert.ToInt32(lvwClassify.SelectedItems[0].SubItems[0].Text)] = classifyList[Convert.ToInt32(lvwClassify.SelectedItems[0].SubItems[0].Text) - 1];
            classifyList[Convert.ToInt32(lvwClassify.SelectedItems[0].SubItems[0].Text) - 1] = temp;
            _classifySelectedIndex = string.Empty;
            InitlvwClassify();
        }

        private void btnClassifyDown_Click(object sender, EventArgs e)
        {
            if (lvwClassify.SelectedItems.Count == 0 || lvwClassify.SelectedItems[0].SubItems[0].Text == (lvwClassify.Items.Count - 1).ToString())
                return;
            RadarClassify temp = classifyList[Convert.ToInt32(lvwClassify.SelectedItems[0].SubItems[0].Text)];
            classifyList[Convert.ToInt32(lvwClassify.SelectedItems[0].SubItems[0].Text)] = classifyList[Convert.ToInt32(lvwClassify.SelectedItems[0].SubItems[0].Text) + 1];
            classifyList[Convert.ToInt32(lvwClassify.SelectedItems[0].SubItems[0].Text) + 1] = temp;
            _classifySelectedIndex = string.Empty;
            InitlvwClassify();
        }

        private void InitlvwClassify()
        {
            gbxClassify.Visible = false;
            lvwClassify.Items.Clear();
            for (int i = 0; i < classifyList.Count; i++)
            {
                string[] value = { i.ToString(), classifyList[i].ClassifyLabel };
                ListViewItem lvm = new ListViewItem(value);
                lvwClassify.Items.Add(lvm);
            }
        }

        private void InitClassify()
        {
            if (ChartParent.RadarSeriesColor == null)
            {
                txtClassifyMainName.Text = "实际值";
                txtClassifyMaxName.Text = "上限";
                txtClassifyMinName.Text = "下限";
                ccboClassifyMainColor.SelectedItem = Color.Green;
                ccboClassifyMaxColor.SelectedItem = Color.Red;
                ccboClassifyMinColor.SelectedItem = Color.Yellow;
            }
            else
            {
                radarSeriesColor = ChartParent.RadarSeriesColor.Clone();
                txtClassifyMainName.Text = radarSeriesColor.ClassifyMainName;
                txtClassifyMaxName.Text = radarSeriesColor.ClassifyMaxName;
                txtClassifyMinName.Text = radarSeriesColor.ClassifyMinName;
                ccboClassifyMainColor.SelectedItem = radarSeriesColor.ClassifyMainColor;
                ccboClassifyMaxColor.SelectedItem = radarSeriesColor.ClassifyMaxColor;
                ccboClassifyMinColor.SelectedItem = radarSeriesColor.ClassifyMinColor;
            }

            if (ChartParent.RadarClassifyList.Count == 0)
                return;
            classifyList.Clear();
            for (int i = 0; i < ChartParent.RadarClassifyList.Count; i++)
            {
                classifyList.Add(ChartParent.RadarClassifyList[i].Clone());
            }
            InitlvwClassify();
        }

        private void ClassifyCommit()
        {
            radarSeriesColor.ClassifyMainName = txtClassifyMainName.Text;
            radarSeriesColor.ClassifyMaxName = txtClassifyMaxName.Text;
            radarSeriesColor.ClassifyMinName = txtClassifyMinName.Text;
            radarSeriesColor.ClassifyMainColor = ccboClassifyMainColor.SelectedItem;
            radarSeriesColor.ClassifyMaxColor = ccboClassifyMaxColor.SelectedItem;
            radarSeriesColor.ClassifyMinColor = ccboClassifyMinColor.SelectedItem;
            ChartParent.RadarSeriesColor = radarSeriesColor.Clone();

            if (lvwClassify.SelectedItems.Count != 0)
            {
                classifyList[Convert.ToInt32(lvwClassify.SelectedItems[0].SubItems[0].Text)].Enable = chkClassifyEnable.Checked;
                classifyList[Convert.ToInt32(lvwClassify.SelectedItems[0].SubItems[0].Text)].ClassifyLabel = txtClassifyLabel.Text;
                classifyList[Convert.ToInt32(lvwClassify.SelectedItems[0].SubItems[0].Text)].ClassifyValue = txtClassifyValue.Text;
                classifyList[Convert.ToInt32(lvwClassify.SelectedItems[0].SubItems[0].Text)].ClassifyMaxValue = txtClassifyMaxValue.Text;
                classifyList[Convert.ToInt32(lvwClassify.SelectedItems[0].SubItems[0].Text)].ClassifyMinValue = txtClassifyMinValue.Text;

                lvwClassify.Items[Convert.ToInt32(lvwClassify.SelectedItems[0].SubItems[0].Text)].SubItems[1].Text = txtClassifyLabel.Text;
            }
            ChartParent.RadarClassifyList.Clear();
            for (int i = 0; i < classifyList.Count; i++)
            {
                ChartParent.RadarClassifyList.Add(classifyList[i].Clone());
            }
        }
        #endregion
    }
}
