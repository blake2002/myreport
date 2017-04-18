using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace PMS.Libraries.ToolControls.Report.Controls.TypeConvert
{
    public class ReportTabelRowsTypeConverter:TypeConverter
    {
        public string _collectionStr = "(集合)";
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (null != sourceType && sourceType ==  typeof(string))
            {
                return false;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (null != destinationType && destinationType == typeof(string))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (null != value)
            {
                if (null != destinationType && destinationType == typeof(string))
                {
                    return _collectionStr;
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
