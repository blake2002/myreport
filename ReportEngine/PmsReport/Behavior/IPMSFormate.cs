using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace PMS.Libraries.ToolControls.Report
{
    public interface IPMSFormate
    {
        string StrFormate
        {
            get;
            set;
        }

        string FormateToString(object obj);
    }

    public class FormateUtil
    {
        public static string Formate(string strFormate, object o)
        {
            IFormattable ft = o as IFormattable;
            if (null != ft)
            {
                return ft.ToString(strFormate, CultureInfo.CurrentCulture);
            }
            if (o != null)
            {
                return o.ToString();
            }
            else
            {
                return o as string;
            }
        }
    }
}
