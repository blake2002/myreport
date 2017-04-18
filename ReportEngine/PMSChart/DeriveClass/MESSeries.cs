using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;

namespace PMS.Libraries.ToolControls.PMSChart
{
    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class MESSeries:PMSSeries
    {

        public MESSeries(Series Aim)
            : base(Aim)
        {
        }
        #region  2011.10.27 私有属性
        private string _BindingField = "";
        #endregion

        #region 2011.10.27 公有属性
        [Description("绑定字段")]
        [Category("MES报表属性")]
        [Editor(typeof(MESSeriesEditor), typeof(UITypeEditor))]
        public string BindingField
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
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public List<string> seriesDataForAppearance
        {
            get;
            set;
        }
        #endregion

        #region 属性配置器
        class MESSeriesEditor : UITypeEditor
        {
            public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                IWindowsFormsEditorService editorService = null;
                if (null != context && null != context.Instance)
                {
                    MESSeries pcc = context.Instance as MESSeries;
                    if (null != pcc && null != pcc.SourceField)
                    {
                        editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                        if (editorService != null)
                        {
                            FieldTreeViewData sfAll = (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetCurrentReportDataDefine()) as FieldTreeViewData;
                            RelationFieldChoose rfc = new RelationFieldChoose(editorService);

                            rfc.strRField = pcc.BindingField;
                            List<PmsField> lp = new List<PmsField>();

                            List<SourceField> lpdb = pcc.SourceField.GetSubSourceField(sfAll);
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
                                            if (pcc.seriesDataForAppearance != null && !pcc.seriesDataForAppearance.Contains(pdb.RecordField))
                                            {
                                                PmsField pf = new PmsField();
                                                pf.fieldName = pdb.RecordField;
                                                pf.fieldDescription = pdb.Name;
                                                lp.Add(pf);
                                            }
                                        }
                                    }
                                }
                                catch
                                {
                                }
                            }
                            rfc.pmsFieldList = lp;
                            editorService.DropDownControl(rfc);
                            if (!string.IsNullOrEmpty(rfc.strRField))
                            {
                                value = rfc.strRField;
                            }
                            return value;
                        }
                    }
                }
                return base.EditValue(context, provider, value);
            }

            public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
            {
                if (null != context && null != context.Instance)
                {

                    return UITypeEditorEditStyle.DropDown;
                }

                return base.GetEditStyle(context);
            }
        }
        #endregion
    }
}
