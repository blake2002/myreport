using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;

namespace PMS.Libraries.ToolControls.PmsSheet.PmsPublicData
{
    /// <summary>
    /// 报表数据管理器接口
    /// </summary>
    public interface IDataTableManager
    {
        #region 数据表管理
        /// <summary>
        /// 获取数据表
        /// </summary>
        /// <param name="tableName">待返回的数据表名</param>
        /// <returns></returns>
        DataTable GetDataTable(string tableName);
        /// <summary>
        /// 查找数据表
        /// </summary>
        /// <param name="tableName">待查找的数据表名</param>
        /// <returns></returns>
        bool FindDataTable(string tableName);

        #endregion

        #region 系统变量管理

        /// <summary>
        /// 变量表
        /// </summary> 
        DataTable VariableDataTable { get; }
        /// <summary>
        /// 获取变量object类型值
        /// </summary>
        /// <param name="varName"></param>
        /// <returns>变量值（object类型）</returns>
        object GetVariableObjValue(string varName);
        /// <summary>
        /// 获取变量字符串类型值
        /// </summary>
        /// <param name="varName"></param>
        /// <returns>变量值（字符串类型）</returns>
        string GetVariableStringValue(string varName);
        #endregion

        #region XmlDataDocument 管理

        /// <summary>
        /// 数据集关联的XmlDataDocument
        /// </summary>
        XmlDataDocument DataSetXmlDataDocument { get; }

        #endregion

        #region 数据表操作
        /// <summary>
        /// 从数据表获取某行某列的 object类型 值
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="rowIndex">行号</param>
        /// <param name="colName">列名</param>
        /// <returns></returns>
        object GetDataFromTable(string tableName, int rowIndex, string colName, string filter);
        /// <summary>
        /// 从数据表获取某行某列的 字符串类型 值
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="rowIndex">行号</param>
        /// <param name="colIndex">列号</param>
        /// <returns></returns>
        object GetDataFromTable(string tableName, int rowIndex, int colIndex);
        /// <summary>
        /// 从数据表获取某行某列的 字符串类型 值
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="rowIndex">行号</param>
        /// <param name="colName">列名</param>
        /// <returns></returns>
        string GetStringDataFromTable(string tableName, int rowIndex, string colName);
        /// <summary>
        /// 从数据表获取某行某列的 字符串类型 值
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="rowIndex">行号</param>
        /// <param name="colIndex">列号</param>
        /// <returns></returns>
        string GetStringDataFromTable(string tableName, int rowIndex, int colIndex);
        /// <summary>
        /// 根据路径取值
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        object GetDataByPath(string path, string filter = null);
        /// <summary>
        /// 添加数据表
        /// </summary>
        /// <param name="newDataTable">新增数据表</param>
        /// <param name="bMerge">如果数据表已经存在的情况下，将新增数据表合并到已存在的数据表中</param>
        /// <returns>添加成功则返回true</returns>
        bool AddDataTable(DataTable newDataTable, bool bMerge);
        /// <summary>
        /// 获取数据管理类中的所有表集合
        /// </summary>
        /// <returns></returns>
        DataTableCollection GetDataTables();

        #endregion

        #region 数据表名操作
        /// <summary>
        /// 获取从表名称
        /// </summary>
        /// <param name="tableName">从表名称</param>
        /// <param name="parentTableName">主表名称</param>
        /// <param name="parentTableRowIndex">从表关联的主表数据表行号</param>
        /// <returns></returns>
        string GetSubTableName(string tableName, string parentTableName, int parentTableRowIndex);

        #endregion
    }

}
