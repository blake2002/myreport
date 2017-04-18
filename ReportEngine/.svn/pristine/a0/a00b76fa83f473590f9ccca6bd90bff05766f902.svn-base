using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;

namespace DesignerLib.ShapeControl
{
    public class ShapeDesigner : ControlDesigner
    {
        // This boolean state reflects whether the mouse is over the control. 
        private bool mouseover = false;
        public bool Mouseover
        {
            get { return mouseover; }
            set { mouseover = value; }
        }

        private Shape _Shape = null;
        private DesignerVerb ClipRegion = null;
        private DesignerActionListCollection _Lists;

        public ShapeDesigner()
        {
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);

            // Get clock control shortcut reference
            _Shape = (Shape)component;
        }

        //protected override bool GetHitTest(System.Drawing.Point point)
        //{
        //    point = _Shape.PointToClient(point);
        //    _Shape.CenterPtTracker.IsActive = _Shape.CenterPtTracker.TrackerRectangle.Contains(point);
        //    _Shape.FocusPtTracker.IsActive = _Shape.FocusPtTracker.TrackerRectangle.Contains(point);
        //    return _Shape.CenterPtTracker.IsActive | _Shape.FocusPtTracker.IsActive;
        //}

        protected override void OnMouseEnter()
        {
            base.OnMouseEnter();
            mouseover = true;
            _Shape.Invalidate();
        }

        protected override void OnMouseLeave()
        {
            base.OnMouseLeave();
            mouseover = false;
            _Shape.Invalidate();
        }

        protected override void OnPaintAdornments(System.Windows.Forms.PaintEventArgs pe)
        {
            base.OnPaintAdornments(pe);

            if (null != this.Control)
            {
                if (this.Control is IShape)
                    (this.Control as IShape).Draw(pe);
            }
        }
    }
}
