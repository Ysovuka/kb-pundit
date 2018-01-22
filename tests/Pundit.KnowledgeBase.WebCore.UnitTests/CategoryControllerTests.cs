using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Pundit.KnowledgeBase.WebCore.Controllers;
using Pundit.KnowledgeBase.WebCore.Service;
using Pundit.KnowledgeBase.WebCore.ViewModels;
using System;
using System.Collections.Generic;
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
        public async Task Create_Category_CallReceived(long categoryId)
        {
            var controller = Substitute.For<CategoryController>();
            controller.CategoryService = Substitute.For<ICategoryService>();

            var category = new CategoryViewModel
            {
                Id = categoryId,
                Name = "Test Category"
            };


            await controller.CreateAsync(category);
            controller.CreateAsync(category).Returns(categoryId);
            

            await controller.Received().CreateAsync(category);
            Assert.AreEqual(categoryId, await controller.CreateAsync(category));
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public async Task Read_Category_CallReceived(long categoryId)
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

        [TestMethod]
        public async Task Read_Categories_CallReceived()
        {
            var controller = Substitute.For<CategoryController>();
            controller.CategoryService = Substitute.For<ICategoryService>();

            await controller.ReadAllAsync();
            controller.ReadAllAsync().Returns(new List<CategoryViewModel>());

            await controller.Received().ReadAllAsync();
            Assert.IsNotNull(await controller.ReadAllAsync());
        }

        [TestMethod]
        public async Task Update_Category_CallReceived()
        {
            var controller = Substitute.For<CategoryController>();
            controller.CategoryService = Substitute.For<ICategoryService>();

            var categoryViewModel = new CategoryViewModel
            {
                Id = 1,
                Name = "Updated"
            };

            await controller.UpdateAsync(categoryViewModel);
            controller.UpdateAsync(categoryViewModel).Returns(categoryViewModel);

            await controller.Received().UpdateAsync(categoryViewModel);
            Assert.IsNotNull(await controller.UpdateAsync(categoryViewModel));
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public async Task Delete_Category_CallReceived(long categoryId)
        {
            var controller = Substitute.For<CategoryController>();
            controller.CategoryService = Substitute.For<ICategoryService>();

            await controller.DeleteAsync(categoryId);
            controller.DeleteAsync(categoryId).Returns(new CategoryViewModel
            {
                Id = categoryId,
            });

            await controller.Received().DeleteAsync(categoryId);
            Assert.IsNotNull(await controller.DeleteAsync(categoryId));
        }
    }
}
