using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pundit.KnowledgeBase.WebCore.Controllers;
using Pundit.KnowledgeBase.WebCore.Domain;
using Pundit.KnowledgeBase.WebCore.Domain.Category;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.UnitTests
{
    [TestClass]
    public class CategoryControllerTests
    {
        [TestMethod]
        public async Task Create_Category_CreatesCategoryFromRequestAndReturnsId()
        {
            // Arrange
            long? parentId = null;
            var request = new CreateCategoryRequest("Test Category", "fa-exclamation", parentId);
            var databaseContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
                                                .UseInMemoryDatabase("Create_Category_CreatesCategoryFromRequestAndReturnsId")
                                                .Options;
            var datbaseContext = new DatabaseContext(databaseContextOptions);
            var createCategoryCommandHandler = new CreateCategoryCommandHandler(datbaseContext);

            var controller = new CategoryController(createCategoryCommandHandler);

            // Act
            long categoryId = await controller.CreateCategoryFromRequestAsync(request);

            // Assert
            Assert.AreEqual(1, categoryId);

        }
    }
}
