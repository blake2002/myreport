using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMS.Libraries.ToolControls.Report.Element;
using PMS.Libraries.ToolControls.Report.Elements.Util;
using System.Drawing.Drawing2D;
using PMS.Libraries.ToolControls.ReportControls;

namespace PMS.Libraries.ToolControls.Report.Controls.EditorDialog
{
    public partial class RectangleBorderSettingPage : BorderSettingPage
    {
        protected RectangleBorder _border = null;
        public override ElementBorder Border
        {
            get
            {
                SetBorder();
                if (!CheckIfChanged())
                {
                    return _originalBorder;
                }
                return _border;
            }
        }

        private RectangleBorder _originalBorder = null; 

        public RectangleBorderSettingPage(ElementBorder border)
        {
            InitializeComponent();
            RegiterLineType();
            RectangleBorder temp = border as RectangleBorder;
            if (null == _border)
            {
                _border = new RectangleBorder(null);
                _border.OwnerElement = border.OwnerElement;
                if (null != temp)
                {
                    _originalBorder = temp;
                    //边框属性的赋值
                    //RectangleBorder dispalyBorder = this.displayBorder.Border as RectangleBorder;
                    _border.HasLeftBorder = temp.HasLeftBorder;
                    displayBorder.HasLeft = _border.HasLeftBorder;
                    //this.LeftBorderCb.Checked = _border.HasLeftBorder;
                    _border.HasBottomBorder = temp.HasBottomBorder;
                    //this.BottomBorderCb.Checked = _border.HasBottomBorder;
                    displayBorder.HasBottom = _border.HasBottomBorder;
                    _border.HasRightBorder = temp.HasRightBorder;
                    //this.RightBorderCb.Checked = _border.HasRightBorder;
                    displayBorder.HasRight = _border.HasRightBorder;
                    _border.HasTopBorder = temp.HasTopBorder;
                    //this.TopBorderCb.Checked = _border.HasTopBorder;
                    displayBorder.HasTop = _border.HasTopBorder;
                    _border.MoveX = temp.MoveX;
                    _border.MoveY = temp.MoveY;
                    if (null != _border.OwnerElement)
                    {
                        ExternData ed = new ExternData("HasLeftBorder", _border.HasLeftBorder);
                        int index = ChecExistkExternDatas(ExternDatas, ed);
                        if (index > -1)
                        {
                            ExternDatas[index] = ed;
                        }
                        else
                        {
                            ExternDatas.Add(ed);
                        }
                        _border.OwnerElement.ExternDatas.Add(ed);

                        ed = new ExternData("HasBottomBorder", _border.HasBottomBorder);
                        index = ChecExistkExternDatas(ExternDatas, ed);
                        if (index > -1)
                        {
                            ExternDatas[index] = ed;
                        }
                        else
                        {
                            ExternDatas.Add(ed);
                        }

                        ed = new ExternData("HasRightBorder", _border.HasRightBorder);
                        index = ChecExistkExternDatas(ExternDatas, ed);
                        if (index > -1)
                        {
                            ExternDatas[index] = ed;
                        }
                        else
                        {
                            ExternDatas.Add(ed);
                        }

                        ed = new ExternData("HasTopBorder", _border.HasTopBorder);
                        index = ChecExistkExternDatas(ExternDatas, ed);
                        if (index > -1)
                        {
                            ExternDatas[index] = ed;
                        }
                        else
                        {
                            ExternDatas.Add(ed);
                        }

                    }
                }
            }
            this.StyleName = _border.Name;
            if (null != border)
            {
                this.BorderWidthTb.Value = Convert.ToDecimal(border.BorderWidth);
                this.displayBorder.BorderColor = border.BorderColor;
                this.BorderColorLabel.BackColor = border.BorderColor;
                _border.BorderColor = border.BorderColor;
                _border.BorderWidth = border.BorderWidth;
                if (this.LineTypeCmb.Items.Count > 0)
                {
                    try
                    {
                        this.LineTypeCmb.SelectedValue = border.DashStyle;
                    }
                    catch
                    { 
                    }
                }
            }
            this.displayBorder.Invalidate();
        }

        

        private void SetBorder()
        {
            if (null != _border)
            {
                //if (sender.Equals(this.LeftBorderCb))
                {
                    _border.HasLeftBorder = displayBorder.HasLeft;
                   // ((RectangleBorder)(this.displayBorder.Border)).HasLeftBorder = this.LeftBorderCb.Checked;
                    ExternData ed = new ExternData("HasLeftBorder", _border.HasLeftBorder);
                    int index = ChecExistkExternDatas(ExternDatas, ed);
                    if (index > -1)
                    {
                        ExternDatas[index] = ed;
                    }
                    else
                    {
                        ExternDatas.Add(ed);
                    }
                }
                //else if (sender.Equals(this.RightBorderCb))
                {
                    _border.HasRightBorder = displayBorder.HasRight;
                    //((RectangleBorder)(this.displayBorder.Border)).HasRightBorder = this.RightBorderCb.Checked;
                    ExternData ed = new ExternData("HasRightBorder", _border.HasRightBorder);
                    int index = ChecExistkExternDatas(ExternDatas, ed);
                    if (index > -1)
                    {
                        ExternDatas[index] = ed;
                    }
                    else
                    {
                        ExternDatas.Add(ed);
                    }
                }
                //else if (sender.Equals(this.TopBorderCb))
                {
                    _border.HasTopBorder = displayBorder.HasTop;
                    //((RectangleBorder)(this.displayBorder.Border)).HasTopBorder = this.TopBorderCb.Checked;
                    ExternData ed = new ExternData("HasTopBorder", _border.HasTopBorder);
                    int index = ChecExistkExternDatas(ExternDatas, ed);
                    if (index > -1)
                    {
                        ExternDatas[index] = ed;
                    }
                    else
                    {
                        ExternDatas.Add(ed);
                    }
                }
                //else
                {
                    _border.HasBottomBorder = this.displayBorder.HasBottom;
                    //((RectangleBorder)(this.displayBorder.Border)).HasBottomBorder = this.BottomBorderCb.Checked;
                    ExternData ed = new ExternData("HasBottomBorder", _border.HasBottomBorder);
                    int index = ChecExistkExternDatas(ExternDatas, ed);
                    if (index > -1)
                    {
                        ExternDatas[index] = ed;
                    }
                    else
                    {
                        ExternDatas.Add(ed);
                    }
                }

            }
        }

        private void BorderWidthValueChanged(object sender, EventArgs e)
        {
            this.displayBorder.BorderWidth = (int)this.BorderWidthTb.Value;
            _border.BorderWidth = (int)this.BorderWidthTb.Value;
            displayBorder.Invalidate();
        }

        private void ColrBtnClick(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (DialogResult.OK == cd.ShowDialog())
            {
                displayBorder.BorderColor = cd.Color;
                this.BorderColorLabel.BackColor = cd.Color;
                this.BorderColorLabel.Text = string.Empty;
                _border.BorderColor = cd.Color;
                displayBorder.Invalidate();
            }
        }

        private int ChecExistkExternDatas(IList<ExternData> list, ExternData ed)
        {
            if (null != list)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (ed.Equals(list[i]))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        private void RegiterLineType()
        {
            this.LineTypeCmb.DisplayMember = "Name";
            this.LineTypeCmb.ValueMember = "DashStyle";
            List<Line> list = new List<Line>();
            Line lt = new Line("实体线", DashStyle.Solid);
            list.Add(lt);
            lt = new Line("点线", DashStyle.Dot);
            list.Add(lt);
            lt = new Line("线段组成的直线", DashStyle.Dash);
            list.Add(lt);
            lt = new Line("划线点图案构成的直线", DashStyle.DashDot);
            list.Add(lt);
            lt = new Line("划线点点图案构成的直线", DashStyle.DashDotDot);
            list.Add(lt);
            lt = new Line("自定义", DashStyle.Custom);
            list.Add(lt);
            this.LineTypeCmb.DataSource = list;
        }

        private void LineTypeCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.displayBorder.DashStyle = (DashStyle)this.LineTypeCmb.SelectedValue;
            if (null != _border)
            {
                _border.DashStyle = (DashStyle)this.LineTypeCmb.SelectedValue;
            }
            displayBorder.Invalidate();
        }

        private bool CheckIfChanged()
        {
            if (null != _border && null != _originalBorder 
                    && _border.Name == _originalBorder.Name)
            { 
                return (_border.BorderWidth != _originalBorder.BorderWidth)
                    || (_border.BorderColor != _originalBorder.BorderColor) 
                    || (_border.DashStyle != _originalBorder.DashStyle)
                    || (_border.HasBottomBorder != _originalBorder.HasBottomBorder)
                    || (_border.HasLeftBorder != _originalBorder.HasLeftBorder)
                    || (_border.HasRightBorder != _originalBorder.HasRightBorder)
                    || (_border.HasTopBorder != _originalBorder.HasTopBorder)
                    || (_border.HorizontalScale != _originalBorder.HorizontalScale)
                    || (_border.VerticalScale != _originalBorder.VerticalScale);
            }
            return true;
        }
    }

    internal class LineType
    {
        public string Name
        {
            get;
            set;
        }
        public DashStyle DashStyle
        {
            get;
            set;
        }

        public LineType(string name, DashStyle ds)
        {
            this.Name = name;
            this.DashStyle = ds;
        }
    }
}
