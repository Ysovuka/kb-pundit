namespace System.Linq
{
    public class FilterParameter
    {
        public string Property { get; private set; }
        public string Value { get; private set; }
        public bool BitwseOr { get; private set; }

        private FilterParameter() { }

        public FilterParameter(string property, string value) : this(property, value, false) { }

        public FilterParameter(string property, string value, bool bitwiseOr)
        {
            Property = property;
            Value = value;
            BitwseOr = bitwiseOr;
        }
    }
}
