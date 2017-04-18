using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace MESReportRunnerShell
{
    public partial class MESReportRunnerShellForm : Form
    {
        private string _fileFullPath = string.Empty;

        public MESReportRunnerShellForm()
        {
            InitializeComponent();
            PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentEnvironment = PMS.Libraries.ToolControls.PMSPublicInfo.MESEnvironment.MESReportRunner;
            this.mesReportViewer1.SetProjectPath(AppDomain.CurrentDomain.BaseDirectory);
            this.mesReportViewer1.SetToolBar(0, true);
        }

        /// <param name="args">
        /// args[0]--报表设计文件路径
        /// args[1]--数据库参数设置
        /// args[2]--设置报表变量值
        /// </param>
        public MESReportRunnerShellForm(string[] args)
        {
            InitializeComponent();
            PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentEnvironment = PMS.Libraries.ToolControls.PMSPublicInfo.MESEnvironment.MESReportRunner;
            this.mesReportViewer1.SetProjectPath(AppDomain.CurrentDomain.BaseDirectory);
            this.mesReportViewer1.SetToolBar(0, true);
            if (args.Count() == 0)
                return;
            if (!string.IsNullOrEmpty(args[0]) && File.Exists(args[0]))
            {
                _fileFullPath = args[0];

                this.mesReportViewer1.SetReport(args[0]);

                if (args.Count() == 1)
                    return;

                if (!string.IsNullOrEmpty(args[1]))
                {
                    string[] DSParas = args[1].Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    string strDSName = string.Empty;
                    foreach (string s in DSParas)
                    {
                        string[] ParaValuePair = s.Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        if (ParaValuePair.Count() == 2 && ParaValuePair[0].Trim() == "DSName")
                        {
                            strDSName = ParaValuePair[1].Trim();
                            //this.mesReportViewer1.SetParameter(ParaValuePair[0].Trim(), ParaValuePair[1].Trim());
                            break;
                        }
                    }
                    if(!string.IsNullOrEmpty(strDSName))
                    {
                        foreach (string s in DSParas)
                        {
                            string[] ParaValuePair = s.Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            if (ParaValuePair.Count() == 2 && ParaValuePair[0].Trim() != "DSName")
                            {
                                this.mesReportViewer1.SetDSParameter(strDSName, ParaValuePair[0].Trim(), ParaValuePair[1].Trim());
                            }
                        }
                    }
                }

                if (args.Count() == 2)
                    return;

                if (!string.IsNullOrEmpty(args[2]))
                {
                    string[] Paras = args[2].Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    foreach (string s in Paras)
                    {
                        string[] ParaValuePair = s.Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        if(ParaValuePair.Count() == 2)
                            this.mesReportViewer1.SetParameter(ParaValuePair[0].Trim(),ParaValuePair[1].Trim());
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (string.Compare(Path.GetExtension(_fileFullPath), ".orpt", true) != 0)
                this.mesReportViewer1.QueryReport();
        }

        const int HTLEFT = 10;
        const int HTRIGHT = 11;
        const int HTTOP = 12;
        const int HTTOPLEFT = 13;
        const int HTTOPRIGHT = 14;
        const int HTBOTTOM = 15;
        const int HTBOTTOMLEFT = 0x10;
        const int HTBOTTOMRIGHT = 17;

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0084:
                    base.WndProc(ref m);
                    //Point vPoint = new Point((int)m.LParam & 0xFFFF,
                    //    (int)m.LParam >> 16 & 0xFFFF);
                    //vPoint = PointToClient(vPoint);
                    //if (vPoint.X <= 5)
                    //    if (vPoint.Y <= 5)
                    //        m.Result = (IntPtr)HTTOPLEFT;
                    //    else if (vPoint.Y >= ClientSize.Height - 5)
                    //        m.Result = (IntPtr)HTBOTTOMLEFT;
                    //    else m.Result = (IntPtr)HTLEFT;
                    //else if (vPoint.X >= ClientSize.Width - 5)
                    //    if (vPoint.Y <= 5)
                    //        m.Result = (IntPtr)HTTOPRIGHT;
                    //    else if (vPoint.Y >= ClientSize.Height - 5)
                    //        m.Result = (IntPtr)HTBOTTOMRIGHT;
                    //    else m.Result = (IntPtr)HTRIGHT;
                    //else if (vPoint.Y <= 5)
                    //    m.Result = (IntPtr)HTTOP;
                    //else if (vPoint.Y >= ClientSize.Height - 5)
                    //    m.Result = (IntPtr)HTBOTTOM;
                    break;
                case 0x0201://鼠标左键按下的消息 
                    //m.Msg = 0x00A1;//更改消息为非客户区按下鼠标 
                    //m.LParam = IntPtr.Zero;//默认值 
                    //m.WParam = new IntPtr(2);//鼠标放在标题栏内 
                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey); 

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.O | Keys.Control:
                    OpenLocalFile();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void OpenLocalFile()
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "rpt files (*.rpt)|*.rpt|orpt files (*.orpt)|*.orpt|drpt files (*.drpt)|*.drpt";
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(openFileDialog1.FileName) && File.Exists(openFileDialog1.FileName))
                    {
                        this.mesReportViewer1.SetReport(openFileDialog1.FileName);
                    }

                    if (string.Compare(Path.GetExtension(_fileFullPath), ".orpt", true) != 0)
                        this.mesReportViewer1.QueryReport();
                }
            }
            catch (System.Exception ex)
            {

            }
        }

        private void MESReportRunnerShellForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                if (!string.IsNullOrEmpty(paths[0]) && File.Exists(paths[0]))
                {
                    string fileExt = Path.GetExtension(paths[0]);
                    if (string.Compare(fileExt, ".rpt", true) == 0 || string.Compare(fileExt, ".orpt", true) == 0 || string.Compare(fileExt, ".drpt", true) == 0)
                    {
                        e.Effect = DragDropEffects.Link;
                    }
                    else
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void MESReportRunnerShellForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (!string.IsNullOrEmpty(paths[0]) && File.Exists(paths[0]))
            {
                this.mesReportViewer1.SetReport(paths[0]);
                if (string.Compare(Path.GetExtension(paths[0]), ".orpt", true) != 0)
                    this.mesReportViewer1.QueryReport();
            }
        }
    }
}
