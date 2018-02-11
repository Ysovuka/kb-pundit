using System;
using System.Collections.Generic;
using System.Text;

namespace System.Linq
{
    public class SortParameter
    {
        public SortOrder Order { get; private set; }
        public string Property { get; private set; }

        private SortParameter() { }
        public SortParameter(string property, string order)
        {
            Property = property;
            Order = SortOrder.Parse(order);
        }
    }
}
