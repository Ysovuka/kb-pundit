using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pundit.KnowledgeBase.WebCore.Commands;
using Pundit.KnowledgeBase.WebCore.Data;
using Pundit.KnowledgeBase.WebCore.Debug;
using Pundit.KnowledgeBase.WebCore.Query;
using Pundit.KnowledgeBase.WebCore.Services;
using Pundit.KnowledgeBase.WebCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.UnitTests
{
    [TestClass]
    public class CategoryServiceTests
    {
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public async Task Service_CreateAsync_Category_ReturnsWithIdAndNullParent(long categoryId)
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<DatabaseRepository>()
                .UseInMemoryDatabase("CategoryService_InMemory_Database");

            var service = new CategoryService(builder);
            var categoryViewModel = new CategoryViewModel($"Test Category {categoryId}", "");

            var createCategoryCommand = categoryViewModel.Cast<CategoryViewModel, CreateCategoryCommand>();

            // Act
            var result = await service.CreateAsync(createCategoryCommand);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(categoryId, result);
        }

        [TestMethod]
        public async Task Service_ReadAsync_Category_ReturnsWithIdAndNullParent()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<DatabaseRepository>()
                .UseInMemoryDatabase("CategoryService_InMemory_Database");

            var service = new CategoryService(builder);

            var getCategoryListQuery = new ReadAllCategoriesQuery();

            // Act
            var result = await service.ReadAllAsync(getCategoryListQuery);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() >= 1);
        }

        [TestMethod]
        [DataRow(3, null)]
        [DataRow(4, (long)3)]
        public async Task Service_CreateAsync_Category_ReturnsWithIdAndParent(long categoryId, long? parentId)
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<DatabaseRepository>()
                            .UseInMemoryDatabase("CategoryService_InMemory_Database");

            var service = new CategoryService(builder);
            var categoryViewModel = new CategoryViewModel($"Test Category {categoryId}", "", parentId);

            var createCategoryCommand = categoryViewModel.Cast<CategoryViewModel, CreateCategoryCommand>();

            // Act
            var result = await service.CreateAsync(createCategoryCommand);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(categoryId, result);
        }

        [TestMethod]
        public async Task Service_ReadAsync_Category_ReturnsWithIdAndParent()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<DatabaseRepository>()
                .UseInMemoryDatabase("CategoryService_InMemory_Database");

            var service = new CategoryService(builder);

            var getCategoryListQuery = new ReadAllCategoriesQuery()
                                            .IncludeChildren();

            // Act
            var result = await service.ReadAllAsync(getCategoryListQuery);
            var category3 = result.FirstOrDefault(c => c.Id == 3);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() >= 1);
            Assert.IsNotNull(category3);
            Assert.IsTrue(category3.Children.Count() >= 1);
        }

        //[TestMethod]
        //[DataRow(3, (long)1)]
        //[DataRow(5, (long)4)]
        //public async Task Service_UpdateAsync_Category_ReturnsWithChangedData(long categoryId, long? parentId)
        //{
        //    // Arrange
        //    var repository = await MockCategoryRepository.GeneratePrepopulatedRepositoryAsync();
        //    var service = new CategoryService(repository);
        //    var categoryViewModel = new CategoryViewModel($"Updated Test Category {categoryId}", "", parentId)
        //    {
        //        Id = categoryId,
        //    };

        //    // Act
        //    var result = await service.UpdateAsync(categoryViewModel);

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(categoryId, result.Id, $"Category Id failed assertion. {categoryId} == {result.Id}");
        //    Assert.AreEqual(parentId, result.ParentId, "Parent Id failed assertion.");
        //    Assert.AreEqual($"Updated Test Category {categoryId}", result.Name);
        //    Assert.AreEqual($"updated-test-category-{ categoryId}", result.SefName);
        //    Assert.AreEqual("", result.Icon);
        //}

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        public async Task Service_ReadAsync_Category_ReturnsWithData(long categoryId)
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<DatabaseRepository>()
                .UseInMemoryDatabase("CategoryService_InMemory_Database");

            var service = new CategoryService(builder);

            var getCategoryQuery = new ReadCategoryQuery()
                                            .ReadById(categoryId);

            // Act
            var result = await service.ReadAsync(getCategoryQuery);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(categoryId, result.Id, $"Category Id failed assertion. {categoryId} == {result.Id}");
            Assert.IsTrue(!string.IsNullOrEmpty(result.Name));
            Assert.IsTrue(!string.IsNullOrEmpty(result.SefName));
            Assert.IsTrue(string.IsNullOrEmpty(result.Icon));
        }

        //[TestMethod]
        //[DataRow(1)]
        //[DataRow(2)]
        //[DataRow(3)]
        //[DataRow(4)]
        //[DataRow(5)]
        //public async Task Service_DeleteAsync_Category_ReturnsWithDeletedDataAndThrowsErrorOnNextRead(long categoryId)
        //{
        //    // Arrange
        //    var repository = await MockCategoryRepository.GeneratePrepopulatedRepositoryAsync();
        //    var service = new CategoryService(repository);

        //    // Act
        //    var result = await service.DeleteAsync(categoryId);

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.ThrowsException<ArgumentException>(() =>
        //    {
        //        try
        //        {
        //            service.ReadAsync(categoryId).Wait();
        //        }
        //        catch(AggregateException e)
        //        {
        //            throw e.InnerException;
        //        }
        //    });
        //}

        //[TestMethod]
        //[DataRow(3)]
        //public async Task Service_DeleteAsync_Category_ReturnsWithDeletedData(long categoryId)
        //{

        //    // Arrange
        //    var builder = new DbContextOptionsBuilder<DatabaseRepository>()
        //        .UseInMemoryDatabase("CategoryService_InMemory_Database");
        //    var repository = new Repository.CategoryRepository(builder);
        //    var service = new CategoryService(repository);

        //    // Act
        //    var beforeCount = await service.CountAsync();
        //    var result = await service.DeleteAsync(categoryId);
        //    var afterCount = await service.CountAsync();

        //    // Assert
        //    Assert.AreNotEqual(beforeCount, afterCount);
        //    Assert.AreEqual(2, afterCount);
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(1, result.Children.Count());
        //    Assert.ThrowsException<ArgumentException>(() =>
        //    {
        //        try
        //        {
        //            service.ReadAsync(categoryId).Wait();
        //        }
        //        catch (AggregateException e)
        //        {
        //            throw e.InnerException;
        //        }
        //    });
        //}

        //[TestMethod]
        //[DataRow(1)]
        //public async Task Service_CreateAsync_Article_ReturnsNewArticleAssociatedWithCategory(long categoryId)
        //{
        //    // Arrange
        //    var builder = new DbContextOptionsBuilder<DatabaseRepository>()
        //        .UseInMemoryDatabase("CategoryService_InMemory_Database");

        //    var categoryRepository = new CategoryRepository(builder);
        //    var categoryService = new CategoryService(categoryRepository);

        //    var articleRepository = new ArticleRepository(builder);
        //    var articleService = new ArticleService(categoryService, articleRepository);

        //    string title = "Test Article";
        //    string content = "This is article was created during unit testing.";
        //    DateTimeOffset publishDate = DateTimeOffset.Now.AddMonths(1);
        //    DateTimeOffset publishExpiration = publishDate.AddYears(1);

        //    var viewModel = new ArticleViewModel(categoryId, title, content, publishDate, publishExpiration);


        //    // Act
        //    var result = await articleService.CreateArticleAsync(viewModel);
        //}
    }
}
