using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MESReportRunnerShell
{
    static class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">
        /// args[0]--报表设计文件路径
        /// args[1]--数据库参数设置
        /// args[2]--设置报表变量值
        /// </param>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            PMS.Libraries.ToolControls.PMSPublicInfo.PMSCultureInfo cultureInfo = new PMS.Libraries.ToolControls.PMSPublicInfo.PMSCultureInfo();
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo.GetPMSCultureinfo();

            Application.Run(new MESReportRunnerShellForm(args));
        }
    }
}
