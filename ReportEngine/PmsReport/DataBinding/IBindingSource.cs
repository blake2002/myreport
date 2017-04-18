using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMS.Libraries.Report.DataBinding
{
    /// <summary>
    /// 绑定数据源接口
    /// </summary>
    public interface IBindingSource
    {
        /// <summary>
        /// 数据源
        /// </summary>
        IDataSource DataSource { get; set; }

    }
}
