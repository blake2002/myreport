using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMS.Libraries.ToolControls.Report.Element;
using System.Drawing.Drawing2D;
using PMS.Libraries.ToolControls.ReportControls;

namespace PMS.Libraries.ToolControls.Report.Controls.EditorDialog
{
    public partial class EllipseBorderSettingPage : BorderSettingPage
    {
        protected EllipseBorder _border = null;
        public override ElementBorder Border
        {
            get
            {
                if (!CheckIfChanged())
                {
                    return _originalBorder;
                }
                return _border;
            }
        }
        private ElementBorder _originalBorder = null; 
        public EllipseBorderSettingPage(ElementBorder border):base()
        {
            InitializeComponent();
            RegiterLineType();
            if (null == _border)
            {
                _border = new EllipseBorder(null);
                if (null != border)
                {
                    _originalBorder = border;
                    _border.OwnerElement = border.OwnerElement;
                    _border.BorderWidth = border.BorderWidth;
                    _border.BorderColor = border.BorderColor;
                }
            }
            this.StyleName = this.ellipseDisplayBorder.Border.Name;
            if (null != border)
            {
                BorderWidthTb.Value = Convert.ToDecimal(border.BorderWidth);
                BorderColorLabel.BackColor = border.BorderColor;
                this.ellipseDisplayBorder.Border.BorderColor = border.BorderColor;
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
            this.ellipseDisplayBorder.Invalidate();
        }

        private void BorderWidthValueChanged(object sender, EventArgs e)
        {
            this.ellipseDisplayBorder.Border.BorderWidth = (int)this.BorderWidthTb.Value;
            _border.BorderWidth = (int)this.BorderWidthTb.Value;
        }

        private void BorderColorClick(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (DialogResult.OK == cd.ShowDialog())
            {
                this.BorderColorLabel.BackColor = cd.Color;
                this.ellipseDisplayBorder.Border.BorderColor = cd.Color;
                this.ellipseDisplayBorder.Text = string.Empty;
                _border.BorderColor = cd.Color;
            }
        }

        private void LineTypeCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ellipseDisplayBorder.Border.DashStyle = (DashStyle)this.LineTypeCmb.SelectedValue;
            if (null != _border)
            {
                _border.DashStyle = (DashStyle)this.LineTypeCmb.SelectedValue;
            }
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
            this.LineTypeCmb.DataSource = list;
        }

        private bool CheckIfChanged()
        {
            if (null != _border && null != _originalBorder
                    && _border.Name == _originalBorder.Name)
            {
                return (_border.BorderWidth != _originalBorder.BorderWidth)
                    || (_border.BorderColor != _originalBorder.BorderColor)
                    || (_border.DashStyle != _originalBorder.DashStyle)
                    || (_border.HorizontalScale != _originalBorder.HorizontalScale)
                    || (_border.VerticalScale != _originalBorder.VerticalScale);
            }
            return true;
        }
    }
}
