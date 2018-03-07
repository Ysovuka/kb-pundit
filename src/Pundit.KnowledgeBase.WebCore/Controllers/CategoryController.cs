using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pundit.Circuit;
using Pundit.Harbinger;
using Pundit.KnowledgeBase.WebCore.Domain.Category;

namespace Pundit.KnowledgeBase.WebCore.Controllers
{
    [Produces("application/json")]
    public class CategoryController : Controller
    {
        private readonly ICircuitBreaker _circuitBreaker;
        private readonly DomainEvents _domainEvents = DomainEvents.Instance;
        private readonly CategoryCommandHandler _categoryCommandHandler;
        private readonly CategoryQueryHandler _categoryQueryHandler;

        public CategoryController(ICircuitBreaker circuitBreaker,
            CategoryCommandHandler categoryCommandHandler,
            CategoryQueryHandler categoryQueryHandler)
        {
            _circuitBreaker = circuitBreaker;
            _categoryCommandHandler = categoryCommandHandler;
            _categoryQueryHandler = categoryQueryHandler;
        }


        // TODO: Apply validation to the request.
        [HttpPost]
        [Route("api/categories")]
        public async Task<CreateCategoryResult> CreateCategoryFromRequestAsync(
            [FromBody] CreateCategoryRequest request,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            AutoResetEvent resetEvent = new AutoResetEvent(false);
            CreateCategoryResult result = null;

            _domainEvents.Register("CreateCategoryFromRequestAsync", (CreateCategoryEvent e) =>
            {
                result = new CreateCategoryResult(request.RequestId, e);

                if (result.IsSuccessful)
                    resetEvent.Set();
            });

            var command = request.GetCommand();

            await _circuitBreaker.ExecuteAsync(async () => 
                await _categoryCommandHandler.ExecuteAsync(command, cancellationToken)
            , cancellationToken);

            resetEvent.WaitOne(10000);

            _domainEvents.Unregister("CreateCategoryFromRequestAsync");

            return result;
        }

        // TODO: Apply validation to the request.
        [HttpPatch]
        [Route("api/categories/{categoryId}")]
        public async Task<UpdateCategoryResult> UpdateCategoryFromRequestAsync(
            [FromBody] UpdateCategoryRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            AutoResetEvent resetEvent = new AutoResetEvent(false);
            UpdateCategoryResult result = null;

            _domainEvents.Register("UpdateCategoryFromRequestAsync", (UpdateCategoryEvent e) =>
            {
                result = new UpdateCategoryResult(request.RequestId, e);

                if (result.IsSuccessful)
                    resetEvent.Set();
            });

            var command = request.GetCommand();

            await _categoryCommandHandler.ExecuteAsync(command, cancellationToken);

            resetEvent.WaitOne(10000);

            _domainEvents.Unregister("UpdateCategoryFromRequestAsync");

            return result;
        }

        [HttpGet]
        [Route("api/categories")]
        [ResponseCache(Duration = -1, NoStore = true)]
        public async Task<ReadAllCategoriesQueryResult> ReadAllCategoriesAsync(
            [FromQuery] ReadAllCategoriesQuery query, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _categoryQueryHandler.ExecuteAsync(query, cancellationToken);
        }

        [HttpGet]
        [Route("api/categories/{categoryId}")]
        [ResponseCache(Duration = -1, NoStore = true)]
        public async Task<ReadCategoryQueryResult> ReadCategoryAsync(
            [FromQuery] ReadCategoryQuery query, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _categoryQueryHandler.ExecuteAsync(query, cancellationToken);
        }

        [HttpGet]
        [Route("api/categories/next")]
        [ResponseCache(Duration = -1, NoStore = true)]
        public async Task<GetNextCategoryIdQueryResult> GetNextCategoryIdAsync(
            [FromQuery] GetNextCategoryIdQuery query, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _categoryQueryHandler.ExecuteAsync(query, cancellationToken);
        }
    }
}