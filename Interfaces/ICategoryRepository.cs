using tajmautAPI.Models;
using tajmautAPI.Models.ModelsREQUEST;

namespace tajmautAPI.Interfaces
{
    public interface ICategoryRepository
    {
        Task<CategoryEvent> CreateCategory(CategoryREQUEST request);
        Task<List<CategoryEvent>> GetAllCategories();
        Task<CategoryEvent> GetCategoryById(int id);
        Task<bool> DeleteCategory(CategoryEvent cat);
        Task<CategoryEvent> UpdateCategory(CategoryEvent category,CategoryREQUEST request);
    }
}
