using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using System.Data.SqlClient;
using System.Windows.Forms.DataVisualization.Charting;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using PMS.Libraries.ToolControls.PMSPublicInfo;
using PMS.Libraries.ToolControls.PmsPowerGroupCheck;
//using PMS.Libraries.ToolControls.PMSReport;
using PMS.Libraries.ToolControls.Report.Controls.EditorDialog;
using PMS.Libraries.ToolControls.Report.Element;
using System.IO;
using PMS.Libraries.ToolControls.Report.Elements.Util;

namespace PMS.Libraries.ToolControls.PMSChart
{
    [ToolboxBitmap(typeof(PMSChartCtrl), "Resources.PMSChartCtrl.png")]
    public partial class PMSChartCtrl : UserControl, ICloneable, IResizable, IElementTranslator,IElement
    {
        public PMSChartCtrl()
        {
            InitializeComponent();

            if (_actionList == null || _actionList.Count == 0)
            {
                _actionList = new List<ActionObj>
                    {
                        new ActionObj{ActionName = "保存图片"},
                        new ActionObj{ActionName = "打印"},
                        new ActionObj{ActionName = "打印预览"},
                        new ActionObj{ActionName = "刷新"}
                    };
            }
            if (_actionInfo == null)
            {
                _actionInfo = new ActionInfo(null);
                _actionInfo.ActionList = _actionList;
            }
            if (_recordField == null)
            {
                _recordField = new List<string>();
            }
            if (_allRecordField == null)
                _allRecordField = new List<string>();
        }

        #region 2012.01.13 增加
        public PMSChartCtrl(MemoryStream Aim)
        {
            try
            {
                InitializeComponent();

                if (Aim != null)
                {
                    Aim.Seek(0, SeekOrigin.Begin);
                    chart1.Serializer.Load(Aim);
                }
                if (_actionList == null || _actionList.Count == 0)
                {
                    _actionList = new List<ActionObj>
                    {
                        new ActionObj{ActionName = "保存图片"},
                        new ActionObj{ActionName = "打印"},
                        new ActionObj{ActionName = "打印预览"},
                        new ActionObj{ActionName = "刷新"}
                    };
                }
                if (_actionInfo == null)
                {
                    _actionInfo = new ActionInfo(null);
                    _actionInfo.ActionList = _actionList;
                }
                if (_recordField == null)
                {
                    _recordField = new List<string>();
                }
                if (_allRecordField == null)
                    _allRecordField = new List<string>();
            }
            catch (System.Exception ex)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, Properties.Resources.ResourceManager.GetString("message0006") + ex.Message, false);
            }
        }
        #endregion

        private DataSource _sqlSource;
        //动作权限组信息
        private List<ActionObj> _actionList;
        private ActionInfo _actionInfo;

        private int _RunMode = 0;
        private bool _isReport = false;
        private List<string> _recordField;
        private List<string> _allRecordField;
        private string _xRecordField = "";
        private DataTable _ReportData;
        private SourceField _SourceField;
        //private SourceField _allSourceField;

        private Point OriginPosition;
        private int OriginWidth;
        private int OriginHeight;
        private int _ChartType;

        #region 属性

        [Category("MES报表属性")]
        [Editor(typeof(SourceEditor), typeof(UITypeEditor))]
        public virtual SourceField SourceField
        {
            get { return _SourceField; }
            set
            {
                _SourceField = value;
                if (_SourceField != null)
                {
                    FieldTreeViewData sfAll = (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetCurrentReportDataDefine(this)) as FieldTreeViewData;
                    if (sfAll != null)
                    {
                        if (_recordField == null)
                            _recordField = new List<string>();

                        if (_allRecordField == null)
                            _allRecordField = new List<string>();
                        _recordField.Clear();
                        _allRecordField.Clear();
                        List<PmsField> lp = new List<PmsField>();

                        List<SourceField> lpdb = _SourceField.GetSubSourceField(sfAll);
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
                                        _recordField.Add(pdb.RecordField);
                                        _allRecordField.Add(pdb.RecordField);
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
        }

        //[Browsable(false)]
        //[Category("MES报表属性")]
        //public SourceField AllSourceField
        //{
        //    get { return _allSourceField; }
        //    set { _allSourceField = value; }
        //}

        [Description("是否嵌入报表")]
        [Category("MES报表属性")]
        [DefaultValue(false)]
        public virtual bool IsReport
        {
            get { return _isReport; }
            set 
            { 
                _isReport = value;
                if (_isReport == true)
                {
                    toolStrip1.Visible = false;
                }
                else
                {
                    toolStrip1.Visible = true;
                }
            }
        }

        ////[Editor(typeof(RecordFieldsEditor), typeof(UITypeEditor))]
        //[Description("报表显示字段")]
        //[Category("MES报表属性")]
        ////[DefaultValue(false)]
        //[Browsable(false)]
        //public List<string> RecordFields
        //{
        //    get;// { return _recordField; }
        //    set;// { _recordField = value; }
        //}

        [Description("报表显示字段")]
        [Category("MES报表属性")]
        //[DefaultValue(false)]
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        public virtual List<string> SelectRecordFields
        {
            get { return _recordField; }
            set { _recordField = value; }
        }
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        public virtual List<string> AllRecordFields
        {
            get { return _allRecordField; }
            set { _allRecordField = value; }
        }

        [Editor(typeof(XRecordFieldEditor), typeof(UITypeEditor))]
        [Description("报表显示字段X轴")]
        [Category("MES报表属性")]
        [DefaultValue(false)]
        public virtual string XRecordField
        {
            get { return _xRecordField; }
            set { _xRecordField = value; }
        }

        [Browsable(false)]
        [Description("报表数据源")]
        [Category("MES报表属性")]
        //[DefaultValue(false)]
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
        [Editor(typeof(SqlEditor), typeof(UITypeEditor))]
        [Description("设置数据源属性")]
        [Category("MES控件属性")]
        public virtual DataSource SqlSource
        {
            get { return _sqlSource; }
            set
            {
                _sqlSource = value;
                if (_isReport&&_sqlSource!=null)
                {
                    _recordField = _sqlSource.YAixs;
                }
                InitailColumnData();

            }
        }

        /// <summary>
        /// 统计图动作配置信息
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        [Editor(typeof(ActionEditor), typeof(UITypeEditor))]
        [Description("统计图动作配置信息")]
        [Category("MES控件属性")]
        public virtual ActionInfo ActionConfig
        {
            get { return _actionInfo; }
            set
            {
                _actionInfo = value;

                if (_actionInfo == null)
                {
                    _actionInfo = new ActionInfo(null);
                    _actionInfo.ActionList = _actionList;
                }
                else
                {
                    if (_actionInfo.ActionList.Count != _actionList.Count)
                        _actionInfo.ActionList = _actionList;
                }
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual int ChartType
        {
            get
            {
                return _ChartType;
            }
            set
            {
                _ChartType = value;
            }
        }
        [Bindable(true)]
        [DefaultValue(typeof(Color), "White")]
        [Editor("System.Windows.Forms.Design.DataVisualization.Charting.ChartColorEditor, System.Windows.Forms.DataVisualization.Design, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.Drawing.Design.UITypeEditor")]
        [TypeConverter(typeof(ColorConverter))]
        [Description("Gets or sets the color of the border line.")]
        public Color BorderlineColor 
        {
            get
            {
                if (_sqlSource != null && _sqlSource.PMSChartAppearance != null)
                {
                    return _sqlSource.PMSChartAppearance.BorderlineColor;
                }
                else
                {
                    return Color.White;
                }
            }
            set
            {
                _sqlSource.PMSChartAppearance.BorderlineColor = value;
                chart1.BorderlineColor = value;
            }
        }

        [Bindable(true)]
        [DefaultValue(ChartDashStyle.NotSet)]
        [Description("Gets or sets the style of the border line.")]
        public ChartDashStyle BorderlineDashStyle 
        {
            get
            {
                if (_sqlSource != null && _sqlSource.PMSChartAppearance != null)
                {
                    return _sqlSource.PMSChartAppearance.BorderlineDashStyle;
                }
                else
                {
                    return ChartDashStyle.NotSet;
                }
            }
            set
            {
                _sqlSource.PMSChartAppearance.BorderlineDashStyle = value;
                chart1.BorderlineDashStyle = value;
            }
        }

        [DefaultValue(1)]
        [Bindable(true)]
        [Description("Gets or sets the width of the border line.")]
        public int BorderlineWidth 
        {
            get
            {
                if (_sqlSource != null && _sqlSource.PMSChartAppearance != null)
                {
                    return _sqlSource.PMSChartAppearance.BorderlineWidth;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                _sqlSource.PMSChartAppearance.BorderlineWidth = value;
                chart1.BorderlineWidth = value;
            }
        }

        #region 2011.01.13 增加 多余属性
        public MESVarType MESType
        {
            get;
            set;
        }
        public bool HasRightBorder
        {
            get;
            set;
        }
        public bool HasLeftBorder { get; set; }
        public bool HasTopBorder { get; set; }
        public bool HasBottomBorder { get; set; }
        public string BorderName { get; set; }
        public bool CanInvalidate { get; set; }
        public List<ExternData> ExternDatas { get; set; }
        public float MoveX { get; set; }
        public float MoveY { get; set; }
        public ElementBorder Border { get; set; }
        public bool HasBorder { get; set; }
        public ExtendObject ExtendObject { get; set; }
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
        #endregion
        ///// <summary>
        ///// 2011.12.21 增加
        ///// 目的:使绑定数据后的图表控件能够序列化
        ///// </summary>
        //[Browsable(false)]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //public MemoryStream Serializer
        //{
        //    get
        //    {
        //        MemoryStream result = new MemoryStream();
        //        if (chart1.Serializer != null)
        //        {
        //            chart1.Serializer.Save(result);
        //        }
        //        return result;
        //    }
        //    set
        //    {
        //        if (value != null)
        //        {
        //            value.Seek(0, SeekOrigin.Begin);
        //            if (chart1.Serializer != null)
        //            {
        //                chart1.Serializer.Load(value);
        //            }
        //        }
        //    }
        //}
        #endregion

        #region 属性编辑器
        internal class SqlEditor : UITypeEditor
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
                        PMSChartCtrl control = null;
                        if (context.Instance.GetType() == typeof(PMSChartCtrl))
                            control = (PMSChartCtrl)context.Instance;

                        FormSql form1 = new FormSql();
                        form1.IsReport = control._isReport;
                        form1.ChartParent = control;
                        DataSource ds = control._sqlSource.Clone();
                        form1.SqlSource = control._sqlSource;
                        if (DialogResult.OK == editorService.ShowDialog(form1))
                        {
                            value = form1.SqlSource;
                            control.SelectRecordFields = form1.SqlSource.YAixs;
                        }
                        else
                            value = ds;
                        return value;
                    }
                }

                return value;
            }
        }
        public class SourceEditor : UITypeEditor
        {
            public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                if (null != context && null != context.Instance && null != context.Container)
                {
                    PMSChartCtrl element = context.Instance as PMSChartCtrl;
                    if (null != element)
                    {
                        SourceField sfAll = (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetCurrentReportDataDefine()) as SourceField;
                        //SourceBindDialog fbd = new SourceBindDialog(sfAll, element.SourceField);
                        SourceBindDialog fbd = new SourceBindDialog(sfAll, element.SourceField, true);
                        if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            value = fbd.SourceField;
                        }
                    }
                    return value;
                }
                return base.EditValue(context, provider, value);
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

        public class XRecordFieldEditor : UITypeEditor
        {
            public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                IWindowsFormsEditorService editorService = null;
                if (null != context && null != context.Instance && null != context.Container)
                {
                    PMSChartCtrl pcc = context.Instance as PMSChartCtrl;
                    if (null != pcc && null != pcc.SourceField)
                    {
                        editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                        if (editorService != null)
                        {
                            FieldTreeViewData sfAll = (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetCurrentReportDataDefine()) as FieldTreeViewData;
                            RelationFieldChoose rfc = new RelationFieldChoose(editorService);

                            rfc.strRField = (string)pcc.XRecordField;
                            List<PmsField> lp = new List<PmsField>();

                            List<SourceField> lpdb = pcc.SourceField.GetSubSourceField(sfAll);
                            foreach (SourceField pdb in lpdb)
                            {
                                PmsField pf = new PmsField();
                                pf.fieldName = pdb.RecordField;
                                pf.fieldDescription = pdb.Name;
                                lp.Add(pf);
                            }
                            rfc.pmsFieldList = lp;
                            editorService.DropDownControl(rfc);
                            value = rfc.strRField;
                            return value;
                        }
                    }
                }
                return base.EditValue(context, provider, value);
            }

            public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
            {
                if (null != context && null != context.Instance && null != context.Container)
                {

                    return UITypeEditorEditStyle.DropDown;
                }

                return base.GetEditStyle(context);
            }
        }
        
        /// <summary>
        /// 动作配置编辑器
        /// </summary>
        internal class ActionEditor : UITypeEditor
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
                        PMSChartCtrl cbx = (PMSChartCtrl)context.Instance;
                        FormActionPower fap = new FormActionPower();
                        fap.ActionObjList = cbx.ActionConfig.ActionList;
                        fap.StartPosition = FormStartPosition.CenterScreen;
                        if (fap.ShowDialog() == DialogResult.OK)
                        {
                            value = new ActionInfo(fap);
                        }
                        return value;
                    }
                }

                return value;
            }
        }
        #endregion

        #region 方法
        public virtual object Clone()
        {
            PMSChartCtrl pcc = new PMSChartCtrl();
            if(this.SourceField!=null)
                pcc.SourceField = this.SourceField.Clone();
            pcc.IsReport = this.IsReport;
            if (this.SelectRecordFields != null)
            {
                pcc.SelectRecordFields = new List<string>();

                foreach (string node in this.SelectRecordFields)
                {
                    pcc.SelectRecordFields.Add(node);
                }
            }
            pcc.SqlSource = this.SqlSource.Clone();
            pcc.XRecordField = this.XRecordField;
            pcc.RunMode = this.RunMode;
            pcc.Location = new Point(this.Location.X, this.Location.Y);
            pcc.OriginPosition = new Point(this.Location.X, this.Location.Y);
            pcc.Height = this.Height;
            pcc.Width = this.Width;
            pcc.Visible = this.Visible;
            pcc.chart1.BorderlineColor = this.chart1.BorderlineColor;
            pcc.chart1.BorderlineDashStyle = this.chart1.BorderlineDashStyle;
            pcc.chart1.BorderlineWidth = this.chart1.BorderlineWidth;
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

        public float HorizontalScale { get; set; }
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
        public void InitailColumnData()
        {
            try
            {
                if (this._sqlSource == null)
                    return;
                if (this._sqlSource.PMSChartAppearance != null)
                {
                    this._sqlSource.PMSChartAppearance.SetChart(chart1);
                }

                InitialChartArea();
                InitialLegend();
                InitialTitle();
                if (_isReport)
                {
                    ResetReportChart();
                }
                else
                {
                    ResetChart();
                }
                #region old
                    /*/
                
                if (this.DesignMode == true)
                {
                    chart1.Series.Clear();
                    Series series1 = new Series();
                    series1.Name = "series1";
                    series1.ChartArea = chart1.ChartAreas[0].Name;
                    chart1.Series.Add(series1);
                    Random random = new Random();
                    for (int i = 0; i < 8; i++)
                        series1.Points.Add(10.0 * random.NextDouble());

                    //series1.Legend = legend1.Name;
                    if (_pMSSeriesInfo != null)
                        _pMSSeriesInfo.SetSeriesValue(series1);
                }
                else
                {
                chart1.Series.Clear();
                string sqlText = "";
                if (_sqlSource.UsingConfig)
                {
                    sqlText = PublicFunctionClass.GetConfigSql(_sqlSource,true);
                }
                else
                {
                    sqlText = _sqlSource.ResultSql;
                }
                List<PmsField> selectFieldList = new List<PmsField>();
                List<string> groupByList = new List<string>();

                PublicFunctionClass.getSelectedField(sqlText, selectFieldList, groupByList);

                if (selectFieldList.Count == 0)//无选择字段
                {
                    MessageBox.Show("无选择字段！");
                    return;
                }

                SqlConnection _SqlConnection1 = SqlStructure.GetSqlConncetion();
                if (_SqlConnection1 == null || _SqlConnection1.State != ConnectionState.Open)
                {
                    MessageBox.Show("连接错误！");
                    return;
                }
                SqlCommand thisCommand = _SqlConnection1.CreateCommand();
                thisCommand.CommandText = sqlText;

                //PMS.Libraries.ToolControls.PMSPublicInfo.Message.Info(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, thisCommand.CommandText, false);
                SqlDataReader thisReader = null;
                try
                {
                    thisReader = thisCommand.ExecuteReader();
                    thisCommand.Dispose();
                }
                catch (Exception ec)
                {
                    MessageBox.Show(ec.Message);
                    thisCommand.Dispose();
                    thisReader.Close();
                    return;
                }
                DataTable dt = new DataTable();
                dt.Load(thisReader);
                thisReader.Close();
                int nSeries = _sqlSource.YAixs.Count;
                if (nSeries <= 0)
                {
                    MessageBox.Show("未绑定Y轴！");
                    return;
                }
                InitailColumnChartArea(_sqlSource.YAixs);

                if (groupByList.Count < 2 )
                {
                    #region 单分组以下                    
                    
                    string xName = "";
                    try
                    {
                        xName = _sqlSource.XAixs;
                        
                        for (int i = 0; i < nSeries; i++)
                        {
                            string yName = _sqlSource.YAixs[i];
                            Series series1 = new Series();
                            series1.Name = yName;
                            series1.ChartArea = "ChartArea1";                                
                            chart1.Series.Add(series1);
                            int point = 0;
                            foreach (DataRow cateRow in dt.Rows)
                            {
                                series1.Points.AddXY(cateRow[0], cateRow[i+1]);
                                series1.Points[point].BorderWidth = 5;
                                series1.Points[point].ToolTip = cateRow[0].ToString();// +"-" + cateRow[i + 1].ToString();
                                point++;
                            }
                            series1.XValueType = ChartValueType.DateTime;
                            series1.IsValueShownAsLabel = true;
                            chart1.ChartAreas[series1.ChartArea].AxisX.LabelStyle.Format = "MMddHH";//ddd,MMM dd
                            //chart1.ChartAreas[series1.ChartArea].AxisX.IsLabelAutoFit = true;

                            chart1.ChartAreas[series1.ChartArea].AxisX.Interval = 1;
                            chart1.ChartAreas[series1.ChartArea].AxisX.IntervalType = DateTimeIntervalType.Days;
                            chart1.ChartAreas[series1.ChartArea].AxisX.IntervalOffset = 3;
                            chart1.ChartAreas[series1.ChartArea].AxisX.IntervalOffsetType = DateTimeIntervalType.Hours;
                        }                        
                    }
                    catch
                    {
                        thisReader.Close();
                        return;
                    }
                    
                    #endregion
                }
                else if (groupByList.Count == 2)//暂时考虑单表？
                {
                    #region datasource
                    string secondGroupSql = getSimpleSql(groupByList[1]);
                    string mainGroupSql = getSimpleSql(groupByList[0]);

                    SqlCommand thisCommandSecond = _SqlConnection1.CreateCommand();
                    SqlCommand thisCommandMain = _SqlConnection1.CreateCommand();
                    thisCommandSecond.CommandText = secondGroupSql;
                    thisCommandMain.CommandText = mainGroupSql;

                    //PMS.Libraries.ToolControls.PMSPublicInfo.Message.Info(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, thisCommand.CommandText, false);
                    SqlDataReader SecondReader = null;
                    SqlDataReader MainReader = null;
                    try
                    {
                        SecondReader = thisCommandSecond.ExecuteReader();
                        thisCommandSecond.Dispose();
                    }
                    catch (Exception ec)
                    {
                        MessageBox.Show(ec.Message);
                        thisCommandSecond.Dispose();
                        SecondReader.Close();
                        return;
                    }
                    DataTable dtSecond = new DataTable();
                    dtSecond.Load(SecondReader);
                    SecondReader.Close();
                    try
                    {
                        MainReader = thisCommandMain.ExecuteReader();
                        thisCommandMain.Dispose();
                    }
                    catch (Exception ec)
                    {
                        MessageBox.Show(ec.Message);
                        thisCommandMain.Dispose();
                        MainReader.Close();
                        return;
                    }
                    DataTable dtMain = new DataTable();
                    dtMain.Load(MainReader);
                    MainReader.Close(); 
                    #endregion
                  
                    #region series
                    
                    string mainColumnName = dt.Columns[0].ColumnName;
                    string secondColumn = dt.Columns[1].ColumnName;
                    
                    try
                    {
                        string mainType = dt.Columns[0].DataType.Name;
                        mainType = PublicFunctionClass.CSTypeToPmsDateType(mainType);

                        string secondType = dt.Columns[1].DataType.Name;
                        secondType = PublicFunctionClass.CSTypeToPmsDateType(secondType);

                        for (int i = 0; i < nSeries; i++)//Y轴个数
                        {
                            string yName = _sqlSource.YAixs[i];
                            foreach (DataRow secondRow in dtSecond.Rows)//第二分组个数
                            {
                                Series series = new Series();
                                series.Name = yName + "(" + _sqlSource.SecondaryGroupBy+"="+secondRow[0].ToString()+")";
                                series.ChartArea ="ChartArea1";
                                chart1.Series.Add(series);
                                foreach (DataRow mainRow in dtMain.Rows)//第一分组个数
                                {
                                    string filter = mainColumnName+PublicFunctionClass.GetValue(mainType, mainRow[0].ToString(), 1, 0);
                                    filter += " and " + secondColumn + PublicFunctionClass.GetValue(secondType, secondRow[0].ToString(), 1, 0);

                                    DataRow[] drc = dt.Select(filter);

                                    if (drc.Length == 0)
                                    {
                                        series.Points.AddXY(mainRow[0], 0);
                                    }
                                    else if (drc.Length == 1)
                                    {
                                        DataRow drData = drc[0];
                                        series.Points.AddXY(mainRow[0], drData[i + 2]);
                                    }
                                }
                            }
                        }
                    }
                    catch 
                    { 
                    }
                    #endregion

                    SecondReader.Close();
                    MainReader.Close();
                    
                }/*/
                    #endregion
            }
            catch (Exception ee)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, ee.Message, false);
            }
        }

        public IControlTranslator ToElement(bool transferChild)
        {
            ChartSerializerClass result = new ChartSerializerClass();
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

        DataTable dt = new DataTable();
        DataTable dtMain = new DataTable();
        DataTable dtSecond = new DataTable();
        private bool ResetDataSource(string sql, List<string> groupByList2)
        {
            dt.Clear();
            dt.Columns.Clear();
            dtMain.Clear();
            dtSecond.Clear();

            #region data
            //PMSDBConnection _SqlConnection1 = PMSDBConnection.GetConnection();
            //if (_SqlConnection1 == null || _SqlConnection1.State != ConnectionState.Open)
            //{
            //    MessageBox.Show("连接错误！");
            //    return false;
            //}
            //SqlCommand thisCommand = _SqlConnection1.CreateCommand();
            //thisCommand.CommandText = sql;

            //SqlDataReader thisReader = null;
            //try
            //{
            //    thisReader = thisCommand.ExecuteReader();
            //    thisCommand.Dispose();
            //}
            //catch (Exception ec)
            //{
            //    MessageBox.Show(ec.Message);
            //    thisCommand.Dispose();
            //    return false;
            //}
            //dt.Load(thisReader);
            //thisReader.Close();
            dt = PMSDBConnection.ExecuteCommand(sql);

            if (groupByList2.Count == 2)
            {
                #region datasource
                string secondGroupSql = this.getSimpleSql(groupByList2[1]);
                string mainGroupSql = this.getSimpleSql(groupByList2[0]);
                dtSecond = PMSDBConnection.ExecuteCommand(secondGroupSql);
                dtMain = PMSDBConnection.ExecuteCommand(mainGroupSql);
                //SqlCommand thisCommandSecond = _SqlConnection1.CreateCommand();
                //SqlCommand thisCommandMain = _SqlConnection1.CreateCommand();
                //thisCommandSecond.CommandText = secondGroupSql;
                //thisCommandMain.CommandText = mainGroupSql;

                ////PMS.Libraries.ToolControls.PMSPublicInfo.Message.Info(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, thisCommand.CommandText, false);
                //SqlDataReader SecondReader = null;
                //SqlDataReader MainReader = null;
                //try
                //{
                //    SecondReader = thisCommandSecond.ExecuteReader();
                //    thisCommandSecond.Dispose();
                //}
                //catch (Exception ec)
                //{
                //    MessageBox.Show(ec.Message);
                //    thisCommandSecond.Dispose();
                //    return false;
                //}
                //dtSecond.Load(SecondReader);
                //SecondReader.Close();
                //try
                //{
                //    MainReader = thisCommandMain.ExecuteReader();
                //    thisCommandMain.Dispose();
                //}
                //catch (Exception ec)
                //{
                //    MessageBox.Show(ec.Message);
                //    thisCommandMain.Dispose();
                //    return false;
                //}
                //dtMain.Load(MainReader);
                //MainReader.Close();
                #endregion

            }
            return true;
            #endregion
        }

        private void InitialChartArea()
        {
            chart1.ChartAreas.Clear();
            foreach (PMSChartArea pca in this._sqlSource.ChartAreaList)
            {
                ChartArea ca = new ChartArea(pca.Name);
                if (pca is CurveY)
                {
                    CurveY temp = pca as CurveY;
                    if (temp != null && temp.IsCopyChart == true)
                    {
                        if (this._sqlSource.YaxisAndCopyChart.ContainsValue(temp.Name))
                        {
                            string key = "";
                            foreach (KeyValuePair<string, string> item in this._sqlSource.YaxisAndCopyChart)
                            {
                                if (item.Value == temp.Name)
                                {
                                    key = item.Key;
                                    break;
                                }
                            }
                            PMSChartArea aa = GetChartAreaByName(key);
                            if (aa != null)
                            {
                                temp.AxisY.Maximum = aa.AxisY.Maximum;
                                temp.AxisY.Minimum = aa.AxisY.Minimum;
                                temp.AxisY.LabelStyle.Enabled = false;
                                temp.AxisY.MajorTickMark.Enabled = false;
                            }
                        }
                        temp.AxisY.LineColor = Color.Transparent;
                    }
                }
                pca.SetChartArea(ca);
                chart1.ChartAreas.Add(ca);
            }
            if (chart1.ChartAreas.Count == 0)
            {
                ChartArea ca = new ChartArea("chartArea1");
                if (_ChartType == 0)
                {
                    ca.Area3DStyle.Enable3D = true;
                }
                else
                {
                    ca.Area3DStyle.Enable3D = false;
                }
                chart1.ChartAreas.Add(ca);
            }
        }

        #region 2011.12.28 增加
        /// <summary>
        /// 2011.12.28 增加
        /// 目的:根据名字从区域集合中获取区域
        /// </summary>
        /// <param name="name">区域名字</param>
        /// <returns>返回查询结果</returns>
        private PMSChartArea GetChartAreaByName(string name)
        {
            PMSChartArea result = null;
            if (this != null && this._sqlSource != null && this._sqlSource.ChartAreaList != null)
            {
                foreach (PMSChartArea pca in this._sqlSource.ChartAreaList)
                {
                    if (pca.Name == name)
                    {
                        result = pca;
                        break;
                    }
                }
            }
            return result;
        }
        #endregion

        private void InitialLegend()
        {
            if (this._sqlSource.LegendList == null)
                return;
            chart1.Legends.Clear();
            foreach (PMSLegend pca in this._sqlSource.LegendList)
            {
                Legend ca = new Legend(pca.Name);
                pca.SetLegend(ca);
                ca.DockedToChartArea = FindChartArea(pca.Name, 2);
                chart1.Legends.Add(ca);
            }
        }

        //初始化标题，在初始化区域之后调用
        private void InitialTitle()
        {
            if (this._sqlSource.TitleList == null)
                return;
            chart1.Titles.Clear();
            foreach (PMSTitle pca in this._sqlSource.TitleList)
            {
                Title ca = new Title(pca.Name);
                pca.SetTitle(ca);
                ca.DockedToChartArea = FindChartArea(pca.Name, 1);
                chart1.Titles.Add(ca);
            }
        }

        /// <summary>
        /// 根据名称查找chartarea
        /// </summary>
        /// <param name="seriesName"></param>
        /// <param name="type">0，series名；1，title名；2，legend名</param>
        /// <returns></returns>
        private string FindChartArea(string seriesName,int type)
        {
            try
            {
                List<string> seriesNameList = null;

                foreach (PMSChartArea pca in this._sqlSource.ChartAreaList)
                {
                    if (!(pca is CurveY)||((pca is CurveY) &&(pca as CurveY).IsCopyChart==true))
                    {
                        if (type == 0)
                            seriesNameList = (List<string>)(pca.SeriesDataList);
                        else if (type == 1)
                            seriesNameList = (List<string>)(pca.TitleDataList);
                        else if (type == 2)
                            seriesNameList = (List<string>)(pca.LegendDataList);
                        else
                            return "NotSet";
                        bool bFind = false;
                        foreach (string seriesName1 in seriesNameList)
                        {
                            if (seriesName == seriesName1)
                            {
                                bFind = true;
                                break;
                            }
                        }
                        if (bFind == true)
                        {
                            return pca.Name;
                        }
                    }
                }
            }
            catch
            {
            }
            if (type == 0)
                return this._sqlSource.ChartAreaList[0].Name;
            else
                return "NotSet";            
        }
        /// <summary>
        /// 2011.11.02 增加根据名字找非第一Y轴的Y轴区域
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        private string FindCurveArea(string Name)
        {
            string result = "NotSet";
            List<string> seriesNameList = null;
            foreach (PMSChartArea pca in this._sqlSource.ChartAreaList)
            {
                if (pca is CurveY && (pca as CurveY).IsCopyChart==false)
                {
                        seriesNameList = (List<string>)(pca.SeriesDataList);

                    bool bFind = false;
                    foreach (string seriesName1 in seriesNameList)
                    {
                        if (Name == seriesName1)
                        {
                            bFind = true;
                            break;
                        }
                    }
                    if (bFind == true)
                    {
                        return pca.Name;
                    }
                }
            }
            return result;
        }
        private PMSSeries FindSeriesAppearance(string seriesName)
        {
            try
            {
                foreach (PMSSeries pca in this._sqlSource.SeriesList)
                {
                    bool bFind = false;
                    List<string> seriesNameList = (List<string>)(pca.SeriesDataList);
                    foreach (string seriesName1 in seriesNameList)
                    {
                        if (seriesName == seriesName1)
                        {
                            bFind = true;
                            break;
                        }
                    }
                    if (bFind == true)
                    {
                        return pca;
                    }
                }
            }
            catch
            {
            }
            return null;
        }
        private PMSLegend FindSeriesLegend(string seriesName)
        {
            try
            {
                foreach (PMSLegend pca in this._sqlSource.LegendList)
                {
                    bool bFind = false;
                    List<string> seriesNameList = (List<string>)(pca.SeriesDataList);
                    foreach (string seriesName1 in seriesNameList)
                    {
                        if (seriesName == seriesName1)
                        {
                            bFind = true;
                            break;
                        }
                    }
                    if (bFind == true)
                    {
                        return pca;
                    }
                }
            }
            catch
            {
            }
            return null;
        }

        private bool DrawVirtualChart(int nSeries)
        {
            if (nSeries == 0)//无选择字段
            {
                chart1.Series.Clear();
                int count = 0;
                foreach (ChartArea ca in chart1.ChartAreas)
                {
                    Series series1 = new Series();
                    series1.Name = "series" + count.ToString();
                    series1.ChartArea = chart1.ChartAreas[count].Name;
                    //2012.05.3 修改
                    //目的:虚画的时候也要将新加入的分段属性考虑进去
                    PMSChartArea areatemp = GetChartAreaByName(series1.ChartArea);
                    if (areatemp != null && areatemp.AxisX != null && areatemp.AxisX.FixPoint > 0 && chart1 != null && chart1.ChartAreas[series1.ChartArea] != null)
                    {
                        chart1.ChartAreas[series1.ChartArea].AxisX.Interval = 10/areatemp.AxisX.FixPoint;
                    }

                    if (_ChartType == 0)
                    {
                        series1.ChartType = SeriesChartType.Column;
                    }
                    else if (_ChartType == 1)
                    {
                        series1.ChartType = SeriesChartType.Line;
                    }
                    //11.30 李琦 添加对应雷达图的默认外观
                    else if (_ChartType == 2) 
                    {
                        series1.ChartType = SeriesChartType.Radar;
                    }
                    count++;
                    chart1.Series.Add(series1);
                    Random random = new Random();
                    for (int i = 0; i < 10; i++)
                        series1.Points.Add(10.0 * random.NextDouble());
                    if (ca.AxisX.LineWidth == 0)
                    {
                        series1.IsVisibleInLegend = false;
                        series1.Color = Color.Transparent;
                        series1.BorderColor = Color.Transparent;
                    }
                }
                return false;
            }
            else
            {
                chart1.Series.Clear();
                for (int i = 0; i < nSeries; i++)
                {
                    string yName = _sqlSource.YAixs[i];
                    Series series1 = new Series();
                    series1.Name = yName;

                    series1.ChartArea = FindChartArea(yName, 0);
                    //2012.05.3 修改
                    //目的:虚画的时候也要将新加入的分段属性考虑进去
                    PMSChartArea areatemp = GetChartAreaByName(series1.ChartArea);
                    if (areatemp != null && areatemp.AxisX != null && areatemp.AxisX.FixPoint > 0 && chart1 != null && chart1.ChartAreas[series1.ChartArea] != null)
                    {
                                chart1.ChartAreas[series1.ChartArea].AxisX.Interval = 10 / areatemp.AxisX.FixPoint;
                    }

                    PMSSeries ps = FindSeriesAppearance(yName);
                    if (ps != null)
                        ps.SetSeriesValue(series1);

                    PMSLegend pl = FindSeriesLegend(yName);
                    if (pl != null)
                    {
                        series1.Legend = pl.Name;
                        pl.DockedToChartArea = FindChartArea(yName, 2);
                    }

                    chart1.Series.Add(series1);
                    Random random = new Random(i);
                    for (int j = 0; j < 10;j++)
                        series1.Points.Add(10.0 * random.NextDouble());
                }
                DealWithMultiYaxis();
                DrawVirtualITrend();
                return true;
            }
        }
        /// <summary>
        /// 2011.11.02 增加
        /// 在不影响以前逻辑的情况下 增加对多Y轴的处理
        /// </summary>
        private void DealWithMultiYaxis()
        {
            if (_sqlSource.MultiYaxis != null)
            {
                for (int i = 0; i < _sqlSource.MultiYaxis.Count; i++)
                {
                    string strtemp = _sqlSource.MultiYaxis[i];
                    Series seriesCopy = new Series();
                    seriesCopy.Name = strtemp+i.ToString();
                    seriesCopy.ChartArea = FindCurveArea(strtemp);
                    seriesCopy.IsVisibleInLegend = false;
                    seriesCopy.Color = Color.Transparent;
                    seriesCopy.BorderColor = Color.Transparent;
                    chart1.Series.Add(seriesCopy);
                    Random random = new Random();
                    for (int j = 0; j < 10; j++)
                        seriesCopy.Points.Add(20.0 * random.NextDouble());
                }
            }
        }
        private bool ResetChart()
        {
            List<string> selectFieldList1 = this._sqlSource.YAixs;
            int nSeries = selectFieldList1.Count;

            List<string> groupByList1 = PublicFunctionClass.GetGroupList(this._sqlSource.MainGroupBy, this._sqlSource.SecondaryGroupBy);

            if (this.DesignMode == true)
            {
                return DrawVirtualChart(nSeries);
            }
            
            if (!Power.IsValidUserDoAction(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, _actionInfo.ActionList[3]))
            {
                return DrawVirtualChart(nSeries);
            }

            if (nSeries == 0)//无选择字段
            {
                //MessageBox.Show("未选择Y轴!");
                chart1.Series.Clear();
                return false;
            }
            chart1.Series.Clear();

            string sqlNew = PublicFunctionClass.GetConfigSql(this._sqlSource, true);

            //取消活动查询
            if (sqlNew == "Query Canceled!")
            {
                return false;
            }
            if (sqlNew == "")
            {
                return DrawVirtualChart(nSeries);
            }            
            
            bool b = ResetDataSource(sqlNew, groupByList1);

            if (!b)
            {
                return DrawVirtualChart(nSeries);
            }

            if (dt.Rows.Count == 0)
            {
                return false;
            }
            if (groupByList1.Count < 2)
            {
                #region 单分组以下

                string xName = "";
                try
                {
                    xName = _sqlSource.XAixs;

                    for (int i = 0; i < nSeries; i++)
                    {
                        string yName = _sqlSource.YAixs[i];
                        Series series1 = new Series();
                        series1.Name = yName;

                        series1.ChartArea = FindChartArea(yName,0);

                        PMSSeries ps = FindSeriesAppearance(yName);
                        if (ps != null)
                            ps.SetSeriesValue(series1);

                        PMSLegend pl = FindSeriesLegend(yName);
                        if (pl != null)
                            series1.Legend = pl.Name;

                        chart1.Series.Add(series1);

                        if (groupByList1.Count == 0)
                        {
                            int point = 0;
                            string source = GetCleanName(xName);
                            string ysource = GetCleanName(yName);
                            try
                            {
                                foreach (DataRow cateRow in dt.Rows)
                                {
                                    series1.Points.AddXY(cateRow[source], cateRow[ysource]);
                                    series1.Points[point].ToolTip = cateRow[0].ToString();
                                    point++;
                                }
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            int point = 0;
                            foreach (DataRow cateRow in dt.Rows)
                            {
                                series1.Points.AddXY(cateRow[0], cateRow[i + 1]);
                                series1.Points[point].ToolTip = cateRow[0].ToString();
                                point++;
                            }
                        }
                    }
                }
                catch
                {
                    return false;
                }

                #endregion
            }
            else if (groupByList1.Count == 2)//暂时考虑单表？
            {
                #region series

                string mainColumnName = dt.Columns[0].ColumnName;
                string secondColumn = dt.Columns[1].ColumnName;

                try
                {
                    string mainType = dt.Columns[0].DataType.Name;
                    mainType = PublicFunctionClass.CSTypeToPmsDateType(mainType);

                    string secondType = dt.Columns[1].DataType.Name;
                    secondType = PublicFunctionClass.CSTypeToPmsDateType(secondType);

                    for (int i = 0; i < nSeries; i++)//Y轴个数
                    {
                        string yName = _sqlSource.YAixs[i];
                        foreach (DataRow secondRow in dtSecond.Rows)//第二分组个数
                        {
                            Series series = new Series();
                            series.Name = yName + "_" + secondRow[0].ToString();

                            series.ChartArea = FindChartArea(series.Name,0);
                            PMSSeries ps = FindSeriesAppearance(series.Name);
                            if (ps != null)
                                ps.SetSeriesValue(series);

                            PMSLegend pl = FindSeriesLegend(series.Name);
                            if (pl != null)
                                series.Legend = pl.Name;

                            chart1.Series.Add(series);
                            foreach (DataRow mainRow in dtMain.Rows)//第一分组个数
                            {
                                string filter = mainColumnName + PublicFunctionClass.GetValue(mainType, mainRow[0].ToString(), 1, 0);
                                filter += " and " + secondColumn + PublicFunctionClass.GetValue(secondType, secondRow[0].ToString(), 1, 0);

                                DataRow[] drc = dt.Select(filter);

                                if (drc.Length == 0)
                                {
                                    series.Points.AddXY(mainRow[0], 0);
                                }
                                else if (drc.Length == 1)
                                {
                                    DataRow drData = drc[0];
                                    series.Points.AddXY(mainRow[0], drData[i + 2]);
                                }
                            }
                        }
                    }
                }
                catch
                {
                }
                #endregion
            }
            return true;
        }
        private string GetCleanName(string source)
        {
            if (string.IsNullOrEmpty(source))
                return "";
            if (source.Length < 2)
                return source;
            if(source[0] == '[' && source[source.Length - 1] == ']')
            {
                return source.Substring(1, source.Length - 2);
            }
            return source;
        }
        private bool ResetReportChart()
        {
            int ii = this._sqlSource.YAixs.Count;
            if (this.DesignMode == true)
            {
                //return DrawVirtualChart(0);
                return DrawVirtualChart(ii);
            }
            
            if (!IsReport&&!Power.IsValidUserDoAction(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, _actionInfo.ActionList[3]))
            {
                //return DrawVirtualChart(0);
                return DrawVirtualChart(ii);
            }

            //if (nSeries == 0)//无选择字段
            //{
            //    MessageBox.Show("未选择Y轴!");
            //    return false;
            //}
            chart1.Series.Clear();
            chart1.Annotations.Clear();

            if (_ReportData == null || _ReportData.Rows.Count == 0)
            {
                return false;
            }
            if (SelectRecordFields == null || SelectRecordFields.Count==0)// || string.IsNullOrEmpty(_xRecordField))
            {
                return false;
            }
            int nSeries = SelectRecordFields.Count;
            {
                #region 单分组以下

                string xName = "";
                try
                {
                    xName = _xRecordField;

                    for (int i = 0; i < nSeries; i++)
                    {
                        string yName = SelectRecordFields[i];
                        Series series1 = new Series();
                        series1.Name = yName;

                        //series1.ChartArea = chart1.ChartAreas[0].Name;

                        series1.ChartArea = FindChartArea(yName,0);
                        PMSSeries ps = FindSeriesAppearance(yName);
                        if (ps != null)
                            ps.SetSeriesValue(series1);

                        PMSLegend pl = FindSeriesLegend(yName);
                        if (pl != null)
                            series1.Legend = pl.Name;

                        chart1.Series.Add(series1);

                        int point = 0;
                        if (string.IsNullOrEmpty(_xRecordField))
                        {
                            foreach (DataRow cateRow in _ReportData.Rows)
                            {
                                series1.Points.AddY( cateRow[yName].ToString());
                                //series1.Points[point].ToolTip = cateRow[0].ToString();
                                point++;
                            }
                        }
                        else
                        {
                            if (_ReportData != null)
                            {
                                DataView tempdata = _ReportData.DefaultView;
                                tempdata.Sort = _xRecordField + " " + MESSortType.ASC.ToString();
                                _ReportData = tempdata.ToTable();
                            }
                            //2011.12.31 修改
                            //目的:判定主区域的X轴是否启用了格式化,如果没有起用格式化
                            //默认以字符串的方式添加坐标点,否则以object方式添加便于格式化
                            //2012.04.24 修改
                            //目的:根据配置以及实际数据自动计算轴的间隔
                            PMSChartArea areatemp = GetChartAreaByName(series1.ChartArea);
                            if (areatemp != null && areatemp.AxisX != null && areatemp.AxisX.FixPoint > 0 && _ReportData.Rows.Count>0 && chart1!=null &&chart1.ChartAreas[series1.ChartArea]!=null)
                            {
                                if (_ReportData.Columns.Contains(_xRecordField))
                                {
                                    Type bb = _ReportData.Columns[_xRecordField].DataType;
                                    if (bb == typeof(System.DateTime))
                                    {
                                        chart1.ChartAreas[series1.ChartArea].AxisX.Interval = _ReportData.Rows.Count / areatemp.AxisX.FixPoint;
                                    }
                                }
                            }
                            if (areatemp != null && areatemp.AxisX != null && areatemp.AxisX.LabelStyle != null && string.IsNullOrEmpty(areatemp.AxisX.LabelStyle.Format))
                            {
                                foreach (DataRow cateRow in _ReportData.Rows)
                                {
                                    series1.Points.AddXY(cateRow[_xRecordField], cateRow[yName]);
                                    point++;
                                }
                            }
                            else
                            {
                                foreach (DataRow cateRow in _ReportData.Rows)
                                {
                                    series1.Points.AddXY(cateRow[_xRecordField], cateRow[yName]);
                                    point++;
                                }
                            }
                        }
                    }
                    //***
                    //2011.11.02 增加对多Y轴处理
                    if (_sqlSource.MultiYaxis != null)
                    {
                        for (int i = 0; i < _sqlSource.MultiYaxis.Count; i++)
                        {
                            string strtemp = _sqlSource.MultiYaxis[i];
                            Series seriesCopy = new Series();
                            seriesCopy.Name = strtemp + i.ToString();
                            seriesCopy.ChartArea = FindCurveArea(strtemp);
                            seriesCopy.IsVisibleInLegend = false;
                            seriesCopy.Color = Color.Transparent;
                            seriesCopy.BorderColor = Color.Transparent;
                            chart1.Series.Add(seriesCopy);
                            int point = 0;
                            if (string.IsNullOrEmpty(_xRecordField))
                            {
                                foreach (DataRow cateRow in _ReportData.Rows)
                                {
                                    seriesCopy.Points.AddY(cateRow[strtemp]);
                                    //series1.Points[point].ToolTip = cateRow[0].ToString();
                                    point++;
                                }
                            }
                            else
                            {
                                if (_ReportData != null)
                                {
                                    DataView tempdata = _ReportData.DefaultView;
                                    tempdata.Sort = _xRecordField + " " + MESSortType.ASC.ToString();
                                    _ReportData = tempdata.ToTable();
                                }
                                foreach (DataRow cateRow in _ReportData.Rows)
                                {
                                    seriesCopy.Points.AddXY(cateRow[_xRecordField], cateRow[strtemp]);
                                    point++;
                                }
                            }
                        }
                    }
                    //*****
                    DealWithITrendCurve();
                }
                catch
                {
                    return false;
                }

                #endregion
            }
            return true;
        }
        #endregion
        
        public string getSimpleSql(string field)
        {
            string simpleSql = "";
            if (_sqlSource.SelectedTableList.Count == 1)
            {
                simpleSql = "select distinct " + field + " from " + _sqlSource.SelectedTableList[0];
            }
            else
            {
                int point = field.IndexOf("].[");
                simpleSql = "select distinct ";

                if (point > 0)
                {
                    simpleSql += field.Substring(point + 2, field.Length - (point + 2));
                    simpleSql += " from " + field.Substring(point + 1);
                }
                else
                {
                    int point1 = field.IndexOf(".");
                    if (point1 > 0)
                    {
                        simpleSql += field.Substring(point + 1, field.Length - (point + 1));
                        simpleSql += " from " + field.Substring(point);
                    }
                    else//error
                    {
                        MessageBox.Show("多表字段部分错误！");
                        return "";
                    }
                }
            }
            return simpleSql;
        }
        private void PMSChartCtrl_Load(object sender, EventArgs e)
        {
            //if (_SourceField != null)
            //{
            //    FieldTreeViewData sfAll = (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetCurrentReportDataDefine(this)) as FieldTreeViewData;
            //    if (sfAll != null)
            //    {
            //        if (_recordField == null)
            //            _recordField = new List<string>();

            //        if (_allRecordField == null)
            //            _allRecordField = new List<string>();
            //        _recordField.Clear();
            //        _allRecordField.Clear();
            //        List<PmsField> lp = new List<PmsField>();

            //        List<SourceField> lpdb = _SourceField.GetSubSourceField(sfAll);
            //        foreach (SourceField pdb in lpdb)
            //        {
            //            try
            //            {
            //                if (!string.IsNullOrEmpty(pdb.DataType))
            //                {
            //                    string typ = pdb.DataType.ToUpper();
            //                    if (typ.Equals("INT", StringComparison.InvariantCultureIgnoreCase) ||
            //                        typ.Equals("FLOAT", StringComparison.InvariantCultureIgnoreCase) ||
            //                        typ.Equals("REAL", StringComparison.InvariantCultureIgnoreCase) ||
            //                        typ.Equals("INT32", StringComparison.InvariantCultureIgnoreCase) ||
            //                        typ.Equals("INT16", StringComparison.InvariantCultureIgnoreCase) ||
            //                        typ.Equals("INT64", StringComparison.InvariantCultureIgnoreCase) ||
            //                        typ.Equals("SYSTEM.SINGLE", StringComparison.InvariantCultureIgnoreCase) ||
            //                        typ.Equals("SYSTEM.DOUBLE", StringComparison.InvariantCultureIgnoreCase) ||
            //                        typ.Equals("SYSTEM.INT32", StringComparison.InvariantCultureIgnoreCase) ||
            //                        typ.Equals("SYSTEM.DECIMAL", StringComparison.InvariantCultureIgnoreCase))
            //                    {
            //                        _recordField.Add(pdb.RecordField);
            //                        _allRecordField.Add(pdb.RecordField);
            //                    }
            //                }
            //            }
            //            catch
            //            {
            //            }
            //        }
            //    }
            //}
            if (_sqlSource == null)
            {
                _sqlSource = new DataSource(null);
                InitailColumnData();
            }

           
        }

        private void toolStripButtonAppearance_Click(object sender, EventArgs e)
        {
            FormAreaFeature faf = new FormAreaFeature();
            faf.ChartSerials = chart1.ChartAreas;
            if (faf.ShowDialog(this) == DialogResult.OK)
            {
            }
        }

        //另存为图片
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (!Power.IsValidUserDoAction(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, _actionInfo.ActionList[0]))
            {
                return;
            }
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            // Sets the current file name filter string, which determines 
            // the choices that appear in the "Save as file type" or 
            // "Files of type" box in the dialog box.
            saveFileDialog1.Filter = "Bitmap (*.bmp)|*.bmp|JPEG (*.jpg)|*.jpg|EMF (*.emf)|*.emf|PNG (*.png)|*.png|GIF (*.gif)|*.gif|TIFF (*.tif)|*.tif";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            // Set image file format
            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                ChartImageFormat format = ChartImageFormat.Bmp;

                if (saveFileDialog1.FileName.EndsWith("bmp"))
                {
                    format = ChartImageFormat.Bmp;
                }
                else if (saveFileDialog1.FileName.EndsWith("jpg"))
                {
                    format = ChartImageFormat.Jpeg;
                }
                else if (saveFileDialog1.FileName.EndsWith("emf"))
                {
                    format = ChartImageFormat.Emf;
                }
                else if (saveFileDialog1.FileName.EndsWith("gif"))
                {
                    format = ChartImageFormat.Gif;
                }
                else if (saveFileDialog1.FileName.EndsWith("png"))
                {
                    format = ChartImageFormat.Png;
                }
                else if (saveFileDialog1.FileName.EndsWith("tif"))
                {
                    format = ChartImageFormat.Tiff;
                }

                // Save image
                chart1.SaveImage(saveFileDialog1.FileName, format);
            }
            
        }

        private void toolStripSaveClipBoard_Click(object sender, EventArgs e)
        {
            // create a memory stream to save the chart image    
            System.IO.MemoryStream stream = new System.IO.MemoryStream();

            // save the chart image to the stream    
            chart1.SaveImage(stream, System.Drawing.Imaging.ImageFormat.Bmp);

            // create a bitmap using the stream    
            Bitmap bmp = new Bitmap(stream);

            // save the bitmap to the clipboard    
            Clipboard.SetDataObject(bmp); 

        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            if (!Power.IsValidUserDoAction(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, _actionInfo.ActionList[2]))
            {
                return;
            }
            try
            {
                chart1.Printing.PrintPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "统计图打印预览信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);                
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (!Power.IsValidUserDoAction(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, _actionInfo.ActionList[1]))
            {
                return;
            }
            try
            {
                chart1.Printing.Print(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "统计图打印信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                MessageBox.Show(this, ex.Message, "统计图打印信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ResetChart();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        #region 2012.06.28增加对特殊标签的处理
        private void DealWithITrendCurve()
        {
            try
            {
                //主字段必须为时间类型
                if (_ReportData == null || !_ReportData.Columns.Contains(_xRecordField) || _ReportData.Columns[_xRecordField].DataType != typeof(DateTime))
                {
                    return;
                }
                DateTime mintime = Convert.ToDateTime(_ReportData.Rows[0][_xRecordField]);
                DateTime maxtime = Convert.ToDateTime(_ReportData.Rows[_ReportData.Rows.Count - 1][_xRecordField]);
                TimeSpan totaltime = maxtime - mintime;
                if (this is ITrendCurve)
                {
                    ITrendCurve temp = this as ITrendCurve;
                    if (temp != null)
                    {
                        if (temp.LabelSource != null)
                        {
                            if (temp.LabelSource.Source != null && temp.LabelSource.FieldFlag != null)
                            {
                                FieldLabel stander = null;
                                foreach (FieldLabel label in temp.LabelSource.FieldFlag)
                                {
                                    if (label.IsMainField && label.IsFlag)
                                    {
                                        stander = label;
                                        break;
                                    }
                                }
                                if (chart1.ChartAreas != null && chart1.ChartAreas.Count > 0)
                                {
                                    if (stander != null && temp.LabelSource.Source.Columns.Contains(stander.CurrentField.Name))
                                    {
                                        if (stander.CurrentField.FieldType == MESVarType.MESDateTime)
                                        {
                                            ChartArea Mainarea = chart1.ChartAreas[0];
                                            foreach (System.Data.DataRow row in temp.LabelSource.Source.Rows)
                                            {
                                                TimeSpan timetemp = Convert.ToDateTime(row[stander.CurrentField.Name]) - mintime;
                                                StripLine aa = new StripLine();
                                                aa.IntervalOffsetType = DateTimeIntervalType.Milliseconds;
                                                aa.IntervalType = Mainarea.AxisX.IntervalType;
                                                aa.Interval = 0;
                                                aa.IntervalOffset = timetemp.TotalMilliseconds;
                                                aa.BorderWidth = temp.LabelSource.LineWidth;
                                                aa.BorderColor = temp.LabelSource.LineColor;
                                                Mainarea.AxisX.StripLines.Add(aa);
                                                string str = string.Empty;
                                                foreach (FieldLabel label in temp.LabelSource.FieldFlag)
                                                {//label.IsMainField == false && 
                                                    if (label.IsFlag)
                                                    {
                                                        if (label.IsFlag)
                                                        {
                                                            if (temp.LabelSource.Source.Columns.Contains(label.CurrentField.Name))
                                                            {
                                                                str += string.Format(label.Format, row[label.CurrentField.Name]) + "\r\n";
                                                            }
                                                            else
                                                            {
                                                                str += row[label.CurrentField.Name].ToString() + "\r\n";
                                                            }
                                                        }
                                                    }
                                                    aa.Text = str;
                                                    aa.TextAlignment = temp.LabelSource.TextLineAlignment;
                                                    aa.TextLineAlignment = temp.LabelSource.TextAlignment;
                                                    aa.TextOrientation = temp.LabelSource.TextOrientation;
                                                    aa.ForeColor = temp.LabelSource.LineColor;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            PMS.Libraries.ToolControls.PMSPublicInfo.Message.Warnning(Properties.Resources.ResourceManager.GetString("message0014"));
                                        }
                                    }
                                    else
                                    {
                                        PMS.Libraries.ToolControls.PMSPublicInfo.Message.Warnning(Properties.Resources.ResourceManager.GetString("message0015"));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(Properties.Resources.ResourceManager.GetString("message0013") + ex.Message);
            }
        }
        /// <summary>
        /// 2012.07.03增加
        /// 目的:虚画设计时特征标签
        /// </summary>
        private void DrawVirtualITrend()
        {
             //主字段必须为时间类型
                if (this is ITrendCurve)
                {
                    ITrendCurve temp = this as ITrendCurve;
                    if (temp != null)
                    {
                        if (temp.LabelSource != null)
                        {
                            if ( temp.LabelSource.FieldFlag != null)
                            {
                                FieldLabel stander = null;
                                foreach (FieldLabel label in temp.LabelSource.FieldFlag)
                                {
                                    if (label.IsMainField && label.IsFlag)
                                    {
                                        stander = label;
                                        break;
                                    }
                                }
                                if (chart1.ChartAreas != null && chart1.ChartAreas.Count > 0)
                                {
                                    if (stander != null)
                                    {
                                        if (stander.CurrentField.FieldType == MESVarType.MESDateTime)
                                        {
                                            ChartArea Mainarea = chart1.ChartAreas[0];
                                            StripLine aa = new StripLine();
                                            aa.IntervalOffsetType = DateTimeIntervalType.Auto;
                                            aa.IntervalType = Mainarea.AxisX.IntervalType;
                                            aa.Interval = 0;
                                            aa.IntervalOffset = 5;
                                            aa.BorderWidth = temp.LabelSource.LineWidth;
                                            aa.BorderColor = temp.LabelSource.LineColor;
                                            Mainarea.AxisX.StripLines.Add(aa);
                                            string str = string.Empty;
                                            foreach (FieldLabel label in temp.LabelSource.FieldFlag)
                                            {//label.IsMainField == false &&
                                                if (label.IsFlag)
                                                {
                                                    if (!string.IsNullOrEmpty(label.Format))
                                                    {
                                                        str += string.Format(label.Format, string.Empty) + "\r\n";
                                                    }
                                                    else
                                                    {
                                                        str += label.CurrentField.Name + "\r\n";
                                                    }
                                                }
                                            }
                                            aa.Text = str;
                                            aa.TextAlignment = StringAlignment.Center;
                                            aa.TextLineAlignment = temp.LabelSource.TextAlignment;
                                            aa.TextOrientation = temp.LabelSource.TextOrientation;
                                            aa.ForeColor = temp.LabelSource.LineColor;
                                        }
                                        else
                                        {
                                            PMS.Libraries.ToolControls.PMSPublicInfo.Message.Warnning(Properties.Resources.ResourceManager.GetString("message0014"));
                                        }
                                    }
                                    else
                                    {
                                        PMS.Libraries.ToolControls.PMSPublicInfo.Message.Warnning(Properties.Resources.ResourceManager.GetString("message0015"));
                                    }
                                }
                            }
                        }
                    }
                }
        }
        #endregion

    }
}
