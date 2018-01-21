using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pundit.KnowledgeBase.WebCore.Business;
using Pundit.KnowledgeBase.WebCore.Controllers;
using Pundit.KnowledgeBase.WebCore.Data;
using Pundit.KnowledgeBase.WebCore.Service;
using Pundit.KnowledgeBase.WebCore.ViewModels;
using System;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.UnitTests
{
    [TestClass]
    public class CategoryControllerTests
    {
        [TestMethod]
        public async Task Create_NewCategory_ReturnsId()
        {
            var controller = MakeCategoryController("Create_NewCategory_ReturnsId");
            var category = MakeInstance<CategoryViewModel>();
            category.Name = "Test Category";

            long resultId = await controller.CreateAsync(category);

            Assert.IsTrue(resultId > 0);
        }

        [TestMethod]
        public async Task Read_Category_NotNull()
        {
            var controller = MakeCategoryController("Read_Category_NotNull");
            long categoryId = 1;

            CategoryViewModel resultCategory = await controller.ReadAsync(categoryId);

            Assert.IsTrue(resultCategory != null);
        }

        private CategoryController MakeCategoryController(string databaseName)
        {
            var dbContextOptions = new DbContextOptionsBuilder<KnowledgeBaseContext>()
                .UseInMemoryDatabase(databaseName);
            ICategoryBusinessLayer categoryBusinessLayer = new AlwaysNonNullCategoryBusinessLayer(dbContextOptions.Options);

            ICategoryFactory categoryFactory = new CategoryFactory();

            ICategoryService categoryService = new CategoryService(categoryBusinessLayer, categoryFactory);

            return new CategoryController(categoryService);
        }

        private T MakeInstance<T>()
        {
            return Activator.CreateInstance<T>();
        }
    }

    public class AlwaysNonNullCategoryBusinessLayer : CategoryBusinessLayer
    {
        public AlwaysNonNullCategoryBusinessLayer(DbContextOptions<KnowledgeBaseContext> options)
            : base(options)
        {

        }

        protected override async Task<Category> OnReadAsync(long categoryId)
        {
            return new Category
            {
                Id = categoryId,
                Name = "Non-Null Category"
            };
        }
    }
}
