using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;

namespace PMS.Libraries.ToolControls.Report.Element
{
    public interface IPmsReportDataBind
    {
        /// <summary>
        /// 数据源
        /// </summary>
        SourceField SourceField { get; set; }
    }
}
