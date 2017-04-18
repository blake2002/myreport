using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace PMS.Libraries.ToolControls.Report.Controls.EditorDialog
{
    public partial class BorderCfgFrm : Form
    {
        public bool HasLeft
        {
            get;
            private set;
        }

        public bool HasTop
        {
            get;
            private set;
        }

        public bool HasRight
        {
            get;
            private set;
        }

        public bool HasBottom
        {
            get;
            private set;
        }

        public DashStyle DashStyle
        {
            get;
            private set;
        }

        public Color BorderColor
        {
            get;
            private set;
        }

        public BorderCfgFrm()
        {
            InitializeComponent();
            ListLineStyles();
            LineLb.OnSelectedItemChanged += new OnSelectedItemChanged(LineLb_OnSelectedItemChanged);
        }

        protected override void OnLoad(EventArgs e)
        {
            // 
            // groupBox1
            //  
            this.groupBox1.Controls.Add(this.groupBox2);
            
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LineLb);
            // 
            // LineLb
            // 
            this.LineLb.ItemHeight = 20;
            this.LineLb.ItemWidth = 80;
            this.LineLb.Location = new System.Drawing.Point(5, 11);
            this.LineLb.Name = "LineLb";
            this.LineLb.SelectedIndex = -1;
            this.LineLb.Size = new System.Drawing.Size(119, 128);
            this.LineLb.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.BorderCtrl);
            
            // 
            // BorderCtrl
            // 
            this.BorderCtrl.BackColor = System.Drawing.Color.Transparent;
            this.BorderCtrl.Location = new System.Drawing.Point(20, 15);
            this.BorderCtrl.Name = "BorderCtrl";
            this.BorderCtrl.Size = new System.Drawing.Size(151, 142);
            this.BorderCtrl.TabIndex = 2;

            base.OnLoad(e);
        }

        public void Bind(IBorder border)
        {
            if (null != border )
            {
                BorderCtrl.Bind(border);
                DashStyle = border.DashStyle;
                BorderColor = border.BorderColor;
                ColorCmb.SelectedItem = border.BorderColor;
            }
        }

        private void LineLb_OnSelectedItemChanged(object o, ItemEventArgs e)
        {
             LineItem li = e.Item as LineItem;
             if (null != li)
             {
                 BorderCtrl.DashStyle = li.DashStyle;
             }
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            HasLeft = BorderCtrl.HasLeft;
            HasTop = BorderCtrl.HasTop;
            HasRight = BorderCtrl.HasRight;
            HasBottom = BorderCtrl.HasBottom;
            DashStyle = BorderCtrl.DashStyle;
            BorderColor = BorderCtrl.BorderColor;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void CancleBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void ListLineStyles()
        {
            LineLb.AddItem(new LineItem(System.Drawing.Drawing2D.DashStyle.Dash));
            LineLb.AddItem(new LineItem(System.Drawing.Drawing2D.DashStyle.DashDot));
            LineLb.AddItem(new LineItem(System.Drawing.Drawing2D.DashStyle.DashDotDot));
            LineLb.AddItem(new LineItem(System.Drawing.Drawing2D.DashStyle.Dot));
            LineLb.AddItem(new LineItem(System.Drawing.Drawing2D.DashStyle.Solid));
        }

        private void NoneBordrBtn_Click(object sender, EventArgs e)
        {
            BorderCtrl.HasLeft = false;
            BorderCtrl.HasTop = false;
            BorderCtrl.HasRight = false;
            BorderCtrl.HasBottom = false;
            BorderCtrl.Invalidate();
        }

        private void AllBorderBtn_Click(object sender, EventArgs e)
        {
            BorderCtrl.HasLeft = true;
            BorderCtrl.HasTop = true;
            BorderCtrl.HasRight = true;
            BorderCtrl.HasBottom = true;
            BorderCtrl.Invalidate();
        }

        private void ColorCmb_SelectColorChanged(object sender, Color OldColor, Color NewColor)
        {
            BorderCtrl.BorderColor = NewColor;
            BorderCtrl.Invalidate();
        }
    }
}
