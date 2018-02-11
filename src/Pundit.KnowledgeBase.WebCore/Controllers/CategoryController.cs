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
    public class CategoryController : Controller
    {
        private readonly CategoryCommandHandler _categoryCommandHandler;
        private readonly CategoryQueryHandler _categoryQueryHandler;
        public CategoryController(CategoryCommandHandler categoryCommandHandler,
            CategoryQueryHandler categoryQueryHandler)
        {
            _categoryCommandHandler = categoryCommandHandler;
            _categoryQueryHandler = categoryQueryHandler;
        }
    

        // TODO: Apply validation to the request.
        [HttpPost]
        [Route("api/categories")]
        public async Task<CreateCategoryResult> CreateCategoryFromRequestAsync([FromBody] CreateCategoryRequest request)
        {
            AutoResetEvent resetEvent = new AutoResetEvent(false);
            CreateCategoryResult result = null;

            DomainEvents.Instance.Register("CreateCategoryFromRequestAsync", (CreateCategoryEvent e) =>
            {                
                result = new CreateCategoryResult(request.RequestId, e);

                if (result.IsSuccessful)
                    resetEvent.Set();
            });
            
            var command = new CreateCategoryCommand(request.RequestId, request.Name, request.Icon, request.ParentId);

            await _categoryCommandHandler.ExecuteAsync(command);

            resetEvent.WaitOne(10000);

            DomainEvents.Instance.Unregister("CreateCategoryFromRequestAsync");

            return result;
        }

        // TODO: Apply validation to the request.
        [HttpPatch]
        [Route("api/categories/{categoryId}")]
        public async Task<UpdateCategoryResult> UpdateCategoryFromRequestAsync([FromBody] UpdateCategoryRequest request)
        {
            AutoResetEvent resetEvent = new AutoResetEvent(false);
            UpdateCategoryResult result = null;

            DomainEvents.Instance.Register("UpdateCategoryFromRequestAsync", (UpdateCategoryEvent e) =>
            {
                result = new UpdateCategoryResult(request.RequestId, e);

                if (result.IsSuccessful)
                    resetEvent.Set();
            });

            var command = new UpdateCategoryCommand(request.RequestId, request.Id, request.Name, request.Icon);

            await _categoryCommandHandler.ExecuteAsync(command);

            resetEvent.WaitOne(10000);

            DomainEvents.Instance.Unregister("UpdateCategoryFromRequestAsync");

            return result;
        }
        
        [HttpGet]
        [Route("api/categories")]
        [ResponseCache(Duration = -1, NoStore = true)]
        public async Task<ReadAllCategoriesQueryResult> ReadAllCategoriesAsync([FromQuery] ReadAllCategoriesQuery query)
        {
            ReadAllCategoriesQueryResult result = await _categoryQueryHandler.ExecuteAsync(query);

            return result;
        }

        [HttpGet]
        [Route("api/categories/{categoryId}")]
        [ResponseCache(Duration = -1, NoStore = true)]
        public async Task<ReadCategoryQueryResult> ReadCategoryAsync([FromQuery] ReadCategoryQuery query)
        {
            ReadCategoryQueryResult result = await _categoryQueryHandler.ExecuteAsync(query);

            return result;
        }
    }
}