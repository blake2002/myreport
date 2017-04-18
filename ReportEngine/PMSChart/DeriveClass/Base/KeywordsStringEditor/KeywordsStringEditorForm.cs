using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;
using System.Globalization;
using System.Drawing;

namespace PMS.Libraries.ToolControls.PMSChart
{
    public class KeywordsStringEditorForm : Form
    {
        // Fields
        private Button _buttonCancel;
        private Button _buttonEdit;
        private Button _buttonInsert;
        private Button _buttonOk;
        private string _classTypeName;
        private Container _components;
        private GroupBox _groupBoxString;
        private string _initialString;
        private Label _labelDescription;
        private int _maxYValueIndex;
        private Panel _panelInsertEditButtons;
        private Panel _panelOkCancelButtons;
        private Panel _panelTopContent;
        private string _propertyName;
        private RichTextBox _richTextBox;
        private int _selectedKeywordLength;
        private string _selectedKeywordName;
        private int _selectedKeywordStart;
        private bool _updating;
        internal ArrayList applicableKeywords;
        internal KeywordsRegistry KeywordsRegistry;
        public string ResultString;

        // Methods
        public KeywordsStringEditorForm()
        {
            this._propertyName = string.Empty;
            this._classTypeName = string.Empty;
            this._initialString = string.Empty;
            this.ResultString = string.Empty;
            this._maxYValueIndex = 9;
            this._selectedKeywordName = string.Empty;
            this._selectedKeywordStart = -1;
            this._selectedKeywordLength = -1;
            this.InitializeComponent();
        }

        public KeywordsStringEditorForm(string initialString, string classTypeName, string propertyName, int maxYValueIndex)
        {
            this._propertyName = string.Empty;
            this._classTypeName = string.Empty;
            this._initialString = string.Empty;
            this.ResultString = string.Empty;
            this._maxYValueIndex = 9;
            this._selectedKeywordName = string.Empty;
            this._selectedKeywordStart = -1;
            this._selectedKeywordLength = -1;
            this.InitializeComponent();
            this._classTypeName = classTypeName;
            this._propertyName = propertyName;
            this._maxYValueIndex = maxYValueIndex;
            this._initialString = initialString;
            this.ResultString = initialString;
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            string keyword = this._richTextBox.Text.Substring(this._selectedKeywordStart, this._selectedKeywordLength);
            KeywordEditor editor = new KeywordEditor(this.applicableKeywords, keyword, this._maxYValueIndex);
            if (editor.ShowDialog() == DialogResult.OK)
            {
                int length = this._selectedKeywordStart;
                int num2 = this._selectedKeywordLength;
                this._richTextBox.Text = this._richTextBox.Text.Substring(0, length) + editor.Keyword + this._richTextBox.Text.Substring(length + num2);
                this._richTextBox.SelectionStart = length + editor.Keyword.Length;
            }
            this._richTextBox.Focus();
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            KeywordEditor editor = new KeywordEditor(this.applicableKeywords, string.Empty, this._maxYValueIndex);
            if (editor.ShowDialog() == DialogResult.OK)
            {
                if (this._selectedKeywordLength > 0)
                {
                    this._richTextBox.SelectionStart += this._richTextBox.SelectionLength;
                    this._richTextBox.SelectionLength = 0;
                    this._richTextBox.SelectedText = " " + editor.Keyword;
                }
                else
                {
                    this._richTextBox.SelectionLength = Math.Max(0, this._selectedKeywordLength);
                    this._richTextBox.SelectedText = editor.Keyword;
                }
            }
            this._richTextBox.Focus();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.ResultString = this._richTextBox.Text;
            this.ResultString = this.ResultString.Replace("\r\n", @"\n");
            this.ResultString = this.ResultString.Replace("\n", @"\n");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this._components != null))
            {
                this._components.Dispose();
            }
            base.Dispose(disposing);
        }

        private ArrayList GetApplicableKeywords()
        {
            ArrayList list = new ArrayList();
            if (((this.KeywordsRegistry == null) || (this._propertyName.Length <= 0)) || (this._classTypeName.Length <= 0))
            {
                return list;
            }
        //Label_0106:
            foreach (KeywordInfo info in this.KeywordsRegistry.registeredKeywords)
            {
                bool flag = false;
                foreach (string str in info.AppliesToTypes.Split(new char[] { ',' }))
                {
                    if (this._classTypeName == str.Trim())
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    foreach (string str2 in info.AppliesToProperties.Split(new char[] { ',' }))
                    {
                        if (this._propertyName == str2.Trim())
                        {
                            list.Add(info);
                            //goto Label_0106;
                        }
                    }
                }
            }
            return list;
        }

        private string GetColorHilightedRtfText(string originalText)
        {
            string str = originalText;
            string name = string.Empty;
            this._selectedKeywordStart = -1;
            this._selectedKeywordLength = 0;
            int selectionStart = this._richTextBox.SelectionStart;
            str = str.Replace(@"\n", "\r\n");
            int num2 = 0;
            for (int i = 0; (i < str.Length) && (i < selectionStart); i++)
            {
                if (str[i] == '\\')
                {
                    num2++;
                }
            }
            selectionStart += num2;
            str = str.Replace(@"\", @"\\");
            foreach (KeywordInfo info in this.applicableKeywords)
            {
                foreach (string str3 in info.GetKeywords())
                {
                    int startIndex = 0;
                    string str4 = str3.Trim();
                    if (str4.Length > 0)
                    {
                        while ((startIndex = str.IndexOf(str4, startIndex, StringComparison.Ordinal)) >= 0)
                        {
                            int length = str4.Length;
                            if ((info.SupportsValueIndex && (str.Length > (startIndex + length))) && (str[startIndex + length] == 'Y'))
                            {
                                length++;
                                if ((str.Length > (startIndex + length)) && char.IsDigit(str[startIndex + length]))
                                {
                                    length++;
                                }
                            }
                            if ((info.SupportsFormatting && (str.Length > (startIndex + length))) && (str[startIndex + length] == '{'))
                            {
                                length++;
                                int num6 = str.IndexOf("}", startIndex + length, StringComparison.Ordinal);
                                if (num6 >= 0)
                                {
                                    length += ((num6 - startIndex) - length) + 1;
                                }
                            }
                            bool flag = (selectionStart > startIndex) && (selectionStart <= (startIndex + length));
                            string str5 = str.Substring(0, startIndex);
                            string str6 = string.Empty + @"\cf1";
                            if (flag)
                            {
                                name = info.Name;
                                name = name + "__" + startIndex.ToString(CultureInfo.InvariantCulture);
                                this._selectedKeywordStart = startIndex;
                                this._selectedKeywordStart -= selectionStart - this._richTextBox.SelectionStart;
                                this._selectedKeywordLength = length;
                                str6 = str6 + @"\b";
                            }
                            str6 = (str6 + @"\ul" + "#_") + str.Substring(startIndex + 1, length - 1) + @"\cf0";
                            if (flag)
                            {
                                str6 = str6 + @"\b0";
                            }
                            str6 = str6 + @"\ul0 ";
                            str = str5 + str6 + str.Substring(startIndex + length);
                            if (startIndex < selectionStart)
                            {
                                selectionStart += str6.Length - length;
                            }
                            startIndex += str6.Length;
                        }
                    }
                }
            }
            this._selectedKeywordName = name;
            if (this._selectedKeywordName.Length > 0)
            {
                this._buttonEdit.Enabled = true;
            }
            else
            {
                this._buttonEdit.Enabled = false;
            }
            return str.Replace("\r\n", @"\par ").Replace("\n", @"\par ").Replace(@"\n", @"\par ").Replace("{", @"\{").Replace("}", @"\}").Replace("#_", "#");
        }

        private string GetRtfText(string originalText)
        {
            string str = string.Empty;
            str = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{\f0\fnil\fcharset0 Microsoft Sans Serif;}}\r\n";
            return ((str + @"{\colortbl ;\red0\green0\blue255;}\r\n" + @"\viewkind4\uc1\pard\f0\fs17 ") + this.GetColorHilightedRtfText(originalText) + @"\par\r\n}");
        }

        private void InitializeComponent()
        {
            this._richTextBox = new System.Windows.Forms.RichTextBox();
            this._groupBoxString = new System.Windows.Forms.GroupBox();
            this._panelInsertEditButtons = new System.Windows.Forms.Panel();
            this._buttonInsert = new System.Windows.Forms.Button();
            this._buttonEdit = new System.Windows.Forms.Button();
            this._buttonOk = new System.Windows.Forms.Button();
            this._buttonCancel = new System.Windows.Forms.Button();
            this._labelDescription = new System.Windows.Forms.Label();
            this._panelOkCancelButtons = new System.Windows.Forms.Panel();
            this._panelTopContent = new System.Windows.Forms.Panel();
            this._groupBoxString.SuspendLayout();
            this._panelInsertEditButtons.SuspendLayout();
            this._panelOkCancelButtons.SuspendLayout();
            this._panelTopContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // _richTextBox
            // 
            this._richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._richTextBox.Location = new System.Drawing.Point(6, 20);
            this._richTextBox.Margin = new System.Windows.Forms.Padding(7);
            this._richTextBox.Name = "_richTextBox";
            this._richTextBox.Size = new System.Drawing.Size(504, 141);
            this._richTextBox.TabIndex = 0;
            this._richTextBox.Text = "";
            this._richTextBox.WordWrap = false;
            this._richTextBox.SelectionChanged += new System.EventHandler(this.richTextBox_SelectionChanged);
            this._richTextBox.TextChanged += new System.EventHandler(this.richTextBox_TextChanged);
            this._richTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox_KeyDown);
            this._richTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.richTextBox_KeyPress);
            // 
            // _groupBoxString
            // 
            this._groupBoxString.Controls.Add(this._panelInsertEditButtons);
            this._groupBoxString.Controls.Add(this._richTextBox);
            this._groupBoxString.Dock = System.Windows.Forms.DockStyle.Fill;
            this._groupBoxString.Location = new System.Drawing.Point(0, 56);
            this._groupBoxString.Name = "_groupBoxString";
            this._groupBoxString.Padding = new System.Windows.Forms.Padding(6);
            this._groupBoxString.Size = new System.Drawing.Size(516, 167);
            this._groupBoxString.TabIndex = 1;
            this._groupBoxString.TabStop = false;
            this._groupBoxString.Text = "具有关键字的字符串(S):";
            // 
            // _panelInsertEditButtons
            // 
            this._panelInsertEditButtons.Controls.Add(this._buttonInsert);
            this._panelInsertEditButtons.Controls.Add(this._buttonEdit);
            this._panelInsertEditButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this._panelInsertEditButtons.Location = new System.Drawing.Point(321, 20);
            this._panelInsertEditButtons.Name = "_panelInsertEditButtons";
            this._panelInsertEditButtons.Size = new System.Drawing.Size(189, 141);
            this._panelInsertEditButtons.TabIndex = 3;
            // 
            // _buttonInsert
            // 
            this._buttonInsert.Location = new System.Drawing.Point(30, 2);
            this._buttonInsert.Name = "_buttonInsert";
            this._buttonInsert.Size = new System.Drawing.Size(156, 27);
            this._buttonInsert.TabIndex = 1;
            this._buttonInsert.Text = "插入新关键字";
            this._buttonInsert.Click += new System.EventHandler(this.buttonInsert_Click);
            // 
            // _buttonEdit
            // 
            this._buttonEdit.Enabled = false;
            this._buttonEdit.Location = new System.Drawing.Point(30, 34);
            this._buttonEdit.Name = "_buttonEdit";
            this._buttonEdit.Size = new System.Drawing.Size(156, 27);
            this._buttonEdit.TabIndex = 2;
            this._buttonEdit.Text = "编辑关键字";
            this._buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // _buttonOk
            // 
            this._buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._buttonOk.Location = new System.Drawing.Point(321, 9);
            this._buttonOk.Name = "_buttonOk";
            this._buttonOk.Size = new System.Drawing.Size(90, 27);
            this._buttonOk.TabIndex = 2;
            this._buttonOk.Text = "确定";
            this._buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // _buttonCancel
            // 
            this._buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonCancel.Location = new System.Drawing.Point(417, 9);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(90, 27);
            this._buttonCancel.TabIndex = 3;
            this._buttonCancel.Text = "取消";
            // 
            // _labelDescription
            // 
            this._labelDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this._labelDescription.Location = new System.Drawing.Point(0, 0);
            this._labelDescription.Name = "_labelDescription";
            this._labelDescription.Size = new System.Drawing.Size(516, 56);
            this._labelDescription.TabIndex = 0;
            this._labelDescription.Text = "您可以在下面的文本编辑器中输入静态文本以及任意个数的关键字。关键字是一种特殊格式的字符序列，它将替换为关联的图表序列值或计算值。在文本编辑器中，按Enter可以插" +
                "入换行符。";
            // 
            // _panelOkCancelButtons
            // 
            this._panelOkCancelButtons.Controls.Add(this._buttonOk);
            this._panelOkCancelButtons.Controls.Add(this._buttonCancel);
            this._panelOkCancelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._panelOkCancelButtons.Location = new System.Drawing.Point(4, 227);
            this._panelOkCancelButtons.Name = "_panelOkCancelButtons";
            this._panelOkCancelButtons.Padding = new System.Windows.Forms.Padding(6);
            this._panelOkCancelButtons.Size = new System.Drawing.Size(516, 44);
            this._panelOkCancelButtons.TabIndex = 4;
            // 
            // _panelTopContent
            // 
            this._panelTopContent.Controls.Add(this._groupBoxString);
            this._panelTopContent.Controls.Add(this._labelDescription);
            this._panelTopContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelTopContent.Location = new System.Drawing.Point(4, 4);
            this._panelTopContent.Name = "_panelTopContent";
            this._panelTopContent.Size = new System.Drawing.Size(516, 223);
            this._panelTopContent.TabIndex = 5;
            // 
            // KeywordsStringEditorForm
            // 
            this.CancelButton = this._buttonCancel;
            this.ClientSize = new System.Drawing.Size(524, 275);
            this.Controls.Add(this._panelTopContent);
            this.Controls.Add(this._panelOkCancelButtons);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(524, 275);
            this.Name = "KeywordsStringEditorForm";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "字符串关键字编辑器";
            this.Load += new System.EventHandler(this.KeywordsStringEditorForm_Load);
            this._groupBoxString.ResumeLayout(false);
            this._panelInsertEditButtons.ResumeLayout(false);
            this._panelOkCancelButtons.ResumeLayout(false);
            this._panelTopContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void KeywordsStringEditorForm_Load(object sender, EventArgs e)
        {
            this._labelDescription.Text = this._labelDescription.Text.Replace(@"\n", "\n");
            this.applicableKeywords = this.GetApplicableKeywords();
            if (this.applicableKeywords.Count == 0)
            {
                this._buttonInsert.Enabled = false;
                this._buttonEdit.Enabled = false;
            }
            if (!string.IsNullOrEmpty(this._initialString))
            {
                this._richTextBox.Rtf = this.GetRtfText(this._initialString);
            }
        }

        private void richTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (((e.KeyCode == Keys.Delete) && (this._selectedKeywordStart >= 0)) && (this._selectedKeywordLength > 0))
            {
                int num = this._selectedKeywordStart;
                string str = this._richTextBox.Text.Substring(0, this._selectedKeywordStart) + this._richTextBox.Text.Substring(this._selectedKeywordStart + this._selectedKeywordLength);
                this._richTextBox.Text = str;
                this._richTextBox.SelectionStart = num;
                e.Handled = true;
            }
        }

        private void richTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == '{') && (this._richTextBox.SelectionColor == Color.Blue))
            {
                e.Handled = true;
                this._richTextBox.SelectedText = "{}";
                this._richTextBox.SelectionStart--;
            }
        }

        private void richTextBox_SelectionChanged(object sender, EventArgs e)
        {
            if (((Control.ModifierKeys & Keys.Shift) != Keys.Shift) && !this._updating)
            {
                this._updating = true;
                string str = this._selectedKeywordName;
                string rtfText = this.GetRtfText(this._richTextBox.Text);
                if (str != this._selectedKeywordName)
                {
                    int selectionStart = this._richTextBox.SelectionStart;
                    this._richTextBox.Rtf = rtfText;
                    this._richTextBox.SelectionStart = selectionStart;
                    this._richTextBox.SelectionLength = 0;
                }
                this._updating = false;
            }
        }

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this._updating)
            {
                this._updating = true;
                int selectionStart = this._richTextBox.SelectionStart;
                int selectionLength = this._richTextBox.SelectionLength;
                this._richTextBox.Rtf = this.GetRtfText(this._richTextBox.Text);
                this._richTextBox.SelectionStart = selectionStart;
                this._richTextBox.SelectionLength = selectionLength;
                this._updating = false;
            }
        }
    }
}
