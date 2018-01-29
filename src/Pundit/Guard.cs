using System;

namespace Pundit
{
    public static class Guard
    {
        public static void Assert<T>(Func<bool> predicate, T exception)
            where T : Exception
        {
            if (predicate())
            {
                throw exception;
            }
        }
    }
}
