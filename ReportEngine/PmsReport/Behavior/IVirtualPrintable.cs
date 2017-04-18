using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PMS.Libraries.ToolControls.Report.Element
{
    public interface IVirtualPrintable
    {
        float VisualWidth
        {
            get;
        }
        float VisualHeight
        {
            get;
        }
        float Print(Canvas g, float x, float y, float viewWidth, float pageHeight,float pageHeaderHeight,bool isSplit, bool firstShow,bool isPrintable);
        float TestNotPrintSize(float startY, float pageHeight, float pageHeaderHeight, float drawedHeightInReport, bool firstShow);
    }
}
