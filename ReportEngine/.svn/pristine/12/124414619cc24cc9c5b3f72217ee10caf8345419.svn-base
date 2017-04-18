using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.Reflection;
using System.Collections;
using System.Data;
using System.IO;

namespace PMS.Libraries.ToolControls.PmsSheet.PmsPublicData
{
    /// <summary>
    /// 2014.6.6增加
    /// TableCtrols控件转换Html控件工具
    /// </summary>
    public class TableCtrolsConvert
    {

    }
    /// <summary>
    /// 2013.1.8 增加
    /// 目的:为工具栏上增加一个数字控件
    /// </summary>
    public class NumericUpDownEx : NumericUpDown, IControlsToHtml
    {
        public NumericUpDownEx()
        {
            this._rField = "";
        }
        private string _rField;
        public string RField
        {
            get { return this._rField; }
            set
            {
                this._rField = value;
            }
        }

        public Guid RNode;//被绑定的节点id，在查询条件中配置

        private List<PmsField> _RelationFields;
        public List<PmsField> RelationFields
        {
            get { return this._RelationFields; }
            set
            {
                this._RelationFields = value;
            }
        }
        private string _ctrlType;

        public string CtrlType
        {
            get { return _ctrlType; }
            set { _ctrlType = value; }
        }

        private bool _AutoShowScreenKeyboard = false;
        public bool AutoShowScreenKeyboard
        {
            get
            {
                return _AutoShowScreenKeyboard;
            }
            set
            {
                _AutoShowScreenKeyboard = value;
            }
        }
        /// <summary>
        /// 将NumericUpDownEx控件转换成Html文本
        /// </summary>
        /// <returns></returns>
        public string ControlsToHtml(string TableIndex)
        {
            StringBuilder strBlder = new StringBuilder();
            strBlder.Append("<fieldset style=");
            strBlder.Append('"');
            strBlder.Append("position:absolute;");
            strBlder.Append("left:" + this.Location.X.ToString() + "px;");
            strBlder.Append("top:" + this.Location.Y.ToString() + "px;");
            strBlder.Append("height:" + this.Height.ToString() + "px;");
            strBlder.Append("width:" + this.Width.ToString() + "px;");
            strBlder.Append('"');
            strBlder.Append("> <");

            strBlder.Append("legend>" + this.Text + "</legend>");
            strBlder.Append("</fieldset>" + "s/n");
            return strBlder.ToString();
        }
        
        public string ControlsToJavascript(string TableID)
        {
            StringBuilder sb = new StringBuilder();
            if (this._rField != null)
            {

            }
            return sb.ToString();
        }
    }

    /// <summary>
    /// 2012.1.30 增加
    /// 目的:为工具栏上增加一个成组控件
    /// </summary>
    public class GroupBoxEx : GroupBox, IControlsToHtml
    {
        public string ControlsToJavascript(string TableID)
        {
            StringBuilder sb = new StringBuilder();
            this.Name = "GroupBoxEx-" + System.Guid.NewGuid().ToString();

            int nCount = this.Controls.Count;
            for (int i = 0; i < nCount; i++)
            {
                StringBuilder sbtemp = new StringBuilder();
                if (this.Controls[i] is IControlsToHtml)
                {
                    sbtemp.Append((this.Controls[i] as IControlsToHtml).ControlsToJavascript(TableID));
                }
                sb.Append(sbtemp.ToString());
            }
       //     if (this != null)
//             {
// 
//             }
            return sb.ToString();
        }
        private string _strTab = "\r\n";
        /// <summary>
        /// 将GroupBoxEx控件转换成Html文本
        /// 注;结尾没有添加</fieldset>，需要在外层添加
        /// </summary>
        /// <returns></returns>
        public string ControlsToHtml( string TableIndex)
        {
            StringBuilder strBlder = new StringBuilder();
            strBlder.Append(" 			");
            strBlder.Append("<fieldset style=");
            strBlder.Append('"');
            strBlder.Append("position:absolute;");
            strBlder.Append("left:" + this.Location.X.ToString() + "px;");
            strBlder.Append("top:" + this.Location.Y.ToString() + "px;");
            strBlder.Append("height:" + this.Height.ToString() + "px;");
            strBlder.Append("width:" + this.Width.ToString() + "px;");
            strBlder.Append('"');
            strBlder.Append("> <");
       
            strBlder.Append("legend>" + this.Text + "</legend>");
            strBlder.Append("\r\n");
            int nCount = this.Controls.Count;
            for (int i = 0; i < nCount;i++ )
            {
                StringBuilder sbtemp = new StringBuilder();
                if (this.Controls[i] is IControlsToHtml)
                {
                    sbtemp.Append((this.Controls[i] as IControlsToHtml).ControlsToHtml(TableIndex));
                }
                strBlder.Append(sbtemp.ToString());
            }
            
            strBlder.Append(" 			"+"</fieldset>" + _strTab);
            return strBlder.ToString();
        }
    }

    /// <summary>
    /// 2012.1.30 增加
    /// 目的:为工具栏上增加一个单选按钮
    /// </summary>
    public class RadioButtonEx : RadioButton,IControlsToHtml
    {
        private string _rField;
        public RadioButtonEx()
        {
            this._rField = "";
        }
        public string RField
        {
            get { return this._rField; }
            set
            {
                this._rField = value;
            }
        }
        public Guid RNode;//被绑定的节点id，在查询条件中配置

        private List<PmsField> _RelationFields;
        public List<PmsField> RelationFields
        {
            get { return this._RelationFields; }
            set
            {
                this._RelationFields = value;
            }
        }
        private string _ctrlType;

        public string CtrlType
        {
            get { return _ctrlType; }
            set { _ctrlType = value; }
        }
        /// <summary>
        /// 将RadioButtonEx控件转换成Html文本
        /// </summary>
        /// <returns></returns>
        public string ControlsToHtml(string TableIndex)
        {
            StringBuilder strBlder = new StringBuilder();
            strBlder.Append(" 			");
            strBlder.Append("<div style=");
            strBlder.Append('"');
            strBlder.Append("position:absolute;");
            strBlder.Append("left:" + this.Location.X.ToString() + "px;");
            strBlder.Append("top:" + this.Location.Y.ToString() + "px;");
            strBlder.Append("height:" + this.Height.ToString() + "px;");
            strBlder.Append("width:" + this.Width.ToString() + "px;");
            strBlder.Append('"');
            strBlder.Append("> <");
           // strBlder.Append("input id=" + '"' + this.Name + '"' + " " + "name=" +'"'+ "radio" + TableIndex + '"'+" " + "type=" + '"' + "radio" + '"' + '/' + '>');

            strBlder.Append("input id=" + '"' + this.Name + '"' + " ");//ID
            strBlder.Append("name=" + '"' + "radio" + TableIndex + '"' + " ");//Name
            strBlder.Append("type=" + '"' + "radio" + '"' + " ");//Type
            string strBing = "";
            if (this._rField != null)
            {
                strBing = this._rField;
            }
            strBlder.Append("binding=" + '"' + strBing + '"' + " ");//binding
            strBlder.Append("value=" + '"' + "true" + '"' + " ");//Value
            strBlder.Append( '>');
            strBlder.Append("</div>");
            strBlder.Append("\r\n");
            return strBlder.ToString();
        }

        /// <summary>
        /// 将RadioButtonEx控件转换成Javascript文本
        /// </summary>
        /// <returns></returns>
        public string ControlsToJavascript(string TableID)
        {
            StringBuilder sb = new StringBuilder();
            this.Name = "RadioButtonEx-" + System.Guid.NewGuid().ToString(); ;
            if (this._rField != "")
            {

                sb.Append("           ");
                sb.Append("t+=");
                sb.Append("document.getElementById(" + '"' + this.Name + '"' + ").attributes[" + '"' + "binding" + '"' + "].nodeValue" + "+" + '"' + "=" + '"' + "+");
                sb.Append("document.getElementById(" + '"' + this.Name + '"' + ").checcked+" + '"' + ";" + '"' + ";");
                sb.Append("\r\n");
            }
            return sb.ToString();
        }
    }

    public class LabelEx : Label, IControlsToHtml
    {
        public LabelEx()
        {
            base.Text = "Label";
            //this.TextAlign = ContentAlignment.BottomRight;            
        }
        private string _ctrlType;

        public string CtrlType
        {
            get { return _ctrlType; }
            set { _ctrlType = value; }
        }
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }
        /// <summary>
        /// 将lable控件转换成Html文本
        /// </summary>
        /// <returns></returns>
        public string ControlsToHtml( string TableIndex)
        {
            StringBuilder strBlder = new StringBuilder();
            strBlder.Append(" 			");
            strBlder.Append("<div style=");
            strBlder.Append('"');
            strBlder.Append("position:absolute;");
            strBlder.Append("left:" + this.Location.X.ToString() + "px;");
            strBlder.Append("top:" + this.Location.Y .ToString() + "px;");
            strBlder.Append("height:" + this.Height.ToString() + "px;");
            strBlder.Append("width:" + this.Width.ToString() + "px;");
            strBlder.Append('"');
            strBlder.Append("> <");

            strBlder.Append("label >" + this.Text + "</label>");
            strBlder.Append("</div>");
            strBlder.Append("\r\n");
            return strBlder.ToString();
        }

        /// <summary>
        /// 将lable控件转换成Javascript文本
        /// </summary>
        /// <returns></returns>
        public string ControlsToJavascript(string TableID)
        {
            StringBuilder sb = new StringBuilder();
            this.Name = "label-" + System.Guid.NewGuid().ToString(); ;
//             if (this._rField != null)
//             {
// 
//             }
            return sb.ToString();
        }
    }

    /// <summary>
    /// 文本框控件,增加关联字段属性
    /// </summary>
    public class TextBoxEx : TextBox,IControlsToHtml
    {
        public TextBoxEx()
        {
            this._rField = "";
            _RelationFields = new List<PmsField>();
            TableName = "";
            base.KeyPress += new KeyPressEventHandler(TextBoxEx_KeyPress);
        }

        void TextBoxEx_KeyPress(object sender, KeyPressEventArgs e)
        {
            //用户表密码字段输入不超过12
            if (TableName!=null&&TableName.Equals("s_UserInfo", StringComparison.CurrentCultureIgnoreCase) &&
                _rField.Equals("Pass", StringComparison.CurrentCultureIgnoreCase))
            {
                if (this.SelectionLength == 0)//没有选择的时候,输入
                {
                    if (this.Text.Length >= 12)
                    {
                        e.Handled = true;
                    }
                }                
            }
            if (PmsField.textBoxKeyPress(this.Text, RType, this, e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public string RType;
        //判断是否用户表，显示密码长度
        public string TableName;
        private string _rField;
        public string RField
        {
            get { return this._rField; }
            set
            {
                this._rField = value;
            }
        }

        public Guid RNode;//被绑定的节点id，在查询条件中配置

        private List<PmsField> _RelationFields;
        public List<PmsField> RelationFields
        {
            get { return this._RelationFields; }
            set
            {
                this._RelationFields = value;
            }
        }
        private string _ctrlType;

        public string CtrlType
        {
            get { return _ctrlType; }
            set { _ctrlType = value; }
        }

        private bool _AutoShowScreenKeyboard = false;
        public bool AutoShowScreenKeyboard
        {
            get
            {
                return _AutoShowScreenKeyboard;
            }
            set
            {
                _AutoShowScreenKeyboard = value;
            }
        }

        /// <summary>
        /// 将TextBox控件转换成Html文本
        /// </summary>
        /// <returns></returns>
        public string ControlsToHtml( string TableIndex)
        {
            StringBuilder strBlder = new StringBuilder();
            strBlder.Append(" 			");
            strBlder.Append("<div ");
           
            strBlder.Append("> <");
           // strBlder.Append("input id=" + '"'+ this.Name+ '"'+" " + "type=" + '"' + "text" + '"' + '/' + '>');

            strBlder.Append("input id=" + '"' + this.Name + '"' + " ");//ID
            strBlder.Append("name=" + '"' + "" + '"' + " ");//Name
            strBlder.Append("type=" + '"' + "text" + '"' + " ");//Type
            string strBinding = "";
            if (this._rField != null)
            {
                strBinding = this._rField;
            }
            strBlder.Append("binding=" + '"' + strBinding + '"' + " ");//binding
            strBlder.Append("value=" + '"' + this .Text+ '"');//Value
            strBlder.Append("style=");
            strBlder.Append('"');
            strBlder.Append("position:absolute;");
            strBlder.Append("left:" + this.Location.X.ToString() + "px;");
            strBlder.Append("top:" + this.Location.Y.ToString() + "px;");
            strBlder.Append("height:" + this.Height.ToString() + "px;");
            strBlder.Append("width:" + this.Width.ToString() + "px;");
            strBlder.Append('"');
            strBlder.Append('>');
            strBlder.Append("</div>");
            strBlder.Append("\r\n");
            return strBlder.ToString();
        }

        /// <summary>
        /// 将TextBox控件转换成Javascript文本
        /// </summary>
        /// <returns></returns>
        public string ControlsToJavascript(string TableID)
        {
            StringBuilder sb = new StringBuilder();
            this.Name = "TextBoxEx-" + System.Guid.NewGuid().ToString();
            if (this._rField != "")
            {
                sb.Append("           ");
                sb.Append("t+=");
                sb.Append("document.getElementById(" + '"' + this.Name + '"' + ").attributes[" + '"' + "binding" + '"' + "].nodeValue" + "+" + '"' + "=" + '"' + "+");
                sb.Append("document.getElementById(" + '"' + this.Name + '"' + ").value+" + '"' + ";" + '"' + ";");
                sb.Append("\r\n");
            }
            return sb.ToString();
        }
    }

    /// <summary>
    /// 组合框控件,增加关联字段属性
    /// </summary>
    public class ComboBoxEx : ComboBox, IControlsToHtml
    {
        private string _rField;
        public ComboBoxEx()
        {
            this._rField = "";
            _RelationFields = new List<PmsField>();
        }

        public string RField
        {
            get { return this._rField; }
            set
            {
                this._rField = value;
            }
        }
        public Guid RNode;//被绑定的节点id，在查询条件中配置

        private List<PmsField> _RelationFields;
        public List<PmsField> RelationFields
        {
            get { return this._RelationFields; }
            set
            {
                this._RelationFields = value;
            }
        }
        private string _ctrlType;

        public string CtrlType
        {
            get { return _ctrlType; }
            set { _ctrlType = value; }
        }

        private bool _AutoShowScreenKeyboard = false;
        public bool AutoShowScreenKeyboard
        {
            get
            {
                return _AutoShowScreenKeyboard;
            }
            set
            {
                _AutoShowScreenKeyboard = value;
            }
        }
        /// <summary>
        /// 将ComboBoxEx控件转换成Html文本
        /// </summary>
        /// <returns></returns>
        public string ControlsToHtml(string TableIndex)
        {
            StringBuilder strBlder = new StringBuilder();
            strBlder.Append(" 			");
            strBlder.Append("<select style=");
            strBlder.Append('"');
            strBlder.Append("position:absolute;");
            strBlder.Append("left:" + this.Location.X.ToString() + "px;");
            strBlder.Append("top:" + this.Location.Y.ToString() + "px;");
            strBlder.Append("height:" + this.Height.ToString() + "px;");
            strBlder.Append("width:" + this.Width.ToString() + "px;");
            strBlder.Append('"');
             // strBlder.Append("input id=" + '"'+ this.Name+ '"'+" " + "type=" + '"' + "text" + '"' + '/' + '>');

            strBlder.Append(" id=" + '"' + this.Name + '"' + " ");//ID
            strBlder.Append("name=" + '"' + this.Text + '"' + " ");//Name
            string strBinding = "";
            if (this._rField != null)
            {
                strBinding = this._rField;
            }
            strBlder.Append("binding=" + '"' + strBinding + '"' + " ");//binding
            strBlder.Append('>');
            strBlder.Append("\r\n");
            int itemCount = this.Items.Count;
            for (int i = 0; i < itemCount;i++ )
            {
                strBlder.Append(" 			" + "<option value=");
                
                strBlder.Append('"' + this.Items[i].ToString() + '"' + '>');//代表的内容
                strBlder.Append(this.Items[i].ToString());//显示名字
                strBlder.Append("</option>");
                strBlder.Append("\r\n");
            }
            strBlder.Append(" 			" + "</select>");
            strBlder.Append("\r\n");
            return strBlder.ToString();
        }

        /// <summary>
        /// 将ComboBoxEx控件转换成Javascript文本
        /// </summary>
        /// <returns></returns>
        public string ControlsToJavascript(string TableID)
        {
            StringBuilder sb = new StringBuilder();
            this.Name = "ComboBoxEx-" + System.Guid.NewGuid().ToString();
            if (this._rField != "")
            {
                sb.Append("           ");
                sb.Append("t+=");
                sb.Append("document.getElementById(" + '"' + this.Name + '"' + ").attributes[" + '"' + "binding" + '"' + "].nodeValue" + "+" + '"' + "=" + '"' + "+");
                sb.Append("document.getElementById(" + '"' + this.Name + '"' + ").value+" + '"' + ";" + '"' + ";");
                sb.Append("\r\n");
            }
            return sb.ToString();
        }
     }

    /// <summary>
    /// 组合框下拉控件,增加关联字段属性,专为外键选择添加
    /// </summary>
    public class ForeignKeyCtrlEx : PMS.Libraries.ToolControls.PmsSheet.ForeignKeyCtrl.ForeignKeyCtrl
    {
        private string _rField;
        public ForeignKeyCtrlEx()
        {
            this._rField = "";
            _RelationFields = new List<PmsField>();
        }

        public string RField
        {
            get { return this._rField; }
            set
            {
                this._rField = value;
            }
        }
        private List<PmsField> _RelationFields;
        public List<PmsField> RelationFields
        {
            get { return this._RelationFields; }
            set
            {
                this._RelationFields = value;
            }
        }
        private string _ctrlType;

        public string CtrlType
        {
            get { return _ctrlType; }
            set { _ctrlType = value; }
        }
    }

    /// <summary>
    /// 复选框控件,增加关联字段属性
    /// </summary>
    public class CheckBoxEx : CheckBox, IControlsToHtml
    {
        private string _rField;
        public CheckBoxEx()
        {
            this._rField = "";
        }
        public string RField
        {
            get { return this._rField; }
            set
            {
                this._rField = value;
            }
        }
        public Guid RNode;//被绑定的节点id，在查询条件中配置

        private List<PmsField> _RelationFields;
        public List<PmsField> RelationFields
        {
            get { return this._RelationFields; }
            set
            {
                this._RelationFields = value;
            }
        }
        private string _ctrlType;

        public string CtrlType
        {
            get { return _ctrlType; }
            set { _ctrlType = value; }
        }
         /// <summary>
        /// 将CheckBoxEx控件转换成Html文本
        /// </summary>
        /// <returns></returns>
        public string ControlsToHtml( string TableIndex)
        {
            StringBuilder strBlder = new StringBuilder();
            strBlder.Append(" 			");
            strBlder.Append("<div style=");
            strBlder.Append('"');
            strBlder.Append("position:absolute;");
            strBlder.Append("left:" + this.Location.X.ToString() + "px;");
            strBlder.Append("top:" + this.Location.Y.ToString() + "px;");
            strBlder.Append("height:" + this.Height.ToString() + "px;");
            strBlder.Append("width:" + this.Width.ToString() + "px;");
            strBlder.Append('"');
            strBlder.Append("> <");
            strBlder.Append("input id=" + '"' + this.Name + '"' + " ");//ID
            strBlder.Append("name=" + '"' + this.Text + '"' + " ");//Name
            strBlder.Append("type=" + '"' + "checkbox" + '"' + " ");//Type
            string strBinding = "";
            if (this._rField != null)
            {
                strBinding = this._rField;
            }
            strBlder.Append("binding=" + '"' + strBinding + '"' + " ");//binding

            strBlder.Append("value=" + '"' + "true" + '"' + " ");//Value
            strBlder.Append("checked=" + '"' +this.Checked + '"' + " ");//Value
            strBlder.Append('>');
            strBlder.Append(this.Text);
            strBlder.Append("</div>");
            strBlder.Append("\r\n");
            return strBlder.ToString();
        }

        /// <summary>
        /// 将CheckBoxExs控件转换成Javascript文本
        /// </summary>
        /// <returns></returns>
        public string ControlsToJavascript(string TableID)
        {
            StringBuilder sb = new StringBuilder();
            this.Name = "CheckBoxEx-"+System.Guid.NewGuid().ToString();
            if (this._rField != "")
            {
                sb.Append("           ");
                sb.Append("t+=");
                sb.Append("document.getElementById(" + '"' + this.Name + '"' + ").attributes[" + '"' + "binding" + '"' + "].nodeValue" + "+" + '"' + "=" + '"' + "+");
                sb.Append("document.getElementById(" + '"' + this.Name + '"' + ").checked+" + '"' + ";" + '"'+";");
                sb.Append("\r\n");
            }
            return sb.ToString();
        }
    }

    /// <summary>
    /// 图片控件,增加关联字段属性
    /// </summary>
    public class PictureBoxEx : PictureBox
    {
        private Control pParentWin = null;
        private string _rField;
        private byte[] _ImageByte;
        private string _RName;
        [Browsable(false)]
        public string strFileType;
        public PictureBoxEx()
        {
            this._rField = "";
            this._RName = "";
            ImageByte = null;
            this.ImageLocation = "";
            strFileType = "";
            pParentWin = null;
            _RelationFields = new List<PmsField>();
        }
        public PictureBoxEx(object pParentWin1)
        {
            this._rField = "";
            this._RName = "";
            ImageByte = null;
            this.ImageLocation = "";
            strFileType = "";
            pParentWin = (Control)pParentWin1;
            _RelationFields = new List<PmsField>();
        }
        /// <summary>
        /// 关联字段
        /// </summary>
        public string RField
        {
            get { return this._rField; }
            set
            {
                this._rField = value;
            }
        }
        /// <summary>
        /// 关联后缀名字段,取该字段对应值,为下载时文件过滤类型
        /// </summary>        
        public string RName
        {
            get { return this._RName; }
            set
            {
                this._RName = value;
            }
        }
        /// <summary>
        /// 对应的二进制流,不在控件属性中显示
        /// </summary>
        [Browsable(false)]
        public byte[] ImageByte
        {
            get { return this._ImageByte; }
            set
            {
                this._ImageByte = value;
            }
        }
        /// <summary>
        /// 鼠标双击事件
        /// </summary>
        /// <param name="e">双击类型,左键上传,右建下载</param>
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                OpenFileDialog fd = new OpenFileDialog();
                if (fd.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        FileStream fileStream = new FileStream(fd.FileName, FileMode.Open);//文件大小限制?
                        long abc = fileStream.Length;
                        _ImageByte = new byte[(int)fileStream.Length];
                        fileStream.Read(_ImageByte, 0, (int)fileStream.Length);
                        fileStream.Close();
                    }
                    catch
                    {
                        MessageBox.Show("文件不存在或被占用!");
                        return;
                    }
                    MemoryStream ms = new MemoryStream(_ImageByte);
                    try
                    {
                        this.Image = Image.FromStream(ms);
                    }
                    catch
                    {
                        this.Image = (Image)Properties.Resources.ResourceManager.GetObject("file");
                        WebBrowser axWebBrowser1;
                        if (this.Tag == null)
                        {
                            axWebBrowser1 = new WebBrowser();
                            this.Tag = axWebBrowser1;
                            this.Parent.Controls.Add(axWebBrowser1);
                        }
                        else
                        {
                            axWebBrowser1 = (WebBrowser)this.Tag;
                        }
                        axWebBrowser1.Visible = true;
                        axWebBrowser1.BringToFront();
                        System.Drawing.Point pt = this.Location;
                        pt.X += 20;
                        axWebBrowser1.Location = pt;
                        axWebBrowser1.Size = new Size(this.Size.Width, this.Size.Height);
                        axWebBrowser1.Navigate(fd.FileName);
                    }

                    //更新文件后缀名字段
                    int dotPos = fd.FileName.LastIndexOf('.');
                    if (dotPos >= 0)
                    {
                        strFileType = fd.FileName.Substring(dotPos + 1);
                        if (pParentWin != null)
                        {
                            TabControl tc = (TabControl)(pParentWin.Parent);
                            foreach (TabPage tp in tc.TabPages)
                            {
                                IEnumerator enumerator = tp.Controls.GetEnumerator();
                                while (enumerator.MoveNext())
                                {
                                    Control cmp = (Control)enumerator.Current;
                                    if (cmp is TextBoxEx)
                                    {
                                        TextBoxEx pTB = (TextBoxEx)cmp;
                                        if (pTB.RField == RName)
                                            pTB.Text = strFileType;
                                    }
                                    else if (cmp is ComboBoxEx)
                                    {
                                        ComboBoxEx pTB = (ComboBoxEx)cmp;
                                        if (pTB.RField == RName)
                                            pTB.Text = strFileType;
                                    }
                                }
                            }
                        }
                    }
                    this.Invalidate();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                SaveFileDialog fd = new SaveFileDialog();

                if (strFileType.Length > 0)
                {
                    fd.DefaultExt = strFileType;
                    fd.Filter = strFileType + "文件|*." + strFileType;
                }
                if (fd.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        string filename = fd.FileName;
                        FileStream stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Read | FileShare.Write);
                        stream.SetLength(this.ImageByte.LongLength);
                        stream.Write(this.ImageByte, 0, this.ImageByte.Length);  //将二进制文件写到指定目录
                        stream.Close();
                        MessageBox.Show("保存成功！");
                    }
                    catch
                    {
                        MessageBox.Show("保存失败！");
                    }
                }
            }
        }

        private List<PmsField> _RelationFields;
        public List<PmsField> RelationFields
        {
            get { return this._RelationFields; }
            set
            {
                this._RelationFields = value;
            }
        }
        private string _ctrlType;

        public string CtrlType
        {
            get { return _ctrlType; }
            set { _ctrlType = value; }
        }
    }
    
    /// <summary>
    /// 自定义属性类
    /// </summary>
    public class CustomProperty
    {
        #region Private Variables
        private string _name = string.Empty;
        private object _defaultValue = null;
        private object _value = null;
        private object _objectSource = null;
        private PropertyInfo[] _propertyInfos = null;
        #endregion

        #region Contructors
        public CustomProperty()
        {
        }

        public CustomProperty(string name, string category, string description, object objectSource)
            : this(name, name, null, category, description, objectSource, null)
        {
        }

        public CustomProperty(string name, string propertyName, string category, string description, object objectSource)
            : this(name, propertyName, null, category, description, objectSource, null)
        {
        }

        public CustomProperty(string name, string propertyName, string category, string description, object objectSource, Type editorType)
            : this(name, propertyName, null, category, description, objectSource, editorType)
        {
        }

        public CustomProperty(string name, string propertyName, Type valueType, string category, string description,
            object objectSource, Type editorType)
            : this(name, new string[] { propertyName }, valueType, null, null, false, true, category, description, objectSource, editorType)
        {
        }

        public CustomProperty(string name, string[] propertyNames, string category, string description, object objectSource)
            : this(name, propertyNames, category, description, objectSource, null)
        {
        }

        public CustomProperty(string name, string[] propertyNames, string category, string description, object objectSource, Type editorType)
            : this(name, propertyNames, null, category, description, objectSource, editorType)
        {
        }

        public CustomProperty(string name, string[] propertyNames, Type valueType, string category, string description,
            object objectSource, Type editorType)
            : this(name, propertyNames, valueType, null, null, false, true, category, description, objectSource, editorType)
        {
        }

        public CustomProperty(string name, string[] propertyNames, Type valueType, object defaultValue, object value,
            bool isReadOnly, bool isBrowsable, string category, string description, object objectSource, Type editorType)
        {
            Name = name;
            PropertyNames = propertyNames;
            ValueType = valueType;
            _defaultValue = defaultValue;
            _value = value;
            IsReadOnly = isReadOnly;
            IsBrowsable = isBrowsable;
            Category = category;
            Description = description;
            ObjectSource = objectSource;
            EditorType = editorType;
        }
        #endregion

        #region Public Properties

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;

                if (PropertyNames == null)
                {
                    PropertyNames = new string[] { _name };
                }
            }
        }

        public string[] PropertyNames { get; set; }

        public Type ValueType { get; set; }

        public object DefaultValue
        {
            get { return _defaultValue; }
            set
            {
                _defaultValue = value;
                if (_defaultValue != null)
                {
                    if (_value == null) _value = _defaultValue;
                    if (ValueType == null) ValueType = _defaultValue.GetType();
                }
            }
        }

        public object Value
        {
            get { return _value; }
            set
            {
                _value = value;

                OnValueChanged();
            }
        }

        public bool IsReadOnly { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public bool IsBrowsable { get; set; }

        public object ObjectSource
        {
            get { return _objectSource; }
            set
            {
                _objectSource = value;
                OnObjectSourceChanged();
            }
        }

        public Type EditorType { get; set; }
        #endregion

        #region Protected Functions

        protected void OnObjectSourceChanged()
        {
            if (PropertyInfos.Length == 0) return;
            if (PropertyInfos[0] != null)
            {
                object value = PropertyInfos[0].GetValue(_objectSource, null);
                if (_defaultValue == null) DefaultValue = value;
                _value = value;
            }
            else
            {
                return;
            }
        }

        protected void OnValueChanged()
        {
            if (_objectSource == null) return;

            foreach (PropertyInfo propertyInfo in PropertyInfos)
            {
                try
                {
                    propertyInfo.SetValue(_objectSource, _value, null);
                }
                catch { }
            }
        }

        protected PropertyInfo[] PropertyInfos
        {
            get
            {
                if (_propertyInfos == null)
                {
                    Type type = ObjectSource.GetType();
                    _propertyInfos = new PropertyInfo[PropertyNames.Length];
                    for (int i = 0; i < PropertyNames.Length; i++)
                    {
                        _propertyInfos[i] = type.GetProperty(PropertyNames[i]);
                    }
                }
                return _propertyInfos;
            }
        }
        #endregion

        #region Prublic Functions
        public void ResetValue()
        {
            Value = DefaultValue;
        }
        #endregion
    }

    /// <summary>
    /// 自定义属性类集合,包含属性自定义编辑器
    /// </summary>
     public class CustomPropertyCollection : List<CustomProperty>, ICustomTypeDescriptor
    {
        public Control CtrlType;

        #region ICustomTypeDescriptor 成员

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            PropertyDescriptorCollection properties = new PropertyDescriptorCollection(null);

            foreach (CustomProperty cp in this)
            {
                List<Attribute> attrs = new List<Attribute>();
                //[Browsable(false)]
                if (!cp.IsBrowsable)
                {
                    attrs.Add(new BrowsableAttribute(cp.IsBrowsable));
                }
                //[ReadOnly(true)]
                if (cp.IsReadOnly)
                {
                    attrs.Add(new ReadOnlyAttribute(cp.IsReadOnly));
                }
                //[Editor(typeof(editor),typeof(UITypeEditor))]
                if (cp.EditorType != null)
                {
                    attrs.Add(new EditorAttribute(cp.EditorType, typeof(System.Drawing.Design.UITypeEditor)));
                }

                properties.Add(new CustomPropertyDescriptor(cp, attrs.ToArray()));
            }
            return properties;
        }

        public PropertyDescriptorCollection GetProperties()
        {
            return TypeDescriptor.GetProperties(this, true);
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        #endregion
    }

    /// <summary>
    /// 自定义属性描述器类
    /// </summary>

    public class CustomPropertyDescriptor : PropertyDescriptor
    {
        private CustomProperty _customProperty = null;

        public CustomPropertyDescriptor(CustomProperty customProperty, Attribute[] attrs)
            : base(customProperty.Name, attrs)
        {
            _customProperty = customProperty;
        }

        public override bool CanResetValue(object component)
        {
            return _customProperty.DefaultValue != null;
        }

        public override Type ComponentType
        {
            get { return _customProperty.GetType(); }
        }

        public override object GetValue(object component)
        {
            return _customProperty.Value;
        }

        public override bool IsReadOnly
        {
            get { return _customProperty.IsReadOnly; }
        }

        public override Type PropertyType
        {
            get { return _customProperty.ValueType; }
        }

        public override void ResetValue(object component)
        {
            _customProperty.ResetValue();
        }

        public override void SetValue(object component, object value)
        {
            _customProperty.Value = value;
        }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }

        //
        public override string Description
        {
            get
            {
                return _customProperty.Description;
            }
        }

        public override string Category
        {
            get
            {
                return _customProperty.Category;
            }
        }

        public override string DisplayName
        {
            get
            {
                return _customProperty.Name;
            }
        }

        public override bool IsBrowsable
        {
            get
            {
                return _customProperty.IsBrowsable;
            }
        }

        public object CustomProperty
        {
            get
            {
                return _customProperty;
            }
        }
    }

    /// <summary>
    /// combobox下拉选择列表编辑器
    /// </summary>

    internal class ScopeDropDownEditor : UITypeEditor
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
                    CustomPropertyCollection cbx = (CustomPropertyCollection)context.Instance;
                    ComboBox.ObjectCollection coc = (ComboBox.ObjectCollection)cbx[6].Value;
                    FormComboBoxItem form2 = new FormComboBoxItem();

                    if(null!=coc&&coc.Count>0)
                        form2.ExplainData = coc[0] as ComboBoxItemData;
                    if (DialogResult.OK == editorService.ShowDialog(form2))
                    {
                        coc.Clear();
                        coc.Add(form2.ExplainData);

                        value = coc;
                    }
                    return value;
                }
            }

            return value;
        }
    }

    /// <summary>
    /// 关联字段选择编辑器
    /// </summary>
    internal class FieldRelationEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context != null && context.Instance != null)
            {
                return UITypeEditorEditStyle.DropDown;
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
                    CustomPropertyCollection cbx = (CustomPropertyCollection)context.Instance;
                    RelationFieldChoose rfc = new RelationFieldChoose(editorService);

                    rfc.strRField = (string)cbx[0].Value;
                    rfc.pmsFieldList = (List<PmsField>)cbx[1].DefaultValue;
                    rfc.CtrlType = (string)cbx[2].DefaultValue;
                    editorService.DropDownControl(rfc);
                    value = rfc.strRField;
                    return value;
                }
            }

            return value;
        }
    }
    /// <summary>
    /// 附件类型关联字段选择编辑器
    /// </summary>
    internal class FileRNameEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context != null && context.Instance != null)
            {
                return UITypeEditorEditStyle.DropDown;
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
                    CustomPropertyCollection cbx = (CustomPropertyCollection)context.Instance;
                    RelationFieldChoose rfc = new RelationFieldChoose(editorService);

                    rfc.strRField = (string)cbx[3].Value;
                    rfc.pmsFieldList = (List<PmsField>)cbx[1].DefaultValue;
                    rfc.CtrlType = "VARCHAR";
                    editorService.DropDownControl(rfc);
                    value = rfc.strRField;
                    
                    return value;
                }
            }

            return value;
        }
    }
    /// <summary>
    /// 弹出框的确定、取消的文本
    /// </summary>
    internal class OkCancelTextEditor : UITypeEditor
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
                    CustomPropertyCollection cbx = (CustomPropertyCollection)context.Instance;
                    OkCancelText rfc = new OkCancelText();
                    rfc.Texts=cbx[1].Value as Dictionary<string,string>;
                    DialogResult result = rfc.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        value = rfc.Texts;
                        return value;
                    }
                }
            }
            return value;
        }
    }
}
