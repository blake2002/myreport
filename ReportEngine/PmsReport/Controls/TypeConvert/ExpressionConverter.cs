using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using PMS.Libraries.ToolControls.Report.Element;

namespace PMS.Libraries.ToolControls.Report.Controls.TypeConvert
{
    public class ExpressionConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }


        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                try
                {
                    string expression = Convert.ToString(value);
                    if (null == expression)
                    {
                        return "";
                    }
                    return expression;
                }
                catch
                {
                    return "";
                }
               
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }


        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
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
            str = Convert.ToString(value);
            MES.Report.MESReportExpressionEditor expEditor = new MES.Report.MESReportExpressionEditor();
            if (expEditor.CheckExpr(str))
            {
                //异常直接抛出，谈MessageBox会导致
                //被执行两次
                return str;
            }
            else
            {
                throw new Exception("表达式非法");
            }
        }

        
        
    }
}
