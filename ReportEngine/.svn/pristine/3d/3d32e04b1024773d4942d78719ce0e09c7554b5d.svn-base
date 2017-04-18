using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMS.Libraries.ToolControls.Report.Element;
using System.ComponentModel;
using System.Drawing;

namespace PmsReport.Controls
{
    public class MyTestPanel:ElementPanel
    {
        private bool _hasBorder = true;
        [Browsable(true)]
        [Category("PMS控件属性")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(false)]
        public override bool HasBorder
        {
            get
            {
                return _hasBorder;
            }
            set
            {
                _hasBorder = value;
                Invalidate();
            }
        }

        //private Color _borderColor = Color.White;
        //[Browsable(true)]
        //[Category("PMS控件属性")]
        //[EditorBrowsable(EditorBrowsableState.Always)]
        //public override Color BorderColor
        //{
        //    get
        //    {
        //        return _borderColor;
        //    }
        //    set
        //    {
        //        _borderColor = value;
        //        Invalidate();
        //    }
        //}
        public override ElementBorder Border
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

        public MyTestPanel() 
        {
            Border = new RectangleBorder(this);
        }

        public override void Print(Canvas ca, float x, float y)
        {
            base.Print(ca, x, y);
        }

       
    }
}
