using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using PMS.Libraries.ToolControls.PMSPublicInfo;

namespace PMS.Libraries.ToolControls.PmsSheet.PmsPublicData
{
    public partial class CustomPageRun : UserControl
    {
        public CustomPageRun()
        {
            InitializeComponent();
        }

        //1,动态查询条件使用
        //0,关联字段使用
        public int IsQuery;
        //当前值列表
        private List<PmsDisplay> _displayList;

        public List<PmsDisplay> DisplayList
        {
            get
            {
                if (RunMode == 1)
                    getNewValue();
                return _displayList;
            }
            set
            {
                _displayList = new List<PmsDisplay>();
                if (null != value)
                {
                    foreach (var pd in value)
                    {
                        PmsDisplay pdNew = new PmsDisplay(pd);
                        _displayList.Add(pdNew);
                    }
                }
                if (RunMode == 1)
                    setNewValue();
            }
        }
        //运行模式，设计时为0，运行时为1
        public int RunMode = 0;
        private PageData _runPageData;

        public PageData RunPageData
        {
            get { return _runPageData; }
            set
            {
                _runPageData = value;
                SetActiveControl();
            }
        }
        //该表表名，用于外键替换显示中，若不需要该功能，此值可以不设，否则必须设置
        private string _tableName;

        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }
        public List<QueryResultObj> QueryResultList
        {
            get
            {
                List<QueryResultObj> qList = new List<QueryResultObj>();
                if (IsQuery == 1)
                {
                    getQueryValue(ref qList);
                }
                return qList;
            }
            set
            {
                if (IsQuery == 1)
                {
                    setQueryValue(value);
                }
            }
        }
        //2012.04.24增加 传递内存连接
        public List<PMSRefDBConnectionObj> Connections
        {
            get;
            set;
        }
        //2012.04.25增加 传递过滤条件数据集
        public DataSet FilterDataSet
        {
            get;
            set;
        }


        private char _charQuotes = '"';
        private char _charComma = ',';
        private string _strTab = "\r\n";
        // 2014.06.05 为报表服务添加
        // Tab控件并转换至html
        public string ConvertToTabHtml()
        {
            //根据this.tabControl1转换出对应html
            //此处为算法实现
            int nCount = this.tabControl1.TabPages.Count;

            StringBuilder sbHeader = new StringBuilder();
            sbHeader.Append(Properties.Resources.StrFilterHeader);

            sbHeader.Append(_strTab);
            sbHeader.Append(@" $(function(){ ");
            sbHeader.Append(_strTab);
            sbHeader.Append("           " + "var cc=new TabControl(");
            string HtmlDivID = this.tabControl1.Name + "DivID";

            sbHeader.Append(_charQuotes + HtmlDivID + _charQuotes + _charComma + "'" + this.tabControl1.Name + "'" + _charComma + nCount.ToString() + _charComma + this.tabControl1.Width.ToString() + _charComma + this.tabControl1.Height.ToString() + ");");
            sbHeader.Append(_strTab);
            for (int i = 0; i < nCount; i++)
            {
                sbHeader.Append("           " + "cc.AddTab(");
                sbHeader.Append("'" + "TabPages" + i.ToString() + "'" + _charComma + "'" + this.tabControl1.TabPages[i].Text + "'" + _charComma);
                string temp = "$(" + _charQuotes + '#' + "TabPages" + i.ToString() + _charQuotes + ")" + ".html()";
                sbHeader.Append(temp);
                if (i == 0)//设置主页，默认0为主页
                {
                    sbHeader.Append(_charComma + "true");
                }
                sbHeader.Append(");");
                sbHeader.Append(_strTab);
            }
            sbHeader.Append("}); " + _strTab);
            sbHeader.Append("</script>" + _strTab);


            string strTemp = "<script type=" + '"' + "text/javascript"+'"' + '>';
            sbHeader.Append(strTemp + _strTab);
            ////////////////////////////2014年8月21日,添加变量
            sbHeader.Append("			" + "var rptID;" + _strTab);
            sbHeader.Append("			" + "var clientID;" + _strTab);
            sbHeader.Append("			" + "var queryID;" + _strTab);
            sbHeader.Append("			" + "var RemotebaseUrl;" + _strTab);

            sbHeader.Append("			" + "var baseUrl;" + _strTab);
            sbHeader.Append("			" + "var UrlParam;" + _strTab);
            ////////////////////////////2014年8月21日,添加变量
            
            ////////////////////////////2014年9月10日,添加心跳函数
            sbHeader.Append("			" + "var heartbeattimer;//当前heartbeat页定时器" + _strTab);

            sbHeader.Append("			" + "function BeginHeartBeat() //函数:间接调用" + _strTab);
            sbHeader.Append("			" + "{" + _strTab);
            sbHeader.Append("			" + "	heartbeattimer=setInterval(DoHeartBeat(), 50000); //启用心跳函数" + _strTab);
            sbHeader.Append("			" + "}" + _strTab);

            sbHeader.Append("			" + "function StopHeartBeat() //函数:停止定时器" + _strTab);
            sbHeader.Append("			" + "{" + _strTab);
            sbHeader.Append("			" + "	clearInterval(heartbeattimer);" + _strTab);
            sbHeader.Append("			" + "}" + _strTab);

            sbHeader.Append("			" + "function DoHeartBeat() //函数:心跳" + _strTab);
            sbHeader.Append("			" + "{" + _strTab);
            sbHeader.Append("			" + "	return function() {HeartBeat();}" + _strTab);
            sbHeader.Append("			" + "}" + _strTab);

            sbHeader.Append("			" + "function HeartBeat()//心跳函数" + _strTab);
            sbHeader.Append("			" + "{" + _strTab);
            sbHeader.Append("			" + "	AjaxToHeartBeat();" + _strTab);
            sbHeader.Append("			" + "}" + _strTab);

            sbHeader.Append("function AjaxToHeartBeat() {" + _strTab);
            string stemp1 = "			" + "var WcfAddress = document.getElementById('WCFServiceAddress').value;" + _strTab;
            string stemp2 = "			" + "var CltID = document.getElementById('CurrentClientID').value;" + _strTab;
            sbHeader.Append(stemp1 + stemp2);
            sbHeader.Append("           " + "var url1=WcfAddress+" + '"' + "/HeartBeat?jsoncallback=?" + '"' + ";" + _strTab);
            sbHeader.Append("			" + " var myData = {" + _strTab);
            sbHeader.Append("			" + "clientID: CltID," + _strTab);
            sbHeader.Append("			" + "};" + _strTab);
            sbHeader.Append("			" + "$.ajax({" + _strTab);
            sbHeader.Append("			" + "type: " + '"' + "POST" + '"' + ',' + _strTab);
            sbHeader.Append("			" + " data: myData," + _strTab);
            sbHeader.Append("			" + " dataType:" + '"' + "json" + '"' + ',' + _strTab);
            sbHeader.Append("			" + " contentType:" + '"' + " application/json;charset=utf-8" + '"' + ',' + _strTab);
            sbHeader.Append("			" + " url:url1" + ',' + _strTab);
           // sbHeader.Append("			" + " processData: true," + _strTab);
            sbHeader.Append("			" + " success: function (data, status) {" + _strTab);
            sbHeader.Append("			" + "}," + _strTab);
            sbHeader.Append("			" + " error: function (xhr) {" + _strTab);
            sbHeader.Append("			" + "  alert(" + '"' + "心跳更新错误" + '"' + ");" + _strTab);
            sbHeader.Append("			" + "}" + _strTab);
            sbHeader.Append("	      " + " });" + _strTab);
            sbHeader.Append("	" + "}" + _strTab);
            ////////////////////////////2014年9月10日,添加心跳函数

            sbHeader.Append("function okBtn()" + _strTab);
            sbHeader.Append("{" + _strTab + "           ");
            sbHeader.Append("var t=" + '"' + '"' + ";" + _strTab);

            for (int i = 0; i < nCount; i++)
            {
                StringBuilder sbtemp = new StringBuilder();
                int mCount = this.tabControl1.TabPages[i].Controls.Count;
                for (int j = 0; j < mCount; j++)
                {

                    Control ctrl = this.tabControl1.TabPages[i].Controls[j];
                    if (ctrl is IControlsToHtml)
                    {
                        sbtemp.Append((ctrl as IControlsToHtml).ControlsToJavascript(i.ToString()));
                    }
                }
                sbHeader.Append(sbtemp.ToString());
            }
            sbHeader.Append("			" + "GetReportWithFilter(rptID, clientID,queryID,t);" + _strTab);//2014年8月21日添加调用web函数
           // sbHeader.Append("			" + " var ext = window.external;" + _strTab);
           // sbHeader.Append("			" + " ext.SubmitFilter(t);" + _strTab);
            sbHeader.Append("}" + _strTab);
            ////////////////////////////2014年8月21日
            sbHeader.Append("function openUri(uri) {" + _strTab);
            sbHeader.Append("			" + "location.href = uri;" + _strTab);
            sbHeader.Append("}" + _strTab);

            sbHeader.Append("function GetReportWithFilter(RptID,CltID,QryID,StrFilter) {" + _strTab);
            sbHeader.Append("			" + "var newQryID=guid();" + _strTab);
            sbHeader.Append("			" + "var WcfAddress = document.getElementById('WCFServiceAddress').value;" + _strTab);//8月26日加入wcf服务
            sbHeader.Append("			" + "baseUrl = RemotebaseUrl" + '+' + '"' + "//LoadingPage2.html" + '"' + ';' + _strTab);
            sbHeader.Append("			" + "UrlParam = " + "'?'" + '+' + "'rptID='" + '+' + "RptID" + '+' + "'&'" + '+' + "'cltID='" + '+' + " CltID" + '+' + "'&'" + '+' + "'qryID='" + '+' + " newQryID" + '+' + "'&'" + '+' + "'wcfID='" + '+' + " WcfAddress;" + _strTab);
            
            sbHeader.Append("           " + "var url2=WcfAddress+" + '"' + "/GetQueryReportWithFilterUrl?jsoncallback=?" + '"' +";"+ _strTab);
            sbHeader.Append("			" + "var myData1 = {" + _strTab);
            sbHeader.Append("			        " + "rptID: RptID," + _strTab);
            sbHeader.Append("			        " + "clientID: CltID," + _strTab);
            sbHeader.Append("			        " + "queryID: QryID," + _strTab);
            sbHeader.Append("			        " + "newQueryID:newQryID," + _strTab);
            sbHeader.Append("			        " + "strFilter:StrFilter," + _strTab);
            sbHeader.Append("			" + "};" + _strTab);
            sbHeader.Append("			" + "$.ajax({" + _strTab);
            sbHeader.Append("			 " + " type: " + '"' + "POST" + '"' + ',' + _strTab);
            sbHeader.Append("			  " + " data: myData1," + _strTab);
            sbHeader.Append("			 " + " dataType:" + '"' + "json" + '"' + ',' + _strTab);
            sbHeader.Append("			 " + " contentType:" + "'" + " application/json;charset=utf-8" + "'" + ',' + _strTab);
            sbHeader.Append("			 " + " url:url2" + ',' + _strTab);
            
           // sbHeader.Append("			 " + " processData: true," + _strTab);
            sbHeader.Append("			 " + " success: function (data, status) {" + _strTab);
            sbHeader.Append("			" + "openUri(baseUrl + UrlParam);" + _strTab);
            sbHeader.Append("			" + "}," + _strTab);
            sbHeader.Append("			" + " error: function (xhr) {" + _strTab);
            sbHeader.Append("			" + "  alert("+'"'+"带过滤条件查询方式失败!"+'"'+");" + _strTab);
            sbHeader.Append("			" + "}" + _strTab);
            sbHeader.Append("	      " + " });" + _strTab);


           
            sbHeader.Append("	" + "}" + _strTab);

            sbHeader.Append("function GetInfo()" + _strTab);
            sbHeader.Append("{" + _strTab);


            sbHeader.Append("			" + "BeginHeartBeat();" + _strTab);
            sbHeader.Append("			" + "rptID = document.getElementById(" + '"' + "CurrentReportID" + '"' + ')' + ".value;" + _strTab);
            sbHeader.Append("			" + "clientID = document.getElementById(" + '"' + "CurrentClientID" + '"' + ')' + ".value;" + _strTab);
            sbHeader.Append("			" + "queryID = document.getElementById(" + '"' + "QueryID" + '"' + ')' + ".value;" + _strTab);
            sbHeader.Append("			" + "RemotebaseUrl = document.getElementById(" + '"' + "CurrentRemotebaseUrl" + '"' + ')' + ".value;" + _strTab);
            sbHeader.Append( "}" + _strTab);
           
            //////////////////////////2014年8月21日

            sbHeader.Append("</script>" + _strTab);
            sbHeader.Append("<title>过滤条件</title>" + _strTab);
            sbHeader.Append("</head>" + _strTab);
            return sbHeader.ToString();
        }

        // 2014.06.06 为报表服务添加
        // 枚举Tab页内控件并转换至html
        public string ConvertToTabContentHtml()
        {
            //根据this.tabControl1转换出对应html
            //此处为算法实现
            string HtmlDivID = this.tabControl1.Name + "DivID";

            StringBuilder sbHeader = new StringBuilder();
            sbHeader.Append("<body "+"onload="+'"'+"GetInfo();"+'"'+">" + _strTab);
            //添加当前页面隐藏的客户端ID和报表ID
            //<input id="CurrentClientID" type="text" value="" style="display:none;">
            string tempStr1 = "<input id=" + '"' + "CurrentClientID" + '"' + " type=" + '"' + "text" + '"' + " value=" + '"' + '"' + " style=" + '"' + "display:none;" + '"' + ">" + _strTab;
            string tempStr2 = "<input id=" + '"' + "CurrentReportID" + '"' + " type=" + '"' + "text" + '"' + " value=" + '"' + '"' + " style=" + '"' + "display:none;" + '"' + ">" + _strTab;
            string tempStr3 = "<input id=" + '"' + "CurrentRemotebaseUrl" + '"' + " type=" + '"' + "text" + '"' + " value=" + '"' + '"' + " style=" + '"' + "display:none;" + '"' + ">" + _strTab;
            string tempStr4 = "<input id=" + '"' + "WCFServiceAddress" + '"' + " type=" + '"' + "text" + '"' + " value=" + '"' + '"' + " style=" + '"' + "display:none;" + '"' + ">" + _strTab;
            string tempStr5 = "<input id=" + '"' + "QueryID" + '"' + " type=" + '"' + "text" + '"' + " value=" + '"' + '"' + " style=" + '"' + "display:none;" + '"' + ">" + _strTab;//2014年10月8日增加
            sbHeader.Append(tempStr1);
            sbHeader.Append(tempStr2);
            sbHeader.Append(tempStr3);
            sbHeader.Append(tempStr4);
            sbHeader.Append(tempStr5);
            //添加当前页面隐藏的客户端ID和报表ID
            sbHeader.Append("<div id=" + _charQuotes + HtmlDivID + _charQuotes + "></div>" + _strTab);

            int nCount = this.tabControl1.TabPages.Count;
            for (int i = 0; i < nCount; i++)
            {
                StringBuilder sbtemp = new StringBuilder();
                sbtemp.Append("	<div style=" + _charQuotes + "display:none" + _charQuotes + " " + "id=" + _charQuotes + "TabPages" + i.ToString() + _charQuotes + ">" + _strTab);
                int mCount = this.tabControl1.TabPages[i].Controls.Count;
                for (int j = 0; j < mCount; j++)
                {

                    Control ctrl = this.tabControl1.TabPages[i].Controls[j];
                    if (ctrl is IControlsToHtml)
                    {
                        sbtemp.Append((ctrl as IControlsToHtml).ControlsToHtml(i.ToString()));
                    }
                }
                sbHeader.Append(sbtemp.ToString() + _strTab);
                sbHeader.Append("</div>" + _strTab);
            }
            sbHeader.Append("<input type=" + '"' + "button" + '"' + " " + "value=" + '"' + "确定" + '"' + " " + "onclick=" + '"' + "okBtn();" + '"' + ">" + _strTab);
            sbHeader.Append("<input type=" + '"' + "button" + '"' + " " + "value=" + '"' + "取消" + '"' + " " + "onclick=" + '"' + "canCelBtn();" + '"' + ">" + _strTab);
            sbHeader.Append("</body>" + _strTab + "</html>" + _strTab);

            return sbHeader.ToString();
        }

        private void SetActiveControl()
        {
            if (_runPageData == null)
                return;
            _runPageData.TableName = _tableName;
            _runPageData.pmsDisplayList = _displayList;
            _runPageData.Connections = Connections;
            _runPageData.FilterDataSet = FilterDataSet;
            _runPageData.populateTab(tabControl1, null, RunMode, false);
            this.Tag = tabControl1.Tag;
            FilterDataSet = _runPageData.FilterDataSet;
        }

        private void CustomPageRun_Load(object sender, EventArgs e)
        {
            if (RunMode == 1)
                setNewValue();
        }
        private void getQueryValue(ref List<QueryResultObj> qList)
        {
            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                IEnumerator enumerator = tabPage.Controls.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    #region 取控件值
                    Control cmp = (Control)enumerator.Current;
                    if (cmp is TextBoxEx)
                    {
                        TextBoxEx pTB = (TextBoxEx)cmp;

                        QueryResultObj qr = new QueryResultObj();
                        qr.UniqueID = pTB.RNode;
                        qr.value = pTB.Text;
                        qList.Add(qr);
                    }
                    else if (cmp is ComboBoxEx)
                    {
                        ComboBoxEx pTB = (ComboBoxEx)cmp;
                        QueryResultObj qr = new QueryResultObj();
                        qr.UniqueID = pTB.RNode;
                        qr.value = pTB.Text;
                        qList.Add(qr);
                    }
                    else if (cmp is CheckBoxEx)
                    {
                        CheckBoxEx pTB = (CheckBoxEx)cmp;
                        QueryResultObj qr = new QueryResultObj();
                        qr.UniqueID = pTB.RNode;
                        qr.value = pTB.Checked;
                        qList.Add(qr);
                    }
                    #endregion
                }
            }
        }
        private void setQueryValue(List<QueryResultObj> qList)
        {
            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                IEnumerator enumerator = tabPage.Controls.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    #region 取控件值
                    Control cmp = (Control)enumerator.Current;
                    if (cmp is TextBoxEx)
                    {
                        TextBoxEx pTB = (TextBoxEx)cmp;

                        foreach (QueryResultObj qr in qList)
                        {
                            if (qr.UniqueID == pTB.RNode)
                            {
                                pTB.Text = qr.value.ToString();
                                break;
                            }
                        }
                    }
                    else if (cmp is ComboBoxEx)
                    {
                        ComboBoxEx pTB = (ComboBoxEx)cmp;
                        foreach (QueryResultObj qr in qList)
                        {
                            if (qr.UniqueID == pTB.RNode)
                            {
                                pTB.Text = qr.value.ToString();
                                break;
                            }
                        }
                        if (pTB.Items.Count > 0)
                            pTB.SelectedIndex = 0;
                    }
                    else if (cmp is CheckBoxEx)
                    {
                        CheckBoxEx pTB = (CheckBoxEx)cmp;
                        foreach (QueryResultObj qr in qList)
                        {
                            if (qr.UniqueID == pTB.RNode)
                            {
                                try
                                {
                                    pTB.Checked = Convert.ToBoolean(qr.value);
                                }
                                catch
                                {
                                    pTB.Checked = false;
                                }

                                break;
                            }
                        }
                    }
                    #endregion
                }
            }
        }
        private void getNewValue()
        {
            if (_displayList == null)
                return;
            for (int i = 0; i < _displayList.Count; i++)
            {
                _displayList[i].bInEditList = false;

            }
            for (int i = 0; i < _displayList.Count; i++)
            {
                PmsDisplay pf = _displayList[i];
                bool bFind = false;
                foreach (TabPage tabPage in tabControl1.TabPages)
                {
                    IEnumerator enumerator = tabPage.Controls.GetEnumerator();

                    while (enumerator.MoveNext())
                    {
                        #region 取控件值
                        Control cmp = (Control)enumerator.Current;
                        if (cmp is TextBoxEx)
                        {
                            TextBoxEx pTB = (TextBoxEx)cmp;
                            if (pf.fieldName == pTB.RField)
                            {
                                pf.fieldValue = pTB.Text;
                                bFind = true;
                                break;
                            }
                        }
                        else if (cmp is FileDisplay)
                        {
                            FileDisplay pTB = (FileDisplay)cmp;
                            if (pf.fieldName == pTB.RField)
                            {
                                pf.fieldValue = pTB.Url;
                                bFind = true;
                                break;
                            }
                        }
                        else if (cmp is ComboBoxEx)
                        {
                            ComboBoxEx pTB = (ComboBoxEx)cmp;
                            if (pf.fieldName == pTB.RField)
                            {
                                string strValue1 = "";
                                int nPos = pTB.Text.IndexOf(" -> ");
                                if (nPos < 0)
                                {
                                    strValue1 = pTB.Text;
                                }
                                else
                                    strValue1 = pTB.Text.Substring(0, nPos);

                                pf.fieldValue = strValue1;
                                bFind = true;
                                break;
                            }
                        }
                        else if (cmp is ForeignKeyCtrlEx)
                        {
                            ForeignKeyCtrlEx pTB = (ForeignKeyCtrlEx)cmp;

                            if (pf.fieldName == pTB.RField)
                            {
                                pf.fieldValue = pTB.Tag;
                                bFind = true;
                                break;
                            }
                        }
                        else if (cmp is CheckBoxEx)
                        {
                            CheckBoxEx pTB = (CheckBoxEx)cmp;
                            if (pf.fieldName == pTB.RField)
                            {
                                string strValue1 = string.Format("{0}", pTB.Checked);
                                pf.fieldValue = strValue1;
                                bFind = true;
                                break;
                            }
                        }
                        else if (cmp is RadioButtonEx)
                        {
                            RadioButtonEx pTB = (RadioButtonEx)cmp;
                            if (pf.fieldName == pTB.RField)
                            {
                                string strValue1 = string.Format("{0}", pTB.Checked);
                                pf.fieldValue = strValue1;
                                bFind = true;
                                break;
                            }
                        }
                        else if (cmp is GroupBoxEx)
                        {
                            GroupBoxEx pTB = (GroupBoxEx)cmp;
                            if (pTB.Controls != null)
                            {
                                GetChildrenValue(pTB.Controls);
                            }
                        }
                        else if (cmp is NumericUpDownEx)
                        {
                            NumericUpDownEx pTB = (NumericUpDownEx)cmp;
                            if (pf.fieldName == pTB.RField)
                            {
                                pf.fieldValue = pTB.Value;
                                bFind = true;
                                break;
                            }
                        }
                        #endregion
                    }
                    if (bFind == true)
                    {
                        pf.bInEditList = true;
                        break;
                    }
                }

            }
        }
        /// <summary>
        /// 2012.1.31 增加
        /// 目的:增加了成组控件后需要递归获取其子控件的值
        /// </summary>
        /// <param name="Aim">子控件集合</param>
        /// <returns>返回处理结果</returns>
        private int GetChildrenValue(Control.ControlCollection Aim)
        {
            int result = 0;
            if (Aim == null)
            {
                return result;
            }
            if (_displayList == null)
                return result;
            for (int i = 0; i < _displayList.Count; i++)
            {
                _displayList[i].bInEditList = false;

            }
            for (int i = 0; i < _displayList.Count; i++)
            {
                PmsDisplay pf = _displayList[i];
                bool bFind = false;
                foreach (Control cmp in Aim)
                {
                    if (cmp is TextBoxEx)
                    {
                        TextBoxEx pTB = (TextBoxEx)cmp;
                        if (pf.fieldName == pTB.RField)
                        {
                            pf.fieldValue = pTB.Text;
                            bFind = true;
                        }
                    }
                    else if (cmp is FileDisplay)
                    {
                        FileDisplay pTB = (FileDisplay)cmp;
                        if (pf.fieldName == pTB.RField)
                        {
                            pf.fieldValue = pTB.Url;
                            bFind = true;
                        }
                    }
                    else if (cmp is ComboBoxEx)
                    {
                        ComboBoxEx pTB = (ComboBoxEx)cmp;
                        if (pf.fieldName == pTB.RField)
                        {
                            string strValue1 = "";
                            int nPos = pTB.Text.IndexOf(" -> ");
                            if (nPos < 0)
                            {
                                strValue1 = pTB.Text;
                            }
                            else
                                strValue1 = pTB.Text.Substring(0, nPos);

                            pf.fieldValue = strValue1;
                            bFind = true;
                        }
                    }
                    else if (cmp is ForeignKeyCtrlEx)
                    {
                        ForeignKeyCtrlEx pTB = (ForeignKeyCtrlEx)cmp;

                        if (pf.fieldName == pTB.RField)
                        {
                            pf.fieldValue = pTB.Tag;
                            bFind = true;
                        }
                    }
                    else if (cmp is CheckBoxEx)
                    {
                        CheckBoxEx pTB = (CheckBoxEx)cmp;
                        if (pf.fieldName == pTB.RField)
                        {
                            string strValue1 = string.Format("{0}", pTB.Checked);
                            pf.fieldValue = strValue1;
                            bFind = true;
                        }
                    }
                    else if (cmp is RadioButtonEx)
                    {
                        RadioButtonEx pTB = (RadioButtonEx)cmp;
                        if (pf.fieldName == pTB.RField)
                        {
                            string strValue1 = string.Format("{0}", pTB.Checked);
                            pf.fieldValue = strValue1;
                            bFind = true;
                        }
                    }
                    else if (cmp is GroupBoxEx)
                    {
                        GroupBoxEx pTB = (GroupBoxEx)cmp;
                        if (pTB.Controls != null)
                        {
                            GetChildrenValue(pTB.Controls);
                        }
                    }
                    else if (cmp is NumericUpDownEx)
                    {
                        NumericUpDownEx pTB = (NumericUpDownEx)cmp;
                        if (pf.fieldName == pTB.RField)
                        {
                            pf.fieldValue = pTB.Value;
                            bFind = true;
                            break;
                        }
                    }
                }
                if (bFind == true)
                {
                    pf.bInEditList = true;
                }

            }
            return result;
        }

        private void DownFile()
        {
            if (_displayList == null)
                return;
            for (int i = 0; i < _displayList.Count; i++)
            {
                PmsDisplay pd = _displayList[i];

                if (pd.fieldType == "IMAGE" && pd.fieldValue == null) //下载文件，每行只下载一次
                {
                    try
                    {
                        byte[] barrImage = (byte[])pd.fieldValue;
                        if (barrImage.Length <= 8)//空文件
                            continue;
                        int iFileType = 8;
                        byte[] barrType = new byte[iFileType];
                        byte[] barrData = new byte[barrImage.Length - iFileType];

                        Array.Copy(barrImage, 0, barrType, 0, iFileType);
                        Array.Copy(barrImage, iFileType, barrData, 0, barrImage.Length - iFileType);

                        string fileType = Encoding.ASCII.GetString(barrType);
                        fileType.Trim('\0');
                        int ips = fileType.IndexOf('\0');
                        if (ips >= 0)
                            fileType = fileType.Substring(0, ips);
                        MemoryStream ms = new MemoryStream(barrData);
                        Random rdom = new Random();
                        string filename = System.Windows.Forms.Application.StartupPath + "\\temp\\f" + rdom.Next(1000).ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                        if (fileType.Length > 0)
                            filename += "." + fileType;

                        FileStream stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Read | FileShare.Write);
                        stream.SetLength(barrData.LongLength);
                        stream.Write(barrData, 0, barrData.Length);  //将二进制文件写到指定目录
                        stream.Close();
                        pd.fileName = filename;
                    }
                    catch (Exception fileEx)
                    {
                        string info = string.Format("下载文件失败：{0}", fileEx.Message);
                        PMS.Libraries.ToolControls.PMSPublicInfo.Message.Warnning(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, info, false);
                    }
                    _displayList[i] = pd;
                }
            }
        }

        private void setNewValue()
        {
            if (_displayList == null)
                return;

            DownFile();
            try
            {
                foreach (TabPage tabPage in tabControl1.TabPages)
                {
                    foreach (Control cmp in tabPage.Controls)
                    {
                        #region textbox
                        if (cmp is TextBoxEx)
                        {
                            TextBoxEx pTB = (TextBoxEx)cmp;
                            for (int i = 0; i < _displayList.Count; i++)
                            {
                                PmsDisplay pd = _displayList[i];
                                if (pd.fieldName == pTB.RField)
                                {
                                    if (pd.fieldValue != null)
                                        pTB.Text = pd.fieldValue.ToString();

                                    break;
                                }
                            }
                        }
                        else if (cmp is ComboBoxEx)
                        {
                            ComboBoxEx pTB = (ComboBoxEx)cmp;
                            for (int i = 0; i < _displayList.Count; i++)
                            {
                                PmsDisplay pd = _displayList[i];//ElementAt(i);
                                if (pd.fieldName == pTB.RField)
                                {
                                    string strValue = "";
                                    if (pd.fieldValue != null)
                                        strValue = pd.fieldValue.ToString();
                                    for (int iPos = 0; iPos < pTB.Items.Count; iPos++)
                                    {
                                        string strItem = (string)pTB.Items[iPos];
                                        int nPos = strItem.IndexOf(" -> ");
                                        if (nPos < 0)
                                        {
                                            pTB.Text = strValue;
                                            goto ComboBoxEnd;
                                        }
                                        strItem = strItem.Substring(0, nPos);
                                        if (strItem == strValue)
                                        {
                                            pTB.SelectedIndex = iPos;
                                            goto ComboBoxEnd;
                                        }
                                    }
                                    pTB.Text = strValue;
                                    break;
                                }
                            }
                        }
                        else if (cmp is ForeignKeyCtrlEx)
                        {
                            ForeignKeyCtrlEx pTB = (ForeignKeyCtrlEx)cmp;
                            for (int i = 0; i < _displayList.Count; i++)
                            {
                                PmsDisplay pd = _displayList[i];//ElementAt(i);
                                if (pd.fieldName == pTB.RField)
                                {
                                    if (this._runPageData.BReplace)
                                    {
                                        pTB.Tag = pd.fieldValue1;
                                    }
                                    else
                                    {
                                        pTB.Tag = pd.fieldValue;
                                    }

                                    if (pd.fieldValue != null)
                                        pTB.Text = pd.fieldValue.ToString();

                                    pTB.tag1 = pd.fieldValue2;
                                    break;
                                }
                            }
                        }
                        else if (cmp is CheckBoxEx)
                        {
                            CheckBoxEx pTB = (CheckBoxEx)cmp;
                            for (int i = 0; i < _displayList.Count; i++)
                            {
                                PmsDisplay pd = _displayList[i];//ElementAt(i);
                                if (pd.fieldName == pTB.RField)
                                {
                                    try
                                    {
                                        pTB.Checked = Convert.ToBoolean(pd.fieldValue);
                                    }
                                    catch
                                    {
                                        pTB.Checked = false;
                                    }
                                    break;
                                }
                            }
                        }
                        else if (cmp is RadioButtonEx)
                        {
                            RadioButtonEx pTB = (RadioButtonEx)cmp;
                            for (int i = 0; i < _displayList.Count; i++)
                            {
                                PmsDisplay pd = _displayList[i];//ElementAt(i);
                                if (pd.fieldName == pTB.RField)
                                {
                                    try
                                    {
                                        pTB.Checked = Convert.ToBoolean(pd.fieldValue);
                                    }
                                    catch
                                    {
                                        pTB.Checked = false;
                                    }
                                    break;
                                }
                            }
                        }
                        else if (cmp is GroupBoxEx)
                        {
                            GroupBoxEx pTB = (GroupBoxEx)cmp;
                            if (pTB.Controls != null)
                            {
                                SetChildrenValue(pTB.Controls);
                            }
                        }
                        else if (cmp is NumericUpDownEx)
                        {
                            NumericUpDownEx pTB = (NumericUpDownEx)cmp;
                            for (int i = 0; i < _displayList.Count; i++)
                            {
                                PmsDisplay pd = _displayList[i];
                                if (pd.fieldName == pTB.RField)
                                {
                                    if (pd.fieldValue != null)
                                    {
                                        try
                                        {
                                            pTB.Value = Convert.ToDecimal(pd.fieldValue);
                                        }
                                        catch (System.Exception ex)
                                        {
                                            PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(ex.Message);
                                        }
                                    }

                                    break;
                                }
                            }
                        }
                        else if (cmp is FileDisplay)
                        {
                            FileDisplay pTB = (FileDisplay)cmp;
                            for (int i = 0; i < _displayList.Count; i++)
                            {
                                PmsDisplay pd = _displayList[i];//ElementAt(i);
                                if (pd.fieldName == pTB.RField)
                                {
                                    pTB.Url = pd.fileName;
                                }
                            }
                        }
                    ComboBoxEnd:
                        cmp.Invalidate();
                        if (cmp is ComboBoxEx)
                        {
                            ComboBoxEx pTB = (ComboBoxEx)cmp;
                            if (pTB.Items.Count > 0 && pTB.SelectedIndex < 0)
                                pTB.SelectedIndex = 0;
                        }
                        #endregion
                    }
                }
            }
            catch (Exception e)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, "设置数据异常:" + e.Message.ToString() + "  " + e.GetBaseException().ToString(), true);
            }
        }
        /// <summary>
        /// 2012.1.31 增加
        /// 目的:增加了成组控件后需要递归给其子控件的赋值
        /// </summary>
        /// <param name="Aim">子控件集合</param>
        /// <returns>返回处理结果</returns>
        private int SetChildrenValue(Control.ControlCollection Aim)
        {
            int result = 0;
            if (Aim == null)
            {
                return result;
            }
            foreach (Control cmp in Aim)
            {
                if (cmp is TextBoxEx)
                {
                    TextBoxEx pTB = (TextBoxEx)cmp;
                    for (int i = 0; i < _displayList.Count; i++)
                    {
                        PmsDisplay pd = _displayList[i];
                        if (pd.fieldName == pTB.RField)
                        {
                            if (pd.fieldValue != null)
                                pTB.Text = pd.fieldValue.ToString();

                            break;
                        }
                    }
                }
                else if (cmp is ComboBoxEx)
                {
                    ComboBoxEx pTB = (ComboBoxEx)cmp;
                    for (int i = 0; i < _displayList.Count; i++)
                    {
                        PmsDisplay pd = _displayList[i];//ElementAt(i);
                        if (pd.fieldName == pTB.RField)
                        {
                            string strValue = "";
                            if (pd.fieldValue != null)
                                strValue = pd.fieldValue.ToString();
                            for (int iPos = 0; iPos < pTB.Items.Count; iPos++)
                            {
                                string strItem = (string)pTB.Items[iPos];
                                int nPos = strItem.IndexOf(" -> ");
                                if (nPos < 0)
                                {
                                    pTB.Text = strValue;
                                    goto ComboBoxEnd;
                                }
                                strItem = strItem.Substring(0, nPos);
                                if (strItem == strValue)
                                {
                                    pTB.SelectedIndex = iPos;
                                    goto ComboBoxEnd;
                                }
                            }
                            pTB.Text = strValue;
                            break;
                        }
                    }
                }
                else if (cmp is ForeignKeyCtrlEx)
                {
                    ForeignKeyCtrlEx pTB = (ForeignKeyCtrlEx)cmp;
                    for (int i = 0; i < _displayList.Count; i++)
                    {
                        PmsDisplay pd = _displayList[i];//ElementAt(i);
                        if (pd.fieldName == pTB.RField)
                        {
                            if (this._runPageData.BReplace)
                            {
                                pTB.Tag = pd.fieldValue1;
                            }
                            else
                            {
                                pTB.Tag = pd.fieldValue;
                            }

                            if (pd.fieldValue != null)
                                pTB.Text = pd.fieldValue.ToString();

                            pTB.tag1 = pd.fieldValue2;
                            break;
                        }
                    }
                }
                else if (cmp is CheckBoxEx)
                {
                    CheckBoxEx pTB = (CheckBoxEx)cmp;
                    for (int i = 0; i < _displayList.Count; i++)
                    {
                        PmsDisplay pd = _displayList[i];//ElementAt(i);
                        if (pd.fieldName == pTB.RField)
                        {
                            try
                            {
                                pTB.Checked = Convert.ToBoolean(pd.fieldValue);
                            }
                            catch
                            {
                                pTB.Checked = false;
                            }
                            break;
                        }
                    }
                }
                else if (cmp is RadioButtonEx)
                {
                    RadioButtonEx pTB = (RadioButtonEx)cmp;
                    for (int i = 0; i < _displayList.Count; i++)
                    {
                        PmsDisplay pd = _displayList[i];//ElementAt(i);
                        if (pd.fieldName == pTB.RField)
                        {
                            try
                            {
                                pTB.Checked = Convert.ToBoolean(pd.fieldValue);
                            }
                            catch
                            {
                                pTB.Checked = false;
                            }
                            break;
                        }
                    }
                }
                else if (cmp is GroupBoxEx)
                {
                    GroupBoxEx pTB = (GroupBoxEx)cmp;
                    if (pTB.Controls != null)
                    {
                        SetChildrenValue(pTB.Controls);
                    }
                }
                if (cmp is NumericUpDownEx)
                {
                    NumericUpDownEx pTB = (NumericUpDownEx)cmp;
                    for (int i = 0; i < _displayList.Count; i++)
                    {
                        PmsDisplay pd = _displayList[i];
                        if (pd.fieldName == pTB.RField)
                        {
                            if (pd.fieldValue != null)
                            {
                                try
                                {
                                    pTB.Value = Convert.ToDecimal(pd.fieldValue);
                                }
                                catch (System.Exception ex)
                                {
                                    PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(ex.Message);
                                }
                            }

                            break;
                        }
                    }
                }
                else if (cmp is FileDisplay)
                {
                    FileDisplay pTB = (FileDisplay)cmp;
                    for (int i = 0; i < _displayList.Count; i++)
                    {
                        PmsDisplay pd = _displayList[i];//ElementAt(i);
                        if (pd.fieldName == pTB.RField)
                        {
                            pTB.Url = pd.fileName;
                        }
                    }
                }
            ComboBoxEnd:
                cmp.Invalidate();

                if (cmp is ComboBoxEx)
                {
                    ComboBoxEx pTB = (ComboBoxEx)cmp;
                    if (pTB.Items.Count > 0 && pTB.SelectedIndex < 0)
                        pTB.SelectedIndex = 0;
                }
            }
            return result;
        }

        public void setForeignGuid(string field, DataRow queryValue)
        {
            try
            {
                foreach (TabPage tabPage in tabControl1.TabPages)
                {
                    IEnumerator enumerator = tabPage.Controls.GetEnumerator();

                    while (enumerator.MoveNext())
                    {
                        #region 赋值
                        Control cmp = (Control)enumerator.Current;
                        if (cmp is ForeignKeyCtrlEx)
                        {
                            ForeignKeyCtrlEx pTB = (ForeignKeyCtrlEx)cmp;
                            if (field == pTB.RField)
                            {
                                if (this._runPageData.BReplace)
                                {
                                    pTB.Tag = queryValue["MAPID"].ToString();
                                    pTB.Text = queryValue["Name"].ToString();
                                }
                                else
                                {
                                    pTB.Text = queryValue["MAPID"].ToString();
                                    pTB.Tag = queryValue["MAPID"].ToString();
                                }
                            }
                        }
                        #endregion
                    }
                }
            }
            catch
            {
            }
        }
    }
}
