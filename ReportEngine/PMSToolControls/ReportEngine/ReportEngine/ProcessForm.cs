using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace NetSCADA.ReportEngine
{
    public partial class ProcessForm : Form
    { 
        private object _Tag;
        private DateTime _StartTime;
        public ProcessForm()
        { 
            _StartTime = DateTime.Now;
            _Tag = null;
            InitializeComponent();
        }

        public ProcessForm(object tag)
        {
            _StartTime = DateTime.Now;
            _Tag = tag;
            InitializeComponent();
        } 
       
        public void Close(bool isOK)
        {
            if (_Tag != null)
            {
                if (_Tag is ReportRuntime)
                {
                    ReportRuntime reportRuntime = _Tag as ReportRuntime;
                    reportRuntime.StopAnalyseReportThread();
                }
                else if (_Tag is ReportDrawing)
                {
                    ReportDrawing reportDrawing = _Tag as ReportDrawing;
                    reportDrawing.StopExportReportThread();
                }
            }

            try
            {
                timer1.Enabled = false;
                if (isOK)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                } 
            }
            catch (System.Exception ex)
            { 
            } 
        }
        private string _ReportLogMessage = "";
        public string ReportLogMessage
        {
            set
            {
                _ReportLogMessage = value;
            }
            get
            {
                return _ReportLogMessage;
            }
        }
        public void UpdateMessage()
        {
            richTextBox1.Text = ReportLogMessage;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        { 
            Close(false);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                UpdateMessage();
                if (progressBar1.Value >= progressBar1.Maximum)
                {
                    progressBar1.Value = 0;
                }
                progressBar1.Value = progressBar1.Value + 5;
                TimeSpan timeSpan = DateTime.Now - _StartTime;
                this.Text = (System.Convert.ToDateTime(timeSpan.ToString())).ToString("HH:mm:ss");
            }
            catch (System.Exception ex)
            {
                Close(false);
            }
        }
       
        private void ProcessForm_Load(object sender, EventArgs e)
        {
            if (_Tag == null )
            {
                return;
            }

            if (_Tag is ReportRuntime)
            {
                ReportRuntime reportRuntime = _Tag as ReportRuntime;
                
                reportRuntime.StartAnalyseReportThread();
            }
            else if (_Tag is ReportDrawing)
            {
                this.Visible = false;
                ReportDrawing reportDrawing = _Tag as ReportDrawing;
                reportDrawing.StartExportReportThread();
            }
            this.Height = 75;
        }

        private void OptionButton_Click(object sender, EventArgs e)
        {
            if (this.Height >= 185)
            {
                this.Height = 75;
            }
            else
            { 
                this.Height = 185;
            }
        }

        /*/
       private void ProcessForm_Paint(object sender, PaintEventArgs e)
       {
           Rectangle clientRectangle =this.ClientRectangle;// new Rectangle(new Point(this.ClientRectangle.Location.X-5,this.ClientRectangle.Location.Y-5, new Size(this.ClientRectangle.Width, CancelButton.Height - progressBar1.Height - 2));
           LinearGradientBrush b = new LinearGradientBrush(clientRectangle, Color.LightSteelBlue, Color.SteelBlue, 0f); //线性渐变
           e.Graphics.FillRectangle(b, clientRectangle); // 填充窗体
           b.Dispose(); // 释放资源
       }
       // 窗体的屏幕坐标
       private Point _FormPoint;
       // 鼠标光标的屏幕坐标
       private Point _MousePoint;
       // 响应鼠标移动，并移动窗口 
       private void ProcessForm_MouseMove(object sender, MouseEventArgs e)
       {
           if (Control.MouseButtons == MouseButtons.Left)
           {
               Point mousePos = Control.MousePosition;
               this.Location = this._FormPoint + (Size)mousePos - (Size)this._MousePoint;
           }
       }
       // 获取窗体的屏幕坐标和鼠标光标的位置（屏幕坐标）
       private void ProcessForm_MouseDown(object sender, MouseEventArgs e)
       {
           _FormPoint = this.Location;
           _MousePoint = Control.MousePosition;
       } 
        /*/

        
    }
}
