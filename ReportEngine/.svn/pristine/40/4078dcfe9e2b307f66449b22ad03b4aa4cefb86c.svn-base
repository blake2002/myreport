using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Data;
using System.ComponentModel;
using System.Collections.Generic;

namespace PMS.Libraries.ToolControls.PMSPublicInfo.OleDBManager
{
	/// <summary>
	/// Specifies a sorting order.
	/// </summary>
	public enum Sort
	{
		NoSort,
		Ascending,
		Descending
	}
    /// <summary>
    /// Specifies a grouping aggregate.
    /// </summary>
	public enum Aggregate
	{
		GroupBy,
		Sum,
		Avg,
		Min,
		Max,
		Count,
		//Expression,
		//Where,
		SumDistinct,
		AvgDistinct,
		MinDistinct,
		MaxDistinct,
		CountDistinct,
		StDev,
		StDevP,
		Var,
		VarP
	}

	/// <summary>
	/// Represents a field in a query.
	/// </summary>
	public class QueryField : 
        ICloneable, 
        INotifyPropertyChanged
	{
		//----------------------------------------------------------------
		#region ** fields

		string		_column;	// column name (or expression)
		string		_alias;		// alias for this field (optional)
		DataTable	_table;		// source table
		bool		_output;	// include in SELECT clause
		Aggregate	_groupBy;	// GROUP BY clause
		Sort		_sort;		// ORDER BY clause
		string		_filter;	// WHERE clause

        // for parsing filter statements
        static Regex _rx1 = new Regex(@"^([^<>=]*)\s*(<|>|=|<>|<=|>=)\s*([^<>=]+)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        static Regex _rx2 = new Regex(@"^([^<>=]*)\s*BETWEEN\s+(.+)\s+AND\s+(.+)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        
        #endregion

		//----------------------------------------------------------------
		#region ** ctor

        public QueryField(DataColumn col)
        {
            _table = col.Table;
            _column = col.ColumnName;
            _output = true;
        }
        public QueryField(DataTable dt)
        {
            _table = dt;
            _column = "*";
            _output = true;
        }

		#endregion

		//----------------------------------------------------------------
		#region ** object model

        /// <summary>
        /// Gets or sets the name of the column or the expression that this field represents.
        /// </summary>
		public string Column
		{
			get { return _column; }
			set 
			{
				if (_column != value)
				{
					_column = value; 
                    OnPropertyChanged("Column");
				}
			}
		}
        /// <summary>
        /// Gets or sets the alias used to keep this field's name unique.
        /// </summary>
		public string Alias
		{
			get { return _alias != null ? _alias : string.Empty; }
			set 
			{
				if (_alias != value)
				{
					_alias = value;
                    OnPropertyChanged("Alias");
                }
			}
		}
        /// <summary>
        /// Gets the name of the table that contains this field.
        /// </summary>
		public string Table
		{
			get { return _table.TableName; }
		}
        /// <summary>
        /// Gets or sets whether this field will be included in the SQL output.
        /// </summary>
		public bool Output
		{
			get { return _output; }
			set 
			{
				if (_output != value)
				{
					_output = value;
                    OnPropertyChanged("Output");
				}
			}
		}
        /// <summary>
        /// Gets or sets how this field will be grouped.
        /// </summary>
		public Aggregate GroupBy
		{
			get { return _groupBy; }
			set 
			{
				if (_groupBy != value)
				{
					_groupBy = value;
                    OnPropertyChanged("GroupBy");
				}
			}
		}
        /// <summary>
        /// Gets or sets the sorting order for this field.
        /// </summary>
		public Sort Sort
		{
			get { return _sort; }
			set 
			{
				if (_sort != value)
				{
					_sort = value;
                    OnPropertyChanged("Sort");
				}
			}
		}
        /// <summary>
        /// Gets or sets the filter criteria for this group.
        /// </summary>
		public string Filter
		{
			get { return _filter != null ? _filter : string.Empty; }
			set 
			{
				if (_filter != value)
				{
					_filter = value;
                    OnPropertyChanged("Filter");
				}
			}
		}
        /// <summary>
        /// Gets a formatted filter expression to be used in the SQL statement.
        /// </summary>
        /// <returns>A formatted filter expression to be used in the SQL statement.</returns>
        public string GetFilterExpression()
        {
            // empty? easy
            string filter = this.Filter.Trim();
            if (filter.Length == 0)
            {
                return string.Empty;
            }

            // get simple expressions
            Match m = _rx1.Match(filter);
            if (m.Success)
            {
                return m.Groups[1].Value.Length == 0
                    ? string.Format("({0} {1})", this.GetFullName(true), filter) // > x
                    : string.Format("({0})", filter); // y > x
            }

            // get 'between' expressions
            m = _rx2.Match(filter);
            if (m.Success)
            {
                return m.Groups[1].Value.Length == 0
                    ? string.Format("({0} {1})", this.GetFullName(), filter) // between x and y
                    : string.Format("({0})", filter); // z between x and y
            }

            // oops...
            Debug.WriteLine("Warning: failed to parse filter...");
            return string.Format("({0} {1})", this.GetFullName(true), filter);
        }

		#endregion 

		//----------------------------------------------------------------
		#region ** ICloneable

		public object Clone()
		{
			return this.MemberwiseClone();
		}

		#endregion

        //----------------------------------------------------------------
        #region ** INotifyPropertyChanged

        /// <summary>
        /// Fires when a property changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Raise the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="e"><see cref="PropertyChangedEventArgs"/> that contains the event data.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        // shorthand
        void OnPropertyChanged(string propName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propName));
        }

        #endregion
        
        //----------------------------------------------------------------
		#region ** overrides

        /// <summary>
        /// Gets the full field name, including the parent table name and brackets,
        /// and optionally including a GROUPBY clause.
        /// </summary>
        /// <param name="groupBy">Whether to include a GROUPBY clause.</param>
        /// <returns>The full field name.</returns>
        public string GetFullName(bool groupBy)
		{
            // default handling
            string str = GetFullName();

            // handle GROUPBY clauses
            if (groupBy)
            {
                string fmt = "{0}";
                switch (GroupBy)
                {
                    case Aggregate.Sum: fmt = "SUM({0})"; break;
                    case Aggregate.Avg: fmt = "AVG({0})"; break;
                    case Aggregate.Min: fmt = "MIN({0})"; break;
                    case Aggregate.Max: fmt = "MAX({0})"; break;
                    case Aggregate.Count: fmt = "COUNT({0})"; break;
                    case Aggregate.StDev: fmt = "STDEV({0})"; break;
                    case Aggregate.StDevP: fmt = "STDEVP({0})"; break;
                    case Aggregate.Var: fmt = "VAR({0})"; break;
                    case Aggregate.VarP: fmt = "VARP({0})"; break;
                    case Aggregate.SumDistinct: fmt = "SUM(DISTINCT {0})"; break;
                    case Aggregate.AvgDistinct: fmt = "AVG(DISTINCT {0})"; break;
                    case Aggregate.MinDistinct: fmt = "MIN(DISTINCT {0})"; break;
                    case Aggregate.MaxDistinct: fmt = "MAX(DISTINCT {0})"; break;
                    case Aggregate.CountDistinct: fmt = "COUNT(DISTINCT {0})"; break;
                }
                str = string.Format(fmt, str);
            }

            // done
            return str;
		}
        /// <summary>
        /// Gets the full field name, including the parent table name and brackets.
        /// </summary>
        /// <returns>The full field name.</returns>
        public string GetFullName()
        {
            // return table.column string
            if (Column == "*" || _table.Columns.Contains(Column))
            {
                return string.Format("{0}.{1}",
                    OleDbSchema.GetFullTableName(_table),
                    OleDbSchema.BracketName(Column));
            }

            // column is not part of a table (e.g. expression)
            return OleDbSchema.BracketName(Column);
        }

		#endregion
    }
    /// <summary>
    /// Represents a bindable collection of <see cref="QueryField"/> objects.
    /// </summary>
    public class QueryFieldCollection : BindingList<QueryField>
    {
        /// <summary>
        /// Overridden to perform validation at list level.
        /// </summary>
        /// <param name="e"><see cref="ListChangedEventArgs"/> that contains the event data.</param>
        protected override void OnListChanged(ListChangedEventArgs e)
        {
            // fix fields when they change
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                var f = this[e.NewIndex];
                switch (e.PropertyDescriptor.Name)
                {
                    // prevent duplicate aliases
                    case "Alias":
                        foreach (QueryField field in this)
                        {
                            if (field != f && f.Alias == field.Alias)
                            {
                                f.Alias = CreateUniqueAlias(f);
                                break;
                            }
                        }
                        break;

                    // if GroupBy is an aggregate, the field needs an alias
                    case "GroupBy":
                        if (f.GroupBy != Aggregate.GroupBy && string.IsNullOrEmpty(f.Alias))
                        {
                            f.Alias = CreateUniqueAlias(f);
                        }

                        break;
                }
            }

            // raise event as usual
            base.OnListChanged(e);
        }

        // creates a unique alias for a field
        string CreateUniqueAlias(QueryField f)
        {
            for (int i = 1; true; i++)
            {
                // try Expr1, Expr2, etc...
                string alias = string.Format("Expr{0}", i);

                // check if this one exists
                bool duplicate = false;
                foreach (QueryField field in this)
                {
                    if (field != f && string.Compare(alias, field.Alias, true) == 0)
                    {
                        duplicate = true;
                        break;
                    }
                }

                // doesn't exist? we're done here
                if (!duplicate)
                {
                    return alias;
                }
            }
        }
    }
}
