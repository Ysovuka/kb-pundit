using System;
using Pundit.KnowledgeBase.WebCore.Data;
using Pundit.KnowledgeBase.WebCore.ViewModels;

namespace Pundit.KnowledgeBase.WebCore.Business
{
    public class CategoryFactory : ICategoryFactory
    {
        public T CreateModel<T>(string name)
        {
            dynamic model = Activator.CreateInstance<T>();
            model.Name = name;

            return model;
        }

        public Category CreateDataModel(CategoryViewModel viewModel)
        {
            return new Category
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
            };
        }

        public CategoryViewModel CreateViewModel(Category category)
        {
            return new CategoryViewModel(category);
        }
    }
}
