using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace PMS.Libraries.ToolControls.PmsSheet.PmsPublicData
{
    [Serializable]
    public  class SourceFieldStoreProcedure : PmsSheet.PmsPublicData.SourceField
    {
        public SourceFieldStoreProcedure()
            : base()
        {
        }
        public SourceFieldStoreProcedure(PmsSheet.PmsPublicData.SourceField Aim)
        {
            base.Name = Aim.Name;
            base.SqlText = Aim.SqlText;
            base.FormatString = Aim.FormatString;
            base.ID = Aim.ID;
            base.RecordField = Aim.RecordField;
            base.ParentNode = Aim.ParentNode;
            base.SubCode = Aim.SubCode;
            base.Tag = Aim.Tag;

            base.TableName = Aim.TableName;
            base.ReadOnly = Aim.ReadOnly;
            base.RelationField = Aim.RelationField;
            base.Standard = Aim.Standard;
            base.IsChildModify = Aim.IsChildModify;

            base.IsSampling = Aim.IsSampling;
            base.SamplingField = Aim.SamplingField;
            base.SamplingDistance = Aim.SamplingDistance;
            base.TimeUnit = Aim.TimeUnit;
            base.SamplingCount = Aim.SamplingCount;
            base.SortType = Aim.SortType;
            base.SamplingStart = Aim.SamplingStart;
            base.SamplingEnd = Aim.SamplingEnd;
            base.SamplingType = Aim.SamplingType;
            base.DBSource = Aim.DBSource;
            base.FieldType = Aim.FieldType;
            base.FieldDataTable = Aim.FieldDataTable;
            base.DataType = Aim.DataType;
            base.RecordFields = Aim.RecordFields;
            base.SourceTable = Aim.SourceTable;
            base.RecordFieldsCurrentValue = Aim.RecordFieldsCurrentValue;

            base.NodeType = Aim.NodeType;
            base.VarName = Aim.VarName;
            if (base.ReplaceList == null)
                base.ReplaceList = new List<PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.ValueMap>();
            if (this.ReplaceList == null)
                this.ReplaceList = new List<PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.ValueMap>();
            base.ReplaceList.Clear();
            foreach (PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.ValueMap vw in Aim.ReplaceList)
            {
                base.ReplaceList.Add(vw.Clone());
            }
        }
        #region 2011年10月19日 屏蔽基类中在此派生类中不需要的属性
        //多表属性不需要
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string TableName
        {
            get
            {
                return base.TableName;
            }
            set
            {
                base.TableName = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Standard
        {
            get
            {
                return base.Standard;
            }
            set
            {
                base.Standard = value;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool IsChildModify
        {
            get
            {
                return base.IsChildModify;
            }
            set
            {
                base.IsChildModify = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool ReadOnly
        {
            get
            {
                return base.ReadOnly;
            }
            set
            {
                base.ReadOnly = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string RelationField
        {
            get
            {
                return base.RelationField;
            }
            set
            {
                base.RelationField = value;
            }
        }

        //基类通用属性不需要的
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override PmsSheet.PmsPublicData.MESNodeType NodeType
        {
            get
            {
                return base.NodeType;
            }
            set
            {
                base.NodeType = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool BQueryDataSource
        {
            get
            {
                return base.BQueryDataSource;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override PmsSheet.PmsPublicData.MESVarType FieldType
        {
            get
            {
                return base.FieldType;
            }
            set
            {
                base.FieldType = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string SubCode
        {
            get
            {
                return base.SubCode;
            }
            set
            {
                base.SubCode = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string FormatString
        {
            get
            {
                return base.FormatString;
            }
            set
            {
                base.FormatString = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string RecordField
        {
            get
            {
                return base.RecordField;
            }
            set
            {
                base.RecordField = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override object RecordFieldCurrentValue
        {
            get
            {
                return base.RecordFieldCurrentValue;
            }
            set
            {
                base.RecordFieldCurrentValue = value;
            }
        }
        public override string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                if (!string.IsNullOrEmpty(value) && this.ParentNode != null && this.ParentNode.TreeView != null)
                {
                    if (CheckSourceFieldName(this.ParentNode.TreeView, value) == false)
                    {
                        throw new NotImplementedException(Properties.Resources.ResourceManager.GetString("message0010"));
                    }
                }
                base.Name = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)] 
        public override Dictionary<string, object> RecordFieldsCurrentValue
        {
            get
            {
                return base.RecordFieldsCurrentValue;
            }
            set
            {
                base.RecordFieldsCurrentValue = value;
            }
        }
        #endregion

        #region SampleDistanceVar
        [Category("抽样")]
        [Description("绑定时间间隔变量")]
        [Editor(typeof(PMSVarChoose), typeof(UITypeEditor))]
        public string SampleDistanceVar
        {
            get;
            set;
        }
        internal class PMSVarChoose : UITypeEditor
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
                        if (editorService != null)
                        {
                            SourceFieldDataTable cbx = context.Instance as SourceFieldDataTable;
                            SampleDistanceVar rfc = new SampleDistanceVar(editorService);
                            if (cbx != null)
                            {
                                rfc.CurrentVarName = cbx.SampleDistanceVar;
                                List<PMSVar> lp = new List<PMSVar>();
                                FieldTreeViewData sfAll = (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetCurrentReportDataDefine()) as FieldTreeViewData;
                                if (sfAll != null && sfAll.Nodes != null && sfAll.Nodes.Length > 0)
                                {
                                    ReportVar temp = sfAll.Nodes[0].Tag as ReportVar;
                                    if (temp != null)
                                    {
                                        lp = temp.PMSVarList;
                                    }
                                }
                                rfc.ReportVarList = lp;
                                editorService.DropDownControl(rfc);
                                value = rfc.CurrentVarName;
                                return value;
                            }
                        }
                    }
                }
                return value;
            }
        }
        #endregion

        [Category("通用")]
        [Description("多数据源查询")]
        [Editor(typeof(DSSqlEditor), typeof(UITypeEditor))]
        public override List<DSSqlPair> MultiDataSource
        {
            get;
            set;
        }

        [Category("通用")]
        [Description("二次排序,查询至内存后再次排序")]
        public override string SecondarySort
        {
            get;
            set;
        }


        internal class DSSqlEditor : UITypeEditor
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
                        if (editorService != null)
                        {
                            SourceFieldDataTable cbx = context.Instance as SourceFieldDataTable;
                            if (cbx != null)
                            {
                                MultiDataSourceConfigForm form = new MultiDataSourceConfigForm();
                                form.MultiDataSource = cbx.MultiDataSource;
                                form.StartPosition = FormStartPosition.CenterParent;
                                if (form.ShowDialog() == DialogResult.OK)
                                {
                                    List<DSSqlPair> list = new List<DSSqlPair>();
                                    foreach (DSSqlPair dsp in form.MultiDataSource)
                                    {
                                        list.Add(dsp.Clone() as DSSqlPair);
                                    }
                                    
                                    value = list;
                                    return value;
                                }
                            }
                        }
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
                PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField sf = node.Tag as PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField;
                if (sf != null && sf.ID != this.ID && sf.GetType()==typeof(SourceFieldDataTable))
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
        #endregion

    }
}
