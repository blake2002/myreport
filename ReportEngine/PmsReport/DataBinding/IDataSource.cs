using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

namespace PMS.Libraries.Report.DataBinding
{
    /// <summary>
    /// 数据源接口，使用该接口是为了方便桥接
    /// 当用户绑定了数据是DataSet，写一个转换类，将DataSet转换符合该接口的类
    /// 当用户绑定的一个实体类，也只需要写一个转换类
    /// 将报表底层封装后，可以将绑定暴露为Object类型
    /// 这样就可以方便以后扩展数据源类型
    /// 因为在用户使用层将看不到IDataSource,并且也是个标识接口
    /// </summary>
    public interface IDataSource:IEnumerator<IDataRow>
    {
        /// <summary>
        /// 数据源的列
        /// </summary>
        IDataColumn Column {get;}
        /// <summary>
        /// 判断是否包含指定成员名的成员
        /// </summary>
        /// <param name="dataMemberName">成员名</param>
        /// <returns>存在true,否则false</returns>
        bool Contains(string dataMemberName);

        /// <summary>
        /// 获取当前指定成员名的数据
        /// </summary>
        /// <param name="dataMemberName">成员名</param>
        /// <returns>数据</returns>
        object GetData(string dataMemberName);

        /// <summary>
        /// 新数据行，得当当前数据架构的拷贝
        /// </summary>
        /// <returns></returns>
        IDataRow NewRow();

        /// <summary>
        /// 添加数据行
        /// </summary>
        /// <param name="row">行对象</param>
        void AddRow(IDataRow row);

        /// <summary>
        /// 删除指定行
        /// <remarks>当索引越界时候将抛出ArgumentException</remarks>
        /// </summary>
        /// <param name="rowIndex">行索引</param>
        void RemoveRow(int rowIndex);

        /// <summary>
        /// 删除所有行
        /// </summary>
        void RemoveAllRow();

        /// <summary>
        /// 获取指定行
        /// <remarks>如果索引越界抛出ArgumentException</remarks>
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>返回行</returns>
        IDataRow GetRow(int index);

        /// <summary>
        /// 数据行
        /// </summary>
        int Count { get; }
    }
}
