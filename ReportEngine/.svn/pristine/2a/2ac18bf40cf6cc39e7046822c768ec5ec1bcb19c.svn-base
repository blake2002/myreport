using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing.Design;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using System.Windows.Forms.Design;
using System.Windows.Forms;

namespace PMS.Libraries.ToolControls.PMSChart
{
    [Serializable]
    [TypeConverter(typeof(LabelConverter))]
    public class CurveSeries : PMSSeries
    {

        public CurveSeries(Series Aim)
            : base(Aim)
        {
        }
        #region  2011.10.27 私有属性
        private string _BindingField = "";
        private string _BindingYaxis = "";
        private object _Tag;
        private List<string> _seriesDataForAppearance = new List<string>();
        #endregion

        #region 2011.10.27 公有属性
        [Description("绑定字段")]
        [Category("MES报表属性")]
        [Editor(typeof(CurveSeriesEditor), typeof(UITypeEditor))]
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
        [Description("绑定Y轴")]
        [Category("MES报表属性")]
        [Editor(typeof(YaxisEditor), typeof(UITypeEditor))]
        public string BindingYaxis
        {
            get
            {
                return _BindingYaxis;
            }
            set
            {
                _BindingYaxis = value;
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
            get
            {
                return new List<string>();
            }
            set
            {
                _seriesDataForAppearance = value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public object Tag
        {
            get
            {
                return null;
            }
            set
            {
                _Tag = value;
            }
        }
        #endregion

        #region 属性配置器
        class CurveSeriesEditor : UITypeEditor
        {
            public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                IWindowsFormsEditorService editorService = null;
                if (null != context && null != context.Instance)
                {
                    CurveSeries pcc = context.Instance as CurveSeries;
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
                                            if (pcc._seriesDataForAppearance != null && !pcc._seriesDataForAppearance.Contains(pdb.RecordField))
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
                    else
                    {
                        MessageBox.Show(Properties.Resources.ResourceManager.GetString("message0004"));
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
        class YaxisEditor : UITypeEditor
        {
            public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                IWindowsFormsEditorService editorService = null;
                if (null != context && null != context.Instance)
                {
                    CurveSeries pcc = context.Instance as CurveSeries;
                    if (null != pcc && null != pcc._Tag && pcc._Tag is TreeNode)
                    {
                        editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                        if (editorService != null)
                        {
                            YaxisChoose rfc = new YaxisChoose(editorService);

                            rfc.CurrentYaxis = pcc.BindingYaxis;
                            List<string> lp = new List<string>();
                            if ((pcc._Tag as TreeNode).Parent != null && (pcc._Tag as TreeNode).Parent.Parent!=null && (pcc._Tag as TreeNode).Parent.Parent.Nodes[Properties.Resources.ResourceManager.GetString("context0015")] != null)
                            {
                                TreeNode temp = (pcc._Tag as TreeNode).Parent.Parent.Nodes[Properties.Resources.ResourceManager.GetString("context0015")];
                                if (temp.Nodes != null && temp.Nodes.Count > 0)
                                {
                                    foreach (TreeNode tempnode in temp.Nodes)
                                    {
                                        if (!lp.Contains(tempnode.Name))
                                        {
                                            lp.Add(tempnode.Name);
                                        }
                                    }
                                }
                            }
                            rfc.Yaxis = lp;
                            editorService.DropDownControl(rfc);
                            if (!string.IsNullOrEmpty(rfc.CurrentYaxis))
                            {
                                value = rfc.CurrentYaxis;
                            }
                            return value;
                        }
                    }
                    else
                    {
                        MessageBox.Show(Properties.Resources.ResourceManager.GetString("message0004"));
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
