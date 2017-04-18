using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMS.Libraries.ToolControls.PMSChart
{
    [Serializable]
    public class TableJoinRelation
    {
        public string mainTable;
        public string secondaryTable;
        public string mainColumn;
        public string secondaryColumn;
        public string compare;
        //
        //4 inner join,1 left out join, 2 right out join
        //3 full out join,0 cross join
        public int joinType;

        public TableJoinRelation Clone()
        {
            TableJoinRelation tjr = new TableJoinRelation();
            tjr.compare = this.compare;
            tjr.secondaryColumn = this.secondaryColumn;
            tjr.mainColumn = this.mainColumn;
            tjr.secondaryTable = this.secondaryTable;
            tjr.mainTable = this.mainTable;
            return tjr;
        }
    }
    [Serializable]
    public class SortClass
    {
        public string fieldName;
        public SortType sortType;
        public override string ToString()
        {
            return fieldName + " " + sortType.ToString();
        }
        public SortClass Clone()
        {
            SortClass sc = new SortClass();
            sc.fieldName = this.fieldName;
            sc.sortType = this.sortType;
            return sc;
        }
    }

    public enum SortType
    {
        NoSort,
        Asc,
        Desc
    }
}
