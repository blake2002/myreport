using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PMS.Libraries.ToolControls.Report.Elements.Util
{
    public class ColorUtil
    {
        public static int CovertColorToInt(Color color)
        {
            return (((int)color.R) | ((int)color.G << 8) | ((int)color.B << 16)) | ((int)color.A << 24);
        }
        public static Color ConvertIntToColor(int colorInt)
        {
            Color color = Color.FromArgb(colorInt);
            return color;
        }
    }
}
