using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;
using PMS.Libraries.ToolControls.Report.MarkupAnnotation;
using System.Windows.Forms.Design;

namespace PMS.Libraries.ToolControls.Report.Designer
{
    public class PmsImageBoxDesigner : ControlDesigner
    {
        private DesignerActionListCollection _designerActionList = null;
        public PmsImageBoxDesigner()
            : base()
        {
           
          
        }

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (null == _designerActionList)
                {
                    _designerActionList = new DesignerActionListCollection();
                    _designerActionList.Add(new PmsImageBoxActionList(this.Component));
                }
                return _designerActionList;
            }
        }

    }
}
