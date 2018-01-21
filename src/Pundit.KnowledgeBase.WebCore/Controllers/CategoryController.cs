using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pundit.KnowledgeBase.WebCore.Debug;
using Pundit.KnowledgeBase.WebCore.Service;
using Pundit.KnowledgeBase.WebCore.ViewModels;

namespace Pundit.KnowledgeBase.WebCore.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;
        public ICategoryService CategoryService { get { return _categoryService; } set { _categoryService = value; } }

        public CategoryController() { }
        public CategoryController(ICategoryService categoryService)
        {
            Guard.Assert(() => categoryService == null, new ArgumentNullException("Category Service", "CategoryService cannot be null."));

            _categoryService = categoryService;
        }

        [HttpPost]
        public virtual async Task<long> CreateAsync([FromBody] CategoryViewModel viewModel)
        {
            Guard.Assert(() => CategoryService == null, new ArgumentNullException("Category Service", "CategoryService cannot be null."));
            return await CategoryService.CreateAsync(viewModel);
        }

        [HttpGet]
        public virtual async Task<IEnumerable<CategoryViewModel>> ReadAllAsync()
        {
            Guard.Assert(() => CategoryService == null, new ArgumentNullException("Category Service", "CategoryService cannot be null."));
            return await CategoryService.ReadAllAsync();
        }

        [HttpGet]
        public virtual async Task<CategoryViewModel> ReadAsync(long categoryId)
        {
            Guard.Assert(() => CategoryService == null, new ArgumentNullException("Category Service", "CategoryService cannot be null."));
            return await CategoryService.ReadAsync(categoryId);
        }

        [HttpPatch]
        public virtual async Task<CategoryViewModel> UpdateAsync([FromBody] CategoryViewModel viewModel)
        {
            Guard.Assert(() => CategoryService == null, new ArgumentNullException("Category Service", "CategoryService cannot be null."));
            return await CategoryService.UpdateAsync(viewModel);
        }

        [HttpDelete]
        public virtual async Task<CategoryViewModel> DeleteAsync(long categoryId)
        {
            Guard.Assert(() => CategoryService == null, new ArgumentNullException("Category Service", "CategoryService cannot be null."));
            return await CategoryService.DeleteAsync(categoryId);
        }
    }
}