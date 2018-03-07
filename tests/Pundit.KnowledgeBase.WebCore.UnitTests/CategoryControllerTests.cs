using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pundit.Circuit;
using Pundit.KnowledgeBase.WebCore.Controllers;
using Pundit.KnowledgeBase.WebCore.Domain;
using Pundit.KnowledgeBase.WebCore.Domain.Category;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.UnitTests
{
    [TestClass]
    public class CategoryControllerTests
    {
        [TestMethod]
        [DataRow(1)]
        public async Task Create_Category_CreatesCategoryFromRequestAndReturnsResult(long id)
        {
            // Arrange
            long? parentId = null;
            var request = new CreateCategoryRequest(id, "Test Category", "fa-exclamation", parentId);
            var databaseContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
                                                .UseInMemoryDatabase("kb-pundit")
                                                .Options;
            var databaseContext = new DatabaseContext(databaseContextOptions);
            var categoryCommandHandler = new CategoryCommandHandler(databaseContext);
            var categoryQueryHandler = new CategoryQueryHandler(databaseContext);
            var circuitBreaker = new CircuitBreaker(TaskScheduler.Default, 5, TimeSpan.FromMilliseconds(1000), TimeSpan.FromMilliseconds(1000));

            var controller = new CategoryController(circuitBreaker, categoryCommandHandler, categoryQueryHandler);
            

            // Act
            var categoryResult = await controller.CreateCategoryFromRequestAsync(request);

            // Assert
            Assert.AreEqual(1, categoryResult.Id);

        }

        [TestMethod]
        [DataRow(1, "Updated Category", "fa-bug")]
        public async Task Update_Category_UpdatesCategoryFromRequestAndReturnsResult
            (long id, string name, string icon)
        {
            // Arrange
            var request = new UpdateCategoryRequest(id, name, icon);
            var databaseContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
                                                .UseInMemoryDatabase("kb-pundit")
                                                .Options;
            var databaseContext = new DatabaseContext(databaseContextOptions);
            var categoryCommandHandler = new CategoryCommandHandler(databaseContext);
            var categoryQueryHandler = new CategoryQueryHandler(databaseContext);
            var circuitBreaker = new CircuitBreaker(TaskScheduler.Default, 5, TimeSpan.FromMilliseconds(10), TimeSpan.FromMilliseconds(1000));

            var controller = new CategoryController(circuitBreaker, categoryCommandHandler, categoryQueryHandler);

            // Act
            var categoryResult = await controller.UpdateCategoryFromRequestAsync(request);

            // Assert
            Assert.AreEqual(1, categoryResult.Id);
            Assert.AreEqual(name, categoryResult.Name);
            Assert.AreEqual(icon, categoryResult.Icon);
        }

        [TestMethod]
        public async Task ReadAll_Category_ReturnsResultFromQuery()
        {
            // Arrange
            var query = new ReadAllCategoriesQuery();
            var databaseContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
                                                .UseInMemoryDatabase("kb-pundit")
                                                .Options;
            var databaseContext = new DatabaseContext(databaseContextOptions);
            var categoryCommandHandler = new CategoryCommandHandler(databaseContext);
            var categoryQueryHandler = new CategoryQueryHandler(databaseContext);
            var circuitBreaker = new CircuitBreaker(TaskScheduler.Default, 5, TimeSpan.FromMilliseconds(10), TimeSpan.FromMilliseconds(1000));

            var controller = new CategoryController(circuitBreaker, categoryCommandHandler, categoryQueryHandler);

            // Act
            var categoryResult = await controller.ReadAllCategoriesAsync(query);

            // Assert
            Assert.IsNotNull(categoryResult, "Result is null.");
            Assert.AreEqual(1, categoryResult.Size);
        }

        [TestMethod]
        [DataRow(1, false)]
        public async Task Read_Category_ReturnsResultFromQuery(long categoryId, bool isNull)
        {
            // Arrange
            var query = new ReadCategoryQuery(categoryId);
            var databaseContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
                                                .UseInMemoryDatabase("kb-pundit")
                                                .Options;
            var databaseContext = new DatabaseContext(databaseContextOptions);
            var categoryCommandHandler = new CategoryCommandHandler(databaseContext);
            var categoryQueryHandler = new CategoryQueryHandler(databaseContext);
            var circuitBreaker = new CircuitBreaker(TaskScheduler.Default, 5, TimeSpan.FromMilliseconds(10), TimeSpan.FromMilliseconds(1000));

            var controller = new CategoryController(circuitBreaker, categoryCommandHandler, categoryQueryHandler);

            // Act
            var categoryResult = await controller.ReadCategoryAsync(query);

            // Assert
            Assert.AreEqual(isNull, categoryResult == null);
        }

        [TestMethod]
        [DataRow(2)]
        public async Task Get_Category_ReturnsNextCategoryId(long expectedId)
        {
            // Arrange
            var query = new GetNextCategoryIdQuery();
            var databaseContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
                                    .UseInMemoryDatabase("kb-pundit")
                                    .Options;
            var databaseContext = new DatabaseContext(databaseContextOptions);
            var categoryCommandHandler = new CategoryCommandHandler(databaseContext);
            var categoryQueryHandler = new CategoryQueryHandler(databaseContext);
            var circuitBreaker = new CircuitBreaker(TaskScheduler.Default, 5, TimeSpan.FromMilliseconds(10), TimeSpan.FromMilliseconds(1000));

            var controller = new CategoryController(circuitBreaker, categoryCommandHandler, categoryQueryHandler);

            // Act
            var result = await controller.GetNextCategoryIdAsync(query);

            // Assert
            Assert.AreEqual(expectedId, result.Id);
        }
    }
}
