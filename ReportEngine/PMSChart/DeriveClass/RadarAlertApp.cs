using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using System.Windows.Forms.DataVisualization.Charting;

namespace PMS.Libraries.ToolControls.PMSChart
{
    public partial class RadarAlertApp : Form
    {
        public RadarAlertApp()
        {
            InitializeComponent();
        }

        public RadarAlertChart ChartParent;
        RadarAlertSeries SeriesNow = new RadarAlertSeries(null);
        RadarAlertSeries SeriesMax = new RadarAlertSeries(null);
        RadarAlertSeries SeriesMin = new RadarAlertSeries(null);
        
        //保存统计信息
        public List<AlertVar> AllAlertList = new List<AlertVar>();
        public List<AlertVar> AlertList = new List<AlertVar>();
        //数据外观管理
        private List<PMSSeries> seriesList = new List<PMSSeries>();

        //图例管理
        private List<PMSLegend> legendList = new List<PMSLegend>();


        //标题管理
        private List<PMSTitle> titleList = new List<PMSTitle>();

        int DrawSelect;
        int LableSelect;
        int ReportSelect;

        public bool isApply = false;
        public DataSource ds;

        public enum VarType { 实际值, 上限外观, 下限外观 };



        public List<PMSTitle> TitleList
        {
            get { return titleList; }
            set { titleList = value; }
        }

        public List<PMSSeries> SeriesList
        {
            get
            {
                return seriesList;
            }
            set
            {
                seriesList = value;

            }
        }


        public List<PMSLegend> LegendList
        {
            get { return legendList; }
            set { legendList = value; }
        }

        private void RadarAlertApp_Load(object sender, EventArgs e)
        {
            try
            {
                List<string> alertName = new List<string>();
                foreach (AlertVar item in AllAlertList)
                {
                    alertName.Add(item.Name);
                }
                FieldTreeViewData sfAll = (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetCurrentReportDataDefine()) as FieldTreeViewData;

                if (ChartParent != null && sfAll != null )
                {
                    if (ChartParent.SourceField != null)
                    {
                        List<SourceField> lpdb = ChartParent.SourceField.GetSubSourceField(sfAll);
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
                                        typ.Equals("SYSTEM.DECIMAL", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        if (!alertName.Contains(pdb.RecordField))
                                        AllAlertList.Add(new AlertVar(pdb.RecordField));
                                        VarcheckedListBox.Items.Add(pdb.RecordField);
                                    }
                                }
                            }
                            catch
                            {
                                throw new Exception("lpdb");
                            }
                        }
                    }

                    //List<AlertVar> varAdd = new List<AlertVar>();
                    List<string> VarName = new List<string>();
                    foreach (AlertVar item in AlertList)
                    {
                        VarName.Add(item.Name);
                    }
                    foreach (AlertVar item in AlertList)
                    {
                        for (int i = 0; i < VarcheckedListBox.Items.Count; i++)
                        {
                            if (item.Name == VarcheckedListBox.Items[i].ToString())
                            {
                                VarcheckedListBox.SetItemChecked(i, true);
                            }
                        }
                    }

                    foreach (AlertVar item in AllAlertList)
                    {
                        if (!VarName.Contains(item.Name))
                        {
                            AlertList.Add(item);
                        }
                    }

                    foreach (PMSTitle item in titleList)
                    {
                        TitlelistBox.Items.Add(item.Name);
                    }

                    foreach (PMSLegend item in legendList)
                    {
                        LegendlistBox.Items.Add(item.Name);
                    }

                    if (SeriesList.Count == 0)
                    {
                        SeriesNow.Name = "实际值";
                        SeriesMax.Name = "上限外观";
                        SeriesMin.Name = "下限外观";
                        SeriesNow.RadarAlertProperties.RadarDrawingStyle = RadarProperties.enumRadarDrawingStyle.Line;

                        SeriesList.Add(SeriesMax);
                        SeriesList.Add(SeriesMin);
                        SeriesList.Add(SeriesNow);
                    }
                    else
                    {
                        SeriesMax = PMStoRadar(SeriesList[0]);
                        SeriesMin = PMStoRadar(SeriesList[1]);
                        SeriesNow = PMStoRadar(SeriesList[2]);
                        SeriesNow.Name = "实际值";
                        SeriesMax.Name = "上限外观";
                        SeriesMin.Name = "下限外观";
                        //if (SeriesList[0].IsVisibleInLegend)
                        //    LegendCheck.Checked = true;
                    }

                    for (int i = 0; i < CBdrawingStyle.Items.Count; i++)
                    {
                        if (CBdrawingStyle.Items[i].ToString() == SeriesMax.RadarAlertProperties.RadarAreaDrawingStyle.ToString())
                        {
                            CBdrawingStyle.Text = SeriesMax.RadarAlertProperties.RadarAreaDrawingStyle.ToString();
                            DrawSelect = i;
                            break;
                        }
                    }
                    for (int i = 0; i < CBlableStyle.Items.Count; i++)
                    {
                        if (CBlableStyle.Items[i].ToString() == SeriesMax.RadarAlertProperties.RadarCircularLabelsStyle.ToString())
                        {
                            CBlableStyle.Text = SeriesMax.RadarAlertProperties.RadarCircularLabelsStyle.ToString();
                            LableSelect = i;
                            break;
                        }
                    }
                    if (AlertList.Count != 0)
                    {
                        for (int i = 0; i < CBreportMode.Items.Count; i++)
                        {
                            if (CBreportMode.Items[i].ToString() == AlertList[0].AlertReportMode.ToString())
                            {
                                CBreportMode.Text = AlertList[0].AlertReportMode.ToString();
                                ReportSelect = i;
                                break;
                            }
                        }
                    }
                    else
                    {
                        CBreportMode.Text = "Percent";
                        CBreportMode.Enabled = false;
                    }

                    PMSChartArea chartArea1 = new PMSChartArea(null);
                    chartArea1.Area3DStyle.Enable3D = true;
                    if (ChartParent.Apperence.ChartAreaList.Count != 0)
                    {
                        chartArea1.AxisY.IntervalOffset = ChartParent.Apperence.ChartAreaList[0].AxisY.IntervalOffset;
                        chartArea1.Area3DStyle.Enable3D = ChartParent.Apperence.ChartAreaList[0].Area3DStyle.Enable3D;
                    }
                    ChartParent.Apperence.ChartAreaList.Clear();
                    ChartParent.Apperence.ChartAreaList.Add(chartArea1);

                    if (ChartParent.Apperence.ChartAreaList.Count != 0)
                    {
                        if (ChartParent.Apperence.ChartAreaList[0].Area3DStyle.Enable3D)
                        {
                            Area3Dcheck.Checked = true;
                        }
                        if (CBreportMode.Text == "Percent")
                        {
                            if ((ChartParent.Apperence.ChartAreaList[0].AxisY.IntervalOffset == 0))
                            {
                                AxisLineCheck.Checked = true;
                            }
                        }
                        else 
                        {
                            if (ChartParent.Apperence.ChartAreaList[0].AxisY.IntervalOffset != 1) 
                            {
                                AxisLineCheck.Checked = true;
                            } 
                        }

                    }
                    else 
                    {
                        ChartParent.Apperence.ChartAreaList.Clear();
                        PMSChartArea chartArea2 = new PMSChartArea(null);
                        chartArea2.Area3DStyle.Enable3D = true;
                        ChartParent.Apperence.ChartAreaList.Add(chartArea2);
                        Area3Dcheck.Checked = true;
                    }

                    propertyTree1.SelectedPaneNode = propertyTree1.PaneNodes[0];

                    ApplistBox.Items.Add(VarType.实际值);
                    ApplistBox.Items.Add(VarType.上限外观);
                    ApplistBox.Items.Add(VarType.下限外观);


                    
                    

                }
            }
            catch { throw new Exception("SFALL"); }
        }

        private void ApplistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            setTitleNullSelect();
            setLegendNullSelect();
            setVarNullSelect();
            if (ApplistBox.SelectedItem != null)
                switch (ApplistBox.SelectedItem.ToString())
                {
                    case "实际值":
                        SeriesNow.legendList = this.LegendList;
                        propertyGrid1.SelectedObject = SeriesNow;
                        break;
                    case "上限外观":
                        SeriesMax.legendList = this.LegendList;
                        propertyGrid1.SelectedObject = SeriesMax;
                        break;
                    case "下限外观":
                        SeriesMin.legendList = this.LegendList;
                        propertyGrid1.SelectedObject = SeriesMin;
                        break;
                }

        }

        private void VarcheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            setAppNullSelect();
            setTitleNullSelect();
            setLegendNullSelect();
            if (VarcheckedListBox.SelectedItem != null)
            {
                foreach (AlertVar item in AlertList)
                {
                    if (item.Name == VarcheckedListBox.SelectedItem.ToString())
                    {
                        propertyGrid1.SelectedObject = item;
                    }
                }

                if (propertyGrid1.SelectedObject != null)
                {
                    AlertVar var = propertyGrid1.SelectedObject as AlertVar;

                    if (var.Name != VarcheckedListBox.SelectedItem.ToString())
                    {
                        foreach (AlertVar item in AllAlertList)
                        {
                            if (item.Name == VarcheckedListBox.SelectedItem.ToString())
                            {
                                propertyGrid1.SelectedObject = item;
                            }
                        }
                    }
                }
            }
        }

        private void LegendlistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            setAppNullSelect();
            setTitleNullSelect();
            setVarNullSelect();
            if (LegendlistBox.SelectedItem != null)
            foreach (PMSLegend item in legendList)
            {
                if (item.Name == LegendlistBox.SelectedItem.ToString())
                {
                    propertyGrid1.SelectedObject = item;
                }
            }
        }

        private void TitlelistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            setAppNullSelect();
            setLegendNullSelect();
            setVarNullSelect();
            if (TitlelistBox.SelectedItem != null)
            foreach (PMSTitle item in titleList)
            {
                if (item.Name ==  TitlelistBox.SelectedItem.ToString())
                {
                    propertyGrid1.SelectedObject = item;
                }
            }
        }

        private void Allcheck_CheckedChanged(object sender, EventArgs e)
        {
            if (Dechecked.Checked == true)
            {
                this.Dechecked.CheckedChanged -= new System.EventHandler(this.Dechecked_CheckedChanged);
                Dechecked.Checked = false;
                this.Dechecked.CheckedChanged += new System.EventHandler(this.Dechecked_CheckedChanged);
            }
            if (Allcheck.Checked == true)
            for (int i = 0; i < VarcheckedListBox.Items.Count; i++)
                VarcheckedListBox.SetItemChecked(i, true);
        }

        private void Dechecked_CheckedChanged(object sender, EventArgs e)
        {
            if (Allcheck.Checked == true)
            {
                this.Allcheck.CheckedChanged -= new System.EventHandler(this.Allcheck_CheckedChanged);
                Allcheck.Checked = false;
                this.Allcheck.CheckedChanged += new System.EventHandler(this.Allcheck_CheckedChanged);
            }
            if (Dechecked.Checked == true)
            for (int i = 0; i < VarcheckedListBox.Items.Count; i++)
                VarcheckedListBox.SetItemChecked(i, false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckedListBox.CheckedItemCollection check = VarcheckedListBox.CheckedItems;
            for (int i = 0; i < VarcheckedListBox.Items.Count; i++)
            {
                if (check.Contains(VarcheckedListBox.Items[i]))
                {
                    VarcheckedListBox.SetItemChecked(i, false);
                }
                else
                {
                    VarcheckedListBox.SetItemChecked(i, true);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (VarcheckedListBox.CheckedItems.Count == VarcheckedListBox.Items.Count & VarcheckedListBox.Items.Count != 0)
            {
                this.Allcheck.CheckedChanged -= new System.EventHandler(this.Allcheck_CheckedChanged);
                Allcheck.Checked = true;
                this.Allcheck.CheckedChanged += new System.EventHandler(this.Allcheck_CheckedChanged);
                this.Dechecked.CheckedChanged -= new System.EventHandler(this.Dechecked_CheckedChanged);
                Dechecked.Checked = false;
                this.Dechecked.CheckedChanged += new System.EventHandler(this.Dechecked_CheckedChanged);
            }
            else if (VarcheckedListBox.CheckedItems.Count == 0 & VarcheckedListBox.Items.Count != 0)
            {
                this.Dechecked.CheckedChanged -= new System.EventHandler(this.Dechecked_CheckedChanged);
                Dechecked.Checked = true;
                this.Dechecked.CheckedChanged += new System.EventHandler(this.Dechecked_CheckedChanged);
                this.Allcheck.CheckedChanged -= new System.EventHandler(this.Allcheck_CheckedChanged);
                Allcheck.Checked = false;
                this.Allcheck.CheckedChanged += new System.EventHandler(this.Allcheck_CheckedChanged);
            }
            else
            {
                this.Allcheck.CheckedChanged -= new System.EventHandler(this.Allcheck_CheckedChanged);
                Allcheck.Checked = false;
                this.Allcheck.CheckedChanged += new System.EventHandler(this.Allcheck_CheckedChanged);
                this.Dechecked.CheckedChanged -= new System.EventHandler(this.Dechecked_CheckedChanged);
                Dechecked.Checked = false;
                this.Dechecked.CheckedChanged += new System.EventHandler(this.Dechecked_CheckedChanged);
            }
        }

        private void VarcheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //if (VarcheckedListBox.CheckedItems.Count == 0) 
            //{
            //    this.Dechecked.CheckedChanged -= new System.EventHandler(this.Dechecked_CheckedChanged);
            //    Dechecked.Checked = true;
            //    this.Dechecked.CheckedChanged += new System.EventHandler(this.Dechecked_CheckedChanged);
            //}
            //if (VarcheckedListBox.CheckedItems.Count == VarcheckedListBox.Items.Count) 
            //{
            //    this.Allcheck.CheckedChanged -= new System.EventHandler(this.Allcheck_CheckedChanged);
            //    Allcheck.Checked = true;
            //    this.Allcheck.CheckedChanged += new System.EventHandler(this.Allcheck_CheckedChanged);
            //}
            bool exist = false;
            if (e.NewValue == CheckState.Unchecked)
            {

                foreach (AlertVar item in AlertList)
                {
                    if (item.Name == VarcheckedListBox.Items[e.Index].ToString())
                        for (int i = 0; i < AllAlertList.Count; i++)
                        {
                            if (AllAlertList[i].Name == item.Name)
                            {
                                AllAlertList[i] = item.Clone() as AlertVar;

                            }
                        }
                }
            }
            else
            {
                foreach (AlertVar item in AlertList)
                {
                    if (item.Name == VarcheckedListBox.Items[e.Index].ToString())
                    {
                        exist = true;
                    }
                }
                if (!exist)
                {
                    for (int i = 0; i < AllAlertList.Count; i++)
                    {
                        if (AllAlertList[i].Name == VarcheckedListBox.Items[e.Index].ToString())
                        {
                            AlertList.Add(AllAlertList[i].Clone() as AlertVar);
                        }
                    }
                }
            }

        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {

            if (e.ChangedItem.Label == "Name")
            {
                if (TitlelistBox.SelectedItem != null)
                {
                    if (!TitlelistBox.Items.Contains(e.ChangedItem.Value))
                    {

                        TitlelistBox.Items.Clear();
                        for (int i = 0; i < titleList.Count; i++)
                        {
                            TitlelistBox.Items.Add(titleList[i].Name);
                        }


                    }
                    else
                    {
                        (propertyGrid1.SelectedObject as PMSTitle).Name = e.OldValue.ToString();
                    }
                }
                if (LegendlistBox.SelectedItem != null)
                {
                    if (!LegendlistBox.Items.Contains(e.ChangedItem.Value))
                    {

                        LegendlistBox.Items.Clear();
                        for (int i = 0; i < legendList.Count; i++)
                        {
                            LegendlistBox.Items.Add(LegendList[i].Name);
                        }


                    }
                    else
                    {
                        (propertyGrid1.SelectedObject as PMSLegend).Name = e.OldValue.ToString();
                    }
                }
                if (ApplistBox.SelectedItem != null) 
                {
                    (propertyGrid1.SelectedObject as RadarAlertSeries).Name = e.OldValue.ToString();
                }
            }
            
            if (e.ChangedItem.Label == "MaxValue") 
            {
                if (int.Parse(e.ChangedItem.Value.ToString()) <= (propertyGrid1.SelectedObject as AlertVar).MinValue) 
                {
                    (propertyGrid1.SelectedObject as AlertVar).MaxValue = (double)e.OldValue;
                }
            }
            if (e.ChangedItem.Label == "MinValue")
            {
                if (int.Parse(e.ChangedItem.Value.ToString()) >= (propertyGrid1.SelectedObject as AlertVar).MaxValue)
                {
                    (propertyGrid1.SelectedObject as AlertVar).MinValue = (double)e.OldValue;
                }
            }
            if (e.ChangedItem.Label == "Legend") 
            {
                bool exist = false;
                foreach (PMSLegend item in legendList)
                {
                    if (e.ChangedItem.Value.ToString() == item.Name) 
                    {
                        exist = true;
                        break;
                    }
                }
                if(!exist)
                {
                    (propertyGrid1.SelectedObject as RadarAlertSeries).Legend = e.OldValue.ToString();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            setAppNullSelect();
            setTitleNullSelect();
            setLegendNullSelect();
            setVarNullSelect();
            propertyGrid1.SelectedObject = null;
            PMSTitle pt = new PMSTitle(null);
            pt.Name = GetNameFromList(titleList, "title");
            pt.Text = pt.Name;
            if (TitlelistBox.Items.Count != 0)
            {
                for (int i = 0; i < TitlelistBox.Items.Count; i++)
                {
                    int j;
                    if (TitlelistBox.Items[i].ToString().Length > 5)
                    {
                        if (int.TryParse(TitlelistBox.Items[i].ToString().Substring(5), out j))
                            if (int.Parse(pt.Name.Substring(5)) < int.Parse(TitlelistBox.Items[i].ToString().Substring(5)))
                            {
                                TitlelistBox.Items.Insert(i, pt.Name);
                                titleList.Insert(i, pt);
                                break;
                            }
                    }
                        if (i == TitlelistBox.Items.Count - 1)
                        {
                            TitlelistBox.Items.Add(pt.Name);
                            titleList.Add(pt);
                            break;
                        }
                                      
                }
            }
            else 
            {
                TitlelistBox.Items.Add(pt.Name);
                titleList.Add(pt);
            }          
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (TitlelistBox.SelectedItem != null)
            {

                foreach (PMSTitle item in titleList)
                {
                    if (TitlelistBox.SelectedItem.ToString() == item.Name)
                    {
                        TitlelistBox.Items.Remove(TitlelistBox.SelectedItem);
                        titleList.Remove(item);
                        break;
                    }
                }
                TitlelistBox.SelectedItem = null;
                propertyGrid1.SelectedObject = null;
            }
        }

        private void btnLegendAdd_Click(object sender, EventArgs e)
        {
            setAppNullSelect();
            setTitleNullSelect();
            setLegendNullSelect();
            setVarNullSelect();
            propertyGrid1.SelectedObject = null;
            if (legendList.Count < 3)
            {
                PMSLegend pl = new PMSLegend(null);
                pl.Name = GetNameFromList(legendList, "legend");
                if (LegendlistBox.Items.Count != 0)
                {
                    for (int i = 0; i < LegendlistBox.Items.Count; i++)
                    {
                        int j;
                        if (LegendlistBox.Items[i].ToString().Length > 6)
                        {
                            if (int.TryParse(LegendlistBox.Items[i].ToString().Substring(6), out j))
                                if (int.Parse(pl.Name.Substring(6)) < int.Parse(LegendlistBox.Items[i].ToString().Substring(6)))
                                {
                                    LegendlistBox.Items.Insert(i, pl.Name);
                                    legendList.Insert(i, pl);
                                    break;
                                }
                        }
                        if (i == LegendlistBox.Items.Count - 1)
                        {
                            LegendlistBox.Items.Add(pl.Name);
                            legendList.Add(pl);
                            break;
                        }

                    }
                }
                else
                {
                    LegendlistBox.Items.Add(pl.Name);
                    legendList.Add(pl);
                }
            }
        }

        private void btnLegendDel_Click(object sender, EventArgs e)
        {
            if (LegendlistBox.SelectedItem != null)
            {

                foreach (PMSLegend item in legendList)
                {
                    if (LegendlistBox.SelectedItem.ToString() == item.Name)
                    {
                        LegendlistBox.Items.Remove(LegendlistBox.SelectedItem);
                        legendList.Remove(item);
                        break;
                    }
                }
                LegendlistBox.SelectedItem = null;
                propertyGrid1.SelectedObject = null;
            }
        }

        private void Sure_Click(object sender, EventArgs e)
        {
            List<AlertVar> varDel = new List<AlertVar>();
            for (int i = 0; i < AlertList.Count; i++)
            {
                if (!VarcheckedListBox.CheckedItems.Contains(AlertList[i].Name))
                {
                    varDel.Add(AlertList[i]);
                }
            }
            for (int i = 0; i < varDel.Count; i++)
            {
                AlertList.Remove(varDel[i]);
            }
            setRadarSeries();
            if (seriesList.Count != 0)
            {
                SeriesList[0] = SeriesMax;
                SeriesList[1] = SeriesMin;
                SeriesList[2] = SeriesNow;
            }

            if (Area3Dcheck.Checked && AxisLineCheck.Checked)
            {
                ChartParent.Apperence.ChartAreaList.Clear();
                PMSChartArea chartArea1 = new PMSChartArea(null);
                chartArea1.Area3DStyle.Enable3D = true;
                ChartParent.Apperence.ChartAreaList.Add(chartArea1);
            }
            if (Area3Dcheck.Checked && !AxisLineCheck.Checked)
            {
                ChartParent.Apperence.ChartAreaList.Clear();
                PMSChartArea chartArea1 = new PMSChartArea(null);
                chartArea1.Area3DStyle.Enable3D = true;
                chartArea1.AxisY.IntervalOffset = 1;
                ChartParent.Apperence.ChartAreaList.Add(chartArea1);
            }
            if (!Area3Dcheck.Checked && AxisLineCheck.Checked)
            {
                ChartParent.Apperence.ChartAreaList.Clear();
                PMSChartArea chartArea1 = new PMSChartArea(null);
                chartArea1.Area3DStyle.Enable3D = false;
                ChartParent.Apperence.ChartAreaList.Add(chartArea1);
            }
            if (!Area3Dcheck.Checked && !AxisLineCheck.Checked)
            {
                ChartParent.Apperence.ChartAreaList.Clear();
                PMSChartArea chartArea1 = new PMSChartArea(null);
                chartArea1.Area3DStyle.Enable3D = false;
                chartArea1.AxisY.IntervalOffset = 1;
                ChartParent.Apperence.ChartAreaList.Add(chartArea1);
            }

            if (CBreportMode.Text == "Percent" && AxisLineCheck.Checked) 
            {
                ChartParent.Apperence.ChartAreaList[0].AxisY.IntervalOffset = 0;
            }

            ChartParent.Apperence.TitleList = this.titleList;
            ChartParent.Apperence.SeriesList = this.seriesList;
            ChartParent.Apperence.alertList = this.AlertList;
            ChartParent.Apperence.LegendList = this.LegendList;
            this.DialogResult = DialogResult.OK;
            ChartParent.InitailColumnData();
            this.Dispose();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            isApply = true;
            List<AlertVar> varDel = new List<AlertVar>();
            for (int i = 0; i < AlertList.Count; i++)
            {
                if (!VarcheckedListBox.CheckedItems.Contains(AlertList[i].Name))
                {
                    varDel.Add(AlertList[i]);
                }
            }
            for (int i = 0; i < varDel.Count; i++)
            {
                AlertList.Remove(varDel[i]);
            }
            setRadarSeries();
            if (seriesList.Count != 0)
            {
                SeriesList[0] = SeriesMax;
                SeriesList[1] = SeriesMin;
                SeriesList[2] = SeriesNow;
            }

            if (Area3Dcheck.Checked && AxisLineCheck.Checked)
            {
                ChartParent.Apperence.ChartAreaList.Clear();
                PMSChartArea chartArea1 = new PMSChartArea(null);
                chartArea1.Area3DStyle.Enable3D = true;
                ChartParent.Apperence.ChartAreaList.Add(chartArea1);
            }
            if (Area3Dcheck.Checked && !AxisLineCheck.Checked)
            {
                ChartParent.Apperence.ChartAreaList.Clear();
                PMSChartArea chartArea1 = new PMSChartArea(null);
                chartArea1.Area3DStyle.Enable3D = true;
                chartArea1.AxisY.IntervalOffset = 1;
                ChartParent.Apperence.ChartAreaList.Add(chartArea1);
            }
            if (!Area3Dcheck.Checked && AxisLineCheck.Checked)
            {
                ChartParent.Apperence.ChartAreaList.Clear();
                PMSChartArea chartArea1 = new PMSChartArea(null);
                chartArea1.Area3DStyle.Enable3D = false;
                ChartParent.Apperence.ChartAreaList.Add(chartArea1);
            }
            if (!Area3Dcheck.Checked && !AxisLineCheck.Checked)
            {
                ChartParent.Apperence.ChartAreaList.Clear();
                PMSChartArea chartArea1 = new PMSChartArea(null);
                chartArea1.Area3DStyle.Enable3D = false;
                chartArea1.AxisY.IntervalOffset = 1;
                ChartParent.Apperence.ChartAreaList.Add(chartArea1);
            }

            if (CBreportMode.Text == "Percent" && AxisLineCheck.Checked)
            {
                ChartParent.Apperence.ChartAreaList[0].AxisY.IntervalOffset = 0;
            }
            
            ChartParent.Apperence.alertList = this.AlertList;
            ChartParent.Apperence.TitleList = this.titleList;
            ChartParent.Apperence.SeriesList = this.seriesList;
            ChartParent.Apperence.LegendList = this.LegendList;
            ChartParent.Apperence.allAlertList = this.AllAlertList;

            ds = ChartParent.Apperence.Clone();
            ChartParent.InitailColumnData();
            
        }

        #region 停用方法
        private void CBdrawingStyle_KeyDown(object sender, KeyEventArgs e)
        {
            for (int i = 0; i < CBdrawingStyle.Items.Count; i++)
            {
                if (CBdrawingStyle.Items[i].ToString().StartsWith(e.KeyData.ToString()))
                {

                    DrawSelect = i;
                    break;
                }
            }
            CBdrawingStyle.Text = CBdrawingStyle.Items[DrawSelect].ToString();
            setRadarSeries();
        }

        private void CBdrawingStyle_KeyUp(object sender, KeyEventArgs e)
        {
            for (int i = 0; i < CBdrawingStyle.Items.Count; i++)
            {
                if (CBdrawingStyle.Items[i].ToString().StartsWith(e.KeyData.ToString()))
                {

                    DrawSelect = i;
                    break;
                }
            }
            CBdrawingStyle.Text = CBdrawingStyle.Items[DrawSelect].ToString();
            setRadarSeries();
        }



        private void CBlableStyle_KeyDown(object sender, KeyEventArgs e)
        {
            for (int i = 0; i < CBlableStyle.Items.Count; i++)
            {
                if (CBlableStyle.Items[i].ToString().StartsWith(e.KeyData.ToString()))
                {

                    LableSelect = i;
                    break;
                }
            }
            CBlableStyle.Text = CBlableStyle.Items[LableSelect].ToString();
            setRadarSeries();
        }

        private void CBlableStyle_KeyUp(object sender, KeyEventArgs e)
        {
            for (int i = 0; i < CBlableStyle.Items.Count; i++)
            {
                if (CBlableStyle.Items[i].ToString().StartsWith(e.KeyData.ToString()))
                {

                    LableSelect = i;
                    break;
                }
            }
            CBlableStyle.Text = CBlableStyle.Items[LableSelect].ToString();
            setRadarSeries();
        }



        private void CBreportMode_KeyUp(object sender, KeyEventArgs e)
        {
            for (int i = 0; i < CBreportMode.Items.Count; i++)
            {
                if (CBreportMode.Items[i].ToString().StartsWith(e.KeyData.ToString()))
                {

                    ReportSelect = i;
                    break;
                }
            }
            CBreportMode.Text = CBreportMode.Items[ReportSelect].ToString();
            setAlertVar();
        }

        private void CBreportMode_KeyDown(object sender, KeyEventArgs e)
        {
            for (int i = 0; i < CBreportMode.Items.Count; i++)
            {
                if (CBreportMode.Items[i].ToString().StartsWith(e.KeyData.ToString()))
                {

                    ReportSelect = i;
                    break;
                }
            }
            CBreportMode.Text = CBreportMode.Items[ReportSelect].ToString();
            setAlertVar();
        }
        #endregion

        private void CBdrawingStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawSelect = CBdrawingStyle.SelectedIndex;
            setRadarSeries();
        }

        private void CBlableStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            LableSelect = CBlableStyle.SelectedIndex;
            setRadarSeries();
        }

        private void CBreportMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReportSelect = CBreportMode.SelectedIndex;
            setAlertVar();
        }

        private void CBdrawingStyle_Enter(object sender, EventArgs e)
        {
            setAppNullSelect();
            setTitleNullSelect();
            setLegendNullSelect();
            setVarNullSelect();
            this.propertyGrid1.SelectedObject = null;
        }

        private void CBlableStyle_Enter(object sender, EventArgs e)
        {
            setAppNullSelect();
            setTitleNullSelect();
            setLegendNullSelect();
            setVarNullSelect();
            this.propertyGrid1.SelectedObject = null;
        }

        private void CBreportMode_Enter(object sender, EventArgs e)
        {
            setAppNullSelect();
            setTitleNullSelect();
            setLegendNullSelect();
            setVarNullSelect();
            this.propertyGrid1.SelectedObject = null;
        }

        /// <summary>
        /// 将PMSSeries转化为RadarAlertSeries
        /// </summary>
        private RadarAlertSeries PMStoRadar(PMSSeries ps)
        {
            Series series1 = new Series();
            ps.SetSeriesValue(series1);
            RadarAlertSeries ras = new RadarAlertSeries(series1);
            ras.RadarAlertProperties.CustomProperties = series1.CustomProperties;
            ras.Legend =  ps.Legend;
            return ras;

        }

        /// <summary>
        /// 获取系统自动生成的名称
        /// </summary>
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
                            if (int.TryParse(item.Name.Substring(aim.Length),out i)) 
                            {
                                nameNO.Add(i);
                            }
                        }
                    }
                    resetList(nameNO);
                    for (int i = 0; i < nameNO.Count; i++) 
                    {
                        if (nameNO[i] != i + 1) 
                        {
                            NO = i+1;
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
                    resetList(nameNO);
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
            return result+NO;
        }

        /// <summary>
        /// 对指定序列进行由小到大的排序
        /// </summary>
        /// <param name="nameNO">需要排序的序列</param>
        /// <returns></returns>
        static public void resetList(List<int> nameNO) 
        {
            int Var;
            for (int i = 0; i < nameNO.Count; i++) 
            {
                for (int j = i+1; j < nameNO.Count; j++) 
                {
                    if (nameNO[i] > nameNO[j]) 
                    {
                        Var = nameNO[i];
                        nameNO[i] = nameNO[j];
                        nameNO[j] = Var;
                    }
                }
            }
        }

        /// <summary>
        /// 对指定序列进行由小到大的排序
        /// </summary>
        /// <param name="nameNO">需要排序的序列</param>
        /// <returns></returns>
        static public void resetList(List<double> nameNO)
        {
            double Var;
            for (int i = 0; i < nameNO.Count; i++)
            {
                for (int j = i + 1; j < nameNO.Count; j++)
                {
                    if (nameNO[i] > nameNO[j])
                    {
                        Var = nameNO[i];
                        nameNO[i] = nameNO[j];
                        nameNO[j] = Var;
                    }
                }
            }
        }

        /// <summary>
        /// 对指定序列进行由小到大的排序
        /// </summary>
        /// <param name="nameNO">需要排序的序列</param>
        /// <returns></returns>
        static public void resetList(List<DateTime> nameNO)
        {
            DateTime Var;
            for (int i = 0; i < nameNO.Count; i++)
            {
                for (int j = i + 1; j < nameNO.Count; j++)
                {
                    if (nameNO[i] > nameNO[j])
                    {
                        Var = nameNO[i];
                        nameNO[i] = nameNO[j];
                        nameNO[j] = Var;
                    }
                }
            }
        }

        /// <summary>
        /// 通过combobox设置雷达图的特有属性
        /// </summary>
        public void setRadarSeries()
        {
            switch (CBdrawingStyle.Text)
            {
                case "Polygon":
                    SeriesMax.RadarAlertProperties.RadarAreaDrawingStyle = RadarProperties.enumRadarAreaDrawingStyle.Polygon;
                    SeriesMin.RadarAlertProperties.RadarAreaDrawingStyle = RadarProperties.enumRadarAreaDrawingStyle.Polygon;
                    SeriesNow.RadarAlertProperties.RadarAreaDrawingStyle = RadarProperties.enumRadarAreaDrawingStyle.Polygon;
                    break;
                case "Circle":
                    SeriesMax.RadarAlertProperties.RadarAreaDrawingStyle = RadarProperties.enumRadarAreaDrawingStyle.Circle;
                    SeriesMin.RadarAlertProperties.RadarAreaDrawingStyle = RadarProperties.enumRadarAreaDrawingStyle.Circle;
                    SeriesNow.RadarAlertProperties.RadarAreaDrawingStyle = RadarProperties.enumRadarAreaDrawingStyle.Circle;
                    break;
            }
            switch (CBlableStyle.Text)
            {
                case "Auto":
                    SeriesMax.RadarAlertProperties.RadarCircularLabelsStyle = RadarProperties.enumRadarCircularLabelsStyle.Auto;
                    SeriesMin.RadarAlertProperties.RadarCircularLabelsStyle = RadarProperties.enumRadarCircularLabelsStyle.Auto;
                    SeriesNow.RadarAlertProperties.RadarCircularLabelsStyle = RadarProperties.enumRadarCircularLabelsStyle.Auto;
                    break;
                case "Radial":
                    SeriesMax.RadarAlertProperties.RadarCircularLabelsStyle = RadarProperties.enumRadarCircularLabelsStyle.Radial;
                    SeriesMin.RadarAlertProperties.RadarCircularLabelsStyle = RadarProperties.enumRadarCircularLabelsStyle.Radial;
                    SeriesNow.RadarAlertProperties.RadarCircularLabelsStyle = RadarProperties.enumRadarCircularLabelsStyle.Radial;
                    break;
                case "Horizontal":
                    SeriesMax.RadarAlertProperties.RadarCircularLabelsStyle = RadarProperties.enumRadarCircularLabelsStyle.Horizontal;
                    SeriesMin.RadarAlertProperties.RadarCircularLabelsStyle = RadarProperties.enumRadarCircularLabelsStyle.Horizontal;
                    SeriesNow.RadarAlertProperties.RadarCircularLabelsStyle = RadarProperties.enumRadarCircularLabelsStyle.Horizontal;
                    break;
                case "Circular":
                    SeriesMax.RadarAlertProperties.RadarCircularLabelsStyle = RadarProperties.enumRadarCircularLabelsStyle.Circular;
                    SeriesMin.RadarAlertProperties.RadarCircularLabelsStyle = RadarProperties.enumRadarCircularLabelsStyle.Circular;
                    SeriesNow.RadarAlertProperties.RadarCircularLabelsStyle = RadarProperties.enumRadarCircularLabelsStyle.Circular;
                    break;
            }
        }

        /// <summary>
        /// 通过combobox设置变量表示模式
        /// </summary>
        public void setAlertVar() 
        {
            switch (CBreportMode.Text)
            {
                case "Percent":
                    for (int i = 0; i < AllAlertList.Count; i++) 
                    {
                        AllAlertList[i].AlertReportMode = AlertVar.enumAlertReportMode.Percent;
                    }
                    for (int i = 0; i < AlertList.Count; i++)
                    {
                        AlertList[i].AlertReportMode = AlertVar.enumAlertReportMode.Percent;
                    }
                        break;
                case "Absolute":
                     for (int i = 0; i < AllAlertList.Count; i++) 
                    {
                        AllAlertList[i].AlertReportMode = AlertVar.enumAlertReportMode.Absolute;
                    }
                    for (int i = 0; i < AlertList.Count; i++)
                    {
                        AlertList[i].AlertReportMode = AlertVar.enumAlertReportMode.Absolute;
                    }
                    break;
                case "Relative":
                    for (int i = 0; i < AllAlertList.Count; i++) 
                    {
                        AllAlertList[i].AlertReportMode = AlertVar.enumAlertReportMode.Relative;
                    }
                    for (int i = 0; i < AlertList.Count; i++)
                    {
                    AlertList[i].AlertReportMode = AlertVar.enumAlertReportMode.Relative;
                    }
                    break;
            } 
        }

        /// <summary>
        /// 设置标题栏的选中状态为全部未选中
        /// </summary>
        void setTitleNullSelect() 
        {
            this.TitlelistBox.SelectedIndexChanged -= new System.EventHandler(this.TitlelistBox_SelectedIndexChanged);
            for (int i = 0; i < TitlelistBox.Items.Count; i++)
            {
                TitlelistBox.SetSelected(i, false);
            }
            this.TitlelistBox.SelectedIndexChanged += new System.EventHandler(this.TitlelistBox_SelectedIndexChanged);
        }

        /// <summary>
        /// 设置外观栏的选中状态为全部未选中
        /// </summary>
        void setAppNullSelect()
        {
            this.ApplistBox.SelectedIndexChanged -= new System.EventHandler(this.ApplistBox_SelectedIndexChanged);
            for (int i = 0; i < ApplistBox.Items.Count; i++)
            {
                ApplistBox.SetSelected(i, false);
            }
            this.ApplistBox.SelectedIndexChanged += new System.EventHandler(this.ApplistBox_SelectedIndexChanged);
        }

        /// <summary>
        /// 设置变量栏的选中状态为全部未选中
        /// </summary>
        void setVarNullSelect()
        {
            this.VarcheckedListBox.SelectedIndexChanged -= new System.EventHandler(this.VarcheckedListBox_SelectedIndexChanged);
            for (int i = 0; i < VarcheckedListBox.Items.Count; i++)
            {
                VarcheckedListBox.SetSelected(i, false);
            }
            this.VarcheckedListBox.SelectedIndexChanged += new System.EventHandler(this.VarcheckedListBox_SelectedIndexChanged);
        }

        /// <summary>
        /// 设置图例栏的选中状态为全部未选中
        /// </summary>
        void setLegendNullSelect()
        {
            this.LegendlistBox.SelectedIndexChanged -= new System.EventHandler(this.LegendlistBox_SelectedIndexChanged);
            for (int i = 0; i < LegendlistBox.Items.Count; i++)
            {
                LegendlistBox.SetSelected(i, false);
            }
            this.LegendlistBox.SelectedIndexChanged += new System.EventHandler(this.LegendlistBox_SelectedIndexChanged);
        }

        


    }

    /// <summary>
    /// 获取或设置报警变量的相关属性
    /// </summary>
    [Serializable]
    public class AlertVar:ICloneable 
    {
        string describeName = "";
        string name;
        double minValue = 0;
        double maxValue = 0;
        public bool hasMaximum = false;

        [Description("变量报警下限")]
        [Category("变量设置")]
        public double MinValue
        {
            get { return minValue; }
            set { minValue = value; }
        }

        [Description("变量报警上限")]
        [Category("变量设置")]
        public double MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; }
        }
        [Browsable(false)]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [Description("变量名称")]
        [Category("变量设置")]
        public string DescribeName
        {
            get 
            {
                if (describeName.Trim() == "")
                {
                    return this.Name;
                }
                else { return describeName; }
            }
            set { describeName = value; }
        }

        [Description("是否显示最大刻度值")]
        [Category("变量设置")]
        public bool HasMaximum
        {
            get { return hasMaximum; }
            set { hasMaximum = value; }
        }

        public enum enumAlertFunction { Sum, Max, Min, Average };
        enumAlertFunction alertFunction;
        [Description("获取或设置对报警字段应用的聚合函数")]
        [Category("函数")]
        public enumAlertFunction AlertFunction
        {
            get { return alertFunction; }
            set { alertFunction = value; }
        }

        public enum enumAlertReportMode { Percent,Absolute,Relative };
        enumAlertReportMode alertReportMode;
        [Description("获取或设置报警字段的值在图表中的显示方式")]
        [Category("显示模式")]
        [Browsable(false)]
        [DefaultValue(enumAlertReportMode.Relative)]
        public enumAlertReportMode AlertReportMode
        {
            get { return alertReportMode; }
            set { alertReportMode = value; }
        }

        public AlertVar(string Name)
        {
            this.Name = Name;

        }

        public object Clone()
        {
            AlertVar a = new AlertVar(this.name);
            a.MinValue = this.MinValue;
            a.MaxValue = this.MaxValue;
            a.DescribeName = this.DescribeName;
            a.AlertFunction = this.AlertFunction;
            a.AlertReportMode = this.AlertReportMode;
            a.HasMaximum = this.HasMaximum;
            return a;
        }
    }
}
