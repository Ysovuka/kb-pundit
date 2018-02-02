using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pundit.Harbinger;
using Pundit.KnowledgeBase.WebCore.Domain;
using Pundit.KnowledgeBase.WebCore.Domain.Category;

namespace Pundit.KnowledgeBase.WebCore.Controllers
{
    [Produces("application/json")]
    [Route("api/Category")]
    public class CategoryController : Controller
    {
        private readonly CreateCategoryCommandHandler _createCategoryCommandHandler;
        public CategoryController(CreateCategoryCommandHandler createCategoryCommandHandler)
        {
            _createCategoryCommandHandler = createCategoryCommandHandler;
        }
    

        // TODO: Apply validation on the requests.
        [HttpPost]
        public async Task<long> CreateCategoryFromRequestAsync([FromBody] CreateCategoryRequest request)
        {
            AutoResetEvent resetEvent = new AutoResetEvent(false);
            long categoryId = -1;

            DomainEvents.Instance.Register("CreateCategoryFromRequestAsync", (CreateCategoryEvent e) =>
            {
                var category = e.Category;

                if (request.Id == category.RequestId)
                {
                    categoryId = e.Category.Id;

                    resetEvent.Set();
                }
            });
            
            var command = new CreateCategoryCommand(request.Id, request.Name, request.Icon, request.ParentId);

            await _createCategoryCommandHandler.ExecuteAsync(command);

            resetEvent.WaitOne(10000);

            DomainEvents.Instance.Unregister("CreateCategoryFromRequestAsync");

            return categoryId;
        }
    }
}