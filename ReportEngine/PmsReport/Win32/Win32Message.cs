using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMS.Libraries.ToolControls.Report.Win32
{
    public class Win32Message
    {
        public const int WM_ERASEBKGND = 0x0014;
        public const int WM_PAINT  = 0x000F;
        public const int WM_LBUTTONUP = 0x0202;
        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_MOUSEMOVE = 0x0200;
        public const int MK_SHIFT = 0x0004;
        public const int MK_CONTROL = 0x0008;
        public const int WM_SIZE = 0x0005;
        public const int WM_NCHITTEST = 0x0084;
        public const int WM_NCLBUTTONUP = 0x00A2;
    }
}
