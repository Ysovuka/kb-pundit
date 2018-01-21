using Pundit.KnowledgeBase.WebCore.ViewModels;
using System.Threading.Tasks;

namespace Pundit.KnowledgeBase.WebCore.Service
{
    public interface ICategoryService
    {
        Task<long> CreateAsync(CategoryViewModel viewModel);
        Task<CategoryViewModel> ReadAsync(long categoryId);
        Task<CategoryViewModel> UpdateAsync(CategoryViewModel viewModel);
        Task<CategoryViewModel> DeleteAsync(long categoryId);
    }
}
