using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace PMS.Libraries.Report.DataBinding
{
    public class EntityToDataSource:IDataToDataSource<object>
    {
        public IDataSource ConvertTo(object data)
        {
            if(null == data)
            {
                return null;
            }
            Type type = data.GetType();
            if (null != type)
            {
                PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                if (null != properties && properties.Length > 0)
                {
                    PmsReportDataSource dataSource = new PmsReportDataSource();
                    IDataColumn column = dataSource.Column;
                    IDataRow row = dataSource.NewRow();
                    foreach (PropertyInfo pi in properties)
                    {
                        column.Add(pi.Name);
                        object value = null;
                        try
                        {
                            value = pi.GetValue(data, null);
                        }
                        catch
                        {
                        }
                        try
                        {
                            row.SetValue(pi.Name, value);
                        }
                        catch
                        {

                        }
                    }
                    dataSource.AddRow(row);
                    return dataSource;
                }
            }
            return null;
        }
    }
}
