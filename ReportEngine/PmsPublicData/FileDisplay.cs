using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PMS.Libraries.ToolControls.PmsSheet.PmsPublicData
{
    internal partial class FileDisplay : UserControl
    {
        public FileDisplay()
        {
            InitializeComponent();
            _bRunMode = false;
        }

        private bool _bRunMode;
        public bool BRunMode
        {
            get { return _bRunMode; }
            set { _bRunMode = value; }
        }

        private string _url;
        public string Url
        {
            get { return _url; }
            set
            {
                _url = value;
                webBrowser1.Navigate(_url);
            }
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
        private bool _loadUnload;
        public bool IsOperable
        {
            get { return this._loadUnload; }
            set
            {
                this._loadUnload = value;
                //下部45
                if (_loadUnload)
                {
                    splitContainer1.IsSplitterFixed = true;
                    splitContainer1.FixedPanel = FixedPanel.Panel2;
                    int height = splitContainer1.Size.Height - 45;
                    int n1 = this.Size.Height;
                    splitContainer1.SplitterDistance = height > 0 ? height : 0;
                }
                else
                {
                    int n1 = this.Size.Height;
                    splitContainer1.IsSplitterFixed = false;
                    splitContainer1.FixedPanel = FixedPanel.None;
                    splitContainer1.SplitterDistance = splitContainer1.Size.Height;
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
        private void FileDisplay_MouseEnter(object sender, EventArgs e)
        {
            this.splitContainer1.SplitterDistance = this.splitContainer1.Height - 100;
        }

        private void FileDisplay_MouseLeave(object sender, EventArgs e)
        {
            this.splitContainer1.SplitterDistance = this.splitContainer1.Height;
        }

        private void FileDisplay_Load(object sender, EventArgs e)
        {
            if (_bRunMode == true)
            {
                this.splitContainer1.SplitterDistance = this.splitContainer1.Height;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog(this) == DialogResult.OK)
            {
                Url = fd.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog fd = new SaveFileDialog();
            string strFileType = "";
            int dotPos = Url.LastIndexOf('.');
            if (dotPos > 0)
            {
                strFileType = Url.Substring(dotPos + 1, Url.Length - dotPos - 1);
            }
            if (strFileType.Length > 0)
            {
                fd.DefaultExt = strFileType;
                fd.Filter = strFileType + "文件|*." + strFileType;
            }
            if (fd.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    File.Copy(Url, fd.FileName, true);
                }
                catch(Exception ex)
                {
                    PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID, "保存文件失败:" + ex.Message.ToString() + ex.GetBaseException().ToString(), true);
                    MessageBox.Show("保存失败！");
                }
            }
        }

        private void splitContainer1_SizeChanged(object sender, EventArgs e)
        {

            int distance = splitContainer1.Height - 40;
            if(distance<0)
            {
                splitContainer1.Height = 50;
                distance = 10;
            }
            splitContainer1.SplitterDistance = distance;
        }
    }
}
