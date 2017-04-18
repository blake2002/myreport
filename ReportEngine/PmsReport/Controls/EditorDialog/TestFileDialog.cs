using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ControlLib.EditorDialog
{
    public partial class TestFileDialog : Form
    {
        private Point _editPoint;
        public Point EditPoint
        {
            get
            {
                return _editPoint;
            }
            set
            {
                _editPoint = value;
            }
        }

        public TestFileDialog(Point pt)
        {
            _editPoint = pt;
            InitializeComponent();
        }

      

        private void OkBtnClick(object sender, EventArgs e)
        {
            if (!CheckIsNumber(this.LeftTb.Text.Trim())
                || !CheckIsNumber(this.TopTb.Text.Trim()))
            {
                MessageBox.Show("输入不合法");
            }
            else
            {
                if (null != EditPoint)
                {
                    _editPoint.X = Convert.ToInt32(this.LeftTb.Text.Trim());
                    _editPoint.Y = Convert.ToInt32(this.TopTb.Text.Trim());
                }
                this.DialogResult = DialogResult.OK;
            }
        }

        private void LeftTb_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !CheckIsNumber(this.LeftTb.Text.Trim());
        }

        private void TopTb_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !CheckIsNumber(this.TopTb.Text.Trim());
        }

        private bool CheckIsNumber(string text)
        {
            try
            {
                Convert.ToInt32(text);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void Form_Closed(object sender, FormClosedEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }
    }
}
