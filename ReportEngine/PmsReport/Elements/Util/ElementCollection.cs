using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using PMS.Libraries.ToolControls.Report.Element;

namespace PMS.Libraries.ToolControls.Report.Util
{
    [Serializable]
    public class ElementCollection<T>:ReadOnlyCollectionBase where T:IElement
    {

        public ElementCollection():base()
        { 

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sourceList">用指定的列表初始化集合中内容</param>
        public ElementCollection(IList sourceList):base()
        {
            InnerList.AddRange(sourceList);
        }

        /// <summary>
        /// 自身索引器
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>返回指定索引的控件</returns>
        public object this[int index]
        {
            get
            {
                return InnerList[index];
            }
        }

        /// <summary>
        ///<remarks> 未找到返回-1</remarks>
        /// </summary>
        /// <param name="value">组件</param>
        /// <returns>返回索引</returns>
        public int IndexOf(object value)
        {
            return InnerList.IndexOf(value);
        }

        /// <summary>
        /// 判断指定对象是否在集合中存在
        /// </summary>
        /// <param name="value">需要判断的对象</param>
        /// <returns>存在返回true,否则false</returns>
        public bool Contains(T value)
        {
            return InnerList.Contains(value);
        }

        public void Add(T value)
        {
            if (null != value)
            {
                InnerList.Add(value);
            }
        }

        public void Remove(T value)
        {
            InnerList.Remove(value);
        }

        public void RemoveAt(int index)
        {
           InnerList.RemoveAt(index);
        }

    }
}
