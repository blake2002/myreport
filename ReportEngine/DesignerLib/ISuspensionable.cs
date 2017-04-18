using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES.Controls.Design
{
    /// <summary>
    /// 装饰器接口
    /// </summary>
    public interface ISuspensionable
    {
        SuspensionItem[] ListSuspensionItems();
    }
}
