using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using PMS.Libraries.ToolControls.PMSPublicInfo;
using System.Data;
namespace PMS.Libraries.ToolControls.PmsSheet.PmsPublicData
{
    public enum DisplayPositionStyle
    {
        DisplayBottom,
        DisplayProp
    }

    public enum GuidNewStyle
    {
        CompletelyNew,//完全新增
        NewIfError//当已有串解析失败时新增
    }
    /// <summary>
    /// 表字段信息,从数据库系统表中获得or根据表设计结构获得
    /// </summary>
    /// 
    [Serializable]
    public class PmsField
    {
        public string fieldName;
        public string fieldType;
        public bool fieldKey;
        public bool fieldForeigner;
        public bool fieldNull;
        public string fieldForeignerType;
        public string fieldDescription;
        public string fieldEncryptType;
        public object fieldDefault;

        /// <summary>
        /// 2011.09.23修改
        /// 目的:以前小马哥仅仅对SQL数据库的类型转换做了处理
        /// 现在加入对Access数据库类型转换处理
        /// </summary>
        /// <param name="sqlDataType"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ToPMSDataType(string sqlDataType, int length)
        {
            if(string.IsNullOrEmpty(sqlDataType))
                return "NoDefinedType";
            string strType = sqlDataType.ToLower();
            string newType = "NoDefinedType";
            if (strType.Equals("bit"))
            {
                newType = "BIT";
            }
            else if (strType.Equals("varchar") || strType.Equals("nvarchar") || strType.Equals("text") || strType.Equals("ntext") || strType.Equals("char") || strType.Equals("nchar")
                 || strType.Equals("varchar2") || strType.Equals("nvarchar2"))
            {
                newType = "VARCHAR(" + length.ToString() + ")"; ;
            }
            else if (strType.Equals("clob") || strType.Equals("nclob"))
            {
                newType = "VARCHAR(" + length.ToString() + ")";
            }
            else if (strType.Equals("float"))
            {
                newType = "FLOAT";
            }
            else if (strType.Equals("real"))
            {
                newType = "REAL";
            }
            else if (strType.Equals("int") || strType.Equals("bigint") || strType.Equals("tinyint")
                ||strType.Equals("smallint"))
            {
                newType = "INT";
            }
            else if (strType.Equals("datetime"))
            {
                newType = "DATETIME";
            }
            else if (strType.Equals("image") || strType.Equals("blob"))
            {
                newType = "IMAGE";
            }
            else if (strType.Equals("uniqueidentifier") || strType.Equals("raw"))
            {
                newType = "GUID";
            }
            else if (strType.Equals("system.string") || strType.Equals("string"))
            {
                newType = "VARCHAR(" + length.ToString() + ")"; ;
            }
            else if (strType.Equals("system.int32") || strType.Equals("int32"))
            {
                newType = "INT32";
            }
            else if (strType.Equals("system.int16") || strType.Equals("int16"))
            {
                newType = "INT16";
            }
            else if (strType.Equals("system.int64") || strType.Equals("int64"))
            {
                newType = "INT64";
            }
            else if (strType.Equals("system.decimal") || strType.Equals("decimal"))
            {
                newType = "DECIMAL";
            }
            else if (strType.Equals("system.datetime") || strType.Equals("datetime") || strType.Equals("smalldatetime") || strType.Equals("date") || strType.StartsWith("timestamp"))
            {
                newType = "DATETIME";
            }
            else if (strType.Equals("system.single") || strType.Equals("single"))
            {
                newType = "FLOAT";
            }
            else if (strType.Equals("system.double") || strType.Equals("double"))
            {
                newType = "FLOAT";
            }
            else if (strType.Equals("money") || strType.Equals("smallmoney") || strType.Equals("number"))
            {
                newType = "DECIMAL";
            }
            else
                newType = "NoDefinedType";
            return newType;
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

        public static List<PmsField> InitialFieldList(string _strTableName)
        {
            List<PmsField> _pmsFieldList = new List<PmsField>();
            try
            {                
                System.Data.DataTable table = PMSDBStructure.GetTableFieldInfo(_strTableName);

                if (table == null || table.Rows.Count == 0)
                    return _pmsFieldList;

                foreach (DataRow row in table.Rows)
                {
                    PmsField field = new PmsField();
                    try
                    {
                        field.fieldName = row["FieldName"].ToString();
                        string strType = row["FieldType"].ToString();
                        strType = strType.ToLower();
                        int length = 20;
                        length = Convert.ToInt32((row["FieldLength"]));
                        field.fieldDescription = row["FieldDescription"].ToString();
                        field.fieldType = PmsField.ToPMSDataType(strType, length);
                        field.fieldKey = (bool)row["FieldPrimaryKey"];
                        field.fieldNull = (bool)row["FieldNullAble"];
                        field.fieldDefault = row["FieldDefault"];
                    }
                    catch
                    {
                    }
                    string propEx = PMSDBStructure.GetTableColumnPropertie(_strTableName, field.fieldName, "ColumnType");

                    //存在加密
                    if (propEx == PMS.Libraries.ToolControls.PMSPublicInfo.ColumnType.Encrypted)
                    {
                        field.fieldEncryptType = PMSDBStructure.GetTableColumnPropertie(_strTableName, field.fieldName, "EncryptType");
                    }


                    _pmsFieldList.Add(field);
                }
                
            }
            catch (Exception ex)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.PublicFunctionClass.EnhancedStackTrace(ex); 
            }
            return _pmsFieldList;
        }


        /// <summary>
        /// 检测输入数据合法性
        /// </summary>
        /// <param name="str">输入数据</param>
        /// <param name="PMSDataType">绑定的字段类型</param>
        /// <param name="textBox2">编辑框对象</param>
        /// <param name="KeyChar">当前输入字符</param>
        /// <returns>合法返回false,非法返回true</returns>
        public static bool textBoxKeyPress(string str, string PMSDataType, TextBox textBox2,char KeyChar)
        {
            if (PMSDataType == null)
                return false;
            if (PMSDataType == "REAL" || PMSDataType == "FLOAT")
            {
                if (str.Length == 0)
                {
                    if (!(Char.IsNumber(KeyChar) || KeyChar == 8 || KeyChar == '-'))
                    {
                        return true;
                    }
                }
                else
                {
                    if (textBox2.SelectionStart == 0)
                    {
                        if (str.Substring(0, 1) == "-")
                        {
                            if (!(Char.IsNumber(KeyChar) || KeyChar == 8))
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if (!(Char.IsNumber(KeyChar) || KeyChar == 8 || KeyChar == '-'))
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        if (str.Length == 1)
                        {
                            if (str == "-")
                            {
                                if (!(Char.IsNumber(KeyChar) || KeyChar == 8))
                                {
                                    return true;

                                }
                            }
                            else
                            {
                                if (!(Char.IsNumber(KeyChar) || KeyChar == 8 || KeyChar == '.'))
                                {
                                    return true;
                                }
                            }
                        }
                        else
                        {
                            if (str.IndexOf('.') > 0)
                            {
                                if (!(Char.IsNumber(KeyChar) || KeyChar == 8))
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                if (!(Char.IsNumber(KeyChar) || KeyChar == 8 || KeyChar == '.'))
                                {
                                    return true;
                                }
                            }

                        }
                    }
                }

            }
            else if (PMSDataType == "BIT")
            {
                if (str.Length > 0)
                {
                    if (textBox2.SelectionLength == str.Length && (KeyChar == '0' || KeyChar == '1'))
                        return false;
                    return true;
                }
                else
                {
                    if (KeyChar == '0' || KeyChar == '1')
                        return false;
                    else
                        return true;
                }
            }
            else if (PMSDataType == "INTEGER" || PMSDataType == "INT" || PMSDataType == "BIGINT")
            {
                if (str.Length == 0)
                {
                    if (!(Char.IsNumber(KeyChar) || KeyChar == 8 || KeyChar == '-'))
                    {
                        return true;
                    }
                }
                else
                {
                    if (textBox2.SelectionStart == 0)
                    {
                        if (str.Substring(0, 1) == "-")
                        {
                            if (!(Char.IsNumber(KeyChar) || KeyChar == 8))
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if (!(Char.IsNumber(KeyChar) || KeyChar == 8 || KeyChar == '-'))
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        if (!(Char.IsNumber(KeyChar) || KeyChar == 8))
                        {
                            return true;
                        }
                    }
                }
            }
            else if (PMSDataType.StartsWith("VARCHAR",StringComparison.CurrentCultureIgnoreCase))
            {
                string slength = PMSDataType.Substring(8);
                slength = slength.Substring(0, slength.Length - 1);
                int length = 100;
                try
                {
                    length = Convert.ToInt32(slength);
                }
                catch (Exception e)
                {
                    PMS.Libraries.ToolControls.PMSPublicInfo.PublicFunctionClass.EnhancedStackTrace(e);
                }

                if (textBox2.SelectionLength == 0)//没有选择的时候,输入
                {
                    if (textBox2.Text.Length >= length)
                    {
                        return true;
                    }
                }

            }
            return false;
        }
        
    }
    /// <summary>
    /// 字段显示属性
    /// fieldName   字段名
    /// fieldTag    字段标题,如果为空则显示字段名
    /// bDisplay    是否显示在表格中
    /// uSort       该列是否排序
    /// uSortOrder  该列排序位置
    /// bStandard   更新时是否作为判别标准
    /// FontColor   前景色与字体
    /// fieldValue  字段当前值
    /// fieldType   字段类型
    /// FormatColor 字段显示格式与背景色
    /// fileName    附件文件路径
    /// bInEditList 是否属于编辑列表
    /// </summary>
    [Serializable]
    public class PmsDisplay
    {
        public string fieldName;
        public string fieldTag;
        public bool bDisplay = false;
        public uint uSort;
        public uint uSortOrder;
        public bool bStandard = false;
        public object fieldValue;
        public object fieldValue1;//替换显示时真实值
        public object fieldValue2;//map表parentID字段编辑时存储其对应的mapid
        public string fieldType;
        public PmsFormat FormatColor;
        public int columnWidth;
        public string fileName;
        public bool bInEditList;

        public PmsDisplay()
        {
        }
        public PmsDisplay(PmsDisplay pd)
        {
            this.fieldName = pd.fieldName;
            this.fieldTag = pd.fieldTag;
            this.bDisplay = pd.bDisplay;
            this.uSort = pd.uSort;
            this.uSortOrder = pd.uSortOrder;
            this.bStandard = pd.bStandard;
            this.fieldValue = pd.fieldValue;
            this.fieldValue1 = pd.fieldValue1;
            this.fieldValue2 = pd.fieldValue2;
            this.fieldType = pd.fieldType;
            this.columnWidth = pd.columnWidth;
            this.fileName = pd.fileName;
            this.bInEditList = pd.bInEditList;
            this.FormatColor = pd.FormatColor;
        }
    }


    public class PmsPassProperty
    {
        public int col;//加密列
        public string fieldName;
        public string fieldEncryptType;//加密类型
    }

    /// <summary>
    /// font    字体
    /// color   颜色
    /// </summary>
    [Serializable]
    public class PmsFont:IDisposable
    {
        public void Dispose()
        {
            _font.Dispose();
        }
        private Font _font;
        public Font Font
        {
            get { return _font; }
            set { _font = value; }
        }
        private Color _backColor;

        public Color BackColor
        {
            get { return _backColor; }
            set { _backColor = value; }
        }
        private Color _foreColor;

        public Color ForeColor
        {
            get { return _foreColor; }
            set { _foreColor = value; }
        }

        private string _logicCondition;

        public string LogicCondition
        {
            get { return _logicCondition; }
            set 
            {
                _logicCondition = value;
                ParseLogic();
            }
        }

        private string _title;

        
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                if (_title == null)
                    _title = "Title";
            }
        }

        public string fieldType;
        private object v1;
        private object v2;
        
        private int compareType = 0;
        public PmsFont()
        {
            _title = "Title";
            _logicCondition = "";
            _font = new Font("Arial", 10);
            _foreColor = Color.FromArgb(0, 0, 0);
            _backColor = Color.FromArgb(255, 255, 255);            
        }
        public PmsFont(string fieldType1)
        {
            this.fieldType = fieldType1;
            _title = "Title";
            _logicCondition = "";
            _font = new Font("Arial", 10);
            _foreColor = Color.FromArgb(0, 0, 0);
            _backColor = Color.FromArgb(255, 255, 255);            
        }

        public override string ToString()
        {
            if (_title == null)
                _title = "Title";
            return _title;
        }

        private void ParseLogic()
        {
            string PriString = _logicCondition;
            PriString = PriString.Trim();
            if (PriString.Length == 0)
            {
                v1 = null;
                v2 = null;
                compareType = 10;
                return;
            }
            if (fieldType == "INT" || fieldType == "FLOAT" || fieldType == "REAL" || fieldType == "DATETIME")
            {
                #region 整型
                
                char char1 = PriString[0];
                char char2 = PriString[PriString.Length - 1];
                if ((char1 == '(' || char1 == '[') && (char2 == ')' || char2 == ']'))
                {
                    int dotPos = PriString.IndexOf(',');
                    if (dotPos < 1)
                    {
                        v1 = null;
                        v2 = null;
                        compareType = 10;
                        return;
                    }
                    int endPos = PriString.IndexOf(char2);
                    string var1 = PriString.Substring(1, dotPos - 1);
                    string var2 = PriString.Substring(dotPos + 1, endPos - dotPos - 1);
                    bool b1 = false;
                    bool b2 = false;
                    try
                    {
                        if (fieldType == "INT")
                        {
                            v1 = Convert.ToInt32(var1);
                        }
                        else if (fieldType == "FLOAT" || fieldType == "REAL")
                        {
                            v1 = Convert.ToDouble(var1);
                        }
                        else if (fieldType == "DATETIME")
                        {
                            v1 = Convert.ToDateTime(var1);
                        }                        
                    }
                    catch
                    {
                        v1 = "-unlimited";
                        b1 = true;
                    }
                    try
                    {
                        if (fieldType == "INT")
                        {
                            v2 = Convert.ToInt32(var2);
                        }
                        else if (fieldType == "FLOAT" || fieldType == "REAL")
                        {
                            v2 = Convert.ToDouble(var2);
                        }
                        else if (fieldType == "DATETIME")
                        {
                            v1 = Convert.ToDateTime(var2);
                        }   
                    }
                    catch
                    {
                        v2 = "unlimited";
                        b2 = true;
                    }
                    if (b1 && b2)
                    {
                        compareType = 0;
                        return;
                    }
                    else if (b1 && !b2)
                    {
                        if (char2 == ')')
                        {
                            compareType = 5;
                        }
                        else if (char2 == ']')
                        {
                            compareType = 7;
                        }

                    }
                    else if (!b1 && b2)
                    {
                        if (char1 == '(')
                        {
                            compareType = 6;
                        }
                        else if (char1 == '[')
                        {
                            compareType = 8;
                        }
                    }
                    else if (!b1 && !b2)
                    {
                        if (char1 == '(' && char2 == ')')
                        {
                            compareType = 1;
                        }
                        else if (char1 == '(' && char2 == ']')
                        {
                            compareType = 2;
                        }
                        else if (char1 == '[' && char2 == ')')
                        {
                            compareType = 3;
                        }
                        else if (char1 == '[' && char2 == ']')
                        {
                            compareType = 4;
                        }
                    }

                }
                #endregion
            }
            else if (fieldType.StartsWith("VARCHAR"))
            {
                //支持1,= ;2,左包含;3,右包含;4,包含
                #region 字符串
                char char1 = PriString[0];
                char char2 = PriString[PriString.Length - 1];
                
                if (char1 == '=')
                {
                    compareType = 1;
                    v1 = PriString.Substring(1, PriString.Length - 1);
                }
                else if (char1 == '%' && char2 == '%')
                {
                    compareType = 4;
                    v1 = PriString.Substring(1, PriString.Length - 2);
                }
                else if (char1 == '%')
                {
                    compareType = 3;
                    v1 = PriString.Substring(1, PriString.Length - 1);
                }
                else if (char2 == '%')
                {
                    compareType = 2;
                    v1 = PriString.Substring(0, PriString.Length - 1);
                }
                else
                    compareType = 10;
                #endregion
            }
        }

        public bool IsLogic(object value)
        {
            try
            {
                if (fieldType == "INT" )
                {
                    return compareINT(value);
                }
                else if (fieldType == "FLOAT" || fieldType == "REAL" )
                {
                    return compareFloat(value);
                }
                else if ( fieldType == "DATETIME")
                {
                    return compareDateTime(value);
                }
                else if (fieldType.StartsWith("VARCHAR"))
                {
                    return compareVARCHAR(value);
                }
            }
            catch
            {
            }
            return false;
        }
        //比较类型 0 无需比较,总返回true
        // 1 ()
        // 2 (]
        // 3 [)
        // 4 []
        // 5 <
        // 6 >
        // 7 <=
        // 8 >=
        // 10 总返回false
        private bool compareINT(object value)
        {
            int var1 = -99999999;
            int var2 = 99999999;
            int var = 0;
            try
            {
                var1 = Convert.ToInt32(v1);                
            }
            catch
            {
            }
            try
            {                
                var2 = Convert.ToInt32(v2);
                
            }
            catch
            {
            }
            try
            {                
                var = Convert.ToInt32(value);
            }
            catch
            {
            }
            bool bRet = false;
            switch (compareType)
            {
                case 0:
                    return true;
                case 1:
                    if (var < var2 && var > var1)
                        bRet = true;
                    break;                    
                case 2:
                    if (var <= var2 && var > var1)
                        bRet = true;
                    break;
                case 3:
                    if (var < var2 && var >= var1)
                        bRet = true;
                    break;
                case 4:
                    if (var <= var2 && var >= var1)
                        bRet = true;
                    break;
                case 5:
                    if (var < var2)
                        bRet = true;
                    break;
                case 6:
                    if (var > var1)
                        bRet = true;
                    break;
                case 7:
                    if (var <= var2)
                        bRet = true;
                    break;
                case 8:
                    if (var >= var1)
                        bRet = true;
                    break;
                default:
                    return false;
            }
            return bRet;
        }

        private bool compareFloat(object value)
        {
            Double var1 = -99999999.0;
            Double var2 = 99999999.0;
            Double var = 0.0;
            try
            {
                var1 = Convert.ToDouble(v1);
            }
            catch
            {
            }
            try
            {
                var2 = Convert.ToDouble(v2);
            }
            catch
            {
            }
            try
            {
                var = Convert.ToDouble(value);
            }
            catch
            {
            }
            bool bRet = false;
            switch (compareType)
            {
                case 0:
                    return true;
                case 1:
                    if (var < var2 && var > var1)
                        bRet = true;
                    break;
                case 2:
                    if (var <= var2 && var > var1)
                        bRet = true;
                    break;
                case 3:
                    if (var < var2 && var >= var1)
                        bRet = true;
                    break;
                case 4:
                    if (var <= var2 && var >= var1)
                        bRet = true;
                    break;
                case 5:
                    if (var < var2)
                        bRet = true;
                    break;
                case 6:
                    if (var > var1)
                        bRet = true;
                    break;
                case 7:
                    if (var <= var2)
                        bRet = true;
                    break;
                case 8:
                    if (var >= var1)
                        bRet = true;
                    break;
                default:
                    return false;
            }
            return bRet;
        }

        private bool compareDateTime(object value)
        {
            DateTime var1 = new DateTime(1900,1,1);
            DateTime var2 = new DateTime(2200, 1, 1);
            DateTime var = DateTime.Now;
            try
            {
                var1 = Convert.ToDateTime(v1);
            }
            catch
            {
            }
            try
            {
                var2 = Convert.ToDateTime(v2);
            }
            catch
            {
            }
            try
            {
                var = Convert.ToDateTime(value);
            }
            catch
            {
            }
            bool bRet = false;
            switch (compareType)
            {
                case 0:
                    return true;
                case 1:
                    if (var < var2 && var > var1)
                        bRet = true;
                    break;
                case 2:
                    if (var <= var2 && var > var1)
                        bRet = true;
                    break;
                case 3:
                    if (var < var2 && var >= var1)
                        bRet = true;
                    break;
                case 4:
                    if (var <= var2 && var >= var1)
                        bRet = true;
                    break;
                case 5:
                    if (var < var2)
                        bRet = true;
                    break;
                case 6:
                    if (var > var1)
                        bRet = true;
                    break;
                case 7:
                    if (var <= var2)
                        bRet = true;
                    break;
                case 8:
                    if (var >= var1)
                        bRet = true;
                    break;
                default:
                    return false;
            }
            return bRet;
        }

        //支持1,= ;2,左包含;3,右包含;4,包含
        private bool compareVARCHAR(object value)
        {
            string var1 = v1.ToString();  //条件          
            string var = value.ToString(); //值
            bool bRet = false;
            switch (compareType)
            {
                case 0:
                    return true;
                case 1:
                    if (var == var1)
                        bRet = true;
                    break;
                case 2:
                    if (var.StartsWith(var1))
                        bRet = true;
                    break;
                case 3:
                    if (var.EndsWith(var1))
                        bRet = true;
                    break;
                case 4:
                    if (var.Contains(var1))
                        bRet = true;
                    break;
                default:
                    return false;
            }
            return bRet;
        }
    }

    /// <summary>
    /// strFormat   格式字符串,具体含义参考微软相关文档,http://msdn.microsoft.com/zh-cn/library/8kb3ddd4.aspx
    /// bkColor     背景色
    /// </summary>
    [Serializable]
    public class PmsFormat
    {
        public string strValue;
        public string strFormat; 
        public bool bPassword;
        public bool bPassSave;
        public List<PmsFont> fontList;
        private DataGridViewContentAlignment _alignment;

        public DataGridViewContentAlignment Alignment
        {
            get { return _alignment; }
            set { _alignment = value; }
        }
        public PmsFormat(string fieldType)
        {
            strFormat = "";
            bPassword = false;
            bPassSave = false;
            fontList = new List<PmsFont>();
            fontList.Add(new PmsFont(fieldType));//至少包含一个默认节点
            _alignment = DataGridViewContentAlignment.MiddleLeft;
            
            if (fieldType == "BIT")
            {
                strValue = "是/否";
            }
            else if (fieldType == "INT")
            {
                strFormat = "D";
                strValue = "12345";
                _alignment = DataGridViewContentAlignment.MiddleRight;
            }
            else if (fieldType == "FLOAT")
            {
                strFormat = "F2";
                strValue = "12345.123456789001";
                _alignment = DataGridViewContentAlignment.MiddleRight;
            }
            else if ( fieldType == "REAL")
            {
                strFormat = "F4";
                strValue = "12345.123456789001";
                _alignment = DataGridViewContentAlignment.MiddleRight;
            }
            else if (fieldType.StartsWith("VARCHAR"))
            {
                string strLength = fieldType.Substring(fieldType.IndexOf('(') + 1);
                strLength = strLength.Substring(0, strLength.IndexOf(')'));
                strFormat = strLength;
                strValue = "AaBbCc...";
            }
            else if (fieldType == "GUID")
            {                
                strFormat = "32";
                strValue = "0000-0000...";
            }
            else if (fieldType == "DATETIME")
            {
                strFormat = "yyyy-MM-dd HH:mm:ss";
                strValue = "2009-09-28 12:12:12";
            }
        }

        //public static PmsFormat operator = (PmsFormat pf1,PmsFormat pf1)
        
    }
    [Serializable]
    public class CustomCtrlProperty
    {
        public Type CtrlType;
        public Color BackColor;
        public Color ForeColor;
        public Font Font1;
        public Point Location;
        public Size ClientSize;
        public string RField;
        public string RType;//关联字段类型,给查询条件使用,详细信息中不需要该信息
        public string RName;//图片控件中关联附件的后缀名,字段控件中关联字段表名 
        public string Text;
        public ComboBoxItemData ExplainData;// = new List<string>();
        public PictureBoxSizeMode SizeMode;
        public ComboBoxStyle DropDownStyle;
        public char PasswordChar;
        public Guid RNode;
        public ContentAlignment TextAlign;
        public List<CustomCtrlProperty> Childs;//2012.1.30 增加对容器型控件处理
        public bool Sorted = false;// 2013.06.20 增加ComboBoxEx的Sorted属性
        public bool AutoShowScreenKeyboard = false; // 2013.11.15 增加TextBoxEx的AutoShowScreenKeyboard属性

        //2013.1.8 增加对数字控件的处理
        public int DecimalPlaces;
        public decimal Maximum;
        public decimal Minimum;
        public decimal Increment;
        public decimal Value;
    }
    
    //combobox 列表属性类
    [Serializable]
    public class ComboBoxItemData
    {
        public ComboBoxItemData()
        {
            IsSolid = false;
            ExplainList = new List<TableField>();
            SortType = SortType.None;
        }

        public bool IsSolid;
        public List<TableField> ExplainList;
        public string tableName;
        public string fieldName;
        public string DBString;
        public SortType SortType = SortType.None;
    }

    public enum SortType
    {
        None,
        ASC,
        DESC
    }

    [Serializable]
    public class QueryResultObj
    {
        public Guid UniqueID;
        public object value;
    }

    public class ConstData
    {
        //控件路径
        public const string ControlPath = "ControlPath";

        //数据源树
        public const string DataSourceTree = "DataTree";

        //编、解码方式
        public const string AssciiEncodingTypeCn = "GB18030";

        //报表序列化信息区域的固定长度
        public const int ContextLength = 400;

        //报表对象序列化信息区域的固定长度
        public const int ObjectLength = 15;
    }

    /// <summary>
    /// 2011.12.16 增加
    /// 目的:为图表控件提供接口,需要实现以下这些属性、方法
    /// </summary>
    public interface IChartFunction
    {
        /// <summary>
        /// 图表控件此时的运行模式(0 设计时,1为运行时)
        /// </summary>
        int RunMode
        {
            get;
            set;
        }
        /// <summary>
        /// 图表控件运行时所需的实际数据
        /// </summary>
        DataTable ReportData
        {
            get;
            set;
        }
        /// <summary>
        /// 图表控件在运行时提供打印、绘制方法
        /// </summary>
        /// <param name="graphics">指定的画板</param>
        /// <param name="position">指定的范围</param>
        void PrintPaint(Graphics graphics, Rectangle position);
        /// <summary>
        /// 图表控件提供缩放的方法
        /// </summary>
        /// <param name="hScale">水平缩放系数</param>
        /// <param name="vScale">垂直缩放系数</param>
        void Zoom(float hScale, float vScale);
        /// <summary>
        /// 图表控件提供初始化X轴数据
        /// </summary>
        void InitailColumnData();
        /// <summary>
        /// 图表控件提供克隆方法
        /// </summary>
        /// <returns>返回控件的克隆</returns>
        object Clone();
        /// <summary>
        /// 图表控件的数据设定
        /// </summary>
        SourceField SourceField 
        { 
            get;
            set;
        }
        /// <summary>
        /// 图表控件提供坐标信息
        /// </summary>
        Point Location
        {
            get;
            set;
        }
        /// <summary>
        /// 图表控件的宽度
        /// </summary>
        int Width
        {
            get;
            set;
        }
        /// <summary>
        /// 图表控件的高度
        /// </summary>
        int Height
        {
            get;
            set;
        }
        /// <summary>
        /// 图表控件的大小
        /// </summary>
        Size Size
        {
            get;
            set;
        }
    }
    /// <summary>
    /// 2012.4.11 增加
    /// 目的:为了增加一个特殊的分段曲线,需要而外提供一个分段曲线接口
    /// </summary>
    public interface IChartSection : IChartFunction
    {
        /// <summary>
        /// 一个页面所包含的点的个数
        /// </summary>
        int PageNum
        {
            get;
            set;
        }
        /// <summary>
        /// 根据给定的数据表获取需要完成的控件个数
        /// </summary>
        /// <param name="Aim"></param>
        /// <returns></returns>
        int GetPagesFromData(DataTable Aim);
        /// <summary>
        /// 给控件赋值,以及页面索引
        /// </summary>
        /// <param name="Aim"></param>
        /// <param name="Index"></param>
        void SetData(DataTable Aim, int Index);
    }

    public partial class Environment
    {
        private static MES.ToolBox.MESScreenKeyBoard _osk = new MES.ToolBox.MESScreenKeyBoard();
        public static MES.ToolBox.MESScreenKeyBoard Osk
        {
            get { return _osk; }
        }
    }
}
