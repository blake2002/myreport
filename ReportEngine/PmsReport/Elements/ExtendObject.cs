using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMS.Libraries.ToolControls.Report.Element
{
    [Serializable]
    public class ExtendObject:ICloneable
    {
        public string Name
        {
            get;
            set;
        }

        public object Tag
        {
            get;
            set;
        }

        public ExtendObject()
        {
            Name = string.Empty;
        }

        public object Clone()
        {
            ExtendObject eo = new ExtendObject();
            eo.Name = Name;
            if (null != Tag && Tag is ICloneable)
            {
                eo.Tag = ((ICloneable)Tag).Clone();
            }
            return eo;
        }

    }
}
