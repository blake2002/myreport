using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using PMS.Libraries.ToolControls.Report.Element;

namespace PMS.Libraries.ToolControls.Report.Controls.TypeConvert
{
    public class BorderConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(String))
            {
                return false;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(String))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                ElementBorder border = value as ElementBorder;
                if (null != border)
                {
                    return border.Name;
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
