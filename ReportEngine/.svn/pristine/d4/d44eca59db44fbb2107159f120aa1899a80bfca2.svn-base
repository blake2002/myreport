using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PMS.Libraries.ToolControls.PMSChart
{
    public partial class RadarApperence : Form
    { 
        public RadarApperence()
        {
            InitializeComponent();
            InitialText();
        }
        #region 2011.10.25 私有成员变量
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

        //区域管理
        private List<PMSChartArea> chartAreaList = new List<PMSChartArea>();

        //数据外观管理
        private List<PMSSeries> seriesList = new List<PMSSeries>();

        //图例管理
        private List<PMSLegend> legendList = new List<PMSLegend>();

        //标题管理
        private List<PMSTitle> titleList = new List<PMSTitle>();

        //x坐标管理
        private List<string> _Xaixs;

        //y坐标管理
        private List<string> _yAixs;

        //所有数据源表
        List<string> _pmsTableList = null;

        //已选数据源表
        List<string> _selectedTableList = new List<string>();

        //表达式，包括单列字段
        List<string> _formulaList = new List<string>();

        ////已选表之间的关系，新增时默认为内联
        //private List<TableJoinRelation> _pmsJoinRelation = null;

        ////记录表关系，缓存
        //private Dictionary<TableField, TableJoinRelation> tableRelationCollection = new Dictionary<TableField, TableJoinRelation>();

        ////查询条件，多表，需改造
        //private TreeViewDataAccess.TreeViewData whereData;

        //选定的节点
        private TreeNode _SelectedNode = null;

        private DataSource _sqlSource;

        private PMSChartApp chartApp;
        private bool _IsReport;
        #endregion

        #region 2011.10.25 公有成员变量
        public PMSChartCtrl ChartParent;

        public PMSChartApp PMSChartAppearance
        {
            get
            {
                return chartApp;
            }
            set
            {
                chartApp = value; 
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
            }
        }

        public List<PMSChartArea> ChartAreaList
        {
            get
            {
                return chartAreaList;
            }
            set
            {
                chartAreaList = value;

            }
        }

        public List<PMSSeries> SeriesList
        {
            get
            {
                return seriesList;
            }
            set
            {
                seriesList = value;

            }
        }


        public List<PMSLegend> LegendList
        {
            get
            {
                return legendList;
            }
            set
            {
                legendList = value;

            }
        }

        public List<PMSTitle> TitleList
        {
            get
            {
                return titleList;
            }
            set
            {
                titleList = value;

            }
        }

        public string XAixs
        {
            get;
            set;
        }
        public List<string> XAixsies
        {
            get
            {
                return _Xaixs;
            }
            set
            {
                _Xaixs = value;
            }
        }
        public List<string> YAixs
        {
            get
            {
                return _yAixs;
            }
            set
            {
                _yAixs = value;
            }
        }


        public DataSource SqlSource
        {
            get
            {
                DealWithConvert();
                _sqlSource = new DataSource(this,1);
                return _sqlSource;
            }
            set
            {
                _sqlSource = value;
                if (_sqlSource != null)
                {
                    _sqlSource.populateFormSql(this,1);
                }
            }
        }

        public List<string> SelectedTableList
        {
            get
            {
                return _selectedTableList;
            }
            set
            {
                _selectedTableList = value;
            }
        }

        public List<string> FormulaList
        {
            get
            {
                return _formulaList;
            }
            set
            {
                _formulaList = value;
            }
        }

        //public List<TableJoinRelation> PmsJoinRelation
        //{
        //    get { return _pmsJoinRelation; }
        //    set
        //    {
        //        _pmsJoinRelation = value;
        //    }
        //}

        //public TreeViewDataAccess.TreeViewData WhereData
        //{
        //    get { return whereData; }
        //    set { whereData = value; }
        //}

        //排序
        public List<SortClass> SortList
        {
            get { return sortList; }
            set { sortList = value; }
        }

        public TreeNode SelectedNode
        {
            get
            {
                return _SelectedNode;
            }
            set
            {
                _SelectedNode = value;
            }
        }
        #endregion



        #region 2011.10.26 内部功能函数
        /// <summary>
        /// 2011.10.26 增加
        /// 目的:在初始化的时候,从资源文件中读取字符串,为多语言做准备
        /// </summary>
        private void InitialText()
        {
            AddArea.Text = Properties.Resources.ResourceManager.GetString("context0003");
            DeleteArea.Text = Properties.Resources.ResourceManager.GetString("context0004");
            AddLegend.Text = Properties.Resources.ResourceManager.GetString("context0006");
            AddTitile.Text = Properties.Resources.ResourceManager.GetString("context0007");
            AddField.Text = Properties.Resources.ResourceManager.GetString("context0010");
            DeleteField.Text = Properties.Resources.ResourceManager.GetString("context0011");
            DeleteLegend.Text = Properties.Resources.ResourceManager.GetString("context0012");
            DeleteTitle.Text = Properties.Resources.ResourceManager.GetString("context0013");
        }
        /// <summary>
        /// 2011.10.26 增加
        /// 目的:在新增一个区域的时候有些子项自动添加在里面
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="imageindex"></param>
        /// <param name="selectedimageindex"></param>
        /// <returns></returns>
        private TreeNode CreatChartArea(int imageindex,int selectedimageindex)
        {
            TreeNode result = null;
            PMSChartArea areatemp = new PMSChartArea(null);
            areatemp.Name = GetNameFromList(chartAreaList, "chartArea");
            result = new TreeNode(areatemp.Name, 1, 1);
            result.Name = areatemp.Name;
            result.Tag = areatemp;
            if(!chartAreaList.Contains(areatemp))
            {
                chartAreaList.Add(areatemp);
            }
            //TreeNode norm = new TreeNode(Properties.Resources.ResourceManager.GetString("context0008"),6,6);
            //norm.Name = Properties.Resources.ResourceManager.GetString("context0008");
            //result.Nodes.Add(norm);
            TreeNode field = new TreeNode(Properties.Resources.ResourceManager.GetString("context0009"), 7, 7);
            field.Name = Properties.Resources.ResourceManager.GetString("context0009");
            result.Nodes.Add(field);
            result.ExpandAll();
            return result;
        }
        /// <summary>
        /// 2011.10.28 重载
        /// 目的:根据序列化信息文件生成对应的树型结构
        /// </summary>
        /// <param name="Aim"></param>
        /// <returns></returns>
        private TreeNode CreatChartArea(PMSChartArea Aim)
        {
            TreeNode result = null;
            if (Aim != null)
            {
                result = new TreeNode(Aim.Name, 1, 1);
                result.Name = Aim.Name;
                result.Tag = Aim;
                TreeNode field = new TreeNode(Properties.Resources.ResourceManager.GetString("context0009"), 7, 7);
                field.Name = Properties.Resources.ResourceManager.GetString("context0009");
                if (Aim.SeriesDataList != null && Aim.SeriesDataList is List<string>)
                {
                    List<string> strtemp = Aim.SeriesDataList as List<string>;
                    for (int i = 0; i < strtemp.Count; i++)
                    {
                        string str = strtemp[i];
                        TreeNode newnode = CreatChartSeries(str);
                        if (newnode != null)
                        {
                            field.Nodes.Add(newnode);
                        }
                    }
                    
                }
                result.Nodes.Add(field);
                if (Aim.LegendDataList != null && Aim.LegendDataList is List<string>)
                {
                    List<string> strtemp = Aim.LegendDataList as List<string>;
                    for (int i = 0; i < strtemp.Count; i++)
                    {
                        string str = strtemp[i];
                        TreeNode newnode = CreatChartLegend(str);
                        if (newnode != null)
                        {
                            result.Nodes.Add(newnode);
                        }
                    }
                }
                if (Aim.TitleDataList != null && Aim.TitleDataList is List<string>)
                {
                    List<string> strtemp = Aim.TitleDataList as List<string>;
                    for (int i = 0; i < strtemp.Count; i++)
                    {
                        string str = strtemp[i];
                        TreeNode newnode = CreatChartTiTle(str);
                        if (newnode != null)
                        {
                            result.Nodes.Add(newnode);
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 2011.10.27 增加
        /// 目的:在树型结构中增加一个序列
        /// </summary>
        /// <returns></returns>
        private TreeNode CreatChartSeries()
        {
            TreeNode result = null;
            RadarSeries seriestemp = new RadarSeries(null);
            //seriestemp.ChartType = SeriesChartType.Column;
            seriestemp.SourceField = this.ChartParent.SourceField;
            seriestemp.seriesDataForAppearance = this.seriesDataForAppearance;
            seriestemp.Name = GetNameFromList(seriesList, "series");
            result = new TreeNode(seriestemp.Name, 3, 3);
            result.Name = seriestemp.Name;
            result.Tag = seriestemp;
            if (!seriesList.Contains(seriestemp as PMSSeries))
            {
                seriesList.Add(seriestemp as PMSSeries);
            }
            return result;
        }
        /// <summary>
        /// 2011.10.28 重载
        /// 目的:根据序列化信息文件生成一个数据序列的树型结构
        /// </summary>
        /// <param name="Aim"></param>
        /// <returns></returns>
        private TreeNode CreatChartSeries(string Aim)
        {
            TreeNode result = null;
            RadarSeries temp = GetRadarSeriesByBindingName(Aim);
            if (temp != null)
            {
                result = new TreeNode(temp.Name, 3, 3);
                result.Name = temp.Name;
                result.Tag = temp;
            }
            return result;
        }
        /// <summary>
        /// 2011.10.27 增加
        /// 目的;在树型结构中加入x轴绑定字段
        /// 在一个控件中有且只有一个绑定基准
        /// </summary>
        /// <returns></returns>
        private TreeNode CreatChartJustice()
        {
            TreeNode result = null;
            result = new TreeNode(Properties.Resources.ResourceManager.GetString("context0008"), 6, 6);
            result.Name = Properties.Resources.ResourceManager.GetString("context0008");
            XGroup temp = new XGroup();
            result.Tag = temp;
            return result;
        }
        /// <summary>
        /// 2011.10.27 增加
        /// 目的;在树型结构中加入x轴绑定字段
        /// 在一个控件中有且只有一个绑定基准
        /// </summary>
        /// <returns></returns>
        private TreeNode CreatChartJustice(string Name)
        {
            TreeNode result = null;
            result = new TreeNode(Properties.Resources.ResourceManager.GetString("context0008"), 6, 6);
            result.Name = Properties.Resources.ResourceManager.GetString("context0008");
            XGroup temp = new XGroup();
            temp.xRecordField = Name;
            result.Tag = temp;
            return result;
        }
        /// <summary>
        /// 2011.10.27 增加
        /// 目的:在树型结构中为一个区域增加一个图例
        /// </summary>
        /// <returns></returns>
        private TreeNode CreatChartLegend()
        {
            TreeNode result = null;
            MESLegend pca = new MESLegend(null);
            pca.Name = GetNameFromList(legendList, "legend");
            pca.SourceField = this.ChartParent.SourceField;
            pca.seriesDataForLegend = this.seriesDataForLegend;
            result = new TreeNode(pca.Name, 2, 2);
            result.Name = pca.Name;
            result.Tag = pca;
            if (!legendList.Contains(pca as PMSLegend))
            {
                legendList.Add(pca as PMSLegend);
            }
            return result;
        }
        /// <summary>
        /// 2011.10.28 重载
        /// 目的:根据序列化信息文件生成一个图例的树型结构
        /// </summary>
        /// <param name="Aim"></param>
        /// <returns></returns>
        private TreeNode CreatChartLegend(string str)
        {
            TreeNode result = null;
            //MESLegend temp = GetMESLegendByBindingName(str);
            MESLegend temp = GetMESLegendByName(str);
            if (temp != null)
            {
                result = new TreeNode(temp.Name, 2, 2);
                result.Name = temp.Name;
                result.Tag = temp;
            }
            return result;
        }
        /// <summary>
        /// 2011.10.27 增加
        /// 目的:在树型结构中为一个区域增加标题
        /// </summary>
        /// <returns></returns>
        private TreeNode CreatChartTiTle()
        {
            TreeNode result = null;
            PMSTitle pca = new PMSTitle(null);
            pca.Name = GetNameFromList(titleList, "title");
            result = new TreeNode(pca.Name, 0, 0);
            result.Name = pca.Name;
            result.Tag = pca;
            if (!titleList.Contains(pca))
            {
                titleList.Add(pca);
            }
            return result;
        }
        /// <summary>
        /// 2011.10.28 增加
        /// 目的:根据序列化信息文件为区域增加标题
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private TreeNode CreatChartTiTle(string str)
        {
            TreeNode result = null;
            PMSTitle pca= GetPMSTitleByName(str);
            if (pca != null)
            {
                result = new TreeNode(pca.Name, 0, 0);
                result.Name = pca.Name;
                result.Tag = pca;
            }
            return result;
        }

        /// <summary>
        /// 对指定序列进行由小到大的排序
        /// </summary>
        /// <param name="nameNO">需要排序的序列</param>
        /// <returns></returns>
        static public void resetList(List<int> nameNO)
        {
            int Var;
            for (int i = 0; i < nameNO.Count; i++)
            {
                for (int j = i + 1; j < nameNO.Count; j++)
                {
                    if (nameNO[i] > nameNO[j])
                    {
                        Var = nameNO[i];
                        nameNO[i] = nameNO[j];
                        nameNO[j] = Var;
                    }
                }
            }
        }

        /// <summary>
        /// 2011.12.23 李琦 增加
        /// 目的:从已有的类型列表中找到系统自动增加的最大的索引号
        /// </summary>
        /// <param name="data"></param>
        /// <param name="aim"></param>
        /// <returns></returns>
        private string GetNameFromList(object data, string aim)
        {
            string result = aim;
            int NO = 0;
            List<int> nameNO = new List<int>();
            if (data is List<PMSChartArea>)
            {
                List<PMSChartArea> areatemp = data as List<PMSChartArea>;
                if (areatemp != null)
                {
                    foreach (PMSChartArea item in areatemp)
                    {
                        if (item.Name.StartsWith(aim))
                        {
                            int i;
                            if (int.TryParse(item.Name.Substring(9), out i))
                            {
                                nameNO.Add(i);
                            }
                        }
                    }
                    resetList(nameNO);
                    for (int i = 0; i < nameNO.Count; i++)
                    {
                        if (nameNO[i] != i + 1)
                        {
                            NO = i + 1;
                            break;
                        }
                    }
                    if (NO == 0 && nameNO.Count != 0)
                    {
                        NO = nameNO[nameNO.Count - 1] + 1;
                    }
                    else if (nameNO.Count == 0) { NO = 1; }
                }
            }
            else if (data is List<PMSSeries>)
            {
                List<PMSSeries> areatemp = data as List<PMSSeries>;
                if (areatemp != null)
                {
                    foreach (PMSSeries item in areatemp)
                    {
                        if (item.Name.StartsWith(aim))
                        {
                            int i;
                            if (int.TryParse(item.Name.Substring(6), out i))
                            {
                                nameNO.Add(i);
                            }
                        }
                    }
                    resetList(nameNO);
                    for (int i = 0; i < nameNO.Count; i++)
                    {
                        if (nameNO[i] != i + 1)
                        {
                            NO = i + 1;
                            break;
                        }
                    }
                    if (NO == 0 && nameNO.Count != 0)
                    {
                        NO = nameNO[nameNO.Count - 1] + 1;
                    }
                    else if (nameNO.Count == 0) { NO = 1; }
                }
            }
            else if (data is List<PMSLegend>)
            {
                List<PMSLegend> areatemp = data as List<PMSLegend>;
                if (areatemp != null)
                {
                    foreach (PMSLegend item in areatemp)
                    {
                        if (item.Name.StartsWith(aim))
                        {
                            int i;
                            if (int.TryParse(item.Name.Substring(6), out i))
                            {
                                nameNO.Add(i);
                            }
                        }
                    }
                    resetList(nameNO);
                    for (int i = 0; i < nameNO.Count; i++)
                    {
                        if (nameNO[i] != i + 1)
                        {
                            NO = i + 1;
                            break;
                        }
                    }
                    if (NO == 0 && nameNO.Count != 0)
                    {
                        NO = nameNO[nameNO.Count - 1] + 1;
                    }
                    else if (nameNO.Count == 0) { NO = 1; }
                }
            }
            else if (data is List<PMSTitle>)
            {
                List<PMSTitle> areatemp = data as List<PMSTitle>;
                if (areatemp != null)
                {
                    foreach (PMSTitle item in areatemp)
                    {
                        if (item.Name.StartsWith(aim))
                        {
                            int i;
                            if (int.TryParse(item.Name.Substring(5), out i))
                            {
                                nameNO.Add(i);
                            }
                        }
                    }
                    resetList(nameNO);
                    for (int i = 0; i < nameNO.Count; i++)
                    {
                        if (nameNO[i] != i + 1)
                        {
                            NO = i + 1;
                            break;
                        }
                    }
                    if (NO == 0 && nameNO.Count != 0)
                    {
                        NO = nameNO[nameNO.Count - 1] + 1;
                    }
                    else if (nameNO.Count == 0) { NO = 1; }
                }
            }
            return result+NO;
        }
        /// <summary>
        /// 2011.10.27 增加
        /// 目的:校验名字是否有重复有返回true无返回false
        /// </summary>
        /// <param name="Aim"></param>
        /// <returns></returns>
        private bool CheckNameFromList(object Aim)
        {
            bool result = false;
            if (Aim != null)
            {
                if (Aim is PMSChartArea)
                {
                    if (chartAreaList != null)
                    {
                        for (int i = 0; i < chartAreaList.Count; i++)
                        {
                            PMSChartArea temp = chartAreaList[i];
                            if (temp!=(Aim as PMSChartArea)&&  temp.Name == (Aim as PMSChartArea).Name)
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
                else if (Aim is PMSSeries)
                {
                    if (seriesList != null)
                    {
                        for (int i = 0; i < seriesList.Count; i++)
                        {
                            PMSSeries temp = seriesList[i];
                            if (temp!=(Aim as PMSSeries)&&  temp.Name == (Aim as PMSSeries).Name)
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
                else if (Aim is PMSLegend)
                {
                    if (legendList != null)
                    {
                        for (int i = 0; i < legendList.Count; i++)
                        {
                            PMSLegend temp = legendList[i];
                            if (temp !=(Aim as PMSLegend)&& temp.Name == (Aim as PMSLegend).Name)
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
                else if (Aim is PMSTitle)
                {
                    if (titleList != null)
                    {
                        for (int i = 0; i < titleList.Count; i++)
                        {
                            PMSTitle temp = titleList[i];
                            if (temp!= (Aim as PMSTitle)&&temp.Name == (Aim as PMSTitle).Name)
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 2011.10.26 增加
        /// 目的:判断字符串中是否全是数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private bool IsAllNum(string str)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(str))
            {
                char[] strCopy = str.ToCharArray();
                for (int i = 0; i < strCopy.Length; i++)
                {
                    if (strCopy[i] > 47 && strCopy[i] < 58)
                    {
                        continue;
                    }
                    else
                    {
                        return result;
                    }
                }
            }
            else
            {
                return result;
            }
            result = true;
            return result;
        }
        /// <summary>
        /// 2011.10.26 增加
        /// 目的:增加一个区域
        /// </summary>
        /// <param name="Aim"></param>
        private void AddChartArea(TreeNode Aim)
        {
            string rootname = Properties.Resources.ResourceManager.GetString("context0002");
            if (Aim.Name == rootname)
            {
                TreeNode nodetemp = CreatChartArea(1, 1);
                if (treeView1.Nodes[rootname] != null)
                {
                    treeView1.Nodes[rootname].Nodes.Add(nodetemp);
                }
            }
            else
            {
            }
        }
        /// <summary>
        /// 2011.10.26 增加
        /// 目的:处理一个区域删除
        /// </summary>
        /// <param name="Aim"></param>
        private void DeleteChartArea(TreeNode Aim)
        {
            string rootname = Properties.Resources.ResourceManager.GetString("context0002");
            if (Aim.Parent != null && Aim.Parent.Name == rootname)
            {
                if (Aim.Parent.Nodes.Count > 1)
                {
                    if (Aim.Nodes != null && Aim.Nodes.Count>0)
                    {
                        for (int i = 0; i < Aim.Nodes.Count; i++)
                        {
                            TreeNode nodetemp = Aim.Nodes[i];
                            if (nodetemp.Tag == null && nodetemp.Name == Properties.Resources.ResourceManager.GetString("context0009"))
                            {
                                if (nodetemp.Nodes != null)
                                {
                                    for (int j = 0; j < nodetemp.Nodes.Count; j++)
                                    {
                                        TreeNode fieldtemp = nodetemp.Nodes[j];
                                        if (fieldtemp.Tag is RadarSeries)
                                        {
                                            RadarSeries mestemp = fieldtemp.Tag as RadarSeries;
                                            seriesDataForAppearance.Remove(mestemp.BindingField);
                                        }
                                    }
                                }
                            }
                            else if (nodetemp.Tag != null && nodetemp.Tag is MESLegend)
                            {
                                MESLegend legtemp = nodetemp.Tag as MESLegend;
                                seriesDataForLegend.Remove(legtemp.BindingField);
                            }
                        }
                    }

                    if (Aim.Tag != null && Aim.Tag is PMSChartArea)
                    {
                        chartAreaList.Remove(Aim.Tag as PMSChartArea);
                    }
                    Aim.Parent.Nodes.Remove(Aim);
                }
                else
                {
                    MessageBox.Show(Properties.Resources.ResourceManager.GetString("message0001"));
                }
            }
            else
            {
            }
        }
        ///// <summary>
        ///// 2011.10.27 增加
        ///// 目的:增加一个表格序列
        ///// </summary>
        ///// <param name="Aim"></param>
        //private void AddChartSerie(TreeNode Aim)
        //{
        //}
        /// <summary>
        /// 2011.10.27 增加
        /// 目的:增加一个图例
        /// </summary>
        /// <param name="Aim"></param>
        private void AddChartLegend(TreeNode Aim)
        {
            if (Aim.Tag != null && Aim.Tag is PMSChartArea)
            {
                TreeNode nodetemp = CreatChartLegend();
                Aim.Nodes.Add(nodetemp);
                Aim.ExpandAll();
            }
        }
        /// <summary>
        /// 2011.10.27 增加
        /// 目的:增加一个标题
        /// </summary>
        /// <param name="Aim"></param>
        private void AddChartTitile(TreeNode Aim)
        {
            if (Aim.Tag != null && Aim.Tag is PMSChartArea)
            {
                TreeNode nodetemp = CreatChartTiTle();
                Aim.Nodes.Add(nodetemp);
                Aim.ExpandAll();
            }
        }
        /// <summary>
        /// 2011.10.27 增加
        /// 目的:增加一个绑定字段
        /// </summary>
        /// <param name="Aim"></param>
        private void AddChartField(TreeNode Aim)
        {
            string rootname = Properties.Resources.ResourceManager.GetString("context0009");
            if (Aim.Tag == null && Aim.Name == rootname)
            {
                TreeNode nodetemp = CreatChartSeries();
                Aim.Nodes.Add(nodetemp);
                Aim.ExpandAll();
            }
        }
        /// <summary>
        /// 2011.10.27 增加
        /// 目的:删除一个绑定字段
        /// </summary>
        /// <param name="Aim"></param>
        private void DeleteChartField(TreeNode Aim)
        {
            if (Aim.Parent != null)
            {
                if (Aim.Parent.Nodes.Count > 0)
                {
                    if (Aim.Tag != null && Aim.Tag is RadarSeries)
                    {
                        seriesDataForAppearance.Remove((Aim.Tag as RadarSeries).BindingField);
                    }
                    if (Aim.Tag != null && Aim.Tag is PMSSeries)
                    {
                        seriesList.Remove(Aim.Tag as PMSSeries);
                    }
                    Aim.Parent.Nodes.Remove(Aim);
                }
            }
            else
            {
            }
        }
        /// <summary>
        /// 2011.10.27 增加
        /// 目的:删除一个图例
        /// </summary>
        /// <param name="Aim"></param>
        private void DeleteChartLegend(TreeNode Aim)
        {
            if (Aim.Parent != null)
            {
                if (Aim.Parent.Nodes.Count > 0)
                {
                    if (Aim.Tag != null && Aim.Tag is MESLegend)
                    {
                        seriesDataForLegend.Remove((Aim.Tag as MESLegend).BindingField);
                    }
                    if (Aim.Tag != null && Aim.Tag is PMSLegend)
                    {
                      legendList.Remove(Aim.Tag as PMSLegend);
                    }
                    Aim.Parent.Nodes.Remove(Aim);
                }
            }
            else
            {
            }
        }
        /// <summary>
        /// 2011.10.27 增加
        /// 目的:删除一个标题
        /// </summary>
        /// <param name="Aim"></param>
        private void DeleteChartTitle(TreeNode Aim)
        {
            if (Aim.Parent != null)
            {
                if (Aim.Parent.Nodes.Count > 0)
                {
                    if (Aim.Tag != null && Aim.Tag is PMSTitle)
                    {
                        titleList.Remove(Aim.Tag as PMSTitle);
                    }
                    Aim.Parent.Nodes.Remove(Aim);
                }
            }
            else
            {
            }
        }
        /// <summary>
        /// 2011.10.27 增加
        /// 目的:由于目前处理是以小马哥以前的处理方式为基础,因此自己扩展出来的东西要以小
        /// 马哥的格式存储,这样的好处是能快速处理
        /// </summary>
        private void DealWithConvert()
        {
            if (treeView1.Nodes != null)
            {
                for (int n = 0; n < treeView1.Nodes.Count; n++)
                {
                    TreeNode temp1 = treeView1.Nodes[n];
                    if (temp1.Name == Properties.Resources.ResourceManager.GetString("context0002") && temp1.Nodes!=null)
                    {
                        for (int m = 0; m < temp1.Nodes.Count; m++)
                        {
                            TreeNode temp2 = temp1.Nodes[m];
                            if (temp2.Tag != null && temp2.Tag is PMSChartArea)
                            {
                                DealWithChartArea(temp2);
                            }
                            else if (temp2.Tag != null && temp2.Tag is XGroup)
                            {
                                XAixs = (temp2.Tag as XGroup).xRecordField;
                            }
                        }
                    }
                }
            }
            _yAixs.Clear();
            if (seriesList != null)
            {
                for (int i = 0; i < seriesList.Count; i++)
                {
                    PMSSeries temp = seriesList[i];
                    if (temp is RadarSeries)
                    {
                        RadarSeries mestemp = temp as RadarSeries;
                        string str = mestemp.BindingField;
                        if (temp.SeriesDataList == null)
                        {
                            temp.SeriesDataList = new List<string>();
                        }
                        if (temp.SeriesDataList is List<string>)
                        {
                            (temp.SeriesDataList as List<string>).Clear();
                            (temp.SeriesDataList as List<string>).Add(str);
                        }
                        if (!_yAixs.Contains(str))
                        {
                            _yAixs.Add(str);
                        }
                    }
                }
            }
            if (legendList != null)
            {
                for (int i = 0; i < legendList.Count; i++)
                {
                    PMSLegend temp = legendList[i];
                    if (temp is MESLegend)
                    {
                        MESLegend mestemp = temp as MESLegend;
                        string str = mestemp.BindingField;
                        if (temp.SeriesDataList == null)
                        {
                            temp.SeriesDataList = new List<string>();
                        }
                        if (temp.SeriesDataList is List<string>)
                        {
                            (temp.SeriesDataList as List<string>).Clear();
                            (temp.SeriesDataList as List<string>).Add(str);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 2011.10.27 增加
        /// 目的:对数据区域进行处理
        /// </summary>
        /// <param name="Aim"></param>
        private void DealWithChartArea(TreeNode Aim)
        {
            if (Aim.Tag != null && Aim.Tag is PMSChartArea)
            {
                PMSChartArea temp = Aim.Tag as PMSChartArea;
                if (temp.LegendDataList == null)
                {
                    temp.LegendDataList = new List<string>();
                }
                if (temp.LegendDataList is List<string>)
                {
                    (temp.LegendDataList as List<string>).Clear();
                }
                if (temp.TitleDataList == null)
                {
                    temp.TitleDataList = new List<string>();
                }
                if (temp.TitleDataList is List<string>)
                {
                    (temp.TitleDataList as List<string>).Clear();
                }
                if (Aim.Nodes != null)
                {
                    for (int i = 0; i < Aim.Nodes.Count; i++)
                    {
                        TreeNode nodetemp = Aim.Nodes[i];
                        if (nodetemp.Tag == null && nodetemp.Name == Properties.Resources.ResourceManager.GetString("context0009"))
                        {
                            if (nodetemp.Nodes != null)
                            {
                                if (temp.SeriesDataList == null)
                                {
                                    temp.SeriesDataList = new List<string>();
                                }
                                if (temp.SeriesDataList is List<string>)
                                {
                                    (temp.SeriesDataList as List<string>).Clear();
                                }
                                for (int j = 0; j < nodetemp.Nodes.Count; j++)
                                {
                                    TreeNode fieldtemp = nodetemp.Nodes[j];
                                    if (fieldtemp.Tag is RadarSeries)
                                    {
                                        RadarSeries mestemp = fieldtemp.Tag as RadarSeries;
                                        if (temp.SeriesDataList is List<string>)
                                        {
                                            if (!(temp.SeriesDataList as List<string>).Contains(mestemp.BindingField))
                                            {
                                                (temp.SeriesDataList as List<string>).Add(mestemp.BindingField);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (nodetemp.Tag != null && nodetemp.Tag is MESLegend)
                        {
                            MESLegend legtemp = nodetemp.Tag as MESLegend;
                            if (temp.LegendDataList is List<string>)
                            {
                                if (!(temp.LegendDataList as List<string>).Contains(legtemp.Name))
                                {
                                    (temp.LegendDataList as List<string>).Add(legtemp.Name);
                                }
                            }
                        }
                        else if (nodetemp.Tag != null && nodetemp.Tag is PMSTitle)
                        {
                            PMSTitle titletemp = nodetemp.Tag as PMSTitle;
                            if (temp.TitleDataList is List<string>)
                            {
                                if (!(temp.TitleDataList as List<string>).Contains(titletemp.Name))
                                {
                                    (temp.TitleDataList as List<string>).Add(titletemp.Name);
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 2011.10.28 增加
        /// 目的:根据名字获取MES数据序列
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private RadarSeries GetRadarSeriesByBindingName(string str)
        {
            RadarSeries result = null;
            if (seriesList != null)
            {
                for (int i = 0; i < seriesList.Count; i++)
                {
                    PMSSeries temp = seriesList[i];
                    if (temp is RadarSeries)
                    {
                        if ((temp as RadarSeries).BindingField == str)
                        {
                            return temp as RadarSeries;
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 2011.10.28 增加
        /// 目的:根据名字获取MES数据序列
        /// (这也就是要求字段与数据序列要限制成一一对应的关系)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private RadarSeries GetRadarSeriesByName(string str)
        {
            RadarSeries result = null;
            if (seriesList != null)
            {
                for (int i = 0; i < seriesList.Count; i++)
                {
                    PMSSeries temp = seriesList[i];
                    if (temp is RadarSeries)
                    {
                        if ((temp as RadarSeries).Name == str)
                        {
                            return temp as RadarSeries;
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 2011.10.28 增加
        /// 目的:根据名字获取MES图例
        /// (这也就是要求字段与数据序列要限制成一一对应的关系)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private MESLegend GetMESLegendByName(string str)
        {
            MESLegend result = null;
            if (legendList != null)
            {
                for (int i = 0; i < legendList.Count; i++)
                {
                    PMSLegend temp = legendList[i];
                    if (temp is MESLegend)
                    {
                        if ((temp as MESLegend).Name == str)
                        {
                            return temp as MESLegend;
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 2011.10.28 增加
        /// 目的:根据绑定字段获取MES图例
        /// (这也就是要求字段与数据序列要限制成一一对应的关系)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private MESLegend GetMESLegendByBindingName(string str)
        {
            MESLegend result = null;
            if (legendList != null)
            {
                for (int i = 0; i < legendList.Count; i++)
                {
                    PMSLegend temp = legendList[i];
                    if (temp is MESLegend)
                    {
                        if ((temp as MESLegend).BindingField == str)
                        {
                            return temp as MESLegend;
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 2011.10.28 增加
        /// 目的:根据名字获取PMS标题
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private PMSTitle GetPMSTitleByName(string str)
        {
            PMSTitle result = null;
            if (titleList != null)
            {
                for (int i = 0; i < titleList.Count; i++)
                {
                    PMSTitle temp = titleList[i];
                    if (temp.Name == str)
                    {
                        return temp;
                    }
                }
            }
            return result;
        }
        #endregion


        #region 2011.10.26 控件响应的各种事件
        /// <summary>
        /// 2011.10.26 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadarApperence_Load(object sender, EventArgs e)
        {
            if (chartAreaList.Count == 0)
            {
                treeView1.Nodes.Clear();
                string rootname = Properties.Resources.ResourceManager.GetString("context0002");
                TreeNode rootnode = new TreeNode(rootname, 1, 1);
                rootnode.Name = rootname;
                treeView1.Nodes.Add(rootnode);
                TreeNode justice = CreatChartJustice();
                if (justice.Tag != null && justice.Tag is XGroup)
                {
                    XGroup temp = justice.Tag as XGroup;
                    if (this.ChartParent != null)
                    {
                        temp.SourceField = this.ChartParent.SourceField;
                    }
                }
                if (treeView1.Nodes[rootname] != null)
                {
                    treeView1.Nodes[rootname].Nodes.Add(justice);
                }
                TreeNode nodetemp = CreatChartArea(1, 1);
                if (treeView1.Nodes[rootname] != null)
                {
                    treeView1.Nodes[rootname].Nodes.Add(nodetemp);
                    treeView1.Nodes[rootname].ExpandAll();
                }
            }
            else
            {
                seriesDataForAppearance.Clear();
                if(seriesList!=null)
                {
                    for(int m=0;m<seriesList.Count;m++)
                    {
                        PMSSeries aa= seriesList[m];
                        if(aa is RadarSeries)
                        {
                            if (this.ChartParent != null)
                            {
                                (aa as RadarSeries).SourceField = this.ChartParent.SourceField;
                                (aa as RadarSeries).seriesDataForAppearance = this.seriesDataForAppearance;
                                if (!seriesDataForAppearance.Contains((aa as RadarSeries).BindingField))
                                {
                                    seriesDataForAppearance.Add((aa as RadarSeries).BindingField);
                                }
                            }
                        }
                    }
                }
                seriesDataForLegend.Clear();
                if (legendList != null)
                {
                    for (int m = 0; m < legendList.Count; m++)
                    {
                        PMSLegend aa = legendList[m];
                        if (aa is MESLegend)
                        {
                            if (this.ChartParent != null)
                            {
                                (aa as MESLegend).SourceField = this.ChartParent.SourceField;
                                (aa as MESLegend).seriesDataForLegend = this.seriesDataForLegend;
                                if (!seriesDataForLegend.Contains((aa as MESLegend).BindingField))
                                {
                                    seriesDataForLegend.Add((aa as MESLegend).BindingField);
                                }
                            }
                        }
                    }
                }
                treeView1.Nodes.Clear();
                string rootname = Properties.Resources.ResourceManager.GetString("context0002");
                TreeNode rootnode = new TreeNode(rootname, 1, 1);
                rootnode.Name = rootname;
                treeView1.Nodes.Add(rootnode);
                TreeNode justice = CreatChartJustice(XAixs);
                if (justice.Tag != null && justice.Tag is XGroup)
                {
                    XGroup temp = justice.Tag as XGroup;
                    if (this.ChartParent != null)
                    {
                        temp.SourceField = this.ChartParent.SourceField;
                    }
                }
                if (treeView1.Nodes[rootname] != null)
                {
                    treeView1.Nodes[rootname].Nodes.Add(justice);
                }
                if (chartAreaList != null)
                {
                    for (int i = 0; i < chartAreaList.Count; i++)
                    {
                        PMSChartArea areatemp = chartAreaList[i];
                        TreeNode areanode = CreatChartArea(areatemp);
                        if (treeView1.Nodes[rootname] != null)
                        {
                            treeView1.Nodes[rootname].Nodes.Add(areanode);
                        }
                    }
                    treeView1.ExpandAll();
                }
            }
        }
        /// <summary>
        /// 2011.10.26 选择树节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            propertyGrid1.SelectedObject = e.Node.Tag;
        }
        /// <summary>
        /// 2011.10.26 按下确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sure_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }
        /// <summary>
        /// 2011.10.26 按下取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }
        /// <summary>
        /// 2011.10.26 按下应用按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Apply_Click(object sender, EventArgs e)
        {
            if (this.ChartParent != null)
            {
                DealWithConvert();
                ChartParent.SqlSource = this.SqlSource;
                ChartParent.InitailColumnData();
            }
        }
        /// <summary>
        /// 2011.10.26 左右鼠标点击节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeView1.SelectedNode = e.Node;
                if (e.Node != null )
                {
                    _SelectedNode = e.Node;
                    if (e.Node.Tag == null && e.Node.Parent==null)
                    {
                        treeView1.ContextMenuStrip = ManageArea;
                        ManageArea.Visible = true;
                    }
                    else if (e.Node.Tag != null && e.Node.Tag is PMSChartArea)
                    {
                        treeView1.ContextMenuStrip = AreaChildManage;
                        AreaChildManage.Visible = true;
                    }
                    else if (e.Node.Tag == null && e.Node.Text == Properties.Resources.ResourceManager.GetString("context0009"))
                    {
                        treeView1.ContextMenuStrip = ManageSeries;
                        ManageSeries.Visible = true;
                    }
                    else if (e.Node.Tag != null && e.Node.Tag is PMSSeries)
                    {
                        treeView1.ContextMenuStrip = FieldDelete;
                        FieldDelete.Visible = true;
                    }
                    else if (e.Node.Tag != null && e.Node.Tag is PMSLegend)
                    {
                        treeView1.ContextMenuStrip = LegendDelete;
                        LegendDelete.Visible = true;
                    }
                    else if (e.Node.Tag != null && e.Node.Tag is PMSTitle)
                    {
                        treeView1.ContextMenuStrip = TitleDelete;
                        TitleDelete.Visible = true;
                    }
                    else
                    {
                        treeView1.ContextMenuStrip = null;
                    }
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                propertyGrid1.SelectedObject = e.Node.Tag;
            }
        }
        /// <summary>
        /// 2011.10.26 属性窗口值变化
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "Name")
            {
                if (this.treeView1.SelectedNode.Tag != null)
                {
                    if (CheckNameFromList(this.treeView1.SelectedNode.Tag) == false)
                    {
                        this.treeView1.SelectedNode.Text = e.ChangedItem.Value.ToString();
                        this.treeView1.Name = e.ChangedItem.Value.ToString();
                    }
                    else
                    {
                        if (this.treeView1.SelectedNode.Tag is PMSChartArea)
                        {
                            (this.treeView1.SelectedNode.Tag as PMSChartArea).Name = e.OldValue.ToString();
                            MessageBox.Show(Properties.Resources.ResourceManager.GetString("message0002"));
                        }
                        else if (this.treeView1.SelectedNode.Tag is PMSSeries)
                        {
                            (this.treeView1.SelectedNode.Tag as PMSSeries).Name = e.OldValue.ToString();
                            MessageBox.Show(Properties.Resources.ResourceManager.GetString("message0002"));
                        }
                        else if (this.treeView1.SelectedNode.Tag is PMSLegend)
                        {
                            (this.treeView1.SelectedNode.Tag as PMSLegend).Name = e.OldValue.ToString();
                            MessageBox.Show(Properties.Resources.ResourceManager.GetString("message0002"));
                        }
                        else if (this.treeView1.SelectedNode.Tag is PMSTitle)
                        {
                            (this.treeView1.SelectedNode.Tag as PMSTitle).Name = e.OldValue.ToString();
                            MessageBox.Show(Properties.Resources.ResourceManager.GetString("message0002"));
                        }
                    }
                }
            }
            else if (e.ChangedItem.Label == "BindingField")
            {
                if (this.treeView1.SelectedNode.Tag != null)
                {
                    if (this.treeView1.SelectedNode.Tag is PMSChartArea)
                    {
                    }
                    else if (this.treeView1.SelectedNode.Tag is RadarSeries)
                    {
                        seriesDataForAppearance.Remove(e.OldValue.ToString());
                        RadarSeries temp=treeView1.SelectedNode.Tag as RadarSeries;
                        if(!seriesDataForAppearance.Contains(temp.BindingField))
                        {
                            seriesDataForAppearance.Add(temp.BindingField);
                        }
                    }
                    else if (this.treeView1.SelectedNode.Tag is MESLegend)
                    {
                        seriesDataForLegend.Remove(e.OldValue.ToString());
                        MESLegend temp = treeView1.SelectedNode.Tag as MESLegend;
                        if (!seriesDataForLegend.Contains(temp.BindingField))
                        {
                            seriesDataForLegend.Add(temp.BindingField);
                        }
                    }
                    else if (this.treeView1.SelectedNode.Tag is PMSTitle)
                    {
                    }
                }
            }
        }
        /// <summary>
        /// 2011.10.26 新增区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddArea_Click(object sender, EventArgs e)
        {
            if (_SelectedNode != null)
            {
                AddChartArea(_SelectedNode);
            }
        }
        /// <summary>
        /// 2011.10.26 删除区域
        /// 删除的时候要校验不能删最后一个,默认至少要有一个区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteArea_Click(object sender, EventArgs e)
        {
            if (_SelectedNode != null)
            {
                DeleteChartArea(_SelectedNode);
            }
        }
        ///// <summary>
        ///// 2011.10.26 新增曲线
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void AddSerie_Click(object sender, EventArgs e)
        //{
        //    if (_SelectedNode != null)
        //    {
        //        AddChartSerie(_SelectedNode);
        //    }
        //}
        /// <summary>
        /// 2011.10.26 新增图例
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddLegend_Click(object sender, EventArgs e)
        {
            if (_SelectedNode != null)
            {
                AddChartLegend(_SelectedNode);
            }
        }
        /// <summary>
        /// 2011.10.26 新增标题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddTitile_Click(object sender, EventArgs e)
        {
            if (_SelectedNode != null)
            {
                AddChartTitile(_SelectedNode);
            }
        }
        /// <summary>
        /// 2011.10.27 新增字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddField_Click(object sender, EventArgs e)
        {
            if (_SelectedNode!=null)
            {
                AddChartField(_SelectedNode);
            }
        }
        /// <summary>
        /// 2011.10.27 删除绑定字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteField_Click(object sender, EventArgs e)
        {
            if (_SelectedNode != null)
            {
                DeleteChartField(_SelectedNode);
            }
        }
        /// <summary>
        /// 2011.10.27 删除图例
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteLegend_Click(object sender, EventArgs e)
        {
            if (_SelectedNode != null)
            {
                DeleteChartLegend(_SelectedNode);
            }
        }
        /// <summary>
        /// 2011.10.27 删除标题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteTitle_Click(object sender, EventArgs e)
        {
            if (_SelectedNode != null)
            {
                DeleteChartTitle(_SelectedNode);
            }
        }

        #endregion




    }
}
