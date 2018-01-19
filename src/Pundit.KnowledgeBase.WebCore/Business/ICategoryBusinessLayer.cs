using Pundit.KnowledgeBase.WebCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Business
{
    public interface ICategoryBusinessLayer
    {
        Task<long> CreateAsync(Category category);
    }
}
