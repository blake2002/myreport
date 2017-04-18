using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using System.ComponentModel.Design;

namespace PMS.Libraries.ToolControls.PMSChart
{
    public class TrendCurve : CurveChart, ITrendCurve
    {
        private TrendLabel _LabelSource;
        [Category("MES报表属性")]
        [Description("设置特殊标签")]
        [Editor(typeof(TrendEditor), typeof(UITypeEditor))]
        public TrendLabel LabelSource
        {
            get
            {
                return _LabelSource;
            }
            set
            {
                _LabelSource = value;
            }
        }
        public TrendCurve()
        {
        }
        public TrendCurve(PMSChartCtrl Aim)
        {
            if (Aim != null)
            {
                if (Aim.SourceField != null)
                    this.SourceField = Aim.SourceField.Clone();
                this.IsReport = this.IsReport;
                if (Aim.SelectRecordFields != null)
                {
                    this.SelectRecordFields = new List<string>();

                    foreach (string node in Aim.SelectRecordFields)
                    {
                        this.SelectRecordFields.Add(node);
                    }
                }
                this.SqlSource = Aim.SqlSource.Clone();
                this.XRecordField = Aim.XRecordField;
                this.RunMode = Aim.RunMode;
                this.Location = new Point(Aim.Location.X, Aim.Location.Y);
                //this.OriginPosition = new Point(Aim.Location.X, Aim.Location.Y);
                this.Height = Aim.Height;
                this.Width = Aim.Width;
                this.Visible = Aim.Visible;
                //this.chart1.BorderlineColor = Aim.chart1.BorderlineColor;
                //this.chart1.BorderlineDashStyle = Aim.chart1.BorderlineDashStyle;
                //this.chart1.BorderlineWidth = Aim.chart1.BorderlineWidth;
            }
        }
        #region  2011.12.8 增加 实现悬浮工具栏效果
        public override MES.Controls.Design.SuspensionItem[] ListSuspensionItems()
        {
            MES.Controls.Design.SuspensionItem[] result = new MES.Controls.Design.SuspensionItem[base.ListSuspensionItems().Length + 1];
            MES.Controls.Design.SuspensionItem[] temp = base.ListSuspensionItems();
            for (int i = 0; i < temp.Length; i++)
            {
                result[i] = temp[i];
            }
            result[result.Length - 1] = new MES.Controls.Design.SuspensionItem(Properties.Resources.flipvertical, Properties.Resources.ResourceManager.GetString("context0026"), Properties.Resources.ResourceManager.GetString("context0026"), new Action(DealWithTrendCurve));
            return result;
        }
        #endregion
        private void DealWithTrendCurve()
        {
            if (null != this)
            {
                TrendCurve element = this as TrendCurve;
                if (null != element)
                {
                    if (element.Parent == null ||
                        (element.Parent != null && element.Parent as PMS.Libraries.ToolControls.Report.Element.IPmsReportDataBind == null))
                    {
                        PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField sfAll = (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetCurrentReportDataDefine()) as PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField;
                        //SourceBindDialog fbd = new SourceBindDialog(sfAll, element.SourceField);
                        TrendCurveApperence fbd = new TrendCurveApperence();
                        fbd.LabelSource = element.LabelSource;
                        fbd.ChartParent = element;
                        if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            LabelSource = fbd.LabelSource;
                            if (null != Site)
                            {
                                IComponentChangeService cs = Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                                if (null != cs)
                                {
                                    cs.OnComponentChanged(this, null, null, null);
                                }
                            }
                        }
                    }
                    else
                    {
                        TrendCurveApperence fbd = new TrendCurveApperence(GetSourceField(element.Parent as PMS.Libraries.ToolControls.Report.Element.IElement));
                        fbd.LabelSource = element.LabelSource;
                        fbd.ChartParent = element;
                        if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            LabelSource = fbd.LabelSource;
                            if (null != Site)
                            {
                                IComponentChangeService cs = Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                                if (null != cs)
                                {
                                    cs.OnComponentChanged(this, null, null, null);
                                }
                            }
                        }
                    }
                }
            }
        }
        private PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField GetSourceField(PMS.Libraries.ToolControls.Report.Element.IElement element)
        {
            if (null == element)
            {
                return null;
            }
            PMS.Libraries.ToolControls.Report.Element.IPmsReportDataBind parent = element as PMS.Libraries.ToolControls.Report.Element.IPmsReportDataBind;
            if (null == parent)
            {
                return null;
            }
            if (null == parent.SourceField)
            {
                return GetSourceField(element.Parent as PMS.Libraries.ToolControls.Report.Element.IElement);
            }

            return parent.SourceField;
        }
        public override object Clone()
        {
            TrendCurve result = new TrendCurve(base.Clone() as PMSChartCtrl);
            if (this.LabelSource != null)
            {
                result.LabelSource = this.LabelSource.Clone() as TrendLabel;
            }
            return result;
        }
    }
    [Serializable]
    public class TrendLabel
    {
        private StringAlignment _TextAlignment = StringAlignment.Center;
        private StringAlignment _TextLineAlignment = StringAlignment.Center;
        private System.Windows.Forms.DataVisualization.Charting.TextOrientation _TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Horizontal;
        private Color _LineColor = Color.Red;
        private int _LineWidth = 2;

        public PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField TrendLabelSource
        {
            get;
            set;
        }
        public System.Data.DataTable Source
        {
            get;
            set;
        }
        public List<FieldLabel> FieldFlag
        {
            get;
            set;
        }
        public StringAlignment TextLineAlignment
        {
            get
            {
                return _TextLineAlignment;
            }
            set
            {
                _TextLineAlignment = value;
            }
        }
        public System.Windows.Forms.DataVisualization.Charting.TextOrientation TextOrientation
        {
            get
            {
                return _TextOrientation;
            }
            set
            {
                _TextOrientation = value;
            }
        }
        public StringAlignment TextAlignment
        {
            get
            {
                return _TextAlignment;
            }
            set
            {
                _TextAlignment = value;
            }
        }
        public Color LineColor
        {
            get
            {
                return _LineColor;
            }
            set
            {
                _LineColor = value;
            }
        }
        [DefaultValue(2)]
        public int LineWidth
        {
            get
            {
                return _LineWidth;
            }
            set
            {
                _LineWidth = value;
            }
        }
        public virtual object Clone()
        {
            TrendLabel pcc = new TrendLabel();
            if (this.TrendLabelSource != null)
                pcc.TrendLabelSource = this.TrendLabelSource.Clone();
            pcc.Source = this.Source;
            pcc.TextAlignment = this.TextAlignment;
            pcc.LineColor = this.LineColor;
            pcc.LineWidth = this.LineWidth;
            pcc._TextLineAlignment = this._TextLineAlignment;
            pcc._TextOrientation = this._TextOrientation;
            if (this.FieldFlag != null)
            {
                FieldLabel[] bb = new FieldLabel[this.FieldFlag.Count];
                this.FieldFlag.CopyTo(bb);
                pcc.FieldFlag = new List<FieldLabel>(bb);
            }
            return pcc;
        }
        public override string ToString()
        {
            return Properties.Resources.ResourceManager.GetString("context0026");
        }
        #region 公开方法
        /// <summary>
        /// 2012.06.28 重写
        /// 目的:定义自定义类的比较方法
        /// </summary>
        /// <param name="obj">要比较的对象</param>
        /// <returns>返回比较结果</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            if (System.Object.ReferenceEquals(this, obj))
            {
                return true;
            }
            return (this.TrendLabelSource == (obj as TrendLabel).TrendLabelSource) && (this.Source == (obj as TrendLabel).Source) && (this.FieldFlag == (obj as TrendLabel).FieldFlag) && (this.TextAlignment == (obj as TrendLabel).TextAlignment) && (this.TextLineAlignment == (obj as TrendLabel).TextLineAlignment) && (this.TextOrientation == (obj as TrendLabel).TextOrientation);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        /// <summary>
        /// 2012.06.28 重写
        /// 目的:重写自定义类的等于操作符的比较方法
        /// </summary>
        /// <param name="c1">要比较的对象</param>
        /// <param name="c2">比较对象</param>
        /// <returns>返回比较结果</returns>
        public static bool operator ==(TrendLabel c1, TrendLabel c2)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(c1, c2))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)c1 == null) || ((object)c2 == null))
            {
                return false;
            }
            return (c1.TrendLabelSource == c2.TrendLabelSource) && (c1.Source == c2.Source) && (c1.FieldFlag == c2.FieldFlag) && (c1.TextAlignment == c2.TextAlignment) && (c1.TextLineAlignment == c2.TextLineAlignment) && (c1.TextOrientation == c2.TextOrientation);
        }
        /// <summary>
        /// 2012.06.28 重写
        /// 目的:重写自定义类的不等于操作符的比较方法
        /// </summary>
        /// <param name="c1">要比较的对象</param>
        /// <param name="c2">比较对象</param>
        /// <returns>返回比较结果</returns>
        public static bool operator !=(TrendLabel c1, TrendLabel c2)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(c1, c2))
            {
                return false;
            }

            // If one is null, but not both, return false.
            if (((object)c1 == null) || ((object)c2 == null))
            {
                return true;
            }
            return (c1.TrendLabelSource != c2.TrendLabelSource) || (c1.Source != c2.Source) || (c1.FieldFlag != c2.FieldFlag) || (c1.TextAlignment != c2.TextAlignment) || (c1.TextLineAlignment != c2.TextLineAlignment) || (c1.TextOrientation != c2.TextOrientation);
        }
        #endregion
    }
    [Serializable]
    public class FieldLabel
    {
        /// <summary>
        /// 当前操作的数据字段
        /// </summary>
        public PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField CurrentField
        {
            get;
            set;
        }
        /// <summary>
        /// 数据字段是否被标记
        /// </summary>
        public bool IsFlag
        {
            get;
            set;
        }
        /// <summary>
        /// 是否作为X轴标记字段
        /// </summary>
        public bool IsMainField
        {
            get;
            set;
        }
        /// <summary>
        /// 字段格式化
        /// </summary>
        public string Format
        {
            get;
            set;
        }
        public virtual object Clone()
        {
            FieldLabel pcc = new FieldLabel();
            if (this.CurrentField != null)
                pcc.CurrentField = this.CurrentField.Clone();
            pcc.IsFlag = this.IsFlag;
            pcc.IsMainField = this.IsMainField;
            pcc.Format = this.Format;
            return pcc;
        }
    }
    #region 属性配置界面
    internal class TrendEditor : UITypeEditor
    {
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (null != context && null != context.Instance && null != context.Container)
            {
                TrendCurve element = context.Instance as TrendCurve;
                if (null != element)
                {
                    if (element.Parent == null ||
                       (element.Parent != null && element.Parent as PMS.Libraries.ToolControls.Report.Element.IPmsReportDataBind == null))
                    {
                        PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField sfAll = (PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetCurrentReportDataDefine()) as PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField;
                        //SourceBindDialog fbd = new SourceBindDialog(sfAll, element.SourceField);
                        //PMS.Libraries.ToolControls.Report.Controls.EditorDialog.SourceBindDialog fbd = new PMS.Libraries.ToolControls.Report.Controls.EditorDialog.SourceBindDialog(sfAll, element.SourceField, true);
                        TrendCurveApperence fbd = new TrendCurveApperence();
                        fbd.LabelSource = element.LabelSource;
                        fbd.ChartParent = element;
                        if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            value = fbd.LabelSource;
                        }
                    }
                    else
                    {
                        //PMS.Libraries.ToolControls.Report.Controls.EditorDialog.SourceBindDialog fbd = new PMS.Libraries.ToolControls.Report.Controls.EditorDialog.SourceBindDialog(GetSourceField(element.Parent as IElement), element.SourceField, true);
                        TrendCurveApperence fbd = new TrendCurveApperence(GetSourceField(element.Parent as PMS.Libraries.ToolControls.Report.Element.IElement));
                        fbd.LabelSource = element.LabelSource;
                        fbd.ChartParent = element;
                        if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            value = fbd.LabelSource;
                        }
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
        private PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.SourceField GetSourceField(PMS.Libraries.ToolControls.Report.Element.IElement element)
        {
            if (null == element)
            {
                return null;
            }
            PMS.Libraries.ToolControls.Report.Element.IPmsReportDataBind parent = element as PMS.Libraries.ToolControls.Report.Element.IPmsReportDataBind;
            if (null == parent)
            {
                return null;
            }
            if (null == parent.SourceField)
            {
                return GetSourceField(element.Parent as PMS.Libraries.ToolControls.Report.Element.IElement);
            }

            return parent.SourceField;
        }
    }
    #endregion

}
