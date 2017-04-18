using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMS.Libraries.ToolControls.Report.Element;

namespace PMS.Libraries.ToolControls.Report.Controls.EditorDialog
{
    public partial class BorderEditorDialog : Form
    {
        protected ElementBorder _border = null;
        public ElementBorder Border
        {
            get
            {
                return _border;
            }
            set
            {
                _border = value;
            }
        }
        public BorderEditorDialog(ElementBorder border)
        {
            InitializeComponent();
            _border = border;
        }

        private void BorderFrameSettingFrmLoad(object sender, EventArgs e)
        {
            RectangleBorderSettingPage page = new RectangleBorderSettingPage(_border);
            page.Visible = false;
            this.BorderTypeCmb.Items.Add(page.StyleName);
            page.Dock = DockStyle.Fill;
            this.BodyPanel.Controls.Add(page);
            EllipseBorderSettingPage page1 = new EllipseBorderSettingPage(_border);
            page1.Visible = false;
            page1.Dock = DockStyle.Fill;
            this.BodyPanel.Controls.Add(page1);
            this.BorderTypeCmb.Items.Add(page1.StyleName);
            if (null != _border)
            {
                this.BorderTypeCmb.SelectedItem = _border.Name;
            }
        }

        private void BoderStyleSelectChanged(object sender, EventArgs e)
        {
            int index = this.BorderTypeCmb.SelectedIndex;
            for (int i = 0; i < this.BodyPanel.Controls.Count;i++ )
            {
                Control ctrl = this.BodyPanel.Controls[i];
                if (i == index)
                {
                    ctrl.Visible = true;
                    continue;
                }
                if (ctrl.Visible)
                {
                    ctrl.Visible = false;
                }
            }
        }

        private void OkBtnClick(object sender, EventArgs e)
        {
            int index = this.BorderTypeCmb.SelectedIndex;
            BorderSettingPage ctrl = this.BodyPanel.Controls[index] as BorderSettingPage;
            if (null != ctrl)
            {
                _border = ctrl.Border;
                if (null != _border.OwnerElement)
                {
                    _border.OwnerElement.ExternDatas = ctrl.ExternDatas;
                }
            }
            this.DialogResult = DialogResult.OK;
        }

        private void CancleBtnClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
