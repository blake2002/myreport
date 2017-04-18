using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Drawing;
using ControlLib.Editor;
using System.Drawing.Design;

namespace ControlLib.MarkupAnnotation
{
    class LineActionList:DesignerActionList
    {
        //private Line _line = null;
        public LineActionList(IComponent component)
            : base(component)
        {
           // _line = (Line)component;
        }

        [Editor(typeof(TestEditor), typeof(UITypeEditor))]
        public Point StartLocation
        {
            get
            {
                return new Point(0, 0);//_line.StartLocation;
            }
            set
            {
                //_line.StartLocation = value;
            }
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection collection = new DesignerActionItemCollection();
            collection.Add(new DesignerActionPropertyItem("StartLocation", "起点坐标", "行为", "线条的起始坐标"));
            return collection;
        }
    }
}
