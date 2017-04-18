using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Data;
using PMS.Libraries.ToolControls.PMSPublicInfo;
using System.Xml;
using System.IO;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using System.Runtime.Serialization;
using System.Globalization;

namespace PMS.Libraries.ToolControls.PmsSheet.PmsPublicData
{
    [Serializable]
    public class ReportVar : IDisposable
    {
        #region Public Property

        private List<PMSVar> _PMSVarList;

        [Editor(typeof(PMSVarEditor), typeof(UITypeEditor))]
        [Category("通用")]
        [Description("变量定义信息")]
        [DisplayName("Variables")]
        public List<PMSVar> PMSVarList
        {
            get { return _PMSVarList; }
            set { _PMSVarList = value; }
        }

        private PageData _pageDatal;

        /// <summary>
        /// 详细信息页面布局信息
        /// </summary>
        [Editor(typeof(PageEditor), typeof(UITypeEditor))]
        [Category("通用")]
        [Description("参数信息页面布局")]
        [DisplayName("ParamDialog")]
        public PageData SheetConfig
        {
            get
            {
                return _pageDatal;
            }
            set
            {
                _pageDatal = value;
            }
        }
        #endregion
        public ReportVar()
        {
            if (_PMSVarList == null)
                _PMSVarList = new List<PMSVar>();
            //if (_pageDatal == null)
            //    _pageDatal = new PageData();
        }

        internal class PMSVarEditor : UITypeEditor
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
                        ReportVar cbx = null;
                        if (context.Instance.GetType() == typeof(ReportVar))
                            cbx = (ReportVar)context.Instance;

                        FormReportVar fse = new FormReportVar();

                        fse.PMSVarList = cbx._PMSVarList;
                        if (fse.ShowDialog() == DialogResult.OK)
                        {
                            value = fse.PMSVarList;
                        }
                        return value;
                    }
                }

                return value;
            }
        }

        /// <summary>
        /// 页面编辑配置编辑器
        /// </summary>
        internal class PageEditor : UITypeEditor
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
                        ReportVar control = null;
                        if (context.Instance.GetType() == typeof(ReportVar))
                            control = (ReportVar)context.Instance;
                        else
                            return value;

                        FormPage form1 = new FormPage();

                        form1.customPage1.SheetConfig = control.SheetConfig;

                        if (control.PMSVarList == null || control.PMSVarList.Count == 0)
                        {
                            MessageBox.Show("先进行变量定义！");
                        }
                        List<PmsField> pfl = new List<PmsField>();
                        foreach (PMSVar pv in control.PMSVarList)
                        {
                            PmsField pf = new PmsField();
                            pf.fieldName = pv.VarName;
                            pf.fieldType = PMSVar.GetVarType(pv.VarType);
                            pf.fieldDefault = pv.VarValue;
                            pf.fieldDescription = pv.VarDesc;
                            pfl.Add(pf);
                        }
                        form1.customPage1.pmsFieldList = pfl;

                        if (DialogResult.OK == editorService.ShowDialog(form1))
                        {
                            value = form1.customPage1.SheetConfig;
                        }
                        return value;
                    }
                }

                return value;
            }
        }
        #region 2011.10.10 增加
        /// <summary>
        /// 2011.10.10 增加
        /// 目的:由于ReportVar里面的成员变量有非托管的内容
        /// 需要手动释放
        /// </summary>
        public void Dispose()
        {
            if (this._PMSVarList != null)
            {
                this._PMSVarList.Clear();
            }
        }
        #endregion
    }
    public enum MESTimePart
    {
        //年,
        //月,
        //日,
        //时,
        //分,
        //秒
        Year,
        Month,
        Day,
        Hour,
        Minute,
        Second
    }
    public enum MESSortType
    {
        ASC,
        DESC
    }
    public enum MESSamplingType
    {
        Regularize,//时间节拍
        Drift//数据节拍
    }
    public enum MESSamplingMethods
    {
        Forward,//当没有数据时向前找数据
        Backward,//当没有数据时向后找数据
        Nearest //两个方向都找，但是只取最近的数据
    }
    public enum MESNodeType
    {
        DataSet,
        BasicRecord,
        Aggregate
    }

    public enum MESVarType
    {
        MESString,
        MESInt,
        MESReal,
        MESDateTime,
        MESBool,
        MESGuid,
        MESDecimal,
        MESNodefined
    }

    [Serializable]
    [DataContract]
    public class PMSVar
    {
        public PMSVar()
        {
            ID = System.Guid.NewGuid();
        }

        private string _VarName;

        [DataMember]
        public string VarName
        {
            get { return _VarName; }
            set { _VarName = value; }
        }

        private string _VarDesc;

        [DataMember]
        public string VarDesc
        {
            get { return _VarDesc; }
            set { _VarDesc = value; }
        }
        public System.Guid ID
        {
            get;
            set;
        }
        private MESVarType _VarType;

        [DataMember]
        public MESVarType VarType
        {
            get { return _VarType; }
            set { _VarType = value; }
        }

        private object _VarValue;

        [DataMember]
        public object VarValue
        {
            get { return _VarValue; }
            set { _VarValue = value; }
        }

        public static string GetVarType(MESVarType VarType)
        {
            string typeName = "";
            switch (VarType)
            {
                case MESVarType.MESString:
                    typeName = "VARCHAR(20)";
                    break;
                case MESVarType.MESInt:
                    typeName = "INT";
                    break;
                case MESVarType.MESDateTime:
                    typeName = "DATETIME";
                    break;
                case MESVarType.MESReal:
                    typeName = "REAL";
                    break;
                case MESVarType.MESBool:
                    typeName = "BIT";
                    break;
                case MESVarType.MESGuid:
                    typeName = "GUID";
                    break;
                case MESVarType.MESDecimal:
                    typeName = "DECIMAL";
                    break;
                default:
                    typeName = "";
                    break;
            }
            return typeName;
        }
        /// <summary>
        /// 2011.09.19 增加
        /// 目的:将报表里面的自定义类型转换成c#的类型
        /// </summary>
        /// <param name="VarType">自定义类型</param>
        /// <returns>转换后的类型</returns>
        public static Type GetCSharpType(MESVarType VarType)
        {
            Type typeName = null;
            switch (VarType)
            {
                case MESVarType.MESString:
                    typeName = typeof(string);
                    break;
                case MESVarType.MESInt:
                    typeName = typeof(Int32);
                    break;
                case MESVarType.MESDateTime:
                    typeName = typeof(DateTime);
                    break;
                case MESVarType.MESReal:
                    typeName = typeof(float);
                    break;
                case MESVarType.MESBool:
                    typeName = typeof(bool);
                    break;
                case MESVarType.MESGuid:
                    typeName = typeof(Guid);
                    break;
                case MESVarType.MESDecimal:
                    typeName = typeof(Decimal);
                    break;
                default:
                    typeName = typeof(string);
                    break;
            }
            return typeName;
        }
    }
    internal class LabelConverter : TypeConverter
    {
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] filter)
        {
            return TypeDescriptor.GetProperties(value, filter);
        }
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
    [TypeConverter(typeof(LabelConverter))]
    [Serializable]
    public class ValueMap
    {
        public ValueMap()
        {
            _OriginValue = _ReplaceValue = "";
        }
        public ValueMap(string oldValue, string replaceValue)
        {
            _OriginValue = oldValue;
            _ReplaceValue = replaceValue;
        }
        private string _OriginValue;
        private string _ReplaceValue;
        public string OriginValue
        {
            get { return _OriginValue; }
            set
            {
                _OriginValue = value;
            }
        }
        public string ReplaceValue
        {
            get { return _ReplaceValue; }
            set
            {
                _ReplaceValue = value;
            }
        }
        public ValueMap Clone()
        {
            return new ValueMap(this.OriginValue, this.ReplaceValue);
        }
        public override string ToString()
        {
            return _OriginValue + "->" + _ReplaceValue;
        }
    }

    [Serializable]
    public class SourceField : IDisposable
    {
        #region Public Property

        #region 通用

        private string _Name;

        [Category("通用")]
        [Description("名称")]
        public virtual string Name
        {
            get { return _Name; }
            set
            {
                if (!string.IsNullOrEmpty(_SqlText) && this.ParentNode != null && this.ParentNode.TreeView != null)
                {
                    if (CheckSameAndRelativeLevelName(this.ParentNode, value) == false)
                    {
                        throw new NotImplementedException(PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.Properties.Resources.ResourceManager.GetString("Message0001"));
                    }
                    else
                    {
                        _Name = value;
                    }
                }
                _Name = value;
                if (ParentNode != null)
                {
                    try
                    {
                        ParentNode.Text = _Name;
                    }
                    catch
                    {
                    }
                    if (ParentNode.TreeView != null)
                    {
                        ParentNode.TreeView.Refresh();
                        ParentNode.TreeView.Invalidate();
                    }
                }
            }
        }

        private string _DBString;

        [Category("通用")]
        [Description("数据源")]
        [DisplayName("DataSource")]
        [Editor(typeof(DBSelectionEditor), typeof(UITypeEditor))]
        public virtual string DBSource
        {
            get { return _DBString; }
            set { _DBString = value; }
        }

        //2011.09.22 增加 目的:可以嵌套传递多个变量
        private List<string> _RecordFields;

        //**2011.09.22 增加
        [Description("关联字段配置")]
        [Category("通用")]
        [DisplayName("RelatedFields")]
        [Editor(typeof(FieldCollectionEditer), typeof(UITypeEditor))]
        public virtual List<string> RecordFields
        {
            get { return _RecordFields; }
            set { _RecordFields = value; }
        }
        //**

        [Description("多数据源查询")]
        [Category("通用")]
        public virtual List<DSSqlPair> MultiDataSource
        {
            get;
            set;
        }
        
        [Category("通用")]
        [Description("二次排序,查询至内存后再次排序")]
        public virtual string SecondarySort
        {
            get;
            set;
        }

        private MESVarType _FieldType;

        [Description("字段类型")]
        [Category("通用")]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public virtual MESVarType FieldType
        {
            set
            {
                _FieldType = value;

                if (_Tag == null)
                {
                    switch (_FieldType)
                    {
                        case MESVarType.MESString:
                            _DataType = "VARCHAR";
                            break;
                        case MESVarType.MESInt:
                            _DataType = "INT";
                            break;
                        case MESVarType.MESDateTime:
                            _DataType = "DATETIME";
                            break;
                        case MESVarType.MESReal:
                            _DataType = "REAL";
                            break;
                        case MESVarType.MESBool:
                            _DataType = "BIT";
                            break;
                        case MESVarType.MESDecimal:
                            _DataType = "DECIMAL";
                            break;
                        default:
                            _DataType = "";
                            break;
                    }
                }
            }
            get
            {
                return GetVarTypeFromString(_DataType);
            }
        }

        [Description("Sql语句")]
        [Category("通用")]
        [DisplayName("Sql")]
        [Editor(typeof(SqlEditor), typeof(UITypeEditor))]
        public virtual string SqlText
        {
            get { return _SqlText; }
            set
            {
                if (!string.IsNullOrEmpty(value) && this.ParentNode != null && this.ParentNode.TreeView != null)
                {
                    if (CheckSameAndRelativeLevelName(this.ParentNode, value) == false)
                    //if (CheckSourceFieldName(this.ParentNode.TreeView, _Name) == false)
                    {
                        throw new NotImplementedException(PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.Properties.Resources.ResourceManager.GetString("Message0001"));
                    }
                }
                _SqlText = value;
            }
        }

        #endregion





        #region 抽样

        [Category("抽样")]
        [Description("抽样起始时间")]
        [TypeConverter(typeof(DateTimeConverter))]
        public virtual DateTime SamplingStart
        {
            get { return samplingStart; }
            set { samplingStart = value; }
        }

        [Category("抽样")]
        [Description("抽样结束时间")]
        [TypeConverter(typeof(DateTimeConverter))]
        public virtual DateTime SamplingEnd
        {
            get { return samplingEnd; }
            set { samplingEnd = value; }
        }

        [Category("抽样")]
        [Description("抽样类型(Regularize为时间节拍,Drift为数据节拍")]
        [DefaultValue(MESSamplingType.Drift)]
        public virtual MESSamplingType SamplingType
        {
            get { return samplingType; }

            set 
            {
                MESSamplingType oldSamplingType = samplingType;
                samplingType = value;
                if (SamplingMethods == MESSamplingMethods.Nearest && samplingType == MESSamplingType.Drift)
                {
                    MessageBox.Show("在以Nearest下不能使用以数据为节拍筛选模式!");
                    samplingType = oldSamplingType;
                }
            }
        }
        [Category("抽样")]
        [Description("抽样方式(Forward向前查找,Backward向后查找,Nearest双向查找只取距离最近数据)")]
        [DefaultValue(MESSamplingMethods.Backward)]
        public virtual MESSamplingMethods SamplingMethods
        {
            get { return samplingMethods; }
            set
            {
                MESSamplingMethods oldSamplingMethods = samplingMethods;
                samplingMethods = value;
                if (SamplingType == MESSamplingType.Drift && SamplingMethods == MESSamplingMethods.Nearest)
                {
                    MessageBox.Show("在以数据为节拍筛选模式下不能使用Nearest!");
                    samplingMethods = oldSamplingMethods;
                }

            }
        }


        [Category("抽样")]
        [Description("时间类型抽样字段")]
        public virtual string SamplingField
        {
            get { return samplingField; }
            set { samplingField = value; }
        }

        [Category("抽样")]
        [Description("是否启用抽样")]
        [DisplayName("EnableSampling")]
        [DefaultValue(false)]
        public virtual bool IsSampling
        {
            get { return bSampling; }
            set { bSampling = value; }
        }

        [Category("抽样")]
        [Description("抽样时间间隔单位")]
        [DefaultValue(MESTimePart.Minute)]
        public virtual MESTimePart TimeUnit
        {
            get { return timePart; }
            set { timePart = value; }
        }

        [Category("抽样")]
        [Description("时间间隔")]
        [DefaultValue(30)]
        public virtual int SamplingDistance
        {
            get { return samplingDistance; }
            set { samplingDistance = value; }
        }

        [Category("抽样")]
        [Description("每次间隔抽样条数")]
        [DefaultValue(1)]
        public virtual int SamplingCount
        {
            get { return samplingCount; }
            set { samplingCount = value; }
        }

        #endregion

        #endregion
        public SourceField()
        {
            fieldDataTable = null;
            _FieldType = MESVarType.MESString;
            _ChildModify = true;
            NodeType = MESNodeType.DataSet;
            samplingType = MESSamplingType.Drift;
            samplingMethods = MESSamplingMethods.Backward;
            replaceList = new List<ValueMap>();
            samplingDistance = 30;
            timePart = MESTimePart.Minute;
        }
        public SourceField(string name)
            : this()
        {
            _Name = name;
        }

        public SourceField(string name, string sql)
            : this(name)
        {
            _SqlText = sql;
        }
        private Guid _id;
        private string _SubCode;
        private string _SqlText;
        private string _FormatString;
        private string _RecordField;




        //节点类型
        private MESNodeType nodeType;
        //当为集合节点或单记录节点子节点时，绑定的变量名
        private string varName;

        //多表单控件增加
        private string _TableName;
        //该字段是否只读，暂未启用（2011_3_21）
        private bool _ReadOnly;
        //子表关联字段
        private string _RelationField;
        //是否作为判定标准
        private bool _Primary;
        //父节点关联字段修改后，子节点关联字段同步修改
        private bool _ChildModify;

        //报表抽样过滤
        //是否启用
        private bool bSampling;
        //抽样字段，暂定为时间类型
        private string samplingField;
        //抽样间隔
        private int samplingDistance;
        //抽样间隔单位
        private MESTimePart timePart = MESTimePart.Second;
        //抽样个数
        private int samplingCount = 1;
        //抽样字段排序规则
        private MESSortType sortType;
        //抽样开始时间
        private DateTime samplingStart;
        //抽样结束时间
        private DateTime samplingEnd;
        //抽样类型,固定or浮动
        private MESSamplingType samplingType;
        //抽样方式
        private MESSamplingMethods samplingMethods;
        //报表替换显示
        private List<ValueMap> replaceList;
        //是否查询此数据源
        private bool _BQueryDataSource = true;

        //2012.03.31 数据源路径
        private string _Path = string.Empty;

        //2012.04.1 绑定的当前节点
        private TreeNode _ParentNode = null;


        [Category("报表属性")]
        [Description("值替换列表")]
        [BrowsableAttribute(false)]
        [Editor(typeof(ValueMapEditor), typeof(UITypeEditor))]
        public virtual List<ValueMap> ReplaceList
        {
            get { return replaceList; }
            set { replaceList = value; }
        }

        [Category("通用属性")]
        [Description("是否查询数据集")]
        [DefaultValue(true)]
        public virtual bool BQueryDataSource
        {
            get
            {
                return _BQueryDataSource;
            }
            //set
            //{
            //    _BQueryDataSource = value;
            //}
        }
        [Category("通用属性")]
        [Description("节点类型")]
        [DefaultValue(MESNodeType.DataSet)]
        public virtual MESNodeType NodeType
        {
            get { return nodeType; }
            set { nodeType = value; }
        }
        [Category("通用属性")]
        [Description("当为集合节点或单记录节点子节点时，绑定的变量名")]
        [Browsable(false)]
        public virtual string VarName
        {
            get { return varName; }
            set { varName = value; }
        }

        [Category("多表单属性")]
        [Description("节点关联表名，编辑时有效")]
        public virtual string TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }
        [Category("多表单属性")]
        [Description("以该节点作为删除和修改标准")]
        [DefaultValue(false)]
        public virtual bool Standard
        {
            get { return _Primary; }
            set { _Primary = value; }
        }
        [DefaultValue(true)]
        [Category("多表单属性")]
        [Description("当父表关联字段修改时，子表是否同步修改")]
        public virtual bool IsChildModify
        {
            get { return _ChildModify; }
            set { _ChildModify = value; }
        }

        [DefaultValue(false)]
        [Category("多表单属性")]
        [Description("节点信息是否只读")]
        public virtual bool ReadOnly
        {
            get { return _ReadOnly; }
            set { _ReadOnly = value; }
        }

        [Category("多表单属性")]
        [Description("表节点信息与上层关联的字段名")]
        public virtual string RelationField
        {
            get { return _RelationField; }
            set { _RelationField = value; }
        }



        private object _Tag;
        [NonSerialized]
        private DataTable fieldDataTable;
        private string realSql;

        //refTable,refField

        [BrowsableAttribute(false)]
        public object Tag
        {
            get { return _Tag; }
            set { _Tag = value; }
        }

        [BrowsableAttribute(false)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public DataTable FieldDataTable
        {
            get { return fieldDataTable; }
            set { fieldDataTable = value; }
        }

        [BrowsableAttribute(false)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public string RealSql
        {
            get { return realSql; }
            set { realSql = value; }
        }

        [Description("")]
        [BrowsableAttribute(false)]
        public TreeNode ParentNode
        {
            get
            {
                return _ParentNode;
            }
            set
            {
                _ParentNode = value;
                if (_ParentNode != null)
                {
                    GetPathFormNode(_ParentNode, ref _Path);
                }
            }
        }

        [Description("")]
        [BrowsableAttribute(false)]
        public Guid ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _DataType;

        [DefaultValue(MESVarType.MESString)]
        [BrowsableAttribute(false)]
        public string DataType
        {
            get
            {
                if (_Tag is PMSRefDBFieldProp)
                {
                    _DataType = ((PMSRefDBFieldProp)_Tag).StrFieldType;
                }
                else
                {
                };
                return _DataType;
            }
            set
            {
                _DataType = value;
                _FieldType = GetVarTypeFromString(_DataType);
            }
        }

        [Description("脚本")]
        [Category("通用属性")]
        public virtual string SubCode
        {
            get { return _SubCode; }
            set { _SubCode = value; }
        }

        [Category("通用属性")]
        [Description("排序方式")]
        [Browsable(false)]
        public virtual MESSortType SortType
        {
            get { return sortType; }
            set { sortType = value; }
        }

        [Description("格式化字符串")]
        [Category("通用属性")]
        public virtual string FormatString
        {
            get { return _FormatString; }
            set { _FormatString = value; }
        }

        [Description("记录的字段信息")]
        [Category("通用属性")]
        public virtual string RecordField
        {
            get { return _RecordField; }
            set { _RecordField = value; }
        }

        [Description("记录的字段信息当前绑定的值")]
        [Category("通用属性")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual object RecordFieldCurrentValue
        {
            get;
            set;
        }
        [System.Xml.Serialization.XmlIgnore]
        [Description("记录的字段信息当前绑定的值")]
        [Category("通用属性")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual Dictionary<string, object> RecordFieldsCurrentValue
        {
            get;
            set;
        }

        [Description("记录所有查询过的数据内容")]
        [Category("通用属性")]
        [Browsable(false)]
        public System.Data.DataTable SourceTable
        {
            get;
            set;
        }


        [BrowsableAttribute(false)]
        public string Path
        {
            get
            {
                return _Path;
            }
        }

        private bool _customMode = false;
        [BrowsableAttribute(false)]
        public bool CustomMode
        {
            get { return _customMode; }
            set { _customMode = value; }
        }

        private string _customTablePath = string.Empty;
        [BrowsableAttribute(false)]
        public string CustomTablePath
        {
            get { return _customTablePath; }
            set { _customTablePath = value; }
        }

        private MESVarType GetVarTypeFromString(string dataType)
        {
            MESVarType thisType;
            string type = PmsField.ToPMSDataType(_DataType, 20);
            switch (type)
            {
                case "INT":
                case "INT16":
                case "INT32":
                case "INT64":
                    thisType = MESVarType.MESInt;
                    break;
                case "DATETIME":
                    thisType = MESVarType.MESDateTime;
                    break;
                case "REAL":
                case "FLOAT":
                    thisType = MESVarType.MESReal;
                    break;
                case "BIT":
                    thisType = MESVarType.MESBool;
                    break;
                case "GUID":
                    thisType = MESVarType.MESGuid;
                    break;
                case "DECIMAL":
                    thisType = MESVarType.MESDecimal;
                    break;
                default:
                    thisType = MESVarType.MESNodefined;
                    break;
            }
            if (type.StartsWith("VARCHAR", StringComparison.InvariantCultureIgnoreCase))
                thisType = MESVarType.MESString;
            return thisType;
        }

        public List<SourceField> GetSubSourceField(FieldTreeViewData root)
        {
            List<SourceField> lsf = new List<SourceField>();
            if (root == null)
                return lsf;

            TreeView tv = new TreeView();

            root.PopulateTree(tv, root.FindNodeBySource(this));

            if (tv.Nodes.Count > 0)
            {
                foreach (TreeNode node in tv.Nodes[0].Nodes)
                {
                    lsf.Add(node.Tag as SourceField);
                }
            }
            return lsf;
        }
        #region 2011.10.14 增加
        /// <summary>
        ///  2011.10.14 增加
        ///  目的:根据当前类信息获取在数据结构层里面当前的字段
        /// </summary>
        /// <returns></returns>
        public List<string> GetDirectSubSourceFieldName()
        {
            List<string> result = new List<string>();
            if (this.ParentNode != null && this.ParentNode.TreeView != null && this.ParentNode.TreeView.Nodes != null)
            {
                TreeNode temp = SearchTreeNode(this.ParentNode.TreeView.Nodes);
                if (temp != null && temp.Nodes != null)
                {
                    foreach (TreeNode nodetemp in temp.Nodes)
                    {
                        result.Add(nodetemp.Text);
                    }
                }
            }
            return result;
        }
        private TreeNode SearchTreeNode(TreeNodeCollection Aim)
        {
            TreeNode result = null;
            if (Aim != null)
            {
                foreach (TreeNode temp in Aim)
                {
                    if (temp.Tag != null && temp.Tag is SourceField)
                    {
                        if ((temp.Tag as SourceField).ID == this.ID)
                        {
                            result = temp;
                            return result;
                        }
                        if (temp.Nodes != null)
                        {
                            result = SearchTreeNode(temp.Nodes);
                        }
                    }
                    else
                    {
                        if (temp.Nodes != null)
                        {
                            result = SearchTreeNode(temp.Nodes);
                        }
                    }
                }
            }
            return result;
        }
        #endregion
        /// <summary>
        /// 表名选择编辑器
        /// </summary>
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
                        SourceField cbx = null;
                        if (context.Instance is SourceField)
                            cbx = context.Instance as SourceField;

                        FormSqlEdit fse = new FormSqlEdit();

                        fse.SqlText = cbx.SqlText;
                        if (fse.ShowDialog() == DialogResult.OK)
                        {
                            value = fse.SqlText;
                        }
                        return value;
                    }
                }

                return value;
            }
        }
        #region 2011.10.09 增加
        /// <summary>
        /// 2011.10.09 增加
        /// 目的:为了给表达式组织数据源,需要将所有绑定SQL语句的SourceField预先查出来
        /// 并塞到一个DataSet里,这样就需要确保所有绑定了SQL语句的SourceField的名字要不一样
        /// 因此需要加入一个校验.
        /// </summary>
        /// <returns>返回校验结果(true表示校验通过 false表示校验不通过)</returns>
        private bool CheckSourceFieldName(TreeView Aim, string value)
        {
            bool result = false;
            if (Aim.Nodes != null)
            {
                result = CheckSubSourceFieldName(Aim.Nodes, value);
            }
            return result;
        }
        private bool CheckSubSourceFieldName(TreeNodeCollection Aim, string value)
        {
            bool result = false;
            foreach (TreeNode node in Aim)
            {
                if (node.Nodes != null)
                {
                    result = CheckSubSourceFieldName(node.Nodes, value);
                    if (result == false)
                    {
                        return result;
                    }
                }
                SourceField sf = node.Tag as SourceField;
                if (sf != null && sf.ID != this.ID && !string.IsNullOrEmpty(sf.SqlText))
                {
                    if (sf.Name == value)
                    {
                        result = false;
                        return result;
                    }
                }
            }
            result = true;
            return result;
        }

        private bool CheckSameAndRelativeLevelName(TreeNode Aim, string value)
        {
            bool result = false;
            // 原则：与同级和相邻级均不重名
            // 同级不同名
            foreach (TreeNode node in Aim.Parent.Nodes)
            {
                if (node.Text == value)
                {
                    result = false;
                    return result;
                }
            }
            // 与父级不同名
            if (null != Aim.Parent)
            {
                if (Aim.Parent.Text == value)
                {
                    result = false;
                    return result;
                }
            }
            // 与子级不同名
            foreach (TreeNode node in Aim.Nodes)
            {
                if (node.Text == value)
                {
                    result = false;
                    return result;
                }
            }

            result = true;
            return result;
        }

        #endregion
        #region 2011.09.22 增加
        /// <summary>
        /// 2011.09.22 增加
        /// 目的:由于以前嵌套查询只能传递一个参数,现在要改成多参数传递
        /// 因此需要有一个多字段属性编辑器
        /// </summary>
        internal class FieldCollectionEditer : UITypeEditor
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
                        SourceField cbx = null;
                        if (context.Instance is SourceField)
                            cbx = context.Instance as SourceField;

                        FieldCollection fse = new FieldCollection();

                        fse.RecordFields = cbx.RecordFields;
                        if (fse.ShowDialog() == DialogResult.OK)
                        {
                            List<string> temp = new List<string>();
                            if (fse.RecordFields != null)
                            {
                                for (int i = 0; i < fse.RecordFields.Count; i++)
                                {
                                    string aa = fse.RecordFields[i];
                                    if (!temp.Contains(aa))
                                    {
                                        temp.Add(aa);
                                    }
                                }
                            }
                            value = temp;
                        }
                        return value;
                    }
                }

                return value;
            }
        }
        #endregion
        internal class ValueMapEditor : UITypeEditor
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
                        SourceField cbx = null;
                        if (context.Instance is SourceField)
                            cbx = context.Instance as SourceField;

                        FormCollection fse = new FormCollection();

                        fse.DataList = cbx.ReplaceList;
                        if (fse.ShowDialog() == DialogResult.OK)
                        {
                            List<ValueMap> temp = new List<ValueMap>();
                            for (int i = 0; i < fse.DataList.Count; i++)
                            {
                                ValueMap aa = fse.DataList[i];
                                temp.Add(aa);
                            }
                            value = temp;
                        }
                        return value;
                    }
                }

                return value;
            }
        }

        /// <summary>
        /// 数据源选择编辑器
        /// </summary>
        internal class DBSelectionEditor : UITypeEditor
        {
            public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            {
                if (context != null && context.Instance != null)
                {
                    return UITypeEditorEditStyle.DropDown;
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
                        SourceField cbx = context.Instance as SourceField;
                        RelationFieldChoose rfc = new RelationFieldChoose(editorService);

                        rfc.strRField = (string)cbx.DBSource;
                        List<PmsField> lp = new List<PmsField>();

                        List<PMSRefDBConnectionObj> lpdb = PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetRefDBConnectionObjList();
                        foreach (PMSRefDBConnectionObj pdb in lpdb)
                        {
                            PmsField pf = new PmsField();
                            pf.fieldName = pdb.StrName;
                            pf.fieldDescription = pdb.StrDescription;
                            lp.Add(pf);
                        }
                        rfc.pmsFieldList = lp;
                        editorService.DropDownControl(rfc);
                        value = rfc.strRField;
                        return value;
                    }
                }

                return value;
            }
        }
        #region 2011.10.10 增加
        /// <summary>
        /// 2011.10.10 增加
        /// 目的:由于SourceField里面的成员变量有非托管的内容
        /// 需要手动释放
        /// </summary>
        public void Dispose()
        {
            if (this.ParentNode != null && this.ParentNode.TreeView != null)
            {
                this.ParentNode.TreeView.Dispose();
            }
            if (this._Tag != null)
            {
                if (_Tag is IDisposable)
                {
                    (_Tag as IDisposable).Dispose();
                }
                _Tag = null;
            }
            if (this.fieldDataTable != null)
            {
                this.fieldDataTable.Dispose();
                this.fieldDataTable = null;
            }
            if (this.SourceTable != null)
            {
                this.SourceTable.Dispose();
                this.SourceTable = null;
            }
            if (_RecordFields != null)
            {
                _RecordFields.Clear();
                _RecordFields = null;
            }
            if (RecordFieldsCurrentValue != null)
            {
                RecordFieldsCurrentValue.Clear();
                RecordFieldsCurrentValue = null;
            }
        }
        #endregion

        #region 2011.10.31 增加
        /// <summary>
        /// 2011.10.31 增加
        /// 目的:根据数据库类型以及字段类型返回c#对应的类型
        /// </summary>
        /// <returns></returns>
        public Type GetRealType()
        {
            Type result = null;
            if (this.Tag != null && this.Tag is PMSRefDBFieldProp)
            {
                PMSRefDBFieldProp temp = this.Tag as PMSRefDBFieldProp;
                if (temp.PMSRefDBConnection != null)
                {
                    if (!string.IsNullOrEmpty(_DataType))
                    {
                        string str = _DataType.ToLower();
                        if (str.Equals("bit") || str.Equals("system.boolean"))
                        {
                            result = typeof(System.Boolean);
                        }
                        else if (/*str.Equals("char") || */str.Equals("system.char"))
                        {
                            result = typeof(System.Char);
                        }
                        else if (str.Equals("char") || str.Equals("nchar") || str.Equals("varchar") || str.Equals("nvarchar") || str.Equals("varchar2") || str.Equals("nvarchar2") || str.Equals("text") || str.Equals("ntext") ||
                            str.Equals("system.string") || str.Equals("clob") || str.Equals("nclob"))
                        {
                            result = typeof(System.String);
                        }
                        else if (str.Equals("float") || str.Equals("system.float") || str.Equals("single") || str.Equals("system.single") || str.Equals("real"))
                        {
                            result = typeof(float);
                        }
                        else if (str.Equals("system.double") || str.Equals("double"))
                        {
                            result = typeof(System.Double);
                        }
                        else if (str.Equals("tinyint") || str.Equals("system.byte"))
                        {
                            result = typeof(Byte);
                        }
                        else if (str.Equals("int") || str.Equals("system.int"))
                        {
                            result = typeof(int);
                        }
                        else if (str.Equals("decimal"))
                        {
                            result = typeof(Decimal);
                        }
                        else if (str.Equals("smalldatetime") || str.Equals("datetime") || str.Equals("date") || str.Equals("timestamp") || str.Equals("system.datetime"))
                        {
                            result = typeof(DateTime);
                        }
                        else if (str.Equals("image") || str.Equals("system.byte[]") || str.Equals("blob"))
                        {
                            result = typeof(System.Byte[]);
                        }
                        else if (str.Equals("uniqueidentifier") || str.Equals("system.guid") || str.Equals("raw"))
                        {
                            result = typeof(System.Guid);
                        }
                        else if (str.Equals("system.int32") || str.Equals("int32"))
                        {
                            result = typeof(System.Int32);
                        }
                        else if (str.Equals("smallint") || str.Equals("system.int16") || str.Equals("int16"))
                        {
                            result = typeof(System.Int16);
                        }
                        else if (str.Equals("system.int64") || str.Equals("int64") || str.Equals("bigint"))
                        {
                            result = typeof(System.Int64);
                        }
                        else if (str.Equals("money") || str.Equals("numeric") || str.Equals("number") || str.Equals("smallmoney") || str.Equals("system.decimal") || str.Equals("decimal"))
                        {
                            result = typeof(System.Decimal);
                        }
                    }
                    else
                    {
                        result = PMSVar.GetCSharpType(_FieldType);
                    }
                }
            }
            else
            {
                switch (this.FieldType)
                {
                    case MESVarType.MESString:
                        result = typeof(System.String);
                        break;
                    case MESVarType.MESInt:
                        result = typeof(System.Int32);
                        break;
                    case MESVarType.MESDateTime:
                        result = typeof(DateTime);
                        break;
                    case MESVarType.MESReal:
                        result = typeof(float);
                        break;
                    case MESVarType.MESBool:
                        result = typeof(System.Boolean);
                        break;
                    case MESVarType.MESGuid:
                        result = typeof(System.Guid);
                        break;
                    case MESVarType.MESDecimal:
                        result = typeof(System.Decimal);
                        break;
                    case MESVarType.MESNodefined:
                        result = typeof(object);
                        break;
                    default:
                        break;
                }
            }
            return result;
        }
        #endregion

        #region 2012.01.12 增加 提供一个对象比较符看自定义两个对象是否相等
        /// <summary>
        /// 2012.01.12 增加 
        /// 目的:提供一个对象比较符看自定义两个对象是否相等
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            bool result = false;
            if (obj != null && obj is SourceField)
            {
                SourceField temp = obj as SourceField;
                if (temp.Name != this.Name)
                {
                    return result;
                }
                if (temp.ID != this.ID)
                {
                    return result;
                }
                if (temp.FormatString != this.FormatString)
                {
                    return result;
                }
                if (temp.RecordField != this.RecordField)
                {
                    return result;
                }
                if (!temp.Tag.Equals(this.Tag))
                {
                    return result;
                }
                if (temp.TableName != this.TableName)
                {
                    return result;
                }
                if (temp.ReadOnly != this.ReadOnly)
                {
                    return result;
                }
                if (temp.RelationField != this.RelationField)
                {
                    return result;
                }
                if (temp.VarName != this.VarName)
                {
                    return result;
                }
                if (temp.IsChildModify != this.IsChildModify)
                {
                    return result;
                }
                if (temp.bSampling != this.bSampling)
                {
                    return result;
                }
                if (temp.samplingField != this.samplingField)
                {
                    return result;
                }
                if (temp.samplingDistance != this.samplingDistance)
                {
                    return result;
                }
                if (temp.timePart != this.timePart)
                {
                    return result;
                }
                if (temp.samplingCount != this.samplingCount)
                {
                    return result;
                }
                if (temp.sortType != this.sortType)
                {
                    return result;
                }
                if (temp.samplingStart != this.samplingStart)
                {
                    return result;
                }
                if (temp.samplingEnd != this.samplingEnd)
                {
                    return result;
                }
                if (temp.samplingType != this.samplingType)
                {
                    return result;
                }
                if (temp.nodeType != this.nodeType)
                {
                    return result;
                }
                result = true;
                return result;
            }
            else
            {
                return base.Equals(obj);
            }
        }
        #endregion


        #region 2012.04.1 增加 为增加一个路径属性所做的处理
        /// <summary>
        /// 2012.04.01 增加
        /// 目的:根据数据源的节点属性获取数据源的路径
        /// </summary>
        /// <param name="Aim">节点</param>
        /// <param name="path">路径</param>
        private void GetPathFormNode(TreeNode Aim, ref string path)
        {
            if (Aim != null && Aim.Parent != null && !string.IsNullOrEmpty(Aim.Parent.Text))
            {
                path = Aim.Parent.Text + "." + path;
                GetPathFormNode(Aim.Parent, ref path);
            }
        }
        #endregion

        public override string ToString()
        {
            return this._Name;
        }
        public SourceField Clone()
        {
            SourceField sf = new SourceField(this._Name, this._SqlText);
            sf.FormatString = this._FormatString;
            //sf.DataType = this.DataType;
            sf.FieldType = this.FieldType;
            sf.ID = this.ID;
            sf.RecordField = this.RecordField;
            sf.ParentNode = this.ParentNode;
            sf.SubCode = this.SubCode;
            sf.Tag = this.Tag;

            sf.TableName = this.TableName;
            sf.ReadOnly = this.ReadOnly;
            sf.RelationField = this.RelationField;
            sf.Standard = this.Standard;
            sf.IsChildModify = this.IsChildModify;

            sf.bSampling = this.bSampling;
            sf.samplingField = this.samplingField;
            sf.samplingDistance = this.samplingDistance;
            sf.timePart = this.timePart;
            sf.samplingCount = this.samplingCount;
            sf.sortType = this.sortType;
            sf.samplingStart = this.samplingStart;
            sf.samplingEnd = this.samplingEnd;
            sf.samplingType = this.samplingType;
            sf.DBSource = this.DBSource;
            sf.CustomMode = this.CustomMode;
            sf.CustomTablePath = this.CustomTablePath;

            sf._Path = this._Path;

            sf.nodeType = this.nodeType;
            sf.VarName = this.VarName;
            if (sf.replaceList == null)
                sf.replaceList = new List<ValueMap>();
            if (this.replaceList == null)
                this.replaceList = new List<ValueMap>();
            sf.replaceList.Clear();
            foreach (ValueMap vw in this.replaceList)
            {
                sf.replaceList.Add(vw.Clone());
            }
            if (sf.RecordFields == null)
                sf.RecordFields = new List<string>();
            if (this.RecordFields == null)
                this.RecordFields = new List<string>();
            for (int i = 0; i < this.RecordFields.Count; i++)
            {
                if (!sf.RecordFields.Contains(this.RecordFields[i]))
                {
                    sf.RecordFields.Add(this.RecordFields[i]);
                }
            }
            sf.RecordFieldsCurrentValue = this.RecordFieldsCurrentValue;
            return sf;
        }
        private static void CreateBlankXml(string path)
        {
            XmlTextWriter textWriter = new XmlTextWriter(path, null);
            textWriter.Formatting = Formatting.Indented;
            textWriter.Indentation = 4;

            // 开始写过程，调用WriteStartDocument方法
            textWriter.WriteStartDocument();

            // 写入说明
            //textWriter.WriteComment("设置参加抽奖人员的信息");
            textWriter.WriteStartElement("ReportExecuteInfo");
            textWriter.WriteEndElement();
            // 写文档结束，调用WriteEndDocument方法
            textWriter.WriteEndDocument();

            // 关闭textWriter
            textWriter.Close();
        }

        public static void ReportError(string name, string description)
        {
            string filePath = System.Windows.Forms.Application.StartupPath + "\\Trace\\ReportExecute.xml";
            if (File.Exists(filePath))
            {
                FileInfo fi = new FileInfo(filePath);
                long len = fi.Length;   //得到文件大
                string fileSavePath = System.Windows.Forms.Application.StartupPath + "\\Trace\\ReportExecute" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
                if (fi.Length > 1024 * 1024 * 10)
                {
                    fi.CopyTo(fileSavePath);
                    CreateBlankXml(filePath);
                }
            }
            else
                CreateBlankXml(filePath);

            PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(name + " " + description);

            XmlDocument xDocument = new XmlDocument();
            xDocument.Load(filePath);

            XmlElement root = xDocument.DocumentElement;

            XmlElement title = xDocument.CreateElement("Title");
            title.SetAttribute("Name", name);


            XmlElement startTime = xDocument.CreateElement("HappenTime");
            XmlText startTimeValue = xDocument.CreateTextNode(DateTime.Now.ToString());
            startTime.AppendChild(startTimeValue);

            //XmlElement endTime = xDocument.CreateElement("EndTime");
            //XmlText endTimeValue = xDocument.CreateTextNode("EndTime1111");
            //endTime.AppendChild(endTimeValue);

            XmlElement actionName = xDocument.CreateElement("Description");
            XmlText actionNameValue = xDocument.CreateTextNode(description);
            actionName.AppendChild(actionNameValue);

            title.AppendChild(startTime);
            //title.AppendChild(endTime);
            title.AppendChild(actionName);

            root.InsertAfter(title, root.LastChild);

            xDocument.Save(filePath);
        }

        internal class DateTimeConverter : UriTypeConverter
        {
            public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, Type destinationType)
            {
                if (destinationType == typeof(DateTime))
                {
                    return true;
                }
                return base.CanConvertTo(context, destinationType);
            }

            public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string) && value is DateTime)
                {
                    if (null != value)
                    {
                        if (((DateTime)value).Ticks == (new DateTime(1, 1, 1, 0, 0, 0)).Ticks)
                            return string.Empty;
                        string str = string.Format("{0:yyyy/MM/dd HH:mm:ss}", value);
                        return str;
                    }
                }
                return base.ConvertTo(context, culture, value, destinationType);
            }

            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                {
                    return true;
                }
                return base.CanConvertFrom(context, sourceType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                if (value is string)
                {
                    try
                    {
                        DateTime dtOut = DateTime.MinValue;
                        if (DateTime.TryParse(value as string, out dtOut))
                        {

                        }
                        return dtOut;
                    }
                    catch
                    {
                        throw new ArgumentException("Can not convert\"" + (string)value + "\"to type DateTime");
                    }
                }
                return base.ConvertFrom(context, culture, value);
            }
        }
    }

    [Serializable]
    public class FieldTreeViewData : IDisposable
    {
        public FieldTreeNodeData[] Nodes;

        public void Dispose()
        {

        }
        /// <summary>
        /// 2012.08.24 增加
        /// 目的:为外部提供设计时变量表
        /// </summary>
        /// <returns></returns>
        public List<PMSVar> GetParameters()
        {
            List<PMSVar> result = new List<PMSVar>();
            if (this.Nodes != null && this.Nodes.Length > 0)
            {
                ReportVar temp = this.Nodes[0].Tag as ReportVar;
                if (temp != null)
                {
                    result = temp.PMSVarList;
                }
            }
            return result;
        }
        public FieldTreeViewData(TreeView treeview)
        {
            if (treeview == null)
            {
                Nodes = new FieldTreeNodeData[0];
                return;
            }
            Nodes = new FieldTreeNodeData[treeview.Nodes.Count];

            for (int i = 0; i <= treeview.Nodes.Count - 1; i++)
            {
                Nodes[i] = new FieldTreeNodeData(treeview.Nodes[i]);
            }
        }
        public override string ToString()
        {
            return "数据集配置信息";
        }
        public FieldTreeViewData Clone()
        {
            FieldTreeViewData newTree = new FieldTreeViewData(null);

            try
            {
                newTree.Nodes = new FieldTreeNodeData[this.Nodes.Length];
                this.Nodes.CopyTo(newTree.Nodes, 0);
            }
            catch
            {
                newTree.Nodes = new FieldTreeNodeData[0];
            }

            return newTree;
        }

        //完整的
        public void PopulateTree(TreeView treeview)
        {
            if (this.Nodes == null || this.Nodes.Length == 0)
            {
                return;
            }
            treeview.BeginUpdate();
            treeview.Nodes.Clear();
            this.Nodes[0].ImageIndex = 1;
            this.Nodes[0].SelectedImageIndex = 1;
            if (this.Nodes[0].Nodes != null && this.Nodes[0].Nodes.Length > 0)
            {
                if (this.Nodes[0].Nodes[0].Tag != null)
                {
                    if (this.Nodes[0].Nodes[0].Tag is SourceField)
                    {
                        SourceField temp = this.Nodes[0].Nodes[0].Tag as SourceField;
                        if (temp != null)
                        {
                            if (temp.ParentNode != null && temp.ParentNode.TreeView != null)
                            {
                                if (temp.ParentNode.TreeView.ImageList != null)
                                {
                                    ImageList imagetemp = new ImageList();
                                    for (int i = 0; i < temp.ParentNode.TreeView.ImageList.Images.Count; i++)
                                    {
                                        System.Drawing.Image icotemp = (System.Drawing.Image)temp.ParentNode.TreeView.ImageList.Images[i].Clone();
                                        imagetemp.Images.Add(icotemp);
                                    }
                                    treeview.ImageList = imagetemp;
                                }
                            }
                        }
                    }
                }
            }
            for (int i = 0; i <= this.Nodes.Length - 1; i++)
            {
                this.Nodes[i].ToTreeNode(treeview.Nodes);
            }
            treeview.EndUpdate();
        }


        //为选择绑定变量而做2011-05-06
        public void PopulateTree(TreeView treeview, bool bBinding)
        {
            if (this.Nodes == null || this.Nodes.Length == 0)
            {
                return;
            }
            treeview.BeginUpdate();
            treeview.Nodes.Clear();
            this.Nodes[0].ImageIndex = 1;
            this.Nodes[0].SelectedImageIndex = 1;
            if (this.Nodes[0].Nodes != null && this.Nodes[0].Nodes.Length > 0)
            {
                if (this.Nodes[0].Nodes[0].Tag != null)
                {
                    if (this.Nodes[0].Nodes[0].Tag is SourceField)
                    {
                        SourceField temp = this.Nodes[0].Nodes[0].Tag as SourceField;
                        if (temp != null)
                        {
                            if (temp.ParentNode != null && temp.ParentNode.TreeView != null)
                            {
                                if (temp.ParentNode.TreeView.ImageList != null)
                                {
                                    ImageList imagetemp = new ImageList();
                                    for (int i = 0; i < temp.ParentNode.TreeView.ImageList.Images.Count; i++)
                                    {
                                        System.Drawing.Image icotemp = (System.Drawing.Image)temp.ParentNode.TreeView.ImageList.Images[i].Clone();
                                        imagetemp.Images.Add(icotemp);
                                    }
                                    treeview.ImageList = imagetemp;
                                }
                            }
                        }
                    }
                }
            }
            for (int i = 0; i <= this.Nodes.Length - 1; i++)
            {
                this.Nodes[i].ToTreeNode(treeview.Nodes);
            }
            BindReportVar(treeview);
            treeview.EndUpdate();
        }


        /// <summary>
        /// 2013.1.8 增加 
        /// 目的:提供一个参数,只允许用户绑定变量而不能绑定SoureceField
        /// </summary>
        /// <param name="treeview">要构造的tree</param>
        /// <param name="bBinding">是否绑定变量</param> 
        /// <param name="BindingSoureceField">是否绑定SoureceField</param>
        public void PopulateTree(TreeView treeview, bool bBinding, bool BindingSoureceField)
        {
            if (this.Nodes == null || this.Nodes.Length == 0)
            {
                return;
            }
            treeview.BeginUpdate();
            treeview.Nodes.Clear();
            if (BindingSoureceField)
            {
                this.Nodes[0].ImageIndex = 1;
                this.Nodes[0].SelectedImageIndex = 1;
                if (this.Nodes[0].Nodes != null && this.Nodes[0].Nodes.Length > 0)
                {
                    if (this.Nodes[0].Nodes[0].Tag != null)
                    {
                        if (this.Nodes[0].Nodes[0].Tag is SourceField)
                        {
                            SourceField temp = this.Nodes[0].Nodes[0].Tag as SourceField;
                            if (temp != null)
                            {
                                if (temp.ParentNode != null && temp.ParentNode.TreeView != null)
                                {
                                    if (temp.ParentNode.TreeView.ImageList != null)
                                    {
                                        ImageList imagetemp = new ImageList();
                                        for (int i = 0; i < temp.ParentNode.TreeView.ImageList.Images.Count; i++)
                                        {
                                            System.Drawing.Image icotemp = (System.Drawing.Image)temp.ParentNode.TreeView.ImageList.Images[i].Clone();
                                            imagetemp.Images.Add(icotemp);
                                        }
                                        treeview.ImageList = imagetemp;
                                    }
                                }
                            }
                        }
                    }
                }
                for (int i = 0; i <= this.Nodes.Length - 1; i++)
                {
                    this.Nodes[i].ToTreeNode(treeview.Nodes);
                }
            }
            BindReportVar(treeview);
            treeview.EndUpdate();
        }
        //从某个节点载入
        public void PopulateTree(TreeView treeview, FieldTreeNodeData rootNode, bool bBinding)
        {
            if (rootNode == null)// || rootNode.Nodes.Length == 0)
            {
                return;
            }
            treeview.BeginUpdate();
            treeview.Nodes.Clear();
            if (rootNode.Tag != null)
            {
                if (rootNode.Tag is SourceField)
                {
                    SourceField temp = rootNode.Tag as SourceField;
                    if (temp != null)
                    {
                        if (temp.ParentNode != null && temp.ParentNode.TreeView != null)
                        {
                            if (temp.ParentNode.TreeView.ImageList != null)
                            {
                                ImageList imagetemp = new ImageList();
                                for (int i = 0; i < temp.ParentNode.TreeView.ImageList.Images.Count; i++)
                                {
                                    System.Drawing.Image icotemp = (System.Drawing.Image)temp.ParentNode.TreeView.ImageList.Images[i].Clone();
                                    imagetemp.Images.Add(icotemp);
                                }
                                treeview.ImageList = imagetemp;
                            }
                        }
                    }
                }
            }
            //for (int i = 0; i <= this.Nodes.Length - 1; i++)
            {
                rootNode.ToTreeNode(treeview.Nodes);
            }
            BindReportVar(treeview);
            treeview.EndUpdate();
        }

        private void BindReportVar(TreeView treeview)
        {
            if (this.Nodes == null || this.Nodes.Length == 0)
            {
                return;
            }
            ReportVar rv = this.Nodes[0].Tag as ReportVar;

            if (null != rv)
            {
                TreeNode tHead = new TreeNode("报表变量");
                treeview.Nodes.Add(tHead);
                tHead.ImageIndex = 2;
                tHead.SelectedImageIndex = 2;
                foreach (PMSVar pv in rv.PMSVarList)
                {
                    TreeNode tnchild = new TreeNode(pv.VarName);
                    tnchild.Tag = pv;
                    tHead.Nodes.Add(tnchild);
                }
            }
        }

        //从某个节点载入
        public void PopulateTree(TreeView treeview, FieldTreeNodeData rootNode)
        {
            if (rootNode == null)// || rootNode.Nodes.Length == 0)
            {
                return;
            }
            treeview.BeginUpdate();
            treeview.Nodes.Clear();
            if (rootNode.Tag != null)
            {
                if (rootNode.Tag is SourceField)
                {
                    SourceField temp = rootNode.Tag as SourceField;
                    if (temp != null)
                    {
                        if (temp.ParentNode != null && temp.ParentNode.TreeView != null)
                        {
                            if (temp.ParentNode.TreeView.ImageList != null)
                            {
                                ImageList imagetemp = new ImageList();
                                for (int i = 0; i < temp.ParentNode.TreeView.ImageList.Images.Count; i++)
                                {
                                    System.Drawing.Image icotemp = (System.Drawing.Image)temp.ParentNode.TreeView.ImageList.Images[i].Clone();
                                    imagetemp.Images.Add(icotemp);
                                }
                                treeview.ImageList = imagetemp;
                            }
                        }
                    }
                }
            }
            //TreeNode Nodetemp = new TreeNode(Properties.Resources.ResourceManager.GetString("Context0001"), 1, 1);
            //treeview.Nodes.Add(Nodetemp);
            //for (int i = 0; i <= this.Nodes.Length - 1; i++)
            {
                rootNode.ToTreeNode(treeview.Nodes);
            }
            //BindReportVar(treeview);
            treeview.EndUpdate();
        }
        //2011.10.27 增加
        //目的:专门给小黄提供一个函数
        public void PopulateTreeAddRoot(TreeView treeview, FieldTreeNodeData rootNode)
        {
            if (rootNode == null)// || rootNode.Nodes.Length == 0)
            {
                return;
            }
            treeview.BeginUpdate();
            treeview.Nodes.Clear();
            if (rootNode.Tag != null)
            {
                if (rootNode.Tag is SourceField)
                {
                    SourceField temp = rootNode.Tag as SourceField;
                    if (temp != null)
                    {
                        if (temp.ParentNode != null && temp.ParentNode.TreeView != null)
                        {
                            if (temp.ParentNode.TreeView.ImageList != null)
                            {
                                ImageList imagetemp = new ImageList();
                                for (int i = 0; i < temp.ParentNode.TreeView.ImageList.Images.Count; i++)
                                {
                                    System.Drawing.Image icotemp = (System.Drawing.Image)temp.ParentNode.TreeView.ImageList.Images[i].Clone();
                                    imagetemp.Images.Add(icotemp);
                                }
                                treeview.ImageList = imagetemp;
                            }
                        }
                    }
                }
            }
            TreeNode Nodetemp = new TreeNode(Properties.Resources.ResourceManager.GetString("Context0001"), 1, 1);
            treeview.Nodes.Add(Nodetemp);
            //for (int i = 0; i <= this.Nodes.Length - 1; i++)
            {
                rootNode.ToTreeNode(treeview.Nodes[0].Nodes);
            }
            //BindReportVar(treeview);
            treeview.EndUpdate();
        }
        public FieldTreeNodeData FindNodeById(Guid id)
        {
            Queue<FieldTreeNodeData> queue = new Queue<FieldTreeNodeData>();

            foreach (var node in this.Nodes)
            {
                if (node.Tag != null && node.Tag is SourceField)
                {
                    SourceField sf = node.Tag as SourceField;

                    if (sf.ID == id)
                    {
                        queue.Clear();
                        return node;
                    }
                }
                queue.Enqueue(node);
            }
            while (queue.Count > 0)
            {
                FieldTreeNodeData ftnd = queue.Dequeue();
                if (ftnd.Tag != null && ftnd.Tag is SourceField)
                {
                    SourceField sf = ftnd.Tag as SourceField;

                    if (sf.ID == id)
                    {
                        queue.Clear();
                        return ftnd;
                    }
                }
                foreach (var node in ftnd.Nodes)
                {
                    queue.Enqueue(node);
                }
            }
            return null;
        }

        #region 2012.04.01 增加 以路径来标记数据源
        /// <summary>
        /// 2012.04.01 增加
        /// 目的:根据路径来查找节点
        /// </summary>
        /// <param name="path">节点</param>
        /// <returns>返回查询结果</returns>
        public FieldTreeNodeData FindNodeByPath(string path)
        {
            Queue<FieldTreeNodeData> queue = new Queue<FieldTreeNodeData>();

            foreach (var node in this.Nodes)
            {
                if (node.Tag != null && node.Tag is SourceField)
                {
                    SourceField sf = node.Tag as SourceField;

                    if (sf.Path + sf.Name == path)
                    {
                        queue.Clear();
                        return node;
                    }
                }
                queue.Enqueue(node);
            }
            while (queue.Count > 0)
            {
                FieldTreeNodeData ftnd = queue.Dequeue();
                if (ftnd.Tag != null && ftnd.Tag is SourceField)
                {
                    SourceField sf = ftnd.Tag as SourceField;

                    if (sf.Path + sf.Name == path)
                    {
                        queue.Clear();
                        return ftnd;
                    }
                }
                foreach (var node in ftnd.Nodes)
                {
                    queue.Enqueue(node);
                }
            }
            return null;
        }
        /// <summary>
        /// 2012.04.01 增加 
        /// 目的:查找此数据源在实际数据源树中对应的数据源
        /// </summary>
        /// <param name="Aim">要查询的数据源</param>
        /// <returns>返回查询结果</returns>
        public FieldTreeNodeData FindNodeBySource(SourceField Aim)
        {
            if (Aim != null)
            {
                if (!string.IsNullOrEmpty(Aim.Path))
                {
                    FieldTreeNodeData result = FindNodeByPath(Aim.Path + Aim.Name);
                    if (result != null)
                    {
                        return result;
                    }
                    else
                    {
                        return FindNodeById(Aim.ID);
                    }
                }
                else
                {
                    return FindNodeById(Aim.ID);
                }
            }
            else
            {
                return null;
            }
        }
        #endregion
    }

    [Serializable]
    public class FieldTreeNodeData : IDisposable
    {
        //public Guid ID;
        public string Text;
        public int ImageIndex;
        public int SelectedImageIndex;
        public bool Expanded;
        public bool Checked;
        public string Name;
        public object Tag;//SourceField
        public FieldTreeNodeData[] Nodes;
        [NonSerialized]
        public ImageList ImageList;

        public void Dispose()
        {
            if (null != Tag)
            {
                if (Tag is IDisposable)
                {
                    (Tag as IDisposable).Dispose();
                }
                Tag = null;
            }

            if (null != ImageList)
            {
                ImageList.Dispose();
                ImageList = null;
            }

            if (null != Nodes)
            {
                foreach (FieldTreeNodeData node in Nodes)
                {
                    node.Dispose();
                }
                Nodes = null;
            }
        }

        public FieldTreeNodeData(TreeNode node)
        {
            this.Text = node.Text;
            this.Name = node.Name;
            this.ImageIndex = node.ImageIndex;
            this.SelectedImageIndex = node.SelectedImageIndex;
            this.Checked = node.Checked;
            this.Expanded = node.IsExpanded;
            this.Nodes = new FieldTreeNodeData[node.Nodes.Count];

            if (node.TreeView != null)
            {
                if (node.TreeView.ImageList != null)
                {
                    ImageList imagetemp = new ImageList();
                    for (int i = 0; i < node.TreeView.ImageList.Images.Count; i++)
                    {
                        System.Drawing.Image icotemp = (System.Drawing.Image)node.TreeView.ImageList.Images[i].Clone();
                        imagetemp.Images.Add(icotemp);
                    }
                    this.ImageList = imagetemp;
                }
            }
            //this.UniqueTag = ((FieldGuid)node.Tag).uniqueID;

            if ((!(node.Tag == null)) && node.Tag.GetType().IsSerializable)
            {
                this.Tag = node.Tag;
            }
            else
            {
                this.Tag = null;
            }
            if (node.Nodes.Count > 0)
            {
                for (int i = 0; i <= node.Nodes.Count - 1; i++)
                {
                    Nodes[i] = new FieldTreeNodeData(node.Nodes[i]);
                }
            }
        }

        public void ToTreeNode(TreeNodeCollection Nodes)
        {
            TreeNode ToTreeNode = new TreeNode(this.Text, this.ImageIndex, this.SelectedImageIndex);
            ToTreeNode.Checked = this.Checked;
            ToTreeNode.Tag = this.Tag;
            ToTreeNode.Name = this.Name;
            if (this.Tag is SourceField && this.Tag != null)
            {
                ToTreeNode.Text = (this.Tag as SourceField).Name;
                (this.Tag as SourceField).ParentNode = ToTreeNode;
            }
            if (this.Expanded)
            {
                ToTreeNode.Expand();
            }
            Nodes.Add(ToTreeNode);
            if (ToTreeNode.TreeView != null && ToTreeNode.TreeView.ImageList == null)
            {
                if (this.ImageList != null)
                {
                    ImageList imagetemp = new ImageList();
                    for (int i = 0; i < this.ImageList.Images.Count; i++)
                    {
                        System.Drawing.Image icotemp = (System.Drawing.Image)this.ImageList.Images[i].Clone();
                        imagetemp.Images.Add(icotemp);
                    }
                    ToTreeNode.TreeView.ImageList = imagetemp;
                }
            }
            if (this.Nodes == null && this.Nodes.Length == 0)
            {
                return;
            }
            for (int i = 0; i <= this.Nodes.Length - 1; i++)
            {
                this.Nodes[i].ToTreeNode(ToTreeNode.Nodes);
            }
        }
    }
}
