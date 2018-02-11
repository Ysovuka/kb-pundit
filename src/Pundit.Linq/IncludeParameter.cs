namespace System.Linq
{
    public class IncludeParameter
    {
        public string Property { get; private set; }

        private IncludeParameter() { }

        public IncludeParameter(string property)
        {
            Property = property;
        }
    }
}
