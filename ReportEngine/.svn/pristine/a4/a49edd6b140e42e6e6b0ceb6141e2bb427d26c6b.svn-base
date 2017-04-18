using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

namespace PMS.Libraries.ToolControls.PmsSheet.PmsPublicData
{
    [Serializable]
    public  class SourceFieldDataField : PmsSheet.PmsPublicData.SourceField
    {
        public SourceFieldDataField() :
            base()
        {
        }
        public SourceFieldDataField(PmsSheet.PmsPublicData.SourceField Aim)
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
            base.DataType=Aim.DataType;
            base.RecordFields=Aim.RecordFields;
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
        //抽样属性不需要
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override DateTime SamplingStart
        {
            get
            {
                return base.SamplingStart;
            }
            set
            {
                base.SamplingStart = value;
            }
        }
        //[Browsable(false)]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //public override List<PmsSheet.PmsPublicData.ValueMap> ReplaceList
        //{
        //    get
        //    {
        //        return base.ReplaceList;
        //    }
        //    set
        //    {
        //        base.ReplaceList = value;
        //    }
        //}
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override DateTime SamplingEnd
        {
            get
            {
                return base.SamplingEnd;
            }
            set
            {
                base.SamplingEnd = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override PmsSheet.PmsPublicData.MESSamplingType SamplingType
        {
            get
            {
                return base.SamplingType;
            }
            set
            {
                base.SamplingType = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string SamplingField
        {
            get
            {
                return base.SamplingField;
            }
            set
            {
                base.SamplingField = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool IsSampling
        {
            get
            {
                return base.IsSampling;
            }
            set
            {
                base.IsSampling = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override PmsSheet.PmsPublicData.MESTimePart TimeUnit
        {
            get
            {
                return base.TimeUnit;
            }
            set
            {
                base.TimeUnit = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int SamplingDistance
        {
            get
            {
                return base.SamplingDistance;
            }
            set
            {
                base.SamplingDistance = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override PmsSheet.PmsPublicData.MESSortType SortType
        {
            get
            {
                return base.SortType;
            }
            set
            {
                base.SortType = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int SamplingCount
        {
            get
            {
                return base.SamplingCount;
            }
            set
            {
                base.SamplingCount = value;
            }
        }
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
        public override string DBSource
        {
            get
            {
                return base.DBSource;
            }
            set
            {
                base.DBSource = value;
            }
        }
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
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string SqlText
        {
            get
            {
                return base.SqlText;
            }
            set
            {
                base.SqlText = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override List<string> RecordFields
        {
            get
            {
                return base.RecordFields;
            }
            set
            {
                base.RecordFields = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string VarName
        {
            get
            {
                return base.VarName;
            }
            set
            {
                base.VarName = value;
            }
        }
        [Description("绑定字段")]
        public override string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (!PMS.Libraries.ToolControls.PMSPublicInfo.PublicFunctionClass.IsValidIdentifier(value))
                    {
                        throw new NotImplementedException("字段名:" + value + "不合法");
                    }

                    if (CheckSameLevelFieldName(this.ParentNode.Parent.Nodes, value) == false)
                    {
                        throw new NotImplementedException(Properties.Resources.ResourceManager.GetString("message0010"));
                    }
                    base.Name = value;
                    base.RecordField = base.Name;
                }
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


        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override List<DSSqlPair> MultiDataSource
        {
            get { return base.MultiDataSource; }
            set { base.MultiDataSource = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string SecondarySort
        {
            get { return base.SecondarySort; }
            set { base.SecondarySort = value; }
        }

        #endregion

        private bool CheckSameLevelFieldName(TreeNodeCollection Aim, string value)
        {
            bool result = false;
            foreach (TreeNode node in Aim)
            {
                PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField sf = node.Tag as PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField;
                if (sf != null && sf.ID != this.ID && sf.GetType() == typeof(SourceFieldDataField))
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
    }
}
