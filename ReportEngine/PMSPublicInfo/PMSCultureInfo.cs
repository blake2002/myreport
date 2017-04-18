using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Microsoft.Win32;

namespace PMS.Libraries.ToolControls.PMSPublicInfo
{
    public class PMSCultureInfo
    {
        #region Instance Fields

        private CultureInfo culture;

        #endregion

        public CultureInfo GetPMSCultureinfo()
        {
            string cultureStr;
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Hite\\PMS\\1.0\\Language", true);
            if (key == null)
            {
                culture = CultureInfo.CurrentCulture;
                cultureStr = culture.ToString();
                key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Hite\\PMS\\1.0\\Language");
                key.SetValue("Language", cultureStr);
            }
            else
            {
                Object obj = key.GetValue("Language");
                cultureStr = obj.ToString();
                culture = CultureInfo.CreateSpecificCulture(cultureStr);
            }
            
            key.Close();

            return culture;
        }
    }
}
