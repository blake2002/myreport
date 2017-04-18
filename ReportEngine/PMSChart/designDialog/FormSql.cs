using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
//using PMS.Libraries.ToolControls.ToolBox;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using PMS.Libraries.ToolControls.PmsSheet.WhereLibrary;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using PMS.Libraries.ToolControls.PMSPublicInfo;
using System.Windows.Forms.DataVisualization.Charting;

namespace PMS.Libraries.ToolControls.PMSChart
{
    public partial class FormSql : Form
    {
        public FormSql()
        {
            InitializeComponent();
            _listBoxTableSel = new ListBox();
            _listBoxTableSel.MouseClick += new MouseEventHandler(_listBoxTableSel_MouseClick);
            _listBoxTableSel.Leave += new EventHandler(_listBoxTableSel_FontChanged);
            _listBoxTableSel.Location = new System.Drawing.Point(buttonAddData.Location.X + this.groupBox3.Location.X, buttonAddData.Location.Y + this.groupBox3.Location.Y + buttonAddData.Size.Height);
            _listBoxTableSel.Size = new System.Drawing.Size(200, 100);
            this.dataSource.Controls.Add(_listBoxTableSel);
            _listBoxTableSel.Visible = false;

            //_pmsTableList = PMSDBStructure.GetAllTables();

            if (_pmsJoinRelation == null)
                _pmsJoinRelation = new List<TableJoinRelation>();
        }
        public PMSChartCtrl ChartParent;
        private List<string> selectFieldList = new List<string>();
        private List<string> groupByList = new List<string>();
        private List<SortClass> sortList = new List<SortClass>();
        //数据列表
        private List<string> seriesData = new List<string>();
        //外观已选数据列表
        private List<string> seriesDataForAppearance = new List<string>();
        //区域已选数据列表
        private List<string> seriesDataForChartArea = new List<string>();
        //图例已选数据列表
        private List<string> seriesDataForLegend = new List<string>();

        //标题已选数据列表
        private List<string> titleDataForChartArea = new List<string>();
        //标题已选数据列表
        private List<string> legendDataForChartArea = new List<string>();

        private PMSChartApp chartApp;
        private bool _IsReport;

        public PMSChartApp PMSChartAppearance
        {
            get
            {
                if (chartApp == null)
                    chartApp = new PMSChartApp(null);
                TreeNode chartAreaParent = treeView1.Nodes["chartAppearance"];

                chartApp = (PMSChartApp)(chartAreaParent.Tag);
                //if (chartAreaParent.Nodes.Count>0)
                    //chartApp = (PMSChartApp)((chartAreaParent.Nodes[0]).Tag);
                return chartApp;
            }
            set
            {
                chartApp = value;
                if (chartApp == null)
                {
                    chartApp = new PMSChartApp(null);
                }
                TreeNode chartAreaParent = treeView1.Nodes["chartAppearance"];

                chartAreaParent.Tag = chartApp;
                /*/
                TreeNode child = new TreeNode(pca.Name);
                child.Tag = pca;
                child.ImageIndex = 1;
                child.SelectedImageIndex = 1;
                chartAreaParent.Nodes.Add(child);/*/
            }
        }

        [Description("是否嵌入报表")]
        [Category("MES报表属性")]
        [DefaultValue(false)]
        public bool IsReport
        {
            get
            {
                return _IsReport;
            }
            set
            {
                _IsReport = value;
                if (value == false)
                {
                    _pmsTableList = PMSDBStructure.GetAllTables();
                }
            }
        }
        private List<PMSChartArea> chartAreaList = new List<PMSChartArea>();

        public List<PMSChartArea> ChartAreaList
        {
            get 
            {
                if(chartAreaList == null)
                    chartAreaList = new List<PMSChartArea>();
                chartAreaList.Clear();
                TreeNode chartAreaParent = treeView1.Nodes["chartArea"];

                foreach (TreeNode tn in chartAreaParent.Nodes)
                {
                    chartAreaList.Add((PMSChartArea)(tn.Tag));
                }
                return chartAreaList;
            }
            set 
            {
                chartAreaList = value;
                if (chartAreaList == null)
                {
                    chartAreaList = new List<PMSChartArea>();
                }
                if (chartAreaList.Count == 0)
                {
                    PMSChartArea pca = new PMSChartArea(null);
                    chartAreaList.Add(pca);
                }
                TreeNode chartAreaParent = treeView1.Nodes["chartArea"];
                chartAreaParent.Nodes.Clear();

                foreach (PMSChartArea pca in chartAreaList)
                {
                    TreeNode child = new TreeNode(pca.Name);
                    child.Tag = pca;
                    child.ImageIndex = 1;
                    child.SelectedImageIndex = 1;
                    chartAreaParent.Nodes.Add(child);
                }

            }
        }

        private List<PMSSeries> seriesList = new List<PMSSeries>();

        public List<PMSSeries> SeriesList
        {
            get
            {
                if (seriesList == null)
                    seriesList = new List<PMSSeries>();
                seriesList.Clear();
                TreeNode chartAreaParent = treeView1.Nodes["series"];

                foreach (TreeNode tn in chartAreaParent.Nodes)
                {
                    seriesList.Add((PMSSeries)(tn.Tag));
                }
                return seriesList;
            }
            set
            {
                seriesList = value;
                if (seriesList == null)
                    return;
                TreeNode chartAreaParent = treeView1.Nodes["series"];
                chartAreaParent.Nodes.Clear();

                foreach (PMSSeries pca in seriesList)
                {
                    TreeNode child = new TreeNode(pca.Name);
                    child.Tag = pca;
                    child.ImageIndex = 3;
                    child.SelectedImageIndex = 3;
                    chartAreaParent.Nodes.Add(child);
                }

            }
        }

        private List<PMSLegend> legendList = new List<PMSLegend>();

        public List<PMSLegend> LegendList
        {
            get
            {
                if (legendList == null)
                    legendList = new List<PMSLegend>();
                legendList.Clear();
                TreeNode chartAreaParent = treeView1.Nodes["legend"];

                foreach (TreeNode tn in chartAreaParent.Nodes)
                {
                    legendList.Add((PMSLegend)(tn.Tag));
                }
                return legendList;
            }
            set
            {
                legendList = value;
                if (legendList == null)
                    return;
                TreeNode chartAreaParent = treeView1.Nodes["legend"];
                chartAreaParent.Nodes.Clear();

                foreach (PMSLegend pca in legendList)
                {
                    TreeNode child = new TreeNode(pca.Name);
                    child.ImageIndex = 2;
                    child.SelectedImageIndex = 2;
                    child.Tag = pca;
                    chartAreaParent.Nodes.Add(child);
                }

            }
        }

        private List<PMSTitle> titleList = new List<PMSTitle>();

        public List<PMSTitle> TitleList
        {
            get
            {
                if (titleList == null)
                    titleList = new List<PMSTitle>();
                titleList.Clear();
                TreeNode chartAreaParent = treeView1.Nodes["title"];

                foreach (TreeNode tn in chartAreaParent.Nodes)
                {
                    titleList.Add((PMSTitle)(tn.Tag));
                }
                return titleList;
            }
            set
            {
                titleList = value;
                if (titleList == null)
                    return;
                TreeNode chartAreaParent = treeView1.Nodes["title"];
                chartAreaParent.Nodes.Clear();

                foreach (PMSTitle pca in titleList)
                {
                    TreeNode child = new TreeNode(pca.Name);
                    child.ImageIndex = 0;
                    child.SelectedImageIndex = 0;
                    child.Tag = pca;
                    chartAreaParent.Nodes.Add(child);
                }

            }
        }

        private string oldSqlForAppearance = "";

        public string MainGroupBy
        {
            get
            {
                return mainField.Text;
            }
            set
            {
                mainField.Text = value;                
            }
        }

        public string XAixs
        {
            get
            {
                return comboBoxX.Text;
            }
            set
            {
                comboBoxX.Text = value;
                if (ChartParent != null && ChartParent.IsReport == true)
                {
                    comboBoxX.Items.Clear();
                    if (!string.IsNullOrEmpty(ChartParent.XRecordField))
                    {
                        comboBoxX.Items.Add(ChartParent.XRecordField);
                        comboBoxX.Text = ChartParent.XRecordField;
                    }
                }
            }
        }
        public List<string> XAixsies
        {
            get
            {
                List<string> Xaixs = new List<string>();
                for (int i = 0; i < comboBoxX.Items.Count; i++)
                {
                    Xaixs.Add((string)comboBoxX.Items[i]);
                }
                return Xaixs;
            }
            set
            {
                if (value != null)
                {
                    if (ChartParent != null && ChartParent.IsReport == true)
                    {
                        return;
                    }
                    List<string> Xaixs = value;
                    comboBoxX.Items.Clear();
                    foreach (string y in Xaixs)
                        comboBoxX.Items.Add(y);
                }
            }
        }
        private List<string> _yAixs;
        public List<string> YAixs
        {
            get
            {
                if(_yAixs==null)
                    _yAixs = new List<string>();

                _yAixs.Clear();
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if(checkedListBox1.GetItemChecked(i))
                        _yAixs.Add((string)checkedListBox1.Items[i]);
                }
                return _yAixs;
            }
            set
            {
                _yAixs = value;
            }
        }


        //= new Dictionary<string, Axis>();
        public string SecondaryGroupBy
        {
            get
            {
                return secondaryField.Text;
            }
            set
            {
                secondaryField.Text = value;
            }
        }

        private DataSource _sqlSource;
        public DataSource SqlSource
        {
            get 
            {
                _sqlSource = new DataSource(this);
                return _sqlSource;
            }
            set 
            { 
                _sqlSource = value;
                if (_sqlSource != null)
                {
                    _sqlSource.populateFormSql(this);
                }
            }
        }
        //所有数据源表
        List<string> _pmsTableList = null;

        //已选数据源表
        List<string> _selectedTableList = new List<string>();
        public List<string> SelectedTableList
        {
            get 
            {
                _selectedTableList.Clear();
                foreach(object table in listBoxDataSource.Items)
                {
                    _selectedTableList.Add((string)table);
                }
                return _selectedTableList; 
            }
            set 
            { 
                _selectedTableList = value;
                listBoxDataSource.Items.Clear();
                foreach(string table in _selectedTableList)
                {
                    listBoxDataSource.Items.Add(table);
                    editFieldInfo(table, 0);
                }
            }
        }

        //表达式，包括单列字段
        List<string> _formulaList = new List<string>();
        public List<string> FormulaList
        {
            get 
            {
                _formulaList.Clear();
                foreach(object table in listBoxFormula.Items)
                {
                    _formulaList.Add((string)table);
                }
                return _formulaList; 
            }
            set 
            { 
                _formulaList = value;
                listBoxFormula.Items.Clear();
                foreach(string table in _formulaList)
                {
                    listBoxFormula.Items.Add(table);
                }
            }
        }

        //所有表字段以及表达式
        private List<PmsField> _pmsFieldList = new List<PmsField>();

        //表选择控件
        private ListBox _listBoxTableSel;

        //已选表之间的关系，新增时默认为内联
        private List<TableJoinRelation> _pmsJoinRelation = null;
        public List<TableJoinRelation> PmsJoinRelation
        {
            get { return _pmsJoinRelation; }
            set 
            {
                _pmsJoinRelation = value;
                if(_pmsJoinRelation.Count==0)
                    buttonRelation.Enabled = false;
                else if (_pmsJoinRelation.Count > 0)
                    buttonRelation.Enabled = true;
            }
        }
        //记录表关系，缓存
        private Dictionary<TableField, TableJoinRelation> tableRelationCollection = new Dictionary<TableField, TableJoinRelation>();

        //查询条件，多表，需改造
        private TreeViewDataAccess.TreeViewData whereData;

        public TreeViewDataAccess.TreeViewData WhereData
        {
            get { return whereData; }
            set { whereData = value; }
        }

        //直接编写的sql
        public string ResultSql
        {
            get 
            {
                    return sqlText.Text;
            }
            set { sqlText.Text = value; }
        }

        //配置的sql
        public string ConfigSql
        {
            get 
            {
                    return getConfigSql();
            }
            set { textBoxConfigSql.Text = value; }
        }

        //启用配置sql
        public bool UsingConfig
        {
            get 
            {
                return !checkBoxUsing.Checked;
            }
            set { checkBoxUsing.Checked = !value; }
        }
      

        //排序
        public List<SortClass> SortList
        {
            get { return sortList; }
            set { sortList = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection _SqlConnection1 = new SqlConnection(@"Data Source=MATH\SQL2005;Initial Catalog=PMSCenter;Persist Security Info=True;User ID=sa;Password=123");
            if(_SqlConnection1 == null)
            {
                MessageBox.Show("连接错误");
                return;
            }
            _SqlConnection1.Open();
            if( _SqlConnection1.State != ConnectionState.Open)
            {
                MessageBox.Show("连接错误");
                return;
            }
            SqlCommand thisCommand = _SqlConnection1.CreateCommand();
            thisCommand.CommandText = sqlText.Text;

                
            //PMS.Libraries.ToolControls.PMSPublicInfo.Message.Info(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, thisCommand.CommandText, false);
                
            try
            {
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                //System.Data.DataTable dt = new System.Data.DataTable();
            }
            catch(Exception ec)
            {
                MessageBox.Show(ec.Message);
                thisCommand.Dispose();
                _SqlConnection1.Close();
                return;
            }
            MessageBox.Show("测试成功！");
            thisCommand.Dispose();
            _SqlConnection1.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.listBoxFormula.SelectedItem != null)
            {
                FormQueryFormula fqf = new FormQueryFormula();
                fqf.FormSql1 = this;
                fqf.OperaterType = 0;
                List<string> tableList = new List<string>();
                foreach (string tableName in listBoxDataSource.Items)
                {
                    tableList.Add(tableName);
                }
                if (tableList.Count == 0)
                    return;
                fqf.FormulaText = (string)this.listBoxFormula.SelectedItem;
                fqf.TableList = tableList;
                fqf.PmsFieldList = this._pmsFieldList;
                if (fqf.ShowDialog(this) == DialogResult.OK)
                {
                    this.listBoxFormula.Items[listBoxFormula.SelectedIndex] = fqf.FormulaText;
                }
            }
        }

        private void buttonAddData_Click(object sender, EventArgs e)
        {
            _listBoxTableSel.Items.Clear();
            if (_pmsTableList != null)
            {
                foreach (string tableName in _pmsTableList)
                {
                    bool bFind = false;
                    foreach (string tableName1 in listBoxDataSource.Items)
                    {
                        if (tableName == tableName1)
                        {
                            bFind = true;
                            break;
                        }
                    }
                    if (bFind == true)
                        continue;

                    _listBoxTableSel.Items.Add(tableName);

                }
                _listBoxTableSel.BringToFront();
                _listBoxTableSel.Visible = true;
                _listBoxTableSel.Focus();
            }
            
        }

        void _listBoxTableSel_MouseClick(object sender, MouseEventArgs e)
        {
            if (_listBoxTableSel.Items.Count == 0)
            {
                _listBoxTableSel.Visible = false;
                return;
            }
            if (_listBoxTableSel.SelectedIndex < 0)
                return;
            string tableName = (string)_listBoxTableSel.SelectedItem;
            
            Microsoft.SqlServer.Management.Smo.Table table = SqlStructure.GetDatabaseTableObj(tableName);
            //DataTable tableForeign = table.EnumForeignKeys();//该表为主表，寻找其外键表
            List<View_GetTableKeyRelation> tableForeign = PMSDBStructure.GetTableKeyRelationFromPKTable(tableName);  
            //ForeignKeyCollection tableMain =  table.ForeignKeys;//该表为外键表，寻找主键表  
            List<View_GetTableKeyRelation> tableMain =  PMSDBStructure.GetTableKeyRelationFromFKTable(tableName);  

            for (int i = 0; i < listBoxDataSource.Items.Count; i++)
            {
                string existTable = (string)listBoxDataSource.Items[i];

                bool bFindForeign = false;
                #region 找外键表
                foreach (View_GetTableKeyRelation row in tableForeign)
                {                    
                    string relatedTable = row.ForeignKeyTableName;
                
                    if (existTable.Equals(relatedTable,StringComparison.InvariantCultureIgnoreCase))//已经存在的表与当前选择的表有关系,当前表为主表
                    {
                        TableField tRelation = new TableField();
                        tRelation.tableName = tableName;
                        tRelation.fieldName = relatedTable;
                        if (tableRelationCollection.ContainsKey(tRelation))
                        {
                            _pmsJoinRelation.Add(tableRelationCollection[tRelation]);                            
                        }
                        else
                        {
                            TableJoinRelation tjr = new TableJoinRelation();
                            tjr.compare = "=";
                            tjr.mainColumn = row.PrimaryKeyFieldName;
                            tjr.mainTable = tableName;
                            tjr.secondaryColumn = row.ForeignKeyFieldName;
                            tjr.secondaryTable = existTable;
                            _pmsJoinRelation.Add(tjr); 

                            tableRelationCollection.Add(tRelation, tjr);
                        }

                        #region old
                        foreach (PmsField pf in _pmsFieldList)
                        {
                            if (pf.fieldForeigner == true)
                            {
                                if (listBoxDataSource.Items.Count == 1)//数据源已存在表仅1个
                                {
                                    TableField tf = new TableField();

                                    if (pf.fieldName[0] == '[' && pf.fieldName[pf.fieldName.Length - 1] == ']')
                                        tf.fieldName = pf.fieldName.Substring(1, pf.fieldName.Length - 2);
                                    else
                                        tf.fieldName = pf.fieldName;

                                    tf.tableName = existTable;
                                    TableField mainTable = new TableField();
                                    mainTable.tableName = tableName;
                                    mainTable.fieldName = row.PrimaryKeyFieldName;// PMSDBStructure.GetTablePrimaryColumn(tableName);


                                    if (!(string.IsNullOrEmpty(mainTable.tableName)) && mainTable.tableName == tableName)//找到主表，且为新增表
                                    {
                                        TableJoinRelation tjr = new TableJoinRelation();
                                        tjr.compare = "=";
                                        tjr.mainColumn = tf.fieldName;
                                        tjr.mainTable = existTable;
                                        tjr.secondaryColumn = mainTable.fieldName;
                                        tjr.secondaryTable = mainTable.tableName;
                                        _pmsJoinRelation.Add(tjr);

                                        TableField tfNew = new TableField();
                                        tfNew.fieldName = relatedTable;
                                        tfNew.tableName = tableName;
                                        tableRelationCollection.Add(tfNew, tjr);
                                        break;
                                    }
                                }
                                else
                                {
                                    if (pf.fieldName.StartsWith("[" + existTable + "]."))//该表
                                    {
                                        TableField tf = new TableField();

                                        int start = pf.fieldName.IndexOf("].[");//].[
                                        if (start > 0)
                                        {
                                            tf.fieldName = pf.fieldName.Substring(start + 2);
                                            if (tf.fieldName[0] == '[' && tf.fieldName[tf.fieldName.Length - 1] == ']')
                                                tf.fieldName = tf.fieldName.Substring(1, tf.fieldName.Length - 2);
                                        }
                                        else
                                            tf.fieldName = "";
                                        tf.tableName = existTable;

                                        TableField mainTable = new TableField();
                                        mainTable.tableName = tableName;
                                        mainTable.fieldName = PMSDBStructure.GetTablePrimaryColumn(tableName);

                                        if (!(string.IsNullOrEmpty(mainTable.tableName)) && mainTable.tableName == tableName)//找到主表，且为新增表
                                        {
                                            TableJoinRelation tjr = new TableJoinRelation();
                                            tjr.compare = "=";
                                            tjr.mainColumn = tf.fieldName;
                                            tjr.mainTable = existTable;
                                            tjr.secondaryColumn = mainTable.fieldName;
                                            tjr.secondaryTable = mainTable.tableName;
                                            _pmsJoinRelation.Add(tjr);

                                            TableField tfNew = new TableField();
                                            tfNew.fieldName = relatedTable;
                                            tfNew.tableName = tableName;
                                            tableRelationCollection.Add(tfNew, tjr);
                                            break;
                                        }
                                    }                                    
                                }
                            }
                        }
                        #endregion
                        bFindForeign = true;
                        break;//找到关系
                    }
                }
                #endregion

                if (bFindForeign == true)
                    continue;
                foreach (View_GetTableKeyRelation row1 in tableMain)
                {
                    if (row1.PrimaryKeyTableName.Equals(existTable,StringComparison.InvariantCultureIgnoreCase))
                    {
                        TableField tRelation = new TableField();
                        tRelation.tableName = existTable;
                        tRelation.fieldName = tableName;
                        if (tableRelationCollection.ContainsKey(tRelation))
                        {
                            _pmsJoinRelation.Add(tableRelationCollection[tRelation]);                           
                            
                        }
                        else
                        {
                            TableJoinRelation tjr = new TableJoinRelation();
                            tjr.compare = "=";
                            tjr.mainColumn = row1.PrimaryKeyFieldName;
                            tjr.mainTable = tableName;
                            tjr.secondaryColumn = row1.ForeignKeyFieldName;
                            tjr.secondaryTable = existTable;
                            _pmsJoinRelation.Add(tjr);

                            tableRelationCollection.Add(tRelation, tjr);
                        }
                        #region old
                        //List<Microsoft.SqlServer.Management.Smo.Column> _pmsFieldColList
                        //    = SqlStructure.getColumnInfo(tableName);
                        //foreach (Microsoft.SqlServer.Management.Smo.Column col in _pmsFieldColList)
                        //{
                        //    if (col.IsForeignKey == true)
                        //    {
                        //        TableField tf = new TableField();
                        //        tf.fieldName = col.Name;
                        //        tf.tableName = tableName;
                        //        TableField mainTable = SqlStructure.getMainKeyTable(tf);

                        //        //找到主表，且为已经存在的表
                        //        if (!(string.IsNullOrEmpty(mainTable.tableName)) && (mainTable.tableName == existTable))
                        //        {
                        //            TableJoinRelation tjr = new TableJoinRelation();
                        //            tjr.compare = "=";
                        //            tjr.mainColumn = mainTable.fieldName;
                        //            tjr.mainTable = mainTable.tableName;
                        //            tjr.secondaryColumn = tf.fieldName;
                        //            tjr.secondaryTable = tf.tableName;
                        //            _pmsJoinRelation.Add(tjr);

                        //            TableField tfNew = new TableField();
                        //            tfNew.fieldName = tableName;
                        //            tfNew.tableName = existTable;
                        //            tableRelationCollection.Add(tfNew, tjr);
                        //            break;
                        //        }
                        //    }
                        //}
                        #endregion
                        break;
                    }
                }
            }
            if (_pmsJoinRelation.Count > 0)
                buttonRelation.Enabled = true;
            listBoxDataSource.Items.Add(tableName);
            listBoxDataSource.SelectedIndex = listBoxDataSource.Items.Count - 1;
            _listBoxTableSel.Visible = false;
            try
            {
                editFieldInfo((string)_listBoxTableSel.SelectedItem, 0);
            }
            catch (Exception ea)
            {
                MessageBox.Show(ea.Message);
            }
        }

        void _listBoxTableSel_FontChanged(object sender, EventArgs e)
        {
            if(_listBoxTableSel.Focused == false)
                _listBoxTableSel.Visible = false;
        }

        private void buttonDeleteData_Click(object sender, EventArgs e)
        {
            if (listBoxDataSource.SelectedIndex < 0)
                return;
            int oldIndex = listBoxDataSource.SelectedIndex;
            string removedTable = (string)(listBoxDataSource.Items[oldIndex]);                     
            listBoxDataSource.Items.RemoveAt(listBoxDataSource.SelectedIndex);
            editFieldInfo(removedTable, 1); 
  
            int nCount = this._pmsJoinRelation.Count();
            for (int i = nCount; i > 0; i--)
            {
                TableJoinRelation tjr = _pmsJoinRelation[i-1];
                if (tjr.secondaryTable == removedTable || tjr.mainTable == removedTable)
                {
                    _pmsJoinRelation.Remove(tjr);
                }
            }
            if (_pmsJoinRelation.Count == 0)
                buttonRelation.Enabled = false;
            if (listBoxDataSource.Items.Count <= oldIndex)
            {
                listBoxDataSource.SelectedIndex = listBoxDataSource.Items.Count - 1;
            }
            else
            {
                listBoxDataSource.SelectedIndex = oldIndex;
            }
        }

        private void addGroup_Click(object sender, EventArgs e)
        {
            FormQueryFormula fqf = new FormQueryFormula();
            fqf.FormSql1 = this;
            fqf.OperaterType = 1;//新增
            List<string> tableList = new List<string>();
            foreach (string tableName in listBoxDataSource.Items)
            {
                tableList.Add(tableName);
            }
            if (tableList.Count == 0)
                return;
            fqf.TableList = tableList;
            //MessageBox.Show(_pmsFieldList.Count.ToString());
            fqf.PmsFieldList = this._pmsFieldList;
            if (fqf.ShowDialog(this) == DialogResult.OK)
            {
                if(fqf.FormulaText.Length>0&&fqf.IsAppied())
                    this.listBoxFormula.Items.Add(fqf.FormulaText);
            }
        }
        public void AddFormula(string text,int type)
        {
            if(type==1)
                this.listBoxFormula.Items.Add(text);
            else if(type==0)
                this.listBoxFormula.Items[listBoxFormula.SelectedIndex] = text;
        }

        private void deleteGroup_Click(object sender, EventArgs e)
        {
            if (listBoxFormula.SelectedIndex < 0)
                return;
            int oldIndex = listBoxFormula.SelectedIndex;
            listBoxFormula.Items.RemoveAt(listBoxFormula.SelectedIndex);

            if (listBoxFormula.Items.Count <= oldIndex)
            {
                listBoxFormula.SelectedIndex = listBoxFormula.Items.Count - 1;
            }
            else
            {
                listBoxFormula.SelectedIndex = oldIndex;
            }
        }

        /// <summary>
        /// 编辑字段信息
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="isAdd">0,增加,1,去除</param>
        private void editFieldInfo(string table, int isAdd)
        {
            int sourceCount = listBoxDataSource.Items.Count;
            
            if (isAdd == 0)
            {
                //List<Microsoft.SqlServer.Management.Smo.Column> _pmsFieldColList
                   // = SqlStructure.getColumnInfo(table);

                List<PmsField> _pmsFieldColList = PmsField.InitialFieldList(table);
                
                if (sourceCount == 2)//将原来的加上表名
                {
                    string oldTable = (string)(listBoxDataSource.Items[0]);
                    if (oldTable == table)
                    {
                        oldTable = (string)(listBoxDataSource.Items[1]);
                    }
                    
                    for (int i = 0; i < _pmsFieldList.Count; i++)
                    {
                        PmsField field = _pmsFieldList[i];
                        field.fieldName = "["+oldTable+"]."+field.fieldName;
                        _pmsFieldList[i] = field;
                    }
                }
                foreach (PmsField col in _pmsFieldColList)
                {
                    PmsField field = new PmsField();
                    
                    if(sourceCount==1)
                        field.fieldName = "[" + col.fieldName + "]";                    
                    else if (sourceCount > 1)
                    {
                        field.fieldName = "[" + table + "].[" + col.fieldName + "]";
                    }
                    //PmsField
                    field.fieldType = col.fieldType;

                    field.fieldKey = col.fieldKey;
                    field.fieldForeigner = col.fieldForeigner;
                    _pmsFieldList.Add(field);
                }
            }
            else if (isAdd == 1)
            {
                if (sourceCount == 0)
                    _pmsFieldList.Clear();
                else
                {
                    int nCount = _pmsFieldList.Count;
                    for (int i = nCount - 1; i >= 0; i--)
                    {
                        PmsField field = _pmsFieldList[i];
                        string tableName = field.fieldName.Substring(1, field.fieldName.IndexOf(']') - 1);

                        if (tableName == table)
                            _pmsFieldList.RemoveAt(i);
                    }
                    if (sourceCount == 1)//去除表名
                    {
                        for (int j = 0; j < _pmsFieldList.Count; j++)
                        {
                            PmsField field = _pmsFieldList[j];
                            int start = field.fieldName.IndexOf("].[");//].[
                            if (start > 0)
                                field.fieldName = field.fieldName.Substring(start + 2);
                            _pmsFieldList[j] = field;
                        }
                    }
                }
            }
            addFieldCombox();
        }

        //public string ToPMSDataType(string sqlDataType, int length)
        //{
        //    string strType = sqlDataType.ToLower();
        //    string newType = "NoDefinedType";
        //    if (strType.Equals("bit"))
        //    {
        //        newType = "BIT";
        //    }
        //    else if (strType.Equals("varchar") || strType.Equals("nvarchar"))
        //    {
        //        newType = "VARCHAR(" + length.ToString() + ")"; ;
        //    }
        //    else if (strType.Equals("float"))
        //    {
        //        newType = "FLOAT";
        //    }
        //    else if (strType.Equals("real"))
        //    {
        //        newType = "REAL";
        //    }
        //    else if (strType.Equals("int") || strType.Equals("bigint") || strType.Equals("tinyint"))
        //    {
        //        newType = "INT";
        //    }
        //    else if (strType.Equals("datetime"))
        //    {
        //        newType = "DATETIME";
        //    }
        //    else if (strType.Equals("image"))
        //    {
        //        newType = "IMAGE";
        //    }
        //    else if (strType.Equals("uniqueidentifier"))
        //    {
        //        newType = "GUID";
        //    }
        //    else
        //        newType = "NoDefinedType";
        //    return newType;
        //}
        private void addFieldCombox()
        {
            mainField.Items.Clear();
            secondaryField.Items.Clear();

            mainField.Items.Add("");
            secondaryField.Items.Add("");
            foreach (PmsField pf in _pmsFieldList)
            {
                mainField.Items.Add(pf.fieldName);
                secondaryField.Items.Add(pf.fieldName);
                if (pf.fieldType == "DATETIME")
                {
                    mainField.Items.Add(pf.fieldName+".Year");
                    mainField.Items.Add(pf.fieldName + ".Month");
                    mainField.Items.Add(pf.fieldName + ".Day");
                    mainField.Items.Add(pf.fieldName + ".Hour");
                    mainField.Items.Add(pf.fieldName + ".Minute");
                    mainField.Items.Add(pf.fieldName + ".Second");

                    secondaryField.Items.Add(pf.fieldName + ".Year");
                    secondaryField.Items.Add(pf.fieldName + ".Month");
                    secondaryField.Items.Add(pf.fieldName + ".Day");
                    secondaryField.Items.Add(pf.fieldName + ".Hour");
                    secondaryField.Items.Add(pf.fieldName + ".Minute");
                    secondaryField.Items.Add(pf.fieldName + ".Second");
                }
            }
        }

        private void FormSql_Load(object sender, EventArgs e)
        {
            if (mainField.Text.Length > 0)
            {
                secondaryField.Enabled = true;                
            }
            else
            {
                secondaryField.Enabled = false;
            }

            if (_pmsJoinRelation.Count == 0)
                buttonRelation.Enabled = false;

            if(propertyTree1.PaneNodes.Count>0)
                propertyTree1.SelectedPaneNode = propertyTree1.PaneNodes[0];
            if (_IsReport)
            {
                propertyTree1.PaneNodes.RemoveAt(0);
                propertyTree1.PaneNodes.RemoveAt(0);
            }

            ResetAppearance();
            //RetSeries();

            treeView1.ExpandAll();
            try
            {
                treeView1.SelectedNode = treeView1.Nodes["chartArea"].Nodes[0];
            }
            catch
            {
            }
            if (ChartParent != null && ChartParent.IsReport == true)
            {
                comboBoxX.Text = ChartParent.XRecordField;
            }
        }

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            FormWhere fw = new FormWhere();
            fw.WhereData = this.whereData;
            
            List<PmsField> pmsNewField = new List<PmsField>();

            if (mainField.Text.Length > 0)
            {
                foreach (var field in this._pmsFieldList)
                {
                    if (field.fieldName == mainField.Text)
                    {
                        pmsNewField.Add(field);
                        break;
                    }
                }
                if (secondaryField.Text.Length > 0)
                {
                    foreach (var field in this._pmsFieldList)
                    {
                        if (field.fieldName == secondaryField.Text)
                        {
                            pmsNewField.Add(field);
                            break;
                        }
                    }
                }
            }

            for (int i = 0; i < this.listBoxFormula.Items.Count; i++)
            {
                string field = (string)this.listBoxFormula.Items[i];

                //如果有别名,应该用别名还是原始统计名?2010_04_21
                int iPos = field.IndexOf(" as ", 0, StringComparison.CurrentCultureIgnoreCase);
                if (iPos > 0)
                {
                    field = field.Substring(0, iPos);
                }

                if (field.IndexOf("sum(",StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                    field.IndexOf("avg(", StringComparison.CurrentCultureIgnoreCase) >= 0)//浮点型
                {
                    PmsField pf = new PmsField();
                    pf.fieldName = field;
                    pf.fieldType = "FLOAT";
                    pf.fieldDescription = "formula_no_ENUM";
                    pmsNewField.Add(pf);
                }
                else if (field.IndexOf("count(", StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    PmsField pf = new PmsField();
                    pf.fieldName = field;
                    pf.fieldType = "INT";
                    pf.fieldDescription = "formula_no_ENUM";
                    pmsNewField.Add(pf);
                }
                else if (field.IndexOf("min(", StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                    field.IndexOf("max(", StringComparison.CurrentCultureIgnoreCase) >= 0)//浮点型
                {
                    PmsField pf = new PmsField();
                    pf.fieldName = field;
                    pf.fieldDescription = "formula_no_ENUM";
                    foreach (var field1 in this._pmsFieldList)
                    {
                        if (field.IndexOf(field1.fieldName, StringComparison.CurrentCultureIgnoreCase) >= 0)
                        {
                            pf.fieldType = field1.fieldType;
                            break;
                        }
                    }

                    pmsNewField.Add(pf);
                }
            }

            fw.PmsFieldList = this._pmsFieldList;
            fw.PmsGroupFieldList = pmsNewField;
            fw.QueryMode = 1;
            if (fw.ShowDialog(this) == DialogResult.OK)
            {
                this.whereData = fw.WhereData;
            }
        }

        private string getConfigSql()
        {
            string groupby = "";
            #region group By
            if (mainField.Text.Length > 0)
            {
                groupby = PublicFunctionClass.GetTimePartSqlField(1, mainField.Text);
                if (secondaryField.Text.Length > 0)
                    groupby += " ," + PublicFunctionClass.GetTimePartSqlField(1, secondaryField.Text);
            }
            #endregion

            string orderby = "";
            #region oder By
            if (sortList!=null&&sortList.Count > 0)
            {
                foreach (var sc in sortList)
                {
                    orderby += sc.ToString() + ",";
                }

                if (orderby.Length > 0)
                    orderby = orderby.Substring(0, orderby.Length - 1);
            }
            #endregion

            string field = "";
            #region Field
            if (mainField.Text.Length > 0)
            {
                field += mainField.Text.ToLower() + ",";
                if (field.IndexOf(mainField.Text.ToLower()) < 0)//group by 字段一定要在select后面
                {
                    field += PublicFunctionClass.GetTimePartSqlField(1, mainField.Text) + ",";

                }
                if (secondaryField.Text.Length > 0)
                {
                    field += PublicFunctionClass.GetTimePartSqlField(1, secondaryField.Text) + ","; ;
                }
            }
            for (int i=0;i<listBoxFormula.Items.Count;i++)
            {
                string itemText = (string)listBoxFormula.Items[i];
                itemText = itemText.ToLower();
                itemText = itemText.Trim();
                #region oldField
                /*/
                if ((itemText.IndexOf(" as ") < 0) && ((itemText.IndexOf("count(") >= 0) ||
                                                               (itemText.IndexOf("sum(") >= 0) ||
                                                               (itemText.IndexOf("avg(") >= 0) ||
                                                               (itemText.IndexOf("min(") >= 0) ||
                                                               (itemText.IndexOf("max(") >= 0)))
                {
                    field += itemText + " as label" + i.ToString() + ",";
                }
                else
                {
                    field += itemText + ",";
                }/*/ 
                #endregion

                if(mainField.Text.Length > 0)
                {
                    if (mainField.Text == itemText||secondaryField.Text == itemText)
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
            int nTable = this.listBoxDataSource.Items.Count;
            if(nTable<1)
                return "";

            string mainTable = (string)(this.listBoxDataSource.Items[0]);

            tables = "[" + mainTable + "]";
            if (nTable > 1)
            {
                if (_pmsJoinRelation.Count == 0)
                {
                    for (int iTable = 1; iTable < nTable; iTable++)
                    {
                        string itemText = (string)listBoxDataSource.Items[iTable];
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
                        foreach (TableJoinRelation tjr in _pmsJoinRelation)
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

                    if (tableSelect.Count < listBoxDataSource.Items.Count)
                    {

                        for (int it = 1; it < nTable; it++)
                        {
                            string tableNoSelect = (string)(listBoxDataSource.Items[it]);
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
            #region Where

            //where = TreeViewDataAccess.GetSQLWhere(this.whereData);
            #endregion

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

        private void buttonRelation_Click(object sender, EventArgs e)
        {
            FormTableRelation ftr = new FormTableRelation();
            ftr.PmsJoinRelation = this._pmsJoinRelation;
            ftr.StartPosition = FormStartPosition.CenterParent;
            if (ftr.ShowDialog(this) == DialogResult.OK)
            {
                this._pmsJoinRelation = ftr.PmsJoinRelation;
            }
        }

        private void mainField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mainField.Text.Length > 0)
            {
                secondaryField.Enabled = true;
            }
            else
            {
                secondaryField.Enabled = false;
            }
        }

        private void AxisConfig()
        {
            if (ChartParent != null && ChartParent.IsReport == true)
            {
                checkedListBox1.Items.Clear();
                foreach (string pf in ChartParent.AllRecordFields)
                {
                    checkedListBox1.Items.Add(pf);
                }
                if (ChartParent.SelectRecordFields.Count == 0)
                {
                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                        checkedListBox1.SetItemChecked(i, true);
                }
                else
                {
                    _yAixs = ChartParent.SelectRecordFields;
                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        bool bFind = false;
                        foreach (string fieldChoose in _yAixs)
                        {
                            if ((string)(checkedListBox1.Items[i]) == fieldChoose)
                            {
                                checkedListBox1.SetItemChecked(i, true);
                                bFind = true;
                                break;
                            }
                        }

                        if (bFind == false)
                            checkedListBox1.SetItemChecked(i, false);
                    }
                }
                _yAixs.Clear();
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                        _yAixs.Add((string)checkedListBox1.Items[i]);
                }
                return;
            }
            selectFieldList = this.FormulaList;
            if (comboBoxX.Items.Count == 0)
            {
                if (groupByList.Count == 0)
                {
                    foreach (string pf in selectFieldList)
                    {
                        comboBoxX.Items.Add(pf);
                    }
                    if (comboBoxX.Items.Count >0)
                        comboBoxX.SelectedIndex = 0;
                }
                else
                {
                    comboBoxX.Text = groupByList[0];
                    comboBoxX.Items.Add(groupByList[0]);
                    comboBoxX.SelectedIndex = 0;
                }
            }
            else
            {
                string oldSelect = comboBoxX.Text;
                comboBoxX.Items.Clear();
                if (groupByList.Count == 0)
                {                                        
                    foreach (string pf in selectFieldList)
                    {
                        comboBoxX.Items.Add(pf);
                    }
                    int iF = comboBoxX.FindString(oldSelect);
                    if (iF < 0)
                        iF = 0;
                    if (comboBoxX.Items.Count > 0)
                        comboBoxX.SelectedIndex = iF;
                }
                else
                {
                    comboBoxX.Text = groupByList[0];
                    comboBoxX.Items.Add(groupByList[0]);
                    comboBoxX.SelectedIndex = 0;
                }
            }
            checkedListBox1.Items.Clear();
            if (groupByList.Count > 0)
            {
                foreach (string pf in selectFieldList)
                {
                    bool bFind22 = false;
                    foreach (string group in groupByList)
                    {
                        if (pf == group)
                        {
                            bFind22 = true;
                            break;
                        }
                    }
                    if (bFind22)
                        continue;
                    checkedListBox1.Items.Add(pf);
                }
            }
            else
            {
                foreach (string pf in selectFieldList)
                {
                    if (pf == comboBoxX.Text)
                    {
                        continue;
                    }
                        
                    checkedListBox1.Items.Add(pf);
                }
            }
            if (_yAixs.Count == 0)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    checkedListBox1.SetItemChecked(i, true);
            }
            else
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    bool bFind = false;
                    foreach (string fieldChoose in _yAixs)
                    {
                        if ((string)(checkedListBox1.Items[i]) == fieldChoose)
                        {
                            checkedListBox1.SetItemChecked(i, true);
                            bFind = true;
                            break;
                        }
                    }

                    if (bFind == false)
                        checkedListBox1.SetItemChecked(i, false);
                }
            }
            _yAixs.Clear();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                    _yAixs.Add((string)checkedListBox1.Items[i]);
            }
            
        }        

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            sqlText.Text = textBoxConfigSql.Text;
        }

        private void buttonTestConfig_Click(object sender, EventArgs e)
        {
            textBoxConfigSql.Text = PublicFunctionClass.GetConfigSql(this.SqlSource,true);

            if (dataGridViewTest.DataSource != null && sqlText.Text == textBoxConfigSql.Text)
                return;

            buttonSqlTest.Enabled = true;
            TestSqlValid(textBoxConfigSql.Text);
        }

        private void buttonSqlTest_Click(object sender, EventArgs e)
        {
            if (dataGridViewTest.DataSource != null && sqlText.Text == textBoxConfigSql.Text)
                return;
            buttonSqlTest.Enabled = false;
            TestSqlValid(sqlText.Text);
        }
        
        private void TestSqlValid(string sql)
        {
            //SqlConnection _SqlConnection1 = SqlStructure.GetSqlConncetion();

            //if (_SqlConnection1 == null||_SqlConnection1.State != ConnectionState.Open)
            //{
            //    MessageBox.Show("连接错误");
            //    return;
            //}
            //SqlCommand thisCommand = _SqlConnection1.CreateCommand();
            //thisCommand.CommandText = sql;

            ////PMS.Libraries.ToolControls.PMSPublicInfo.Message.Info(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, thisCommand.CommandText, false);
            //SqlDataReader thisReader = null;
            //try
            //{
            //    thisReader = thisCommand.ExecuteReader();
            //}
            //catch (Exception ec)
            //{
            //    MessageBox.Show(ec.Message);
            //    thisCommand.Dispose();
            //    _SqlConnection1.Close();
            //    return;
            //}
            //System.Data.DataTable dt = new System.Data.DataTable();
            //dt.Load(thisReader); 
            System.Data.DataTable dt = PMSDBConnection.ExecuteCommand(sql);

            if (dataGridViewTest.DataSource != null)
                ((System.Data.DataTable)dataGridViewTest.DataSource).Rows.Clear();
            dataGridViewTest.Columns.Clear();
            
            dataGridViewTest.DataSource = dt;

            //thisCommand.Dispose();
        }

        private void sqlText_TextChanged(object sender, EventArgs e)
        {
            buttonSqlTest.Enabled = true;
        }

        private void checkBoxUsing_CheckedChanged(object sender, EventArgs e)
        {
            //sqlChanged = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<PmsField> pmsNewField = new List<PmsField>();

            if (mainField.Text.Length == 0)
                pmsNewField.AddRange(this._pmsFieldList);
            else
            {
                PmsField pf = new PmsField();
                pf.fieldName = mainField.Text;
                pmsNewField.Add(pf);
                if (secondaryField.Text.Length > 0)
                {
                    pf = new PmsField();
                    pf.fieldName = secondaryField.Text;
                    pmsNewField.Add(pf);
                }
            }

            for (int i = 0; i < this.listBoxFormula.Items.Count; i++)
            {
                string field = (string)this.listBoxFormula.Items[i];
                if (field.IndexOf("sum(", StringComparison.CurrentCultureIgnoreCase) >= 0 || field.IndexOf("avg(", StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                    field.IndexOf("min(", StringComparison.CurrentCultureIgnoreCase) >= 0 || field.IndexOf("max(", StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                    field.IndexOf("count(", StringComparison.CurrentCultureIgnoreCase) >= 0)//浮点型
                {
                    PmsField pf = new PmsField();

                    int asPos = field.IndexOf(" as ", StringComparison.CurrentCultureIgnoreCase);
                    if (asPos >= 0)
                        pf.fieldName = field.Substring(0, asPos);
                    else
                        pf.fieldName = field;
                    pmsNewField.Add(pf);
                }
            }
            /*/
            if (PublicFunctionClass.IsDateTimePart(mainField.Text))
            {
                PmsField pf = new PmsField();
                pf.fieldName = mainField.Text;
                pmsNewField.Add(pf);
            }
            if (PublicFunctionClass.IsDateTimePart(secondaryField.Text))
            {
                PmsField pf = new PmsField();
                pf.fieldName = secondaryField.Text;
                pmsNewField.Add(pf);
            }/*/
            FormSort fs = new FormSort();
            fs.FieldList = pmsNewField;
            fs.StartPosition = FormStartPosition.CenterParent;
            fs.SortList = this.sortList;
            if (fs.ShowDialog(this) == DialogResult.OK)
            {
                this.sortList = fs.SortList;
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// 直接使用表达式作为字段
        /// </summary>
        private void ResetAppearance()
        {
            /*/
            string sqlNew = "";
            if (checkBoxUsing.Checked)
                sqlNew = sqlText.Text;//
            else
            {
                sqlNew = PublicFunctionClass.GetConfigSql(new DataSource(this,true),false);              
            }

            if (sqlNew != oldSqlForData)
            {
                PublicFunctionClass.getSelectedField(sqlNew, selectFieldList);
                groupByList = PublicFunctionClass.GetGroupList(this.MainGroupBy,this.SecondaryGroupBy);
                oldSqlForData = sqlNew;
                AxisConfig();
            }
            /*/
            groupByList = PublicFunctionClass.GetGroupList(this.MainGroupBy,this.SecondaryGroupBy);            
            AxisConfig();   
        }
        private void propertyTree1_PaneActivated(WRM.Windows.Forms.PropertyTree sender, WRM.Windows.Forms.PaneSelectionEventArgs psea)
        {
            if (psea.CurPaneNode == this.ppPane3.PaneNode)//前次切换的是3
            {
                ppPane3_Leave();
            }
            if (this.ppPane3.PaneNode.Selected)
            {
                ResetAppearance();
            }
            else if (this.ppPane2.PaneNode.Selected)
            {
                
            }
            else if (this.ppPane4.PaneNode.Selected)
            {
                #region 初始化数据树
                
                TreeNode seriesValueParent = treeViewSeries.Nodes["seriesData"];
                seriesValueParent.Nodes.Clear();
                if (RetSeries())
                {                                        
                    foreach (string seriesName in seriesData)
                    {
                        TreeNode child = new TreeNode(seriesName);
                        child.ImageIndex = 4;
                        child.SelectedImageIndex = 4;
                        seriesValueParent.Nodes.Add(child);
                    }
                }
                #endregion

                #region 初始化位置树

                TreeNode titleParent = treeViewPosition.Nodes["title"];
                titleParent.Nodes.Clear();

                foreach (var pmsTitle in TitleList)
                {
                    TreeNode child = new TreeNode(pmsTitle.Name);
                    child.ImageIndex = 0;
                    child.SelectedImageIndex = 0;
                    titleParent.Nodes.Add(child);
                }
                TreeNode legendParent = treeViewPosition.Nodes["legend"];
                legendParent.Nodes.Clear();

                foreach (var pmsLegend in LegendList)
                {
                    TreeNode child = new TreeNode(pmsLegend.Name);
                    child.ImageIndex = 2;
                    child.SelectedImageIndex = 2;
                    legendParent.Nodes.Add(child);
                }
                #endregion

                #region 初始化treeViewAppearance
               
                seriesDataForAppearance.Clear();
                seriesDataForChartArea.Clear();
                seriesDataForLegend.Clear();

                TreeNode chartAreaParent = treeViewAppearance.Nodes["chartArea"];
                chartAreaParent.Nodes.Clear();
                foreach (PMSChartArea pca in chartAreaList)
                {
                    TreeNode child = new TreeNode(pca.Name);
                    child.Tag = pca;
                    child.ImageIndex = 1;
                    child.SelectedImageIndex = 1;
                    chartAreaParent.Nodes.Add(child);
                    if (pca.SeriesDataList != null)
                    {
                        List<string> seriesDataList = (List<string>)pca.SeriesDataList;

                        foreach (string series in seriesDataList)
                        {
                            bool bFind = false;
                            foreach (string seriesName in seriesData)
                            {
                                if (seriesName == series)//真实存在的series
                                {
                                    bFind = true;
                                    break;
                                }
                            }
                            if (bFind)
                            {
                                TreeNode childSeries = new TreeNode(series);
                                childSeries.ImageIndex = 4;
                                childSeries.SelectedImageIndex = 4;
                                child.Nodes.Add(childSeries);
                                seriesDataForChartArea.Add(series);
                            }
                        }
                    }
                }

                TreeNode seriesParent = treeViewAppearance.Nodes["series"];
                seriesParent.Nodes.Clear();

                foreach (PMSSeries pca in seriesList)
                {
                    TreeNode child = new TreeNode(pca.Name);
                    child.Tag = pca;
                    child.ImageIndex = 3;
                    child.SelectedImageIndex = 3;
                    seriesParent.Nodes.Add(child);
                    if (pca.SeriesDataList != null)
                    {
                        List<string> seriesDataList = (List<string>)pca.SeriesDataList;

                        foreach (string series in seriesDataList)
                        {
                            bool bFind = false;
                            foreach (string seriesName in seriesData)
                            {
                                if (seriesName == series)//真实存在的series
                                {
                                    bFind = true;
                                    break;
                                }
                            }
                            if (bFind)
                            {
                                TreeNode childSeries = new TreeNode(series);
                                childSeries.ImageIndex = 4;
                                childSeries.SelectedImageIndex = 4;
                                child.Nodes.Add(childSeries);
                                seriesDataForAppearance.Add(series);
                            }
                        }
                    }
                }

                TreeNode legendParent2 = treeViewAppearance.Nodes["legend"];
                legendParent2.Nodes.Clear();

                foreach (PMSLegend pca in legendList)
                {
                    TreeNode child = new TreeNode(pca.Name);
                    child.Tag = pca;
                    child.ImageIndex = 2;
                    child.SelectedImageIndex = 2;
                    legendParent2.Nodes.Add(child);
                    if (pca.SeriesDataList != null)
                    {
                        List<string> seriesDataList = (List<string>)pca.SeriesDataList;

                        foreach (string series in seriesDataList)
                        {
                            bool bFind = false;
                            foreach (string seriesName in seriesData)
                            {
                                if (seriesName == series)//真实存在的series
                                {
                                    bFind = true;
                                    break;
                                }
                            }
                            if (bFind)
                            {
                                TreeNode childSeries = new TreeNode(series);
                                childSeries.ImageIndex = 4;
                                childSeries.SelectedImageIndex = 4;
                                child.Nodes.Add(childSeries);
                                seriesDataForLegend.Add(series);
                            }
                        }
                    }
                }
                #endregion

                #region 初始化treeViewChartArea

                titleDataForChartArea.Clear();
                legendDataForChartArea.Clear();

                TreeNode chartAreaParent2 = treeViewChartArea.Nodes["chartArea"];
                chartAreaParent2.Nodes.Clear();
                foreach (PMSChartArea pca in chartAreaList)
                {
                    TreeNode child = new TreeNode(pca.Name);
                    child.Tag = pca;
                    child.ImageIndex = 1;
                    child.SelectedImageIndex = 1;
                    chartAreaParent2.Nodes.Add(child);
                    if (pca.LegendDataList != null)
                    {
                        List<string> legendDataList = (List<string>)pca.LegendDataList;

                        foreach (string legend in legendDataList)
                        {
                            bool bFind = false;
                            foreach (var pmsLegend in legendList)
                            {
                                if (legend == pmsLegend.Name)//真实存在的series
                                {
                                    bFind = true;
                                    break;
                                }
                            }
                            if (bFind)
                            {
                                TreeNode childSeries = new TreeNode(legend);
                                childSeries.ImageIndex = 2;
                                childSeries.SelectedImageIndex = 2;
                                child.Nodes.Add(childSeries);
                                legendDataForChartArea.Add(legend);
                            }
                        }
                    }
                    if (pca.TitleDataList != null)
                    {
                        List<string> titleDataList = (List<string>)pca.TitleDataList;

                        foreach (string title in titleDataList)
                        {
                            bool bFind = false;
                            foreach (var pmsTitle in titleList)
                            {
                                if (title == pmsTitle.Name)//真实存在的series
                                {
                                    bFind = true;
                                    break;
                                }
                            }
                            if (bFind)
                            {
                                TreeNode childSeries = new TreeNode(title);
                                childSeries.ImageIndex = 0;
                                childSeries.SelectedImageIndex = 0;
                                child.Nodes.Add(childSeries);
                                titleDataForChartArea.Add(title);
                            }
                        }
                    }
                }
                #endregion

                treeViewAppearance.ExpandAll();
                treeViewSeries.ExpandAll();
                treeViewChartArea.ExpandAll();
                treeViewPosition.ExpandAll();
            }
            else if (this.dataSource.PaneNode.Selected)
            {
            }
        }
        TreeNode selectNode = null;
        private void AddAtom_Click(object sender, EventArgs e)
        {
            if (selectNode != null)
            {
                TreeNode parent = selectNode.Parent;

                if (parent == null)
                {
                    if (selectNode.Name == "chartArea")//||(&&parent.Name == "chartArea")
                    {
                        AddChartArea(selectNode);
                    }
                    else if (selectNode.Name == "legend")
                    {
                        AddLegend(selectNode);
                        
                    }
                    else if (selectNode.Name == "title")
                    {
                        AddTitle(selectNode);
                    }
                    else if (selectNode.Name == "series")
                    {
                        AddSeries(selectNode);
                    }
                }
                else
                {
                    if (parent.Name == "chartArea")
                    {
                        AddChartArea(parent);
                    }
                    else if (parent.Name == "legend")
                    {
                        AddLegend(parent);
                    }
                    else if (parent.Name == "title")
                    {
                    }
                    else if (parent.Name == "series")
                    {
                        AddSeries(parent);
                    }
                }
            }
        }
        private void AddChartArea(TreeNode parent)
        {
            PMSChartArea pca = new PMSChartArea(null);
            TreeNode tn = new TreeNode();

            pca.Parent = parent;
            tn.Text = pca.Name = GetNewName(parent,"chartArea");
            tn.Tag = pca;
            tn.ImageIndex = parent.ImageIndex;
            tn.SelectedImageIndex = parent.SelectedImageIndex;
            parent.Nodes.Add(tn);
            treeView1.SelectedNode = tn;
        }
        private void AddTitle(TreeNode parent)
        {
            PMSTitle pca = new PMSTitle(null);
            TreeNode tn = new TreeNode();

            pca.Parent = parent;
            tn.Text = pca.Name = GetNewName(parent,"title");
            tn.Tag = pca;
            tn.ImageIndex = parent.ImageIndex;
            tn.SelectedImageIndex = parent.SelectedImageIndex;
            parent.Nodes.Add(tn);
            treeView1.SelectedNode = tn;
        }
        private void AddLegend(TreeNode parent)
        {
            PMSLegend pca = new PMSLegend(null);
            TreeNode tn = new TreeNode();

            pca.Parent = parent;
            tn.Text = pca.Name = GetNewName(parent, "legend");
            tn.Tag = pca;
            tn.ImageIndex = parent.ImageIndex;
            tn.SelectedImageIndex = parent.SelectedImageIndex;
            parent.Nodes.Add(tn);
            treeView1.SelectedNode = tn;
        }
        private void AddSeries(TreeNode parent)
        {
            PMSSeries pse = new PMSSeries(null);
            TreeNode tn = new TreeNode();

            pse.Parent = parent;
            tn.Text = pse.Name = GetNewName(parent, "series");
            tn.Tag = pse;
            tn.ImageIndex = parent.ImageIndex;
            tn.SelectedImageIndex = parent.SelectedImageIndex;
            parent.Nodes.Add(tn);
            treeView1.SelectedNode = tn;
        }
        private string GetNewName(TreeNode parent,string primary)
        {
            for (int i = 0; i < parent.Nodes.Count; i++)
            {
                string newstring = primary + (i + 1).ToString();
                bool bExist = false;
                foreach (TreeNode child in parent.Nodes)
                {
                    if (child.Text == newstring)
                    {
                        bExist = true;
                        break;
                    }
                }
                if (!bExist)
                    return newstring;
            }
            return primary + (parent.Nodes.Count+1).ToString();
        }
        private void DeleteAtom_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null || treeView1.SelectedNode.Parent == null)
                return;

            if (treeView1.SelectedNode.ImageIndex == 1 && treeView1.SelectedNode.Parent.Nodes.Count == 1)
            {
                //剩下最后一个chartarea，不允许删除
                return;
            }
            treeView1.SelectedNode.Remove();

            if (treeView1.SelectedNode.ImageIndex == 1)//area
            {
                
            }
            else if (treeView1.SelectedNode.ImageIndex == 3)//series
            {
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeView1.SelectedNode = e.Node;
                if (e.Node != null)
                {
                    selectNode = e.Node;
                    contextMenuStrip1.Visible = true;
                }
                else
                {
                    selectNode = null;
                    contextMenuStrip1.Visible = false;
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                propertyGrid2.SelectedObject = e.Node.Tag;
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            propertyGrid2.SelectedObject = e.Node.Tag;
        }
        DataTable dt = new DataTable();
        DataTable dtMain = new DataTable();
        DataTable dtSecond = new DataTable();
        List<string> selectFieldList1 = new List<string>();
        //List<string> groupByList1 = new List<string>();
        private bool RetSeries()
        {
            seriesData.Clear();

            if (ChartParent != null && ChartParent.IsReport == true)
            {
                int nSeries1 = _yAixs.Count;
                if (nSeries1 <= 0)
                {
                    foreach (string pf in ChartParent.SelectRecordFields)
                    {
                        _yAixs.Add(pf);
                    }
                }
                nSeries1 = _yAixs.Count;
                if (nSeries1 <= 0)
                {
                    MessageBox.Show("没有选择字段！");
                    return false;
                }

                for (int i = 0; i < nSeries1; i++)
                {
                    seriesData.Add(_yAixs[i]);
                }
                return true;
            }
            selectFieldList1 = this.FormulaList;
            groupByList = PublicFunctionClass.GetGroupList(this.MainGroupBy, this.SecondaryGroupBy);            

            if (selectFieldList1.Count == 0)//无选择字段
            {                
                return false;
            }

            int nSeries = _yAixs.Count;
            if (nSeries <= 0)
            {
                foreach (string pf in FormulaList)
                {
                    bool bFind22 = false;
                    foreach (string group in groupByList)
                    {
                        if (pf == group)
                        {
                            bFind22 = true;
                            break;
                        }
                    }
                    if (bFind22)
                        continue;
                    _yAixs.Add(pf);
                }
            }
            nSeries = _yAixs.Count;
            if (nSeries <= 0)
            {
                MessageBox.Show("没有选择字段！");
                return false;
            }

            if (groupByList.Count < 2)
            {
                for (int i = 0; i < nSeries; i++)
                {
                    seriesData.Add(_yAixs[i]);
                }
                return true;
            }
            else if (groupByList.Count == 2)
            {
                string sqlNew = "";
                DataSource ds = new DataSource(this, true);
                if (checkBoxUsing.Checked)
                    sqlNew = sqlText.Text;//
                else
                {
                    sqlNew = PublicFunctionClass.GetConfigSql(ds, false);
                }
                if (sqlNew != this.oldSqlForAppearance)
                {
                    bool b = ResetDataSource(sqlNew, groupByList);
                }

                oldSqlForAppearance = sqlNew;
                
                #region series
                try
                {
                    for (int i = 0; i < nSeries; i++)//Y轴个数
                    {
                        string yName = _yAixs[i];
                        foreach (DataRow secondRow in dtSecond.Rows)//第二分组个数
                        {
                            seriesData.Add(yName + "_" + secondRow[0].ToString());
                        }
                    }
                }
                catch(Exception ec)
                {
                    PMS.Libraries.ToolControls.PMSPublicInfo.Message.Info(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, ec.Message, false);
                    seriesData.Clear();
                    return false;
                }
                #endregion
                return true;
            }
            return false;
        }

        private bool ResetDataSource(string sql,List<string> groupByList2)
        {
            dt.Clear();
            dt.Columns.Clear();
            dtMain.Clear();
            dtMain.Columns.Clear();
            dtSecond.Clear();
            dtSecond.Columns.Clear();

            #region data
            //SqlConnection _SqlConnection1 = SqlStructure.GetSqlConncetion();
            //if (_SqlConnection1 == null || _SqlConnection1.State != ConnectionState.Open)
            //{
            //    MessageBox.Show("连接错误！");
            //    return false;
            //}
            //SqlCommand thisCommand = _SqlConnection1.CreateCommand();
            //thisCommand.CommandText = sql;

            //SqlDataReader thisReader = null;
            //try
            //{
            //    thisReader = thisCommand.ExecuteReader();
            //    thisCommand.Dispose();
            //}
            //catch (Exception ec)
            //{
            //    MessageBox.Show(ec.Message);
            //    thisCommand.Dispose();
            //    return false;
            //}
            //dt.Load(thisReader);
            //thisReader.Close();
            dt = PMSDBConnection.ExecuteCommand(sql);

            if (groupByList2.Count == 2)
            {
                #region datasource
                string secondGroupSql = this.ChartParent.getSimpleSql(groupByList2[1]);
                string mainGroupSql = this.ChartParent.getSimpleSql(groupByList2[0]);

                dtSecond = PMSDBConnection.ExecuteCommand(secondGroupSql);
                dtMain = PMSDBConnection.ExecuteCommand(mainGroupSql);

                //SqlCommand thisCommandSecond = _SqlConnection1.CreateCommand();
                //SqlCommand thisCommandMain = _SqlConnection1.CreateCommand();
                //thisCommandSecond.CommandText = secondGroupSql;
                //thisCommandMain.CommandText = mainGroupSql;

                ////PMS.Libraries.ToolControls.PMSPublicInfo.Message.Info(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, thisCommand.CommandText, false);
                //SqlDataReader SecondReader = null;
                //SqlDataReader MainReader = null;
                //try
                //{
                //    SecondReader = thisCommandSecond.ExecuteReader();
                //    thisCommandSecond.Dispose();
                //}
                //catch (Exception ec)
                //{
                //    MessageBox.Show(ec.Message);
                //    thisCommandSecond.Dispose();
                //    return false;
                //}
                //dtSecond.Load(SecondReader);
                //SecondReader.Close();
                //try
                //{
                //    MainReader = thisCommandMain.ExecuteReader();
                //    thisCommandMain.Dispose();
                //}
                //catch (Exception ec)
                //{
                //    MessageBox.Show(ec.Message);
                //    thisCommandMain.Dispose();
                //    return false;
                //}
                //dtMain.Load(MainReader);
                //MainReader.Close();
                #endregion
                
            }
            return true;
            #endregion
        }        

        #region 消息响应
        private void treeViewSeries_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void treeViewSeries_ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeNode tn = (TreeNode)e.Item;
            if (tn.Parent == null)
                return;
            DoDragDrop(e.Item, DragDropEffects.Copy);
        }

        private void treeViewAppearance_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode)))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="NewNode">source</param>
        /// <param name="DestinationNode"></param>
        private void DragDropOperation(TreeNode NewNode, TreeNode DestinationNode)
        {
            TreeNode parentNode = DestinationNode.Parent;

            #region different
            if (parentNode == null)//拖在根节点上，无效
                return;

            if (parentNode.Parent == null)//拖在二级节点上，判断该节点下是否已经存在该series
            {

                TreeNode newNode = new TreeNode(NewNode.Text, NewNode.ImageIndex, NewNode.SelectedImageIndex);
                if (NewNode.ImageIndex == 4)
                {
                    #region seriesData
                    if (DestinationNode.Tag is PMSChartArea)
                    {
                        foreach (string existSeries in this.seriesDataForChartArea)
                        {
                            if (NewNode.Text == existSeries)
                            {
                                return;
                            }
                        }
                        seriesDataForChartArea.Add(NewNode.Text);
                        PMSChartArea pca = (PMSChartArea)(DestinationNode.Tag);
                        List<string> seriesList = (List<string>)pca.SeriesDataList;
                        if (seriesList == null)
                            seriesList = new List<string>();


                        seriesList.Add(NewNode.Text);
                        pca.SeriesDataList = seriesList;
                        DestinationNode.Tag = pca;
                    }
                    else if (DestinationNode.Tag is PMSSeries)
                    {
                        foreach (string existSeries in this.seriesDataForAppearance)
                        {
                            if (NewNode.Text == existSeries)
                            {
                                return;
                            }
                        }
                        seriesDataForChartArea.Add(NewNode.Text);

                        PMSSeries pca = (PMSSeries)(DestinationNode.Tag);
                        List<string> seriesList = (List<string>)pca.SeriesDataList;

                        if (seriesList == null)
                            seriesList = new List<string>();
                        seriesList.Add(NewNode.Text);
                        pca.SeriesDataList = seriesList;
                        DestinationNode.Tag = pca;
                    }
                    else if (DestinationNode.Tag is PMSLegend)
                    {
                        foreach (string existSeries in this.seriesDataForLegend)
                        {
                            if (NewNode.Text == existSeries)
                            {
                                return;
                            }
                        }
                        seriesDataForLegend.Add(NewNode.Text);

                        PMSLegend pca = (PMSLegend)(DestinationNode.Tag);
                        List<string> seriesList = (List<string>)pca.SeriesDataList;

                        if (seriesList == null)
                            seriesList = new List<string>();
                        seriesList.Add(NewNode.Text);
                        pca.SeriesDataList = seriesList;
                        DestinationNode.Tag = pca;
                    }
                    else
                        return;
                    #endregion
                }
                else if (NewNode.ImageIndex == 0)
                {
                    if (DestinationNode.Tag is PMSChartArea)
                    {
                        foreach (string existSeries in this.titleDataForChartArea)
                        {
                            if (NewNode.Text == existSeries)
                            {
                                return;
                            }
                        }
                        titleDataForChartArea.Add(NewNode.Text);
                        PMSChartArea pca = (PMSChartArea)(DestinationNode.Tag);
                        List<string> seriesList = (List<string>)pca.TitleDataList;
                        if (seriesList == null)
                            seriesList = new List<string>();

                        seriesList.Add(NewNode.Text);
                        pca.TitleDataList = seriesList;
                        DestinationNode.Tag = pca;
                    }
                    else
                        return;
                }
                else if (NewNode.ImageIndex == 2)
                {
                    if (DestinationNode.Tag is PMSChartArea)
                    {
                        foreach (string existSeries in this.legendDataForChartArea)
                        {
                            if (NewNode.Text == existSeries)
                            {
                                return;
                            }
                        }
                        legendDataForChartArea.Add(NewNode.Text);
                        PMSChartArea pca = (PMSChartArea)(DestinationNode.Tag);
                        List<string> seriesList = (List<string>)pca.LegendDataList;
                        if (seriesList == null)
                            seriesList = new List<string>();

                        seriesList.Add(NewNode.Text);
                        pca.LegendDataList = seriesList;
                        DestinationNode.Tag = pca;
                    }
                    else
                        return;
                }


                DestinationNode.Nodes.Add(newNode);
                DestinationNode.Expand();

            }
            else if (parentNode.Parent.Parent == null)//拖在三级节点上，判断其父节点下是否已经存在该series
            {
                TreeNode newNode = new TreeNode(NewNode.Text, NewNode.ImageIndex, NewNode.SelectedImageIndex);
                if (NewNode.ImageIndex == 4)
                {
                    #region seriesData
                    if (parentNode.Tag is PMSChartArea)
                    {
                        foreach (string existSeries in this.seriesDataForChartArea)
                        {
                            if (NewNode.Text == existSeries)
                            {
                                return;
                            }
                        }
                        seriesDataForChartArea.Add(NewNode.Text);
                        PMSChartArea pca = (PMSChartArea)(parentNode.Tag);
                        List<string> seriesList = (List<string>)pca.SeriesDataList;
                        if (seriesList == null)
                            seriesList = new List<string>();
                        seriesList.Add(NewNode.Text);
                        pca.SeriesDataList = seriesList;
                        parentNode.Tag = pca;
                    }
                    else if (parentNode.Tag is PMSSeries)
                    {
                        foreach (string existSeries in this.seriesDataForAppearance)
                        {
                            if (NewNode.Text == existSeries)
                            {
                                return;
                            }
                        }
                        seriesDataForChartArea.Add(NewNode.Text);
                        PMSSeries pca = (PMSSeries)(parentNode.Tag);
                        List<string> seriesList = (List<string>)pca.SeriesDataList;

                        if (seriesList == null)
                            seriesList = new List<string>();
                        seriesList.Add(NewNode.Text);
                        pca.SeriesDataList = seriesList;
                        parentNode.Tag = pca;
                    }
                    else if (parentNode.Tag is PMSLegend)
                    {
                        foreach (string existSeries in this.seriesDataForLegend)
                        {
                            if (NewNode.Text == existSeries)
                            {
                                return;
                            }
                        }
                        seriesDataForLegend.Add(NewNode.Text);
                        PMSLegend pca = (PMSLegend)(parentNode.Tag);
                        List<string> seriesList = (List<string>)pca.SeriesDataList;

                        if (seriesList == null)
                            seriesList = new List<string>();
                        seriesList.Add(NewNode.Text);
                        pca.SeriesDataList = seriesList;
                        parentNode.Tag = pca;
                    }
                    else
                        return;
                    #endregion
                }
                else if (NewNode.ImageIndex == 0)
                {
                    if (parentNode.Tag is PMSChartArea)
                    {
                        foreach (string existSeries in this.titleDataForChartArea)
                        {
                            if (NewNode.Text == existSeries)
                            {
                                return;
                            }
                        }
                        titleDataForChartArea.Add(NewNode.Text);
                        PMSChartArea pca = (PMSChartArea)(parentNode.Tag);
                        List<string> seriesList = (List<string>)pca.TitleDataList;
                        if (seriesList == null)
                            seriesList = new List<string>();

                        seriesList.Add(NewNode.Text);
                        pca.TitleDataList = seriesList;
                        parentNode.Tag = pca;
                    }
                    else
                        return;
                }
                else if (NewNode.ImageIndex == 2)
                {
                    if (parentNode.Tag is PMSChartArea)
                    {
                        foreach (string existSeries in this.legendDataForChartArea)
                        {
                            if (NewNode.Text == existSeries)
                            {
                                return;
                            }
                        }
                        legendDataForChartArea.Add(NewNode.Text);
                        PMSChartArea pca = (PMSChartArea)(parentNode.Tag);
                        List<string> seriesList = (List<string>)pca.LegendDataList;
                        if (seriesList == null)
                            seriesList = new List<string>();

                        seriesList.Add(NewNode.Text);
                        pca.LegendDataList = seriesList;
                        parentNode.Tag = pca;
                    }
                    else
                        return;
                }

                parentNode.Nodes.Add(newNode);
                parentNode.Expand();
            }
            #endregion
        }
        private void treeViewAppearance_DragDrop(object sender, DragEventArgs e)
        {
            TreeView treeView2 = (TreeView)sender;//目的树


            TreeNode NewNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
            if (NewNode == null)
                return;

            Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
            TreeNode DestinationNode = treeView2.GetNodeAt(pt);

            if (treeView2 == treeViewAppearance)
            {
                #region treeViewAppearance

                if (NewNode.TreeView == treeViewSeries)
                {
                    DragDropOperation(NewNode, DestinationNode);
                }
                else
                    return;
                #endregion
            }
            else if (treeView2 == treeViewChartArea)
            {
                #region treeViewChartArea
                if (NewNode.TreeView == treeViewPosition)
                {
                    DragDropOperation(NewNode, DestinationNode);
                }
                else
                    return;
                #endregion
            }
        #region 放弃同树拖拽功能
            /*/ 
            else
            {
                if (NewNode.Parent == null || NewNode.Parent.Parent == null)//源非叶节点，返回
                {
                    return;
                }
                if (parentNode == null)//拖在根节点上，无效
                    return;

                TreeNode parentSource = NewNode.Parent;
                if (parentNode.Parent == null)//拖在二级节点上，判断该节点下是否已经存在该series
                {
                    #region 二级节点

                    if (NewNode.Parent == DestinationNode) //同源，返回
                    {
                        return;
                    }

                    if (DestinationNode.Tag is PMSChartArea)//目的父为区域
                    {
                        PMSChartArea pcaDestinaton = (PMSChartArea)(DestinationNode.Tag);
                        List<string> seriesListDestinaton = (List<string>)pcaDestinaton.SeriesDataList;
                        if (seriesListDestinaton == null)
                            seriesListDestinaton = new List<string>();
                        if (parentSource.Tag is PMSChartArea)//源父为区域
                        {
                            //判断目的父区域内是否已存在源节点
                            foreach (string targetSeries in seriesListDestinaton)
                            {
                                if (NewNode.Text == targetSeries)//存在，无任何操作
                                {
                                    return;
                                }
                            }
                            //移动源节点
                            TreeNode newNode = new TreeNode(NewNode.Text, NewNode.ImageIndex, NewNode.SelectedImageIndex);
                            DestinationNode.Nodes.Add(newNode);
                            seriesListDestinaton.Add(NewNode.Text);
                            pcaDestinaton.SeriesDataList = seriesListDestinaton;
                            DestinationNode.Tag = pcaDestinaton;

                            NewNode.Remove();
                            PMSChartArea pcaSource = (PMSChartArea)(parentSource.Tag);
                            List<string> seriesListSource = (List<string>)pcaSource.SeriesDataList;
                            if (seriesListSource != null)
                            {
                                seriesListSource.Remove(NewNode.Text);
                                pcaSource.SeriesDataList = seriesListSource;
                                parentSource.Tag = pcaSource;
                            }
                        }
                        else if (parentSource.Tag is PMSSeries)//源父为数据外观
                        {
                            //判断目的父区域内是否已存在源节点

                            foreach (string existSeries in this.seriesDataForChartArea)
                            {
                                if (NewNode.Text == existSeries)
                                {
                                    return;
                                }
                            }
                            seriesDataForChartArea.Add(NewNode.Text);
                            //复制源节点
                            TreeNode newNode = new TreeNode(NewNode.Text, NewNode.ImageIndex, NewNode.SelectedImageIndex);
                            DestinationNode.Nodes.Add(newNode);
                            seriesListDestinaton.Add(NewNode.Text);
                            pcaDestinaton.SeriesDataList = seriesListDestinaton;
                            DestinationNode.Tag = pcaDestinaton;
                        }
                    }
                    else if (DestinationNode.Tag is PMSSeries)
                    {
                        PMSSeries pcaDestinaton = (PMSSeries)(DestinationNode.Tag);
                        List<string> seriesListDestinaton = (List<string>)pcaDestinaton.SeriesDataList;
                        if (seriesListDestinaton == null)
                            seriesListDestinaton = new List<string>();
                        if (parentSource.Tag is PMSChartArea)//源父为区域
                        {
                            //判断目的父区域内是否已存在源节点

                            foreach (string existSeries in this.seriesDataForAppearance)
                            {
                                if (NewNode.Text == existSeries)
                                {
                                    return;
                                }
                            }
                            seriesDataForAppearance.Add(NewNode.Text);
                            //复制源节点
                            TreeNode newNode = new TreeNode(NewNode.Text, NewNode.ImageIndex, NewNode.SelectedImageIndex);

                            DestinationNode.Nodes.Add(newNode);
                            seriesListDestinaton.Add(NewNode.Text);
                            pcaDestinaton.SeriesDataList = seriesListDestinaton;
                            DestinationNode.Tag = pcaDestinaton;
                        }
                        else if (parentSource.Tag is PMSSeries)//源父为数据外观
                        {
                            //判断目的父区域内是否已存在源节点

                            foreach (string targetSeries in seriesListDestinaton)
                            {
                                if (NewNode.Text == targetSeries)//存在，无任何操作
                                {
                                    return;
                                }
                            }
                            //移动源节点
                            TreeNode newNode = new TreeNode(NewNode.Text, NewNode.ImageIndex, NewNode.SelectedImageIndex);
                            DestinationNode.Nodes.Add(newNode);
                            seriesListDestinaton.Add(NewNode.Text);
                            pcaDestinaton.SeriesDataList = seriesListDestinaton;
                            DestinationNode.Tag = pcaDestinaton;
                            NewNode.Remove();
                            //删除源节点的串中该节点信息
                            PMSSeries pcaSource = (PMSSeries)(parentSource.Tag);
                            List<string> seriesListSource = (List<string>)pcaSource.SeriesDataList;
                            if (seriesListSource != null)
                            {
                                seriesListSource.Remove(NewNode.Text);
                                pcaSource.SeriesDataList = seriesListSource;
                                parentSource.Tag = pcaSource;
                            }
                        }
                    }
                    #endregion
                }
                else if (parentNode.Parent.Parent == null)//拖在三级节点上，判断其父节点下是否已经存在该series
                {
                    #region 三级节点

                    if (NewNode.Parent == parentNode) //同源，返回
                    {
                        return;
                    }

                    if (parentNode.Tag is PMSChartArea)//目的父为区域
                    {
                        PMSChartArea pcaDestinaton = (PMSChartArea)(parentNode.Tag);
                        List<string> seriesListDestinaton = (List<string>)pcaDestinaton.SeriesDataList;
                        if (seriesListDestinaton == null)
                            seriesListDestinaton = new List<string>();
                        if (parentSource.Tag is PMSChartArea)//源父为区域
                        {
                            //判断目的父区域内是否已存在源节点
                            foreach (string targetSeries in seriesListDestinaton)
                            {
                                if (NewNode.Text == targetSeries)//存在，无任何操作
                                {
                                    return;
                                }
                            }
                            //移动源节点
                            TreeNode newNode = new TreeNode(NewNode.Text, NewNode.ImageIndex, NewNode.SelectedImageIndex);
                            parentNode.Nodes.Add(newNode);
                            seriesListDestinaton.Add(NewNode.Text);
                            pcaDestinaton.SeriesDataList = seriesListDestinaton;
                            parentNode.Tag = pcaDestinaton;

                            NewNode.Remove();
                            PMSChartArea pcaSource = (PMSChartArea)(parentSource.Tag);
                            List<string> seriesListSource = (List<string>)pcaSource.SeriesDataList;
                            if (seriesListSource != null)
                            {
                                seriesListSource.Remove(NewNode.Text);
                                pcaSource.SeriesDataList = seriesListSource;
                                parentSource.Tag = pcaSource;
                            }
                        }
                        else if (parentSource.Tag is PMSSeries)//源父为数据外观
                        {
                            //判断目的父区域内是否已存在源节点

                            foreach (string existSeries in this.seriesDataForChartArea)
                            {
                                if (NewNode.Text == existSeries)
                                {
                                    return;
                                }
                            }
                            seriesDataForAppearance.Add(NewNode.Text);
                            //复制源节点
                            TreeNode newNode = new TreeNode(NewNode.Text, NewNode.ImageIndex, NewNode.SelectedImageIndex);
                            parentNode.Nodes.Add(newNode);
                            seriesListDestinaton.Add(NewNode.Text);
                            pcaDestinaton.SeriesDataList = seriesListDestinaton;
                            parentNode.Tag = pcaDestinaton;
                        }
                    }
                    else if (parentNode.Tag is PMSSeries)
                    {
                        PMSSeries pcaDestinaton = (PMSSeries)(parentNode.Tag);
                        List<string> seriesListDestinaton = (List<string>)pcaDestinaton.SeriesDataList;
                        if (seriesListDestinaton == null)
                            seriesListDestinaton = new List<string>();
                        if (parentSource.Tag is PMSChartArea)//源父为区域
                        {
                            //判断目的父区域内是否已存在源节点

                            foreach (string existSeries in this.seriesDataForAppearance)
                            {
                                if (NewNode.Text == existSeries)
                                {
                                    return;
                                }
                            }
                            seriesDataForAppearance.Add(NewNode.Text);
                            //复制源节点
                            TreeNode newNode = new TreeNode(NewNode.Text, NewNode.ImageIndex, NewNode.SelectedImageIndex);

                            parentNode.Nodes.Add(newNode);
                            seriesListDestinaton.Add(NewNode.Text);
                            pcaDestinaton.SeriesDataList = seriesListDestinaton;
                            parentNode.Tag = pcaDestinaton;
                        }
                        else if (parentSource.Tag is PMSSeries)//源父为数据外观
                        {
                            //判断目的父区域内是否已存在源节点

                            foreach (string targetSeries in seriesListDestinaton)
                            {
                                if (NewNode.Text == targetSeries)//存在，无任何操作
                                {
                                    return;
                                }
                            }
                            //移动源节点
                            TreeNode newNode = new TreeNode(NewNode.Text, NewNode.ImageIndex, NewNode.SelectedImageIndex);
                            parentNode.Nodes.Add(newNode);
                            seriesListDestinaton.Add(NewNode.Text);
                            pcaDestinaton.SeriesDataList = seriesListDestinaton;
                            parentNode.Tag = pcaDestinaton;
                            NewNode.Remove();
                            //删除源节点的串中该节点信息
                            PMSSeries pcaSource = (PMSSeries)(parentSource.Tag);
                            List<string> seriesListSource = (List<string>)pcaSource.SeriesDataList;
                            if (seriesListSource != null)
                            {
                                seriesListSource.Remove(NewNode.Text);
                                pcaSource.SeriesDataList = seriesListSource;
                                parentSource.Tag = pcaSource;
                            }
                        }
                    }
                    #endregion
                }
            }/*/
            #endregion
        }

        private void treeViewAppearance_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Copy);
        }

        private void treeViewSeries_DragOver(object sender, DragEventArgs e)
        {
            
        }

        private void treeViewSeries_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode)))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None; 
        }

        private void ppPane3_Leave()
        {
            _yAixs.Clear();

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                    _yAixs.Add((string)(checkedListBox1.Items[i]));
            }

            if (chartAreaList == null)
                chartAreaList = new List<PMSChartArea>();
            chartAreaList.Clear();
            TreeNode chartAreaParent = treeView1.Nodes["chartArea"];

            foreach (TreeNode tn in chartAreaParent.Nodes)
            {
                chartAreaList.Add((PMSChartArea)(tn.Tag));
            }

            if (seriesList == null)
                seriesList = new List<PMSSeries>();
            seriesList.Clear();
            TreeNode seriesParent = treeView1.Nodes["series"];

            foreach (TreeNode tn in seriesParent.Nodes)
            {
                seriesList.Add((PMSSeries)(tn.Tag));
            }
            if (legendList == null)
                legendList = new List<PMSLegend>();
            legendList.Clear();
            TreeNode legendParent = treeView1.Nodes["legend"];

            foreach (TreeNode tn in legendParent.Nodes)
            {
                legendList.Add((PMSLegend)(tn.Tag));
            }
            
        }

        private void treeViewAppearance_KeyDown(object sender, KeyEventArgs e)
        {
            //deleteSeriesNodeData(treeViewAppearance.SelectedNode);
        }

        //treeViewAppearance,treeViewChartArea
        TreeNode selectNode1 = null;
        private void treeViewAppearance_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeView treeViewAppearance1 = (TreeView)sender;
            if (e.Button == MouseButtons.Right)
            {
                treeViewAppearance1.SelectedNode = selectNode1 = e.Node;
            }
            else
                selectNode1 = null;
        }
        private void deleteSeriesNodeData(TreeNode node)
        {
            if (node == null)
                return;
            try
            {
                if (node.TreeView == treeViewAppearance)
                {
                    if (node.ImageIndex == 4)
                    {
                        if (node.Parent.Tag is PMSSeries)
                        {
                            this.seriesDataForAppearance.Remove(node.Text);

                            PMSSeries PMSSeries1 = (PMSSeries)node.Parent.Tag;
                            ((List<string>)PMSSeries1.SeriesDataList).Remove(node.Text);
                        }
                        else if (node.Parent.Tag is PMSChartArea)
                        {
                            this.seriesDataForChartArea.Remove(node.Text);
                            PMSChartArea PMSSeries1 = (PMSChartArea)node.Parent.Tag;
                            ((List<string>)PMSSeries1.SeriesDataList).Remove(node.Text);
                        }
                        else if (node.Parent.Tag is PMSLegend)
                        {
                            this.seriesDataForLegend.Remove(node.Text);
                            PMSLegend PMSSeries1 = (PMSLegend)node.Parent.Tag;
                            ((List<string>)PMSSeries1.SeriesDataList).Remove(node.Text);
                        }
                        node.Remove();
                    }
                }
                else if (node.TreeView == treeViewChartArea)
                {
                    if (node.ImageIndex == 0)
                    {
                        this.titleDataForChartArea.Remove(node.Text);
                        PMSChartArea PMSSeries1 = (PMSChartArea)node.Parent.Tag;
                        ((List<string>)PMSSeries1.TitleDataList).Remove(node.Text);
                        node.Remove();
                    }
                    else if (node.ImageIndex == 2)
                    {
                        this.legendDataForChartArea.Remove(node.Text);
                        PMSChartArea PMSSeries1 = (PMSChartArea)node.Parent.Tag;
                        ((List<string>)PMSSeries1.LegendDataList).Remove(node.Text);
                        node.Remove();
                    }
                }
            }
            catch(Exception ee)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, ee.Message, false);
            }
            
        }
        private void deleteSeriesData_Click(object sender, EventArgs e)
        {
            //TreeView treeViewAppearance1 = (TreeView)sender;
            deleteSeriesNodeData(selectNode1);
        }
        #endregion

        private void propertyGrid2_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "Name")
            {
                this.treeView1.SelectedNode.Text = e.ChangedItem.Value.ToString();
            }
        }

        private void comboBoxX_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ChartParent.IsReport == true)
            {
                //comboBoxX.Items.Clear();
                //comboBoxX.Items.Add(y);
                return;
            }
            checkedListBox1.Items.Clear();
            if (groupByList.Count == 0)
            {
                foreach (string pf in selectFieldList)
                {
                    if (pf == comboBoxX.Text)
                    {
                        continue;
                    }

                    checkedListBox1.Items.Add(pf);
                }
            }
            else
            {
                //几乎不会执行
                foreach (string pf in selectFieldList)
                {
                    bool bFind22 = false;
                    foreach (string group in groupByList)
                    {
                        if (pf == group)
                        {
                            bFind22 = true;
                            break;
                        }
                    }
                    if (bFind22)
                        continue;
                    checkedListBox1.Items.Add(pf);
                }
            }
            if (_yAixs.Count == 0)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    checkedListBox1.SetItemChecked(i, true);
            }
            else
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    bool bFind = false;
                    foreach (string fieldChoose in _yAixs)
                    {
                        if ((string)(checkedListBox1.Items[i]) == fieldChoose)
                        {
                            checkedListBox1.SetItemChecked(i, true);
                            bFind = true;
                            break;
                        }
                    }

                    if (bFind == false)
                        checkedListBox1.SetItemChecked(i, false);
                }
            }
            _yAixs.Clear();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                    _yAixs.Add((string)checkedListBox1.Items[i]);
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (this.ChartParent != null)
            {
                ChartParent.SqlSource = this.SqlSource;
                ChartParent.InitailColumnData();
            }
        }
    }

    [Serializable]
    public class DataSource
    {
        public List<string> SelectedTableList;
        public List<string> FormulaList;
        public List<TableJoinRelation> PmsJoinRelation;
        public TreeViewDataAccess.TreeViewData WhereData;
        public TreeViewDataAccess.TreeViewData HavingData;
        public string ResultSql;
        public string ConfigSql;
        public bool UsingConfig;
        public string MainGroupBy;
        public string SecondaryGroupBy;
        public SortType MainSort;
        public SortType SecondarySort;
        public List<SortClass> SortList;

        public string XAixs;
        public List<string> YAixs;
        public List<string> XAixsies;

        public List<PMSChartArea> ChartAreaList;
        public List<PMSSeries> SeriesList;
        public List<PMSLegend> LegendList;
        public List<PMSTitle> TitleList;
        public PMSChartApp PMSChartAppearance;



        ///2011.11.02 增加
        ///目的:在多Y轴的时候字段在同一
        ///图中并非唯一的因此要加如一个属性
        public List<string> MultiYaxis;

        public Dictionary<string, List<string>> MultiYRelation;

        public Dictionary<string, string> YaxisAndCopyChart;

        public Dictionary<string, List<string>> MultiField;

        //2011.12.15 李琦 增加
        //目的：保存雷达报警图的报警变量的属性，包括上限，下限，计算函数等
        public List<AlertVar> alertList = new List<AlertVar>();
        public List<AlertVar> allAlertList = new List<AlertVar>();

        //2011.3.28 李琦 增加
        //目的：分段曲线一些外观配置
        public List<PMSAnnotation> annotationList = new List<PMSAnnotation>();
        //纯语句属性
        public DataSource(FormSql formSql,bool bSql)
        {
            if (formSql == null)
                return;

            SelectedTableList = formSql.SelectedTableList;
            FormulaList = formSql.FormulaList;
            PmsJoinRelation = formSql.PmsJoinRelation;
            WhereData = formSql.WhereData;
            ResultSql = formSql.ResultSql;
            UsingConfig = formSql.UsingConfig;
            SecondaryGroupBy = formSql.SecondaryGroupBy;
            MainGroupBy = formSql.MainGroupBy;
            SortList = formSql.SortList;
        }
        public DataSource(FormSql formSql)
        {
            if (formSql == null)
            {
                SelectedTableList = new List<string>();
                FormulaList = new List<string>();
                PmsJoinRelation = new List<TableJoinRelation>();
                WhereData = new TreeViewDataAccess.TreeViewData(null);
                ResultSql = "";
                UsingConfig = true;
                SecondaryGroupBy = "";
                MainGroupBy = "";
                SortList = new List<SortClass>();

                XAixs = "";
                YAixs = new List<string>();
                XAixsies = new List<string>();
                ChartAreaList = new List<PMSChartArea>();
                SeriesList = new List<PMSSeries>();
                LegendList = new List<PMSLegend>();
                TitleList = new List<PMSTitle>();
                PMSChartAppearance = new PMSChartApp(null);
            }
            else
            {
                SelectedTableList = formSql.SelectedTableList;
                FormulaList = formSql.FormulaList;
                PmsJoinRelation = formSql.PmsJoinRelation;
                WhereData = formSql.WhereData;
                ResultSql = formSql.ResultSql;
                UsingConfig = formSql.UsingConfig;
                SecondaryGroupBy = formSql.SecondaryGroupBy;
                MainGroupBy = formSql.MainGroupBy;
                SortList = formSql.SortList;

                XAixs = formSql.XAixs;
                YAixs = formSql.YAixs;
                XAixsies = formSql.XAixsies;
                LegendList = formSql.LegendList;
                SeriesList = formSql.SeriesList;
                ChartAreaList = formSql.ChartAreaList;
                TitleList = formSql.TitleList;
                PMSChartAppearance = formSql.PMSChartAppearance;
            }
        }
        public DataSource(StatisticalApperence formSql,int flag)
        {
            if (formSql == null)
            {
                SelectedTableList = new List<string>();
                FormulaList = new List<string>();
                PmsJoinRelation = new List<TableJoinRelation>();
                WhereData = new TreeViewDataAccess.TreeViewData(null);
                ResultSql = "";
                UsingConfig = true;
                SecondaryGroupBy = "";
                MainGroupBy = "";
                SortList = new List<SortClass>();

                XAixs = "";
                YAixs = new List<string>();
                XAixsies = new List<string>();
                ChartAreaList = new List<PMSChartArea>();
                SeriesList = new List<PMSSeries>();
                LegendList = new List<PMSLegend>();
                TitleList = new List<PMSTitle>();
                PMSChartAppearance = new PMSChartApp(null);
                MultiField = new Dictionary<string, List<string>>();
            }
            else
            {
                SelectedTableList = formSql.SelectedTableList;
                FormulaList = formSql.FormulaList;
                //PmsJoinRelation = formSql.PmsJoinRelation;
                //WhereData = formSql.WhereData;
                SortList = formSql.SortList;

                XAixs = formSql.XAixs;
                YAixs = formSql.YAixs;
                XAixsies = formSql.XAixsies;
                LegendList = formSql.LegendList;
                SeriesList = formSql.SeriesList;
                ChartAreaList = formSql.ChartAreaList;
                TitleList = formSql.TitleList;
                PMSChartAppearance = formSql.PMSChartAppearance;
                MultiField = formSql.MultiField;
            }
        }

        //11.30 李琦 添加对应雷达图的重载
        public DataSource(RadarApperence formSql,int flag)
        {
            if (formSql == null)
            {
                SelectedTableList = new List<string>();
                FormulaList = new List<string>();
                PmsJoinRelation = new List<TableJoinRelation>();
                WhereData = new TreeViewDataAccess.TreeViewData(null);
                ResultSql = "";
                UsingConfig = true;
                SecondaryGroupBy = "";
                MainGroupBy = "";
                SortList = new List<SortClass>();

                XAixs = "";
                YAixs = new List<string>();
                XAixsies = new List<string>();
                ChartAreaList = new List<PMSChartArea>();
                SeriesList = new List<PMSSeries>();
                LegendList = new List<PMSLegend>();
                TitleList = new List<PMSTitle>();
                PMSChartAppearance = new PMSChartApp(null);
            }
            else
            {
                SelectedTableList = formSql.SelectedTableList;
                FormulaList = formSql.FormulaList;
                //PmsJoinRelation = formSql.PmsJoinRelation;
                //WhereData = formSql.WhereData;
                SortList = formSql.SortList;

                XAixs = formSql.XAixs;
                YAixs = formSql.YAixs;
                XAixsies = formSql.XAixsies;
                LegendList = formSql.LegendList;
                SeriesList = formSql.SeriesList;
                ChartAreaList = formSql.ChartAreaList;
                TitleList = formSql.TitleList;
                PMSChartAppearance = formSql.PMSChartAppearance;
            }
        }

        public DataSource(CurveApperence formSql, bool flag)
        {
            if (formSql == null)
            {
                SelectedTableList = new List<string>();
                FormulaList = new List<string>();
                PmsJoinRelation = new List<TableJoinRelation>();
                WhereData = new TreeViewDataAccess.TreeViewData(null);
                ResultSql = "";
                UsingConfig = true;
                SecondaryGroupBy = "";
                MainGroupBy = "";
                SortList = new List<SortClass>();

                XAixs = "";
                YAixs = new List<string>();
                XAixsies = new List<string>();
                ChartAreaList = new List<PMSChartArea>();
                SeriesList = new List<PMSSeries>();
                LegendList = new List<PMSLegend>();
                TitleList = new List<PMSTitle>();
                MultiYaxis = new List<string>();
                MultiYRelation = new Dictionary<string, List<string>>();
                YaxisAndCopyChart = new Dictionary<string, string>();
                PMSChartAppearance = new PMSChartApp(null);
                MultiField = new Dictionary<string, List<string>>();
            }
            else
            {
                SelectedTableList = formSql.SelectedTableList;
                FormulaList = formSql.FormulaList;
                //PmsJoinRelation = formSql.PmsJoinRelation;
                //WhereData = formSql.WhereData;
                SortList = formSql.SortList;

                XAixs = formSql.XAixs;
                YAixs = formSql.YAixs;
                XAixsies = formSql.XAixsies;
                LegendList = formSql.LegendList;
                SeriesList = formSql.SeriesList;
                ChartAreaList = formSql.ChartAreaList;
                TitleList = formSql.TitleList;
                PMSChartAppearance = formSql.PMSChartAppearance;
                MultiYaxis = formSql.MultiYaxis;
                MultiYRelation = formSql.MultiYRelation;
                YaxisAndCopyChart = formSql.YaxisAndCopyChart;
                MultiField = formSql.MultiField;
            }
        }
        public void populateFormSql(FormSql formSql) 
        {            
            formSql.FormulaList = FormulaList;
            formSql.PmsJoinRelation = PmsJoinRelation;
            formSql.WhereData = WhereData;
            formSql.SelectedTableList = SelectedTableList;
            formSql.ResultSql = ResultSql;
            formSql.UsingConfig = UsingConfig;
            formSql.SecondaryGroupBy = SecondaryGroupBy;
            formSql.MainGroupBy = MainGroupBy;
            formSql.SortList = SortList;

            formSql.XAixs = XAixs;
            formSql.YAixs = YAixs;
            formSql.XAixsies = XAixsies;
            formSql.ChartAreaList = ChartAreaList;
            formSql.SeriesList = SeriesList;
            formSql.LegendList = LegendList;
            formSql.TitleList = TitleList;
            formSql.PMSChartAppearance = PMSChartAppearance;
        }

        //11.30 李琦 添加对应雷达图的重载
        public void populateFormSql(RadarApperence formSql, int flag)
        {
            formSql.FormulaList = FormulaList;
            //formSql.PmsJoinRelation = PmsJoinRelation;
            //formSql.WhereData = WhereData;
            formSql.SelectedTableList = SelectedTableList;
            formSql.SortList = SortList;

            formSql.XAixs = XAixs;
            formSql.YAixs = YAixs;
            formSql.XAixsies = XAixsies;
            formSql.ChartAreaList = ChartAreaList;
            formSql.SeriesList = SeriesList;
            formSql.LegendList = LegendList;
            formSql.TitleList = TitleList;
            formSql.PMSChartAppearance = PMSChartAppearance;
        }

        public void populateFormSql(StatisticalApperence formSql, int flag)
        {
            formSql.FormulaList = FormulaList;
            //formSql.PmsJoinRelation = PmsJoinRelation;
            //formSql.WhereData = WhereData;
            formSql.SelectedTableList = SelectedTableList;
            formSql.SortList = SortList;

            formSql.XAixs = XAixs;
            formSql.YAixs = YAixs;
            formSql.XAixsies = XAixsies;
            formSql.ChartAreaList = ChartAreaList;
            formSql.SeriesList = SeriesList;
            formSql.LegendList = LegendList;
            formSql.TitleList = TitleList;
            formSql.PMSChartAppearance = PMSChartAppearance;
            formSql.MultiField = MultiField;
        }
        public void populateFormSql(CurveApperence formSql, bool flag)
        {
            formSql.FormulaList = FormulaList;
            //formSql.PmsJoinRelation = PmsJoinRelation;
            //formSql.WhereData = WhereData;
            formSql.SelectedTableList = SelectedTableList;
            formSql.SortList = SortList;

            formSql.XAixs = XAixs;
            formSql.YAixs = YAixs;
            formSql.XAixsies = XAixsies;
            formSql.ChartAreaList = ChartAreaList;
            formSql.SeriesList = SeriesList;
            formSql.LegendList = LegendList;
            formSql.TitleList = TitleList;
            formSql.MultiYaxis = MultiYaxis;
            formSql.PMSChartAppearance = PMSChartAppearance;
            formSql.MultiYRelation = MultiYRelation;
            formSql.YaxisAndCopyChart = YaxisAndCopyChart;
            formSql.MultiField = MultiField;
        }
        public override string ToString()
        {
            return Properties.Resources.ResourceManager.GetString("context0001");
        }

        public DataSource Clone()
        {
            DataSource ds = new DataSource(null);
            try
            {
                foreach (PMSChartArea node in this.ChartAreaList)
                {

                    ChartArea ca = node.ToChartArea();
                    ca.Name = node.Name;
                    if (node is CurveY)
                    {
                        CurveY pca = new CurveY(ca);
                        pca.IsCopyChart = (node as CurveY).IsCopyChart;
                        pca.AxisY = (node as CurveY).AxisY;
                        if (node.SeriesDataList != null)
                        {
                            ((List<string>)pca.SeriesDataList).AddRange(((List<string>)node.SeriesDataList));
                        }
                        if (node.LegendDataList != null)
                        {
                            ((List<string>)pca.LegendDataList).AddRange(((List<string>)node.LegendDataList));
                        }
                        if (node.TitleDataList != null)
                        {
                            ((List<string>)pca.TitleDataList).AddRange(((List<string>)node.TitleDataList));
                        }
                        ds.ChartAreaList.Add(pca as PMSChartArea);

                    }
                    else
                    {
                        PMSChartArea pca = new PMSChartArea(ca);
                        pca.AxisX.FixPoint = node.AxisX.FixPoint;
                        if (node.SeriesDataList != null)
                        {
                            ((List<string>)pca.SeriesDataList).AddRange(((List<string>)node.SeriesDataList));
                        }
                        if (node.LegendDataList != null)
                        {
                            ((List<string>)pca.LegendDataList).AddRange(((List<string>)node.LegendDataList));
                        }
                        if (node.TitleDataList != null)
                        {
                            ((List<string>)pca.TitleDataList).AddRange(((List<string>)node.TitleDataList));
                        }
                        ds.ChartAreaList.Add(pca);
                    }
                }
            }
            catch { }
            try
            {
                foreach (PMSLegend node in this.LegendList)
                {

                    Legend ca = node.ToLegend();
                    ca.Name = node.Name;
                    if (node is MESLegend)
                    {
                        MESLegend pca = new MESLegend(ca);
                        pca.BindingField = (node as MESLegend).BindingField;
                        ds.LegendList.Add(pca);
                    }
                    else
                    {
                        PMSLegend pca = new PMSLegend(ca);
                        if (node.SeriesDataList != null)
                        {
                            ((List<string>)pca.SeriesDataList).AddRange(((List<string>)node.SeriesDataList));
                        }
                        ds.LegendList.Add(pca);
                    }
                }
            }
            catch { }
            try
            {
                foreach (PMSSeries node in this.SeriesList)
                {
                    Series ca = node.ToSeries();
                    ca.Name = node.Name;
                    if (node is CurveSeries)
                    {
                        CurveSeries pca = new CurveSeries(ca);
                        pca.BindingField = (node as CurveSeries).BindingField;
                        pca.BindingYaxis = (node as CurveSeries).BindingYaxis;
                        if (node.SeriesDataList != null)
                        {
                            ((List<string>)pca.SeriesDataList).AddRange(((List<string>)node.SeriesDataList));
                        }
                        ds.SeriesList.Add(pca as PMSSeries);
                    }
                    else if (node is MESSeries)
                    {
                        MESSeries pca = new MESSeries(ca);
                        pca.BindingField = (node as MESSeries).BindingField;
                        if (node.SeriesDataList != null)
                        {
                            ((List<string>)pca.SeriesDataList).AddRange(((List<string>)node.SeriesDataList));
                        }
                        ds.SeriesList.Add(pca as PMSSeries);
                    }
                    else if (node is SectionSeries) 
                    {
                        SectionSeries ss = new SectionSeries(ca);
                        ss.SectionChartType = (node as SectionSeries).SectionChartType;
                        ss.Distance = (node as SectionSeries).Distance;
                        ss.SortWay = (node as SectionSeries).SortWay;
                        ss.TimeType = (node as SectionSeries).TimeType;
                        ss.SectionField = (node as SectionSeries).SectionField;
                        ss.SourceField = (node as SectionSeries).SourceField;
                        ss.BindingField = (node as SectionSeries).BindingField;
                        ss.AxisMum = (node as SectionSeries).AxisMum;
                        ss.Limit = (node as SectionSeries).Limit;
                        ss.PointsCount = (node as SectionSeries).PointsCount;
                        ss.LabelStyle = (node as SectionSeries).LabelStyle;
                        ss.Format = (node as SectionSeries).Format;
                        ds.SeriesList.Add(ss);
                    }
                    else
                    {
                        PMSSeries pca = new PMSSeries(ca);
                        if (node.SeriesDataList != null)
                        {
                            ((List<string>)pca.SeriesDataList).AddRange(((List<string>)node.SeriesDataList));
                        }
                        ds.SeriesList.Add(pca);
                    }
                }
            }
            catch { }
            try
            {
                foreach (PMSTitle node in this.TitleList)
                {

                    Title ca = node.ToTitle();
                    ca.Name = node.Name;
                    PMSTitle pca = new PMSTitle(ca);
                    ds.TitleList.Add(pca);
                }
            }
            catch { }
            
            try
            {
                Chart ca = PMSChartAppearance.ToChart();
                ds.PMSChartAppearance = new PMSChartApp(ca);
            }
            catch { }
            ds.ConfigSql = this.ConfigSql;
            foreach(string node in this.FormulaList)
            {
                ds.FormulaList.Add(node);
            }
            ds.HavingData = this.HavingData;

            ds.MainGroupBy = this.MainGroupBy;
            ds.MainSort = this.MainSort;
            if (this.PmsJoinRelation != null)
            {
                foreach (TableJoinRelation node in this.PmsJoinRelation)
                {
                    ds.PmsJoinRelation.Add(node.Clone());
                }
            }
            ds.ResultSql = this.ResultSql;
            ds.SecondaryGroupBy = this.SecondaryGroupBy;
            ds.SecondarySort = this.SecondarySort;
            foreach(string node in this.SelectedTableList)
            {
                ds.SelectedTableList.Add(node);
            }
            foreach(SortClass node in this.SortList)
            {
                ds.SortList.Add(node.Clone());
            }

            ds.UsingConfig = this.UsingConfig;
            ds.WhereData = this.WhereData;
            ds.XAixs = this.XAixs;
            foreach(string node in this.XAixsies)
            {
                ds.XAixsies.Add(node);
            }
            foreach(string node in this.YAixs)
            {
                ds.YAixs.Add(node);
            }
            if (this.MultiYaxis != null)
            {
                foreach (string node in this.MultiYaxis)
                {
                    if (ds.MultiYaxis == null)
                    {
                        ds.MultiYaxis = new List<string>();
                    }
                    if (!ds.MultiYaxis.Contains(node))
                    {
                        ds.MultiYaxis.Add(node);
                    }
                }
            }
            if (this.MultiYRelation != null)
            {
                ds.MultiYRelation = new Dictionary<string, List<string>>();
                foreach (KeyValuePair<string, List<string>> item in this.MultiYRelation)
                {
                    string str = item.Key;
                    List<string> temp = new List<string>();
                    temp.AddRange(item.Value);
                    ds.MultiYRelation.Add(str, temp);
                }
            }
            if (this.YaxisAndCopyChart != null)
            {
                ds.YaxisAndCopyChart = new Dictionary<string, string>();
                foreach (KeyValuePair<string, string> item in this.YaxisAndCopyChart)
                {
                    string str = item.Key;
                    string val = item.Value;
                    ds.YaxisAndCopyChart.Add(str, val);
                }
            }
            if (this.MultiField != null)
            {
                ds.MultiField = new Dictionary<string, List<string>>();
                foreach (KeyValuePair<string, List<string>> item in this.MultiField)
                {
                    string str = item.Key;
                    List<string> temp = new List<string>();
                    temp.AddRange(item.Value);
                    ds.MultiField.Add(str, temp);
                }
            }

            //12.26 李琦 添加Clone方法对报警变量的克隆
            if (null != ds)
            {
                ds.alertList = new List<AlertVar>();
                ds.allAlertList = new List<AlertVar>();
                if (null != this.alertList)
                {
                    foreach (AlertVar av in this.alertList)
                    {
                        ds.alertList.Add(av.Clone() as AlertVar);
                    }
                }
                if (null != this.allAlertList)
                {
                    foreach (AlertVar av in this.allAlertList)
                    {
                        ds.allAlertList.Add(av.Clone() as AlertVar);
                    }
                }
            }


            //3.28 李琦 添加Clone方法对Annotation的克隆
            if (null != ds)
            {
                ds.annotationList = new List<PMSAnnotation>();
                if (null != this.annotationList)
                {
                    foreach (PMSAnnotation pa in this.annotationList)
                    {
                        LineAnnotation la= new LineAnnotation();
                        pa.SetAnnotation(la);
                        PMSAnnotation att = new PMSAnnotation(la);
                        ds.annotationList.Add(att);
                    }
                }
            }

            return ds;
        }
    }
}
