using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace PMS.Libraries.ToolControls.Report.Controls.EditorDialog
{
    public interface IBorder
    {
        bool HasTopBorder
        {
            get;
            set;
        }

        bool HasLeftBorder
        {
            get;
            set;
        }

        bool HasBottomBorder
        {
            get;
            set;
        }

        bool HasRightBorder
        {
            get;
            set;
        }

        DashStyle DashStyle
        {
            get;
            set;
        }

        Color BorderColor
        {
            get;
            set;
        }
    }
}
