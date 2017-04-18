using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PMS.Libraries.Report.DataBinding
{
    public class PmsReportDataSource : IDataSource
    {
        protected IList<IDataRow> _values = null;
        protected int _currentIndex = -1;
        protected IDataColumn _column = null;

        /// <summary>
        /// 数据源的列
        /// </summary>
        public IDataColumn Column
        {
            get
            {
                return _column;
            }
        }

        public PmsReportDataSource():this(null)
        {
         
        }

        public PmsReportDataSource(string[] columns)
        {
            _values = new List<IDataRow>();
            _column = new PmsDataColumn(columns);
        }

        public object this[int row,string colName]
        {
            get
            {
                int index = _column.GetIndexByName(colName);
                return this[row, index];
            }
            set
            {
                int index = _column.GetIndexByName(colName);
                this[row, index] = value;
            }
        }

        public object this[int row,int col]
        {
            get 
            {
                if (col == -1 || col<0 || col>=_column.Count)
                {
                    throw new ArgumentException("列不存在");
                }
                if (row < 0 || row > _values.Count)
                {
                    throw new ArgumentException("行索引越界");
                }
                return _values[row].GetValue(col);
            }
            set
            {
                if (col == -1 || col < 0 || col >= _column.Count)
                {
                    throw new ArgumentException("列不存在");
                }
                if (row < 0 || row > _values.Count)
                {
                    throw new ArgumentException("行索引越界");
                }
                _values[row].SetValue(col, value);
            }
        }

        /// <summary>
        /// 判断是否包含指定成员名的成员
        /// </summary>
        /// <param name="dataMemberName">成员名</param>
        /// <returns>存在true,否则false</returns>
        public bool Contains(string dataMemberName)
        {
            if (null != Current)
            {
                if (null != Current.Column)
                {
                    return Current.Column.GetIndexByName(dataMemberName) != -1;
                }
            }
            return false;
        }

        /// <summary>
        /// IEnumerator的Current成员
        /// </summary>
        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        /// <summary>
        /// 当前数据成员
        /// </summary>
        public IDataRow Current
        {
            get
            {
                return GetValueByInex(_currentIndex);
            }
        }

        ///<summary>
        /// 数据行
        /// </summary>
        public int Count 
        {
            get
            {
                return _values.Count;
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
                return false;
            }
            return true;
            
        }

        /// <summary>
        /// 重置为第一个元素
        /// </summary>
        public void Reset()
        {
            _currentIndex = -1;
        }

        /// <summary>
        /// 获取当前指定成员名的数据
        /// </summary>
        /// <param name="dataMemberName">成员名</param>
        /// <returns>数据</returns>
        public object GetData(string dataMemberName)
        {
            IDataRow row = Current;
            if (null != row)
            {
                return row.GetValue(dataMemberName);
            }
            throw new Exception("未指当前定到行");
        }

        /// <summary>
        /// 新数据行，得当当前数据架构的拷贝
        /// </summary>
        /// <returns></returns>
        public IDataRow NewRow()
        {
            if (_values.Count > 0)
            {
                return _values[0].Copy();
            }
            return new PmsDataRow(_column);
        }

        /// <summary>
        /// 添加数据行
        /// </summary>
        /// <param name="row">行对象</param>
        public void AddRow(IDataRow row)
        {
            if (null != row)
            {
                if (!_column.Compatible(row.Column))
                {
                    throw new ArgumentException("添加了不兼容的行，请查看列属性");
                }
            }
            _values.Add(row);
        }

        /// <summary>
        /// 删除指定行
        /// <remarks>当索引越界时候将抛出ArgumentException</remarks>
        /// </summary>
        /// <param name="rowIndex">行索引</param>
        public void RemoveRow(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= _values.Count)
            {
                throw new ArgumentException("索引超出范围");
            }
            _values.RemoveAt(rowIndex);
        }

        /// <summary>
        /// 删除所有行
        /// </summary>
        public void RemoveAllRow()
        {
            if (null != _values)
            {
                _values.Clear();
            }
        }

        /// <summary>
        /// 获取指定行
        /// <remarks>如果索引越界抛出ArgumentException</remarks>
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>返回行</returns>
        public IDataRow GetRow(int index)
        {
            if (index < 0 || index >= this._values.Count)
            {
                throw new ArgumentException("索引越界");
            }
            return _values[index];
        }

        /// <summary>
        /// 释放非托管资源
        /// </summary>
        public void Dispose()
        { 
            //没有非托管资源释放
            //所有托管资源由GC释放
            //所以空实现
        }

        private IDataRow GetValueByInex(int index)
        {
            if (index>=0 && index < _values.Count)
            {
                return _values[index];
            }
            throw new Exception("未指当前定到行");
        }
    }
}
