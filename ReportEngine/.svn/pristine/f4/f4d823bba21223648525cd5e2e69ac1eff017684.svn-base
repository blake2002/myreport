using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PMS.Libraries.Report.DataBinding
{
    public class PmsDataRow : IDataRow
    {

        protected IList<object> _values = null;
        
        protected IDataColumn _column = null;
        /// <summary>
        /// 行拥有的列
        /// </summary>
        public IDataColumn Column 
        {
            get
            {
                return _column;
            }
        }
        internal PmsDataRow(IDataColumn column)
        { 
            _values = new List<object>();
            _column = column;
            if (null != _column)
            {
                for (int i = 0; i < _column.Count; i++)
                {
                    _values.Add(null);
                }
            }
           
        }

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>数据</returns>
        public object this[string name]
        {
            get
            {
                if (null == _column)
                {
                    throw new ArgumentException("当前行不存在列");
                }
                int index =_column.GetIndexByName(name);
                return this[index];
            }
            set
            {
                if (null == _column)
                {
                    throw new ArgumentException("当前行不存在列");
                }
                int index = _column.GetIndexByName(name);
                this[index] = value;
            }
        }

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="name">索引</param>
        /// <returns>数据</returns>
        public object this[int index]
        {
            get
            {
                if (null == _column)
                {
                    throw new ArgumentException("当前行不存在列");
                }
                return GetValueByInex(index);
            }
            set
            {
                if (null == _column)
                {
                    throw new ArgumentException("当前行不存在列");
                }
                if (index < 0)
                {
                    throw new ArgumentException("不存在的索引");
                }
                if (index < _values.Count)
                {
                    _values[index] = value;
                }
                else
                {
                    if (index < _column.Count)
                    {
                        for (int i = _values.Count; i < _column.Count; i++)
                        {
                            _values.Add(null);
                        }
                        _values[index] = value;
                    }
                    else
                    {
                        throw new ArgumentException("不存在的索引");
                    }
                }
            }
        }

        #region IEnumerator

        protected int _currentIndex = 0;

        /// <summary>
        /// 当前数据成员
        /// </summary>
        public object Current
        {
            get
            {
                if (null == _column)
                {
                    throw new ArgumentException("当前行不存在列");
                }
                return GetValueByInex(_currentIndex);
            }
        }

        /// <summary>
        /// 下一条数据
        /// </summary>
        /// <returns>是否有下一条数据</returns>
        public bool MoveNext()
        {
            _currentIndex++;
            if (_currentIndex >= _values.Count)
            {
                Reset();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 重置为第一个元素
        /// </summary>
        public void Reset()
        {
            _currentIndex = 0;
        }

        private object GetValueByInex(int index)
        {
            if (index >= 0 && index < _values.Count)
            {
                return _values[index];
            }
            else
            {
                if (index >= _values.Count && index < _column.Count)
                {
                    for (int i = _values.Count; i < _column.Count; i++)
                    {
                        _values.Add(null);
                    }
                    return _values[index];
                }
            }
            throw new ArgumentException("不存在的索引");
        }
        #endregion

        #region IEnumerable
        public IEnumerator GetEnumerator()
        {
            return this;
        }
        #endregion 

        /// <summary>
        /// 为指定列添加值
        /// </summary>
        /// <param name="colName">列名</param>
        /// <param name="value">值</param>
        public void SetValue(string colName, object value)
        {
            this[colName] = value;  
        }

        /// <summary>
        /// 为指定列添加值
        /// </summary>
        /// <param name="index">列索引</param>
        /// <param name="value">值</param>
        public void SetValue(int index, object value)
        {
            this[index] = value;
        }

        /// <summary>
        /// 获得指定列的值
        /// </summary>
        /// <param name="colName">列名</param>
        /// <returns>值</returns>
        public object GetValue(string colName)
        {
            return this[colName];
        }

        /// <summary>
        /// 获得指定列的值
        /// </summary>
        /// <param name="colName">列索引</param>
        /// <returns>值</returns>
        public object GetValue(int index)
        {
            return this[index];
        }

        /// <summary>
        /// 拷贝
        /// </summary>
        /// <returns>拷贝后的结果</returns>
        public IDataRow Copy()
        {
            PmsDataRow row = new PmsDataRow(_column);
            return row;
        }

        /// <summary>
        /// 判断行是否与目标兼容
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Compatible(IDataRow obj)
        {
            if (null == obj)
            {
                return false;
            }
            IDataColumn col = obj.Column;
            if (null == col && null == this._column)
            {
                return true;
            }
            return this._column.Compatible(col);
        }
    }
}
