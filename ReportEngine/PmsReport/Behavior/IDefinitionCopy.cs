using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMS.Libraries.ToolControls.Report.Element
{
    public interface IDefinitionCopy<T>
    {
        /// <summary>
        /// 拷贝
        /// </summary>
        /// <returns>拷贝后的结果</returns>
        T Copy();
    }
}
