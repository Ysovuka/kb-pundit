using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<CategoryViewModel> CreateViewModels(IEnumerable<Category> categories)
        {
            return categories.Select(category => new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
            });
        }
    }
}
