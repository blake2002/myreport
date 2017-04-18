using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.Design.Behavior;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Drawing;
using System.Reflection;

namespace MES.Controls.Design
{
    public class MESGlyph : Glyph,IDisposable
    {
        private Control _ctrl = null;

        private ISelectionService _selectionService = null;

        public Adorner Adorner
        {
            get;
            private set;
        }

        private Rectangle _clipRectangle = Rectangle.Empty;

        private AdornerPanel _adornerPanel = null;

        private BehaviorService _behaviorService = null;

        private EventHandler _selectHandler = null;

        public MESGlyph(Adorner adorner, ISelectionService selectionService, Control ctrl, BehaviorService bs, Behavior bh)
            : base(bh)
        {
            _ctrl = ctrl;
            _selectHandler = new EventHandler(SelectionService_SelectionChanged);
            Adorner = adorner;
            _selectionService = selectionService;
            if (null != _selectionService)
            {
                selectionService.SelectionChanged += _selectHandler;
            }
            _behaviorService = bs;

            if (null == _adornerPanel)
            {
                _adornerPanel = new AdornerPanel(_ctrl);
            }
            if (null != _ctrl)
            {
                _ctrl.SizeChanged += new EventHandler(Ctrl_SizeChanged);
                _ctrl.LocationChanged += new EventHandler(Ctrl_LocationChanged);
            }
        }

        private void Ctrl_LocationChanged(object sender, EventArgs e)
        {
            ValidateAdorner();
        }

        private void Ctrl_SizeChanged(object sender, EventArgs e)
        {
            ValidateAdorner();
        }

        private void ValidateAdorner()
        {
            if (null != _adornerPanel)
            {
                if (null != _behaviorService)
                {
                    if (_clipRectangle != Rectangle.Empty)
                    {
                        Graphics g = _behaviorService.AdornerWindowGraphics;

                        _behaviorService.Invalidate(new Rectangle(_adornerPanel.Location.X,
                                                             _adornerPanel.Location.Y,
                                                             _adornerPanel.PrevWidth,
                                                             AdornerPanel.PanelHeight));
                        //Paint(new PaintEventArgs(g,_clipRectangle));
                    }
                }
            }
        }

        public void Dispose()
        {
            if (null != _adornerPanel)
            {
                _adornerPanel.Dispose();
                if (null != _selectionService)
                {
                    _selectionService.SelectionChanged -= _selectHandler;
                }
            }
        }

        private void SelectionService_SelectionChanged(object sender, EventArgs e)
        {
            if (object.ReferenceEquals(_selectionService.PrimarySelection, _ctrl))
            {
                Adorner.Enabled = true;
            }
            else
            {
                Adorner.Enabled = false;
                if (null != _ctrl && null != _ctrl.Site)
                {
                    MethodInfo mi = _ctrl.GetType().GetMethod("OnLeave",
                                                               BindingFlags.Public |
                                                               BindingFlags.NonPublic |
                                                               BindingFlags.Instance);
                    mi.Invoke(_ctrl, new object[] { null});
                }
            }
        }

        public override System.Windows.Forms.Cursor GetHitTest(System.Drawing.Point p)
        {
            if (null == _ctrl.Site)
            {
                return null;
            }
            if (null == _adornerPanel)
            {
                return null;
            }

            if (object.ReferenceEquals(_selectionService.PrimarySelection, _ctrl))
            {
                DesignSurface ds = _ctrl.Site.GetService(typeof(DesignSurface)) as DesignSurface;
                Control view = ds.View as Control;
                Point point = p;
                if (null != view)
                {
                    Point scPt = GetScrollPoint();
                    point.X -= scPt.X;
                    point.Y -= scPt.Y;
                    point = view.PointToScreen(point);
                }
                point = _ctrl.PointToClient(point);
                if (point.X >= 0 && point.Y > -1 * AdornerPanel.PanelHeight 
                    && point.Y <= 0 && point.X < _ctrl.Width)
                {
                    return Cursors.Hand;
                }
            }
            return null;
        }

        public override void Paint(PaintEventArgs pe)
        {
            if (null == _ctrl.Site)
            {
                return;
            }

            if (null == _adornerPanel)
            {
                return ;
            }

            if (object.ReferenceEquals(_selectionService.PrimarySelection, _ctrl))
            {
                if (_ctrl.Parent == null)
                    return;
                Point pt = _ctrl.Parent.PointToScreen(_ctrl.Location);
                DesignSurface ds = _ctrl.Site.GetService(typeof(DesignSurface)) as DesignSurface;
                Control view = ds.View as Control;
                if (null != view)
                {
                    pt = view.PointToClient(pt);
                }
                Point scPt = GetScrollPoint();
                pt.X += scPt.X;
                pt.Y += scPt.Y;
                pt = new Point(pt.X, pt.Y - AdornerPanel.PanelHeight);

                if (_adornerPanel.ItemList.Count == 0)
                {
                    ISuspensionable supspension = _ctrl as ISuspensionable;
                    if (null != supspension)
                    {
                    
                        SuspensionItem[] items = supspension.ListSuspensionItems();
                        for (int i = 0; i < items.Length; i++)
                        {
                            if (null != items[i])
                            {
                                _adornerPanel.AddItem(items[i]);
                            }
                        }
                    }
                }
                _adornerPanel.Location = pt;
                _adornerPanel.Paint(pe.Graphics);
                _clipRectangle = pe.ClipRectangle;
            }
        }

        public Point GetScrollPoint()
        {
            IDesignerHost host = _ctrl.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
            if (null != host && null != host.RootComponent && host.RootComponent is Control)
            {
                Control ctrl = host.RootComponent as Control;
                if (null != ctrl.Parent)
                {
                    if (ctrl.Parent is ScrollableControl)
                    {
                        Point pt = new Point(0,0);
                        ScrollableControl sc = ctrl.Parent as ScrollableControl;
                        if (sc.HorizontalScroll.Visible)
                        {
                            pt.X = sc.HorizontalScroll.Value;
                        }
                        if (sc.VerticalScroll.Visible)
                        {
                            pt.Y = sc.VerticalScroll.Value;
                        }
                        return pt;
                    }
                }
            }
            return Point.Empty;
        }

        public bool IsGlyphHitTest(Point pt)
        {
            return false;
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

        public bool AdornerMouseDown(System.Windows.Forms.MouseButtons button, System.Drawing.Point mouseLoc)
        {
            if (null == _adornerPanel)
            {
                return false;
            }

            Point pt = new Point(mouseLoc.X - _adornerPanel.Location.X, mouseLoc.Y - _adornerPanel.Location.Y);
            if (pt.X >= 0 && pt.Y >= 0 && pt.X < _adornerPanel.Width && pt.Y < AdornerPanel.PanelHeight)
            {
                if (_adornerPanel.RaiseMouseDown(button, pt))
                {
                    RefreshAdorner();
                    return true;
                }
            }
            return false;
        }

        public bool AdornerMouseUp(System.Windows.Forms.MouseButtons button, System.Drawing.Point mouseLoc)
        {
            if (null == _adornerPanel)
            {
                return false;
            }

            Point pt = new Point(mouseLoc.X - _adornerPanel.Location.X, mouseLoc.Y - _adornerPanel.Location.Y);
            if (pt.X >= 0 && pt.Y >= 0 && pt.X < _adornerPanel.Width && pt.Y < AdornerPanel.PanelHeight)
            {
                if (_adornerPanel.RaiseMouseUp(button, pt))
                {
                    RefreshAdorner();
                    return true;
                }
            }
            return false;
        }

        public bool AdornerMouseMove(System.Windows.Forms.MouseButtons button, System.Drawing.Point mouseLoc)
        {
            if (null == _adornerPanel)
            {
                return false;
            }

            Point pt = new Point(mouseLoc.X - _adornerPanel.Location.X, mouseLoc.Y - _adornerPanel.Location.Y);
            if (pt.X >= 0 && pt.Y >= 0 && pt.X < _adornerPanel.Width && pt.Y < AdornerPanel.PanelHeight)
            {
                if (_adornerPanel.RaiseMouseMove(button, pt))
                {
                    RefreshAdorner();
                    return true;
                }
            }
            return false;
        }

        public bool AdornerMouseLeave()
        {
            if (null != _adornerPanel)
            {
                if (_adornerPanel.RaiseMouseLeave())
                {
                    RefreshAdorner();
                    return true;
                }
            }
            return false;
        }

        private void RefreshAdorner()
        {
            if (null != _behaviorService)
            {
                _adornerPanel.Paint(_behaviorService.AdornerWindowGraphics);
            }
        }

        private Color GetCtrlBckColor()
        {
            //Color color = Color.Empty;
            //System.Windows.Forms.Control parent = _ctrl.Parent;
            //while (null != parent)
            //{
            //    if (parent.BackColor == Color.Transparent)
            //    {
            //        parent = parent.Parent;
            //    }
            //    else
            //    {
            //        color = parent.BackColor;
            //        break;
            //    }
            //}
            //if (Color.Empty == color)
            //{
            //    color = Color.White;
            //}
            Color color = Color.FromArgb(255,247, 247, 247);
            return color;
        }

    }
}
