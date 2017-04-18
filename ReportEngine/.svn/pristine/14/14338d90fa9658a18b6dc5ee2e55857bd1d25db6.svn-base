using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using PMS.Libraries.ToolControls.Report.Element;

namespace PMS.Libraries.Report.DataBinding
{

    public interface IDataColumn : IDefinitionCopy<IDataColumn>, ICompatible<IDataColumn>
    {
        
        /// <summary>
        /// 列的索引
        /// </summary>
        /// <param name="colName">列的索引</param>
        /// <returns>列存在返回索引,不存在返回-1</returns>
        int GetIndexByName(string colName);
        /// <summary>
        /// 添加列
        /// <remarks>若该列名已经存在则抛出ArgumentException异常</remarks>
        /// </summary>
        /// <param name="colName">列名</param>
        void Add(string colName);
        /// <summary>
        /// 删除指定列
        /// <remarks>若该列名不存在则抛出ArgumentException异常</remarks>
        /// </summary>
        /// <param name="colName">列名</param>
        void Remove(string colName);
        /// <summary>
        /// 返回当前的列名
        /// </summary>
        string[] Columns { get; }
        /// <summary>
        ///总列数
        /// </summary>
        int Count { get; }
    }
}
