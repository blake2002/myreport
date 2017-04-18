using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMS.Libraries.ToolControls.Report.Win32;

namespace PMS.Libraries.ToolControls.Report.Event
{
    public class Win32Delagate
    {
        public delegate void EraseBackGround(object o, EraseBkgArgs args);

        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);
    }
}
