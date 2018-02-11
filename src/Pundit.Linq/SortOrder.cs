using System;
using System.Collections.Generic;
using System.Text;

namespace System.Linq
{
    public abstract class SortOrder
    {
        public SortOrder() { }

        public static SortOrder Ascending = new AscendingSortOrder();
        public static SortOrder Descending = new DescendingSortOrder();

        public static SortOrder Parse(string order)
        {
            if (order.Contains("asc")) return Ascending;
            return Descending;
        }

        public abstract string OrderBy();
        public abstract string ThenBy();

        private class AscendingSortOrder : SortOrder
        {
            public override string OrderBy() => "OrderBy";
            public override string ThenBy() => "ThenBy";
        }

        private class DescendingSortOrder : SortOrder
        {
            public override string OrderBy() => "OrderByDescending";
            public override string ThenBy() => "ThenByDescending";
        }
    }
}
