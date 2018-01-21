using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Debug
{
    public class DefaultLogger : ILogger
    {
        private string _category;
        public string Category { get { return _category; } set { _category = value; } }

        public DefaultLogger() { }
        public DefaultLogger(string category)
        {
            _category = category;
        }

        public async Task ErrorAsync<T>(T exception)
            where T : Exception
        {
            await Log(exception);
        }

        public void Error<T>(T exception)
            where T : Exception
        {
            Log(exception).RunSynchronously();
        }

        public virtual async Task Log<T>(T exception)
            where T : Exception
        {
            System.Diagnostics.Debug.WriteLine(exception.Message, Category);
        }
    }
}
