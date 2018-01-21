using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Debug
{
    public interface ILogger
    {
        Task ErrorAsync<T>(T exception) where T : Exception;
        void Error<T>(T exception) where T : Exception;
    }
}
