using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PMS.Libraries.ToolControls.Report.Controls.EditorDialog
{
    public class LineItem:ICustomDrawItem
    {
        private DashStyle _dashStyle = DashStyle.Solid;
        public DashStyle DashStyle
        {
            get
            {
                return _dashStyle;
            }
        }

        public LineItem(DashStyle dashStyle)
        {
            _dashStyle = dashStyle;
        }

        public void Draw(Graphics g, float x, float y,float width,float height)
        {
            if (null != g)
            { 
                using(Pen pen = new Pen(Brushes.Black))
                {
                    pen.DashStyle = _dashStyle;
                    g.DrawLine(pen, x, y + height / 2, x + width, y + height / 2);
                }
            }
        }
    }
}
