using System;
using System.ComponentModel;
using System.Drawing;

namespace PMS.Libraries.ToolControls.PMSChart
{
    [Serializable]
    public class RadarClassify
    {
        private bool enable = true;
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get { return enable; } set { enable = value; } }
        /// <summary>
        /// 分类标签
        /// </summary>
        public string ClassifyLabel { get; set; }
        /// <summary>
        /// 实际值
        /// </summary>
        public string ClassifyValue { get; set; }
        /// <summary>
        /// 上限
        /// </summary>
        public string ClassifyMaxValue { get; set; }
        /// <summary>
        /// 下限
        /// </summary>
        public string ClassifyMinValue { get; set; }

        public RadarClassify Clone()
        {
            RadarClassify rc = new RadarClassify();
            rc.Enable = Enable;
            rc.ClassifyLabel = ClassifyLabel;
            rc.ClassifyValue = ClassifyValue;
            rc.ClassifyMaxValue = ClassifyMaxValue;
            rc.ClassifyMinValue = ClassifyMinValue;
            return rc;
        }
    }
}

