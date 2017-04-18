using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Windows.Forms;
using ControlLib.MarkupAnnotation;

namespace ControlLib.Designer
{
    public class LineDesigner : System.Windows.Forms.Design.ControlDesigner
    {
        public LineDesigner():base()
        {
            //this.Verbs.Add(new DesignerVerb("abc",new EventHandler(ABCHandle)));
        }
        public override void DoDefaultAction()
        {
           base.DoDefaultAction();
        }

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                DesignerActionListCollection col = new DesignerActionListCollection();
                col.Add(new LineActionList(this.Component));
                return col;
            }
        }
    }
}
