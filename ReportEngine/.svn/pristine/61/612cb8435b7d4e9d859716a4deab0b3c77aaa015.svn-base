using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMS.Libraries.ToolControls.PMSChart
{
    /// <summary>
    /// ComboxItem类
    /// 用于对Combobox添加Items，以实现实际值与显示值分离
    /// </summary>
    [Serializable]
    public class ComboxItem
    {
        private string _Name = "";
        private object _Value = null;

        public ComboxItem()
        {

        }

        public ComboxItem(string name, object value)
        {
            _Name = name;
            _Value = value;
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public object Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
    }
}
