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
            Guard.Assert(() => categoryService == null, new ArgumentNullException("CategoryService", "CategoryService cannot be null."));

            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<long> CreateAsync(CategoryViewModel viewModel)
        {
            return await _categoryService.CreateAsync(viewModel);
        }

        [HttpGet]
        public async Task<CategoryViewModel> ReadAsync(long categoryId)
        {
            return await _categoryService.ReadAsync(categoryId);
        }
    }
}