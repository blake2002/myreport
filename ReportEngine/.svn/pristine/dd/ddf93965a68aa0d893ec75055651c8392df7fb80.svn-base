using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;

namespace PMS.Libraries.ToolControls.Report.Element
{
    public interface IBindDataTableManager
    {
        /// <summary>
        /// 绑定数据管理器设置
        /// </summary>
        /// <param name="dtm">数据管理器引用</param>
        /// <param name="bindPath">绑定数据集的绝对路径(eg:tb1[1].tb[3].tb3[1]),路径不包含最后的字段信息，如果是表格控件此参数无意义</param>
        /// <returns>
        /// 1.如果是表格返回绑定后需要绘制的总行数，不进行实质性插入操作
        /// 2.如果是其他控件做绑值处理
        /// </returns>
        int BindDataTableManager(IDataTableManager dtm, string bindPath);
    }
}
