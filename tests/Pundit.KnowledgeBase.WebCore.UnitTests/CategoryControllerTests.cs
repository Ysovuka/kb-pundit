using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pundit.KnowledgeBase.WebCore.Controllers;
using Pundit.KnowledgeBase.WebCore.Domain;
using Pundit.KnowledgeBase.WebCore.Domain.Category;
using System.Linq;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.UnitTests
{
    [TestClass]
    public class CategoryControllerTests
    {
        [TestMethod]
        public async Task Create_Category_CreatesCategoryFromRequestAndReturnsResult()
        {
            // Arrange
            long? parentId = null;
            var request = new CreateCategoryRequest("Test Category", "fa-exclamation", parentId);
            var databaseContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
                                                .UseInMemoryDatabase("kb-pundit")
                                                .Options;
            var databaseContext = new DatabaseContext(databaseContextOptions);
            var categoryCommandHandler = new CategoryCommandHandler(databaseContext);
            var categoryQueryHandler = new CategoryQueryHandler(databaseContext);

            var controller = new CategoryController(categoryCommandHandler, categoryQueryHandler);
            

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
            var request = new UpdateCategoryRequest(id, name, icon);
            var databaseContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
                                                .UseInMemoryDatabase("kb-pundit")
                                                .Options;
            var databaseContext = new DatabaseContext(databaseContextOptions);
            var categoryCommandHandler = new CategoryCommandHandler(databaseContext);
            var categoryQueryHandler = new CategoryQueryHandler(databaseContext);

            var controller = new CategoryController(categoryCommandHandler, categoryQueryHandler);

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
            var query = new ReadAllCategoriesQuery();
            var databaseContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
                                                .UseInMemoryDatabase("kb-pundit")
                                                .Options;
            var databaseContext = new DatabaseContext(databaseContextOptions);
            var categoryCommandHandler = new CategoryCommandHandler(databaseContext);
            var categoryQueryHandler = new CategoryQueryHandler(databaseContext);

            var controller = new CategoryController(categoryCommandHandler, categoryQueryHandler);

            // Act
            var categoryResult = await controller.ReadAllCategoriesAsync(query);

            // Assert
            Assert.IsNotNull(categoryResult);
            Assert.AreEqual(1, categoryResult.Size);
        }

        [TestMethod]
        [DataRow(1, false)]
        public async Task Read_Category_ReturnsResultFromQuery(long categoryId, bool isNull)
        {
            var query = new ReadCategoryQuery(categoryId);
            var databaseContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
                                                .UseInMemoryDatabase("kb-pundit")
                                                .Options;
            var databaseContext = new DatabaseContext(databaseContextOptions);
            var categoryCommandHandler = new CategoryCommandHandler(databaseContext);
            var categoryQueryHandler = new CategoryQueryHandler(databaseContext);

            var controller = new CategoryController(categoryCommandHandler, categoryQueryHandler);

            // Act
            var categoryResult = await controller.ReadCategoryAsync(query);

            // Assert
            Assert.AreEqual(isNull, categoryResult == null);
        }
    }
}
