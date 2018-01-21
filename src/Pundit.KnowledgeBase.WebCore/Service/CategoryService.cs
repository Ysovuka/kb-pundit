using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pundit.KnowledgeBase.WebCore.Business;
using Pundit.KnowledgeBase.WebCore.Debug;
using Pundit.KnowledgeBase.WebCore.ViewModels;

namespace Pundit.KnowledgeBase.WebCore.Service
{
    public class CategoryService : ICategoryService
    {  
        private ICategoryBusinessLayer _categoryBusinessLayer;
        private ICategoryFactory _categoryFactory;

        public CategoryService(ICategoryBusinessLayer categoryBusinessLayer,
            ICategoryFactory categoryFactory)
        {
            Guard.Assert(() => categoryBusinessLayer == null, new ArgumentNullException("Category Business Layer", "CategoryBusinessLayer cannot be null."));
            Guard.Assert(() => categoryFactory == null, new ArgumentNullException("Category Factory", "CategoryFactory cannot be null."));

            _categoryBusinessLayer = categoryBusinessLayer;
            _categoryFactory = categoryFactory;
        }

        public async Task<long> CreateAsync(CategoryViewModel viewModel)
        {
            return await OnCreateAsync(viewModel);
        }

        public async Task<CategoryViewModel> ReadAsync(long categoryId)
        {
            return await OnReadAsync(categoryId);
        }

        protected virtual async Task<long> OnCreateAsync(CategoryViewModel viewModel)
        {
            var category = _categoryFactory.CreateDataModel(viewModel);

            return await _categoryBusinessLayer.CreateAsync(category);
        }

        protected virtual async Task<CategoryViewModel> OnReadAsync(long categoryId)
        {
            var result = await _categoryBusinessLayer.ReadAsync(categoryId);

            var viewModel = _categoryFactory.CreateViewModel(result);

            return viewModel;
        }
    }
}
