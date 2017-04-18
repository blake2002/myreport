using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using System.ComponentModel;
using System.Drawing.Design;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using System.Windows.Forms.Design;

namespace PMS.Libraries.ToolControls.PMSChart
{
    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class CurveX:PMSAxis
    {
        public CurveX(Axis axis)
            : base(axis)
        {
        }
        #region 私有属性
        private string _xRecordField = "";
        #endregion

        #region 公有属性
        [Description("绑定字段")]
        [Category("MES报表属性")]
        [Editor(typeof(CurveXEditor), typeof(UITypeEditor))]
        public string xRecordField
        {
            get
            {
                return _xRecordField;
            }
            set
            {
                _xRecordField = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public SourceField SourceField
        {
            get;
            set;
        }
        #endregion

        #region 属性编辑器
        class CurveXEditor : UITypeEditor
        {
            public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                IWindowsFormsEditorService editorService = null;
                if (null != context && null != context.Instance)
                {
                    CurveX pcc = context.Instance as CurveX;
                    if (null != pcc && null != pcc.SourceField)
                    {
                        editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                        if (editorService != null)
                        {
                            FieldTreeViewData sfAll = (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetCurrentReportDataDefine()) as FieldTreeViewData;
                            RelationFieldChoose rfc = new RelationFieldChoose(editorService);

                            rfc.strRField = (string)pcc.xRecordField;
                            List<PmsField> lp = new List<PmsField>();

                            List<SourceField> lpdb = pcc.SourceField.GetSubSourceField(sfAll);
                            foreach (SourceField pdb in lpdb)
                            {
                                if (string.IsNullOrEmpty(pdb.SqlText))
                                {
                                    PmsField pf = new PmsField();
                                    pf.fieldName = pdb.RecordField;
                                    pf.fieldDescription = pdb.Name;
                                    lp.Add(pf);
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
