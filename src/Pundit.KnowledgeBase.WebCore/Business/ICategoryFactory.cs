using Pundit.KnowledgeBase.WebCore.Data;
using Pundit.KnowledgeBase.WebCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Business
{
    public interface ICategoryFactory
    {
        T CreateModel<T>(string name);

        Category CreateDataModel(CategoryViewModel viewModel);

        CategoryViewModel CreateViewModel(Category category);
    }
}
