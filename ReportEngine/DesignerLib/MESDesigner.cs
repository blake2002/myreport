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
    public class MESDesigner:ControlDesigner
    {
        private BehaviorService _behaviorSvc = null;
        protected ISelectionService _selectionService = null;
        private Adorner _mesAdorner = null;
        private MESGlyph _glyph = null;

        public override void Initialize(System.ComponentModel.IComponent component)
        {
            base.Initialize(component);
            InitialService();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (null != _behaviorSvc && null != _mesAdorner)
                {
                    _behaviorSvc.Adorners.Remove(_mesAdorner);
                    foreach (Glyph glyph in _mesAdorner.Glyphs)
                    {
                        IDisposable dispose = glyph as IDisposable;
                        if (null != dispose)
                        {
                            dispose.Dispose();
                        }
                    }
                    _behaviorSvc = null;
                }
            }
            base.Dispose(disposing);
        }

        protected override bool GetHitTest(System.Drawing.Point point)
        {
            return base.GetHitTest(point);
        }

        private void InitialService()
        {
            _behaviorSvc =
                GetService(typeof(BehaviorService))
                as BehaviorService;
            _selectionService =
                GetService(typeof(ISelectionService))
                as ISelectionService;
            if (null != _behaviorSvc)
            {
                _mesAdorner = new Adorner();
                _glyph = new MESGlyph(_mesAdorner, _selectionService, Control, _behaviorSvc, new MESBehavior(Control, true, _behaviorSvc));
                _mesAdorner.Glyphs.Add(_glyph);
                _behaviorSvc.Adorners.Add(_mesAdorner);
            }
        }
       
    }
}
