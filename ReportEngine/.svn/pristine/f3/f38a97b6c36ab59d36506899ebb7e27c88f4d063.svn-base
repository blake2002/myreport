using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.Design.Behavior;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace MES.Controls.Design
{
    public class MESBehavior:MyBehavior
    {
       private Control _ctrl = null;
       private BehaviorService _behaviorService = null;
       private Point _mouseLocation = Point.Empty;
       public MESBehavior(Control ctrl, bool callParent, BehaviorService bs)
            : base(callParent, bs)
        {
            _ctrl = ctrl;
            _behaviorService = bs;
        }

		public override Cursor Cursor {
			get {
				return Cursors.Default;
			}
		}

        public override void OnDragDrop(Glyph g, System.Windows.Forms.DragEventArgs e)
        {
            base.OnDragDrop(g, e);
        }

        public override void OnDragEnter(Glyph g, System.Windows.Forms.DragEventArgs e)
        {
            base.OnDragEnter(g, e);
        }

        public override void OnDragLeave(Glyph g, EventArgs e)
        {   
            base.OnDragLeave(g, e);
        }

        public override void OnDragOver(Glyph g, System.Windows.Forms.DragEventArgs e)
        {
            base.OnDragOver(g, e);
        }

        public override void OnGiveFeedback(Glyph g, System.Windows.Forms.GiveFeedbackEventArgs e)
        {
            base.OnGiveFeedback(g, e);
        }

        public override void OnLoseCapture(Glyph g, EventArgs e)
        {
            base.OnLoseCapture(g, e);
        }

        public override bool OnMouseDoubleClick(Glyph g, System.Windows.Forms.MouseButtons button, System.Drawing.Point mouseLoc)
        {
            return base.OnMouseDoubleClick(g, button, mouseLoc);
        }

        public override bool OnMouseDown(Glyph g, System.Windows.Forms.MouseButtons button, System.Drawing.Point mouseLoc)
        {
            
            //MESGlyph mesGlyph = g as MESGlyph;
            //if (null != mesGlyph)
            //{

            //    if (null != _ctrl)
            //    {
            //        IDesignEdit de = _ctrl as IDesignEdit;
            //        if (null != de)
            //        {
            //            if (de.IsEdit)
            //            {
            //                de.EndEdit();
            //            }
            //        }
            //    }
                //if (mesGlyph.AdornerMouseDown(button, mouseLoc))
                //{
                //    return true;
                //}
            //}
            _mouseLocation = mouseLoc;
            return base.OnMouseDown(g, button, mouseLoc);
        }

        public override bool OnMouseEnter(Glyph g)
        {
            return base.OnMouseEnter(g);
        }

        public override bool OnMouseHover(Glyph g, System.Drawing.Point mouseLoc)
        {
            return base.OnMouseHover(g, mouseLoc);
        }

        public override bool OnMouseLeave(Glyph g)
        {
            if (null == _ctrl.Site)
            {
                return false;
            }
            MESGlyph mesGlyph = g as MESGlyph;
            if (null != mesGlyph)
            {
                if (mesGlyph.AdornerMouseLeave())
                {
                    return true;
                }
            }
            return base.OnMouseLeave(g);
        }

        public override bool OnMouseMove(Glyph g, System.Windows.Forms.MouseButtons button, System.Drawing.Point mouseLoc)
        {
            MESGlyph mesGlyph = g as MESGlyph;
            if (null != mesGlyph)
            {
                if (mesGlyph.AdornerMouseMove(button, mouseLoc))
                {
                    return true;
                }
            }
            return base.OnMouseMove(g, button, mouseLoc);
        }

        public override bool OnMouseUp(Glyph g, System.Windows.Forms.MouseButtons button)
        {
            MESGlyph mesGlyph = g as MESGlyph;
            if (null != mesGlyph)
            {

                if (null != _ctrl)
                {
                    IDesignEdit de = _ctrl as IDesignEdit;
                    if (null != de)
                    {
                        if (de.IsEdit)
                        {
                            de.EndEdit();
                        }
                    }
                }

                if (_mouseLocation != Point.Empty)
                {
                    if (mesGlyph.AdornerMouseUp(button, _mouseLocation))
                    {
                        _mouseLocation = Point.Empty;
                        return true;
                    }
                }
                
            }
            return base.OnMouseUp(g, button);
        }

         

        public override void OnQueryContinueDrag(Glyph g, System.Windows.Forms.QueryContinueDragEventArgs e)
        {
            base.OnQueryContinueDrag(g, e);
        }

        /// <summary>
        /// 将designSurface上的坐标
        /// 转到当前设计器上操作上的控件的坐标
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        private Point PointFromDesignSurfaceToControl(Point pt)
        {
            if (null == _ctrl.Site)
            {
                return pt;
            }
            DesignSurface ds = _ctrl.Site.GetService(typeof(DesignSurface)) as DesignSurface;
            Control view = ds.View as Control;
            if (null != view)
            {
                pt = view.PointToScreen(pt);
            }
            pt = _ctrl.PointToClient(pt);
            return pt;
        }
    }
}
