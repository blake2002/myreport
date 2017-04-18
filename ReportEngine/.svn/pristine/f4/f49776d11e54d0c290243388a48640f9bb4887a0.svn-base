using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using PMS.Libraries.ToolControls.Report.Element;

namespace PMS.Libraries.ToolControls.Report.Element
{
    /// <summary>
    /// 可印刷及显示接口
    /// </summary>
    public interface IPrintable
    {
        /// <summary>
        /// 打印或者显示在指定画布,该方法的调用用在显示报表
        /// <remarks>使用x,y偏移量是为了绘图转换坐标系方便</remarks>
        /// </summary>
        /// <param name="g">绘图图面,拥有该参数是为了可以灵活的转移图形的输出画面</param>
        /// <param name="x">横坐标位置的偏移值</param>
        /// <param name="y">纵坐标位置的偏移值</param>
        void Print(Canvas ca, float x, float y);
    }
}
