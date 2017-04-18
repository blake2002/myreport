using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PMS.Libraries.ToolControls.PmsSheet.PmsPublicData
{
    public partial class SqlEditorControl : UserControl
    {

        public event OKEventHander OKEvent;
        public delegate void OKEventHander();

        public event CancelEventHander CancelEvent;
        public delegate void CancelEventHander();

        public SqlEditorControl()
        {
            InitializeComponent();
        }

        public string SqlText
        {
            get
            {
                return this.richTextBoxParser1.Text;
            }
            set
            {
                this.richTextBoxParser1.Text = value;
            }
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            if (null != OKEvent)
                OKEvent();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            if (null != CancelEvent)
                CancelEvent();
        }
    }
}
