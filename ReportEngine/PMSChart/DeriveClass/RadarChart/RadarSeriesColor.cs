using System;
using System.Drawing;

namespace PMS.Libraries.ToolControls.PMSChart
{
    [Serializable]
    public class RadarSeriesColor
    {
        /// <summary>
        /// 实际值曲线名称
        /// </summary>
        public string ClassifyMainName { get; set; }
        /// <summary>
        /// 上限曲线名称
        /// </summary>
        public string ClassifyMaxName { get; set; }
        /// <summary>
        /// 下限曲线名称
        /// </summary>
        public string ClassifyMinName { get; set; }
        /// <summary>
        /// 实际值曲线颜色
        /// </summary>
        public Color ClassifyMainColor { get; set; }
        /// <summary>
        /// 上限曲线颜色
        /// </summary>
        public Color ClassifyMaxColor { get; set; }
        /// <summary>
        /// 下限曲线颜色
        /// </summary>
        public Color ClassifyMinColor { get; set; }

        public RadarSeriesColor Clone()
        {
            RadarSeriesColor rc = new RadarSeriesColor();
            rc.ClassifyMainName = ClassifyMainName;
            rc.ClassifyMaxName = ClassifyMaxName;
            rc.ClassifyMinName = ClassifyMinName;
            rc.ClassifyMainColor = ClassifyMainColor;
            rc.ClassifyMaxColor = ClassifyMaxColor;
            rc.ClassifyMinColor = ClassifyMinColor;
            return rc;
        }
    }
}
