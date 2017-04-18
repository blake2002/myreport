using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using System.Data.SqlClient;
using PMS.Libraries.ToolControls.ToolBox;
using System.Windows.Forms.DataVisualization.Charting;


namespace PMS.Libraries.ToolControls.PMSChart
{
    public class PMSChartCtrl:System.Windows.Forms.DataVisualization.Charting.Chart
    {
        public PMSChartCtrl()
        {
            InitailChart();
            InitailData();
        }

        #region 成员
        private DataSource _sqlSource;

        private string groupyFieldMain = "";
        private string groupyFieldSecondary = "";
        private List<TableField> fieldList = new List<TableField>();
        private List<string> groupByList = new List<string>();
        
        #endregion
        #region 属性

        [Editor(typeof(SqlEditor), typeof(UITypeEditor))]
        [Description("sql语句编辑器")]
        [Category("PMS控件属性")]
        public DataSource SqlSource
        {
            get { return _sqlSource; }
            set 
            { 
                _sqlSource = value;
                InitailData();
            }
        }
        #endregion
        #region 事件
        #endregion
        #region 属性编辑器
        internal class SqlEditor : UITypeEditor
        {
            public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            {
                if (context != null && context.Instance != null)
                {
                    return UITypeEditorEditStyle.Modal;
                }

                return base.GetEditStyle(context);
            }

            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                IWindowsFormsEditorService editorService = null;

                if (context != null && context.Instance != null && provider != null)
                {
                    editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                    if (editorService != null)
                    {
                        PMSChartCtrl control = null;
                        if (context.Instance.GetType() == typeof(PMSChartCtrl))
                            control = (PMSChartCtrl)context.Instance;

                        FormSql form1 = new FormSql();
                        form1.StartPosition = FormStartPosition.CenterParent;
                        form1.SqlSource = control.SqlSource;
                        if (DialogResult.OK == editorService.ShowDialog(form1))
                        {

                            value = form1.SqlSource;                            
                        }
                        return value;
                    }
                }

                return value;
            }
        }
        #endregion
        #region 方法
        private void InitailChart()
        {
            ChartArea chartArea1 = new ChartArea();
            chartArea1.Name = "ChartArea1";
            base.ChartAreas.Add(chartArea1);
        }
        private void InitailData()
        {
            if (this.DesignMode == false&&_sqlSource!=null&&_sqlSource.UsingConfig)//配置语句
            {
                SqlConnection _SqlConnection1 = SqlStructure.GetSqlConncetion();

                if (_SqlConnection1 == null || _SqlConnection1.State != ConnectionState.Open)
                {
                    MessageBox.Show("连接错误");
                    return;
                }
                SqlCommand thisCommand = _SqlConnection1.CreateCommand();
                thisCommand.CommandText = _sqlSource.ConfigSql;

                //PMS.Libraries.ToolControls.PMSPublicInfo.Message.Info(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, thisCommand.CommandText, false);
                SqlDataReader thisReader = null;
                try
                {
                    thisReader = thisCommand.ExecuteReader();
                    thisCommand.Dispose();
                }
                catch (Exception ec)
                {
                    MessageBox.Show(ec.Message);
                    thisCommand.Dispose();
                    thisReader.Close();
                    return;
                }
                Series series1 = new Series();
                series1.Name = _sqlSource.XAixsies[0];
                series1.ChartArea = "ChartArea1";                
                base.Series.Add(series1);
                //base.Series[_sqlSource.XAixsies[0]].Points.DataBindXY(thisReader, , thisReader, _sqlSource.YAixs[0]);
                base.DataSource = thisReader;
                base.Series[_sqlSource.XAixsies[0]].XValueMember = _sqlSource.XAixsies[0];
                base.Series[_sqlSource.XAixsies[0]].YValueMembers = _sqlSource.YAixs[0];
                base.DataBind();

                thisReader.Close();
            }
        }
        public bool ParseGroupBy(string subGroupBy)
        {
            subGroupBy = subGroupBy.Trim();            
            #region group by

            if (subGroupBy.Length == 0)
            {
                groupByList.Clear();
                return false;
            }
            try
            {
                string oldGroup = "";
                while (oldGroup != subGroupBy)
                {
                    oldGroup = subGroupBy;
                    if (subGroupBy[0] == '[')///]算转义字符，需要考虑
                    {
                        #region []模式
                        int endI = subGroupBy.IndexOf("]");
                        if (endI > 0)
                        {
                            string field = subGroupBy.Substring(0,endI+1);
                            string fieldOther = subGroupBy.Substring(endI+1,subGroupBy.Length-(endI+1));
                            fieldOther = fieldOther.Trim(); 
                            if(fieldOther.Length==0)//正确返回
                            {
                                groupByList.Add(field);
                                return true;
                            }
                            if(fieldOther[0]== '.')//多表格式[a].f
                            {
                                fieldOther = fieldOther.Substring(1);
                                fieldOther = fieldOther.Trim();
                                if(fieldOther.Length==0)
                                {
                                    groupByList.Clear();
                                    return false;
                                }
                                if(fieldOther[0]== '[')//].[模式
                                {
                                    #region ].[模式
                                    int endMulti = fieldOther.IndexOf("]");
                                    if (endMulti > 0)
                                    {
                                        field += "."+fieldOther.Substring(0,endMulti+1);
                                        fieldOther = fieldOther.Substring(endMulti+1,fieldOther.Length-(endMulti+1));
                                        fieldOther = fieldOther.Trim();
                                        if(fieldOther.Length==0)//正确返回
                                        {
                                            groupByList.Add(field);
                                            return true;
                                        }
                                        if(fieldOther[0] == ',')//有后续group by 字段
                                        {
                                            subGroupBy = fieldOther.Substring(1);
                                            groupByList.Add(field);
                                            continue;
                                        }
                                        else
                                        {
                                            groupByList.Clear();
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        groupByList.Clear();
                                        return false;
                                    }
                                    #endregion
                                }
                                else //]. ![模式
                                {
                                    int indexCom = fieldOther.IndexOf(",");
                                    if(indexCom>0)//有后续
                                    {
                                        field += "."+fieldOther.Substring(0,indexCom);
                                        subGroupBy = fieldOther.Substring(indexCom+1,fieldOther.Length - indexCom-1);
                                        groupByList.Add(field);
                                        continue;
                                    }
                                    else
                                    {
                                        field += "."+fieldOther;
                                        groupByList.Add(field);
                                        return true;
                                    }
                                }
                            }
                            else//单表
                            {
                                fieldOther = fieldOther.Trim();
                                if(fieldOther.Length==0)
                                {
                                    groupByList.Add(field);
                                    return true;
                                }
                                if(fieldOther[0]==',')
                                {
                                    subGroupBy = fieldOther.Substring(1);
                                    if(subGroupBy.Length==0)
                                    {
                                        groupByList.Add(field);
                                        return true;
                                    }
                                    groupByList.Add(field);
                                }
                                else//错误的语句
                                {
                                    groupByList.Clear();
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            //error
                            groupByList.Clear();
                            return false;
                        }
                        #endregion
                    }
                    else////// a;a.[a] ;a.a; a,b; a.a,b;a.[a],b
                    {
                        int indexS = subGroupBy.IndexOf("[");
                        if(indexS>0)//后面存在[
                        {
                            string newGroup = subGroupBy.Substring(0,indexS);
                            int indexCom = newGroup.IndexOf(',');
                            if (indexCom > 0)/// a,*[*
                            {
                                string field = (subGroupBy.Substring(0, indexCom)).Trim();
                                subGroupBy = (subGroupBy.Substring(indexCom + 1, subGroupBy.Length - indexCom - 1)).Trim();
                                groupByList.Add(field);
                                continue;
                            }
                            else
                            {
                                int indexDot = newGroup.IndexOf('.');
                                if (indexDot > 0)
                                {
                                }
                            }
                        }
                        else
                        {
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return false;
            #endregion
        }
        //前提：语句语法正确
        public bool ParseSql(string sql)
        {
            sql = sql.ToLower();
            int index = 0;
            sql = sql.Replace("\r\n", " ");
            sql = sql.Replace("\r", " ");
            sql = sql.Replace("\n", " ");
            #region field 
            index = sql.IndexOf("select");
            if (index >= 0)//group
            {
                int indexFrom = sql.IndexOf("from");
                if (indexFrom < index)
                    return false;
                string subSelect = sql.Substring(index + 7, indexFrom - (index + 7));

                indexFrom = sql.IndexOf("distinct ");

                if (indexFrom > 0)
                {
                    subSelect = sql.Substring(indexFrom + 9, subSelect.Length - (index + 9));
                }

                try
                {
                    string fieldn = "";
                    string oldSelect = "";
                    while (oldSelect != subSelect)
                    {
                        oldSelect = subSelect;
                        while (subSelect.Length>0&&subSelect[0] == ' ')
                        {
                            subSelect = subSelect.Substring(1);
                        }
                        if (subSelect.Length == 0)
                            break;

                        if (subSelect[0] == '[')
                        {
                            int endI = subSelect.IndexOf("]");
                            if (endI > 0)
                            {
                                fieldn = subSelect.Substring(0, endI + 1);

                                TableField tf = new TableField();
                                tf.fieldName = fieldn;
                                
                                subSelect = subSelect.Substring(endI + 2, subSelect.Length - endI - 2);
                                if (subSelect.Length == 0)
                                {
                                    fieldList.Add(tf);
                                    break;
                                }
                                #region 存在别名
                                while (subSelect.Length > 0 && subSelect[0] == ' ')
                                {
                                    subSelect = subSelect.Substring(1);
                                }
                                if (subSelect.StartsWith("as "))
                                {
                                    int asSpace = subSelect.IndexOf(" ");
                                    if (asSpace > 0)
                                    {
                                        string alias = subSelect.Substring(asSpace + 1, subSelect.Length - asSpace - 1);
                                        while (alias.Length > 0 && alias[0] == ' ')
                                        {
                                            alias = alias.Substring(1);
                                        }

                                        if (alias[0] == '[')
                                        {
                                            int endAlias = alias.IndexOf("]");
                                            if (endAlias > 0)
                                            {
                                                subSelect = alias.Substring(endAlias + 2, alias.Length - endAlias - 2);
                                                tf.tableName = alias.Substring(0, endAlias + 1);
                                            }
                                            else
                                                return false;
                                        }
                                        else
                                        {
                                            int indexDot = alias.IndexOf(",");
                                            if (indexDot > 0)
                                            {
                                                string aliasField = alias.Substring(0, indexDot);

                                                while (aliasField.Length - 1 > 0 && aliasField[aliasField.Length - 1] == ' ')//除去末尾空格
                                                {
                                                    aliasField = aliasField.Substring(0, aliasField.Length - 1);
                                                }
                                                tf.tableName = aliasField;
                                                subSelect = alias.Substring(indexDot + 1, alias.Length - indexDot - 1);
                                                if (subSelect.Length == 0)
                                                    break;
                                            }
                                        }
                                    }
                                    else
                                        return false;

                                    fieldList.Add(tf);
                                }
                                #endregion
                            }
                            else
                            {
                                //解析失败
                                return false;
                            }
                        }
                        else
                        {
                            while (subSelect.Length > 0 && subSelect[subSelect.Length - 1] == ' ')//除去末尾空格
                            {
                                subSelect = subSelect.Substring(0, subSelect.Length - 1);
                            }
                            TableField tf = new TableField();
                            string asString = "";
                            int asSpace = subSelect.IndexOf(" ");
                            if (asSpace > 0)
                            {
                                tf.fieldName = subSelect.Substring(0, asSpace);
                                asString = subSelect.Substring(asSpace + 1, subSelect.Length - asSpace - 1);
                                while (asString.Length > 0 && asString[0] == ' ')//
                                {
                                    asString = asString.Substring(1);
                                }
                                #region 存在别名
                                if (asString.StartsWith("as "))
                                {
                                    asSpace = asString.IndexOf(" ");
                                    if (asSpace > 0)
                                    {
                                        string alias = asString.Substring(asSpace + 1, asString.Length - asSpace - 1);
                                        while (alias.Length > 0 && alias[0] == ' ')
                                        {
                                            alias = alias.Substring(1);
                                        }
                                        tf.tableName = alias;
                                    }
                                    else
                                        return false;
                                }
                                #endregion
                            }
                            else
                            {
                                tf.fieldName = fieldn;
                            }
                            /*
                            int indexDot = subSelect.IndexOf(",");
                            if (indexDot > 0)
                            {
                                fieldn = subSelect.Substring(0, indexDot);
                                subSelect = subSelect.Substring(indexDot + 1, subSelect.Length - indexDot - 1);
                            }
                            else//最后一个
                            {
                                fieldn = subSelect;
                            }
                            while (fieldn.Length - 1 > 0 && fieldn[fieldn.Length - 1] == ' ')//除去末尾空格
                            {
                                fieldn = fieldn.Substring(0, fieldn.Length - 1);
                            }
                            
                            TableField tf = new TableField();
                            string asString = "";
                            int asSpace = fieldn.IndexOf(" ");
                            if (asSpace > 0)
                            {
                                tf.fieldName = fieldn.Substring(0, asSpace);
                                asString = fieldn.Substring(asSpace + 1, fieldn.Length - asSpace - 1);
                                while (asString.Length > 0 && asString[0] == ' ')//
                                {
                                    asString = asString.Substring(1);
                                }
                                #region 存在别名
                                if (asString.StartsWith("as "))
                                {
                                    asSpace = asString.IndexOf(" ");
                                    if (asSpace > 0)
                                    {
                                        string alias = asString.Substring(asSpace + 1, asString.Length - asSpace - 1);
                                        while (alias.Length > 0 && alias[0] == ' ')
                                        {
                                            alias = alias.Substring(1);
                                        }
                                        tf.tableName = alias;
                                    }
                                    else
                                        return false;
                                }
                                #endregion
                            }
                            else
                            {
                                tf.fieldName = fieldn;
                            }
                             */
                            fieldList.Add(tf);
                            
                            if (subSelect.Length == 0)
                                break;
                        }                        
                    }
                }
                catch (Exception e)
                {
                }
            }
            #endregion
            #region group by
            index = sql.IndexOf("group by");
            if (index>0)//group
            {
                string subGroupBy = sql.Substring(index + 8, sql.Length - (index + 8));
                try
                {
                    while (subGroupBy[0] == ' ')
                    {
                        subGroupBy = subGroupBy.Substring(1);
                    }
                    if (subGroupBy[0] == '[')
                    {
                        int endI = subGroupBy.IndexOf("]");
                        if (endI > 0)
                        {
                            groupyFieldMain = subGroupBy.Substring(0, endI + 1);

                            #region 寻找第二分组
                            subGroupBy = subGroupBy.Substring(endI + 1, subGroupBy.Length - endI - 1);
                            while (subGroupBy[0] == ' ')
                            {
                                subGroupBy = subGroupBy.Substring(1);
                            }
                            if (subGroupBy[0] == ',')//存在第二分组
                            {
                                subGroupBy = subGroupBy.Substring(1);
                                if (subGroupBy[0] == '[')
                                {
                                    endI = subGroupBy.IndexOf("]");
                                    if (endI > 0)
                                    {
                                        groupyFieldSecondary = subGroupBy.Substring(0, endI + 1);
                                    }
                                }
                                else
                                {
                                    endI = subGroupBy.IndexOf(" ");
                                    if (endI > 0)
                                    {
                                        groupyFieldSecondary = subGroupBy.Substring(0, endI + 1);
                                    }
                                    else
                                        groupyFieldSecondary = subGroupBy;
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        int endI = subGroupBy.IndexOf("order by");
                        if (endI > 0)
                        {
                            subGroupBy = subGroupBy.Substring(0, endI);
                        }
                        
                        endI = subGroupBy.IndexOf(",");
                        if (endI > 0)
                        {
                            groupyFieldMain = subGroupBy.Substring(0, endI);

                            #region 寻找第二分组
                            subGroupBy = subGroupBy.Substring(endI + 1, subGroupBy.Length - endI - 1);
                            while (subGroupBy[0] == ' ')
                            {
                                subGroupBy = subGroupBy.Substring(1);
                            }
                            groupyFieldSecondary = subGroupBy;
                            
                            #endregion
                        }
                        else
                        {
                            groupyFieldMain = subGroupBy;
                        }
                    }
                }
                catch (Exception e)
                {
                }
            }
            #endregion
            return true;
        }
        #endregion
    }
}
