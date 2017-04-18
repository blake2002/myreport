using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
//using PMS.Libraries.ToolControls.ToolBox;
using Microsoft.SqlServer.Management.Smo;
using PMS.Libraries.ToolControls.PMSPublicInfo;

namespace PMS.Libraries.ToolControls.PMSChart
{
    class PublicFunctionClass
    {
        public static bool IsDateTimePart(string field)
        {
            if (field.EndsWith(".Year", StringComparison.CurrentCultureIgnoreCase) ||
                field.EndsWith(".Month", StringComparison.CurrentCultureIgnoreCase) ||
                field.EndsWith(".Day", StringComparison.CurrentCultureIgnoreCase) ||
                field.EndsWith(".Hour", StringComparison.CurrentCultureIgnoreCase) ||
                field.EndsWith(".Minute", StringComparison.CurrentCultureIgnoreCase) ||
                field.EndsWith(".Second", StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            else
                return false;
        }
        public static string GetTimePartSqlField(int dbType, string strVar)
        {
            if (strVar.Length == 0)
                return "";
            string sCondition = "";
            if (strVar.EndsWith(".Year", StringComparison.CurrentCultureIgnoreCase))
            {
                strVar = strVar.Replace(".Year", "");

                if (dbType == 1)
                {
                    sCondition = string.Format("Year({0})", strVar);
                }
                else if (dbType == 0)
                {
                    sCondition = string.Format("Year({0})", strVar);
                }
            }
            else if (strVar.EndsWith(".Month", StringComparison.CurrentCultureIgnoreCase))
            {
                strVar = strVar.Replace(".Month", "");

                if (dbType == 1)//NETSCADA_DATABASE_TYPE_SQL)
                {
                    sCondition = string.Format("Month({0})", strVar);
                }
                else if (dbType == 0)//NETSCADA_DATABASE_TYPE_ACCESS)
                {
                    sCondition = string.Format("Month({0})", strVar);
                }
            }
            else if (strVar.EndsWith(".Day", StringComparison.CurrentCultureIgnoreCase))
            {
                strVar = strVar.Replace(".Day", "");

                if (dbType == 1)
                {
                    sCondition = string.Format("Day({0})", strVar);
                }
                else if (dbType == 0)
                {
                    sCondition = string.Format("Day({0})", strVar);
                }
            }
            else if (strVar.EndsWith(".Hour", StringComparison.CurrentCultureIgnoreCase))
            {
                strVar = strVar.Replace(".Hour", "");

                if (dbType == 1)
                {
                    sCondition = string.Format("DATENAME(hour,{0})", strVar);
                }
                else if (dbType == 0)
                {
                    sCondition = string.Format("Hour({0})", strVar);
                }
            }
            else if (strVar.EndsWith(".Minute", StringComparison.CurrentCultureIgnoreCase))
            {
                strVar = strVar.Replace(".Minute", "");

                if (dbType == 1)
                {
                    sCondition = string.Format("DATENAME(minute,{0})", strVar);
                }
                else if (dbType == 0)
                {
                    sCondition = string.Format("Minute({0})", strVar);
                }
            }
            else if (strVar.EndsWith(".Second", StringComparison.CurrentCultureIgnoreCase))
            {
                strVar = strVar.Replace(".Second", "");

                if (dbType == 1)
                {
                    sCondition = string.Format("DATENAME(second,{0})", strVar);
                }
                else if (dbType == 0)
                {
                    sCondition = string.Format("Second({0})", strVar);
                }
            }
            else
                sCondition = strVar;
            return sCondition;
        }
        /// <summary>
        /// sql 2005下测试
        /// </summary>
        /// <param name="CSType"></param>
        /// <returns></returns>
        public static string CSTypeToPmsDateType(string CSType)
        {
            CSType = CSType.ToLower();
            if (CSType == "int32" || CSType == "int16" || CSType == "int64" ||
                CSType == "decimal" || CSType == "single" || CSType == "double")
            {
                CSType = "REAL";//BIT,DATETIME,VARCHAR
            }
            else if (CSType == "datetime")
            {
                CSType = "DATETIME";
            }
            else if (CSType == "string")
            {
                CSType = "VARCHAR";
            }
            else if (CSType == "guid")
            {
                CSType = "GUID";
            }
            else if (CSType == "boolean")
            {                
                CSType = "BIT";
            }
            return CSType;
        }
        ///获得值等式
        ///nOperateType = 
        ///2 赋值,不带等于号,用于insert
        ///1 赋值,带等于号,用于update
        ///0 比较,用于select,update
        public static string GetValue(string strType, string strValue, int iDBType, int nOperateType)
        {
            string strWhere = "";
            if (strType == "REAL" ||
                strType == "FLOAT" ||
                strType == "INTEGER" ||
                strType == "INT" ||
                strType == "BIGINT")
            {
                if (strValue.Length == 0)
                    strValue = "0";
                strWhere += strValue;
            }
            else if (strType == "BIT")
            {
                if (strValue.ToUpper() == "TRUE" || strValue.ToUpper() == "YES")
                {
                    strWhere += "1";
                }
                else
                {
                    strWhere += "0";
                }
            }
            else if (strType.StartsWith("VARCHAR", StringComparison.CurrentCulture) == true)
            {
                strWhere += "'";
                strWhere += strValue;
                strWhere += "'";
            }
            else if (strType.StartsWith("GUID", StringComparison.CurrentCulture) == true)
            {
                if (strValue.Length == 0)
                    strWhere += "null";
                else
                {
                    strWhere += "'";
                    strWhere += strValue;
                    strWhere += "'";
                }
            }
            else if (strType == "DATETIME")
            {
                if (iDBType == 0)//NETSCADA_DATABASE_TYPE_ACCESS
                {
                    if (strValue.Length == 0)
                        strWhere += " null";
                    else
                    {
                        strWhere += "#";
                        strWhere += strValue;
                        strWhere += "#,";
                    }
                }
                else if (iDBType == 1)//NETSCADA_DATABASE_TYPE_SQL
                {
                    if (strValue.Length == 0)
                        strWhere += " null";
                    else
                    {
                        strWhere += "'";
                        strWhere += strValue;
                        strWhere += "'";
                    }
                }
            }
            else if (strType == "IMAGE")
            {
                if (iDBType == 1)//NETSCADA_DATABASE_TYPE_SQL
                {
                    strWhere += "@" + strValue;
                }
            }
            if (strType == "DATETIME" || (strType.StartsWith("GUID", StringComparison.CurrentCulture) == true))
            {
                if (nOperateType == 1)
                {
                    strWhere = "=" + strWhere;
                }
                else
                    if (nOperateType == 0)
                    {
                        if (strValue.Length == 0)
                            strWhere = " is " + strWhere;
                        else
                            strWhere = " = " + strWhere;
                    }
            }
            else
            {
                if (nOperateType == 1 || nOperateType == 0)
                {
                    strWhere = "=" + strWhere;
                }
            }
            return strWhere;
        }
        
        private static string oldSql = "";

        //public static void getSelectedField(string sql, List<PmsField> selectFieldList)//, List<string> fieldList
        //{
        //    //语句没有变化
        //    if (oldSql == sql && selectFieldList.Count > 0)
        //        return;
        //    else
        //        oldSql = sql;

        //    SqlConnection _SqlConnection1 = SqlStructure.GetSqlConncetion();

        //    if (_SqlConnection1 == null || _SqlConnection1.State != ConnectionState.Open)
        //    {
        //        MessageBox.Show("连接错误");
        //        return;
        //    }
        //    SqlCommand thisCommand = _SqlConnection1.CreateCommand();
        //    thisCommand.CommandText = sql;

        //    //PMS.Libraries.ToolControls.PMSPublicInfo.Message.Info(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, thisCommand.CommandText, false);
        //    SqlDataReader thisReader = null;
        //    try
        //    {
        //        thisReader = thisCommand.ExecuteReader();
        //    }
        //    catch (Exception ec)
        //    {
        //        MessageBox.Show(ec.Message);
        //        thisCommand.Dispose();
        //        _SqlConnection1.Close();
        //        return;
        //    }
        //    List<PmsField> selectFieldListSave = new List<PmsField>();
        //    foreach (var pf in selectFieldList)
        //    {
        //        PmsField pfNew = new PmsField();
        //        pfNew.fieldName = pf.fieldName;
        //        pfNew.fieldType = pf.fieldType;
        //        pfNew.fieldKey = pf.fieldKey;
        //        selectFieldListSave.Add(pfNew);
        //    }
        //    selectFieldList.Clear();
        //    System.Data.DataTable dt = new System.Data.DataTable();
        //    dt.Load(thisReader);
        //    foreach (DataColumn dc in dt.Columns)
        //    {
        //        PmsField pf = new PmsField();
        //        pf.fieldName = dc.ColumnName.ToLower();
        //        pf.fieldType = dc.DataType.ToString();
        //        pf.fieldKey = false;
        //        selectFieldList.Add(pf);
        //    }
        //    foreach (var pf in selectFieldList)
        //    {
        //        foreach (var pfSave in selectFieldListSave)
        //        {
        //            if (pf.fieldName == pfSave.fieldName && pf.fieldType == pfSave.fieldType)
        //            {
        //                //未更新之前已存在
        //                pf.fieldKey = true;
        //                break;
        //            }
        //        }
        //    }

        //   // getGroupBy(sql, selectFieldList,fieldList);
        //}

        //不用,20100606
        public static void getGroupBy(string sql, List<PmsField> selectFieldList, List<string> fieldList)
        {
            fieldList.Clear();
            sql = sql.ToLower();
            int indexGroupBy = sql.IndexOf("group by ");
            string groupBy = "";


            if (indexGroupBy > 0)
            {
                string groupby1 = (sql.Substring(indexGroupBy + 8, sql.Length - (indexGroupBy + 8))).Trim();

                int indexorderBy = groupby1.IndexOf("order by  ");
                if (indexorderBy > 0)
                {
                    groupBy = groupby1.Substring(0, indexorderBy);
                }
                else
                {
                    int indexComputeBy = sql.IndexOf("compute by ");
                    if (indexComputeBy > 0)
                    {
                        groupBy = groupby1.Substring(0, indexComputeBy);
                    }
                    else
                        groupBy = groupby1;
                }
            }
            else
                return;

            groupBy = groupBy.Trim();            
            fieldList.Clear();
            fieldList.AddRange(groupBy.Split(','));
            /*/if (groupBy.Length > 0)
            {
                foreach (PmsField pf in selectFieldList)
                {
                    if (groupBy.IndexOf(pf.fieldName) >= 0)
                    {
                        fieldList.Add(pf.fieldName);
                    }
                }
            }/*/
        }
        public static List<string> GetGroupList(string main,string second)
        {
            List<string> group = new List<string>();

            if (!string.IsNullOrEmpty(main))
            {
                group.Add(PublicFunctionClass.GetTimePartSqlField(1, main));
                if (!string.IsNullOrEmpty(second))
                {
                    group.Add(PublicFunctionClass.GetTimePartSqlField(1, second));
                }
            }
            return group;
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="bRun">true ,have where</param>
        /// <returns></returns>
        public static string GetConfigSql(DataSource dataSource,bool bRun)
        {
            if (dataSource == null)
                return "";

            string groupby = "";
            #region group By
            if (dataSource.MainGroupBy.Length > 0)
            {
                groupby = PublicFunctionClass.GetTimePartSqlField(1, dataSource.MainGroupBy);
                if (dataSource.SecondaryGroupBy.Length > 0)
                    groupby += " ," + PublicFunctionClass.GetTimePartSqlField(1, dataSource.SecondaryGroupBy);
            }
            #endregion

            string orderby = "";
            #region oder By
            if (dataSource.SortList != null && dataSource.SortList.Count > 0)
            {
                foreach (var sc in dataSource.SortList)
                {
                    sc.fieldName = PublicFunctionClass.GetTimePartSqlField(1, sc.fieldName);
                    orderby +=  sc.ToString() + ",";
                }

                if (orderby.Length > 0)
                    orderby = orderby.Substring(0, orderby.Length - 1);
            }
            #endregion

            string field = "";
            #region Field
            if (dataSource.MainGroupBy.Length > 0)
            {
                //field += dataSource.MainGroupBy.ToLower() + ",";
                //if (field.IndexOf(dataSource.MainGroupBy.ToLower()) < 0)//group by 字段一定要在select后面
                {
                    field += PublicFunctionClass.GetTimePartSqlField(1, dataSource.MainGroupBy) + ",";

                }
                if (dataSource.SecondaryGroupBy.Length > 0)
                {
                    field += PublicFunctionClass.GetTimePartSqlField(1, dataSource.SecondaryGroupBy) + ","; ;
                }
            }
            for (int i = 0; i < dataSource.FormulaList.Count; i++)
            {
                string itemText = (string)dataSource.FormulaList[i];
                //itemText = itemText.ToLower();
                itemText = itemText.Trim();

                if (dataSource.MainGroupBy.Length > 0)
                {
                    if (dataSource.MainGroupBy.Equals(itemText,StringComparison.CurrentCultureIgnoreCase)
                     || dataSource.SecondaryGroupBy.Equals(itemText, StringComparison.CurrentCultureIgnoreCase))
                        continue;
                }
                field += itemText + ",";
            }

            if (field.Length > 0)
            {
                field = field.Substring(0, field.Length - 1);
            }
            else//未选择字段
            {
                return "";
            }
            #endregion

            string tables = "";
            #region Table
            int nTable = dataSource.SelectedTableList.Count;
            if (nTable < 1)
                return "";

            string mainTable = (string)(dataSource.SelectedTableList[0]);

            tables = "[" + mainTable + "]";
            if (nTable > 1)
            {
                if (dataSource.PmsJoinRelation.Count == 0)
                {
                    for (int iTable = 1; iTable < nTable; iTable++)
                    {
                        string itemText = (string)dataSource.SelectedTableList[iTable];
                        itemText.ToLower();

                        {
                            tables += " cross join [" + itemText + "]";
                        }
                    }
                }
                else
                {
                    string previewTable = mainTable;//记录上一个表

                    List<string> tableSelect = new List<string>();
                    tableSelect.Add(mainTable);
                    while (previewTable.Length > 0)
                    {
                        bool bFind = false;
                        foreach (TableJoinRelation tjr in dataSource.PmsJoinRelation)
                        {
                            if (tjr.mainTable.Equals(previewTable, StringComparison.CurrentCultureIgnoreCase))
                            {
                                string joinType = "";
                                //4 inner join,1 left out join, 2 right out join
                                //3 full out join,0 cross join
                                if (tjr.joinType == 0)
                                    joinType = " inner join ";
                                else if (tjr.joinType == 3)
                                    joinType = " full outer join ";
                                else if (tjr.joinType == 2)
                                    joinType = " right outer join ";
                                else if (tjr.joinType == 1)
                                    joinType = " left outer join ";

                                tables += joinType + "[" + tjr.secondaryTable + "] on [" + tjr.mainTable;
                                tables += "].[" + tjr.mainColumn + "]";
                                tables += tjr.compare + "[" + tjr.secondaryTable;
                                tables += "].[" + tjr.secondaryColumn + "]";

                                previewTable = tjr.secondaryTable;
                                if (previewTable.Equals(mainTable, StringComparison.CurrentCultureIgnoreCase))//怕形成死循环
                                {
                                    previewTable = "";
                                    break;
                                }
                                tableSelect.Add(previewTable);
                                bFind = true;
                                break;
                            }
                        }
                        if (bFind == false)
                        {
                            previewTable = "";
                        }
                    }

                    if (tableSelect.Count < dataSource.SelectedTableList.Count)
                    {

                        for (int it = 1; it < nTable; it++)
                        {
                            string tableNoSelect = (string)(dataSource.SelectedTableList[it]);
                            bool bSelected = false;
                            foreach (string tableHaveSelected in tableSelect)
                            {
                                if (tableNoSelect.Equals(tableHaveSelected, StringComparison.CurrentCultureIgnoreCase))//已选
                                {
                                    bSelected = true;
                                    break;
                                }
                            }
                            if (bSelected == true)
                            {
                                continue;
                            }
                            else
                            {
                                tables += " cross join [" + tableNoSelect + "]";
                            }
                        }
                    }
                }
            }
            #endregion

            string having = "";
            string where = "";
            if (bRun)
            {
                #region Where

                if (dataSource.SelectedTableList != null && dataSource.SelectedTableList.Count == 1)
                {
                    int ireturn = PMS.Libraries.ToolControls.PmsSheet.WhereLibrary.TreeViewDataAccess.GetSQLWhere(dataSource.WhereData.Clone(), dataSource.SelectedTableList[0], ref where, ref having);

                    if (ireturn == 2)
                        return where;
                }
                else if (dataSource.SelectedTableList != null && dataSource.SelectedTableList.Count > 1)
                {
                    int ireturn = PMS.Libraries.ToolControls.PmsSheet.WhereLibrary.TreeViewDataAccess.GetSQLWhere(dataSource.WhereData.Clone(), ref where, ref having);

                    if (ireturn == 2)
                        return where;
                }
                #endregion
            }

            string returnSql = "select " + field + " from " + tables;


            if (where.Length > 0)
                returnSql += " where " + where;

            bool bGroup = false;
            if (groupby.Length > 0)
            {
                returnSql += " group by " + groupby;
                bGroup = true;
            }

            if (having.Length > 0 && bGroup)
                returnSql += " having " + having;
            if (orderby.Length > 0)
                returnSql += " order by " + orderby;

            return returnSql;
        }

        /// <summary>
        /// 获得多分组查询中简单查询语句
        /// </summary>
        /// <param name="field">查询字段</param>
        /// <param name="dataSource">数据源</param>
        /// <returns>错误返回空串</returns>
        public static string getSimpleSql(string field, DataSource dataSource)
        {
            string simpleSql = "";
            if (dataSource.SelectedTableList.Count == 1)
            {
                simpleSql = "select distinct " + field + " from " + dataSource.SelectedTableList[0];
            }
            else
            {
                int point = field.IndexOf("].[");
                simpleSql = "select distinct ";

                if (point > 0)
                {
                    simpleSql += field.Substring(point + 2, field.Length - (point + 2));
                    simpleSql += " from " + field.Substring(point + 1);
                }
                else
                {
                    int point1 = field.IndexOf(".");
                    if (point1 > 0)
                    {
                        simpleSql += field.Substring(point + 1, field.Length - (point + 1));
                        simpleSql += " from " + field.Substring(point);
                    }
                    else//error
                    {
                        MessageBox.Show("多表字段部分错误！");
                        return "";
                    }
                }
            }
            return simpleSql;
        }
    }
}
