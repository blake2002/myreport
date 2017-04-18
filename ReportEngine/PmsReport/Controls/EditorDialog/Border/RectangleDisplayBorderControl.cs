using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMS.Libraries.ToolControls.Report.Element;
using System.ComponentModel;
using System.Drawing;

namespace PMS.Libraries.ToolControls.Report.Controls.EditorDialog 
{
    public class RectangleDisplayBorderControl:ElementBase
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



        public RectangleDisplayBorderControl()
            : base()
        {
            Border = new RectangleBorder(this);
        }

        public override void Print(Canvas ca, float x, float y)
        {
            Border.Print(ca, x, y);
        }

    }
}
