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

namespace PMS.Libraries.ToolControls.Report.Controls.EditorDialog
{
    public  partial class BorderSettingPage : UserControl
    {
        public List<ExternData> ExternDatas = new List<ExternData>();

        private ElementBorder _border = null;
        public virtual ElementBorder Border
        {
            get
            {
                return _border;
            }
        }

        protected string _styleName = string.Empty;
        [Browsable(true)]
        [Category("设计")]
        public string StyleName
        {
            get
            {
                return _styleName;
            }
            set
            {
                _styleName = value;
            }
        }

        public BorderSettingPage()
        {
            InitializeComponent();
        }
    }
}
