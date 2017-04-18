using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using PMS.Libraries.ToolControls.Report.Element;

namespace PMS.Libraries.ToolControls.Report.Element
{
    /// <summary>
    /// 控件直接绘制接口(新版本引擎使用)
    /// 2013-06-26 add by luyj
    /// 添加dpiZoom参数为了解决线框问题（2013-07-16 modified by luyj）
    /// </summary>
    public interface IDirectDrawable
    {
        /// <summary>
        /// 根据给定的画布和开始坐标绘制报表控件
        /// </summary>
        /// <param name="ca">画布</param>
        /// <param name="x">横坐标位置的偏移值</param>
        /// <param name="y">纵坐标位置的偏移值</param>
        /// <param name="dpiZoom">设计时dpix与最终绘图所在介质dpix的比例</param>
        void DirectDraw(Canvas ca, float x, float y, float dpiZoom = 1);
    }
}
