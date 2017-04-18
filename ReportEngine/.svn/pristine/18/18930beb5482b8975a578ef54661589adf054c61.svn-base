using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel;

namespace PMS.Libraries.ToolControls.PMSChart
{
    public class KeywordsRegistry : IServiceProvider
    {
        // Fields
        internal ArrayList registeredKeywords = new ArrayList();

        // Methods
        public KeywordsRegistry()
        {
            this.RegisterKeywords();
        }

        public void Register(string name, string keyword, string keywordAliases, string description, string appliesToTypes, string appliesToProperties, bool supportsFormatting, bool supportsValueIndex)
        {
            KeywordInfo info = new KeywordInfo(name, keyword, keywordAliases, description, appliesToTypes, appliesToProperties, supportsFormatting, supportsValueIndex);
            this.registeredKeywords.Add(info);
        }

        private void RegisterKeywords()
        {
            string appliesToProperties = "Text,Label,LabelMapAreaAttributes,ToolTip,Url,LabelToolTip,MapAreaAttributes,AxisLabel,LegendToolTip,LegendMapAreaAttributes,LegendUrl,LegendText";
            this.Register("该数据点的索引", "#INDEX", string.Empty, "表示序列中的数据点索引。\n\n该数据点索引从零开始，可用于唯一标识序列中的点。", "DataPoint", appliesToProperties, false, false);
            this.Register("X 值", "#VALX", string.Empty, "表示序列数据点的 X 值。\n\n如果序列只填充了 Y 值，则所有 X 值点都设置为零。", "Series,DataPoint,Annotation,LegendCellColumn", appliesToProperties, true, false);
            this.Register("Y 值", "#VAL", string.Empty, "表示序列数据点的 Y 值。\n\n如果图表类型支持多个 Y 值，则可以指定应使用的 Y 值。", "Series,DataPoint,Annotation,LegendCellColumn,LegendCellColumn", appliesToProperties, true, true);
            this.Register("Y 值总计", "#TOTAL", string.Empty, "表示序列中所有 Y 值总计（总和）。\n\n如果图表类型支持多个 Y 值，则可以指定应使用的 Y 值。", "Series,DataPoint,Annotation,LegendCellColumn", appliesToProperties, true, false);
            this.Register("作为总计百分比的 Y 值", "#PERCENT", string.Empty, "将数据点 Y 值表示为序列中所有 Y 值的百分比。", "Series,DataPoint,Annotation,LegendCellColumn", appliesToProperties, true, true);
            this.Register("该数据点的索引", "#INDEX", string.Empty, "表示序列中的数据点索引。\n\n该数据点索引从零开始，可用于唯一标识序列中的点。", "Series,DataPoint,Annotation,LegendCellColumn", appliesToProperties, false, false);
            this.Register("数据点的标签", "#LABEL", string.Empty, "表示数据点的标签。\n\n此关键字将替换为关联数据点的 Label 属性的值。", "Series,DataPoint,Annotation,LegendCellColumn", appliesToProperties, false, false);
            this.Register("数据点的轴标签", "#AXISLABEL", string.Empty, "表示数据点的轴标签。\n\n此关键字将替换为关联数据点的 AxisLabel 属性的值。", "Series,DataPoint,Annotation,LegendCellColumn", appliesToProperties, false, false);
            this.Register("图例文本", "#LEGENDTEXT", string.Empty, "表示图例文本。\n\n此关键字将替换为关联对象的 LegendText 属性的值。", "Series,DataPoint,Annotation,LegendCellColumn", appliesToProperties, false, false);
            this.Register("序列名称", "#SERIESNAME", "#SER", "表示序列名称。\n\n此关键字将替换为关联序列 Name 属性。", "Series,DataPoint,Annotation,LegendCellColumn", appliesToProperties, false, false);
            this.Register("Y 值的平均值", "#AVG", string.Empty, "表示序列中所有 Y 值的平均值。\n\n如果图表类型支持多个 Y 值，则可以指定应使用的 Y 值。", "Series,DataPoint,Annotation,LegendCellColumn", appliesToProperties, true, true);
            this.Register("Y 值的最大值", "#MAX", string.Empty, "表示序列中所有 Y 值的最大值。\n\n如果图表类型支持多个 Y 值，则可以指定应使用的 Y 值。", "Series,DataPoint,Annotation,LegendCellColumn", appliesToProperties, true, true);
            this.Register("Y 值的最小值", "#MIN", string.Empty, "表示序列中所有 Y 值的最小值。\n\n如果图表类型支持多个 Y 值，则可以指定应使用的 Y 值。", "Series,DataPoint,Annotation,LegendCellColumn", appliesToProperties, true, true);
            this.Register("最后一个点的 Y 值", "#LAST", string.Empty, "表示序列中最后一个点的 Y 值。\n\n如果图表类型支持多个 Y 值，则可以指定应使用的 Y 值。", "Series,DataPoint,Annotation,LegendCellColumn", appliesToProperties, true, true);
            this.Register("第一个点的 Y 值", "#FIRST", string.Empty, "表示序列中第一个点的 Y 值。\n\n如果图表类型支持多个 Y 值，则可以指定应使用的 Y 值。", "Series,DataPoint,Annotation,LegendCellColumn", appliesToProperties, true, true);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        object IServiceProvider.GetService(Type serviceType)
        {
            if (serviceType != typeof(KeywordsRegistry))
            {
                throw new ArgumentException(/*SR.*/"ExceptionKeywordsRegistryUnsupportedType(serviceType.ToString())");
            }
            return this;
        }
    }
}
