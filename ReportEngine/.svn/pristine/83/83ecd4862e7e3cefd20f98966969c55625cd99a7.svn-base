using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMS.Libraries.ToolControls.PmsSheet.PmsPublicData
{
    [Serializable]
    public class DSSqlPair:ICloneable
    {
        public DSSqlPair()
        {

        }

        public DSSqlPair(string ds,string sql)
        {
            DataSource = ds;
            Sql = sql;
        }

        public string DataSource
        {
            get;
            set;
        }

        public string Sql
        {
            get;
            set;
        }

        public object Clone()
        {
            return new DSSqlPair(DataSource, Sql);
        }

        public override bool Equals(object obj)
        {
            return obj is DSSqlPair && this == (DSSqlPair)obj;
        }

        public bool Equals(DSSqlPair p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (DataSource == p.DataSource) && (Sql == p.Sql);
        }

        public static bool operator ==(DSSqlPair x, DSSqlPair y)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(x, y))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)x == null) || ((object)y == null))
            {
                return false;
            }
            return x.DataSource == y.DataSource && x.Sql == y.Sql;
        }

        public static bool operator !=(DSSqlPair x, DSSqlPair y)
        {
            return !(x == y);
        }

        public override int GetHashCode()
        {
            return DataSource.GetHashCode() ^ Sql.GetHashCode();
        }

    }
}
