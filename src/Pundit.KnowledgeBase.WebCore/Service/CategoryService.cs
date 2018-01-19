using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pundit.KnowledgeBase.WebCore.Business;
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
            _categoryBusinessLayer = categoryBusinessLayer;
            _categoryFactory = categoryFactory;
        }

        public async Task<long> CreateAsync(CategoryViewModel viewModel)
        {
            var category = _categoryFactory.CreateDataModel(viewModel);

            return await _categoryBusinessLayer.CreateAsync(category);
        }
    }
}
