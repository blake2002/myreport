using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.Drawing;

namespace DesignerLib.ShapeControl
{
    public class Shape : Control, IShape
    {
        public Shape()
            : base()
        {
            //SetStyle(ControlStyles.UserPaint, true);
            //SetStyle(ControlStyles.Opaque, true);
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //base.CreateControl();

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        //开启 WS_EX_TRANSPARENT, 使控件支持透明
        //        cp.ExStyle |= 0x00000020;
        //        return cp;
        //    }
        //}

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Draw(e);
        }

        public virtual void Draw(PaintEventArgs e)
        {
            
        }
    }
}
