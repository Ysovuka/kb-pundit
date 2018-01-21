using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Pundit.KnowledgeBase.WebCore.Business;
using Pundit.KnowledgeBase.WebCore.Controllers;
using Pundit.KnowledgeBase.WebCore.Data;
using Pundit.KnowledgeBase.WebCore.Service;
using Pundit.KnowledgeBase.WebCore.ViewModels;
using System;
using System.Threading.Tasks;

/// <summary>
/// Unit Tests using http://nsubstitute.github.io/
/// </summary>
namespace Pundit.KnowledgeBase.WebCore.UnitTests
{
    [TestClass]
    public class CategoryControllerTests
    {
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public async Task Create_NewCategory_ReturnsId(long categoryId)
        {
            var controller = Substitute.For<CategoryController>();
            controller.CategoryService = Substitute.For<ICategoryService>();

            var category = MakeInstance<CategoryViewModel>();
            category.Id = categoryId;
            category.Name = "Test Category";


            await controller.CreateAsync(category);
            controller.CreateAsync(category).Returns(categoryId);
            

            await controller.Received().CreateAsync(category);
            Assert.AreEqual(categoryId, await controller.CreateAsync(category));
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public async Task Read_Category_NotNull(long categoryId)
        {
            var controller = Substitute.For<CategoryController>();
            controller.CategoryService = Substitute.For<ICategoryService>();

            await controller.ReadAsync(categoryId);
            controller.ReadAsync(categoryId).Returns(new CategoryViewModel
            {
                Id = categoryId,
            });
            
            await controller.Received().ReadAsync(categoryId);
            Assert.IsNotNull(await controller.ReadAsync(categoryId));
        }

        private T MakeInstance<T>()
        {
            return Activator.CreateInstance<T>();
        }
    }
}
