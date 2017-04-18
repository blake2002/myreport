using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace PMS.Libraries.ToolControls.PMSChart
{
    public class KeywordEditor : Form
    {
        // Fields
        private ArrayList _applicableKeywords;
        private Button _buttonCancel;
        private Button _buttonOk;
        private ComboBox _comboBoxFormat;
        private IContainer _components;
        private GroupBox _groupBoxDescription;
        private GroupBox _groupBoxFormat;
        private GroupBox _groupBoxKeywords;
        private Label _labelCustomFormat;
        private Label _labelDescription;
        private Label _labelFormat;
        private Label _labelPrecision;
        private Label _labelSample;
        private Label _labelYValue;
        private ListBox _listBoxKeywords;
        private int _maxYValueIndex;
        private NumericUpDown _numericUpDownYValue;
        private TextBox _textBoxCustomFormat;
        private TextBox _textBoxPrecision;
        private TextBox _textBoxSample;
        private ToolTip _toolTip;
        private IContainer components;
        internal string Keyword;

        // Methods
        public KeywordEditor()
        {
            this.Keyword = string.Empty;
            this._maxYValueIndex = 9;
            this.InitializeComponent();
        }

        public KeywordEditor(ArrayList applicableKeywords, string keyword, int maxYValueIndex)
        {
            this.Keyword = string.Empty;
            this._maxYValueIndex = 9;
            this.InitializeComponent();
            this._applicableKeywords = applicableKeywords;
            this.Keyword = keyword;
            this._maxYValueIndex = maxYValueIndex;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Keyword = string.Empty;
            KeywordInfo selectedItem = this._listBoxKeywords.SelectedItem as KeywordInfo;
            if (selectedItem != null)
            {
                this.Keyword = selectedItem.Keyword;
                if (selectedItem.SupportsValueIndex && (((int)this._numericUpDownYValue.Value) > 0))
                {
                    this.Keyword = this.Keyword + "Y" + ((int)this._numericUpDownYValue.Value).ToString(CultureInfo.InvariantCulture);
                }
                if ((selectedItem.SupportsFormatting && (this._comboBoxFormat.SelectedIndex > 0)) && (this.GetFormatString().Length > 0))
                {
                    this.Keyword = this.Keyword + "{" + this.GetFormatString() + "}";
                }
            }
        }

        private void comboBoxFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._labelCustomFormat.Enabled = this._comboBoxFormat.SelectedIndex > 0;
            this._textBoxCustomFormat.Enabled = this._comboBoxFormat.SelectedIndex > 0;
            this._labelPrecision.Enabled = this._comboBoxFormat.SelectedIndex > 0;
            this._textBoxPrecision.Enabled = this._comboBoxFormat.SelectedIndex > 0;
            this._labelSample.Enabled = this._comboBoxFormat.SelectedIndex > 0;
            this._textBoxSample.Enabled = this._comboBoxFormat.SelectedIndex > 0;
            bool flag = ((string)this._comboBoxFormat.SelectedItem) == "Custom";
            this._labelCustomFormat.Visible = flag;
            this._textBoxCustomFormat.Visible = flag;
            this._labelPrecision.Visible = !flag;
            this._textBoxPrecision.Visible = !flag;
            this.UpdateNumericSample();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this._components != null))
            {
                this._components.Dispose();
            }
            base.Dispose(disposing);
        }

        private string GetFormatString()
        {
            string text = string.Empty;
            if (this._comboBoxFormat.Enabled && (this._comboBoxFormat.SelectedIndex == 1))
            {
                return ("C" + this._textBoxPrecision.Text);
            }
            if (this._comboBoxFormat.SelectedIndex == 2)
            {
                return ("D" + this._textBoxPrecision.Text);
            }
            if (this._comboBoxFormat.SelectedIndex == 3)
            {
                return ("E" + this._textBoxPrecision.Text);
            }
            if (this._comboBoxFormat.SelectedIndex == 4)
            {
                return ("G" + this._textBoxPrecision.Text);
            }
            if (this._comboBoxFormat.SelectedIndex == 5)
            {
                return ("G" + this._textBoxPrecision.Text);
            }
            if (this._comboBoxFormat.SelectedIndex == 6)
            {
                return ("N" + this._textBoxPrecision.Text);
            }
            if (this._comboBoxFormat.SelectedIndex == 7)
            {
                return ("P" + this._textBoxPrecision.Text);
            }
            if (this._comboBoxFormat.SelectedIndex == 8)
            {
                text = this._textBoxCustomFormat.Text;
            }
            return text;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._groupBoxKeywords = new System.Windows.Forms.GroupBox();
            this._listBoxKeywords = new System.Windows.Forms.ListBox();
            this._groupBoxDescription = new System.Windows.Forms.GroupBox();
            this._labelDescription = new System.Windows.Forms.Label();
            this._buttonCancel = new System.Windows.Forms.Button();
            this._buttonOk = new System.Windows.Forms.Button();
            this._groupBoxFormat = new System.Windows.Forms.GroupBox();
            this._textBoxPrecision = new System.Windows.Forms.TextBox();
            this._labelSample = new System.Windows.Forms.Label();
            this._textBoxSample = new System.Windows.Forms.TextBox();
            this._numericUpDownYValue = new System.Windows.Forms.NumericUpDown();
            this._labelYValue = new System.Windows.Forms.Label();
            this._comboBoxFormat = new System.Windows.Forms.ComboBox();
            this._labelPrecision = new System.Windows.Forms.Label();
            this._labelFormat = new System.Windows.Forms.Label();
            this._labelCustomFormat = new System.Windows.Forms.Label();
            this._textBoxCustomFormat = new System.Windows.Forms.TextBox();
            this._toolTip = new System.Windows.Forms.ToolTip(this.components);
            this._groupBoxKeywords.SuspendLayout();
            this._groupBoxDescription.SuspendLayout();
            this._groupBoxFormat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._numericUpDownYValue)).BeginInit();
            this.SuspendLayout();
            // 
            // _groupBoxKeywords
            // 
            this._groupBoxKeywords.Controls.Add(this._listBoxKeywords);
            this._groupBoxKeywords.Location = new System.Drawing.Point(10, 17);
            this._groupBoxKeywords.Name = "_groupBoxKeywords";
            this._groupBoxKeywords.Size = new System.Drawing.Size(259, 250);
            this._groupBoxKeywords.TabIndex = 0;
            this._groupBoxKeywords.TabStop = false;
            this._groupBoxKeywords.Text = "关键字(K): ";
            // 
            // _listBoxKeywords
            // 
            this._listBoxKeywords.ItemHeight = 12;
            this._listBoxKeywords.Location = new System.Drawing.Point(10, 26);
            this._listBoxKeywords.Name = "_listBoxKeywords";
            this._listBoxKeywords.Size = new System.Drawing.Size(240, 208);
            this._listBoxKeywords.TabIndex = 0;
            this._listBoxKeywords.SelectedIndexChanged += new System.EventHandler(this.listBoxKeywords_SelectedIndexChanged);
            this._listBoxKeywords.DoubleClick += new System.EventHandler(this.listBoxKeywords_DoubleClick);
            // 
            // _groupBoxDescription
            // 
            this._groupBoxDescription.Controls.Add(this._labelDescription);
            this._groupBoxDescription.Location = new System.Drawing.Point(288, 17);
            this._groupBoxDescription.Name = "_groupBoxDescription";
            this._groupBoxDescription.Size = new System.Drawing.Size(394, 95);
            this._groupBoxDescription.TabIndex = 1;
            this._groupBoxDescription.TabStop = false;
            this._groupBoxDescription.Text = "说明: ";
            // 
            // _labelDescription
            // 
            this._labelDescription.Location = new System.Drawing.Point(19, 26);
            this._labelDescription.Name = "_labelDescription";
            this._labelDescription.Size = new System.Drawing.Size(365, 60);
            this._labelDescription.TabIndex = 0;
            this._labelDescription.Text = "<replaced at runtime>";
            // 
            // _buttonCancel
            // 
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonCancel.Location = new System.Drawing.Point(575, 276);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(108, 29);
            this._buttonCancel.TabIndex = 4;
            this._buttonCancel.Text = "取消";
            // 
            // _buttonOk
            // 
            this._buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._buttonOk.Location = new System.Drawing.Point(440, 276);
            this._buttonOk.Name = "_buttonOk";
            this._buttonOk.Size = new System.Drawing.Size(108, 29);
            this._buttonOk.TabIndex = 3;
            this._buttonOk.Text = "确定";
            this._buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // _groupBoxFormat
            // 
            this._groupBoxFormat.Controls.Add(this._textBoxPrecision);
            this._groupBoxFormat.Controls.Add(this._labelSample);
            this._groupBoxFormat.Controls.Add(this._textBoxSample);
            this._groupBoxFormat.Controls.Add(this._numericUpDownYValue);
            this._groupBoxFormat.Controls.Add(this._labelYValue);
            this._groupBoxFormat.Controls.Add(this._comboBoxFormat);
            this._groupBoxFormat.Controls.Add(this._labelPrecision);
            this._groupBoxFormat.Controls.Add(this._labelFormat);
            this._groupBoxFormat.Controls.Add(this._labelCustomFormat);
            this._groupBoxFormat.Controls.Add(this._textBoxCustomFormat);
            this._groupBoxFormat.Location = new System.Drawing.Point(288, 121);
            this._groupBoxFormat.Name = "_groupBoxFormat";
            this._groupBoxFormat.Size = new System.Drawing.Size(394, 146);
            this._groupBoxFormat.TabIndex = 2;
            this._groupBoxFormat.TabStop = false;
            this._groupBoxFormat.Text = "值格式: ";
            // 
            // _textBoxPrecision
            // 
            this._textBoxPrecision.Location = new System.Drawing.Point(134, 52);
            this._textBoxPrecision.Name = "_textBoxPrecision";
            this._textBoxPrecision.Size = new System.Drawing.Size(77, 21);
            this._textBoxPrecision.TabIndex = 3;
            this._textBoxPrecision.TextChanged += new System.EventHandler(this.textBoxPrecision_TextChanged);
            // 
            // _labelSample
            // 
            this._labelSample.Location = new System.Drawing.Point(10, 78);
            this._labelSample.Name = "_labelSample";
            this._labelSample.Size = new System.Drawing.Size(115, 24);
            this._labelSample.TabIndex = 7;
            this._labelSample.Text = "格式示例(S):";
            this._labelSample.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _textBoxSample
            // 
            this._textBoxSample.Location = new System.Drawing.Point(134, 78);
            this._textBoxSample.Name = "_textBoxSample";
            this._textBoxSample.ReadOnly = true;
            this._textBoxSample.Size = new System.Drawing.Size(231, 21);
            this._textBoxSample.TabIndex = 8;
            // 
            // _numericUpDownYValue
            // 
            this._numericUpDownYValue.CausesValidation = false;
            this._numericUpDownYValue.Location = new System.Drawing.Point(134, 112);
            this._numericUpDownYValue.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this._numericUpDownYValue.Name = "_numericUpDownYValue";
            this._numericUpDownYValue.Size = new System.Drawing.Size(77, 21);
            this._numericUpDownYValue.TabIndex = 10;
            this._numericUpDownYValue.ValueChanged += new System.EventHandler(this.numericUpDownYValue_ValueChanged);
            // 
            // _labelYValue
            // 
            this._labelYValue.Location = new System.Drawing.Point(10, 112);
            this._labelYValue.Name = "_labelYValue";
            this._labelYValue.Size = new System.Drawing.Size(115, 25);
            this._labelYValue.TabIndex = 9;
            this._labelYValue.Text = "Y值索引(Y):";
            this._labelYValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _comboBoxFormat
            // 
            this._comboBoxFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxFormat.Items.AddRange(new object[] {
            "无",
            "货币",
            "小数",
            "科学型",
            "固定点",
            "常规",
            "数字",
            "百分比",
            "自定义"});
            this._comboBoxFormat.Location = new System.Drawing.Point(134, 26);
            this._comboBoxFormat.MaxDropDownItems = 10;
            this._comboBoxFormat.Name = "_comboBoxFormat";
            this._comboBoxFormat.Size = new System.Drawing.Size(231, 20);
            this._comboBoxFormat.TabIndex = 1;
            this._comboBoxFormat.SelectedIndexChanged += new System.EventHandler(this.comboBoxFormat_SelectedIndexChanged);
            // 
            // _labelPrecision
            // 
            this._labelPrecision.Location = new System.Drawing.Point(10, 52);
            this._labelPrecision.Name = "_labelPrecision";
            this._labelPrecision.Size = new System.Drawing.Size(115, 24);
            this._labelPrecision.TabIndex = 2;
            this._labelPrecision.Text = "精度(P):";
            this._labelPrecision.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _labelFormat
            // 
            this._labelFormat.Location = new System.Drawing.Point(10, 26);
            this._labelFormat.Name = "_labelFormat";
            this._labelFormat.Size = new System.Drawing.Size(115, 25);
            this._labelFormat.TabIndex = 0;
            this._labelFormat.Text = "格式(F):";
            this._labelFormat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _labelCustomFormat
            // 
            this._labelCustomFormat.Location = new System.Drawing.Point(10, 52);
            this._labelCustomFormat.Name = "_labelCustomFormat";
            this._labelCustomFormat.Size = new System.Drawing.Size(115, 24);
            this._labelCustomFormat.TabIndex = 4;
            this._labelCustomFormat.Text = "LabelKeyCustomFormat";
            this._labelCustomFormat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._labelCustomFormat.Visible = false;
            // 
            // _textBoxCustomFormat
            // 
            this._textBoxCustomFormat.Location = new System.Drawing.Point(134, 52);
            this._textBoxCustomFormat.Name = "_textBoxCustomFormat";
            this._textBoxCustomFormat.Size = new System.Drawing.Size(231, 21);
            this._textBoxCustomFormat.TabIndex = 5;
            this._textBoxCustomFormat.Visible = false;
            this._textBoxCustomFormat.TextChanged += new System.EventHandler(this.textBoxCustomFormat_TextChanged);
            // 
            // KeywordEditor
            // 
            this.AcceptButton = this._buttonOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.CancelButton = this._buttonCancel;
            this.ClientSize = new System.Drawing.Size(695, 314);
            this.Controls.Add(this._groupBoxFormat);
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._buttonOk);
            this.Controls.Add(this._groupBoxDescription);
            this.Controls.Add(this._groupBoxKeywords);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KeywordEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "关键字编辑器";
            this.Load += new System.EventHandler(this.KeywordEditor_Load);
            this._groupBoxKeywords.ResumeLayout(false);
            this._groupBoxDescription.ResumeLayout(false);
            this._groupBoxFormat.ResumeLayout(false);
            this._groupBoxFormat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._numericUpDownYValue)).EndInit();
            this.ResumeLayout(false);

        }

        private void KeywordEditor_Load(object sender, EventArgs e)
        {
            if ((this._maxYValueIndex >= 0) && (this._maxYValueIndex < 10))
            {
                this._numericUpDownYValue.Maximum = this._maxYValueIndex;
            }
            this._numericUpDownYValue.Enabled = this._maxYValueIndex > 0;
            this._labelYValue.Enabled = this._maxYValueIndex > 0;
            this._toolTip.SetToolTip(this._textBoxCustomFormat, /*SR.*/"DescriptionToolTipCustomFormatCharacters");
            this._comboBoxFormat.SelectedIndex = 0;
            if (this._applicableKeywords != null)
            {
                foreach (KeywordInfo info in this._applicableKeywords)
                {
                    this._listBoxKeywords.Items.Add(info);
                }
            }
            if (this.Keyword.Length == 0)
            {
                this._listBoxKeywords.SelectedIndex = 0;
                this._comboBoxFormat.SelectedIndex = 0;
            }
            else
            {
                bool flag = false;
                foreach (KeywordInfo info2 in this._applicableKeywords)
                {
                    foreach (string str in info2.GetKeywords())
                    {
                        if (this.Keyword.StartsWith(str, StringComparison.Ordinal))
                        {
                            this._listBoxKeywords.SelectedItem = info2;
                            int length = str.Length;
                            if ((info2.SupportsValueIndex && (this.Keyword.Length > length)) && (this.Keyword[length] == 'Y'))
                            {
                                length++;
                                if ((this.Keyword.Length > length) && char.IsDigit(this.Keyword[length]))
                                {
                                    int num2 = int.Parse(this.Keyword.Substring(length, 1), CultureInfo.InvariantCulture);
                                    if ((num2 < 0) || (num2 > this._maxYValueIndex))
                                    {
                                        num2 = 0;
                                    }
                                    this._numericUpDownYValue.Value = num2;
                                    length++;
                                }
                            }
                            if ((info2.SupportsFormatting && (this.Keyword.Length > length)) && ((this.Keyword[length] == '{') && this.Keyword.EndsWith("}", StringComparison.Ordinal)))
                            {
                                string str2 = this.Keyword.Substring(length + 1, (this.Keyword.Length - length) - 2);
                                if (str2.Length == 0)
                                {
                                    this._comboBoxFormat.SelectedIndex = 0;
                                }
                                else if (((str2.Length == 1) || ((str2.Length == 2) && char.IsDigit(str2[1]))) || ((str2.Length == 3) && char.IsDigit(str2[2])))
                                {
                                    if (str2[0] == 'C')
                                    {
                                        this._comboBoxFormat.SelectedIndex = 1;
                                    }
                                    else if (str2[0] == 'D')
                                    {
                                        this._comboBoxFormat.SelectedIndex = 2;
                                    }
                                    else if (str2[0] == 'E')
                                    {
                                        this._comboBoxFormat.SelectedIndex = 3;
                                    }
                                    else if (str2[0] == 'F')
                                    {
                                        this._comboBoxFormat.SelectedIndex = 4;
                                    }
                                    else if (str2[0] == 'G')
                                    {
                                        this._comboBoxFormat.SelectedIndex = 5;
                                    }
                                    else if (str2[0] == 'N')
                                    {
                                        this._comboBoxFormat.SelectedIndex = 6;
                                    }
                                    else if (str2[0] == 'P')
                                    {
                                        this._comboBoxFormat.SelectedIndex = 7;
                                    }
                                    else
                                    {
                                        this._comboBoxFormat.SelectedIndex = 8;
                                        this._textBoxCustomFormat.Text = str2;
                                    }
                                    if ((this._comboBoxFormat.SelectedIndex != 8) && (str2.Length > 0))
                                    {
                                        this._textBoxPrecision.Text = str2.Substring(1);
                                    }
                                }
                                else
                                {
                                    this._comboBoxFormat.SelectedIndex = 8;
                                    this._textBoxCustomFormat.Text = str2;
                                }
                            }
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                    {
                        break;
                    }
                }
            }
        }

        private void listBoxKeywords_DoubleClick(object sender, EventArgs e)
        {
            base.AcceptButton.PerformClick();
        }

        private void listBoxKeywords_SelectedIndexChanged(object sender, EventArgs e)
        {
            KeywordInfo selectedItem = this._listBoxKeywords.SelectedItem as KeywordInfo;
            if (selectedItem != null)
            {
                this._labelDescription.Text = selectedItem.Description.Replace(@"\n", "\n");
                this._groupBoxFormat.Enabled = selectedItem.SupportsFormatting;
                this._labelYValue.Enabled = selectedItem.SupportsValueIndex;
                this._numericUpDownYValue.Enabled = selectedItem.SupportsValueIndex && (this._maxYValueIndex > 0);
                this._labelYValue.Enabled = selectedItem.SupportsValueIndex && (this._maxYValueIndex > 0);
            }
        }

        private void numericUpDownYValue_ValueChanged(object sender, EventArgs e)
        {
            if ((this._numericUpDownYValue.Value > this._maxYValueIndex) && (this._numericUpDownYValue.Value < 0M))
            {
                MessageBoxOptions options = 0;
                if (this.RightToLeft == RightToLeft.Yes)
                {
                    options = MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign;
                }
                //MessageBox.Show(this, SR.MessageYValueIndexInvalid(this._maxYValueIndex.ToString(CultureInfo.CurrentCulture)), /*SR.*/"MessageChartTitle", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, options);
                this._numericUpDownYValue.Value = 0M;
            }
        }

        private void textBoxCustomFormat_TextChanged(object sender, EventArgs e)
        {
            this.UpdateNumericSample();
        }

        private void textBoxPrecision_TextChanged(object sender, EventArgs e)
        {
            MessageBoxOptions options = 0;
            if (this.RightToLeft == RightToLeft.Yes)
            {
                options = MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign;
            }
            if ((this._textBoxPrecision.Text.Length >= 1) && !char.IsDigit(this._textBoxPrecision.Text[0]))
            {
                MessageBox.Show(this, /*SR.*/"MessagePrecisionInvalid", /*SR.*/"MessageChartTitle", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, options);
                this._textBoxPrecision.Text = "";
            }
            else if ((this._textBoxPrecision.Text.Length >= 2) && (!char.IsDigit(this._textBoxPrecision.Text[0]) || !char.IsDigit(this._textBoxPrecision.Text[1])))
            {
                MessageBox.Show(this, /*SR.*/"MessagePrecisionInvalid", /*SR.*/"MessageChartTitle", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, options);
                this._textBoxPrecision.Text = "";
            }
            this.UpdateNumericSample();
        }

        private void UpdateNumericSample()
        {
            string formatString = this.GetFormatString();
            if (this._comboBoxFormat.SelectedIndex == 0)
            {
                this._textBoxSample.Text = string.Empty;
            }
            else if (this._comboBoxFormat.SelectedIndex == 1)
            {
                this._textBoxSample.Text = string.Format(CultureInfo.CurrentCulture, "{0:" + formatString + "}", new object[] { 12345.6789 });
            }
            else if (this._comboBoxFormat.SelectedIndex == 2)
            {
                this._textBoxSample.Text = string.Format(CultureInfo.CurrentCulture, "{0:" + formatString + "}", new object[] { 0x3039 });
            }
            else if (this._comboBoxFormat.SelectedIndex == 3)
            {
                this._textBoxSample.Text = string.Format(CultureInfo.CurrentCulture, "{0:" + formatString + "}", new object[] { 12345.6789 });
            }
            else if (this._comboBoxFormat.SelectedIndex == 4)
            {
                this._textBoxSample.Text = string.Format(CultureInfo.CurrentCulture, "{0:" + formatString + "}", new object[] { 12345.6789 });
            }
            else if (this._comboBoxFormat.SelectedIndex == 5)
            {
                this._textBoxSample.Text = string.Format(CultureInfo.CurrentCulture, "{0:" + formatString + "}", new object[] { 12345.6789 });
            }
            else if (this._comboBoxFormat.SelectedIndex == 6)
            {
                this._textBoxSample.Text = string.Format(CultureInfo.CurrentCulture, "{0:" + formatString + "}", new object[] { 12345.6789 });
            }
            else if (this._comboBoxFormat.SelectedIndex == 7)
            {
                this._textBoxSample.Text = string.Format(CultureInfo.CurrentCulture, "{0:" + formatString + "}", new object[] { 0.126 });
            }
            else if (this._comboBoxFormat.SelectedIndex == 8)
            {
                bool flag = false;
                try
                {
                    this._textBoxSample.Text = string.Format(CultureInfo.CurrentCulture, "{0:" + formatString + "}", new object[] { 12345.6789 });
                    flag = true;
                }
                catch (FormatException)
                {
                }
                if (!flag)
                {
                    try
                    {
                        this._textBoxSample.Text = string.Format(CultureInfo.CurrentCulture, "{0:" + formatString + "}", new object[] { 0x3039 });
                        flag = true;
                    }
                    catch (FormatException)
                    {
                    }
                }
                if (!flag)
                {
                    this._textBoxSample.Text = /*SR.*/"DesciptionCustomLabelFormatInvalid";
                }
            }
        }
    }



}
