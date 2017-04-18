using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMS.Libraries.ToolControls.Report.Element
{
    public interface IBindField
    {
        void BindValue(IDictionary<string, Object> values);
    }
}
