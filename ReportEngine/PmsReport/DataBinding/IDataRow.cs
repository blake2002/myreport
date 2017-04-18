using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using PMS.Libraries.ToolControls.Report.Element;

namespace PMS.Libraries.Report.DataBinding
{
    public interface IDataRow : IEnumerable, IEnumerator, IDefinitionCopy<IDataRow>,ICompatible<IDataRow>
    {
        /// <summary>
        /// 行拥有的列
        /// </summary>
        IDataColumn Column { get; }
        /// <summary>
        /// 为指定列添加值
        /// </summary>
        /// <param name="colName">列名</param>
        /// <param name="value">值</param>
        void SetValue(string colName, object value);
        /// <summary>
        /// 为指定列添加值
        /// </summary>
        /// <param name="index">列索引</param>
        /// <param name="value">值</param>
        void SetValue(int index, object value);
        /// <summary>
        /// 获得指定列的值
        /// </summary>
        /// <param name="colName">列名</param>
        /// <returns>值</returns>
        object GetValue(string colName);
        /// <summary>
        /// 获得指定列的值
        /// </summary>
        /// <param name="colName">列索引</param>
        /// <returns>值</returns>
        object GetValue(int index);
    }
}
