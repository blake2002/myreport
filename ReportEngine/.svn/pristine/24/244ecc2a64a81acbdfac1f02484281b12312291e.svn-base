using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;

namespace PMS.Libraries.ToolControls.PMSChart
{
    public partial class SectionApperence : Form
    {
        public SectionApperence()
        {
            InitializeComponent();
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

        string sectionDec = "分段曲线";
        string title = "标题";
        string xaxis = "X轴";
        string yaxis = "Y轴";
        string Section = "分段";
        string curve = "曲线";
        string apperence = "外观";
        string alert = "警戒线";

        private void SectionApperence_Load(object sender, EventArgs e)
        {
            if (seriesList.Count != 0)
            {
                seriesMain = getSectionInfo(seriesList[0], scX, scY, section);
            }
            else
            {
                seriesMain.Color = Color.Blue;
            }
            //else
            //{
            //    seriesMain = setSectionSeries(seriesList[0]);
            //    //seriesUpper = setSectionSeries(seriesList[1]);
            //    //seriesLower = setSectionSeries(seriesList[2]);
            //    seriesXaxis = setSectionSeries(seriesList[3]);
            //}
            if (titleList.Count == 0 && !isIntial)
            {
                PMSTitle title = new PMSTitle(null);
                title.Text = "分段曲线";
                title.Name = "标题1";
                titleList.Add(title);
            }
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
            setAno(section.StartAnnotation, startAn);
            setAno(section.EndAnnotation, endAn);
            //setUpperAndLower(scp.UpperLimit, seriesUpper);
            //setUpperAndLower(scp.LowerLimit, seriesLower);

            scX.SourceField = ChartParent.SourceField;
            section.SourceField = ChartParent.SourceField;
            //treeView1.Nodes.Add("分段曲线", "分段曲线");
            //if (treeView1.Nodes["分段曲线"] != null)
            //{
            //    treeView1.Nodes["分段曲线"].Nodes.Add("外观配置", "外观配置");
            //    treeView1.Nodes["分段曲线"].Nodes.Add("分段属性配置", "分段属性配置");
            //    treeView1.Nodes["分段曲线"].Nodes.Add("标题配置", "标题配置");
            //    if (titleList.Count != 0)
            //    {
            //        for (int i = 0; i < titleList.Count; i++)
            //        {
            //            treeView1.Nodes["分段曲线"].Nodes["标题配置"].Nodes.Add(titleList[i].Name, titleList[i].Name);
            //            treeView1.Nodes["分段曲线"].Nodes["标题配置"].Nodes[titleList[i].Name].Tag = titleList[i];
            //        }

            //    }
            //    treeView1.Nodes["分段曲线"].Nodes["外观配置"].Tag = seriesMain;
            //    treeView1.Nodes["分段曲线"].Nodes["分段属性配置"].Tag = scp;
            //    treeView1.ExpandAll();
            //}
            intTree();
            if (!(seriesList.Count - 1 > 0) && !isIntial)
            {
                AddLimitToolStripMenuItem_Click(null, null);
            }
            treeView1.ExpandAll();
        }

        private void Sure_Click(object sender, EventArgs e)
        {
            loadAno(section.StartAnnotation, startAn);
            loadAno(section.EndAnnotation, endAn);
            //loadUpperAndLower(scp.UpperLimit, seriesUpper);
            //loadUpperAndLower(scp.LowerLimit, seriesLower);
            seriesList.Clear();
            seriesList.Add(setSectionSeries(seriesMain,scX,scY,section));
            for (int i = 0; i < limitList.Count; i++) 
            {
                seriesList.Add(getSeriesFromLimit(limitList[i]));
            }
                //seriesList.Add(seriesUpper);
                //seriesList.Add(seriesLower);
            dsApply = new DataSource(null);
            dsApply.SeriesList = this.seriesList;
            dsApply.TitleList = this.titleList;
            dsApply.annotationList = this.Annotations;
            ChartParent.Apperence = dsApply.Clone();
            this.DialogResult = DialogResult.OK;
            this.Dispose();

            ChartParent.InitailColumnData();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
            //ChartParent.InitailColumnData();
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            isApply = true;
            loadAno(section.StartAnnotation, startAn);
            loadAno(section.EndAnnotation, endAn);
            //loadUpperAndLower(scp.UpperLimit, seriesUpper);
            //loadUpperAndLower(scp.LowerLimit, seriesLower);
            seriesList.Clear();
            seriesList.Add(setSectionSeries(seriesMain, scX, scY, section));
            for (int i = 0; i < limitList.Count; i++)
            {
                seriesList.Add(getSeriesFromLimit(limitList[i]));
            }
            //seriesList.Add(seriesUpper);
            //seriesList.Add(seriesLower);
            dsApply = new DataSource(null);
            dsApply.SeriesList = this.seriesList;
            dsApply.TitleList = this.titleList;
            dsApply.annotationList = this.Annotations;
            ChartParent.Apperence = dsApply.Clone();


            ChartParent.InitailColumnData();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeView1.SelectedNode = e.Node;
                if (e.Node != null)
                {
                    if (e.Node.Text == title) 
                    {
                        treeView1.ContextMenuStrip = titleAdd;
                    }
                    else if (e.Node.Text == alert)
                    {
                        treeView1.ContextMenuStrip = limitAdd;
                    }
                    else if (e.Node.Tag is PMSTitle)
                    {
                        treeView1.ContextMenuStrip = titleDelete;
                    }
                    else if (e.Node.Tag is sectionLimit)
                    {
                        treeView1.ContextMenuStrip = limitDelete;
                    }
                    else 
                    {
                        treeView1.ContextMenuStrip = null;
                    }
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                propertyGrid1.SelectedObject = e.Node.Tag;
                treeView1.ContextMenuStrip = null;
            }
        }

        private void addTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string aim = "标题";
            treeView1.ContextMenuStrip = null;
            treeView1.ExpandAll();
            propertyGrid1.SelectedObject = null;
            PMSTitle pt = new PMSTitle(null);
            pt.Name = GetNameFromList(titleList, "标题");
            pt.Text = pt.Name;
            if (treeView1.Nodes[sectionDec].Nodes[title].Nodes.Count != 0)
            {
                for (int i = 0; i < treeView1.Nodes[sectionDec].Nodes[title].Nodes.Count; i++)
                {
                    int j;
                    if (treeView1.Nodes[sectionDec].Nodes[title].Nodes[i].Text.Length > aim.Length)
                    {
                        if (int.TryParse(treeView1.Nodes[sectionDec].Nodes[title].Nodes[i].Text.Substring(aim.Length), out j))
                            if (int.Parse(pt.Name.Substring(aim.Length)) < int.Parse(treeView1.Nodes[sectionDec].Nodes[title].Nodes[i].Text.Substring(aim.Length)))
                            {
                                treeView1.Nodes[sectionDec].Nodes[title].Nodes.Insert(i, pt.Name, pt.Name);
                                titleList.Insert(i, pt);
                                break;
                            }
                    }
                    if (i == treeView1.Nodes[sectionDec].Nodes[title].Nodes.Count - 1)
                        {
                            treeView1.Nodes[sectionDec].Nodes[title].Nodes.Add(pt.Name, pt.Name);
                            titleList.Add(pt);
                            break;
                        }
                                      
                }
            }
            else 
            {
                treeView1.Nodes[sectionDec].Nodes[title].Nodes.Add(pt.Name, pt.Name);
                titleList.Add(pt);
            }
            treeView1.Nodes[sectionDec].Nodes[title].Nodes[pt.Name].Tag = pt;
        }

        private void deleteTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.ContextMenuStrip = null;
            if (treeView1.SelectedNode != null)
            {

                foreach (PMSTitle item in titleList)
                {
                    if (treeView1.SelectedNode.Text == item.Name)
                    {
                        treeView1.Nodes.Remove(treeView1.SelectedNode);
                        titleList.Remove(item);
                        break;
                    }
                }
                treeView1.SelectedNode = null;
                propertyGrid1.SelectedObject = null;
            }
        }

        private void AddLimitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string aim = "警戒线";
            treeView1.ContextMenuStrip = null;
            treeView1.ExpandAll();
            propertyGrid1.SelectedObject = null;
            sectionLimit pt = new sectionLimit();
            pt.Name = GetNameFromList(limitList, aim);
            if (treeView1.Nodes[sectionDec].Nodes[curve].Nodes[alert].Nodes.Count != 0)
            {
                for (int i = 0; i < treeView1.Nodes[sectionDec].Nodes[curve].Nodes[alert].Nodes.Count; i++)
                {
                    int j;
                    if (treeView1.Nodes[sectionDec].Nodes[curve].Nodes[alert].Nodes[i].Text.Length > aim.Length)
                    {
                        if (int.TryParse(treeView1.Nodes[sectionDec].Nodes[curve].Nodes[alert].Nodes[i].Text.Substring(aim.Length), out j))
                            if (int.Parse(pt.Name.Substring(aim.Length)) < int.Parse(treeView1.Nodes[sectionDec].Nodes[curve].Nodes[alert].Nodes[i].Text.Substring(aim.Length)))
                            {
                                treeView1.Nodes[sectionDec].Nodes[curve].Nodes[alert].Nodes.Insert(i, pt.Name, pt.Name);
                                limitList.Insert(i, pt);
                                break;
                            }
                    }
                    if (i == treeView1.Nodes[sectionDec].Nodes[curve].Nodes[alert].Nodes.Count - 1)
                    {
                        treeView1.Nodes[sectionDec].Nodes[curve].Nodes[alert].Nodes.Add(pt.Name, pt.Name);
                        limitList.Add(pt);
                        break;
                    }

                }
            }
            else
            {
                treeView1.Nodes[sectionDec].Nodes[curve].Nodes[alert].Nodes.Add(pt.Name, pt.Name);
                limitList.Add(pt);
            }
            pt.Color = Color.Red;
            pt.Enable = true;
            pt.Style = ChartDashStyle.Solid;
            pt.Width = 1;


            treeView1.Nodes[sectionDec].Nodes[curve].Nodes[alert].Nodes[pt.Name].Tag = pt;
        }

        private void DeleteLimitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.ContextMenuStrip = null;
            if (treeView1.SelectedNode != null)
            {

                foreach (sectionLimit item in limitList)
                {
                    if (treeView1.SelectedNode.Text == item.Name)
                    {
                        treeView1.Nodes.Remove(treeView1.SelectedNode);
                        limitList.Remove(item);
                        break;
                    }
                }
                treeView1.SelectedNode = null;
                propertyGrid1.SelectedObject = null;
            }
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {

            int count = 0;
            if (treeView1.SelectedNode != null)
            {
                if (treeView1.SelectedNode.Tag is sectionLimit)
                {
                    foreach (sectionLimit item in limitList)
                    {
                        if (item.Name == (treeView1.SelectedNode.Tag as sectionLimit).Name)
                        {
                            count++;
                        }
                    }
                    if (count > 1)
                    {
                        (treeView1.SelectedNode.Tag as sectionLimit).Name = (treeView1.SelectedNode.Tag as sectionLimit).Name + "重命名";
                    }
                    treeView1.SelectedNode.Text = (treeView1.SelectedNode.Tag as sectionLimit).Name;
                }
                else if (treeView1.SelectedNode.Tag is PMSTitle)
                {
                    foreach (PMSTitle item in titleList)
                    {
                        if (item.Name == (treeView1.SelectedNode.Tag as PMSTitle).Name)
                        {
                            count++;
                        }
                    }
                    if (count > 1)
                    {
                        (treeView1.SelectedNode.Tag as PMSTitle).Name = (treeView1.SelectedNode.Tag as PMSTitle).Name + "重命名";
                    }
                    treeView1.SelectedNode.Text = (treeView1.SelectedNode.Tag as PMSTitle).Name;
                }
                treeView1.Nodes.Clear();
                intTreeLimit();
            }
        }

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

        private  void loadAno(startAnnotation sa,PMSAnnotation pa)
        {
            pa.Color = sa.Color;
            pa.Width = sa.Width;
            pa.LineDashStyle = sa.LineDashStyle;
            pa.StartStyle = sa.StartStyle;
            pa.EndStyle = sa.EndStyle;
            pa.enable = sa.Enable;
        }

        private void loadAno(endAnnotation ea, PMSAnnotation pa)
        {
            pa.Color = ea.Color;
            pa.Width = ea.Width;
            pa.LineDashStyle = ea.LineDashStyle;
            pa.StartStyle = ea.StartStyle;
            pa.EndStyle = ea.EndStyle;
            pa.enable = ea.Enable;
        }

        private void loadUpperAndLower(lowerLimit llt, SectionSeries ps) 
        {
            ps.Color = llt.Color;
            ps.BorderWidth = llt.Width;
            ps.BorderDashStyle = llt.Style;
            ps.Limit = llt.Limit;
            ps.Enabled = llt.Enable;
        }

        private void loadUpperAndLower(upperLimit ult, SectionSeries ps)
        {
            ps.Color = ult.Color;
            ps.BorderWidth = ult.Width;
            ps.BorderDashStyle = ult.Style;
            ps.Limit = ult.Limit;
            ps.Enabled = ult.Enable;
        }


        private void setUpperAndLower(lowerLimit llt, SectionSeries ps)
        {
            llt.Color = ps.Color;
            llt.Width = ps.BorderWidth;
            llt.Style = ps.BorderDashStyle;
            llt.Limit = ps.Limit;
            llt.Enable = ps.Enabled;
        }

        private void setUpperAndLower(upperLimit ult, SectionSeries ps)
        {
            ult.Color = ps.Color;
            ult.Width = ps.BorderWidth;
            ult.Style = ps.BorderDashStyle;
            ult.Limit = ps.Limit;
            ult.Enable = ps.Enabled;
        }

        SectionSeries setSectionSeries(PMSSeries ps)
        {
            SectionSeries ss = new SectionSeries(ps.ToSeries());
            ss.SectionChartType = (ps as SectionSeries).SectionChartType;
            ss.Distance = (ps as SectionSeries).Distance;
            ss.SortWay = (ps as SectionSeries).SortWay;
            ss.TimeType = (ps as SectionSeries).TimeType;
            ss.SectionField = (ps as SectionSeries).SectionField;
            ss.SourceField = (ps as SectionSeries).SourceField;
            ss.BindingField = (ps as SectionSeries).BindingField;
            ss.Limit = (ps as SectionSeries).Limit;
            return ss;
        }

        private void setAno(startAnnotation sa, PMSAnnotation pa)
        {
            sa.Color = pa.Color;
            sa.Width = pa.Width;
            sa.LineDashStyle = pa.LineDashStyle;
            sa.StartStyle = pa.StartStyle;
            sa.EndStyle = pa.EndStyle;
            sa.Enable = pa.enable;
        }

        private void setAno(endAnnotation ea, PMSAnnotation pa)
        {
            ea.Color = pa.Color;
            ea.Width = pa.Width;
            ea.LineDashStyle = pa.LineDashStyle;
            ea.StartStyle = pa.StartStyle;
            ea.EndStyle = pa.EndStyle;
            ea.Enable = pa.enable;
        }

        sectionLimit setLimit(PMSSeries ps) 
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

        SectionSeries setSectionSeries(backSeries ps, sectionClass scX, sectionClassY scY, Section sec) 
        {
            SectionSeries ss = new SectionSeries(ps.ToSeries());
            ss.SectionChartType = ps.SectionChartType;
            ss.Distance = sec.Distance;
            //ss.SortWay = sc.SortWay;
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

        backSeries getSectionInfo(PMSSeries ps,sectionClass scX,sectionClassY scY,Section sec) 
        {
            backSeries bs = new backSeries(ps.ToSeries());
            bs.SectionChartType = (ps as SectionSeries).SectionChartType; ;
            sec.Distance = (ps as SectionSeries).Distance;
            //sec.SortWay = (ps as SectionSeries).SortWay;
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
            //ssY.Limit = (ps as SectionSeries).Limit;
        }

        void intTreeLimit()
        {
            treeView1.Nodes.Add(sectionDec, sectionDec);
            if (treeView1.Nodes[sectionDec] != null)
            {
                treeView1.Nodes[sectionDec].Nodes.Add(xaxis, xaxis);
                treeView1.Nodes[sectionDec].Nodes[xaxis].Tag = scX;
                treeView1.Nodes[sectionDec].Nodes.Add(yaxis, yaxis);
                treeView1.Nodes[sectionDec].Nodes[yaxis].Tag = scY;
                treeView1.Nodes[sectionDec].Nodes.Add(curve, curve);
                treeView1.Nodes[sectionDec].Nodes[curve].Nodes.Add(Section, Section);
                treeView1.Nodes[sectionDec].Nodes[curve].Nodes[Section].Tag = section;
                treeView1.Nodes[sectionDec].Nodes[curve].Nodes.Add(alert, alert);
                treeView1.Nodes[sectionDec].Nodes.Add(apperence, apperence);
                treeView1.Nodes[sectionDec].Nodes[apperence].Tag = seriesMain;
                treeView1.Nodes[sectionDec].Nodes.Add(title, title);
                if (titleList.Count != 0)
                {
                    for (int i = 0; i < titleList.Count; i++)
                    {
                        treeView1.Nodes[sectionDec].Nodes[title].Nodes.Add(titleList[i].Name, titleList[i].Name);
                        treeView1.Nodes[sectionDec].Nodes[title].Nodes[titleList[i].Name].Tag = titleList[i];
                    }

                }
                if (limitList.Count > 0)
                {
                    for (int i = 0; i < limitList.Count; i++)
                    {
                        treeView1.Nodes[sectionDec].Nodes[curve].Nodes[alert].Nodes.Add(limitList[i].Name, limitList[i].Name);
                        treeView1.Nodes[sectionDec].Nodes[curve].Nodes[alert].Nodes[limitList[i].Name].Tag = limitList[i];
                    }
                }
                treeView1.ExpandAll();
            }
        }

        void intTree() 
        {
            treeView1.Nodes.Add(sectionDec, sectionDec);
            if (treeView1.Nodes[sectionDec] != null)
            {
                treeView1.Nodes[sectionDec].Nodes.Add(xaxis, xaxis);
                treeView1.Nodes[sectionDec].Nodes[xaxis].Tag = scX;
                treeView1.Nodes[sectionDec].Nodes.Add(yaxis, yaxis);
                treeView1.Nodes[sectionDec].Nodes[yaxis].Tag = scY;
                treeView1.Nodes[sectionDec].Nodes.Add(curve, curve);
                treeView1.Nodes[sectionDec].Nodes[curve].Nodes.Add(Section, Section);
                treeView1.Nodes[sectionDec].Nodes[curve].Nodes[Section].Tag = section;
                treeView1.Nodes[sectionDec].Nodes[curve].Nodes.Add(alert, alert);
                treeView1.Nodes[sectionDec].Nodes.Add(apperence, apperence);
                treeView1.Nodes[sectionDec].Nodes[apperence].Tag = seriesMain;
                treeView1.Nodes[sectionDec].Nodes.Add(title, title);
                if (titleList.Count != 0)
                {
                    for (int i = 0; i < titleList.Count; i++)
                    {
                        treeView1.Nodes[sectionDec].Nodes[title].Nodes.Add(titleList[i].Name, titleList[i].Name);
                        treeView1.Nodes[sectionDec].Nodes[title].Nodes[titleList[i].Name].Tag = titleList[i];
                    }

                }
                if (seriesList.Count - 1 > 0)
                {
                    for (int i = 0; i < seriesList.Count - 1; i++)
                    {
                        treeView1.Nodes[sectionDec].Nodes[curve].Nodes[alert].Nodes.Add(seriesList[i + 1].Name, seriesList[i + 1].Name);
                        limitList.Add(setLimit(seriesList[i + 1]));
                        treeView1.Nodes[sectionDec].Nodes[curve].Nodes[alert].Nodes[seriesList[i + 1].Name].Tag = limitList[i];
                    }
                }
                treeView1.ExpandAll();
            }
        }
    }

    public class sectionClass
    {
        //public enum enumSectionChartType { Line, Spline, Stepline, Fastline, Area };
        SectionSeries.enumSectionChartType sectionChartType;
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Category("MES报表属性")]
        [DefaultValue(SectionSeries.enumSectionChartType.Line)]
        [Description("获取或设置分段曲线的图表类型")]
        public SectionSeries.enumSectionChartType SectionChartType
        {
            get { return sectionChartType; }
            set { sectionChartType = value; }
        }

        //[Browsable(false)]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //public override SeriesChartType ChartType { get; set; }

        //public enum enumSortWay { Ascending, Descending };
        SectionSeries.enumSortWay sortWay;
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Category("MES报表属性")]
        [Description("获取或设置数据的按时间字段排序方式")]
        public SectionSeries.enumSortWay SortWay
        {
            get { return sortWay; }
            set { sortWay = value; }
        }

        //private string _SectionField = "";
        //[Description("分段字段")]
        //[Category("MES报表属性")]
        //[Editor(typeof(SectionEditor), typeof(UITypeEditor))]
        //public string SectionField
        //{
        //    get { return _SectionField; }
        //    set { _SectionField = value; }
        //}

        private string _BindingField = "";
        [Description("绑定字段")]
        [Category("MES报表属性")]
        [Editor(typeof(SectionEditor), typeof(UITypeEditor))]
        public virtual string BindingField
        {
            get
            {
                return _BindingField;
            }
            set
            {
                _BindingField = value;
            }
        }

        public enum enumLabelStyle { WordWrap, LabelsAngleStep90 };

        enumLabelStyle labelStyle;
        [Category("X轴标签设置")]
        [Description("获取或设置分段曲线的X轴标签自适应模式")]
        public enumLabelStyle LabelStyle
        {
            get { return labelStyle; }
            set { labelStyle = value; }
        }

        //public enum enumTimeStyle { LongDate, LongTime, ShortDate, ShortTime,FullDateTime }

        string format;
        [Category("X轴标签设置")]
        //[Browsable(false)]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        [Description(@"获取或设置分段曲线的X轴标签的格式化信息（仅对时间类型):
   yyyy/MM/dd
   HH:mm:ss
   yyyy/MM/dd <HH:mm:ss>")]
        public string Format
        {
            get { return format; }
            set 
            {
                format = value;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public SourceField SourceField
        {
            get;
            set;
        }
    }

    public class sectionClassY
    {
        bool autoScale;
        [Category("Y轴刻度范围")]
        [Description("是否使用自动生成Y轴刻度范围")]
        [DefaultValue(false)]
        public bool AutoScale
        {
            get { return autoScale; }
            set { autoScale = value; }
        }

        double max;
        [Category("Y轴刻度范围")]
        [DefaultValue(double.NaN)]
        public double Max
        {
            get { return max; }
            set { max = value; }
        }

        double min;
        [Category("Y轴刻度范围")]
        [DefaultValue(double.NaN)]
        public double Min
        {
            get { return min; }
            set { min = value; }
        }
    }

    public class Section
    {
        private string _BindingField = "";
        [Description("绑定字段")]
        [Category("MES报表属性")]
        [Editor(typeof(SectionEditor), typeof(UITypeEditor))]
        public virtual string BindingField
        {
            get
            {
                return _BindingField;
            }
            set
            {
                _BindingField = value;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public SourceField SourceField
        {
            get;
            set;
        }

        int pointsCount=1;
        [Description("获取或设置控件中每个图标区域包含的数据点的数量")]
        [Category("MES报表属性")]
        public int PointsCount
        {
            get { return pointsCount; }
            set 
            {
                if (value > 0)
                {
                    pointsCount = value;
                }
                else
                {
                    throw new System.Exception(Properties.Resources.ResourceManager.GetString("message0018"));
                }
            }
        }

        double distance;
        [Category("X轴分段设置")]
        [Description("获取或设置分段曲线的时间间隔")]
        public double Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        //public enum enumTimeType { second, minute, hour };
        SectionSeries.enumTimeType timeType;
        [Category("X轴分段设置")]
        [Description("获取或设置分段曲线的时间间隔单位")]
        public SectionSeries.enumTimeType TimeType
        {
            get { return timeType; }
            set { timeType = value; }
        }

        startAnnotation startAnnotation;
        [Bindable(true)]
        [Category("分段曲线属性")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("获取或设置分段起始标记相关的属性")]
        [TypeConverter(typeof(StringPropertiesConverter))]
        public startAnnotation StartAnnotation
        {
            get
            {
                if (startAnnotation == null)
                {
                    startAnnotation = new startAnnotation();
                }
                return startAnnotation;
            }
        }

        endAnnotation endAnnotation;
        [Bindable(true)]
        [Category("分段曲线属性")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("获取或设置分段终结标记相关的属性")]
        [TypeConverter(typeof(StringPropertiesConverter))]
        public endAnnotation EndAnnotation
        {
            get
            {
                if (endAnnotation == null)
                {
                    endAnnotation = new endAnnotation();
                }
                return endAnnotation;
            }
        }
        //MaxandMinmum axisMum;
        //[Category("")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //[Description("Y轴最大值")]
        //[TypeConverter(typeof(StringPropertiesConverter))]
        //public MaxandMinmum AxisMum
        //{
        //    get
        //    {
        //        if (axisMum == null)
        //        {
        //            axisMum = new MaxandMinmum();
        //        }
        //        return axisMum;
        //    }
        //}
    }
    [Serializable]
    public class MaxandMinmum 
    {
        bool enable;
        [Category("MES报表属性")]
        [DefaultValue(false)]
        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }

        double yaxisMaxmum;
        [Category("MES报表属性")]
        [DefaultValue(double.NaN)]
        public double YaxisMaxmum
        {
            get { return yaxisMaxmum; }
            set { yaxisMaxmum = value; }
        }

        double yaxisMinmum;
        [Category("MES报表属性")]
        [DefaultValue(double.NaN)]
        public double YaxisMinmum
        {
            get { return yaxisMinmum; }
            set { yaxisMinmum = value; }
        }
    }
}
