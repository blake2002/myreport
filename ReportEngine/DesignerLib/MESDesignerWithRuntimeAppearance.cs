using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.Design;
using System.Windows.Forms.Design.Behavior;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

namespace MES.Controls.Design
{
    public class MESDesignerWithRuntimeAppearance : MESDesigner
    {
        protected override bool GetHitTest(System.Drawing.Point point)
        {
            point = Control.PointToClient(point);
            if (object.ReferenceEquals(_selectionService.PrimarySelection, Control))
            {
                if (Control.ClientRectangle.Contains(point))
                {
                    if (!(point.X <= 5 && point.Y <= 5 && point.X >= 0 && point.Y >= 0))
                    {
                        return true;
                    }
                }
            }
            return base.GetHitTest(point);
        }
    }
}
