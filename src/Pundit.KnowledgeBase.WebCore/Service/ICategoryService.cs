using Pundit.KnowledgeBase.WebCore.ViewModels;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Service
{
    public interface ICategoryService
    {
        Task<long> CreateAsync(CategoryViewModel viewModel);
    }
}
