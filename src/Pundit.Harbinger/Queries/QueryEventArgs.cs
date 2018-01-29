using System;

namespace Pundit.Harbinger.Queries
{
    public class QueryEventArgs : EventArgs
    {
        public QueryEventArgs(object data)
        {
            Data = data;
        }

        public object Data { get; private set; }
    }
}
