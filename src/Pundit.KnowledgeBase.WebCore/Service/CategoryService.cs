using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pundit.KnowledgeBase.WebCore.Business;
using Pundit.KnowledgeBase.WebCore.Debug;
using Pundit.KnowledgeBase.WebCore.ViewModels;

namespace Pundit.KnowledgeBase.WebCore.Service
{
    public class CategoryService : ICategoryService, IDisposable
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

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<long> CreateAsync(CategoryViewModel viewModel)
        {
            return await OnCreateAsync(viewModel);
        }

        public async Task<IEnumerable<CategoryViewModel>> ReadAllAsync()
        {
            return await OnReadAllAsync();
        }

        public async Task<CategoryViewModel> ReadAsync(long categoryId)
        {
            return await OnReadAsync(categoryId);
        }

        public async Task<CategoryViewModel> UpdateAsync(CategoryViewModel viewModel)
        {
            return await OnUpdateAsync(viewModel);
        }

        public async Task<CategoryViewModel> DeleteAsync(long categoryId)
        {
            return await OnDeleteAsync(categoryId);
        }

        protected virtual async Task<long> OnCreateAsync(CategoryViewModel viewModel)
        {
            var category = _categoryFactory.CreateDataModel(viewModel);

            return await _categoryBusinessLayer.CreateAsync(category);
        }

        protected virtual async Task<IEnumerable<CategoryViewModel>> OnReadAllAsync()
        {
            var results = await _categoryBusinessLayer.ReadAllAsync();

            var viewModels = _categoryFactory.CreateViewModels(results);

            return viewModels;
        }

        protected virtual async Task<CategoryViewModel> OnReadAsync(long categoryId)
        {
            var result = await _categoryBusinessLayer.ReadAsync(categoryId);

            var viewModel = _categoryFactory.CreateViewModel(result);

            return viewModel;
        }

        protected virtual async Task<CategoryViewModel> OnUpdateAsync(CategoryViewModel viewModel)
        {
            var category = _categoryFactory.CreateDataModel(viewModel);

            var result = await _categoryBusinessLayer.UpdateAsync(category);

            var resultViewModel = _categoryFactory.CreateViewModel(result);

            return resultViewModel;
        }

        protected virtual async Task<CategoryViewModel> OnDeleteAsync(long categoryId)
        {
            var result = await _categoryBusinessLayer.DeleteAsync(categoryId);

            var viewModel = _categoryFactory.CreateViewModel(result);

            return viewModel;
        }
    }
}
