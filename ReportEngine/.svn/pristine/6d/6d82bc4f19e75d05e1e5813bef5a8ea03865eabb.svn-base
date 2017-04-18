using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PMS.Libraries.Report.DataBinding
{
    public class PmsDataColumn : IDataColumn
    {
        protected IList<string> _columns = null;

        public PmsDataColumn():this(null)
        {

        }

        public PmsDataColumn(string[] columns)
        {
            _columns = new List<string>();
            if (null != columns)
            {
                foreach (string col in columns)
                {
                    _columns.Add(col);
                }
            }
        }

        /// <summary>
        /// 根据列明查找列索引
        /// </summary>
        /// <param name="colName">列名</param>
        /// <returns>索引</returns>
        public int GetIndexByName(string colName)
        {
            return _columns.IndexOf(colName);
        }

        /// <summary>
        /// 添加列
        /// </summary>
        /// <param name="colName">列名</param>
        public void Add(string colName)
        {
            if (_columns.Contains(colName))
            {
                throw new ArgumentException("已经存在的列");
            }
            _columns.Add(colName);
        }

        /// <summary>
        /// 删除列
        /// </summary>
        /// <param name="colName"></param>
        public void Remove(string colName)
        {
            if (_columns.Contains(colName))
            {
                _columns.Remove(colName);
            }
            throw new ArgumentException("不存在的列");
        }

        /// <summary>
        /// 获取列集合
        /// </summary>
        public string[] Columns 
        {
            get
            {
                return _columns.ToArray();
            }
        }

        /// <summary>
        /// 列数
        /// </summary>
        public int Count 
        {
            get
            {
                return _columns.Count;
            }
        }

        protected IDataSource _dataSource = null;
        /// <summary>
        /// 列所绑定到数据源
        /// </summary>
        public IDataSource DataSource 
        {
            get
            {
                return _dataSource;
            }
        }

        /// <summary>
        /// 拷贝
        /// </summary>
        /// <returns>拷贝后的结果</returns>
        public IDataColumn Copy()
        {
            PmsDataColumn col = new PmsDataColumn(_columns == null?null: _columns.ToArray());
            return col;
        }

        /// <summary>
        /// 判断列是否一样
        /// </summary>
        /// <param name="obj">目标对象</param>
        /// <returns>两个列相符合则为True,否则false</returns>
        public bool Compatible(IDataColumn obj)
        {
            if (null == obj)
            {
                return false;
            }
            if (this.Count != obj.Count)
            {
                return false;
            }
            for (int i = 0; i < this.Count; i++)
            {
                if (!_columns[i].Equals(obj.Columns[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
