using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMS.Libraries.Report.DataBinding
{
    public interface IDataToDataSource<T>
    {
        IDataSource ConvertTo(T data);
    }
}
