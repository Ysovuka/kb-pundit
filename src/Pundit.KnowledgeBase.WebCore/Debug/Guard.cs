using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Debug
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
