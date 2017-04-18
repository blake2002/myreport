using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace PMS.Libraries.ToolControls.Report.Controls.TypeConvert
{
    public class EditConverter : TypeConverter
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
            if (null != context && null != context.Instance && null != context.Container)
            {
                if (context.Instance is PmsEdit)
                {
                    if (null != value)
                    {
                        return value.ToString();
                    }
                    return string.Empty;
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            //if (null != context && null != context.Instance && null != context.Container)
            //{
            if (null == context || null == context.Instance)
            {
                try
                {
                    return Convert.ToString(value);
                }
                catch
                {
                    return "";
                }
            }
            string str = string.Empty;
            try
            {
                str = Convert.ToString(value);
            }
            catch
            { 
            
            }
            return str;
        }

    }
}
