using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMS.Libraries.ToolControls.Report.Element
{
    public interface IResizable
    {
        /// <summary>
        /// 横向比例因子
        /// </summary>
        float HorizontalScale { get; set; }

        /// <summary>
        /// 纵向比例因子
        /// </summary>
        float VerticalScale { get; set; }

        /// <summary>
        /// 缩放
        /// </summary>
        void Zoom();
        /// <summary>
        /// 按照指定的因子比例缩放
        /// </summary>
        /// <param name="hScale">横向比例</param>
        /// <param name="vScale">纵向比例</param>
        void Zoom(float hScale, float vScale);
    }
}
