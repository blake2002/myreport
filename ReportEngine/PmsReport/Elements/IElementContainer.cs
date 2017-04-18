using System;
using System.Collections.Generic;
using System.Text;


namespace PMS.Libraries.ToolControls.Report.Element
{
    public interface IElementContainer
    {
        /// <summary>
        /// 子元素集合
        /// </summary>
        IList<IElement> Elements { get; }         
    }
}
