using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMS.Libraries.ToolControls.Report.Element;


namespace PMS.Libraries.ToolControls.Report.Element
{
    public interface IElementTranslator
    {
        IControlTranslator ToElement(bool transferChild);
    }
}
