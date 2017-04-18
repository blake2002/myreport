using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using PMS.Libraries.ToolControls.Report.Element;
using System.Reflection;

namespace PMS.Libraries.ToolControls.Report.Elements.Util
{
    public class BorderFactory
    {
        private IDictionary<string, Type> _borderMap = new Dictionary<string, Type>();

        private  static object _syncRoot = new object();

        protected static BorderFactory _instance = null;
        public static BorderFactory Instacne
        {
            get
            {
                if (null == _instance)
                {
                    lock (_syncRoot)
                    {
                        if (null == _instance)
                        {
                            _instance = new BorderFactory();
                        }
                    }
                }
                return _instance;
            }
        }

        protected BorderFactory()
        {
            Register();
        }

        public ElementBorder CreateBorder(string name, IElement element)
        {
            if (_borderMap.ContainsKey(name))
            {
                Type type = _borderMap[name];
                ConstructorInfo ci = type.GetConstructor(new Type[] { typeof(IElement) });
                ElementBorder o = ci.Invoke(new object[] { element }) as ElementBorder;
                //VS设计器中，下列代码不会执行，保留用于可能自定义设计
                if (null != element && null != element.ExternDatas)
                {
                    for (int i = 0; i < element.ExternDatas.Count; i++)
                    {
                        ExternData data = element.ExternDatas[i]; ;
                        if (null != data)
                        {
                            if (!string.IsNullOrEmpty(data.Key))
                            {
                                PropertyInfo pi = type.GetProperty(data.Key);
                                if (null != pi)
                                {
                                    pi.SetValue(o, data.Value, null);
                                }
                            }
                        }

                    }
                    
                    return o;
                }
            }
            return null;
        }

        private void Register()
        {
            RectangleBorder border1 = new RectangleBorder(null);
            _borderMap.Add(border1.Name, border1.GetType());
            EllipseBorder border2 = new EllipseBorder(null);
            _borderMap.Add(border2.Name, border2.GetType());
        }
    }
}
