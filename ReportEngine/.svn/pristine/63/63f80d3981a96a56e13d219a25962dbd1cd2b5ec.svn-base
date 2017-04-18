using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace PMS.Libraries.ToolControls.Report.Elements.Util
{
    
    [Serializable]
    public class ExternData
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Key { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public object Value { get; set; }

        public ExternData()
        { 
            
        }

        public ExternData(string key, object value)
        {
            this.Key = key;
            Value = value;
        }

        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }
            ExternData ed = (ExternData)obj;

            return string.Equals(Key, ed.Key);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
